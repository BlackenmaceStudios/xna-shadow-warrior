public static class GlobalMembersUTIL_LIB
{

	#if (__MSDOS__ && !__FLAT__)
	//extern short _argc;
	#else
	//extern int _argc;
	#endif
	//extern sbyte ** _argv;
private delegate void shutdownDelegate();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void RegisterShutdownFunction(shutdownDelegate shutdown);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void Error(ref string error, params object[] LegacyParamArray);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//sbyte CheckParm(ref string check);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//IntPtr SafeMalloc(int size);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int SafeMallocSize(IntPtr  ptr);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void SafeFree(IntPtr  ptr);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void SafeRealloc(ref IntPtr ptr, int newsize);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int ParseHex(ref string hex);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int ParseNum(ref string str);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//short MotoShort(short l);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//short IntelShort(short l);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MotoLong(int l);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int IntelLong(int l);
private delegate int compareDelegate();
private delegate void switcherDelegate();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void HeapSort(ref string base, int nel, int width, compareDelegate compare, switcherDelegate switcher);

	#if __cplusplus
	}
	#endif
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

//***************************************************************************
//
//    UTIL_LIB.C - various utils
//
//***************************************************************************

#if __cplusplus
//C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
extern "C"
{
#endif
