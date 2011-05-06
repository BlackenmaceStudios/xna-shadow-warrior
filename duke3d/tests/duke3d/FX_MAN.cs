public static class GlobalMembersFX_MAN
{


//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//string FX_ErrorString(int ErrorNumber);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SetupCard(int SoundCard, ref fx_device device);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_GetBlasterSettings(ref fx_blaster_config blaster);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SetupSoundBlaster(fx_blaster_config blaster, ref int MaxVoices, ref int MaxSampleBits, ref int MaxChannels);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_Init(int SoundCard, int numvoices, int numchannels, int samplebits, uint mixrate);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_Shutdown();
private delegate void functionDelegate(uint NamelessParameter);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SetCallBack(functionDelegate function);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void FX_SetVolume(int volume);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_GetVolume();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void FX_SetReverseStereo(int setting);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_GetReverseStereo();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void FX_SetReverb(int reverb);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void FX_SetFastReverb(int reverb);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_GetMaxReverbDelay();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_GetReverbDelay();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void FX_SetReverbDelay(int delay);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_VoiceAvailable(int priority);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SetPan(int handle, int vol, int left, int right);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SetPitch(int handle, int pitchoffset);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SetFrequency(int handle, int frequency);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayVOC(ref string ptr, int pitchoffset, int vol, int left, int right, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayLoopedVOC(ref string ptr, int loopstart, int loopend, int pitchoffset, int vol, int left, int right, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayWAV(ref string ptr, int pitchoffset, int vol, int left, int right, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayLoopedWAV(ref string ptr, int loopstart, int loopend, int pitchoffset, int vol, int left, int right, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayVOC3D(ref string ptr, int pitchoffset, int angle, int distance, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayWAV3D(ref string ptr, int pitchoffset, int angle, int distance, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayRaw(ref string ptr, uint length, uint rate, int pitchoffset, int vol, int left, int right, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_PlayLoopedRaw(ref string ptr, uint length, ref string loopstart, ref string loopend, uint rate, int pitchoffset, int vol, int left, int right, int priority, uint callbackval);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_Pan3D(int handle, int angle, int distance);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SoundActive(int handle);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_SoundsPlaying();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_StopSound(int handle);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_StopAllSounds();
private delegate void functionDelegate(ref string[] ptr, ref uint length);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_StartDemandFeedPlayback(functionDelegate function, int rate, int pitchoffset, int vol, int left, int right, int priority, uint callbackval);
private delegate void functionDelegate(ref string ptr, int length);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//int FX_StartRecording(int MixRate, functionDelegate function);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void FX_StopRecord();
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



public class fx_device
   {
   public int MaxVoices;
   public int MaxSampleBits;
   public int MaxChannels;
   }

#define MonoFx
#define StereoFx

public class fx_blaster_config
   {
   public uint Address;
   public uint Type;
   public uint Interrupt;
   public uint Dma8;
   public uint Dma16;
   public uint Midi;
   public uint Emu;
   }

public enum FX_ERRORS
   {
   FX_Warning = -2,
   FX_Error = -1,
   FX_Ok = 0,
   FX_ASSVersion,
   FX_BlasterError,
   FX_SoundCardError,
   FX_InvalidCard,
   FX_MultiVocError,
   FX_DPMI_Error
   }

public enum fx_BLASTER_Types
   {
   fx_SB = 1,
   fx_SBPro = 2,
   fx_SB20 = 3,
   fx_SBPro2 = 4,
   fx_SB16 = 6
   }

