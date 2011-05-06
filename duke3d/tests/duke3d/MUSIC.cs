public static class GlobalMembersMUSIC
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
	
	Original Source: 1994 - Jim Dose
	Prepared for public release: 03/21/2003 - Charlie Wiederhold, 3D Realms
	*/
	//-------------------------------------------------------------------------



	//extern int MUSIC_ErrorCode;

	#define MUSIC_LoopSong
	#define MUSIC_PlayOnce

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//string MUSIC_ErrorString(int ErrorNumber);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_Init(int SoundCard, int Address);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_Shutdown();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetMaxFMMidiChannel(int channel);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetVolume(int volume);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetMidiChannelVolume(int channel, int volume);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_ResetMidiChannelVolumes();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_GetVolume();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetLoopFlag(int loopflag);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_SongPlaying();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_Continue();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_Pause();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_StopSong();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_PlaySong(ref byte song, int loopflag);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetContext(int context);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_GetContext();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetSongTick(uint PositionInTicks);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetSongTime(uint milliseconds);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_SetSongPosition(int measure, int beat, int tick);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_GetSongPosition(ref songposition pos);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_GetSongLength(ref songposition pos);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_FadeVolume(int tovolume, int milliseconds);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int MUSIC_FadeActive();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_StopFade();
	private void MUSIC_RerouteMidiChannel(int channel, int cdecl (*function)(int event, int c1, int c2));
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void MUSIC_RegisterTimbreBank(ref byte timbres);
}

public enum MUSIC_ERRORS
   {
   MUSIC_Warning = -2,
   MUSIC_Error = -1,
   MUSIC_Ok = 0,
   MUSIC_ASSVersion,
   MUSIC_SoundCardError,
   MUSIC_MPU401Error,
   MUSIC_InvalidCard,
   MUSIC_MidiError,
   MUSIC_TaskManError,
   MUSIC_FMNotDetected,
   MUSIC_DPMI_Error
   }

public class songposition
   {
   public uint tickposition;
   public uint milliseconds;
   public uint measure;
   public uint beat;
   public uint tick;
   }

