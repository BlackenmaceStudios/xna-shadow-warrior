public static class GlobalMembersGLOBAL
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


	public static string mymembuf;
	public static string MusicPtr = new string(new char[72000]);

	public static short global_random;
	public static short neartagsector;
	public static short neartagwall;
	public static short neartagsprite;

	public static int gc;
	public static int neartaghitdist;
	public static int lockclock;
	public static int max_player_health;
	public static int max_armour_amount;
	public static int[] max_ammo_amount = new int[MAX_WEAPONS];

	// long temp_data[MAXSPRITES][6];
	public static weaponhit[] hittype = new weaponhit[MAXSPRITES];
	public static short[] spriteq = new short[1024];
	public static short spriteqloc;
	public static short spriteqamount=64;
	public static short moustat;
	public static animwalltype[] animwall = new animwalltype[MAXANIMWALLS];
	public static short numanimwalls;
	public static int[] animateptr = new int[MAXANIMATES];
	public static int[] animategoal = new int[MAXANIMATES];
	public static int[] animatevel = new int[MAXANIMATES];
	public static int animatecnt;
	// long oanimateval[MAXANIMATES];
	public static short[] animatesect = new short[MAXANIMATES];
	public static int[] msx = new int[2048];
	public static int[] msy = new int[2048];
	public static short[,] cyclers = new short[MAXCYCLERS, 6];
	public static short numcyclers;

	public static sbyte[,] fta_quotes = new sbyte[NUMOFFIRSTTIMEACTIVE, 64];

	public static byte[] tempbuf = new byte[2048];
	public static byte[] packbuf = new byte[576];

	public static string buf = new string(new char[80]);

	public static short camsprite;
	public static short[] mirrorwall = new short[64];
	public static short[] mirrorsector = new short[64];
	public static short mirrorcnt;

	public static int current_menu;

	public static string betaname = new string(new char[80]);

	public static sbyte[,] level_names = new sbyte[44, 33];
	public static sbyte[,] level_file_names = new sbyte[44, 128];
	public static int[] partime = new int[44];
	public static int[] designertime = new int[44];
	public static sbyte[,] volume_names = new sbyte[4, 33];
	public static sbyte[,] skill_names = new sbyte[5, 33];

	public static volatile int checksume;
	public static int[] soundsiz = new int[NUM_SOUNDS];

	public static short[] soundps = new short[NUM_SOUNDS];
	public static short[] soundpe = new short[NUM_SOUNDS];
	public static short[] soundvo = new short[NUM_SOUNDS];
	public static string soundm = new string(new char[NUM_SOUNDS]);
	public static string soundpr = new string(new char[NUM_SOUNDS]);
	public static sbyte[,] sounds = new sbyte[NUM_SOUNDS, 14];

	public static short title_zoom;

	public static fx_device device = new fx_device();

	public static SAMPLE[] Sound = new SAMPLE[NUM_SOUNDS];
	public static SOUNDOWNER[,] SoundOwner = new SOUNDOWNER[NUM_SOUNDS, 4];

	public static sbyte numplayersprites;
	public static sbyte loadfromgrouponly;
	public static sbyte earthquaketime;

	public static int fricxv;
	public static int fricyv;
	public static player_orig[] po = new player_orig[MAXPLAYERS];
	public static player_struct[] ps = new player_struct[MAXPLAYERS];
	public static user_defs ud = new user_defs();

	public static sbyte pus;
	public static sbyte pub;
	public static sbyte syncstat;
	public static sbyte[,] syncval = new sbyte[MAXPLAYERS, MOVEFIFOSIZ];
	public static int[] syncvalhead = new int[MAXPLAYERS];
	public static int syncvaltail;
	public static int syncvaltottail;

	public static input[] sync = new input[MAXPLAYERS];
	public static input loc = new input();
	public static input[] recsync = new input[RECSYNCBUFSIZ];
	public static int avgfvel;
	public static int avgsvel;
	public static int avgavel;
	public static int avghorz;
	public static int avgbits;


	public static input[,] inputfifo = new input[MOVEFIFOSIZ, MAXPLAYERS];
	public static input[] recsync = new input[RECSYNCBUFSIZ];

	public static int movefifosendplc;

	  //Multiplayer syncing variables
	public static short screenpeek;
	public static int[] movefifoend = new int[MAXPLAYERS];


		//Game recording variables

	public static string playerreadyflag = new string(new char[MAXPLAYERS]);
	public static sbyte ready2send;
	public static string playerquitflag = new string(new char[MAXPLAYERS]);
	public static int vel;
	public static int svel;
	public static int angvel;
	public static int horiz;
	public static int ototalclock;
	public static int respawnactortime=768;
	public static int respawnitemtime=768;
	public static int groupfile;

	public static int[] script = new int[MAXSCRIPTSIZE];
	public static int *scriptptr;
	public static int *insptr;
	public static int *labelcode;
	public static int labelcnt;
	public static int[] actorscrptr = new int[MAXTILES];
	public static int *parsing_actor;
	public static string label;
	public static string textptr;
	public static sbyte error;
	public static sbyte warning;
	public static sbyte killit_flag;
	public static string music_pointer;
	public static string actortype = new string(new char[MAXTILES]);


	public static sbyte display_mirror;
	public static sbyte typebuflen;
	public static string typebuf = new string(new char[41]);

	public static sbyte[,,] music_fn = new sbyte[4, 11, 13];
	public static sbyte music_select;
	public static sbyte[,] env_music_fn = new sbyte[4, 13];
	public static sbyte rtsplaying;


	public static short[] weaponsandammosprites = { RPGSPRITE, CHAINGUNSPRITE, DEVISTATORAMMO, RPGAMMO, RPGAMMO, JETPACK, SHIELD, FIRSTAID, STEROIDS, RPGAMMO, RPGAMMO, RPGSPRITE, RPGAMMO, FREEZESPRITE, FREEZEAMMO };

	public static int impact_damage;

			//GLOBAL.C - replace the end "my's" with this
	public static int myx;
	public static int omyx;
	public static int myxvel;
	public static int myy;
	public static int omyy;
	public static int myyvel;
	public static int myz;
	public static int omyz;
	public static int myzvel;
	public static short myhoriz;
	public static short omyhoriz;
	public static short myhorizoff;
	public static short omyhorizoff;
	public static short myang;
	public static short omyang;
	public static short mycursectnum;
	public static short myjumpingcounter;
	public static short[,] frags = new short[MAXPLAYERS, MAXPLAYERS];

	public static sbyte myjumpingtoggle;
	public static sbyte myonground;
	public static sbyte myhardlanding;
	public static sbyte myreturntocenter;
	public static sbyte multiwho;
	public static sbyte multipos;
	public static sbyte multiwhat;
	public static sbyte multiflag;

	public static int fakemovefifoplc;
	public static int movefifoplc;
	public static int[] myxbak = new int[MOVEFIFOSIZ];
	public static int[] myybak = new int[MOVEFIFOSIZ];
	public static int[] myzbak = new int[MOVEFIFOSIZ];
	public static int[] myhorizbak = new int[MOVEFIFOSIZ];
	public static int dukefriction = 0xcc00;
	public static int show_shareware;

	public static short[] myangbak = new short[MOVEFIFOSIZ];
	public static string myname = new string(new char[32]);
	public static sbyte camerashitable;
	public static sbyte freezerhurtowner=0;
	public static sbyte lasermode;
	// CTW - MODIFICATION
	// char networkmode = 255, movesperpacket = 1,gamequit = 0,playonten = 0,everyothertime;
	public static sbyte networkmode = 255;
	public static sbyte movesperpacket = 1;
	public static sbyte gamequit = 0;
	public static sbyte everyothertime;
	// CTW END - MODIFICATION
	public static int numfreezebounces=3;
	public static int rpgblastradius;
	public static int pipebombblastradius;
	public static int tripbombblastradius;
	public static int shrinkerblastradius;
	public static int morterblastradius;
	public static int bouncemineblastradius;
	public static int seenineblastradius;
	public static STATUSBARTYPE sbar = new STATUSBARTYPE();

	public static int[] myminlag = new int[MAXPLAYERS];
	public static int mymaxlag;
	public static int otherminlag;
	public static int bufferjitter = 1;
	public static short numclouds;
	public static short[] clouds = new short[128];
	public static short[] cloudx = new short[128];
	public static short[] cloudy = new short[128];
	public static int cloudtotalclock = 0;
	public static int totalmemory = 0;
	public static int numinterpolations = 0;
	public static int startofdynamicinterpolations = 0;
	public static int[] oldipos = new int[MAXINTERPOLATIONS];
	public static int[] bakipos = new int[MAXINTERPOLATIONS];
	public static int[] curipos = new int[MAXINTERPOLATIONS];
}

