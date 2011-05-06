public static class GlobalMembersANIMLIB
{

//****************************************************************************
//
//      ANIM_LoadAnim ()
//
//****************************************************************************


	//****************************************************************************
	//
	//      ANIM_LoadAnim ()
	//
	// Setup internal anim data structure
	//
	//****************************************************************************

	public static void ANIM_LoadAnim(ref string buffer)
	   {
	   uint16 i;
	   int32 size;

	   if (!Anim_Started)
		   Anim_Started = true;

	   anim.buffer = buffer;
	   anim.curlpnum = 0xffff;
	   anim.currentframe = -1;
	   size = sizeof(lpfileheader);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	   memcpy(anim.lpheader, buffer, size);
	   buffer += size+128;
	   // load the color palette
	   for (i = 0; i < 768; i += 3)
		  {
		  anim.pal[i+2] = buffer ++;
		  anim.pal[i+1] = buffer ++;
		  anim.pal[i] = buffer ++;
		  buffer = buffer.Substring(1);
		  }
			// read in large page descriptors
	   size = sizeof(anim.LpArray);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
	   memcpy(anim.LpArray, buffer, size);
	   }

//****************************************************************************
//
//      ANIM_FreeAnim ()
//
//****************************************************************************


	//****************************************************************************
	//
	//      ANIM_FreeAnim ()
	//
	// Free up internal anim data structure
	//
	//****************************************************************************

	public static void ANIM_FreeAnim()
	   {
	   if (Anim_Started)
		  {
	//      SafeFree(anim);
		  Anim_Started = false;
		  }
	   }

//****************************************************************************
//
//      ANIM_NumFrames ()
//
//****************************************************************************


	//****************************************************************************
	//
	//      ANIM_NumFrames ()
	//
	// returns the number of frames in the current anim
	//
	//****************************************************************************

	public static int32 ANIM_NumFrames()
	   {
	   GlobalMembersANIMLIB.CheckAnimStarted("NumFrames");
	   return anim.lpheader.nRecords;
	   }

//****************************************************************************
//
//      ANIM_DrawFrame ()
//
//****************************************************************************


	//****************************************************************************
	//
	//      ANIM_DrawFrame ()
	//
	// Draw the frame to a returned buffer
	//
	//****************************************************************************

	public static byte ANIM_DrawFrame(int32 framenumber)
	   {
	   int32 cnt;

	   GlobalMembersANIMLIB.CheckAnimStarted("DrawFrame");
	   if ((anim.currentframe != -1) && (anim.currentframe<=framenumber))
		  {
		  for (cnt = anim.currentframe; cnt < framenumber; cnt++)
			  GlobalMembersANIMLIB.drawframe(cnt);
		  }
	   else
		  {
		  for (cnt = 0; cnt < framenumber; cnt++)
			 GlobalMembersANIMLIB.drawframe(cnt);
		  }
	   anim.currentframe = framenumber;
	   return anim.imagebuffer;
	   }

//****************************************************************************
//
//      ANIM_GetPalette ()
//
//****************************************************************************


	//****************************************************************************
	//
	//      ANIM_GetPalette ()
	//
	// return the palette of the anim
	//****************************************************************************

	public static byte ANIM_GetPalette()
	   {
	   GlobalMembersANIMLIB.CheckAnimStarted("GetPalette");
	   return anim.pal;
	   }

	//extern anim_t * anim;

	#if __cplusplus
	}
	#endif


	//****************************************************************************
	//
	// GLOBALS
	//
	//****************************************************************************

	//****************************************************************************
	//
	// LOCALS
	//
	//****************************************************************************
	public static anim_t anim=null;
	internal static boolean Anim_Started = false;

	//****************************************************************************
	//
	//      CheckAnimStarted ()
	//
	//****************************************************************************

	public static void CheckAnimStarted(ref string funcname)
	   {
	   if (!Anim_Started)
		  Error("ANIMLIB_%s: Anim has not been initialized\n",funcname);
	   }
	//****************************************************************************
	//
	//      findpage ()
	//              - given a frame number return the large page number it resides in
	//
	//****************************************************************************

	public static uint16 findpage(uint16 framenumber)
	   {
	   uint16 i;

	   GlobalMembersANIMLIB.CheckAnimStarted("findpage");
	   for(i = 0; i<anim.lpheader.nLps; i++)
		  {
		  if (anim.LpArray[i].baseRecord <= framenumber && anim.LpArray[i].baseRecord + anim.LpArray[i].nRecords > framenumber)
			 return(i);
		  }
	   return(i);
	   }


	//****************************************************************************
	//
	//      loadpage ()
	//      - seek out and load in the large page specified
	//
	//****************************************************************************

	public static void loadpage(uint16 pagenumber, ref uint16 pagepointer)
	   {
	   int32 size;
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
	   byte * buffer;

	   GlobalMembersANIMLIB.CheckAnimStarted("loadpage");
	   buffer = anim.buffer;
	   if (anim.curlpnum != pagenumber)
		  {
		  anim.curlpnum = pagenumber;
		  buffer += 0xb00 + (pagenumber *0x10000);
		  size = sizeof(lp_descriptor);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
		  memcpy(anim.curlp, buffer, size);
		  buffer += size + sizeof(uint16);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
		  memcpy(pagepointer,buffer,anim.curlp.nBytes+(anim.curlp.nRecords *2));
		  }
	   }


	//****************************************************************************
	//
	//      CPlayRunSkipDump ()
	//      - This version of the decompressor is here for portability to non PC's
	//
	//****************************************************************************

	public static void CPlayRunSkipDump(ref string srcP, ref string dstP)
	   {
	   sbyte cnt;
	   uint16 wordCnt;
	   byte pixel;


	nextOp:
	   cnt = (sbyte) srcP ++;
	   if (cnt > 0)
		  goto dump;
	   if (cnt == 0)
		  goto run;
	   cnt -= 0x80;
	   if (cnt == 0)
		  goto longOp;
	/* shortSkip */
	   dstP += cnt; // adding 7-bit count to 32-bit pointer
	   goto nextOp;
	dump:
	   do
		  {
		  dstP ++= srcP ++;
		  } while (--cnt);
	   goto nextOp;
	run:
	   wordCnt = (byte) srcP ++; // 8-bit unsigned count
	   pixel = srcP ++;
	   do
		  {
		  dstP ++= pixel;
		  } while (--wordCnt);

	   goto nextOp;
	longOp:
	   wordCnt = (uint16)srcP;
	   srcP += sizeof(uint16);
	   if ((int16)wordCnt <= 0)
		  goto notLongSkip; // Do SIGNED test.

	/* longSkip. */
	   dstP += wordCnt;
	   goto nextOp;

	notLongSkip:
	   if (wordCnt == 0)
		  goto stop;
	   wordCnt -= 0x8000; // Remove sign bit.
	   if (wordCnt >= 0x4000)
		  goto longRun;

	/* longDump. */
	   do
		  {
		  dstP ++= srcP ++;
		  } while (--wordCnt);
	   goto nextOp;

	longRun:
	   wordCnt -= 0x4000; // Clear "longRun" bit.
	   pixel = srcP ++;
	   do
		  {
		  dstP ++= pixel;
		  } while (--wordCnt);
	   goto nextOp;

	stop: // all done
	   ;
	   }



	//****************************************************************************
	//
	//      renderframe ()
	//      - draw the frame sepcified from the large page in the buffer pointed to
	//
	//****************************************************************************

	public static void renderframe(uint16 framenumber, uint16[] pagepointer)
	   {
	   uint16 offset = 0;
	   uint16 i;
	   uint16 destframe;
	   byte ppointer;

	   GlobalMembersANIMLIB.CheckAnimStarted("renderframe");
	   destframe = framenumber - anim.curlp.baseRecord;

	   for(i = 0; i < destframe; i++)
		  {
		  offset += pagepointer[i];
		  }
	   ppointer = (byte)pagepointer;

	   ppointer+=anim.curlp.nRecords *2+offset;
	   if(ppointer[1])
		  {
		  ppointer += (4 + (((uint16)ppointer)[1] + (((uint16)ppointer)[1] & 1)));
		  }
	   else
		  {
		  ppointer+=4;
		  }

	   GlobalMembersANIMLIB.CPlayRunSkipDump(ref ppointer, ref anim.imagebuffer);
	   }


	//****************************************************************************
	//
	//      drawframe ()
	//      - high level frame draw routine
	//
	//****************************************************************************

	public static void drawframe(uint16 framenumber)
	   {
	   GlobalMembersANIMLIB.CheckAnimStarted("drawframe");
	   GlobalMembersANIMLIB.loadpage(GlobalMembersANIMLIB.findpage(framenumber), ref anim.thepage);
	   GlobalMembersANIMLIB.renderframe(framenumber, anim.thepage);
	   }
}

//-------------------------------------------------------------------------
/*
Copyright (C) 1996, 2003 - 3D Realms Entertainment

This file is part of Duke Nukem 3D version 1.5 - Atomic Edition

Duke Nukem 3D is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  

See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

Original Source: 1996 - Todd Replogle
Prepared for public release: 03/21/2003 - Charlie Wiederhold, 3D Realms
*/
//-------------------------------------------------------------------------


//-------------------------------------------------------------------------
/*
Copyright (C) 1996, 2003 - 3D Realms Entertainment

This file is part of Duke Nukem 3D version 1.5 - Atomic Edition

Duke Nukem 3D is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  

See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

Original Source: 1996 - Todd Replogle
Prepared for public release: 03/21/2003 - Charlie Wiederhold, 3D Realms
*/
//-------------------------------------------------------------------------

/////////////////////////////////////////////////////////////////////////////
//
//      ANIMLIB.H
//
/////////////////////////////////////////////////////////////////////////////

#if __cplusplus
//C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
extern "C"
{
#endif


// structure declarations for deluxe animate large page files */

public class lpfileheader
   {
   public uint32 id; // 4 character ID == "LPF " */
   public uint16 maxLps; // max # largePages allowed. 256 FOR NOW. */
   public uint16 nLps; // # largePages in this file. */
   public uint32 nRecords; // # records in this file. 65534 is current limit plus */
						// one for last-to-first delta for looping the animation */
   public uint16 maxRecsPerLp; // # records permitted in an lp. 256 FOR NOW. */
   public uint16 lpfTableOffset; // Absolute Seek position of lpfTable. 1280 FOR NOW.
										 // The lpf Table is an array of 256 large page structures
										 // that is used to facilitate finding records in an anim
										 // file without having to seek through all of the Large
										 // Pages to find which one a specific record lives in. */
   public uint32 contentType; // 4 character ID == "ANIM" */
   public uint16 width; // Width of screen in pixels. */
   public uint16 height; // Height of screen in pixels. */
   public byte variant; // 0==ANIM. */
   public byte version; // 0==frame rate is multiple of 18 cycles/sec.
										// 1==frame rate is multiple of 70 cycles/sec.  */
   public byte hasLastDelta; // 1==Last record is a delta from last-to-first frame. */
   public byte lastDeltaValid; // 0==The last-to-first delta (if present) hasn't been
								  // updated to match the current first&last frames,    so it
								  // should be ignored. */
   public byte pixelType; // // 0==256 color.
   public byte CompressionType; // // 1==(RunSkipDump) Only one used FOR NOW.
   public byte otherRecsPerFrm; // // 0 FOR NOW.
   public byte bitmaptype; // // 1==320x200, 256-color. Only one implemented so far.
   public byte[] recordTypes = new byte[32]; // // Not yet implemented.
   public uint32 nFrames; /* In case future version adds other records at end of
                                //      file, we still know how many actual frames.
                                  //    NOTE: DOES include last-to-first delta when present. */
   public uint16 framesPerSecond; // Number of frames to play per second. */
   public uint16[] pad2 = new uint16[29]; // 58 bytes of filler to round up to 128 bytes total. */
   }

// this is the format of a large page structure
public class lp_descriptor
   {
   public uint16 baseRecord; // Number of first record in this large page.
   public uint16 nRecords; // Number of records in lp.
													  // bit 15 of "nRecords" == "has continuation from previous lp".
											  // bit 14 of "nRecords" == "final record continues on next lp".
   public uint16 nBytes; // Total number of bytes of contents, excluding header.
   }

public class anim_t
   {
   public uint16 framecount; // current frame of anim
   public lpfileheader lpheader = new lpfileheader(); // file header will be loaded into this structure
   public lp_descriptor[] LpArray = new lp_descriptor[256]; // arrays of large page structs used to find frames
   public uint16 curlpnum; // initialize to an invalid Large page number
   public lp_descriptor curlp = new lp_descriptor(); // header of large page currently in memory
   public uint16[] thepage = new uint16[0x8000]; // buffer where current large page is loaded
   public byte[] imagebuffer = new byte[0x10000]; // buffer where anim frame is decoded
   public byte buffer;
   public byte[] pal = new byte[768];
   public int32 currentframe;
  }
