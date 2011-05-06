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

namespace duke3d.game
{
    //
    // Game
    //
    public class Game
    {
        private Anim logoAnm = new Anim();
        public const int MAXPLAYERS = 16;

        public static bool[] actortype = new bool[bMap.MAXTILES];
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
            Engine.setgamemode(0, 640, 480, 8, ref canvasimage);

            // Load in the tiles.
            Engine.loadpics("tiles000.art");

            logoAnm.ANIM_LoadAnim("logo.anm");
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

        //
        // Frame
        //
        int frametest=0;
        public void Frame()
        {
            
            if (frametest >= logoAnm.ANIM_NumFrames())
            {
                frametest = 0;
            }
            
            logoAnm.ANIM_BlitFrame(frametest);
            frametest++;
            Engine.NextPage();
        }
    }
}
