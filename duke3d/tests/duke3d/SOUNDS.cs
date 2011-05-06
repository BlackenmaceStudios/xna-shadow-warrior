using System;

public static class GlobalMembersSOUNDS
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

	//****************************************************************************
	//
	// sounds.h
	//
	//****************************************************************************


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

/*
===================
=
= SoundStartup
=
===================
*/


	public static void SoundStartup()
	   {
	   int32 status;

	   // if they chose None lets return
	   if (GlobalMembersCONFIG.FXDevice == NumSoundCards)
		   return;

	   // Do special Sound Blaster, AWE32 stuff
	   if ((GlobalMembersCONFIG.FXDevice == SoundBlaster) || (GlobalMembersCONFIG.FXDevice == Awe32))
		  {
		  int MaxVoices;
		  int MaxBits;
		  int MaxChannels;

		  status = FX_SetupSoundBlaster (GlobalMembersCONFIG.BlasterConfig, (int)&MaxVoices, (int)&MaxBits, (int)&MaxChannels);
		  }
	   else
		  {
		  status = FX_Ok;
		  }

	   if (status == FX_Ok)
		  {
		  if (GlobalMembersGAME.eightytwofifty != 0 && numplayers > 1)
			 {
			 status = FX_Init(GlobalMembersCONFIG.FXDevice, min(GlobalMembersCONFIG.NumVoices,4), 1, 8, 8000);
			 }
		  else
			 {
			 status = FX_Init(GlobalMembersCONFIG.FXDevice, GlobalMembersCONFIG.NumVoices, GlobalMembersCONFIG.NumChannels, GlobalMembersCONFIG.NumBits, GlobalMembersCONFIG.MixRate);
			 }
		  if (status == FX_Ok)
			 {

			 FX_SetVolume(GlobalMembersCONFIG.FXVolume);
			 if (GlobalMembersCONFIG.ReverseStereo == 1)
				{
				FX_SetReverseStereo(!FX_GetReverseStereo());
				}
			 }
		  }
	   if (status != FX_Ok)
		  {
		  Error(FX_ErrorString(FX_Error));
		  }

	   status = FX_SetCallBack(GlobalMembersSOUNDS.TestCallBack);

	   if (status != FX_Ok)
		  {
		  Error(FX_ErrorString(FX_Error));
		  }
	   }

/*
===================
=
= SoundShutdown
=
===================
*/

	public static void SoundShutdown()
	   {
	   int32 status;

	   // if they chose None lets return
	   if (GlobalMembersCONFIG.FXDevice == NumSoundCards)
		  return;

	   status = FX_Shutdown();
	   if (status != FX_Ok)
		  {
		  Error(FX_ErrorString(FX_Error));
		  }
	   }

/*
===================
=
= MusicStartup
=
===================
*/

	public static void MusicStartup()
	   {
	   int32 status;

	   // if they chose None lets return
	   if ((GlobalMembersCONFIG.MusicDevice == NumSoundCards) || (GlobalMembersGAME.eightytwofifty != 0 && numplayers > 1))
		  return;

	   // satisfy AWE32 and WAVEBLASTER stuff
	   GlobalMembersCONFIG.BlasterConfig.Midi = GlobalMembersCONFIG.MidiPort;

	   // Do special Sound Blaster, AWE32 stuff
	   if ((GlobalMembersCONFIG.FXDevice == SoundBlaster) || (GlobalMembersCONFIG.FXDevice == Awe32))
		  {
		  int MaxVoices;
		  int MaxBits;
		  int MaxChannels;

		  FX_SetupSoundBlaster (GlobalMembersCONFIG.BlasterConfig, (int)&MaxVoices, (int)&MaxBits, (int)&MaxChannels);
		  }
	   status = MUSIC_Init(GlobalMembersCONFIG.MusicDevice, GlobalMembersCONFIG.MidiPort);

	   if (status == MUSIC_Ok)
		  {
		  MUSIC_SetVolume(GlobalMembersCONFIG.MusicVolume);
		  }
	   else
	   {
		  Console.WriteLine("Couldn't find selected sound card, or, error w/ sound card itself.");

		  GlobalMembersSOUNDS.SoundShutdown();
		  GlobalMembersGAME.uninittimer();
		  uninitengine();
		  CONTROL_Shutdown();
		  GlobalMembersCONFIG.CONFIG_WriteSetup();
		  KB_Shutdown();
		  uninitgroupfile();
		  unlink("duke3d.tmp");
		  Environment.Exit(-1);
	   }
	}

/*
===================
=
= MusicShutdown
=
===================
*/

	public static void MusicShutdown()
	   {
	   int32 status;

	   // if they chose None lets return
	   if ((GlobalMembersCONFIG.MusicDevice == NumSoundCards) || (GlobalMembersGAME.eightytwofifty != 0 && numplayers > 1))
		  return;

	   status = MUSIC_Shutdown();
	   if (status != MUSIC_Ok)
		  {
		  Error(MUSIC_ErrorString(MUSIC_ErrorCode));
		  }
	   }




	#define LOUDESTVOLUME

	public static int backflag;
	public static int numenvsnds;

	public static int USRHOOKS_GetMem(ref string[] ptr, uint size)
	{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'malloc' has no equivalent in C#:
	   *ptr = malloc(size);

	   if (*ptr == null)
		  return(USRHOOKS_Error);

	   return(USRHOOKS_Ok);

	}

	public static int USRHOOKS_FreeMem(ref string ptr)
	{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'free' has no equivalent in C#:
	   free(ptr);
	   return(USRHOOKS_Ok);
	}

	public static sbyte menunum=0;

	public static void intomenusounds()
	{
		short i;
		short[] menusnds = { LASERTRIP_EXPLODE, DUKE_GRUNT, DUKE_LAND_HURT, CHAINGUN_FIRE, SQUISHED, KICK_HIT, PISTOL_RICOCHET, PISTOL_BODYHIT, PISTOL_FIRE, SHOTGUN_FIRE, BOS1_WALK, RPG_EXPLODE, PIPEBOMB_BOUNCE, PIPEBOMB_EXPLODE, NITEVISION_ONOFF, RPG_SHOOT, SELECT_WEAPON };
		GlobalMembersSOUNDS.sound(menusnds[menunum++]);
		menunum %= 17;
	}

	public static void playmusic(ref string fn)
	{
		short fp;
		int l;

		if(GlobalMembersCONFIG.MusicToggle == 0)
			return;
		if(GlobalMembersCONFIG.MusicDevice == NumSoundCards)
			return;
		if(GlobalMembersGAME.eightytwofifty != 0 && numplayers > 1)
			return;

		fp = kopen4load(fn,0);

		if(fp == -1)
			return;

		l = kfilelength(fp);
		if(l >= 72000)
		{
			kclose(fp);
			return;
		}

		kread(fp, GlobalMembersGLOBAL.MusicPtr, l);
		kclose(fp);
		MUSIC_PlaySong(GlobalMembersGLOBAL.MusicPtr, MUSIC_LoopSong);
	}

	public static sbyte loadsound(ushort num)
	{
		int fp;
		int l;

		if(num >= NUM_SOUNDS || GlobalMembersCONFIG.SoundToggle == 0)
			return 0;
		if (GlobalMembersCONFIG.FXDevice == NumSoundCards)
			return 0;

		fp = kopen4load(GlobalMembersGLOBAL.sounds[num],GlobalMembersGLOBAL.loadfromgrouponly);
		if(fp == -1)
		{
			GlobalMembersGLOBAL.fta_quotes[113, 0] = string.Format("Sound {0}(#{1:D}) not found.", GlobalMembersGLOBAL.sounds[num], num);
			GlobalMembersGAME.FTA(113, ref GlobalMembersGLOBAL.ps[myconnectindex]);
			return 0;
		}

		l = kfilelength(fp);
		GlobalMembersGLOBAL.soundsiz[num] = l;

		GlobalMembersGLOBAL.Sound[num].lock = 200;

		allocache((int)GlobalMembersGLOBAL.Sound[num].ptr,l,GlobalMembersGLOBAL.Sound[num].lock);
		kread(fp, GlobalMembersGLOBAL.Sound[num].ptr, l);
		kclose(fp);
		return 1;
	}

	public static int xyzsound(short num,short i,int x,int y,int z)
	{
		int sndist;
		int cx;
		int cy;
		int cz;
		int j;
		int k;
		short pitche;
		short pitchs;
		short cs;
		int voice;
		int sndang;
		int ca;
		int pitch;

	//    if(num != 358) return 0;

		if(num >= NUM_SOUNDS || GlobalMembersCONFIG.FXDevice == NumSoundCards || ((GlobalMembersGLOBAL.soundm[num]&8) && GlobalMembersGLOBAL.ud.lockout) || GlobalMembersCONFIG.SoundToggle == 0 || GlobalMembersGLOBAL.Sound[num].num > 3 || FX_VoiceAvailable(GlobalMembersGLOBAL.soundpr[num]) == 0 || (GlobalMembersGLOBAL.ps[myconnectindex].timebeforeexit > 0 && GlobalMembersGLOBAL.ps[myconnectindex].timebeforeexit <= 26 *3) || GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU)
			return -1;

		if(GlobalMembersGLOBAL.soundm[num]&128)
		{
			GlobalMembersSOUNDS.sound(num);
			return 0;
		}

		if(GlobalMembersGLOBAL.soundm[num]&4)
		{
			if(GlobalMembersCONFIG.VoiceToggle == 0 || (GlobalMembersGLOBAL.ud.multimode > 1 && PN == APLAYER && sprite[i].yvel != GlobalMembersGLOBAL.screenpeek && GlobalMembersGLOBAL.ud.coop != 1))
				return -1;

			for(j = 0;j<NUM_SOUNDS;j++)
			  for(k = 0;k<GlobalMembersGLOBAL.Sound[j].num;k++)
				if((GlobalMembersGLOBAL.Sound[j].num > 0) && (GlobalMembersGLOBAL.soundm[j]&4))
				  return -1;
		}

		cx = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposx;
		cy = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposy;
		cz = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposz;
		cs = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cursectnum;
		ca = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ang+GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].look_ang;

		sndist = FindDistance3D((cx-x),(cy-y),(cz-z)>>4);

		if(i >= 0 && (GlobalMembersGLOBAL.soundm[num]&16) == 0 && PN == MUSICANDSFX && SLT < 999 && (sector[SECT].lotag &0xff) < 9)
			sndist = divscale14(sndist,(SHT+1));

		pitchs = GlobalMembersGLOBAL.soundps[num];
		pitche = GlobalMembersGLOBAL.soundpe[num];
		cx = klabs(pitche-pitchs);

		if (cx != 0)
		{
			if(pitchs < pitche)
				 pitch = pitchs + (RandomNumbers.NextNumber()%cx);
			else
				pitch = pitche + (RandomNumbers.NextNumber()%cx);
		}
		else
			pitch = pitchs;

		sndist += GlobalMembersGLOBAL.soundvo[num];
		if(sndist < 0)
			sndist = 0;
		if(sndist != 0 && PN != MUSICANDSFX && !cansee(cx,cy,cz-(24<<8),cs,SX,SY,SZ-(24<<8),SECT))
			sndist += sndist>>5;

		switch(num)
		{
			case PIPEBOMB_EXPLODE:
			case LASERTRIP_EXPLODE:
			case RPG_EXPLODE:
				if(sndist > (6144))
					sndist = 6144;
				if(sector[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cursectnum].lotag == 2)
					pitch -= 1024;
				break;
			default:
				if(sector[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cursectnum].lotag == 2 && (GlobalMembersGLOBAL.soundm[num]&4) == 0)
					pitch = -768;
				if(sndist > 31444 && PN != MUSICANDSFX)
					return -1;
				break;
		}


		if(GlobalMembersGLOBAL.Sound[num].num > 0 && PN != MUSICANDSFX)
		{
			if(GlobalMembersGLOBAL.SoundOwner[num, 0].i == i)
				GlobalMembersSOUNDS.stopsound(num);
			else if(GlobalMembersGLOBAL.Sound[num].num > 1)
				GlobalMembersSOUNDS.stopsound(num);
			else if(GlobalMembersGAME.badguy(ref sprite[i]) != 0 && sprite[i].extra <= 0)
				GlobalMembersSOUNDS.stopsound(num);
		}

		if(PN == APLAYER && sprite[i].yvel == GlobalMembersGLOBAL.screenpeek)
		{
			sndang = 0;
			sndist = 0;
		}
		else
		{
			sndang = 2048 + ca - getangle(cx-x,cy-y);
			sndang &= 2047;
		}

		if(GlobalMembersGLOBAL.Sound[num].ptr == 0)
		{
			if(GlobalMembersSOUNDS.loadsound(num) == 0)
				return 0;
		}
		else
		{
		   if (GlobalMembersGLOBAL.Sound[num].lock < 200)
			  GlobalMembersGLOBAL.Sound[num].lock = 200;
		   else
			   GlobalMembersGLOBAL.Sound[num].lock++;
		}

		if(GlobalMembersGLOBAL.soundm[num]&16)
			sndist = 0;

		if(sndist < ((255-DefineConstants.LOUDESTVOLUME)<<6))
			sndist = ((255-DefineConstants.LOUDESTVOLUME)<<6);

		if(GlobalMembersGLOBAL.soundm[num]&1)
		{
			ushort start;

			if(GlobalMembersGLOBAL.Sound[num].num > 0)
				return -1;

			start = (ushort)(GlobalMembersGLOBAL.Sound[num].ptr + 0x14);

			if(*GlobalMembersGLOBAL.Sound[num].ptr == 'C')
				voice = FX_PlayLoopedVOC(GlobalMembersGLOBAL.Sound[num].ptr, start, start + GlobalMembersGLOBAL.soundsiz[num], pitch,sndist>>6,sndist>>6,0,GlobalMembersGLOBAL.soundpr[num],num);
			else
				voice = FX_PlayLoopedWAV(GlobalMembersGLOBAL.Sound[num].ptr, start, start + GlobalMembersGLOBAL.soundsiz[num], pitch,sndist>>6,sndist>>6,0,GlobalMembersGLOBAL.soundpr[num],num);
		}
		else
		{
			if(*GlobalMembersGLOBAL.Sound[num].ptr == 'C')
				voice = FX_PlayVOC3D(GlobalMembersGLOBAL.Sound[num].ptr,pitch,sndang>>6,sndist>>6, GlobalMembersGLOBAL.soundpr[num], num);
			else
				voice = FX_PlayWAV3D(GlobalMembersGLOBAL.Sound[num].ptr,pitch,sndang>>6,sndist>>6, GlobalMembersGLOBAL.soundpr[num], num);
		}

		if (voice > FX_Ok)
		{
			GlobalMembersGLOBAL.SoundOwner[num, GlobalMembersGLOBAL.Sound[num].num].i = i;
			GlobalMembersGLOBAL.SoundOwner[num, GlobalMembersGLOBAL.Sound[num].num].voice = voice;
			GlobalMembersGLOBAL.Sound[num].num++;
		}
		else
			GlobalMembersGLOBAL.Sound[num].lock--;
		return (voice);
	}

	public static void sound(short num)
	{
		short pitch;
		short pitche;
		short pitchs;
		short cx;
		int voice;
		int start;

		if (GlobalMembersCONFIG.FXDevice == NumSoundCards)
			return;
		if(GlobalMembersCONFIG.SoundToggle == 0)
			return;
		if(GlobalMembersCONFIG.VoiceToggle == 0 && (GlobalMembersGLOBAL.soundm[num]&4))
			return;
		if((GlobalMembersGLOBAL.soundm[num]&8) && GlobalMembersGLOBAL.ud.lockout)
			return;
		if(FX_VoiceAvailable(GlobalMembersGLOBAL.soundpr[num]) == 0)
			return;

		pitchs = GlobalMembersGLOBAL.soundps[num];
		pitche = GlobalMembersGLOBAL.soundpe[num];
		cx = klabs(pitche-pitchs);

		if (cx != 0)
		{
			if(pitchs < pitche)
				 pitch = pitchs + (RandomNumbers.NextNumber()%cx);
			else
				pitch = pitche + (RandomNumbers.NextNumber()%cx);
		}
		else
			pitch = pitchs;

		if(GlobalMembersGLOBAL.Sound[num].ptr == 0)
		{
			if(GlobalMembersSOUNDS.loadsound(num) == 0)
				return;
		}
		else
		{
		   if (GlobalMembersGLOBAL.Sound[num].lock < 200)
			  GlobalMembersGLOBAL.Sound[num].lock = 200;
		   else
			   GlobalMembersGLOBAL.Sound[num].lock++;
		}

		if(GlobalMembersGLOBAL.soundm[num]&1)
		{
			if(*GlobalMembersGLOBAL.Sound[num].ptr == 'C')
			{
				start = (int)*(ushort)(GlobalMembersGLOBAL.Sound[num].ptr + 0x14);
				voice = FX_PlayLoopedVOC(GlobalMembersGLOBAL.Sound[num].ptr, start, start + GlobalMembersGLOBAL.soundsiz[num], pitch,DefineConstants.LOUDESTVOLUME,DefineConstants.LOUDESTVOLUME,DefineConstants.LOUDESTVOLUME,GlobalMembersGLOBAL.soundpr[num],num);
			}
			else
			{
				start = (int)*(ushort)(GlobalMembersGLOBAL.Sound[num].ptr + 0x14);
				voice = FX_PlayLoopedWAV(GlobalMembersGLOBAL.Sound[num].ptr, start, start + GlobalMembersGLOBAL.soundsiz[num], pitch,DefineConstants.LOUDESTVOLUME,DefineConstants.LOUDESTVOLUME,DefineConstants.LOUDESTVOLUME,GlobalMembersGLOBAL.soundpr[num],num);
			}
		}
		else
		{
			if(*GlobalMembersGLOBAL.Sound[num].ptr == 'C')
				voice = FX_PlayVOC3D(GlobalMembersGLOBAL.Sound[num].ptr, pitch,0,255-DefineConstants.LOUDESTVOLUME,GlobalMembersGLOBAL.soundpr[num], num);
			else
				voice = FX_PlayWAV3D(GlobalMembersGLOBAL.Sound[num].ptr, pitch,0,255-DefineConstants.LOUDESTVOLUME,GlobalMembersGLOBAL.soundpr[num], num);
		}

		if(voice > FX_Ok)
			return;
		GlobalMembersGLOBAL.Sound[num].lock--;
	}

	public static int spritesound(ushort num, short i)
	{
		if(num >= NUM_SOUNDS)
			return -1;
		return GlobalMembersSOUNDS.xyzsound(num, i, SX, SY, SZ);
	}

	public static void stopsound(short num)
	{
		if(GlobalMembersGLOBAL.Sound[num].num > 0)
		{
			FX_StopSound(GlobalMembersGLOBAL.SoundOwner[num, GlobalMembersGLOBAL.Sound[num].num-1].voice);
			testcallback(num);
		}
	}

	public static void stopenvsound(short num,short i)
	{
		short j;
		short k;

		if(GlobalMembersGLOBAL.Sound[num].num > 0)
		{
			k = GlobalMembersGLOBAL.Sound[num].num;
			for(j = 0;j<k;j++)
			   if(GlobalMembersGLOBAL.SoundOwner[num, j].i == i)
			{
				FX_StopSound(GlobalMembersGLOBAL.SoundOwner[num, j].voice);
				break;
			}
		}
	}

	public static void pan3dsound()
	{
		int sndist;
		int sx;
		int sy;
		int sz;
		int cx;
		int cy;
		int cz;
		short sndang;
		short ca;
		short j;
		short k;
		short i;
		short cs;

		numenvsnds = 0;

		if(GlobalMembersGLOBAL.ud.camerasprite == -1)
		{
			cx = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposx;
			cy = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposy;
			cz = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposz;
			cs = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cursectnum;
			ca = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ang+GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].look_ang;
		}
		else
		{
			cx = sprite[GlobalMembersGLOBAL.ud.camerasprite].x;
			cy = sprite[GlobalMembersGLOBAL.ud.camerasprite].y;
			cz = sprite[GlobalMembersGLOBAL.ud.camerasprite].z;
			cs = sprite[GlobalMembersGLOBAL.ud.camerasprite].sectnum;
			ca = sprite[GlobalMembersGLOBAL.ud.camerasprite].ang;
		}

		for(j = 0;j<NUM_SOUNDS;j++)
			for(k = 0;k<GlobalMembersGLOBAL.Sound[j].num;k++)
		{
			i = GlobalMembersGLOBAL.SoundOwner[j, k].i;

			sx = sprite[i].x;
			sy = sprite[i].y;
			sz = sprite[i].z;

			if(PN == APLAYER && sprite[i].yvel == GlobalMembersGLOBAL.screenpeek)
			{
				sndang = 0;
				sndist = 0;
			}
			else
			{
				sndang = 2048 + ca - getangle(cx-sx,cy-sy);
				sndang &= 2047;
				sndist = FindDistance3D((cx-sx),(cy-sy),(cz-sz)>>4);
				if(i >= 0 && (GlobalMembersGLOBAL.soundm[j]&16) == 0 && PN == MUSICANDSFX && SLT < 999 && (sector[SECT].lotag &0xff) < 9)
					sndist = divscale14(sndist,(SHT+1));
			}

			sndist += GlobalMembersGLOBAL.soundvo[j];
			if(sndist < 0)
				sndist = 0;

			if(sndist != 0 && PN != MUSICANDSFX && !cansee(cx,cy,cz-(24<<8),cs,sx,sy,sz-(24<<8),SECT))
				sndist += sndist>>5;

			if(PN == MUSICANDSFX && SLT < 999)
				numenvsnds++;

			switch(j)
			{
				case PIPEBOMB_EXPLODE:
				case LASERTRIP_EXPLODE:
				case RPG_EXPLODE:
					if(sndist > (6144))
						sndist = (6144);
					break;
				default:
					if(sndist > 31444 && PN != MUSICANDSFX)
					{
						GlobalMembersSOUNDS.stopsound(j);
						continue;
					}
			}

			if(GlobalMembersGLOBAL.Sound[j].ptr == 0 && GlobalMembersSOUNDS.loadsound(j) == 0)
				continue;
			if(GlobalMembersGLOBAL.soundm[j]&16)
				sndist = 0;

			if(sndist < ((255-DefineConstants.LOUDESTVOLUME)<<6))
				sndist = ((255-DefineConstants.LOUDESTVOLUME)<<6);

			FX_Pan3D(GlobalMembersGLOBAL.SoundOwner[j, k].voice,sndang>>6,sndist>>6);
		}
	}

	public static void TestCallBack(int num)
	{
		short tempi;
		short tempj;
		short tempk;

			if(num < 0)
			{
				if(GlobalMembersRTS.lumplockbyte[-num] >= 200)
					GlobalMembersRTS.lumplockbyte[-num]--;
				return;
			}

			tempk = GlobalMembersGLOBAL.Sound[num].num;

			if(tempk > 0)
			{
				if((GlobalMembersGLOBAL.soundm[num]&16) == 0)
					for(tempj = 0;tempj<tempk;tempj++)
				{
					tempi = GlobalMembersGLOBAL.SoundOwner[num, tempj].i;
					if(sprite[tempi].picnum == MUSICANDSFX && sector[sprite[tempi].sectnum].lotag < 3 && sprite[tempi].lotag < 999)
					{
						GlobalMembersGLOBAL.hittype[tempi].temp_data[0] = 0;
						if((tempj + 1) < tempk)
						{
							GlobalMembersGLOBAL.SoundOwner[num, tempj].voice = GlobalMembersGLOBAL.SoundOwner[num, tempk-1].voice;
							GlobalMembersGLOBAL.SoundOwner[num, tempj].i = GlobalMembersGLOBAL.SoundOwner[num, tempk-1].i;
						}
						break;
					}
				}

				GlobalMembersGLOBAL.Sound[num].num--;
				GlobalMembersGLOBAL.SoundOwner[num, tempk-1].i = -1;
			}

			GlobalMembersGLOBAL.Sound[num].lock--;
	}

	public static void clearsoundlocks()
	{
		int i;

		for(i = 0;i<NUM_SOUNDS;i++)
			if(GlobalMembersGLOBAL.Sound[i].lock >= 200)
				GlobalMembersGLOBAL.Sound[i].lock = 199;

		for(i = 0;i<11;i++)
			if(GlobalMembersRTS.lumplockbyte[i] >= 200)
				GlobalMembersRTS.lumplockbyte = StringFunctions.ChangeCharacter(GlobalMembersRTS.lumplockbyte, i, 199);
	}
}

