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
using System.Windows.Media.Imaging;
using build;

namespace buildlite
{
    //
    // BuildEditor
    //
    public class BuildEditor
    {
        int xdim2d = 640, ydim2d = 480, xdimgame = 640, ydimgame = 480, bppgame = 8;
        int posx = 32768;
		int posy = 32768;
		int posz = 0;
		short ang = 1536;
		int numsectors = 0;
		int numwalls = 0;
        int numsprites = 0;
        short cursectnum = -1;
        short grid = 3, gridlock = 1, showtags = 1;
        int zoom = 768, gettilezoom = 1;
        bool draw2dview = true;
        int pointhighlight = 0;

        int mousx2 = 8;
        int mousy2 = 8;
        int fvel = 0;
        int svel = 0;
        int zvel = 0;
        int angvel = 0;

        public const int STATUS2DSIZ = 144;
        private readonly string kensig = "BUILD by Ken Silverman";

        public void Init(ref Image canvasimage)
        {
            // Init the build engine.
            Engine.Init();

            Engine.editstatus = true;

            // Load in the game data.
            Engine.initgroupfile("sw.grp");

            Engine.LoadTables();

            // Init the device
            Engine.setgamemode(0, 640, 480, 8, ref canvasimage);

            // Load in the tiles.
            Engine.loadpics("tiles000.art");

            Engine.loadboard("$bullet.map", ref posx, ref posy, ref posz, ref ang, ref cursectnum);
            
            
        }

        private int adjustmark(ref int xplc, ref int yplc, short danumwalls)
        {
            int i, dst, dist, dax, day, pointlockdist;

            if (danumwalls < 0)
                danumwalls = (short)numwalls;

            pointlockdist = 0;
            if ((grid > 0) && (gridlock > 0))
                pointlockdist = (128 >> grid);

            dist = pointlockdist;
            dax = xplc;
            day = yplc;
            for (i = 0; i < danumwalls; i++)
            {
                dst = pragmas.klabs((xplc) - Engine.board.wall[i].x) + pragmas.klabs((yplc) - Engine.board.wall[i].y);
                if (dst < dist)
                {
                    dist = dst;
                    dax = Engine.board.wall[i].x;
                    day = Engine.board.wall[i].y;
                }
            }
            if (dist == pointlockdist)
                if ((gridlock > 0) && (grid > 0))
                {
                    dax = (int)((dax + (1024 >> grid)) & (0xffffffff << (11 - grid)));
                    day = (int)((day + (1024 >> grid)) & (0xffffffff << (11 - grid)));
                }

            xplc = dax;
            yplc = day;
            return (0);
        }

        void printcoords16(int posxe, int posye, short ange)
        {
	        string snotbuf;
            int i;
            bool m;

            snotbuf = "x=" + posxe + " y=" + posye + " ang=" + ange;

	       // Bsprintf(snotbuf,"x=%ld y=%ld ang=%d",posxe,posye,ange);
            i = snotbuf.Length;
            if (i >= 30)
                i = i - (i - 27);
	        //while ((snotbuf[i] != 0) && (i < 30))
		      //  i++;
	        while (i < 30)
	        {
                snotbuf += " ";
		        i++;
	        }

            m = (numsectors > bMap.MAXSECTORS || numwalls > bMap.MAXWALLS || numsprites > bMap.MAXSPRITES);

	        Engine.printext16(8, Engine._device.ydim-STATUS2DSIZ+128, 11, 6, snotbuf,0);

            snotbuf = numsectors + "/"+ bMap.MAXSECTORS + " sect. " + numwalls + "/" + bMap.MAXWALLS + " walls " + numsprites + "/" + bMap.MAXSPRITES + "spri.";

	        
	        i = snotbuf.Length;
            if (i >= 40)
                i = i - (i - 43);
	       // while ((snotbuf[i] != 0) && (i < 46))
		     //   i++;
	        while (i < 46)
	        {
                snotbuf += " ";
		        i++;
	        }

            Engine.printext16(264, Engine.ydim - STATUS2DSIZ + 128, 14, 6, snotbuf, 0);
        }

        int checkautoinsert(int dax, int day, short danumwalls)
        {
            int i, x1, y1, x2, y2;

            if (danumwalls < 0)
                danumwalls = (short)numwalls;
            for (i = 0; i < danumwalls; i++)       // Check if a point should be inserted
            {
                x1 = Engine.board.wall[i].x;
                y1 = Engine.board.wall[i].y;
                x2 = Engine.board.wall[Engine.board.wall[i].point2].x;
                y2 = Engine.board.wall[Engine.board.wall[i].point2].y;

                if ((x1 != dax) || (y1 != day))
                    if ((x2 != dax) || (y2 != day))
                        if (((x1 <= dax) && (dax <= x2)) || ((x2 <= dax) && (dax <= x1)))
                            if (((y1 <= day) && (day <= y2)) || ((y2 <= day) && (day <= y1)))
                                if ((dax - x1) * (y2 - y1) == (day - y1) * (x2 - x1))
                                    return (1);          //insertpoint((short)i,dax,day);
            }
            return (0);
        }

        private char[] snotbuf = new char[55];
        public void printmessage16(string name)
        {
	        int i;

	        i = 0;
	        while ((i < name.Length) && (i < 54))
	        {
		        snotbuf[i] = name[i];
		        i++;
	        }
	        while (i < 54)
	        {
		        snotbuf[i] = (char)32;
		        i++;
	        }
            snotbuf[54] = (char)0;

            Engine._device.BeginDrawing();
	        Engine.printext16(200, Engine.ydim-STATUS2DSIZ+8, 0, 6, new string(snotbuf), 0);
            Engine._device.EndDrawing();
        }

        private void clearmidstatbar16()
        {
            Engine._device.BeginDrawing();
            Engine._device._screenbuffer.Clear();
            Engine.copybufint(Engine._device._screenbuffer.Pixels, (Engine.frameplace + (Engine._device.bytesperline * (Engine._device.ydim - (STATUS2DSIZ)))), Engine._device._screenbuffer.Pixels.Length, 0x08080808*4);
            Engine._device.EndDrawing();
        }
        private void getpoint(int searchxe, int searchye, ref int x, ref int y)
        {
            if (posx <= -131072) posx = -131072;
            if (posx >= 131072) posx = 131072;
            if (posy <= -131072) posy = -131072;
            if (posy >= 131072) posy = 131072;

            x = posx + pragmas.divscale14(searchxe - Engine._device.halfxdim16, zoom);
            y = posy + pragmas.divscale14(searchye - Engine._device.midydim16, zoom);

            if (x <= -131072) x = -131072;
            if (x >= 131072) x = 131072;
            if (y <= -131072) y = -131072;
            if (y >= 131072) y = 131072;
        }

        private void getpoint2(int searchxe, int searchye, ref int x, ref int y)
        {
            x = pragmas.divscale14(searchxe - Engine._device.halfxdim16, zoom);
            y = pragmas.divscale14(searchye - Engine._device.midydim16, zoom);
        }

        public void editinputkeyup(bool mouserightdown, bool mouseleftdown, Key key)
        {
            switch (key)
            {
                case Key.A:
                    //svel += 400;
                    angvel = 0;
                    break;
                case Key.W:
                    fvel = 0;
                    break;
                case Key.D:
                    //svel -= 400;
                    angvel = 0;
                    break;
                case Key.S:
                    fvel = 0;
                    break;
                case Key.Q:
                    svel = 0;
                    break;
                case Key.E:
                    svel = 0;
                    break;
                case Key.Space:
                    zvel = 0;
                    break;
                case Key.C:
                    zvel = 0;
                    break;
                default:
                    return;
            }
        }

        public void editinputkey(bool mouserightdown, bool mouseleftdown, Key key)
        {
            if (mouseleftdown)
            {
                if (draw2dview)
                {
                    getpoint(mousx2, mousy2, ref posx, ref posy);
                    Engine.board.updatesector(posx, posy, ref cursectnum);

                    if (cursectnum >= 0)
                    {
                        posz = Engine.board.sector[cursectnum].floorz;
                    }
                }

                return;
            }

            switch (key)
            {
                case Key.A:
                    //svel += 400;
                    angvel = -30;
                    break;
                case Key.W:
                    fvel = 400;
                    break;
                case Key.D:
                    //svel -= 400;
                    angvel = 30;
                    break;
                case Key.S:
                    fvel = -400;
                    break;
                case Key.Q:
                    svel = 400;
                    break;
                case Key.E:
                    svel = -200;
                    break;
                case Key.Space:
                    zvel = -300;
                    break;
                case Key.C:
                    zvel = 300;
                    break;
                case Key.Enter:
                    if (cursectnum >= 0)
                        draw2dview = !draw2dview;
                    break;
                default:
                    return;
            }

                /*
            else if (key == Key.Enter)
            {
                
            }
                 */
        }

        public void editinputmouse(double mousx, double mousy)
        {
            mousx2 = (int)(mousx * 1.0f);
            mousy2 = (int)(mousy * 1.0f);



            Engine.searchx = mousx2; // (mousx2 >> 1);
            Engine.searchy = mousy2; // (mousy2 >> 1);
            if (Engine.searchx < 4) mousx2 = 4;
            if (Engine.searchy < 4) mousy2 = 4;
            if (Engine.searchx > Engine.xdim - 5) mousx2 = Engine.xdim - 5;
            if (Engine.searchy > Engine.ydim - 5) mousy2 = Engine.ydim - 5;
        }

        private void showmouse()
        {
	        int i;

	        for(i=1;i<=4;i++)
	        {
                Engine.plotpixel(mousx2 + i, mousy2, 12);
                Engine.plotpixel(mousx2 - i, mousy2, 12);
                Engine.plotpixel(mousx2, mousy2 - i, 12);
                Engine.plotpixel(mousx2, mousy2 + i, 12);
	        }
        }



        private void overheadeditor()
        {
            // Clear all the status bar positions to the correct color;
            clearmidstatbar16();

           	Engine.xdim2d = Engine._device.xdim;
	        Engine.ydim2d = Engine._device.ydim;

	        Engine.searchx = pragmas.scale(Engine.searchx,xdim2d,xdimgame);
	        Engine.searchy = pragmas.scale(Engine.searchy,ydim2d-STATUS2DSIZ,ydimgame);
	       // oposz = posz;

	        Engine.ydim16 = Engine.ydim;
	        Engine.drawline16(0,Engine._device.ydim-STATUS2DSIZ,Engine._device.xdim-1,Engine._device.ydim-STATUS2DSIZ,7);
	        Engine.drawline16(0,Engine._device.ydim-1,Engine._device.xdim-1,Engine._device.ydim-1,7);
	        Engine.drawline16(0,Engine._device.ydim-STATUS2DSIZ,0,Engine._device.ydim-1,7);
	        Engine.drawline16(Engine._device.xdim-1,Engine._device.ydim-STATUS2DSIZ,Engine._device.xdim-1,Engine._device.ydim-1,7);
	        Engine.drawline16(0,Engine._device.ydim-STATUS2DSIZ+24,Engine._device.xdim-1,Engine._device.ydim-STATUS2DSIZ+24,7);
	        Engine.drawline16(192,Engine._device.ydim-STATUS2DSIZ,192,Engine._device.ydim-STATUS2DSIZ+24,7);
	        Engine.printext16(9,Engine._device.ydim-STATUS2DSIZ+9,4,-1,kensig,0);
	        Engine.printext16(8,Engine._device.ydim-STATUS2DSIZ+8,12,-1,kensig,0);
	        printmessage16("WebBuild: By Justin Marshall v1");
	        Engine.drawline16(0,Engine._device.ydim-1-24,Engine._device.xdim-1,Engine._device.ydim-1-24,7);
	        Engine.drawline16(256,Engine._device.ydim-1-24,256,Engine._device.ydim-1,7);
	        Engine.ydim16 = Engine.ydim-STATUS2DSIZ;
	        Engine._device.EndDrawing();

            printcoords16(posx, posy, ang);

            Engine.clear2dscreen();
            Engine.draw2dgrid(posx, posy, ang, zoom, grid);

            Engine.board.draw2dscreen(posx, posy, ang, zoom, grid);

            int mousxplc = 0, mousyplc = 0;
            getpoint(mousx2, mousy2, ref mousxplc, ref mousyplc);
            pointhighlight = getpointhighlight(mousxplc, mousyplc, pointhighlight);


            if ((pointhighlight & 0xc000) == 16384)
            {
                drawspriteinfo((pointhighlight & 16383), (Engine._device.ydim - STATUS2DSIZ) + 30);
            }     
        }

        //
        // ProcessMovement
        //
        private void ProcessMovement(int xvect, int yvect)
        {
            int i = 40;
            int fz = 0, cz = 0, hz = 0, lz = 0;
            int k;

            Engine.board.updatesector(posx, posy, ref cursectnum);
            Engine.board.getzrange(posx, posy, posz, cursectnum, ref cz, ref hz, ref fz, ref lz, 163, Engine.CLIPMASK0);

            posz += zvel;

            if (posz > fz)
            {
                posz = fz;
            }
            else if (posz < cz)
            {
                posz = cz;
            }

            Engine.board.clipmove(ref posx, ref posy, ref posz, ref cursectnum, xvect, yvect, 164, (4 << 8), (4 << 8), Engine.CLIPMASK0);
        }

        private void MoveViewer()
        {
            int xvect = 0, yvect = 0;

            ang += (short)angvel;

            int doubvel = 3;

            xvect = 0; yvect = 0;
            if (fvel != 0)
            {
                xvect += ((((int)fvel) * doubvel * (int)Engine.table.sintable[(ang + 512) & 2047]) >> 3);
                yvect += ((((int)fvel) * doubvel * (int)Engine.table.sintable[ang & 2047]) >> 3);
            }
            if (svel != 0)
            {
                xvect += ((((int)svel) * doubvel * (int)Engine.table.sintable[ang & 2047]) >> 3);
                yvect += ((((int)svel) * doubvel * (int)Engine.table.sintable[(ang + 1536) & 2047]) >> 3);
            }

            ProcessMovement(xvect, yvect);

            svel = 0;
            fvel = 0;
            angvel = 0;
        }

        Int32 getpointhighlight(Int32 xplc, Int32 yplc, Int32 point)
        {
            Int32 i, j, dst, dist = 512, closest = -1;
            Int32 dax=0,day=0;

            if (Engine.board.numwalls == 0)
                return -1;

            if (grid < 1)
                dist = 0;

   

            for (i = 0; i < Engine.board.numsectors; i++)
            {
                for (j=Engine.board.sector[i].wallptr; j<Engine.board.sector[i].wallptr+Engine.board.sector[i].wallnum; j++)
                {
                    Engine.screencoords(ref dax, ref day, Engine.board.wall[j].x - posx, Engine.board.wall[j].y - posy, zoom);
                    day += Engine.getscreenvdisp(Engine.board.getflorzofslope((short)i, Engine.board.wall[j].x, Engine.board.wall[j].y) - posz, zoom);

                    if (Engine._device.halfxdim16+dax < 0 || Engine._device.halfxdim16+dax >= Engine._device.xdim || Engine._device.midydim16+day < 0 || Engine._device.midydim16+day >= Engine._device.ydim)
                         continue;

                    dst = pragmas.klabs(Engine._device.halfxdim16+dax-Engine.searchx) + pragmas.klabs(Engine._device.midydim16+day-Engine.searchy);

                    if (dst <= dist)
                    {
                        // prefer white walls
                        if (dst<dist || closest==-1 || (!(Engine.board.wall[j].nextwall>=0) || !(Engine.board.wall[closest].nextwall>=0)))
                        {
                            dist = dst;
                            closest = j;
                        }
                    }
                }
            }

            if (zoom >= 256)
                for (i = 0; i < bMap.MAXSPRITES; i++)
                {
                    if (Engine.board.sprite[i] == null)
                        continue;

                    if (Engine.board.sprite[i].statnum < bMap.MAXSTATUS)
                    {
                        //   if (!m32_sideview && Engine.board.sprite[i].sectnum >= 0)
                        //      YAX_SKIPSECTOR(sprite[i].sectnum);

                        if (true /*!m32_sideview*/)
                        {
                            
                            dst = pragmas.klabs(xplc - Engine.board.sprite[i].x) + pragmas.klabs(yplc - Engine.board.sprite[i].y)-150;
                        }
                        else
                        {
                            Engine.screencoords(ref dax, ref day, Engine.board.sprite[i].x - posx, Engine.board.sprite[i].y - posy, zoom);
                            day += Engine.getscreenvdisp(Engine.board.sprite[i].z - posz, zoom);

                            if (Engine._device.halfxdim16 + dax < 0 || Engine._device.halfxdim16 + dax >= Engine._device.xdim || Engine._device.midydim16 + day < 0 || Engine._device.midydim16 + day >= Engine._device.ydim)
                                continue;

                            dst = pragmas.klabs(Engine._device.halfxdim16 + dax - Engine.searchx) + pragmas.klabs(Engine._device.midydim16 + day - Engine.searchy);
                        }

                        // was (dst <= dist), but this way, when duplicating sprites,
                        // the selected ones are dragged first
                        if (dst < dist || (dst == dist /*&& (show2dsprite[i>>3]&(1<<(i&7)))*/))
                        {
                            dist = dst;
                            closest = i + 16384;
                        }
                    }
                }

            return closest;
        }

        private void drawspriteinfo(int hitsprite, int ypos)
        {
            if (hitsprite >= 0)
            {
                Engine.printext16(0, ypos + 0, 15, -1, "Sprite: " + hitsprite + " Stats", 0);
                Engine.printext16(0, ypos + 15, 15, -1, "Hitag: " + Engine.board.sprite[hitsprite].hitag, 0);
                Engine.printext16(0, ypos + 25, 15, -1, "Lotag: " + Engine.board.sprite[hitsprite].lotag, 0);
                Engine.printext16(0, ypos + 35, 15, -1, "Picnum: " + Engine.board.sprite[hitsprite].picnum, 0);
                Engine.printext16(0, ypos + 45, 15, -1, "Pal: " + Engine.board.sprite[hitsprite].pal, 0);
                Engine.printext16(0, ypos + 55, 15, -1, "Shade: " + Engine.board.sprite[hitsprite].pal, 0);
                Engine.rotatesprite(125 << 16, (ypos + 20) << 16, 65536, 0, Engine.board.sprite[hitsprite].picnum, Engine.board.sprite[hitsprite].shade, Engine.board.sprite[hitsprite].pal, 8 | 16, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
            }
        }

        private void draw3dcursorinfo()
        {
            short hitsprite = 0, hitwall = 0;
            int hitsector = 0;
            int hitx = 0, hity = 0, hitz = 0;
            int dax = 16384;
            int day = pragmas.divscale14(mousx2 - (Engine._device.xdim >> 1), Engine._device.xdim >> 1);
            Engine.rotatepoint(0, 0, dax, day, ang, ref dax, ref day);

            Engine.board.hitscan(posx, posy, posz, cursectnum,              //Start position
                    dax, day, (pragmas.scale(mousy2, 200, Engine._device.ydim) - 100) * 2000, //vector of 3D ang
                    ref hitsector, ref hitwall, ref hitsprite, ref hitx, ref hity, ref hitz, Engine.CLIPMASK1);

            drawspriteinfo(hitsprite, 0);
        }

        private void draw3dview()
        {
            Engine.board.drawrooms(posx, posy, posz  - 768, ang, 100, cursectnum);
            Engine.board.drawmasks();


            draw3dcursorinfo();
        }

        public void Frame()
        {
            
            if(cursectnum >= 0)
                MoveViewer();

            if (draw2dview)
            {
                overheadeditor();
            }
            else
            {
                draw3dview();
            }

                  

            showmouse();

            Engine.NextPage();
        }
    }
}
