using System;

public static class GlobalMembersPREMAP
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


	//extern sbyte everyothertime;
	public static short which_palookup = 9;


	public static tloadtile(short tilenume)
	{
		gotpic[tilenume>>3] |= (1<<(tilenume &7));
	}

	public static void cachespritenum(short i)
	{
		sbyte maxc;
		short j;

		if(GlobalMembersGLOBAL.ud.monsters_off && GlobalMembersGAME.badguy(ref sprite[i]) != 0)
			return;

		maxc = 1;

		switch(PN)
		{
			case HYDRENT:
				GlobalMembersPREMAP.tloadtile(BROKEFIREHYDRENT);
				for(j = TOILETWATER; j < (TOILETWATER+4); j++)
					if(waloff[j] == 0)
						GlobalMembersPREMAP.tloadtile(j);
				break;
			case TOILET:
				GlobalMembersPREMAP.tloadtile(TOILETBROKE);
				for(j = TOILETWATER; j < (TOILETWATER+4); j++)
					if(waloff[j] == 0)
						GlobalMembersPREMAP.tloadtile(j);
				break;
			case STALL:
				GlobalMembersPREMAP.tloadtile(STALLBROKE);
				for(j = TOILETWATER; j < (TOILETWATER+4); j++)
					if(waloff[j] == 0)
						GlobalMembersPREMAP.tloadtile(j);
				break;
			case RUBBERCAN:
				maxc = 2;
				break;
			case TOILETWATER:
				maxc = 4;
				break;
			case FEMPIC1:
				maxc = 44;
				break;
			case LIZTROOP:
			case LIZTROOPRUNNING:
			case LIZTROOPSHOOT:
			case LIZTROOPJETPACK:
			case LIZTROOPONTOILET:
			case LIZTROOPDUCKING:
				for(j = LIZTROOP; j < (LIZTROOP+72); j++)
					if(waloff[j] == 0)
						GlobalMembersPREMAP.tloadtile(j);
				for(j = HEADJIB1;j<LEGJIB1+3;j++)
					if(waloff[j] == 0)
						GlobalMembersPREMAP.tloadtile(j);
				maxc = 0;
				break;
			case WOODENHORSE:
				maxc = 5;
				for(j = HORSEONSIDE; j < (HORSEONSIDE+4); j++)
					if(waloff[j] == 0)
						GlobalMembersPREMAP.tloadtile(j);
				break;
			case NEWBEAST:
			case NEWBEASTSTAYPUT:
				maxc = 90;
				break;
			case BOSS1:
			case BOSS2:
			case BOSS3:
				maxc = 30;
				break;
			case OCTABRAIN:
			case OCTABRAINSTAYPUT:
			case COMMANDER:
			case COMMANDERSTAYPUT:
				maxc = 38;
				break;
			case RECON:
				maxc = 13;
				break;
			case PIGCOP:
			case PIGCOPDIVE:
				maxc = 61;
				break;
			case SHARK:
				maxc = 30;
				break;
			case LIZMAN:
			case LIZMANSPITTING:
			case LIZMANFEEDING:
			case LIZMANJUMP:
				for(j = LIZMANHEAD1;j<LIZMANLEG1+3;j++)
					if(waloff[j] == 0)
						GlobalMembersPREMAP.tloadtile(j);
				maxc = 80;
				break;
			case APLAYER:
				maxc = 0;
				if(GlobalMembersGLOBAL.ud.multimode > 1)
				{
					maxc = 5;
					for(j = 1420;j < 1420+106; j++)
						if(waloff[j] == -1)
							GlobalMembersPREMAP.tloadtile(j);
				}
				break;
			case ATOMICHEALTH:
				maxc = 14;
				break;
			case DRONE:
				maxc = 10;
				break;
			case EXPLODINGBARREL:
			case SEENINE:
			case OOZFILTER:
				maxc = 3;
				break;
			case NUKEBARREL:
			case CAMERA1:
				maxc = 5;
				break;
		}

		for(j = PN; j < (PN+maxc); j++)
			if(waloff[j] == 0)
				GlobalMembersPREMAP.tloadtile(j);
	}

	public static void cachegoodsprites()
	{
		short i;

		if(GlobalMembersGLOBAL.ud.screen_size >= 8)
		{
			if(waloff[BOTTOMSTATUSBAR] == 0)
				GlobalMembersPREMAP.tloadtile(BOTTOMSTATUSBAR);
			if(GlobalMembersGLOBAL.ud.multimode > 1)
			{
				if(waloff[FRAGBAR] == 0)
					GlobalMembersPREMAP.tloadtile(FRAGBAR);
				for(i = MINIFONT;i<MINIFONT+63;i++)
					if(waloff[i] == 0)
						GlobalMembersPREMAP.tloadtile(i);
			}
		}

		GlobalMembersPREMAP.tloadtile(VIEWSCREEN);

		for(i = STARTALPHANUM;i<ENDALPHANUM+1;i++)
			if (waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = FOOTPRINTS;i<FOOTPRINTS+3;i++)
			if (waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = BIGALPHANUM; i < BIGALPHANUM+82; i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = BURNING; i < BURNING+14; i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = BURNING2; i < BURNING2+14; i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = CRACKKNUCKLES; i < CRACKKNUCKLES+4; i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = FIRSTGUN; i < FIRSTGUN+3 ; i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = EXPLOSION2; i < EXPLOSION2+21 ; i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		GlobalMembersPREMAP.tloadtile(BULLETHOLE);

		for(i = FIRSTGUNRELOAD; i < FIRSTGUNRELOAD+8 ; i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		GlobalMembersPREMAP.tloadtile(FOOTPRINTS);

		for(i = JIBS1; i < (JIBS5+5); i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = SCRAP1; i < (SCRAP1+19); i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);

		for(i = SMALLSMOKE; i < (SMALLSMOKE+4); i++)
			if(waloff[i] == 0)
				GlobalMembersPREMAP.tloadtile(i);
	}

	public static sbyte getsound(ushort num)
	{
		short fp;
		int l;

		if(num >= NUM_SOUNDS || GlobalMembersCONFIG.SoundToggle == 0)
			return 0;
		if (GlobalMembersCONFIG.FXDevice == NumSoundCards)
			return 0;

		fp = kopen4load(GlobalMembersGLOBAL.sounds[num],GlobalMembersGLOBAL.loadfromgrouponly);
		if(fp == -1)
			return 0;

		l = kfilelength(fp);
		GlobalMembersGLOBAL.soundsiz[num] = l;

		if((GlobalMembersGLOBAL.ud.level_number == 0 && GlobalMembersGLOBAL.ud.volume_number == 0 && (num == 189 || num == 232 || num == 99 || num == 233 || num == 17)) || (l < 12288))
		{
			GlobalMembersGLOBAL.Sound[num].lock = 2;
			allocache((int)GlobalMembersGLOBAL.Sound[num].ptr,l,GlobalMembersGLOBAL.Sound[num].lock);
			if(GlobalMembersGLOBAL.Sound[num].ptr != null)
				kread(fp, GlobalMembersGLOBAL.Sound[num].ptr, l);
		}
		kclose(fp);
		return 1;
	}

	public static void precachenecessarysounds()
	{
		short i;
		short j;

		if (GlobalMembersCONFIG.FXDevice == NumSoundCards)
			return;
		j = 0;

		for(i = 0;i<NUM_SOUNDS;i++)
			if(GlobalMembersGLOBAL.Sound[i].ptr == 0)
			{
				j++;
				if((j &7) == 0)
					GlobalMembersGAME.getpackets();
				GlobalMembersPREMAP.getsound(i);
			}
	}


	public static void cacheit()
	{
		short i;
		short j;

		GlobalMembersPREMAP.precachenecessarysounds();

		GlobalMembersPREMAP.cachegoodsprites();

		for(i = 0;i<numwalls;i++)
			if(waloff[wall[i].picnum] == 0)
		{
			if(waloff[wall[i].picnum] == 0)
				GlobalMembersPREMAP.tloadtile(wall[i].picnum);
			if(wall[i].overpicnum >= 0 && waloff[wall[i].overpicnum] == 0)
				GlobalMembersPREMAP.tloadtile(wall[i].overpicnum);
		}

		for(i = 0;i<numsectors;i++)
		{
			if(waloff[sector[i].floorpicnum] == 0)
				GlobalMembersPREMAP.tloadtile(sector[i].floorpicnum);
			if(waloff[sector[i].ceilingpicnum] == 0)
			{
				GlobalMembersPREMAP.tloadtile(sector[i].ceilingpicnum);
				if(waloff[sector[i].ceilingpicnum] == LA)
				{
					GlobalMembersPREMAP.tloadtile(LA+1);
					GlobalMembersPREMAP.tloadtile(LA+2);
				}
			}

			j = headspritesect[i];
			while(j >= 0)
			{
				if(sprite[j].xrepeat != 0 && sprite[j].yrepeat != 0 && (sprite[j].cstat &32768) == 0)
					if(waloff[sprite[j].picnum] == 0)
						GlobalMembersPREMAP.cachespritenum(j);
				j = nextspritesect[j];
			}
		}

	}

	public static void docacheit()
	{
		int i;
		int j;

		j = 0;

		for(i = 0;i<MAXTILES;i++)
			if((gotpic[i>>3]&(1<<(i &7))) && waloff[i] == 0)
		{
			loadtile((short)i);
			j++;
			if((j &7) == 0)
				GlobalMembersGAME.getpackets();
		}

		clearbufbyte(gotpic,sizeof(gotpic),0);

	}



	public static void xyzmirror(short i,short wn)
	{
		if (waloff[wn] == 0)
			loadtile(wn);
		setviewtotile(wn,tilesizy[wn],tilesizx[wn]);

		drawrooms(SX,SY,SZ,SA,100+sprite[i].shade,SECT);
		GlobalMembersGLOBAL.display_mirror = 1;
		GlobalMembersGAME.animatesprites(SX, SY, SA, 65536);
		GlobalMembersGLOBAL.display_mirror = 0;
		drawmasks();

		setviewback();
		squarerotatetile(wn);
	}

	public static void vscrn()
	{
		 int i;
		 int j;
		 int ss;
		 int x1;
		 int x2;
		 int y1;
		 int y2;

		 if(GlobalMembersGLOBAL.ud.screen_size < 0)
			 GlobalMembersGLOBAL.ud.screen_size = 0;
		 else if(GlobalMembersGLOBAL.ud.screen_size > 63)
			 GlobalMembersGLOBAL.ud.screen_size = 64;

		 if(GlobalMembersGLOBAL.ud.screen_size == 0)
			 flushperms();

		 ss = max(GlobalMembersGLOBAL.ud.screen_size-8,0);

		 x1 = scale(ss,xdim,160);
		 x2 = xdim-x1;

		 y1 = ss;
		 y2 = 200;
		 if (GlobalMembersGLOBAL.ud.screen_size > 0 && GlobalMembersGLOBAL.ud.coop != 1 && GlobalMembersGLOBAL.ud.multimode > 1)
		 {
			 j = 0;
			 for(i = connecthead;i>=0;i = connectpoint2[i])
				 if(i > j)
					 j = i;

			 if (j >= 1)
				 y1 += 8;
			 if (j >= 4)
				 y1 += 8;
			 if (j >= 8)
				 y1 += 8;
			 if (j >= 12)
				 y1 += 8;
		 }

		 if (GlobalMembersGLOBAL.ud.screen_size >= 8)
			 y2 -= (ss+34);

		 y1 = scale(y1,ydim,200);
		 y2 = scale(y2,ydim,200);

		 setview(x1,y1,x2-1,y2-1);

		 GlobalMembersGLOBAL.pub = NUMPAGES;
		 GlobalMembersGLOBAL.pus = NUMPAGES;
	}

	public static void pickrandomspot(short snum)
	{
		player_struct p;
		short i;

		p = GlobalMembersGLOBAL.ps[snum];

		if(GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop != 1)
			i = TRAND%GlobalMembersGLOBAL.numplayersprites;
		else
			i = snum;

		p.bobposx = p.oposx = p.posx = GlobalMembersGLOBAL.po[i].ox;
		p.bobposy = p.oposy = p.posy = GlobalMembersGLOBAL.po[i].oy;
		p.oposz = p.posz = GlobalMembersGLOBAL.po[i].oz;
		p.ang = GlobalMembersGLOBAL.po[i].oa;
		p.cursectnum = GlobalMembersGLOBAL.po[i].os;
	}

	public static void resetplayerstats(short snum)
	{
		player_struct p;
		short i;

		p = GlobalMembersGLOBAL.ps[snum];

		GlobalMembersGLOBAL.ud.show_help = 0;
		GlobalMembersGLOBAL.ud.showallmap = 0;
		p.dead_flag = 0;
		p.wackedbyactor = -1;
		p.falling_counter = 0;
		p.quick_kick = 0;
		p.subweapon = 0;
		p.last_full_weapon = 0;
		p.ftq = 0;
		p.fta = 0;
		p.tipincs = 0;
		p.buttonpalette = 0;
		p.actorsqu =-1;
		p.invdisptime = 0;
		p.refresh_inventory= 0;
		p.last_pissed_time = 0;
		p.holster_weapon = 0;
		p.pycount = 0;
		p.pyoff = 0;
		p.opyoff = 0;
		p.loogcnt = 0;
		p.angvel = 0;
		p.weapon_sway = 0;
	//    p->select_dir       = 0;
		p.extra_extra8 = 0;
		p.show_empty_weapon= 0;
		p.dummyplayersprite=-1;
		p.crack_time = 0;
		p.hbomb_hold_delay = 0;
		p.transporter_hold = 0;
		p.wantweaponfire = -1;
		p.hurt_delay = 0;
		p.footprintcount = 0;
		p.footprintpal = 0;
		p.footprintshade = 0;
		p.jumping_toggle = 0;
		p.ohoriz = p.horiz= 140;
		p.horizoff = 0;
		p.bobcounter = 0;
		p.on_ground = 0;
		p.player_par = 0;
		p.return_to_center = 9;
		p.airleft = 15 *26;
		p.rapid_fire_hold = 0;
		p.toggle_key_flag = 0;
		p.access_spritenum = -1;
		if(GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop != 1)
			p.got_access = 7;
		else
			p.got_access = 0;
		p.random_club_frame= 0;
		GlobalMembersGLOBAL.pus = 1;
		p.on_warping_sector = 0;
		p.spritebridge = 0;
		p.palette = (string) &palette[0];

		if(p.steroids_amount < 400)
		{
			p.steroids_amount = 0;
			p.inven_icon = 0;
		}
		p.heat_on = 0;
		p.jetpack_on = 0;
		p.holoduke_on = -1;

		p.look_ang = 512 - ((GlobalMembersGLOBAL.ud.level_number &1)<<10);

		p.rotscrnang = 0;
		p.newowner =-1;
		p.jumping_counter = 0;
		p.hard_landing = 0;
		p.posxv = 0;
		p.posyv = 0;
		p.poszv = 0;
		GlobalMembersGLOBAL.fricxv = 0;
		GlobalMembersGLOBAL.fricyv = 0;
		p.somethingonplayer =-1;
		p.one_eighty_count = 0;
		p.cheat_phase = 0;

		p.on_crane = -1;

		if(p.curr_weapon == PISTOL_WEAPON)
			p.kickback_pic = 5;
		else
			p.kickback_pic = 0;

		p.weapon_pos = 6;
		p.walking_snd_toggle= 0;
		p.weapon_ang = 0;

		p.knuckle_incs = 1;
		p.fist_incs = 0;
		p.knee_incs = 0;
		p.jetpack_on = 0;
		GlobalMembersPLAYER.setpal(ref p);
	}



	public static void resetweapons(short snum)
	{
		short weapon;
		player_struct p;

		p = GlobalMembersGLOBAL.ps[snum];

		for (weapon = PISTOL_WEAPON; weapon < MAX_WEAPONS; weapon++)
			p.gotweapon[weapon] = 0;
		for (weapon = PISTOL_WEAPON; weapon < MAX_WEAPONS; weapon++)
			p.ammo_amount[weapon] = 0;

		p.weapon_pos = 6;
		p.kickback_pic = 5;
		p.curr_weapon = PISTOL_WEAPON;
		p.gotweapon[PISTOL_WEAPON] = 1;
		p.gotweapon[KNEE_WEAPON] = 1;
		p.ammo_amount[PISTOL_WEAPON] = 48;
		p.gotweapon[HANDREMOTE_WEAPON] = 1;
		p.last_weapon = -1;

		p.show_empty_weapon= 0;
		p.last_pissed_time = 0;
		p.holster_weapon = 0;
	}

	public static void resetinventory(short snum)
	{
		player_struct p;
		short i;

		p = GlobalMembersGLOBAL.ps[snum];

		p.inven_icon = 0;
		p.boot_amount = 0;
		p.scuba_on = 0;
		p.scuba_amount = 0;
		p.heat_amount = 0;
		p.heat_on = 0;
		p.jetpack_on = 0;
		p.jetpack_amount = 0;
		p.shield_amount = GlobalMembersGLOBAL.max_armour_amount;
		p.holoduke_on = -1;
		p.holoduke_amount = 0;
		p.firstaid_amount = 0;
		p.steroids_amount = 0;
		p.inven_icon = 0;
	}


	public static void resetprestat(short snum,sbyte g)
	{
		player_struct p;
		short i;

		p = GlobalMembersGLOBAL.ps[snum];

		GlobalMembersGLOBAL.spriteqloc = 0;
		for(i = 0;i<GlobalMembersGLOBAL.spriteqamount;i++)
			GlobalMembersGLOBAL.spriteq[i] = -1;

		p.hbomb_on = 0;
		p.cheat_phase = 0;
		p.pals_time = 0;
		p.toggle_key_flag = 0;
		p.secret_rooms = 0;
		p.max_secret_rooms = 0;
		p.actors_killed = 0;
		p.max_actors_killed = 0;
		p.lastrandomspot = 0;
		p.weapon_pos = 6;
		p.kickback_pic = 5;
		p.last_weapon = -1;
		p.weapreccnt = 0;
		p.show_empty_weapon= 0;
		p.holster_weapon = 0;
		p.last_pissed_time = 0;

		p.one_parallax_sectnum = -1;
		p.visibility = GlobalMembersGLOBAL.ud.const_visibility;

		GlobalMembersGLOBAL.screenpeek = myconnectindex;
		GlobalMembersGLOBAL.numanimwalls = 0;
		GlobalMembersGLOBAL.numcyclers = 0;
		GlobalMembersGLOBAL.animatecnt = 0;
		parallaxtype = 0;
		randomseed = 17;
		GlobalMembersGLOBAL.ud.pause_on = 0;
		GlobalMembersGLOBAL.ud.camerasprite =-1;
		GlobalMembersGLOBAL.ud.eog = 0;
		GlobalMembersGAME.tempwallptr = 0;
		GlobalMembersGLOBAL.camsprite = -1;
		GlobalMembersGLOBAL.earthquaketime = 0;

		GlobalMembersGLOBAL.numinterpolations = 0;
		GlobalMembersGLOBAL.startofdynamicinterpolations = 0;

		if(((g &MODE_EOL) != MODE_EOL && numplayers < 2) || (GlobalMembersGLOBAL.ud.coop != 1 && numplayers > 1))
		{
			GlobalMembersPREMAP.resetweapons(snum);
			GlobalMembersPREMAP.resetinventory(snum);
		}
		else if(p.curr_weapon == HANDREMOTE_WEAPON)
		{
			p.ammo_amount[HANDBOMB_WEAPON]++;
			p.curr_weapon = HANDBOMB_WEAPON;
		}

		p.timebeforeexit = 0;
		p.customexitsound = 0;

	}

	public static void setupbackdrop(short sky)
	{
		short i;

		for(i = 0;i<MAXPSKYTILES;i++)
			pskyoff[i]=0;

		if(parallaxyscale != 65536)
			parallaxyscale = 32768;

		switch(sky)
		{
			case CLOUDYOCEAN:
				parallaxyscale = 65536;
				break;
			case MOONSKY1 :
				pskyoff[6]=1;
				pskyoff[1]=2;
				pskyoff[4]=2;
				pskyoff[2]=3;
				break;
			case BIGORBIT1: // orbit
				pskyoff[5]=1;
				pskyoff[6]=2;
				pskyoff[7]=3;
				pskyoff[2]=4;
				break;
			case LA:
				parallaxyscale = 16384+1024;
				pskyoff[0]=1;
				pskyoff[1]=2;
				pskyoff[2]=1;
				pskyoff[3]=3;
				pskyoff[4]=4;
				pskyoff[5]=0;
				pskyoff[6]=2;
				pskyoff[7]=3;
				break;
	   }

	   pskybits=3;
	}

	public static void prelevel(sbyte g)
	{
		short i;
		short nexti;
		short j;
		short startwall;
		short endwall;
		short lotaglist;
		short[] lotags = new short[65];


		clearbufbyte(show2dsector,sizeof(show2dsector),0);
		clearbufbyte(show2dwall,sizeof(show2dwall),0);
		clearbufbyte(show2dsprite,sizeof(show2dsprite),0);

		GlobalMembersPREMAP.resetprestat(0, g);
		GlobalMembersGLOBAL.numclouds = 0;

		for(i = 0;i<numsectors;i++)
		{
			sector[i].extra = 256;

			switch(sector[i].lotag)
			{
				case 20:
				case 22:
					if(sector[i].floorz > sector[i].ceilingz)
						sector[i].lotag |= 32768;
					continue;
			}

			if(sector[i].ceilingstat &1)
			{
				if(waloff[sector[i].ceilingpicnum] == 0)
				{
					if(sector[i].ceilingpicnum == LA)
						for(j = 0;j<5;j++)
							if(waloff[sector[i].ceilingpicnum+j] == 0)
								GlobalMembersPREMAP.tloadtile(sector[i].ceilingpicnum+j);
				}
				GlobalMembersPREMAP.setupbackdrop(sector[i].ceilingpicnum);

				if(sector[i].ceilingpicnum == CLOUDYSKIES && GlobalMembersGLOBAL.numclouds < 127)
					GlobalMembersGLOBAL.clouds[GlobalMembersGLOBAL.numclouds++] = i;

				if(GlobalMembersGLOBAL.ps[0].one_parallax_sectnum == -1)
					GlobalMembersGLOBAL.ps[0].one_parallax_sectnum = i;
			}

			if(sector[i].lotag == 32767) //Found a secret room
			{
				GlobalMembersGLOBAL.ps[0].max_secret_rooms++;
				continue;
			}

			if(sector[i].lotag == -1)
			{
				GlobalMembersGLOBAL.ps[0].exitx = wall[sector[i].wallptr].x;
				GlobalMembersGLOBAL.ps[0].exity = wall[sector[i].wallptr].y;
				continue;
			}
		}

		i = headspritestat[0];
		while(i >= 0)
		{
			nexti = nextspritestat[i];

			if(sprite[i].lotag == -1 && (sprite[i].cstat &16))
			{
				GlobalMembersGLOBAL.ps[0].exitx = SX;
				GlobalMembersGLOBAL.ps[0].exity = SY;
			}
			else
				switch(PN)
			{
				case GPSPEED:
					sector[SECT].extra = SLT;
					deletesprite(i);
					break;

				case CYCLER:
					if(GlobalMembersGLOBAL.numcyclers >= MAXCYCLERS)
						GlobalMembersGAME.gameexit("\nToo many cycling sectors.");
					GlobalMembersGLOBAL.cyclers[GlobalMembersGLOBAL.numcyclers, 0] = SECT;
					GlobalMembersGLOBAL.cyclers[GlobalMembersGLOBAL.numcyclers, 1] = SLT;
					GlobalMembersGLOBAL.cyclers[GlobalMembersGLOBAL.numcyclers, 2] = SS;
					GlobalMembersGLOBAL.cyclers[GlobalMembersGLOBAL.numcyclers, 3] = sector[SECT].floorshade;
					GlobalMembersGLOBAL.cyclers[GlobalMembersGLOBAL.numcyclers, 4] = SHT;
					GlobalMembersGLOBAL.cyclers[GlobalMembersGLOBAL.numcyclers, 5] = (SA == 1536);
					GlobalMembersGLOBAL.numcyclers++;
					deletesprite(i);
					break;
			}
			i = nexti;
		}

		for(i = 0;i < MAXSPRITES;i++)
		{
			if(sprite[i].statnum < MAXSTATUS)
			{
				if(PN == SECTOREFFECTOR && SLT == 14)
					continue;
				GlobalMembersGAME.spawn(-1, i);
			}
		}

		for(i = 0;i < MAXSPRITES;i++)
			if(sprite[i].statnum < MAXSTATUS)
			{
				if(PN == SECTOREFFECTOR && SLT == 14)
					GlobalMembersGAME.spawn(-1, i);
			}

		lotaglist = 0;

		i = headspritestat[0];
		while(i >= 0)
		{
			switch(PN)
			{
				case DIPSWITCH:
				case DIPSWITCH2:
				case ACCESSSWITCH:
				case PULLSWITCH:
				case HANDSWITCH:
				case SLOTDOOR:
				case LIGHTSWITCH:
				case SPACELIGHTSWITCH:
				case SPACEDOORSWITCH:
				case FRANKENSTINESWITCH:
				case LIGHTSWITCH2:
				case POWERSWITCH1:
				case LOCKSWITCH1:
				case POWERSWITCH2:
					break;
				case DIPSWITCH+1:
				case DIPSWITCH2+1:
				case PULLSWITCH+1:
				case HANDSWITCH+1:
				case SLOTDOOR+1:
				case LIGHTSWITCH+1:
				case SPACELIGHTSWITCH+1:
				case SPACEDOORSWITCH+1:
				case FRANKENSTINESWITCH+1:
				case LIGHTSWITCH2+1:
				case POWERSWITCH1+1:
				case LOCKSWITCH1+1:
				case POWERSWITCH2+1:
					for(j = 0;j<lotaglist;j++)
						if(SLT == lotags[j])
							break;

					if(j == lotaglist)
					{
						lotags[lotaglist] = SLT;
						lotaglist++;
						if(lotaglist > 64)
							GlobalMembersGAME.gameexit("\nToo many switches (64 max).");

						j = headspritestat[3];
						while(j >= 0)
						{
							if(sprite[j].lotag == 12 && sprite[j].hitag == SLT)
								GlobalMembersGLOBAL.hittype[j].temp_data[0] = 1;
							j = nextspritestat[j];
						}
					}
					break;
			}
			i = nextspritestat[i];
		}

		GlobalMembersGLOBAL.mirrorcnt = 0;

		for(i = 0; i < numwalls; i++)
		{
			walltype wal;
			wal = wall[i];

			if(wal.overpicnum == MIRROR && (wal.cstat &32) != 0)
			{
				j = wal.nextsector;

				if(GlobalMembersGLOBAL.mirrorcnt > 63)
					GlobalMembersGAME.gameexit("\nToo many mirrors (64 max.)");
				if ((j >= 0) && sector[j].ceilingpicnum != MIRROR)
				{
					sector[j].ceilingpicnum = MIRROR;
					sector[j].floorpicnum = MIRROR;
					GlobalMembersGLOBAL.mirrorwall[GlobalMembersGLOBAL.mirrorcnt] = i;
					GlobalMembersGLOBAL.mirrorsector[GlobalMembersGLOBAL.mirrorcnt] = j;
					GlobalMembersGLOBAL.mirrorcnt++;
					continue;
				}
			}

			if(GlobalMembersGLOBAL.numanimwalls >= MAXANIMWALLS)
				GlobalMembersGAME.gameexit("\nToo many 'anim' walls (max 512.)");

			GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].tag = 0;
			GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].wallnum = 0;

			switch(wal.overpicnum)
			{
				case FANSHADOW:
				case FANSPRITE:
					wall.cstat |= 65;
					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].wallnum = i;
					GlobalMembersGLOBAL.numanimwalls++;
					break;

				case W_FORCEFIELD:
					if(waloff[W_FORCEFIELD] == 0)
						for(j = 0;j<3;j++)
							GlobalMembersPREMAP.tloadtile(W_FORCEFIELD+j);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case W_FORCEFIELD+1:
				case W_FORCEFIELD+2:
					if(wal.shade > 31)
						wal.cstat = 0;
					else
						wal.cstat |= 85+256;


					if(wal.lotag && wal.nextwall >= 0)
						wall[wal.nextwall].lotag = wal.lotag;

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case BIGFORCE:

					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].wallnum = i;
					GlobalMembersGLOBAL.numanimwalls++;

					continue;
			}

			wal.extra = -1;

			switch(wal.picnum)
			{
				case WATERTILE2:
					for(j = 0;j<3;j++)
						if(waloff[wal.picnum+j] == 0)
							GlobalMembersPREMAP.tloadtile(wal.picnum+j);
					break;

				case TECHLIGHT2:
				case TECHLIGHT4:
					if(waloff[wal.picnum] == 0)
						GlobalMembersPREMAP.tloadtile(wal.picnum);
					break;
				case W_TECHWALL1:
				case W_TECHWALL2:
				case W_TECHWALL3:
				case W_TECHWALL4:
					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].wallnum = i;
	//                animwall[numanimwalls].tag = -1;
					GlobalMembersGLOBAL.numanimwalls++;
					break;
				case SCREENBREAK6:
				case SCREENBREAK7:
				case SCREENBREAK8:
					if(waloff[SCREENBREAK6] == 0)
						for(j = SCREENBREAK6;j<SCREENBREAK9;j++)
							GlobalMembersPREMAP.tloadtile(j);
					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].wallnum = i;
					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].tag = -1;
					GlobalMembersGLOBAL.numanimwalls++;
					break;

				case FEMPIC1:
				case FEMPIC2:
				case FEMPIC3:

					wal.extra = wal.picnum;
					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].tag = -1;
					if(GlobalMembersGLOBAL.ud.lockout)
					{
						if(wal.picnum == FEMPIC1)
							wal.picnum = BLANKSCREEN;
						else
							wal.picnum = SCREENBREAK6;
					}

					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].wallnum = i;
					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].tag = wal.picnum;
					GlobalMembersGLOBAL.numanimwalls++;
					break;

				case SCREENBREAK1:
				case SCREENBREAK2:
				case SCREENBREAK3:
				case SCREENBREAK4:
				case SCREENBREAK5:

				case SCREENBREAK9:
				case SCREENBREAK10:
				case SCREENBREAK11:
				case SCREENBREAK12:
				case SCREENBREAK13:
				case SCREENBREAK14:
				case SCREENBREAK15:
				case SCREENBREAK16:
				case SCREENBREAK17:
				case SCREENBREAK18:
				case SCREENBREAK19:

					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].wallnum = i;
					GlobalMembersGLOBAL.animwall[GlobalMembersGLOBAL.numanimwalls].tag = wal.picnum;
					GlobalMembersGLOBAL.numanimwalls++;
					break;
			}
		}

		//Invalidate textures in sector behind mirror
		for(i = 0;i<GlobalMembersGLOBAL.mirrorcnt;i++)
		{
			startwall = sector[GlobalMembersGLOBAL.mirrorsector[i]].wallptr;
			endwall = startwall + sector[GlobalMembersGLOBAL.mirrorsector[i]].wallnum;
			for(j = startwall;j<endwall;j++)
			{
				wall[j].picnum = MIRROR;
				wall[j].overpicnum = MIRROR;
			}
		}
	}


	public static void newgame(sbyte vn,sbyte ln,sbyte sk)
	{
		player_struct p = GlobalMembersGLOBAL.ps[0];
		short i;

		if(GlobalMembersMENUES.globalskillsound >= 0)
			while(GlobalMembersGLOBAL.Sound[GlobalMembersMENUES.globalskillsound].lock>=200);
		GlobalMembersMENUES.globalskillsound = -1;

		GlobalMembersPREMAP.waitforeverybody();
		GlobalMembersGLOBAL.ready2send = 0;

		if(GlobalMembersGLOBAL.ud.m_recstat != 2 && GlobalMembersGLOBAL.ud.last_level >= 0 && GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop != 1)
			GlobalMembersGAME.dobonus(1);

		if(ln == 0 && vn == 3 && GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.lockout == 0)
		{
			GlobalMembersSOUNDS.playmusic(ref GlobalMembersGLOBAL.env_music_fn[1, 0]);

			flushperms();
			setview(0,0,xdim-1,ydim-1);
			clearview(0);
			nextpage();

			GlobalMembersMENUES.playanm("vol41a.anm", 6);
			clearview(0);
			nextpage();
			GlobalMembersMENUES.playanm("vol42a.anm", 7);
	//        clearview(0L);
	  //      nextpage();
			GlobalMembersMENUES.playanm("vol43a.anm", 9);
			clearview(0);
			nextpage();

			FX_StopAllSounds();
		}

		GlobalMembersGLOBAL.show_shareware = 26 *34;

		GlobalMembersGLOBAL.ud.level_number = ln;
		GlobalMembersGLOBAL.ud.volume_number = vn;
		GlobalMembersGLOBAL.ud.player_skill = sk;
		GlobalMembersGLOBAL.ud.secretlevel = 0;
		GlobalMembersGLOBAL.ud.from_bonus = 0;
		parallaxyscale = 0;

		GlobalMembersGLOBAL.ud.last_level = -1;
		GlobalMembersMENUES.lastsavedpos = -1;
		p.zoom = 768;
		p.gm = 0;

		if(GlobalMembersGLOBAL.ud.m_coop != 1)
		{
			p.curr_weapon = PISTOL_WEAPON;
			p.gotweapon[PISTOL_WEAPON] = 1;
			p.gotweapon[KNEE_WEAPON] = 1;
			p.ammo_amount[PISTOL_WEAPON] = 48;
			p.gotweapon[HANDREMOTE_WEAPON] = 1;
			p.last_weapon = -1;
		}

		GlobalMembersGLOBAL.display_mirror = 0;

		if(GlobalMembersGLOBAL.ud.multimode > 1)
		{
			if(numplayers < 2)
			{
				connecthead = 0;
				for(i = 0;i<MAXPLAYERS;i++)
					connectpoint2[i] = i+1;
				connectpoint2[GlobalMembersGLOBAL.ud.multimode-1] = -1;
			}
		}
		else
		{
			connecthead = 0;
			connectpoint2[0] = -1;
		}
	}


	public static void resetpspritevars(sbyte g)
	{
		short i;
		short j;
		short nexti;
		short circ;
		int firstx;
		int firsty;
		spritetype s;
		string aimmode = new string(new char[MAXPLAYERS]);
		STATUSBARTYPE[] tsbar = new STATUSBARTYPE[MAXPLAYERS];

		GlobalMembersGAME.EGS(GlobalMembersGLOBAL.ps[0].cursectnum, GlobalMembersGLOBAL.ps[0].posx, GlobalMembersGLOBAL.ps[0].posy, GlobalMembersGLOBAL.ps[0].posz, APLAYER, 0, 0, 0, GlobalMembersGLOBAL.ps[0].ang, 0, 0, 0, 10);

		if(GlobalMembersGLOBAL.ud.recstat != 2)
			for(i = 0;i<MAXPLAYERS;i++)
		{
			aimmode = StringFunctions.ChangeCharacter(aimmode, i, GlobalMembersGLOBAL.ps[i].aim_mode);
			if(GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop == 1 && GlobalMembersGLOBAL.ud.last_level >= 0)
			{
				for(j = 0;j<MAX_WEAPONS;j++)
				{
					tsbar[i].ammo_amount[j] = GlobalMembersGLOBAL.ps[i].ammo_amount[j];
					tsbar[i].gotweapon[j] = GlobalMembersGLOBAL.ps[i].gotweapon[j];
				}

				tsbar[i].shield_amount = GlobalMembersGLOBAL.ps[i].shield_amount;
				tsbar[i].curr_weapon = GlobalMembersGLOBAL.ps[i].curr_weapon;
				tsbar[i].inven_icon = GlobalMembersGLOBAL.ps[i].inven_icon;

				tsbar[i].firstaid_amount = GlobalMembersGLOBAL.ps[i].firstaid_amount;
				tsbar[i].steroids_amount = GlobalMembersGLOBAL.ps[i].steroids_amount;
				tsbar[i].holoduke_amount = GlobalMembersGLOBAL.ps[i].holoduke_amount;
				tsbar[i].jetpack_amount = GlobalMembersGLOBAL.ps[i].jetpack_amount;
				tsbar[i].heat_amount = GlobalMembersGLOBAL.ps[i].heat_amount;
				tsbar[i].scuba_amount = GlobalMembersGLOBAL.ps[i].scuba_amount;
				tsbar[i].boot_amount = GlobalMembersGLOBAL.ps[i].boot_amount;
			}
		}

		GlobalMembersPREMAP.resetplayerstats(0);

		for(i = 1;i<MAXPLAYERS;i++)
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
		   memcpy(GlobalMembersGLOBAL.ps[i],GlobalMembersGLOBAL.ps[0],sizeof(player_struct));

		if(GlobalMembersGLOBAL.ud.recstat != 2)
			for(i = 0;i<MAXPLAYERS;i++)
		{
			GlobalMembersGLOBAL.ps[i].aim_mode = aimmode[i];
			if(GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop == 1 && GlobalMembersGLOBAL.ud.last_level >= 0)
			{
				for(j = 0;j<MAX_WEAPONS;j++)
				{
					GlobalMembersGLOBAL.ps[i].ammo_amount[j] = tsbar[i].ammo_amount[j];
					GlobalMembersGLOBAL.ps[i].gotweapon[j] = tsbar[i].gotweapon[j];
				}
				GlobalMembersGLOBAL.ps[i].shield_amount = tsbar[i].shield_amount;
				GlobalMembersGLOBAL.ps[i].curr_weapon = tsbar[i].curr_weapon;
				GlobalMembersGLOBAL.ps[i].inven_icon = tsbar[i].inven_icon;

				GlobalMembersGLOBAL.ps[i].firstaid_amount = tsbar[i].firstaid_amount;
				GlobalMembersGLOBAL.ps[i].steroids_amount= tsbar[i].steroids_amount;
				GlobalMembersGLOBAL.ps[i].holoduke_amount = tsbar[i].holoduke_amount;
				GlobalMembersGLOBAL.ps[i].jetpack_amount = tsbar[i].jetpack_amount;
				GlobalMembersGLOBAL.ps[i].heat_amount = tsbar[i].heat_amount;
				GlobalMembersGLOBAL.ps[i].scuba_amount= tsbar[i].scuba_amount;
				GlobalMembersGLOBAL.ps[i].boot_amount = tsbar[i].boot_amount;
			}
		}

		GlobalMembersGLOBAL.numplayersprites = 0;
		circ = 2048/GlobalMembersGLOBAL.ud.multimode;

		which_palookup = 9;
		j = connecthead;
		i = headspritestat[10];
		while(i >= 0)
		{
			nexti = nextspritestat[i];
			s = sprite[i];

			if(GlobalMembersGLOBAL.numplayersprites == MAXPLAYERS)
				GlobalMembersGAME.gameexit("\nToo many player sprites (max 16.)");

			if(GlobalMembersGLOBAL.numplayersprites == 0)
			{
				firstx = GlobalMembersGLOBAL.ps[0].posx;
				firsty = GlobalMembersGLOBAL.ps[0].posy;
			}

			GlobalMembersGLOBAL.po[GlobalMembersGLOBAL.numplayersprites].ox = s.x;
			GlobalMembersGLOBAL.po[GlobalMembersGLOBAL.numplayersprites].oy = s.y;
			GlobalMembersGLOBAL.po[GlobalMembersGLOBAL.numplayersprites].oz = s.z;
			GlobalMembersGLOBAL.po[GlobalMembersGLOBAL.numplayersprites].oa = s.ang;
			GlobalMembersGLOBAL.po[GlobalMembersGLOBAL.numplayersprites].os = s.sectnum;

			GlobalMembersGLOBAL.numplayersprites++;
			if(j >= 0)
			{
				s.owner = i;
				s.shade = 0;
				s.xrepeat = 42;
				s.yrepeat = 36;
				s.cstat = 1+256;
				s.xoffset = 0;
				s.clipdist = 64;

				if((g &MODE_EOL) != MODE_EOL || GlobalMembersGLOBAL.ps[j].last_extra == 0)
				{
					GlobalMembersGLOBAL.ps[j].last_extra = GlobalMembersGLOBAL.max_player_health;
					s.extra = GlobalMembersGLOBAL.max_player_health;
				}
				else
					s.extra = GlobalMembersGLOBAL.ps[j].last_extra;

				s.yvel = j;

				if(s.pal == 0)
				{
					s.pal = GlobalMembersGLOBAL.ps[j].palookup = which_palookup;
					which_palookup++;
					if(which_palookup >= 17)
						which_palookup = 9;
				}
				else
					GlobalMembersGLOBAL.ps[j].palookup = s.pal;

				GlobalMembersGLOBAL.ps[j].i = i;
				GlobalMembersGLOBAL.ps[j].frag_ps = j;
				GlobalMembersGLOBAL.hittype[i].owner = i;

				GlobalMembersGLOBAL.hittype[i].bposx = GlobalMembersGLOBAL.ps[j].bobposx = GlobalMembersGLOBAL.ps[j].oposx = GlobalMembersGLOBAL.ps[j].posx = s.x;
				GlobalMembersGLOBAL.hittype[i].bposy = GlobalMembersGLOBAL.ps[j].bobposy = GlobalMembersGLOBAL.ps[j].oposy = GlobalMembersGLOBAL.ps[j].posy = s.y;
				GlobalMembersGLOBAL.hittype[i].bposz = GlobalMembersGLOBAL.ps[j].oposz = GlobalMembersGLOBAL.ps[j].posz = s.z;
				GlobalMembersGLOBAL.ps[j].oang = GlobalMembersGLOBAL.ps[j].ang = s.ang;

				updatesector(s.x,s.y,GlobalMembersGLOBAL.ps[j].cursectnum);

				j = connectpoint2[j];

			}
			else
				deletesprite(i);
			i = nexti;
		}
	}

	public static void clearfrags()
	{
		short i;

		for(i = 0;i<MAXPLAYERS;i++)
			GlobalMembersGLOBAL.ps[i].frag = GlobalMembersGLOBAL.ps[i].fraggedself = 0;
		 clearbufbyte(GlobalMembersGLOBAL.frags[0, 0],(MAXPLAYERS *MAXPLAYERS)<<1,0);
	}

	public static void resettimevars()
	{
		GlobalMembersGLOBAL.vel = GlobalMembersGLOBAL.svel = GlobalMembersGLOBAL.angvel = GlobalMembersGLOBAL.horiz = 0;

		totalclock = 0;
		GlobalMembersGLOBAL.cloudtotalclock = 0;
		GlobalMembersGLOBAL.ototalclock = 0;
		GlobalMembersGLOBAL.lockclock = 0;
		GlobalMembersGLOBAL.ready2send = 1;
	}


	public static void genspriteremaps()
	{
		int j;
		int fp;
		sbyte look_pos;
		string lookfn = "lookup.dat";
		sbyte numl;

		fp = kopen4load(lookfn,0);
		if(fp != -1)
			kread(fp, (string)&numl, 1);
		else
			GlobalMembersGAME.gameexit("\nERROR: File 'LOOKUP.DAT' not found.");

		for(j = 0;j < numl;j++)
		{
			kread(fp, (string)&look_pos, 1);
			kread(fp,GlobalMembersGLOBAL.tempbuf,256);
			makepalookup((int)look_pos,GlobalMembersGLOBAL.tempbuf,0,0,0,1);
		}

		kread(fp,GlobalMembersGAME.waterpal[0],768);
		kread(fp,GlobalMembersGAME.slimepal[0],768);
		kread(fp,GlobalMembersGAME.titlepal[0],768);
		kread(fp,GlobalMembersGAME.drealms[0],768);
		kread(fp,GlobalMembersGAME.endingpal[0],768);

		palette[765] = palette[766] = palette[767] = 0;
		GlobalMembersGAME.slimepal = StringFunctions.ChangeCharacter(GlobalMembersGAME.slimepal, 765, GlobalMembersGAME.slimepal[766] = GlobalMembersGAME.slimepal[767] = 0);
		GlobalMembersGAME.waterpal = StringFunctions.ChangeCharacter(GlobalMembersGAME.waterpal, 765, GlobalMembersGAME.waterpal[766] = GlobalMembersGAME.waterpal[767] = 0);

		kclose(fp);
	}

	public static void waitforeverybody()
	{
		int i;

		if (numplayers < 2)
			return;
		GlobalMembersGLOBAL.packbuf[0] = 250;
		for(i = connecthead;i>=0;i = connectpoint2[i])
			if (i != myconnectindex)
				sendpacket(i,GlobalMembersGLOBAL.packbuf,1);

		GlobalMembersGLOBAL.playerreadyflag[myconnectindex]++;
		do
		{
			GlobalMembersGAME.getpackets();
			for(i = connecthead;i>=0;i = connectpoint2[i])
				if (GlobalMembersGLOBAL.playerreadyflag[i] < GlobalMembersGLOBAL.playerreadyflag[myconnectindex])
					break;
		} while (i >= 0);
	}

	public static void dofrontscreens()
	{
		int tincs;
		int i;
		int j;

		if(GlobalMembersGLOBAL.ud.recstat != 2)
		{
			GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
			for(j = 0;j<63;j+=7)
				GlobalMembersMENUES.palto(0, 0, 0, j);
			i = GlobalMembersGLOBAL.ud.screen_size;
			GlobalMembersGLOBAL.ud.screen_size = 0;
			GlobalMembersPREMAP.vscrn();
			clearview(0);

			rotatesprite(320<<15,200<<15,65536,0,LOADSCREEN,0,0,2+8+64,0,0,xdim-1,ydim-1);

			if(GlobalMembersGAME.boardfilename[0] != 0 && GlobalMembersGLOBAL.ud.level_number == 7 && GlobalMembersGLOBAL.ud.volume_number == 0)
			{
				GlobalMembersMENUES.menutext(160, 90, 0, 0, "ENTERING USER MAP");
				GlobalMembersGAME.gametextpal(160, 90+10, ref GlobalMembersGAME.boardfilename, 14, 2);
			}
			else
			{
				GlobalMembersMENUES.menutext(160, 90, 0, 0, "ENTERING");
				GlobalMembersMENUES.menutext(160, 90+16+8, 0, 0, ref GlobalMembersGLOBAL.level_names[(GlobalMembersGLOBAL.ud.volume_number *11) + GlobalMembersGLOBAL.ud.level_number]);
			}

			nextpage();

			for(j = 63;j>0;j-=7)
				GlobalMembersMENUES.palto(0, 0, 0, j);

			KB_FlushKeyboardQueue();
			GlobalMembersGLOBAL.ud.screen_size = i;
		}
		else
		{
			clearview(0);
			GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
			GlobalMembersMENUES.palto(0, 0, 0, 0);
			rotatesprite(320<<15,200<<15,65536,0,LOADSCREEN,0,0,2+8+64,0,0,xdim-1,ydim-1);
			GlobalMembersMENUES.menutext(160, 105, 0, 0, "LOADING...");
			nextpage();
		}
	}

	public static void clearfifo()
	{
		GlobalMembersGLOBAL.syncvaltail = 0;
		GlobalMembersGLOBAL.syncvaltottail = 0;
		GlobalMembersGLOBAL.syncstat = 0;
		GlobalMembersGLOBAL.bufferjitter = 1;
		GlobalMembersGLOBAL.mymaxlag = GlobalMembersGLOBAL.otherminlag = 0;

		GlobalMembersGLOBAL.movefifoplc = GlobalMembersGLOBAL.movefifosendplc = GlobalMembersGLOBAL.fakemovefifoplc = 0;
		GlobalMembersGLOBAL.avgfvel = GlobalMembersGLOBAL.avgsvel = GlobalMembersGLOBAL.avgavel = GlobalMembersGLOBAL.avghorz = GlobalMembersGLOBAL.avgbits = 0;
		GlobalMembersGLOBAL.otherminlag = GlobalMembersGLOBAL.mymaxlag = 0;

		clearbufbyte(GlobalMembersGLOBAL.myminlag,MAXPLAYERS<<2,0);
		clearbufbyte(GlobalMembersGLOBAL.loc,sizeof(input),0);
		clearbufbyte(GlobalMembersGLOBAL.sync[0],sizeof(input),0);
		clearbufbyte(GlobalMembersGLOBAL.inputfifo,sizeof(input)*MOVEFIFOSIZ *MAXPLAYERS,0);

		clearbuf(GlobalMembersGLOBAL.movefifoend,MAXPLAYERS,0);
		clearbuf(GlobalMembersGLOBAL.syncvalhead,MAXPLAYERS,0);
		clearbuf(GlobalMembersGLOBAL.myminlag,MAXPLAYERS,0);

	//    clearbufbyte(playerquitflag,MAXPLAYERS,0x01);
	}

	public static void resetmys()
	{
		  GlobalMembersGLOBAL.myx = GlobalMembersGLOBAL.omyx = GlobalMembersGLOBAL.ps[myconnectindex].posx;
		  GlobalMembersGLOBAL.myy = GlobalMembersGLOBAL.omyy = GlobalMembersGLOBAL.ps[myconnectindex].posy;
		  GlobalMembersGLOBAL.myz = GlobalMembersGLOBAL.omyz = GlobalMembersGLOBAL.ps[myconnectindex].posz;
		  GlobalMembersGLOBAL.myxvel = GlobalMembersGLOBAL.myyvel = GlobalMembersGLOBAL.myzvel = 0;
		  GlobalMembersGLOBAL.myang = GlobalMembersGLOBAL.omyang = GlobalMembersGLOBAL.ps[myconnectindex].ang;
		  GlobalMembersGLOBAL.myhoriz = GlobalMembersGLOBAL.omyhoriz = GlobalMembersGLOBAL.ps[myconnectindex].horiz;
		  GlobalMembersGLOBAL.myhorizoff = GlobalMembersGLOBAL.omyhorizoff = GlobalMembersGLOBAL.ps[myconnectindex].horizoff;
		  GlobalMembersGLOBAL.mycursectnum = GlobalMembersGLOBAL.ps[myconnectindex].cursectnum;
		  GlobalMembersGLOBAL.myjumpingcounter = GlobalMembersGLOBAL.ps[myconnectindex].jumping_counter;
		  GlobalMembersGLOBAL.myjumpingtoggle = GlobalMembersGLOBAL.ps[myconnectindex].jumping_toggle;
		  GlobalMembersGLOBAL.myonground = GlobalMembersGLOBAL.ps[myconnectindex].on_ground;
		  GlobalMembersGLOBAL.myhardlanding = GlobalMembersGLOBAL.ps[myconnectindex].hard_landing;
		  GlobalMembersGLOBAL.myreturntocenter = GlobalMembersGLOBAL.ps[myconnectindex].return_to_center;
	}

	public static void enterlevel(sbyte g)
	{
		short i;
		short j;
		int l;
		string levname = new string(new char[256]);

		if((g &MODE_DEMO) != MODE_DEMO)
			GlobalMembersGLOBAL.ud.recstat = GlobalMembersGLOBAL.ud.m_recstat;
		GlobalMembersGLOBAL.ud.respawn_monsters = GlobalMembersGLOBAL.ud.m_respawn_monsters;
		GlobalMembersGLOBAL.ud.respawn_items = GlobalMembersGLOBAL.ud.m_respawn_items;
		GlobalMembersGLOBAL.ud.respawn_inventory = GlobalMembersGLOBAL.ud.m_respawn_inventory;
		GlobalMembersGLOBAL.ud.monsters_off = GlobalMembersGLOBAL.ud.m_monsters_off;
		GlobalMembersGLOBAL.ud.coop = GlobalMembersGLOBAL.ud.m_coop;
		GlobalMembersGLOBAL.ud.marker = GlobalMembersGLOBAL.ud.m_marker;
		GlobalMembersGLOBAL.ud.ffire = GlobalMembersGLOBAL.ud.m_ffire;

		if((g &MODE_DEMO) == 0 && GlobalMembersGLOBAL.ud.recstat == 2)
			GlobalMembersGLOBAL.ud.recstat = 0;

		FX_StopAllSounds();
		GlobalMembersSOUNDS.clearsoundlocks();
		FX_SetReverb(0);

		i = GlobalMembersGLOBAL.ud.screen_size;
		GlobalMembersGLOBAL.ud.screen_size = 0;
		GlobalMembersPREMAP.dofrontscreens();
		GlobalMembersPREMAP.vscrn();
		GlobalMembersGLOBAL.ud.screen_size = i;

	#if ! VOLUMEONE

		if(GlobalMembersGAME.boardfilename[0] != 0 && GlobalMembersGLOBAL.ud.m_level_number == 7 && GlobalMembersGLOBAL.ud.m_volume_number == 0)
		{
			if (loadboard(GlobalMembersGAME.boardfilename,GlobalMembersGLOBAL.ps[0].posx, GlobalMembersGLOBAL.ps[0].posy, GlobalMembersGLOBAL.ps[0].posz, GlobalMembersGLOBAL.ps[0].ang,GlobalMembersGLOBAL.ps[0].cursectnum) == -1)
			{
				GlobalMembersGLOBAL.tempbuf = string.Format("Map {0} not found!", GlobalMembersGAME.boardfilename);
				GlobalMembersGAME.gameexit(ref GlobalMembersGLOBAL.tempbuf);
			}
		}
		else if (loadboard(GlobalMembersGLOBAL.level_file_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.level_number],GlobalMembersGLOBAL.ps[0].posx, GlobalMembersGLOBAL.ps[0].posy, GlobalMembersGLOBAL.ps[0].posz, GlobalMembersGLOBAL.ps[0].ang,GlobalMembersGLOBAL.ps[0].cursectnum) == -1)
		{
			GlobalMembersGLOBAL.tempbuf = string.Format("Map {0} not found!", GlobalMembersGLOBAL.level_file_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.level_number]);
			GlobalMembersGAME.gameexit(ref GlobalMembersGLOBAL.tempbuf);
		}

	#else

		l = Convert.ToString(GlobalMembersGLOBAL.level_file_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.level_number]).Length;
		copybufbyte(GlobalMembersGLOBAL.level_file_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.level_number], levname[0], l);
		levname = StringFunctions.ChangeCharacter(levname, l, 255);
		levname = StringFunctions.ChangeCharacter(levname, l+1, 0);

		if (loadboard(levname,GlobalMembersGLOBAL.ps[0].posx, GlobalMembersGLOBAL.ps[0].posy, GlobalMembersGLOBAL.ps[0].posz, GlobalMembersGLOBAL.ps[0].ang,GlobalMembersGLOBAL.ps[0].cursectnum) == -1)
		{
			GlobalMembersGLOBAL.tempbuf = string.Format("Map {0} not found!", GlobalMembersGLOBAL.level_file_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.level_number]);
			GlobalMembersGAME.gameexit(ref GlobalMembersGLOBAL.tempbuf);
		}
	#endif

		clearbufbyte(gotpic,sizeof(gotpic),0);

		GlobalMembersPREMAP.prelevel(g);

		GlobalMembersSECTOR.allignwarpelevators();
		GlobalMembersPREMAP.resetpspritevars(g);

		cachedebug = 0;
		automapping = 0;

		if(GlobalMembersGLOBAL.ud.recstat != 2)
			MUSIC_StopSong();

		GlobalMembersPREMAP.cacheit();
		GlobalMembersPREMAP.docacheit();

		if(GlobalMembersGLOBAL.ud.recstat != 2)
		{
			GlobalMembersGLOBAL.music_select = (GlobalMembersGLOBAL.ud.volume_number *11) + GlobalMembersGLOBAL.ud.level_number;
			GlobalMembersSOUNDS.playmusic(ref GlobalMembersGLOBAL.music_fn[0, GlobalMembersGLOBAL.music_select, 0]);
		}

		if((g &MODE_GAME) || (g &MODE_EOL))
			GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
		else if(g &MODE_RESTART)
		{
			if(GlobalMembersGLOBAL.ud.recstat == 2)
				GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_DEMO;
			else
				GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
		}

		if((GlobalMembersGLOBAL.ud.recstat == 1) && (g &MODE_RESTART) != MODE_RESTART)
			GlobalMembersGAME.opendemowrite();

	#if VOLUMEONE
		if(GlobalMembersGLOBAL.ud.level_number == 0 && GlobalMembersGLOBAL.ud.recstat != 2)
			GlobalMembersGAME.FTA(40, ref GlobalMembersGLOBAL.ps[myconnectindex]);
	#endif

		for(i = connecthead;i>=0;i = connectpoint2[i])
			switch(sector[sprite[GlobalMembersGLOBAL.ps[i].i].sectnum].floorpicnum)
			{
				case HURTRAIL:
				case FLOORSLIME:
				case FLOORPLASMA:
					GlobalMembersPREMAP.resetweapons(i);
					GlobalMembersPREMAP.resetinventory(i);
					GlobalMembersGLOBAL.ps[i].gotweapon[PISTOL_WEAPON] = 0;
					GlobalMembersGLOBAL.ps[i].ammo_amount[PISTOL_WEAPON] = 0;
					GlobalMembersGLOBAL.ps[i].curr_weapon = KNEE_WEAPON;
					GlobalMembersGLOBAL.ps[i].kickback_pic = 0;
					break;
			}

		  //PREMAP.C - replace near the my's at the end of the file

		 GlobalMembersPREMAP.resetmys();

		 GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
		 GlobalMembersMENUES.palto(0, 0, 0, 0);

		 GlobalMembersPLAYER.setpal(ref GlobalMembersGLOBAL.ps[myconnectindex]);
		 flushperms();

		 GlobalMembersGLOBAL.everyothertime = 0;
		 GlobalMembersGLOBAL.global_random = 0;

		 GlobalMembersGLOBAL.ud.last_level = GlobalMembersGLOBAL.ud.level_number+1;

		 GlobalMembersPREMAP.clearfifo();

		 for(i = GlobalMembersGLOBAL.numinterpolations-1;i>=0;i--)
			 GlobalMembersGLOBAL.bakipos[i] = GlobalMembersGLOBAL.curipos[i];

		 GlobalMembersGAME.restorepalette = 1;

		 flushpackets();
		 GlobalMembersPREMAP.waitforeverybody();

		 GlobalMembersMENUES.palto(0, 0, 0, 0);
		 GlobalMembersPREMAP.vscrn();
		 clearview(0);
		 GlobalMembersGAME.drawbackground();

		 clearbufbyte(GlobalMembersGLOBAL.playerquitflag,MAXPLAYERS,0x01010101);
		 GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on = 0;

		 GlobalMembersPREMAP.clearfrags();

		 GlobalMembersPREMAP.resettimevars(); // Here we go


	}
}

/*
Duke Nukem V

Layout:

      Settings:
        Suburbs
          Duke inflitrating neighborhoods inf. by aliens
        Death Valley:
          Sorta like a western.  Bull-skulls halb buried in the sand
          Military compound:  Aliens take over nuke-missle silo, duke
            must destroy.
          Abondend Aircraft field
        Vegas:
          Blast anything bright!  Alien lights camoflauged.
          Alien Drug factory. The Blue Liquid
        Mountainal Cave:
          Interior cave battles.
        Jungle:
          Trees, canopee, animals, a mysterious hole in the earth
        Penetencury:
          Good use of spotlights:
      Inventory:
        Wood,
        Metal,
        Torch,
        Rope,
        Plastique,
        Cloth,
        Wiring,
        Glue,
        Cigars,
        Food,
        Duck Tape,
        Nails,
        Piping,
        Petrol,
        Uranium,
        Gold,
        Prism,
        Power Cell,

        Hand spikes (Limited usage, they become dull)
        Oxygent     (Oxygen mixed with stimulant)


      Player Skills:
        R-Left,R-Right,Foward,Back
        Strafe, Jump, Double Flip Jump for distance
        Help, Escape
        Fire/Use
        Use Menu

Programming:
     Images: Polys
     Actors:
       Multi-Object sections for change (head,arms,legs,torsoe,all change)
       Facial expressions.  Pal lookup per poly?

     struct imagetype
        {
            int *itable; // AngX,AngY,AngZ,Xoff,Yoff,Zoff;
            int *idata;
            struct imagetype *prev, *next;
        }

*/
