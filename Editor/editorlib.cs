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

namespace Editor
{
    public static class EditorLib
    {
        public static void updatenumsprites()
        {
	        long i;

	        Engine.board.numsprites = 0;
	        for(i=0;i<bMap.MAXSPRITES;i++)
                if (Engine.board.sprite[i] != null && Engine.board.sprite[i].statnum < bMap.MAXSTATUS)
                    Engine.board.numsprites++;
        }

        public static void fixrepeats(short i)
        {
            int dax, day, dist;

            dax = Engine.board.wall[Engine.board.wall[i].point2].x - Engine.board.wall[i].x;
            day = Engine.board.wall[Engine.board.wall[i].point2].y - Engine.board.wall[i].y;
            dist = pragmas.ksqrt(dax * dax + day * day);
            dax = Engine.board.wall[i].xrepeat; day = Engine.board.wall[i].yrepeat;
            Engine.board.wall[i].xrepeat = (byte)Math.Min(Math.Max(pragmas.mulscale10(dist, day), 1), 255);
        }

        public static int whitelinescan(short dalinehighlight)
        {
            int i, j, k;
            short sucksect, newnumwalls;

            sucksect = (short)Engine.board.sectorofwall(dalinehighlight);

            //memcpy(&sector[numsectors], &sector[sucksect], sizeof(sectortype));
            Engine.board.sector[sucksect].copyto(ref Engine.board.sector[Engine.board.numsectors]);
            Engine.board.sector[Engine.board.numsectors].wallptr = (short)Engine.board.numwalls;
            Engine.board.sector[Engine.board.numsectors].wallnum = 0;
            i = dalinehighlight;
            newnumwalls = (short)Engine.board.numwalls;
            do
            {
                j = Engine.board.lastwall((short)i);
                if (Engine.board.wall[j].nextwall >= 0)
                {
                    j = Engine.board.wall[j].point2;
                    for (k = 0; k < Engine.board.numwalls; k++)
                    {
                        if (Engine.board.wall[Engine.board.wall[k].point2].x == Engine.board.wall[j].x)
                            if (Engine.board.wall[Engine.board.wall[k].point2].y == Engine.board.wall[j].y)
                                if (Engine.board.wall[k].nextwall == -1)
                                {
                                    j = k;
                                    break;
                                }
                    }
                }

                //memcpy(&wall[newnumwalls], &wall[i], sizeof(walltype));

                Engine.board.wall[i].copyto(ref Engine.board.wall[newnumwalls]);

                Engine.board.wall[newnumwalls].nextwall = (short)j;
                Engine.board.wall[newnumwalls].nextsector = (short)Engine.board.sectorofwall((short)j);

                newnumwalls++;
                Engine.board.sector[Engine.board.numsectors].wallnum++;

                i = j;
            }
            while (i != dalinehighlight);

            for (i = Engine.board.numwalls; i < newnumwalls - 1; i++)
                Engine.board.wall[i].point2 = (short)(i + 1);
            Engine.board.wall[newnumwalls - 1].point2 = (short)Engine.board.numwalls;

            if (Engine.board.clockdir((short)Engine.board.numwalls) == 1)
                return (-1);
            else
                return (newnumwalls);
        }

        public static void insertpoint(short linehighlight, int dax, int day)
        {
	        short sucksect;
            int i, j, k;

	        j = linehighlight;
	        sucksect = (short)Engine.board.sectorofwall((short)j);

            Engine.board.sector[sucksect].wallnum++;
            for (i = sucksect + 1; i < Engine.board.numsectors; i++)
                Engine.board.sector[i].wallptr++;

	        movewalls((int)j+1,+1);
	       // memcpy(&wall[j+1],&wall[j],sizeof(walltype));
            Engine.board.wall[j].copyto(ref Engine.board.wall[j + 1]);

            Engine.board.wall[j].point2 = (short)(j + 1);
            Engine.board.wall[j + 1].x = dax;
            Engine.board.wall[j + 1].y = day;
	        fixrepeats((short)j);
	        fixrepeats((short)(j+1));

            if (Engine.board.wall[j].nextwall >= 0)
	        {
                k = Engine.board.wall[j].nextwall;

                sucksect = (short)Engine.board.sectorofwall((short)k);

                Engine.board.sector[sucksect].wallnum++;
                for (i = sucksect + 1; i < Engine.board.numsectors; i++)
                    Engine.board.sector[i].wallptr++;

		        movewalls(k+1,+1);
		        //memcpy(&wall[k+1],&wall[k],sizeof(walltype));

                Engine.board.wall[k].point2 = (short)(k + 1);
                Engine.board.wall[k + 1].x = dax;
                Engine.board.wall[k + 1].y = day;
		        fixrepeats((short)k);
		        fixrepeats((short)(k+1));

                j = Engine.board.wall[k].nextwall;
                Engine.board.wall[j].nextwall = (short)(k + 1);
                Engine.board.wall[j + 1].nextwall = (short)k;
                Engine.board.wall[k].nextwall = (short)(j + 1);
                Engine.board.wall[k + 1].nextwall = (short)j;
	        }
        }


        public static void getclosestpointonwall(int x, int y, int dawall, ref int nx, ref int ny)
        {
	        walltype wal;
	        int i, j, dx, dy;

	        wal = Engine.board.wall[dawall];
            dx = Engine.board.wall[wal.point2].x - wal.x;
            dy = Engine.board.wall[wal.point2].y - wal.y;
	        i = dx*(x-wal.x) + dy*(y-wal.y);
	        if (i <= 0) { nx = wal.x; ny = wal.y; return; }
	        j = dx*dx+dy*dy;
	        if (i >= j) { nx = wal.x+dx; ny = wal.y+dy; return; }
	        i = pragmas.divscale30(i,j);
            nx = wal.x + pragmas.mulscale30(dx, i);
            ny = wal.y + pragmas.mulscale30(dy, i);
        }

        public static int getlinehighlight(int xplc, int yplc)
        {
	        int i, dst, dist, closest, x1, y1, x2, y2, nx = 0, ny = 0;

	        if (Engine.board.numwalls == 0)
		        return(-1);
	        dist = 0x7fffffff;
	        closest = Engine.board.numwalls-1;
	        for(i=0;i<Engine.board.numwalls;i++)
	        {
		        getclosestpointonwall(xplc,yplc,i,ref nx,ref ny);
		        dst = pragmas.klabs(xplc-nx)+pragmas.klabs(yplc-ny);
		        if (dst <= dist)
                {
                    dist = dst; closest = i;
                }
	        }

	        if (Engine.board.wall[closest].nextwall >= 0)
	        {    //if red line, allow highlighting of both sides
		        x1 = Engine.board.wall[closest].x;
		        y1 = Engine.board.wall[closest].y;
		        x2 = Engine.board.wall[Engine.board.wall[closest].point2].x;
		        y2 = Engine.board.wall[Engine.board.wall[closest].point2].y;
		        if (pragmas.dmulscale32(xplc-x1,y2-y1,-(x2-x1),yplc-y1) >= 0)
			        closest = Engine.board.wall[closest].nextwall;
	        }

	        return(closest);
        }


        public static int movewalls(int start, int offs)
        {
	        int i;

	        if (offs < 0)  //Delete
	        {
                for (i = start; i < Engine.board.numwalls + offs; i++)
                {
                 //   memcpy(&wall[i], &wall[i - offs], sizeof(walltype));
                    Engine.board.wall[i - offs].copyto(ref Engine.board.wall[i]);
                }
	        }
	        else if (offs > 0)  //Insert
	        {
                for (i = Engine.board.numwalls + offs - 1; i >= start + offs; i--)
                {
                    //memcpy(&wall[i], &wall[i - offs], sizeof(walltype));
                    Engine.board.wall[i - offs].copyto(ref Engine.board.wall[i]);
                }
	        }
            Engine.board.numwalls += offs;
            for (i = 0; i < Engine.board.numwalls; i++)
	        {
                if (Engine.board.wall[i] == null) break;
                if (Engine.board.wall[i].nextwall >= start) Engine.board.wall[i].nextwall += (short)offs;
                if (Engine.board.wall[i].point2 >= start) Engine.board.wall[i].point2 += (short)offs;
	        }
	        return(0);
        }

        
        public static void flipwalls(short numwalls, short newnumwalls)
        {
	        int i, j, nume, templong;

	        nume = newnumwalls-numwalls;

	        for(i=numwalls;i<numwalls+(nume>>1);i++)
	        {
		        j = numwalls+newnumwalls-i-1;
                templong = Engine.board.wall[i].x; Engine.board.wall[i].x = Engine.board.wall[j].x; Engine.board.wall[j].x = templong;
                templong = Engine.board.wall[i].y; Engine.board.wall[i].y = Engine.board.wall[j].y; Engine.board.wall[j].y = templong;
	        }
        }

        
        public static void deletepoint(short point)
        {
	        long i, j, k, sucksect;

	        sucksect = Engine.board.sectorofwall(point);

            Engine.board.sector[sucksect].wallnum--;
            for (i = sucksect + 1; i < Engine.board.numsectors; i++)
                Engine.board.sector[i].wallptr--;

            j = Engine.board.lastwall(point);
            k = Engine.board.wall[point].point2;
            Engine.board.wall[j].point2 = (short)k;

            if (Engine.board.wall[j].nextwall >= 0)
	        {
                Engine.board.wall[Engine.board.wall[j].nextwall].nextwall = -1;
                Engine.board.wall[Engine.board.wall[j].nextwall].nextsector = -1;
	        }
            if (Engine.board.wall[point].nextwall >= 0)
	        {
                Engine.board.wall[Engine.board.wall[point].nextwall].nextwall = -1;
                Engine.board.wall[Engine.board.wall[point].nextwall].nextsector = -1;
	        }
	        movewalls(point,-1);

	        checksectorpointer((short)j,(short)sucksect);
        }

        public static int checksectorpointer(short i, short sectnum)
        {
            int j, k, startwall, endwall, x1, y1, x2, y2;

            x1 = Engine.board.wall[i].x;
            y1 = Engine.board.wall[i].y;
            x2 = Engine.board.wall[Engine.board.wall[i].point2].x;
            y2 = Engine.board.wall[Engine.board.wall[i].point2].y;

            if (Engine.board.wall[i].nextwall >= 0)          //Check for early exit
            {
                k = Engine.board.wall[i].nextwall;
                if ((Engine.board.wall[k].x == x2) && (Engine.board.wall[k].y == y2))
                    if ((Engine.board.wall[Engine.board.wall[k].point2].x == x1) && (Engine.board.wall[Engine.board.wall[k].point2].y == y1))
                        return (0);
            }

            Engine.board.wall[i].nextsector = -1;
            Engine.board.wall[i].nextwall = -1;
            for (j = 0; j < Engine.board.numsectors; j++)
            {
                startwall = Engine.board.sector[j].wallptr;
                endwall = startwall + Engine.board.sector[j].wallnum - 1;
                for (k = startwall; k <= endwall; k++)
                {
                    if ((Engine.board.wall[k].x == x2) && (Engine.board.wall[k].y == y2))
                        if ((Engine.board.wall[Engine.board.wall[k].point2].x == x1) && (Engine.board.wall[Engine.board.wall[k].point2].y == y1))
                            if (j != sectnum)
                            {
                                Engine.board.wall[i].nextsector = (short)j;
                                Engine.board.wall[i].nextwall = (short)k;
                                Engine.board.wall[k].nextsector = sectnum;
                                Engine.board.wall[k].nextwall = i;
                            }
                }
            }
            return (0);
        }


        public static void deletesector(short sucksect)
        {
	        int i, j, k, nextk, startwall, endwall;

	        while (Engine.board.headspritesect[sucksect] >= 0)
                Engine.board.deletesprite((short)Engine.board.headspritesect[sucksect]);
            updatenumsprites();

            startwall = Engine.board.sector[sucksect].wallptr;
            endwall = startwall + Engine.board.sector[sucksect].wallnum - 1;
            j = Engine.board.sector[sucksect].wallnum;

            for (i = sucksect; i < Engine.board.numsectors - 1; i++)
	        {
                k = Engine.board.headspritesect[i + 1];
		        while (k != -1)
		        {
                    nextk = Engine.board.nextspritesect[k];
                    Engine.board.changespritesect((short)k, (short)i);
			        k = nextk;
		        }

                Engine.board.sector[i + 1].copyto(ref Engine.board.sector[i]);

		        //memcpy(&sector[i],&sector[i+1],sizeof(sectortype));
                Engine.board.sector[i].wallptr -= (short)j;
	        }
            Engine.board.numsectors--;

	        j = endwall-startwall+1;
	        for (i=startwall;i<=endwall;i++)
                if (Engine.board.wall[i].nextwall != -1)
		        {
                    Engine.board.wall[Engine.board.wall[i].nextwall].nextwall = -1;
                    Engine.board.wall[Engine.board.wall[i].nextwall].nextsector = -1;
		        }
	        movewalls(startwall,-j);
            for (i = 0; i < Engine.board.numwalls; i++)
                if (Engine.board.wall[i].nextwall >= startwall)
                    Engine.board.wall[i].nextsector--;
        }
    }
}
