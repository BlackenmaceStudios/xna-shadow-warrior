using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using build;
using mact;

namespace duke3d.game
{
    class weaponhit
    {
        public byte cgg;
        public short picnum,ang,extra,owner,movflag;
        public short tempang,actorstayput,dispicnum;
        public short timetosleep;
        public int floorz,ceilingz,lastvx,lastvy,bposx,bposy,bposz;
        public int[] temp_data = new int[6];
    };

    class SOUNDOWNER
    {
        public short i;
        public int voice;
    };

    enum USRHOOKS_Errors
    {
        USRHOOKS_Warning = -2,
        USRHOOKS_Error   = -1,
        USRHOOKS_Ok      = 0
    };

    struct input
    {
        public sbyte avel, horz;
        public short fvel, svel;
        public uint bits;
    };

    struct SAMPLE
    {
        public bSoundEffect sndeffect;
        public int num;
    };

    struct animwalltype
    {
         public short wallnum;
         public int tag;
    };

    class STATUSBARTYPE
    {
        public short[] frag = new short[Game.MAXPLAYERS];
        public short got_access, last_extra, shield_amount, curr_weapon;
        public short[] ammo_amount = new short[Duke3d.MAX_WEAPONS];
        public short holoduke_on;
        public byte[] gotweapon = new byte[Duke3d.MAX_WEAPONS];
        public byte inven_icon, jetpack_on, heat_on;
        public short firstaid_amount, steroids_amount, holoduke_amount, jetpack_amount;
        public short heat_amount, scuba_amount, boot_amount;
        public short last_weapon, weapon_pos, kickback_pic;
    };

    class user_defs
    {
        public char god,warp_on,cashman,eog,showallmap;
        public char show_help,scrollmode,clipping;
        public string[] user_name = new string[Game.MAXPLAYERS];
        public string[] ridecule = new string[10];
        public string[] savegame = new string[10];
        public string pwlockout,rtsname;
        public char overhead_on,last_overhead,showweapons;

        public short pause_on,from_bonus;
        public short camerasprite,last_camsprite;
        public short last_level,secretlevel;

        public int const_visibility,uw_framerate;
        public int camera_time,folfvel,folavel,folx,foly,fola;
        public int reccnt;

        public Int32 entered_name,screen_tilting,shadows,fta_on,executions,auto_run;
        public Int32 coords,tickrate,m_coop,coop,screen_size,lockout,crosshair;
        public Int32[,] wchoice = new Int32[Game.MAXPLAYERS,Duke3d.MAX_WEAPONS];
        public Int32 playerai;

        public Int32 respawn_monsters,respawn_items,respawn_inventory,recstat,monsters_off,brightness;
        public Int32 m_respawn_items,m_respawn_monsters,m_respawn_inventory,m_recstat,m_monsters_off,detail;
        public Int32 m_ffire,ffire,m_player_skill,m_level_number,m_volume_number,multimode;
        public Int32 player_skill,level_number,volume_number,m_marker,marker,mouseflip;

    };

    struct player_orig
    {
        public int ox,oy,oz;
        public short oa,os;
    };

    class player_struct
    {
        public int zoom,exitx,exity;
        public int[] loogiex = new int[64],loogiey = new int[64];
        public int numloogs,loogcnt;
        public int posx, posy, posz, horiz, ohoriz, ohorizoff, invdisptime;
        public int bobposx,bobposy,oposx,oposy,oposz,pyoff,opyoff;
        public int posxv,posyv,poszv,last_pissed_time,truefz,truecz;
        public int player_par,visibility;
        public int bobcounter,weapon_sway;
        public int pals_time,randomflamex,crack_time;

        public Int32 aim_mode;

        public short ang,oang,angvel,cursectnum,look_ang,last_extra,subweapon;
        public short[] ammo_amount = new short[Duke3d.MAX_WEAPONS];
        public short wackedbyactor,frag,fraggedself;

        public short curr_weapon, last_weapon, tipincs, horizoff, wantweaponfire;
        public short holoduke_amount,newowner,hurt_delay,hbomb_hold_delay;
        public short jumping_counter,airleft,knee_incs,access_incs;
        public short fta,ftq,access_wallnum,access_spritenum;
        public short kickback_pic,got_access,weapon_ang,firstaid_amount;
        public short somethingonplayer,on_crane,i,one_parallax_sectnum;
        public short over_shoulder_on,random_club_frame,fist_incs;
        public short one_eighty_count,cheat_phase;
        public short dummyplayersprite,extra_extra8,quick_kick;
        public short heat_amount,actorsqu,timebeforeexit,customexitsound;

        public short[] weaprecs = new short[16];
        public short weapreccnt,interface_toggle_flag;

        public short rotscrnang,dead_flag,show_empty_weapon;
        public short scuba_amount,jetpack_amount,steroids_amount,shield_amount;
        public short holoduke_on,pycount,weapon_pos,frag_ps;
        public short transporter_hold,last_full_weapon,footprintshade,boot_amount;

        public int scream_voice;

        public char gm,on_warping_sector,footprintcount;
        public char hbomb_on,jumping_toggle,rapid_fire_hold,on_ground;
        public string name;
        public byte inven_icon,buttonpalette;

        public char jetpack_on,spritebridge,lastrandomspot;
        public char scuba_on,footprintpal,heat_on;

        public char  holster_weapon,falling_counter;
        public byte[] gotweapon = new byte[Duke3d.MAX_WEAPONS],palette;
        public byte refresh_inventory;

        public char toggle_key_flag,knuckle_incs; // ,select_dir;
        public char walking_snd_toggle, palookup, hard_landing;
        public char max_secret_rooms,secret_rooms,/*fire_flag,*/pals[3];
        public char max_actors_killed,actors_killed,return_to_center;
    };

    public static class Duke3d
    {
        public const int MAXSLEEPDIST =  16384;
        public const int SLEEPTIME = 24*64;

        #if VOLUMEONE
            public const int BYTEVERSION = 27;
        #else
            public const int BYTEVERSION = 116;
        #endif

        public const int NUMPAGES = 1;

        public const int AUTO_AIM_ANGLE =       48;
        public const int RECSYNCBUFSIZ = 2520;   //2520 is the (LCM of 1-8)*3
        public const int MOVEFIFOSIZ = 256;

        public const int FOURSLEIGHT = (1<<8);

        public const int TICRATE = (120);
        public const int TICSPERFRAME = (TICRATE/26);

        // public const int GC (TICSPERFRAME*44)

        public const int NUM_SOUNDS = 450;

//        public const int    ALT_IS_PRESSED ( KB_KeyPressed( sc_RightAlt ) || KB_KeyPressed( sc_LeftAlt ) )
        //public const int    SHIFTS_IS_PRESSED ( KB_KeyPressed( sc_RightShift ) || KB_KeyPressed( sc_LeftShift ) )
 //       public const int    RANDOMSCRAP EGS(s->sectnum,s->x+(TRAND&255)-128,s->y+(TRAND&255)-128,s->z-(8<<8)-(TRAND&8191),SCRAP6+(TRAND&15),-8,48,48,TRAND&2047,(TRAND&63)+64,-512-(TRAND&2047),i,5)

        public const int    BLACK = 0;
        public const int    DARKBLUE = 1;
        public const int    DARKGREEN = 2;
        public const int    DARKCYAN = 3;
        public const int    DARKRED = 4;
        public const int    DARKPURPLE = 5;
        public const int    BROWN = 6;
        public const int    LIGHTGRAY = 7;

        public const int    DARKGRAY = 8;
        public const int    BLUE = 9;
        public const int    GREEN = 10;
        public const int    CYAN = 11;
        public const int    RED = 12;
        public const int    PURPLE = 13;
        public const int    YELLOW = 14;
        public const int    WHITE = 15;

        public const int    PHEIGHT = (38<<8);

        // public const int P(X) printf("%ld\n",X);

        //public const int WAIT(X) ototalclock=totalclock+(X);while(totalclock<ototalclock)


        public const int MODE_MENU    =   1;
        public const int MODE_DEMO    =   2;
        public const int MODE_GAME    =   4;
        public const int MODE_EOL     =   8;
        public const int MODE_TYPE    =   16;
        public const int MODE_RESTART =   32;
        public const int MODE_SENDTOWHOM =64;
        public const int MODE_END     =  128;


        public const int MAXANIMWALLS = 512;
        public const int MAXINTERPOLATIONS = 2048;
        public const int NUMOFFIRSTTIMEACTIVE = 192;

        public const int MAXCYCLERS = 256;
        public const int MAXSCRIPTSIZE = 20460;
        public const int MAXANIMATES = 64;

        public static int SP(int spritenum) {return Engine.board.sprite[spritenum].yvel; }
        public static int SX(int spritenum) {return Engine.board.sprite[spritenum].x; }
        public static int SY(int spritenum) {return Engine.board.sprite[spritenum].y; }
        public static int SZ(int spritenum) {return Engine.board.sprite[spritenum].z; }
        public static int SS(int spritenum) {return Engine.board.sprite[spritenum].shade; }
        public static int PN(int spritenum) {return Engine.board.sprite[spritenum].picnum; }
        public static int SA(int spritenum) {return Engine.board.sprite[spritenum].ang; }
        public static int SV(int spritenum) {return Engine.board.sprite[spritenum].xvel; }
        public static int ZV(int spritenum) {return Engine.board.sprite[spritenum].zvel; }
        public static int RX(int spritenum) {return Engine.board.sprite[spritenum].xrepeat; }
        public static int RY(int spritenum) {return Engine.board.sprite[spritenum].yrepeat; }
        public static int OW(int spritenum) {return Engine.board.sprite[spritenum].owner; }
        public static int CS(int spritenum) {return Engine.board.sprite[spritenum].cstat; }
        public static int SH(int spritenum) {return Engine.board.sprite[spritenum].extra; }
        public static int CX(int spritenum) {return Engine.board.sprite[spritenum].xoffset; }
        public static int CY(int spritenum) {return Engine.board.sprite[spritenum].yoffset; }
        public static int CD(int spritenum) {return Engine.board.sprite[spritenum].clipdist; }
        public static int PL(int spritenum) {return Engine.board.sprite[spritenum].pal; }
        public static int SLT(int spritenum) {return Engine.board.sprite[spritenum].lotag; }
        public static int SHT(int spritenum) {return Engine.board.sprite[spritenum].hitag; }
        public static int SECT(int spritenum) {return Engine.board.sprite[spritenum].sectnum; }

        public const int face_player = 1;
        public const int geth = 2;
        public const int getv = 4;
        public const int random_angle = 8;
        public const int face_player_slow = 16;
        public const int spin = 32;
        public const int face_player_smart = 64;
        public const int fleeenemy = 128;
        public const int jumptoplayer = 257;
        public const int seekplayer = 512;
        public const int furthestdir = 1024;
        public const int dodgebullet = 4096;

        public static int TRAND() { return (int)Engine.krand(); }


        public const int MAX_WEAPONS = 12;

        public const int KNEE_WEAPON      =    0;
        public const int PISTOL_WEAPON    =    1;
        public const int SHOTGUN_WEAPON   =    2;
        public const int CHAINGUN_WEAPON  =    3;
        public const int RPG_WEAPON       =    4;
        public const int HANDBOMB_WEAPON  =    5;
        public const int SHRINKER_WEAPON  =    6;
        public const int DEVISTATOR_WEAPON=    7;
        public const int TRIPBOMB_WEAPON  =    8;
        public const int FREEZE_WEAPON    =    9;
        public const int HANDREMOTE_WEAPON=    10;
        public const int GROW_WEAPON      =    11;


        public static weaponhit[] hittype = new weaponhit[bMap.MAXSPRITES];

        public int T1(int i) { return hittype[i].temp_data[0]; }
        public int T2(int i) { return hittype[i].temp_data[1]; }
        public int T3(int i) { return hittype[i].temp_data[2]; }
        public int T4(int i) { return hittype[i].temp_data[3]; }
        public int T5(int i) { return hittype[i].temp_data[4]; }
        public int T6(int i) { return hittype[i].temp_data[5]; }

        //public const int ESCESCAPE if(KB_KeyPressed( sc_Escape ) ) gameexit(" ");

        public bool IFWITHIN(int PN, int B,int E) { if((PN)>=(B) && (PN)<=(E)) return true; return false; }
        //public const int KILLIT(KX) {deletesprite(KX);goto BOLT;}


        public bool IFMOVING(short i) { if(Actors.ssp(i,Engine.CLIPMASK0)) return true; return false; }
       // public const int IFHIT j=ifhitbyweapon(i);if(j >= 0)
      //  public const int IFHITSECT j=ifhitsectors(s->sectnum);if(j >= 0)

       // public const int AFLAMABLE(X) (X==BOX||X==TREE1||X==TREE2||X==TIRE||X==CONE)


      //  public const int IFSKILL1 if(player_skill<1)
      //  public const int IFSKILL2 if(player_skill<2)
      //  public const int IFSKILL3 if(player_skill<3)
      //  public const int IFSKILL4 if(player_skill<4)

      //  public const int rnd(X) ((TRAND>>8)>=(255-(X)))

        public static input[,] inputfifo = new input[MOVEFIFOSIZ,Game.MAXPLAYERS];
        public static input[] sync = new input[Game.MAXPLAYERS];
        public static input[] recsync = new input[RECSYNCBUFSIZ];

        public static int movefifosendplc;

        
        public static animwalltype[] animwall = new animwalltype[MAXANIMWALLS];
        public static short numanimwalls,probey,lastprobey;

        public static byte[] mymembuf;
        public static byte typebuflen;
        public static byte[] typebuf = new byte[41];
        //public static char MusicPtr[72000];
        public static int[] msx = new int[2048],msy = new int[2048];
        public static short[,] cyclers = new short[MAXCYCLERS,6];
        public static short numcyclers;
        public static string myname = "";

        

        public static byte numplayersprites;
       // public static byte[] picsiz = new byte[MAXTILES];

       // void add_ammo( short, short, short, short );


        public static int fricxv,fricyv;

        public static short[] tempbufshort = new short[2048];
        public static byte[] tempbuf = new byte[2048], packbuf = new byte[576];

        public static int gc,max_player_health,max_armour_amount;
        public static int[] max_ammo_amount = new int[MAX_WEAPONS];

        public static int impact_damage,respawnactortime,respawnitemtime;

        public const int MOVFIFOSIZ = 256;

        public static short[] spriteq = new short[1024];
        public static short spriteqloc,spriteqamount;
        public static player_struct[] ps = new player_struct[Game.MAXPLAYERS];
        public static player_orig[] po = new player_orig[Game.MAXPLAYERS];
        public static user_defs ud;
        public static short moustat;
        public static short global_random;
        public static int scaredfallz;
        public static string buf;//[80]; //My own generic input buffer

        public static string[] fta_quotes = new string[NUMOFFIRSTTIMEACTIVE];
        public static char[] scantoasc = new char[128];
        public static char ready2send;
        public static char[] scantoascwithshift = new char[128];

        //public static fx_device device;
        public static SAMPLE[] Sound = new SAMPLE[ NUM_SOUNDS ];
        public static Int32 VoiceToggle,AmbienceToggle;
        public static SOUNDOWNER[,] SoundOwner = new SOUNDOWNER[NUM_SOUNDS,4];

        public static byte[] playerreadyflag = new byte[Game.MAXPLAYERS],playerquitflag = new byte[Game.MAXPLAYERS];
        public static string[] sounds = new string[NUM_SOUNDS];
        /*
        public static int script[MAXSCRIPTSIZE],*scriptptr,*insptr,*labelcode,labelcnt;
        public static string label,textptr;
        public static byte error,warning,killit_flag;
        public static int *actorscrptr[MAXTILES],*parsing_actor;
        public static char actortype[MAXTILES];
        public static char *music_pointer;

        public static char ipath[80],opath[80];

        public static char music_fn[4][11][13],music_select;
        public static char env_music_fn[4][13];
        public static short camsprite;

        // public static char gotz;
        public static char inspace(short sectnum);

        public static input loc;
        public static input recsync[RECSYNCBUFSIZ];
        public static int avgfvel, avgsvel, avgavel, avghorz, avgbits;

        public static short numplayers, myconnectindex;
        public static short connecthead, connectpoint2[MAXPLAYERS];   //Player linked list variables (indeces, not connection numbers)
        public static short screenpeek;

        public static int current_menu;
        public static int tempwallptr,animatecnt;
        public static int lockclock,frameplace;
        public static char display_mirror,loadfromgrouponly,rtsplaying;

        public static int movefifoend[MAXPLAYERS], groupfile;
        public static int ototalclock;

        public static int *animateptr[MAXANIMATES], animategoal[MAXANIMATES];
        public static int animatevel[MAXANIMATES];
        // public static int oanimateval[MAXANIMATES];
        public static short neartagsector, neartagwall, neartagsprite;
        public static int neartaghitdist;
        public static short animatesect[MAXANIMATES];
        public static int movefifoplc, vel,svel,angvel,horiz;

        public static short mirrorwall[64], mirrorsector[64], mirrorcnt;

        public static void TestCallBack(unsigned int);

        public const int NUMKEYS 19

        public static int frameplace, chainplace, chainnumpages;
        public static volatile int checksume;

        #include "funct.h"

        public static char screencapt;
        public static short soundps[NUM_SOUNDS],soundpe[NUM_SOUNDS],soundvo[NUM_SOUNDS];
        public static char soundpr[NUM_SOUNDS],soundm[NUM_SOUNDS];
        public static int soundsiz[NUM_SOUNDS];
        public static char level_names[44][33];
        public static int partime[44],designertime[44];
        public static char volume_names[4][33];
        public static char skill_names[5][33];
        public static char level_file_names[44][128];

        public static int32 SoundToggle,MusicToggle;
        public static short last_threehundred,lastsavedpos;
        public static char restorepalette;

        public static short buttonstat;
        public static int cachecount;
        public static char boardfilename[128],waterpal[768],slimepal[768],titlepal[768],drealms[768],endingpal[768];
        public static char betaname[80];
        public static char cachedebug,earthquaketime;
        public static char networkmode;
        public static char lumplockbyte[11];

            //DUKE3D.H - replace the end "my's" with this
        public static int myx, omyx, myxvel, myy, omyy, myyvel, myz, omyz, myzvel;
        public static short myhoriz, omyhoriz, myhorizoff, omyhorizoff, globalskillsound;
        public static short myang, omyang, mycursectnum, myjumpingcounter;
        public static char myjumpingtoggle, myonground, myhardlanding,myreturntocenter;
        public static int fakemovefifoplc;
        public static int myxbak[MOVEFIFOSIZ], myybak[MOVEFIFOSIZ], myzbak[MOVEFIFOSIZ];
        public static int myhorizbak[MOVEFIFOSIZ];
        public static short myangbak[MOVEFIFOSIZ];

        public static short weaponsandammosprites[15];
        */



        

        public static STATUSBARTYPE sbar;
        public static short[,] frags = new short[Game.MAXPLAYERS,Game.MAXPLAYERS];
        public static int cameradist, cameraclock, dukefriction,show_shareware;
        public static char networkmode, movesperpacket;
        public static char gamequit;

        public static char pus,pub,camerashitable,freezerhurtowner,lasermode;
        public static char syncstat;
        public static byte[,] syncval = new byte[Game.MAXPLAYERS,MOVEFIFOSIZ];
        public static sbyte multiwho, multipos, multiwhat, multiflag;
        public static int[] syncvalhead = new int[Game.MAXPLAYERS];
        public static int syncvaltail, syncvaltottail;
        public static int numfreezebounces,rpgblastradius,pipebombblastradius,tripbombblastradius,shrinkerblastradius,morterblastradius,bouncemineblastradius,seenineblastradius;
        // CTW - MODIFICATION
        // public static char stereo,eightytwofifty,playerswhenstarted,playonten,everyothertime;
        public static char stereo,eightytwofifty,playerswhenstarted,everyothertime;
        // CTW END - MODIFICATION
        public static int[] myminlag = new int[Game.MAXPLAYERS];
        public static int mymaxlag, otherminlag, bufferjitter;

        public static int numinterpolations, startofdynamicinterpolations;
        public static int[] oldipos = new int[MAXINTERPOLATIONS];
        public static int[] bakipos = new int[MAXINTERPOLATIONS];
        public static int[] curipos = new int[MAXINTERPOLATIONS];
        public static int[] curiposobj;

        public static short numclouds;
        public static short[] clouds = new short[128],cloudx = new short[128],cloudy = new short[128];
        public static int cloudtotalclock,totalmemory;

        public static int stereomode, stereowidth, stereopixelwidth;

        public static int myaimmode, myaimstat, omyaimstat;

        
    }

    
}
