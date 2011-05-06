public static class GlobalMembersKEYBOARD
{

	/*
	=============================================================================
	
	                                    DEFINES
	
	=============================================================================
	*/


	#define sc_None
	#define sc_Bad
	#define sc_Comma
	#define sc_Period
	#define sc_Return
	#define sc_Enter
	#define sc_Escape
	#define sc_Space
	#define sc_BackSpace
	#define sc_Tab
	#define sc_LeftAlt
	#define sc_LeftControl
	#define sc_CapsLock
	#define sc_LeftShift
	#define sc_RightShift
	#define sc_F1
	#define sc_F2
	#define sc_F3
	#define sc_F4
	#define sc_F5
	#define sc_F6
	#define sc_F7
	#define sc_F8
	#define sc_F9
	#define sc_F10
	#define sc_F11
	#define sc_F12
	#define sc_Kpad_Star
	#define sc_Pause
	#define sc_ScrollLock
	#define sc_NumLock
	#define sc_Slash
	#define sc_SemiColon
	#define sc_Quote
	#define sc_Tilde
	#define sc_BackSlash

	#define sc_OpenBracket
	#define sc_CloseBracket

	#define sc_1
	#define sc_2
	#define sc_3
	#define sc_4
	#define sc_5
	#define sc_6
	#define sc_7
	#define sc_8
	#define sc_9
	#define sc_0
	#define sc_Minus
	#define sc_Equals
	#define sc_Plus

	#define sc_kpad_1
	#define sc_kpad_2
	#define sc_kpad_3
	#define sc_kpad_4
	#define sc_kpad_5
	#define sc_kpad_6
	#define sc_kpad_7
	#define sc_kpad_8
	#define sc_kpad_9
	#define sc_kpad_0
	#define sc_kpad_Minus
	#define sc_kpad_Plus
	#define sc_kpad_Period

	#define sc_A
	#define sc_B
	#define sc_C
	#define sc_D
	#define sc_E
	#define sc_F
	#define sc_G
	#define sc_H
	#define sc_I
	#define sc_J
	#define sc_K
	#define sc_L
	#define sc_M
	#define sc_N
	#define sc_O
	#define sc_P
	#define sc_Q
	#define sc_R
	#define sc_S
	#define sc_T
	#define sc_U
	#define sc_V
	#define sc_W
	#define sc_X
	#define sc_Y
	#define sc_Z

	// Extended scan codes

	#define sc_UpArrow
	#define sc_DownArrow
	#define sc_LeftArrow
	#define sc_RightArrow
	#define sc_Insert
	#define sc_Delete
	#define sc_Home
	#define sc_End
	#define sc_PgUp
	#define sc_PgDn
	#define sc_RightAlt
	#define sc_RightControl
	#define sc_kpad_Slash
	#define sc_kpad_Enter
	#define sc_PrintScreen
	#define sc_LastScanCode

	// Ascii scan codes

	#define asc_Enter
	#define asc_Escape
	#define asc_BackSpace
	#define asc_Tab
	#define asc_Space

	#define MAXKEYBOARDSCAN


	/*
	=============================================================================
	
	                               GLOBAL VARIABLES
	
	=============================================================================
	*/

	//extern volatile byte KB_KeyDown[DefineConstants.MAXKEYBOARDSCAN]; // Keyboard state array
	//extern volatile byte KB_LastScan;


	/*
	=============================================================================
	
	                                    MACROS
	
	=============================================================================
	*/

	#define KB_GetLastScanCode

	#define KB_SetLastScanCode

	#define KB_ClearLastScanCode

	#define KB_KeyPressed

	#define KB_ClearKeyDown


	/*
	=============================================================================
	
	                              FUNCTION PROTOTYPES
	
	=============================================================================
	*/

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_KeyEvent(int scancode, int keypressed); // Interprets scancodes
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int KB_KeyWaiting(); // Checks if a character is waiting in the keyboard queue
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//sbyte KB_Getch(); // Gets the next keypress
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_Addch(sbyte ch); // Adds key to end of queue
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_FlushKeyboardQueue(); // Empties the keyboard queue of all waiting characters.
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_ClearKeysDown(); // Clears all keys down flags.
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//string KB_ScanCodeToString(byte scancode); // convert scancode into a string
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//byte KB_StringToScanCode(ref string string); // convert a string into a scancode
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_TurnKeypadOn(); // turn the keypad on
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_TurnKeypadOff(); // turn the keypad off
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int KB_KeypadActive(); // check whether keypad is active
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_Startup();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void KB_Shutdown();

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

#if __cplusplus
//C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
extern "C"
{
#endif
