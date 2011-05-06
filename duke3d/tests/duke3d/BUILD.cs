public static class GlobalMembersBUILD
{

	#if EXTERN_AlternateDefinition1
	 public static sectortype[] sector = new sectortype[DefineConstants.MAXSECTORS];
	#elif EXTERN_AlternateDefinition2
	//extern sectortype sector[DefineConstants.MAXSECTORS];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static walltype[] wall = new walltype[DefineConstants.MAXWALLS];
	#elif EXTERN_AlternateDefinition2
	//extern walltype wall[DefineConstants.MAXWALLS];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static spritetype[] sprite = new spritetype[DefineConstants.MAXSPRITES];
	#elif EXTERN_AlternateDefinition2
	//extern spritetype sprite[DefineConstants.MAXSPRITES];
	#endif

	#if EXTERN_AlternateDefinition1
	 public static int spritesortcnt;
	#elif EXTERN_AlternateDefinition2
	//extern int spritesortcnt;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static spritetype[] tsprite = new spritetype[DefineConstants.MAXSPRITESONSCREEN];
	#elif EXTERN_AlternateDefinition2
	//extern spritetype tsprite[DefineConstants.MAXSPRITESONSCREEN];
	#endif

	#if EXTERN_AlternateDefinition1
	 public static sbyte vidoption;
	#elif EXTERN_AlternateDefinition2
	//extern sbyte vidoption;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static int xdim;
	 public static int ydim;
	 public static int[] ylookup = new int[DefineConstants.MAXYDIM+1];
	 public static int numpages;
	#elif EXTERN_AlternateDefinition2
	//extern int xdim, ydim, ylookup[DefineConstants.MAXYDIM+1], numpages;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static int yxaspect;
	 public static int viewingrange;
	#elif EXTERN_AlternateDefinition2
	//extern int yxaspect, viewingrange;
	#endif

	#if EXTERN_AlternateDefinition1
	 public static short numsectors;
	 public static short numwalls;
	#elif EXTERN_AlternateDefinition2
	//extern short numsectors, numwalls;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static volatile int totalclock;
	#elif EXTERN_AlternateDefinition2
	//extern volatile int totalclock;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static int numframes;
	 public static int randomseed;
	#elif EXTERN_AlternateDefinition2
	//extern int numframes, randomseed;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static short[] sintable = new short[2048];
	#elif EXTERN_AlternateDefinition2
	//extern short sintable[2048];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static string palette = new string(new char[768]);
	#elif EXTERN_AlternateDefinition2
	//extern sbyte palette[768];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static short numpalookups;
	#elif EXTERN_AlternateDefinition2
	//extern short numpalookups;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static string[] palookup = new string[DefineConstants.MAXPALOOKUPS];
	#elif EXTERN_AlternateDefinition2
	//extern sbyte *palookup[DefineConstants.MAXPALOOKUPS];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static sbyte parallaxtype;
	 public static sbyte showinvisibility;
	#elif EXTERN_AlternateDefinition2
	//extern sbyte parallaxtype, showinvisibility;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static int parallaxyoffs;
	 public static int parallaxyscale;
	#elif EXTERN_AlternateDefinition2
	//extern int parallaxyoffs, parallaxyscale;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static int visibility;
	 public static int parallaxvisibility;
	#elif EXTERN_AlternateDefinition2
	//extern int visibility, parallaxvisibility;
	#endif

	#if EXTERN_AlternateDefinition1
	 public static int windowx1;
	 public static int windowy1;
	 public static int windowx2;
	 public static int windowy2;
	#elif EXTERN_AlternateDefinition2
	//extern int windowx1, windowy1, windowx2, windowy2;
	#endif
	#if EXTERN_AlternateDefinition1
	 public static short[] startumost = new short[DefineConstants.MAXXDIM];
	 public static short[] startdmost = new short[DefineConstants.MAXXDIM];
	#elif EXTERN_AlternateDefinition2
	//extern short startumost[DefineConstants.MAXXDIM], startdmost[DefineConstants.MAXXDIM];
	#endif

	#if EXTERN_AlternateDefinition1
	 public static short[] pskyoff = new short[DefineConstants.MAXPSKYTILES];
	 public static short pskybits;
	#elif EXTERN_AlternateDefinition2
	//extern short pskyoff[DefineConstants.MAXPSKYTILES], pskybits;
	#endif

	#if EXTERN_AlternateDefinition1
	 public static short[] headspritesect = new short[DefineConstants.MAXSECTORS+1];
	 public static short[] headspritestat = new short[DefineConstants.MAXSTATUS+1];
	#elif EXTERN_AlternateDefinition2
	//extern short headspritesect[DefineConstants.MAXSECTORS+1], headspritestat[DefineConstants.MAXSTATUS+1];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static short[] prevspritesect = new short[DefineConstants.MAXSPRITES];
	 public static short[] prevspritestat = new short[DefineConstants.MAXSPRITES];
	#elif EXTERN_AlternateDefinition2
	//extern short prevspritesect[DefineConstants.MAXSPRITES], prevspritestat[DefineConstants.MAXSPRITES];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static short[] nextspritesect = new short[DefineConstants.MAXSPRITES];
	 public static short[] nextspritestat = new short[DefineConstants.MAXSPRITES];
	#elif EXTERN_AlternateDefinition2
	//extern short nextspritesect[DefineConstants.MAXSPRITES], nextspritestat[DefineConstants.MAXSPRITES];
	#endif

	#if EXTERN_AlternateDefinition1
	 public static short[] tilesizx = new short[DefineConstants.MAXTILES];
	 public static short[] tilesizy = new short[DefineConstants.MAXTILES];
	#elif EXTERN_AlternateDefinition2
	//extern short tilesizx[DefineConstants.MAXTILES], tilesizy[DefineConstants.MAXTILES];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static string walock = new string(new char[DefineConstants.MAXTILES]);
	#elif EXTERN_AlternateDefinition2
	//extern sbyte walock[DefineConstants.MAXTILES];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static int numtiles;
	 public static int[] picanm = new int[DefineConstants.MAXTILES];
	 public static int[] waloff = new int[DefineConstants.MAXTILES];
	#elif EXTERN_AlternateDefinition2
	//extern int numtiles, picanm[DefineConstants.MAXTILES], waloff[DefineConstants.MAXTILES];
	#endif

		//These variables are for auto-mapping with the draw2dscreen function.
		//When you load a new board, these bits are all set to 0 - since
		//you haven't mapped out anything yet.  Note that these arrays are
		//bit-mapped.
		//If you want draw2dscreen() to show sprite #54 then you say:
		//   spritenum = 54;
		//   show2dsprite[spritenum>>3] |= (1<<(spritenum&7));
		//And if you want draw2dscreen() to not show sprite #54 then you say:
		//   spritenum = 54;
		//   show2dsprite[spritenum>>3] &= ~(1<<(spritenum&7));
		//Automapping defaults to 0 (do nothing).  If you set automapping to 1,
		//   then in 3D mode, the walls and sprites that you see will show up the
		//   next time you flip to 2D mode.

	#if EXTERN_AlternateDefinition1
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	 sbyte show2dsector[(DefineConstants.MAXSECTORS+7)>>3];
	#elif EXTERN_AlternateDefinition2
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	extern sbyte show2dsector[(DefineConstants.MAXSECTORS+7)>>3];
	#endif
	#if EXTERN_AlternateDefinition1
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	 sbyte show2dwall[(DefineConstants.MAXWALLS+7)>>3];
	#elif EXTERN_AlternateDefinition2
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	extern sbyte show2dwall[(DefineConstants.MAXWALLS+7)>>3];
	#endif
	#if EXTERN_AlternateDefinition1
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	 sbyte show2dsprite[(DefineConstants.MAXSPRITES+7)>>3];
	#elif EXTERN_AlternateDefinition2
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	extern sbyte show2dsprite[(DefineConstants.MAXSPRITES+7)>>3];
	#endif
	#if EXTERN_AlternateDefinition1
	 public static sbyte automapping;
	#elif EXTERN_AlternateDefinition2
	//extern sbyte automapping;
	#endif

	#if EXTERN_AlternateDefinition1
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	 sbyte gotpic[(DefineConstants.MAXTILES+7)>>3];
	#elif EXTERN_AlternateDefinition2
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	extern sbyte gotpic[(DefineConstants.MAXTILES+7)>>3];
	#endif
	#if EXTERN_AlternateDefinition1
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	 sbyte gotsector[(DefineConstants.MAXSECTORS+7)>>3];
	#elif EXTERN_AlternateDefinition2
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	extern sbyte gotsector[(DefineConstants.MAXSECTORS+7)>>3];
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

#define MAXSECTORS
#define MAXWALLS
#define MAXSPRITES

#define MAXTILES
#define MAXSTATUS
#define MAXPLAYERS
#define MAXXDIM
#define MAXYDIM
#define MAXPALOOKUPS
#define MAXPSKYTILES
#define MAXSPRITESONSCREEN

#define CLIPMASK0
#define CLIPMASK1

	//Make all variables in BUILD.H defined in the ENGINE,
	//and externed in GAME
#if ENGINE
	#define EXTERN_AlternateDefinition1
	#define EXTERN
#else
	#define EXTERN_AlternateDefinition2
	#define EXTERN
#endif

//ceilingstat/floorstat:
//   bit 0: 1 = parallaxing, 0 = not                                 "P"
//   bit 1: 1 = groudraw, 0 = not
//   bit 2: 1 = swap x&y, 0 = not                                    "F"
//   bit 3: 1 = double smooshiness                                   "E"
//   bit 4: 1 = x-flip                                               "F"
//   bit 5: 1 = y-flip                                               "F"
//   bit 6: 1 = Align texture to first wall of sector                "R"
//   bits 7-15: reserved

	//40 bytes
public class sectortype
{
	public short wallptr;
	public short wallnum;
	public int ceilingz;
	public int floorz;
	public short ceilingstat;
	public short floorstat;
	public short ceilingpicnum;
	public short ceilingheinum;
	public sbyte ceilingshade;
	public sbyte ceilingpal;
	public sbyte ceilingxpanning;
	public sbyte ceilingypanning;
	public short floorpicnum;
	public short floorheinum;
	public sbyte floorshade;
	public sbyte floorpal;
	public sbyte floorxpanning;
	public sbyte floorypanning;
	public sbyte visibility;
	public sbyte filler;
	public short lotag;
	public short hitag;
	public short extra;
}

//cstat:
//   bit 0: 1 = Blocking wall (use with clipmove, getzrange)         "B"
//   bit 1: 1 = bottoms of invisible walls swapped, 0 = not          "2"
//   bit 2: 1 = align picture on bottom (for doors), 0 = top         "O"
//   bit 3: 1 = x-flipped, 0 = normal                                "F"
//   bit 4: 1 = masking wall, 0 = not                                "M"
//   bit 5: 1 = 1-way wall, 0 = not                                  "1"
//   bit 6: 1 = Blocking wall (use with hitscan / cliptype 1)        "H"
//   bit 7: 1 = Transluscence, 0 = not                               "T"
//   bit 8: 1 = y-flipped, 0 = normal                                "F"
//   bit 9: 1 = Transluscence reversing, 0 = normal                  "T"
//   bits 10-15: reserved

	//32 bytes
public class walltype
{
	public int x;
	public int y;
	public short point2;
	public short nextwall;
	public short nextsector;
	public short cstat;
	public short picnum;
	public short overpicnum;
	public sbyte shade;
	public sbyte pal;
	public sbyte xrepeat;
	public sbyte yrepeat;
	public sbyte xpanning;
	public sbyte ypanning;
	public short lotag;
	public short hitag;
	public short extra;
}

//cstat:
//   bit 0: 1 = Blocking sprite (use with clipmove, getzrange)       "B"
//   bit 1: 1 = transluscence, 0 = normal                            "T"
//   bit 2: 1 = x-flipped, 0 = normal                                "F"
//   bit 3: 1 = y-flipped, 0 = normal                                "F"
//   bits 5-4: 00 = FACE sprite (default)                            "R"
//             01 = WALL sprite (like masked walls)
//             10 = FLOOR sprite (parallel to ceilings&floors)
//   bit 6: 1 = 1-sided sprite, 0 = normal                           "1"
//   bit 7: 1 = Real centered centering, 0 = foot center             "C"
//   bit 8: 1 = Blocking sprite (use with hitscan / cliptype 1)      "H"
//   bit 9: 1 = Transluscence reversing, 0 = normal                  "T"
//   bits 10-14: reserved
//   bit 15: 1 = Invisible sprite, 0 = not invisible

	//44 bytes
public class spritetype
{
	public int x;
	public int y;
	public int z;
	public short cstat;
	public short picnum;
	public sbyte shade;
	public sbyte pal;
	public sbyte clipdist;
	public sbyte filler;
	public byte xrepeat;
	public byte yrepeat;
	public sbyte xoffset;
	public sbyte yoffset;
	public short sectnum;
	public short statnum;
	public short ang;
	public short owner;
	public short xvel;
	public short yvel;
	public short zvel;
	public short lotag;
	public short hitag;
	public short extra;
}

/*************************************************************************
POSITION VARIABLES:

		POSX is your x - position ranging from 0 to 65535
		POSY is your y - position ranging from 0 to 65535
			(the length of a side of the grid in EDITBORD would be 1024)
		POSZ is your z - position (height) ranging from 0 to 65535, 0 highest.
		ANG is your angle ranging from 0 to 2047.  Instead of 360 degrees, or
			 2 * PI radians, I use 2048 different angles, so 90 degrees would
			 be 512 in my system.

SPRITE VARIABLES:

	EXTERN short headspritesect[MAXSECTORS+1], headspritestat[MAXSTATUS+1];
	EXTERN short prevspritesect[MAXSPRITES], prevspritestat[MAXSPRITES];
	EXTERN short nextspritesect[MAXSPRITES], nextspritestat[MAXSPRITES];

	Example: if the linked lists look like the following:
		 旼컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴커
		 �      Sector lists:               Status lists:               �
		 쳐컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴캑
		 �  Sector0:  4, 5, 8             Status0:  2, 0, 8             �
		 �  Sector1:  16, 2, 0, 7         Status1:  4, 5, 16, 7, 3, 9   �
		 �  Sector2:  3, 9                                              �
		 읕컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴켸
	Notice that each number listed above is shown exactly once on both the
		left and right side.  This is because any sprite that exists must
		be in some sector, and must have some kind of status that you define.


Coding example #1:
	To go through all the sprites in sector 1, the code can look like this:

		sectnum = 1;
		i = headspritesect[sectnum];
		while (i != -1)
		{
			nexti = nextspritesect[i];

			//your code goes here
			//ex: printf("Sprite %d is in sector %d\n",i,sectnum);

			i = nexti;
		}

Coding example #2:
	To go through all sprites with status = 1, the code can look like this:

		statnum = 1;        //status 1
		i = headspritestat[statnum];
		while (i != -1)
		{
			nexti = nextspritestat[i];

			//your code goes here
			//ex: printf("Sprite %d has a status of 1 (active)\n",i,statnum);

			i = nexti;
		}

			 insertsprite(short sectnum, short statnum);
			 deletesprite(short spritenum);
			 changespritesect(short spritenum, short newsectnum);
			 changespritestat(short spritenum, short newstatnum);

TILE VARIABLES:
		NUMTILES - the number of tiles found TILES.DAT.
		TILESIZX[MAXTILES] - simply the x-dimension of the tile number.
		TILESIZY[MAXTILES] - simply the y-dimension of the tile number.
		WALOFF[MAXTILES] - the actual 32-bit offset pointing to the top-left
								 corner of the tile.
		PICANM[MAXTILES] - flags for animating the tile.

TIMING VARIABLES:
		TOTALCLOCK - When the engine is initialized, TOTALCLOCK is set to zero.
			From then on, it is incremented 120 times a second by 1.  That
			means that the number of seconds elapsed is totalclock / 120.
		NUMFRAMES - The number of times the draw3dscreen function was called
			since the engine was initialized.  This helps to determine frame
			rate.  (Frame rate = numframes * 120 / totalclock.)

OTHER VARIABLES:

		STARTUMOST[320] is an array of the highest y-coordinates on each column
				that my engine is allowed to write to.  You need to set it only
				once.
		STARTDMOST[320] is an array of the lowest y-coordinates on each column
				that my engine is allowed to write to.  You need to set it only
				once.
		SINTABLE[2048] is a sin table with 2048 angles rather than the
			normal 360 angles for higher precision.  Also since SINTABLE is in
			all integers, the range is multiplied by 16383, so instead of the
			normal -1<sin(x)<1, the range of sintable is -16383<sintable[]<16383
			If you use this sintable, you can possibly speed up your code as
			well as save space in memory.  If you plan to use sintable, 2
			identities you may want to keep in mind are:
				sintable[ang&2047]       = sin(ang * (3.141592/1024)) * 16383
				sintable[(ang+512)&2047] = cos(ang * (3.141592/1024)) * 16383
		NUMSECTORS - the total number of existing sectors.  Modified every time
			you call the loadboard function.
***************************************************************************/
