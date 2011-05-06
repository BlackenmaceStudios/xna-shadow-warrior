public static class GlobalMembersDUKE3D
{

	//extern input inputfifo[DefineConstants.MOVEFIFOSIZ][DefineConstants.MAXPLAYERS], sync[DefineConstants.MAXPLAYERS];
	//extern input recsync[DefineConstants.RECSYNCBUFSIZ];

	//extern int movefifosendplc;
	//extern struct animwalltype animwall[DefineConstants.MAXANIMWALLS];
	//extern short numanimwalls,probey,lastprobey;

	//extern sbyte *mymembuf;
	//extern sbyte typebuflen,typebuf[41];
	//extern sbyte MusicPtr[72000];
	//extern int msx[2048],msy[2048];
	//extern short cyclers[DefineConstants.MAXCYCLERS][6],numcyclers;
	//extern sbyte myname[32];


	//extern sbyte numplayersprites;
	//extern sbyte picsiz[DefineConstants.MAXTILES];

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void add_ammo(short NamelessParameter1, short NamelessParameter2, short NamelessParameter3, short NamelessParameter4);


	//extern int fricxv,fricyv;

	//extern byte tempbuf[2048], packbuf[576];

	//extern int gc,max_player_health,max_armour_amount,max_ammo_amount[DefineConstants.MAX_WEAPONS];

	//extern int impact_damage,respawnactortime,respawnitemtime;

	#define MOVFIFOSIZ

	//extern short spriteq[1024],spriteqloc,spriteqamount;
	//extern struct player_struct ps[DefineConstants.MAXPLAYERS];
	//extern struct player_orig po[DefineConstants.MAXPLAYERS];
	//extern struct user_defs ud;
	//extern short moustat;
	//extern short global_random;
	//extern int scaredfallz;
	//extern sbyte buf[80]; //My own generic input buffer

	//extern sbyte fta_quotes[DefineConstants.NUMOFFIRSTTIMEACTIVE][64];
	//extern sbyte scantoasc[128],ready2send;
	//extern sbyte scantoascwithshift[128];

	//extern fx_device device;
	//extern SAMPLE Sound[DefineConstants.NUM_SOUNDS];
	//extern int VoiceToggle,AmbienceToggle;
	//extern SOUNDOWNER SoundOwner[DefineConstants.NUM_SOUNDS][4];

	//extern sbyte playerreadyflag[DefineConstants.MAXPLAYERS],playerquitflag[DefineConstants.MAXPLAYERS];
	//extern sbyte sounds[DefineConstants.NUM_SOUNDS][14];

	//extern int script[DefineConstants.MAXSCRIPTSIZE],*scriptptr,*insptr,*labelcode,labelcnt;
	//extern sbyte *label,*textptr,error,warning,killit_flag;
	//extern int *actorscrptr[DefineConstants.MAXTILES],*parsing_actor;
	//extern sbyte actortype[DefineConstants.MAXTILES];
	//extern sbyte *music_pointer;

	//extern sbyte ipath[80],opath[80];

	//extern sbyte music_fn[4][11][13],music_select;
	//extern sbyte env_music_fn[4][13];
	//extern short camsprite;

	// extern char gotz;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//extern sbyte inspace(short sectnum);

	//extern struct weaponhit hittype[DefineConstants.MAXSPRITES];

	//extern input loc;
	//extern input recsync[DefineConstants.RECSYNCBUFSIZ];
	//extern int avgfvel, avgsvel, avgavel, avghorz, avgbits;

	//extern short numplayers, myconnectindex;
	//extern short connecthead, connectpoint2[DefineConstants.MAXPLAYERS]; //Player linked list variables (indeces, not connection numbers)
	//extern short screenpeek;

	//extern int current_menu;
	//extern int tempwallptr,animatecnt;
	//extern int lockclock,frameplace;
	//extern sbyte display_mirror,loadfromgrouponly,rtsplaying;

	//extern int movefifoend[DefineConstants.MAXPLAYERS], groupfile;
	//extern int ototalclock;

	//extern int *animateptr[DefineConstants.MAXANIMATES], animategoal[DefineConstants.MAXANIMATES];
	//extern int animatevel[DefineConstants.MAXANIMATES];
	// extern long oanimateval[MAXANIMATES];
	//extern short neartagsector, neartagwall, neartagsprite;
	//extern int neartaghitdist;
	//extern short animatesect[DefineConstants.MAXANIMATES];
	//extern int movefifoplc, vel,svel,angvel,horiz;

	//extern short mirrorwall[64], mirrorsector[64], mirrorcnt;

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//extern void TestCallBack(uint NamelessParameter);

	#define NUMKEYS

	//extern int frameplace, chainplace, chainnumpages;
	//extern volatile int checksume;


	//extern sbyte screencapt;
	//extern short soundps[DefineConstants.NUM_SOUNDS],soundpe[DefineConstants.NUM_SOUNDS],soundvo[DefineConstants.NUM_SOUNDS];
	//extern sbyte soundpr[DefineConstants.NUM_SOUNDS],soundm[DefineConstants.NUM_SOUNDS];
	//extern int soundsiz[DefineConstants.NUM_SOUNDS];
	//extern sbyte level_names[44][33];
	//extern int partime[44],designertime[44];
	//extern sbyte volume_names[4][33];
	//extern sbyte skill_names[5][33];
	//extern sbyte level_file_names[44][128];

	//extern int SoundToggle,MusicToggle;
	//extern short last_threehundred,lastsavedpos;
	//extern sbyte restorepalette;

	//extern short buttonstat;
	//extern int cachecount;
	//extern sbyte boardfilename[128],waterpal[768],slimepal[768],titlepal[768],drealms[768],endingpal[768];
	//extern sbyte betaname[80];
	//extern sbyte cachedebug,earthquaketime;
	//extern sbyte networkmode;
	//extern sbyte lumplockbyte[11];

		//DUKE3D.H - replace the end "my's" with this
	//extern int myx, omyx, myxvel, myy, omyy, myyvel, myz, omyz, myzvel;
	//extern short myhoriz, omyhoriz, myhorizoff, omyhorizoff, globalskillsound;
	//extern short myang, omyang, mycursectnum, myjumpingcounter;
	//extern sbyte myjumpingtoggle, myonground, myhardlanding,myreturntocenter;
	//extern int fakemovefifoplc;
	//extern int myxbak[DefineConstants.MOVEFIFOSIZ], myybak[DefineConstants.MOVEFIFOSIZ], myzbak[DefineConstants.MOVEFIFOSIZ];
	//extern int myhorizbak[DefineConstants.MOVEFIFOSIZ];
	//extern short myangbak[DefineConstants.MOVEFIFOSIZ];

	//extern short weaponsandammosprites[15];

	//extern STATUSBARTYPE sbar;
	//extern short frags[DefineConstants.MAXPLAYERS][DefineConstants.MAXPLAYERS];
	//extern int cameradist, cameraclock, dukefriction,show_shareware;
	//extern sbyte networkmode, movesperpacket;
	//extern sbyte gamequit;

	//extern sbyte pus,pub,camerashitable,freezerhurtowner,lasermode;
	//extern sbyte syncstat, syncval[DefineConstants.MAXPLAYERS][DefineConstants.MOVEFIFOSIZ];
	//extern sbyte multiwho, multipos, multiwhat, multiflag;
	//extern int syncvalhead[DefineConstants.MAXPLAYERS], syncvaltail, syncvaltottail;
	//extern int numfreezebounces,rpgblastradius,pipebombblastradius,tripbombblastradius,shrinkerblastradius,morterblastradius,bouncemineblastradius,seenineblastradius;
	// CTW - MODIFICATION
	// extern char stereo,eightytwofifty,playerswhenstarted,playonten,everyothertime;
	//extern sbyte stereo,eightytwofifty,playerswhenstarted,everyothertime;
	// CTW END - MODIFICATION
	//extern int myminlag[DefineConstants.MAXPLAYERS], mymaxlag, otherminlag, bufferjitter;

	//extern int numinterpolations, startofdynamicinterpolations;
	//extern int oldipos[DefineConstants.MAXINTERPOLATIONS];
	//extern int bakipos[DefineConstants.MAXINTERPOLATIONS];
	//extern int *curipos[DefineConstants.MAXINTERPOLATIONS];

	//extern short numclouds,clouds[128],cloudx[128],cloudy[128];
	//extern int cloudtotalclock,totalmemory;

	//extern int stereomode, stereowidth, stereopixelwidth;

	//extern int myaimmode, myaimstat, omyaimstat;
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


#define VOLUMEALL
#define PLUTOPAK
// #define VOLUMEONE
// #define ONELEVELDEMO

// #define TEN
// #define BETA

// #define AUSTRALIA

#define MAXSLEEPDIST
#define SLEEPTIME

#if VOLUMEONE
	#define BYTEVERSION_AlternateDefinition1
	#define BYTEVERSION
#else
	#define BYTEVERSION_AlternateDefinition2
	#define BYTEVERSION
#endif

#define NUMPAGES

#define AUTO_AIM_ANGLE
#define RECSYNCBUFSIZ
#define MOVEFIFOSIZ

#define FOURSLEIGHT






#define TICRATE
#define TICSPERFRAME

// #define GC (TICSPERFRAME*44)

#define NUM_SOUNDS

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma aux sgn = "add ebx, ebx", "sbb eax, eax", "cmp eax, ebx", "adc eax, 0", parm [ebx]
#define ALT_IS_PRESSED
#define SHIFTS_IS_PRESSED
#define RANDOMSCRAP

#define BLACK
#define DARKBLUE
#define DARKGREEN
#define DARKCYAN
#define DARKRED
#define DARKPURPLE
#define BROWN
#define LIGHTGRAY

#define DARKGRAY
#define BLUE
#define GREEN
#define CYAN
#define RED
#define PURPLE
#define YELLOW
#define WHITE

#define PHEIGHT

// #define P(X) printf("%ld\n",X);

#define WAIT


#define MODE_MENU
#define MODE_DEMO
#define MODE_GAME
#define MODE_EOL
#define MODE_TYPE
#define MODE_RESTART
#define MODE_SENDTOWHOM
#define MODE_END


#define MAXANIMWALLS
#define MAXINTERPOLATIONS
#define NUMOFFIRSTTIMEACTIVE

#define MAXCYCLERS
#define MAXSCRIPTSIZE
#define MAXANIMATES

#define SP
#define SX
#define SY
#define SZ
#define SS
#define PN
#define SA
#define SV
#define ZV
#define RX
#define RY
#define OW
#define CS
#define SH
#define CX
#define CY
#define CD
#define PL
#define SLT
#define SHT
#define SECT

#define face_player
#define geth
#define getv
#define random_angle
#define face_player_slow
#define spin
#define face_player_smart
#define fleeenemy
#define jumptoplayer
#define seekplayer
#define furthestdir
#define dodgebullet

#define TRAND


#define MAX_WEAPONS

#define KNEE_WEAPON
#define PISTOL_WEAPON
#define SHOTGUN_WEAPON
#define CHAINGUN_WEAPON
#define RPG_WEAPON
#define HANDBOMB_WEAPON
#define SHRINKER_WEAPON
#define DEVISTATOR_WEAPON
#define TRIPBOMB_WEAPON
#define FREEZE_WEAPON
#define HANDREMOTE_WEAPON
#define GROW_WEAPON

#define T1
#define T2
#define T3
#define T4
#define T5
#define T6

#define ESCESCAPE

#define IFWITHIN
#define KILLIT


#define IFMOVING
#define IFHIT
#define IFHITSECT

#define AFLAMABLE


#define IFSKILL1
#define IFSKILL2
#define IFSKILL3
#define IFSKILL4

#define rnd

public class SOUNDOWNER
{
	public short i;
	public int voice;
}

#define __USRHOOKS_H

public enum USRHOOKS_Errors
   {
   USRHOOKS_Warning = -2,
   USRHOOKS_Error = -1,
   USRHOOKS_Ok = 0
   }

public class input
{
	public sbyte avel;
	public sbyte horz;
	public short fvel;
	public short svel;
	public uint bits;
}

public class SAMPLE
{
	public string ptr;
	public volatile sbyte @lock;
	public int length;
	public int num;
}

public static class animwalltype
{
		public short wallnum;
		public int tag;
}

public class user_defs
{
	public sbyte god;
	public sbyte warp_on;
	public sbyte cashman;
	public sbyte eog;
	public sbyte showallmap;
	public sbyte show_help;
	public sbyte scrollmode;
	public sbyte clipping;
	public sbyte[,] user_name = new sbyte[DefineConstants.MAXPLAYERS, 32];
	public sbyte[,] ridecule = new sbyte[10, 40];
	public sbyte[,] savegame = new sbyte[10, 22];
	public string pwlockout = new string(new char[128]);
	public string rtsname = new string(new char[128]);
	public sbyte overhead_on;
	public sbyte last_overhead;
	public sbyte showweapons;

	public short pause_on;
	public short from_bonus;
	public short camerasprite;
	public short last_camsprite;
	public short last_level;
	public short secretlevel;

	public int const_visibility;
	public int uw_framerate;
	public int camera_time;
	public int folfvel;
	public int folavel;
	public int folx;
	public int foly;
	public int fola;
	public int reccnt;

	public int entered_name;
	public int screen_tilting;
	public int shadows;
	public int fta_on;
	public int executions;
	public int auto_run;
	public int coords;
	public int tickrate;
	public int m_coop;
	public int coop;
	public int screen_size;
	public int lockout;
	public int crosshair;
	public int[,] wchoice = new int[DefineConstants.MAXPLAYERS, DefineConstants.MAX_WEAPONS];
	public int playerai;

	public int respawn_monsters;
	public int respawn_items;
	public int respawn_inventory;
	public int recstat;
	public int monsters_off;
	public int brightness;
	public int m_respawn_items;
	public int m_respawn_monsters;
	public int m_respawn_inventory;
	public int m_recstat;
	public int m_monsters_off;
	public int detail;
	public int m_ffire;
	public int ffire;
	public int m_player_skill;
	public int m_level_number;
	public int m_volume_number;
	public int multimode;
	public int player_skill;
	public int level_number;
	public int volume_number;
	public int m_marker;
	public int marker;
	public int mouseflip;

}

public class player_orig
{
	public int ox;
	public int oy;
	public int oz;
	public short oa;
	public short os;
}

public class player_struct
{
	public int zoom;
	public int exitx;
	public int exity;
	public int[] loogiex = new int[64];
	public int[] loogiey = new int[64];
	public int numloogs;
	public int loogcnt;
	public int posx;
	public int posy;
	public int posz;
	public int horiz;
	public int ohoriz;
	public int ohorizoff;
	public int invdisptime;
	public int bobposx;
	public int bobposy;
	public int oposx;
	public int oposy;
	public int oposz;
	public int pyoff;
	public int opyoff;
	public int posxv;
	public int posyv;
	public int poszv;
	public int last_pissed_time;
	public int truefz;
	public int truecz;
	public int player_par;
	public int visibility;
	public int bobcounter;
	public int weapon_sway;
	public int pals_time;
	public int randomflamex;
	public int crack_time;

	public int aim_mode;

	public short ang;
	public short oang;
	public short angvel;
	public short cursectnum;
	public short look_ang;
	public short last_extra;
	public short subweapon;
	public short[] ammo_amount = new short[DefineConstants.MAX_WEAPONS];
	public short wackedbyactor;
	public short frag;
	public short fraggedself;

	public short curr_weapon;
	public short last_weapon;
	public short tipincs;
	public short horizoff;
	public short wantweaponfire;
	public short holoduke_amount;
	public short newowner;
	public short hurt_delay;
	public short hbomb_hold_delay;
	public short jumping_counter;
	public short airleft;
	public short knee_incs;
	public short access_incs;
	public short fta;
	public short ftq;
	public short access_wallnum;
	public short access_spritenum;
	public short kickback_pic;
	public short got_access;
	public short weapon_ang;
	public short firstaid_amount;
	public short somethingonplayer;
	public short on_crane;
	public short i;
	public short one_parallax_sectnum;
	public short over_shoulder_on;
	public short random_club_frame;
	public short fist_incs;
	public short one_eighty_count;
	public short cheat_phase;
	public short dummyplayersprite;
	public short extra_extra8;
	public short quick_kick;
	public short heat_amount;
	public short actorsqu;
	public short timebeforeexit;
	public short customexitsound;

	public short[] weaprecs = new short[16];
	public short weapreccnt;
	public short interface_toggle_flag;

	public short rotscrnang;
	public short dead_flag;
	public short show_empty_weapon;
	public short scuba_amount;
	public short jetpack_amount;
	public short steroids_amount;
	public short shield_amount;
	public short holoduke_on;
	public short pycount;
	public short weapon_pos;
	public short frag_ps;
	public short transporter_hold;
	public short last_full_weapon;
	public short footprintshade;
	public short boot_amount;

	public int scream_voice;

	public sbyte gm;
	public sbyte on_warping_sector;
	public sbyte footprintcount;
	public sbyte hbomb_on;
	public sbyte jumping_toggle;
	public sbyte rapid_fire_hold;
	public sbyte on_ground;
	public string name = new string(new char[32]);
	public sbyte inven_icon;
	public sbyte buttonpalette;

	public sbyte jetpack_on;
	public sbyte spritebridge;
	public sbyte lastrandomspot;
	public sbyte scuba_on;
	public sbyte footprintpal;
	public sbyte heat_on;

	public sbyte holster_weapon;
	public sbyte falling_counter;
	public string gotweapon = new string(new char[DefineConstants.MAX_WEAPONS]);
	public sbyte refresh_inventory;
	public string palette;

	public sbyte toggle_key_flag; // ,select_dir;
	public sbyte knuckle_incs;
	public sbyte walking_snd_toggle;
	public sbyte palookup;
	public sbyte hard_landing;
	public sbyte max_secret_rooms; //fire_flag,
	public sbyte secret_rooms;
	public string pals = new string(new char[3]);
	public sbyte max_actors_killed;
	public sbyte actors_killed;
	public sbyte return_to_center;
}


public class weaponhit
{
	public sbyte cgg;
	public short picnum;
	public short ang;
	public short extra;
	public short owner;
	public short movflag;
	public short tempang;
	public short actorstayput;
	public short dispicnum;
	public short timetosleep;
	public int floorz;
	public int ceilingz;
	public int lastvx;
	public int lastvy;
	public int bposx;
	public int bposy;
	public int bposz;
	public int[] temp_data = new int[6];
}




//DUKE3D.H:
public class STATUSBARTYPE
{
		public short[] frag = new short[DefineConstants.MAXPLAYERS];
		public short got_access;
		public short last_extra;
		public short shield_amount;
		public short curr_weapon;
		public short[] ammo_amount = new short[DefineConstants.MAX_WEAPONS];
		public short holoduke_on;
		public string gotweapon = new string(new char[DefineConstants.MAX_WEAPONS]);
		public sbyte inven_icon;
		public sbyte jetpack_on;
		public sbyte heat_on;
		public short firstaid_amount;
		public short steroids_amount;
		public short holoduke_amount;
		public short jetpack_amount;
		public short heat_amount;
		public short scuba_amount;
		public short boot_amount;
		public short last_weapon;
		public short weapon_pos;
		public short kickback_pic;

}

