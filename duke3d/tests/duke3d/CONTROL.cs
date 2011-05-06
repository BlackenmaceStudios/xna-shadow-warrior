public static class GlobalMembersCONTROL
{


	//***************************************************************************
	//
	// GLOBALS
	//
	//***************************************************************************

	//extern int CONTROL_RudderEnabled;
	//extern int CONTROL_MousePresent;
	//extern int CONTROL_JoysPresent[DefineConstants.MaxJoys];
	//extern int CONTROL_MouseEnabled;
	//extern int CONTROL_JoystickEnabled;
	//extern byte CONTROL_JoystickPort;
	//extern uint CONTROL_ButtonState1;
	//extern uint CONTROL_ButtonHeldState1;
	//extern uint CONTROL_ButtonState2;
	//extern uint CONTROL_ButtonHeldState2;


	//***************************************************************************
	//
	// PROTOTYPES
	//
	//***************************************************************************

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_MapKey(int which, byte key1, byte key2);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_MapButton(int whichfunction, int whichbutton, int doubleclicked);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_DefineFlag(int which, int toggle);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int CONTROL_FlagActive(int which);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_ClearAssignments();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_GetUserInput(ref UserInput info);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_GetInput(ref ControlInfo info);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_ClearButton(int whichbutton);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_ClearUserInput(ref UserInput info);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_WaitRelease();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_Ack();
private delegate void CenterCenterDelegate();
private delegate void UpperLeftDelegate();
private delegate void LowerRightDelegate();
private delegate void CenterThrottleDelegate();
private delegate void CenterRudderDelegate();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_CenterJoystick(CenterCenterDelegate CenterCenter, UpperLeftDelegate UpperLeft, LowerRightDelegate LowerRight, CenterThrottleDelegate CenterThrottle, CenterRudderDelegate CenterRudder);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int CONTROL_GetMouseSensitivity();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_SetMouseSensitivity(int newsensitivity);
private delegate int TimeFunctionDelegate();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_Startup(controltype which, TimeFunctionDelegate TimeFunction, int ticspersecond);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_Shutdown();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_MapAnalogAxis(int whichaxis, int whichanalog);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_MapDigitalAxis(int whichaxis, int whichfunction, int direction);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_SetAnalogAxisScale(int whichaxis, int axisscale);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void CONTROL_PrintAxes();

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
// Public header for CONTROL.C.
//
//***************************************************************************

#if __cplusplus
//C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
extern "C"
{
#endif


//***************************************************************************
//
// DEFINES
//
//***************************************************************************

#define MaxJoys
#define MAXGAMEBUTTONS

#define BUTTON
#define BUTTONHELD
#define BUTTONJUSTPRESSED
#define BUTTONRELEASED
#define BUTTONSTATECHANGED

//***************************************************************************
//
// TYPEDEFS
//
//***************************************************************************
public enum axisdirection
   {
   axis_up,
   axis_down,
   axis_left,
   axis_right
   }

public enum analogcontrol
   {
   analog_turning=0,
   analog_strafing=1,
   analog_lookingupanddown=2,
   analog_elevation=3,
   analog_rolling=4,
   analog_moving=5,
   analog_maxtype
   }

public enum direction
   {
   dir_North,
   dir_NorthEast,
   dir_East,
   dir_SouthEast,
   dir_South,
   dir_SouthWest,
   dir_West,
   dir_NorthWest,
   dir_None
   }

public class UserInput
   {
   public int button0;
   public int button1;
   public direction dir;
   }

public class ControlInfo
   {
   public int dx;
   public int dy;
   public int dz;
   public int dyaw;
   public int dpitch;
   public int droll;
   }

public enum controltype
   {
   controltype_keyboard,
   controltype_keyboardandmouse,
   controltype_keyboardandjoystick,
   controltype_keyboardandexternal,
   controltype_keyboardandgamepad,
   controltype_keyboardandflightstick,
   controltype_keyboardandthrustmaster
   }
