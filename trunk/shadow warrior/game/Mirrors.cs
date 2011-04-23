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

namespace sw
{
    public static class Mirrors
    {
        // HACK: This is not how SW does mirrors fixme
        private const int MIRROR = 340;
        private const int MIRRORLABEL = 1320;

        private const int MAXMIRRORS = 64;
        private static short[] mirrorwall = new short[MAXMIRRORS];
        private static short[] mirrorsector = new short[MAXMIRRORS];
        private static short mirrorcnt;
        private static short[] floormirrorsector = new short[64];
        private static short floormirrorcnt;

        //
        // SetupMirrors
        //
        public static void SetupMirrors()
        {
            return;

            mirrorcnt = 0;
            Engine.tilesizx[MIRROR] = 0;
            Engine.tilesizy[MIRROR] = 0;

            for( int i = 0; i < Engine.board.wall.Length; i++ )
            {
                walltype wall = Engine.board.wall[i];

                if (wall == null)
                    continue;

                if (wall.nextsector >= 0 && wall.overpicnum == MIRROR && (wall.cstat & 32) != 0)
                {
                    if ((Engine.board.sector[wall.nextsector].floorstat & 1) == 0)
                    {
                        Engine.board.wall[i].overpicnum = (short)(MIRRORLABEL + mirrorcnt);
                        Engine.board.sector[wall.nextsector].ceilingpicnum = (short)(MIRRORLABEL + mirrorcnt);
                        Engine.board.sector[wall.nextsector].floorpicnum = (short)(MIRRORLABEL + mirrorcnt);
                        Engine.board.sector[wall.nextsector].floorstat |= 1;
                        //  Engine.board.wall[i].picnum = Engine.board.wall[i].overpicnum;
                        mirrorwall[mirrorcnt] = (short)i;
                        mirrorsector[mirrorcnt] = wall.nextsector;
                        mirrorcnt++;
                    }
                    else
                    {
                        Engine.board.wall[i].overpicnum = Engine.board.sector[wall.nextsector].ceilingpicnum;
                    }
                }
            }

            //Invalidate textures in sector behind mirror
            for (int i = 0; i < mirrorcnt; i++)
            {
                int k = mirrorsector[i];
                int startwall = Engine.board.sector[k].wallptr;
                int endwall = startwall + Engine.board.sector[k].wallnum;
                for (int j = startwall; j < endwall; j++)
                {
                    Engine.board.wall[j].picnum = MIRROR;
                    Engine.board.wall[j].overpicnum = MIRROR;
                }
            }
        }

        public static bool IsMirrorVisible()
        {
            for (int i = 0; i < mirrorcnt; i++)
            {
                if (Engine.board.gotpic[MIRRORLABEL + i] == false)
                    continue;

                return true;
            }

            return false;
        }

        public static void MirrorsThink(int cposx, int cposy, int cposz, short cang, int choriz)
        {
            int tposx = 0, tposy = 0;
            short tang = 0;
            int oldvisibility = Engine.board.visibility;

            Engine.board.visibility = (oldvisibility >> 1) + (oldvisibility >> 2);

            for (int i = 0; i < mirrorcnt; i++)
            {
                if (Engine.board.gotpic[MIRRORLABEL + i] == false)
                    continue;

                Engine.board.preparemirror(cposx, cposy, cposz, cang, choriz,
                             mirrorwall[i], mirrorsector[i], ref tposx, ref tposy, ref tang);

                Engine.board.drawrooms(tposx, tposy, cposz, tang, choriz, (short)(mirrorsector[i] | bMap.MAXSECTORS));

                Engine.board.drawmasks();

                Engine.board.completemirror();

                Engine.board.gotpic[MIRRORLABEL + i] = false;
            }

            Engine.board.visibility = oldvisibility;
        }
    }
}
