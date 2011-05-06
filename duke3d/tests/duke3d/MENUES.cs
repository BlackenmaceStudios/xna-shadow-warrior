using System;

public static class GlobalMembersMENUES
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


	//extern sbyte inputloc;
	//extern int recfilep;
	//extern sbyte vgacompatible;
	public static short probey=0;
	public static short lastprobey=0;
	public static short last_menu;
	public static short globalskillsound=-1;
	public static short sh;
	public static short onbar;
	public static short buttonstat;
	public static short deletespot;
	public static short last_zero;
	public static short last_fifty;
	public static short last_threehundred = 0;
	internal static sbyte fileselect = 1;
	internal static sbyte menunamecnt;
	internal static sbyte[,] menuname = new sbyte[256, 17];
	internal static string curpath = new string(new char[80]);
	internal static string menupath = new string(new char[80]);

	// CTW - REMOVED
	/* Error codes */
	/*
	#define eTenBnNotInWindows 3801
	#define eTenBnBadGameIni 3802
	#define eTenBnBadTenIni 3803
	#define eTenBnBrowseCancel 3804
	#define eTenBnBadTenInst 3805
	
	int  tenBnStart(void);
	void tenBnSetBrowseRtn(char *(*rtn)(char *str, int len));
	void tenBnSetExitRtn(void (*rtn)(void));
	void tenBnSetEndRtn(void (*rtn)(void));*/
	// CTW END - REMOVED

	public static void dummyfunc()
	{
	}

	public static void dummymess(int i, ref string c)
	{
	}

	// CTW - REMOVED
	/*
	void TENtext(void)
	{
	    long dacount,dalastcount;
	
	    puts("\nDuke Nukem 3D has been licensed exclusively to TEN (Total");
	    puts("Entertainment Network) for wide-area networked (WAN) multiplayer");
	    puts("games.\n");
	
	    puts("The multiplayer code within Duke Nukem 3D has been highly");
	    puts("customized to run best on TEN, where you'll experience fast and");
	    puts("stable performance, plus other special benefits.\n");
	
	    puts("We do not authorize or recommend the use of Duke Nukem 3D with");
	    puts("gaming services other than TEN.\n");
	
	    puts("Duke Nukem 3D is protected by United States copyright law and");
	    puts("international treaty.\n");
	
	    puts("For the best online multiplayer gaming experience, please call TEN");
	    puts("at 800-8040-TEN, or visit TEN's Web Site at www.ten.net.\n");
	
	    puts("Press any key to continue.\n");
	
	    _bios_timeofday(0,&dacount);
	
	    while( _bios_keybrd(1) == 0 )
	    {
	        _bios_timeofday(0,&dalastcount);
	        if( (dacount+240) < dalastcount ) break;
	    }
	}
	*/
	// CTW END - REMOVED

	public static void cmenu(short cm)
	{
		GlobalMembersGLOBAL.current_menu = cm;

		if((cm >= 1000 && cm <= 1009))
			return;

		if(cm == 0)
			probey = last_zero;
		else if(cm == 50)
			probey = last_fifty;
		else if(cm >= 300 && cm < 400)
			probey = last_threehundred;
		else if(cm == 110)
			probey = 1;
		else
			probey = 0;
		lastprobey = -1;
	}


	public static void savetemp(ref string fn, int daptr, int dasiz)
	{
		int fp;

		fp = open(fn,O_WRONLY|O_CREAT|O_TRUNC|O_BINARY,S_IRUSR|S_IWUSR|S_IRGRP|S_IWGRP);

		write(fp,(string)daptr,dasiz);

		close(fp);
	}

	public static void getangplayers(short snum)
	{
		short i;
		short a;

		for(i = connecthead;i>=0;i = connectpoint2[i])
		{
			if(i != snum)
			{
				a = GlobalMembersGLOBAL.ps[snum].ang+getangle(GlobalMembersGLOBAL.ps[i].posx-GlobalMembersGLOBAL.ps[snum].posx,GlobalMembersGLOBAL.ps[i].posy-GlobalMembersGLOBAL.ps[snum].posy);
				a = a-1024;
				rotatesprite((320<<15) + (((sintable[(a+512)&2047])>>7)<<15), (320<<15) - (((sintable[a &2047])>>8)<<15), klabs(sintable[((a>>1)+768)&2047]<<2), 0, APLAYER, 0, GlobalMembersGLOBAL.ps[i].palookup, 0, 0, 0, xdim-1, ydim-1);
			}
		}
	}

	public static loadpheader(sbyte spot, ref int32 vn, ref int32 ln, ref int32 psk, ref int32 nump)
	{

		 int i;
			 string fn = "game0.sav";
			 int fil;
		 int bv;

			 fn = StringFunctions.ChangeCharacter(fn, 4, spot+'0');

		 if ((fil = kopen4load(fn,0)) == -1)
			 return(-1);

		 walock[MAXTILES-3] = 255;

		 kdfread(bv, 4, 1, fil);
		 if(bv != BYTEVERSION)
		 {
			GlobalMembersGAME.FTA(114, ref GlobalMembersGLOBAL.ps[myconnectindex]);
			kclose(fil);
			return 1;
		 }

		 kdfread(nump,sizeof(int32),1,fil);

		 kdfread(GlobalMembersGLOBAL.tempbuf,19,1,fil);
			 kdfread(vn,sizeof(int32),1,fil);
			 kdfread(ln,sizeof(int32),1,fil);
		 kdfread(psk,sizeof(int32),1,fil);

		 if (waloff[MAXTILES-3] == 0)
			 allocache(waloff[MAXTILES-3], 160 *100, walock[MAXTILES-3]);
		 tilesizx[MAXTILES-3] = 100;
		 tilesizy[MAXTILES-3] = 160;
		 kdfread((string)waloff[MAXTILES-3],160,100,fil);

			 kclose(fil);

			 return(0);
	}


	public static loadplayer(sbyte spot)
	{
		 short k;
		 short music_changed;
		 string fn = "game0.sav";
		 string mpfn = "gameA_00.sav";
		 string fnptr;
		 string scriptptrs = new string(new char[MAXSCRIPTSIZE]);
		 int fil;
		 int bv;
		 int i;
		 int j;
		 int x;
		 int32 nump;

		 if(spot < 0)
		 {
			GlobalMembersGLOBAL.multiflag = 1;
			GlobalMembersGLOBAL.multiwhat = 0;
			GlobalMembersGLOBAL.multipos = -spot-1;
			return -1;
		 }

		 if(GlobalMembersGLOBAL.multiflag == 2 && GlobalMembersGLOBAL.multiwho != myconnectindex)
		 {
			 fnptr = mpfn;
			 mpfn[4] = spot + 'A';

			 if(GlobalMembersGLOBAL.ud.multimode > 9)
			 {
				 mpfn[6] = (GlobalMembersGLOBAL.multiwho/10) + '0';
				 mpfn[7] = (GlobalMembersGLOBAL.multiwho%10) + '0';
			 }
			 else
				 mpfn[7] = GlobalMembersGLOBAL.multiwho + '0';
		 }
		 else
		 {
			fnptr = fn;
			fn[4] = spot + '0';
		 }

		 if ((fil = kopen4load(fnptr,0)) == -1)
			 return(-1);

		 GlobalMembersGLOBAL.ready2send = 0;

		 kdfread(bv, 4, 1, fil);
		 if(bv != BYTEVERSION)
		 {
			GlobalMembersGAME.FTA(114, ref GlobalMembersGLOBAL.ps[myconnectindex]);
			kclose(fil);
			GlobalMembersGLOBAL.ototalclock = totalclock;
			GlobalMembersGLOBAL.ready2send = 1;
			return 1;
		 }

		 kdfread(nump, sizeof(int32), 1, fil);
		 if(nump != numplayers)
		 {
			kclose(fil);
			GlobalMembersGLOBAL.ototalclock = totalclock;
			GlobalMembersGLOBAL.ready2send = 1;
			GlobalMembersGAME.FTA(124, ref GlobalMembersGLOBAL.ps[myconnectindex]);
			return 1;
		 }

		 if(numplayers > 1)
		 {
			 GlobalMembersGLOBAL.pub = NUMPAGES;
			 GlobalMembersGLOBAL.pus = NUMPAGES;
			 GlobalMembersPREMAP.vscrn();
			 GlobalMembersGAME.drawbackground();
			 GlobalMembersMENUES.menutext(160, 100, 0, 0, "LOADING...");
			 nextpage();
		}

		 GlobalMembersPREMAP.waitforeverybody();

			 FX_StopAllSounds();
		 GlobalMembersSOUNDS.clearsoundlocks();
			 MUSIC_StopSong();

		 if(numplayers > 1)
			 kdfread(GlobalMembersGLOBAL.buf,19,1,fil);
		 else
			 kdfread(GlobalMembersGLOBAL.ud.savegame[spot][0],19,1,fil);

		 music_changed = (GlobalMembersGLOBAL.music_select != (GlobalMembersGLOBAL.ud.volume_number *11) + GlobalMembersGLOBAL.ud.level_number);

			 kdfread(GlobalMembersGLOBAL.ud.volume_number,sizeof(GlobalMembersGLOBAL.ud.volume_number),1,fil);
			 kdfread(GlobalMembersGLOBAL.ud.level_number,sizeof(GlobalMembersGLOBAL.ud.level_number),1,fil);
			 kdfread(GlobalMembersGLOBAL.ud.player_skill,sizeof(GlobalMembersGLOBAL.ud.player_skill),1,fil);

			 GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
			 GlobalMembersGLOBAL.ud.m_volume_number = GlobalMembersGLOBAL.ud.volume_number;
			 GlobalMembersGLOBAL.ud.m_player_skill = GlobalMembersGLOBAL.ud.player_skill;

					 //Fake read because lseek won't work with compression
		 walock[MAXTILES-3] = 1;
		 if (waloff[MAXTILES-3] == 0)
			 allocache(waloff[MAXTILES-3], 160 *100, walock[MAXTILES-3]);
		 tilesizx[MAXTILES-3] = 100;
		 tilesizy[MAXTILES-3] = 160;
		 kdfread((string)waloff[MAXTILES-3],160,100,fil);

			 kdfread(numwalls, 2, 1, fil);
		 kdfread(wall[0], sizeof(walltype), MAXWALLS, fil);
			 kdfread(numsectors, 2, 1, fil);
		 kdfread(sector[0], sizeof(sectortype), MAXSECTORS, fil);
			 kdfread(sprite[0], sizeof(spritetype), MAXSPRITES, fil);
			 kdfread(headspritesect[0], 2, MAXSECTORS+1, fil);
			 kdfread(prevspritesect[0], 2, MAXSPRITES, fil);
			 kdfread(nextspritesect[0], 2, MAXSPRITES, fil);
			 kdfread(headspritestat[0], 2, MAXSTATUS+1, fil);
			 kdfread(prevspritestat[0], 2, MAXSPRITES, fil);
			 kdfread(nextspritestat[0], 2, MAXSPRITES, fil);
			 kdfread(GlobalMembersGLOBAL.numcyclers,sizeof(short),1,fil);
			 kdfread(GlobalMembersGLOBAL.cyclers[0, 0],12,MAXCYCLERS,fil);
		 kdfread(GlobalMembersGLOBAL.ps,sizeof(player_struct),1,fil);
		 kdfread(GlobalMembersGLOBAL.po,sizeof(player_orig),1,fil);
			 kdfread(GlobalMembersGLOBAL.numanimwalls,sizeof(short),1,fil);
			 kdfread(GlobalMembersGLOBAL.animwall,sizeof(animwalltype),1,fil);
			 kdfread(GlobalMembersGLOBAL.msx[0],sizeof(int),GlobalMembersGLOBAL.msx.Length,fil);
			 kdfread(GlobalMembersGLOBAL.msy[0],sizeof(int),GlobalMembersGLOBAL.msy.Length,fil);
		 kdfread((short)GlobalMembersGLOBAL.spriteqloc,sizeof(short),1,fil);
		 kdfread((short)GlobalMembersGLOBAL.spriteqamount,sizeof(short),1,fil);
		 kdfread((short)GlobalMembersGLOBAL.spriteq[0],sizeof(short),GlobalMembersGLOBAL.spriteqamount,fil);
			 kdfread(GlobalMembersGLOBAL.mirrorcnt,sizeof(short),1,fil);
			 kdfread(GlobalMembersGLOBAL.mirrorwall[0],sizeof(short),64,fil);
		 kdfread(GlobalMembersGLOBAL.mirrorsector[0],sizeof(short),64,fil);
		 kdfread(show2dsector[0], sizeof(sbyte), MAXSECTORS>>3, fil);
		 kdfread(GlobalMembersGLOBAL.actortype[0],sizeof(sbyte),MAXTILES,fil);
		 kdfread(GlobalMembersGAME.boardfilename[0],sizeof(sbyte),1,fil);

		 kdfread(GlobalMembersGLOBAL.numclouds,sizeof(short),1,fil);
		 kdfread(GlobalMembersGLOBAL.clouds[0],sizeof(short)<<7,1,fil);
		 kdfread(GlobalMembersGLOBAL.cloudx[0],sizeof(short)<<7,1,fil);
		 kdfread(GlobalMembersGLOBAL.cloudy[0],sizeof(short)<<7,1,fil);

		 kdfread(scriptptrs[0], 1, MAXSCRIPTSIZE, fil);
		 kdfread(GlobalMembersGLOBAL.script[0],4,MAXSCRIPTSIZE,fil);
		 for(i = 0;i<MAXSCRIPTSIZE;i++)
			if(scriptptrs[i])
		 {
			 j = (int)GlobalMembersGLOBAL.script[i]+(int)GlobalMembersGLOBAL.script[0];
			 GlobalMembersGLOBAL.script[i] = j;
		 }

		 kdfread(GlobalMembersGLOBAL.actorscrptr[0],4,MAXTILES,fil);
		 for(i = 0;i<MAXTILES;i++)
			 if (GlobalMembersGLOBAL.actorscrptr[i] != 0)
		 {
			j = (int)GlobalMembersGLOBAL.actorscrptr[i]+(int)GlobalMembersGLOBAL.script[0];
			GlobalMembersGLOBAL.actorscrptr[i] = (int)j;
		 }

		 kdfread(scriptptrs[0], 1, MAXSPRITES, fil);
		 kdfread(GlobalMembersGLOBAL.hittype[0],sizeof(weaponhit),MAXSPRITES,fil);

		 for(i = 0;i<MAXSPRITES;i++)
		 {
			j = (int)(GlobalMembersGLOBAL.script[0]);
			if(scriptptrs[i]&1)
				T2 += j;
			if(scriptptrs[i]&2)
				T5 += j;
			if(scriptptrs[i]&4)
				T6 += j;
		 }

			 kdfread(GlobalMembersGLOBAL.lockclock,sizeof(int),1,fil);
		 kdfread(pskybits, sizeof(pskybits), 1, fil);
		 kdfread(pskyoff[0], sizeof(pskyoff[0]), MAXPSKYTILES, fil);

			 kdfread(GlobalMembersGLOBAL.animatecnt,sizeof(int),1,fil);
			 kdfread(GlobalMembersGLOBAL.animatesect[0],2,MAXANIMATES,fil);
			 kdfread(GlobalMembersGLOBAL.animateptr[0],4,MAXANIMATES,fil);
		 for(i = GlobalMembersGLOBAL.animatecnt-1;i>=0;i--)
			 GlobalMembersGLOBAL.animateptr[i] = (int)((int)GlobalMembersGLOBAL.animateptr[i]+(int)(sector[0]));
			 kdfread(GlobalMembersGLOBAL.animategoal[0],4,MAXANIMATES,fil);
			 kdfread(GlobalMembersGLOBAL.animatevel[0],4,MAXANIMATES,fil);

			 kdfread(GlobalMembersGLOBAL.earthquaketime,sizeof(sbyte),1,fil);
		 kdfread(GlobalMembersGLOBAL.ud.from_bonus,sizeof(GlobalMembersGLOBAL.ud.from_bonus),1,fil);
		 kdfread(GlobalMembersGLOBAL.ud.secretlevel,sizeof(GlobalMembersGLOBAL.ud.secretlevel),1,fil);
		 kdfread(GlobalMembersGLOBAL.ud.respawn_monsters,sizeof(GlobalMembersGLOBAL.ud.respawn_monsters),1,fil);
		 GlobalMembersGLOBAL.ud.m_respawn_monsters = GlobalMembersGLOBAL.ud.respawn_monsters;
		 kdfread(GlobalMembersGLOBAL.ud.respawn_items,sizeof(GlobalMembersGLOBAL.ud.respawn_items),1,fil);
		 GlobalMembersGLOBAL.ud.m_respawn_items = GlobalMembersGLOBAL.ud.respawn_items;
		 kdfread(GlobalMembersGLOBAL.ud.respawn_inventory,sizeof(GlobalMembersGLOBAL.ud.respawn_inventory),1,fil);
		 GlobalMembersGLOBAL.ud.m_respawn_inventory = GlobalMembersGLOBAL.ud.respawn_inventory;

		 kdfread(GlobalMembersGLOBAL.ud.god,sizeof(GlobalMembersGLOBAL.ud.god),1,fil);
		 kdfread(GlobalMembersGLOBAL.ud.auto_run,sizeof(GlobalMembersGLOBAL.ud.auto_run),1,fil);
		 kdfread(GlobalMembersGLOBAL.ud.crosshair,sizeof(GlobalMembersGLOBAL.ud.crosshair),1,fil);
		 kdfread(GlobalMembersGLOBAL.ud.monsters_off,sizeof(GlobalMembersGLOBAL.ud.monsters_off),1,fil);
		 GlobalMembersGLOBAL.ud.m_monsters_off = GlobalMembersGLOBAL.ud.monsters_off;
		 kdfread(GlobalMembersGLOBAL.ud.last_level,sizeof(GlobalMembersGLOBAL.ud.last_level),1,fil);
		 kdfread(GlobalMembersGLOBAL.ud.eog,sizeof(GlobalMembersGLOBAL.ud.eog),1,fil);

		 kdfread(GlobalMembersGLOBAL.ud.coop,sizeof(GlobalMembersGLOBAL.ud.coop),1,fil);
		 GlobalMembersGLOBAL.ud.m_coop = GlobalMembersGLOBAL.ud.coop;
		 kdfread(GlobalMembersGLOBAL.ud.marker,sizeof(GlobalMembersGLOBAL.ud.marker),1,fil);
		 GlobalMembersGLOBAL.ud.m_marker = GlobalMembersGLOBAL.ud.marker;
		 kdfread(GlobalMembersGLOBAL.ud.ffire,sizeof(GlobalMembersGLOBAL.ud.ffire),1,fil);
		 GlobalMembersGLOBAL.ud.m_ffire = GlobalMembersGLOBAL.ud.ffire;

		 kdfread(GlobalMembersGLOBAL.camsprite,sizeof(short),1,fil);
		 kdfread(connecthead, sizeof(connecthead), 1, fil);
		 kdfread(connectpoint2,sizeof(connectpoint2),1,fil);
		 kdfread(GlobalMembersGLOBAL.numplayersprites,sizeof(sbyte),1,fil);
		 kdfread((short)GlobalMembersGLOBAL.frags[0, 0],sizeof(short),1,fil);

		 kdfread(randomseed, sizeof(randomseed), 1, fil);
		 kdfread(GlobalMembersGLOBAL.global_random,sizeof(short),1,fil);
		 kdfread(parallaxyscale, sizeof(parallaxyscale), 1, fil);

		 kclose(fil);

		 if(GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on != 0)
		 {
			 GlobalMembersGAME.cameradist = 0;
			 GlobalMembersGAME.cameraclock = 0;
			 GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on = 1;
		 }

		 GlobalMembersGLOBAL.screenpeek = myconnectindex;

		 clearbufbyte(gotpic,sizeof(gotpic),0);
		 GlobalMembersSOUNDS.clearsoundlocks();
			 GlobalMembersPREMAP.cacheit();
		 GlobalMembersPREMAP.docacheit();

		 if(music_changed == 0)
			GlobalMembersGLOBAL.music_select = (GlobalMembersGLOBAL.ud.volume_number *11) + GlobalMembersGLOBAL.ud.level_number;
		 GlobalMembersSOUNDS.playmusic(ref GlobalMembersGLOBAL.music_fn[0, GlobalMembersGLOBAL.music_select, 0]);

		 GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
			 GlobalMembersGLOBAL.ud.recstat = 0;

		 if(GlobalMembersGLOBAL.ps[myconnectindex].jetpack_on)
			 GlobalMembersSOUNDS.spritesound(DUKE_JETPACK_IDLE, GlobalMembersGLOBAL.ps[myconnectindex].i);

		 GlobalMembersGAME.restorepalette = 1;
		 GlobalMembersPLAYER.setpal(ref GlobalMembersGLOBAL.ps[myconnectindex]);
		 GlobalMembersPREMAP.vscrn();

		 FX_SetReverb(0);

		 if(GlobalMembersGLOBAL.ud.lockout == 0)
		 {
			 for(x = 0;x<GlobalMembersGLOBAL.numanimwalls;x++)
				 if(wall[GlobalMembersGLOBAL.animwall[x].wallnum].extra >= 0)
					 wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum = wall[GlobalMembersGLOBAL.animwall[x].wallnum].extra;
		 }
		 else
		 {
			 for(x = 0;x<GlobalMembersGLOBAL.numanimwalls;x++)
				 switch(wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum)
			 {
				 case FEMPIC1:
					 wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum = BLANKSCREEN;
					 break;
				 case FEMPIC2:
				 case FEMPIC3:
					 wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum = SCREENBREAK6;
					 break;
			 }
		 }

		 GlobalMembersGLOBAL.numinterpolations = 0;
		 GlobalMembersGLOBAL.startofdynamicinterpolations = 0;

		 k = headspritestat[3];
		 while(k >= 0)
		 {
			switch(sprite[k].lotag)
			{
				case 31:
					GlobalMembersACTORS.setinterpolation(ref sector[sprite[k].sectnum].floorz);
					break;
				case 32:
					GlobalMembersACTORS.setinterpolation(ref sector[sprite[k].sectnum].ceilingz);
					break;
				case 25:
					GlobalMembersACTORS.setinterpolation(ref sector[sprite[k].sectnum].floorz);
					GlobalMembersACTORS.setinterpolation(ref sector[sprite[k].sectnum].ceilingz);
					break;
				case 17:
					GlobalMembersACTORS.setinterpolation(ref sector[sprite[k].sectnum].floorz);
					GlobalMembersACTORS.setinterpolation(ref sector[sprite[k].sectnum].ceilingz);
					break;
				case 0:
				case 5:
				case 6:
				case 11:
				case 14:
				case 15:
				case 16:
				case 26:
				case 30:
					GlobalMembersACTORS.setsectinterpolate(k);
					break;
			}

			k = nextspritestat[k];
		 }

		 for(i = GlobalMembersGLOBAL.numinterpolations-1;i>=0;i--)
			 GlobalMembersGLOBAL.bakipos[i] = GlobalMembersGLOBAL.curipos[i];
		 for(i = GlobalMembersGLOBAL.animatecnt-1;i>=0;i--)
			 GlobalMembersACTORS.setinterpolation(ref GlobalMembersGLOBAL.animateptr[i]);

		 GlobalMembersGLOBAL.show_shareware = 0;
		 GlobalMembersGLOBAL.everyothertime = 0;

		 clearbufbyte(GlobalMembersGLOBAL.playerquitflag,MAXPLAYERS,0x01010101);

		 GlobalMembersPREMAP.resetmys();

		 GlobalMembersGLOBAL.ready2send = 1;

		 flushpackets();
		 GlobalMembersPREMAP.clearfifo();
		 GlobalMembersPREMAP.waitforeverybody();

		 GlobalMembersPREMAP.resettimevars();

		 return(0);
	}

	public static saveplayer(sbyte spot)
	{
		 int i;
		 int j;
			 string fn = "game0.sav";
		 string mpfn = "gameA_00.sav";
		 string fnptr;
		 string scriptptrs = new string(new char[MAXSCRIPTSIZE]);
			 FILE fil;
		 int bv = BYTEVERSION;

		 if(spot < 0)
		 {
			GlobalMembersGLOBAL.multiflag = 1;
			GlobalMembersGLOBAL.multiwhat = 1;
			GlobalMembersGLOBAL.multipos = -spot-1;
			return -1;
		 }

		 GlobalMembersPREMAP.waitforeverybody();

		 if(GlobalMembersGLOBAL.multiflag == 2 && GlobalMembersGLOBAL.multiwho != myconnectindex)
		 {
			 fnptr = mpfn;
			 mpfn[4] = spot + 'A';

			 if(GlobalMembersGLOBAL.ud.multimode > 9)
			 {
				 mpfn[6] = (GlobalMembersGLOBAL.multiwho/10) + '0';
				 mpfn[7] = GlobalMembersGLOBAL.multiwho + '0';
			 }
			 else
				 mpfn[7] = GlobalMembersGLOBAL.multiwho + '0';
		 }
		 else
		 {
			fnptr = fn;
			fn[4] = spot + '0';
		 }

		 if ((fil = fopen(fnptr,"wb")) == 0)
			 return(-1);

		 GlobalMembersGLOBAL.ready2send = 0;

		 dfwrite(bv, 4, 1, fil);
		 dfwrite(GlobalMembersGLOBAL.ud.multimode,sizeof(GlobalMembersGLOBAL.ud.multimode),1,fil);

			 dfwrite(GlobalMembersGLOBAL.ud.savegame[spot][0],19,1,fil);
			 dfwrite(GlobalMembersGLOBAL.ud.volume_number,sizeof(GlobalMembersGLOBAL.ud.volume_number),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.level_number,sizeof(GlobalMembersGLOBAL.ud.level_number),1,fil);
			 dfwrite(GlobalMembersGLOBAL.ud.player_skill,sizeof(GlobalMembersGLOBAL.ud.player_skill),1,fil);
		 dfwrite((string)waloff[MAXTILES-1],160,100,fil);

			 dfwrite(numwalls, 2, 1, fil);
		 dfwrite(wall[0], sizeof(walltype), MAXWALLS, fil);
			 dfwrite(numsectors, 2, 1, fil);
		 dfwrite(sector[0], sizeof(sectortype), MAXSECTORS, fil);
			 dfwrite(sprite[0], sizeof(spritetype), MAXSPRITES, fil);
			 dfwrite(headspritesect[0], 2, MAXSECTORS+1, fil);
			 dfwrite(prevspritesect[0], 2, MAXSPRITES, fil);
			 dfwrite(nextspritesect[0], 2, MAXSPRITES, fil);
			 dfwrite(headspritestat[0], 2, MAXSTATUS+1, fil);
			 dfwrite(prevspritestat[0], 2, MAXSPRITES, fil);
			 dfwrite(nextspritestat[0], 2, MAXSPRITES, fil);
			 dfwrite(GlobalMembersGLOBAL.numcyclers,sizeof(short),1,fil);
			 dfwrite(GlobalMembersGLOBAL.cyclers[0, 0],12,MAXCYCLERS,fil);
		 dfwrite(GlobalMembersGLOBAL.ps,sizeof(player_struct),1,fil);
		 dfwrite(GlobalMembersGLOBAL.po,sizeof(player_orig),1,fil);
			 dfwrite(GlobalMembersGLOBAL.numanimwalls,sizeof(short),1,fil);
			 dfwrite(GlobalMembersGLOBAL.animwall,sizeof(animwalltype),1,fil);
			 dfwrite(GlobalMembersGLOBAL.msx[0],sizeof(int),GlobalMembersGLOBAL.msx.Length,fil);
			 dfwrite(GlobalMembersGLOBAL.msy[0],sizeof(int),GlobalMembersGLOBAL.msy.Length,fil);
		 dfwrite(GlobalMembersGLOBAL.spriteqloc,sizeof(short),1,fil);
		 dfwrite(GlobalMembersGLOBAL.spriteqamount,sizeof(short),1,fil);
		 dfwrite(GlobalMembersGLOBAL.spriteq[0],sizeof(short),GlobalMembersGLOBAL.spriteqamount,fil);
			 dfwrite(GlobalMembersGLOBAL.mirrorcnt,sizeof(short),1,fil);
			 dfwrite(GlobalMembersGLOBAL.mirrorwall[0],sizeof(short),64,fil);
			 dfwrite(GlobalMembersGLOBAL.mirrorsector[0],sizeof(short),64,fil);
		 dfwrite(show2dsector[0], sizeof(sbyte), MAXSECTORS>>3, fil);
		 dfwrite(GlobalMembersGLOBAL.actortype[0],sizeof(sbyte),MAXTILES,fil);
		 dfwrite(GlobalMembersGAME.boardfilename[0],sizeof(sbyte),1,fil);

		 dfwrite(GlobalMembersGLOBAL.numclouds,sizeof(short),1,fil);
		 dfwrite(GlobalMembersGLOBAL.clouds[0],sizeof(short)<<7,1,fil);
		 dfwrite(GlobalMembersGLOBAL.cloudx[0],sizeof(short)<<7,1,fil);
		 dfwrite(GlobalMembersGLOBAL.cloudy[0],sizeof(short)<<7,1,fil);

		 for(i = 0;i<MAXSCRIPTSIZE;i++)
		 {
			  if((int)GlobalMembersGLOBAL.script[i] >= (int)(GlobalMembersGLOBAL.script[0]) && (int)GlobalMembersGLOBAL.script[i] < (int)(GlobalMembersGLOBAL.script[MAXSCRIPTSIZE]))
			  {
					scriptptrs = StringFunctions.ChangeCharacter(scriptptrs, i, 1);
					j = (int)GlobalMembersGLOBAL.script[i] - (int)GlobalMembersGLOBAL.script[0];
					GlobalMembersGLOBAL.script[i] = j;
			  }
			  else
				  scriptptrs = StringFunctions.ChangeCharacter(scriptptrs, i, 0);
		 }

		 dfwrite(scriptptrs[0], 1, MAXSCRIPTSIZE, fil);
		 dfwrite(GlobalMembersGLOBAL.script[0],4,MAXSCRIPTSIZE,fil);

		 for(i = 0;i<MAXSCRIPTSIZE;i++)
			if(scriptptrs[i])
		 {
			j = GlobalMembersGLOBAL.script[i]+(int)GlobalMembersGLOBAL.script[0];
			GlobalMembersGLOBAL.script[i] = j;
		 }

		 for(i = 0;i<MAXTILES;i++)
			 if (GlobalMembersGLOBAL.actorscrptr[i] != 0)
		 {
			j = (int)GlobalMembersGLOBAL.actorscrptr[i]-(int)GlobalMembersGLOBAL.script[0];
			GlobalMembersGLOBAL.actorscrptr[i] = (int)j;
		 }
		 dfwrite(GlobalMembersGLOBAL.actorscrptr[0],4,MAXTILES,fil);
		 for(i = 0;i<MAXTILES;i++)
			 if (GlobalMembersGLOBAL.actorscrptr[i] != 0)
		 {
			 j = (int)GlobalMembersGLOBAL.actorscrptr[i]+(int)GlobalMembersGLOBAL.script[0];
			 GlobalMembersGLOBAL.actorscrptr[i] = (int)j;
		 }

		 for(i = 0;i<MAXSPRITES;i++)
		 {
			scriptptrs = StringFunctions.ChangeCharacter(scriptptrs, i, 0);

			if(GlobalMembersGLOBAL.actorscrptr[PN] == 0)
				continue;

			j = (int)GlobalMembersGLOBAL.script[0];

			if(T2 >= j && T2 < (int)(GlobalMembersGLOBAL.script[MAXSCRIPTSIZE]))
			{
				scriptptrs[i] |= 1;
				T2 -= j;
			}
			if(T5 >= j && T5 < (int)(GlobalMembersGLOBAL.script[MAXSCRIPTSIZE]))
			{
				scriptptrs[i] |= 2;
				T5 -= j;
			}
			if(T6 >= j && T6 < (int)(GlobalMembersGLOBAL.script[MAXSCRIPTSIZE]))
			{
				scriptptrs[i] |= 4;
				T6 -= j;
			}
		}

		dfwrite(scriptptrs[0], 1, MAXSPRITES, fil);
		dfwrite(GlobalMembersGLOBAL.hittype[0],sizeof(weaponhit),MAXSPRITES,fil);

		for(i = 0;i<MAXSPRITES;i++)
		{
			if(GlobalMembersGLOBAL.actorscrptr[PN] == 0)
				continue;
			j = (int)GlobalMembersGLOBAL.script[0];

			if(scriptptrs[i]&1)
				T2 += j;
			if(scriptptrs[i]&2)
				T5 += j;
			if(scriptptrs[i]&4)
				T6 += j;
		}

			 dfwrite(GlobalMembersGLOBAL.lockclock,sizeof(int),1,fil);
		 dfwrite(pskybits, sizeof(pskybits), 1, fil);
		 dfwrite(pskyoff[0], sizeof(pskyoff[0]), MAXPSKYTILES, fil);
			 dfwrite(GlobalMembersGLOBAL.animatecnt,sizeof(int),1,fil);
			 dfwrite(GlobalMembersGLOBAL.animatesect[0],2,MAXANIMATES,fil);
			 for(i = GlobalMembersGLOBAL.animatecnt-1;i>=0;i--)
				 GlobalMembersGLOBAL.animateptr[i] = (int)((int)GlobalMembersGLOBAL.animateptr[i]-(int)(sector[0]));
			 dfwrite(GlobalMembersGLOBAL.animateptr[0],4,MAXANIMATES,fil);
			 for(i = GlobalMembersGLOBAL.animatecnt-1;i>=0;i--)
				 GlobalMembersGLOBAL.animateptr[i] = (int)((int)GlobalMembersGLOBAL.animateptr[i]+(int)(sector[0]));
			 dfwrite(GlobalMembersGLOBAL.animategoal[0],4,MAXANIMATES,fil);
			 dfwrite(GlobalMembersGLOBAL.animatevel[0],4,MAXANIMATES,fil);

			 dfwrite(GlobalMembersGLOBAL.earthquaketime,sizeof(sbyte),1,fil);
			 dfwrite(GlobalMembersGLOBAL.ud.from_bonus,sizeof(GlobalMembersGLOBAL.ud.from_bonus),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.secretlevel,sizeof(GlobalMembersGLOBAL.ud.secretlevel),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.respawn_monsters,sizeof(GlobalMembersGLOBAL.ud.respawn_monsters),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.respawn_items,sizeof(GlobalMembersGLOBAL.ud.respawn_items),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.respawn_inventory,sizeof(GlobalMembersGLOBAL.ud.respawn_inventory),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.god,sizeof(GlobalMembersGLOBAL.ud.god),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.auto_run,sizeof(GlobalMembersGLOBAL.ud.auto_run),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.crosshair,sizeof(GlobalMembersGLOBAL.ud.crosshair),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.monsters_off,sizeof(GlobalMembersGLOBAL.ud.monsters_off),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.last_level,sizeof(GlobalMembersGLOBAL.ud.last_level),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.eog,sizeof(GlobalMembersGLOBAL.ud.eog),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.coop,sizeof(GlobalMembersGLOBAL.ud.coop),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.marker,sizeof(GlobalMembersGLOBAL.ud.marker),1,fil);
		 dfwrite(GlobalMembersGLOBAL.ud.ffire,sizeof(GlobalMembersGLOBAL.ud.ffire),1,fil);
		 dfwrite(GlobalMembersGLOBAL.camsprite,sizeof(short),1,fil);
		 dfwrite(connecthead, sizeof(connecthead), 1, fil);
		 dfwrite(connectpoint2,sizeof(connectpoint2),1,fil);
		 dfwrite(GlobalMembersGLOBAL.numplayersprites,sizeof(sbyte),1,fil);
		 dfwrite((short)GlobalMembersGLOBAL.frags[0, 0],sizeof(short),1,fil);

		 dfwrite(randomseed, sizeof(randomseed), 1, fil);
		 dfwrite(GlobalMembersGLOBAL.global_random,sizeof(short),1,fil);
		 dfwrite(parallaxyscale, sizeof(parallaxyscale), 1, fil);

			 fclose(fil);

		 if(GlobalMembersGLOBAL.ud.multimode < 2)
		 {
			 GlobalMembersGLOBAL.fta_quotes[122] = "GAME SAVED";
			 GlobalMembersGAME.FTA(122, ref GlobalMembersGLOBAL.ps[myconnectindex]);
		 }

		 GlobalMembersGLOBAL.ready2send = 1;

		 GlobalMembersPREMAP.waitforeverybody();

		 GlobalMembersGLOBAL.ototalclock = totalclock;

		 return(0);
	}

	#define LMB
	#define RMB

	public static ControlInfo minfo = new ControlInfo();

	public static int mi;

	public static int probe(int x,int y,int i,int n)
	{
		short centre;
		short s;

		s = 1+(CONTROL_GetMouseSensitivity()>>4);

		if(GlobalMembersCONFIG.ControllerType == 1 && CONTROL_MousePresent)
		{
			CONTROL_GetInput(minfo);
			mi += minfo.dz;
		}

		else
			minfo.dz = minfo.dyaw = 0;

		if(x == (320>>1))
			centre = 320>>2;
		else
			centre = 0;

		if(!buttonstat)
		{
			if(KB_KeyPressed(sc_UpArrow) || KB_KeyPressed(sc_PgUp) || KB_KeyPressed(sc_kpad_8) || mi < -8192)
			{
				mi = 0;
				KB_ClearKeyDown(sc_UpArrow);
				KB_ClearKeyDown(sc_kpad_8);
				KB_ClearKeyDown(sc_PgUp);
				GlobalMembersSOUNDS.sound(KICK_HIT);

				probey--;
				if(probey < 0)
					probey = n-1;
				minfo.dz = 0;
			}
			if(KB_KeyPressed(sc_DownArrow) || KB_KeyPressed(sc_PgDn) || KB_KeyPressed(sc_kpad_2) || mi > 8192)
			{
				mi = 0;
				KB_ClearKeyDown(sc_DownArrow);
				KB_ClearKeyDown(sc_kpad_2);
				KB_ClearKeyDown(sc_PgDn);
				GlobalMembersSOUNDS.sound(KICK_HIT);
				probey++;
				minfo.dz = 0;
			}
		}

		if(probey >= n)
			probey = 0;

		if (centre != 0)
		{
	//        rotatesprite(((320>>1)+(centre)+54)<<16,(y+(probey*i)-4)<<16,65536L,0,SPINNINGNUKEICON+6-((6+(totalclock>>3))%7),sh,0,10,0,0,xdim-1,ydim-1);
	//        rotatesprite(((320>>1)-(centre)-54)<<16,(y+(probey*i)-4)<<16,65536L,0,SPINNINGNUKEICON+((totalclock>>3)%7),sh,0,10,0,0,xdim-1,ydim-1);

			rotatesprite(((320>>1)+(centre>>1)+70)<<16,(y+(probey *i)-4)<<16,65536,0,SPINNINGNUKEICON+6-((6+(totalclock>>3))%7),sh,0,10,0,0,xdim-1,ydim-1);
			rotatesprite(((320>>1)-(centre>>1)-70)<<16,(y+(probey *i)-4)<<16,65536,0,SPINNINGNUKEICON+((totalclock>>3)%7),sh,0,10,0,0,xdim-1,ydim-1);
		}
		else
			rotatesprite((x-tilesizx[BIGFNTCURSOR]-4)<<16,(y+(probey *i)-4)<<16,65536,0,SPINNINGNUKEICON+(((totalclock>>3))%7),sh,0,10,0,0,xdim-1,ydim-1);

		if(KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_Enter) || (buttonstat &1 && !onbar))
		{
			if(GlobalMembersGLOBAL.current_menu != 110)
				GlobalMembersSOUNDS.sound(PISTOL_BODYHIT);
			KB_ClearKeyDown(sc_Enter);
			KB_ClearKeyDown(sc_Space);
			KB_ClearKeyDown(sc_kpad_Enter);
			return(probey);
		}
		else if(KB_KeyPressed(sc_Escape) || (buttonstat &2))
		{
			onbar = 0;
			KB_ClearKeyDown(sc_Escape);
			GlobalMembersSOUNDS.sound(EXITMENUSOUND);
			return(-1);
		}
		else
		{
			if(onbar == 0)
				return(-probey-2);
			if (KB_KeyPressed(sc_LeftArrow) || KB_KeyPressed(sc_kpad_4) || ((buttonstat &1) && minfo.dyaw < -128))
				return(probey);
			else if (KB_KeyPressed(sc_RightArrow) || KB_KeyPressed(sc_kpad_6) || ((buttonstat &1) && minfo.dyaw > 128))
				return(probey);
			else
				return(-probey-2);
		}
	}

	public static int menutext(int x, int y, short s, short p, ref string t)
	{
		short i;
		short ac;
		short centre;

		y -= 12;

		i = centre = 0;

		if(x == (320>>1))
		{
			while(*t.Substring(i))
			{
				if(*t.Substring(i) == ' ')
				{
					centre += 5;
					i++;
					continue;
				}
				ac = 0;
				if(*t.Substring(i) >= '0' && *t.Substring(i) <= '9')
					ac = *t.Substring(i) - '0' + BIGALPHANUM-10;
				else if(*t.Substring(i) >= 'a' && *t.Substring(i) <= 'z')
					ac = char.ToUpper(*t.Substring(i)) - 'A' + BIGALPHANUM;
				else if(*t.Substring(i) >= 'A' && *t.Substring(i) <= 'Z')
					ac = *t.Substring(i) - 'A' + BIGALPHANUM;
				else
					switch(*t.Substring(i))
				{
					case '-':
						ac = BIGALPHANUM-11;
						break;
					case '.':
						ac = BIGPERIOD;
						break;
					case '\'':
						ac = BIGAPPOS;
						break;
					case ',':
						ac = BIGCOMMA;
						break;
					case '!':
						ac = BIGX;
						break;
					case '?':
						ac = BIGQ;
						break;
					case ';':
						ac = BIGSEMI;
						break;
					case ':':
						ac = BIGSEMI;
						break;
					default:
						centre += 5;
						i++;
						continue;
				}

				centre += tilesizx[ac]-1;
				i++;
			}
		}

		if (centre != 0)
			x = (320-centre-10)>>1;

		while (t != null)
		{
			if(t == ' ')
			{
				x+=5;
				t = t.Substring(1);
				continue;
			}
			ac = 0;
			if(t >= '0' && t <= '9')
				ac = t - '0' + BIGALPHANUM-10;
			else if(t >= 'a' && t <= 'z')
				ac = char.ToUpper(t) - 'A' + BIGALPHANUM;
			else if(t >= 'A' && t <= 'Z')
				ac = t - 'A' + BIGALPHANUM;
			else
				switch(t)
			{
				case '-':
					ac = BIGALPHANUM-11;
					break;
				case '.':
					ac = BIGPERIOD;
					break;
				case ',':
					ac = BIGCOMMA;
					break;
				case '!':
					ac = BIGX;
					break;
				case '\'':
					ac = BIGAPPOS;
					break;
				case '?':
					ac = BIGQ;
					break;
				case ';':
					ac = BIGSEMI;
					break;
				case ':':
					ac = BIGCOLIN;
					break;
				default:
					x += 5;
					t = t.Substring(1);
					continue;
			}

			rotatesprite(x<<16,y<<16,65536,0,ac,s,p,10+16,0,0,xdim-1,ydim-1);

			x += tilesizx[ac];
			t = t.Substring(1);
		}
		return (x);
	}

	public static int menutextc(int x, int y, short s, short p, ref string t)
	{
		short i;
		short ac;
		short centre;

		s += 8;
		y -= 12;

		i = centre = 0;

	//    if( x == (320>>1) )
		{
			while(*t.Substring(i))
			{
				if(*t.Substring(i) == ' ')
				{
					centre += 5;
					i++;
					continue;
				}
				ac = 0;
				if(*t.Substring(i) >= '0' && *t.Substring(i) <= '9')
					ac = *t.Substring(i) - '0' + BIGALPHANUM+26+26;
				if(*t.Substring(i) >= 'a' && *t.Substring(i) <= 'z')
					ac = *t.Substring(i) - 'a' + BIGALPHANUM+26;
				if(*t.Substring(i) >= 'A' && *t.Substring(i) <= 'Z')
					ac = *t.Substring(i) - 'A' + BIGALPHANUM;

				else
					switch(t)
				{
					case '-':
						ac = BIGALPHANUM-11;
						break;
					case '.':
						ac = BIGPERIOD;
						break;
					case ',':
						ac = BIGCOMMA;
						break;
					case '!':
						ac = BIGX;
						break;
					case '?':
						ac = BIGQ;
						break;
					case ';':
						ac = BIGSEMI;
						break;
					case ':':
						ac = BIGCOLIN;
						break;
				}

				centre += tilesizx[ac]-1;
				i++;
			}
		}

		x -= centre>>1;

		while (t != null)
		{
			if(t == ' ')
			{
				x+=5;
				t = t.Substring(1);
				continue;
			}
			ac = 0;
			if(t >= '0' && t <= '9')
				ac = t - '0' + BIGALPHANUM+26+26;
			if(t >= 'a' && t <= 'z')
				ac = t - 'a' + BIGALPHANUM+26;
			if(t >= 'A' && t <= 'Z')
				ac = t - 'A' + BIGALPHANUM;
			switch(t)
			{
				case '-':
					ac = BIGALPHANUM-11;
					break;
				case '.':
					ac = BIGPERIOD;
					break;
				case ',':
					ac = BIGCOMMA;
					break;
				case '!':
					ac = BIGX;
					break;
				case '?':
					ac = BIGQ;
					break;
				case ';':
					ac = BIGSEMI;
					break;
				case ':':
					ac = BIGCOLIN;
					break;
			}

			rotatesprite(x<<16,y<<16,65536,0,ac,s,p,10+16,0,0,xdim-1,ydim-1);

			x += tilesizx[ac];
			t = t.Substring(1);
		}
		return (x);
	}


	public static void bar(int x, int y, ref short p, short dainc, sbyte damodify, short s, short pa)
	{
		short xloc;
		sbyte rev;

		if(dainc < 0)
		{
			dainc = -dainc;
			rev = 1;
		}
		else
			rev = 0;
		y-=2;

		if (damodify != 0)
		{
			if(rev == 0)
			{
				if(KB_KeyPressed(sc_LeftArrow) || KB_KeyPressed(sc_kpad_4) || ((buttonstat &1) && minfo.dyaw < -256)) // && onbar) )
				{
					KB_ClearKeyDown(sc_LeftArrow);
					KB_ClearKeyDown(sc_kpad_4);

					p -= dainc;
					if(p < 0)
						p = 0;
					GlobalMembersSOUNDS.sound(KICK_HIT);
				}
				if(KB_KeyPressed(sc_RightArrow) || KB_KeyPressed(sc_kpad_6) || ((buttonstat &1) && minfo.dyaw > 256)) //&& onbar) )
				{
					KB_ClearKeyDown(sc_RightArrow);
					KB_ClearKeyDown(sc_kpad_6);

					p += dainc;
					if(p > 63)
						p = 63;
					GlobalMembersSOUNDS.sound(KICK_HIT);
				}
			}
			else
			{
				if(KB_KeyPressed(sc_RightArrow) || KB_KeyPressed(sc_kpad_6) || ((buttonstat &1) && minfo.dyaw > 256)) //&& onbar ))
				{
					KB_ClearKeyDown(sc_RightArrow);
					KB_ClearKeyDown(sc_kpad_6);

					p -= dainc;
					if(p < 0)
						p = 0;
					GlobalMembersSOUNDS.sound(KICK_HIT);
				}
				if(KB_KeyPressed(sc_LeftArrow) || KB_KeyPressed(sc_kpad_4) || ((buttonstat &1) && minfo.dyaw < -256)) // && onbar) )
				{
					KB_ClearKeyDown(sc_LeftArrow);
					KB_ClearKeyDown(sc_kpad_4);

					p += dainc;
					if(p > 64)
						p = 64;
					GlobalMembersSOUNDS.sound(KICK_HIT);
				}
			}
		}

		xloc = p;

		rotatesprite((x+22)<<16,(y-3)<<16,65536,0,SLIDEBAR,s,pa,10,0,0,xdim-1,ydim-1);
		if(rev == 0)
			rotatesprite((x+xloc+1)<<16,(y+1)<<16,65536,0,SLIDEBAR+1,s,pa,10,0,0,xdim-1,ydim-1);
		else
			rotatesprite((x+(65-xloc))<<16,(y+1)<<16,65536,0,SLIDEBAR+1,s,pa,10,0,0,xdim-1,ydim-1);
	}

	#define SHX
	// ((x==X)*(-sh))
	#define PHX
	// ((x==X)?1:2)
	#define MWIN
	#define MWINXY


	public static int32 volnum;
	public static int32 levnum;
	public static int32 plrskl;
	public static int32 numplr;
	public static short lastsavedpos = -1;

	public static void dispnames()
	{
		short x;
		short c = 160;

		c += 64;
		for(x = 0;x <= 108;x += 12)
		rotatesprite((c+91-64)<<16,(x+56)<<16,65536,0,TEXTBOX,24,0,10,0,0,xdim-1,ydim-1);

		rotatesprite(22<<16,97<<16,65536,0,WINDOWBORDER2,24,0,10,0,0,xdim-1,ydim-1);
		rotatesprite(180<<16,97<<16,65536,1024,WINDOWBORDER2,24,0,10,0,0,xdim-1,ydim-1);
		rotatesprite(99<<16,50<<16,65536,512,WINDOWBORDER1,24,0,10,0,0,xdim-1,ydim-1);
		rotatesprite(103<<16,144<<16,65536,1024+512,WINDOWBORDER1,24,0,10,0,0,xdim-1,ydim-1);

		GlobalMembersGAME.minitext(c, 48, ref GlobalMembersGLOBAL.ud.savegame[0], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12, ref GlobalMembersGLOBAL.ud.savegame[1], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12, ref GlobalMembersGLOBAL.ud.savegame[2], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12+12, ref GlobalMembersGLOBAL.ud.savegame[3], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12+12+12, ref GlobalMembersGLOBAL.ud.savegame[4], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12+12+12+12, ref GlobalMembersGLOBAL.ud.savegame[5], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12+12+12+12+12, ref GlobalMembersGLOBAL.ud.savegame[6], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12+12+12+12+12+12, ref GlobalMembersGLOBAL.ud.savegame[7], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12+12+12+12+12+12+12, ref GlobalMembersGLOBAL.ud.savegame[8], 2, 10+16);
		GlobalMembersGAME.minitext(c, 48+12+12+12+12+12+12+12+12+12, ref GlobalMembersGLOBAL.ud.savegame[9], 2, 10+16);

	}

	public static getfilenames(ref string kind)
	{
			short type;
			find_t fileinfo = new find_t();

			if (string.Compare(kind,"SUBD") == 0)
			{
					kind = "*.*";
					if (_dos_findfirst(kind, _A_SUBDIR, fileinfo) != 0)
							return(-1);
					type = 1;
			}
			else
			{
					if (_dos_findfirst(kind, _A_NORMAL, fileinfo) != 0)
							return(-1);
					type = 0;
			}
			do
			{
					if ((type == 0) || ((fileinfo.attrib &16) > 0))
							if ((fileinfo.name[0] != '.') || (fileinfo.name[1] != 0))
							{
									menuname[menunamecnt] = fileinfo.name;
									menuname[menunamecnt][16] = type;
									menunamecnt++;
							}
			}
			while (_dos_findnext(fileinfo) == 0);

			return(0);
	}

	public static void sortfilenames()
	{
			string sortbuffer = new string(new char[17]);
			int i;
			int j;
			int k;

			for(i = 1;i<menunamecnt;i++)
					for(j = 0;j<i;j++)
					{
							 k = 0;
							 while ((menuname[i][k] == menuname[j][k]) && (menuname[i][k] != 0) && (menuname[j][k] != 0))
									 k++;
							if (menuname[i][k] < menuname[j][k])
							{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
									memcpy(sortbuffer[0], menuname[i][0], sizeof(menuname[0]));
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
									memcpy(menuname[i][0], menuname[j][0], sizeof(menuname[0]));
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
									memcpy(menuname[j][0], sortbuffer[0], sizeof(menuname[0]));
							}
					}
	}

	public static int quittimer = 0;

	public static void menus()
	{
		short c;
		short x;
		int l;
	// CTW - REMOVED
	//  int tenerr;
	// CTW END - REMOVED

		GlobalMembersGAME.getpackets();

		if(GlobalMembersCONFIG.ControllerType == 1 && CONTROL_MousePresent)
		{
			if(buttonstat != 0 && !onbar)
			{
				x = MOUSE_GetButtons()<<3;
				if (x != 0)
					buttonstat = x<<3;
				else
					buttonstat = 0;
			}
			else
				buttonstat = MOUSE_GetButtons();
		}
		else
			buttonstat = 0;

		if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) == 0)
		{
			walock[MAXTILES-3] = 1;
			return;
		}

		GlobalMembersGLOBAL.ps[myconnectindex].gm &= (0xff-MODE_TYPE);
		GlobalMembersGLOBAL.ps[myconnectindex].fta = 0;

		x = 0;

		sh = 4-(sintable[(totalclock<<4)&2047]>>11);

		if(!(GlobalMembersGLOBAL.current_menu >= 1000 && GlobalMembersGLOBAL.current_menu <= 2999 && GlobalMembersGLOBAL.current_menu >= 300 && GlobalMembersGLOBAL.current_menu <= 369))
			GlobalMembersPREMAP.vscrn();

		switch(GlobalMembersGLOBAL.current_menu)
		{
			case 25000:
				GlobalMembersGAME.gametext(160, 90, "SELECT A SAVE SPOT BEFORE", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 90+9, "YOU QUICK RESTORE.", 0, 2+8+16);

				x = GlobalMembersMENUES.probe(186, 124, 0, 0);
				if(x >= -1)
				{
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 1;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}
					GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
				}
				break;

			case 20000:
				x = GlobalMembersMENUES.probe(326, 190, 0, 0);
				GlobalMembersGAME.gametext(160, 50-8, "YOU ARE PLAYING THE SHAREWARE", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 59-8, "VERSION OF DUKE NUKEM 3D.  WHILE", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 68-8, "THIS VERSION IS REALLY COOL, YOU", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 77-8, "ARE MISSING OVER 75% OF THE TOTAL", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 86-8, "GAME, ALONG WITH OTHER GREAT EXTRAS", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 95-8, "AND GAMES, WHICH YOU'LL GET WHEN", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 104-8, "YOU ORDER THE COMPLETE VERSION AND", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 113-8, "GET THE FINAL TWO EPISODES.", 0, 2+8+16);

				GlobalMembersGAME.gametext(160, 113+8, "PLEASE READ THE 'HOW TO ORDER' ITEM", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 122+8, "ON THE MAIN MENU IF YOU WISH TO", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 131+8, "UPGRADE TO THE FULL REGISTERED", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 140+8, "VERSION OF DUKE NUKEM 3D.", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 149+16, "PRESS ANY KEY...", 0, 2+8+16);

				if(x >= -1)
					GlobalMembersMENUES.cmenu(100);
				break;
	// CTW - REMOVED
	/*      case 20001:
	            x = probe(188,80+32+32,0,0);
	            gametext(160,86-8,"You must be in Windows 95 to",0,2+8+16);
	            gametext(160,86,"play on TEN",0,2+8+16);
	            gametext(160,86+32,"PRESS ANY KEY...",0,2+8+16);
	            if(x >= -1) cmenu(0);
	            break;
	
	        case 20002:
	            x = probe(188,80+32+32+32,0,0);
	            gametext(160,86-8,"MISSING FILE: TENGAME.INI.  PLEASE",0,2+8+16);
	            gametext(160,86,"CONNECT TO TEN BY LAUNCHING THE",0,2+8+16);
	            gametext(160,86+8,"CONNECT TO TEN SHORTCUT OR CONTACT",0,2+8+16);
	            gametext(160,86+8+8,"CUSTOMER SUPPORT AT 1-800-8040-TEN.",0,2+8+16);
	            gametext(160,86+8+8+32,"PRESS ANY KEY...",0,2+8+16);
	            if(x >= -1) cmenu(0);
	            break;
	        case 20003:
	            x = probe(188,80+32+32,0,0);
	            gametext(160,86-8,"BAD TEN INSTALL:  PLEASE RE-INSTALL",0,2+8+16);
	            gametext(160,86,"BAD TEN INSTALL:  PLEASE RE-INSTALL TEN",0,2+8+16);
	            gametext(160,86+32,"PRESS ANY KEY...",0,2+8+16);
	            if(x >= -1) cmenu(0);
	            break;
	        case 20005:
	            x = probe(188,80+32+32,0,0);
	            gametext(160,86-8,"GET THE LATEST TEN SOFTWARE AT",0,2+8+16);
	            gametext(160,86,"HTTP://WWW.TEN.NET",0,2+8+16);
	            gametext(160,86+32,"PRESS ANY KEY...",0,2+8+16);
	            if(x >= -1) cmenu(0);
	            break;*/
	// CTW END - REMOVED

			case 15001:
			case 15000:

				GlobalMembersGAME.gametext(160, 90, "LOAD last game:", 0, 2+8+16);

				GlobalMembersGLOBAL.tempbuf = string.Format("\"{0}\"", GlobalMembersGLOBAL.ud.savegame[lastsavedpos]);
				GlobalMembersGAME.gametext(160, 99, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

				GlobalMembersGAME.gametext(160, 99+9, "(Y/N)", 0, 2+8+16);

				if(KB_KeyPressed(sc_Escape) || KB_KeyPressed(sc_N) || buttonstat &2)
				{
					if(sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].extra <= 0)
					{
						GlobalMembersPREMAP.enterlevel(MODE_GAME);
						return;
					}

					KB_ClearKeyDown(sc_N);
					KB_ClearKeyDown(sc_Escape);

					GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 1;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}
				}

				if(KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_Y) || buttonstat &1)
				{
					KB_FlushKeyboardQueue();
					FX_StopAllSounds();

					if(GlobalMembersGLOBAL.ud.multimode > 1)
					{
						GlobalMembersMENUES.loadplayer(-1-lastsavedpos);
						GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
					}
					else
					{
						c = GlobalMembersMENUES.loadplayer(lastsavedpos);
						if(c == 0)
							GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
					}
				}

				GlobalMembersMENUES.probe(186, 124+9, 0, 0);

				break;

			case 10000:
			case 10001:

				c = 60;
				rotatesprite(160<<16,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(160, 24, 0, 0, "ADULT MODE");

				x = GlobalMembersMENUES.probe(60, 50+16, 16, 2);
				if(x == -1)
				{
					GlobalMembersMENUES.cmenu(200);
					break;
				}

				GlobalMembersMENUES.menutext(c, 50+16, (X) 0(-2), (X) 0(-2), "ADULT MODE");
				GlobalMembersMENUES.menutext(c, 50+16+16, (X) 0(-3), (X) 0(-3), "ENTER PASSWORD");

				if(GlobalMembersGLOBAL.ud.lockout)
					GlobalMembersMENUES.menutext(c+160+40, 50+16, 0, 0, "OFF");
				else
					GlobalMembersMENUES.menutext(c+160+40, 50+16, 0, 0, "ON");

				if(GlobalMembersGLOBAL.current_menu == 10001)
				{
					GlobalMembersGAME.gametext(160, 50+16+16+16+16-12, "ENTER PASSWORD", 0, 2+8+16);
					x = GlobalMembersGAME.strget((320>>1), 50+16+16+16+16, ref GlobalMembersGLOBAL.buf, 19, 998);

					if (x != 0)
					{
						if(GlobalMembersGLOBAL.ud.pwlockout[0] == 0 || GlobalMembersGLOBAL.ud.lockout == 0)
							GlobalMembersGLOBAL.ud.pwlockout[0] = GlobalMembersGLOBAL.buf;
						else if(string.Compare(GlobalMembersGLOBAL.buf,GlobalMembersGLOBAL.ud.pwlockout[0]) == 0)
						{
							GlobalMembersGLOBAL.ud.lockout = 0;
							GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 0, 0);

							for(x = 0;x<GlobalMembersGLOBAL.numanimwalls;x++)
								if(wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum != W_SCREENBREAK && wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum != W_SCREENBREAK+1 && wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum != W_SCREENBREAK+2)
										if(wall[GlobalMembersGLOBAL.animwall[x].wallnum].extra >= 0)
											wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum = wall[GlobalMembersGLOBAL.animwall[x].wallnum].extra;

						}
						GlobalMembersGLOBAL.current_menu = 10000;
						KB_ClearKeyDown(sc_Enter);
						KB_ClearKeyDown(sc_kpad_Enter);
						KB_FlushKeyboardQueue();
					}
				}
				else
				{
					if(x == 0)
					{
						if(GlobalMembersGLOBAL.ud.lockout == 1)
						{
							if(GlobalMembersGLOBAL.ud.pwlockout[0] == 0)
							{
								GlobalMembersGLOBAL.ud.lockout = 0;
								for(x = 0;x<GlobalMembersGLOBAL.numanimwalls;x++)
								if(wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum != W_SCREENBREAK && wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum != W_SCREENBREAK+1 && wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum != W_SCREENBREAK+2)
										if(wall[GlobalMembersGLOBAL.animwall[x].wallnum].extra >= 0)
											wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum = wall[GlobalMembersGLOBAL.animwall[x].wallnum].extra;
							}
							else
							{
								GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 0, 0);
								GlobalMembersGLOBAL.current_menu = 10001;
								GlobalMembersGAME.inputloc = 0;
								KB_FlushKeyboardQueue();
							}
						}
						else
						{
							GlobalMembersGLOBAL.ud.lockout = 1;

							for(x = 0;x<GlobalMembersGLOBAL.numanimwalls;x++)
								switch(wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum)
								{
									case FEMPIC1:
										wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum = BLANKSCREEN;
										break;
									case FEMPIC2:
									case FEMPIC3:
										wall[GlobalMembersGLOBAL.animwall[x].wallnum].picnum = SCREENBREAK6;
										break;
								}
						}
					}

					else if(x == 1)
					{
						GlobalMembersGLOBAL.current_menu = 10001;
						GlobalMembersGAME.inputloc = 0;
						KB_FlushKeyboardQueue();
					}
				}

				break;

			case 1000:
			case 1001:
			case 1002:
			case 1003:
			case 1004:
			case 1005:
			case 1006:
			case 1007:
			case 1008:
			case 1009:

				rotatesprite(160<<16,200<<15,65536,0,MENUSCREEN,16,0,10+64,0,0,xdim-1,ydim-1);
				rotatesprite(160<<16,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(160, 24, 0, 0, "LOAD GAME");
				rotatesprite(101<<16,97<<16,65536,512,MAXTILES-3,-32,0,4+10+64,0,0,xdim-1,ydim-1);

				GlobalMembersMENUES.dispnames();

				GlobalMembersGLOBAL.tempbuf = string.Format("PLAYERS: {0,-2:D}                      ", numplr);
				GlobalMembersGAME.gametext(160, 158, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

				GlobalMembersGLOBAL.tempbuf = string.Format("EPISODE: {0,-2:D} / LEVEL: {1,-2:D} / SKILL: {2,-2:D}", 1+volnum, 1+levnum, plrskl);
				GlobalMembersGAME.gametext(160, 170, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

				GlobalMembersGAME.gametext(160, 90, "LOAD game:", 0, 2+8+16);
				GlobalMembersGLOBAL.tempbuf = string.Format("\"{0}\"", GlobalMembersGLOBAL.ud.savegame[GlobalMembersGLOBAL.current_menu-1000]);
				GlobalMembersGAME.gametext(160, 99, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 99+9, "(Y/N)", 0, 2+8+16);

				if(KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_Y) || buttonstat &1)
				{
					lastsavedpos = GlobalMembersGLOBAL.current_menu-1000;

					KB_FlushKeyboardQueue();
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 1;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}

					if(GlobalMembersGLOBAL.ud.multimode > 1)
					{
						if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
						{
							GlobalMembersMENUES.loadplayer(-1-lastsavedpos);
							GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
						}
						else
						{
							GlobalMembersGLOBAL.tempbuf[0] = 126;
							GlobalMembersGLOBAL.tempbuf[1] = lastsavedpos;
							for(x = connecthead;x>=0;x = connectpoint2[x])
								if(x != myconnectindex)
									sendpacket(x,GlobalMembersGLOBAL.tempbuf,2);

							GlobalMembersGAME.getpackets();

							GlobalMembersMENUES.loadplayer(lastsavedpos);

							GlobalMembersGLOBAL.multiflag = 0;
						}
					}
					else
					{
						c = GlobalMembersMENUES.loadplayer(lastsavedpos);
						if(c == 0)
							GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
					}

					break;
				}
				if(KB_KeyPressed(sc_N) || KB_KeyPressed(sc_Escape) || buttonstat &2)
				{
					KB_ClearKeyDown(sc_N);
					KB_ClearKeyDown(sc_Escape);
					GlobalMembersSOUNDS.sound(EXITMENUSOUND);
					if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_DEMO)
						GlobalMembersMENUES.cmenu(300);
					else
					{
						GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
						if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
						{
							GlobalMembersGLOBAL.ready2send = 1;
							totalclock = GlobalMembersGLOBAL.ototalclock;
						}
					}
				}

				GlobalMembersMENUES.probe(186, 124+9, 0, 0);

				break;

			case 1500:

				if(KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_Y) || buttonstat &1)
				{
					KB_FlushKeyboardQueue();
					GlobalMembersMENUES.cmenu(100);
				}
				if(KB_KeyPressed(sc_N) || KB_KeyPressed(sc_Escape) || buttonstat &2)
				{
					KB_ClearKeyDown(sc_N);
					KB_ClearKeyDown(sc_Escape);
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 1;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}
					GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
					GlobalMembersSOUNDS.sound(EXITMENUSOUND);
					break;
				}
				GlobalMembersMENUES.probe(186, 124, 0, 0);
				GlobalMembersGAME.gametext(160, 90, "ABORT this game?", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 90+9, "(Y/N)", 0, 2+8+16);

				break;

			case 2000:
			case 2001:
			case 2002:
			case 2003:
			case 2004:
			case 2005:
			case 2006:
			case 2007:
			case 2008:
			case 2009:

				rotatesprite(160<<16,200<<15,65536,0,MENUSCREEN,16,0,10+64,0,0,xdim-1,ydim-1);
				rotatesprite(160<<16,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(160, 24, 0, 0, "SAVE GAME");

				rotatesprite(101<<16,97<<16,65536,512,MAXTILES-3,-32,0,4+10+64,0,0,xdim-1,ydim-1);
				GlobalMembersGLOBAL.tempbuf = string.Format("PLAYERS: {0,-2:D}                      ", GlobalMembersGLOBAL.ud.multimode);
				GlobalMembersGAME.gametext(160, 158, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

				GlobalMembersGLOBAL.tempbuf = string.Format("EPISODE: {0,-2:D} / LEVEL: {1,-2:D} / SKILL: {2,-2:D}", 1+GlobalMembersGLOBAL.ud.volume_number, 1+GlobalMembersGLOBAL.ud.level_number, GlobalMembersGLOBAL.ud.player_skill);
				GlobalMembersGAME.gametext(160, 170, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

				GlobalMembersMENUES.dispnames();

				GlobalMembersGAME.gametext(160, 90, "OVERWRITE previous SAVED game?", 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 90+9, "(Y/N)", 0, 2+8+16);

				if(KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_Y) || buttonstat &1)
				{
					KB_FlushKeyboardQueue();
					GlobalMembersGAME.inputloc = Convert.ToString(GlobalMembersGLOBAL.ud.savegame[GlobalMembersGLOBAL.current_menu-2000][0]).Length;

					GlobalMembersMENUES.cmenu(GlobalMembersGLOBAL.current_menu-2000+360);

					KB_FlushKeyboardQueue();
					break;
				}
				if(KB_KeyPressed(sc_N) || KB_KeyPressed(sc_Escape) || buttonstat &2)
				{
					KB_ClearKeyDown(sc_N);
					KB_ClearKeyDown(sc_Escape);
					GlobalMembersMENUES.cmenu(351);
					GlobalMembersSOUNDS.sound(EXITMENUSOUND);
				}

				GlobalMembersMENUES.probe(186, 124, 0, 0);

				break;

			case 990:
			case 991:
			case 992:
			case 993:
			case 994:
			case 995:
			case 996:
			case 997:

	//            rotatesprite(c<<16,200<<15,65536L,0,MENUSCREEN,16,0,10+64,0,0,xdim-1,ydim-1);
	//            rotatesprite(c<<16,19<<16,65536L,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
	//            menutext(c,24,0,0,"CREDITS");

				if(KB_KeyPressed(sc_Escape))
				{
					GlobalMembersMENUES.cmenu(0);
					break;
				}

				if(KB_KeyPressed(sc_LeftArrow) || KB_KeyPressed(sc_kpad_4) || KB_KeyPressed(sc_UpArrow) || KB_KeyPressed(sc_PgUp) || KB_KeyPressed(sc_kpad_8))
				{
					KB_ClearKeyDown(sc_LeftArrow);
					KB_ClearKeyDown(sc_kpad_4);
					KB_ClearKeyDown(sc_UpArrow);
					KB_ClearKeyDown(sc_PgUp);
					KB_ClearKeyDown(sc_kpad_8);

					GlobalMembersSOUNDS.sound(KICK_HIT);
					GlobalMembersGLOBAL.current_menu--;
					if(GlobalMembersGLOBAL.current_menu < 990)
						GlobalMembersGLOBAL.current_menu = 992;
				}
				else if(KB_KeyPressed(sc_PgDn) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_RightArrow) || KB_KeyPressed(sc_DownArrow) || KB_KeyPressed(sc_kpad_2) || KB_KeyPressed(sc_kpad_9) || KB_KeyPressed(sc_kpad_6))
				{
					KB_ClearKeyDown(sc_PgDn);
					KB_ClearKeyDown(sc_Enter);
					KB_ClearKeyDown(sc_RightArrow);
					KB_ClearKeyDown(sc_kpad_Enter);
					KB_ClearKeyDown(sc_kpad_6);
					KB_ClearKeyDown(sc_kpad_9);
					KB_ClearKeyDown(sc_kpad_2);
					KB_ClearKeyDown(sc_DownArrow);
					KB_ClearKeyDown(sc_Space);
					GlobalMembersSOUNDS.sound(KICK_HIT);
					GlobalMembersGLOBAL.current_menu++;
					if(GlobalMembersGLOBAL.current_menu > 992)
						GlobalMembersGLOBAL.current_menu = 990;
				}

				switch(GlobalMembersGLOBAL.current_menu)
				{
					case 990:
					case 991:
					case 992:
					   rotatesprite(160<<16,200<<15,65536,0,2504+GlobalMembersGLOBAL.current_menu-990,0,0,10+64,0,0,xdim-1,ydim-1);

					   break;

				}
				break;

			case 0:
				c = (320>>1);
				rotatesprite(c<<16,28<<16,65536,0,INGAMEDUKETHREEDEE,0,0,10,0,0,xdim-1,ydim-1);
				rotatesprite((c+100)<<16, 36<<16, 65536, 0, PLUTOPAKSPRITE+2, (sintable[(totalclock<<4)&2047]>>11), 0, 2+8, 0, 0, xdim-1, ydim-1);
	// CTW - MODIFICATION
	//          x = probe(c,67,16,7);
				x = GlobalMembersMENUES.probe(c, 67, 16, 6);
	// CTW END - MODIFICATION
				if(x >= 0)
				{
					if(GlobalMembersGLOBAL.ud.multimode > 1 && x == 0 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						if(GlobalMembersGLOBAL.movesperpacket == 4 && myconnectindex != connecthead)
							break;

						last_zero = 0;
						GlobalMembersMENUES.cmenu(600);
					}
					else
					{
						last_zero = x;
						switch(x)
						{
							case 0:
								GlobalMembersMENUES.cmenu(100);
								break;
	// CTW - MODIFICATION
	// Shifted the entire menu input results up one.
	/*                      case 1:
	                            if(movesperpacket == 4 || numplayers > 1)
	                                break;
	
	                            tenBnSetExitRtn(dummyfunc);
	                            setDebugMsgRoutine(dummymess);
	                            tenerr = tenBnStart();
	
	                            switch(tenerr)
	                            {
	                                case eTenBnNotInWindows:
	                                    cmenu(20001);
	                                    break;
	                                case eTenBnBadGameIni:
	                                    cmenu(20002);
	                                    break;
	                                case eTenBnBadTenIni:
	                                    cmenu(20003);
	                                    break;
	                                case eTenBnBrowseCancel:
	                                    cmenu(20004);
	                                    break;
	                                case eTenBnBadTenInst:
	                                    cmenu(20005);
	                                    break;
	                                default:
	                                    playonten = 1;
	                                    gameexit(" ");
	                                    break;
	                            }
	                            break;*/
							case 1:
								GlobalMembersMENUES.cmenu(200);
								break;
							case 2:
								if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
									break;
								GlobalMembersMENUES.cmenu(300);
								break;
							case 3:
								KB_FlushKeyboardQueue();
								GlobalMembersMENUES.cmenu(400);
								break;
							case 4:
								GlobalMembersMENUES.cmenu(990);
								break;
							case 5:
								GlobalMembersMENUES.cmenu(500);
								break;
	// CTW END - MODIFICATION
						}
					}
				}

				if(KB_KeyPressed(sc_Q))
					GlobalMembersMENUES.cmenu(500);

				if(x == -1)
				{
					GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 1;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}
				}

				if(GlobalMembersGLOBAL.movesperpacket == 4)
				{
					if(myconnectindex == connecthead)
						GlobalMembersMENUES.menutext(c, 67, (X) 0(-2), (X) 0(-2), "NEW GAME");
					else
						GlobalMembersMENUES.menutext(c, 67, (X) 0(-2), 1, "NEW GAME");
				}
				else
					GlobalMembersMENUES.menutext(c, 67, (X) 0(-2), (X) 0(-2), "NEW GAME");

	// CTW - REMOVED
	/*          if(movesperpacket != 4 && numplayers < 2)
	                menutext(c,67+16,SHX(-3),PHX(-3),"PLAY ON TEN");
	            else
	                menutext(c,67+16,SHX(-3),1,"PLAY ON TEN");*/
	// CTW END - REMOVED

	// CTW - MODIFICATION - Not going to comment out the exact old code here.
	// I shifted up every menu option by 16.
	// I shifted up every menu result by 1.
				GlobalMembersMENUES.menutext(c, 67+16, (X) 0(-3), (X) 0(-3), "OPTIONS");

				if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
					GlobalMembersMENUES.menutext(c, 67+16+16, (X) 0(-4), 1, "LOAD GAME");
				else
					GlobalMembersMENUES.menutext(c, 67+16+16, (X) 0(-4), (X) 0(-4), "LOAD GAME");

	#if ! VOLUMEALL
				GlobalMembersMENUES.menutext(c, 67+16+16+16, (X) 0(-5), (X) 0(-5), "HOW TO ORDER");
	#else
				GlobalMembersMENUES.menutext(c, 67+16+16+16, (X) 0(-5), (X) 0(-5), "HELP");
	#endif
				GlobalMembersMENUES.menutext(c, 67+16+16+16+16, (X) 0(-6), (X) 0(-6), "CREDITS");

				GlobalMembersMENUES.menutext(c, 67+16+16+16+16+16, (X) 0(-7), (X) 0(-7), "QUIT");

				break;
	// CTW END - MODIFICATION

			case 50:
				c = (320>>1);
				rotatesprite(c<<16,32<<16,65536,0,INGAMEDUKETHREEDEE,0,0,10,0,0,xdim-1,ydim-1);
				rotatesprite((c+100)<<16, 36<<16, 65536, 0, PLUTOPAKSPRITE+2, (sintable[(totalclock<<4)&2047]>>11), 0, 2+8, 0, 0, xdim-1, ydim-1);
				x = GlobalMembersMENUES.probe(c, 67, 16, 7);
				switch(x)
				{
					case 0:
						if(GlobalMembersGLOBAL.movesperpacket == 4 && myconnectindex != connecthead)
							break;
						if(GlobalMembersGLOBAL.ud.multimode < 2 || GlobalMembersGLOBAL.ud.recstat == 2)
							GlobalMembersMENUES.cmenu(1500);
						else
						{
							GlobalMembersMENUES.cmenu(600);
							last_fifty = 0;
						}
						break;
					case 1:
						if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
							break;
						if(GlobalMembersGLOBAL.ud.recstat != 2)
						{
							last_fifty = 1;
							GlobalMembersMENUES.cmenu(350);
							setview(0,0,xdim-1,ydim-1);
						}
						break;
					case 2:
						if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
							break;
						last_fifty = 2;
						GlobalMembersMENUES.cmenu(300);
						break;
					case 3:
						last_fifty = 3;
						GlobalMembersMENUES.cmenu(200);
						break;
					case 4:
						last_fifty = 4;
						KB_FlushKeyboardQueue();
						GlobalMembersMENUES.cmenu(400);
						break;
					case 5:
						if(numplayers < 2)
						{
							last_fifty = 5;
							GlobalMembersMENUES.cmenu(501);
						}
						break;
					case 6:
						last_fifty = 6;
						GlobalMembersMENUES.cmenu(500);
						break;
					case -1:
						GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
						if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
						{
							GlobalMembersGLOBAL.ready2send = 1;
							totalclock = GlobalMembersGLOBAL.ototalclock;
						}
						break;
				}

				if(KB_KeyPressed(sc_Q))
					GlobalMembersMENUES.cmenu(500);

				if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
				{
					GlobalMembersMENUES.menutext(c, 67, (X) 0(-2), 1, "NEW GAME");
					GlobalMembersMENUES.menutext(c, 67+16, (X) 0(-3), 1, "SAVE GAME");
					GlobalMembersMENUES.menutext(c, 67+16+16, (X) 0(-4), 1, "LOAD GAME");
				}
				else
				{
					GlobalMembersMENUES.menutext(c, 67, (X) 0(-2), (X) 0(-2), "NEW GAME");
					GlobalMembersMENUES.menutext(c, 67+16, (X) 0(-3), (X) 0(-3), "SAVE GAME");
					GlobalMembersMENUES.menutext(c, 67+16+16, (X) 0(-4), (X) 0(-4), "LOAD GAME");
				}

				GlobalMembersMENUES.menutext(c, 67+16+16+16, (X) 0(-5), (X) 0(-5), "OPTIONS");
	#if ! VOLUMEALL
				GlobalMembersMENUES.menutext(c, 67+16+16+16+16, (X) 0(-6), (X) 0(-6), "HOW TO ORDER");
	#else
				GlobalMembersMENUES.menutext(c, 67+16+16+16+16, (X) 0(-6), (X) 0(-6), " HELP");
	#endif
				if(numplayers > 1)
					GlobalMembersMENUES.menutext(c, 67+16+16+16+16+16, (X) 0(-7), 1, "QUIT TO TITLE");
				else
					GlobalMembersMENUES.menutext(c, 67+16+16+16+16+16, (X) 0(-7), (X) 0(-7), "QUIT TO TITLE");
				GlobalMembersMENUES.menutext(c, 67+16+16+16+16+16+16, (X) 0(-8), (X) 0(-8), "QUIT GAME");
				break;

			case 100:
				rotatesprite(160<<16,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(160, 24, 0, 0, "SELECT AN EPISODE");
	#if PLUTOPAK
				if(GlobalMembersGAME.boardfilename[0])
					x = GlobalMembersMENUES.probe(160, 60, 20, 5);
				else
					x = GlobalMembersMENUES.probe(160, 60, 20, 4);
	#else
				if(GlobalMembersGAME.boardfilename[0])
					x = GlobalMembersMENUES.probe(160, 60, 20, 4);
				else
					x = GlobalMembersMENUES.probe(160, 60, 20, 3);
	#endif
				if(x >= 0)
				{
	#if VOLUMEONE
					if(x > 0)
						GlobalMembersMENUES.cmenu(20000);
					else
					{
						GlobalMembersGLOBAL.ud.m_volume_number = x;
						GlobalMembersGLOBAL.ud.m_level_number = 0;
						GlobalMembersMENUES.cmenu(110);
					}
	#endif

	#if ! VOLUMEONE
	#if ! PLUTOPAK

					if(x == 3 && GlobalMembersGAME.boardfilename[0])
					{
						GlobalMembersGLOBAL.ud.m_volume_number = 0;
						GlobalMembersGLOBAL.ud.m_level_number = 7;
					}
	#else
					if(x == 4 && GlobalMembersGAME.boardfilename[0])
					{
						GlobalMembersGLOBAL.ud.m_volume_number = 0;
						GlobalMembersGLOBAL.ud.m_level_number = 7;
					}
	#endif

					else
					{
						GlobalMembersGLOBAL.ud.m_volume_number = x;
						GlobalMembersGLOBAL.ud.m_level_number = 0;
					}
					GlobalMembersMENUES.cmenu(110);
	#endif
				}
				else if(x == -1)
				{
					if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
						GlobalMembersMENUES.cmenu(50);
					else
						GlobalMembersMENUES.cmenu(0);
				}

				GlobalMembersMENUES.menutext(160, 60, (X) 0(-2), (X) 0(-2), ref GlobalMembersGLOBAL.volume_names[0]);

				c = 80;
	#if VOLUMEONE
				GlobalMembersMENUES.menutext(160, 60+20, (X) 0(-3), 1, ref GlobalMembersGLOBAL.volume_names[1]);
				GlobalMembersMENUES.menutext(160, 60+20+20, (X) 0(-4), 1, ref GlobalMembersGLOBAL.volume_names[2]);
	#if PLUTOPAK
				GlobalMembersMENUES.menutext(160, 60+20+20, (X) 0(-5), 1, ref GlobalMembersGLOBAL.volume_names[3]);
	#endif
	#else
				GlobalMembersMENUES.menutext(160, 60+20, (X) 0(-3), (X) 0(-3), ref GlobalMembersGLOBAL.volume_names[1]);
				GlobalMembersMENUES.menutext(160, 60+20+20, (X) 0(-4), (X) 0(-4), ref GlobalMembersGLOBAL.volume_names[2]);
	#if PLUTOPAK
				GlobalMembersMENUES.menutext(160, 60+20+20+20, (X) 0(-5), (X) 0(-5), ref GlobalMembersGLOBAL.volume_names[3]);
				if(GlobalMembersGAME.boardfilename[0])
				{

					GlobalMembersMENUES.menutext(160, 60+20+20+20+20, (X) 0(-6), (X) 0(-6), "USER MAP");
					GlobalMembersGAME.gametextpal(160, 60+20+20+20+20+3, ref GlobalMembersGAME.boardfilename, 16+(sintable[(totalclock<<4)&2047]>>11), 2);
				}
	#else
				if(GlobalMembersGAME.boardfilename[0])
				{
					GlobalMembersMENUES.menutext(160, 60+20+20+20, (X) 0(-6), (X) 0(-6), "USER MAP");
					GlobalMembersGAME.gametext(160, 60+20+20+20+6, ref GlobalMembersGAME.boardfilename, 2, 2+8+16);
				}
	#endif

	#endif
				break;

			case 110:
				c = (320>>1);
				rotatesprite(c<<16,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(c, 24, 0, 0, "SELECT SKILL");
				x = GlobalMembersMENUES.probe(c, 70, 19, 4);
				if(x >= 0)
				{
					switch(x)
					{
						case 0:
							globalskillsound = JIBBED_ACTOR6;
							break;
						case 1:
							globalskillsound = BONUS_SPEECH1;
							break;
						case 2:
							globalskillsound = DUKE_GETWEAPON2;
							break;
						case 3:
							globalskillsound = JIBBED_ACTOR5;
							break;
					}

					GlobalMembersSOUNDS.sound(globalskillsound);

					GlobalMembersGLOBAL.ud.m_player_skill = x+1;
					if(x == 3)
						GlobalMembersGLOBAL.ud.m_respawn_monsters = 1;
					else
						GlobalMembersGLOBAL.ud.m_respawn_monsters = 0;

					GlobalMembersGLOBAL.ud.m_monsters_off = GlobalMembersGLOBAL.ud.monsters_off = 0;

					GlobalMembersGLOBAL.ud.m_respawn_items = 0;
					GlobalMembersGLOBAL.ud.m_respawn_inventory = 0;

					GlobalMembersGLOBAL.ud.multimode = 1;

					if(GlobalMembersGLOBAL.ud.m_volume_number == 3)
					{
						flushperms();
						setview(0,0,xdim-1,ydim-1);
						clearview(0);
						nextpage();
					}

					GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.m_volume_number, GlobalMembersGLOBAL.ud.m_level_number, GlobalMembersGLOBAL.ud.m_player_skill);
					GlobalMembersPREMAP.enterlevel(MODE_GAME);
				}
				else if(x == -1)
				{
					GlobalMembersMENUES.cmenu(100);
					KB_FlushKeyboardQueue();
				}

				GlobalMembersMENUES.menutext(c, 70, (X) 0(-2), (X) 0(-2), ref GlobalMembersGLOBAL.skill_names[0]);
				GlobalMembersMENUES.menutext(c, 70+19, (X) 0(-3), (X) 0(-3), ref GlobalMembersGLOBAL.skill_names[1]);
				GlobalMembersMENUES.menutext(c, 70+19+19, (X) 0(-4), (X) 0(-4), ref GlobalMembersGLOBAL.skill_names[2]);
				GlobalMembersMENUES.menutext(c, 70+19+19+19, (X) 0(-5), (X) 0(-5), ref GlobalMembersGLOBAL.skill_names[3]);
				break;

			case 200:

				rotatesprite(320<<15,10<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(320>>1, 15, 0, 0, "OPTIONS");

				c = (320>>1)-120;

				onbar = (probey == 3 || probey == 4 || probey == 5);
				x = GlobalMembersMENUES.probe(c+6, 31, 15, 10);

				if(x == -1)
					{
						if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
							GlobalMembersMENUES.cmenu(50);
							else
								GlobalMembersMENUES.cmenu(0);
					}

				if(onbar == 0)
					switch(x)
				{
					case 0:
						GlobalMembersGLOBAL.ud.detail = 1-GlobalMembersGLOBAL.ud.detail;
						break;
					case 1:
						GlobalMembersGLOBAL.ud.shadows = 1-GlobalMembersGLOBAL.ud.shadows;
						break;
					case 2:
						GlobalMembersGLOBAL.ud.screen_tilting = 1-GlobalMembersGLOBAL.ud.screen_tilting;
						break;
					case 6:
						if (GlobalMembersCONFIG.ControllerType == controltype_keyboardandmouse)
							GlobalMembersGLOBAL.ud.mouseflip = 1-GlobalMembersGLOBAL.ud.mouseflip;
						break;
					case 7:
						GlobalMembersMENUES.cmenu(700);
						break;
					case 8:
	#if ! AUSTRALIA
						GlobalMembersMENUES.cmenu(10000);
	#endif
						break;
					case 9:
						if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME))
						{
							GlobalMembersGAME.closedemowrite();
							break;
						}
						GlobalMembersGLOBAL.ud.m_recstat = !GlobalMembersGLOBAL.ud.m_recstat;
						break;
				}

				if(GlobalMembersGLOBAL.ud.detail)
					GlobalMembersMENUES.menutext(c+160+40, 31, 0, 0, "HIGH");
				else
					GlobalMembersMENUES.menutext(c+160+40, 31, 0, 0, "LOW");

				if(GlobalMembersGLOBAL.ud.shadows)
					GlobalMembersMENUES.menutext(c+160+40, 31+15, 0, 0, "ON");
				else
					GlobalMembersMENUES.menutext(c+160+40, 31+15, 0, 0, "OFF");

				switch(GlobalMembersGLOBAL.ud.screen_tilting)
				{
					case 0:
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15, 0, 0, "OFF");
						break;
					case 1:
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15, 0, 0, "ON");
						break;
					case 2:
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15, 0, 0, "FULL");
						break;
				}

				GlobalMembersMENUES.menutext(c, 31, (X) 0(-2), (X) 0(-2), "DETAIL");
				GlobalMembersMENUES.menutext(c, 31+15, (X) 0(-3), (X) 0(-3), "SHADOWS");
				GlobalMembersMENUES.menutext(c, 31+15+15, (X) 0(-4), (X) 0(-4), "SCREEN TILTING");
				GlobalMembersMENUES.menutext(c, 31+15+15+15, (X) 0(-5), (X) 0(-5), "SCREEN SIZE");

					GlobalMembersMENUES.bar(c+167+40, 31+15+15+15, ref (short)GlobalMembersGLOBAL.ud.screen_size, -4, x == 3, (X) 0(-5), (X) 0(-5));

				GlobalMembersMENUES.menutext(c, 31+15+15+15+15, (X) 0(-6), (X) 0(-6), "BRIGHTNESS");
					GlobalMembersMENUES.bar(c+167+40, 31+15+15+15+15, ref (short)GlobalMembersGLOBAL.ud.brightness, 8, x == 4, (X) 0(-6), (X) 0(-6));
					if(x == 4)
						setbrightness(GlobalMembersGLOBAL.ud.brightness>>2,GlobalMembersGLOBAL.ps[myconnectindex].palette[0]);

				if (GlobalMembersCONFIG.ControllerType == controltype_keyboardandmouse)
				{
					short sense;
					sense = CONTROL_GetMouseSensitivity()>>10;

					GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15, (X) 0(-7), (X) 0(-7), "MOUSE SENSITIVITY");
					GlobalMembersMENUES.bar(c+167+40, 31+15+15+15+15+15, ref sense, 4, x == 5, (X) 0(-7), (X) 0(-7));
					CONTROL_SetMouseSensitivity(sense<<10);
					GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15+15, (X) 0(-7), (X) 0(-7), "MOUSE AIMING FLIP");

					if(GlobalMembersGLOBAL.ud.mouseflip)
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15+15+15+15+15, (X) 0(-7), (X) 0(-7), "ON");
					else
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15+15+15+15+15, (X) 0(-7), (X) 0(-7), "OFF");

				}
				else
				{
					short sense;
					sense = CONTROL_GetMouseSensitivity()>>10;

					GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15, (X) 0(-7), 1, "MOUSE SENSITIVITY");
					GlobalMembersMENUES.bar(c+167+40, 31+15+15+15+15+15, ref sense, 4, x == 99, (X) 0(-7), 1);
					GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15+15, (X) 0(-7), 1, "MOUSE AIMING FLIP");

					if(GlobalMembersGLOBAL.ud.mouseflip)
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15+15+15+15+15, (X) 0(-7), 1, "ON");
					else
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15+15+15+15+15, (X) 0(-7), 1, "OFF");
				}

				GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15+15+15, (X) 0(-8), (X) 0(-8), "SOUNDS");
	#if ! AUSTRALIA
				GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15+15+15+15, (X) 0(-9), (X) 0(-9), "PARENTAL LOCK");
	#else
				GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15+15+15+15, (X) 0(-9), 1, "PARENTAL LOCK");
	#endif
				if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME) && GlobalMembersGLOBAL.ud.m_recstat != 1)
				{
					GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15+15+15+15+15, (X) 0(-10), 1, "RECORD");
					GlobalMembersMENUES.menutext(c+160+40, 31+15+15+15+15+15+15+15+15+15, (X) 0(-10), 1, "OFF");
				}
				else
				{
					GlobalMembersMENUES.menutext(c, 31+15+15+15+15+15+15+15+15+15, (X) 0(-10), (X) 0(-10), "RECORD");

					if(GlobalMembersGLOBAL.ud.m_recstat == 1)
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15+15+15+15+15+15+15+15, (X) 0(-10), (X) 0(-10), "ON");
					else
						GlobalMembersMENUES.menutext(c+160+40, 31+15+15+15+15+15+15+15+15+15, (X) 0(-10), (X) 0(-10), "OFF");
				}

				break;

			case 700:
				c = (320>>1)-120;
				rotatesprite(320<<15,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(320>>1, 24, 0, 0, "SOUNDS");
				onbar = (probey == 2 || probey == 3);

				x = GlobalMembersMENUES.probe(c, 50, 16, 7);
				switch(x)
				{
					case -1:
						if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
						{
							GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
							if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
							{
								GlobalMembersGLOBAL.ready2send = 1;
								totalclock = GlobalMembersGLOBAL.ototalclock;
							}
						}

						else
							GlobalMembersMENUES.cmenu(200);
						break;
					case 0:
						if (GlobalMembersCONFIG.FXDevice != NumSoundCards)
						{
							GlobalMembersCONFIG.SoundToggle = 1-GlobalMembersCONFIG.SoundToggle;
							if(GlobalMembersCONFIG.SoundToggle == 0)
							{
								FX_StopAllSounds();
								GlobalMembersSOUNDS.clearsoundlocks();
							}
							onbar = 0;
						}
						break;
					case 1:

						if(GlobalMembersGAME.eightytwofifty == 0 || numplayers < 2)
							if(GlobalMembersCONFIG.MusicDevice != NumSoundCards)
						{
							GlobalMembersCONFIG.MusicToggle = 1-GlobalMembersCONFIG.MusicToggle;
							if(GlobalMembersCONFIG.MusicToggle == 0)
								MUSIC_Pause();
							else
							{
								if(GlobalMembersGLOBAL.ud.recstat != 2 && GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
									GlobalMembersSOUNDS.playmusic(ref GlobalMembersGLOBAL.music_fn[0, GlobalMembersGLOBAL.music_select, 0]);
								else
									GlobalMembersSOUNDS.playmusic(ref GlobalMembersGLOBAL.env_music_fn[0, 0]);

								MUSIC_Continue();
							}
						}
						onbar = 0;

						break;
					case 4:
						if(GlobalMembersCONFIG.SoundToggle != 0 && (GlobalMembersCONFIG.FXDevice != NumSoundCards))
							GlobalMembersCONFIG.VoiceToggle = 1-GlobalMembersCONFIG.VoiceToggle;
						onbar = 0;
						break;
					case 5:
						if(GlobalMembersCONFIG.SoundToggle != 0 && (GlobalMembersCONFIG.FXDevice != NumSoundCards))
							GlobalMembersCONFIG.AmbienceToggle = 1-GlobalMembersCONFIG.AmbienceToggle;
						onbar = 0;
						break;
					case 6:
						if(GlobalMembersCONFIG.SoundToggle != 0 && (GlobalMembersCONFIG.FXDevice != NumSoundCards))
						{
							GlobalMembersCONFIG.ReverseStereo = 1-GlobalMembersCONFIG.ReverseStereo;
							FX_SetReverseStereo(GlobalMembersCONFIG.ReverseStereo);
						}
						onbar = 0;
						break;
					default:
						onbar = 1;
						break;
				}

				if(GlobalMembersCONFIG.SoundToggle != 0 && GlobalMembersCONFIG.FXDevice != NumSoundCards)
					GlobalMembersMENUES.menutext(c+160+40, 50, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards), "ON");
				else
					GlobalMembersMENUES.menutext(c+160+40, 50, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards), "OFF");

				if(GlobalMembersCONFIG.MusicToggle != 0 && (GlobalMembersCONFIG.MusicDevice != NumSoundCards) && (GlobalMembersGAME.eightytwofifty == 0||numplayers<2))
					GlobalMembersMENUES.menutext(c+160+40, 50+16, 0, (GlobalMembersCONFIG.MusicDevice == NumSoundCards), "ON");
				else
					GlobalMembersMENUES.menutext(c+160+40, 50+16, 0, (GlobalMembersCONFIG.MusicDevice == NumSoundCards), "OFF");

				GlobalMembersMENUES.menutext(c, 50, (X) 0(-2), (GlobalMembersCONFIG.FXDevice == NumSoundCards), "SOUND");
				GlobalMembersMENUES.menutext(c, 50+16+16, (X) 0(-4), (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "SOUND VOLUME");
				{
					l = GlobalMembersCONFIG.FXVolume;
					GlobalMembersCONFIG.FXVolume >>= 2;
					GlobalMembersMENUES.bar(c+167+40, 50+16+16, ref (short)GlobalMembersCONFIG.FXVolume, 4, (GlobalMembersCONFIG.FXDevice!=NumSoundCards)&&x == 2, (X) 0(-4), GlobalMembersCONFIG.SoundToggle == 0||(GlobalMembersCONFIG.FXDevice == NumSoundCards));
					if(l != GlobalMembersCONFIG.FXVolume)
						GlobalMembersCONFIG.FXVolume <<= 2;
					if(l != GlobalMembersCONFIG.FXVolume)
						FX_SetVolume((short) GlobalMembersCONFIG.FXVolume);
				}
				GlobalMembersMENUES.menutext(c, 50+16, (X) 0(-3), (GlobalMembersCONFIG.MusicDevice == NumSoundCards), "MUSIC");
				GlobalMembersMENUES.menutext(c, 50+16+16+16, (X) 0(-5), (GlobalMembersCONFIG.MusicDevice == NumSoundCards)||(numplayers > 1 && GlobalMembersGAME.eightytwofifty != 0)||GlobalMembersCONFIG.MusicToggle == 0, "MUSIC VOLUME");
				{
					l = GlobalMembersCONFIG.MusicVolume;
					GlobalMembersCONFIG.MusicVolume >>= 2;
					GlobalMembersMENUES.bar(c+167+40, 50+16+16+16, ref (short)GlobalMembersCONFIG.MusicVolume, 4, (GlobalMembersGAME.eightytwofifty == 0||numplayers < 2) && (GlobalMembersCONFIG.MusicDevice!=NumSoundCards) && x == 3, (X) 0(-5), (numplayers > 1 && GlobalMembersGAME.eightytwofifty != 0)||GlobalMembersCONFIG.MusicToggle == 0||(GlobalMembersCONFIG.MusicDevice == NumSoundCards));
					GlobalMembersCONFIG.MusicVolume <<= 2;
					if(l != GlobalMembersCONFIG.MusicVolume)
						Music_SetVolume((short) GlobalMembersCONFIG.MusicVolume);

				}
				GlobalMembersMENUES.menutext(c, 50+16+16+16+16, (X) 0(-6), (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "DUKE TALK");
				GlobalMembersMENUES.menutext(c, 50+16+16+16+16+16, (X) 0(-7), (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "AMBIENCE");

				GlobalMembersMENUES.menutext(c, 50+16+16+16+16+16+16, (X) 0(-8), (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "FLIP STEREO");

				if (GlobalMembersCONFIG.VoiceToggle != 0)
					GlobalMembersMENUES.menutext(c+160+40, 50+16+16+16+16, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "ON");
				else
					GlobalMembersMENUES.menutext(c+160+40, 50+16+16+16+16, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "OFF");

				if (GlobalMembersCONFIG.AmbienceToggle != 0)
					GlobalMembersMENUES.menutext(c+160+40, 50+16+16+16+16+16, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "ON");
				else
					GlobalMembersMENUES.menutext(c+160+40, 50+16+16+16+16+16, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "OFF");

				if (GlobalMembersCONFIG.ReverseStereo != 0)
					GlobalMembersMENUES.menutext(c+160+40, 50+16+16+16+16+16+16, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "ON");
				else
					GlobalMembersMENUES.menutext(c+160+40, 50+16+16+16+16+16+16, 0, (GlobalMembersCONFIG.FXDevice == NumSoundCards)||GlobalMembersCONFIG.SoundToggle == 0, "OFF");


				break;

			case 350:
				GlobalMembersMENUES.cmenu(351);
				GlobalMembersGAME.screencapt = 1;
				GlobalMembersGAME.displayrooms(myconnectindex, 65536);
				GlobalMembersMENUES.savetemp("duke3d.tmp", waloff[MAXTILES-1], 160 *100);
				GlobalMembersGAME.screencapt = 0;
				break;

			case 360:
			case 361:
			case 362:
			case 363:
			case 364:
			case 365:
			case 366:
			case 367:
			case 368:
			case 369:
			case 351:
			case 300:

				c = 320>>1;
				rotatesprite(c<<16,200<<15,65536,0,MENUSCREEN,16,0,10+64,0,0,xdim-1,ydim-1);
				rotatesprite(c<<16,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);

				if(GlobalMembersGLOBAL.current_menu == 300)
					GlobalMembersMENUES.menutext(c, 24, 0, 0, "LOAD GAME");
				else
					GlobalMembersMENUES.menutext(c, 24, 0, 0, "SAVE GAME");

				if(GlobalMembersGLOBAL.current_menu >= 360 && GlobalMembersGLOBAL.current_menu <= 369)
				{
					GlobalMembersGLOBAL.tempbuf = string.Format("PLAYERS: {0,-2:D}                      ", GlobalMembersGLOBAL.ud.multimode);
					GlobalMembersGAME.gametext(160, 158, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
					GlobalMembersGLOBAL.tempbuf = string.Format("EPISODE: {0,-2:D} / LEVEL: {1,-2:D} / SKILL: {2,-2:D}", 1+GlobalMembersGLOBAL.ud.volume_number, 1+GlobalMembersGLOBAL.ud.level_number, GlobalMembersGLOBAL.ud.player_skill);
					GlobalMembersGAME.gametext(160, 170, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

					x = GlobalMembersGAME.strget((320>>1), 184, ref GlobalMembersGLOBAL.ud.savegame[GlobalMembersGLOBAL.current_menu-360][0], 19, 999);

					if(x == -1)
					{
				//        readsavenames();
						GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
						if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
						{
							GlobalMembersGLOBAL.ready2send = 1;
							totalclock = GlobalMembersGLOBAL.ototalclock;
						}
						goto DISPLAYNAMES;
					}

					if(x == 1)
					{
						if(GlobalMembersGLOBAL.ud.savegame[GlobalMembersGLOBAL.current_menu-360][0] == 0)
						{
							KB_FlushKeyboardQueue();
							GlobalMembersMENUES.cmenu(351);
						}
						else
						{
							if(GlobalMembersGLOBAL.ud.multimode > 1)
								GlobalMembersMENUES.saveplayer(-1-(GlobalMembersGLOBAL.current_menu-360));
							else
								GlobalMembersMENUES.saveplayer(GlobalMembersGLOBAL.current_menu-360);
							lastsavedpos = GlobalMembersGLOBAL.current_menu-360;
							GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;

							if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
							{
								GlobalMembersGLOBAL.ready2send = 1;
								totalclock = GlobalMembersGLOBAL.ototalclock;
							}
							KB_ClearKeyDown(sc_Escape);
							GlobalMembersSOUNDS.sound(EXITMENUSOUND);
						}
					}

					rotatesprite(101<<16,97<<16,65536,512,MAXTILES-1,-32,0,2+4+8+64,0,0,xdim-1,ydim-1);
					GlobalMembersMENUES.dispnames();
					rotatesprite((c+67+Convert.ToString(GlobalMembersGLOBAL.ud.savegame[GlobalMembersGLOBAL.current_menu-360][0]).Length*4)<<16,(50+12 *probey)<<16,32768-10240,0,SPINNINGNUKEICON+(((totalclock)>>3)%7),0,0,10,0,0,xdim-1,ydim-1);
					break;
				}

			   last_threehundred = probey;

				x = GlobalMembersMENUES.probe(c+68, 54, 12, 10);

			  if(GlobalMembersGLOBAL.current_menu == 300)
			  {
				  if(GlobalMembersGLOBAL.ud.savegame[probey][0])
				  {
					  if(lastprobey != probey)
					  {
						 GlobalMembersMENUES.loadpheader(probey, ref volnum, ref levnum, ref plrskl, ref numplr);
						 lastprobey = probey;
					  }

					  rotatesprite(101<<16,97<<16,65536,512,MAXTILES-3,-32,0,4+10+64,0,0,xdim-1,ydim-1);
					  GlobalMembersGLOBAL.tempbuf = string.Format("PLAYERS: {0,-2:D}                      ", numplr);
					  GlobalMembersGAME.gametext(160, 158, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
					  GlobalMembersGLOBAL.tempbuf = string.Format("EPISODE: {0,-2:D} / LEVEL: {1,-2:D} / SKILL: {2,-2:D}", 1+volnum, 1+levnum, plrskl);
					  GlobalMembersGAME.gametext(160, 170, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
				  }
				  else
					  GlobalMembersMENUES.menutext(69, 70, 0, 0, "EMPTY");
			  }
			  else
			  {
				  if(GlobalMembersGLOBAL.ud.savegame[probey][0])
				  {
					  if(lastprobey != probey)
						  GlobalMembersMENUES.loadpheader(probey, ref volnum, ref levnum, ref plrskl, ref numplr);
					  lastprobey = probey;
					  rotatesprite(101<<16,97<<16,65536,512,MAXTILES-3,-32,0,4+10+64,0,0,xdim-1,ydim-1);
				  }
				  else
					  GlobalMembersMENUES.menutext(69, 70, 0, 0, "EMPTY");
				  GlobalMembersGLOBAL.tempbuf = string.Format("PLAYERS: {0,-2:D}                      ", GlobalMembersGLOBAL.ud.multimode);
				  GlobalMembersGAME.gametext(160, 158, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
				  GlobalMembersGLOBAL.tempbuf = string.Format("EPISODE: {0,-2:D} / LEVEL: {1,-2:D} / SKILL: {2,-2:D}", 1+GlobalMembersGLOBAL.ud.volume_number, 1+GlobalMembersGLOBAL.ud.level_number, GlobalMembersGLOBAL.ud.player_skill);
				  GlobalMembersGAME.gametext(160, 170, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
			  }

				switch(x)
				{
					case -1:
						if(GlobalMembersGLOBAL.current_menu == 300)
						{
							if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME) != MODE_GAME)
							{
								GlobalMembersMENUES.cmenu(0);
								break;
							}
							else
								GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
						}
						else
							GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;

						if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
						{
							GlobalMembersGLOBAL.ready2send = 1;
							totalclock = GlobalMembersGLOBAL.ototalclock;
						}

						break;
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
						if(GlobalMembersGLOBAL.current_menu == 300)
						{
							if(GlobalMembersGLOBAL.ud.savegame[x][0])
								GlobalMembersGLOBAL.current_menu = (1000+x);
						}
						else
						{
							if(GlobalMembersGLOBAL.ud.savegame[x][0] != 0)
								GlobalMembersGLOBAL.current_menu = 2000+x;
							else
							{
								KB_FlushKeyboardQueue();
								GlobalMembersGLOBAL.current_menu = (360+x);
								GlobalMembersGLOBAL.ud.savegame[x][0] = 0;
								GlobalMembersGAME.inputloc = 0;
							}
						}
						break;
				}

				DISPLAYNAMES:
				GlobalMembersMENUES.dispnames();
				break;

	#if ! VOLUMEALL
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case 400:
			case 401:
			case 402:
			case 403:

				c = 320>>1;

				if(KB_KeyPressed(sc_LeftArrow) || KB_KeyPressed(sc_kpad_4) || KB_KeyPressed(sc_UpArrow) || KB_KeyPressed(sc_PgUp) || KB_KeyPressed(sc_kpad_8))
				{
					KB_ClearKeyDown(sc_LeftArrow);
					KB_ClearKeyDown(sc_kpad_4);
					KB_ClearKeyDown(sc_UpArrow);
					KB_ClearKeyDown(sc_PgUp);
					KB_ClearKeyDown(sc_kpad_8);

					GlobalMembersSOUNDS.sound(KICK_HIT);
					GlobalMembersGLOBAL.current_menu--;
					if(GlobalMembersGLOBAL.current_menu < 400)
						GlobalMembersGLOBAL.current_menu = 403;
				}
				else if(KB_KeyPressed(sc_PgDn) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_RightArrow) || KB_KeyPressed(sc_DownArrow) || KB_KeyPressed(sc_kpad_2) || KB_KeyPressed(sc_kpad_9) || KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_kpad_6))
				{
					KB_ClearKeyDown(sc_PgDn);
					KB_ClearKeyDown(sc_Enter);
					KB_ClearKeyDown(sc_RightArrow);
					KB_ClearKeyDown(sc_kpad_Enter);
					KB_ClearKeyDown(sc_kpad_6);
					KB_ClearKeyDown(sc_kpad_9);
					KB_ClearKeyDown(sc_kpad_2);
					KB_ClearKeyDown(sc_DownArrow);
					KB_ClearKeyDown(sc_Space);
					GlobalMembersSOUNDS.sound(KICK_HIT);
					GlobalMembersGLOBAL.current_menu++;
					if(GlobalMembersGLOBAL.current_menu > 403)
						GlobalMembersGLOBAL.current_menu = 400;
				}

				if(KB_KeyPressed(sc_Escape))
				{
					if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
						GlobalMembersMENUES.cmenu(50);
					else
						GlobalMembersMENUES.cmenu(0);
					return;
				}

				flushperms();
				rotatesprite(0,0,65536,0,ORDERING+GlobalMembersGLOBAL.current_menu-400,0,0,10+16+64,0,0,xdim-1,ydim-1);

	#else
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case 400:
			case 401:

				c = 320>>1;

				if(KB_KeyPressed(sc_LeftArrow) || KB_KeyPressed(sc_kpad_4) || KB_KeyPressed(sc_UpArrow) || KB_KeyPressed(sc_PgUp) || KB_KeyPressed(sc_kpad_8))
				{
					KB_ClearKeyDown(sc_LeftArrow);
					KB_ClearKeyDown(sc_kpad_4);
					KB_ClearKeyDown(sc_UpArrow);
					KB_ClearKeyDown(sc_PgUp);
					KB_ClearKeyDown(sc_kpad_8);

					GlobalMembersSOUNDS.sound(KICK_HIT);
					GlobalMembersGLOBAL.current_menu--;
					if(GlobalMembersGLOBAL.current_menu < 400)
						GlobalMembersGLOBAL.current_menu = 401;
				}
				else if(KB_KeyPressed(sc_PgDn) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_RightArrow) || KB_KeyPressed(sc_DownArrow) || KB_KeyPressed(sc_kpad_2) || KB_KeyPressed(sc_kpad_9) || KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_kpad_6))
				{
					KB_ClearKeyDown(sc_PgDn);
					KB_ClearKeyDown(sc_Enter);
					KB_ClearKeyDown(sc_RightArrow);
					KB_ClearKeyDown(sc_kpad_Enter);
					KB_ClearKeyDown(sc_kpad_6);
					KB_ClearKeyDown(sc_kpad_9);
					KB_ClearKeyDown(sc_kpad_2);
					KB_ClearKeyDown(sc_DownArrow);
					KB_ClearKeyDown(sc_Space);
					GlobalMembersSOUNDS.sound(KICK_HIT);
					GlobalMembersGLOBAL.current_menu++;
					if(GlobalMembersGLOBAL.current_menu > 401)
						GlobalMembersGLOBAL.current_menu = 400;
				}

				if(KB_KeyPressed(sc_Escape))
				{
					if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
						GlobalMembersMENUES.cmenu(50);
					else
						GlobalMembersMENUES.cmenu(0);
					return;
				}

				flushperms();
				switch(GlobalMembersGLOBAL.current_menu)
				{
					case 400:
						rotatesprite(0,0,65536,0,TEXTSTORY,0,0,10+16+64, 0,0,xdim-1,ydim-1);
						break;
					case 401:
						rotatesprite(0,0,65536,0,F1HELP,0,0,10+16+64, 0,0,xdim-1,ydim-1);
						break;
				}

	#endif

				break;

			case 500:
				c = 320>>1;

				GlobalMembersGAME.gametext(c, 90, "Are you sure you want to quit?", 0, 2+8+16);
				GlobalMembersGAME.gametext(c, 99, "(Y/N)", 0, 2+8+16);

				if(KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_Y) || buttonstat &1)
				{
					KB_FlushKeyboardQueue();

					if(GlobalMembersGLOBAL.gamequit == 0 && (numplayers > 1))
					{
						if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
						{
							GlobalMembersGLOBAL.gamequit = 1;
							quittimer = totalclock+120;
						}
						else
						{
							sendlogoff();
							GlobalMembersGAME.gameexit(" ");
						}
					}
					else if(numplayers < 2)
						GlobalMembersGAME.gameexit(" ");

					if((totalclock > quittimer) && (GlobalMembersGLOBAL.gamequit == 1))
						GlobalMembersGAME.gameexit("Timed out.");
				}

				x = GlobalMembersMENUES.probe(186, 124, 0, 0);
				if(x == -1 || KB_KeyPressed(sc_N) || buttonstat &2)
				{
					KB_ClearKeyDown(sc_N);
					quittimer = 0;
					if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_DEMO)
						GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_DEMO;
					else
					{
						GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
						if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
						{
							GlobalMembersGLOBAL.ready2send = 1;
							totalclock = GlobalMembersGLOBAL.ototalclock;
						}
					}
				}

				break;
			case 501:
				c = 320>>1;
				GlobalMembersGAME.gametext(c, 90, "Quit to Title?", 0, 2+8+16);
				GlobalMembersGAME.gametext(c, 99, "(Y/N)", 0, 2+8+16);

				if(KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter) || KB_KeyPressed(sc_Y) || buttonstat &1)
				{
					KB_FlushKeyboardQueue();
					GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_DEMO;
					if(GlobalMembersGLOBAL.ud.recstat == 1)
						GlobalMembersGAME.closedemowrite();
					GlobalMembersMENUES.cmenu(0);
				}

				x = GlobalMembersMENUES.probe(186, 124, 0, 0);

				if(x == -1 || KB_KeyPressed(sc_N) || buttonstat &2)
				{
					GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 1;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}
				}

				break;

			case 601:
				GlobalMembersGAME.displayfragbar();
				rotatesprite(160<<16,29<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(320>>1, 34, 0, 0, ref GlobalMembersGLOBAL.ud.user_name[myconnectindex][0]);

				GlobalMembersGLOBAL.tempbuf = "Waiting for master";
				GlobalMembersGAME.gametext(160, 50, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
				GlobalMembersGAME.gametext(160, 59, "to select level", 0, 2+8+16);

				if(KB_KeyPressed(sc_Escape))
				{
					KB_ClearKeyDown(sc_Escape);
					GlobalMembersSOUNDS.sound(EXITMENUSOUND);
					GlobalMembersMENUES.cmenu(0);
				}
				break;

			case 602:
				if(menunamecnt == 0)
				{
			//        getfilenames("SUBD");
					GlobalMembersMENUES.getfilenames("*.MAP");
					GlobalMembersMENUES.sortfilenames();
					if (menunamecnt == 0)
						GlobalMembersMENUES.cmenu(600);
				}
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case 603:
				c = (320>>1) - 120;
				GlobalMembersGAME.displayfragbar();
				rotatesprite(320>>1<<16,19<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(320>>1, 24, 0, 0, "USER MAPS");
				for(x = 0;x<menunamecnt;x++)
				{
					if(x == fileselect)
						GlobalMembersGAME.minitext(15 + (x/15)*54, 32 + (x%15)*8, ref menuname[x], 0, 26);
					else
						GlobalMembersGAME.minitext(15 + (x/15)*54, 32 + (x%15)*8, ref menuname[x], 16, 26);
				}

				fileselect = probey;
				if(KB_KeyPressed(sc_LeftArrow) || KB_KeyPressed(sc_kpad_4) || ((buttonstat &1) && minfo.dyaw < -256))
				{
					KB_ClearKeyDown(sc_LeftArrow);
					KB_ClearKeyDown(sc_kpad_4);
					probey -= 15;
					if(probey < 0)
						probey += 15;
					else
						GlobalMembersSOUNDS.sound(KICK_HIT);
				}
				if(KB_KeyPressed(sc_RightArrow) || KB_KeyPressed(sc_kpad_6) || ((buttonstat &1) && minfo.dyaw > 256))
				{
					KB_ClearKeyDown(sc_RightArrow);
					KB_ClearKeyDown(sc_kpad_6);
					probey += 15;
					if(probey >= menunamecnt)
						probey -= 15;
					else
						GlobalMembersSOUNDS.sound(KICK_HIT);
				}

				onbar = 0;
				x = GlobalMembersMENUES.probe(0, 0, 0, menunamecnt);

				if(x == -1)
					GlobalMembersMENUES.cmenu(600);
				else if(x >= 0)
				{
					GlobalMembersGLOBAL.tempbuf[0] = 8;
					GlobalMembersGLOBAL.tempbuf[1] = GlobalMembersGLOBAL.ud.m_level_number = 6;
					GlobalMembersGLOBAL.tempbuf[2] = GlobalMembersGLOBAL.ud.m_volume_number = 0;
					GlobalMembersGLOBAL.tempbuf[3] = GlobalMembersGLOBAL.ud.m_player_skill+1;

					if(GlobalMembersGLOBAL.ud.player_skill == 3)
						GlobalMembersGLOBAL.ud.m_respawn_monsters = 1;
					else
						GlobalMembersGLOBAL.ud.m_respawn_monsters = 0;

					if(GlobalMembersGLOBAL.ud.m_coop == 0)
						GlobalMembersGLOBAL.ud.m_respawn_items = 1;
					else
						GlobalMembersGLOBAL.ud.m_respawn_items = 0;

					GlobalMembersGLOBAL.ud.m_respawn_inventory = 1;

					GlobalMembersGLOBAL.tempbuf[4] = GlobalMembersGLOBAL.ud.m_monsters_off;
					GlobalMembersGLOBAL.tempbuf[5] = GlobalMembersGLOBAL.ud.m_respawn_monsters;
					GlobalMembersGLOBAL.tempbuf[6] = GlobalMembersGLOBAL.ud.m_respawn_items;
					GlobalMembersGLOBAL.tempbuf[7] = GlobalMembersGLOBAL.ud.m_respawn_inventory;
					GlobalMembersGLOBAL.tempbuf[8] = GlobalMembersGLOBAL.ud.m_coop;
					GlobalMembersGLOBAL.tempbuf[9] = GlobalMembersGLOBAL.ud.m_marker;

					x = Convert.ToString(menuname[probey]).Length;

					copybufbyte(menuname[probey],GlobalMembersGLOBAL.tempbuf+10,x);
					copybufbyte(menuname[probey],GlobalMembersGAME.boardfilename,x+1);

					for(c = connecthead;c>=0;c = connectpoint2[c])
						if(c != myconnectindex)
							sendpacket(c,GlobalMembersGLOBAL.tempbuf,x+10);

					GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.m_volume_number, GlobalMembersGLOBAL.ud.m_level_number, GlobalMembersGLOBAL.ud.m_player_skill+1);
					GlobalMembersPREMAP.enterlevel(MODE_GAME);
				}
				break;

			case 600:
				c = (320>>1) - 120;
				if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME) != MODE_GAME)
					GlobalMembersGAME.displayfragbar();
				rotatesprite(160<<16,26<<16,65536,0,MENUBAR,16,0,10,0,0,xdim-1,ydim-1);
				GlobalMembersMENUES.menutext(160, 31, 0, 0, ref GlobalMembersGLOBAL.ud.user_name[myconnectindex][0]);

				x = GlobalMembersMENUES.probe(c, 57-8, 16, 8);

				switch(x)
				{
					case -1:
						GlobalMembersGLOBAL.ud.m_recstat = 0;
						if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
							GlobalMembersMENUES.cmenu(50);
						else
							GlobalMembersMENUES.cmenu(0);
						break;
					case 0:
						GlobalMembersGLOBAL.ud.m_coop++;
						if(GlobalMembersGLOBAL.ud.m_coop == 3)
							GlobalMembersGLOBAL.ud.m_coop = 0;
						break;
					case 1:
	#if ! VOLUMEONE
						GlobalMembersGLOBAL.ud.m_volume_number++;
	#if PLUTOPAK
						if(GlobalMembersGLOBAL.ud.m_volume_number > 3)
							GlobalMembersGLOBAL.ud.m_volume_number = 0;
	#else
						if(GlobalMembersGLOBAL.ud.m_volume_number > 2)
							GlobalMembersGLOBAL.ud.m_volume_number = 0;
	#endif
						if(GlobalMembersGLOBAL.ud.m_volume_number == 0 && GlobalMembersGLOBAL.ud.m_level_number > 6)
							GlobalMembersGLOBAL.ud.m_level_number = 0;
						if(GlobalMembersGLOBAL.ud.m_level_number > 10)
							GlobalMembersGLOBAL.ud.m_level_number = 0;
	#endif
						break;
					case 2:
	#if ! ONELEVELDEMO
						GlobalMembersGLOBAL.ud.m_level_number++;
	#if ! VOLUMEONE
						if(GlobalMembersGLOBAL.ud.m_volume_number == 0 && GlobalMembersGLOBAL.ud.m_level_number > 6)
							GlobalMembersGLOBAL.ud.m_level_number = 0;
	#else
						if(GlobalMembersGLOBAL.ud.m_volume_number == 0 && GlobalMembersGLOBAL.ud.m_level_number > 5)
							GlobalMembersGLOBAL.ud.m_level_number = 0;
	#endif
						if(GlobalMembersGLOBAL.ud.m_level_number > 10)
							GlobalMembersGLOBAL.ud.m_level_number = 0;
	#endif
						break;
					case 3:
						if(GlobalMembersGLOBAL.ud.m_monsters_off == 1 && GlobalMembersGLOBAL.ud.m_player_skill > 0)
							GlobalMembersGLOBAL.ud.m_monsters_off = 0;

						if(GlobalMembersGLOBAL.ud.m_monsters_off == 0)
						{
							GlobalMembersGLOBAL.ud.m_player_skill++;
							if(GlobalMembersGLOBAL.ud.m_player_skill > 3)
							{
								GlobalMembersGLOBAL.ud.m_player_skill = 0;
								GlobalMembersGLOBAL.ud.m_monsters_off = 1;
							}
						}
						else
							GlobalMembersGLOBAL.ud.m_monsters_off = 0;

						break;

					case 4:
						if(GlobalMembersGLOBAL.ud.m_coop == 0)
							GlobalMembersGLOBAL.ud.m_marker = !GlobalMembersGLOBAL.ud.m_marker;
						break;

					case 5:
						if(GlobalMembersGLOBAL.ud.m_coop == 1)
							GlobalMembersGLOBAL.ud.m_ffire = !GlobalMembersGLOBAL.ud.m_ffire;
						break;

					case 6:
	#if VOLUMEALL
						if(GlobalMembersGAME.boardfilename[0] == 0)
							break;

						GlobalMembersGLOBAL.tempbuf[0] = 5;
						GlobalMembersGLOBAL.tempbuf[1] = GlobalMembersGLOBAL.ud.m_level_number = 7;
						GlobalMembersGLOBAL.tempbuf[2] = GlobalMembersGLOBAL.ud.m_volume_number = 0;
						GlobalMembersGLOBAL.tempbuf[3] = GlobalMembersGLOBAL.ud.m_player_skill+1;

						GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.ud.m_level_number;
						GlobalMembersGLOBAL.ud.volume_number = GlobalMembersGLOBAL.ud.m_volume_number;

						if(GlobalMembersGLOBAL.ud.m_player_skill == 3)
							GlobalMembersGLOBAL.ud.m_respawn_monsters = 1;
						else
							GlobalMembersGLOBAL.ud.m_respawn_monsters = 0;

						if(GlobalMembersGLOBAL.ud.m_coop == 0)
							GlobalMembersGLOBAL.ud.m_respawn_items = 1;
						else
							GlobalMembersGLOBAL.ud.m_respawn_items = 0;

						GlobalMembersGLOBAL.ud.m_respawn_inventory = 1;

						GlobalMembersGLOBAL.tempbuf[4] = GlobalMembersGLOBAL.ud.m_monsters_off;
						GlobalMembersGLOBAL.tempbuf[5] = GlobalMembersGLOBAL.ud.m_respawn_monsters;
						GlobalMembersGLOBAL.tempbuf[6] = GlobalMembersGLOBAL.ud.m_respawn_items;
						GlobalMembersGLOBAL.tempbuf[7] = GlobalMembersGLOBAL.ud.m_respawn_inventory;
						GlobalMembersGLOBAL.tempbuf[8] = GlobalMembersGLOBAL.ud.m_coop;
						GlobalMembersGLOBAL.tempbuf[9] = GlobalMembersGLOBAL.ud.m_marker;
						GlobalMembersGLOBAL.tempbuf[10] = GlobalMembersGLOBAL.ud.m_ffire;

						for(c = connecthead;c>=0;c = connectpoint2[c])
						{
							GlobalMembersPREMAP.resetweapons(c);
							GlobalMembersPREMAP.resetinventory(c);

							if(c != myconnectindex)
								sendpacket(c,GlobalMembersGLOBAL.tempbuf,11);
						}

						GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.m_volume_number, GlobalMembersGLOBAL.ud.m_level_number, GlobalMembersGLOBAL.ud.m_player_skill+1);
						GlobalMembersPREMAP.enterlevel(MODE_GAME);

						return;
	#endif
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
					case 7:

						GlobalMembersGLOBAL.tempbuf[0] = 5;
						GlobalMembersGLOBAL.tempbuf[1] = GlobalMembersGLOBAL.ud.m_level_number;
						GlobalMembersGLOBAL.tempbuf[2] = GlobalMembersGLOBAL.ud.m_volume_number;
						GlobalMembersGLOBAL.tempbuf[3] = GlobalMembersGLOBAL.ud.m_player_skill+1;

						if(GlobalMembersGLOBAL.ud.m_player_skill == 3)
							GlobalMembersGLOBAL.ud.m_respawn_monsters = 1;
						else
							GlobalMembersGLOBAL.ud.m_respawn_monsters = 0;

						if(GlobalMembersGLOBAL.ud.m_coop == 0)
							GlobalMembersGLOBAL.ud.m_respawn_items = 1;
						else
							GlobalMembersGLOBAL.ud.m_respawn_items = 0;

						GlobalMembersGLOBAL.ud.m_respawn_inventory = 1;

						GlobalMembersGLOBAL.tempbuf[4] = GlobalMembersGLOBAL.ud.m_monsters_off;
						GlobalMembersGLOBAL.tempbuf[5] = GlobalMembersGLOBAL.ud.m_respawn_monsters;
						GlobalMembersGLOBAL.tempbuf[6] = GlobalMembersGLOBAL.ud.m_respawn_items;
						GlobalMembersGLOBAL.tempbuf[7] = GlobalMembersGLOBAL.ud.m_respawn_inventory;
						GlobalMembersGLOBAL.tempbuf[8] = GlobalMembersGLOBAL.ud.m_coop;
						GlobalMembersGLOBAL.tempbuf[9] = GlobalMembersGLOBAL.ud.m_marker;
						GlobalMembersGLOBAL.tempbuf[10] = GlobalMembersGLOBAL.ud.m_ffire;

						for(c = connecthead;c>=0;c = connectpoint2[c])
						{
							GlobalMembersPREMAP.resetweapons(c);
							GlobalMembersPREMAP.resetinventory(c);

							if(c != myconnectindex)
								sendpacket(c,GlobalMembersGLOBAL.tempbuf,11);
						}

						GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.m_volume_number, GlobalMembersGLOBAL.ud.m_level_number, GlobalMembersGLOBAL.ud.m_player_skill+1);
						GlobalMembersPREMAP.enterlevel(MODE_GAME);

						return;

				}

				c += 40;

				if(GlobalMembersGLOBAL.ud.m_coop==1)
					GlobalMembersGAME.gametext(c+70, 57-7-9, "COOPERATIVE PLAY", 0, 2+8+16);
				else if(GlobalMembersGLOBAL.ud.m_coop==2)
					GlobalMembersGAME.gametext(c+70, 57-7-9, "DUKEMATCH (NO SPAWN)", 0, 2+8+16);
				else
					GlobalMembersGAME.gametext(c+70, 57-7-9, "DUKEMATCH (SPAWN)", 0, 2+8+16);

	 #if VOLUMEONE
				GlobalMembersGAME.gametext(c+70, 57+16-7-9, ref GlobalMembersGLOBAL.volume_names[GlobalMembersGLOBAL.ud.m_volume_number], 0, 2+8+16);
	 #else
				GlobalMembersGAME.gametext(c+70, 57+16-7-9, ref GlobalMembersGLOBAL.volume_names[GlobalMembersGLOBAL.ud.m_volume_number], 0, 2+8+16);
	 #endif

				GlobalMembersGAME.gametext(c+70, 57+16+16-7-9, ref GlobalMembersGLOBAL.level_names[11 *GlobalMembersGLOBAL.ud.m_volume_number+GlobalMembersGLOBAL.ud.m_level_number, 0], 0, 2+8+16);

				if(GlobalMembersGLOBAL.ud.m_monsters_off == 0 || GlobalMembersGLOBAL.ud.m_player_skill > 0)
					GlobalMembersGAME.gametext(c+70, 57+16+16+16-7-9, ref GlobalMembersGLOBAL.skill_names[GlobalMembersGLOBAL.ud.m_player_skill], 0, 2+8+16);
				else
					GlobalMembersGAME.gametext(c+70, 57+16+16+16-7-9, "NONE", 0, 2+8+16);

				if(GlobalMembersGLOBAL.ud.m_coop == 0)
				{
					if(GlobalMembersGLOBAL.ud.m_marker)
						GlobalMembersGAME.gametext(c+70, 57+16+16+16+16-7-9, "ON", 0, 2+8+16);
					else
						GlobalMembersGAME.gametext(c+70, 57+16+16+16+16-7-9, "OFF", 0, 2+8+16);
				}

				if(GlobalMembersGLOBAL.ud.m_coop == 1)
				{
					if(GlobalMembersGLOBAL.ud.m_ffire)
						GlobalMembersGAME.gametext(c+70, 57+16+16+16+16+16-7-9, "ON", 0, 2+8+16);
					else
						GlobalMembersGAME.gametext(c+70, 57+16+16+16+16+16-7-9, "OFF", 0, 2+8+16);
				}

				c -= 44;

				GlobalMembersMENUES.menutext(c, 57-9, (X) 0(-2), (X) 0(-2), "GAME TYPE");

	#if VOLUMEONE
				GlobalMembersGLOBAL.tempbuf = string.Format("EPISODE {0:D}", GlobalMembersGLOBAL.ud.m_volume_number+1);
				GlobalMembersMENUES.menutext(c, 57+16-9, (X) 0(-3), 1, ref GlobalMembersGLOBAL.tempbuf);
	#else
				GlobalMembersGLOBAL.tempbuf = string.Format("EPISODE {0:D}", GlobalMembersGLOBAL.ud.m_volume_number+1);
				GlobalMembersMENUES.menutext(c, 57+16-9, (X) 0(-3), (X) 0(-3), ref GlobalMembersGLOBAL.tempbuf);
	#endif

	#if ! ONELEVELDEMO
				GlobalMembersGLOBAL.tempbuf = string.Format("LEVEL {0:D}", GlobalMembersGLOBAL.ud.m_level_number+1);
				GlobalMembersMENUES.menutext(c, 57+16+16-9, (X) 0(-4), (X) 0(-4), ref GlobalMembersGLOBAL.tempbuf);
	#else
				GlobalMembersGLOBAL.tempbuf = string.Format("LEVEL {0:D}", GlobalMembersGLOBAL.ud.m_level_number+1);
				GlobalMembersMENUES.menutext(c, 57+16+16-9, (X) 0(-4), 1, ref GlobalMembersGLOBAL.tempbuf);
	#endif
				GlobalMembersMENUES.menutext(c, 57+16+16+16-9, (X) 0(-5), (X) 0(-5), "MONSTERS");

				if(GlobalMembersGLOBAL.ud.m_coop == 0)
					GlobalMembersMENUES.menutext(c, 57+16+16+16+16-9, (X) 0(-6), (X) 0(-6), "MARKERS");
				else
					GlobalMembersMENUES.menutext(c, 57+16+16+16+16-9, (X) 0(-6), 1, "MARKERS");

				if(GlobalMembersGLOBAL.ud.m_coop == 1)
					GlobalMembersMENUES.menutext(c, 57+16+16+16+16+16-9, (X) 0(-6), (X) 0(-6), "FR. FIRE");
				else
					GlobalMembersMENUES.menutext(c, 57+16+16+16+16+16-9, (X) 0(-6), 1, "FR. FIRE");

	#if VOLUMEALL
				GlobalMembersMENUES.menutext(c, 57+16+16+16+16+16+16-9, (X) 0(-7), GlobalMembersGAME.boardfilename[0] == 0, "USER MAP");
				if(GlobalMembersGAME.boardfilename[0] != 0)
					GlobalMembersGAME.gametext(c+70+44, 57+16+16+16+16+16, ref GlobalMembersGAME.boardfilename, 0, 2+8+16);
	#else
				GlobalMembersMENUES.menutext(c, 57+16+16+16+16+16+16-9, (X) 0(-7), 1, "USER MAP");
	#endif

				GlobalMembersMENUES.menutext(c, 57+16+16+16+16+16+16+16-9, (X) 0(-8), (X) 0(-8), "START GAME");

				break;
		}

		if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) != MODE_MENU)
		{
			GlobalMembersPREMAP.vscrn();
			GlobalMembersGAME.cameraclock = totalclock;
			GlobalMembersGAME.cameradist = 65536;
		}
	}

	public static void palto(sbyte r,sbyte g,sbyte b,int e)
	{
		int i;
		string temparray = new string(new char[768]);

		for(i = 0;i<768;i+=3)
		{
			temparray = StringFunctions.ChangeCharacter(temparray, i, GlobalMembersGLOBAL.ps[myconnectindex].palette[i+0]+((((int)r-(int)GlobalMembersGLOBAL.ps[myconnectindex].palette[i+0])*(int)(e &127))>>6));
			temparray = StringFunctions.ChangeCharacter(temparray, i+1, GlobalMembersGLOBAL.ps[myconnectindex].palette[i+1]+((((int)g-(int)GlobalMembersGLOBAL.ps[myconnectindex].palette[i+1])*(int)(e &127))>>6));
			temparray = StringFunctions.ChangeCharacter(temparray, i+2, GlobalMembersGLOBAL.ps[myconnectindex].palette[i+2]+((((int)b-(int)GlobalMembersGLOBAL.ps[myconnectindex].palette[i+2])*(int)(e &127))>>6));
		}

	// CTW - MODIFICATION
	/*  if( (e&128) == 0 )
	        if ((vidoption != 1) || (vgacompatible == 1)) limitrate();*/
		if((e &128) == 0)
			if ((GlobalMembersCONFIG.ScreenMode != 1) || (vgacompatible == 1))
				limitrate();
	// CTW END - MODIFICATION

		setbrightness(GlobalMembersGLOBAL.ud.brightness>>2,temparray);
	}


	public static void drawoverheadmap(int cposx, int cposy, int czoom, short cang)
	{
			int i;
			int j;
			int k;
			int l;
			int x1;
			int y1;
			int x2;
			int y2;
			int x3;
			int y3;
			int x4;
			int y4;
			int ox;
			int oy;
			int xoff;
			int yoff;
			int dax;
			int day;
			int cosang;
			int sinang;
			int xspan;
			int yspan;
			int sprx;
			int spry;
			int xrepeat;
			int yrepeat;
			int z1;
			int z2;
			int startwall;
			int endwall;
			int tilenum;
			int daang;
			int xvect;
			int yvect;
			int xvect2;
			int yvect2;
			short p;
			sbyte col;
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
			walltype *wal = new walltype();
			walltype wal2;
			spritetype spr;

			xvect = sintable[(-cang)&2047] * czoom;
			yvect = sintable[(1536-cang)&2047] * czoom;
			xvect2 = mulscale16(xvect,yxaspect);
			yvect2 = mulscale16(yvect,yxaspect);

					//Draw red lines
			for(i = 0;i<numsectors;i++)
			{
					if (!(show2dsector[i>>3]&(1<<(i &7))))
						continue;

					startwall = sector[i].wallptr;
					endwall = sector[i].wallptr + sector[i].wallnum;

					z1 = sector[i].ceilingz;
					z2 = sector[i].floorz;

					for(j = startwall, wal = &wall[startwall];j<endwall;j++, wal++)
					{
							k = wal.nextwall;
							if (k < 0)
								continue;

							//if ((show2dwall[j>>3]&(1<<(j&7))) == 0) continue;
							//if ((k > j) && ((show2dwall[k>>3]&(1<<(k&7))) > 0)) continue;

							if (sector[wal.nextsector].ceilingz == z1)
									if (sector[wal.nextsector].floorz == z2)
											if (((wal.cstat|wall[wal.nextwall].cstat)&(16+32)) == 0)
												continue;

							col = 139; //red
							if ((wal.cstat|wall[wal.nextwall].cstat)&1) //magenta
								col = 234;

							if (!(show2dsector[wal.nextsector>>3]&(1<<(wal.nextsector &7))))
									col = 24;
				else
					continue;

							ox = wal.x-cposx;
							oy = wal.y-cposy;
							x1 = dmulscale16(ox,xvect,-oy,yvect)+(xdim<<11);
							y1 = dmulscale16(oy,xvect2,ox,yvect2)+(ydim<<11);

							wal2 = wall[wal.point2];
							ox = wal2.x-cposx;
							oy = wal2.y-cposy;
							x2 = dmulscale16(ox,xvect,-oy,yvect)+(xdim<<11);
							y2 = dmulscale16(oy,xvect2,ox,yvect2)+(ydim<<11);

							drawline256(x1,y1,x2,y2,col);
					}
			}

					//Draw sprites
			k = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].i;
			for(i = 0;i<numsectors;i++)
			{
					if (!(show2dsector[i>>3]&(1<<(i &7))))
						continue;
					for(j = headspritesect[i];j>=0;j = nextspritesect[j])
							//if ((show2dsprite[j>>3]&(1<<(j&7))) > 0)
							{
					spr = sprite[j];

					if (j == k || (spr.cstat &0x8000) || spr.cstat == 257 || spr.xrepeat == 0)
						continue;

									col = 71; //cyan
									if (spr.cstat &1) //magenta
										col = 234;

									sprx = spr.x;
									spry = spr.y;

					if((spr.cstat &257) != 0)
						switch (spr.cstat &48)
									{
						case 0:
							break;

													ox = sprx-cposx;
													oy = spry-cposy;
													x1 = dmulscale16(ox,xvect,-oy,yvect);
													y1 = dmulscale16(oy,xvect2,ox,yvect2);

													ox = (sintable[(spr.ang+512)&2047]>>7);
													oy = (sintable[(spr.ang)&2047]>>7);
													x2 = dmulscale16(ox,xvect,-oy,yvect);
													y2 = dmulscale16(oy,xvect,ox,yvect);

													x3 = mulscale16(x2,yxaspect);
													y3 = mulscale16(y2,yxaspect);

													drawline256(x1-x2+(xdim<<11),y1-y3+(ydim<<11), x1+x2+(xdim<<11),y1+y3+(ydim<<11),col);
													drawline256(x1-y2+(xdim<<11),y1+x3+(ydim<<11), x1+x2+(xdim<<11),y1+y3+(ydim<<11),col);
													drawline256(x1+y2+(xdim<<11),y1-x3+(ydim<<11), x1+x2+(xdim<<11),y1+y3+(ydim<<11),col);
							break;

											case 16:
							if(spr.picnum == LASERLINE)
							{
								x1 = sprx;
								y1 = spry;
								tilenum = spr.picnum;
								xoff = (int)((sbyte)((picanm[tilenum]>>8)&255))+((int)spr.xoffset);
								if ((spr.cstat &4) > 0)
									xoff = -xoff;
								k = spr.ang;
								l = spr.xrepeat;
								dax = sintable[k &2047]*l;
								day = sintable[(k+1536)&2047]*l;
								l = tilesizx[tilenum];
								k = (l>>1)+xoff;
								x1 -= mulscale16(dax,k);
								x2 = x1+mulscale16(dax,l);
								y1 -= mulscale16(day,k);
								y2 = y1+mulscale16(day,l);

								ox = x1-cposx;
								oy = y1-cposy;
								x1 = dmulscale16(ox,xvect,-oy,yvect);
								y1 = dmulscale16(oy,xvect2,ox,yvect2);

								ox = x2-cposx;
								oy = y2-cposy;
								x2 = dmulscale16(ox,xvect,-oy,yvect);
								y2 = dmulscale16(oy,xvect2,ox,yvect2);

								drawline256(x1+(xdim<<11),y1+(ydim<<11), x2+(xdim<<11),y2+(ydim<<11),col);
							}

							break;

						case 32:

													tilenum = spr.picnum;
													xoff = (int)((sbyte)((picanm[tilenum]>>8)&255))+((int)spr.xoffset);
													yoff = (int)((sbyte)((picanm[tilenum]>>16)&255))+((int)spr.yoffset);
													if ((spr.cstat &4) > 0)
														xoff = -xoff;
													if ((spr.cstat &8) > 0)
														yoff = -yoff;

													k = spr.ang;
													cosang = sintable[(k+512)&2047];
													sinang = sintable[k];
													xspan = tilesizx[tilenum];
													xrepeat = spr.xrepeat;
													yspan = tilesizy[tilenum];
													yrepeat = spr.yrepeat;

													dax = ((xspan>>1)+xoff)*xrepeat;
													day = ((yspan>>1)+yoff)*yrepeat;
													x1 = sprx + dmulscale16(sinang,dax,cosang,day);
													y1 = spry + dmulscale16(sinang,day,-cosang,dax);
													l = xspan *xrepeat;
													x2 = x1 - mulscale16(sinang,l);
													y2 = y1 + mulscale16(cosang,l);
													l = yspan *yrepeat;
													k = -mulscale16(cosang,l);
													x3 = x2+k;
													x4 = x1+k;
													k = -mulscale16(sinang,l);
													y3 = y2+k;
													y4 = y1+k;

													ox = x1-cposx;
													oy = y1-cposy;
													x1 = dmulscale16(ox,xvect,-oy,yvect);
													y1 = dmulscale16(oy,xvect2,ox,yvect2);

													ox = x2-cposx;
													oy = y2-cposy;
													x2 = dmulscale16(ox,xvect,-oy,yvect);
													y2 = dmulscale16(oy,xvect2,ox,yvect2);

													ox = x3-cposx;
													oy = y3-cposy;
													x3 = dmulscale16(ox,xvect,-oy,yvect);
													y3 = dmulscale16(oy,xvect2,ox,yvect2);

													ox = x4-cposx;
													oy = y4-cposy;
													x4 = dmulscale16(ox,xvect,-oy,yvect);
													y4 = dmulscale16(oy,xvect2,ox,yvect2);

													drawline256(x1+(xdim<<11),y1+(ydim<<11), x2+(xdim<<11),y2+(ydim<<11),col);

													drawline256(x2+(xdim<<11),y2+(ydim<<11), x3+(xdim<<11),y3+(ydim<<11),col);

													drawline256(x3+(xdim<<11),y3+(ydim<<11), x4+(xdim<<11),y4+(ydim<<11),col);

													drawline256(x4+(xdim<<11),y4+(ydim<<11), x1+(xdim<<11),y1+(ydim<<11),col);

													break;
									}
							}
			}

					//Draw white lines
			for(i = 0;i<numsectors;i++)
			{
					if (!(show2dsector[i>>3]&(1<<(i &7))))
						continue;

					startwall = sector[i].wallptr;
					endwall = sector[i].wallptr + sector[i].wallnum;

					k = -1;
					for(j = startwall, wal = &wall[startwall];j<endwall;j++, wal++)
					{
							if (wal.nextwall >= 0)
								continue;

							//if ((show2dwall[j>>3]&(1<<(j&7))) == 0) continue;

							if (tilesizx[wal.picnum] == 0)
								continue;
							if (tilesizy[wal.picnum] == 0)
								continue;

							if (j == k)
									{
										x1 = x2;
										y1 = y2;
									}
							else
							{
									ox = wal.x-cposx;
									oy = wal.y-cposy;
									x1 = dmulscale16(ox,xvect,-oy,yvect)+(xdim<<11);
									y1 = dmulscale16(oy,xvect2,ox,yvect2)+(ydim<<11);
							}

							k = wal.point2;
							wal2 = wall[k];
							ox = wal2.x-cposx;
							oy = wal2.y-cposy;
							x2 = dmulscale16(ox,xvect,-oy,yvect)+(xdim<<11);
							y2 = dmulscale16(oy,xvect2,ox,yvect2)+(ydim<<11);

							drawline256(x1,y1,x2,y2,24);
					}
			}

			 for(p = connecthead;p >= 0;p = connectpoint2[p])
			 {
			  if(GlobalMembersGLOBAL.ud.scrollmode && p == GlobalMembersGLOBAL.screenpeek)
				  continue;

			  ox = sprite[GlobalMembersGLOBAL.ps[p].i].x-cposx;
			  oy = sprite[GlobalMembersGLOBAL.ps[p].i].y-cposy;
					  daang = (sprite[GlobalMembersGLOBAL.ps[p].i].ang-cang)&2047;
					  if (p == GlobalMembersGLOBAL.screenpeek)
					  {
						  ox = 0;
						  oy = 0;
						  daang = 0;
					  }
					  x1 = mulscale(ox,xvect,16) - mulscale(oy,yvect,16);
					  y1 = mulscale(oy,xvect2,16) + mulscale(ox,yvect2,16);

			  if(p == GlobalMembersGLOBAL.screenpeek || GlobalMembersGLOBAL.ud.coop == 1)
			  {
					if(sprite[GlobalMembersGLOBAL.ps[p].i].xvel > 16 && GlobalMembersGLOBAL.ps[p].on_ground)
						i = APLAYERTOP+((totalclock>>4)&3);
					else
						i = APLAYERTOP;

					j = klabs(GlobalMembersGLOBAL.ps[p].truefz-GlobalMembersGLOBAL.ps[p].posz)>>8;
					j = mulscale(czoom*(sprite[GlobalMembersGLOBAL.ps[p].i].yrepeat+j),yxaspect,16);

					if(j < 22000)
						j = 22000;
					else if(j > (65536<<1))
						j = (65536<<1);

					rotatesprite((x1<<4)+(xdim<<15), (y1<<4)+(ydim<<15), j, daang, i, sprite[GlobalMembersGLOBAL.ps[p].i].shade, sprite[GlobalMembersGLOBAL.ps[p].i].pal, (sprite[GlobalMembersGLOBAL.ps[p].i].cstat &2)>>1, windowx1, windowy1, windowx2, windowy2);
			  }
			 }
	}



	public static void endanimsounds(int fr)
	{
		switch(GlobalMembersGLOBAL.ud.volume_number)
		{
			case 0:
				break;
			case 1:
				switch(fr)
				{
					case 1:
						GlobalMembersSOUNDS.sound(WIND_AMBIENCE);
						break;
					case 26:
						GlobalMembersSOUNDS.sound(ENDSEQVOL2SND1);
						break;
					case 36:
						GlobalMembersSOUNDS.sound(ENDSEQVOL2SND2);
						break;
					case 54:
						GlobalMembersSOUNDS.sound(THUD);
						break;
					case 62:
						GlobalMembersSOUNDS.sound(ENDSEQVOL2SND3);
						break;
					case 75:
						GlobalMembersSOUNDS.sound(ENDSEQVOL2SND4);
						break;
					case 81:
						GlobalMembersSOUNDS.sound(ENDSEQVOL2SND5);
						break;
					case 115:
						GlobalMembersSOUNDS.sound(ENDSEQVOL2SND6);
						break;
					case 124:
						GlobalMembersSOUNDS.sound(ENDSEQVOL2SND7);
						break;
				}
				break;
			case 2:
				switch(fr)
				{
					case 1:
						GlobalMembersSOUNDS.sound(WIND_REPEAT);
						break;
					case 98:
						GlobalMembersSOUNDS.sound(DUKE_GRUNT);
						break;
					case 82+20:
						GlobalMembersSOUNDS.sound(THUD);
						GlobalMembersSOUNDS.sound(SQUISHED);
						break;
					case 104+20:
						GlobalMembersSOUNDS.sound(ENDSEQVOL3SND3);
						break;
					case 114+20:
						GlobalMembersSOUNDS.sound(ENDSEQVOL3SND2);
						break;
					case 158:
						GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
						break;
				}
				break;
		}
	}

	public static void logoanimsounds(int fr)
	{
		switch(fr)
		{
			case 1:
				GlobalMembersSOUNDS.sound(FLY_BY);
				break;
			case 19:
				GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
				break;
		}
	}

	public static void intro4animsounds(int fr)
	{
		switch(fr)
		{
			case 1:
				GlobalMembersSOUNDS.sound(INTRO4_B);
				break;
			case 12:
			case 34:
				GlobalMembersSOUNDS.sound(SHORT_CIRCUIT);
				break;
			case 18:
				GlobalMembersSOUNDS.sound(INTRO4_5);
				break;
		}
	}

	public static void first4animsounds(int fr)
	{
		switch(fr)
		{
			case 1:
				GlobalMembersSOUNDS.sound(INTRO4_1);
				break;
			case 12:
				GlobalMembersSOUNDS.sound(INTRO4_2);
				break;
			case 7:
				GlobalMembersSOUNDS.sound(INTRO4_3);
				break;
			case 26:
				GlobalMembersSOUNDS.sound(INTRO4_4);
				break;
		}
	}

	public static void intro42animsounds(int fr)
	{
		switch(fr)
		{
			case 10:
				GlobalMembersSOUNDS.sound(INTRO4_6);
				break;
		}
	}




	public static void endanimvol41(int fr)
	{
		switch(fr)
		{
			case 3:
				GlobalMembersSOUNDS.sound(DUKE_UNDERWATER);
				break;
			case 35:
				GlobalMembersSOUNDS.sound(VOL4ENDSND1);
				break;
		}
	}

	public static void endanimvol42(int fr)
	{
		switch(fr)
		{
			case 11:
				GlobalMembersSOUNDS.sound(DUKE_UNDERWATER);
				break;
			case 20:
				GlobalMembersSOUNDS.sound(VOL4ENDSND1);
				break;
			case 39:
				GlobalMembersSOUNDS.sound(VOL4ENDSND2);
				break;
			case 50:
				FX_StopAllSounds();
				break;
		}
	}

	public static void endanimvol43(int fr)
	{
		switch(fr)
		{
			case 1:
				GlobalMembersSOUNDS.sound(BOSS4_DEADSPEECH);
				break;
			case 40:
				GlobalMembersSOUNDS.sound(VOL4ENDSND1);
				GlobalMembersSOUNDS.sound(DUKE_UNDERWATER);
				break;
			case 50:
				GlobalMembersSOUNDS.sound(BIGBANG);
				break;
		}
	}


	public static int lastanimhack=0;
	public static void playanm(ref string fn, sbyte t)
	{
			string animbuf;
			string palptr;
		int i;
		int j;
		int k;
		int length = 0;
		int numframes = 0;
		int32 handle = -1;

	//    return;

		if(t != 7 && t != 9 && t != 10 && t != 11)
			KB_FlushKeyboardQueue();

		if(KB_KeyWaiting())
		{
			FX_StopAllSounds();
			goto ENDOFANIMLOOP;
		}

			handle = kopen4load(fn,0);
			if(handle == -1)
				return;
			length = kfilelength(handle);

		walock[MAXTILES-3-t] = 219+t;

		if(GlobalMembersANIMLIB.anim == 0 || lastanimhack != (MAXTILES-3-t))
			allocache((int)GlobalMembersANIMLIB.anim, length+sizeof(anim_t), walock[MAXTILES-3-t]);

		animbuf = (string)(FP_OFF(GlobalMembersANIMLIB.anim)+sizeof(anim_t));

		lastanimhack = (MAXTILES-3-t);

		tilesizx[MAXTILES-3-t] = 200;
		tilesizy[MAXTILES-3-t] = 320;

			kread(handle,animbuf,length);
			kclose(handle);

			GlobalMembersANIMLIB.ANIM_LoadAnim(ref animbuf);
			numframes = GlobalMembersANIMLIB.ANIM_NumFrames();

			palptr = GlobalMembersANIMLIB.ANIM_GetPalette();
			for(i = 0;i<256;i++)
			{
					j = (i<<2);
					k = j-i;
					GlobalMembersGLOBAL.tempbuf[j+0] = (palptr[k+2]>>2);
					GlobalMembersGLOBAL.tempbuf[j+1] = (palptr[k+1]>>2);
					GlobalMembersGLOBAL.tempbuf[j+2] = (palptr[k+0]>>2);
					GlobalMembersGLOBAL.tempbuf[j+3] = 0;
			}

			VBE_setPalette(0,256,GlobalMembersGLOBAL.tempbuf);

		GlobalMembersGLOBAL.ototalclock = totalclock + 10;

			for(i = 1;i<numframes;i++)
			{
		   while(totalclock < GlobalMembersGLOBAL.ototalclock)
		   {
			  if(KB_KeyWaiting())
				  goto ENDOFANIMLOOP;
			  GlobalMembersGAME.getpackets();
		   }

		   if(t == 10)
			   GlobalMembersGLOBAL.ototalclock += 14;
		   else if(t == 9)
			   GlobalMembersGLOBAL.ototalclock += 10;
		   else if(t == 7)
			   GlobalMembersGLOBAL.ototalclock += 18;
		   else if(t == 6)
			   GlobalMembersGLOBAL.ototalclock += 14;
		   else if(t == 5)
			   GlobalMembersGLOBAL.ototalclock += 9;
		   else if(GlobalMembersGLOBAL.ud.volume_number == 3)
			   GlobalMembersGLOBAL.ototalclock += 10;
		   else if(GlobalMembersGLOBAL.ud.volume_number == 2)
			   GlobalMembersGLOBAL.ototalclock += 10;
		   else if(GlobalMembersGLOBAL.ud.volume_number == 1)
			   GlobalMembersGLOBAL.ototalclock += 18;
		   else
			   GlobalMembersGLOBAL.ototalclock += 10;

		   waloff[MAXTILES-3-t] = FP_OFF(GlobalMembersANIMLIB.ANIM_DrawFrame(i));
		   rotatesprite(0<<16,0<<16,65536,512,MAXTILES-3-t,0,0,2+4+8+16+64, 0,0,xdim-1,ydim-1);
		   nextpage();

		   if(t == 8)
			   GlobalMembersMENUES.endanimvol41(i);
		   else if(t == 10)
			   GlobalMembersMENUES.endanimvol42(i);
		   else if(t == 11)
			   GlobalMembersMENUES.endanimvol43(i);
		   else if(t == 9)
			   GlobalMembersMENUES.intro42animsounds(i);
		   else if(t == 7)
			   GlobalMembersMENUES.intro4animsounds(i);
		   else if(t == 6)
			   GlobalMembersMENUES.first4animsounds(i);
		   else if(t == 5)
			   GlobalMembersMENUES.logoanimsounds(i);
		   else if(t < 4)
			   GlobalMembersMENUES.endanimsounds(i);
			}

		ENDOFANIMLOOP:

		GlobalMembersANIMLIB.ANIM_FreeAnim();
		walock[MAXTILES-3-t] = 1;
	}
}

