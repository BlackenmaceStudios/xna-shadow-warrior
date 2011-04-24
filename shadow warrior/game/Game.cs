using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using build;
using System.Windows.Controls;
using mact;

namespace sw
{
    class Game
    {
        MediaElement _music;
        private enum gamestate_t
        {
            GAMESTATE_LOGO = 0,
            GAMESTATE_INTRO,
            GAMESTATE_DISCONNECTED,
            GAMESTATE_LOADING,
            GAMESTATE_INGAME
        }
        
        bSoundManager soundManager = new bSoundManager();

        private List<Actor> _actors = new List<Actor>();
        private Menus menu = new Menus();

        private const short STAT_SCREEN_PIC = 5114;
        private const short TITLE_PIC = 2324;
        private const short THREED_REALMS_PIC = 2325;

        private readonly int TITLE_ROT_FLAGS = (Flags.ROTATE_SPRITE_CORNER | Flags.ROTATE_SPRITE_SCREEN_CLIP | Flags.ROTATE_SPRITE_NON_MASK);

      //  private Palette threedrealmspal = new Palette();
        private gamestate_t gamestate = gamestate_t.GAMESTATE_LOGO;

        private Player localplayer;

        private int nextlevelnum = -1;
        public static int totalclock = 0;

        struct LEVEL_INFO
        {
            public string LevelName;
            public string SongName;
            public string Description;
            public string BestTime;
            public string ParTime;

            public LEVEL_INFO(string levelname, string songname, string description, string besttime, string partime)
            {
                LevelName = levelname;
                SongName = songname;
                Description = description;
                BestTime = besttime;
                ParTime = partime;
            }
        }

        LEVEL_INFO[] LevelInfo = new LEVEL_INFO[]
        {
            new LEVEL_INFO("title.map",      "theme.mid", " ", " ", " "  ),
            new LEVEL_INFO("$bullet.map",    "e1l01.mid", "Seppuku Station", "0 : 55", "5 : 00"  ),
            new LEVEL_INFO("$dozer.map",     "e1l03.mid", "Zilla Construction", "4 : 59", "8 : 00"  ),
            new LEVEL_INFO("$shrine.map",    "e1l02.mid", "Master Leep's Temple", "3 : 16", "10 : 00"  ),
            new LEVEL_INFO("$woods.map",     "e1l04.mid", "Dark Woods of the Serpent", "7 : 06", "16 : 00"  ),
            new LEVEL_INFO("$whirl.map",     "yokoha03.mid", "Rising Son", "5 : 30", "10 : 00"   ),
            new LEVEL_INFO("$tank.map",      "nippon34.mid", "Killing Fields", "1 : 46", "4 : 00"   ),
            new LEVEL_INFO("$boat.map",      "execut11.mid", "Hara-Kiri Harbor", "1 : 56", "4 : 00"   ),
            new LEVEL_INFO("$garden.map",    "execut11.mid", "Zilla's Villa", "1 : 06", "2 : 00"   ),
            new LEVEL_INFO("$outpost.map",   "sanai.mid",    "Monastery", "1 : 23", "3 : 00"      ),
            new LEVEL_INFO("$hidtemp.map",   "kotec2.mid",   "Raider of the Lost Wang", "2 : 05", "4 : 10"     ),
            new LEVEL_INFO("$plax1.map",     "kotec2.mid",   "Sumo Sky Palace", "6 : 32", "12 : 00"     ),
            new LEVEL_INFO("$bath.map",      "yokoha03.mid", "Bath House", "10 : 00", "10 : 00"   ),
            new LEVEL_INFO("$airport.map",   "nippon34.mid", "Unfriendly Skies", "2 : 59", "6 : 00"   ),
            new LEVEL_INFO("$refiner.map",   "kotoki12.mid", "Crude Oil", "2 : 40", "5 : 00"   ),
            new LEVEL_INFO("$newmine.map",   "hoshia02.mid", "Coolie Mines", "2 : 48", "6 : 00"   ),
            new LEVEL_INFO("$subbase.map",   "hoshia02.mid", "Subpen 7", "2 : 02", "4 : 00"   ),
            new LEVEL_INFO("$rock.map",      "kotoki12.mid", "The Great Escape", "3 : 18", "6 : 00"   ),
            new LEVEL_INFO("$yamato.map",    "sanai.mid",    "Floating Fortress", "11 : 38", "20 : 00"      ),
            new LEVEL_INFO("$seabase.map",   "kotec2.mid",   "Water Torture", "5 : 07", "10 : 00"     ),
            new LEVEL_INFO("$volcano.map",   "kotec2.mid",   "Stone Rain", "9 : 15", "20 : 00"     ),
            new LEVEL_INFO("$shore.map",     "kotec2.mid",   "Shanghai Shipwreck", "3 : 58", "8 : 00"     ),
            new LEVEL_INFO("$auto.map",      "kotec2.mid",   "Auto Maul", "4 : 07", "8 : 00"     ),
            new LEVEL_INFO("tank.map",       "kotec2.mid",   "Heavy Metal (DM only)", "10 : 00", "10 : 00"     ),
            new LEVEL_INFO("$dmwoods.map",   "kotec2.mid",   "Ripper Valley (DM only)", "10 : 00", "10 : 00"     ),
            new LEVEL_INFO("$dmshrin.map",   "kotec2.mid",   "House of Wang (DM only)", "10 : 00", "10 : 00"     ),
            new LEVEL_INFO("$rush.map",      "kotec2.mid",   "Lo Wang Rally (DM only)", "10 : 00", "10 : 00"     ),
            new LEVEL_INFO("shotgun.map",    "kotec2.mid",   "Ruins of the Ronin (CTF)", "10 : 00", "10 : 00"     ),
            new LEVEL_INFO("$dmdrop.map",    "kotec2.mid",   "Killing Fields (CTF)", "10 : 00", "10 : 00"     )
        };

        void DrawLoadLevelScreen()
        {
            Engine.rotatesprite(0, 0, Flags.RS_SCALE, 0, TITLE_PIC, 20, 0, (byte)TITLE_ROT_FLAGS, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
        }

        public void HandleLocalPlayerInputFire()
        {
            localplayer.FireWeapon();
        }

        public void HandleLocalPlayerInputJump()
        {
            localplayer.Jump();
        }

        public void HandleLocalPlayerInput(int fvel, int svel, int angvel)
        {
            if (localplayer != null)
            {
                localplayer.ProcessInput(fvel, svel, angvel);
            }
        }

        private void LoadingLevelScreen(bool DemoMode, string level_name)
        {
            short w= 0,h= 0;
            string ds;
            
            DrawLoadLevelScreen();
    
            if (DemoMode)
                ds = "DEMO";
            else    
                ds = "ENTERING";
        
            menu.MNU_MeasureString(ds, ref w, ref h);
            menu.MNU_DrawString((short)menu.TEXT_TEST_COL(w), 170, ds, 1, 16);

            ds = level_name;

            menu.MNU_MeasureString(ds, ref w,ref h);
            menu.MNU_DrawString((short)menu.TEXT_TEST_COL(w), 180, ds, 1, 16);
        }

        //
        // PlayMusic
        //
        private void PlayMusic(int tracknum)
        {
            _music.Stop();
            _music.Source = new Uri("base/music/track" + (tracknum + 2) + ".mp3", UriKind.RelativeOrAbsolute);
            _music.Position = TimeSpan.Zero;
            _music.Play();
        }

        //
        // AlphaMessage
        //
        public bool AlphaMessage()
        {
            /*
            Engine.settextcolor(Color.White);
            Engine.setbkcolor(Color.DarkGreen);

            Engine.Printf("                     SHADOW WARRIOR(tm) BETA Version 1.2                     \n");
            Engine.Printf("                  Copyright (c) 1997 3D Realms Entertainment                 \n");

            Engine.settextcolor(Color.White);
            Engine.setbkcolor(Color.Black);
            Engine.Printf("\"BuildEngine\" and Port (c) 2011 JV Software\n");
            Engine.Printf("Build " + GetBuildTime() + " For Testing Purposes Only");

            Engine.Printf("\n");
            Engine.Printf("\n");
            Engine.Printf("\n");

            Engine.Printf("       This is a BETA version of Shadow Warrior(tm).  DO NOT DISTRIBUTE!    \n\n");
            Engine.Printf("                     This is a in house copy.\n\n");

            Engine.Printf("                   Tap the screen to continue. \n");

            Engine.Printf("\n");
            Engine.Printf("\n");
            Engine.Printf("\n");
            */
            return true;
        }

        //
        // Init
        //
        public void Init(ref Image canvasimage, MediaElement music)
        {
            _music = music;
            // Init the build engine.
            Engine.Init();

            // Load in the game data.
            Engine.initgroupfile("sw.grp");

            Engine.LoadTables();

            // Init the device
            Engine.setgamemode(0, 320, 200, 8, ref canvasimage);

            // Create the colormaps.
            ColorMapManager.InitPalette();

            // Load in the tiles.
            Engine.loadpics("tiles000.art");

            // Load in the voxel overrides.
            Engine.Printf("Loading Voxels...");
            VoxelManager.LoadVoxels();

            // Hide the console.
            //Engine.HideConsole();

            // Play the intro music
           // PlayMusic(0);

            //threedrealmspal.InitFromPalette("3drealms.pal");
        }

        //
        // LogoAnim
        //
        private void LogoAnim()
        {
            if (gamestate == gamestate_t.GAMESTATE_LOGO)
            {
              //  threedrealmspal.SetActive();
                Engine.rotatesprite(0, 0, Flags.RS_SCALE, 0, THREED_REALMS_PIC, 0, 0, (byte)TITLE_ROT_FLAGS, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            }
            else if (gamestate == gamestate_t.GAMESTATE_INTRO)
            {
                Engine.rotatesprite(0, 0, Flags.RS_SCALE, 0, TITLE_PIC, 0, 0, (byte)TITLE_ROT_FLAGS, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
            }
        }

        //
        // StartGame
        //
        private void StartGame(int mapnum)
        {
            LoadingLevelScreen(false, LevelInfo[mapnum].Description);

            nextlevelnum = mapnum;
            gamestate = gamestate_t.GAMESTATE_LOADING;
        }

        //
        // SpawnActors
        //
        private void SpawnActors()
        {
            _actors.Clear();

            foreach (spritetype sprite in Engine.board.sprite)
            {
                if (sprite == null)
                    continue;

                int picnum = sprite.picnum;

                Actor actor = null;
// jv - hack for mirrors
                if (picnum == 5058)
                {
                    sprite.cstat = MyTypes.SET(sprite.cstat, Flags.CSTAT_SPRITE_INVISIBLE);
                }
                if ((picnum >= Names2.ST1 && picnum <= Names2.ST_QUICK_DEFEND) || picnum == Names2.TRACK_SPRITE || picnum == 1901)
                    actor = new ST1(sprite);

                if (picnum == 4096 || picnum == 4162)
                    actor = new EvilNinja(sprite);

                /*
                switch (picnum)
                {
                    case Names2.ST1:
                        actor = new ST1(sprite);
                        break;

                    case Names2.ST1:
                        actor = new ST1(sprite);
                        break;
                }
                */
                if (actor != null)
                {
                    sprite.obj = actor;
                    actor.Spawn();
                    _actors.Add(actor);
                }
            }
        }

        //
        // EnterGame
        //
        private void EnterGame(int mapnum)
        {
            int daposx = 0, daposy = 0, daposz = 0;
            short daang = 0, dacursectnum = 0;

            localplayer = new Player();

            Engine.loadboard(LevelInfo[mapnum].LevelName, ref daposx, ref daposy, ref daposz, ref daang, ref dacursectnum);
            localplayer.UpdatePosition(daposx, daposy, daposz, daang, dacursectnum);
            localplayer.Spawn();
            localplayer.Precache(soundManager);

            SpawnActors();

            Mirrors.SetupMirrors();

            PlayMusic(2);

            gamestate = gamestate_t.GAMESTATE_INGAME;
        }

        //
        // GameFrame
        //
        private void GameFrame()
        {
            localplayer.Think();

            foreach (Actor actor in _actors)
            {
                actor.Think();
            }
        }

        //
        // Frame
        //
        public void Frame()
        {
            switch (gamestate)
            {
                case gamestate_t.GAMESTATE_LOGO:
                    StartGame(1);
                    Engine.NextPage();
                    //LogoAnim();
                    break;
                case gamestate_t.GAMESTATE_LOADING:
                    EnterGame(nextlevelnum);
                    Engine.NextPage();
                    break;
                case gamestate_t.GAMESTATE_INTRO:
                    LogoAnim();
                    Engine.NextPage();
                    break;
                case gamestate_t.GAMESTATE_INGAME:
                    GameFrame();
                    break;
            }

            totalclock++;
        }

        public void printcentertext(string str)
        {
            printext((int)((Engine.xdim >> 1) - (str.Length << 2)), Engine.ydim - 24, str, 73);
        }

        public void printext(int x, int y, string buffer, short tilenum)
        {
	        int i;
	        char ch;

	        for(i=0;i < buffer.Length;i++)
	        {
		        ch = buffer[(int)i];
                Engine.rotatesprite((x - ((8 & 15) << 3)) << 16, (y - ((8 >> 4) << 3)) << 16, 65536, 0, tilenum, 0, 0, 8 + 16 + 64 + 128, x, y, x + 7, y + 7);
                Engine.rotatesprite((x - ((ch & 15) << 3)) << 16, (y - ((ch >> 4) << 3)) << 16, 65536, 0, tilenum, 0, 0, 8 + 16 + 128, x, y, x + 7, y + 7);
		        x += 8;
	        }
        }
    }
}
