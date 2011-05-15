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
using duke3d.mact;
using duke3d.game.script;
namespace duke3d.game
{
    public class weaponhit
    {
        public byte cgg;
        public short picnum,ang,extra,owner,movflag;
        public short tempang,actorstayput,dispicnum;
        public short timetosleep;
        public int floorz,ceilingz,lastvx,lastvy,bposx,bposy,bposz;
        public int[] temp_data = new int[6];
    };

    public class SOUNDOWNER
    {
        public short i;
        public int voice;
    };

    public enum USRHOOKS_Errors
    {
        USRHOOKS_Warning = -2,
        USRHOOKS_Error   = -1,
        USRHOOKS_Ok      = 0
    };

    public struct input
    {
        public sbyte avel, horz;
        public short fvel, svel;
        public uint bits;
    };

    public struct SAMPLE
    {
        public bSoundEffect sndeffect;
        public int num;
    };

    public struct animwalltype
    {
         public short wallnum;
         public int tag;
    };

    public class STATUSBARTYPE
    {
        public short[] frag = new short[Game.MAXPLAYERS];
        public short got_access, last_extra, shield_amount, curr_weapon;
        public short[] ammo_amount = new short[Globals.MAX_WEAPONS];
        public short holoduke_on;
        public byte[] gotweapon = new byte[Globals.MAX_WEAPONS];
        public byte inven_icon, jetpack_on, heat_on;
        public short firstaid_amount, steroids_amount, holoduke_amount, jetpack_amount;
        public short heat_amount, scuba_amount, boot_amount;
        public short last_weapon, weapon_pos, kickback_pic;
    };

    public class user_defs
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
        public Int32 coords,tickrate,m_coop,coop,screen_size,crosshair;
        public bool lockout;
        public Int32[,] wchoice = new Int32[Game.MAXPLAYERS, Globals.MAX_WEAPONS];
        public Int32 playerai;

        public Int32 respawn_monsters,respawn_items,respawn_inventory,recstat,monsters_off,brightness;
        public Int32 m_respawn_items,m_respawn_monsters,m_respawn_inventory,m_recstat,m_monsters_off,detail;
        public Int32 m_ffire,ffire,m_player_skill,m_level_number,m_volume_number,multimode;
        public Int32 player_skill,level_number,volume_number,m_marker,marker,mouseflip;

    };

    

    

    public static class Globals
    {
        public static Gamescript script = new Gamescript();
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


        private static Random random = new Random();
        public static int rand( int max )
        {
            return random.Next(0, max);
        }

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
        private static int[] _badguylist;

        public static void SetBadguyList(int[] list)
        {
            _badguylist = list;
        }

        public static bool badguy(spritetype s)
        {
            int picnum = s.picnum;

            for (int i = 0; i < _badguylist.Length; i++)
            {
                if (_badguylist[i] == picnum)
                    return true;
            }

            return false;
        }

        public static int ldist(spritetype s1, spritetype s2)
        {
            int vx, vy;
            vx = s1.x - s2.x;
            vy = s1.y - s2.y;
            return (MathUtility.FindDistance2D(vx, vy) + 1);
        }

        public static int ldist3d(spritetype s1, spritetype s2)
        {
            int vx, vy, vz;
            vx = s1.x - s2.x;
            vy = s1.y - s2.y;
            vz = s1.z - s2.z;
            return (MathUtility.FindDistance3D(vx, vy, vz) + 1);
        }

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
        public static spritetype lastspawnedsprite;
        public static int T1(int i) { return hittype[i].temp_data[0]; }
        public static int T2(int i) { return hittype[i].temp_data[1]; }
        public static int T3(int i) { return hittype[i].temp_data[2]; }
        public static int T4(int i) { return hittype[i].temp_data[3]; }
        public static int T5(int i) { return hittype[i].temp_data[4]; }
        public static int T6(int i) { return hittype[i].temp_data[5]; }

        //public const int ESCESCAPE if(KB_KeyPressed( sc_Escape ) ) gameexit(" ");

        public static bool IFWITHIN(int PN, int B, int E) { if ((PN) >= (B) && (PN) <= (E)) return true; return false; }
        //public const int KILLIT(KX) {deletesprite(KX);goto BOLT;}


       // public static bool IFMOVING(short i) { if (Actors.ssp(i, Engine.CLIPMASK0)) return true; return false; }
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

       


        public const int MOVFIFOSIZ = 256;

        public static Player[] ps = new Player[Game.MAXPLAYERS];
        public static player_orig[] po = new player_orig[Game.MAXPLAYERS];
        public static user_defs ud = new user_defs();
        public static short moustat;
        public static short global_random;
        public static int scaredfallz;
        public static string buf;//[80]; //My own generic input buffer

        public static SAMPLE[] Sound = new SAMPLE[NUM_SOUNDS];
        
    }

    
}
