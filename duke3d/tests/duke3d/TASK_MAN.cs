public static class GlobalMembersTASK_MAN
{

	// TS_InInterrupt is TRUE during a taskman interrupt.
	// Use this if you have code that may be used both outside
	// and within interrupts.

	//extern volatile int TS_InInterrupt;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void TS_Shutdown();
private delegate void FunctionDelegate(ref task NamelessParameter);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//task TS_ScheduleTask(FunctionDelegate Function, int rate, int priority, IntPtr data);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int TS_Terminate(ref task ptr);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void TS_Dispatch();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void TS_SetTaskRate(ref task Task, int rate);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void TS_UnlockMemory();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int TS_LockMemory();
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

Original Source: 1994 - Jim Dose
Prepared for public release: 03/21/2003 - Charlie Wiederhold, 3D Realms
*/
//-------------------------------------------------------------------------


public enum TASK_ERRORS
   {
   TASK_Warning = -2,
   TASK_Error = -1,
   TASK_Ok = 0
   }

public class task
{
	public task next;
	public task prev;
	public delegate void TaskServiceDelegate(ref task UnnamedParameter1);
	public TaskServiceDelegate TaskService;
	public IntPtr data;
	public int rate;
	public volatile int count;
	public int priority;
	public int active;
}

