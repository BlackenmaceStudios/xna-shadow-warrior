using System;

public static class GlobalMembersRTS
{

/*
====================
=
= RTS_Init
=
= Files with a .rts extension are idlink files with multiple lumps
=
====================
*/

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

	//***************************************************************************
	//
	//    RTS.H
	//
	//***************************************************************************


	/*
	====================
	=
	= RTS_Init
	=
	= Files with a .rts extension are idlink files with multiple lumps
	=
	====================
	*/

	public static void RTS_Init(ref string filename)
	   {
	   int32 length;
	   //
	   // open all the files, load headers, and count lumps
	   //
	   numlumps = 0;
	   lumpinfo = SafeMalloc(5); // will be realloced as lumps are added

	   Console.Write("RTS Manager Started.\n");
	   if (SafeFileExists(filename))
		  GlobalMembersRTS.RTS_AddFile(ref filename);

	   if (!numlumps)
		   return;

	   //
	   // set up caching
	   //
	   length = (numlumps) * sizeof(*lumpcache);
	   lumpcache = SafeMalloc(length);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
	   memset(lumpcache,0,length);
	   RTS_Started = true;
	   }

/*
====================
=
= RTS_NumSounds
=
====================
*/

	/*
	====================
	=
	= RTS_NumSounds
	=
	====================
	*/

	public static int32 RTS_NumSounds()
	   {
	   return numlumps-1;
	   }

/*
====================
=
= RTS_SoundLength
=
= Returns the buffer size needed to load the given lump
=
====================
*/

	/*
	====================
	=
	= RTS_SoundLength
	=
	= Returns the buffer size needed to load the given lump
	=
	====================
	*/

	public static int32 RTS_SoundLength(int32 lump)
	   {
	   lump++;
	   if (lump >= numlumps)
		  Error ("RTS_SoundLength: %i >= numlumps",lump);
	   return lumpinfo[lump].size;
	   }

/*
====================
=
= RTS_GetSoundName
=
====================
*/

	/*
	====================
	=
	= RTS_GetSoundName
	=
	====================
	*/

	public static string RTS_GetSoundName(int32 i)
	   {
	   i++;
	   if (i>=numlumps)
		  Error ("RTS_GetSoundName: %i >= numlumps",i);
	   return (lumpinfo[i].name[0]);
	   }

/*
====================
=
= RTS_GetSound
=
====================
*/
	/*
	====================
	=
	= RTS_GetSound
	=
	====================
	*/
	public static IntPtr RTS_GetSound(int32 lump)
	{
	   lump++;
	   if ((uint32)lump >= numlumps)
		  Error ("RTS_GetSound: %i >= %i\n",lump,numlumps);

	   if (lumpcache[lump] == null)
	   {
		  lumplockbyte[lump] = 200;
		  allocache((string)&lumpcache[lump], (int)GlobalMembersRTS.RTS_SoundLength(lump-1), lumplockbyte[lump]);
		  GlobalMembersRTS.RTS_ReadLump(lump, lumpcache[lump]);
	   }
	   else
	   {
		  if (lumplockbyte[lump] < 200)
			 lumplockbyte[lump] = 200;
		  else
			 lumplockbyte[lump]++;
	   }
	   return lumpcache[lump];
	}


	//=============
	// STATICS
	//=============

	public static int32 numlumps;
	internal static IntPtr *lumpcache;
	internal static lumpinfo_t[] lumpinfo; // location of each lump on disk
	internal static boolean RTS_Started = false;

	public static string lumplockbyte = new string(new char[11]);

	/*
	============================================================================
	
	                                                LUMP BASED ROUTINES
	
	============================================================================
	*/

	/*
	====================
	=
	= RTS_AddFile
	=
	= All files are optional, but at least one file must be found
	= Files with a .rts extension are wadlink files with multiple lumps
	= Other files are single lumps with the base filename for the lump name
	=
	====================
	*/

	public static void RTS_AddFile(ref string filename)
	   {
	   wadinfo_t header = new wadinfo_t();
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
	   lumpinfo_t *lump_p = new lumpinfo_t();
	   uint32 i;
	   int32 handle;
	   int32 length;
	   int32 startlump;
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
	   filelump_t *fileinfo = new filelump_t();

	//
	// read the entire file in
	//      FIXME: shared opens

	   handle = SafeOpenRead(filename, filetype_binary);

	   startlump = numlumps;

	   // WAD file
	   Console.Write("    Adding {0}.\n",filename);
	   SafeRead(handle, header, sizeof(wadinfo_t));
	   if (string.Compare(header.identification, 0, "IWAD", 0, 4))
		  Error ("RTS file %s doesn't have IWAD id\n",filename);
	   header.numlumps = IntelLong(header.numlumps);
	   header.infotableofs = IntelLong(header.infotableofs);
	   length = header.numlumps *sizeof(filelump_t);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'alloca' has no equivalent in C#:
	   fileinfo = alloca (length);
	   if (fileinfo == null)
		  Error ("RTS file could not allocate header info on stack");
	   lseek (handle, header.infotableofs, SEEK_SET);
	   SafeRead (handle, fileinfo, length);
	   numlumps += header.numlumps;

	//
	// Fill in lumpinfo
	//
	   SafeRealloc(lumpinfo, numlumps *sizeof(lumpinfo_t));
	   lump_p = &lumpinfo[startlump];

	   for (i = startlump ; i<numlumps ; i++,lump_p++, fileinfo++)
		  {
		  lump_p.handle = handle;
		  lump_p.position = IntelLong(fileinfo.filepos);
		  lump_p.size = IntelLong(fileinfo.size);
		  lump_p.name = fileinfo.name.Substring(0, 8);
		  }
	   }

	/*
	====================
	=
	= RTS_ReadLump
	=
	= Loads the lump into the given buffer, which must be >= RTS_SoundLength()
	=
	====================
	*/
	public static void RTS_ReadLump(int32 lump, IntPtr dest)
	   {
	   lumpinfo_t l;

	   if (lump >= numlumps)
		  Error ("RTS_ReadLump: %i >= numlumps",lump);
	   if (lump < 0)
		  Error ("RTS_ReadLump: %i < 0",lump);
	   l = lumpinfo+lump;
	   lseek (l.handle, l.position, SEEK_SET);
	   SafeRead(l.handle,dest,l.size);
	   }
}

