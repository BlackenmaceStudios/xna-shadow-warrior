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

//****************************************************************************
//
// gamedefs.h
//
// common defines between the game and the setup program
//
//****************************************************************************

#if __cplusplus
//C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
extern "C"
{
#endif

//****************************************************************************
//
// DEFINES
//
//****************************************************************************

//
// Setup program defines
//
#define SETUPFILENAME


// Max number of players

#define MAXPLAYERS

// Number of Mouse buttons

#define MAXMOUSEBUTTONS

// Number of JOY buttons

#define MAXJOYBUTTONS

// Number of EXTERNAL buttons

//#define MAXEXTERNALBUTTONS 6

//
// modem string defines
//

#define MAXMODEMSTRING

// MACRO defines

#define MAXMACROS
#define MAXMACROLENGTH

// Phone list defines

#define PHONENUMBERLENGTH
#define PHONENAMELENGTH
#define MAXPHONEENTRIES

// length of program functions

#define MAXFUNCTIONLENGTH

// length of axis functions

#define MAXAXISFUNCTIONLENGTH

// Max Player Name length

#define MAXPLAYERNAMELENGTH

// Max RTS Name length

#define MAXRTSNAMELENGTH

// Number of Mouse Axes

#define MAXMOUSEAXES

// Number of JOY axes

#define MAXJOYAXES

// Number of GAMEPAD axes

#define MAXGAMEPADAXES

// MIN/MAX scale value for controller scales

#define MAXCONTROLSCALEVALUE

// DEFAULT scale value for controller scales

#define DEFAULTCONTROLSCALEVALUE

// base value for controller scales

#define BASECONTROLSCALEVALUE

// MAX mouse sensitivity scale

#define MAXMOUSESENSITIVITY

// DEFAULT mouse sensitivity scale

#define DEFAULTMOUSESENSITIVITY

public enum AnonymousEnum4
   {
   gametype_network=3,
   gametype_serial=1,
   gametype_modem=2
   }

public enum AnonymousEnum5
   {
   connecttype_dialing=0,
   connecttype_answer=1,
   connecttype_alreadyconnected=2
   }

public enum AnonymousEnum6
   {
   screenbuffer_320x200,
   screenbuffer_640x400,
   screenbuffer_640x480,
   screenbuffer_800x600,
   screenbuffer_1024x768,
   screenbuffer_1280x1024,
   screenbuffer_1600x1200
   }

public enum AnonymousEnum7
   {
   vesa_320x200,
   vesa_360x200,
   vesa_320x240,
   vesa_360x240,
   vesa_320x400,
   vesa_360x400,
   vesa_640x350,
   vesa_640x400,
   vesa_640x480,
   vesa_800x600,
   vesa_1024x768,
   vesa_1280x1024,
   vesa_1600x1200
   }

public enum AnonymousEnum8
   {
   screenmode_chained,
   screenmode_vesa,
   screenmode_buffered,
   screenmode_tseng,
   screenmode_paradise,
   screenmode_s3,
   screenmode_crystal,
   screenmode_redblue,
   }


#if __cplusplus
}
#endif

