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
using duke3d.game.script;
namespace duke3d.game
{
    public enum GameState
    {
        GAMESTATE_STARTUP,
        GAMESTATE_DREALMS,
        GAMESTATE_INTRO,
        GAMESTATE_INTRO_P0,
        GAMESTATE_INTRO_P1,
        GAMESTATE_INTRO_P2,
        GAMESTATE_INTRO_P3,
        GAMESTATE_INTRO_P4,
        GAMESTATE_DISCONNECTED,
        GAMESTATE_DISCONNECTEDDEMO,
        GAMESTATE_LOADING,
        GAMESTATE_INGAMEMENU,
        GAMESTATE_INGAME
    }
    //
    // Game
    //
    public class Game
    {
        private static GameState gameState = GameState.GAMESTATE_INTRO;
        private Anim logoAnm = new Anim();
        private Anim playingAnm;
        private static Menu menus = new Menu();
        public const int MAXPLAYERS = 16;
        private static System.Collections.Generic.List<Actor> actors = new System.Collections.Generic.List<Actor>();

        public static bool[] keysdown = new bool[256];
        public static LookupTable lookupTable = new LookupTable();
        public static bool[] actortype = new bool[bMap.MAXTILES];
        public static int totalclock = 0;

        public static void SetGameState(GameState gameState)
        {
            Game.gameState = gameState;
        }

        public static GameState State
        {
            get
            {
                return gameState;
            }
        }

        private void InitScripts()
        {
            Globals.script.Init("game.dll");
        }

        public void vscrn()
        {
           
        }

        public static void enterlevel(GameState state)
        {
            int posx = 0, posy = 0, posz = 0;
            short cursectnum = 0, ang = 0;
            Game.SetGameState(state);

            Player localplayer = new Player();

            SoundSystem.playmusic(Globals.script.volumes[Globals.ud.volume_number][Globals.ud.level_number].musicfilename);

            if (Engine.loadboard(Globals.script.volumes[Globals.ud.volume_number][Globals.ud.level_number].filename, ref posx, ref posy, ref posz, ref ang, ref cursectnum) == -1)
            {
                throw new Exception("Map not found!");
            }

            // Spawn each entity that has an actor
            foreach (spritetype spr in Engine.board.sprite)
            {
                if (spr == null)
                    continue;

                Gamescript.ActorScriptFunction sprfunc = Globals.script.GetFunctionForActor(spr.picnum);

                if (sprfunc != null)
                {
                    Actor actor = new Actor();
                    actor.SetAIScript(sprfunc);
                    actor.Spawn(spr);

                    Gamescript.ActorScriptFunction spawnfunc = Globals.script.GetSpawnFunctionForActor(spr.picnum);
                    if(spawnfunc != null)
                        spawnfunc.Invoke(actor);

                    actors.Add(actor);
                }
            }

            localplayer.SetPosition(posx, posy, posz, ang, cursectnum);
            localplayer.Spawn(null);

            Globals.ps[0] = localplayer;
        }

        public static spritetype EGS(short whatsect,int s_x,int s_y,int s_z,short s_pn, sbyte s_s, byte s_xr,byte s_yr,short s_a,short s_ve,short s_zv,short s_ow, sbyte s_ss)
        {
            spritetype s = SpawnActor(s_x, s_y, s_z, s_ss, s_pn);

           // Globals.hittype[i].bposx = s_x;
           // Globals.hittype[i].bposy = s_y;
           // Globals.hittype[i].bposz = s_z;

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
            /*
            Globals.hittype[i].picnum = sprite[s_ow].picnum;

            Globals.hittype[i].lastvx = 0;
            Globals.hittype[i].lastvy = 0;

            Globals.hittype[i].timetosleep = 0;
            Globals.hittype[i].actorstayput = -1;
            Globals.hittype[i].extra = -1;
            Globals.hittype[i].owner = s_ow;
            Globals.hittype[i].cgg = 0;
            Globals.hittype[i].movflag = 0;
            Globals.hittype[i].tempang = 0;
            Globals.hittype[i].dispicnum = 0;
            Globals.hittype[i].floorz = Globals.hittype[s_ow].floorz;
            Globals.hittype[i].ceilingz = Globals.hittype[s_ow].ceilingz;
            */
           // if (show2dsector[SECT>>3]&(1<<(SECT&7))) show2dsprite[i>>3] |= (1<<(i&7));
        //    else show2dsprite[i>>3] &= ~(1<<(i&7));
        /*
            if(s->sectnum < 0)
            {
                s->xrepeat = s->yrepeat = 0;
                changespritestat(i,5);
            }
        */
            return s;
        }

        public static spritetype SpawnActor(int x, int y, int z, short sectornum, short picnum)
        {
            int spritenum = Engine.board.insertsprite(sectornum, bMap.MAXSTATUS);
            spritetype sprite = Engine.board.sprite[spritenum];

            sprite.x = x;
            sprite.y = y;
            sprite.z = z;
            sprite.sectnum = sectornum;
            sprite.picnum = picnum;
            sprite.xrepeat = 47;
            sprite.yrepeat = 47;

            Gamescript.ActorScriptFunction sprfunc = Globals.script.GetFunctionForActor(sprite.picnum);

            Actor actor = new Actor();
            actor.SetAIScript(sprfunc);
            actor.Spawn(sprite);
            actor.ForceAwake();

            Gamescript.ActorScriptFunction spawnfunc = Globals.script.GetSpawnFunctionForActor(sprite.picnum);
            if (spawnfunc != null)
                spawnfunc.Invoke(actor);

            actors.Add(actor);
            return sprite;
        }

        //
        // KillActor
        //
        public static void KillActor(Actor actor)
        {
            actor.Destroy();
            actors.Remove(actor);
        }

        public static void newgame(int vn, int ln, int sk)
        {
            Game.DrawLoadingScreen();
        }

        //
        // Init
        //
        public void Init(ref Image canvasimage)
        {
            // Init the build engine.
            Engine.Init();

            // Load in the game data.
            Engine.initgroupfile("duke3d.grp");

            Engine.LoadTables();

            // Init the device
            Engine.setgamemode(0, 320, 200, 8, ref canvasimage);

            // Load in the tiles.
            Engine.loadpics("tiles000.art");

            // Load in the con scripts.
            InitScripts();

            lookupTable.Init();
            SoundSystem.SoundStartup();

            logoAnm.ANIM_LoadAnim("logo.anm");

            SoundSystem.loadsound(SoundId.PIPEBOMB_EXPLODE);
            playingAnm = logoAnm;
            gameState = GameState.GAMESTATE_STARTUP;
        }

        private void SetMaxFPS(int fps)
        {
            Application.Current.Host.Settings.MaxFrameRate = fps;
        }

        short inputloc = 0;
        public static short strget(short x, short y, string t, short dalen, short c)
        {
            short sc;
            Key ch;
            /*
            //while(KB_KeyWaiting())
            {
                sc = 0;
                ch = GameKeys.KB_GetLastScanCode();

                if (ch == 0)
                {

                    sc = KB_Getch();
                    if( sc == 104) return(1);

                    continue;
                }
                else
                {
                    if(ch == 8)
                    {
                        if( inputloc > 0 )
                        {
                            inputloc--;
                            *(t+inputloc) = 0;
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
                        else if ( ch >= 32 && inputloc < dalen && ch < 127)
                        {
                            ch = toupper(ch);
                            *(t+inputloc) = ch;
                            *(t+inputloc+1) = 0;
                            inputloc++;
                        }
                    }
                }
            }

            if( c == 999 ) return(0);
            if( c == 998 )
            {
                char b[41],ii;
                for(ii=0;ii<inputloc;ii++)
                    b[ii] = '*';
                b[ii] = 0;
                x = gametext(x,y,b,c,2+8+16);
            }
            else x = gametext(x,y,t,c,2+8+16);
            c = 4-(sintable[(totalclock<<4)&2047]>>11);
            rotatesprite((x+8)<<16,(y+4)<<16,32768L,0,SPINNINGNUKEICON+((totalclock>>3)%7),c,0,2+8,0,0,xdim-1,ydim-1);
            */
            return (0);
        }

        public static int gametext(int x, int y, string t, byte s, short dabits)
        {
            short ac,newx;
            bool centre;
            int tpos = 0;

            centre = ( x == (320>>1) );
            newx = 0;

            if(centre)
            {
                while(tpos < t.Length)
                {
                    if(t[tpos] == 32) 
                    {
                        newx+=5;
                        tpos++;
                        continue;
                    }
                    else ac = (short)(t[tpos] - '!' + Names.STARTALPHANUM);

                    if( ac < Names.STARTALPHANUM || ac > Names.ENDALPHANUM ) break;

                    if (t[tpos] >= '0' && t[tpos] <= '9')
                        newx += 8;
                    else newx += Engine.tilesizx[ac];
                    tpos++;
                }

                tpos = 0;
                x = (320>>1)-(newx>>1);
            }

            while(tpos < t.Length)
            {
                if (t[tpos] == 32) 
                { 
                    x += 5; 
                    tpos++; 
                    continue; 
                }
                else
                    ac = (short)(t[tpos] - '!' + Names.STARTALPHANUM);

                if( ac < Names.STARTALPHANUM || ac > Names.ENDALPHANUM )
                    break;

                Engine.rotatesprite(x<<16,y<<16,65536,0,ac,(sbyte)s,0,(byte)dabits,0,0,Engine._device.xdim-1,Engine._device.ydim-1);

                if(t[tpos] >= '0' && t[tpos] <= '9')
                    x += 8;
                else x += Engine.tilesizx[ac];

                tpos++;
            }

            return (x);
        }

        public static void digitalnumber(int x,int y,int n,sbyte s,byte cs)
        {
            short i, j, k, p, c;
            string b = "" + n;

            i = (short)b.Length;
            j = 0;

            for(k=0;k<i;k++)
            {
                p = (short)(Names.DIGITALNUM+(b[k])-'0');
                j += (short)(Engine.tilesizx[p]+1);
            }
            c = (short)(x-(j>>1));

            j = 0;
            for(k=0;k<i;k++)
            {
                p = (short)(Names.DIGITALNUM+(b[k])-'0');
                Engine.rotatesprite((c + j) << 16, y << 16, 65536, 0, p, s, 0, cs, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                j += (short)(Engine.tilesizx[p]+1);
            }
        }

        public static void weaponnum(short ind, int x, int y, int num1, int num2, byte ha)
        {
            string dabuf = "";
            unchecked
            {
                Engine.rotatesprite((x - 7) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + ind + 1), (sbyte)(ha - 10), 7, (byte)(10 + 128), 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                Engine.rotatesprite((x - 3) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + 10), (sbyte)ha, 0, (byte)(10 + 128), 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                Engine.rotatesprite((x + 9) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + 11), (sbyte)ha, 0, 10 + 128, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
            
                if(num1 > 99) num1 = 99;
                if(num2 > 99) num2 = 99;

                dabuf = "" + num1;
                if(num1 > 9)
                {
                    Engine.rotatesprite((x) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine._device.xdim - 1, Engine.ydim - 1);
                    Engine.rotatesprite((x + 4) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[1] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine._device.xdim - 1, Engine.ydim - 1);
                }
                else
                    Engine.rotatesprite((x + 4) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);

                dabuf = "" + num2;
                if(num2 > 9)
                {
                    Engine.rotatesprite((x + 13) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                    Engine.rotatesprite((x + 17) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[1] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                }
                else Engine.rotatesprite((x + 13) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            }
        }

        public static void weaponnum999(byte ind, int x, int y, int num1, int num2, byte ha)
        {
            string dabuf;

            Engine.rotatesprite((x - 7) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + ind + 1), (sbyte)(ha - 10), 7, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            Engine.rotatesprite((x - 4) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + 10), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            Engine.rotatesprite((x + 13) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + 11), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);

            dabuf = "" + num1;
            if(num1 > 99)
            {
                Engine.rotatesprite((x) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                Engine.rotatesprite((x + 4) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[1] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                Engine.rotatesprite((x + 8) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[2] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            }
            else if(num1 > 9)
            {
                Engine.rotatesprite((x + 4) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                Engine.rotatesprite((x + 8) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[1] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            }
            else Engine.rotatesprite((x + 8) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);

            dabuf = "" + num2;
            if(num2 > 99)
            {
                Engine.rotatesprite((x + 17) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                Engine.rotatesprite((x + 21) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[1] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                Engine.rotatesprite((x + 25) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[2] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            }
            else if(num2 > 9)
            {
                Engine.rotatesprite((x + 17) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                Engine.rotatesprite((x + 21) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[1] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            }
            else Engine.rotatesprite((x + 25) << 16, y << 16, 65536, 0, (short)(Names.THREEBYFIVE + dabuf[0] - '0'), (sbyte)ha, 0, 10 + 128, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
        }

        public static int gametextpal(int x, int y, string t, char s, char p)
        {
            short ac,newx;
            bool centre;
            int tpos = 0;

            centre = ( x == (320>>1) );
            newx = 0;

            if(centre)
            {
                while(tpos < t.Length)
                {
                    if(t[tpos] == 32) 
                    {
                        newx+=5;
                        tpos++;
                        continue;
                    }
                    else ac = (short)(t[tpos] - '!' + Names.STARTALPHANUM);

                    if( ac < Names.STARTALPHANUM || ac > Names.ENDALPHANUM ) break;

                    if (t[tpos] >= '0' && t[tpos] <= '9')
                        newx += 8;
                    else newx += Engine.tilesizx[ac];
                    tpos++;
                }

                tpos = 0;
                x = (320>>1)-(newx>>1);
            }

            while(tpos < t.Length)
            {
                if (t[tpos] == 32) 
                { 
                    x += 5; 
                    tpos++; 
                    continue; 
                }
                else
                    ac = (short)(t[tpos] - '!' + Names.STARTALPHANUM);

                if( ac < Names.STARTALPHANUM || ac > Names.ENDALPHANUM )
                    break;

                Engine.rotatesprite(x<<16,y<<16,65536,0,ac,(sbyte)s,(byte)p,(byte)(2+8+16),0,0,Engine._device.xdim-1,Engine._device.ydim-1);

                if(t[tpos] >= '0' && t[tpos] <= '9')
                    x += 8;
                else x += Engine.tilesizx[ac];

                tpos++;
            }

            return (x);
        }

        public static bool badguy(spritetype s)
        {

            switch (s.picnum)
            {
                case Names.SHARK:
                case Names.RECON:
                case Names.DRONE:
                case Names.LIZTROOPONTOILET:
                case Names.LIZTROOPJUSTSIT:
                case Names.LIZTROOPSTAYPUT:
                case Names.LIZTROOPSHOOT:
                case Names.LIZTROOPJETPACK:
                case Names.LIZTROOPDUCKING:
                case Names.LIZTROOPRUNNING:
                case Names.LIZTROOP:
                case Names.OCTABRAIN:
                case Names.COMMANDER:
                case Names.COMMANDERSTAYPUT:
                case Names.PIGCOP:
                case Names.EGG:
                case Names.PIGCOPSTAYPUT:
                case Names.PIGCOPDIVE:
                case Names.LIZMAN:
                case Names.LIZMANSPITTING:
                case Names.LIZMANFEEDING:
                case Names.LIZMANJUMP:
                case Names.ORGANTIC:
                case Names.BOSS1:
                case Names.BOSS2:
                case Names.BOSS3:
                case Names.BOSS4:
                case Names.GREENSLIME:
                case Names.GREENSLIME + 1:
                case Names.GREENSLIME + 2:
                case Names.GREENSLIME + 3:
                case Names.GREENSLIME + 4:
                case Names.GREENSLIME + 5:
                case Names.GREENSLIME + 6:
                case Names.GREENSLIME + 7:
                case Names.RAT:
                case Names.ROTATEGUN:
                    return true;
            }
            if (actortype[s.picnum]) return true;

            return false;
        }

        

        int frametest = 0;
        private void PlayAnm()
        {
            if (frametest >= playingAnm.ANIM_NumFrames())
            {
                playingAnm = null;
                return;
            }

            if (frametest == 0)
            {
                SoundSystem.sound(SoundId.FLY_BY);
            }

            playingAnm.ANIM_BlitFrame(frametest);
            frametest++;
        }

        //
        // Logo
        //
        private void Logo()
        {
            switch (gameState)
            {
                case GameState.GAMESTATE_STARTUP:
                    SetMaxFPS(60);
                    totalclock = 0;
                    SoundSystem.playmusic(Globals.script.Titlesong);
                    gameState++;
                    break;

                case GameState.GAMESTATE_DREALMS:
                    lookupTable.SetPalette(PaletteType.PALETTE_DREALMS);
                    Engine.rotatesprite(0, 0, 65536, 0, Names.DREALMS, 0, 0, 2 + 8 + 16 + 64, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                    if (totalclock > 310)
                        gameState++;
                    break;

                case GameState.GAMESTATE_INTRO:
                    totalclock = 0;
                    lookupTable.SetPalette(PaletteType.PALETTE_TITLEPAL);
                    gameState++;
                    break;
            }

            if (gameState >= GameState.GAMESTATE_INTRO_P0 && gameState <= GameState.GAMESTATE_INTRO_P4)
            {
                Engine.rotatesprite(0, 0, 65536, 0, Names.BETASCREEN, 0, 0, 2 + 8 + 16 + 64, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);

                if (totalclock > 120 && totalclock < (120 + 60))
                {
                    if (gameState == GameState.GAMESTATE_INTRO_P0)
                    {
                        gameState = GameState.GAMESTATE_INTRO_P1;
                        SoundSystem.sound(SoundId.PIPEBOMB_EXPLODE);
                    }
                    Engine.rotatesprite(160 << 16, 104 << 16, (totalclock - 120) << 10, 0, Names.DUKENUKEM, 0, 0, 2 + 8, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                }
                else if (totalclock >= (120 + 60))
                    Engine.rotatesprite(160 << 16, (104) << 16, 60 << 10, 0, Names.DUKENUKEM, 0, 0, 2 + 8, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);

                if (totalclock > 220 && totalclock < (220 + 30))
                {
                    if (gameState == GameState.GAMESTATE_INTRO_P1)
                    {
                        gameState = GameState.GAMESTATE_INTRO_P2;
                        SoundSystem.sound(SoundId.PIPEBOMB_EXPLODE);
                    }

                    Engine.rotatesprite(160 << 16, (104) << 16, 60 << 10, 0, Names.DUKENUKEM, 0, 0, 2 + 8, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                    Engine.rotatesprite(160 << 16, (129) << 16, (totalclock - 220) << 11, 0, Names.THREEDEE, 0, 0, 2 + 8, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                }
                else if (totalclock >= (220 + 30))
                    Engine.rotatesprite(160 << 16, (129) << 16, 30 << 11, 0, Names.THREEDEE, 0, 0, 2 + 8, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);

                if (totalclock >= 280 && totalclock < 395)
                {
                    Engine.rotatesprite(160 << 16, (151) << 16, (410 - totalclock) << 12, 0, Names.PLUTOPAKSPRITE + 1, 0, 0, 2 + 8, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                    if (gameState == GameState.GAMESTATE_INTRO_P2)
                    {
                        gameState = GameState.GAMESTATE_INTRO_P3;
                        SoundSystem.sound(SoundId.FLY_BY);
                    }
                }
                else if (totalclock >= 395 && totalclock < 585)
                {
                    if (gameState == GameState.GAMESTATE_INTRO_P3)
                    {
                        gameState = GameState.GAMESTATE_INTRO_P4;
                        SoundSystem.sound(SoundId.PIPEBOMB_EXPLODE);
                    }
                    Engine.rotatesprite(160 << 16, (151) << 16, 30 << 11, 0, Names.PLUTOPAKSPRITE + 1, 0, 0, 2 + 8, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                }
                else if (totalclock >= 585)
                {
                    gameState = GameState.GAMESTATE_DISCONNECTED;
                }

                if (keysdown[(int)Key.Enter] || keysdown[(int)Key.Escape] || keysdown[(int)Key.Space])
                {
                    gameState = GameState.GAMESTATE_DISCONNECTED;
                    GameKeys.KB_FlushKeyboardQueue();
                }
            }
            else
            {
                if (keysdown[(int)Key.Enter] || keysdown[(int)Key.Escape] || keysdown[(int)Key.Space])
                {
                    GameKeys.KB_FlushKeyboardQueue();

                    gameState++;
                }
            }

            if (gameState == GameState.GAMESTATE_DISCONNECTED)
            {
                menus.SetMenu(MenuScreen.MENUSCREEN_MAINMENU);
                lookupTable.SetPalette(PaletteType.PALETTE_DEFAULTPAL);
            }
        }

        //
        // SetKeyDown
        //
        public void SetKeyDown(Key key)
        {
            keysdown[(int)key] = true;
        }

        //
        // SetKeyUp
        //
        public void SetKeyUp(Key key)
        {
            keysdown[(int)key] = false;
        }

        //
        // GameFrame
        //
        private void GameFrame()
        {
            if (GameKeys.KB_KeyPressed(Key.Escape) && gameState == GameState.GAMESTATE_INGAME)
            {
                gameState = GameState.GAMESTATE_INGAMEMENU;
                menus.SetMenu(MenuScreen.MENUSCREEN_MAINMENUINGAME);
                SoundSystem.intomenusounds();
                GameKeys.KB_FlushKeyboardQueue();
            }

            // Run all the actor frame.
            if (Game.State != GameState.GAMESTATE_INGAMEMENU)
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    actors[i].Frame();
                }
            }

            // Run the player frame.
            Globals.ps[0].Frame();

            // If we are in the menu draw it.
            if (Game.State == GameState.GAMESTATE_INGAMEMENU)
            {
                menus.DrawMenus();
            }
        }

        private static void DrawLoadingScreen()
        {
            if (Globals.ud.recstat != 2)
            {
                //for (j = 0; j < 63; j += 7) palto(0, 0, 0, j);
                //i = ud.screen_size;
                Globals.ud.screen_size = 0;
                Engine.clearview();

                Engine.rotatesprite(320 << 15, 200 << 15, 65536, 0, Names.LOADSCREEN, 0, 0, 2 + 8 + 64, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);

                menus.menutext(160, 90, 0, 0, "ENTERING");
                menus.menutext(160, 90 + 16 + 8, 0, 0, Globals.script.volumes[Globals.ud.volume_number][Globals.ud.level_number].mapname);
            }
        }

#if WINDOWS_PHONE
        private bool _refreshpage = false;
#endif
        //
        // Frame
        //
        public void Frame()
        {
            if (playingAnm != null)
            {
                SetMaxFPS(24);

                PlayAnm();
                Engine.NextPage();

                if (keysdown[(int)Key.Enter] || keysdown[(int)Key.Escape] || keysdown[(int)Key.Space])
                {
                    playingAnm = null;
                    GameKeys.KB_FlushKeyboardQueue();
                }
                return;
            }

            if (gameState >= GameState.GAMESTATE_STARTUP && gameState <= GameState.GAMESTATE_INTRO_P4)
            {
                Logo();
            }
            else if (gameState == GameState.GAMESTATE_DISCONNECTED || gameState == GameState.GAMESTATE_DISCONNECTEDDEMO)
            {
                menus.DrawMenus();
            }
            else if (gameState == GameState.GAMESTATE_LOADING)
            {
                if (SoundSystem.SoundsActive == false)
                {
                    SoundSystem.stopmusic();
                    Game.newgame(Globals.ud.m_volume_number, Globals.ud.m_level_number, Globals.ud.m_player_skill);
                    Game.enterlevel(GameState.GAMESTATE_INGAME);
                    gameState = GameState.GAMESTATE_INGAME;

#if WINDOWS_PHONE
                    _refreshpage = true;
#endif
                }
            }
            else if (gameState == GameState.GAMESTATE_INGAME || gameState == GameState.GAMESTATE_INGAMEMENU)
            {
                GameFrame();
            }
            
#if WINDOWS_PHONE
            if(_refreshpage)
                Engine.NextPage();

            _refreshpage = !_refreshpage;

            totalclock += 6;
#else
            Engine.NextPage();
            totalclock += 2;
#endif
        }
    }
}
