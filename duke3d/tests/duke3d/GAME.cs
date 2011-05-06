using System;

public static class GlobalMembersGAME
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




	#if VOLUMEONE
		#define VERSION
	#else
		#define VERSION
	#endif

	#define HEAD
	#if PLUTOPAK
	#define HEAD2_AlternateDefinition1
	#define HEAD2
	#else
	#define HEAD2_AlternateDefinition2
	#define HEAD2
	#endif
	#define HEADA
	#define HEAD2A

	#define IDFSIZE
	// #define IDFSIZE 9961476
	// #define IDFSIZE 16384
	#define IDFILENAME


	#define TIMERUPDATESIZ

	public static int cameradist = 0;
	public static int cameraclock = 0;
	public static sbyte eightytwofifty = 0;
	public static sbyte playerswhenstarted;
	public static sbyte qe;
	public static sbyte cp;

	public static int32 CommandSoundToggleOff = 0;
	public static int32 CommandMusicToggleOff = 0;

	public static string[] confilename = {"GAME.CON"};
	public static string boardfilename = "";
	public static string waterpal = new string(new char[768]);
	public static string slimepal = new string(new char[768]);
	public static string titlepal = new string(new char[768]);
	public static string drealms = new string(new char[768]);
	public static string endingpal = new string(new char[768]);
	public static string firstdemofile = "";

	#define patchstatusbar

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void __interrupt __far newint24(int errval, int ax, int bp, int si);

	public static int recfilep;
	public static int totalreccnt;
	public static sbyte debug_on = 0;
	public static sbyte actor_tog = 0;
	public static string rtsptr;
	public static sbyte memorycheckoveride=0;



	//extern sbyte syncstate;
	//extern int32 numlumps;

	public static FILE frecfilep = (FILE *)null;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void pitch_test();

	public static sbyte restorepalette;
	public static sbyte screencapt;
	public static sbyte nomorelogohack;
	public static int sendmessagecommand = -1;

	public static task TimerPtr=null;

	//extern int lastvisinc;

	public static void timerhandler()
	{
		totalclock++;
	}

	public static void inittimer()
	{
		TimerPtr = TS_ScheduleTask(GlobalMembersGAME.timerhandler,TICRATE, 1, null);
		TS_Dispatch();
	}

	public static void uninittimer()
	{
	   if (TimerPtr)
		  TS_Terminate(TimerPtr);
	   TimerPtr = null;
	   TS_Shutdown();
	}

	public static int gametext(int x, int y, ref string t, sbyte s, short dabits)
	{
		short ac;
		short newx;
		sbyte centre;
		string oldt;

		centre = (x == (320>>1));
		newx = 0;
		oldt = t;

		if (centre != 0)
		{
			while (t != null)
			{
				if(t == 32)
				{
					newx+=5;
					t = t.Substring(1);
					continue;
				}
				else
					ac = t - '!' + STARTALPHANUM;

				if(ac < STARTALPHANUM || ac > ENDALPHANUM)
					break;

				if(t >= '0' && t <= '9')
					newx += 8;
				else
					newx += tilesizx[ac];
				t = t.Substring(1);
			}

			t = oldt;
			x = (320>>1)-(newx>>1);
		}

		while (t != null)
		{
			if(t == 32)
			{
				x+=5;
				t = t.Substring(1);
				continue;
			}
			else
				ac = t - '!' + STARTALPHANUM;

			if(ac < STARTALPHANUM || ac > ENDALPHANUM)
				break;

			rotatesprite(x<<16,y<<16,65536,0,ac,s,0,dabits,0,0,xdim-1,ydim-1);

			if(t >= '0' && t <= '9')
				x += 8;
			else
				x += tilesizx[ac];

			t = t.Substring(1);
		}

		return (x);
	}

	public static int gametextpal(int x, int y, ref string t, sbyte s, sbyte p)
	{
		short ac;
		short newx;
		sbyte centre;
		string oldt;

		centre = (x == (320>>1));
		newx = 0;
		oldt = t;

		if (centre != 0)
		{
			while (t != null)
			{
				if(t == 32)
				{
					newx+=5;
					t = t.Substring(1);
					continue;
				}
				else
					ac = t - '!' + STARTALPHANUM;

				if(ac < STARTALPHANUM || ac > ENDALPHANUM)
					break;

				if(t >= '0' && t <= '9')
					newx += 8;
				else
					newx += tilesizx[ac];
				t = t.Substring(1);
			}

			t = oldt;
			x = (320>>1)-(newx>>1);
		}

		while (t != null)
		{
			if(t == 32)
			{
				x+=5;
				t = t.Substring(1);
				continue;
			}
			else
				ac = t - '!' + STARTALPHANUM;

			if(ac < STARTALPHANUM || ac > ENDALPHANUM)
				break;

			rotatesprite(x<<16,y<<16,65536,0,ac,s,p,2+8+16,0,0,xdim-1,ydim-1);
			if(t >= '0' && t <= '9')
				x += 8;
			else
				x += tilesizx[ac];

			t = t.Substring(1);
		}

		return (x);
	}

	public static int gametextpart(int x, int y, ref string t, sbyte s, short p)
	{
		short ac;
		short newx;
		short cnt;
		sbyte centre;
		string oldt;

		centre = (x == (320>>1));
		newx = 0;
		oldt = t;
		cnt = 0;

		if (centre != 0)
		{
			while (t != null)
			{
				if(cnt == p)
					break;

				if(t == 32)
				{
					newx+=5;
					t = t.Substring(1);
					continue;
				}
				else
					ac = t - '!' + STARTALPHANUM;

				if(ac < STARTALPHANUM || ac > ENDALPHANUM)
					break;

				newx += tilesizx[ac];
				t = t.Substring(1);
				cnt++;

			}

			t = oldt;
			x = (320>>1)-(newx>>1);
		}

		cnt = 0;
		while (t != null)
		{
			if(t == 32)
			{
				x+=5;
				t = t.Substring(1);
				continue;
			}
			else
				ac = t - '!' + STARTALPHANUM;

			if(ac < STARTALPHANUM || ac > ENDALPHANUM)
				break;

			if(cnt == p)
			{
				rotatesprite(x<<16,y<<16,65536,0,ac,s,1,2+8+16,0,0,xdim-1,ydim-1);
				break;
			}
			else
				rotatesprite(x<<16,y<<16,65536,0,ac,s,0,2+8+16,0,0,xdim-1,ydim-1);

			x += tilesizx[ac];

			t = t.Substring(1);
			cnt++;
		}

		return (x);
	}

	public static int minitext(int x, int y, ref string t, sbyte p, sbyte sb)
	{
		short ac;

		while (t != null)
		{
			t = char.ToUpper(t);
			if(t == 32)
			{
				x+=5;
				t = t.Substring(1);
				continue;
			}
			else
				ac = t - '!' + MINIFONT;

			rotatesprite(x<<16,y<<16,65536,0,ac,0,p,sb,0,0,xdim-1,ydim-1);
			x += 4; // tilesizx[ac]+1;

			t = t.Substring(1);
		}
		return (x);
	}

	public static int minitextshade(int x, int y, ref string t, sbyte s, sbyte p, sbyte sb)
	{
		short ac;

		while (t != null)
		{
			t = char.ToUpper(t);
			if(t == 32)
			{
				x+=5;
				t = t.Substring(1);
				continue;
			}
			else
				ac = t - '!' + MINIFONT;

			rotatesprite(x<<16,y<<16,65536,0,ac,s,p,sb,0,0,xdim-1,ydim-1);
			x += 4; // tilesizx[ac]+1;

			t = t.Substring(1);
		}
		return (x);
	}

	public static void gamenumber(int x,int y,int n,sbyte s)
	{
		string b = new string(new char[10]);
		ltoa(n,b,10);
		GlobalMembersGAME.gametext(x, y, ref b, s, 2+8+16);
	}


	public static string recbuf = new string(new char[80]);
	public static void allowtimetocorrecterrorswhenquitting()
	{
		 int i;
		 int j;
		 int oldtotalclock;

		 GlobalMembersGLOBAL.ready2send = 0;

		 for(j = 0;j<8;j++)
		 {
			  oldtotalclock = totalclock;

			  while (totalclock < oldtotalclock+TICSPERFRAME)
				  GlobalMembersGAME.getpackets();

			  if(KB_KeyPressed(sc_Escape))
				  return;

			  GlobalMembersGLOBAL.packbuf[0] = 127;
			  for(i = connecthead;i>=0;i = connectpoint2[i])
					if (i != myconnectindex)
						 sendpacket(i,GlobalMembersGLOBAL.packbuf,1);
		 }
	}

	#define MAXUSERQUOTES
	public static int quotebot;
	public static int quotebotgoal;
	public static short[] user_quote_time = new short[DefineConstants.MAXUSERQUOTES];
	public static sbyte[,] user_quote = new sbyte[DefineConstants.MAXUSERQUOTES, 128];
	// char typebuflen,typebuf[41];

	public static adduserquote(ref string daquote)
	{
		int i;

		for(i = DefineConstants.MAXUSERQUOTES-1;i>0;i--)
		{
			user_quote[i] = user_quote[i-1];
			user_quote_time[i] = user_quote_time[i-1];
		}
		user_quote[0] = daquote;
		user_quote_time[0] = 180;
		GlobalMembersGLOBAL.pub = NUMPAGES;
	}


	public static void getpackets()
	{
		int i;
		int j;
		int k;
		int l;
		FILE fp;
		short other;
		short packbufleng;
		input osyn;
		input nsyn;

		if(qe == 0 && KB_KeyPressed(sc_LeftControl) && KB_KeyPressed(sc_LeftAlt) && KB_KeyPressed(sc_Delete))
		{
			qe = 1;
			GlobalMembersGAME.gameexit("Quick Exit.");
		}

		if (numplayers < 2)
			return;
		while ((packbufleng = getpacket(other, GlobalMembersGLOBAL.packbuf)) > 0)
		{
			switch(GlobalMembersGLOBAL.packbuf[0])
			{
				case 125:
					cp = 0;
					break;

				case 126:
					GlobalMembersGLOBAL.multiflag = 2;
					GlobalMembersGLOBAL.multiwhat = 0;
					GlobalMembersGLOBAL.multiwho = other;
					GlobalMembersGLOBAL.multipos = GlobalMembersGLOBAL.packbuf[1];
					GlobalMembersMENUES.loadplayer(GlobalMembersGLOBAL.multipos);
					GlobalMembersGLOBAL.multiflag = 0;
					break;
				case 0: //[0] (receive master sync buffer)
					j = 1;

					if ((GlobalMembersGLOBAL.movefifoend[other]&(DefineConstants.TIMERUPDATESIZ-1)) == 0)
						for(i = connectpoint2[connecthead];i>=0;i = connectpoint2[i])
						{
							if (GlobalMembersGLOBAL.playerquitflag[i] == 0)
								continue;
							if (i == myconnectindex)
								GlobalMembersGLOBAL.otherminlag = (int)((sbyte)GlobalMembersGLOBAL.packbuf[j]);
							j++;
						}

					osyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[connecthead]-1)&(MOVEFIFOSIZ-1), 0];
					nsyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[connecthead])&(MOVEFIFOSIZ-1), 0];

					k = j;
					for(i = connecthead;i>=0;i = connectpoint2[i])
						j += GlobalMembersGLOBAL.playerquitflag[i];
					for(i = connecthead;i>=0;i = connectpoint2[i])
					{
						if (GlobalMembersGLOBAL.playerquitflag[i] == 0)
							continue;

						l = GlobalMembersGLOBAL.packbuf[k++];
						if (i == myconnectindex)
							{
								j += ((l &1)<<1)+(l &2)+((l &4)>>2)+((l &8)>>3)+((l &16)>>4)+((l &32)>>5)+((l &64)>>6)+((l &128)>>7);
								continue;
							}

						copybufbyte(osyn[i], nsyn[i], sizeof(input));
						if (l &1 != 0)
							nsyn[i].fvel = GlobalMembersGLOBAL.packbuf[j]+((short)GlobalMembersGLOBAL.packbuf[j+1]<<8), j += 2;
						if (l &2 != 0)
							nsyn[i].svel = GlobalMembersGLOBAL.packbuf[j]+((short)GlobalMembersGLOBAL.packbuf[j+1]<<8), j += 2;
						if (l &4 != 0)
							nsyn[i].avel = (sbyte)GlobalMembersGLOBAL.packbuf[j++];
						if (l &8 != 0)
							nsyn[i].bits = ((nsyn[i].bits &0xffffff00)|((int)GlobalMembersGLOBAL.packbuf[j++]));
						if (l &16 != 0)
							nsyn[i].bits = ((nsyn[i].bits &0xffff00ff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<8);
						if (l &32 != 0)
							nsyn[i].bits = ((nsyn[i].bits &0xff00ffff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<16);
						if (l &64 != 0)
							nsyn[i].bits = ((nsyn[i].bits &0x00ffffff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<24);
						if (l &128 != 0)
							nsyn[i].horz = (sbyte)GlobalMembersGLOBAL.packbuf[j++];

						if (nsyn[i].bits&(1<<26))
							GlobalMembersGLOBAL.playerquitflag = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.playerquitflag, i, 0);
						GlobalMembersGLOBAL.movefifoend[i]++;
					}

					while (j != packbufleng)
					{
						for(i = connecthead;i>=0;i = connectpoint2[i])
							if(i != myconnectindex)
						{
							GlobalMembersGLOBAL.syncval[i, GlobalMembersGLOBAL.syncvalhead[i]&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.packbuf[j];
							GlobalMembersGLOBAL.syncvalhead[i]++;
						}
						j++;
					}

					for(i = connecthead;i>=0;i = connectpoint2[i])
						if (i != myconnectindex)
							for(j = 1;j<GlobalMembersGLOBAL.movesperpacket;j++)
							{
								copybufbyte(nsyn[i], GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoend[i]&(MOVEFIFOSIZ-1), i], sizeof(input));
								GlobalMembersGLOBAL.movefifoend[i]++;
							}

					 GlobalMembersGLOBAL.movefifosendplc += GlobalMembersGLOBAL.movesperpacket;

					break;
				case 1: //[1] (receive slave sync buffer)
					j = 2;
					k = GlobalMembersGLOBAL.packbuf[1];

					osyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[other]-1)&(MOVEFIFOSIZ-1), 0];
					nsyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[other])&(MOVEFIFOSIZ-1), 0];

					copybufbyte(osyn[other], nsyn[other], sizeof(input));
					if (k &1 != 0)
						nsyn[other].fvel = GlobalMembersGLOBAL.packbuf[j]+((short)GlobalMembersGLOBAL.packbuf[j+1]<<8), j += 2;
					if (k &2 != 0)
						nsyn[other].svel = GlobalMembersGLOBAL.packbuf[j]+((short)GlobalMembersGLOBAL.packbuf[j+1]<<8), j += 2;
					if (k &4 != 0)
						nsyn[other].avel = (sbyte)GlobalMembersGLOBAL.packbuf[j++];
					if (k &8 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0xffffff00)|((int)GlobalMembersGLOBAL.packbuf[j++]));
					if (k &16 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0xffff00ff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<8);
					if (k &32 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0xff00ffff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<16);
					if (k &64 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0x00ffffff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<24);
					if (k &128 != 0)
						nsyn[other].horz = (sbyte)GlobalMembersGLOBAL.packbuf[j++];
					GlobalMembersGLOBAL.movefifoend[other]++;

					while (j != packbufleng)
					{
						GlobalMembersGLOBAL.syncval[other, GlobalMembersGLOBAL.syncvalhead[other]&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.packbuf[j++];
						GlobalMembersGLOBAL.syncvalhead[other]++;
					}

					for(i = 1;i<GlobalMembersGLOBAL.movesperpacket;i++)
					{
						copybufbyte(nsyn[other], GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoend[other]&(MOVEFIFOSIZ-1), other], sizeof(input));
						GlobalMembersGLOBAL.movefifoend[other]++;
					}

					break;

				case 4:
					recbuf = GlobalMembersGLOBAL.packbuf+1;
					recbuf[packbufleng-1] = 0;

					GlobalMembersGAME.adduserquote(ref recbuf);
					GlobalMembersSOUNDS.sound(EXITMENUSOUND);

					GlobalMembersGLOBAL.pus = NUMPAGES;
					GlobalMembersGLOBAL.pub = NUMPAGES;

					break;

				case 5:
					GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.packbuf[1];
					GlobalMembersGLOBAL.ud.m_volume_number = GlobalMembersGLOBAL.ud.volume_number = GlobalMembersGLOBAL.packbuf[2];
					GlobalMembersGLOBAL.ud.m_player_skill = GlobalMembersGLOBAL.ud.player_skill = GlobalMembersGLOBAL.packbuf[3];
					GlobalMembersGLOBAL.ud.m_monsters_off = GlobalMembersGLOBAL.ud.monsters_off = GlobalMembersGLOBAL.packbuf[4];
					GlobalMembersGLOBAL.ud.m_respawn_monsters = GlobalMembersGLOBAL.ud.respawn_monsters = GlobalMembersGLOBAL.packbuf[5];
					GlobalMembersGLOBAL.ud.m_respawn_items = GlobalMembersGLOBAL.ud.respawn_items = GlobalMembersGLOBAL.packbuf[6];
					GlobalMembersGLOBAL.ud.m_respawn_inventory = GlobalMembersGLOBAL.ud.respawn_inventory = GlobalMembersGLOBAL.packbuf[7];
					GlobalMembersGLOBAL.ud.m_coop = GlobalMembersGLOBAL.packbuf[8];
					GlobalMembersGLOBAL.ud.m_marker = GlobalMembersGLOBAL.ud.marker = GlobalMembersGLOBAL.packbuf[9];
					GlobalMembersGLOBAL.ud.m_ffire = GlobalMembersGLOBAL.ud.ffire = GlobalMembersGLOBAL.packbuf[10];

					for(i = connecthead;i>=0;i = connectpoint2[i])
					{
						GlobalMembersPREMAP.resetweapons(i);
						GlobalMembersPREMAP.resetinventory(i);
					}

					GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.volume_number, GlobalMembersGLOBAL.ud.level_number, GlobalMembersGLOBAL.ud.player_skill);
					GlobalMembersGLOBAL.ud.coop = GlobalMembersGLOBAL.ud.m_coop;

					GlobalMembersPREMAP.enterlevel(MODE_GAME);

					break;
				case 6:
					if (GlobalMembersGLOBAL.packbuf[1] != BYTEVERSION)
						GlobalMembersGAME.gameexit("\nYou cannot play Duke with different versions.");
					for (i = 2;GlobalMembersGLOBAL.packbuf[i];i++)
						GlobalMembersGLOBAL.ud.user_name[other][i-2] = GlobalMembersGLOBAL.packbuf[i];
					GlobalMembersGLOBAL.ud.user_name[other][i-2] = 0;
					break;
				case 9:
					for (i = 1;i<packbufleng;i++)
						GlobalMembersGLOBAL.ud.wchoice[other][i-1] = GlobalMembersGLOBAL.packbuf[i];
					break;
				case 7:

					if(GlobalMembersRTS.numlumps == 0)
						break;

					if (GlobalMembersCONFIG.SoundToggle == 0 || GlobalMembersGLOBAL.ud.lockout == 1 || GlobalMembersCONFIG.FXDevice == NumSoundCards)
						break;
					rtsptr = (string)GlobalMembersRTS.RTS_GetSound(GlobalMembersGLOBAL.packbuf[1]-1);
					if (*rtsptr == 'C')
						FX_PlayVOC3D(rtsptr,0,0,0,255,-GlobalMembersGLOBAL.packbuf[1]);
					else
						FX_PlayWAV3D(rtsptr,0,0,0,255,-GlobalMembersGLOBAL.packbuf[1]);
					GlobalMembersGLOBAL.rtsplaying = 7;
					break;
				case 8:
					GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.packbuf[1];
					GlobalMembersGLOBAL.ud.m_volume_number = GlobalMembersGLOBAL.ud.volume_number = GlobalMembersGLOBAL.packbuf[2];
					GlobalMembersGLOBAL.ud.m_player_skill = GlobalMembersGLOBAL.ud.player_skill = GlobalMembersGLOBAL.packbuf[3];
					GlobalMembersGLOBAL.ud.m_monsters_off = GlobalMembersGLOBAL.ud.monsters_off = GlobalMembersGLOBAL.packbuf[4];
					GlobalMembersGLOBAL.ud.m_respawn_monsters = GlobalMembersGLOBAL.ud.respawn_monsters = GlobalMembersGLOBAL.packbuf[5];
					GlobalMembersGLOBAL.ud.m_respawn_items = GlobalMembersGLOBAL.ud.respawn_items = GlobalMembersGLOBAL.packbuf[6];
					GlobalMembersGLOBAL.ud.m_respawn_inventory = GlobalMembersGLOBAL.ud.respawn_inventory = GlobalMembersGLOBAL.packbuf[7];
					GlobalMembersGLOBAL.ud.m_coop = GlobalMembersGLOBAL.ud.coop = GlobalMembersGLOBAL.packbuf[8];
					GlobalMembersGLOBAL.ud.m_marker = GlobalMembersGLOBAL.ud.marker = GlobalMembersGLOBAL.packbuf[9];
					GlobalMembersGLOBAL.ud.m_ffire = GlobalMembersGLOBAL.ud.ffire = GlobalMembersGLOBAL.packbuf[10];

					copybufbyte(GlobalMembersGLOBAL.packbuf+10,boardfilename,packbufleng-11);
					boardfilename[packbufleng-11] = 0;

					for(i = connecthead;i>=0;i = connectpoint2[i])
					{
						GlobalMembersPREMAP.resetweapons(i);
						GlobalMembersPREMAP.resetinventory(i);
					}

					GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.volume_number, GlobalMembersGLOBAL.ud.level_number, GlobalMembersGLOBAL.ud.player_skill);
					GlobalMembersPREMAP.enterlevel(MODE_GAME);
					break;

				case 16:
					GlobalMembersGLOBAL.movefifoend[other] = GlobalMembersGLOBAL.movefifoplc = GlobalMembersGLOBAL.movefifosendplc = GlobalMembersGLOBAL.fakemovefifoplc = 0;
					GlobalMembersGLOBAL.syncvalhead[other] = GlobalMembersGLOBAL.syncvaltottail = 0;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case 17:
					j = 1;

					if ((GlobalMembersGLOBAL.movefifoend[other]&(DefineConstants.TIMERUPDATESIZ-1)) == 0)
						if (other == connecthead)
							for(i = connectpoint2[connecthead];i>=0;i = connectpoint2[i])
							{
								if (i == myconnectindex)
									GlobalMembersGLOBAL.otherminlag = (int)((sbyte)GlobalMembersGLOBAL.packbuf[j]);
								j++;
							}

					osyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[other]-1)&(MOVEFIFOSIZ-1), 0];
					nsyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[other])&(MOVEFIFOSIZ-1), 0];

					copybufbyte(osyn[other], nsyn[other], sizeof(input));
					k = GlobalMembersGLOBAL.packbuf[j++];
					if (k &1 != 0)
						nsyn[other].fvel = GlobalMembersGLOBAL.packbuf[j]+((short)GlobalMembersGLOBAL.packbuf[j+1]<<8), j += 2;
					if (k &2 != 0)
						nsyn[other].svel = GlobalMembersGLOBAL.packbuf[j]+((short)GlobalMembersGLOBAL.packbuf[j+1]<<8), j += 2;
					if (k &4 != 0)
						nsyn[other].avel = (sbyte)GlobalMembersGLOBAL.packbuf[j++];
					if (k &8 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0xffffff00)|((int)GlobalMembersGLOBAL.packbuf[j++]));
					if (k &16 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0xffff00ff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<8);
					if (k &32 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0xff00ffff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<16);
					if (k &64 != 0)
						nsyn[other].bits = ((nsyn[other].bits &0x00ffffff)|((int)GlobalMembersGLOBAL.packbuf[j++])<<24);
					if (k &128 != 0)
						nsyn[other].horz = (sbyte)GlobalMembersGLOBAL.packbuf[j++];
					GlobalMembersGLOBAL.movefifoend[other]++;

					for(i = 1;i<GlobalMembersGLOBAL.movesperpacket;i++)
					{
						copybufbyte(nsyn[other], GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoend[other]&(MOVEFIFOSIZ-1), other], sizeof(input));
						GlobalMembersGLOBAL.movefifoend[other]++;
					}

					if (j > packbufleng)
						Console.Write("INVALID GAME PACKET!!! ({0:D} too many bytes)\n",j-packbufleng);

					while (j != packbufleng)
					{
						GlobalMembersGLOBAL.syncval[other, GlobalMembersGLOBAL.syncvalhead[other]&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.packbuf[j++];
						GlobalMembersGLOBAL.syncvalhead[other]++;
					}

					break;
				case 127:
					break;

				case 250:
					GlobalMembersGLOBAL.playerreadyflag[other]++;
					break;
				case 255:
					GlobalMembersGAME.gameexit(" ");
					break;
			}
		}
	}

	public static void faketimerhandler()
	{
		int i;
		int j;
		int k;
		int l;
	//    short who;
		input osyn;
		input nsyn;

		if(qe == 0 && KB_KeyPressed(sc_LeftControl) && KB_KeyPressed(sc_LeftAlt) && KB_KeyPressed(sc_Delete))
		{
			qe = 1;
			GlobalMembersGAME.gameexit("Quick Exit.");
		}

		if ((totalclock < GlobalMembersGLOBAL.ototalclock+TICSPERFRAME) || (GlobalMembersGLOBAL.ready2send == 0))
			return;
		GlobalMembersGLOBAL.ototalclock += TICSPERFRAME;

		GlobalMembersGAME.getpackets();
		if (getoutputcirclesize() >= 16)
			return;

		for(i = connecthead;i>=0;i = connectpoint2[i])
			if (i != myconnectindex)
				if (GlobalMembersGLOBAL.movefifoend[i] < GlobalMembersGLOBAL.movefifoend[myconnectindex]-200)
					return;

		 GlobalMembersPLAYER.getinput(myconnectindex);

		 GlobalMembersGLOBAL.avgfvel += GlobalMembersGLOBAL.loc.fvel;
		 GlobalMembersGLOBAL.avgsvel += GlobalMembersGLOBAL.loc.svel;
		 GlobalMembersGLOBAL.avgavel += GlobalMembersGLOBAL.loc.avel;
		 GlobalMembersGLOBAL.avghorz += GlobalMembersGLOBAL.loc.horz;
		 GlobalMembersGLOBAL.avgbits |= GlobalMembersGLOBAL.loc.bits;
		 if (GlobalMembersGLOBAL.movefifoend[myconnectindex]&(GlobalMembersGLOBAL.movesperpacket-1))
		 {
			  copybufbyte(GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[myconnectindex]-1)&(MOVEFIFOSIZ-1), myconnectindex], GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoend[myconnectindex]&(MOVEFIFOSIZ-1), myconnectindex], sizeof(input));
			  GlobalMembersGLOBAL.movefifoend[myconnectindex]++;
			  return;
		 }
		 nsyn = GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoend[myconnectindex]&(MOVEFIFOSIZ-1), myconnectindex];
		 nsyn[0].fvel = GlobalMembersGLOBAL.avgfvel/GlobalMembersGLOBAL.movesperpacket;
		 nsyn[0].svel = GlobalMembersGLOBAL.avgsvel/GlobalMembersGLOBAL.movesperpacket;
		 nsyn[0].avel = GlobalMembersGLOBAL.avgavel/GlobalMembersGLOBAL.movesperpacket;
		 nsyn[0].horz = GlobalMembersGLOBAL.avghorz/GlobalMembersGLOBAL.movesperpacket;
		 nsyn[0].bits = GlobalMembersGLOBAL.avgbits;
		 GlobalMembersGLOBAL.avgfvel = GlobalMembersGLOBAL.avgsvel = GlobalMembersGLOBAL.avgavel = GlobalMembersGLOBAL.avghorz = GlobalMembersGLOBAL.avgbits = 0;
		 GlobalMembersGLOBAL.movefifoend[myconnectindex]++;

		 if (numplayers < 2)
		 {
			  if (GlobalMembersGLOBAL.ud.multimode > 1)
				  for(i = connecthead;i>=0;i = connectpoint2[i])
				  if(i != myconnectindex)
				  {
					  //clearbufbyte(&inputfifo[movefifoend[i]&(MOVEFIFOSIZ-1)][i],sizeof(input),0L);
					  if(GlobalMembersGLOBAL.ud.playerai)
						  GlobalMembersPLAYER.computergetinput(i, ref GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoend[i]&(MOVEFIFOSIZ-1), i]);
					  GlobalMembersGLOBAL.movefifoend[i]++;
				  }
			  return;
		 }

		for(i = connecthead;i>=0;i = connectpoint2[i])
			if (i != myconnectindex)
			{
				k = (GlobalMembersGLOBAL.movefifoend[myconnectindex]-1)-GlobalMembersGLOBAL.movefifoend[i];
				GlobalMembersGLOBAL.myminlag[i] = min(GlobalMembersGLOBAL.myminlag[i],k);
				GlobalMembersGLOBAL.mymaxlag = max(GlobalMembersGLOBAL.mymaxlag,k);
			}

		if (((GlobalMembersGLOBAL.movefifoend[myconnectindex]-1)&(DefineConstants.TIMERUPDATESIZ-1)) == 0)
		{
			i = GlobalMembersGLOBAL.mymaxlag-GlobalMembersGLOBAL.bufferjitter;
			GlobalMembersGLOBAL.mymaxlag = 0;
			if (i > 0)
				GlobalMembersGLOBAL.bufferjitter += ((3+i)>>2);
			else if (i < 0)
				GlobalMembersGLOBAL.bufferjitter -= ((1-i)>>2);
		}

		if (GlobalMembersGLOBAL.networkmode == 1)
		{
			GlobalMembersGLOBAL.packbuf[0] = 17;
			if ((GlobalMembersGLOBAL.movefifoend[myconnectindex]-1) == 0)
				GlobalMembersGLOBAL.packbuf[0] = 16;
			j = 1;

				//Fix timers and buffer/jitter value
			if (((GlobalMembersGLOBAL.movefifoend[myconnectindex]-1)&(DefineConstants.TIMERUPDATESIZ-1)) == 0)
			{
				if (myconnectindex != connecthead)
				{
					i = GlobalMembersGLOBAL.myminlag[connecthead]-GlobalMembersGLOBAL.otherminlag;
					if (klabs(i) > 8)
						i >>= 1;
					else if (klabs(i) > 2)
						i = ksgn(i);
					else
						i = 0;

					totalclock -= TICSPERFRAME *i;
					GlobalMembersGLOBAL.myminlag[connecthead] -= i;
					GlobalMembersGLOBAL.otherminlag += i;
				}

				if (myconnectindex == connecthead)
					for(i = connectpoint2[connecthead];i>=0;i = connectpoint2[i])
						GlobalMembersGLOBAL.packbuf[j++] = min(max(GlobalMembersGLOBAL.myminlag[i],-128),127);

				for(i = connecthead;i>=0;i = connectpoint2[i])
					GlobalMembersGLOBAL.myminlag[i] = 0x7fffffff;
			}

			osyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[myconnectindex]-2)&(MOVEFIFOSIZ-1), myconnectindex];
			nsyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[myconnectindex]-1)&(MOVEFIFOSIZ-1), myconnectindex];

			k = j;
			GlobalMembersGLOBAL.packbuf[j++] = 0;

			if (nsyn[0].fvel != osyn[0].fvel)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].fvel;
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)(nsyn[0].fvel>>8);
				GlobalMembersGLOBAL.packbuf[k] |= 1;
			}
			if (nsyn[0].svel != osyn[0].svel)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].svel;
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)(nsyn[0].svel>>8);
				GlobalMembersGLOBAL.packbuf[k] |= 2;
			}
			if (nsyn[0].avel != osyn[0].avel)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].avel;
				GlobalMembersGLOBAL.packbuf[k] |= 4;
			}
			if ((nsyn[0].bits ^osyn[0].bits)&0x000000ff)
				GlobalMembersGLOBAL.packbuf[j++] = (nsyn[0].bits &255), GlobalMembersGLOBAL.packbuf[k] |= 8;
			if ((nsyn[0].bits ^osyn[0].bits)&0x0000ff00)
				GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[0].bits>>8)&255), GlobalMembersGLOBAL.packbuf[k] |= 16;
			if ((nsyn[0].bits ^osyn[0].bits)&0x00ff0000)
				GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[0].bits>>16)&255), GlobalMembersGLOBAL.packbuf[k] |= 32;
			if ((nsyn[0].bits ^osyn[0].bits)&0xff000000)
				GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[0].bits>>24)&255), GlobalMembersGLOBAL.packbuf[k] |= 64;
			if (nsyn[0].horz != osyn[0].horz)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].horz;
				GlobalMembersGLOBAL.packbuf[k] |= 128;
			}

			while (GlobalMembersGLOBAL.syncvalhead[myconnectindex] != GlobalMembersGLOBAL.syncvaltail)
			{
				GlobalMembersGLOBAL.packbuf[j++] = GlobalMembersGLOBAL.syncval[myconnectindex, GlobalMembersGLOBAL.syncvaltail&(MOVEFIFOSIZ-1)];
				GlobalMembersGLOBAL.syncvaltail++;
			}

			for(i = connecthead;i>=0;i = connectpoint2[i])
				if (i != myconnectindex)
					sendpacket(i,GlobalMembersGLOBAL.packbuf,j);

			return;
		}
		if (myconnectindex != connecthead) //Slave
		{
				//Fix timers and buffer/jitter value
			if (((GlobalMembersGLOBAL.movefifoend[myconnectindex]-1)&(DefineConstants.TIMERUPDATESIZ-1)) == 0)
			{
				i = GlobalMembersGLOBAL.myminlag[connecthead]-GlobalMembersGLOBAL.otherminlag;
				if (klabs(i) > 8)
					i >>= 1;
				else if (klabs(i) > 2)
					i = ksgn(i);
				else
					i = 0;

				totalclock -= TICSPERFRAME *i;
				GlobalMembersGLOBAL.myminlag[connecthead] -= i;
				GlobalMembersGLOBAL.otherminlag += i;

				for(i = connecthead;i>=0;i = connectpoint2[i])
					GlobalMembersGLOBAL.myminlag[i] = 0x7fffffff;
			}

			GlobalMembersGLOBAL.packbuf[0] = 1;
			GlobalMembersGLOBAL.packbuf[1] = 0;
			j = 2;

			osyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[myconnectindex]-2)&(MOVEFIFOSIZ-1), myconnectindex];
			nsyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifoend[myconnectindex]-1)&(MOVEFIFOSIZ-1), myconnectindex];

			if (nsyn[0].fvel != osyn[0].fvel)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].fvel;
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)(nsyn[0].fvel>>8);
				GlobalMembersGLOBAL.packbuf[1] |= 1;
			}
			if (nsyn[0].svel != osyn[0].svel)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].svel;
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)(nsyn[0].svel>>8);
				GlobalMembersGLOBAL.packbuf[1] |= 2;
			}
			if (nsyn[0].avel != osyn[0].avel)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].avel;
				GlobalMembersGLOBAL.packbuf[1] |= 4;
			}
			if ((nsyn[0].bits ^osyn[0].bits)&0x000000ff)
				GlobalMembersGLOBAL.packbuf[j++] = (nsyn[0].bits &255), GlobalMembersGLOBAL.packbuf[1] |= 8;
			if ((nsyn[0].bits ^osyn[0].bits)&0x0000ff00)
				GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[0].bits>>8)&255), GlobalMembersGLOBAL.packbuf[1] |= 16;
			if ((nsyn[0].bits ^osyn[0].bits)&0x00ff0000)
				GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[0].bits>>16)&255), GlobalMembersGLOBAL.packbuf[1] |= 32;
			if ((nsyn[0].bits ^osyn[0].bits)&0xff000000)
				GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[0].bits>>24)&255), GlobalMembersGLOBAL.packbuf[1] |= 64;
			if (nsyn[0].horz != osyn[0].horz)
			{
				GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[0].horz;
				GlobalMembersGLOBAL.packbuf[1] |= 128;
			}

			while (GlobalMembersGLOBAL.syncvalhead[myconnectindex] != GlobalMembersGLOBAL.syncvaltail)
			{
				GlobalMembersGLOBAL.packbuf[j++] = GlobalMembersGLOBAL.syncval[myconnectindex, GlobalMembersGLOBAL.syncvaltail&(MOVEFIFOSIZ-1)];
				GlobalMembersGLOBAL.syncvaltail++;
			}

			sendpacket(connecthead,GlobalMembersGLOBAL.packbuf,j);
			return;
		}

			//This allows allow packet-resends
		for(i = connecthead;i>=0;i = connectpoint2[i])
			if (GlobalMembersGLOBAL.movefifoend[i] <= GlobalMembersGLOBAL.movefifosendplc)
			{
				GlobalMembersGLOBAL.packbuf[0] = 127;
				for(i = connectpoint2[connecthead];i>=0;i = connectpoint2[i])
				   sendpacket(i,GlobalMembersGLOBAL.packbuf,1);
				return;
			}

		while (true) //Master
		{
			for(i = connecthead;i>=0;i = connectpoint2[i])
				if (GlobalMembersGLOBAL.playerquitflag[i] && (GlobalMembersGLOBAL.movefifoend[i] <= GlobalMembersGLOBAL.movefifosendplc))
					return;

			osyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifosendplc-1)&(MOVEFIFOSIZ-1), 0];
			nsyn = (input)GlobalMembersGLOBAL.inputfifo[(GlobalMembersGLOBAL.movefifosendplc)&(MOVEFIFOSIZ-1), 0];

				//MASTER -> SLAVE packet
			GlobalMembersGLOBAL.packbuf[0] = 0;
			j = 1;

				//Fix timers and buffer/jitter value
			if ((GlobalMembersGLOBAL.movefifosendplc&(DefineConstants.TIMERUPDATESIZ-1)) == 0)
			{
				for(i = connectpoint2[connecthead];i>=0;i = connectpoint2[i])
				   if (GlobalMembersGLOBAL.playerquitflag[i])
					GlobalMembersGLOBAL.packbuf[j++] = min(max(GlobalMembersGLOBAL.myminlag[i],-128),127);

				for(i = connecthead;i>=0;i = connectpoint2[i])
					GlobalMembersGLOBAL.myminlag[i] = 0x7fffffff;
			}

			k = j;
			for(i = connecthead;i>=0;i = connectpoint2[i])
			   j += GlobalMembersGLOBAL.playerquitflag[i];
			for(i = connecthead;i>=0;i = connectpoint2[i])
			{
				if (GlobalMembersGLOBAL.playerquitflag[i] == 0)
					continue;

				GlobalMembersGLOBAL.packbuf[k] = 0;
				if (nsyn[i].fvel != osyn[i].fvel)
				{
					GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[i].fvel;
					GlobalMembersGLOBAL.packbuf[j++] = (sbyte)(nsyn[i].fvel>>8);
					GlobalMembersGLOBAL.packbuf[k] |= 1;
				}
				if (nsyn[i].svel != osyn[i].svel)
				{
					GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[i].svel;
					GlobalMembersGLOBAL.packbuf[j++] = (sbyte)(nsyn[i].svel>>8);
					GlobalMembersGLOBAL.packbuf[k] |= 2;
				}
				if (nsyn[i].avel != osyn[i].avel)
				{
					GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[i].avel;
					GlobalMembersGLOBAL.packbuf[k] |= 4;
				}
				if ((nsyn[i].bits ^osyn[i].bits)&0x000000ff)
					GlobalMembersGLOBAL.packbuf[j++] = (nsyn[i].bits &255), GlobalMembersGLOBAL.packbuf[k] |= 8;
				if ((nsyn[i].bits ^osyn[i].bits)&0x0000ff00)
					GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[i].bits>>8)&255), GlobalMembersGLOBAL.packbuf[k] |= 16;
				if ((nsyn[i].bits ^osyn[i].bits)&0x00ff0000)
					GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[i].bits>>16)&255), GlobalMembersGLOBAL.packbuf[k] |= 32;
				if ((nsyn[i].bits ^osyn[i].bits)&0xff000000)
					GlobalMembersGLOBAL.packbuf[j++] = ((nsyn[i].bits>>24)&255), GlobalMembersGLOBAL.packbuf[k] |= 64;
				if (nsyn[i].horz != osyn[i].horz)
				{
					GlobalMembersGLOBAL.packbuf[j++] = (sbyte)nsyn[i].horz;
					GlobalMembersGLOBAL.packbuf[k] |= 128;
				}
				k++;
			}

			while (GlobalMembersGLOBAL.syncvalhead[myconnectindex] != GlobalMembersGLOBAL.syncvaltail)
			{
				GlobalMembersGLOBAL.packbuf[j++] = GlobalMembersGLOBAL.syncval[myconnectindex, GlobalMembersGLOBAL.syncvaltail&(MOVEFIFOSIZ-1)];
				GlobalMembersGLOBAL.syncvaltail++;
			}

			for(i = connectpoint2[connecthead];i>=0;i = connectpoint2[i])
				if (GlobalMembersGLOBAL.playerquitflag[i])
				{
					 sendpacket(i,GlobalMembersGLOBAL.packbuf,j);
					 if (nsyn[i].bits&(1<<26))
						GlobalMembersGLOBAL.playerquitflag = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.playerquitflag, i, 0);
				}

			GlobalMembersGLOBAL.movefifosendplc += GlobalMembersGLOBAL.movesperpacket;
		}
	}

	//extern int cacnum;
	//extern cactype cac[];

	public static void caches()
	{
		 short i;
		 short k;

		 k = 0;
		 for(i = 0;i<cacnum;i++)
			  if ((*cac[i].lock) >= 200)
			  {
					GlobalMembersGLOBAL.tempbuf = string.Format("Locked- {0:D}: Leng:{1:D}, Lock:{2:D}", i, cac[i].leng, *cac[i].lock);
					printext256(0,k,31,-1,GlobalMembersGLOBAL.tempbuf,1);
					k += 6;
			  }

		 k += 6;

		 for(i = 1;i<11;i++)
			  if (GlobalMembersRTS.lumplockbyte[i] >= 200)
			  {
					GlobalMembersGLOBAL.tempbuf = string.Format("RTS Locked {0:D}:", i);
					printext256(0,k,31,-1,GlobalMembersGLOBAL.tempbuf,1);
					k += 6;
			  }


	}



	public static void checksync()
	{
		  int i;
		  int k;

		  for(i = connecthead;i>=0;i = connectpoint2[i])
				if (GlobalMembersGLOBAL.syncvalhead[i] == GlobalMembersGLOBAL.syncvaltottail)
					break;
		  if (i < 0)
		  {
				 GlobalMembersGLOBAL.syncstat = 0;
				 do
				 {
						 for(i = connectpoint2[connecthead];i>=0;i = connectpoint2[i])
								if (GlobalMembersGLOBAL.syncval[i, GlobalMembersGLOBAL.syncvaltottail&(MOVEFIFOSIZ-1)] != GlobalMembersGLOBAL.syncval[connecthead, GlobalMembersGLOBAL.syncvaltottail&(MOVEFIFOSIZ-1)])
									 GlobalMembersGLOBAL.syncstat = 1;
						 GlobalMembersGLOBAL.syncvaltottail++;
						 for(i = connecthead;i>=0;i = connectpoint2[i])
								if (GlobalMembersGLOBAL.syncvalhead[i] == GlobalMembersGLOBAL.syncvaltottail)
									break;
				 } while (i < 0);
		  }
		  if (connectpoint2[connecthead] < 0)
			  GlobalMembersGLOBAL.syncstat = 0;

		  if (GlobalMembersGLOBAL.syncstat != 0)
		  {
			  printext256(4,130,31,0,"Out Of Sync - Please restart game",0);
			  printext256(4,138,31,0,"RUN DN3DHELP.EXE for information.",0);
		  }
		  if (syncstate)
		  {
			  printext256(4,160,31,0,"Missed Network packet!",0);
			  printext256(4,138,31,0,"RUN DN3DHELP.EXE for information.",0);
		  }
	}


	public static void check_fta_sounds(short i)
	{
		if(sprite[i].extra > 0)
			switch(PN)
		{
			case LIZTROOPONTOILET:
			case LIZTROOPJUSTSIT:
			case LIZTROOPSHOOT:
			case LIZTROOPJETPACK:
			case LIZTROOPDUCKING:
			case LIZTROOPRUNNING:
			case LIZTROOP:
				GlobalMembersSOUNDS.spritesound(PRED_RECOG, i);
				break;
			case LIZMAN:
			case LIZMANSPITTING:
			case LIZMANFEEDING:
			case LIZMANJUMP:
				GlobalMembersSOUNDS.spritesound(CAPT_RECOG, i);
				break;
			case PIGCOP:
			case PIGCOPDIVE:
				GlobalMembersSOUNDS.spritesound(PIG_RECOG, i);
				break;
			case RECON:
				GlobalMembersSOUNDS.spritesound(RECO_RECOG, i);
				break;
			case DRONE:
				GlobalMembersSOUNDS.spritesound(DRON_RECOG, i);
				break;
			case COMMANDER:
			case COMMANDERSTAYPUT:
				GlobalMembersSOUNDS.spritesound(COMM_RECOG, i);
				break;
			case ORGANTIC:
				GlobalMembersSOUNDS.spritesound(TURR_RECOG, i);
				break;
			case OCTABRAIN:
			case OCTABRAINSTAYPUT:
				GlobalMembersSOUNDS.spritesound(OCTA_RECOG, i);
				break;
			case BOSS1:
				GlobalMembersSOUNDS.sound(BOS1_RECOG);
				break;
			case BOSS2:
				if(sprite[i].pal == 1)
					GlobalMembersSOUNDS.sound(BOS2_RECOG);
				else
					GlobalMembersSOUNDS.sound(WHIPYOURASS);
				break;
			case BOSS3:
				if(sprite[i].pal == 1)
					GlobalMembersSOUNDS.sound(BOS3_RECOG);
				else
					GlobalMembersSOUNDS.sound(RIPHEADNECK);
				break;
			case BOSS4:
			case BOSS4STAYPUT:
				if(sprite[i].pal == 1)
					GlobalMembersSOUNDS.sound(BOS4_RECOG);
				GlobalMembersSOUNDS.sound(BOSS4_FIRSTSEE);
				break;
			case GREENSLIME:
				GlobalMembersSOUNDS.spritesound(SLIM_RECOG, i);
				break;
		}
	}

	public static short inventory(ref spritetype s)
	{
		switch(s.picnum)
		{
			case FIRSTAID:
			case STEROIDS:
			case HEATSENSOR:
			case BOOTS:
			case JETPACK:
			case HOLODUKE:
			case AIRTANK:
				return 1;
		}
		return 0;
	}


	public static short badguy(ref spritetype s)
	{

		switch(s.picnum)
		{
				case SHARK:
				case RECON:
				case DRONE:
				case LIZTROOPONTOILET:
				case LIZTROOPJUSTSIT:
				case LIZTROOPSTAYPUT:
				case LIZTROOPSHOOT:
				case LIZTROOPJETPACK:
				case LIZTROOPDUCKING:
				case LIZTROOPRUNNING:
				case LIZTROOP:
				case OCTABRAIN:
				case COMMANDER:
				case COMMANDERSTAYPUT:
				case PIGCOP:
				case EGG:
				case PIGCOPSTAYPUT:
				case PIGCOPDIVE:
				case LIZMAN:
				case LIZMANSPITTING:
				case LIZMANFEEDING:
				case LIZMANJUMP:
				case ORGANTIC:
				case BOSS1:
				case BOSS2:
				case BOSS3:
				case BOSS4:
				case GREENSLIME:
				case GREENSLIME+1:
				case GREENSLIME+2:
				case GREENSLIME+3:
				case GREENSLIME+4:
				case GREENSLIME+5:
				case GREENSLIME+6:
				case GREENSLIME+7:
				case RAT:
				case ROTATEGUN:
					return 1;
		}
		if(GlobalMembersGLOBAL.actortype[s.picnum])
			return 1;

		return 0;
	}


	public static short badguypic(short pn)
	{

		switch(pn)
		{
				case SHARK:
				case RECON:
				case DRONE:
				case LIZTROOPONTOILET:
				case LIZTROOPJUSTSIT:
				case LIZTROOPSTAYPUT:
				case LIZTROOPSHOOT:
				case LIZTROOPJETPACK:
				case LIZTROOPDUCKING:
				case LIZTROOPRUNNING:
				case LIZTROOP:
				case OCTABRAIN:
				case COMMANDER:
				case COMMANDERSTAYPUT:
				case PIGCOP:
				case EGG:
				case PIGCOPSTAYPUT:
				case PIGCOPDIVE:
				case LIZMAN:
				case LIZMANSPITTING:
				case LIZMANFEEDING:
				case LIZMANJUMP:
				case ORGANTIC:
				case BOSS1:
				case BOSS2:
				case BOSS3:
				case BOSS4:
				case GREENSLIME:
				case GREENSLIME+1:
				case GREENSLIME+2:
				case GREENSLIME+3:
				case GREENSLIME+4:
				case GREENSLIME+5:
				case GREENSLIME+6:
				case GREENSLIME+7:
				case RAT:
				case ROTATEGUN:
					return 1;
		}

		if(GlobalMembersGLOBAL.actortype[pn])
			return 1;

		return 0;
	}



	public static void myos(int x, int y, short tilenum, sbyte shade, sbyte orientation)
	{
		sbyte p;
		short a;

		if(orientation &4)
			a = 1024;
		else
			a = 0;

		p = sector[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cursectnum].floorpal;
		rotatesprite(x<<16,y<<16,65536,a,tilenum,shade,p,2|orientation,windowx1,windowy1,windowx2,windowy2);
	}

	public static void myospal(int x, int y, short tilenum, sbyte shade, sbyte orientation, sbyte p)
	{
		sbyte fp;
		short a;

		if(orientation &4)
			a = 1024;
		else
			a = 0;

		fp = sector[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cursectnum].floorpal;

		rotatesprite(x<<16,y<<16,65536,a,tilenum,shade,p,2|orientation,windowx1,windowy1,windowx2,windowy2);

	}

	public static void invennum(int x,int y,sbyte num1,sbyte ha,sbyte sbits)
	{
		string dabuf = "";
		dabuf = string.Format("{0:D}", num1);
		if(num1 > 99)
		{
			rotatesprite((x-4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,sbits,0,0,xdim-1,ydim-1);
			rotatesprite((x)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,sbits,0,0,xdim-1,ydim-1);
			rotatesprite((x+4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[2]-'0',ha,0,sbits,0,0,xdim-1,ydim-1);
		}
		else if(num1 > 9)
		{
			rotatesprite((x)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,sbits,0,0,xdim-1,ydim-1);
			rotatesprite((x+4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,sbits,0,0,xdim-1,ydim-1);
		}
		else
			rotatesprite((x+4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,sbits,0,0,xdim-1,ydim-1);
	}

	#if VOLUMEONE
	public static void orderweaponnum(short ind,int x,int y,int num1, int num2,sbyte ha)
	{
		rotatesprite((x-7)<<16,y<<16,65536,0,THREEBYFIVE+ind+1,ha-10,7,10+128,0,0,xdim-1,ydim-1);
		rotatesprite((x-3)<<16,y<<16,65536,0,THREEBYFIVE+10,ha,0,10+128,0,0,xdim-1,ydim-1);

		GlobalMembersGAME.minitextshade(x+1, y-4, "ORDER", 26, 6, 2+8+16+128);
	}
	#endif


	public static void weaponnum(short ind,int x,int y,int num1, int num2,sbyte ha)
	{
		string dabuf = "";

		rotatesprite((x-7)<<16,y<<16,65536,0,THREEBYFIVE+ind+1,ha-10,7,10+128,0,0,xdim-1,ydim-1);
		rotatesprite((x-3)<<16,y<<16,65536,0,THREEBYFIVE+10,ha,0,10+128,0,0,xdim-1,ydim-1);
		rotatesprite((x+9)<<16,y<<16,65536,0,THREEBYFIVE+11,ha,0,10+128,0,0,xdim-1,ydim-1);

		if(num1 > 99)
			num1 = 99;
		if(num2 > 99)
			num2 = 99;

		dabuf = string.Format("{0:D}", num1);
		if(num1 > 9)
		{
			rotatesprite((x)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
		}
		else
			rotatesprite((x+4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);

		dabuf = string.Format("{0:D}", num2);
		if(num2 > 9)
		{
			rotatesprite((x+13)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+17)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
		}
		else
			rotatesprite((x+13)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
	}

	public static void weaponnum999(sbyte ind,int x,int y,int num1, int num2,sbyte ha)
	{
		string dabuf = "";

		rotatesprite((x-7)<<16,y<<16,65536,0,THREEBYFIVE+ind+1,ha-10,7,10+128,0,0,xdim-1,ydim-1);
		rotatesprite((x-4)<<16,y<<16,65536,0,THREEBYFIVE+10,ha,0,10+128,0,0,xdim-1,ydim-1);
		rotatesprite((x+13)<<16,y<<16,65536,0,THREEBYFIVE+11,ha,0,10+128,0,0,xdim-1,ydim-1);

		dabuf = string.Format("{0:D}", num1);
		if(num1 > 99)
		{
			rotatesprite((x)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+8)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[2]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
		}
		else if(num1 > 9)
		{
			rotatesprite((x+4)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+8)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
		}
		else
			rotatesprite((x+8)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);

		dabuf = string.Format("{0:D}", num2);
		if(num2 > 99)
		{
			rotatesprite((x+17)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+21)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+25)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[2]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
		}
		else if(num2 > 9)
		{
			rotatesprite((x+17)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
			rotatesprite((x+21)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[1]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
		}
		else
			rotatesprite((x+25)<<16,y<<16,65536,0,THREEBYFIVE+dabuf[0]-'0',ha,0,10+128,0,0,xdim-1,ydim-1);
	}


		//REPLACE FULLY
	public static void weapon_amounts(ref player_struct p, int x, int y, int u)
	{
		 int cw;

		 cw = p.curr_weapon;

		 if (u &4 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(96,xdim,320),scale(178,ydim,200), scale(96+12,xdim,320)-1,scale(178+6,ydim,200)-1);
				 };
			 GlobalMembersGAME.weaponnum999(PISTOL_WEAPON, x, y, p.ammo_amount[PISTOL_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[PISTOL_WEAPON], 12-20*(cw == PISTOL_WEAPON));
		 }
		 if (u &8 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(96,xdim,320),scale(184,ydim,200), scale(96+12,xdim,320)-1,scale(184+6,ydim,200)-1);
				 };
			 GlobalMembersGAME.weaponnum999(SHOTGUN_WEAPON, x, y+6, p.ammo_amount[SHOTGUN_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[SHOTGUN_WEAPON], (!p.gotweapon[SHOTGUN_WEAPON]*9)+12-18* (cw == SHOTGUN_WEAPON));
		 }
		 if (u &16 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(96,xdim,320),scale(190,ydim,200), scale(96+12,xdim,320)-1,scale(190+6,ydim,200)-1);
				 };
			 GlobalMembersGAME.weaponnum999(CHAINGUN_WEAPON, x, y+12, p.ammo_amount[CHAINGUN_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[CHAINGUN_WEAPON], (!p.gotweapon[CHAINGUN_WEAPON]*9)+12-18* (cw == CHAINGUN_WEAPON));
		 }
		 if (u &32 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(135,xdim,320),scale(178,ydim,200), scale(135+8,xdim,320)-1,scale(178+6,ydim,200)-1);
				 };
			 GlobalMembersGAME.weaponnum(RPG_WEAPON, x+39, y, p.ammo_amount[RPG_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[RPG_WEAPON], (!p.gotweapon[RPG_WEAPON]*9)+12-19* (cw == RPG_WEAPON));
		 }
		 if (u &64 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(135,xdim,320),scale(184,ydim,200), scale(135+8,xdim,320)-1,scale(184+6,ydim,200)-1);
				 };
			 GlobalMembersGAME.weaponnum(HANDBOMB_WEAPON, x+39, y+6, p.ammo_amount[HANDBOMB_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[HANDBOMB_WEAPON], (((!p.ammo_amount[HANDBOMB_WEAPON])|(!p.gotweapon[HANDBOMB_WEAPON]))*9)+12-19* ((cw == HANDBOMB_WEAPON) || (cw == HANDREMOTE_WEAPON)));
		 }
		 if (u &128 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(135,xdim,320),scale(190,ydim,200), scale(135+8,xdim,320)-1,scale(190+6,ydim,200)-1);
				 };

	#if VOLUMEONE
			 GlobalMembersGAME.orderweaponnum(SHRINKER_WEAPON, x+39, y+12, p.ammo_amount[SHRINKER_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[SHRINKER_WEAPON], (!p.gotweapon[SHRINKER_WEAPON]*9)+12-18* (cw == SHRINKER_WEAPON));
	#else
			 if(p.subweapon&(1<<GROW_WEAPON))
				 GlobalMembersGAME.weaponnum(SHRINKER_WEAPON, x+39, y+12, p.ammo_amount[GROW_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[GROW_WEAPON], (!p.gotweapon[GROW_WEAPON]*9)+12-18* (cw == GROW_WEAPON));
			 else
				 GlobalMembersGAME.weaponnum(SHRINKER_WEAPON, x+39, y+12, p.ammo_amount[SHRINKER_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[SHRINKER_WEAPON], (!p.gotweapon[SHRINKER_WEAPON]*9)+12-18* (cw == SHRINKER_WEAPON));
	#endif
		 }
		 if (u &256 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(166,xdim,320),scale(178,ydim,200), scale(166+8,xdim,320)-1,scale(178+6,ydim,200)-1);
				 };

	#if VOLUMEONE
			GlobalMembersGAME.orderweaponnum(DEVISTATOR_WEAPON, x+70, y, p.ammo_amount[DEVISTATOR_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[DEVISTATOR_WEAPON], (!p.gotweapon[DEVISTATOR_WEAPON]*9)+12-18* (cw == DEVISTATOR_WEAPON));
	#else
			 GlobalMembersGAME.weaponnum(DEVISTATOR_WEAPON, x+70, y, p.ammo_amount[DEVISTATOR_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[DEVISTATOR_WEAPON], (!p.gotweapon[DEVISTATOR_WEAPON]*9)+12-18* (cw == DEVISTATOR_WEAPON));
	#endif
		 }
		 if (u &512 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(166,xdim,320),scale(184,ydim,200), scale(166+8,xdim,320)-1,scale(184+6,ydim,200)-1);
				 };
	#if VOLUMEONE
			 GlobalMembersGAME.orderweaponnum(TRIPBOMB_WEAPON, x+70, y+6, p.ammo_amount[TRIPBOMB_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[TRIPBOMB_WEAPON], (!p.gotweapon[TRIPBOMB_WEAPON]*9)+12-18* (cw == TRIPBOMB_WEAPON));
	#else
			 GlobalMembersGAME.weaponnum(TRIPBOMB_WEAPON, x+70, y+6, p.ammo_amount[TRIPBOMB_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[TRIPBOMB_WEAPON], (!p.gotweapon[TRIPBOMB_WEAPON]*9)+12-18* (cw == TRIPBOMB_WEAPON));
	#endif
		 }

		 if (u &65536 != 0)
		 {
			 if (u != 0xffffffff)
				 {
					 rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(166,xdim,320),scale(190,ydim,200), scale(166+8,xdim,320)-1,scale(190+6,ydim,200)-1);
				 };
	#if VOLUMEONE
			GlobalMembersGAME.orderweaponnum(-1, x+70, y+12, p.ammo_amount[FREEZE_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[FREEZE_WEAPON], (!p.gotweapon[FREEZE_WEAPON]*9)+12-18* (cw == FREEZE_WEAPON));
	#else
			 GlobalMembersGAME.weaponnum(-1, x+70, y+12, p.ammo_amount[FREEZE_WEAPON], GlobalMembersGLOBAL.max_ammo_amount[FREEZE_WEAPON], (!p.gotweapon[FREEZE_WEAPON]*9)+12-18* (cw == FREEZE_WEAPON));
	#endif
		 }
	}

	public static void digitalnumber(int x,int y,int n,sbyte s,sbyte cs)
	{
		short i;
		short j;
		short k;
		short p;
		short c;
		string b = new string(new char[10]);

		ltoa(n,b,10);
		i = b.Length;
		j = 0;

		for(k = 0;k<i;k++)
		{
			p = DIGITALNUM+*b.Substring(k)-'0';
			j += tilesizx[p]+1;
		}
		c = x-(j>>1);

		j = 0;
		for(k = 0;k<i;k++)
		{
			p = DIGITALNUM+*b.Substring(k)-'0';
			rotatesprite((c+j)<<16,y<<16,65536,0,p,s,0,cs,0,0,xdim-1,ydim-1);
			j += tilesizx[p]+1;
		}
	}

	/*
	
	void scratchmarks(long x,long y,long n,char s,char p)
	{
	    long i, ni;
	
	    ni = n/5;
	    for(i=ni;i >= 0;i--)
	    {
	        overwritesprite(x-2,y,SCRATCH+4,s,0,0);
	        x += tilesizx[SCRATCH+4]-1;
	    }
	
	    ni = n%5;
	    if(ni) overwritesprite(x,y,SCRATCH+ni-1,s,p,0);
	}
	  */
	public static void displayinventory(ref player_struct p)
	{
		short n;
		short j;
		short xoff;
		short y;

		j = xoff = 0;

		n = (p.jetpack_amount > 0)<<3;
		if(n &8)
			j++;
		n |= (p.scuba_amount > 0)<<5;
		if(n &32)
			j++;
		n |= (p.steroids_amount > 0)<<1;
		if(n &2)
			j++;
		n |= (p.holoduke_amount > 0)<<2;
		if(n &4)
			j++;
		n |= (p.firstaid_amount > 0);
		if(n &1)
			j++;
		n |= (p.heat_amount > 0)<<4;
		if(n &16)
			j++;
		n |= (p.boot_amount > 0)<<6;
		if(n &64)
			j++;

		xoff = 160-(j *11);

		j = 0;

		if(GlobalMembersGLOBAL.ud.screen_size > 4)
			y = 154;
		else
			y = 172;

		if(GlobalMembersGLOBAL.ud.screen_size == 4)
		{
			if(GlobalMembersGLOBAL.ud.multimode > 1)
				xoff += 56;
			else
				xoff += 65;
		}

		while(j <= 9)
		{
			if(n&(1<<j))
			{
				switch(n&(1<<j))
				{
					case 1:
					rotatesprite(xoff<<16,y<<16,65536,0,FIRSTAID_ICON,0,0,2+16,windowx1,windowy1,windowx2,windowy2);
					break;
					case 2:
					rotatesprite((xoff+1)<<16,y<<16,65536,0,STEROIDS_ICON,0,0,2+16,windowx1,windowy1,windowx2,windowy2);
					break;
					case 4:
					rotatesprite((xoff+2)<<16,y<<16,65536,0,HOLODUKE_ICON,0,0,2+16,windowx1,windowy1,windowx2,windowy2);
					break;
					case 8:
					rotatesprite(xoff<<16,y<<16,65536,0,JETPACK_ICON,0,0,2+16,windowx1,windowy1,windowx2,windowy2);
					break;
					case 16:
					rotatesprite(xoff<<16,y<<16,65536,0,HEAT_ICON,0,0,2+16,windowx1,windowy1,windowx2,windowy2);
					break;
					case 32:
					rotatesprite(xoff<<16,y<<16,65536,0,AIRTANK_ICON,0,0,2+16,windowx1,windowy1,windowx2,windowy2);
					break;
					case 64:
					rotatesprite(xoff<<16,(y-1)<<16,65536,0,BOOT_ICON,0,0,2+16,windowx1,windowy1,windowx2,windowy2);
					break;
				}

				xoff += 22;

				if(p.inven_icon == j+1)
					rotatesprite((xoff-2)<<16,(y+19)<<16,65536,1024,ARROW,-32,0,2+16,windowx1,windowy1,windowx2,windowy2);
			}

			j++;
		}
	}



	public static void displayfragbar()
	{
		short i;
		short j;

		j = 0;

		for(i = connecthead;i>=0;i = connectpoint2[i])
			if(i > j)
				j = i;

		rotatesprite(0,0,65600,0,FRAGBAR,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
		if(j >= 4)
			rotatesprite(319,(8)<<16,65600,0,FRAGBAR,0,0,10+16+64+128,0,0,xdim-1,ydim-1);
		if(j >= 8)
			rotatesprite(319,(16)<<16,65600,0,FRAGBAR,0,0,10+16+64+128,0,0,xdim-1,ydim-1);
		if(j >= 12)
			rotatesprite(319,(24)<<16,65600,0,FRAGBAR,0,0,10+16+64+128,0,0,xdim-1,ydim-1);

		for(i = connecthead;i>=0;i = connectpoint2[i])
		{
			GlobalMembersGAME.minitext(21+(73*(i &3)), 2+((i &28)<<1), ref GlobalMembersGLOBAL.ud.user_name[i][0], sprite[GlobalMembersGLOBAL.ps[i].i].pal, 2+8+16+128);
			GlobalMembersGLOBAL.tempbuf = string.Format("{0:D}", GlobalMembersGLOBAL.ps[i].frag-GlobalMembersGLOBAL.ps[i].fraggedself);
			GlobalMembersGAME.minitext(17+50+(73*(i &3)), 2+((i &28)<<1), ref GlobalMembersGLOBAL.tempbuf, sprite[GlobalMembersGLOBAL.ps[i].i].pal, 2+8+16+128);
		}
	}

	public static void coolgaugetext(short snum)
	{
		player_struct p;
		int i;
		int j;
		int o;
		int ss;
		int u;
		sbyte c;
		sbyte permbit;

		p = GlobalMembersGLOBAL.ps[snum];

		if (p.invdisptime > 0)
			GlobalMembersGAME.displayinventory(ref p);


		if(GlobalMembersGLOBAL.ps[snum].gm &MODE_MENU)
			if((GlobalMembersGLOBAL.current_menu >= 400 && GlobalMembersGLOBAL.current_menu <= 405))
				return;

		ss = GlobalMembersGLOBAL.ud.screen_size;
		if (ss < 4)
			return;

		if (GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop != 1)
		{
			if (GlobalMembersGLOBAL.pus != 0)
				{
					GlobalMembersGAME.displayfragbar();
				}
			else
			{
				for(i = connecthead;i>=0;i = connectpoint2[i])
					if (GlobalMembersGLOBAL.ps[i].frag != GlobalMembersGLOBAL.sbar.frag[i])
					{
						GlobalMembersGAME.displayfragbar();
						break;
					}
			}
			for(i = connecthead;i>=0;i = connectpoint2[i])
				if (i != myconnectindex)
					GlobalMembersGLOBAL.sbar.frag[i] = GlobalMembersGLOBAL.ps[i].frag;
		}

		if (ss == 4) //DRAW MINI STATUS BAR:
		{
			rotatesprite(5<<16,(200-28)<<16,65536,0,HEALTHBOX,0,21,10+16,0,0,xdim-1,ydim-1);
			if (p.inven_icon)
				rotatesprite(69<<16,(200-30)<<16,65536,0,INVENTORYBOX,0,21,10+16,0,0,xdim-1,ydim-1);

			if(sprite[p.i].pal == 1 && p.last_extra < 2)
				GlobalMembersGAME.digitalnumber(20, 200-17, 1, -16, 10+16);
			else
				GlobalMembersGAME.digitalnumber(20, 200-17, p.last_extra, -16, 10+16);

			rotatesprite(37<<16,(200-28)<<16,65536,0,AMMOBOX,0,21,10+16,0,0,xdim-1,ydim-1);

			if (p.curr_weapon == HANDREMOTE_WEAPON)
				i = HANDBOMB_WEAPON;
				else
					i = p.curr_weapon;
			GlobalMembersGAME.digitalnumber(53, 200-17, p.ammo_amount[i], -16, 10+16);

			o = 158;
			permbit = 0;
			if (p.inven_icon)
			{
				switch(p.inven_icon)
				{
					case 1:
						i = FIRSTAID_ICON;
						break;
					case 2:
						i = STEROIDS_ICON;
						break;
					case 3:
						i = HOLODUKE_ICON;
						break;
					case 4:
						i = JETPACK_ICON;
						break;
					case 5:
						i = HEAT_ICON;
						break;
					case 6:
						i = AIRTANK_ICON;
						break;
					case 7:
						i = BOOT_ICON;
						break;
					default:
						i = -1;
					break;
				}
				if (i >= 0)
					rotatesprite((231-o)<<16,(200-21)<<16,65536,0,i,0,0,10+16+permbit,0,0,xdim-1,ydim-1);

				GlobalMembersGAME.minitext(292-30-o, 190, "%", 6, 10+16+permbit);

				j = 0x80000000;
				switch(p.inven_icon)
				{
					case 1:
						i = p.firstaid_amount;
						break;
					case 2:
						i = ((p.steroids_amount+3)>>2);
						break;
					case 3:
						i = ((p.holoduke_amount+15)/24);
						j = p.holoduke_on;
						break;
					case 4:
						i = ((p.jetpack_amount+15)>>4);
						j = p.jetpack_on;
						break;
					case 5:
						i = p.heat_amount/12;
						j = p.heat_on;
						break;
					case 6:
						i = ((p.scuba_amount+63)>>6);
						break;
					case 7:
						i = (p.boot_amount>>1);
						break;
				}
				GlobalMembersGAME.invennum(284-30-o, 200-6, (sbyte)i, 0, 10+permbit);
				if (j > 0)
					GlobalMembersGAME.minitext(288-30-o, 180, "ON", 0, 10+16+permbit);
				else if (j != 0x80000000)
					GlobalMembersGAME.minitext(284-30-o, 180, "OFF", 2, 10+16+permbit);
				if (p.inven_icon >= 6)
					GlobalMembersGAME.minitext(284-35-o, 180, "AUTO", 2, 10+16+permbit);
			}
			return;
		}

			//DRAW/UPDATE FULL STATUS BAR:

		if (GlobalMembersGLOBAL.pus != 0)
		{
			GlobalMembersGLOBAL.pus = 0;
			u = 0xffffffff;
		}
		else
			u = 0;

		if (GlobalMembersGLOBAL.sbar.frag[myconnectindex] != p.frag)
		{
			GlobalMembersGLOBAL.sbar.frag[myconnectindex] = p.frag;
			u |= 32768;
		}
		if (GlobalMembersGLOBAL.sbar.got_access != p.got_access)
		{
			GlobalMembersGLOBAL.sbar.got_access = p.got_access;
			u |= 16384;
		}
		if (GlobalMembersGLOBAL.sbar.last_extra != p.last_extra)
		{
			GlobalMembersGLOBAL.sbar.last_extra = p.last_extra;
			u |= 1;
		}
		if (GlobalMembersGLOBAL.sbar.shield_amount != p.shield_amount)
		{
			GlobalMembersGLOBAL.sbar.shield_amount = p.shield_amount;
			u |= 2;
		}
		if (GlobalMembersGLOBAL.sbar.curr_weapon != p.curr_weapon)
		{
			GlobalMembersGLOBAL.sbar.curr_weapon = p.curr_weapon;
			u |= (4+8+16+32+64+128+256+512+1024+65536);
		}
		for(i = 1;i < 10;i++)
		{
			if (GlobalMembersGLOBAL.sbar.ammo_amount[i] != p.ammo_amount[i])
			{
			GlobalMembersGLOBAL.sbar.ammo_amount[i] = p.ammo_amount[i];
			if(i < 9)
				u |= ((2<<i)+1024);
				else
					u |= 65536+1024;
			}
			if (GlobalMembersGLOBAL.sbar.gotweapon[i] != p.gotweapon[i])
			{
				GlobalMembersGLOBAL.sbar.gotweapon[i] = p.gotweapon[i];
				if(i < 9)
					u |= ((2<<i)+1024);
					else
						u |= 65536+1024;
			}
		}
		if (GlobalMembersGLOBAL.sbar.inven_icon != p.inven_icon)
		{
			GlobalMembersGLOBAL.sbar.inven_icon = p.inven_icon;
			u |= (2048+4096+8192);
		}
		if (GlobalMembersGLOBAL.sbar.holoduke_on != p.holoduke_on)
		{
			GlobalMembersGLOBAL.sbar.holoduke_on = p.holoduke_on;
			u |= (4096+8192);
		}
		if (GlobalMembersGLOBAL.sbar.jetpack_on != p.jetpack_on)
		{
			GlobalMembersGLOBAL.sbar.jetpack_on = p.jetpack_on;
			u |= (4096+8192);
		}
		if (GlobalMembersGLOBAL.sbar.heat_on != p.heat_on)
		{
			GlobalMembersGLOBAL.sbar.heat_on = p.heat_on;
			u |= (4096+8192);
		}
		if (GlobalMembersGLOBAL.sbar.firstaid_amount != p.firstaid_amount)
		{
			GlobalMembersGLOBAL.sbar.firstaid_amount = p.firstaid_amount;
			u |= 8192;
		}
		if (GlobalMembersGLOBAL.sbar.steroids_amount != p.steroids_amount)
		{
			GlobalMembersGLOBAL.sbar.steroids_amount = p.steroids_amount;
			u |= 8192;
		}
		if (GlobalMembersGLOBAL.sbar.holoduke_amount != p.holoduke_amount)
		{
			GlobalMembersGLOBAL.sbar.holoduke_amount = p.holoduke_amount;
			u |= 8192;
		}
		if (GlobalMembersGLOBAL.sbar.jetpack_amount != p.jetpack_amount)
		{
			GlobalMembersGLOBAL.sbar.jetpack_amount = p.jetpack_amount;
			u |= 8192;
		}
		if (GlobalMembersGLOBAL.sbar.heat_amount != p.heat_amount)
		{
			GlobalMembersGLOBAL.sbar.heat_amount = p.heat_amount;
			u |= 8192;
		}
		if (GlobalMembersGLOBAL.sbar.scuba_amount != p.scuba_amount)
		{
			GlobalMembersGLOBAL.sbar.scuba_amount = p.scuba_amount;
			u |= 8192;
		}
		if (GlobalMembersGLOBAL.sbar.boot_amount != p.boot_amount)
		{
			GlobalMembersGLOBAL.sbar.boot_amount = p.boot_amount;
			u |= 8192;
		}
		if (u == 0)
			return;

		//0 - update health
		//1 - update armor
		//2 - update PISTOL_WEAPON ammo
		//3 - update SHOTGUN_WEAPON ammo
		//4 - update CHAINGUN_WEAPON ammo
		//5 - update RPG_WEAPON ammo
		//6 - update HANDBOMB_WEAPON ammo
		//7 - update SHRINKER_WEAPON ammo
		//8 - update DEVISTATOR_WEAPON ammo
		//9 - update TRIPBOMB_WEAPON ammo
		//10 - update ammo display
		//11 - update inventory icon
		//12 - update inventory on/off
		//13 - update inventory %
		//14 - update keys
		//15 - update kills
		//16 - update FREEZE_WEAPON ammo

		if (u == 0xffffffff)
		{
			{
				rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(0,xdim,320),scale(0,ydim,200), scale(320,xdim,320)-1,scale(200,ydim,200)-1);
			};
			if (GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop != 1)
				rotatesprite(277<<16,(200-27)<<16,65536,0,KILLSICON,0,0,10+16+128,0,0,xdim-1,ydim-1);
		}
		if (GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop != 1)
		{
			if (u &32768 != 0)
			{
				if (u != 0xffffffff)
					{
						rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(276,xdim,320),scale(183,ydim,200), scale(299,xdim,320)-1,scale(193,ydim,200)-1);
					};
				GlobalMembersGAME.digitalnumber(287, 200-17, max(p.frag-p.fraggedself,0), -16, 10+16+128);
			}
		}
		else
		{
			if (u &16384 != 0)
			{
				if (u != 0xffffffff)
					{
						rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(275,xdim,320),scale(182,ydim,200), scale(299,xdim,320)-1,scale(194,ydim,200)-1);
					};
				if (p.got_access &4)
					rotatesprite(275<<16,182<<16,65536,0,ACCESS_ICON,0,23,10+16+128,0,0,xdim-1,ydim-1);
				if (p.got_access &2)
					rotatesprite(288<<16,182<<16,65536,0,ACCESS_ICON,0,21,10+16+128,0,0,xdim-1,ydim-1);
				if (p.got_access &1)
					rotatesprite(281<<16,189<<16,65536,0,ACCESS_ICON,0,0,10+16+128,0,0,xdim-1,ydim-1);
			}
		}
		if (u&(4+8+16+32+64+128+256+512+65536))
			GlobalMembersGAME.weapon_amounts(ref p, 96, 182, u);

		if (u &1 != 0)
		{
			if (u != 0xffffffff)
				{
					rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(20,xdim,320),scale(183,ydim,200), scale(43,xdim,320)-1,scale(193,ydim,200)-1);
				};
			if(sprite[p.i].pal == 1 && p.last_extra < 2)
				GlobalMembersGAME.digitalnumber(32, 200-17, 1, -16, 10+16+128);
			else
				GlobalMembersGAME.digitalnumber(32, 200-17, p.last_extra, -16, 10+16+128);
		}
		if (u &2 != 0)
		{
			if (u != 0xffffffff)
				{
					rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(52,xdim,320),scale(183,ydim,200), scale(75,xdim,320)-1,scale(193,ydim,200)-1);
				};
			GlobalMembersGAME.digitalnumber(64, 200-17, p.shield_amount, -16, 10+16+128);
		}

		if (u &1024 != 0)
		{
			if (u != 0xffffffff)
				{
					rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(196,xdim,320),scale(183,ydim,200), scale(219,xdim,320)-1,scale(193,ydim,200)-1);
				};
			if (p.curr_weapon != KNEE_WEAPON)
			{
				if (p.curr_weapon == HANDREMOTE_WEAPON)
					i = HANDBOMB_WEAPON;
					else
						i = p.curr_weapon;
				GlobalMembersGAME.digitalnumber(230-22, 200-17, p.ammo_amount[i], -16, 10+16+128);
			}
		}

		if (u&(2048+4096+8192))
		{
			if (u != 0xffffffff)
			{
				if (u&(2048+4096))
				{
					{
						rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(231,xdim,320),scale(179,ydim,200), scale(265,xdim,320)-1,scale(197,ydim,200)-1);
					};
				}
								  else
								  {
									  {
										  rotatesprite(0,(200-34)<<16,65536,0,BOTTOMSTATUSBAR,4,0,10+16+64+128, scale(250,xdim,320),scale(190,ydim,200), scale(261,xdim,320)-1,scale(195,ydim,200)-1);
									  };
								  }
			}
			if (p.inven_icon)
			{
				o = 0;
				permbit = 128;

				if (u&(2048+4096))
				{
					switch(p.inven_icon)
					{
						case 1:
							i = FIRSTAID_ICON;
							break;
						case 2:
							i = STEROIDS_ICON;
							break;
						case 3:
							i = HOLODUKE_ICON;
							break;
						case 4:
							i = JETPACK_ICON;
							break;
						case 5:
							i = HEAT_ICON;
							break;
						case 6:
							i = AIRTANK_ICON;
							break;
						case 7:
							i = BOOT_ICON;
							break;
					}
					rotatesprite((231-o)<<16,(200-21)<<16,65536,0,i,0,0,10+16+permbit,0,0,xdim-1,ydim-1);
					GlobalMembersGAME.minitext(292-30-o, 190, "%", 6, 10+16+permbit);
					if (p.inven_icon >= 6)
						GlobalMembersGAME.minitext(284-35-o, 180, "AUTO", 2, 10+16+permbit);
				}
				if (u&(2048+4096))
				{
					switch(p.inven_icon)
					{
						case 3:
							j = p.holoduke_on;
							break;
						case 4:
							j = p.jetpack_on;
							break;
						case 5:
							j = p.heat_on;
							break;
						default:
							j = 0x80000000;
						break;
					}
					if (j > 0)
						GlobalMembersGAME.minitext(288-30-o, 180, "ON", 0, 10+16+permbit);
					else if (j != 0x80000000)
						GlobalMembersGAME.minitext(284-30-o, 180, "OFF", 2, 10+16+permbit);
				}
				if (u &8192 != 0)
				{
					switch(p.inven_icon)
					{
						case 1:
							i = p.firstaid_amount;
							break;
						case 2:
							i = ((p.steroids_amount+3)>>2);
							break;
						case 3:
							i = ((p.holoduke_amount+15)/24);
							break;
						case 4:
							i = ((p.jetpack_amount+15)>>4);
							break;
						case 5:
							i = p.heat_amount/12;
							break;
						case 6:
							i = ((p.scuba_amount+63)>>6);
							break;
						case 7:
							i = (p.boot_amount>>1);
							break;
					}
					GlobalMembersGAME.invennum(284-30-o, 200-6, (sbyte)i, 0, 10+permbit);
				}
			}
		}
	}


	#define AVERAGEFRAMES
	internal static int[] frameval = new int[DefineConstants.AVERAGEFRAMES];
	internal static int framecnt = 0;

	public static void tics()
	{
		int i;
		string b = new string(new char[10]);

		i = totalclock;
		if (i != frameval[framecnt])
		{
			b = string.Format("{0:D}", (TICRATE *DefineConstants.AVERAGEFRAMES)/(i-frameval[framecnt]));
			printext256(windowx1,windowy1,31,-21,b,1);
			frameval[framecnt] = i;
		}

		framecnt = ((framecnt+1)&(DefineConstants.AVERAGEFRAMES-1));
	}

	public static void coords(short snum)
	{
		short y = 0;

		if(GlobalMembersGLOBAL.ud.coop != 1)
		{
			if(GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.multimode < 5)
				y = 8;
			else if(GlobalMembersGLOBAL.ud.multimode > 4)
				y = 16;
		}

		GlobalMembersGLOBAL.tempbuf = string.Format("X= {0:D}", GlobalMembersGLOBAL.ps[snum].posx);
		printext256(250,y,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("Y= {0:D}", GlobalMembersGLOBAL.ps[snum].posy);
		printext256(250,y+7,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("Z= {0:D}", GlobalMembersGLOBAL.ps[snum].posz);
		printext256(250,y+14,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("A= {0:D}", GlobalMembersGLOBAL.ps[snum].ang);
		printext256(250,y+21,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("ZV= {0:D}", GlobalMembersGLOBAL.ps[snum].poszv);
		printext256(250,y+28,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("OG= {0:D}", GlobalMembersGLOBAL.ps[snum].on_ground);
		printext256(250,y+35,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("AM= {0:D}", GlobalMembersGLOBAL.ps[snum].ammo_amount[GROW_WEAPON]);
		printext256(250,y+43,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("LFW= {0:D}", GlobalMembersGLOBAL.ps[snum].last_full_weapon);
		printext256(250,y+50,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("SECTL= {0:D}", sector[GlobalMembersGLOBAL.ps[snum].cursectnum].lotag);
		printext256(250,y+57,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("SEED= {0:D}", randomseed);
		printext256(250,y+64,31,-1,GlobalMembersGLOBAL.tempbuf,1);
		GlobalMembersGLOBAL.tempbuf = string.Format("THOLD= {0:D}", GlobalMembersGLOBAL.ps[snum].transporter_hold);
		printext256(250,y+64+7,31,-1,GlobalMembersGLOBAL.tempbuf,1);
	}

	public static void operatefta()
	{
		 int i;
		 int j;
		 int k;

		 if(GlobalMembersGLOBAL.ud.screen_size > 0)
			 j = 200-45;
			 else
				 j = 200-8;
		 quotebot = min(quotebot,j);
		 quotebotgoal = min(quotebotgoal,j);
		 if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_TYPE)
			 j -= 8;
		 quotebotgoal = j;
		 j = quotebot;
		 for(i = 0;i<DefineConstants.MAXUSERQUOTES;i++)
		 {
			 k = user_quote_time[i];
			 if (k <= 0)
				 break;

			 if (k > 4)
				  GlobalMembersGAME.gametext(320>>1, j, ref user_quote[i], 0, 2+8+16);
			 else if (k > 2)
				 GlobalMembersGAME.gametext(320>>1, j, ref user_quote[i], 0, 2+8+16+1);
				 else
					 GlobalMembersGAME.gametext(320>>1, j, ref user_quote[i], 0, 2+8+16+1+32);
			 j -= 8;
		 }

		 if (GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].fta <= 1)
			 return;

		 if (GlobalMembersGLOBAL.ud.coop != 1 && GlobalMembersGLOBAL.ud.screen_size > 0 && GlobalMembersGLOBAL.ud.multimode > 1)
		 {
			 j = 0;
			 k = 8;
			 for(i = connecthead;i>=0;i = connectpoint2[i])
				 if (i > j)
					 j = i;

			 if (j >= 4 && j <= 8)
				 k += 8;
			 else if (j > 8 && j <= 12)
				 k += 16;
			 else if (j > 12)
				 k += 24;
		 }
		 else
			 k = 0;

		 if (GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ftq == 115 || GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ftq == 116)
		 {
			 k = quotebot;
			 for(i = 0;i<DefineConstants.MAXUSERQUOTES;i++)
			 {
				 if (user_quote_time[i] <= 0)
					 break;
				 k -= 8;
			 }
			 k -= 4;
		 }

		 j = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].fta;
		 if (j > 4)
			  GlobalMembersGAME.gametext(320>>1, k, ref GlobalMembersGLOBAL.fta_quotes[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ftq], 0, 2+8+16);
		 else
			 if (j > 2)
				 GlobalMembersGAME.gametext(320>>1, k, ref GlobalMembersGLOBAL.fta_quotes[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ftq], 0, 2+8+16+1);
		 else
			 GlobalMembersGAME.gametext(320>>1, k, ref GlobalMembersGLOBAL.fta_quotes[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ftq], 0, 2+8+16+1+32);
	}

	public static void FTA(short q, ref player_struct p)
	{
		if(GlobalMembersGLOBAL.ud.fta_on == 1)
		{
			if(p.fta > 0 && q != 115 && q != 116)
				if(p.ftq == 115 || p.ftq == 116)
					return;

			p.fta = 100;

			if(p.ftq != q || q == 26)
			// || q == 26 || q == 115 || q ==116 || q == 117 || q == 122 )
			{
				p.ftq = q;
				GlobalMembersGLOBAL.pub = NUMPAGES;
				GlobalMembersGLOBAL.pus = NUMPAGES;
			}
		}
	}

	public static void showtwoscreens()
	{
		short i;

	#if ! VOLUMEALL
		setview(0,0,xdim-1,ydim-1);
		flushperms();
		GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
		for(i = 0;i<64;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		KB_FlushKeyboardQueue();
		rotatesprite(0,0,65536,0,3291,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		while(!KB_KeyWaiting())
			GlobalMembersGAME.getpackets();

		for(i = 0;i<64;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		KB_FlushKeyboardQueue();
		rotatesprite(0,0,65536,0,3290,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		while(!KB_KeyWaiting())
			GlobalMembersGAME.getpackets();
	#else
	// CTW - REMOVED
	/*  setview(0,0,xdim-1,ydim-1);
	    flushperms();
	    ps[myconnectindex].palette = palette;
	    for(i=0;i<64;i+=7) palto(0,0,0,i);
	    KB_FlushKeyboardQueue();
	    clearview(0L);
	    rotatesprite(0,0,65536L,0,TENSCREEN,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
	    nextpage(); for(i=63;i>0;i-=7) palto(0,0,0,i);
	    totalclock = 0;
	    while( !KB_KeyWaiting() && totalclock < 2400) getpackets();*/
	// CTW END - REMOVED
	#endif
	}

	public static void binscreen()
	{
		int fil;
	#if VOLUMEONE
		fil = kopen4load("dukesw.bin",1);
	#else
		fil = kopen4load("duke3d.bin",1);
	#endif
		if(fil == -1)
			return;
		kread(fil,(string)0xb8000,4000);
		kclose(fil);
	}


	public static void gameexit(ref string t)
	{
		short i;

		if(t != 0)
			GlobalMembersGLOBAL.ps[myconnectindex].palette = (string) &palette[0];

		if(numplayers > 1)
		{
			GlobalMembersGAME.allowtimetocorrecterrorswhenquitting();
			uninitmultiplayers();
		}

		if(GlobalMembersGLOBAL.ud.recstat == 1)
			GlobalMembersGAME.closedemowrite();
		else if(GlobalMembersGLOBAL.ud.recstat == 2)
		{
			fclose(frecfilep);
		}

		if(qe || cp)
			goto GOTOHERE;

		if(playerswhenstarted > 1 && GlobalMembersGLOBAL.ud.coop != 1 && t == ' ')
		{
			GlobalMembersGAME.dobonus(1);
	// CTW - MODIFICATION
	//      setgamemode();
			setgamemode(GlobalMembersCONFIG.ScreenMode,GlobalMembersCONFIG.ScreenWidth,GlobalMembersCONFIG.ScreenHeight);
	// CTW END - MODIFICATION
		}
	#if ONELEVELDEMO
		GlobalMembersGAME.doorders();
		t = "You have been playing a ONE LEVEL demo of Duke Nukem 3D.";
	#endif

	// CTW - MODIFICATION
	/*  if( *t != 0 && *(t+1) != 'V' && *(t+1) != 'Y' && playonten == 0 )
	        showtwoscreens();*/
		if(t != 0 && *t.Substring(1) != 'V' && *t.Substring(1) != 'Y' && true)
			GlobalMembersGAME.showtwoscreens();
	// CTW END - MODIFICATION

		GOTOHERE:

		Shutdown();

		if(t != 0)
		{
			setvmode(0x3);
			GlobalMembersGAME.binscreen();
	// CTW - MODIFICATION
	/*      if(playonten == 0)
	        {
	            if(*t == ' ' && *(t+1) == 0) *t = 0;
	            printf("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
	            printf("%s%s","\n",t);
	        }*/
			if(true)
			{
				if(t == ' ' && *t.Substring(1) == 0)
					t = 0;
				Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
				Console.Write("{0}{1}","\n",t);
			}
	// CTW END - MODIFICATION        
		}

		uninitgroupfile();

		unlink("duke3d.tmp");

		Environment.Exit(0);
	}




	public static short inputloc = 0;
	public static short strget(short x, short y, ref string t, short dalen, short c)
	{
		short ch;
		short sc;

		while(KB_KeyWaiting())
		{
			sc = 0;
			ch = KB_Getch();

			if (ch == 0)
			{

				sc = KB_Getch();
				if(sc == 104)
					return(1);

				continue;
			}
			else
			{
				if(ch == 8)
				{
					if(inputloc > 0)
					{
						inputloc--;
						*(t + inputloc) = 0;
					}
				}
				else
				{
					if(ch == asc_Enter || sc == 104)
					{
						KB_ClearKeyDown(sc_Enter);
						KB_ClearKeyDown(sc_kpad_Enter);
						return (1);
					}
					else if(ch == asc_Escape)
					{
						KB_ClearKeyDown(sc_Escape);
						return (-1);
					}
					else if (ch >= 32 && inputloc < dalen && ch < 127)
					{
						ch = char.ToUpper(ch);
						*(t + inputloc) = ch;
						*(t + inputloc+1) = 0;
						inputloc++;
					}
				}
			}
		}

		if(c == 999)
			return(0);
		if(c == 998)
		{
			string b = new string(new char[41]);
			sbyte ii;
			for(ii = 0;ii<inputloc;ii++)
				b = StringFunctions.ChangeCharacter(b, ii, '*');
			b = StringFunctions.ChangeCharacter(b, ii, 0);
			x = GlobalMembersGAME.gametext(x, y, ref b, c, 2+8+16);
		}
		else
			x = GlobalMembersGAME.gametext(x, y, ref t, c, 2+8+16);
		c = 4-(sintable[(totalclock<<4)&2047]>>11);
		rotatesprite((x+8)<<16,(y+4)<<16,32768,0,SPINNINGNUKEICON+((totalclock>>3)%7),c,0,2+8,0,0,xdim-1,ydim-1);

		return (0);
	}

	public static void typemode()
	{
		 short ch;
		 short hitstate;
		 short i;
		 short j;

		 if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_SENDTOWHOM)
		 {
			  if(sendmessagecommand != -1 || GlobalMembersGLOBAL.ud.multimode < 3 || GlobalMembersGLOBAL.movesperpacket == 4)
			  {
					GlobalMembersGLOBAL.tempbuf[0] = 4;
					GlobalMembersGLOBAL.tempbuf[1] = 0;
					recbuf[0] = 0;

					if(GlobalMembersGLOBAL.ud.multimode < 3)
						 sendmessagecommand = 2;

					recbuf += GlobalMembersGLOBAL.ud.user_name[myconnectindex];
					recbuf += ": ";
					recbuf += GlobalMembersGLOBAL.typebuf;
					j = recbuf.Length;
					recbuf[j] = 0;
					GlobalMembersGLOBAL.tempbuf+1 += recbuf;

					if(sendmessagecommand >= GlobalMembersGLOBAL.ud.multimode || GlobalMembersGLOBAL.movesperpacket == 4)
					{
						 for(ch = connecthead;ch >= 0;ch = connectpoint2[ch])
							  if (ch != myconnectindex)
									sendpacket(ch,GlobalMembersGLOBAL.tempbuf,j+1);

						 GlobalMembersGAME.adduserquote(ref recbuf);
						 quotebot += 8;
						 quotebotgoal = quotebot;
					}
					else if(sendmessagecommand >= 0)
						 sendpacket(sendmessagecommand,GlobalMembersGLOBAL.tempbuf,j+1);

					sendmessagecommand = -1;
					GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~(MODE_TYPE|MODE_SENDTOWHOM);
			  }
			  else if(sendmessagecommand == -1)
			  {
					j = 50;
					GlobalMembersGAME.gametext(320>>1, j, "SEND MESSAGE TO...", 0, 2+8+16);
					j += 8;
					for(i = connecthead;i>=0;i = connectpoint2[i])
	//                for(i=0;i<ud.multimode;i++)
					{
						 if (i == myconnectindex)
						 {
							 GlobalMembersGAME.minitextshade((320>>1)-40+1, j+1, "A/ENTER - ALL", 26, 0, 2+8+16);
							 GlobalMembersGAME.minitext((320>>1)-40, j, "A/ENTER - ALL", 0, 2+8+16);
							 j += 7;
						 }
						 else
						 {
							 GlobalMembersGLOBAL.buf = string.Format("      {0:D} - {1}", i+1, GlobalMembersGLOBAL.ud.user_name[i]);
							 GlobalMembersGAME.minitextshade((320>>1)-40-6+1, j+1, ref GlobalMembersGLOBAL.buf, 26, 0, 2+8+16);
							 GlobalMembersGAME.minitext((320>>1)-40-6, j, ref GlobalMembersGLOBAL.buf, 0, 2+8+16);
							 j += 7;
						 }
					}
					GlobalMembersGAME.minitextshade((320>>1)-40-4+1, j+1, "    ESC - Abort", 26, 0, 2+8+16);
					GlobalMembersGAME.minitext((320>>1)-40-4, j, "    ESC - Abort", 0, 2+8+16);
					j += 7;

					//sprintf(buf,"PRESS 1-%ld FOR INDIVIDUAL PLAYER.",ud.multimode);
					//gametext(320>>1,j,buf,0,2+8+16); j += 8;
					//gametext(320>>1,j,"'A' OR 'ENTER' FOR ALL PLAYERS",0,2+8+16); j += 8;
					//gametext(320>>1,j,"ESC ABORTS",0,2+8+16); j += 8;

					if (GlobalMembersGLOBAL.ud.screen_size > 0)
						j = 200-45;
						else
							j = 200-8;
					GlobalMembersGAME.gametext(320>>1, j, ref GlobalMembersGLOBAL.typebuf, 0, 2+8+16);

					if(KB_KeyWaiting())
					{
						 i = KB_GetCh();

						 if(i == 'A' || i == 'a' || i == 13)
							  sendmessagecommand = GlobalMembersGLOBAL.ud.multimode;
						 else if(i >= '1' || i <= (GlobalMembersGLOBAL.ud.multimode + '1'))
							  sendmessagecommand = i - '1';
						 else
						 {
							sendmessagecommand = GlobalMembersGLOBAL.ud.multimode;
							  if(i == 27)
							  {
								  GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~(MODE_TYPE|MODE_SENDTOWHOM);
								  sendmessagecommand = -1;
							  }
							  else
							  GlobalMembersGLOBAL.typebuf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.typebuf, 0, 0);
						 }

						 KB_ClearKeyDown(sc_1);
						 KB_ClearKeyDown(sc_2);
						 KB_ClearKeyDown(sc_3);
						 KB_ClearKeyDown(sc_4);
						 KB_ClearKeyDown(sc_5);
						 KB_ClearKeyDown(sc_6);
						 KB_ClearKeyDown(sc_7);
						 KB_ClearKeyDown(sc_8);
						 KB_ClearKeyDown(sc_A);
						 KB_ClearKeyDown(sc_Escape);
						 KB_ClearKeyDown(sc_Enter);
					}
			  }
		 }
		 else
		 {
			  if(GlobalMembersGLOBAL.ud.screen_size > 0)
				  j = 200-45;
				  else
					  j = 200-8;
			  hitstate = GlobalMembersGAME.strget(320>>1, j, ref GlobalMembersGLOBAL.typebuf, 30, 1);

			  if(hitstate == 1)
			  {
					KB_ClearKeyDown(sc_Enter);
					GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_SENDTOWHOM;
			  }
			  else if(hitstate == -1)
					GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~(MODE_TYPE|MODE_SENDTOWHOM);
			  else
				  GlobalMembersGLOBAL.pub = NUMPAGES;
		 }
	}

	public static void moveclouds()
	{
		if(totalclock > GlobalMembersGLOBAL.cloudtotalclock || totalclock < (GlobalMembersGLOBAL.cloudtotalclock-7))
		{
			short i;

			GlobalMembersGLOBAL.cloudtotalclock = totalclock+6;

			for(i = 0;i<GlobalMembersGLOBAL.numclouds;i++)
			{
				GlobalMembersGLOBAL.cloudx[i] += (sintable[(GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ang+512)&2047]>>9);
				GlobalMembersGLOBAL.cloudy[i] += (sintable[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].ang &2047]>>9);

				sector[GlobalMembersGLOBAL.clouds[i]].ceilingxpanning = GlobalMembersGLOBAL.cloudx[i]>>6;
				sector[GlobalMembersGLOBAL.clouds[i]].ceilingypanning = GlobalMembersGLOBAL.cloudy[i]>>6;
			}
		}
	}


	public static void displayrest(int smoothratio)
	{
		int a;
		int i;
		int j;

		player_struct pp;
		walltype wal;
		int cposx;
		int cposy;
		int cang;

		pp = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek];

		if(pp.pals_time > 0 && pp.loogcnt == 0)
		{
			GlobalMembersMENUES.palto(pp.pals[0], pp.pals[1], pp.pals[2], pp.pals_time|128);

			restorepalette = 1;
		}
		else if(restorepalette)
		{
			setbrightness(GlobalMembersGLOBAL.ud.brightness>>2, pp.palette[0]);
			restorepalette = 0;
		}
		else if(pp.loogcnt > 0)
			GlobalMembersMENUES.palto(0, 64, 0, (pp.loogcnt>>1)+128);

		if(GlobalMembersGLOBAL.ud.show_help)
		{
			switch(GlobalMembersGLOBAL.ud.show_help)
			{
				case 1:
					rotatesprite(0,0,65536,0,TEXTSTORY,0,0,10+16+64, 0,0,xdim-1,ydim-1);
					break;
				case 2:
					rotatesprite(0,0,65536,0,F1HELP,0,0,10+16+64, 0,0,xdim-1,ydim-1);
					break;
			}

			if (KB_KeyPressed(sc_Escape))
			{
				KB_ClearKeyDown(sc_Escape);
				GlobalMembersGLOBAL.ud.show_help = 0;
				if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
				{
					GlobalMembersGLOBAL.ready2send = 1;
					totalclock = GlobalMembersGLOBAL.ototalclock;
				}
				GlobalMembersPREMAP.vscrn();
			}
			return;
		}

		i = pp.cursectnum;

		show2dsector[i>>3] |= (1<<(i &7));
		wal = wall[sector[i].wallptr];
		for(j = sector[i].wallnum;j>0;j--,wal++)
		{
			i = wal.nextsector;
			if (i < 0)
				continue;
			if (wal.cstat &0x0071)
				continue;
			if (wall[wal.nextwall].cstat &0x0071)
				continue;
			if (sector[i].lotag == 32767)
				continue;
			if (sector[i].ceilingz >= sector[i].floorz)
				continue;
			show2dsector[i>>3] |= (1<<(i &7));
		}

		if(GlobalMembersGLOBAL.ud.camerasprite == -1)
		{
			if(GlobalMembersGLOBAL.ud.overhead_on != 2)
			{
				if(pp.newowner >= 0)
					GlobalMembersGAME.cameratext(pp.newowner);
				else
				{
					GlobalMembersPLAYER.displayweapon(GlobalMembersGLOBAL.screenpeek);
					if(pp.over_shoulder_on == 0)
						GlobalMembersPLAYER.displaymasks(GlobalMembersGLOBAL.screenpeek);
				}
				GlobalMembersGAME.moveclouds();
			}

			if(GlobalMembersGLOBAL.ud.overhead_on > 0)
			{
					smoothratio = min(max(smoothratio,0),65536);
					GlobalMembersACTORS.dointerpolations(smoothratio);
					if(GlobalMembersGLOBAL.ud.scrollmode == 0)
					{
						 if(pp.newowner == -1)
						 {
							 if (GlobalMembersGLOBAL.screenpeek == myconnectindex && numplayers > 1)
							 {
								 cposx = GlobalMembersGLOBAL.omyx+mulscale16((int)(GlobalMembersGLOBAL.myx-GlobalMembersGLOBAL.omyx),smoothratio);
								 cposy = GlobalMembersGLOBAL.omyy+mulscale16((int)(GlobalMembersGLOBAL.myy-GlobalMembersGLOBAL.omyy),smoothratio);
								 cang = GlobalMembersGLOBAL.omyang+mulscale16((int)(((GlobalMembersGLOBAL.myang+1024-GlobalMembersGLOBAL.omyang)&2047)-1024), smoothratio);
							 }
							 else
							 {
								  cposx = pp.oposx+mulscale16((int)(pp.posx-pp.oposx),smoothratio);
								  cposy = pp.oposy+mulscale16((int)(pp.posy-pp.oposy),smoothratio);
								  cang = pp.oang+mulscale16((int)(((pp.ang+1024-pp.oang)&2047)-1024), smoothratio);
							 }
						}
						else
						{
							cposx = pp.oposx;
							cposy = pp.oposy;
							cang = pp.oang;
						}
					}
					else
					{

						 GlobalMembersGLOBAL.ud.fola += GlobalMembersGLOBAL.ud.folavel>>3;
						 GlobalMembersGLOBAL.ud.folx += (GlobalMembersGLOBAL.ud.folfvel *sintable[(512+2048-GlobalMembersGLOBAL.ud.fola)&2047])>>14;
						 GlobalMembersGLOBAL.ud.foly += (GlobalMembersGLOBAL.ud.folfvel *sintable[(512+1024-512-GlobalMembersGLOBAL.ud.fola)&2047])>>14;

						 cposx = GlobalMembersGLOBAL.ud.folx;
						 cposy = GlobalMembersGLOBAL.ud.foly;
						 cang = GlobalMembersGLOBAL.ud.fola;
					}

					if(GlobalMembersGLOBAL.ud.overhead_on == 2)
					{
						clearview(0);
						drawmapview(cposx,cposy,pp.zoom,cang);
					}
					GlobalMembersMENUES.drawoverheadmap(cposx, cposy, pp.zoom, cang);

					GlobalMembersACTORS.restoreinterpolations();

					if(GlobalMembersGLOBAL.ud.overhead_on == 2)
					{
						if(GlobalMembersGLOBAL.ud.screen_size > 0)
							a = 147;
						else
							a = 182;

						GlobalMembersGAME.minitext(1, a+6, ref GlobalMembersGLOBAL.volume_names[GlobalMembersGLOBAL.ud.volume_number], 0, 2+8+16);
						GlobalMembersGAME.minitext(1, a+12, ref GlobalMembersGLOBAL.level_names[GlobalMembersGLOBAL.ud.volume_number *11 + GlobalMembersGLOBAL.ud.level_number], 0, 2+8+16);
					}
			}
		}

		GlobalMembersGAME.coolgaugetext(GlobalMembersGLOBAL.screenpeek);
		GlobalMembersGAME.operatefta();

		if(KB_KeyPressed(sc_Escape) && GlobalMembersGLOBAL.ud.overhead_on == 0 && GlobalMembersGLOBAL.ud.show_help == 0 && GlobalMembersGLOBAL.ps[myconnectindex].newowner == -1)
		{
			if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) == MODE_MENU && GlobalMembersGLOBAL.current_menu < 51)
			{
				KB_ClearKeyDown(sc_Escape);
				GlobalMembersGLOBAL.ps[myconnectindex].gm &= ~MODE_MENU;
				if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
				{
					GlobalMembersGLOBAL.ready2send = 1;
					totalclock = GlobalMembersGLOBAL.ototalclock;
					cameraclock = totalclock;
					cameradist = 65536;
				}
				walock[MAXTILES-1] = 199;
				GlobalMembersPREMAP.vscrn();
			}
			else if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) != MODE_MENU && GlobalMembersGLOBAL.ps[myconnectindex].newowner == -1 && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_TYPE) != MODE_TYPE)
			{
				KB_ClearKeyDown(sc_Escape);
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();

				GlobalMembersSOUNDS.intomenusounds();

				GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;

				if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					GlobalMembersGLOBAL.ready2send = 0;

				if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
					GlobalMembersMENUES.cmenu(50);
				else
					GlobalMembersMENUES.cmenu(0);
				GlobalMembersGLOBAL.screenpeek = myconnectindex;
			}
		}

		if(GlobalMembersGLOBAL.ps[myconnectindex].newowner == -1 && GlobalMembersGLOBAL.ud.overhead_on == 0 && GlobalMembersGLOBAL.ud.crosshair && GlobalMembersGLOBAL.ud.camerasprite == -1)
			rotatesprite((160-(GlobalMembersGLOBAL.ps[myconnectindex].look_ang>>1))<<16,100<<16,65536,0,CROSSHAIR,0,0,2+1,windowx1,windowy1,windowx2,windowy2);

		if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_TYPE)
			GlobalMembersGAME.typemode();
		else
			GlobalMembersMENUES.menus();

		if(GlobalMembersGLOBAL.ud.pause_on==1 && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) == 0)
			GlobalMembersMENUES.menutext(160, 100, 0, 0, "GAME PAUSED");

		if(GlobalMembersGLOBAL.ud.coords)
			GlobalMembersGAME.coords(GlobalMembersGLOBAL.screenpeek);
		if(GlobalMembersGLOBAL.ud.tickrate)
			GlobalMembersGAME.tics();
	}


	public static void updatesectorz(int x, int y, int z, ref short sectnum)
	{
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
		walltype *wal = new walltype();
		int i;
		int j;
		int cz;
		int fz;

		getzsofslope(sectnum, x, y, cz, fz);
		if ((z >= cz) && (z <= fz))
			if (inside(x,y, sectnum) != 0)
				return;

		if ((sectnum >= 0) && (sectnum < numsectors))
		{
			wal = &wall[sector[sectnum].wallptr];
			j = sector[sectnum].wallnum;
			do
			{
				i = wal.nextsector;
				if (i >= 0)
				{
					getzsofslope(i, x, y, cz, fz);
					if ((z >= cz) && (z <= fz))
						if (inside(x,y,(short)i) == 1)
							{
								sectnum = i;
								return;
							}
				}
				wal++;
				j--;
			} while (j != 0);
		}

		for(i = numsectors-1;i>=0;i--)
		{
			getzsofslope(i, x, y, cz, fz);
			if ((z >= cz) && (z <= fz))
				if (inside(x,y,(short)i) == 1)
					{
						sectnum = i;
						return;
					}
		}

		sectnum = -1;
	}

	public static void view(ref player_struct pp, ref int vx, ref int vy, ref int vz, ref short vsectnum, short ang, short horiz)
	{
		 spritetype sp;
		 int i;
		 int nx;
		 int ny;
		 int nz;
		 int hx;
		 int hy;
		 int hz;
		 int hitx;
		 int hity;
		 int hitz;
		 short bakcstat;
		 short hitsect;
		 short hitwall;
		 short hitsprite;
		 short daang;

		 nx = (sintable[(ang+1536)&2047]>>4);
		 ny = (sintable[(ang+1024)&2047]>>4);
		 nz = (horiz-100)*128;

		 sp = sprite[pp.i];

		 bakcstat = sp.cstat;
		 sp.cstat &= (short)~0x101;

		 GlobalMembersGAME.updatesectorz(vx, vy, vz, ref vsectnum);
		 hitscan(vx, vy, vz, vsectnum, nx, ny, nz, hitsect, hitwall, hitsprite, hitx, hity, hitz, CLIPMASK1);

		 if(vsectnum < 0)
		 {
			sp.cstat = bakcstat;
			return;
		 }

		 hx = hitx-(vx);
		 hy = hity-(vy);
		 if (klabs(nx)+klabs(ny) > klabs(hx)+klabs(hy))
		 {
			 vsectnum = hitsect;
			 if (hitwall >= 0)
			 {
				 daang = getangle(wall[wall[hitwall].point2].x-wall[hitwall].x, wall[wall[hitwall].point2].y-wall[hitwall].y);

				 i = nx *sintable[daang]+ny *sintable[(daang+1536)&2047];
				 if (klabs(nx) > klabs(ny))
					 hx -= mulscale28(nx,i);
											  else
												  hy -= mulscale28(ny,i);
			 }
			 else if (hitsprite < 0)
			 {
				 if (klabs(nx) > klabs(ny))
					 hx -= (nx>>5);
											  else
												  hy -= (ny>>5);
			 }
			 if (klabs(nx) > klabs(ny))
				 i = divscale16(hx,nx);
										  else
											  i = divscale16(hy,ny);
			 if (i < cameradist)
				 cameradist = i;
		 }
		 vx = (vx)+mulscale16(nx,cameradist);
		 vy = (vy)+mulscale16(ny,cameradist);
		 vz = (vz)+mulscale16(nz,cameradist);

		 cameradist = min(cameradist+((totalclock-cameraclock)<<10),65536);
		 cameraclock = totalclock;

		 GlobalMembersGAME.updatesectorz(vx, vy, vz, ref vsectnum);

		 sp.cstat = bakcstat;
	}

		//REPLACE FULLY
	public static void drawbackground()
	{
		 short dapicnum;
		 int x;
		 int y;
		 int x1;
		 int y1;
		 int x2;
		 int y2;
		 int topy;

		 flushperms();

		 switch(GlobalMembersGLOBAL.ud.m_volume_number)
		 {
			  default:
				  dapicnum = BIGHOLE;
				  break;
			  case 1:
				  dapicnum = BIGHOLE;
				  break;
			  case 2:
				  dapicnum = BIGHOLE;
				  break;
		 }

		 y1 = 0;
		 y2 = ydim;
		 if(GlobalMembersGLOBAL.ready2send != 0 || GlobalMembersGLOBAL.ud.recstat == 2)
		 {
			if(GlobalMembersGLOBAL.ud.coop != 1)
			{
				if (GlobalMembersGLOBAL.ud.multimode > 1)
					y1 += scale(ydim,8,200);
				if (GlobalMembersGLOBAL.ud.multimode > 4)
					y1 += scale(ydim,8,200);
			}
			if (GlobalMembersGLOBAL.ud.screen_size >= 8)
				y2 = scale(ydim,200-34,200);
		 }

		 for(y = y1;y<y2;y+=128)
			  for(x = 0;x<xdim;x+=128)
					rotatesprite(x<<16,y<<16,65536,0,dapicnum,8,0,8+16+64+128,0,y1,xdim-1,y2-1);

		 if(GlobalMembersGLOBAL.ud.screen_size > 8)
		 {
			  y = 0;
			  if(GlobalMembersGLOBAL.ud.coop != 1)
			  {
				 if (GlobalMembersGLOBAL.ud.multimode > 1)
					 y += 8;
				 if (GlobalMembersGLOBAL.ud.multimode > 4)
					 y += 8;
			  }

			  x1 = max(windowx1-4,0);
			  y1 = max(windowy1-4,y);
			  x2 = min(windowx2+4,xdim-1);
			  y2 = min(windowy2+4,scale(ydim,200-34,200)-1);

			  for(y = y1+4;y<y2-4;y+=64)
			  {
					rotatesprite(x1<<16,y<<16,65536,0,VIEWBORDER,0,0,8+16+64+128,x1,y1,x2,y2);
					rotatesprite((x2+1)<<16,(y+64)<<16,65536,1024,VIEWBORDER,0,0,8+16+64+128,x1,y1,x2,y2);
			  }

			  for(x = x1+4;x<x2-4;x+=64)
			  {
					rotatesprite((x+64)<<16,y1<<16,65536,512,VIEWBORDER,0,0,8+16+64+128,x1,y1,x2,y2);
					rotatesprite(x<<16,(y2+1)<<16,65536,1536,VIEWBORDER,0,0,8+16+64+128,x1,y1,x2,y2);
			  }

			  rotatesprite(x1<<16,y1<<16,65536,0,VIEWBORDER+1,0,0,8+16+64+128,x1,y1,x2,y2);
			  rotatesprite((x2+1)<<16,y1<<16,65536,512,VIEWBORDER+1,0,0,8+16+64+128,x1,y1,x2,y2);
			  rotatesprite((x2+1)<<16,(y2+1)<<16,65536,1024,VIEWBORDER+1,0,0,8+16+64+128,x1,y1,x2,y2);
			  rotatesprite(x1<<16,(y2+1)<<16,65536,1536,VIEWBORDER+1,0,0,8+16+64+128,x1,y1,x2,y2);
		 }
	}


	// Floor Over Floor

	// If standing in sector with SE42
	// then draw viewing to SE41 and raise all =hi SE43 cielings.

	// If standing in sector with SE43
	// then draw viewing to SE40 and lower all =hi SE42 floors.

	// If standing in sector with SE44
	// then draw viewing to SE40.

	// If standing in sector with SE45
	// then draw viewing to SE41.

	#define FOFTILE
	#define FOFTILEX
	#define FOFTILEY
	public static int[] tempsectorz = new int[MAXSECTORS];
	public static int[] tempsectorpicnum = new int[MAXSECTORS];
	//short tempcursectnum;

	public static SE40_Draw(int spnum,int x,int y,int z,short a,short h,int smoothratio)
	{
	 int i = 0;
	 int j = 0;
	 int k = 0;
	 int floor1 = 0;
	 int floor2 = 0;
	 int ok = 0;
	 int fofmode = 0;
	 int offx;
	 int offy;

	 if(sprite[spnum].ang!=512)
		 return;

	 i = DefineConstants.FOFTILE; //Effect TILE
	 if (!(gotpic[i>>3]&(1<<(i &7))))
		 return;
	 gotpic[i>>3] &= ~(1<<(i &7));

	 floor1 = spnum;

	 if(sprite[spnum].lotag==42)
		 fofmode = 40;
	 if(sprite[spnum].lotag==43)
		 fofmode = 41;
	 if(sprite[spnum].lotag==44)
		 fofmode = 40;
	 if(sprite[spnum].lotag==45)
		 fofmode = 41;

	// fofmode=sprite[spnum].lotag-2;

	// sectnum=sprite[j].sectnum;
	// sectnum=cursectnum;
	 ok++;

	/*  recursive?
	 for(j=0;j<MAXSPRITES;j++)
	 {
	  if(
	     sprite[j].sectnum==sectnum &&
	     sprite[j].picnum==1 &&
	     sprite[j].lotag==110
	    ) { DrawFloorOverFloor(j); break;}
	 }
	*/

	// if(ok==0) { Message("no fof",RED); return; }

	 for(j = 0;j<MAXSPRITES;j++)
	 {
	  if(sprite[j].picnum==1 && sprite[j].lotag==fofmode && sprite[j].hitag==sprite[floor1].hitag)
	  {
		  floor1 = j;
		  fofmode = sprite[j].lotag;
		  ok++;
		  break;
	  }
	 }
	// if(ok==1) { Message("no floor1",RED); return; }

	 if(fofmode == 40)
		 k = 41;
		 else
			 k = 40;

	 for(j = 0;j<MAXSPRITES;j++)
	 {
	  if(sprite[j].picnum==1 && sprite[j].lotag==k && sprite[j].hitag==sprite[floor1].hitag)
	  {
		  floor2 = j;
		  ok++;
		  break;
	  }
	 }

	// if(ok==2) { Message("no floor2",RED); return; }

	 for(j = 0;j<MAXSPRITES;j++) // raise ceiling or floor
	 {
	  if(sprite[j].picnum==1 && sprite[j].lotag==k+2 && sprite[j].hitag==sprite[floor1].hitag)
		{
		 if(k == 40)
		 {
			 tempsectorz[sprite[j].sectnum]=sector[sprite[j].sectnum].floorz;
		  sector[sprite[j].sectnum].floorz+=(((z-sector[sprite[j].sectnum].floorz)/32768)+1)*32768;
		  tempsectorpicnum[sprite[j].sectnum]=sector[sprite[j].sectnum].floorpicnum;
		  sector[sprite[j].sectnum].floorpicnum=13;
		 }
		 if(k == 41)
		 {
			 tempsectorz[sprite[j].sectnum]=sector[sprite[j].sectnum].ceilingz;
		  sector[sprite[j].sectnum].ceilingz+=(((z-sector[sprite[j].sectnum].ceilingz)/32768)-1)*32768;
		  tempsectorpicnum[sprite[j].sectnum]=sector[sprite[j].sectnum].ceilingpicnum;
		  sector[sprite[j].sectnum].ceilingpicnum=13;
		 }
		}
	 }

	 i = floor1;
	 offx = x-sprite[i].x;
	 offy = y-sprite[i].y;
	 i = floor2;
	 drawrooms(offx+sprite[i].x,offy+sprite[i].y,z,a,h,sprite[i].sectnum);
	 GlobalMembersGAME.animatesprites(x, y, a, smoothratio);
	 drawmasks();

	 for(j = 0;j<MAXSPRITES;j++) // restore ceiling or floor
	 {
	  if(sprite[j].picnum==1 && sprite[j].lotag==k+2 && sprite[j].hitag==sprite[floor1].hitag)
		{
		 if(k == 40)
		 {
			 sector[sprite[j].sectnum].floorz=tempsectorz[sprite[j].sectnum];
		  sector[sprite[j].sectnum].floorpicnum=tempsectorpicnum[sprite[j].sectnum];
		 }
		 if(k == 41)
		 {
			 sector[sprite[j].sectnum].ceilingz=tempsectorz[sprite[j].sectnum];
		  sector[sprite[j].sectnum].ceilingpicnum=tempsectorpicnum[sprite[j].sectnum];
		 }
		} // end if
	 } // end for

	} // end SE40




	public static void se40code(int x,int y,int z,int a,int h, int smoothratio)
	{
		int i;

		i = headspritestat[15];
		while(i >= 0)
		{
			switch(sprite[i].lotag)
			{
	//            case 40:
	//            case 41:
	//                SE40_Draw(i,x,y,a,smoothratio);
	//                break;
				case 42:
				case 43:
				case 44:
				case 45:
					if(GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cursectnum == sprite[i].sectnum)
						GlobalMembersGAME.SE40_Draw(i, x, y, z, a, h, smoothratio);
					break;
			}
			i = nextspritestat[i];
		}
	}

	internal static int oyrepeat=-1;

	public static void displayrooms(short snum,int smoothratio)
	{
		int cposx;
		int cposy;
		int cposz;
		int dst;
		int j;
		int fz;
		int cz;
		int hz;
		int lz;
		short sect;
		short cang;
		short k;
		short choriz;
		short tsect;
		player_struct p;
		int tposx;
		int tposy;
		int tposz;
		int dx;
		int dy;
		int thoriz;
		int i;
		short tang;

		p = GlobalMembersGLOBAL.ps[snum];

	//    if(screencapt == 0 && (p->gm&MODE_MENU) && ( (current_menu/100) == 3 ) || (current_menu >= 1000 && current_menu < 2999 ) )
	  //      return;

		if(GlobalMembersGLOBAL.pub > 0)
		{
			if(GlobalMembersGLOBAL.ud.screen_size > 8)
				GlobalMembersGAME.drawbackground();
			GlobalMembersGLOBAL.pub = 0;
		}

		if(GlobalMembersGLOBAL.ud.overhead_on == 2 || GlobalMembersGLOBAL.ud.show_help || p.cursectnum == -1)
			return;

		smoothratio = min(max(smoothratio,0),65536);

		visibility = p.visibility;

		if(GlobalMembersGLOBAL.ud.pause_on || GlobalMembersGLOBAL.ps[snum].on_crane > -1)
			smoothratio = 65536;

		sect = p.cursectnum;
		if(sect < 0 || sect >= MAXSECTORS)
			return;

		GlobalMembersACTORS.dointerpolations(smoothratio);

		GlobalMembersSECTOR.animatecamsprite();

		if(GlobalMembersGLOBAL.ud.camerasprite >= 0)
		{
			spritetype s;

			s = sprite[GlobalMembersGLOBAL.ud.camerasprite];

			if(s.yvel < 0)
				s.yvel = -100;
			else if(s.yvel > 199)
				s.yvel = 300;

			cang = GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ud.camerasprite].tempang+mulscale16((int)(((s.ang+1024-GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ud.camerasprite].tempang)&2047)-1024), smoothratio);

			GlobalMembersGAME.se40code(s.x, s.y, s.z, cang, s.yvel, smoothratio);

			drawrooms(s.x,s.y,s.z-(4<<8),cang,s.yvel,s.sectnum);
			GlobalMembersGAME.animatesprites(s.x, s.y, cang, smoothratio);
			drawmasks();
		}
		else
		{
			i = divscale22(1,sprite[p.i].yrepeat+28);
			if (i != oyrepeat)
			{
				oyrepeat = i;
				setaspect(oyrepeat,yxaspect);
			}

			if(screencapt)
			{
				walock[MAXTILES-1] = 254;
				if (waloff[MAXTILES-1] == 0)
					allocache((int)&waloff[MAXTILES-1], 100 *160, walock[MAXTILES-1]);
				setviewtotile(MAXTILES-1,100,160);
			}
			else if((GlobalMembersGLOBAL.ud.screen_tilting && p.rotscrnang) || GlobalMembersGLOBAL.ud.detail==0)
			{
					if (GlobalMembersGLOBAL.ud.screen_tilting)
						tang = p.rotscrnang;
						else
							tang = 0;

					walock[MAXTILES-2] = 255;
					if (waloff[MAXTILES-2] == 0)
						allocache(waloff[MAXTILES-2], 320 *320, walock[MAXTILES-2]);
					if ((tang &1023) == 0)
						setviewtotile(MAXTILES-2,200>>(1-GlobalMembersGLOBAL.ud.detail),320>>(1-GlobalMembersGLOBAL.ud.detail));
					else
						setviewtotile(MAXTILES-2,320>>(1-GlobalMembersGLOBAL.ud.detail),320>>(1-GlobalMembersGLOBAL.ud.detail));
					if ((tang &1023) == 512)
					{ //Block off unscreen section of 90ø tilted screen
						j = ((320-60)>>(1-GlobalMembersGLOBAL.ud.detail));
						for(i = (60>>(1-GlobalMembersGLOBAL.ud.detail))-1;i>=0;i--)
						{
							startumost[i] = 1;
							startumost[i+j] = 1;
							startdmost[i] = 0;
							startdmost[i+j] = 0;
						}
					}

					i = (tang &511);
					if (i > 256)
						i = 512-i;
					i = sintable[i+512]*8 + sintable[i]*5;
					setaspect(i>>1,yxaspect);
			  }

			  if ((snum == myconnectindex) && (numplayers > 1))
					  {
									cposx = GlobalMembersGLOBAL.omyx+mulscale16((int)(GlobalMembersGLOBAL.myx-GlobalMembersGLOBAL.omyx),smoothratio);
									cposy = GlobalMembersGLOBAL.omyy+mulscale16((int)(GlobalMembersGLOBAL.myy-GlobalMembersGLOBAL.omyy),smoothratio);
									cposz = GlobalMembersGLOBAL.omyz+mulscale16((int)(GlobalMembersGLOBAL.myz-GlobalMembersGLOBAL.omyz),smoothratio);
									cang = GlobalMembersGLOBAL.omyang+mulscale16((int)(((GlobalMembersGLOBAL.myang+1024-GlobalMembersGLOBAL.omyang)&2047)-1024), smoothratio);
									choriz = GlobalMembersGLOBAL.omyhoriz+GlobalMembersGLOBAL.omyhorizoff+mulscale16((int)(GlobalMembersGLOBAL.myhoriz+GlobalMembersGLOBAL.myhorizoff-GlobalMembersGLOBAL.omyhoriz-GlobalMembersGLOBAL.omyhorizoff),smoothratio);
									sect = GlobalMembersGLOBAL.mycursectnum;
					  }
					  else
					  {
									cposx = p.oposx+mulscale16((int)(p.posx-p.oposx),smoothratio);
									cposy = p.oposy+mulscale16((int)(p.posy-p.oposy),smoothratio);
									cposz = p.oposz+mulscale16((int)(p.posz-p.oposz),smoothratio);
									cang = p.oang+mulscale16((int)(((p.ang+1024-p.oang)&2047)-1024), smoothratio);
									choriz = p.ohoriz+p.ohorizoff+mulscale16((int)(p.horiz+p.horizoff-p.ohoriz-p.ohorizoff),smoothratio);
					  }
					  cang += p.look_ang;

					  if (p.newowner >= 0)
					  {
									cang = p.ang+p.look_ang;
									choriz = p.horiz+p.horizoff;
									cposx = p.posx;
									cposy = p.posy;
									cposz = p.posz;
									sect = sprite[p.newowner].sectnum;
									smoothratio = 65536;
					  }

					  else if(p.over_shoulder_on == 0)
									cposz += p.opyoff+mulscale16((int)(p.pyoff-p.opyoff),smoothratio);
					  else
						  GlobalMembersGAME.view(ref p, ref cposx, ref cposy, ref cposz, ref sect, cang, choriz);

			cz = GlobalMembersGLOBAL.hittype[p.i].ceilingz;
			fz = GlobalMembersGLOBAL.hittype[p.i].floorz;

			if(GlobalMembersGLOBAL.earthquaketime > 0 && p.on_ground == 1)
			{
				cposz += 256-(((GlobalMembersGLOBAL.earthquaketime)&1)<<9);
				cang += (2-((GlobalMembersGLOBAL.earthquaketime)&2))<<2;
			}

			if(sprite[p.i].pal == 1)
				cposz -= (18<<8);

			if(p.newowner >= 0)
				choriz = 100+sprite[p.newowner].shade;
			else if(p.spritebridge == 0)
			{
				if(cposz < (p.truecz + (4<<8)))
					cposz = cz + (4<<8);
				else if(cposz > (p.truefz - (4<<8)))
					cposz = fz - (4<<8);
			}

			if (sect >= 0)
			{
				getzsofslope(sect, cposx, cposy, cz, fz);
				if (cposz < cz+(4<<8))
					cposz = cz+(4<<8);
				if (cposz > fz-(4<<8))
					cposz = fz-(4<<8);
			}

			if(choriz > 299)
				choriz = 299;
			else if(choriz < -99)
				choriz = -99;

			GlobalMembersGAME.se40code(cposx, cposy, cposz, cang, choriz, smoothratio);

			if ((gotpic[MIRROR>>3]&(1<<(MIRROR &7))) > 0)
			{
				dst = 0x7fffffff;
				i = 0;
				for(k = 0;k<GlobalMembersGLOBAL.mirrorcnt;k++)
				{
					j = klabs(wall[GlobalMembersGLOBAL.mirrorwall[k]].x-cposx);
					j += klabs(wall[GlobalMembersGLOBAL.mirrorwall[k]].y-cposy);
					if (j < dst)
						dst = j, i = k;
				}

				if(wall[GlobalMembersGLOBAL.mirrorwall[i]].overpicnum == MIRROR)
				{
					preparemirror(cposx, cposy, cposz, cang, choriz, GlobalMembersGLOBAL.mirrorwall[i], GlobalMembersGLOBAL.mirrorsector[i], tposx, tposy, tang);

					j = visibility;
					visibility = (j>>1) + (j>>2);

					drawrooms(tposx,tposy,cposz,tang,choriz,GlobalMembersGLOBAL.mirrorsector[i]+MAXSECTORS);

					GlobalMembersGLOBAL.display_mirror = 1;
					GlobalMembersGAME.animatesprites(tposx, tposy, tang, smoothratio);
					GlobalMembersGLOBAL.display_mirror = 0;

					drawmasks();
					completemirror(); //Reverse screen x-wise in this function
					visibility = j;
				}
				gotpic[MIRROR>>3] &= ~(1<<(MIRROR &7));
			}

			drawrooms(cposx,cposy,cposz,cang,choriz,sect);
			GlobalMembersGAME.animatesprites(cposx, cposy, cang, smoothratio);
			drawmasks();

			if(screencapt == 1)
			{
				setviewback();
				walock[MAXTILES-1] = 1;
				screencapt = 0;
			}
			else if((GlobalMembersGLOBAL.ud.screen_tilting && p.rotscrnang) || GlobalMembersGLOBAL.ud.detail==0)
			{
				if (GlobalMembersGLOBAL.ud.screen_tilting)
					tang = p.rotscrnang;
					else
						tang = 0;
				setviewback();
				picanm[MAXTILES-2] &= 0xff0000ff;
				i = (tang &511);
				if (i > 256)
					i = 512-i;
				i = sintable[i+512]*8 + sintable[i]*5;
				if ((1-GlobalMembersGLOBAL.ud.detail) == 0)
					i >>= 1;
				rotatesprite(160<<16,100<<16,i,tang+512,MAXTILES-2,0,0,4+2+64,windowx1,windowy1,windowx2,windowy2);
				walock[MAXTILES-2] = 199;
			}
		}

		GlobalMembersACTORS.restoreinterpolations();

		if (totalclock < GlobalMembersPLAYER.lastvisinc)
		{
			if (klabs(p.visibility-GlobalMembersGLOBAL.ud.const_visibility) > 8)
				p.visibility += (GlobalMembersGLOBAL.ud.const_visibility-p.visibility)>>2;
		}
		else
			p.visibility = GlobalMembersGLOBAL.ud.const_visibility;
	}





	public static short LocateTheLocator(short n,short sn)
	{
		short i;

		i = headspritestat[7];
		while(i >= 0)
		{
			if((sn == -1 || sn == SECT) && n == SLT)
				return i;
			i = nextspritestat[i];
		}
		return -1;
	}

	public static short EGS(short whatsect,int s_x,int s_y,int s_z,short s_pn,sbyte s_s,sbyte s_xr,sbyte s_yr,short s_a,short s_ve,int s_zv,short s_ow,sbyte s_ss)
	{
		short i;
		spritetype s;

		i = insertsprite(whatsect,s_ss);

		if(i < 0)
			GlobalMembersGAME.gameexit(" Too many sprites spawned.");

		GlobalMembersGLOBAL.hittype[i].bposx = s_x;
		GlobalMembersGLOBAL.hittype[i].bposy = s_y;
		GlobalMembersGLOBAL.hittype[i].bposz = s_z;

		s = sprite[i];

		s.x = s_x;
		s.y = s_y;
		s.z = s_z;
		s.cstat = 0;
		s.picnum = s_pn;
		s.shade = s_s;
		s.xrepeat = s_xr;
		s.yrepeat = s_yr;
		s.pal = 0;

		s.ang = s_a;
		s.xvel = s_ve;
		s.zvel = s_zv;
		s.owner = s_ow;
		s.xoffset = 0;
		s.yoffset = 0;
		s.yvel = 0;
		s.clipdist = 0;
		s.pal = 0;
		s.lotag = 0;

		GlobalMembersGLOBAL.hittype[i].picnum = sprite[s_ow].picnum;

		GlobalMembersGLOBAL.hittype[i].lastvx = 0;
		GlobalMembersGLOBAL.hittype[i].lastvy = 0;

		GlobalMembersGLOBAL.hittype[i].timetosleep = 0;
		GlobalMembersGLOBAL.hittype[i].actorstayput = -1;
		GlobalMembersGLOBAL.hittype[i].extra = -1;
		GlobalMembersGLOBAL.hittype[i].owner = s_ow;
		GlobalMembersGLOBAL.hittype[i].cgg = 0;
		GlobalMembersGLOBAL.hittype[i].movflag = 0;
		GlobalMembersGLOBAL.hittype[i].tempang = 0;
		GlobalMembersGLOBAL.hittype[i].dispicnum = 0;
		GlobalMembersGLOBAL.hittype[i].floorz = GlobalMembersGLOBAL.hittype[s_ow].floorz;
		GlobalMembersGLOBAL.hittype[i].ceilingz = GlobalMembersGLOBAL.hittype[s_ow].ceilingz;

		T1=T3=T4=T6=0;
		if (GlobalMembersGLOBAL.actorscrptr[s_pn] != 0)
		{
			s.extra = GlobalMembersGLOBAL.actorscrptr[s_pn];
			T5 = *(GlobalMembersGLOBAL.actorscrptr[s_pn]+1);
			T2 = *(GlobalMembersGLOBAL.actorscrptr[s_pn]+2);
			s.hitag = *(GlobalMembersGLOBAL.actorscrptr[s_pn]+3);
		}
		else
		{
			T2=T5=0;
			s.extra = 0;
			s.hitag = 0;
		}

		if (show2dsector[SECT>>3]&(1<<(SECT &7)))
			show2dsprite[i>>3] |= (1<<(i &7));
		else
			show2dsprite[i>>3] &= ~(1<<(i &7));
	/*
	    if(s->sectnum < 0)
	    {
	        s->xrepeat = s->yrepeat = 0;
	        changespritestat(i,5);
	    }
	*/
		return(i);
	}

	public static sbyte wallswitchcheck(short i)
	{
		switch(PN)
		{
			case HANDPRINTSWITCH:
			case HANDPRINTSWITCH+1:
			case ALIENSWITCH:
			case ALIENSWITCH+1:
			case MULTISWITCH:
			case MULTISWITCH+1:
			case MULTISWITCH+2:
			case MULTISWITCH+3:
			case ACCESSSWITCH:
			case ACCESSSWITCH2:
			case PULLSWITCH:
			case PULLSWITCH+1:
			case HANDSWITCH:
			case HANDSWITCH+1:
			case SLOTDOOR:
			case SLOTDOOR+1:
			case LIGHTSWITCH:
			case LIGHTSWITCH+1:
			case SPACELIGHTSWITCH:
			case SPACELIGHTSWITCH+1:
			case SPACEDOORSWITCH:
			case SPACEDOORSWITCH+1:
			case FRANKENSTINESWITCH:
			case FRANKENSTINESWITCH+1:
			case LIGHTSWITCH2:
			case LIGHTSWITCH2+1:
			case POWERSWITCH1:
			case POWERSWITCH1+1:
			case LOCKSWITCH1:
			case LOCKSWITCH1+1:
			case POWERSWITCH2:
			case POWERSWITCH2+1:
			case DIPSWITCH:
			case DIPSWITCH+1:
			case DIPSWITCH2:
			case DIPSWITCH2+1:
			case TECHSWITCH:
			case TECHSWITCH+1:
			case DIPSWITCH3:
			case DIPSWITCH3+1:
				return 1;
		}
		return 0;
	}


	public static int tempwallptr;
	public static short spawn(short j, short pn)
	{
		short i;
		short s;
		short startwall;
		short endwall;
		short sect;
		short clostest;
		int x;
		int y;
		int d;
		spritetype sp;

		if(j >= 0)
		{
			i = GlobalMembersGAME.EGS(sprite[j].sectnum, sprite[j].x, sprite[j].y, sprite[j].z, pn, 0, 0, 0, 0, 0, 0, j, 0);
			GlobalMembersGLOBAL.hittype[i].picnum = sprite[j].picnum;
		}
		else
		{
			i = pn;

			GlobalMembersGLOBAL.hittype[i].picnum = PN;
			GlobalMembersGLOBAL.hittype[i].timetosleep = 0;
			GlobalMembersGLOBAL.hittype[i].extra = -1;

			GlobalMembersGLOBAL.hittype[i].bposx = SX;
			GlobalMembersGLOBAL.hittype[i].bposy = SY;
			GlobalMembersGLOBAL.hittype[i].bposz = SZ;

			OW = GlobalMembersGLOBAL.hittype[i].owner = i;
			GlobalMembersGLOBAL.hittype[i].cgg = 0;
			GlobalMembersGLOBAL.hittype[i].movflag = 0;
			GlobalMembersGLOBAL.hittype[i].tempang = 0;
			GlobalMembersGLOBAL.hittype[i].dispicnum = 0;
			GlobalMembersGLOBAL.hittype[i].floorz = sector[SECT].floorz;
			GlobalMembersGLOBAL.hittype[i].ceilingz = sector[SECT].ceilingz;

			GlobalMembersGLOBAL.hittype[i].lastvx = 0;
			GlobalMembersGLOBAL.hittype[i].lastvy = 0;
			GlobalMembersGLOBAL.hittype[i].actorstayput = -1;

			T1 = T2 = T3 = T4 = T5 = T6 = 0;

			if(PN != SPEAKER && PN != LETTER && PN != DUCK && PN != TARGET && PN != TRIPBOMB && PN != VIEWSCREEN && PN != VIEWSCREEN2 && (CS &48))
				if(!(PN >= CRACK1 && PN <= CRACK4))
			{
				if(SS == 127)
					return i;
				if(GlobalMembersGAME.wallswitchcheck(i) == 1 && (CS &16))
				{
					if(PN != ACCESSSWITCH && PN != ACCESSSWITCH2 && sprite[i].pal)
					{
						if((GlobalMembersGLOBAL.ud.multimode < 2) || (GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop==1))
						{
							sprite[i].xrepeat = sprite[i].yrepeat = 0;
							sprite[i].cstat = SLT = SHT = 0;
							return i;
						}
					}
					CS |= 257;
					if(sprite[i].pal && PN != ACCESSSWITCH && PN != ACCESSSWITCH2)
						sprite[i].pal = 0;
					return i;
				}

				if(SHT)
				{
					changespritestat(i,12);
					CS |= 257;
					SH = GlobalMembersGLOBAL.impact_damage;
					return i;
				}
			}

			s = PN;

			if(CS &1)
				CS |= 256;

			if (GlobalMembersGLOBAL.actorscrptr[s] != 0)
			{
				SH = *(GlobalMembersGLOBAL.actorscrptr[s]);
				T5 = *(GlobalMembersGLOBAL.actorscrptr[s]+1);
				T2 = *(GlobalMembersGLOBAL.actorscrptr[s]+2);
				if(*(GlobalMembersGLOBAL.actorscrptr[s]+3) && SHT == 0)
					SHT = *(GlobalMembersGLOBAL.actorscrptr[s]+3);
			}
			else
				T2 = T5 = 0;
		}

		sp = sprite[i];
		sect = sp.sectnum;

		switch(sp.picnum)
		{
				default:

					if (GlobalMembersGLOBAL.actorscrptr[sp.picnum] != 0)
					{
						if(j == -1 && sp.lotag > GlobalMembersGLOBAL.ud.player_skill)
						{
							sp.xrepeat=sp.yrepeat=0;
							changespritestat(i,5);
							break;
						}

							//  Init the size
						if(sp.xrepeat == 0 || sp.yrepeat == 0)
							sp.xrepeat = sp.yrepeat = 1;

						if(GlobalMembersGLOBAL.actortype[sp.picnum] & 3)
						{
							if(GlobalMembersGLOBAL.ud.monsters_off == 1)
							{
								sp.xrepeat=sp.yrepeat=0;
								changespritestat(i,5);
								break;
							}

							GlobalMembersGAMEDEF.makeitfall(i);

							if(GlobalMembersGLOBAL.actortype[sp.picnum] & 2)
								GlobalMembersGLOBAL.hittype[i].actorstayput = sp.sectnum;

							GlobalMembersGLOBAL.ps[myconnectindex].max_actors_killed++;
							sp.clipdist = 80;
							if(j >= 0)
							{
								if(sprite[j].picnum == RESPAWN)
									GlobalMembersGLOBAL.hittype[i].tempang = sprite[i].pal = sprite[j].pal;
								changespritestat(i,1);
							}
							else
								changespritestat(i,2);
						}
						else
						{
							sp.clipdist = 40;
							sp.owner = i;
							changespritestat(i,1);
						}

						GlobalMembersGLOBAL.hittype[i].timetosleep = 0;

						if(j >= 0)
							sp.ang = sprite[j].ang;
					}
					break;
				case FOF:
					sp.xrepeat = sp.yrepeat = 0;
					changespritestat(i,5);
					break;
				case WATERSPLASH2:
					if(j >= 0)
					{
						setsprite(i,sprite[j].x,sprite[j].y,sprite[j].z);
						sp.xrepeat = sp.yrepeat = 8+(TRAND &7);
					}
					else
						sp.xrepeat = sp.yrepeat = 16+(TRAND &15);

					sp.shade = -16;
					sp.cstat |= 128;
					if(j >= 0)
					{
						if(sector[sprite[j].sectnum].lotag == 2)
						{
							sp.z = getceilzofslope(SECT,SX,SY)+(16<<8);
							sp.cstat |= 8;
						}
						else if(sector[sprite[j].sectnum].lotag == 1)
							sp.z = getflorzofslope(SECT,SX,SY);
					}

					if(sector[sect].floorpicnum == FLOORSLIME || sector[sect].ceilingpicnum == FLOORSLIME)
							sp.pal = 7;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case NEON1:
				case NEON2:
				case NEON3:
				case NEON4:
				case NEON5:
				case NEON6:
				case DOMELITE:
					if(sp.picnum != WATERSPLASH2)
						sp.cstat |= 257;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case NUKEBUTTON:
					if(sp.picnum == DOMELITE)
						sp.cstat |= 257;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case JIBS1:
				case JIBS2:
				case JIBS3:
				case JIBS4:
				case JIBS5:
				case JIBS6:
				case HEADJIB1:
				case ARMJIB1:
				case LEGJIB1:
				case LIZMANHEAD1:
				case LIZMANARM1:
				case LIZMANLEG1:
				case DUKETORSO:
				case DUKEGUN:
				case DUKELEG:
					changespritestat(i,5);
					break;
				case TONGUE:
					if(j >= 0)
						sp.ang = sprite[j].ang;
					sp.z -= 38<<8;
					sp.zvel = 256-(TRAND &511);
					sp.xvel = 64-(TRAND &127);
					changespritestat(i,4);
					break;
				case NATURALLIGHTNING:
					sp.cstat &= ~257;
					sp.cstat |= 32768;
					break;
				case TRANSPORTERSTAR:
				case TRANSPORTERBEAM:
					if(j == -1)
						break;
					if(sp.picnum == TRANSPORTERBEAM)
					{
						sp.xrepeat = 31;
						sp.yrepeat = 1;
						sp.z = sector[sprite[j].sectnum].floorz-(40<<8);
					}
					else
					{
						if(sprite[j].statnum == 4)
						{
							sp.xrepeat = 8;
							sp.yrepeat = 8;
						}
						else
						{
							sp.xrepeat = 48;
							sp.yrepeat = 64;
							if(sprite[j].statnum == 10 || GlobalMembersGAME.badguy(ref sprite[j]) != 0)
								sp.z -= (32<<8);
						}
					}

					sp.shade = -127;
					sp.cstat = 128|2;
					sp.ang = sprite[j].ang;

					sp.xvel = 128;
					changespritestat(i,5);
					GlobalMembersACTORS.ssp(i, CLIPMASK0);
					setsprite(i,sp.x,sp.y,sp.z);
					break;

				case FRAMEEFFECT1:
					if(j >= 0)
					{
						sp.xrepeat = sprite[j].xrepeat;
						sp.yrepeat = sprite[j].yrepeat;
						T2 = sprite[j].picnum;
					}
					else
						sp.xrepeat = sp.yrepeat = 0;

					changespritestat(i,5);

					break;

				case LASERLINE:
					sp.yrepeat = 6;
					sp.xrepeat = 32;

					if(GlobalMembersGLOBAL.lasermode == 1)
						sp.cstat = 16 + 2;
					else if(GlobalMembersGLOBAL.lasermode == 0 || GlobalMembersGLOBAL.lasermode == 2)
						sp.cstat = 16;
					else
					{
						sp.xrepeat = 0;
						sp.yrepeat = 0;
					}

					if(j >= 0)
						sp.ang = GlobalMembersGLOBAL.hittype[j].temp_data[5]+512;
					changespritestat(i,5);
					break;

				case FORCESPHERE:
					if(j == -1)
					{
						sp.cstat = (short) 32768;
						changespritestat(i,2);
					}
					else
					{
						sp.xrepeat = sp.yrepeat = 1;
						changespritestat(i,5);
					}
					break;

				case BLOOD:
				   sp.xrepeat = sp.yrepeat = 16;
				   sp.z -= (26<<8);
				   if(j >= 0 && sprite[j].pal == 6)
					   sp.pal = 6;
				   changespritestat(i,5);
				   break;
				case BLOODPOOL:
				case PUKE:
					{
						short s1;
						s1 = sp.sectnum;

						updatesector(sp.x+108, sp.y+108, s1);
						if(s1 >= 0 && sector[s1].floorz == sector[sp.sectnum].floorz)
						{
							updatesector(sp.x-108, sp.y-108, s1);
							if(s1 >= 0 && sector[s1].floorz == sector[sp.sectnum].floorz)
							{
								updatesector(sp.x+108, sp.y-108, s1);
								if(s1 >= 0 && sector[s1].floorz == sector[sp.sectnum].floorz)
								{
									updatesector(sp.x-108, sp.y+108, s1);
									if(s1 >= 0 && sector[s1].floorz != sector[sp.sectnum].floorz)
									{
										sp.xrepeat = sp.yrepeat = 0;
										changespritestat(i,5);
										break;
									}
								}
								else
								{
									sp.xrepeat = sp.yrepeat = 0;
									changespritestat(i,5);
									break;
								}
							}
							else
							{
								sp.xrepeat = sp.yrepeat = 0;
								changespritestat(i,5);
								break;
							}
						}
						else
						{
							sp.xrepeat = sp.yrepeat = 0;
							changespritestat(i,5);
							break;
						}
					}

					if(sector[SECT].lotag == 1)
					{
						changespritestat(i,5);
						break;
					}

					if(j >= 0 && sp.picnum != PUKE)
					{
						if(sprite[j].pal == 1)
							sp.pal = 1;
						else if(sprite[j].pal != 6 && sprite[j].picnum != NUKEBARREL && sprite[j].picnum != TIRE)
						{
							if(sprite[j].picnum == FECES)
								sp.pal = 7; // Brown
							else // Red
								sp.pal = 2;
						}
						else // green
							sp.pal = 0;

						if(sprite[j].picnum == TIRE)
							sp.shade = 127;
					}
					sp.cstat |= 32;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case FECES:
					if(j >= 0)
						sp.xrepeat = sp.yrepeat = 1;
					changespritestat(i,5);
					break;

				case BLOODSPLAT1:
				case BLOODSPLAT2:
				case BLOODSPLAT3:
				case BLOODSPLAT4:
					sp.cstat |= 16;
					sp.xrepeat = 7+(TRAND &7);
					sp.yrepeat = 7+(TRAND &7);
					sp.z -= (16<<8);
					if(j >= 0 && sprite[j].pal == 6)
						sp.pal = 6;
					GlobalMembersACTORS.insertspriteq(i);
					changespritestat(i,5);
					break;

				case TRIPBOMB:
					if(sp.lotag > GlobalMembersGLOBAL.ud.player_skill)
					{
						sp.xrepeat=sp.yrepeat=0;
						changespritestat(i,5);
						break;
					}

					sp.xrepeat=4;
					sp.yrepeat=5;

					sp.owner = i;
					sp.hitag = i;

					sp.xvel = 16;
					GlobalMembersACTORS.ssp(i, CLIPMASK0);
					GlobalMembersGLOBAL.hittype[i].temp_data[0] = 17;
					GlobalMembersGLOBAL.hittype[i].temp_data[2] = 0;
					GlobalMembersGLOBAL.hittype[i].temp_data[5] = sp.ang;

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case SPACEMARINE:
					if(sp.picnum == SPACEMARINE)
					{
						sp.extra = 20;
						sp.cstat |= 257;
					}
					changespritestat(i,2);
					break;

				case HYDRENT:
				case PANNEL1:
				case PANNEL2:
				case SATELITE:
				case FUELPOD:
				case SOLARPANNEL:
				case ANTENNA:
				case GRATE1:
				case CHAIR1:
				case CHAIR2:
				case CHAIR3:
				case BOTTLE1:
				case BOTTLE2:
				case BOTTLE3:
				case BOTTLE4:
				case BOTTLE5:
				case BOTTLE6:
				case BOTTLE7:
				case BOTTLE8:
				case BOTTLE10:
				case BOTTLE11:
				case BOTTLE12:
				case BOTTLE13:
				case BOTTLE14:
				case BOTTLE15:
				case BOTTLE16:
				case BOTTLE17:
				case BOTTLE18:
				case BOTTLE19:
				case OCEANSPRITE1:
				case OCEANSPRITE2:
				case OCEANSPRITE3:
				case OCEANSPRITE5:
				case MONK:
				case INDY:
				case LUKE:
				case JURYGUY:
				case SCALE:
				case VACUUM:
				case FANSPRITE:
				case CACTUS:
				case CACTUSBROKE:
				case HANGLIGHT:
				case FETUS:
				case FETUSBROKE:
				case CAMERALIGHT:
				case MOVIECAMERA:
				case IVUNIT:
				case POT1:
				case POT2:
				case POT3:
				case TRIPODCAMERA:
				case SUSHIPLATE1:
				case SUSHIPLATE2:
				case SUSHIPLATE3:
				case SUSHIPLATE4:
				case SUSHIPLATE5:
				case WAITTOBESEATED:
				case VASE:
				case PIPE1:
				case PIPE2:
				case PIPE3:
				case PIPE4:
				case PIPE5:
				case PIPE6:
					sp.clipdist = 32;
					sp.cstat |= 257;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case OCEANSPRITE4:
					changespritestat(i,0);
					break;
				case FEMMAG1:
				case FEMMAG2:
					sp.cstat &= ~257;
					changespritestat(i,0);
					break;
				case DUKETAG:
				case SIGN1:
				case SIGN2:
					if(GlobalMembersGLOBAL.ud.multimode < 2 && sp.pal)
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
					}
					else
						sp.pal = 0;
					break;
				case MASKWALL1:
				case MASKWALL2:
				case MASKWALL3:
				case MASKWALL4:
				case MASKWALL5:
				case MASKWALL6:
				case MASKWALL7:
				case MASKWALL8:
				case MASKWALL9:
				case MASKWALL10:
				case MASKWALL11:
				case MASKWALL12:
				case MASKWALL13:
				case MASKWALL14:
				case MASKWALL15:
					j = sp.cstat &60;
					sp.cstat = j|1;
					changespritestat(i,0);
					break;
				case FOOTPRINTS:
				case FOOTPRINTS2:
				case FOOTPRINTS3:
				case FOOTPRINTS4:
					if(j >= 0)
					{
						short s1;
						s1 = sp.sectnum;

						updatesector(sp.x+84, sp.y+84, s1);
						if(s1 >= 0 && sector[s1].floorz == sector[sp.sectnum].floorz)
						{
							updatesector(sp.x-84, sp.y-84, s1);
							if(s1 >= 0 && sector[s1].floorz == sector[sp.sectnum].floorz)
							{
								updatesector(sp.x+84, sp.y-84, s1);
								if(s1 >= 0 && sector[s1].floorz == sector[sp.sectnum].floorz)
								{
									updatesector(sp.x-84, sp.y+84, s1);
									if(s1 >= 0 && sector[s1].floorz != sector[sp.sectnum].floorz)
									{
										sp.xrepeat = sp.yrepeat = 0;
										changespritestat(i,5);
										break;
									}
								}
								else
								{
									sp.xrepeat = sp.yrepeat = 0;
									break;
								}
							}
							else
							{
								sp.xrepeat = sp.yrepeat = 0;
								break;
							}
						}
						else
						{
							sp.xrepeat = sp.yrepeat = 0;
							break;
						}

						sp.cstat = 32+((GlobalMembersGLOBAL.ps[sprite[j].yvel].footprintcount &1)<<2);
						sp.ang = sprite[j].ang;
					}

					sp.z = sector[sect].floorz;
					if(sector[sect].lotag != 1 && sector[sect].lotag != 2)
						sp.xrepeat = sp.yrepeat = 32;

					GlobalMembersACTORS.insertspriteq(i);
					changespritestat(i,5);
					break;

				case FEM1:
				case FEM2:
				case FEM3:
				case FEM4:
				case FEM5:
				case FEM6:
				case FEM7:
				case FEM8:
				case FEM9:
				case FEM10:
				case PODFEM1:
				case NAKED1:
				case STATUE:
				case TOUGHGAL:
					sp.yvel = sp.hitag;
					sp.hitag = -1;
					if(sp.picnum == PODFEM1)
						sp.extra <<= 1;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case BLOODYPOLE:

				case QUEBALL:
				case STRIPEBALL:

					if(sp.picnum == QUEBALL || sp.picnum == STRIPEBALL)
					{
						sp.cstat = 256;
						sp.clipdist = 8;
					}
					else
					{
						sp.cstat |= 257;
						sp.clipdist = 32;
					}

					changespritestat(i,2);
					break;

				case DUKELYINGDEAD:
					if(j >= 0 && sprite[j].picnum == APLAYER)
					{
						sp.xrepeat = sprite[j].xrepeat;
						sp.yrepeat = sprite[j].yrepeat;
						sp.shade = sprite[j].shade;
						sp.pal = GlobalMembersGLOBAL.ps[sprite[j].yvel].palookup;
					}
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case DUKECAR:
				case HELECOPT:
	//                if(sp->picnum == HELECOPT || sp->picnum == DUKECAR) sp->xvel = 1024;
					sp.cstat = 0;
					sp.extra = 1;
					sp.xvel = 292;
					sp.zvel = 360;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case RESPAWNMARKERRED:
				case BLIMP:

					if(sp.picnum == RESPAWNMARKERRED)
					{
						sp.xrepeat = sp.yrepeat = 24;
						if(j >= 0) // -(1<<4);
							sp.z = GlobalMembersGLOBAL.hittype[j].floorz;
					}
					else
					{
						sp.cstat |= 257;
						sp.clipdist = 128;
					}
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case MIKE:
					if(sp.picnum == MIKE)
						sp.yvel = sp.hitag;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case WEATHERWARN:
					changespritestat(i,1);
					break;

				case SPOTLITE:
					T1 = sp.x;
					T2 = sp.y;
					break;
				case BULLETHOLE:
					sp.xrepeat = sp.yrepeat = 3;
					sp.cstat = 16+(krand()&12);
					GlobalMembersACTORS.insertspriteq(i);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case MONEY:
				case MAIL:
				case PAPER:
					if(sp.picnum == MONEY || sp.picnum == MAIL || sp.picnum == PAPER)
					{
						GlobalMembersGLOBAL.hittype[i].temp_data[0] = TRAND &2047;
						sp.cstat = TRAND &12;
						sp.xrepeat = sp.yrepeat = 8;
						sp.ang = TRAND &2047;
					}
					changespritestat(i,5);
					break;

				case VIEWSCREEN:
				case VIEWSCREEN2:
					sp.owner = i;
					sp.lotag = 1;
					sp.extra = 1;
					changespritestat(i,6);
					break;

				case SHELL: //From the player
				case SHOTGUNSHELL:
					if(j >= 0)
					{
						short snum;
						short a;

						if(sprite[j].picnum == APLAYER)
						{
							snum = sprite[j].yvel;
							a = GlobalMembersGLOBAL.ps[snum].ang-(TRAND &63)+8; //Fine tune

							T1 = TRAND &1;
							if(sp.picnum == SHOTGUNSHELL)
								sp.z = (6<<8)+GlobalMembersGLOBAL.ps[snum].pyoff+GlobalMembersGLOBAL.ps[snum].posz-((GlobalMembersGLOBAL.ps[snum].horizoff+GlobalMembersGLOBAL.ps[snum].horiz-100)<<4);
							else
								sp.z = (3<<8)+GlobalMembersGLOBAL.ps[snum].pyoff+GlobalMembersGLOBAL.ps[snum].posz-((GlobalMembersGLOBAL.ps[snum].horizoff+GlobalMembersGLOBAL.ps[snum].horiz-100)<<4);
							sp.zvel = -(TRAND &255);
						}
						else
						{
							a = sp.ang;
							sp.z = sprite[j].z-PHEIGHT+(3<<8);
						}

						sp.x = sprite[j].x+(sintable[(a+512)&2047]>>7);
						sp.y = sprite[j].y+(sintable[a &2047]>>7);

						sp.shade = -8;

						sp.ang = a-512;
						sp.xvel = 20;

						sp.xrepeat=sp.yrepeat=4;

						changespritestat(i,5);
					}
					break;

				case RESPAWN:
					sp.extra = 66-13;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case MUSICANDSFX:
					if(GlobalMembersGLOBAL.ud.multimode < 2 && sp.pal == 1)
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
						break;
					}
					sp.cstat = (short)32768;
					changespritestat(i,11);
					break;

				case EXPLOSION2:
				case EXPLOSION2BOT:
				case BURNING:
				case BURNING2:
				case SMALLSMOKE:
				case SHRINKEREXPLOSION:
				case COOLEXPLOSION1:

					if(j >= 0)
					{
						sp.ang = sprite[j].ang;
						sp.shade = -64;
						sp.cstat = 128|(TRAND &4);
					}

					if(sp.picnum == EXPLOSION2 || sp.picnum == EXPLOSION2BOT)
					{
						sp.xrepeat = 48;
						sp.yrepeat = 48;
						sp.shade = -127;
						sp.cstat |= 128;
					}
					else if(sp.picnum == SHRINKEREXPLOSION)
					{
						sp.xrepeat = 32;
						sp.yrepeat = 32;
					}
					else if(sp.picnum == SMALLSMOKE)
					{
						// 64 "money"
						sp.xrepeat = 24;
						sp.yrepeat = 24;
					}
					else if(sp.picnum == BURNING || sp.picnum == BURNING2)
					{
						sp.xrepeat = 4;
						sp.yrepeat = 4;
					}

					if(j >= 0)
					{
						x = getflorzofslope(sp.sectnum,sp.x,sp.y);
						if(sp.z > x-(12<<8))
							sp.z = x-(12<<8);
					}

					changespritestat(i,5);

					break;

				case PLAYERONWATER:
					if(j >= 0)
					{
						sp.xrepeat = sprite[j].xrepeat;
						sp.yrepeat = sprite[j].yrepeat;
						sp.zvel = 128;
						if(sector[sp.sectnum].lotag != 2)
							sp.cstat |= 32768;
					}
					changespritestat(i,13);
					break;

				case APLAYER:
					sp.xrepeat = sp.yrepeat = 0;
					j = GlobalMembersGLOBAL.ud.coop;
					if(j == 2)
						j = 0;

					if(GlobalMembersGLOBAL.ud.multimode < 2 || (GlobalMembersGLOBAL.ud.multimode > 1 && j != sp.lotag))
						changespritestat(i,5);
					else
						changespritestat(i,10);
					break;
				case WATERBUBBLE:
					if(j >= 0 && sprite[j].picnum == APLAYER)
						sp.z -= (16<<8);
					if(sp.picnum == WATERBUBBLE)
					{
						if(j >= 0)
							sp.ang = sprite[j].ang;
						sp.xrepeat = sp.yrepeat = 4;
					}
					else
						sp.xrepeat = sp.yrepeat = 32;

					changespritestat(i,5);
					break;

				case CRANE:

					sp.cstat |= 64|257;

					sp.picnum += 2;
					sp.z = sector[sect].ceilingz+(48<<8);
					T5 = tempwallptr;

					GlobalMembersGLOBAL.msx[tempwallptr] = sp.x;
					GlobalMembersGLOBAL.msy[tempwallptr] = sp.y;
					GlobalMembersGLOBAL.msx[tempwallptr+2] = sp.z;

					s = headspritestat[0];
					while(s >= 0)
					{
						if(sprite[s].picnum == CRANEPOLE && SHT == (sprite[s].hitag))
						{
							GlobalMembersGLOBAL.msy[tempwallptr+2] = s;

							T2 = sprite[s].sectnum;

							sprite[s].xrepeat = 48;
							sprite[s].yrepeat = 128;

							GlobalMembersGLOBAL.msx[tempwallptr+1] = sprite[s].x;
							GlobalMembersGLOBAL.msy[tempwallptr+1] = sprite[s].y;

							sprite[s].x = sp.x;
							sprite[s].y = sp.y;
							sprite[s].z = sp.z;
							sprite[s].shade = sp.shade;

							setsprite(s,sprite[s].x,sprite[s].y,sprite[s].z);
							break;
						}
						s = nextspritestat[s];
					}

					tempwallptr += 3;
					sp.owner = -1;
					sp.extra = 8;
					changespritestat(i,6);
					break;

				case WATERDRIP:
					if(j >= 0 && sprite[j].statnum == 10 || sprite[j].statnum == 1)
					{
						sp.shade = 32;
						if(sprite[j].pal != 1)
						{
							sp.pal = 2;
							sp.z -= (18<<8);
						}
						else
							sp.z -= (13<<8);
						sp.ang = getangle(GlobalMembersGLOBAL.ps[connecthead].posx-sp.x,GlobalMembersGLOBAL.ps[connecthead].posy-sp.y);
						sp.xvel = 48-(TRAND &31);
						GlobalMembersACTORS.ssp(i, CLIPMASK0);
					}
					else if(j == -1)
					{
						sp.z += (4<<8);
						T1 = sp.z;
						T2 = TRAND &127;
					}
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case TRASH:

					if(sp.picnum != WATERDRIP)
						sp.ang = TRAND &2047;

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case WATERDRIPSPLASH:

					sp.xrepeat = 24;
					sp.yrepeat = 24;


					changespritestat(i,6);
					break;

				case PLUG:
					sp.lotag = 9999;
					changespritestat(i,6);
					break;
				case TOUCHPLATE:
					T3 = sector[sect].floorz;
					if(sector[sect].lotag != 1 && sector[sect].lotag != 2)
						sector[sect].floorz = sp.z;
					if(sp.pal && GlobalMembersGLOBAL.ud.multimode > 1)
					{
						sp.xrepeat=sp.yrepeat=0;
						changespritestat(i,5);
						break;
					}
				case WATERBUBBLEMAKER:
					sp.cstat |= 32768;
					changespritestat(i,6);
					break;
				case BOLT1:
				case BOLT1+1:
				case BOLT1+2:
				case BOLT1+3:
				case SIDEBOLT1:
				case SIDEBOLT1+1:
				case SIDEBOLT1+2:
				case SIDEBOLT1+3:
					T1 = sp.xrepeat;
					T2 = sp.yrepeat;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case MASTERSWITCH:
					if(sp.picnum == MASTERSWITCH)
						sp.cstat |= 32768;
					sp.yvel = 0;
					changespritestat(i,6);
					break;
				case TARGET:
				case DUCK:
				case LETTER:
					sp.extra = 1;
					sp.cstat |= 257;
					changespritestat(i,1);
					break;
				case OCTABRAINSTAYPUT:
				case LIZTROOPSTAYPUT:
				case PIGCOPSTAYPUT:
				case LIZMANSTAYPUT:
				case BOSS1STAYPUT:
				case PIGCOPDIVE:
				case COMMANDERSTAYPUT:
				case BOSS4STAYPUT:
					GlobalMembersGLOBAL.hittype[i].actorstayput = sp.sectnum;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case BOSS1:
				case BOSS2:
				case BOSS3:
				case BOSS4:
				case ROTATEGUN:
				case GREENSLIME:
					if(sp.picnum == GREENSLIME)
						sp.extra = 1;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case DRONE:
				case LIZTROOPONTOILET:
				case LIZTROOPJUSTSIT:
				case LIZTROOPSHOOT:
				case LIZTROOPJETPACK:
				case LIZTROOPDUCKING:
				case LIZTROOPRUNNING:
				case LIZTROOP:
				case OCTABRAIN:
				case COMMANDER:
				case PIGCOP:
				case LIZMAN:
				case LIZMANSPITTING:
				case LIZMANFEEDING:
				case LIZMANJUMP:
				case ORGANTIC:
				case RAT:
				case SHARK:

					if(sp.pal == 0)
					{
						switch(sp.picnum)
						{
							case LIZTROOPONTOILET:
							case LIZTROOPSHOOT:
							case LIZTROOPJETPACK:
							case LIZTROOPDUCKING:
							case LIZTROOPRUNNING:
							case LIZTROOPSTAYPUT:
							case LIZTROOPJUSTSIT:
							case LIZTROOP:
								sp.pal = 22;
								break;
						}
					}

					if(sp.picnum == BOSS4STAYPUT || sp.picnum == BOSS1 || sp.picnum == BOSS2 || sp.picnum == BOSS1STAYPUT || sp.picnum == BOSS3 || sp.picnum == BOSS4)
					{
						if(j >= 0 && sprite[j].picnum == RESPAWN)
							sp.pal = sprite[j].pal;
						if(sp.pal)
						{
							sp.clipdist = 80;
							sp.xrepeat = 40;
							sp.yrepeat = 40;
						}
						else
						{
							sp.xrepeat = 80;
							sp.yrepeat = 80;
							sp.clipdist = 164;
						}
					}
					else
					{
						if(sp.picnum != SHARK)
						{
							sp.xrepeat = 40;
							sp.yrepeat = 40;
							sp.clipdist = 80;
						}
						else
						{
							sp.xrepeat = 60;
							sp.yrepeat = 60;
							sp.clipdist = 40;
						}
					}

					if(j >= 0)
						sp.lotag = 0;

					if((sp.lotag > GlobalMembersGLOBAL.ud.player_skill) || GlobalMembersGLOBAL.ud.monsters_off == 1)
					{
						sp.xrepeat=sp.yrepeat=0;
						changespritestat(i,5);
						break;
					}
					else
					{
						GlobalMembersGAMEDEF.makeitfall(i);

						if(sp.picnum == RAT)
						{
							sp.ang = TRAND &2047;
							sp.xrepeat = sp.yrepeat = 48;
							sp.cstat = 0;
						}
						else
						{
							sp.cstat |= 257;

							if(sp.picnum != SHARK)
								GlobalMembersGLOBAL.ps[myconnectindex].max_actors_killed++;
						}

						if(sp.picnum == ORGANTIC)
							sp.cstat |= 128;

						if(j >= 0)
						{
							GlobalMembersGLOBAL.hittype[i].timetosleep = 0;
							GlobalMembersGAME.check_fta_sounds(i);
							changespritestat(i,1);
						}
						else
							changespritestat(i,2);
					}

					if(sp.picnum == ROTATEGUN)
						sp.zvel = 0;

					break;

				case LOCATORS:
					sp.cstat |= 32768;
					changespritestat(i,7);
					break;

				case ACTIVATORLOCKED:
				case ACTIVATOR:
					sp.cstat = (short) 32768;
					if(sp.picnum == ACTIVATORLOCKED)
						sector[sp.sectnum].lotag |= 16384;
					changespritestat(i,8);
					break;

				case DOORSHOCK:
					sp.cstat |= 1+256;
					sp.shade = -12;
					changespritestat(i,6);
					break;

				case OOZ:
				case OOZ2:
					sp.shade = -12;

					if(j >= 0)
					{
						if(sprite[j].picnum == NUKEBARREL)
							sp.pal = 8;
						GlobalMembersACTORS.insertspriteq(i);
					}

					changespritestat(i,1);

					GlobalMembersGAMEDEF.getglobalz(i);

					j = (GlobalMembersGLOBAL.hittype[i].floorz-GlobalMembersGLOBAL.hittype[i].ceilingz)>>9;

					sp.yrepeat = j;
					sp.xrepeat = 25-(j>>1);
					sp.cstat |= (TRAND &4);

					break;

				case HEAVYHBOMB:
					if(j >= 0)
						sp.owner = j;
					else
						sp.owner = i;
					sp.xrepeat = sp.yrepeat = 9;
					sp.yvel = 4;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case REACTOR2:
				case REACTOR:
				case RECON:

					if(sp.picnum == RECON)
					{
						if(sp.lotag > GlobalMembersGLOBAL.ud.player_skill)
						{
							sp.xrepeat = sp.yrepeat = 0;
							changespritestat(i,5);
							return i;
						}
						GlobalMembersGLOBAL.ps[myconnectindex].max_actors_killed++;
						GlobalMembersGLOBAL.hittype[i].temp_data[5] = 0;
						if(GlobalMembersGLOBAL.ud.monsters_off == 1)
						{
							sp.xrepeat = sp.yrepeat = 0;
							changespritestat(i,5);
							break;
						}
						sp.extra = 130;
					}

					if(sp.picnum == REACTOR || sp.picnum == REACTOR2)
						sp.extra = GlobalMembersGLOBAL.impact_damage;

					CS |= 257; // Make it hitable

					if(GlobalMembersGLOBAL.ud.multimode < 2 && sp.pal != 0)
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
						break;
					}
					sp.pal = 0;
					SS = -17;

					changespritestat(i,2);
					break;

				case ATOMICHEALTH:
				case STEROIDS:
				case HEATSENSOR:
				case SHIELD:
				case AIRTANK:
				case TRIPBOMBSPRITE:
				case JETPACK:
				case HOLODUKE:

				case FIRSTGUNSPRITE:
				case CHAINGUNSPRITE:
				case SHOTGUNSPRITE:
				case RPGSPRITE:
				case SHRINKERSPRITE:
				case FREEZESPRITE:
				case DEVISTATORSPRITE:

				case SHOTGUNAMMO:
				case FREEZEAMMO:
				case HBOMBAMMO:
				case CRYSTALAMMO:
				case GROWAMMO:
				case BATTERYAMMO:
				case DEVISTATORAMMO:
				case RPGAMMO:
				case BOOTS:
				case AMMO:
				case AMMOLOTS:
				case COLA:
				case FIRSTAID:
				case SIXPAK:
					if(j >= 0)
					{
						sp.lotag = 0;
						sp.z -= (32<<8);
						sp.zvel = -1024;
						GlobalMembersACTORS.ssp(i, CLIPMASK0);
						sp.cstat = TRAND &4;
					}
					else
					{
						sp.owner = i;
						sp.cstat = 0;
					}

					if((GlobalMembersGLOBAL.ud.multimode < 2 && sp.pal != 0) || (sp.lotag > GlobalMembersGLOBAL.ud.player_skill))
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
						break;
					}

					sp.pal = 0;

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case ACCESSCARD:

					if(sp.picnum == ATOMICHEALTH)
						sp.cstat |= 128;

					if(GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.coop != 1 && sp.picnum == ACCESSCARD)
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
						break;
					}
					else
					{
						if(sp.picnum == AMMO)
							sp.xrepeat = sp.yrepeat = 16;
						else
							sp.xrepeat = sp.yrepeat = 32;
					}

					sp.shade = -17;

					if(j >= 0)
						changespritestat(i,1);
					else
					{
						changespritestat(i,2);
						GlobalMembersGAMEDEF.makeitfall(i);
					}
					break;

				case WATERFOUNTAIN:
					SLT = 1;

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case TREE1:
				case TREE2:
				case TIRE:
				case CONE:
				case BOX:
					CS = 257; // Make it hitable
					sprite[i].extra = 1;
					changespritestat(i,6);
					break;

				case FLOORFLAME:
					sp.shade = -127;
					changespritestat(i,6);
					break;

				case BOUNCEMINE:
					sp.owner = i;
					sp.cstat |= 1+256; //Make it hitable
					sp.xrepeat = sp.yrepeat = 24;
					sp.shade = -127;
					sp.extra = GlobalMembersGLOBAL.impact_damage<<2;
					changespritestat(i,2);
					break;

				case CAMERA1:
				case CAMERA1+1:
				case CAMERA1+2:
				case CAMERA1+3:
				case CAMERA1+4:
				case CAMERAPOLE:
					sp.extra = 1;

					if (GlobalMembersGLOBAL.camerashitable != 0)
						sp.cstat = 257;
					else
						sp.cstat = 0;

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case GENERICPOLE:

					if(GlobalMembersGLOBAL.ud.multimode < 2 && sp.pal != 0)
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
						break;
					}
					else
						sp.pal = 0;
					if(sp.picnum == CAMERAPOLE || sp.picnum == GENERICPOLE)
						break;
					sp.picnum = CAMERA1;
					changespritestat(i,1);
					break;
				case STEAM:
					if(j >= 0)
					{
						sp.ang = sprite[j].ang;
						sp.cstat = 16+128+2;
						sp.xrepeat=sp.yrepeat=1;
						sp.xvel = -8;
						GlobalMembersACTORS.ssp(i, CLIPMASK0);
					}
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case CEILINGSTEAM:
					changespritestat(i,6);
					break;

				case SECTOREFFECTOR:
					sp.yvel = sector[sect].extra;
					sp.cstat |= 32768;
					sp.xrepeat = sp.yrepeat = 0;

					switch(sp.lotag)
					{
						case 28:
							T6 = 65; // Delay for lightning
							break;
						case 7: // Transporters!!!!
						case 23: // XPTR END
							if(sp.lotag != 23)
							{
								for(j = 0;j<MAXSPRITES;j++)
									if(sprite[j].statnum < MAXSTATUS && sprite[j].picnum == SECTOREFFECTOR && (sprite[j].lotag == 7 || sprite[j].lotag == 23) && i != j && sprite[j].hitag == SHT)
									{
										OW = j;
										break;
									}
							}
							else
								OW = i;

							T5 = sector[sect].floorz == SZ;
							sp.cstat = 0;
							changespritestat(i,9);
							return i;
						case 1:
							sp.owner = -1;
							T1 = 1;
							break;
						case 18:

							if(sp.ang == 512)
							{
								T2 = sector[sect].ceilingz;
								if(sp.pal)
									sector[sect].ceilingz = sp.z;
							}
							else
							{
								T2 = sector[sect].floorz;
								if(sp.pal)
									sector[sect].floorz = sp.z;
							}

							sp.hitag <<= 2;
							break;

						case 19:
							sp.owner = -1;
							break;
						case 25: // Pistons
							T4 = sector[sect].ceilingz;
							T5 = 1;
							sector[sect].ceilingz = sp.z;
							GlobalMembersACTORS.setinterpolation(ref sector[sect].ceilingz);
							break;
						case 35:
							sector[sect].ceilingz = sp.z;
							break;
						case 27:
							if(GlobalMembersGLOBAL.ud.recstat == 1)
							{
								sp.xrepeat=sp.yrepeat=64;
								sp.cstat &= 32767;
							}
							break;
						case 12:

							T2 = sector[sect].floorshade;
							T3 = sector[sect].ceilingshade;
							break;

						case 13:

							T1 = sector[sect].ceilingz;
							T2 = sector[sect].floorz;

							if(klabs(T1-sp.z) < klabs(T2-sp.z))
								sp.owner = 1;
							else
								sp.owner = 0;

							if(sp.ang == 512)
							{
								if(sp.owner)
									sector[sect].ceilingz = sp.z;
								else
									sector[sect].floorz = sp.z;
							}
							else
								sector[sect].ceilingz = sector[sect].floorz = sp.z;

							if(sector[sect].ceilingstat &1)
							{
								sector[sect].ceilingstat ^= 1;
								T4 = 1;

								if(!sp.owner && sp.ang==512)
								{
									sector[sect].ceilingstat ^= 1;
									T4 = 0;
								}

								sector[sect].ceilingshade = sector[sect].floorshade;

								if(sp.ang==512)
								{
									startwall = sector[sect].wallptr;
									endwall = startwall+sector[sect].wallnum;
									for(j = startwall;j<endwall;j++)
									{
										x = wall[j].nextsector;
										if(x >= 0)
											if(!(sector[x].ceilingstat &1))
										{
											sector[sect].ceilingpicnum = sector[x].ceilingpicnum;
											sector[sect].ceilingshade = sector[x].ceilingshade;
											break; //Leave earily
										}
									}
								}
							}

							break;

						case 17:

							T3 = sector[sect].floorz; //Stopping loc

							j = nextsectorneighborz(sect,sector[sect].floorz,-1,-1);
							T4 = sector[j].ceilingz;

							j = nextsectorneighborz(sect,sector[sect].ceilingz,1,1);
							T5 = sector[j].floorz;

							if(numplayers < 2)
							{
								GlobalMembersACTORS.setinterpolation(ref sector[sect].floorz);
								GlobalMembersACTORS.setinterpolation(ref sector[sect].ceilingz);
							}

							break;

						case 24:
							sp.yvel <<= 1;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 36:
							break;

						case 20:
						{
							int q;

							startwall = sector[sect].wallptr;
							endwall = startwall+sector[sect].wallnum;

							//find the two most clostest wall x's and y's
							q = 0x7fffffff;

							for(s = startwall;s<endwall;s++)
							{
								x = wall[s].x;
								y = wall[s].y;

								d = FindDistance2D(sp.x-x,sp.y-y);
								if(d < q)
								{
									q = d;
									clostest = s;
								}
							}

							T2 = clostest;

							q = 0x7fffffff;

							for(s = startwall;s<endwall;s++)
							{
								x = wall[s].x;
								y = wall[s].y;

								d = FindDistance2D(sp.x-x,sp.y-y);
								if(d < q && s != T2)
								{
									q = d;
									clostest = s;
								}
							}

							T3 = clostest;
						}

						break;

						case 3:

							T4=sector[sect].floorshade;

							sector[sect].floorshade = sp.shade;
							sector[sect].ceilingshade = sp.shade;

							sp.owner = sector[sect].ceilingpal<<8;
							sp.owner |= sector[sect].floorpal;

							//fix all the walls;

							startwall = sector[sect].wallptr;
							endwall = startwall+sector[sect].wallnum;

							for(s = startwall;s<endwall;s++)
							{
								if(!(wall[s].hitag &1))
									wall[s].shade=sp.shade;
								if((wall[s].cstat &2) && wall[s].nextwall >= 0)
									wall[wall[s].nextwall].shade = sp.shade;
							}
							break;

						case 31:
							T2 = sector[sect].floorz;
						//    T3 = sp->hitag;
							if(sp.ang != 1536)
								sector[sect].floorz = sp.z;

							startwall = sector[sect].wallptr;
							endwall = startwall+sector[sect].wallnum;

							for(s = startwall;s<endwall;s++)
								if(wall[s].hitag == 0)
									wall[s].hitag = 9999;

							GlobalMembersACTORS.setinterpolation(ref sector[sect].floorz);

							break;
						case 32:
							T2 = sector[sect].ceilingz;
							T3 = sp.hitag;
							if(sp.ang != 1536)
								sector[sect].ceilingz = sp.z;

							startwall = sector[sect].wallptr;
							endwall = startwall+sector[sect].wallnum;

							for(s = startwall;s<endwall;s++)
								if(wall[s].hitag == 0)
									wall[s].hitag = 9999;

							GlobalMembersACTORS.setinterpolation(ref sector[sect].ceilingz);

							break;

						case 4: //Flashing lights

							T3 = sector[sect].floorshade;

							startwall = sector[sect].wallptr;
							endwall = startwall+sector[sect].wallnum;

							sp.owner = sector[sect].ceilingpal<<8;
							sp.owner |= sector[sect].floorpal;

							for(s = startwall;s<endwall;s++)
								if(wall[s].shade > T4)
									T4 = wall[s].shade;

							break;

						case 9:
							if(sector[sect].lotag && Math.Abs(sector[sect].ceilingz-sp.z) > 1024)
									sector[sect].lotag |= 32768; //If its open
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 8:
							//First, get the ceiling-floor shade

							T1 = sector[sect].floorshade;
							T2 = sector[sect].ceilingshade;

							startwall = sector[sect].wallptr;
							endwall = startwall+sector[sect].wallnum;

							for(s = startwall;s<endwall;s++)
								if(wall[s].shade > T3)
									T3 = wall[s].shade;

							T4 = 1; //Take Out;

							break;

						case 11: //Pivitor rotater
							if(sp.ang>1024)
								T4 = 2;
							else
								T4 = -2;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 0:
						case 2: //Earthquakemakers
						case 5: //Boss Creature
						case 6: //Subway
						case 14: //Caboos
						case 15: //Subwaytype sliding door
						case 16: //That rotating blocker reactor thing
						case 26: //ESCELATOR
						case 30: //No rotational subways

							if(sp.lotag == 0)
							{
								if(sector[sect].lotag == 30)
								{
									if(sp.pal)
										sprite[i].clipdist = 1;
									else
										sprite[i].clipdist = 0;
									T4 = sector[sect].floorz;
									sector[sect].hitag = i;
								}

								for(j = 0;j < MAXSPRITES;j++)
								{
									if(sprite[j].statnum < MAXSTATUS)
									if(sprite[j].picnum == SECTOREFFECTOR && sprite[j].lotag == 1 && sprite[j].hitag == sp.hitag)
									{
										if(sp.ang == 512)
										{
											sp.x = sprite[j].x;
											sp.y = sprite[j].y;
										}
										break;
									}
								}
								if(j == MAXSPRITES)
								{
									GlobalMembersGLOBAL.tempbuf = string.Format("Found lonely Sector Effector (lotag 0) at ({0:D},{1:D})\n", sp.x, sp.y);
									GlobalMembersGAME.gameexit(ref GlobalMembersGLOBAL.tempbuf);
								}
								sp.owner = j;
							}

							startwall = sector[sect].wallptr;
							endwall = startwall+sector[sect].wallnum;

							T2 = tempwallptr;
							for(s = startwall;s<endwall;s++)
							{
								GlobalMembersGLOBAL.msx[tempwallptr] = wall[s].x-sp.x;
								GlobalMembersGLOBAL.msy[tempwallptr] = wall[s].y-sp.y;
								tempwallptr++;
								if(tempwallptr > 2047)
								{
									GlobalMembersGLOBAL.tempbuf = string.Format("Too many moving sectors at ({0:D},{1:D}).\n", wall[s].x, wall[s].y);
									GlobalMembersGAME.gameexit(ref GlobalMembersGLOBAL.tempbuf);
								}
							}
							if(sp.lotag == 30 || sp.lotag == 6 || sp.lotag == 14 || sp.lotag == 5)
							{

								startwall = sector[sect].wallptr;
								endwall = startwall+sector[sect].wallnum;

								if(sector[sect].hitag == -1)
									sp.extra = 0;
								else
									sp.extra = 1;

								sector[sect].hitag = i;

								j = 0;

								for(s = startwall;s<endwall;s++)
								{
									if(wall[s].nextsector >= 0 && sector[wall[s].nextsector].hitag == 0 && sector[wall[s].nextsector].lotag < 3)
										{
											s = wall[s].nextsector;
											j = 1;
											break;
										}
								}

								if(j == 0)
								{
									GlobalMembersGLOBAL.tempbuf = string.Format("Subway found no zero'd sectors with locators\nat ({0:D},{1:D}).\n", sp.x, sp.y);
									GlobalMembersGAME.gameexit(ref GlobalMembersGLOBAL.tempbuf);
								}

								sp.owner = -1;
								T1 = s;

								if(sp.lotag != 30)
									T4 = sp.hitag;
							}

							else if(sp.lotag == 16)
								T4 = sector[sect].ceilingz;

							else if(sp.lotag == 26)
							{
								T4 = sp.x;
								T5 = sp.y;
								if(sp.shade==sector[sect].floorshade) //UP
									sp.zvel = -256;
								else
									sp.zvel = 256;

								sp.shade = 0;
							}
							else if(sp.lotag == 2)
							{
								T6 = sector[sp.sectnum].floorheinum;
								sector[sp.sectnum].floorheinum = 0;
							}
						break;
					}

					switch(sp.lotag)
					{
						case 6:
						case 14:
							j = GlobalMembersSECTOR.callsound(sect, i);
							if(j == -1)
								j = SUBWAY;
							GlobalMembersGLOBAL.hittype[i].lastvx = j;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 30:
							if(numplayers > 1)
								break;
						case 0:
						case 1:
						case 5:
						case 11:
						case 15:
						case 16:
						case 26:
							GlobalMembersACTORS.setsectinterpolate(i);
							break;
					}

					switch(sprite[i].lotag)
					{
						case 40:
						case 41:
						case 43:
						case 44:
						case 45:
							changespritestat(i,15);
							break;
						default:
							changespritestat(i,3);
							break;
					}

					break;


				case SEENINE:
				case OOZFILTER:

					sp.shade = -16;
					if(sp.xrepeat <= 8)
					{
						sp.cstat = (short)32768;
						sp.xrepeat=sp.yrepeat=0;
					}
					else
						sp.cstat = 1+256;
					sp.extra = GlobalMembersGLOBAL.impact_damage<<2;
					sp.owner = i;

					changespritestat(i,6);
					break;

				case CRACK1:
				case CRACK2:
				case CRACK3:
				case CRACK4:
				case FIREEXT:
					if(sp.picnum == FIREEXT)
					{
						sp.cstat = 257;
						sp.extra = GlobalMembersGLOBAL.impact_damage<<2;
					}
					else
					{
						sp.cstat |= 17;
						sp.extra = 1;
					}

					if(GlobalMembersGLOBAL.ud.multimode < 2 && sp.pal != 0)
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
						break;
					}

					sp.pal = 0;
					sp.owner = i;
					changespritestat(i,6);
					sp.xvel = 8;
					GlobalMembersACTORS.ssp(i, CLIPMASK0);
					break;

				case TOILET:
				case STALL:
					sp.lotag = 1;
					sp.cstat |= 257;
					sp.clipdist = 8;
					sp.owner = i;
					break;
				case CANWITHSOMETHING:
				case CANWITHSOMETHING2:
				case CANWITHSOMETHING3:
				case CANWITHSOMETHING4:
				case RUBBERCAN:
					sp.extra = 0;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case EXPLODINGBARREL:
				case HORSEONSIDE:
				case FIREBARREL:
				case NUKEBARREL:
				case FIREVASE:
				case NUKEBARRELDENTED:
				case NUKEBARRELLEAKED:
				case WOODENHORSE:

					if(j >= 0)
						sp.xrepeat = sp.yrepeat = 32;
					sp.clipdist = 72;
					GlobalMembersGAMEDEF.makeitfall(i);
					if(j >= 0)
						sp.owner = j;
					else
						sp.owner = i;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case EGG:
					if(GlobalMembersGLOBAL.ud.monsters_off == 1 && sp.picnum == EGG)
					{
						sp.xrepeat = sp.yrepeat = 0;
						changespritestat(i,5);
					}
					else
					{
						if(sp.picnum == EGG)
							sp.clipdist = 24;
						sp.cstat = 257|(TRAND &4);
						changespritestat(i,2);
					}
					break;
				case TOILETWATER:
					sp.shade = -16;
					changespritestat(i,6);
					break;
		}
		return i;
	}


	public static void animatesprites(int x,int y,short a,int smoothratio)
	{
		short i;
		short j;
		short k;
		short p;
		short sect;
		int l;
		int t1;
		int t3;
		int t4;
		spritetype s;
		spritetype t;

		for(j = 0;j < spritesortcnt; j++)
		{
			t = tsprite[j];
			i = t.owner;
			s = sprite[t.owner];

			switch(t.picnum)
			{
				case BLOODPOOL:
				case PUKE:
				case FOOTPRINTS:
				case FOOTPRINTS2:
				case FOOTPRINTS3:
				case FOOTPRINTS4:
					if(t.shade == 127)
						continue;
					break;
				case RESPAWNMARKERRED:
				case RESPAWNMARKERYELLOW:
				case RESPAWNMARKERGREEN:
					if(GlobalMembersGLOBAL.ud.marker == 0)
						t.xrepeat = t.yrepeat = 0;
					continue;
				case CHAIR3:

					k = (((t.ang+3072+128-a)&2047)>>8)&7;
					if(k>4)
					{
						k = 8-k;
						t.cstat |= 4;
					}
					else
						t.cstat &= ~4;
					t.picnum = s.picnum+k;
					break;
				case BLOODSPLAT1:
				case BLOODSPLAT2:
				case BLOODSPLAT3:
				case BLOODSPLAT4:
					if(GlobalMembersGLOBAL.ud.lockout)
						t.xrepeat = t.yrepeat = 0;
					else if(t.pal == 6)
					{
						t.shade = -127;
						continue;
					}
				case BULLETHOLE:
				case CRACK1:
				case CRACK2:
				case CRACK3:
				case CRACK4:
					t.shade = 16;
					continue;
				case NEON1:
				case NEON2:
				case NEON3:
				case NEON4:
				case NEON5:
				case NEON6:
					continue;
				case GREENSLIME:
				case GREENSLIME+1:
				case GREENSLIME+2:
				case GREENSLIME+3:
				case GREENSLIME+4:
				case GREENSLIME+5:
				case GREENSLIME+6:
				case GREENSLIME+7:
					break;
				default:
					if(((t.cstat &16)) || (GlobalMembersGAME.badguy(ref t) != 0 && t.extra > 0) || t.statnum == 10)
						continue;
			}

			if (sector[t.sectnum].ceilingstat &1)
				l = sector[t.sectnum].ceilingshade;
			else
				l = sector[t.sectnum].floorshade;

			if(l < -127)
				l = -127;
			if(l > 128)
				l = 127;
			t.shade = l;
		}


		for(j = 0;j < spritesortcnt; j++) //Between drawrooms() and drawmasks()
		{ //is the perfect time to animate sprites
			t = tsprite[j];
			i = t.owner;
			s = sprite[i];

			switch(s.picnum)
			{
				case SECTOREFFECTOR:
					if(t.lotag == 27 && GlobalMembersGLOBAL.ud.recstat == 1)
					{
						t.picnum = 11+((totalclock>>3)&1);
						t.cstat |= 128;
					}
					else
						t.xrepeat = t.yrepeat = 0;
					break;
				case NATURALLIGHTNING:
				   t.shade = -127;
				   break;
				case FEM1:
				case FEM2:
				case FEM3:
				case FEM4:
				case FEM5:
				case FEM6:
				case FEM7:
				case FEM8:
				case FEM9:
				case FEM10:
				case MAN:
				case MAN2:
				case WOMAN:
				case NAKED1:
				case PODFEM1:
				case FEMMAG1:
				case FEMMAG2:
				case FEMPIC1:
				case FEMPIC2:
				case FEMPIC3:
				case FEMPIC4:
				case FEMPIC5:
				case FEMPIC6:
				case FEMPIC7:
				case BLOODYPOLE:
				case FEM6PAD:
				case STATUE:
				case STATUEFLASH:
				case OOZ:
				case OOZ2:
				case WALLBLOOD1:
				case WALLBLOOD2:
				case WALLBLOOD3:
				case WALLBLOOD4:
				case WALLBLOOD5:
				case WALLBLOOD7:
				case WALLBLOOD8:
				case SUSHIPLATE1:
				case SUSHIPLATE2:
				case SUSHIPLATE3:
				case SUSHIPLATE4:
				case FETUS:
				case FETUSJIB:
				case FETUSBROKE:
				case HOTMEAT:
				case FOODOBJECT16:
				case DOLPHIN1:
				case DOLPHIN2:
				case TOUGHGAL:
				case TAMPON:
				case XXXSTACY:
				case 4946:
				case 4947:
				case 693:
				case 2254:
				case 4560:
				case 4561:
				case 4562:
				case 4498:
				case 4957:
					if(GlobalMembersGLOBAL.ud.lockout)
					{
						t.xrepeat = t.yrepeat = 0;
						continue;
					}
			}

			if(t.statnum == 99)
				continue;
			if(s.statnum != 1 && s.picnum == APLAYER && GlobalMembersGLOBAL.ps[s.yvel].newowner == -1 && s.owner >= 0)
			{
				t.x -= mulscale16(65536-smoothratio,GlobalMembersGLOBAL.ps[s.yvel].posx-GlobalMembersGLOBAL.ps[s.yvel].oposx);
				t.y -= mulscale16(65536-smoothratio,GlobalMembersGLOBAL.ps[s.yvel].posy-GlobalMembersGLOBAL.ps[s.yvel].oposy);
				t.z = GlobalMembersGLOBAL.ps[s.yvel].oposz + mulscale16(smoothratio,GlobalMembersGLOBAL.ps[s.yvel].posz-GlobalMembersGLOBAL.ps[s.yvel].oposz);
				t.z += (40<<8);
			}
			else if((s.statnum == 0 && s.picnum != CRANEPOLE) || s.statnum == 10 || s.statnum == 6 || s.statnum == 4 || s.statnum == 5 || s.statnum == 1)
			{
				t.x -= mulscale16(65536-smoothratio,s.x-GlobalMembersGLOBAL.hittype[i].bposx);
				t.y -= mulscale16(65536-smoothratio,s.y-GlobalMembersGLOBAL.hittype[i].bposy);
				t.z -= mulscale16(65536-smoothratio,s.z-GlobalMembersGLOBAL.hittype[i].bposz);
			}

			sect = s.sectnum;
			t1 = T2;
			t3 = T4;
			t4 = T5;

			switch(s.picnum)
			{
				case DUKELYINGDEAD:
					t.z += (24<<8);
					break;
				case BLOODPOOL:
				case FOOTPRINTS:
				case FOOTPRINTS2:
				case FOOTPRINTS3:
				case FOOTPRINTS4:
					if(t.pal == 6)
						t.shade = -127;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case PUKE:
				case MONEY:
				case MONEY+1:
				case MAIL:
				case MAIL+1:
				case PAPER:
				case PAPER+1:
					if(GlobalMembersGLOBAL.ud.lockout && s.pal == 2)
					{
						t.xrepeat = t.yrepeat = 0;
						continue;
					}
					break;
				case TRIPBOMB:
					continue;
				case FORCESPHERE:
					if(t.statnum == 5)
					{
						short sqa;
						short sqb;

						sqa = getangle(sprite[s.owner].x-GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].posx, sprite[s.owner].y-GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].posy);
						sqb = getangle(sprite[s.owner].x-t.x, sprite[s.owner].y-t.y);

						if(klabs(GlobalMembersGAMEDEF.getincangle(sqa, sqb)) > 512)
							if(GlobalMembersSECTOR.ldist(ref sprite[s.owner], ref t) < GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].i], ref sprite[s.owner]))
								t.xrepeat = t.yrepeat = 0;
					}
					continue;
				case BURNING:
				case BURNING2:
					if(sprite[s.owner].statnum == 10)
					{
						if(GlobalMembersGLOBAL.display_mirror == 0 && sprite[s.owner].yvel == GlobalMembersGLOBAL.screenpeek && GlobalMembersGLOBAL.ps[sprite[s.owner].yvel].over_shoulder_on == 0)
							t.xrepeat = 0;
						else
						{
							t.ang = getangle(x-t.x,y-t.y);
							t.x = sprite[s.owner].x;
							t.y = sprite[s.owner].y;
							t.x += sintable[(t.ang+512)&2047]>>10;
							t.y += sintable[t.ang &2047]>>10;
						}
					}
					break;

				case ATOMICHEALTH:
					t.z -= (4<<8);
					break;
				case CRYSTALAMMO:
					t.shade = (sintable[(totalclock<<4)&2047]>>10);
					continue;
				case VIEWSCREEN:
				case VIEWSCREEN2:
					if(GlobalMembersGLOBAL.camsprite >= 0 && GlobalMembersGLOBAL.hittype[OW].temp_data[0] == 1)
					{
						t.picnum = STATIC;
						t.cstat |= (RandomNumbers.NextNumber()&12);
						t.xrepeat += 8;
						t.yrepeat += 8;
					}
					break;

				case SHRINKSPARK:
					t.picnum = SHRINKSPARK+((totalclock>>4)&3);
					break;
				case GROWSPARK:
					t.picnum = GROWSPARK+((totalclock>>4)&3);
					break;
				case RPG:
					 k = getangle(s.x-x,s.y-y);
					 k = (((s.ang+3072+128-k)&2047)/170);
					 if(k > 6)
					 {
						k = 12-k;
						t.cstat |= 4;
					 }
					 else
						 t.cstat &= ~4;
					 t.picnum = RPG+k;
					 break;

				case RECON:

					k = getangle(s.x-x,s.y-y);
					if(T1 < 4)
						k = (((s.ang+3072+128-k)&2047)/170);
					else
						k = (((s.ang+3072+128-k)&2047)/170);

					if(k>6)
					{
						k = 12-k;
						t.cstat |= 4;
					}
					else
						t.cstat &= ~4;

					if(klabs(t3) > 64)
						k += 7;
					t.picnum = RECON+k;

					break;

				case APLAYER:

					p = s.yvel;

					if(t.pal == 1)
						t.z -= (18<<8);

					if(GlobalMembersGLOBAL.ps[p].over_shoulder_on > 0 && GlobalMembersGLOBAL.ps[p].newowner < 0)
					{
						t.cstat |= 2;
						if (GlobalMembersGLOBAL.screenpeek == myconnectindex && numplayers >= 2)
						{
							t.x = GlobalMembersGLOBAL.omyx+mulscale16((int)(GlobalMembersGLOBAL.myx-GlobalMembersGLOBAL.omyx),smoothratio);
							t.y = GlobalMembersGLOBAL.omyy+mulscale16((int)(GlobalMembersGLOBAL.myy-GlobalMembersGLOBAL.omyy),smoothratio);
							t.z = GlobalMembersGLOBAL.omyz+mulscale16((int)(GlobalMembersGLOBAL.myz-GlobalMembersGLOBAL.omyz),smoothratio)+(40<<8);
							t.ang = GlobalMembersGLOBAL.omyang+mulscale16((int)(((GlobalMembersGLOBAL.myang+1024-GlobalMembersGLOBAL.omyang)&2047)-1024), smoothratio);
							t.sectnum = GlobalMembersGLOBAL.mycursectnum;
						}
					}

					if((GlobalMembersGLOBAL.display_mirror == 1 || GlobalMembersGLOBAL.screenpeek != p || s.owner == -1) && GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ud.showweapons && sprite[GlobalMembersGLOBAL.ps[p].i].extra > 0 && GlobalMembersGLOBAL.ps[p].curr_weapon > 0)
					{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
						memcpy((spritetype)&tsprite[spritesortcnt], (spritetype)t, sizeof(spritetype));

						tsprite[spritesortcnt].statnum = 99;

						tsprite[spritesortcnt].yrepeat = (t.yrepeat>>3);
						if(t.yrepeat < 4)
							t.yrepeat = 4;

						tsprite[spritesortcnt].shade = t.shade;
						tsprite[spritesortcnt].cstat = 0;

						switch(GlobalMembersGLOBAL.ps[p].curr_weapon)
						{
							case PISTOL_WEAPON:
								tsprite[spritesortcnt].picnum = FIRSTGUNSPRITE;
								break;
							case SHOTGUN_WEAPON:
								tsprite[spritesortcnt].picnum = SHOTGUNSPRITE;
								break;
							case CHAINGUN_WEAPON:
								tsprite[spritesortcnt].picnum = CHAINGUNSPRITE;
								break;
							case RPG_WEAPON:
								tsprite[spritesortcnt].picnum = RPGSPRITE;
								break;
							case HANDREMOTE_WEAPON:
							case HANDBOMB_WEAPON:
								tsprite[spritesortcnt].picnum = HEAVYHBOMB;
								break;
							case TRIPBOMB_WEAPON:
								tsprite[spritesortcnt].picnum = TRIPBOMBSPRITE;
								break;
							case GROW_WEAPON:
								tsprite[spritesortcnt].picnum = GROWSPRITEICON;
								break;
							case SHRINKER_WEAPON:
								tsprite[spritesortcnt].picnum = SHRINKERSPRITE;
								break;
							case FREEZE_WEAPON:
								tsprite[spritesortcnt].picnum = FREEZESPRITE;
								break;
							case DEVISTATOR_WEAPON:
								tsprite[spritesortcnt].picnum = DEVISTATORSPRITE;
								break;
						}

						if(s.owner >= 0)
							tsprite[spritesortcnt].z = GlobalMembersGLOBAL.ps[p].posz-(12<<8);
						else
							tsprite[spritesortcnt].z = s.z-(51<<8);
						if(GlobalMembersGLOBAL.ps[p].curr_weapon == HANDBOMB_WEAPON)
						{
							tsprite[spritesortcnt].xrepeat = 10;
							tsprite[spritesortcnt].yrepeat = 10;
						}
						else
						{
							tsprite[spritesortcnt].xrepeat = 16;
							tsprite[spritesortcnt].yrepeat = 16;
						}
						tsprite[spritesortcnt].pal = 0;
						spritesortcnt++;
					}

					if(s.owner == -1)
					{
						k = (((s.ang+3072+128-a)&2047)>>8)&7;
						if(k>4)
						{
							k = 8-k;
							t.cstat |= 4;
						}
						else
							t.cstat &= ~4;

						if(sector[t.sectnum].lotag == 2)
							k += 1795-1405;
						else if((GlobalMembersGLOBAL.hittype[i].floorz-s.z) > (64<<8))
							k += 60;

						t.picnum += k;
						t.pal = GlobalMembersGLOBAL.ps[p].palookup;

						goto PALONLY;
					}

					if(GlobalMembersGLOBAL.ps[p].on_crane == -1 && (sector[s.sectnum].lotag &0x7ff) != 1)
					{
						l = s.z-GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[p].i].floorz+(3<<8);
						if(l > 1024 && s.yrepeat > 32 && s.extra > 0)
							s.yoffset = (sbyte)(l/(s.yrepeat<<2));
						else
							s.yoffset=0;
					}

					if(GlobalMembersGLOBAL.ps[p].newowner > -1)
					{
						t4 = *(GlobalMembersGLOBAL.actorscrptr[APLAYER]+1);
						t3 = 0;
						t1 = *(GlobalMembersGLOBAL.actorscrptr[APLAYER]+2);
					}

					if(GlobalMembersGLOBAL.ud.camerasprite == -1 && GlobalMembersGLOBAL.ps[p].newowner == -1)
						if(s.owner >= 0 && GlobalMembersGLOBAL.display_mirror == 0 && GlobalMembersGLOBAL.ps[p].over_shoulder_on == 0)
							if(GlobalMembersGLOBAL.ud.multimode < 2 || (GlobalMembersGLOBAL.ud.multimode > 1 && p == GlobalMembersGLOBAL.screenpeek))
					{
						t.owner = -1;
						t.xrepeat = t.yrepeat = 0;
						continue;
					}

					PALONLY:

					if(sector[sect].floorpal)
						t.pal = sector[sect].floorpal;

					if(s.owner == -1)
						continue;

					if(t.z > GlobalMembersGLOBAL.hittype[i].floorz && t.xrepeat < 32)
						t.z = GlobalMembersGLOBAL.hittype[i].floorz;

					break;

				case JIBS1:
				case JIBS2:
				case JIBS3:
				case JIBS4:
				case JIBS5:
				case JIBS6:
				case HEADJIB1:
				case LEGJIB1:
				case ARMJIB1:
				case LIZMANHEAD1:
				case LIZMANARM1:
				case LIZMANLEG1:
				case DUKELEG:
				case DUKEGUN:
				case DUKETORSO:
					if(GlobalMembersGLOBAL.ud.lockout)
					{
						t.xrepeat = t.yrepeat = 0;
						continue;
					}
					if(t.pal == 6)
						t.shade = -120;

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case SCRAP1:
				case SCRAP2:
				case SCRAP3:
				case SCRAP4:
				case SCRAP5:
				case SCRAP6:
				case SCRAP6+1:
				case SCRAP6+2:
				case SCRAP6+3:
				case SCRAP6+4:
				case SCRAP6+5:
				case SCRAP6+6:
				case SCRAP6+7:

					if(GlobalMembersGLOBAL.hittype[i].picnum == BLIMP && t.picnum == SCRAP1 && s.yvel >= 0)
						t.picnum = s.yvel;
					else
						t.picnum += T1;
					t.shade -= 6;

					if(sector[sect].floorpal)
						t.pal = sector[sect].floorpal;
					break;

				case WATERBUBBLE:
					if(sector[t.sectnum].floorpicnum == FLOORSLIME)
					{
						t.pal = 7;
						break;
					}
				default:

					if(sector[sect].floorpal)
						t.pal = sector[sect].floorpal;
					break;
			}

			if (GlobalMembersGLOBAL.actorscrptr[s.picnum] != 0)
			{
				if (t4 != 0)
				{
					l = (int)(t4+8);

					switch(l)
					{
						case 2:
							k = (((s.ang+3072+128-a)&2047)>>8)&1;
							break;

						case 3:
						case 4:
							k = (((s.ang+3072+128-a)&2047)>>7)&7;
							if(k > 3)
							{
								t.cstat |= 4;
								k = 7-k;
							}
							else
								t.cstat &= ~4;
							break;

						case 5:
							k = getangle(s.x-x,s.y-y);
							k = (((s.ang+3072+128-k)&2047)>>8)&7;
							if(k>4)
							{
								k = 8-k;
								t.cstat |= 4;
							}
							else
								t.cstat &= ~4;
							break;
						case 7:
							k = getangle(s.x-x,s.y-y);
							k = (((s.ang+3072+128-k)&2047)/170);
							if(k>6)
							{
								k = 12-k;
								t.cstat |= 4;
							}
							else
								t.cstat &= ~4;
							break;
						case 8:
							k = (((s.ang+3072+128-a)&2047)>>8)&7;
							t.cstat &= ~4;
							break;
						default:
							k = 0;
							break;
					}

					t.picnum += k + ((int)t4) + l * t3;

					if(l > 0)
						while(tilesizx[t.picnum] == 0 && t.picnum > 0)
						t.picnum -= l; //Hack, for actors

					if(GlobalMembersGLOBAL.hittype[i].dispicnum >= 0)
						GlobalMembersGLOBAL.hittype[i].dispicnum = t.picnum;
				}
				else if(GlobalMembersGLOBAL.display_mirror == 1)
					t.cstat |= 4;
			}

			if(s.statnum == 13 || GlobalMembersGAME.badguy(ref s) || (s.picnum == APLAYER && s.owner >= 0))
				if(t.statnum != 99 && s.picnum != EXPLOSION2 && s.picnum != HANGLIGHT && s.picnum != DOMELITE)
					if(s.picnum != HOTMEAT)
			{
				if(GlobalMembersGLOBAL.hittype[i].dispicnum < 0)
				{
					GlobalMembersGLOBAL.hittype[i].dispicnum++;
					continue;
				}
				else if(GlobalMembersGLOBAL.ud.shadows && spritesortcnt < (MAXSPRITESONSCREEN-2))
				{
					int daz;
					int xrep;
					int yrep;

					if((sector[sect].lotag &0xff) > 2 || s.statnum == 4 || s.statnum == 5 || s.picnum == DRONE || s.picnum == COMMANDER)
						daz = sector[sect].floorz;
					else
						daz = GlobalMembersGLOBAL.hittype[i].floorz;

					if((s.z-daz) < (8<<8))
						if(GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].posz < daz)
					{
//C++ TO C# CONVERTER TODO TASK: The memory management function 'memcpy' has no equivalent in C#:
						memcpy((spritetype)&tsprite[spritesortcnt], (spritetype)t, sizeof(spritetype));

						tsprite[spritesortcnt].statnum = 99;

						tsprite[spritesortcnt].yrepeat = (t.yrepeat>>3);
						if(t.yrepeat < 4)
							t.yrepeat = 4;

						tsprite[spritesortcnt].shade = 127;
						tsprite[spritesortcnt].cstat |= 2;

						tsprite[spritesortcnt].z = daz;
						xrep = tsprite[spritesortcnt].xrepeat; // - (klabs(daz-t->z)>>11);
						tsprite[spritesortcnt].xrepeat = xrep;
						tsprite[spritesortcnt].pal = 4;

						yrep = tsprite[spritesortcnt].yrepeat; // - (klabs(daz-t->z)>>11);
						tsprite[spritesortcnt].yrepeat = yrep;
						spritesortcnt++;
					}
				}

				if(GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].heat_amount > 0 && GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].heat_on)
				{
					t.pal = 6;
					t.shade = 0;
				}
			}


			switch(s.picnum)
			{
				case LASERLINE:
					if(sector[t.sectnum].lotag == 2)
						t.pal = 8;
					t.z = sprite[s.owner].z-(3<<8);
					if(GlobalMembersGLOBAL.lasermode == 2 && GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].heat_on == 0)
						t.yrepeat = 0;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case EXPLOSION2:
				case EXPLOSION2BOT:
				case FREEZEBLAST:
				case ATOMICHEALTH:
				case FIRELASER:
				case SHRINKSPARK:
				case GROWSPARK:
				case CHAINGUN:
				case SHRINKEREXPLOSION:
				case RPG:
				case FLOORFLAME:
					if(t.picnum == EXPLOSION2)
					{
						GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].visibility = -127;
						GlobalMembersPLAYER.lastvisinc = totalclock+32;
						restorepalette = 1;
					}
					t.shade = -127;
					break;
				case FIRE:
				case FIRE2:
				case BURNING:
				case BURNING2:
					if(sprite[s.owner].picnum != TREE1 && sprite[s.owner].picnum != TREE2)
						t.z = sector[t.sectnum].floorz;
					t.shade = -127;
					break;
				case COOLEXPLOSION1:
					t.shade = -127;
					t.picnum += (s.shade>>1);
					break;
				case PLAYERONWATER:

					k = (((t.ang+3072+128-a)&2047)>>8)&7;
					if(k>4)
					{
						k = 8-k;
						t.cstat |= 4;
					}
					else
						t.cstat &= ~4;

					t.picnum = s.picnum+k+((T1<4)*5);
					t.shade = sprite[s.owner].shade;

					break;

				case WATERSPLASH2:
					t.picnum = WATERSPLASH2+t1;
					break;
				case REACTOR2:
					t.picnum = s.picnum + T3;
					break;
				case SHELL:
					t.picnum = s.picnum+(T1 &1);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case SHOTGUNSHELL:
					t.cstat |= 12;
					if(T1 > 1)
						t.cstat &= ~4;
					if(T1 > 2)
						t.cstat &= ~12;
					break;
				case FRAMEEFFECT1:
					if(s.owner >= 0 && sprite[s.owner].statnum < MAXSTATUS)
					{
						if(sprite[s.owner].picnum == APLAYER)
							if(GlobalMembersGLOBAL.ud.camerasprite == -1)
								if(GlobalMembersGLOBAL.screenpeek == sprite[s.owner].yvel && GlobalMembersGLOBAL.display_mirror == 0)
						{
							t.owner = -1;
							break;
						}
						if((sprite[s.owner].cstat &32768) == 0)
						{
							t.picnum = GlobalMembersGLOBAL.hittype[s.owner].dispicnum;
							t.pal = sprite[s.owner].pal;
							t.shade = sprite[s.owner].shade;
							t.ang = sprite[s.owner].ang;
							t.cstat = 2|sprite[s.owner].cstat;
						}
					}
					break;

				case CAMERA1:
				case RAT:
					k = (((t.ang+3072+128-a)&2047)>>8)&7;
					if(k>4)
					{
						k = 8-k;
						t.cstat |= 4;
					}
					else
						t.cstat &= ~4;
					t.picnum = s.picnum+k;
					break;
			}

			GlobalMembersGLOBAL.hittype[i].dispicnum = t.picnum;
			if(sector[t.sectnum].floorpicnum == MIRROR)
				t.xrepeat = t.yrepeat = 0;
		}
	}



	#define NUMCHEATCODES
	#if ! VOLUMEONE
	#if ! VOLUMEONE
	public static sbyte[,] cheatquotes = { {"cornholio"}, {"stuff"}, {"scotty###"}, {"coords"}, {"view"}, {"time"}, {"unlock"}, {"cashman"}, {"items"}, {"rate"}, {"skill#"}, {"beta"}, {"hyper"}, {"monsters"}, {"<RESERVED>"}, {"<RESERVED>"}, {"todd"}, {"showmap"}, {"kroz"}, {"allen"}, {"clip"}, {"weapons"}, {"inventory"}, {"keys"}, {"debug"} };
	#else
		{
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
			"scotty##"
		}
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	   ,
	#endif
	#else
		{
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
			"<RESERVED>"
		}
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
	   ,
	#endif
	// #ifndef VOLUMEONE
	//  {"bonus"},
	// #else
	// #endif
	//    {"ending"}


	public static string cheatbuf = new string(new char[10]);
	public static sbyte cheatbuflen;
	public static void cheats()
	{
		short ch;
		short i;
		short j;
		short k;
		short keystate;
		short weapon;

		if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_TYPE) || (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU))
			return;

	#if BETA
		return;
	#endif

		if (GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase == 1)
		{
		   while (KB_KeyWaiting())
		   {
			  ch = KB_Getch();
			  ch = char.ToLower(ch);

			  if(!((ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9')))
			  {
				 GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
	//             FTA(46,&ps[myconnectindex]);
				 return;
			  }

			  cheatbuf[cheatbuflen++] = ch;
			  cheatbuf[cheatbuflen] = 0;

			  if(cheatbuflen > 11)
			  {
				  GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
				  return;
			  }

			  for(k = 0;k < DefineConstants.NUMCHEATCODES;k++)
			  {
				  for(j = 0;j<cheatbuflen;j++)
				  {
					  if(cheatbuf[j] == cheatquotes[k][j] || (cheatquotes[k][j] == '#' && ch >= '0' && ch <= '9'))
					  {
						  if(cheatquotes[k][j+1] == 0)
							  goto FOUNDCHEAT;
						  if(j == cheatbuflen-1)
							  return;
					  }
					  else
						  break;
				  }
			  }

			  GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
			  return;

			  FOUNDCHEAT:
			  {
					switch(k)
					{
						case 21:
	#if VOLUMEONE
							j = 6;
	#else
							j = 0;
	#endif

							for (weapon = PISTOL_WEAPON;weapon < MAX_WEAPONS-j;weapon++)
							{
								GlobalMembersACTORS.addammo(weapon, ref GlobalMembersGLOBAL.ps[myconnectindex], GlobalMembersGLOBAL.max_ammo_amount[weapon]);
								GlobalMembersGLOBAL.ps[myconnectindex].gotweapon[weapon] = 1;
							}

							KB_FlushKeyBoardQueue();
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							GlobalMembersGAME.FTA(119, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							return;
						case 22:
							KB_FlushKeyBoardQueue();
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							GlobalMembersGLOBAL.ps[myconnectindex].steroids_amount = 400;
							GlobalMembersGLOBAL.ps[myconnectindex].heat_amount = 1200;
							GlobalMembersGLOBAL.ps[myconnectindex].boot_amount = 200;
							GlobalMembersGLOBAL.ps[myconnectindex].shield_amount = 100;
							GlobalMembersGLOBAL.ps[myconnectindex].scuba_amount = 6400;
							GlobalMembersGLOBAL.ps[myconnectindex].holoduke_amount = 2400;
							GlobalMembersGLOBAL.ps[myconnectindex].jetpack_amount = 1600;
							GlobalMembersGLOBAL.ps[myconnectindex].firstaid_amount = GlobalMembersGLOBAL.max_player_health;
							GlobalMembersGAME.FTA(120, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							return;
						case 23:
							GlobalMembersGLOBAL.ps[myconnectindex].got_access = 7;
							KB_FlushKeyBoardQueue();
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							GlobalMembersGAME.FTA(121, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							return;
						case 24:
							debug_on = 1-debug_on;
							KB_FlushKeyBoardQueue();
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							break;
						case 20:
							GlobalMembersGLOBAL.ud.clipping = 1-GlobalMembersGLOBAL.ud.clipping;
							KB_FlushKeyBoardQueue();
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							GlobalMembersGAME.FTA(112+GlobalMembersGLOBAL.ud.clipping, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							return;

						case 15:
							GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_EOL;
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;

						case 19:
							GlobalMembersGAME.FTA(79, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_ClearKeyDown(sc_N);
							return;
						case 0:
						case 18:

							GlobalMembersGLOBAL.ud.god = 1-GlobalMembersGLOBAL.ud.god;

							if(GlobalMembersGLOBAL.ud.god)
							{
								GlobalMembersGLOBAL.pus = 1;
								GlobalMembersGLOBAL.pub = 1;
								sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].cstat = 257;

								GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].temp_data[0] = 0;
								GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].temp_data[1] = 0;
								GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].temp_data[2] = 0;
								GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].temp_data[3] = 0;
								GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].temp_data[4] = 0;
								GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].temp_data[5] = 0;

								sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].hitag = 0;
								sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].lotag = 0;
								sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].pal = GlobalMembersGLOBAL.ps[myconnectindex].palookup;

								GlobalMembersGAME.FTA(17, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							}
							else
							{
								GlobalMembersGLOBAL.ud.god = 0;
								sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].extra = GlobalMembersGLOBAL.max_player_health;
								GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].extra = -1;
								GlobalMembersGLOBAL.ps[myconnectindex].last_extra = GlobalMembersGLOBAL.max_player_health;
								GlobalMembersGAME.FTA(18, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							}

							sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].extra = GlobalMembersGLOBAL.max_player_health;
							GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.ps[myconnectindex].i].extra = 0;
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();

							return;

						case 1:

	#if VOLUMEONE
							j = 6;
	#else
							j = 0;
	#endif
							for (weapon = PISTOL_WEAPON;weapon < MAX_WEAPONS-j;weapon++)
							   GlobalMembersGLOBAL.ps[myconnectindex].gotweapon[weapon] = 1;

							for (weapon = PISTOL_WEAPON; weapon < (MAX_WEAPONS-j); weapon++)
								GlobalMembersACTORS.addammo(weapon, ref GlobalMembersGLOBAL.ps[myconnectindex], GlobalMembersGLOBAL.max_ammo_amount[weapon]);

							GlobalMembersGLOBAL.ps[myconnectindex].ammo_amount[GROW_WEAPON] = 50;

							GlobalMembersGLOBAL.ps[myconnectindex].steroids_amount = 400;
							GlobalMembersGLOBAL.ps[myconnectindex].heat_amount = 1200;
							GlobalMembersGLOBAL.ps[myconnectindex].boot_amount = 200;
							GlobalMembersGLOBAL.ps[myconnectindex].shield_amount = 100;
							GlobalMembersGLOBAL.ps[myconnectindex].scuba_amount = 6400;
							GlobalMembersGLOBAL.ps[myconnectindex].holoduke_amount = 2400;
							GlobalMembersGLOBAL.ps[myconnectindex].jetpack_amount = 1600;
							GlobalMembersGLOBAL.ps[myconnectindex].firstaid_amount = GlobalMembersGLOBAL.max_player_health;

							GlobalMembersGLOBAL.ps[myconnectindex].got_access = 7;
							GlobalMembersGAME.FTA(5, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;


	//                        FTA(21,&ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							GlobalMembersGLOBAL.ps[myconnectindex].inven_icon = 1;
							return;

						case 2:
						case 10:
	#if ONELEVELDEMO
		GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
		break;
	#endif

							if(k == 2)
							{
								short volnume;
								short levnume;
	#if VOLUMEALL
								volnume = cheatbuf[6] - '0';
								levnume = (cheatbuf[7] - '0')*10+(cheatbuf[8]-'0');
	#else
								volnume = cheatbuf[6] - '0';
								levnume = cheatbuf[7] - '0';
	#endif

								volnume--;
								levnume--;
	#if VOLUMEONE
								if(volnume > 0)
								{
									GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
									KB_FlushKeyBoardQueue();
									return;
								}
	#endif
	#if PLUTOPAK
								if(volnume > 4)
								{
									GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
									KB_FlushKeyBoardQueue();
									return;
								}
								else
	#else
								if(volnume > 3)
								{
									GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
									KB_FlushKeyBoardQueue();
									return;
								}
								else
	#endif

								if(volnume == 0)
								{
									if(levnume > 5)
									{
										GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
										KB_FlushKeyBoardQueue();
										return;
									}
								}
								else
								{
									if(levnume >= 11)
									{
										GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
										KB_FlushKeyBoardQueue();
										return;
									}
								}

								GlobalMembersGLOBAL.ud.m_volume_number = GlobalMembersGLOBAL.ud.volume_number = volnume;
								GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number = levnume;

							}
							else
								GlobalMembersGLOBAL.ud.m_player_skill = GlobalMembersGLOBAL.ud.player_skill = cheatbuf[5] - '1';

							if(numplayers > 1 && myconnectindex == connecthead)
							{
								GlobalMembersGLOBAL.tempbuf[0] = 5;
								GlobalMembersGLOBAL.tempbuf[1] = GlobalMembersGLOBAL.ud.m_level_number;
								GlobalMembersGLOBAL.tempbuf[2] = GlobalMembersGLOBAL.ud.m_volume_number;
								GlobalMembersGLOBAL.tempbuf[3] = GlobalMembersGLOBAL.ud.m_player_skill;
								GlobalMembersGLOBAL.tempbuf[4] = GlobalMembersGLOBAL.ud.m_monsters_off;
								GlobalMembersGLOBAL.tempbuf[5] = GlobalMembersGLOBAL.ud.m_respawn_monsters;
								GlobalMembersGLOBAL.tempbuf[6] = GlobalMembersGLOBAL.ud.m_respawn_items;
								GlobalMembersGLOBAL.tempbuf[7] = GlobalMembersGLOBAL.ud.m_respawn_inventory;
								GlobalMembersGLOBAL.tempbuf[8] = GlobalMembersGLOBAL.ud.m_coop;
								GlobalMembersGLOBAL.tempbuf[9] = GlobalMembersGLOBAL.ud.m_marker;
								GlobalMembersGLOBAL.tempbuf[10] = GlobalMembersGLOBAL.ud.m_ffire;

								for(i = connecthead;i>=0;i = connectpoint2[i])
									sendpacket(i,GlobalMembersGLOBAL.tempbuf,11);
							}
							else
								GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_RESTART;

							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;

						case 3:
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							GlobalMembersGLOBAL.ud.coords = 1-GlobalMembersGLOBAL.ud.coords;
							KB_FlushKeyBoardQueue();
							return;

						case 4:
							if(GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on)
								GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on = 0;
							else
							{
								GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on = 1;
								cameradist = 0;
								cameraclock = totalclock;
							}
							GlobalMembersGAME.FTA(22, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
						case 5:

							GlobalMembersGAME.FTA(21, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
	#if ! VOLUMEONE
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 6:
							for(i = numsectors-1;i>=0;i--) //Unlock
							{
								j = sector[i].lotag;
								if(j == -1 || j == 32767)
									continue;
								if((j & 0x7fff) > 2)
								{
									if(j&(0xffff-16384))
										sector[i].lotag &= (0xffff-16384);
									GlobalMembersSECTOR.operatesectors(i, GlobalMembersGLOBAL.ps[myconnectindex].i);
								}
							}
							GlobalMembersSECTOR.operateforcefields(GlobalMembersGLOBAL.ps[myconnectindex].i, -1);

							GlobalMembersGAME.FTA(100, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
	#endif

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 7:
							GlobalMembersGLOBAL.ud.cashman = 1-GlobalMembersGLOBAL.ud.cashman;
							KB_ClearKeyDown(sc_N);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							return;
						case 8:

							GlobalMembersGLOBAL.ps[myconnectindex].steroids_amount = 400;
							GlobalMembersGLOBAL.ps[myconnectindex].heat_amount = 1200;
							GlobalMembersGLOBAL.ps[myconnectindex].boot_amount = 200;
							GlobalMembersGLOBAL.ps[myconnectindex].shield_amount = 100;
							GlobalMembersGLOBAL.ps[myconnectindex].scuba_amount = 6400;
							GlobalMembersGLOBAL.ps[myconnectindex].holoduke_amount = 2400;
							GlobalMembersGLOBAL.ps[myconnectindex].jetpack_amount = 1600;

							GlobalMembersGLOBAL.ps[myconnectindex].firstaid_amount = GlobalMembersGLOBAL.max_player_health;
							GlobalMembersGLOBAL.ps[myconnectindex].got_access = 7;
							GlobalMembersGAME.FTA(5, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
						case 17: // SHOW ALL OF THE MAP TOGGLE;
							GlobalMembersGLOBAL.ud.showallmap = 1-GlobalMembersGLOBAL.ud.showallmap;
							if(GlobalMembersGLOBAL.ud.showallmap)
							{
								for(i = 0;i<(MAXSECTORS>>3);i++)
									show2dsector[i] = 255;
								for(i = 0;i<(MAXWALLS>>3);i++)
									show2dwall[i] = 255;
								GlobalMembersGAME.FTA(111, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							}
							else
							{
								for(i = 0;i<(MAXSECTORS>>3);i++)
									show2dsector[i] = 0;
								for(i = 0;i<(MAXWALLS>>3);i++)
									show2dwall[i] = 0;
								GlobalMembersGAME.FTA(1, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							}
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;

						case 16:
							GlobalMembersGAME.FTA(99, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
						case 9:
							GlobalMembersGLOBAL.ud.tickrate = !GlobalMembersGLOBAL.ud.tickrate;
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
						case 11:
							GlobalMembersGAME.FTA(105, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							KB_ClearKeyDown(sc_H);
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
						case 12:
							GlobalMembersGLOBAL.ps[myconnectindex].steroids_amount = 399;
							GlobalMembersGLOBAL.ps[myconnectindex].heat_amount = 1200;
							GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
							GlobalMembersGAME.FTA(37, ref GlobalMembersGLOBAL.ps[myconnectindex]);
							KB_FlushKeyBoardQueue();
							return;
						case 13:
							if(actor_tog == 3)
								actor_tog = 0;
							actor_tog++;
							GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].cheat_phase = 0;
							KB_FlushKeyBoardQueue();
							return;
						case 14:
						case 25:
							GlobalMembersGLOBAL.ud.eog = 1;
							GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_EOL;
							KB_FlushKeyBoardQueue();
							return;
					}
				 }
			  }
		   }

		else
		{
			if(KB_KeyPressed(sc_D))
			{
				if(GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase >= 0 && numplayers < 2 && GlobalMembersGLOBAL.ud.recstat == 0)
					GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = -1;
			}

			if(KB_KeyPressed(sc_N))
			{
				if(GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase == -1)
				{
					if(GlobalMembersGLOBAL.ud.player_skill == 4)
					{
						GlobalMembersGAME.FTA(22, ref GlobalMembersGLOBAL.ps[myconnectindex]);
						GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
					}
					else
					{
						GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 1;
	//                    FTA(25,&ps[myconnectindex]);
						cheatbuflen = 0;
					}
					KB_FlushKeyboardQueue();
				}
				else if(GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase != 0)
				{
					GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase = 0;
					KB_ClearKeyDown(sc_D);
					KB_ClearKeyDown(sc_N);
				}
			}
		}
	}


	public static int nonsharedtimer;
	public static void nonsharedkeys()
	{
		short i;
		short ch;
		short weapon;
		int j;

		if(GlobalMembersGLOBAL.ud.recstat == 2)
		{
			ControlInfo noshareinfo = new ControlInfo();
			CONTROL_GetInput(noshareinfo);
		}

		if(KB_KeyPressed(sc_F12))
		{
			KB_ClearKeyDown(sc_F12);
			screencapture("duke0000.pcx",0);
			GlobalMembersGAME.FTA(103, ref GlobalMembersGLOBAL.ps[myconnectindex]);
		}

		if(!ALT_IS_PRESSED && GlobalMembersGLOBAL.ud.overhead_on == 0)
			{
				if(BUTTON(gamefunc_Enlarge_Screen))
				{
					CONTROL_ClearButton(gamefunc_Enlarge_Screen);
					if(GlobalMembersGLOBAL.ud.screen_size > 0)
						GlobalMembersSOUNDS.sound(THUD);
					GlobalMembersGLOBAL.ud.screen_size -= 4;
					GlobalMembersPREMAP.vscrn();
				}
				if(BUTTON(gamefunc_Shrink_Screen))
				{
					CONTROL_ClearButton(gamefunc_Shrink_Screen);
					if(GlobalMembersGLOBAL.ud.screen_size < 64)
						GlobalMembersSOUNDS.sound(THUD);
					GlobalMembersGLOBAL.ud.screen_size += 4;
					GlobalMembersPREMAP.vscrn();
				}
			}

		if(GlobalMembersGLOBAL.ps[myconnectindex].cheat_phase == 1 || GlobalMembersGLOBAL.ps[myconnectindex].gm&(MODE_MENU|MODE_TYPE))
			return;

		if(BUTTON(gamefunc_See_Coop_View) && (GlobalMembersGLOBAL.ud.coop == 1 || GlobalMembersGLOBAL.ud.recstat == 2))
		{
			CONTROL_ClearButton(gamefunc_See_Coop_View);
			GlobalMembersGLOBAL.screenpeek = connectpoint2[GlobalMembersGLOBAL.screenpeek];
			if(GlobalMembersGLOBAL.screenpeek == -1)
				GlobalMembersGLOBAL.screenpeek = connecthead;
			restorepalette = 1;
		}

		if(GlobalMembersGLOBAL.ud.multimode > 1 && BUTTON(gamefunc_Show_Opponents_Weapon))
		{
			CONTROL_ClearButton(gamefunc_Show_Opponents_Weapon);
			GlobalMembersGLOBAL.ud.showweapons = 1-GlobalMembersGLOBAL.ud.showweapons;
			GlobalMembersGAME.FTA(82-GlobalMembersGLOBAL.ud.showweapons, ref GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek]);
		}

		if(BUTTON(gamefunc_Toggle_Crosshair))
		{
			CONTROL_ClearButton(gamefunc_Toggle_Crosshair);
			GlobalMembersGLOBAL.ud.crosshair = 1-GlobalMembersGLOBAL.ud.crosshair;
			GlobalMembersGAME.FTA(21-GlobalMembersGLOBAL.ud.crosshair, ref GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek]);
		}

		if(GlobalMembersGLOBAL.ud.overhead_on && BUTTON(gamefunc_Map_Follow_Mode))
		{
			CONTROL_ClearButton(gamefunc_Map_Follow_Mode);
			GlobalMembersGLOBAL.ud.scrollmode = 1-GlobalMembersGLOBAL.ud.scrollmode;
			if(GlobalMembersGLOBAL.ud.scrollmode)
			{
				GlobalMembersGLOBAL.ud.folx = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposx;
				GlobalMembersGLOBAL.ud.foly = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oposy;
				GlobalMembersGLOBAL.ud.fola = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].oang;
			}
			GlobalMembersGAME.FTA(83+GlobalMembersGLOBAL.ud.scrollmode, ref GlobalMembersGLOBAL.ps[myconnectindex]);
		}

		if(SHIFTS_IS_PRESSED || ALT_IS_PRESSED)
		{
			i = 0;
			if(KB_KeyPressed(sc_F1))
			{
				KB_ClearKeyDown(sc_F1);
				i = 1;
			}
			if(KB_KeyPressed(sc_F2))
			{
				KB_ClearKeyDown(sc_F2);
				i = 2;
			}
			if(KB_KeyPressed(sc_F3))
			{
				KB_ClearKeyDown(sc_F3);
				i = 3;
			}
			if(KB_KeyPressed(sc_F4))
			{
				KB_ClearKeyDown(sc_F4);
				i = 4;
			}
			if(KB_KeyPressed(sc_F5))
			{
				KB_ClearKeyDown(sc_F5);
				i = 5;
			}
			if(KB_KeyPressed(sc_F6))
			{
				KB_ClearKeyDown(sc_F6);
				i = 6;
			}
			if(KB_KeyPressed(sc_F7))
			{
				KB_ClearKeyDown(sc_F7);
				i = 7;
			}
			if(KB_KeyPressed(sc_F8))
			{
				KB_ClearKeyDown(sc_F8);
				i = 8;
			}
			if(KB_KeyPressed(sc_F9))
			{
				KB_ClearKeyDown(sc_F9);
				i = 9;
			}
			if(KB_KeyPressed(sc_F10))
			{
				KB_ClearKeyDown(sc_F10);
				i = 10;
			}

			if (i != 0)
			{
				if(SHIFTS_IS_PRESSED)
				{
					if(i == 5 && GlobalMembersGLOBAL.ps[myconnectindex].fta > 0 && GlobalMembersGLOBAL.ps[myconnectindex].ftq == 26)
					{
						GlobalMembersGLOBAL.music_select++;
	#if VOLUMEALL
						if(GlobalMembersGLOBAL.music_select == 44)
							GlobalMembersGLOBAL.music_select = 0;
	#else
						if(GlobalMembersGLOBAL.music_select == 6)
							GlobalMembersGLOBAL.music_select = 0;
	#endif
						GlobalMembersGLOBAL.tempbuf[0] = "PLAYING ";
						GlobalMembersGLOBAL.tempbuf[0] += GlobalMembersGLOBAL.music_fn[0, GlobalMembersGLOBAL.music_select, 0];
						GlobalMembersSOUNDS.playmusic(ref GlobalMembersGLOBAL.music_fn[0, GlobalMembersGLOBAL.music_select, 0]);
						GlobalMembersGLOBAL.fta_quotes[26, 0] = GlobalMembersGLOBAL.tempbuf[0];
						GlobalMembersGAME.FTA(26, ref GlobalMembersGLOBAL.ps[myconnectindex]);
						return;
					}

					GlobalMembersGAME.adduserquote(ref GlobalMembersGLOBAL.ud.ridecule[i-1]);

					ch = 0;

					GlobalMembersGLOBAL.tempbuf[ch] = 4;
					GlobalMembersGLOBAL.tempbuf[ch+1] = 0;
					GlobalMembersGLOBAL.tempbuf+1 += GlobalMembersGLOBAL.ud.ridecule[i-1];

					i = 1+Convert.ToString(GlobalMembersGLOBAL.ud.ridecule[i-1]).Length;

					if(GlobalMembersGLOBAL.ud.multimode > 1)
						for(ch = connecthead;ch>=0;ch = connectpoint2[ch])
							if (ch != myconnectindex)
								sendpacket(ch,GlobalMembersGLOBAL.tempbuf,i);

					GlobalMembersGLOBAL.pus = NUMPAGES;
					GlobalMembersGLOBAL.pub = NUMPAGES;

					return;

				}

				if(GlobalMembersGLOBAL.ud.lockout == 0)
					if(GlobalMembersCONFIG.SoundToggle != 0 && ALT_IS_PRESSED && (GlobalMembersRTS.RTS_NumSounds() > 0) && GlobalMembersGLOBAL.rtsplaying == 0 && GlobalMembersCONFIG.VoiceToggle != 0)
				{
					rtsptr = (string)GlobalMembersRTS.RTS_GetSound(i-1);
					if(*rtsptr == 'C')
						FX_PlayVOC3D(rtsptr,0,0,0,255,-i);
					else
						FX_PlayWAV3D(rtsptr,0,0,0,255,-i);

					GlobalMembersGLOBAL.rtsplaying = 7;

					if(GlobalMembersGLOBAL.ud.multimode > 1)
					{
						GlobalMembersGLOBAL.tempbuf[0] = 7;
						GlobalMembersGLOBAL.tempbuf[1] = i;

						for(ch = connecthead;ch>=0;ch = connectpoint2[ch])
							if(ch != myconnectindex)
								sendpacket(ch,GlobalMembersGLOBAL.tempbuf,2);
					}

					GlobalMembersGLOBAL.pus = NUMPAGES;
					GlobalMembersGLOBAL.pub = NUMPAGES;

					return;
				}
			}
		}

		if(!ALT_IS_PRESSED && !SHIFTS_IS_PRESSED)
		{

			if(GlobalMembersGLOBAL.ud.multimode > 1 && BUTTON(gamefunc_SendMessage))
			{
				KB_FlushKeyboardQueue();
				CONTROL_ClearButton(gamefunc_SendMessage);
				GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_TYPE;
				GlobalMembersGLOBAL.typebuf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.typebuf, 0, 0);
				inputloc = 0;
			}

			if(KB_KeyPressed(sc_F1) || (GlobalMembersGLOBAL.ud.show_help && (KB_KeyPressed(sc_Space) || KB_KeyPressed(sc_Enter) || KB_KeyPressed(sc_kpad_Enter))))
			{
				KB_ClearKeyDown(sc_F1);
				KB_ClearKeyDown(sc_Space);
				KB_ClearKeyDown(sc_kpad_Enter);
				KB_ClearKeyDown(sc_Enter);
				GlobalMembersGLOBAL.ud.show_help ++;

				if(GlobalMembersGLOBAL.ud.show_help > 2)
				{
					GlobalMembersGLOBAL.ud.show_help = 0;
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
						GlobalMembersGLOBAL.ready2send = 1;
					GlobalMembersPREMAP.vscrn();
				}
				else
				{
					setview(0,0,xdim-1,ydim-1);
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 0;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}
				}
			}

	//        if(ud.multimode < 2)
			{
				if(GlobalMembersGLOBAL.ud.recstat != 2 && KB_KeyPressed(sc_F2))
				{
					KB_ClearKeyDown(sc_F2);

					if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
						return;

					FAKE_F2:
					if(sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].extra <= 0)
					{
						GlobalMembersGAME.FTA(118, ref GlobalMembersGLOBAL.ps[myconnectindex]);
						return;
					}
					GlobalMembersMENUES.cmenu(350);
					screencapt = 1;
					GlobalMembersGAME.displayrooms(myconnectindex, 65536);
					GlobalMembersMENUES.savetemp("duke3d.tmp", waloff[MAXTILES-1], 160 *100);
					screencapt = 0;
					FX_StopAllSounds();
					GlobalMembersSOUNDS.clearsoundlocks();

	//                setview(0,0,xdim-1,ydim-1);
					GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;

					if(GlobalMembersGLOBAL.ud.multimode < 2)
					{
						GlobalMembersGLOBAL.ready2send = 0;
						totalclock = GlobalMembersGLOBAL.ototalclock;
						GlobalMembersGLOBAL.screenpeek = myconnectindex;
					}
				}

				if(KB_KeyPressed(sc_F3))
				{
					KB_ClearKeyDown(sc_F3);

					if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
						return;

					GlobalMembersMENUES.cmenu(300);
					FX_StopAllSounds();
					GlobalMembersSOUNDS.clearsoundlocks();

	//                setview(0,0,xdim-1,ydim-1);
					GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;
					if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ready2send = 0;
						totalclock = GlobalMembersGLOBAL.ototalclock;
					}
					GlobalMembersGLOBAL.screenpeek = myconnectindex;
				}
			}

			if(KB_KeyPressed(sc_F4) && GlobalMembersCONFIG.FXDevice != NumSoundCards)
			{
				KB_ClearKeyDown(sc_F4);
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();

				GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;
				if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
				{
					GlobalMembersGLOBAL.ready2send = 0;
					totalclock = GlobalMembersGLOBAL.ototalclock;
				}
				GlobalMembersMENUES.cmenu(700);

			}

			if(KB_KeyPressed(sc_F6) && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME))
			{
				KB_ClearKeyDown(sc_F6);

				if(GlobalMembersGLOBAL.movesperpacket == 4 && connecthead != myconnectindex)
					return;

				if(GlobalMembersMENUES.lastsavedpos == -1)
					goto FAKE_F2;

				KB_FlushKeyboardQueue();

				if(sprite[GlobalMembersGLOBAL.ps[myconnectindex].i].extra <= 0)
				{
					GlobalMembersGAME.FTA(118, ref GlobalMembersGLOBAL.ps[myconnectindex]);
					return;
				}
				screencapt = 1;
				GlobalMembersGAME.displayrooms(myconnectindex, 65536);
				GlobalMembersMENUES.savetemp("duke3d.tmp", waloff[MAXTILES-1], 160 *100);
				screencapt = 0;
				if(GlobalMembersMENUES.lastsavedpos >= 0)
				{
					inputloc = Convert.ToString(GlobalMembersGLOBAL.ud.savegame[GlobalMembersMENUES.lastsavedpos][0]).Length;
					GlobalMembersGLOBAL.current_menu = 360+GlobalMembersMENUES.lastsavedpos;
					GlobalMembersMENUES.probey = GlobalMembersMENUES.lastsavedpos;
				}
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();

				setview(0,0,xdim-1,ydim-1);
				GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;
				if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
				{
					GlobalMembersGLOBAL.ready2send = 0;
					totalclock = GlobalMembersGLOBAL.ototalclock;
				}
			}

			if(KB_KeyPressed(sc_F7))
			{
				KB_ClearKeyDown(sc_F7);
				if(GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on)
					GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on = 0;
				else
				{
					GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on = 1;
					cameradist = 0;
					cameraclock = totalclock;
				}
				GlobalMembersGAME.FTA(109+GlobalMembersGLOBAL.ps[myconnectindex].over_shoulder_on, ref GlobalMembersGLOBAL.ps[myconnectindex]);
			}

			if(KB_KeyPressed(sc_F5) && GlobalMembersCONFIG.MusicDevice != NumSoundCards)
			{
				KB_ClearKeyDown(sc_F5);
				GlobalMembersGLOBAL.tempbuf[0] = GlobalMembersGLOBAL.music_fn[0, GlobalMembersGLOBAL.music_select, 0];
				GlobalMembersGLOBAL.tempbuf[0] += ".  USE SHIFT-F5 TO CHANGE.";
				GlobalMembersGLOBAL.fta_quotes[26, 0] = GlobalMembersGLOBAL.tempbuf[0];
				GlobalMembersGAME.FTA(26, ref GlobalMembersGLOBAL.ps[myconnectindex]);

			}

			if(KB_KeyPressed(sc_F8))
			{
				KB_ClearKeyDown(sc_F8);
				GlobalMembersGLOBAL.ud.fta_on = !GlobalMembersGLOBAL.ud.fta_on;
				if(GlobalMembersGLOBAL.ud.fta_on)
					GlobalMembersGAME.FTA(23, ref GlobalMembersGLOBAL.ps[myconnectindex]);
				else
				{
					GlobalMembersGLOBAL.ud.fta_on = 1;
					GlobalMembersGAME.FTA(24, ref GlobalMembersGLOBAL.ps[myconnectindex]);
					GlobalMembersGLOBAL.ud.fta_on = 0;
				}
			}

			if(KB_KeyPressed(sc_F9) && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME))
			{
				KB_ClearKeyDown(sc_F9);

				if(GlobalMembersGLOBAL.movesperpacket == 4 && myconnectindex != connecthead)
					return;

				if(GlobalMembersMENUES.lastsavedpos >= 0)
					GlobalMembersMENUES.cmenu(15001);
				else
					GlobalMembersMENUES.cmenu(25000);
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();
				GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;
				if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
				{
					GlobalMembersGLOBAL.ready2send = 0;
					totalclock = GlobalMembersGLOBAL.ototalclock;
				}
			}

			if(KB_KeyPressed(sc_F10))
			{
				KB_ClearKeyDown(sc_F10);
				GlobalMembersMENUES.cmenu(500);
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();
				GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;
				if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.recstat != 2)
				{
					GlobalMembersGLOBAL.ready2send = 0;
					totalclock = GlobalMembersGLOBAL.ototalclock;
				}
			}


			if(GlobalMembersGLOBAL.ud.overhead_on != 0)
			{

				j = totalclock-nonsharedtimer;
				nonsharedtimer += j;
				if (BUTTON(gamefunc_Enlarge_Screen))
					GlobalMembersGLOBAL.ps[myconnectindex].zoom += mulscale6(j,max(GlobalMembersGLOBAL.ps[myconnectindex].zoom,256));
				if (BUTTON(gamefunc_Shrink_Screen))
					GlobalMembersGLOBAL.ps[myconnectindex].zoom -= mulscale6(j,max(GlobalMembersGLOBAL.ps[myconnectindex].zoom,256));

				if((GlobalMembersGLOBAL.ps[myconnectindex].zoom > 2048))
					GlobalMembersGLOBAL.ps[myconnectindex].zoom = 2048;
				if((GlobalMembersGLOBAL.ps[myconnectindex].zoom < 48))
					GlobalMembersGLOBAL.ps[myconnectindex].zoom = 48;

			}
		}

		if(KB_KeyPressed(sc_Escape) && GlobalMembersGLOBAL.ud.overhead_on && GlobalMembersGLOBAL.ps[myconnectindex].newowner == -1)
		{
			KB_ClearKeyDown(sc_Escape);
			GlobalMembersGLOBAL.ud.last_overhead = GlobalMembersGLOBAL.ud.overhead_on;
			GlobalMembersGLOBAL.ud.overhead_on = 0;
			GlobalMembersGLOBAL.ud.scrollmode = 0;
			GlobalMembersPREMAP.vscrn();
		}

		if(BUTTON(gamefunc_AutoRun))
		{
			CONTROL_ClearButton(gamefunc_AutoRun);
			GlobalMembersGLOBAL.ud.auto_run = 1-GlobalMembersGLOBAL.ud.auto_run;
			GlobalMembersGAME.FTA(85+GlobalMembersGLOBAL.ud.auto_run, ref GlobalMembersGLOBAL.ps[myconnectindex]);
		}

		if(BUTTON(gamefunc_Map))
		{
			CONTROL_ClearButton(gamefunc_Map);
			if(GlobalMembersGLOBAL.ud.last_overhead != GlobalMembersGLOBAL.ud.overhead_on && GlobalMembersGLOBAL.ud.last_overhead)
			{
				GlobalMembersGLOBAL.ud.overhead_on = GlobalMembersGLOBAL.ud.last_overhead;
				GlobalMembersGLOBAL.ud.last_overhead = 0;
			}
			else
			{
				GlobalMembersGLOBAL.ud.overhead_on++;
				if(GlobalMembersGLOBAL.ud.overhead_on == 3)
					GlobalMembersGLOBAL.ud.overhead_on = 0;
				GlobalMembersGLOBAL.ud.last_overhead = GlobalMembersGLOBAL.ud.overhead_on;
			}
			restorepalette = 1;
			GlobalMembersPREMAP.vscrn();
		}

		if(KB_KeyPressed(sc_F11))
		{
			KB_ClearKeyDown(sc_F11);
			if(SHIFTS_IS_PRESSED)
				GlobalMembersGLOBAL.ud.brightness-=4;
			else
				GlobalMembersGLOBAL.ud.brightness+=4;

			if (GlobalMembersGLOBAL.ud.brightness > (7<<2))
				GlobalMembersGLOBAL.ud.brightness = 0;
			else if(GlobalMembersGLOBAL.ud.brightness < 0)
				GlobalMembersGLOBAL.ud.brightness = (7<<2);

			setbrightness(GlobalMembersGLOBAL.ud.brightness>>2,GlobalMembersGLOBAL.ps[myconnectindex].palette[0]);
			if(GlobalMembersGLOBAL.ud.brightness < 20)
				GlobalMembersGAME.FTA(29 + (GlobalMembersGLOBAL.ud.brightness>>2), ref GlobalMembersGLOBAL.ps[myconnectindex]);
			else if(GlobalMembersGLOBAL.ud.brightness < 40)
				GlobalMembersGAME.FTA(96 + (GlobalMembersGLOBAL.ud.brightness>>2) - 5, ref GlobalMembersGLOBAL.ps[myconnectindex]);
		}
	}



	public static void comlinehelp(ref string[] argv)
	{
		Console.Write("Command line help.  {0} [/flags...]\n",argv[0]);
		Console.WriteLine(" ?, /?         This help message");
		Console.WriteLine(" /l##          Level (1-11)");
		Console.WriteLine(" /v#           Volume (1-4)");
		Console.WriteLine(" /s#           Skill (1-4)");
		Console.WriteLine(" /r            Record demo");
		Console.WriteLine(" /dFILE        Start to play demo FILE");
		Console.WriteLine(" /m            No monsters");
		Console.WriteLine(" /ns           No sound");
		Console.WriteLine(" /nm           No music");
		Console.WriteLine(" /t#           Respawn, 1 = Monsters, 2 = Items, 3 = Inventory, x = All");
		Console.WriteLine(" /c#           MP mode, 1 = DukeMatch(spawn), 2 = Coop, 3 = Dukematch(no spawn)");
		Console.WriteLine(" /q#           Fake multiplayer (2-8 players)");
		Console.WriteLine(" /a            Use player AI (fake multiplayer only)");
		Console.WriteLine(" /i#           Network mode (1/0) (multiplayer only) (default == 1)");
		Console.WriteLine(" /f#           Send fewer packets (1, 2, 4) (multiplayer only)");
		Console.WriteLine(" /gFILE, /g... Use multiple group files (must be last on command line)");
		Console.WriteLine(" /xFILE        Compile FILE (default GAME.CON)");
		Console.WriteLine(" /u#########   User's favorite weapon order (default: 3425689071)");
		Console.WriteLine(" /#            Load and run a game (slot 0-9)");
		Console.WriteLine(" /z            Skip memory check");
		Console.WriteLine(" -map FILE     Use a map FILE");
		Console.WriteLine(" -name NAME    Foward NAME");
	  Console.Write(" -net          Net mode game");
	}

	public static void checkcommandline(int argc, ref string[] argv)
	{
		short i;
		short j;
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
		sbyte *c;

		i = 1;

		GlobalMembersGLOBAL.ud.fta_on = 1;
		GlobalMembersGLOBAL.ud.god = 0;
		GlobalMembersGLOBAL.ud.m_respawn_items = 0;
		GlobalMembersGLOBAL.ud.m_respawn_monsters = 0;
		GlobalMembersGLOBAL.ud.m_respawn_inventory = 0;
		GlobalMembersGLOBAL.ud.warp_on = 0;
		GlobalMembersGLOBAL.ud.cashman = 0;
		GlobalMembersGLOBAL.ud.m_player_skill = GlobalMembersGLOBAL.ud.player_skill = 2;

	#if BETA
		return;
	#endif

		if(argc > 1)
		{
			while(i < argc)
			{
				c = argv[i];
				if(*c == '-')
				{
					if(*(c+1) == '8')
						eightytwofifty = 1;
					i++;
					continue;
				}

				if(*c == '?')
				{
					GlobalMembersGAME.comlinehelp(ref argv);
					Environment.Exit(-1);
				}

				if(*c == '/')
				{
					c++;
					switch(*c)
					{
						default:
	  //                      printf("Unknown command line parameter '%s'\n",argv[i]);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case '?':
							GlobalMembersGAME.comlinehelp(ref argv);
							Environment.Exit(0);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 'x':
						case 'X':
							c++;
							if(*c)
							{
								confilename = c;
								if(SafeFileExists(c) == 0)
								{
									Console.Write("Could not find con file '{0}'.\n",confilename);
									Environment.Exit(-1);
								}
								else
									Console.Write("Using con file: '{0}'\n",confilename);
							}
							break;
						case 'g':
						case 'G':
							c++;
							if(*c)
							{
								if(StringFunctions.StrChr(c,'.') == 0)
								   c += ".grp";

								j = initgroupfile(c);
								if(j == -1)
									Console.Write("Could not find group file {0}.\n",c);
								else
								{
									GlobalMembersGLOBAL.groupfile = j;
									Console.Write("Using group file {0}.\n",c);
								}
							}

							break;
						case 'a':
						case 'A':
							GlobalMembersGLOBAL.ud.playerai = 1;
							Console.WriteLine("Other player AI.");
							break;
						case 'n':
						case 'N':
							c++;
							if(*c == 's' || *c == 'S')
							{
								CommandSoundToggleOff = 2;
								Console.WriteLine("Sound off.");
							}
							else if(*c == 'm' || *c == 'M')
							{
								CommandMusicToggleOff = 1;
								Console.WriteLine("Music off.");
							}
							else
							{
								GlobalMembersGAME.comlinehelp(ref argv);
								Environment.Exit(-1);
							}
							break;
						case 'i':
						case 'I':
							c++;
							if(*c == '0')
								GlobalMembersGLOBAL.networkmode = 0;
							if(*c == '1')
								GlobalMembersGLOBAL.networkmode = 1;
							Console.Write("Network Mode {0:D}\n",GlobalMembersGLOBAL.networkmode);
							break;
						case 'c':
						case 'C':
							c++;
							if(*c == '1' || *c == '2' || *c == '3')
								GlobalMembersGLOBAL.ud.m_coop = *c - '0' - 1;
							else
								GlobalMembersGLOBAL.ud.m_coop = 0;

							switch(GlobalMembersGLOBAL.ud.m_coop)
							{
								case 0:
									Console.WriteLine("Dukematch (spawn).");
									break;
								case 1:
									Console.WriteLine("Cooperative play.");
									break;
								case 2:
									Console.WriteLine("Dukematch (no spawn).");
									break;
							}

							break;
						case 'z':
						case 'Z':
							memorycheckoveride = 1;
							break;
						case 'f':
						case 'F':
							c++;
							if(*c == '1')
								GlobalMembersGLOBAL.movesperpacket = 1;
							if(*c == '2')
								GlobalMembersGLOBAL.movesperpacket = 2;
							if(*c == '4')
							{
								GlobalMembersGLOBAL.movesperpacket = 4;
								setpackettimeout(0x3fffffff,0x3fffffff);
							}
							break;
						case 't':
						case 'T':
							c++;
							if(*c == '1')
								GlobalMembersGLOBAL.ud.m_respawn_monsters = 1;
							else if(*c == '2')
								GlobalMembersGLOBAL.ud.m_respawn_items = 1;
							else if(*c == '3')
								GlobalMembersGLOBAL.ud.m_respawn_inventory = 1;
							else
							{
								GlobalMembersGLOBAL.ud.m_respawn_monsters = 1;
								GlobalMembersGLOBAL.ud.m_respawn_items = 1;
								GlobalMembersGLOBAL.ud.m_respawn_inventory = 1;
							}
							Console.WriteLine("Respawn on.");
							break;
						case 'm':
						case 'M':
							if(*(c+1) != 'a' && *(c+1) != 'A')
							{
								GlobalMembersGLOBAL.ud.m_monsters_off = 1;
								GlobalMembersGLOBAL.ud.m_player_skill = GlobalMembersGLOBAL.ud.player_skill = 0;
								Console.WriteLine("Monsters off.");
							}
							break;
						case 'w':
						case 'W':
							GlobalMembersGLOBAL.ud.coords = 1;
							break;
						case 'q':
						case 'Q':
							Console.WriteLine("Fake multiplayer mode.");
							if(*(++c) == 0)
								GlobalMembersGLOBAL.ud.multimode = 1;
							else
								GlobalMembersGLOBAL.ud.multimode = Convert.ToInt64(c)%17;
							GlobalMembersGLOBAL.ud.m_coop = GlobalMembersGLOBAL.ud.coop = 0;
							GlobalMembersGLOBAL.ud.m_marker = GlobalMembersGLOBAL.ud.marker = 1;
							GlobalMembersGLOBAL.ud.m_respawn_monsters = GlobalMembersGLOBAL.ud.respawn_monsters = 1;
							GlobalMembersGLOBAL.ud.m_respawn_items = GlobalMembersGLOBAL.ud.respawn_items = 1;
							GlobalMembersGLOBAL.ud.m_respawn_inventory = GlobalMembersGLOBAL.ud.respawn_inventory = 1;

							break;
						case 'r':
						case 'R':
							GlobalMembersGLOBAL.ud.m_recstat = 1;
							Console.WriteLine("Demo record mode on.");
							break;
						case 'd':
						case 'D':
							c++;
							if(StringFunctions.StrChr(c,'.') == 0)
								c += ".dmo";
							Console.Write("Play demo {0}.\n",c);
							firstdemofile = c;
							break;
						case 'l':
						case 'L':
							GlobalMembersGLOBAL.ud.warp_on = 1;
							c++;
							GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number = (Convert.ToInt64(c)-1)%11;
							break;
						case 'j':
						case 'J':
	#if VOLUMEALL
		#if AUSTRALIA
			Console.Write("Duke Nukem 3D (AUSSIE FULL VERSION) v{0}\n",DefineConstants.VERSION);
		#else
			Console.Write("Duke Nukem 3D (FULL VERSION) v{0}\n",DefineConstants.VERSION);
		#endif
	#else
		#if AUSTRALIA
			Console.Write("Duke Nukem 3D (AUSSIE SHAREWARE) v{0}\n",DefineConstants.VERSION);
		#else
			Console.Write("Duke Nukem 3D (SHAREWARE) v{0}\n",DefineConstants.VERSION);
		#endif
	#endif

							Environment.Exit(0);

//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 'v':
						case 'V':
							c++;
							GlobalMembersGLOBAL.ud.warp_on = 1;
							GlobalMembersGLOBAL.ud.m_volume_number = GlobalMembersGLOBAL.ud.volume_number = Convert.ToInt64(c)-1;
							break;
						case 's':
						case 'S':
							c++;
							GlobalMembersGLOBAL.ud.m_player_skill = GlobalMembersGLOBAL.ud.player_skill = (Convert.ToInt64(c)%5);
							if(GlobalMembersGLOBAL.ud.m_player_skill == 4)
								GlobalMembersGLOBAL.ud.m_respawn_monsters = GlobalMembersGLOBAL.ud.respawn_monsters = 1;
							break;
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							GlobalMembersGLOBAL.ud.warp_on = 2 + (*c) - '0';
							break;
						case 'u':
						case 'U':
							c++;
							j = 0;
							if(*c)
							{
								Console.WriteLine("Using favorite weapon order(s).");
								while(*c)
								{
									GlobalMembersGLOBAL.ud.wchoice[0][j] = *c-'0';
									c++;
									j++;
								}
								while(j < 10)
								{
									if(j == 9)
										GlobalMembersGLOBAL.ud.wchoice[0][9] = 1;
									else
										GlobalMembersGLOBAL.ud.wchoice[0][j] = 2;

									j++;
								}
							}
							else
							{
								Console.WriteLine("Using default weapon orders.");
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
							}

							break;
					}
				}
				i++;
			}
		}
	}



	public static void printstr(short x, short y, ref string @string, sbyte attribute)
	{
			sbyte character;
			short i;
			short pos;

			pos = (y *80+x)<<1;
			i = 0;
			while (@string[i] != 0)
			{
					character = @string[i];
					printchrasm(0xb8000+(int)pos,1,((int)attribute<<8)+(int)character);
					i++;
					pos+=2;
			}
	}

	/*
	void cacheicon(void)
	{
	    if(cachecount > 0)
	    {
	        if( (ps[myconnectindex].gm&MODE_MENU) == 0 )
	            rotatesprite((320-7)<<16,(200-23)<<16,32768L,0,SPINNINGNUKEICON,0,0,2,windowx1,windowy1,windowx2,windowy2);
	        cachecount = 0;
	    }
	}
	       */

	public static void Logo()
	{
		short i;
		short j;
		short soundanm;

		soundanm = 0;

		GlobalMembersGLOBAL.ready2send = 0;

		KB_FlushKeyboardQueue();

		setview(0,0,xdim-1,ydim-1);
		clearview(0);
		GlobalMembersMENUES.palto(0, 0, 0, 63);

		flushperms();
		nextpage();

		MUSIC_StopSong();

	#if VOLUMEALL

		if(!KB_KeyWaiting() && nomorelogohack == 0)
		{
			GlobalMembersGAME.getpackets();
			GlobalMembersMENUES.playanm("logo.anm", 5);
			GlobalMembersMENUES.palto(0, 0, 0, 63);
			KB_FlushKeyboardQueue();
		}

		clearview(0);
		nextpage();
	#endif

		PlayMusic(GlobalMembersGLOBAL.env_music_fn[0, 0]);
		for(i = 0;i<64;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		GlobalMembersGLOBAL.ps[myconnectindex].palette = drealms;
		GlobalMembersMENUES.palto(0, 0, 0, 63);
		rotatesprite(0,0,65536,0,DREALMS,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		while(totalclock < (120 *7) && !KB_KeyWaiting())
			GlobalMembersGAME.getpackets();

		for(i = 0;i<64;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		clearview(0);
		nextpage();

		GlobalMembersGLOBAL.ps[myconnectindex].palette = titlepal;
		flushperms();
		rotatesprite(0,0,65536,0,BETASCREEN,0,0,2+8+16+64,0,0,xdim-1,ydim-1);
		KB_FlushKeyboardQueue();
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;

		while(totalclock < (860+120) && !KB_KeyWaiting())
		{
			rotatesprite(0,0,65536,0,BETASCREEN,0,0,2+8+16+64,0,0,xdim-1,ydim-1);

			if(totalclock > 120 && totalclock < (120+60))
			{
				if(soundanm == 0)
				{
					soundanm = 1;
					GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
				}
				rotatesprite(160<<16,104<<16,(totalclock-120)<<10,0,DUKENUKEM,0,0,2+8,0,0,xdim-1,ydim-1);
			}
			else if(totalclock >= (120+60))
				rotatesprite(160<<16,(104)<<16,60<<10,0,DUKENUKEM,0,0,2+8,0,0,xdim-1,ydim-1);

			if(totalclock > 220 && totalclock < (220+30))
			{
				if(soundanm == 1)
				{
					soundanm = 2;
					GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
				}

				rotatesprite(160<<16,(104)<<16,60<<10,0,DUKENUKEM,0,0,2+8,0,0,xdim-1,ydim-1);
				rotatesprite(160<<16,(129)<<16,(totalclock - 220)<<11,0,THREEDEE,0,0,2+8,0,0,xdim-1,ydim-1);
			}
			else if(totalclock >= (220+30))
				rotatesprite(160<<16,(129)<<16,30<<11,0,THREEDEE,0,0,2+8,0,0,xdim-1,ydim-1);

			if(totalclock >= 280 && totalclock < 395)
			{
				rotatesprite(160<<16,(151)<<16,(410-totalclock)<<12,0,PLUTOPAKSPRITE+1,0,0,2+8,0,0,xdim-1,ydim-1);
				if(soundanm == 2)
				{
					soundanm = 3;
					GlobalMembersSOUNDS.sound(FLY_BY);
				}
			}
			else if(totalclock >= 395)
			{
				if(soundanm == 3)
				{
					soundanm = 4;
					GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
				}
				rotatesprite(160<<16,(151)<<16,30<<11,0,PLUTOPAKSPRITE+1,0,0,2+8,0,0,xdim-1,ydim-1);
			}

			GlobalMembersGAME.getpackets();
			nextpage();
		}

		if(GlobalMembersGLOBAL.ud.multimode > 1)
		{
			rotatesprite(0,0,65536,0,BETASCREEN,0,0,2+8+16+64,0,0,xdim-1,ydim-1);

			rotatesprite(160<<16,(104)<<16,60<<10,0,DUKENUKEM,0,0,2+8,0,0,xdim-1,ydim-1);
			rotatesprite(160<<16,(129)<<16,30<<11,0,THREEDEE,0,0,2+8,0,0,xdim-1,ydim-1);
			rotatesprite(160<<16,(151)<<16,30<<11,0,PLUTOPAKSPRITE+1,0,0,2+8,0,0,xdim-1,ydim-1);

			GlobalMembersGAME.gametext(160, 190, "WAITING FOR PLAYERS", 14, 2);
			nextpage();
		}

		GlobalMembersPREMAP.waitforeverybody();

		flushperms();
		clearview(0);
		nextpage();

		GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
		GlobalMembersSOUNDS.sound(NITEVISION_ONOFF);

		GlobalMembersMENUES.palto(0, 0, 0, 0);
		clearview(0);
	}

	public static void loadtmb()
	{
		string tmb = new string(new char[8000]);
		int fil;
		int l;

		fil = kopen4load("d3dtimbr.tmb",0);
		if(fil == -1)
			return;
		l = kfilelength(fil);
		kread(fil,(string)tmb,l);
		MUSIC_RegisterTimbreBank(tmb);
		kclose(fil);
	}

	/*
	===================
	=
	= ShutDown
	=
	===================
	*/

	public static void ShutDown()
	{
		GlobalMembersSOUNDS.SoundShutdown();
		GlobalMembersSOUNDS.MusicShutdown();
		GlobalMembersGAME.uninittimer();
		uninitengine();
		CONTROL_Shutdown();
		GlobalMembersCONFIG.CONFIG_WriteSetup();
		KB_Shutdown();
	}

	internal static string todd = "Duke Nukem 3D(tm) Copyright 1989, 1996 Todd Replogle and 3D Realms Entertainment";
	internal static string trees = "I want to make a game with trees";
	internal static string sixteen = "16 Possible Dukes";

	/*
	===================
	=
	= Startup
	=
	===================
	*/

	public static void compilecons()
	{
	   GlobalMembersGLOBAL.mymembuf = (string)GlobalMembersGLOBAL.hittype[0];
	   GlobalMembersGLOBAL.labelcode = (int)&sector[0];
	   GlobalMembersGLOBAL.label = (string)&sprite[0];

	//   printf("%ld %ld %ld\n",sizeof(hittype),sizeof(sector),sizeof(sprite));
	//   exit(0);

	   GlobalMembersGAMEDEF.loadefs(ref confilename, ref GlobalMembersGLOBAL.mymembuf);
	   if (GlobalMembersGLOBAL.loadfromgrouponly != 0)
	   {
		   Console.Write("  * Writing defaults to current directory.\n");
		   GlobalMembersGAMEDEF.loadefs(ref confilename, ref GlobalMembersGLOBAL.mymembuf);
	   }
	}


	public static void Startup()
	{
	   int i;

	   KB_Startup();

	   GlobalMembersGAME.compilecons();

	   GlobalMembersCONFIG.CONFIG_GetSetupFilename();
	   GlobalMembersCONFIG.CONFIG_ReadSetup();

	#if AUSTRALIA
	  GlobalMembersGLOBAL.ud.lockout = 1;
	#endif

	   if(CommandSoundToggleOff)
		   GlobalMembersCONFIG.SoundToggle = 0;
	   if(CommandMusicToggleOff)
		   GlobalMembersCONFIG.MusicToggle = 0;

	#if VOLUMEONE

	   Console.Write("\n*** You have run Duke Nukem 3D {0:D} times. ***\n\n",GlobalMembersGLOBAL.ud.executions);
	   if(GlobalMembersGLOBAL.ud.executions >= 50)
		   Console.WriteLine("IT IS NOW TIME TO UPGRADE TO THE COMPLETE VERSION!!!\n");

	#endif

	   CONTROL_Startup(GlobalMembersCONFIG.ControllerType, GlobalMembersGAME.GetTime, TICRATE);

	// CTW - MODIFICATION
	// initengine(ScreenMode,ScreenWidth,ScreenHeight);
	   initengine();
	// CTW END - MODIFICATION
	   GlobalMembersGAME.inittimer();

	   Console.WriteLine("* Hold Esc to Abort. *");
	   Console.WriteLine("Loading art header.");
	   loadpics("tiles000.art");

	   GlobalMembersCONFIG.readsavenames();

	   tilesizx[MIRROR] = tilesizy[MIRROR] = 0;

	   for(i = 0;i<MAXPLAYERS;i++)
		   GlobalMembersGLOBAL.playerreadyflag = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.playerreadyflag, i, 0);
	   initmultiplayers(0,0,0);

	   if(numplayers > 1)
		Console.WriteLine("Multiplayer initialized.");

	   GlobalMembersGLOBAL.ps[myconnectindex].palette = (string) &palette[0];
	   GlobalMembersGAME.SetupGameButtons();

	   if(GlobalMembersGLOBAL.networkmode == 255)
		   GlobalMembersGLOBAL.networkmode = 1;

	   Console.WriteLine("Checking music inits.");
	   GlobalMembersSOUNDS.MusicStartup();
	   Console.WriteLine("Checking sound inits.");
	   GlobalMembersSOUNDS.SoundStartup();
	   GlobalMembersGAME.loadtmb();
	}


	public static void sendscore(ref string s)
	{
		if(numplayers > 1)
		  genericmultifunction(-1,s,s.Length+1,5);
	}


	public static void getnames()
	{
		short i;
		short l;

		for(l = 0;GlobalMembersGLOBAL.myname[l];l++)
		{
			GlobalMembersGLOBAL.ud.user_name[myconnectindex][l] = char.ToUpper(GlobalMembersGLOBAL.myname[l]);
			GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, l+2, char.ToUpper(GlobalMembersGLOBAL.myname[l]));
		}

		if(numplayers > 1)
		{
			GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 0, 6);
			GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 1, BYTEVERSION);

			GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, l+2, 0);
			l += 3;

			for(i = connecthead;i>=0;i = connectpoint2[i])
				if(i != myconnectindex)
					sendpacket(i,GlobalMembersGLOBAL.buf[0],l);

	  //      getpackets();

			l = 1;
			GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 0, 9);

			for(i = 0;i<10;i++)
			{
				GlobalMembersGLOBAL.ud.wchoice[myconnectindex][i] = GlobalMembersGLOBAL.ud.wchoice[0][i];
				GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, l, (sbyte) GlobalMembersGLOBAL.ud.wchoice[0][i]);
				l++;
			}

			for(i = connecthead;i>=0;i = connectpoint2[i])
				if(i != myconnectindex)
					sendpacket(i,GlobalMembersGLOBAL.buf[0],11);

	//        getpackets();

			GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 0, 10);
			GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 1, GlobalMembersGLOBAL.ps[0].aim_mode);
			GlobalMembersGLOBAL.ps[myconnectindex].aim_mode = GlobalMembersGLOBAL.ps[0].aim_mode;

			for(i = connecthead;i>=0;i = connectpoint2[i])
				if(i != myconnectindex)
					sendpacket(i,GlobalMembersGLOBAL.buf,2);

	//        getpackets();

			if(cp == 0)
			{
				GlobalMembersGLOBAL.buf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.buf, 0, 125);

				for(i = connecthead;i>=0;i = connectpoint2[i])
					if(i != myconnectindex)
						sendpacket(i,GlobalMembersGLOBAL.buf,1);
			}

			GlobalMembersGAME.getpackets();

			GlobalMembersPREMAP.waitforeverybody();
		}

		if(cp == 1)
			GlobalMembersGAME.gameexit("Please put Duke Nukem 3D Atomic Edition CD in drive.");
	}

	public static void writestring(int a1,int a2,int a3,short a4,int vx,int vy,int vz)
	{

		FILE fp;

		fp = (FILE)fopen("debug.txt","rt+");

		fprintf(fp,"%ld %ld %ld %ld %ld %ld %ld\n",a1,a2,a3,a4,vx,vy,vz);

		fclose(fp);

	}


	public static sbyte testcd(ref string fn)
	{

	 short drive_count;
	 short drive;
	 int dalen = 0;
	 find_t dafilet = new find_t();
	 int fil;

	 _REGS ir = new _REGS();
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
	// union _REGS ||;
	// struct _SREGS sr;

	 if(DefineConstants.IDFSIZE != 9961476)
	 {
		 drive = char.ToUpper(fn)-'A';

		 ir.w.ax = 0x1500;
		 ir.w.bx = 0; // check that MSCDEX is installed
		 int386(0x2f, ir, ||);
		 drive_count = ||.w.bx;

		 if(drive_count == 0)
			 return 1;

		 ir.w.ax = 0x150b;
		 ir.w.bx = 0;
		 ir.w.cx = drive;
		 int386(0x2f, ir, ||);

		 if (||.w.ax == 0 || ||.w.bx != 0xadad)
			 return 1;

		 ir.w.ax = 0x1502;
		 ir.w.bx = FP_OFF(GlobalMembersGLOBAL.buf);
		 sr.es = FP_SEG(GlobalMembersGLOBAL.buf);
		 ir.w.cx = drive;
		 int386x(0x2f, ir, ||, sr);

		 if(||.h.al == 0 || ||.h.al == 30)
			 return 1;

	  }

	  fil = open(fn,O_RDONLY,S_IREAD);

	  if (fil < 0)
		  return 1;

	  // ( DO A SEE/Byte check here.) (Not coded in this version)


	  dalen = filelength(fil);

	  close(fil);

	  return(dalen != DefineConstants.IDFSIZE);

	}


	public static void copyprotect()
	{
		FILE fp;
		string idfile = new string(new char[256]);

		return;

		cp = 0;

		fp = (FILE)fopen("cdrom.ini","rt");
		if(fp == (FILE) null)
		{
			cp = 1;
			return;
		}

		fscanf(fp,"%s",idfile);
		fclose(fp);

		idfile += DefineConstants.IDFILENAME;

		if (GlobalMembersGAME.testcd(ref idfile) != 0)
		{
			cp = 1;
			return;
		}
	}

	static void Main(void argc, string[] args)
	{
		int i;
		int j;
		int k;
		int l;
		int32 tempautorun;

		GlobalMembersGAME.copyprotect();

		todd[0] = 'T';
		sixteen[0] = 'D';
		trees[0] = 'I';

		setvmode(0x03);

		GlobalMembersGAME.printstr(0, 0, "                                                                                ", 79);

	#if VOLUMEALL
		#if AUSTRALIA
			GlobalMembersGAME.printstr(40-(Convert.ToString("Duke Nukem 3D AUSSIE Full Version v"DefineConstants.VERSION).Length>>1), 0, ref "Duke Nukem 3D AUSSIE Full Version v"DefineConstants.VERSION, 79);
		#else
	#if HEAD2_AlternateDefinition1
			GlobalMembersGAME.printstr(40-(Convert.ToString("Duke Nukem 3D v"DefineConstants.VERSION" - Atomic Edition").Length>>1), 0, ref "Duke Nukem 3D v"DefineConstants.VERSION" - Atomic Edition", 79);
	#elif HEAD2_AlternateDefinition2
			GlobalMembersGAME.printstr(40-(Convert.ToString("Duke Nukem 3D Full Version v"DefineConstants.VERSION).Length>>1), 0, ref "Duke Nukem 3D Full Version v"DefineConstants.VERSION, 79);
	#endif
		#endif
	#else
		#if AUSTRALIA
			GlobalMembersGAME.printstr(40-(Convert.ToString("Duke Nukem 3D AUSSIE Unregistered Shareware v"DefineConstants.VERSION).Length>>1), 0, ref "Duke Nukem 3D AUSSIE Unregistered Shareware v"DefineConstants.VERSION, 79);
		#else
			GlobalMembersGAME.printstr(40-(Convert.ToString("Duke Nukem 3D Unregistered Shareware v"DefineConstants.VERSION" ").Length>>1), 0, ref "Duke Nukem 3D Unregistered Shareware v"DefineConstants.VERSION" ", 79);
		#endif
	#endif

	#if BETA
		GlobalMembersGAME.printstr(0, 1, "BETA VERSION BETA VERSION BETA VERSION BETA VERSION BETA VERSION BETA VERSION ", 79);
	#endif

	#if ONELEVELDEMO
		GlobalMembersGAME.printstr(33, 1, "ONE LEVEL DEMO", 79);
	#endif

		GlobalMembersGLOBAL.ud.multimode = 1;
		GlobalMembersGAME.printstr(0, 1, "                   Copyright (c) 1996 3D Realms Entertainment                   ", 79);

	//    printstr(0,2,"  ***     DUKE NUKEM v1.4 BETA VERSION.  USED FOR INTERNAL USE ONLY!!!     ***  ",79);

		Console.Write("\n\n");

		initgroupfile("duke3d.grp");

		GlobalMembersGAME.checkcommandline(argc, ref args);

		GlobalMembersGLOBAL.totalmemory = Z_AvailHeap();

		if(memorycheckoveride == 0)
		{
			if(GlobalMembersGLOBAL.totalmemory < (3162000-350000))
			{
				Console.WriteLine("You don't have enough free memory to run Duke Nukem 3D.");
				Console.WriteLine("The DOS \"mem\" command should report 6,800K (or 6.8 megs)");
				Console.WriteLine("of \"total memory free\".\n");
				Console.Write("Duke Nukem 3D requires {0:D} more bytes to run.\n",3162000-350000-GlobalMembersGLOBAL.totalmemory);
				Environment.Exit(0);
			}
		}
		else
			Console.Write("Using {0:D} bytes for heap.\n",GlobalMembersGLOBAL.totalmemory);

	#if ! ONELEVELDEMO
	// CTW - REMOVED
	/*  if(movesperpacket == 4)
	        TENtext();*/
	// CTW END - REMOVED
	#endif

		RegisterShutdownFunction(GlobalMembersGAME.ShutDown);

	#if VOLUMEONE
		Console.WriteLine("Distribution of shareware Duke Nukem 3D is restricted in certain ways.");
		Console.WriteLine("Please read LICENSE.DOC for more details.\n");
	#endif
	#if ONELEVELDEMO
		Console.WriteLine("DUKE NUKEM 3D SINGLE-LEVEL PROMOTIONAL EDITION\n");
		Console.WriteLine("This single-level promotional edition of Duke Nukem 3D (tm) may not be");
		Console.WriteLine("distributed domestically (North America) by any publication other than");
		Console.WriteLine("Computer Gaming World, a Ziff-Davis publication.  It is a promotional");
		Console.WriteLine("version, licensed for a single month's run, and may not be redistributed");
		Console.WriteLine("by any online service, BBS, commercial publisher, magazine or distributor.");
		Console.WriteLine("International distribution rights are reserved.\n");
		Console.WriteLine("Please read LICENSE.DOC for further information about this special version.");
		Console.WriteLine("NOTE: DUKE NUKEM 3D CONTAINS MATURE CONTENT.\n");
		Console.WriteLine("Press any key to continue.");
		Console.ReadKey(true).KeyChar;
	#endif

		GlobalMembersGAME.Startup();

		if(eightytwofifty && numplayers > 1 && (GlobalMembersCONFIG.MusicDevice != NumSoundCards))
		{
			Console.WriteLine("\n=========================================================================");
			Console.WriteLine("WARNING: 8250 UART detected.");
			Console.WriteLine("Music is being disabled and lower quality sound is being set.  We apologize");
			Console.WriteLine("for this, but it is necessary to maintain high frame rates while trying to");
			Console.WriteLine("play the game on an 8250.  We suggest upgrading to a 16550 or better UART");
			Console.WriteLine("for maximum performance.  Press any key to continue.");
			Console.WriteLine("=========================================================================\n");

			while(!KB_KeyWaiting())
				GlobalMembersGAME.getpackets();
		}

		if(numplayers > 1)
		{
			GlobalMembersGLOBAL.ud.multimode = numplayers;
			sendlogon();
		}
		else if(boardfilename[0] != 0)
		{
			GlobalMembersGLOBAL.ud.m_level_number = 7;
			GlobalMembersGLOBAL.ud.m_volume_number = 0;
			GlobalMembersGLOBAL.ud.warp_on = 1;
		}

		GlobalMembersGAME.getnames();

		if(GlobalMembersGLOBAL.ud.multimode > 1)
		{
			playerswhenstarted = GlobalMembersGLOBAL.ud.multimode;

			if(GlobalMembersGLOBAL.ud.warp_on == 0)
			{
				GlobalMembersGLOBAL.ud.m_monsters_off = 1;
				GlobalMembersGLOBAL.ud.m_player_skill = 0;
			}
		}

		GlobalMembersGLOBAL.ud.last_level = -1;

	   GlobalMembersRTS.RTS_Init(ref GlobalMembersGLOBAL.ud.rtsname);
	   if (GlobalMembersRTS.numlumps != 0)
		   Console.Write("Using .RTS file:{0}\n",GlobalMembersGLOBAL.ud.rtsname);

	   if (CONTROL_JoystickEnabled)
		   CONTROL_CenterJoystick (GlobalMembersGAME.CenterCenter, GlobalMembersGAME.UpperLeft, GlobalMembersGAME.LowerRight, GlobalMembersGAME.CenterThrottle, GlobalMembersGAME.CenterRudder);

			Console.WriteLine("Loading palette/lookups.");

	// CTW - MODIFICATION
	/*  if( setgamemode(ScreenMode,ScreenWidth,ScreenHeight) < 0 )
	    {
	        printf("\nVESA driver for ( %i * %i ) not found/supported!\n",xdim,ydim);
	        vidoption = 2;
	        setgamemode(vidoption,320,200);
	    }*/
		if(setgamemode(GlobalMembersCONFIG.ScreenMode,GlobalMembersCONFIG.ScreenWidth,GlobalMembersCONFIG.ScreenHeight) < 0)
		{
			Console.Write("\nVESA driver for ( {0:D} * {1:D} ) not found/supported!\n",xdim,ydim);
			GlobalMembersCONFIG.ScreenMode = 2;
			GlobalMembersCONFIG.ScreenWidth = 320;
			GlobalMembersCONFIG.ScreenHeight = 200;
			setgamemode(GlobalMembersCONFIG.ScreenMode,GlobalMembersCONFIG.ScreenWidth,GlobalMembersCONFIG.ScreenHeight);
		}
	// CTW END - MODIFICATION

		GlobalMembersPREMAP.genspriteremaps();

	#if VOLUMEONE
			if(numplayers > 4 || GlobalMembersGLOBAL.ud.multimode > 4)
				GlobalMembersGAME.gameexit(" The full version of Duke Nukem 3D supports 5 or more players.");
	#endif

		setbrightness(GlobalMembersGLOBAL.ud.brightness>>2,GlobalMembersGLOBAL.ps[myconnectindex].palette[0]);

		ESCESCAPE;

		FX_StopAllSounds();
		GlobalMembersSOUNDS.clearsoundlocks();

		if(GlobalMembersGLOBAL.ud.warp_on > 1 && GlobalMembersGLOBAL.ud.multimode < 2)
		{
			clearview(0);
			GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
			GlobalMembersMENUES.palto(0, 0, 0, 0);
			rotatesprite(320<<15,200<<15,65536,0,LOADSCREEN,0,0,2+8+64,0,0,xdim-1,ydim-1);
			GlobalMembersMENUES.menutext(160, 105, 0, 0, "LOADING SAVED GAME...");
			nextpage();

			j = GlobalMembersMENUES.loadplayer(GlobalMembersGLOBAL.ud.warp_on-2);
			if (j != 0)
				GlobalMembersGLOBAL.ud.warp_on = 0;
		}

	//    getpackets();

		MAIN_LOOP_RESTART:

		if(GlobalMembersGLOBAL.ud.warp_on == 0)
			GlobalMembersGAME.Logo();
		else if(GlobalMembersGLOBAL.ud.warp_on == 1)
		{
			GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.m_volume_number, GlobalMembersGLOBAL.ud.m_level_number, GlobalMembersGLOBAL.ud.m_player_skill);
			GlobalMembersPREMAP.enterlevel(MODE_GAME);
		}
		else
			GlobalMembersPREMAP.vscrn();

		tempautorun = GlobalMembersGLOBAL.ud.auto_run;

		if(GlobalMembersGLOBAL.ud.warp_on == 0 && GlobalMembersGAME.playback() != 0)
		{
			FX_StopAllSounds();
			GlobalMembersSOUNDS.clearsoundlocks();
			nomorelogohack = 1;
			goto MAIN_LOOP_RESTART;
		}

		GlobalMembersGLOBAL.ud.auto_run = tempautorun;

		GlobalMembersGLOBAL.ud.warp_on = 0;

		while (!(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_END)) //The whole loop!!!!!!!!!!!!!!!!!!
		{
			if(GlobalMembersGLOBAL.ud.recstat == 2 || GlobalMembersGLOBAL.ud.multimode > 1 || (GlobalMembersGLOBAL.ud.show_help == 0 && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) != MODE_MENU))
				if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_GAME)
					if (GlobalMembersGAME.moveloop() != 0)
						continue;

			if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_EOL || GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_RESTART)
			{
				if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_EOL)
				{
	#if ONELEVELDEMO
					GlobalMembersGAME.gameexit(" ");
	#endif
					GlobalMembersGAME.closedemowrite();

					GlobalMembersGLOBAL.ready2send = 0;

					i = GlobalMembersGLOBAL.ud.screen_size;
					GlobalMembersGLOBAL.ud.screen_size = 0;
					GlobalMembersPREMAP.vscrn();
					GlobalMembersGLOBAL.ud.screen_size = i;
					GlobalMembersGAME.dobonus(0);

					if(GlobalMembersGLOBAL.ud.eog)
					{
						GlobalMembersGLOBAL.ud.eog = 0;
						if(GlobalMembersGLOBAL.ud.multimode < 2)
						{
	#if ! VOLUMEALL
							GlobalMembersGAME.doorders();
	#endif
							GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_MENU;
							GlobalMembersMENUES.cmenu(0);
							GlobalMembersMENUES.probey = 0;
							goto MAIN_LOOP_RESTART;
						}
						else
						{
							GlobalMembersGLOBAL.ud.m_level_number = 0;
							GlobalMembersGLOBAL.ud.level_number = 0;
						}
					}
				}

				GlobalMembersGLOBAL.ready2send = 0;
				if(numplayers > 1)
					GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_GAME;
				GlobalMembersPREMAP.enterlevel(GlobalMembersGLOBAL.ps[myconnectindex].gm);
				continue;
			}

			GlobalMembersGAME.cheats();
			GlobalMembersGAME.nonsharedkeys();

			if((GlobalMembersGLOBAL.ud.show_help == 0 && GlobalMembersGLOBAL.ud.multimode < 2 && !(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU)) || GlobalMembersGLOBAL.ud.multimode > 1 || GlobalMembersGLOBAL.ud.recstat == 2)
				i = min(max((totalclock-GlobalMembersGLOBAL.ototalclock)*(65536/TICSPERFRAME),0),65536);
			else
				i = 65536;

			GlobalMembersGAME.displayrooms(GlobalMembersGLOBAL.screenpeek, i);
			GlobalMembersGAME.displayrest(i);

	//        if( KB_KeyPressed(sc_F) )
	//        {
	//            KB_ClearKeyDown(sc_F);
	//            addplayer();
	//        }

			if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_DEMO)
				goto MAIN_LOOP_RESTART;

			if(debug_on)
				GlobalMembersGAME.caches();

			GlobalMembersGAME.checksync();

	#if VOLUMEONE
			if(GlobalMembersGLOBAL.ud.show_help == 0 && GlobalMembersGLOBAL.show_shareware > 0 && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) == 0)
				rotatesprite((320-50)<<16,9<<16,65536,0,BETAVERSION,0,0,2+8+16+128,0,0,xdim-1,ydim-1);
	#endif
			nextpage();
		}

		GlobalMembersGAME.gameexit(" ");
	}

	public static sbyte opendemoread(sbyte which_demo) // 0 = mine
	{
		string d = "demo_.dmo";
		sbyte ver;
		short i;

		if(which_demo == 10)
			d = StringFunctions.ChangeCharacter(d, 4, 'x');
		else
			d = StringFunctions.ChangeCharacter(d, 4, '0' + which_demo);

		GlobalMembersGLOBAL.ud.reccnt = 0;

		 if(which_demo == 1 && firstdemofile[0] != 0)
		 {
		   if ((recfilep = kopen4load(firstdemofile,GlobalMembersGLOBAL.loadfromgrouponly)) == -1)
			   return(0);
		 }
		 else
		   if ((recfilep = kopen4load(d,GlobalMembersGLOBAL.loadfromgrouponly)) == -1)
			   return(0);

		 kread(recfilep,GlobalMembersGLOBAL.ud.reccnt,sizeof(int));
		 kread(recfilep, ver, sizeof(sbyte));
		 if((ver != BYTEVERSION)) // || (ud.reccnt < 512) )
		 {
			kclose(recfilep);
			return 0;
		 }
			 kread(recfilep,(string)GlobalMembersGLOBAL.ud.volume_number,sizeof(sbyte));
			 kread(recfilep,(string)GlobalMembersGLOBAL.ud.level_number,sizeof(sbyte));
			 kread(recfilep,(string)GlobalMembersGLOBAL.ud.player_skill,sizeof(sbyte));
		 kread(recfilep,(string)GlobalMembersGLOBAL.ud.m_coop,sizeof(sbyte));
		 kread(recfilep,(string)GlobalMembersGLOBAL.ud.m_ffire,sizeof(sbyte));
		 kread(recfilep,(short)GlobalMembersGLOBAL.ud.multimode,sizeof(short));
		 kread(recfilep,(short)GlobalMembersGLOBAL.ud.m_monsters_off,sizeof(short));
		 kread(recfilep,(int32)GlobalMembersGLOBAL.ud.m_respawn_monsters,sizeof(int32));
		 kread(recfilep,(int32)GlobalMembersGLOBAL.ud.m_respawn_items,sizeof(int32));
		 kread(recfilep,(int32)GlobalMembersGLOBAL.ud.m_respawn_inventory,sizeof(int32));
		 kread(recfilep,(int32)GlobalMembersGLOBAL.ud.playerai,sizeof(int32));
		 kread(recfilep,(string)GlobalMembersGLOBAL.ud.user_name[0][0],sizeof(GlobalMembersGLOBAL.ud.user_name),1);
		 kread(recfilep,(int32)GlobalMembersGLOBAL.ud.auto_run,sizeof(int32));
		 kread(recfilep,(string)boardfilename,sizeof(boardfilename));
		 if(boardfilename[0] != 0)
		 {
			GlobalMembersGLOBAL.ud.m_level_number = 7;
			GlobalMembersGLOBAL.ud.m_volume_number = 0;
		 }

		 for(i = 0;i<GlobalMembersGLOBAL.ud.multimode;i++)
			kread(recfilep,(int32)GlobalMembersGLOBAL.ps[i].aim_mode,sizeof(sbyte),1);
		 GlobalMembersGLOBAL.ud.god = GlobalMembersGLOBAL.ud.cashman = GlobalMembersGLOBAL.ud.eog = GlobalMembersGLOBAL.ud.showallmap = 0;
		 GlobalMembersGLOBAL.ud.clipping = GlobalMembersGLOBAL.ud.scrollmode = GlobalMembersGLOBAL.ud.overhead_on = 0;
		 GlobalMembersGLOBAL.ud.showweapons = GlobalMembersGLOBAL.ud.pause_on = GlobalMembersGLOBAL.ud.auto_run = 0;

			 GlobalMembersPREMAP.newgame(GlobalMembersGLOBAL.ud.volume_number, GlobalMembersGLOBAL.ud.level_number, GlobalMembersGLOBAL.ud.player_skill);
			 return(1);
	}


	public static void opendemowrite()
	{
		string d = "demo1.dmo";
		int dummylong = 0;
		sbyte ver;
		short i;

		if(GlobalMembersGLOBAL.ud.recstat == 2)
			kclose(recfilep);

		ver = BYTEVERSION;

	// CTW - MODIFICATION
	//  if ((frecfilep = fopen(d,"wb")) == -1) return;
		if ((frecfilep = fopen(d,"wb")) == null)
			return;
	// CTW END - MODIFICATION
		fwrite(dummylong, 4, 1, frecfilep);
		fwrite(ver, sizeof(sbyte), 1, frecfilep);
		fwrite((string)GlobalMembersGLOBAL.ud.volume_number,sizeof(sbyte),1,frecfilep);
		fwrite((string)GlobalMembersGLOBAL.ud.level_number,sizeof(sbyte),1,frecfilep);
		fwrite((string)GlobalMembersGLOBAL.ud.player_skill,sizeof(sbyte),1,frecfilep);
		fwrite((string)GlobalMembersGLOBAL.ud.m_coop,sizeof(sbyte),1,frecfilep);
		fwrite((string)GlobalMembersGLOBAL.ud.m_ffire,sizeof(sbyte),1,frecfilep);
		fwrite((short)GlobalMembersGLOBAL.ud.multimode,sizeof(short),1,frecfilep);
		fwrite((short)GlobalMembersGLOBAL.ud.m_monsters_off,sizeof(short),1,frecfilep);
		fwrite((int32)GlobalMembersGLOBAL.ud.m_respawn_monsters,sizeof(int32),1,frecfilep);
		fwrite((int32)GlobalMembersGLOBAL.ud.m_respawn_items,sizeof(int32),1,frecfilep);
		fwrite((int32)GlobalMembersGLOBAL.ud.m_respawn_inventory,sizeof(int32),1,frecfilep);
		fwrite((int32)GlobalMembersGLOBAL.ud.playerai,sizeof(int32),1,frecfilep);
		fwrite((string)GlobalMembersGLOBAL.ud.user_name[0][0],sizeof(GlobalMembersGLOBAL.ud.user_name),1,frecfilep);
		fwrite((int32)GlobalMembersGLOBAL.ud.auto_run,sizeof(int32),1,frecfilep);
		fwrite((string)boardfilename,sizeof(boardfilename),1,frecfilep);

		for(i = 0;i<GlobalMembersGLOBAL.ud.multimode;i++)
			fwrite((int32)GlobalMembersGLOBAL.ps[i].aim_mode,sizeof(sbyte),1,frecfilep);

		totalreccnt = 0;
		GlobalMembersGLOBAL.ud.reccnt = 0;
	}

	public static void record()
	{
		short i;

		for(i = connecthead;i>=0;i = connectpoint2[i])
			 {
			 copybufbyte(GlobalMembersGLOBAL.sync[i],GlobalMembersGLOBAL.recsync[GlobalMembersGLOBAL.ud.reccnt],sizeof(input));
					 GlobalMembersGLOBAL.ud.reccnt++;
					 totalreccnt++;
					 if (GlobalMembersGLOBAL.ud.reccnt >= RECSYNCBUFSIZ)
					 {
				  dfwrite(GlobalMembersGLOBAL.recsync,sizeof(input)*GlobalMembersGLOBAL.ud.multimode,GlobalMembersGLOBAL.ud.reccnt/GlobalMembersGLOBAL.ud.multimode,frecfilep);
							  GlobalMembersGLOBAL.ud.reccnt = 0;
					 }
			 }
	}

	public static void closedemowrite()
	{
			 if (GlobalMembersGLOBAL.ud.recstat == 1)
			 {
			if (GlobalMembersGLOBAL.ud.reccnt > 0)
			{
				dfwrite(GlobalMembersGLOBAL.recsync,sizeof(input)*GlobalMembersGLOBAL.ud.multimode,GlobalMembersGLOBAL.ud.reccnt/GlobalMembersGLOBAL.ud.multimode,frecfilep);

				fseek(frecfilep,SEEK_SET,0);
				fwrite(totalreccnt, sizeof(int), 1, frecfilep);
				GlobalMembersGLOBAL.ud.recstat = GlobalMembersGLOBAL.ud.m_recstat = 0;
			}
			fclose(frecfilep);
		}
	}

	// CTW - MODIFICATION
	// On my XP machine, demo playback causes the game to crash shortly in.
	// Only bug found so far, not sure if it's OS dependent or compiler or what.
	// Seems to happen when player input starts being simulated, but just guessing.
	// This change effectively disables it. The related code is still enabled.
	// char which_demo = 1;
	public static sbyte which_demo = 0;
	// CTW END - MODIFICATION

	public static sbyte in_menu = 0;

	// extern long syncs[];
	public static int playback()
	{
		int i;
		int j;
		int k;
		int l;
		int t;
		short p;
		sbyte foundemo;

		if (GlobalMembersGLOBAL.ready2send != 0)
			return 0;

		foundemo = 0;

		RECHECK:

		in_menu = GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU;

		GlobalMembersGLOBAL.pub = NUMPAGES;
		GlobalMembersGLOBAL.pus = NUMPAGES;

		flushperms();

		if(numplayers < 2)
			foundemo = GlobalMembersGAME.opendemoread(which_demo);

		if(foundemo == 0)
		{
			if(which_demo > 1)
			{
				which_demo = 1;
				goto RECHECK;
			}
			for(t = 0;t<63;t+=7)
				GlobalMembersMENUES.palto(0, 0, 0, t);
			GlobalMembersGAME.drawbackground();
			GlobalMembersMENUES.menus();
			GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
			nextpage();
			for(t = 63;t>0;t-=7)
				GlobalMembersMENUES.palto(0, 0, 0, t);
			GlobalMembersGLOBAL.ud.reccnt = 0;
		}
		else
		{
			GlobalMembersGLOBAL.ud.recstat = 2;
			which_demo++;
			if(which_demo == 10)
				which_demo = 1;
			GlobalMembersPREMAP.enterlevel(MODE_DEMO);
		}

		if(foundemo == 0 || in_menu || KB_KeyWaiting() || numplayers > 1)
		{
			FX_StopAllSounds();
			GlobalMembersSOUNDS.clearsoundlocks();
			GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;
		}

		GlobalMembersGLOBAL.ready2send = 0;
		i = 0;

		KB_FlushKeyboardQueue();

		k = 0;

		while (GlobalMembersGLOBAL.ud.reccnt > 0 || foundemo == 0)
		{
			if (foundemo != 0)
				while (totalclock >= (GlobalMembersGLOBAL.lockclock+TICSPERFRAME))
			{
				if ((i == 0) || (i >= RECSYNCBUFSIZ))
				{
					i = 0;
					l = min(GlobalMembersGLOBAL.ud.reccnt,RECSYNCBUFSIZ);
					kdfread(GlobalMembersGLOBAL.recsync,sizeof(input)*GlobalMembersGLOBAL.ud.multimode,l/GlobalMembersGLOBAL.ud.multimode,recfilep);
				}

				for(j = connecthead;j>=0;j = connectpoint2[j])
				{
				   copybufbyte(GlobalMembersGLOBAL.recsync[i], GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoend[j]&(MOVEFIFOSIZ-1), j], sizeof(input));
				   GlobalMembersGLOBAL.movefifoend[j]++;
				   i++;
				   GlobalMembersGLOBAL.ud.reccnt--;
				}
				GlobalMembersGAME.domovethings();
			}

			if(foundemo == 0)
				GlobalMembersGAME.drawbackground();
			else
			{
				GlobalMembersGAME.nonsharedkeys();

				j = min(max((totalclock-GlobalMembersGLOBAL.lockclock)*(65536/TICSPERFRAME),0),65536);
				GlobalMembersGAME.displayrooms(GlobalMembersGLOBAL.screenpeek, j);
				GlobalMembersGAME.displayrest(j);

				if(GlobalMembersGLOBAL.ud.multimode > 1 && GlobalMembersGLOBAL.ps[myconnectindex].gm)
					GlobalMembersGAME.getpackets();
			}

			if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_EOL))
				goto RECHECK;

			if (KB_KeyPressed(sc_Escape))
			{
				KB_ClearKeyDown(sc_Escape);
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();
				GlobalMembersGLOBAL.ps[myconnectindex].gm |= MODE_MENU;
				GlobalMembersMENUES.cmenu(0);
				GlobalMembersSOUNDS.intomenusounds();
			}

			if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_TYPE)
			{
				GlobalMembersGAME.typemode();
				if((GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_TYPE) != MODE_TYPE)
					GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_MENU;
			}
			else
			{
				GlobalMembersMENUES.menus();
				if(GlobalMembersGLOBAL.ud.multimode > 1)
				{
					ControlInfo noshareinfo = new ControlInfo();
					CONTROL_GetInput(noshareinfo);
					if(BUTTON(gamefunc_SendMessage))
					{
						KB_FlushKeyboardQueue();
						CONTROL_ClearButton(gamefunc_SendMessage);
						GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_TYPE;
						GlobalMembersGLOBAL.typebuf = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.typebuf, 0, 0);
						inputloc = 0;
					}
				}
			}

			GlobalMembersGAME.operatefta();

			if(GlobalMembersGLOBAL.ud.last_camsprite != GlobalMembersGLOBAL.ud.camerasprite)
			{
				GlobalMembersGLOBAL.ud.last_camsprite = GlobalMembersGLOBAL.ud.camerasprite;
				GlobalMembersGLOBAL.ud.camera_time = totalclock+(TICRATE *2);
			}

	#if VOLUMEONE
			if(GlobalMembersGLOBAL.ud.show_help == 0 && (GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU) == 0)
				rotatesprite((320-50)<<16,9<<16,65536,0,BETAVERSION,0,0,2+8+16+128,0,0,xdim-1,ydim-1);
	#endif
			GlobalMembersGAME.getpackets();
			nextpage();

			if(GlobalMembersGLOBAL.ps[myconnectindex].gm==MODE_END || GlobalMembersGLOBAL.ps[myconnectindex].gm==MODE_GAME)
			{
				if (foundemo != 0)
					kclose(recfilep);
				return 0;
			}
		}
		kclose(recfilep);
		if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_MENU)
			goto RECHECK;
		return 1;
	}

	public static sbyte moveloop()
	{
		int i;

		if (numplayers > 1)
			while (GlobalMembersGLOBAL.fakemovefifoplc < GlobalMembersGLOBAL.movefifoend[myconnectindex])
				GlobalMembersGAME.fakedomovethings();

		GlobalMembersGAME.getpackets();

		if (numplayers < 2)
			GlobalMembersGLOBAL.bufferjitter = 0;
		while (GlobalMembersGLOBAL.movefifoend[myconnectindex]-GlobalMembersGLOBAL.movefifoplc > GlobalMembersGLOBAL.bufferjitter)
		{
			for(i = connecthead;i>=0;i = connectpoint2[i])
				if (GlobalMembersGLOBAL.movefifoplc == GlobalMembersGLOBAL.movefifoend[i])
					break;
			if (i >= 0)
				break;
			if (GlobalMembersGAME.domovethings() != 0)
				return 1;
		}
		return 0;
	}

	public static void fakedomovethingscorrect()
	{
		 int i;
		 player_struct p;

		 if (numplayers < 2)
			 return;

		 i = ((GlobalMembersGLOBAL.movefifoplc-1)&(MOVEFIFOSIZ-1));
		 p = GlobalMembersGLOBAL.ps[myconnectindex];

		 if (p.posx == GlobalMembersGLOBAL.myxbak[i] && p.posy == GlobalMembersGLOBAL.myybak[i] && p.posz == GlobalMembersGLOBAL.myzbak[i] && p.horiz == GlobalMembersGLOBAL.myhorizbak[i] && p.ang == GlobalMembersGLOBAL.myangbak[i])
			 return;

		 GlobalMembersGLOBAL.myx = p.posx;
		 GlobalMembersGLOBAL.omyx = p.oposx;
		 GlobalMembersGLOBAL.myxvel = p.posxv;
		 GlobalMembersGLOBAL.myy = p.posy;
		 GlobalMembersGLOBAL.omyy = p.oposy;
		 GlobalMembersGLOBAL.myyvel = p.posyv;
		 GlobalMembersGLOBAL.myz = p.posz;
		 GlobalMembersGLOBAL.omyz = p.oposz;
		 GlobalMembersGLOBAL.myzvel = p.poszv;
		 GlobalMembersGLOBAL.myang = p.ang;
		 GlobalMembersGLOBAL.omyang = p.oang;
		 GlobalMembersGLOBAL.mycursectnum = p.cursectnum;
		 GlobalMembersGLOBAL.myhoriz = p.horiz;
		 GlobalMembersGLOBAL.omyhoriz = p.ohoriz;
		 GlobalMembersGLOBAL.myhorizoff = p.horizoff;
		 GlobalMembersGLOBAL.omyhorizoff = p.ohorizoff;
		 GlobalMembersGLOBAL.myjumpingcounter = p.jumping_counter;
		 GlobalMembersGLOBAL.myjumpingtoggle = p.jumping_toggle;
		 GlobalMembersGLOBAL.myonground = p.on_ground;
		 GlobalMembersGLOBAL.myhardlanding = p.hard_landing;
		 GlobalMembersGLOBAL.myreturntocenter = p.return_to_center;

		 GlobalMembersGLOBAL.fakemovefifoplc = GlobalMembersGLOBAL.movefifoplc;
		 while (GlobalMembersGLOBAL.fakemovefifoplc < GlobalMembersGLOBAL.movefifoend[myconnectindex])
			  GlobalMembersGAME.fakedomovethings();

	}

	public static void fakedomovethings()
	{
			input syn;
			player_struct p;
			int i;
			int j;
			int k;
			int doubvel;
			int fz;
			int cz;
			int hz;
			int lz;
			int x;
			int y;
			uint sb_snum;
			short psect;
			short psectlotag;
			short tempsect;
			short backcstat;
			sbyte shrunk;
			sbyte spritebridge;

			syn = (input)GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.fakemovefifoplc&(MOVEFIFOSIZ-1), myconnectindex];

			p = GlobalMembersGLOBAL.ps[myconnectindex];

			backcstat = sprite[p.i].cstat;
			sprite[p.i].cstat &= ~257;

			sb_snum = syn.bits;

			psect = GlobalMembersGLOBAL.mycursectnum;
			psectlotag = sector[psect].lotag;
			spritebridge = 0;

			shrunk = (sprite[p.i].yrepeat < 32);

			if(GlobalMembersGLOBAL.ud.clipping == 0 && (sector[psect].floorpicnum == MIRROR || psect < 0 || psect >= MAXSECTORS))
			{
				GlobalMembersGLOBAL.myx = GlobalMembersGLOBAL.omyx;
				GlobalMembersGLOBAL.myy = GlobalMembersGLOBAL.omyy;
			}
			else
			{
				GlobalMembersGLOBAL.omyx = GlobalMembersGLOBAL.myx;
				GlobalMembersGLOBAL.omyy = GlobalMembersGLOBAL.myy;
			}

			GlobalMembersGLOBAL.omyhoriz = GlobalMembersGLOBAL.myhoriz;
			GlobalMembersGLOBAL.omyhorizoff = GlobalMembersGLOBAL.myhorizoff;
			GlobalMembersGLOBAL.omyz = GlobalMembersGLOBAL.myz;
			GlobalMembersGLOBAL.omyang = GlobalMembersGLOBAL.myang;

			getzrange(GlobalMembersGLOBAL.myx, GlobalMembersGLOBAL.myy, GlobalMembersGLOBAL.myz, psect, cz, hz, fz, lz, 163, CLIPMASK0);

			j = getflorzofslope(psect,GlobalMembersGLOBAL.myx,GlobalMembersGLOBAL.myy);

			if((lz &49152) == 16384 && psectlotag == 1 && klabs(GlobalMembersGLOBAL.myz-j) > PHEIGHT+(16<<8))
				psectlotag = 0;

			if(p.aim_mode == 0 && GlobalMembersGLOBAL.myonground && psectlotag != 2 && (sector[psect].floorstat &2))
			{
					x = GlobalMembersGLOBAL.myx+(sintable[(GlobalMembersGLOBAL.myang+512)&2047]>>5);
					y = GlobalMembersGLOBAL.myy+(sintable[GlobalMembersGLOBAL.myang &2047]>>5);
					tempsect = psect;
					updatesector(x, y, tempsect);
					if (tempsect >= 0)
					{
						 k = getflorzofslope(psect,x,y);
						 if (psect == tempsect)
							  GlobalMembersGLOBAL.myhorizoff += mulscale16(j-k,160);
						 else if (klabs(getflorzofslope(tempsect,x,y)-k) <= (4<<8))
							  GlobalMembersGLOBAL.myhorizoff += mulscale16(j-k,160);
					}
			}
			if (GlobalMembersGLOBAL.myhorizoff > 0)
				GlobalMembersGLOBAL.myhorizoff -= ((GlobalMembersGLOBAL.myhorizoff>>3)+1);
			else if (GlobalMembersGLOBAL.myhorizoff < 0)
				GlobalMembersGLOBAL.myhorizoff += (((-GlobalMembersGLOBAL.myhorizoff)>>3)+1);

			if(hz >= 0 && (hz &49152) == 49152)
			{
					hz &= (MAXSPRITES-1);
					if (sprite[hz].statnum == 1 && sprite[hz].extra >= 0)
					{
						hz = 0;
						cz = getceilzofslope(psect,GlobalMembersGLOBAL.myx,GlobalMembersGLOBAL.myy);
					}
			}

			if(lz >= 0 && (lz &49152) == 49152)
			{
					 j = lz&(MAXSPRITES-1);
					 if ((sprite[j].cstat &33) == 33)
					 {
							psectlotag = 0;
							spritebridge = 1;
					 }
					 if(GlobalMembersGAME.badguy(ref sprite[j]) != 0 && sprite[j].xrepeat > 24 && klabs(sprite[p.i].z-sprite[j].z) < (84<<8))
					 {
						j = getangle(sprite[j].x-GlobalMembersGLOBAL.myx,sprite[j].y-GlobalMembersGLOBAL.myy);
						GlobalMembersGLOBAL.myxvel -= sintable[(j+512)&2047]<<4;
						GlobalMembersGLOBAL.myyvel -= sintable[j &2047]<<4;
					}
			}

			if(sprite[p.i].extra <= 0)
			{
					 if(psectlotag == 2)
					 {
								if(p.on_warping_sector == 0)
								{
										 if(klabs(GlobalMembersGLOBAL.myz-fz) > (PHEIGHT>>1))
												 GlobalMembersGLOBAL.myz += 348;
								}
								clipmove(GlobalMembersGLOBAL.myx,GlobalMembersGLOBAL.myy,GlobalMembersGLOBAL.myz,GlobalMembersGLOBAL.mycursectnum,0,0,164,(4<<8),(4<<8),CLIPMASK0);
					 }

					 updatesector(GlobalMembersGLOBAL.myx,GlobalMembersGLOBAL.myy,GlobalMembersGLOBAL.mycursectnum);
					 pushmove(GlobalMembersGLOBAL.myx,GlobalMembersGLOBAL.myy,GlobalMembersGLOBAL.myz,GlobalMembersGLOBAL.mycursectnum,128,(4<<8),(20<<8),CLIPMASK0);

					GlobalMembersGLOBAL.myhoriz = 100;
					GlobalMembersGLOBAL.myhorizoff = 0;

					 goto ENDFAKEPROCESSINPUT;
			}

			doubvel = TICSPERFRAME;

			if(p.on_crane >= 0)
				goto FAKEHORIZONLY;

			if(p.one_eighty_count < 0)
				GlobalMembersGLOBAL.myang += 128;

			i = 40;

			if(psectlotag == 2)
			{
					 GlobalMembersGLOBAL.myjumpingcounter = 0;

					 if (sb_snum &1 != 0)
					 {
								if(GlobalMembersGLOBAL.myzvel > 0)
									GlobalMembersGLOBAL.myzvel = 0;
								GlobalMembersGLOBAL.myzvel -= 348;
								if(GlobalMembersGLOBAL.myzvel < -(256 *6))
									GlobalMembersGLOBAL.myzvel = -(256 *6);
					 }
					 else if (sb_snum&(1<<1))
					 {
								if(GlobalMembersGLOBAL.myzvel < 0)
									GlobalMembersGLOBAL.myzvel = 0;
								GlobalMembersGLOBAL.myzvel += 348;
								if(GlobalMembersGLOBAL.myzvel > (256 *6))
									GlobalMembersGLOBAL.myzvel = (256 *6);
					 }
					 else
					 {
						if(GlobalMembersGLOBAL.myzvel < 0)
						{
							GlobalMembersGLOBAL.myzvel += 256;
							if(GlobalMembersGLOBAL.myzvel > 0)
								GlobalMembersGLOBAL.myzvel = 0;
						}
						if(GlobalMembersGLOBAL.myzvel > 0)
						{
							GlobalMembersGLOBAL.myzvel -= 256;
							if(GlobalMembersGLOBAL.myzvel < 0)
								GlobalMembersGLOBAL.myzvel = 0;
						}
					}

					if(GlobalMembersGLOBAL.myzvel > 2048)
						GlobalMembersGLOBAL.myzvel >>= 1;

					 GlobalMembersGLOBAL.myz += GlobalMembersGLOBAL.myzvel;

					 if(GlobalMembersGLOBAL.myz > (fz-(15<<8)))
								GlobalMembersGLOBAL.myz += ((fz-(15<<8))-GlobalMembersGLOBAL.myz)>>1;

					 if(GlobalMembersGLOBAL.myz < (cz+(4<<8)))
					 {
								GlobalMembersGLOBAL.myz = cz+(4<<8);
								GlobalMembersGLOBAL.myzvel = 0;
					 }
			}

			else if(p.jetpack_on)
			{
					 GlobalMembersGLOBAL.myonground = 0;
					 GlobalMembersGLOBAL.myjumpingcounter = 0;
					 GlobalMembersGLOBAL.myhardlanding = 0;

					 if(p.jetpack_on < 11)
								GlobalMembersGLOBAL.myz -= (p.jetpack_on<<7); //Goin up

					 if (shrunk != 0)
						 j = 512;
					 else
						 j = 2048;

					 if (sb_snum &1 != 0) //A
								GlobalMembersGLOBAL.myz -= j;
					 if (sb_snum&(1<<1)) //Z
								GlobalMembersGLOBAL.myz += j;

					 if(shrunk == 0 && (psectlotag == 0 || psectlotag == 2))
						 k = 32;
					 else
						 k = 16;

					 if(GlobalMembersGLOBAL.myz > (fz-(k<<8)))
								GlobalMembersGLOBAL.myz += ((fz-(k<<8))-GlobalMembersGLOBAL.myz)>>1;
					 if(GlobalMembersGLOBAL.myz < (cz+(18<<8)))
								GlobalMembersGLOBAL.myz = cz+(18<<8);
			}
			else if(psectlotag != 2)
			{
				if (psectlotag == 1 && p.spritebridge == 0)
				{
					 if(shrunk == 0)
						 i = 34;
					 else
						 i = 12;
				}
					 if(GlobalMembersGLOBAL.myz < (fz-(i<<8)) && (GlobalMembersACTORS.floorspace(psect)|GlobalMembersACTORS.ceilingspace(psect)) == 0) //falling
					 {
								if((sb_snum &3) == 0 && GlobalMembersGLOBAL.myonground && (sector[psect].floorstat &2) && GlobalMembersGLOBAL.myz >= (fz-(i<<8)-(16<<8)))
										 GlobalMembersGLOBAL.myz = fz-(i<<8);
								else
								{
										 GlobalMembersGLOBAL.myonground = 0;

										 GlobalMembersGLOBAL.myzvel += (GlobalMembersGLOBAL.gc+80);

										 if(GlobalMembersGLOBAL.myzvel >= (4096+2048))
											 GlobalMembersGLOBAL.myzvel = (4096+2048);
								}
					 }

					 else
					 {
								if(psectlotag != 1 && psectlotag != 2 && GlobalMembersGLOBAL.myonground == 0 && GlobalMembersGLOBAL.myzvel > (6144>>1))
									 GlobalMembersGLOBAL.myhardlanding = GlobalMembersGLOBAL.myzvel>>10;
								GlobalMembersGLOBAL.myonground = 1;

								if(i == 40)
								{
										 //Smooth on the ground

										 k = ((fz-(i<<8))-GlobalMembersGLOBAL.myz)>>1;
										 if(klabs(k) < 256)
											 k = 0;
										 GlobalMembersGLOBAL.myz += k; // ((fz-(i<<8))-myz)>>1;
										 GlobalMembersGLOBAL.myzvel -= 768; // 412;
										 if(GlobalMembersGLOBAL.myzvel < 0)
											 GlobalMembersGLOBAL.myzvel = 0;
								}
								else if(GlobalMembersGLOBAL.myjumpingcounter == 0)
								{
									GlobalMembersGLOBAL.myz += ((fz-(i<<7))-GlobalMembersGLOBAL.myz)>>1; //Smooth on the water
									if(p.on_warping_sector == 0 && GlobalMembersGLOBAL.myz > fz-(16<<8))
									{
										GlobalMembersGLOBAL.myz = fz-(16<<8);
										GlobalMembersGLOBAL.myzvel >>= 1;
									}
								}

								if(sb_snum &2)
										 GlobalMembersGLOBAL.myz += (2048+768);

								if((sb_snum &1) == 0 && GlobalMembersGLOBAL.myjumpingtoggle == 1)
										 GlobalMembersGLOBAL.myjumpingtoggle = 0;

								else if((sb_snum &1) && GlobalMembersGLOBAL.myjumpingtoggle == 0)
								{
										 if(GlobalMembersGLOBAL.myjumpingcounter == 0)
												 if((fz-cz) > (56<<8))
												 {
													GlobalMembersGLOBAL.myjumpingcounter = 1;
													GlobalMembersGLOBAL.myjumpingtoggle = 1;
												 }
								}
								if(GlobalMembersGLOBAL.myjumpingcounter != 0 && (sb_snum &1) == 0)
									GlobalMembersGLOBAL.myjumpingcounter = 0;
					 }

					 if (GlobalMembersGLOBAL.myjumpingcounter != 0)
					 {
								if((sb_snum &1) == 0 && GlobalMembersGLOBAL.myjumpingtoggle == 1)
										 GlobalMembersGLOBAL.myjumpingtoggle = 0;

								if(GlobalMembersGLOBAL.myjumpingcounter < (1024+256))
								{
										 if(psectlotag == 1 && GlobalMembersGLOBAL.myjumpingcounter > 768)
										 {
												 GlobalMembersGLOBAL.myjumpingcounter = 0;
												 GlobalMembersGLOBAL.myzvel = -512;
										 }
										 else
										 {
												 GlobalMembersGLOBAL.myzvel -= (sintable[(2048-128+GlobalMembersGLOBAL.myjumpingcounter)&2047])/12;
												 GlobalMembersGLOBAL.myjumpingcounter += 180;

												 GlobalMembersGLOBAL.myonground = 0;
										 }
								}
								else
								{
										 GlobalMembersGLOBAL.myjumpingcounter = 0;
										 GlobalMembersGLOBAL.myzvel = 0;
								}
					 }

					 GlobalMembersGLOBAL.myz += GlobalMembersGLOBAL.myzvel;

					 if(GlobalMembersGLOBAL.myz < (cz+(4<<8)))
					 {
								GlobalMembersGLOBAL.myjumpingcounter = 0;
								if(GlobalMembersGLOBAL.myzvel < 0)
									GlobalMembersGLOBAL.myxvel = GlobalMembersGLOBAL.myyvel = 0;
								GlobalMembersGLOBAL.myzvel = 128;
								GlobalMembersGLOBAL.myz = cz+(4<<8);
					 }

			}

			if (p.fist_incs || p.transporter_hold > 2 || GlobalMembersGLOBAL.myhardlanding || p.access_incs > 0 || p.knee_incs > 0 || (p.curr_weapon == TRIPBOMB_WEAPON && p.kickback_pic > 1 && p.kickback_pic < 4))
			{
					 doubvel = 0;
					 GlobalMembersGLOBAL.myxvel = 0;
					 GlobalMembersGLOBAL.myyvel = 0;
			}
			else if (syn.avel) //p->ang += syncangvel * constant
			{ //ENGINE calculates angvel for you
				int tempang;

				tempang = syn.avel<<1;

				if(psectlotag == 2)
					GlobalMembersGLOBAL.myang += (tempang-(tempang>>3))*sgn(doubvel);
				else
					GlobalMembersGLOBAL.myang += (tempang)*sgn(doubvel);
				GlobalMembersGLOBAL.myang &= 2047;
			}

			if (GlobalMembersGLOBAL.myxvel != 0 || GlobalMembersGLOBAL.myyvel || syn.fvel || syn.svel)
			{
					 if(p.steroids_amount > 0 && p.steroids_amount < 400)
						 doubvel <<= 1;

					 GlobalMembersGLOBAL.myxvel += ((syn.fvel *doubvel)<<6);
					 GlobalMembersGLOBAL.myyvel += ((syn.svel *doubvel)<<6);

					 if((p.curr_weapon == KNEE_WEAPON && p.kickback_pic > 10 && GlobalMembersGLOBAL.myonground != 0) || (GlobalMembersGLOBAL.myonground != 0 && (sb_snum &2)))
					 {
								GlobalMembersGLOBAL.myxvel = mulscale16(GlobalMembersGLOBAL.myxvel,GlobalMembersGLOBAL.dukefriction-0x2000);
								GlobalMembersGLOBAL.myyvel = mulscale16(GlobalMembersGLOBAL.myyvel,GlobalMembersGLOBAL.dukefriction-0x2000);
					 }
					 else
					 {
						if(psectlotag == 2)
						{
							GlobalMembersGLOBAL.myxvel = mulscale16(GlobalMembersGLOBAL.myxvel,GlobalMembersGLOBAL.dukefriction-0x1400);
							GlobalMembersGLOBAL.myyvel = mulscale16(GlobalMembersGLOBAL.myyvel,GlobalMembersGLOBAL.dukefriction-0x1400);
						}
						else
						{
							GlobalMembersGLOBAL.myxvel = mulscale16(GlobalMembersGLOBAL.myxvel,GlobalMembersGLOBAL.dukefriction);
							GlobalMembersGLOBAL.myyvel = mulscale16(GlobalMembersGLOBAL.myyvel,GlobalMembersGLOBAL.dukefriction);
						}
					 }

					 if(Math.Abs(GlobalMembersGLOBAL.myxvel) < 2048 && Math.Abs(GlobalMembersGLOBAL.myyvel) < 2048)
						 GlobalMembersGLOBAL.myxvel = GlobalMembersGLOBAL.myyvel = 0;

					 if (shrunk != 0)
					 {
						 GlobalMembersGLOBAL.myxvel = mulscale16(GlobalMembersGLOBAL.myxvel,(GlobalMembersGLOBAL.dukefriction)-(GlobalMembersGLOBAL.dukefriction>>1)+(GlobalMembersGLOBAL.dukefriction>>2));
						 GlobalMembersGLOBAL.myyvel = mulscale16(GlobalMembersGLOBAL.myyvel,(GlobalMembersGLOBAL.dukefriction)-(GlobalMembersGLOBAL.dukefriction>>1)+(GlobalMembersGLOBAL.dukefriction>>2));
					 }
			}

	FAKEHORIZONLY:
			if(psectlotag == 1 || spritebridge == 1)
				i = (4<<8);
				else
					i = (20<<8);

			clipmove(GlobalMembersGLOBAL.myx,GlobalMembersGLOBAL.myy,GlobalMembersGLOBAL.myz,GlobalMembersGLOBAL.mycursectnum,GlobalMembersGLOBAL.myxvel,GlobalMembersGLOBAL.myyvel,164,4<<8,i,CLIPMASK0);
			pushmove(GlobalMembersGLOBAL.myx,GlobalMembersGLOBAL.myy,GlobalMembersGLOBAL.myz,GlobalMembersGLOBAL.mycursectnum,164,4<<8,4<<8,CLIPMASK0);

			if(p.jetpack_on == 0 && psectlotag != 1 && psectlotag != 2 && shrunk != 0)
				GlobalMembersGLOBAL.myz += 30<<8;

			if ((sb_snum&(1<<18)) || GlobalMembersGLOBAL.myhardlanding != 0)
				GlobalMembersGLOBAL.myreturntocenter = 9;

			if (sb_snum&(1<<13))
			{
					GlobalMembersGLOBAL.myreturntocenter = 9;
					if (sb_snum&(1<<5))
						GlobalMembersGLOBAL.myhoriz += 6;
					GlobalMembersGLOBAL.myhoriz += 6;
			}
			else if (sb_snum&(1<<14))
			{
					GlobalMembersGLOBAL.myreturntocenter = 9;
					if (sb_snum&(1<<5))
						GlobalMembersGLOBAL.myhoriz -= 6;
					GlobalMembersGLOBAL.myhoriz -= 6;
			}
			else if (sb_snum&(1<<3))
			{
					if (sb_snum&(1<<5))
						GlobalMembersGLOBAL.myhoriz += 6;
					GlobalMembersGLOBAL.myhoriz += 6;
			}
			else if (sb_snum&(1<<4))
			{
					if (sb_snum&(1<<5))
						GlobalMembersGLOBAL.myhoriz -= 6;
					GlobalMembersGLOBAL.myhoriz -= 6;
			}

			if (GlobalMembersGLOBAL.myreturntocenter > 0)
				if ((sb_snum&(1<<13)) == 0 && (sb_snum&(1<<14)) == 0)
			{
				 GlobalMembersGLOBAL.myreturntocenter--;
				 GlobalMembersGLOBAL.myhoriz += 33-(GlobalMembersGLOBAL.myhoriz/3);
			}

			if(p.aim_mode)
				GlobalMembersGLOBAL.myhoriz += syn.horz>>1;
			else
			{
				if(GlobalMembersGLOBAL.myhoriz > 95 && GlobalMembersGLOBAL.myhoriz < 105)
					GlobalMembersGLOBAL.myhoriz = 100;
				if(GlobalMembersGLOBAL.myhorizoff > -5 && GlobalMembersGLOBAL.myhorizoff < 5)
					GlobalMembersGLOBAL.myhorizoff = 0;
			}

			if (GlobalMembersGLOBAL.myhardlanding > 0)
			{
				GlobalMembersGLOBAL.myhardlanding--;
				GlobalMembersGLOBAL.myhoriz -= (GlobalMembersGLOBAL.myhardlanding<<4);
			}

			if (GlobalMembersGLOBAL.myhoriz > 299)
				GlobalMembersGLOBAL.myhoriz = 299;
			else if (GlobalMembersGLOBAL.myhoriz < -99)
				GlobalMembersGLOBAL.myhoriz = -99;

			if(p.knee_incs > 0)
			{
				GlobalMembersGLOBAL.myhoriz -= 48;
				GlobalMembersGLOBAL.myreturntocenter = 9;
			}


	ENDFAKEPROCESSINPUT:

			GlobalMembersGLOBAL.myxbak[GlobalMembersGLOBAL.fakemovefifoplc&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.myx;
			GlobalMembersGLOBAL.myybak[GlobalMembersGLOBAL.fakemovefifoplc&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.myy;
			GlobalMembersGLOBAL.myzbak[GlobalMembersGLOBAL.fakemovefifoplc&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.myz;
			GlobalMembersGLOBAL.myangbak[GlobalMembersGLOBAL.fakemovefifoplc&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.myang;
			GlobalMembersGLOBAL.myhorizbak[GlobalMembersGLOBAL.fakemovefifoplc&(MOVEFIFOSIZ-1)] = GlobalMembersGLOBAL.myhoriz;
			GlobalMembersGLOBAL.fakemovefifoplc++;

			sprite[p.i].cstat = backcstat;
	}


	public static sbyte domovethings()
	{
		short i;
		short j;
		sbyte ch;

		for(i = connecthead;i>=0;i = connectpoint2[i])
			if(GlobalMembersGLOBAL.sync[i].bits&(1<<17))
		{
			GlobalMembersGLOBAL.multiflag = 2;
			GlobalMembersGLOBAL.multiwhat = (GlobalMembersGLOBAL.sync[i].bits>>18)&1;
			GlobalMembersGLOBAL.multipos = (uint)(GlobalMembersGLOBAL.sync[i].bits>>19)&15;
			GlobalMembersGLOBAL.multiwho = i;

			if (GlobalMembersGLOBAL.multiwhat != 0)
			{
				GlobalMembersMENUES.saveplayer(GlobalMembersGLOBAL.multipos);
				GlobalMembersGLOBAL.multiflag = 0;

				if(GlobalMembersGLOBAL.multiwho != myconnectindex)
				{
					GlobalMembersGLOBAL.fta_quotes[122] = GlobalMembersGLOBAL.ud.user_name[GlobalMembersGLOBAL.multiwho][0];
					GlobalMembersGLOBAL.fta_quotes[122] += " SAVED A MULTIPLAYER GAME";
					GlobalMembersGAME.FTA(122, ref GlobalMembersGLOBAL.ps[myconnectindex]);
				}
				else
				{
					GlobalMembersGLOBAL.fta_quotes[122] = "MULTIPLAYER GAME SAVED";
					GlobalMembersGAME.FTA(122, ref GlobalMembersGLOBAL.ps[myconnectindex]);
				}
				break;
			}
			else
			{
	//            waitforeverybody();

				j = GlobalMembersMENUES.loadplayer(GlobalMembersGLOBAL.multipos);

				GlobalMembersGLOBAL.multiflag = 0;

				if(j == 0)
				{
					if(GlobalMembersGLOBAL.multiwho != myconnectindex)
					{
						GlobalMembersGLOBAL.fta_quotes[122] = GlobalMembersGLOBAL.ud.user_name[GlobalMembersGLOBAL.multiwho][0];
						GlobalMembersGLOBAL.fta_quotes[122] += " LOADED A MULTIPLAYER GAME";
						GlobalMembersGAME.FTA(122, ref GlobalMembersGLOBAL.ps[myconnectindex]);
					}
					else
					{
						GlobalMembersGLOBAL.fta_quotes[122] = "MULTIPLAYER GAME LOADED";
						GlobalMembersGAME.FTA(122, ref GlobalMembersGLOBAL.ps[myconnectindex]);
					}
					return 1;
				}
			}
		}

		GlobalMembersGLOBAL.ud.camerasprite = -1;
		GlobalMembersGLOBAL.lockclock += TICSPERFRAME;

		if(GlobalMembersGLOBAL.earthquaketime > 0)
			GlobalMembersGLOBAL.earthquaketime--;
		if(GlobalMembersGLOBAL.rtsplaying > 0)
			GlobalMembersGLOBAL.rtsplaying--;

		for(i = 0;i<DefineConstants.MAXUSERQUOTES;i++)
			 if (user_quote_time[i])
			 {
				 user_quote_time[i]--;
				 if (!user_quote_time[i])
					 GlobalMembersGLOBAL.pub = NUMPAGES;
			 }
		 if ((klabs(quotebotgoal-quotebot) <= 16) && (GlobalMembersGLOBAL.ud.screen_size <= 8))
			 quotebot += ksgn(quotebotgoal-quotebot);
		 else
			 quotebot = quotebotgoal;

		if(GlobalMembersGLOBAL.show_shareware > 0)
		{
			GlobalMembersGLOBAL.show_shareware--;
			if(GlobalMembersGLOBAL.show_shareware == 0)
			{
				GlobalMembersGLOBAL.pus = NUMPAGES;
				GlobalMembersGLOBAL.pub = NUMPAGES;
			}
		}

		GlobalMembersGLOBAL.everyothertime++;

		for(i = connecthead;i>=0;i = connectpoint2[i])
			copybufbyte(GlobalMembersGLOBAL.inputfifo[GlobalMembersGLOBAL.movefifoplc&(MOVEFIFOSIZ-1), i], GlobalMembersGLOBAL.sync[i], sizeof(input));
		GlobalMembersGLOBAL.movefifoplc++;

		GlobalMembersACTORS.updateinterpolations();

		j = -1;
		for(i = connecthead;i>=0;i = connectpoint2[i])
		 {
			  if ((GlobalMembersGLOBAL.sync[i].bits&(1<<26)) == 0)
			  {
				  j = i;
				  continue;
			  }

			  GlobalMembersGAME.closedemowrite();

			  if (i == myconnectindex)
				  GlobalMembersGAME.gameexit(" ");
			  if (GlobalMembersGLOBAL.screenpeek == i)
			  {
					GlobalMembersGLOBAL.screenpeek = connectpoint2[i];
					if (GlobalMembersGLOBAL.screenpeek < 0)
						GlobalMembersGLOBAL.screenpeek = connecthead;
			  }

			  if (i == connecthead)
				  connecthead = connectpoint2[connecthead];
			  else
				  connectpoint2[j] = connectpoint2[i];

			  numplayers--;
			  GlobalMembersGLOBAL.ud.multimode--;

			  if (numplayers < 2)
				  GlobalMembersSOUNDS.sound(GENERIC_AMBIENCE17);

			  GlobalMembersGLOBAL.pub = NUMPAGES;
			  GlobalMembersGLOBAL.pus = NUMPAGES;
			  GlobalMembersPREMAP.vscrn();

			  GlobalMembersGLOBAL.buf = string.Format("{0} is history!", GlobalMembersGLOBAL.ud.user_name[i]);

			  GlobalMembersPLAYER.quickkill(ref GlobalMembersGLOBAL.ps[i]);
			  deletesprite(GlobalMembersGLOBAL.ps[i].i);

			  GlobalMembersGAME.adduserquote(ref GlobalMembersGLOBAL.buf);

			  if(j < 0 && GlobalMembersGLOBAL.networkmode == 0)
				  GlobalMembersGAME.gameexit(" \nThe 'MASTER/First player' just quit the game.  All\nplayers are returned from the game. This only happens in 5-8\nplayer mode as a different network scheme is used.");
		  }

		  if ((numplayers >= 2) && ((GlobalMembersGLOBAL.movefifoplc &7) == 7))
		  {
				ch = (sbyte)(randomseed &255);
				for(i = connecthead;i>=0;i = connectpoint2[i])
					 ch += ((GlobalMembersGLOBAL.ps[i].posx+GlobalMembersGLOBAL.ps[i].posy+GlobalMembersGLOBAL.ps[i].posz+GlobalMembersGLOBAL.ps[i].ang+GlobalMembersGLOBAL.ps[i].horiz)&255);
				GlobalMembersGLOBAL.syncval[myconnectindex, GlobalMembersGLOBAL.syncvalhead[myconnectindex]&(MOVEFIFOSIZ-1)] = ch;
				GlobalMembersGLOBAL.syncvalhead[myconnectindex]++;
		  }

		if(GlobalMembersGLOBAL.ud.recstat == 1)
			GlobalMembersGAME.record();

		if(GlobalMembersGLOBAL.ud.pause_on == 0)
		{
			GlobalMembersGLOBAL.global_random = TRAND;
			GlobalMembersACTORS.movedummyplayers(); //ST 13
		}

		for(i = connecthead;i>=0;i = connectpoint2[i])
		{
			GlobalMembersSECTOR.cheatkeys(i);

			if(GlobalMembersGLOBAL.ud.pause_on == 0)
			{
				GlobalMembersPLAYER.processinput(i);
				GlobalMembersSECTOR.checksectors(i);
			}
		}

		if(GlobalMembersGLOBAL.ud.pause_on == 0)
		{
			GlobalMembersACTORS.movefta(); //ST 2
			GlobalMembersACTORS.moveweapons(); //ST 5 (must be last)
			GlobalMembersACTORS.movetransports(); //ST 9

			GlobalMembersACTORS.moveplayers(); //ST 10
			GlobalMembersACTORS.movefallers(); //ST 12
			GlobalMembersACTORS.moveexplosions(); //ST 4

			GlobalMembersACTORS.moveactors(); //ST 1
			GlobalMembersACTORS.moveeffectors(); //ST 3

			GlobalMembersACTORS.movestandables(); //ST 6
			GlobalMembersSECTOR.doanimations();
			GlobalMembersACTORS.movefx(); //ST 11
		}

		GlobalMembersGAME.fakedomovethingscorrect();

		if((GlobalMembersGLOBAL.everyothertime &1) == 0)
		{
			GlobalMembersSECTOR.animatewalls();
			GlobalMembersACTORS.movecyclers();
			GlobalMembersSOUNDS.pan3dsound();
		}


		return 0;
	}


	public static void doorders()
	{
		short i;

		setview(0,0,xdim-1,ydim-1);

		for(i = 0;i<63;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
		totalclock = 0;
		KB_FlushKeyboardQueue();
		rotatesprite(0,0,65536,0,ORDERING,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		while(!KB_KeyWaiting())
			GlobalMembersGAME.getpackets();

		for(i = 0;i<63;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		KB_FlushKeyboardQueue();
		rotatesprite(0,0,65536,0,ORDERING+1,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		while(!KB_KeyWaiting())
			GlobalMembersGAME.getpackets();

		for(i = 0;i<63;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		KB_FlushKeyboardQueue();
		rotatesprite(0,0,65536,0,ORDERING+2,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		while(!KB_KeyWaiting())
			GlobalMembersGAME.getpackets();

		for(i = 0;i<63;i+=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		KB_FlushKeyboardQueue();
		rotatesprite(0,0,65536,0,ORDERING+3,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
		nextpage();
		for(i = 63;i>0;i-=7)
			GlobalMembersMENUES.palto(0, 0, 0, i);
		totalclock = 0;
		while(!KB_KeyWaiting())
			GlobalMembersGAME.getpackets();
	}

	public static void dobonus(sbyte bonusonly)
	{
		short t;
		short r;
		short tinc;
		short gfx_offset;
		int i;
		int y;
		int xfragtotal;
		int yfragtotal;
		short bonuscnt;

		int[] breathe = { 0, 30,VICTORY1+1,176,59, 30, 60,VICTORY1+2,176,59, 60, 90,VICTORY1+1,176,59, 90, 120,0,176,59 };

		int[] bossmove = { 0, 120,VICTORY1+3,86,59, 220, 260,VICTORY1+4,86,59, 260, 290,VICTORY1+5,86,59, 290, 320,VICTORY1+6,86,59, 320, 350,VICTORY1+7,86,59, 350, 380,VICTORY1+8,86,59 };

		bonuscnt = 0;

		for(t = 0;t<64;t+=7)
			GlobalMembersMENUES.palto(0, 0, 0, t);
		setview(0,0,xdim-1,ydim-1);
		clearview(0);
		nextpage();
		flushperms();

		FX_StopAllSounds();
		GlobalMembersSOUNDS.clearsoundlocks();
		FX_SetReverb(0);

		if (bonusonly != 0)
			goto FRAGBONUS;

		if(numplayers < 2 && GlobalMembersGLOBAL.ud.eog && GlobalMembersGLOBAL.ud.from_bonus == 0)
			switch(GlobalMembersGLOBAL.ud.volume_number)
		{
			case 0:
				if(GlobalMembersGLOBAL.ud.lockout == 0)
				{
					clearview(0);
					rotatesprite(0,50<<16,65536,0,VICTORY1,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
					nextpage();
					GlobalMembersGLOBAL.ps[myconnectindex].palette = endingpal;
					for(t = 63;t>=0;t--)
						GlobalMembersMENUES.palto(0, 0, 0, t);

					KB_FlushKeyboardQueue();
					totalclock = 0;
					tinc = 0;
					while (true)
					{
						clearview(0);
						rotatesprite(0,50<<16,65536,0,VICTORY1,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);

						// boss
						if(totalclock > 390 && totalclock < 780)
							for(t = 0;t<35;t+=5)
								if(bossmove[t+2] != 0 && (totalclock%390) > bossmove[t] && (totalclock%390) <= bossmove[t+1])
						{
							if(t == 10 && bonuscnt == 1)
							{
								GlobalMembersSOUNDS.sound(SHOTGUN_FIRE);
								GlobalMembersSOUNDS.sound(SQUISHED);
								bonuscnt++;
							}
							rotatesprite(bossmove[t+3]<<16,bossmove[t+4]<<16,65536,0,bossmove[t+2],0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
						}

						// Breathe
						if(totalclock < 450 || totalclock >= 750)
						{
							if(totalclock >= 750)
							{
								rotatesprite(86<<16,59<<16,65536,0,VICTORY1+8,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
								if(totalclock >= 750 && bonuscnt == 2)
								{
									GlobalMembersSOUNDS.sound(DUKETALKTOBOSS);
									bonuscnt++;
								}
							}
							for(t = 0;t<20;t+=5)
								if(breathe[t+2] != 0 && (totalclock%120) > breathe[t] && (totalclock%120) <= breathe[t+1])
							{
									if(t == 5 && bonuscnt == 0)
									{
										GlobalMembersSOUNDS.sound(BOSSTALKTODUKE);
										bonuscnt++;
									}
									rotatesprite(breathe[t+3]<<16,breathe[t+4]<<16,65536,0,breathe[t+2],0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
							}
						}

						GlobalMembersGAME.getpackets();
						nextpage();
						if(KB_KeyWaiting())
							break;
					}
				}

				for(t = 0;t<64;t++)
					GlobalMembersMENUES.palto(0, 0, 0, t);

				KB_FlushKeyboardQueue();
				GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;

				rotatesprite(0,0,65536,0,3292,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
				nextpage();
				for(t = 63;t>0;t--)
					GlobalMembersMENUES.palto(0, 0, 0, t);
				while(!KB_KeyWaiting())
					GlobalMembersGAME.getpackets();
				for(t = 0;t<64;t++)
					GlobalMembersMENUES.palto(0, 0, 0, t);
				MUSIC_StopSong();
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();
				break;
			case 1:
				MUSIC_StopSong();
				clearview(0);
				nextpage();

				if(GlobalMembersGLOBAL.ud.lockout == 0)
				{
					GlobalMembersMENUES.playanm("cineov2.anm", 1);
					KB_FlushKeyBoardQueue();
					clearview(0);
					nextpage();
				}

				GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);

				for(t = 0;t<64;t++)
					GlobalMembersMENUES.palto(0, 0, 0, t);
				setview(0,0,xdim-1,ydim-1);
				KB_FlushKeyboardQueue();
				GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
				rotatesprite(0,0,65536,0,3293,0,0,2+8+16+64, 0,0,xdim-1,ydim-1);
				nextpage();
				for(t = 63;t>0;t--)
					GlobalMembersMENUES.palto(0, 0, 0, t);
				while(!KB_KeyWaiting())
					GlobalMembersGAME.getpackets();
				for(t = 0;t<64;t++)
					GlobalMembersMENUES.palto(0, 0, 0, t);

				break;

			case 3:

				setview(0,0,xdim-1,ydim-1);

				MUSIC_StopSong();
				clearview(0);
				nextpage();

				if(GlobalMembersGLOBAL.ud.lockout == 0)
				{
					KB_FlushKeyboardQueue();
					GlobalMembersMENUES.playanm("vol4e1.anm", 8);
					clearview(0);
					nextpage();
					GlobalMembersMENUES.playanm("vol4e2.anm", 10);
					clearview(0);
					nextpage();
					GlobalMembersMENUES.playanm("vol4e3.anm", 11);
					clearview(0);
					nextpage();
				}

				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();
				GlobalMembersSOUNDS.sound(ENDSEQVOL3SND4);
				KB_FlushKeyBoardQueue();

				GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
				GlobalMembersMENUES.palto(0, 0, 0, 63);
				clearview(0);
				GlobalMembersMENUES.menutext(160, 60, 0, 0, "THANKS TO ALL OUR");
				GlobalMembersMENUES.menutext(160, 60+16, 0, 0, "FANS FOR GIVING");
				GlobalMembersMENUES.menutext(160, 60+16+16, 0, 0, "US BIG HEADS.");
				GlobalMembersMENUES.menutext(160, 70+16+16+16, 0, 0, "LOOK FOR A DUKE NUKEM 3D");
				GlobalMembersMENUES.menutext(160, 70+16+16+16+16, 0, 0, "SEQUEL SOON.");
				nextpage();

				for(t = 63;t>0;t-=3)
					GlobalMembersMENUES.palto(0, 0, 0, t);
				KB_FlushKeyboardQueue();
				while(!KB_KeyWaiting())
					GlobalMembersGAME.getpackets();
				for(t = 0;t<64;t+=3)
					GlobalMembersMENUES.palto(0, 0, 0, t);

				clearview(0);
				nextpage();

				GlobalMembersMENUES.playanm("DUKETEAM.ANM", 4);

				KB_FlushKeyBoardQueue();
				while(!KB_KeyWaiting())
					GlobalMembersGAME.getpackets();

				clearview(0);
				nextpage();
				GlobalMembersMENUES.palto(0, 0, 0, 63);

				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();
				KB_FlushKeyBoardQueue();

				break;

			case 2:

				MUSIC_StopSong();
				clearview(0);
				nextpage();
				if(GlobalMembersGLOBAL.ud.lockout == 0)
				{
					for(t = 63;t>=0;t--)
						GlobalMembersMENUES.palto(0, 0, 0, t);
					GlobalMembersMENUES.playanm("cineov3.anm", 2);
					KB_FlushKeyBoardQueue();
					GlobalMembersGLOBAL.ototalclock = totalclock+200;
					while(totalclock < GlobalMembersGLOBAL.ototalclock)
						GlobalMembersGAME.getpackets();
					clearview(0);
					nextpage();

					FX_StopAllSounds();
					GlobalMembersSOUNDS.clearsoundlocks();
				}

				GlobalMembersMENUES.playanm("RADLOGO.ANM", 3);

				if(GlobalMembersGLOBAL.ud.lockout == 0 && !KB_KeyWaiting())
				{
					GlobalMembersSOUNDS.sound(ENDSEQVOL3SND5);
					while(GlobalMembersGLOBAL.Sound[ENDSEQVOL3SND5].lock>=200)
						GlobalMembersGAME.getpackets();
					if(KB_KeyWaiting())
						goto ENDANM;
					GlobalMembersSOUNDS.sound(ENDSEQVOL3SND6);
					while(GlobalMembersGLOBAL.Sound[ENDSEQVOL3SND6].lock>=200)
						GlobalMembersGAME.getpackets();
					if(KB_KeyWaiting())
						goto ENDANM;
					GlobalMembersSOUNDS.sound(ENDSEQVOL3SND7);
					while(GlobalMembersGLOBAL.Sound[ENDSEQVOL3SND7].lock>=200)
						GlobalMembersGAME.getpackets();
					if(KB_KeyWaiting())
						goto ENDANM;
					GlobalMembersSOUNDS.sound(ENDSEQVOL3SND8);
					while(GlobalMembersGLOBAL.Sound[ENDSEQVOL3SND8].lock>=200)
						GlobalMembersGAME.getpackets();
					if(KB_KeyWaiting())
						goto ENDANM;
					GlobalMembersSOUNDS.sound(ENDSEQVOL3SND9);
					while(GlobalMembersGLOBAL.Sound[ENDSEQVOL3SND9].lock>=200)
						GlobalMembersGAME.getpackets();
				}

				KB_FlushKeyBoardQueue();
				totalclock = 0;
				while(!KB_KeyWaiting() && totalclock < 120)
					GlobalMembersGAME.getpackets();

				ENDANM:

				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();

				KB_FlushKeyBoardQueue();

				clearview(0);

				break;
		}

		FRAGBONUS:

		GlobalMembersGLOBAL.ps[myconnectindex].palette = palette;
		KB_FlushKeyboardQueue();
		totalclock = 0;
		tinc = 0;
		bonuscnt = 0;

		MUSIC_StopSong();
		FX_StopAllSounds();
		GlobalMembersSOUNDS.clearsoundlocks();

		if(playerswhenstarted > 1 && GlobalMembersGLOBAL.ud.coop != 1)
		{
			if(!(GlobalMembersCONFIG.MusicToggle == 0 || GlobalMembersCONFIG.MusicDevice == NumSoundCards))
				GlobalMembersSOUNDS.sound(BONUSMUSIC);

			rotatesprite(0,0,65536,0,MENUSCREEN,16,0,2+8+16+64,0,0,xdim-1,ydim-1);
			rotatesprite(160<<16,34<<16,65536,0,INGAMEDUKETHREEDEE,0,0,10,0,0,xdim-1,ydim-1);
			rotatesprite((260)<<16,36<<16,65536,0,PLUTOPAKSPRITE+2,0,0,2+8,0,0,xdim-1,ydim-1);
			GlobalMembersGAME.gametext(160, 58+2, "MULTIPLAYER TOTALS", 0, 2+8+16);
			GlobalMembersGAME.gametext(160, 58+10, ref GlobalMembersGLOBAL.level_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.last_level-1], 0, 2+8+16);

			GlobalMembersGAME.gametext(160, 165, "PRESS ANY KEY TO CONTINUE", 0, 2+8+16);


			t = 0;
			GlobalMembersGAME.minitext(23, 80, "   NAME                                           KILLS", 8, 2+8+16+128);
			for(i = 0;i<playerswhenstarted;i++)
			{
				GlobalMembersGLOBAL.tempbuf = string.Format("{0,-4:D}", i+1);
				GlobalMembersGAME.minitext(92+(i *23), 80, ref GlobalMembersGLOBAL.tempbuf, 3, 2+8+16+128);
			}

			for(i = 0;i<playerswhenstarted;i++)
			{
				xfragtotal = 0;
				GlobalMembersGLOBAL.tempbuf = string.Format("{0:D}", i+1);

				GlobalMembersGAME.minitext(30, 90+t, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16+128);
				GlobalMembersGAME.minitext(38, 90+t, ref GlobalMembersGLOBAL.ud.user_name[i], GlobalMembersGLOBAL.ps[i].palookup, 2+8+16+128);

				for(y = 0;y<playerswhenstarted;y++)
				{
					if(i == y)
					{
						GlobalMembersGLOBAL.tempbuf = string.Format("{0,-4:D}", GlobalMembersGLOBAL.ps[y].fraggedself);
						GlobalMembersGAME.minitext(92+(y *23), 90+t, ref GlobalMembersGLOBAL.tempbuf, 2, 2+8+16+128);
						xfragtotal -= GlobalMembersGLOBAL.ps[y].fraggedself;
					}
					else
					{
						GlobalMembersGLOBAL.tempbuf = string.Format("{0,-4:D}", GlobalMembersGLOBAL.frags[i, y]);
						GlobalMembersGAME.minitext(92+(y *23), 90+t, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16+128);
						xfragtotal += GlobalMembersGLOBAL.frags[i, y];
					}

					if(myconnectindex == connecthead)
					{
						GlobalMembersGLOBAL.tempbuf = string.Format("stats {0:D} killed {1:D} {2:D}\n", i+1, y+1, GlobalMembersGLOBAL.frags[i, y]);
						GlobalMembersGAME.sendscore(ref GlobalMembersGLOBAL.tempbuf);
					}
				}

				GlobalMembersGLOBAL.tempbuf = string.Format("{0,-4:D}", xfragtotal);
				GlobalMembersGAME.minitext(101+(8 *23), 90+t, ref GlobalMembersGLOBAL.tempbuf, 2, 2+8+16+128);

				t += 7;
			}

			for(y = 0;y<playerswhenstarted;y++)
			{
				yfragtotal = 0;
				for(i = 0;i<playerswhenstarted;i++)
				{
					if(i == y)
						yfragtotal += GlobalMembersGLOBAL.ps[i].fraggedself;
					yfragtotal += GlobalMembersGLOBAL.frags[i, y];
				}
				GlobalMembersGLOBAL.tempbuf = string.Format("{0,-4:D}", yfragtotal);
				GlobalMembersGAME.minitext(92+(y *23), 96+(8 *7), ref GlobalMembersGLOBAL.tempbuf, 2, 2+8+16+128);
			}

			GlobalMembersGAME.minitext(45, 96+(8 *7), "DEATHS", 8, 2+8+16+128);
			nextpage();

			for(t = 0;t<64;t+=7)
				GlobalMembersMENUES.palto(0, 0, 0, 63-t);

			KB_FlushKeyboardQueue();
			while(KB_KeyWaiting()==0)
				GlobalMembersGAME.getpackets();

			if(KB_KeyPressed(sc_F12))
			{
				KB_ClearKeyDown(sc_F12);
				screencapture("duke0000.pcx",0);
			}

			if(bonusonly != 0 || GlobalMembersGLOBAL.ud.multimode > 1)
				return;

			for(t = 0;t<64;t+=7)
				GlobalMembersMENUES.palto(0, 0, 0, t);
		}

		if(bonusonly != 0 || GlobalMembersGLOBAL.ud.multimode > 1)
			return;

		switch(GlobalMembersGLOBAL.ud.volume_number)
		{
			case 1:
				gfx_offset = 5;
				break;
			default:
				gfx_offset = 0;
				break;
		}

		rotatesprite(0,0,65536,0,BONUSSCREEN+gfx_offset,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);

		GlobalMembersMENUES.menutext(160, 20-6, 0, 0, ref GlobalMembersGLOBAL.level_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.last_level-1, 0]);
		GlobalMembersMENUES.menutext(160, 36-6, 0, 0, "COMPLETED");

		GlobalMembersGAME.gametext(160, 192, "PRESS ANY KEY TO CONTINUE", 16, 2+8+16);

		if(!(GlobalMembersCONFIG.MusicToggle == 0 || GlobalMembersCONFIG.MusicDevice == NumSoundCards))
			GlobalMembersSOUNDS.sound(BONUSMUSIC);

		nextpage();
		KB_FlushKeyboardQueue();
		for(t = 0;t<64;t++)
			GlobalMembersMENUES.palto(0, 0, 0, 63-t);
		bonuscnt = 0;
		totalclock = 0;
		tinc = 0;

		while (true)
		{
			if(GlobalMembersGLOBAL.ps[myconnectindex].gm &MODE_EOL)
			{
				rotatesprite(0,0,65536,0,BONUSSCREEN+gfx_offset,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);

				if(totalclock > (1000000000) && totalclock < (1000000320))
				{
					switch((totalclock>>4)%15)
					{
						case 0:
							if(bonuscnt == 6)
							{
								bonuscnt++;
								GlobalMembersSOUNDS.sound(SHOTGUN_COCK);
								switch(RandomNumbers.NextNumber()&3)
								{
									case 0:
										GlobalMembersSOUNDS.sound(BONUS_SPEECH1);
										break;
									case 1:
										GlobalMembersSOUNDS.sound(BONUS_SPEECH2);
										break;
									case 2:
										GlobalMembersSOUNDS.sound(BONUS_SPEECH3);
										break;
									case 3:
										GlobalMembersSOUNDS.sound(BONUS_SPEECH4);
										break;
								}
							}
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
						case 1:
						case 4:
						case 5:
							rotatesprite(199<<16,31<<16,65536,0,BONUSSCREEN+3+gfx_offset,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
							break;
						case 2:
						case 3:
						   rotatesprite(199<<16,31<<16,65536,0,BONUSSCREEN+4+gfx_offset,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
						   break;
					}
				}
				else if(totalclock > (10240+120))
					break;
				else
				{
					switch((totalclock>>5)&3)
					{
						case 1:
						case 3:
							rotatesprite(199<<16,31<<16,65536,0,BONUSSCREEN+1+gfx_offset,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
							break;
						case 2:
							rotatesprite(199<<16,31<<16,65536,0,BONUSSCREEN+2+gfx_offset,0,0,2+8+16+64+128,0,0,xdim-1,ydim-1);
							break;
					}
				}

				GlobalMembersMENUES.menutext(160, 20-6, 0, 0, ref GlobalMembersGLOBAL.level_names[(GlobalMembersGLOBAL.ud.volume_number *11)+GlobalMembersGLOBAL.ud.last_level-1, 0]);
				GlobalMembersMENUES.menutext(160, 36-6, 0, 0, "COMPLETED");

				GlobalMembersGAME.gametext(160, 192, "PRESS ANY KEY TO CONTINUE", 16, 2+8+16);

				if(totalclock > (60 *3))
				{
					GlobalMembersGAME.gametext(10, 59+9, "Your Time:", 0, 2+8+16);
					GlobalMembersGAME.gametext(10, 69+9, "Par time:", 0, 2+8+16);
					GlobalMembersGAME.gametext(10, 78+9, "3D Realms' Time:", 0, 2+8+16);
					if(bonuscnt == 0)
						bonuscnt++;

					if(totalclock > (60 *4))
					{
						if(bonuscnt == 1)
						{
							bonuscnt++;
							GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
						}
						GlobalMembersGLOBAL.tempbuf = string.Format("{0:D2}:{1:D2}", (GlobalMembersGLOBAL.ps[myconnectindex].player_par/(26 *60))%60, (GlobalMembersGLOBAL.ps[myconnectindex].player_par/26)%60);
						GlobalMembersGAME.gametext((320>>2)+71, 60+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

						GlobalMembersGLOBAL.tempbuf = string.Format("{0:D2}:{1:D2}", (GlobalMembersGLOBAL.partime[GlobalMembersGLOBAL.ud.volume_number *11+GlobalMembersGLOBAL.ud.last_level-1]/(26 *60))%60, (GlobalMembersGLOBAL.partime[GlobalMembersGLOBAL.ud.volume_number *11+GlobalMembersGLOBAL.ud.last_level-1]/26)%60);
						GlobalMembersGAME.gametext((320>>2)+71, 69+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

						GlobalMembersGLOBAL.tempbuf = string.Format("{0:D2}:{1:D2}", (GlobalMembersGLOBAL.designertime[GlobalMembersGLOBAL.ud.volume_number *11+GlobalMembersGLOBAL.ud.last_level-1]/(26 *60))%60, (GlobalMembersGLOBAL.designertime[GlobalMembersGLOBAL.ud.volume_number *11+GlobalMembersGLOBAL.ud.last_level-1]/26)%60);
						GlobalMembersGAME.gametext((320>>2)+71, 78+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);

					}
				}
				if(totalclock > (60 *6))
				{
					GlobalMembersGAME.gametext(10, 94+9, "Enemies Killed:", 0, 2+8+16);
					GlobalMembersGAME.gametext(10, 99+4+9, "Enemies Left:", 0, 2+8+16);

					if(bonuscnt == 2)
					{
						bonuscnt++;
						GlobalMembersSOUNDS.sound(FLY_BY);
					}

					if(totalclock > (60 *7))
					{
						if(bonuscnt == 3)
						{
							bonuscnt++;
							GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
						}
						GlobalMembersGLOBAL.tempbuf = string.Format("{0,-3:D}", GlobalMembersGLOBAL.ps[myconnectindex].actors_killed);
						GlobalMembersGAME.gametext((320>>2)+70, 93+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
						if(GlobalMembersGLOBAL.ud.player_skill > 3)
						{
							GlobalMembersGLOBAL.tempbuf = "N/A";
							GlobalMembersGAME.gametext((320>>2)+70, 99+4+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
						}
						else
						{
							if((GlobalMembersGLOBAL.ps[myconnectindex].max_actors_killed-GlobalMembersGLOBAL.ps[myconnectindex].actors_killed) < 0)
								GlobalMembersGLOBAL.tempbuf = string.Format("{0,-3:D}", Convert.ToChar(0));
							else
								GlobalMembersGLOBAL.tempbuf = string.Format("{0,-3:D}", GlobalMembersGLOBAL.ps[myconnectindex].max_actors_killed-GlobalMembersGLOBAL.ps[myconnectindex].actors_killed);
							GlobalMembersGAME.gametext((320>>2)+70, 99+4+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
						}
					}
				}
				if(totalclock > (60 *9))
				{
					GlobalMembersGAME.gametext(10, 120+9, "Secrets Found:", 0, 2+8+16);
					GlobalMembersGAME.gametext(10, 130+9, "Secrets Missed:", 0, 2+8+16);
					if(bonuscnt == 4)
						bonuscnt++;

					if(totalclock > (60 *10))
					{
						if(bonuscnt == 5)
						{
							bonuscnt++;
							GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
						}
						GlobalMembersGLOBAL.tempbuf = string.Format("{0,-3:D}", GlobalMembersGLOBAL.ps[myconnectindex].secret_rooms);
						GlobalMembersGAME.gametext((320>>2)+70, 120+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
						if(GlobalMembersGLOBAL.ps[myconnectindex].secret_rooms > 0)
//C++ TO C# CONVERTER TODO TASK: The following line has a C format specifier which cannot be directly translated to C#:
							GlobalMembersGLOBAL.tempbuf = string.Format("{0,-3:D}%", (100 *GlobalMembersGLOBAL.ps[myconnectindex].secret_rooms/GlobalMembersGLOBAL.ps[myconnectindex].max_secret_rooms));
						GlobalMembersGLOBAL.tempbuf = string.Format("{0,-3:D}", GlobalMembersGLOBAL.ps[myconnectindex].max_secret_rooms-GlobalMembersGLOBAL.ps[myconnectindex].secret_rooms);
						GlobalMembersGAME.gametext((320>>2)+70, 130+9, ref GlobalMembersGLOBAL.tempbuf, 0, 2+8+16);
					}
				}

				if(totalclock > 10240 && totalclock < 10240+10240)
					totalclock = 1024;

				if(KB_KeyWaiting() && totalclock > (60 *2))
				{
					if(KB_KeyPressed(sc_F12))
					{
						KB_ClearKeyDown(sc_F12);
						screencapture("duke0000.pcx",0);
					}

					if(totalclock < (60 *13))
					{
						KB_FlushKeyboardQueue();
						totalclock = (60 *13);
					}
					else if(totalclock < (1000000000))
					   totalclock = (1000000000);
				}
			}
			else
				break;
			nextpage();
		}
	}


	public static void cameratext(short i)
	{
		sbyte flipbits;
		int x;
		int y;

		if(!T1)
		{
			rotatesprite(24<<16,33<<16,65536,0,CAMCORNER,0,0,2,windowx1,windowy1,windowx2,windowy2);
			rotatesprite((320-26)<<16,34<<16,65536,0,CAMCORNER+1,0,0,2,windowx1,windowy1,windowx2,windowy2);
			rotatesprite(22<<16,163<<16,65536,512,CAMCORNER+1,0,0,2+4,windowx1,windowy1,windowx2,windowy2);
			rotatesprite((310-10)<<16,163<<16,65536,512,CAMCORNER+1,0,0,2,windowx1,windowy1,windowx2,windowy2);
			if(totalclock &16)
				rotatesprite(46<<16,32<<16,65536,0,CAMLIGHT,0,0,2,windowx1,windowy1,windowx2,windowy2);
		}
		else
		{
			flipbits = (totalclock<<1)&48;
			for(x = 0;x<394;x+=64)
				for(y = 0;y<200;y+=64)
					rotatesprite(x<<16,y<<16,65536,0,STATIC,0,0,2+flipbits,windowx1,windowy1,windowx2,windowy2);
		}
	}

	public static void vglass(int x,int y,short a,short wn,short n)
	{
		int z;
		int zincs;
		short sect;

		sect = wall[wn].nextsector;
		if(sect == -1)
			return;
		zincs = (sector[sect].floorz-sector[sect].ceilingz) / n;

		for(z = sector[sect].ceilingz;z < sector[sect].floorz; z += zincs)
			GlobalMembersGAME.EGS(sect, x, y, z-(TRAND &8191), GLASSPIECES+(z&(TRAND%3)), -32, 36, 36, a+128-(TRAND &255), 16+(TRAND &31), 0, -1, 5);
	}

	public static void lotsofglass(short i,short wallnum,short n)
	{
		 int j;
		 int xv;
		 int yv;
		 int z;
		 int x1;
		 int y1;
		 short sect;
		 short a;

		 sect = -1;

		 if(wallnum < 0)
		 {
			for(j = n-1; j >= 0 ;j--)
			{
				a = SA-256+(TRAND &511)+1024;
				GlobalMembersGAME.EGS(SECT, SX, SY, SZ, GLASSPIECES+(j%3), -32, 36, 36, a, 32+(TRAND &63), 1024-(TRAND &1023), i, 5);
			}
			return;
		 }

		 j = n+1;

		 x1 = wall[wallnum].x;
		 y1 = wall[wallnum].y;

		 xv = wall[wall[wallnum].point2].x-x1;
		 yv = wall[wall[wallnum].point2].y-y1;

		 x1 -= ksgn(yv);
		 y1 += ksgn(xv);

		 xv /= j;
		 yv /= j;

		 for(j = n;j>0;j--)
			 {
					  x1 += xv;
					  y1 += yv;

			  updatesector(x1, y1, sect);
			  if(sect >= 0)
			  {
				  z = sector[sect].floorz-(TRAND&(klabs(sector[sect].ceilingz-sector[sect].floorz)));
				  if(z < -(32<<8) || z > (32<<8))
					  z = SZ-(32<<8)+(TRAND&((64<<8)-1));
				  a = SA-1024;
				  GlobalMembersGAME.EGS(SECT, x1, y1, z, GLASSPIECES+(j%3), -32, 36, 36, a, 32+(TRAND &63), -(TRAND &1023), i, 5);
			  }
			 }
	}

	public static void spriteglass(short i,short n)
	{
		int j;
		int k;
		int a;
		int z;

		for(j = n;j>0;j--)
		{
			a = TRAND &2047;
			z = SZ-((TRAND &16)<<8);
			k = GlobalMembersGAME.EGS(SECT, SX, SY, z, GLASSPIECES+(j%3), TRAND &15, 36, 36, a, 32+(TRAND &63), -512-(TRAND &2047), i, 5);
			sprite[k].pal = sprite[i].pal;
		}
	}

	public static void ceilingglass(short i,short sectnum,short n)
	{
		 int j;
		 int xv;
		 int yv;
		 int z;
		 int x1;
		 int y1;
		 short a;
		 short s;
		 short startwall;
		 short endwall;

		 startwall = sector[sectnum].wallptr;
		 endwall = startwall+sector[sectnum].wallnum;

		 for(s = startwall;s<(endwall-1);s++)
		 {
			 x1 = wall[s].x;
			 y1 = wall[s].y;

			 xv = (wall[s+1].x-x1)/(n+1);
			 yv = (wall[s+1].y-y1)/(n+1);

			 for(j = n;j>0;j--)
			 {
				  x1 += xv;
				  y1 += yv;
				  a = TRAND &2047;
				  z = sector[sectnum].ceilingz+((TRAND &15)<<8);
				  GlobalMembersGAME.EGS(sectnum, x1, y1, z, GLASSPIECES+(j%3), -32, 36, 36, a, (TRAND &31), 0, i, 5);
			  }
		 }
	}



	public static void lotsofcolourglass(short i,short wallnum,short n)
	{
		 int j;
		 int xv;
		 int yv;
		 int z;
		 int x1;
		 int y1;
		 short sect = -1;
		 short a;
		 short k;

		 if(wallnum < 0)
		 {
			for(j = n-1; j >= 0 ;j--)
			{
				a = TRAND &2047;
				k = GlobalMembersGAME.EGS(SECT, SX, SY, SZ-(TRAND&(63<<8)), GLASSPIECES+(j%3), -32, 36, 36, a, 32+(TRAND &63), 1024-(TRAND &2047), i, 5);
				sprite[k].pal = TRAND &15;
			}
			return;
		 }

		 j = n+1;
		 x1 = wall[wallnum].x;
		 y1 = wall[wallnum].y;

		 xv = (wall[wall[wallnum].point2].x-wall[wallnum].x)/j;
		 yv = (wall[wall[wallnum].point2].y-wall[wallnum].y)/j;

		 for(j = n;j>0;j--)
			 {
					  x1 += xv;
					  y1 += yv;

			  updatesector(x1, y1, sect);
			  z = sector[sect].floorz-(TRAND&(klabs(sector[sect].ceilingz-sector[sect].floorz)));
			  if(z < -(32<<8) || z > (32<<8))
				  z = SZ-(32<<8)+(TRAND&((64<<8)-1));
			  a = SA-1024;
			  k = GlobalMembersGAME.EGS(SECT, x1, y1, z, GLASSPIECES+(j%3), -32, 36, 36, a, 32+(TRAND &63), -(TRAND &2047), i, 5);
			  sprite[k].pal = TRAND &7;
			 }
	}

	public static void SetupGameButtons()
	{
	   CONTROL_DefineFlag(gamefunc_Move_Forward,false);
	   CONTROL_DefineFlag(gamefunc_Move_Backward,false);
	   CONTROL_DefineFlag(gamefunc_Turn_Left,false);
	   CONTROL_DefineFlag(gamefunc_Turn_Right,false);
	   CONTROL_DefineFlag(gamefunc_Strafe,false);
	   CONTROL_DefineFlag(gamefunc_Fire,false);
	   CONTROL_DefineFlag(gamefunc_Open,false);
	   CONTROL_DefineFlag(gamefunc_Run,false);
	   CONTROL_DefineFlag(gamefunc_AutoRun,false);
	   CONTROL_DefineFlag(gamefunc_Jump,false);
	   CONTROL_DefineFlag(gamefunc_Crouch,false);
	   CONTROL_DefineFlag(gamefunc_Look_Up,false);
	   CONTROL_DefineFlag(gamefunc_Look_Down,false);
	   CONTROL_DefineFlag(gamefunc_Look_Left,false);
	   CONTROL_DefineFlag(gamefunc_Look_Right,false);
	   CONTROL_DefineFlag(gamefunc_Strafe_Left,false);
	   CONTROL_DefineFlag(gamefunc_Strafe_Right,false);
	   CONTROL_DefineFlag(gamefunc_Aim_Up,false);
	   CONTROL_DefineFlag(gamefunc_Aim_Down,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_1,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_2,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_3,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_4,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_5,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_6,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_7,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_8,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_9,false);
	   CONTROL_DefineFlag(gamefunc_Weapon_10,false);
	   CONTROL_DefineFlag(gamefunc_Inventory,false);
	   CONTROL_DefineFlag(gamefunc_Inventory_Left,false);
	   CONTROL_DefineFlag(gamefunc_Inventory_Right,false);
	   CONTROL_DefineFlag(gamefunc_Holo_Duke,false);
	   CONTROL_DefineFlag(gamefunc_Jetpack,false);
	   CONTROL_DefineFlag(gamefunc_NightVision,false);
	   CONTROL_DefineFlag(gamefunc_MedKit,false);
	   CONTROL_DefineFlag(gamefunc_TurnAround,false);
	   CONTROL_DefineFlag(gamefunc_SendMessage,false);
	   CONTROL_DefineFlag(gamefunc_Map,false);
	   CONTROL_DefineFlag(gamefunc_Shrink_Screen,false);
	   CONTROL_DefineFlag(gamefunc_Enlarge_Screen,false);
	   CONTROL_DefineFlag(gamefunc_Center_View,false);
	   CONTROL_DefineFlag(gamefunc_Holster_Weapon,false);
	   CONTROL_DefineFlag(gamefunc_Show_Opponents_Weapon,false);
	   CONTROL_DefineFlag(gamefunc_Map_Follow_Mode,false);
	   CONTROL_DefineFlag(gamefunc_See_Coop_View,false);
	   CONTROL_DefineFlag(gamefunc_Mouse_Aiming,false);
	   CONTROL_DefineFlag(gamefunc_Toggle_Crosshair,false);
	   CONTROL_DefineFlag(gamefunc_Steroids,false);
	   CONTROL_DefineFlag(gamefunc_Quick_Kick,false);
	   CONTROL_DefineFlag(gamefunc_Next_Weapon,false);
	   CONTROL_DefineFlag(gamefunc_Previous_Weapon,false);
	}

	/*
	===================
	=
	= GetTime
	=
	===================
	*/

	public static int GetTime()
	   {
	   return totalclock;
	   }


	/*
	===================
	=
	= CenterCenter
	=
	===================
	*/

	public static void CenterCenter()
	   {
	   Console.Write("Center the joystick and press a button\n");
	   }

	/*
	===================
	=
	= UpperLeft
	=
	===================
	*/

	public static void UpperLeft()
	   {
	   Console.Write("Move joystick to upper-left corner and press a button\n");
	   }

	/*
	===================
	=
	= LowerRight
	=
	===================
	*/

	public static void LowerRight()
	   {
	   Console.Write("Move joystick to lower-right corner and press a button\n");
	   }

	/*
	===================
	=
	= CenterThrottle
	=
	===================
	*/

	public static void CenterThrottle()
	   {
	   Console.Write("Center the throttle control and press a button\n");
	   }

	/*
	===================
	=
	= CenterRudder
	=
	===================
	*/

	public static void CenterRudder()
	{
	   Console.Write("Center the rudder control and press a button\n");
	}
}

public class cactype
{
	public int hand;
	public int leng;
	public string @lock;
}

// Rare Multiplayer, when dead, total screen screwup back again!
// E3l1 (Coop /w monsters) sprite list corrupt 50%
// Univbe exit, instead, default to screen buffer.
// Check all caches bounds and memory usages
// Fix enlarger weapon selections to perfection
// Need sounds.c
// Spawning a couple of sounds at the same time
// Check Weapon Switching
// FIRE and FIRE2
// Where should I flash the screen white???
// Jittery on subs in mp?
// Check accurate memory amounts!
// Why squish sound at hit space when dead?
// Falling Counter Not reset in mp
// Wierd small freezer
// Double freeze on player?, still firing
// Do Mouse Flip option
// Save mouse aiming
// Laser bounce off mirrors
// GEORGE:   Ten in text screen.
// Alien:
// Freeze: change
// Press space holding player
// Press space
// tank broke
// 2d mode fucked in fake mp mode
// 207
// Mail not rolling up on conveyers
// Fix all alien animations
// Do episode names in .CONS
// do syntak check for "{?????"
// Make commline parms set approiate multiplayer flags

// Check all breakables to see if they are exploding properly
// Fix freezing palette on Alien

// Do a demo make run overnite
// Fix Super Duck
// Slime Guies, use quickkick.

// Make Lasers from trip bombs reflect off mirrors
// Remember for lockout of sound swears
// Pass sender in packed, NOT
// Fatal sync give no message for TEN
// Hitting TEN BUTTON(OPTION) no TEN SCreen
// Check multioperateswitches for se 31,32
// Fix pal for ceilings (SE#18)
// case 31: sprites up one high
// E1l1 No Kill All troops in room, sleep time

// Fifo for message list

// Bloodsplat on conveyers

// Meclanical
// Increase sound
// Mouse Delay at death
// Wierd slowdown

// Footprints on stuff floating

// Ken, The inside function is called a lot in -1 sectors
// No loading Univbe message rewrite
// Expander must cycle with rest of weapons
// Duck SHOOT PIPEBOMB, red wall

// Get commit source from mark

/*
1. fix pipebomb bug
2. check george maps
4. Save/Restore check (MP and SP)
5. Check TEN
6. Get Commit fixed
8. Is mail slow?
9. Cacheing
10. Blue out "PLAY ON TEN" in MULTIPLAYER
11. Eight Player test
12. Postal.voc not found.
13. All Monsters explode in arcade,
    check SEENINE STRENGTH,
    Change 28<<8 back to 16<<8 in hitradius
    Compare 1.3d to 1.4
14. Check sounds/gfx for for parr lock
15. Player # Loaded a game
16. Replace Crane code 1.3d to 1.4
17. Fix Greenslime
18. Small Freeze sprite,below floor
19. Vesa message auto abort in mp?
20. Fucked Palette in my skip ahead in MP
21. Load in main menu
22. Rotated frag screen no game screen
23. Jibs sounds when killed other dukes
24. Ten code and /f4 mode
25. Fix All MP Glitches!!
26. Unrem Menues anim tenbn
27. buy groc,clothes,scanner
28. Why Double Defs in global and game, is so at work
29. Check that all .objs are erased
30. Check why 1.3ds gotweapon gamedef coop code no workie
31. Heavy mods to net code
32. Make sure all commline stuff works,
33. killed all waitfor???
34. 90k stack
35. double door probs
36: copy protection
* when you start a game the duke saying that is played when you choose a skill the sound is cut off.
* NEWBEASTJUMPING is not deleted at premap in multi-play
if(*c == '4') no work need objs ask ken, commit
{
movesperpacket = 4;
setpackettimeout(0x3fffffff,0x3fffffff);
}
remember, netcode load
*/
//  Ai Problem in god mode.
// Checkplayerhurtwall for forcefields bigforce
// Nuddie, posters. IMF
// Release commit.c to public?
// Document Save bug with mp
// Check moves per packet /f4 waitforeverybody over net?
// Kill IDF OBJ
// No shotguns under water @ tanker
// Unrem copyprotect
// Look for printf and puts
// Check con rewrites
// erase mmulti.c, or get newest objs
// Why nomonsters screwy in load menu in mp
// load last > 'y' == NOT
// Check xptr oos when dead rising to surface.
//    diaginal warping with shotguns
// Test white room.  Lasertripbomb arming crash
// The Bog
// Run Duke Out of windows
// Put Version number in con files
// Test diff. version playing together
// Reorganize dukecd
// Put out patch w/ two weeks testing
// Print draw3d
// Double Klick

/*
Duke Nukem V

Layout:

      Settings:
        Suburbs
          Duke inflitrating neighborhoods inf. by aliens
        Death Valley:
          Sorta like a western.  Bull-skulls half buried in the sand
          Military compound:  Aliens take over nuke-missle silo, duke
            must destroy.
          Abondend Aircraft field
        Vegas:
          Blast anything bright!  Alien lights camoflauged.
          Alien Drug factory. The Blue Liquid
        Mountainal Cave:
          Interior cave battles.
        Jungle:
          Trees, canopee, animals, a mysterious hole in the earth with
          gas seaping thru.
        Penetencury:
          Good use of spotlights:
        Mental ward:
          People whom have claimed to be slowly changing into an
          alien species

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

    After a brief resbit, Duke decides to get back to work.

Cmdr:   "Duke, we've got a lot of scared people down there.
         Some reports even claim that people are already
         slowly changing into aliens."
Duke:   "No problem, my speciality is in croud control."
Cmdr:   "Croud control, my ass!  Remember that incident
         during the war?  You created nuthin' but death and
         destruction."
Duke:   "Not destruction, justice."
Cmdr:   "I'll take no responsibility for your actions.  Your on
         your own!  Behave your self, damnit!  You got that,
         soldger?"
Duke:   "I've always been on my own...   Face it, it's ass kickin' time,
         SIR!"
Cmdr:   "Get outta here...!"
        (Duke gives the Cmdr a hard stair, then cocks his weapon and
         walks out of the room)
Cmdr:   In a wisper: "Good luck, my friend."

        (Cut to a scene where aliens are injecting genetic material
         into an unconcious subject)

Programming:   ( the functions I need )
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


// Test frag screen name fuckup
// Test all xptrs
// Make Jibs stick to ceiling
// Save Game menu crash
// Cache len sum err
// Loading in main (MP), reset totalclock?
// White Room
// Sound hitch with repeat bits
// Rewrite saved menues so no crash
// Put a getpackets after loadplayer in menus
// Put "loading..." before waitfor in loadpla
// No ready2send = 0 for loading
// Test Joystick
// Ten
// Bog
// Test Blimp respawn
// move 1 in player???


