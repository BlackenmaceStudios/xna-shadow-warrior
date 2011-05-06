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

#if __cplusplus
//C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
extern "C"
{
#endif


//***************************************************************************
//
//  Global Data Types (For portability)
//
//***************************************************************************





#define MAXINT32
#define MININT32
#define MAXUINT32
#define MINUINT32

#define MAXINT16
#define MININT16
#define MAXUINT16
#define MINUINT16

//***************************************************************************
//
//  boolean values
//
//***************************************************************************

#define true
#define false

//***************************************************************************
//
//  BYTE ACCESS MACROS
//
//***************************************************************************

// WORD macros
#define Int16_HighByte
#define Int16_LowByte

// DWORD macros
#define Int32_4Byte
#define Int32_3Byte
#define Int32_2Byte
#define Int32_1Byte

#if __NeXT__
#define stricmp
#define strcmpi
#endif

#if __cplusplus
}
#endif
