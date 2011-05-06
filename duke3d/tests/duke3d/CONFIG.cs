using System;

public static class GlobalMembersCONFIG
{
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


	#define SETUPNAMEPARM

	//extern int32 FXDevice;
	//extern int32 MusicDevice;
	//extern int32 FXVolume;
	//extern int32 MusicVolume;
	//extern fx_blaster_config BlasterConfig;
	//extern int32 NumVoices;
	//extern int32 NumChannels;
	//extern int32 NumBits;
	//extern int32 MixRate;
	//extern int32 MidiPort;
	//extern int32 ReverseStereo;

	//extern int32 ControllerType;
	//extern int32 MouseAiming;
	//extern int32 ScreenMode;
	//extern int32 ScreenWidth;
	//extern int32 ScreenHeight;

/*
===================
=
= CONFIG_ReadSetup
=
===================
*/


	public static void CONFIG_ReadSetup()
	{
	   int32 dummy;
	   string commmacro = "CommbatMacro# ";

	   if (!SafeFileExists(setupfilename))
		  {
		  Error("ReadSetup: %s does not exist\n" + "           Please run SETUP.EXE\n",setupfilename);
		  }

	   GlobalMembersCONFIG.CONFIG_SetDefaults();
	   scripthandle = SCRIPT_Load(setupfilename);

	   for(dummy = 0;dummy < 10;dummy++)
	   {
		   commmacro = StringFunctions.ChangeCharacter(commmacro, 13, dummy+'0');
		   SCRIPT_GetString(scripthandle, "Comm Setup",commmacro,GlobalMembersGLOBAL.ud.ridecule[dummy][0]);
	   }

	   SCRIPT_GetString(scripthandle, "Comm Setup","PlayerName",GlobalMembersGLOBAL.myname[0]);

	   dummy = CheckParm("NAME");
	   if (dummy != 0)
		   GlobalMembersGLOBAL.myname = _argv[dummy+1];
	   dummy = CheckParm("MAP");
	#if ! VOLUMEONE
	   if (dummy != 0)
	   {
		   GlobalMembersGAME.boardfilename = _argv[dummy+1];
		   if(StringFunctions.StrChr(GlobalMembersGAME.boardfilename,'.') == 0)
			   GlobalMembersGAME.boardfilename += ".map";
		   Console.Write("Using level: '{0}'.\n",GlobalMembersGAME.boardfilename);
	   }
	   else
	#else
		if (dummy != 0)
			Console.WriteLine("The -map option is available in the registered version only!");
	#endif
		   GlobalMembersGAME.boardfilename = StringFunctions.ChangeCharacter(GlobalMembersGAME.boardfilename, 0, 0);

	   SCRIPT_GetString(scripthandle, "Comm Setup","RTSName",GlobalMembersGLOBAL.ud.rtsname[0]);

	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "Shadows",GlobalMembersGLOBAL.ud.shadows);
	   SCRIPT_GetString(scripthandle, "Screen Setup","Password",GlobalMembersGLOBAL.ud.pwlockout[0]);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "Detail",GlobalMembersGLOBAL.ud.detail);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "Tilt",GlobalMembersGLOBAL.ud.screen_tilting);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "Messages",GlobalMembersGLOBAL.ud.fta_on);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "ScreenWidth", ScreenWidth);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "ScreenHeight", ScreenHeight);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "ScreenMode", ScreenMode);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "ScreenGamma",GlobalMembersGLOBAL.ud.brightness);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "ScreenSize",GlobalMembersGLOBAL.ud.screen_size);
	   SCRIPT_GetNumber(scripthandle, "Screen Setup", "Out",GlobalMembersGLOBAL.ud.lockout);

	   SCRIPT_GetNumber(scripthandle, "Misc", "Executions",GlobalMembersGLOBAL.ud.executions);
	   GlobalMembersGLOBAL.ud.executions++;
	   SCRIPT_GetNumber(scripthandle, "Misc", "RunMode",GlobalMembersGLOBAL.ud.auto_run);
	   SCRIPT_GetNumber(scripthandle, "Misc", "Crosshairs",GlobalMembersGLOBAL.ud.crosshair);
	   if(GlobalMembersGLOBAL.ud.wchoice[0][0] == 0 && GlobalMembersGLOBAL.ud.wchoice[0][1] == 0)
	   {
		   GlobalMembersGLOBAL.ud.wchoice[0][0] = 3;
		   GlobalMembersGLOBAL.ud.wchoice[0][1] = 4;
		   GlobalMembersGLOBAL.ud.wchoice[0][2] = 5;
		   GlobalMembersGLOBAL.ud.wchoice[0][3] = 7;
		   GlobalMembersGLOBAL.ud.wchoice[0][4] = 8;
		   GlobalMembersGLOBAL.ud.wchoice[0][5] = 6;
		   GlobalMembersGLOBAL.ud.wchoice[0][6] = 0;
		   GlobalMembersGLOBAL.ud.wchoice[0][7] = 2;
		   GlobalMembersGLOBAL.ud.wchoice[0][8] = 9;
		   GlobalMembersGLOBAL.ud.wchoice[0][9] = 1;

		   for(dummy = 0;dummy<10;dummy++)
		   {
			   GlobalMembersGLOBAL.buf = string.Format("WeaponChoice{0:D}", dummy);
			   SCRIPT_GetNumber(scripthandle, "Misc", GlobalMembersGLOBAL.buf, GlobalMembersGLOBAL.ud.wchoice[0][dummy]);
		   }
		}

	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "FXDevice", FXDevice);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "MusicDevice", MusicDevice);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "FXVolume", FXVolume);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "MusicVolume", MusicVolume);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "SoundToggle", SoundToggle);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "MusicToggle", MusicToggle);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "VoiceToggle", VoiceToggle);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "AmbienceToggle", AmbienceToggle);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "NumVoices", NumVoices);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "NumChannels", NumChannels);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "NumBits", NumBits);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "MixRate", MixRate);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "MidiPort", MidiPort);
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "BlasterAddress", dummy);
	   BlasterConfig.Address = dummy;
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "BlasterType", dummy);
	   BlasterConfig.Type = dummy;
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "BlasterInterrupt", dummy);
	   BlasterConfig.Interrupt = dummy;
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "BlasterDma8", dummy);
	   BlasterConfig.Dma8 = dummy;
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "BlasterDma16", dummy);
	   BlasterConfig.Dma16 = dummy;
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "BlasterEmu", dummy);
	   BlasterConfig.Emu = dummy;
	   SCRIPT_GetNumber(scripthandle, "Sound Setup", "ReverseStereo", dummy);
	   ReverseStereo = dummy;

	   SCRIPT_GetNumber(scripthandle, "Controls", "ControllerType", ControllerType);
	   SCRIPT_GetNumber(scripthandle, "Controls","MouseAimingFlipped",GlobalMembersGLOBAL.ud.mouseflip);
	   SCRIPT_GetNumber(scripthandle, "Controls", "MouseAiming", MouseAiming);
	   SCRIPT_GetNumber(scripthandle, "Controls","GameMouseAiming",(int32)GlobalMembersGLOBAL.ps[0].aim_mode);
	   SCRIPT_GetNumber(scripthandle, "Controls","AimingFlag",(int32)GlobalMembersPLAYER.myaimmode);

	   CONTROL_ClearAssignments();

	   GlobalMembersCONFIG.CONFIG_ReadKeys();

	   switch (ControllerType)
		  {
		  case controltype_keyboardandmouse:
			 GlobalMembersCONFIG.CONFIG_SetupMouse(scripthandle);
			 break;
		  default:
			 GlobalMembersCONFIG.CONFIG_SetupMouse(scripthandle);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
		  case controltype_keyboardandjoystick:
		  case controltype_keyboardandflightstick:
		  case controltype_keyboardandthrustmaster:
			 GlobalMembersCONFIG.CONFIG_SetupJoystick(scripthandle);
			 break;
		  case controltype_keyboardandgamepad:
			 GlobalMembersCONFIG.CONFIG_SetupGamePad(scripthandle);
			 break;

		  }
	   setupread = 1;
	   }
	public static void CONFIG_GetSetupFilename()
	   {
	   find_t fblock = new find_t();
	   string extension = new string(new char[10]);
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
	   sbyte * src;
	   string[] filenames = new string[DefineConstants.MAXSETUPFILES];
	   int32 numfiles;
	   int32 i;

	   setupfilename = SETUPFILENAME;

	   // determine extension

	   src = setupfilename + setupfilename.Length - 1;

	   while (*src != '.')
		  {
		  src--;
		  }
	   &extension[1] = src;
	   extension = StringFunctions.ChangeCharacter(extension, 0, '*');

	   numfiles = 0;
	   if (_dos_findfirst(extension, 0, fblock)==0)
		  {
		  do
			 {
			 filenames[numfiles] = SafeMalloc(128);
			 filenames[numfiles] = fblock.name;
			 numfiles++;
			 if (numfiles == DefineConstants.MAXSETUPFILES)
				break;
			 }
		  while(!_dos_findnext(fblock));
		  }
	   i = CheckParm (DefineConstants.SETUPNAMEPARM);
	   if (i!=0)
		  {
		  numfiles = 0;
		  setupfilename = _argv[i+1];
		  }
	   if (numfiles>1)
		  {
		  int32 time;
		  int32 oldtime;
		  int32 count;

		  Console.Write("\nMultiple Configuration Files Encountered\n");
		  Console.Write("========================================\n");
		  Console.Write("Please choose a configuration file from the following list by pressing its\n");
		  Console.Write("corresponding letter:\n");
		  for (i = 0;i<numfiles;i++)
			 {
			 if (strcmpi(filenames[i],SETUPFILENAME))
				{
				Console.Write("{0}. {1}\n",'a'+(sbyte)i,filenames[i]);
				}
			 else
				{
				Console.Write("{0}. {1} <DEFAULT>\n",'a'+(sbyte)i,filenames[i]);
				}
			 }
		  Console.Write("\n");
		  Console.Write("({0} will be used if no selection is made within 10 seconds.)\n\n",SETUPFILENAME);
		  KB_FlushKeyboardQueue();
		  KB_ClearKeysDown();
		  count = 9;
		  oldtime = clock();
		  time = clock()+(10 *CLOCKS_PER_SEC);
		  while (clock()<time)
			 {
			 if (clock()>oldtime)
				{
				Console.Write("{0:D} seconds left. \r",count);
				fflush(stdout);
				oldtime = clock()+CLOCKS_PER_SEC;
				count--;
				}
			 if (KB_KeyWaiting())
				{
				int32 ch = KB_Getch();
				ch -='a';
				if (ch>=0 && ch<numfiles)
				   {
				   setupfilename = filenames[ch];
				   break;
				   }
				}
			 }
		  Console.Write("\n\n");
		  }
	   if (numfiles == 1)
		  setupfilename = filenames[0];
	   Console.Write("Using Setup file: '{0}'\n",setupfilename);
	   i = clock()+(3 *CLOCKS_PER_SEC/4);
	   while (clock()<i)
		  {
		  ;
		  }
	   for (i = 0;i<numfiles;i++)
		  {
		  SafeFree(filenames[i]);
		  }
	   }

/*
===================
=
= CONFIG_WriteSetup
=
===================
*/

	public static void CONFIG_WriteSetup()
	   {
	   int32 dummy;

	   if (!setupread)
		   return;

	   SCRIPT_PutNumber(scripthandle, "Screen Setup", "Shadows",GlobalMembersGLOBAL.ud.shadows,false,false);
	   SCRIPT_PutString(scripthandle, "Screen Setup", "Password",GlobalMembersGLOBAL.ud.pwlockout);
	   SCRIPT_PutNumber(scripthandle, "Screen Setup", "Detail",GlobalMembersGLOBAL.ud.detail,false,false);
	   SCRIPT_PutNumber(scripthandle, "Screen Setup", "Tilt",GlobalMembersGLOBAL.ud.screen_tilting,false,false);
	   SCRIPT_PutNumber(scripthandle, "Screen Setup", "Messages",GlobalMembersGLOBAL.ud.fta_on,false,false);
	   SCRIPT_PutNumber(scripthandle, "Screen Setup", "Out",GlobalMembersGLOBAL.ud.lockout,false,false);
	   SCRIPT_PutNumber(scripthandle, "Sound Setup", "FXVolume",FXVolume,false,false);
	   SCRIPT_PutNumber(scripthandle, "Sound Setup", "MusicVolume",MusicVolume,false,false);
	   SCRIPT_PutNumber(scripthandle, "Sound Setup", "SoundToggle",SoundToggle,false,false);
	   SCRIPT_PutNumber(scripthandle, "Sound Setup", "VoiceToggle",VoiceToggle,false,false);
	   SCRIPT_PutNumber(scripthandle, "Sound Setup", "AmbienceToggle",AmbienceToggle,false,false);
	   SCRIPT_PutNumber(scripthandle, "Sound Setup", "MusicToggle",MusicToggle,false,false);
	   SCRIPT_PutNumber(scripthandle, "Sound Setup", "ReverseStereo",ReverseStereo,false,false);
	   SCRIPT_PutNumber(scripthandle, "Screen Setup", "ScreenSize",GlobalMembersGLOBAL.ud.screen_size,false,false);
	   SCRIPT_PutNumber(scripthandle, "Screen Setup", "ScreenGamma",GlobalMembersGLOBAL.ud.brightness,false,false);
	   SCRIPT_PutNumber(scripthandle, "Misc", "Executions",GlobalMembersGLOBAL.ud.executions,false,false);
	   SCRIPT_PutNumber(scripthandle, "Misc", "RunMode",GlobalMembersGLOBAL.ud.auto_run,false,false);
	   SCRIPT_PutNumber(scripthandle, "Misc", "Crosshairs",GlobalMembersGLOBAL.ud.crosshair,false,false);
	   SCRIPT_PutNumber(scripthandle, "Controls", "MouseAimingFlipped",GlobalMembersGLOBAL.ud.mouseflip,false,false);
	   SCRIPT_PutNumber(scripthandle, "Controls","MouseAiming",MouseAiming,false,false);
	   SCRIPT_PutNumber(scripthandle, "Controls","GameMouseAiming",(int32) GlobalMembersGLOBAL.ps[myconnectindex].aim_mode,false,false);
	   SCRIPT_PutNumber(scripthandle, "Controls","AimingFlag",(int) GlobalMembersPLAYER.myaimmode,false,false);

	   for(dummy = 0;dummy<10;dummy++)
	   {
		   GlobalMembersGLOBAL.buf = string.Format("WeaponChoice{0:D}", dummy);
		   SCRIPT_PutNumber(scripthandle, "Misc",GlobalMembersGLOBAL.buf,GlobalMembersGLOBAL.ud.wchoice[myconnectindex][dummy],false,false);
	   }

	   switch (ControllerType)
		  {
		  case controltype_keyboardandmouse:
			 dummy = CONTROL_GetMouseSensitivity();
			 SCRIPT_PutNumber(scripthandle, "Controls","MouseSensitivity",dummy,false,false);
			 break;
		  }
	   SCRIPT_Save (scripthandle, setupfilename);
	   SCRIPT_Free (scripthandle);
	   }



	// we load this in to get default button and key assignments
	// as well as setting up function mappings


	//
	// Sound variables
	//
	public static int32 FXDevice;
	public static int32 MusicDevice;
	public static int32 FXVolume;
	public static int32 MusicVolume;
	public static int32 SoundToggle;
	public static int32 MusicToggle;
	public static int32 VoiceToggle;
	public static int32 AmbienceToggle;
	public static fx_blaster_config BlasterConfig = new fx_blaster_config();
	public static int32 NumVoices;
	public static int32 NumChannels;
	public static int32 NumBits;
	public static int32 MixRate;
	public static int32 MidiPort;
	public static int32 ReverseStereo;

	public static int32 ControllerType;
	public static int32 MouseAiming;

	//
	// Screen variables
	//

	public static int32 ScreenMode;
	public static int32 ScreenWidth;
	public static int32 ScreenHeight;

	internal static string setupfilename={SETUPFILENAME};
	internal static int32 scripthandle;
	internal static int32 setupread=0;
	/*
	===================
	=
	= CONFIG_GetSetupFilename
	=
	===================
	*/
	#define MAXSETUPFILES

	/*
	===================
	=
	= CONFIG_FunctionNameToNum
	=
	===================
	*/

	public static int32 CONFIG_FunctionNameToNum(ref string func)
	   {
	   int32 i;

	   for (i = 0;i<NUMGAMEFUNCTIONS;i++)
		  {
		  if (!string.Compare(func,gamefunctions[i], true))
			 {
			 return i;
			 }
		  }
	   return -1;
	   }

	/*
	===================
	=
	= CONFIG_FunctionNumToName
	=
	===================
	*/

	public static string CONFIG_FunctionNumToName(int32 func)
	   {
	   if (func < NUMGAMEFUNCTIONS)
		  {
		  return gamefunctions[func];
		  }
	   else
		  {
		  return null;
		  }
	   }

	/*
	===================
	=
	= CONFIG_AnalogNameToNum
	=
	===================
	*/


	public static int32 CONFIG_AnalogNameToNum(ref string func)
	   {

	   if (!string.Compare(func,"analog_turning", true))
		  {
		  return analog_turning;
		  }
	   if (!string.Compare(func,"analog_strafing", true))
		  {
		  return analog_strafing;
		  }
	   if (!string.Compare(func,"analog_moving", true))
		  {
		  return analog_moving;
		  }
	   if (!string.Compare(func,"analog_lookingupanddown", true))
		  {
		  return analog_lookingupanddown;
		  }

	   return -1;
	   }

	/*
	===================
	=
	= CONFIG_SetDefaults
	=
	===================
	*/

	public static void CONFIG_SetDefaults()
	   {
	   SoundToggle = 1;
	   MusicToggle = 1;
	   VoiceToggle = 1;
	   AmbienceToggle = 1;
	   FXVolume = 192;
	   MusicVolume = 128;
	   ReverseStereo = 0;
	   GlobalMembersGLOBAL.ps[0].aim_mode = 0;
	   GlobalMembersGLOBAL.ud.screen_size = 8;
	   GlobalMembersGLOBAL.ud.screen_tilting = 1;
	   GlobalMembersGLOBAL.ud.shadows = 1;
	   GlobalMembersGLOBAL.ud.detail = 1;
	   GlobalMembersGLOBAL.ud.lockout = 0;
	   GlobalMembersGLOBAL.ud.pwlockout[0] = '\0';
	   GlobalMembersGLOBAL.ud.crosshair = 0;
	   GlobalMembersGLOBAL.ud.m_marker = 1;
	   GlobalMembersGLOBAL.ud.m_ffire = 1;
	}
	/*
	===================
	=
	= CONFIG_ReadKeys
	=
	===================
	*/

	public static void CONFIG_ReadKeys()
	   {
	   int32 i;
	   int32 numkeyentries;
	   int32 function;
	   string keyname1 = new string(new char[80]);
	   string keyname2 = new string(new char[80]);
	   kb_scancode key1 = new kb_scancode();
	   kb_scancode key2 = new kb_scancode();

	   numkeyentries = SCRIPT_NumberEntries(scripthandle, "KeyDefinitions");

	   for (i = 0;i<numkeyentries;i++)
		  {
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref SCRIPT_Entry(scripthandle, "KeyDefinitions", i));
		  if (function != -1)
			 {
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
			 memset(keyname1,0,sizeof(sbyte));
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
			 memset(keyname2,0,sizeof(sbyte));
			 SCRIPT_GetDoubleString (scripthandle, "KeyDefinitions", SCRIPT_Entry(scripthandle,"KeyDefinitions", i), keyname1, keyname2);
			 key1 = 0;
			 key2 = 0;
			 if (keyname1[0])
				{
				key1 = (byte) KB_StringToScanCode(keyname1);
				}
			 if (keyname2[0])
				{
				key2 = (byte) KB_StringToScanCode(keyname2);
				}
			 CONTROL_MapKey(function, key1, key2);
			 }
		  }
	   }


	/*
	===================
	=
	= CONFIG_SetupMouse
	=
	===================
	*/

	public static void CONFIG_SetupMouse(int32 scripthandle)
	   {
	   int32 i;
	   string str = new string(new char[80]);
	   string temp = new string(new char[80]);
	   int32 function;
	   int32 scale;

	   for (i = 0;i<MAXMOUSEBUTTONS;i++)
		  {
		  str = string.Format("MouseButton{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle,"Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapButton(function, i, false);
		  str = string.Format("MouseButtonClicked{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle,"Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapButton(function, i, true);
		  }
	   // map over the axes
	   for (i = 0;i<MAXMOUSEAXES;i++)
		  {
		  str = string.Format("MouseAnalogAxes{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_AnalogNameToNum(ref temp);
		  if (function != -1)
			 {
			 CONTROL_MapAnalogAxis(i,function);
			 }
		  str = string.Format("MouseDigitalAxes{0:D}_0", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapDigitalAxis(i, function, 0);
		  str = string.Format("MouseDigitalAxes{0:D}_1", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapDigitalAxis(i, function, 1);
		  str = string.Format("MouseAnalogScale{0:D}", i);
		  SCRIPT_GetNumber(scripthandle, "Controls", str, scale);
		  CONTROL_SetAnalogAxisScale(i, scale);
		  }

	   SCRIPT_GetNumber(scripthandle, "Controls", "MouseSensitivity", function);
	   CONTROL_SetMouseSensitivity(function);
	   }

	/*
	===================
	=
	= CONFIG_SetupGamePad
	=
	===================
	*/

	public static void CONFIG_SetupGamePad(int32 scripthandle)
	   {
	   int32 i;
	   string str = new string(new char[80]);
	   string temp = new string(new char[80]);
	   int32 function;


	   for (i = 0;i<MAXJOYBUTTONS;i++)
		  {
		  str = string.Format("JoystickButton{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle,"Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapButton(function, i, false);
		  str = string.Format("JoystickButtonClicked{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle,"Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapButton(function, i, true);
		  }
	   // map over the axes
	   for (i = 0;i<MAXGAMEPADAXES;i++)
		  {
		  str = string.Format("GamePadDigitalAxes{0:D}_0", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapDigitalAxis(i, function, 0);
		  str = string.Format("GamePadDigitalAxes{0:D}_1", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapDigitalAxis(i, function, 1);
		  }
	   SCRIPT_GetNumber(scripthandle, "Controls", "JoystickPort", function);
	   CONTROL_JoystickPort = function;
	   }

	/*
	===================
	=
	= CONFIG_SetupJoystick
	=
	===================
	*/

	public static void CONFIG_SetupJoystick(int32 scripthandle)
	   {
	   int32 i;
	   string str = new string(new char[80]);
	   string temp = new string(new char[80]);
	   int32 function;
	   int32 scale;

	   for (i = 0;i<MAXJOYBUTTONS;i++)
		  {
		  str = string.Format("JoystickButton{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle,"Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapButton(function, i, false);
		  str = string.Format("JoystickButtonClicked{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle,"Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapButton(function, i, true);
		  }
	   // map over the axes
	   for (i = 0;i<MAXJOYAXES;i++)
		  {
		  str = string.Format("JoystickAnalogAxes{0:D}", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_AnalogNameToNum(ref temp);
		  if (function != -1)
			 {
			 CONTROL_MapAnalogAxis(i,function);
			 }
		  str = string.Format("JoystickDigitalAxes{0:D}_0", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapDigitalAxis(i, function, 0);
		  str = string.Format("JoystickDigitalAxes{0:D}_1", i);
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memset' has no equivalent in C#:
		  memset(temp,0,sizeof(sbyte));
		  SCRIPT_GetString(scripthandle, "Controls", str,temp);
		  function = GlobalMembersCONFIG.CONFIG_FunctionNameToNum(ref temp);
		  if (function != -1)
			 CONTROL_MapDigitalAxis(i, function, 1);
		  str = string.Format("JoystickAnalogScale{0:D}", i);
		  SCRIPT_GetNumber(scripthandle, "Controls", str, scale);
		  CONTROL_SetAnalogAxisScale(i, scale);
		  }
	   // read in JoystickPort
	   SCRIPT_GetNumber(scripthandle, "Controls", "JoystickPort", function);
	   CONTROL_JoystickPort = function;
	   // read in rudder state
	   SCRIPT_GetNumber(scripthandle, "Controls", "EnableRudder", CONTROL_RudderEnabled);
	   }

	public static void readsavenames()
	{
		int dummy;
		short i;
		string[] fn = {"game_.sav"};
		FILE fil;

		for (i = 0;i<10;i++)
		{
			fn[4] = i+'0';
			if ((fil = fopen(fn,"rb")) == null)
				continue;
			dfread(dummy, 4, 1, fil);

			if(dummy != BYTEVERSION)
				return;
			dfread(dummy, 4, 1, fil);
			dfread(GlobalMembersGLOBAL.ud.savegame[i][0],19,1,fil);
			fclose(fil);
		}
	}
}

