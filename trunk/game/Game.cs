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

namespace game
{
    public class Game
    {
        Player player = null;

        //
        // Init
        //
        public void Init(ref Image canvasimage)
        {
            // Init the build engine.
            Engine.Init();

            // Load in the game data.
            Engine.initgroupfile("data.grp");

            Engine.LoadTables();

            // Init the device
            Engine.setgamemode(0, 640, 480, 8, ref canvasimage);

            // Load in the tiles.
            Engine.loadpics("tiles000.art");
        }

        public void HandleLocalPlayerInput(int fvel, int svel, int angvel)
        {
            if (player != null)
            {
                player.ProcessInput(fvel, svel, angvel);
            }
        }

        public void HandleLocalPlayerInputJump()
        {
            if (player != null)
            {
                player.Jump();
            }
        }

        //
        // SpawnGame
        //
        public void SpawnGame(string mapwapfilepath, System.IO.Stream usermap)
        {
            int posx = 0, posy = 0, posz = 0;
            short sectornum = 0, ang = 0;

            if (usermap != null)
            {
                kFile fil = new kFile(usermap);
                Engine.newboard();
                Engine.board.loadboard(fil, ref posx, ref posy, ref posz, ref ang, ref sectornum);
            }
            else
            {
                Engine.loadboard(mapwapfilepath, ref posx, ref posy, ref posz, ref ang, ref sectornum);
            }

            player = new Player();
            player.Spawn(posx, posy, posz, sectornum, ang);
        }

        public void Frame()
        {
            player.Think();
        }
    }
}
