//-------------------------------------------------------------------------
/*
Copyright (c) 2010 - JV Software
Copyright (C) 1996, 2003 - 3D Realms Entertainment

This file is part of the XNA Duke Nukem 3D Atomic Edition Port

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
Ported to Silverlight/XNA C# 05/03/2011 - Justin Marshall, JV Software
*/
//-------------------------------------------------------------------------

using System;
using System.Net;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using build;

namespace duke3d.game
{
    public class Anim
    {
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
        private anim_t  anim = new anim_t();
        private bool Anim_Started = false;

        //****************************************************************************
        //
        //      CheckAnimStarted ()
        //
        //****************************************************************************

        public void CheckAnimStarted ( string funcname )
        {
           if (!Anim_Started)
              throw new Exception("ANIMLIB_"+funcname +": Anim has not been initialized\n");
        }
        //****************************************************************************
        //
        //      findpage ()
        //              - given a frame number return the large page number it resides in
        //
        //****************************************************************************

        private UInt16 findpage (UInt16 framenumber)
        {
           UInt16 i;

           CheckAnimStarted ( "findpage" );
           for(i=0; i<anim.lpheader.nLps; i++)
              {
              if
                 (
                 anim.LpArray[i].baseRecord <= framenumber &&
                 anim.LpArray[i].baseRecord + anim.LpArray[i].nRecords > framenumber
                 )
                 return(i);
              }
           return(i);
        }


       


        //****************************************************************************
        //
        //      drawframe ()
        //      - high level frame draw routine
        //
        //****************************************************************************

        public void drawframe (UInt16 framenumber)
        {
           CheckAnimStarted ( "drawframe" );

        }


        //****************************************************************************
        //
        //      ANIM_LoadAnim ()
        //
        //****************************************************************************

        public void ANIM_LoadAnim (string animpath)
        {
           UInt16 i;
           Int32 size;

           if (!Anim_Started) Anim_Started = true;

           anim.fil = Engine.filesystem.kopen4load( animpath );
           if (anim.fil == null)
               throw new Exception("ANIM_LoadAnim: Failed to load " + animpath);

           anim.curlpnum = 0xffff;
           anim.currentframe = -1;

           anim.lpheader = new lpfileheader(anim.fil);
           anim.fil.Position += 128;

           // load the color palette
           for (i = 0; i < 256; i ++)
           {
              byte r, g, b;
              b = anim.fil.kreadbyte();
              g = anim.fil.kreadbyte();
              r = anim.fil.kreadbyte();
              anim.pal[i] = (255 << 24) | ((r ) << 16) | ((g ) << 8) | ((b ));
              anim.fil.Position++;
           }
           
           // read in large page descriptors
           for(i = 0; i < anim.LpArray.Length; i++ )
           {
               anim.LpArray[i] = new lp_descriptor();
               anim.LpArray[i].Parse(anim.fil);
           }

           anim.pages = new Page[anim.lpheader.nFrames];
           for (i = 0; i < anim.lpheader.nFrames; i++)
           {
               anim.pages[i] = new Page();
               anim.pages[i].Init(findpage(i), i, anim.fil);
           }

           anim.fil.Close();
           anim.fil = null;
        }

        public void ANIM_BlitFrame(Int32 framenumber)
        {
            Array.Clear(Engine._device._screenbuffer.Pixels, 0, 320 * 200);

            for (int i = 0; i < framenumber; i++)
            {
                int pixelnum = 0;
                foreach (short ch in anim.pages[i].pageptr)
                {
                    if (ch >= 0)
                        Engine._device._screenbuffer.Pixels[pixelnum] = anim.pal[ch];
                    else if (i == 0)
                    {
                        Engine._device._screenbuffer.Pixels[pixelnum] = anim.pal[0];
                    }
                    pixelnum++;
                }
            }
        }

        public uint ANIM_NumFrames()
        {
            return anim.lpheader.nFrames;
        }

        //****************************************************************************
        //
        //      ANIM_GetPalette ()
        //
        //****************************************************************************

        public int[] ANIM_GetPalette ()
        {
           CheckAnimStarted ( "GetPalette" );
           return anim.pal;
        }
#region structure declarations for deluxe animate large page files
        [StructLayout(LayoutKind.Explicit)]
        struct lpPagePointer
        {
            [FieldOffset(0)]
            public sbyte sbyteval;

            [FieldOffset(0)]
            public byte byteval;

            [FieldOffset(1)]
            public byte byteval1;

            [FieldOffset(0)]
            public ushort shortval;
        }

        class lpfileheader
        {
           public lpfileheader( kFile file )
           {
                id = file.kreaduint();
                maxLps = file.kreadushort();
                nLps = file.kreadushort();
                nRecords  = file.kreaduint();
                                
                maxRecsPerLp = file.kreadushort();
                lpfTableOffset = file.kreadushort();
                contentType  = file.kreaduint();
                width = file.kreadushort();
                height = file.kreadushort();
                variant = file.kreadbyte();
                version  = file.kreadbyte();
                                               
                hasLastDelta  = file.kreadbyte();
                lastDeltaValid  = file.kreadbyte();
                                          
                pixelType  = file.kreadbyte();
                CompressionType  = file.kreadbyte();
                otherRecsPerFrm  = file.kreadbyte();
                bitmaptype  = file.kreadbyte();
               
                for(int i = 0; i < recordTypes.Length; i++ )
                {
                    recordTypes[i] = file.kreadbyte();
                }

                nFrames  = file.kreaduint();
                                        
                framesPerSecond = file.kreadushort();
        
                for(int i = 0; i < pad2.Length; i++ )
                {
                    pad2[i] = file.kreadushort();
                }
           }
           public UInt32 id;                 // 4 character ID == "LPF " */
           public UInt16 maxLps;                  // max # largePages allowed. 256 FOR NOW.   */
           public UInt16 nLps;            // # largePages in this file. */
           public UInt32 nRecords;        // # records in this file.  65534 is current limit plus */
                                // one for last-to-first delta for looping the animation */
           public UInt16 maxRecsPerLp; // # records permitted in an lp. 256 FOR NOW.   */
           public UInt16 lpfTableOffset; // Absolute Seek position of lpfTable.  1280 FOR NOW.
                                                 // The lpf Table is an array of 256 large page structures
                                                 // that is used to facilitate finding records in an anim
                                                 // file without having to seek through all of the Large
                                                 // Pages to find which one a specific record lives in. */
           public UInt32 contentType;  // 4 character ID == "ANIM" */
           public UInt16 width;                   // Width of screen in pixels. */
           public UInt16 height;                  // Height of screen in pixels. */
           public byte variant;              // 0==ANIM. */
           public byte version;              // 0==frame rate is multiple of 18 cycles/sec.
                                                // 1==frame rate is multiple of 70 cycles/sec.  */
           public byte hasLastDelta;   // 1==Last record is a delta from last-to-first frame. */
           public byte lastDeltaValid; // 0==The last-to-first delta (if present) hasn't been
                                          // updated to match the current first&last frames,    so it
                                          // should be ignored. */
           public byte pixelType;         //   /* 0==256 color. */
           public byte CompressionType;//      /* 1==(RunSkipDump) Only one used FOR NOW. */
           public byte otherRecsPerFrm;//      /* 0 FOR NOW. */
           public byte bitmaptype;     //   /* 1==320x200, 256-color.  Only one implemented so far. */
           public byte[] recordTypes = new byte[32];//      /* Not yet implemented. */
           public UInt32 nFrames;         //   /* In case future version adds other records at end of
                                        //      file, we still know how many actual frames.
                                          //    NOTE: DOES include last-to-first delta when present. */
           public UInt16 framesPerSecond;      // Number of frames to play per second. */
           public UInt16[] pad2 = new UInt16[29];           // 58 bytes of filler to round up to 128 bytes total. */
       };

       class Page
       {
           lp_descriptor curlp;
           public short[] pageptr = new short[320 * 200];

           private void FitToScreen(int newWidth, int newHeight)
           {
               int _width = 320, _height = 200;
                //
                // Get a new buuffer to interpolate into
                short[] newData = new short[newWidth * newHeight];

                double scaleWidth =  (double)newWidth / (double)_width;
                double scaleHeight = (double)newHeight / (double)_height;

                for(int cy = 0; cy < newHeight; cy++)
                {
                    for(int cx = 0; cx < newWidth; cx++)
                    {
                        int pixel = (cy * (newWidth)) + (cx);
                        int nearestMatch =  (((int)(cy / scaleHeight) * (_width)) + ((int)(cx / scaleWidth)) );

                        newData[pixel] = pageptr[nearestMatch];
                    }
                }

                pageptr = newData;
           }

           //
           // Init
           //
           public void Init(int pagenumber, int framenumber, kFile fil)
           {
               fil.Position = 0;
               fil.Position += 0xb00 + (pagenumber * 0x10000);
               curlp = new lp_descriptor();
               curlp.Parse(fil);
               fil.Position += sizeof(UInt16);


               for (int i = 0; i < 320 * 200; i++)
               {
                   pageptr[i] = -1;
               }

               SetLargePageStart(framenumber, fil);
               LoadPagePtr(fil);
               FitToScreen(Engine._device.xdim, Engine._device.ydim);
           }

           private void SetLargePageStart(int framenumber, kFile fil)
           {
               UInt16 offset=0;
               UInt16 i;
               UInt16 destframe;

               destframe = (ushort)(framenumber - curlp.baseRecord);
               int oldposition = fil.Position;
               for(i = 0; i < destframe; i++)
                  {
                  offset += fil.kreadushort();
                  }
               fil.Position = oldposition + curlp.nRecords * 2 + offset;
               fil.Position++;
               byte ctrlbyte = fil.kreadbyte();
               fil.Position -= 2;
               if (ctrlbyte != 0)
               {
                      ushort ctrlint16 = fil.kreadushort();
                      fil.Position += (4 + (ctrlint16 + (ctrlint16 & 1)));
               }
               else
               {
                  fil.Position+=4;
               }

           }

           private void LoadPagePtr(kFile fil)
           {
               sbyte cnt;
               UInt16 wordCnt;
               byte pixel;
               int dstP = 0;

           nextOp:
               cnt = fil.kreadsbyte();
               if (cnt > 0)
                  goto dump;
               if (cnt == 0)
                  goto run;
               unchecked
               {
                   cnt -= (sbyte)128;
               }
               if (cnt == 0)
                  goto longOp;
            /* shortSkip */
               dstP += cnt;                    /* adding 7-bit count to 32-bit pointer */
               goto nextOp;
            dump:
               do
                  {
                      pageptr[dstP++] = fil.kreadbyte();
                  } while ((--cnt) > 0);
               goto nextOp;
            run:
               wordCnt = fil.kreadbyte();                /* 8-bit unsigned count */
            pixel = fil.kreadbyte();
               do
                  {
                      pageptr[dstP++] = pixel;
                  } while ((--wordCnt) > 0);

               goto nextOp;
           longOp:
               wordCnt = fil.kreadushort();
               if ((Int16)wordCnt <= 0)
                  goto notLongSkip;       /* Do SIGNED test. */

            /* longSkip. */
               dstP += wordCnt;
               goto nextOp;

            notLongSkip:
               if (wordCnt == 0)
                  goto stop;
               wordCnt -= 32768;              /* Remove sign bit. */
               if (wordCnt >= 16384)
                  goto longRun;

            /* longDump. */
               do
                  {
                      pageptr[dstP++] = fil.kreadbyte();
                  } while ((--wordCnt) > 0);
               goto nextOp;

            longRun:
               wordCnt -= 16384;              /* Clear "longRun" bit. */
            pixel = fil.kreadbyte();
               do
                  {
                      pageptr[dstP++] = pixel;
                  } while ((--wordCnt) > 0);
               goto nextOp;

            stop:   /* all done */
               ;
           }
       }

        // this is the format of a large page structure
       class lp_descriptor
       {
           public UInt16 baseRecord;   // Number of first record in this large page.
           public UInt16 nRecords;        // Number of records in lp.
                                                              // bit 15 of "nRecords" == "has continuation from previous lp".
                                                      // bit 14 of "nRecords" == "final record continues on next lp".
           public UInt16 nBytes;                  // Total number of bytes of contents, excluding header.

           public void Parse( kFile fil )
           {
               baseRecord = fil.kreadushort();
               nRecords = fil.kreadushort();
               nBytes = fil.kreadushort();
           }
       };

       class anim_t
       {
           public UInt16 framecount;          // current frame of anim
           public lpfileheader lpheader;           // file header will be loaded into this structure
           public lp_descriptor[] LpArray = new lp_descriptor[256]; // arrays of large page structs used to find frames
           public UInt16 curlpnum;               // initialize to an invalid Large page number
           public lp_descriptor curlp = new lp_descriptor();        // header of large page currently in memory
           public UInt16[] thepage = new UInt16[0x8000];     // buffer where current large page is loaded
           public byte[] imagebuffer = new byte[0x10000]; // buffer where anim frame is decoded
           public lpPagePointer[] pagepointer = new lpPagePointer[0x10000 * 4];
           public kFile fil;
           public int[] pal = new int[256];
           public Int32  currentframe;
           public Page[] pages;
       };
#endregion
    }
}
