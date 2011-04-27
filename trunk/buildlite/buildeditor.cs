using System;
using System.Collections.Generic;
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
    enum MouseSectorHitType
    {
        MOUSE_SECTORHIT_NONE,
        MOUSE_SECTORHIT_FLOOR,
        MOUSE_SECTORHIT_CEILING
    }
    class MouseTrace
    {
        public short hitsprite = 0, hitwall = 0;
        public int hitsector = 0;
        public MouseSectorHitType hittype = MouseSectorHitType.MOUSE_SECTORHIT_NONE;
        public int hitx = 0, hity = 0, hitz = 0;
    }

    enum EditorState
    {
        STATE_2DVIEW = 0,
        STATE_3DVIEW,
        STATE_TILESELECT
    }

    //
    // BuildEditor
    //
    public class BuildEditor
    {
        int xdim2d = 640, ydim2d = 480, xdimgame = 640, ydimgame = 480, bppgame = 8;
        int posx = 32768;
		int posy = 32768;
		int posz = 0;
        int mousxplc = 0, mousyplc = 0;
        int linehighlight = -1;
		short ang = 1536;
        private short numsectors
        {
            get
            {
                return (short)Engine.board.numsectors;
            }
            set
            {
                Engine.board.numsectors = value;
            }
        }
        private short numwalls
        {
            get
            {
                return (short)Engine.board.numwalls;
            }
            set
            {
                Engine.board.numwalls = value;
            }
        }
        
        int numsprites = 0;
        short cursectnum = -1;
        short grid = 3, gridlock = 1, showtags = 1;
        int zoom = 768, gettilezoom = 1;
        EditorState editorState = EditorState.STATE_2DVIEW;
        int pointhighlight = 0;
        MouseTrace mouseTrace = new MouseTrace();
        List<walltype> dragpoint = new List<walltype>();

        short[] localartlookup = new short[bMap.MAXTILES];
        short localartlookupnum = -1;
        bool inbuildmenu = false;
        bool inloadmenu = false;
        string loadmenuname = "";
        int mousx2 = 8;
        int mousy2 = 8;
        int fvel = 0;
        int svel = 0;
        int zvel = 0;
        int angvel = 0;
        int whitecol = 0;
        int objzvel = 0;

        private void initnewboard()
        {
            Engine.newboard();
            xdim2d = 640; ydim2d = 480; xdimgame = 640; ydimgame = 480; bppgame = 8;
            posx = 32768;
            posy = 32768;
            posz = 0;
            ang = 1536;
            numsectors = 0;
            numwalls = 0;

            numsprites = 0;
            cursectnum = -1;
            grid = 3; gridlock = 1; showtags = 1;
            zoom = 768; gettilezoom = 1;
        }

        public const int STATUS2DSIZ = 144;
        private readonly string kensig = "BUILD by Ken Silverman";

        public void Init(ref Image canvasimage)
        {
            // Init the build engine.
            Engine.Init();

            Engine.editstatus = true;

            // Load in the game data.
            Engine.initgroupfile("stuff.dat");

            Engine.LoadTables();

            // Init the device
            Engine.setgamemode(0, 640, 480, 8, ref canvasimage);

            // Load in the tiles.
            Engine.loadpics("tiles000.art");

            int j = 0, k = 0;
            for (int i = 0; i < 256; i++)
            {
                j = Engine.palette._palettebuffer[i];
                if (j > k) { k = j; whitecol = i; }
            }

          //  Engine.loadboard("nukeland.map", ref posx, ref posy, ref posz, ref ang, ref cursectnum);
            initnewboard();
            

            for (short i = 0; i < bMap.MAXTILES; i++)
            {
          //      localartfreq[i] = 0;
                localartlookup[i] = i;
            }
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

        public void editinputkeyup(bool mouserightup, bool mouseleftup, Key key)
        {
            if (mouseleftup)
            {
                dragpoint.Clear();
                return;
            }
            switch (key)
            {
                case Key.A:
                    svel = 0;
                   //angvel = 0;
                    break;
                case Key.Up:
                    fvel = 0;
                    break;
                case Key.D:
                    svel = 0;
                   // angvel = 0;
                    break;
                case Key.Down:
                    fvel = 0;
                    break;
                case Key.Left:
                    angvel = 0;
                    break;
                case Key.Right:
                    angvel = 0;
                    break;
                case Key.Space:
                    zvel = 0;
                    break;
                case Key.C:
                    zvel = 0;
                    break;
                case Key.PageDown:
                    objzvel = 0;
                    break;
                case Key.PageUp:
                    objzvel = 0;
                    break;
                default:
                    return;
            }
        }

        private void ChangeObjectZ(int val)
        {
            if (editorState == EditorState.STATE_3DVIEW)
            {
                if (mouseTrace.hitsprite >= 0)
                {
                    Engine.board.sprite[mouseTrace.hitsprite].z += val;
                }
                else if (mouseTrace.hitsector >= 0 && mouseTrace.hitwall == -1)
                {
                    if (Engine.board.getflorzofslope((short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity) <= mouseTrace.hitz)
                    {
                        Engine.board.sector[mouseTrace.hitsector].floorz += val;
                    }
                    else if (Engine.board.getceilzofslope( (short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity) >= mouseTrace.hitz)
                    {
                        Engine.board.sector[mouseTrace.hitsector].ceilingz += val;
                    }
                }
            }
        }

        private void InsertPointOnHighlightedLine()
        {
            int dax = 0, day = 0, i, j;
            if (linehighlight >= 0)
            {
                EditorLib.getclosestpointonwall(mousxplc, mousyplc, (int)linehighlight, ref dax, ref day);
                adjustmark(ref dax, ref day, (short)newnumwalls);
                EditorLib.insertpoint((short)linehighlight, dax, day);
                printmessage16("Point inserted.");

                j = 0;
                //Check to see if point was inserted over another point
                for (i = numwalls - 1; i >= 0; i--)     //delete points
                    if (Math.Abs(Engine.board.wall[i].x - Engine.board.wall[Engine.board.wall[i].point2].x) < 100 && Math.Abs(Engine.board.wall[i].y - Engine.board.wall[Engine.board.wall[i].point2].y) < 100)
                  //  if (Engine.board.wall[i].x == Engine.board.wall[Engine.board.wall[i].point2].x)
                    //    if (Engine.board.wall[i].y == Engine.board.wall[Engine.board.wall[i].point2].y)
                        {
                            EditorLib.deletepoint((short)i);
                            j++;
                        }
                for (i = 0; i < numwalls; i++)        //make new red lines?
                {
                    if (Math.Abs(Engine.board.wall[i].x - dax) < 100 && Math.Abs(Engine.board.wall[i].x - day) < 100)
                    {
                        EditorLib.checksectorpointer((short)i, (short)Engine.board.sectorofwall((short)i));
                        EditorLib.fixrepeats((short)i);
                    }
                    else if ((Engine.board.wall[Engine.board.wall[i].point2].x - dax) < 100 && (Engine.board.wall[Engine.board.wall[i].point2].y - day) < 100)
                    {
                        EditorLib.checksectorpointer((short)i, (short)Engine.board.sectorofwall((short)i));
                        EditorLib.fixrepeats((short)i);
                    }
                }
                //if (j != 0)
                //{
                //   dax = ((wall[linehighlight].x + wall[wall[linehighlight].point2].x)>>1);
                //   day = ((wall[linehighlight].y + wall[wall[linehighlight].point2].y)>>1);
                //   if ((dax != wall[linehighlight].x) || (day != wall[linehighlight].y))
                //      if ((dax != wall[wall[linehighlight].point2].x) || (day != wall[wall[linehighlight].point2].y))
                //      {
                //         insertpoint(linehighlight,dax,day);
                //         printmessage16("Point inserted at midpoint.");
                //      }
                //}

              //  asksave = 1;
            }
        }

        public void editinputkey(bool mouserightdown, bool mouseleftdown, Key key)
        {
            if (inloadmenu)
            {
                if (key == Key.Enter)
                {
                    Engine.loadboard(loadmenuname + ".map", ref posx, ref posy, ref posz, ref ang, ref cursectnum);
                    inloadmenu = false;
                }
                else if (key == Key.Escape)
                {
                    inloadmenu = false;
                }
                else if (key == Key.Back)
                {
                    if (loadmenuname.Length > 0)
                    {
                        loadmenuname.Remove(loadmenuname.Length - 1);
                    }
                }
                else
                {
                    loadmenuname += key;
                }
                return;
            }
            else if (inbuildmenu)
            {
                if (key == Key.N)
                {
                    initnewboard();
                }
                else if (key == Key.L)
                {
                    loadmenuname = "";
                    inloadmenu = true;
                }

                inbuildmenu = !inbuildmenu;
                return;
            }
            if (mouseleftdown)
            {
                if (newnumwalls >= 0)
                {
                    overheaddrawwalls();
                }
                else if (editorState == EditorState.STATE_2DVIEW && dragpoint.Count == 0)
                {
                    for (int i = 0; i < numwalls; i++)
                    {
                        if (Math.Abs(Engine.board.wall[i].x - mousxplc) < 100 && Math.Abs(Engine.board.wall[i].y - mousyplc) < 100)
                        {
                            dragpoint.Add( Engine.board.wall[i] );
                        }
                    }
                    if (dragpoint.Count > 0) return;
                    posx = mousxplc;
                    posy = mousyplc;
                    //getpoint(mousx2, mousy2, ref posx, ref posy);
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
                    svel += 400;
                  //  angvel = -30;
                    break;
                case Key.W:
                    
                    break;
                case Key.D:
                    svel -= 400;
                  //  angvel = 30;
                    break;
                case Key.S:
                    {
                        short sectornum = -1;
                        int dax = -1, day = -1, daz = -1;
                        if (editorState == EditorState.STATE_3DVIEW)
                        {
                            sectornum = (short)mouseTrace.hitsector;
                            dax = mouseTrace.hitx;
                            day = mouseTrace.hity;
                            daz = mouseTrace.hitz;
                        }
                        else if (editorState == EditorState.STATE_2DVIEW)
                        {
                            Engine.board.updatesector(mousxplc, mousxplc, ref sectornum);
                            if (sectornum >= 0)
                            {
                                dax = mousxplc;
                                day = mousyplc;
                                daz = Engine.board.sector[sectornum].floorz;
                            }
                        }

                        if (sectornum != -1)
                        {
                            int i = Engine.board.insertsprite(sectornum, 0);

                            Engine.board.sprite[i].x = dax;
                            Engine.board.sprite[i].y = day;
                            Engine.board.sprite[i].cstat = 0;
                            Engine.board.sprite[i].shade = 0;
                            Engine.board.sprite[i].pal = 0;
                            Engine.board.sprite[i].xrepeat = 64;
                            Engine.board.sprite[i].yrepeat = 64;
                            Engine.board.sprite[i].xoffset = 0;
                            Engine.board.sprite[i].yoffset = 0;
                            Engine.board.sprite[i].ang = 1536;
                            Engine.board.sprite[i].xvel = 0;
                            Engine.board.sprite[i].yvel = 0;
                            Engine.board.sprite[i].zvel = 0;
                            Engine.board.sprite[i].owner = -1;
                            Engine.board.sprite[i].clipdist = 32;
                            Engine.board.sprite[i].lotag = 0;
                            Engine.board.sprite[i].hitag = 0;
                            Engine.board.sprite[i].extra = -1;

                            int j = ((Engine.tilesizy[Engine.board.sprite[i].picnum] * Engine.board.sprite[i].yrepeat) << 1);
                            if ((Engine.board.sprite[i].cstat & 128) == 0)
                                Engine.board.sprite[i].z = Math.Min(Math.Max(mouseTrace.hitz, Engine.board.getceilzofslope((short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity) + (j << 1)), Engine.board.getflorzofslope((short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity));
                            else
                                Engine.board.sprite[i].z = Math.Min(Math.Max(mouseTrace.hitz, Engine.board.getceilzofslope((short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity) + j), Engine.board.getflorzofslope((short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity) - j);

                            if ((Engine.searchstat == 0) || (Engine.searchstat == 4))
                            {
                                Engine.board.sprite[i].cstat |= (16 + 64);
                                if (mouseTrace.hitwall >= 0)
                                    Engine.board.sprite[i].ang = (short)((Engine.getangle(Engine.board.wall[Engine.board.wall[mouseTrace.hitwall].point2].x - Engine.board.wall[mouseTrace.hitwall].x, Engine.board.wall[Engine.board.wall[mouseTrace.hitwall].point2].y - Engine.board.wall[mouseTrace.hitwall].y) + 512) & 2047);

                                //Make sure sprite's in right sector
                                if (Engine.board.inside(Engine.board.sprite[i].x, Engine.board.sprite[i].y, Engine.board.sprite[i].sectnum) == 0)
                                {
                                    j = Engine.board.wall[mouseTrace.hitwall].point2;
                                    Engine.board.sprite[i].x -= pragmas.ksgn(Engine.board.wall[j].y - Engine.board.wall[mouseTrace.hitwall].y);
                                    Engine.board.sprite[i].y += pragmas.ksgn(Engine.board.wall[j].x - Engine.board.wall[mouseTrace.hitwall].x);
                                }
                            }
                            else
                            {
                                if (Engine.tilesizy[Engine.board.sprite[i].picnum] >= 32) Engine.board.sprite[i].cstat |= 1;
                            }


                            EditorLib.updatenumsprites();
                        }
                    }
                    break;
                case Key.Q:
                    svel = 400;
                    break;
                case Key.E:
                    svel = -200;
                    break;
                case Key.Space:
                    if (editorState == EditorState.STATE_3DVIEW)
                    {
                        zvel = -300;
                    }
                    else if (editorState == EditorState.STATE_2DVIEW)
                    {
                        overheaddrawwalls();
                    }
                    break;
                case Key.C:
                    zvel = 300;
                    break;
                case Key.PageDown:
                    objzvel = 1024;
                    break;
                case Key.PageUp:
                    objzvel = -1024;
                    break;
                case Key.Insert:
                    if (editorState == EditorState.STATE_2DVIEW)
                        InsertPointOnHighlightedLine();
                    break;
                case Key.Escape:
                    if (editorState == EditorState.STATE_TILESELECT)
                        editorState = EditorState.STATE_3DVIEW;
                    else if (editorState == EditorState.STATE_2DVIEW)
                    {
                        if (newnumwalls >= 0)
                        {
                            newnumwalls = -1;
                        }
                        if (inloadmenu == false)
                            inbuildmenu = !inbuildmenu;
                    }
                    break;
                case Key.Down:
                    if (editorState == EditorState.STATE_TILESELECT)
                    {
                        localartlookupnum += (short)((Engine._device.xdim >> 6) * 2);
                    }
                    else
                    {
                        fvel = -400;
                    }
                    break;
                case Key.Up:
                    if (editorState == EditorState.STATE_TILESELECT)
                    {
                        localartlookupnum -= (short)((Engine._device.xdim >> 6) * 2);
                    }
                    else
                    {
                        fvel = 400;
                    }
                    break;
                case Key.Right:
                    if (editorState == EditorState.STATE_TILESELECT)
                    {
                        localartlookupnum++;
                    }
                    else
                    {
                        angvel = 30;
                    }
                    break;
                case Key.Left:
                    if (editorState == EditorState.STATE_TILESELECT)
                    {
                        localartlookupnum--;
                    }
                    else
                    {
                        angvel = -30;
                    }
                    break;

                case Key.NumPad4:

                    break;
 
                case Key.V:
                    if (editorState == EditorState.STATE_3DVIEW)
                    {
                        if (mouseTrace.hitsprite >= 0)
                        {
                            localartlookupnum = Engine.board.sprite[mouseTrace.hitsprite].picnum;
                        }
                        else if (mouseTrace.hitwall >= 0)
                        {
                            localartlookupnum = Engine.board.wall[mouseTrace.hitwall].picnum;
                        }
                        else if (mouseTrace.hittype == MouseSectorHitType.MOUSE_SECTORHIT_FLOOR)
                        {
                            localartlookupnum = Engine.board.sector[mouseTrace.hitsector].floorpicnum;
                        }
                        else if (mouseTrace.hittype == MouseSectorHitType.MOUSE_SECTORHIT_CEILING)
                        {
                            localartlookupnum = Engine.board.sector[mouseTrace.hitsector].ceilingpicnum;
                        }

                        editorState = EditorState.STATE_TILESELECT;
                    }
                    break;
                case Key.Enter:
                    if (cursectnum >= 0)
                    {
                        if (editorState == EditorState.STATE_3DVIEW)
                        {
                            editorState = EditorState.STATE_2DVIEW;
                        }
                        else if (editorState == EditorState.STATE_2DVIEW)
                        {
                            editorState = EditorState.STATE_3DVIEW;
                        }
                        else if (editorState == EditorState.STATE_TILESELECT && Engine.tilesizx[localartlookupnum] != 0 && Engine.tilesizy[localartlookupnum] != 0)
                        {
                            if (mouseTrace.hitsprite >= 0)
                            {
                                Engine.board.sprite[mouseTrace.hitsprite].picnum = localartlookupnum;
                            }
                            else if (mouseTrace.hitwall >= 0)
                            {
                                Engine.board.wall[mouseTrace.hitwall].picnum = localartlookupnum;
                            }
                            else if (mouseTrace.hittype == MouseSectorHitType.MOUSE_SECTORHIT_FLOOR)
                            {
                                Engine.board.sector[mouseTrace.hitsector].floorpicnum = localartlookupnum;
                            }
                            else if (mouseTrace.hittype == MouseSectorHitType.MOUSE_SECTORHIT_CEILING)
                            {
                                Engine.board.sector[mouseTrace.hitsector].ceilingpicnum = localartlookupnum;
                            }

                            editorState = EditorState.STATE_3DVIEW;
                        }
                    }
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

            getpoint(mousx2, mousy2, ref mousxplc, ref mousyplc);

            
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

        private void drawtilescreen(int pictopleft, int picbox)
        {
	        int i, j, vidpos, vidpos2, dat, wallnum, xdime, ydime, cnt, pinc;
	        int dax, day, scaledown, xtiles, ytiles, tottiles;
            byte[] picptr;
            string snotbuf = ""; //[80];
            int picptrpos = 0;

            xtiles = (Engine._device.xdim >> 6); ytiles = (Engine._device.ydim >> 6); tottiles = xtiles * ytiles;

            pinc = Engine.ylookup[1];
	        Engine.clearview();
	        for(cnt=0;cnt<(tottiles<<(gettilezoom<<1));cnt++)         //draw the 5*3 grid of tiles
	        {
		        wallnum = cnt+pictopleft;
                if (wallnum < 0)
                    continue;
		      //  if (wallnum <= localartlookupnum)
		      //  {
			       // wallnum = localartlookup[wallnum];

                    short w = Engine.tilesizx[wallnum];
                    short h = Engine.tilesizy[wallnum];
                    if ((Engine.tilesizx[wallnum] != 0) && (Engine.tilesizy[wallnum] != 0))
			        {
                        if (Engine.waloff[wallnum] == null) Engine.loadtile((short)wallnum);
				        picptr = Engine.waloff[wallnum].memory;
                        picptrpos = 0;
                        xdime = Engine.tilesizx[wallnum];
                        ydime = Engine.tilesizy[wallnum];

				        dax = ((cnt%(xtiles<<gettilezoom))<<(6-gettilezoom));
				        day = ((cnt/(xtiles<<gettilezoom))<<(6-gettilezoom));
                        vidpos = Engine.ylookup[day] + dax + Engine.frameplace;
				        if ((xdime <= (64>>gettilezoom)) && (ydime <= (64>>gettilezoom)))
				        {
					        for(i=0;i<xdime;i++)
					        {
						        vidpos2 = vidpos+i;
						        for(j=0;j<ydime;j++)
						        {
                                    A.DrawPixelPallete(vidpos2, picptr[picptrpos++]);
							        vidpos2 += pinc;
						        }
					        }
				        }
				        else                          //if 1 dimension > 64
				        {
					        if (xdime > ydime)
						        scaledown = ((xdime+(63>>gettilezoom))>>(6-gettilezoom));
					        else
						        scaledown = ((ydime+(63>>gettilezoom))>>(6-gettilezoom));

					        for(i=0;i<xdime;i+=scaledown)
					        {
                                if (Engine.waloff[wallnum] == null) Engine.loadtile((short)wallnum);
                                picptr = Engine.waloff[wallnum].memory;
                                picptrpos = ydime * i;
                                vidpos2 = vidpos;
                                
						        for(j=0;j<ydime;j+=scaledown)
						        {
                                    A.DrawPixelPallete(vidpos2, picptr[picptrpos]);
							        picptrpos += scaledown;
							        vidpos2 += pinc;
						        }
						        vidpos++;
					        }
				        }
				        if (localartlookupnum < bMap.MAXTILES)
				        {
					        dax = ((cnt%(xtiles<<gettilezoom))<<(6-gettilezoom));
					        day = ((cnt/(xtiles<<gettilezoom))<<(6-gettilezoom));
					      //  sprintf(snotbuf,"%ld",localartfreq[cnt+pictopleft]);
					      //  printext256(dax,day,whitecol,-1,snotbuf,1);
				        }
			        }
		      //  }
	        }

	        cnt = picbox-pictopleft;    //draw open white box
	        dax = ((cnt%(xtiles<<gettilezoom))<<(6-gettilezoom));
	        day = ((cnt/(xtiles<<gettilezoom))<<(6-gettilezoom));

	        for(i=0;i<(64>>gettilezoom);i++)
	        {
		        Engine.plotpixel(dax+i,day,15);
                Engine.plotpixel(dax + i, day + (63 >> gettilezoom), 15);
                Engine.plotpixel(dax, day + i, 15);
                Engine.plotpixel(dax + (63 >> gettilezoom), day + i, 15);
	        }

	        i = localartlookup[picbox];
	        //sprintf(snotbuf,"%ld",i);
            snotbuf = "" + i;
	        Engine.printext256(0,Engine._device.ydim-8,15,-1,snotbuf,0);
            //Engine.printext256(Engine._device.xdim - (strlen(names[i]) << 3), ydim - 8, 15, -1, names[i], 0);
        }

        private walltype GetPointCloseToMouse()
        {
            return null;
        }

        private int newnumwalls = -1;
        private int firstx = 0, firsty = 0;
        private int suckwall = -1, sucksect = -1;
        private int split = 0;
        private void overheaddrawwalls()
        {
            int bad = 0, danumwalls = 0, splitendwall = 0, loopnum = 0, secondstartwall = 0;
            int i, j, dax, day, startwall, endwall, m, k, splitsect = 0, splitstartwall = 0;
                        

            if ((newnumwalls < numwalls) && (numwalls < bMap.MAXWALLS-1))
			{
				firstx = mousxplc; firsty = mousyplc;  //Make first point
				newnumwalls = numwalls;
				suckwall = -1;
				split = 0;

                Engine.board.wall[newnumwalls] = null;
              //  GC.Collect();
                Engine.board.wall[newnumwalls] = new walltype();

                Engine.board.wall[newnumwalls].extra = -1;

                Engine.board.wall[newnumwalls].x = mousxplc;
                Engine.board.wall[newnumwalls].y = mousyplc;
                Engine.board.wall[newnumwalls].nextsector = -1;
                Engine.board.wall[newnumwalls].nextwall = -1;
				for(i=0;i<numwalls;i++)
                    if ((Engine.board.wall[i].x == mousxplc) && (Engine.board.wall[i].y == mousyplc))
						suckwall = i;
                Engine.board.wall[newnumwalls].point2 = (short)(newnumwalls + 1);

				printmessage16("Sector drawing started.");
				newnumwalls++;
			}
			else
			{  //if not back to first point
// jv
		//		if ((firstx != mousxplc) || (firsty != mousyplc))  //nextpoint
                if (Math.Abs(firstx - mousxplc) > 400 || Math.Abs(firsty - mousyplc) > 400)
// jv end
				{
					j = 0;
					for(i=numwalls;i<newnumwalls;i++)
                        if ((mousxplc == Engine.board.wall[i].x) && (mousyplc == Engine.board.wall[i].y))
							j = 1;
					if (j == 0)
					{
							//check if starting to split a sector
						if (newnumwalls == numwalls+1)
						{
                            dax = ((Engine.board.wall[numwalls].x + mousxplc) >> 1);
                            day = ((Engine.board.wall[numwalls].y + mousyplc) >> 1);
							for(i=0;i<numsectors;i++)
								if (Engine.board.inside(dax,day,(short)i) == 1)
								{    //check if first point at point of sector
									m = -1;
                                    startwall = Engine.board.sector[i].wallptr;
                                    endwall = startwall + Engine.board.sector[i].wallnum - 1;
									for(k=startwall;k<=endwall;k++)
                                        if (Engine.board.wall[k].x == Engine.board.wall[numwalls].x)
                                            if (Engine.board.wall[k].y == Engine.board.wall[numwalls].y)
											{
												m = k;
												break;
											}
									if (m >= 0)
                                        if ((Engine.board.wall[Engine.board.wall[k].point2].x != mousxplc) || (Engine.board.wall[Engine.board.wall[k].point2].y != mousyplc))
                                            if ((Engine.board.wall[Engine.board.lastwall((short)k)].x != mousxplc) || (Engine.board.wall[Engine.board.lastwall((short)k)].y != mousyplc))
											{
												split = 1;
												splitsect = i;
												splitstartwall = m;
												break;
											}
								}
						}

							//make new point

						//make sure not drawing over old red line
						bad = 0;
						for(i=0;i<numwalls;i++)
						{
                            if (Engine.board.wall[i].nextwall >= 0)
							{
                                if ((Engine.board.wall[i].x == mousxplc) && (Engine.board.wall[i].y == mousyplc))
                                    if ((Engine.board.wall[Engine.board.wall[i].point2].x == Engine.board.wall[newnumwalls - 1].x) && (Engine.board.wall[Engine.board.wall[i].point2].y == Engine.board.wall[newnumwalls - 1].y))
										bad = 1;
                                if ((Engine.board.wall[i].x == Engine.board.wall[newnumwalls - 1].x) && (Engine.board.wall[i].y == Engine.board.wall[newnumwalls - 1].y))
                                    if ((Engine.board.wall[Engine.board.wall[i].point2].x == mousxplc) && (Engine.board.wall[Engine.board.wall[i].point2].y == mousyplc))
										bad = 1;
							}
						}

						if (bad == 0)
						{
                            Engine.board.wall[newnumwalls] = null;
                          //  GC.Collect();
                            Engine.board.wall[newnumwalls] = new walltype();
                            Engine.board.wall[newnumwalls].extra = -1;

                            Engine.board.wall[newnumwalls].x = mousxplc;
                            Engine.board.wall[newnumwalls].y = mousyplc;
                            Engine.board.wall[newnumwalls].nextsector = -1;
                            Engine.board.wall[newnumwalls].nextwall = -1;
							for(i=0;i<numwalls;i++)
                                if ((Engine.board.wall[i].x == mousxplc) && (Engine.board.wall[i].y == mousyplc))
									suckwall = i;
                            Engine.board.wall[newnumwalls].point2 = (short)(newnumwalls + 1);
							newnumwalls++;
						}
						else
						{
							printmessage16("You can't draw new lines over red lines.");
						}
					}
				}

					//if not split and back to first point
// jv - else if
				else if ((split == 0) /*&& (firstx == mousxplc) && (firsty == mousyplc)*/ && (newnumwalls >= numwalls+3))
// jv end
				{
                    Engine.board.wall[newnumwalls - 1].point2 = (short)numwalls;

					if (suckwall == -1)  //if no connections to other sectors
					{
						k = -1;
						for(i=0;i<numsectors;i++)
                            if (Engine.board.inside(firstx, firsty, (short)i) == 1)
								k = i;
						if (k == -1)   //if not inside another sector either
						{              //add island sector
                            if (Engine.board.clockdir(numwalls) == 1)
								EditorLib.flipwalls(numwalls,(short)newnumwalls);

                            Engine.board.sector[numsectors] = null;
                          //  GC.Collect();
                            Engine.board.sector[numsectors] = new sectortype();
                            Engine.board.sector[numsectors].extra = -1;

                            Engine.board.sector[numsectors].wallptr = numwalls;
                            Engine.board.sector[numsectors].wallnum = (short)(newnumwalls - numwalls);
                            Engine.board.sector[numsectors].ceilingz = (-32 << 8);
                            Engine.board.sector[numsectors].floorz = (32 << 8);
							for(i=numwalls;i<newnumwalls;i++)
							{
                                Engine.board.wall[i].cstat = 0;
                                Engine.board.wall[i].shade = 0;
                                Engine.board.wall[i].yrepeat = 8;
								EditorLib.fixrepeats((short)i);
                                Engine.board.wall[i].picnum = 0;
                                Engine.board.wall[i].overpicnum = 0;
                                Engine.board.wall[i].nextsector = -1;
                                Engine.board.wall[i].nextwall = -1;
							}
                            Engine.board.headspritesect[numsectors] = -1;
							numsectors++;
                            
						}
						else       //else add loop to sector
						{
							if (Engine.board.clockdir(numwalls) == 0)
								EditorLib.flipwalls(numwalls,(short)newnumwalls);

							j = newnumwalls-numwalls;

                            Engine.board.sector[k].wallnum += (short)j;
							for(i=k+1;i<numsectors;i++)
                                Engine.board.sector[i].wallptr += (short)j;
                            suckwall = Engine.board.sector[k].wallptr;

							for(i=0;i<numwalls;i++)
							{
                                if (Engine.board.wall[i].nextwall >= suckwall)
                                    Engine.board.wall[i].nextwall += (short)j;
                                if (Engine.board.wall[i].point2 >= suckwall)
                                    Engine.board.wall[i].point2 += (short)j;
							}

                            for (i = newnumwalls - 1; i >= suckwall; i--)
                            {
                                //memcpy(&wall[i+j],&wall[i],sizeof(walltype));
                                Engine.board.wall[i].copyto(ref Engine.board.wall[i + j]);
                            }
                            for (i = 0; i < j; i++)
                            {
                                //memcpy(&wall[i + suckwall], &wall[i + newnumwalls], sizeof(walltype));
                                Engine.board.wall[i + newnumwalls].copyto(ref Engine.board.wall[i + suckwall]);
                            }

							for(i=suckwall;i<suckwall+j;i++)
							{
                                Engine.board.wall[i].point2 += (short)(suckwall - numwalls);

                                Engine.board.wall[i].cstat = Engine.board.wall[suckwall + j].cstat;
                                Engine.board.wall[i].shade = Engine.board.wall[suckwall + j].shade;
                                Engine.board.wall[i].yrepeat = Engine.board.wall[suckwall + j].yrepeat;
								EditorLib.fixrepeats((short)i);
                                Engine.board.wall[i].picnum = Engine.board.wall[suckwall + j].picnum;
                                Engine.board.wall[i].overpicnum = Engine.board.wall[suckwall + j].overpicnum;

                                Engine.board.wall[i].nextsector = -1;
                                Engine.board.wall[i].nextwall = -1;
							}
						}
					}
					else
					{
						  //add new sector with connections
						if (Engine.board.clockdir(numwalls) == 1)
							EditorLib.flipwalls(numwalls,(short)newnumwalls);

						//clearbufbyte(FP_OFF(&sector[numsectors]),sizeof(sectortype),0L);
                        Engine.board.sector[numsectors] = null;
                   //     GC.Collect();
                        Engine.board.sector[numsectors] = new sectortype();
                        Engine.board.sector[numsectors].extra = -1;

                        Engine.board.sector[numsectors].wallptr = numwalls;
                        Engine.board.sector[numsectors].wallnum = (short)(newnumwalls - numwalls);
                        sucksect = (short)Engine.board.sectorofwall((short)suckwall);
                        Engine.board.sector[numsectors].ceilingstat = Engine.board.sector[sucksect].ceilingstat;
                        Engine.board.sector[numsectors].floorstat = Engine.board.sector[sucksect].floorstat;
                        Engine.board.sector[numsectors].ceilingxpanning = Engine.board.sector[sucksect].ceilingxpanning;
                        Engine.board.sector[numsectors].floorxpanning = Engine.board.sector[sucksect].floorxpanning;
                        Engine.board.sector[numsectors].ceilingshade = Engine.board.sector[sucksect].ceilingshade;
                        Engine.board.sector[numsectors].floorshade = Engine.board.sector[sucksect].floorshade;
                        Engine.board.sector[numsectors].ceilingz = Engine.board.sector[sucksect].ceilingz;
                        Engine.board.sector[numsectors].floorz = Engine.board.sector[sucksect].floorz;
                        Engine.board.sector[numsectors].ceilingpicnum = Engine.board.sector[sucksect].ceilingpicnum;
                        Engine.board.sector[numsectors].floorpicnum = Engine.board.sector[sucksect].floorpicnum;
                        Engine.board.sector[numsectors].ceilingheinum = Engine.board.sector[sucksect].ceilingheinum;
                        Engine.board.sector[numsectors].floorheinum = Engine.board.sector[sucksect].floorheinum;
						for(i=numwalls;i<newnumwalls;i++)
						{
                            Engine.board.wall[i].cstat = Engine.board.wall[suckwall].cstat;
                            Engine.board.wall[i].shade = Engine.board.wall[suckwall].shade;
                            Engine.board.wall[i].yrepeat = Engine.board.wall[suckwall].yrepeat;
							EditorLib.fixrepeats((short)i);
                            Engine.board.wall[i].picnum = Engine.board.wall[suckwall].picnum;
                            Engine.board.wall[i].overpicnum = Engine.board.wall[suckwall].overpicnum;
							EditorLib.checksectorpointer((short)i,(short)numsectors);
						}
                        Engine.board.headspritesect[numsectors] = -1;
						numsectors++;
                        
					}
					numwalls = (short)newnumwalls;
					newnumwalls = -1;
					//asksave = 1;
				}
				if (split == 1)
				{
						 //split sector
					startwall = Engine.board.sector[splitsect].wallptr;
                    endwall = startwall + Engine.board.sector[splitsect].wallnum - 1;
					for(k=startwall;k<=endwall;k++)
                        if (Engine.board.wall[k].x == Engine.board.wall[newnumwalls - 1].x)
                            if (Engine.board.wall[k].y == Engine.board.wall[newnumwalls - 1].y)
							{
								bad = 0;
                                if (Engine.board.loopnumofsector((short)splitsect, (short)splitstartwall) != Engine.board.loopnumofsector((short)splitsect, (short)k))
									bad = 1;

								if (bad == 0)
								{
									//SPLIT IT!
									//Split splitsect given: startwall,
									//   new points: numwalls to newnumwalls-2

									splitendwall = k;
									newnumwalls--;  //first fix up the new walls
									for(i=numwalls;i<newnumwalls;i++)
									{
                                        Engine.board.wall[i].cstat = Engine.board.wall[startwall].cstat;
                                        Engine.board.wall[i].shade = Engine.board.wall[startwall].shade;
                                        Engine.board.wall[i].yrepeat = Engine.board.wall[startwall].yrepeat;
										EditorLib.fixrepeats((short)i);
                                        Engine.board.wall[i].picnum = Engine.board.wall[startwall].picnum;
                                        Engine.board.wall[i].overpicnum = Engine.board.wall[startwall].overpicnum;

                                        Engine.board.wall[i].nextwall = -1;
                                        Engine.board.wall[i].nextsector = -1;
                                        Engine.board.wall[i].point2 = (short)(i + 1);
									}

									danumwalls = newnumwalls;  //where to add more walls
									m = splitendwall;          //copy rest of loop next
									while (m != splitstartwall)
									{
										//memcpy(&wall[danumwalls],&wall[m],sizeof(walltype));

                                        Engine.board.wall[m].copyto(ref Engine.board.wall[danumwalls]);

                                        Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
										danumwalls++;
                                        m = Engine.board.wall[m].point2;
									}
                                    Engine.board.wall[danumwalls - 1].point2 = numwalls;

										//Add other loops for 1st sector
                                    loopnum = Engine.board.loopnumofsector((short)splitsect, (short)splitstartwall);
									i = loopnum;
									for(j=startwall;j<=endwall;j++)
									{
                                        k = Engine.board.loopnumofsector((short)splitsect, (short)j);
										if ((k != i) && (k != loopnum))
										{
											i = k;
                                            if (Engine.board.loopinside(Engine.board.wall[j].x, Engine.board.wall[j].y, (short)numwalls) == 1)
											{
												m = j;          //copy loop
												k = danumwalls;
												do
												{
													//memcpy(&wall[danumwalls],&wall[m],sizeof(walltype));
                                                    Engine.board.wall[m].copyto(ref Engine.board.wall[danumwalls]);
                                                    Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
													danumwalls++;
                                                    m = Engine.board.wall[m].point2;
												}
												while (m != j);
                                                Engine.board.wall[danumwalls - 1].point2 = (short)k;
											}
										}
									}

									secondstartwall = danumwalls;
										//copy split points for other sector backwards
									for(j=newnumwalls;j>numwalls;j--)
									{
										//memcpy(&wall[danumwalls],&wall[j],sizeof(walltype));
                                        Engine.board.wall[j].copyto(ref Engine.board.wall[danumwalls]);
                                        Engine.board.wall[danumwalls].nextwall = -1;
                                        Engine.board.wall[danumwalls].nextsector = -1;
                                        Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
										danumwalls++;
									}
									m = splitstartwall;     //copy rest of loop next
									while (m != splitendwall)
									{
										//memcpy(&wall[danumwalls],&wall[m],sizeof(walltype));
                                        Engine.board.wall[m].copyto(ref Engine.board.wall[danumwalls]);
                                        Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
										danumwalls++;
                                        m = Engine.board.wall[m].point2;
									}
                                    Engine.board.wall[danumwalls - 1].point2 = (short)secondstartwall;

										//Add other loops for 2nd sector
                                    loopnum = Engine.board.loopnumofsector((short)splitsect, (short)splitstartwall);
									i = loopnum;
									for(j=startwall;j<=endwall;j++)
									{
                                        k = Engine.board.loopnumofsector((short)splitsect, (short)j);
										if ((k != i) && (k != loopnum))
										{
											i = k;
                                            if (Engine.board.loopinside(Engine.board.wall[j].x, Engine.board.wall[j].y, (short)secondstartwall) == 1)
											{
												m = j;          //copy loop
												k = danumwalls;
												do
												{
													//memcpy(&wall[danumwalls],&wall[m],sizeof(walltype));
                                                    Engine.board.wall[m].copyto(ref Engine.board.wall[danumwalls]);
                                                    Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
													danumwalls++;
                                                    m = Engine.board.wall[m].point2;
												}
												while (m != j);
                                                Engine.board.wall[danumwalls - 1].point2 = (short)k;
											}
										}
									}

										//fix all next pointers on old sector line
									for(j=numwalls;j<danumwalls;j++)
									{
                                        if (Engine.board.wall[j].nextwall >= 0)
										{
                                            Engine.board.wall[Engine.board.wall[j].nextwall].nextwall = (short)j;
											if (j < secondstartwall)
                                                Engine.board.wall[Engine.board.wall[j].nextwall].nextsector = numsectors;
											else
                                                Engine.board.wall[Engine.board.wall[j].nextwall].nextsector = (short)(numsectors + 1);
										}
									}
										//set all next pointers on split
									for(j=numwalls;j<newnumwalls;j++)
									{
										m = secondstartwall+(newnumwalls-1-j);
                                        Engine.board.wall[j].nextwall = (short)m;
                                        Engine.board.wall[j].nextsector = (short)(numsectors + 1);
                                        Engine.board.wall[m].nextwall = (short)j;
                                        Engine.board.wall[m].nextsector = numsectors;
									}
										//copy sector attributes & fix wall pointers
									//memcpy(&sector[numsectors],&sector[splitsect],sizeof(sectortype));
									//memcpy(&sector[numsectors+1],&sector[splitsect],sizeof(sectortype));
                                    Engine.board.sector[splitsect].copyto(ref Engine.board.sector[numsectors]);
                                    Engine.board.sector[splitsect].copyto(ref Engine.board.sector[numsectors+1]);

                                    Engine.board.sector[numsectors].wallptr = numwalls;
                                    Engine.board.sector[numsectors].wallnum = (short)(secondstartwall - numwalls);
                                    Engine.board.sector[numsectors + 1].wallptr = (short)(secondstartwall);
                                    Engine.board.sector[numsectors + 1].wallnum = (short)(danumwalls - secondstartwall);

										//fix sprites
                                    j = Engine.board.headspritesect[splitsect];
									while (j != -1)
									{
                                        k = Engine.board.nextspritesect[j];
                                        if (Engine.board.loopinside(Engine.board.sprite[j].x, Engine.board.sprite[j].y, numwalls) == 1)
                                            Engine.board.changespritesect((short)j, numsectors);
										//else if (loopinside(sprite[j].x,sprite[j].y,secondstartwall) == 1)
										else  //Make sure no sprites get left out & deleted!
                                            Engine.board.changespritesect((short)j, (short)(numsectors + 1));
										j = k;
									}

									numsectors+=2;

										//Back of number of walls of new sector for later
									k = danumwalls-numwalls;

										//clear out old sector's next pointers for clean deletesector
									numwalls = (short)danumwalls;
									for(j=startwall;j<=endwall;j++)
									{
										Engine.board.wall[j].nextwall = -1;
                                        Engine.board.wall[j].nextsector = -1;
									}
									EditorLib.deletesector((short)splitsect);

										//Check pointers
									for(j=numwalls-k;j<numwalls;j++)
									{
                                        if (Engine.board.wall[j].nextwall >= 0)
                                            EditorLib.checksectorpointer(Engine.board.wall[j].nextwall, Engine.board.wall[j].nextsector);
                                        EditorLib.checksectorpointer((short)j, (short)Engine.board.sectorofwall((short)j));
									}

										//k now safe to use as temp

									for(m=numsectors-2;m<numsectors;m++)
									{
                                        j = Engine.board.headspritesect[m];
										while (j != -1)
										{
                                            k = Engine.board.nextspritesect[j];
                                            Engine.board.setsprite((short)j, Engine.board.sprite[j].x, Engine.board.sprite[j].y, Engine.board.sprite[j].z);
											j = k;
										}
									}

									newnumwalls = -1;
									printmessage16("Sector split.");
									break;
								}
								else
								{
										//Sector split - actually loop joining

									splitendwall = k;
									newnumwalls--;  //first fix up the new walls
									for(i=numwalls;i<newnumwalls;i++)
									{
                                        Engine.board.wall[i].cstat = Engine.board.wall[startwall].cstat;
                                        Engine.board.wall[i].shade = Engine.board.wall[startwall].shade;
                                        Engine.board.wall[i].yrepeat = Engine.board.wall[startwall].yrepeat;
										EditorLib.fixrepeats((short)i);
                                        Engine.board.wall[i].picnum = Engine.board.wall[startwall].picnum;
                                        Engine.board.wall[i].overpicnum = Engine.board.wall[startwall].overpicnum;

                                        Engine.board.wall[i].nextwall = -1;
                                        Engine.board.wall[i].nextsector = -1;
                                        Engine.board.wall[i].point2 = (short)(i + 1);
									}

									danumwalls = newnumwalls;  //where to add more walls
									m = splitendwall;          //copy rest of loop next
									do
									{
										//memcpy(&wall[danumwalls],&wall[m],sizeof(walltype));
                                        Engine.board.wall[m].copyto(ref Engine.board.wall[danumwalls]);
                                        Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
										danumwalls++;
                                        m = Engine.board.wall[m].point2;
									} while (m != splitendwall);

									//copy split points for other sector backwards
									for(j=newnumwalls;j>numwalls;j--)
									{
										//memcpy(&wall[danumwalls],&wall[j],sizeof(walltype));
                                        Engine.board.wall[j].copyto(ref Engine.board.wall[danumwalls]);
                                        Engine.board.wall[danumwalls].nextwall = -1;
                                        Engine.board.wall[danumwalls].nextsector = -1;
                                        Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
										danumwalls++;
									}

									m = splitstartwall;     //copy rest of loop next
									do
									{
										//memcpy(&wall[danumwalls],&wall[m],sizeof(walltype));
                                        Engine.board.wall[m].copyto(ref Engine.board.wall[danumwalls]);
                                        Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
										danumwalls++;
                                        m = Engine.board.wall[m].point2;
									} while (m != splitstartwall);
                                    Engine.board.wall[danumwalls - 1].point2 = numwalls;

										//Add other loops to sector
                                    loopnum = Engine.board.loopnumofsector((short)splitsect, (short)splitstartwall);
									i = loopnum;
									for(j=startwall;j<=endwall;j++)
									{
                                        k = Engine.board.loopnumofsector((short)splitsect, (short)j);
                                        if ((k != i) && (k != Engine.board.loopnumofsector((short)splitsect, (short)splitstartwall)) && (k != Engine.board.loopnumofsector((short)splitsect, (short)splitendwall)))
										{
											i = k;
											m = j; k = danumwalls;     //copy loop
											do
											{
												//memcpy(&wall[danumwalls],&wall[m],sizeof(walltype));
                                                Engine.board.wall[m].copyto(ref Engine.board.wall[danumwalls]);
                                                Engine.board.wall[danumwalls].point2 = (short)(danumwalls + 1);
												danumwalls++;
                                                m = Engine.board.wall[m].point2;
											} while (m != j);
                                            Engine.board.wall[danumwalls - 1].point2 = (short)k;
										}
									}

										//fix all next pointers on old sector line
									for(j=numwalls;j<danumwalls;j++)
									{
                                        if (Engine.board.wall[j].nextwall >= 0)
										{
                                            Engine.board.wall[Engine.board.wall[j].nextwall].nextwall = (short)j;
                                            Engine.board.wall[Engine.board.wall[j].nextwall].nextsector = numsectors;
										}
									}

										//copy sector attributes & fix wall pointers
									//memcpy(&sector[numsectors],&sector[splitsect],sizeof(sectortype));
                                    Engine.board.sector[splitsect].copyto(ref Engine.board.sector[numsectors]);
                                    Engine.board.sector[numsectors].wallptr = numwalls;
                                    Engine.board.sector[numsectors].wallnum = (short)(danumwalls - numwalls);

										//fix sprites
                                    j = Engine.board.headspritesect[splitsect];
									while (j != -1)
									{
                                        k = Engine.board.nextspritesect[j];
                                        Engine.board.changespritesect((short)j, numsectors);
										j = k;
									}

									numsectors++;
                                    
										//Back of number of walls of new sector for later
									k = danumwalls-numwalls;

										//clear out old sector's next pointers for clean deletesector
									numwalls = (short)danumwalls;
									for(j=startwall;j<=endwall;j++)
									{
                                        Engine.board.wall[j].nextwall = -1;
                                        Engine.board.wall[j].nextsector = -1;
									}
									EditorLib.deletesector((short)splitsect);

										//Check pointers
									for(j=numwalls-k;j<numwalls;j++)
									{
                                        if (Engine.board.wall[j].nextwall >= 0)
                                            EditorLib.checksectorpointer(Engine.board.wall[j].nextwall, Engine.board.wall[j].nextsector);
                                        EditorLib.checksectorpointer((short)j, (short)(numsectors - 1));
									}

									newnumwalls = -1;
									printmessage16("Loops joined.");
									break;
								}
							}
				}
			}
        }

        

        

        private void overheadeditor()
        {
            linehighlight = EditorLib.getlinehighlight(mousxplc, mousyplc);

            if (dragpoint != null)
            {
                for (int i = 0; i < dragpoint.Count; i++)
                {
                    dragpoint[i].x = mousxplc;
                    dragpoint[i].y = mousyplc;
                }
            }

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
            if (inbuildmenu)
            {
                printmessage16("(N)ew, (L)oad, (S)ave as");
            }
            else if (inloadmenu)
            {
                printmessage16("Board Name: " + loadmenuname + "_");
            }
            else
            {
                printmessage16("WebBuild: By Justin Marshall v1");
            }
	        Engine.drawline16(0,Engine._device.ydim-1-24,Engine._device.xdim-1,Engine._device.ydim-1-24,7);
	        Engine.drawline16(256,Engine._device.ydim-1-24,256,Engine._device.ydim-1,7);
	        Engine.ydim16 = Engine.ydim-STATUS2DSIZ;
	        Engine._device.EndDrawing();

            printcoords16(posx, posy, ang);

            short onumwalls = numwalls;
            if (newnumwalls > numwalls)
            {
                numwalls = (short)(newnumwalls);
            }


            if (newnumwalls > 0)
            {

                

                if (Engine.board.wall[newnumwalls] == null)
                {
                    Engine.board.wall[newnumwalls - 1].copyto(ref Engine.board.wall[newnumwalls]);
                }

                Engine.board.wall[newnumwalls].x = mousxplc;
                Engine.board.wall[newnumwalls].y = mousyplc;
            }

            Engine.clear2dscreen();
            Engine.draw2dgrid(posx, posy, ang, zoom, grid);

            Engine.board.draw2dscreen(posx, posy, ang, zoom, grid);

            numwalls = onumwalls;

            
            
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
            if (cursectnum == -1)
                return;
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
                int picnum = Engine.board.sprite[hitsprite].picnum;
                Engine.printext16(0, ypos + 0, 15, -1, "Sprite: " + hitsprite + " Stats", 0);
                Engine.printext16(0, ypos + 15, 15, -1, "Hitag: " + Engine.board.sprite[hitsprite].hitag, 0);
                Engine.printext16(0, ypos + 25, 15, -1, "Lotag: " + Engine.board.sprite[hitsprite].lotag, 0);
                Engine.printext16(0, ypos + 35, 15, -1, "Picnum: " + Engine.board.sprite[hitsprite].picnum, 0);
                Engine.printext16(0, ypos + 45, 15, -1, "Pal: " + Engine.board.sprite[hitsprite].pal, 0);
                Engine.printext16(0, ypos + 55, 15, -1, "Shade: " + Engine.board.sprite[hitsprite].shade, 0);
                Engine.rotatesprite(125 << 16, (ypos + 20) << 16, 65536, 0, Engine.board.sprite[hitsprite].picnum, Engine.board.sprite[hitsprite].shade, Engine.board.sprite[hitsprite].pal, 8 | 16, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 30);
            }
        }

        private void drawwallinfo(int hitwall, int ypos)
        {
            if (hitwall >= 0)
            {
                int picnum = Engine.board.wall[hitwall].picnum;
                Engine.printext16(0, ypos + 0, 15, -1, "Wall: " + hitwall + " Stats", 0);
                Engine.printext16(0, ypos + 15, 15, -1, "Hitag: " + Engine.board.wall[hitwall].hitag, 0);
                Engine.printext16(0, ypos + 25, 15, -1, "Lotag: " + Engine.board.wall[hitwall].lotag, 0);
                Engine.printext16(0, ypos + 35, 15, -1, "Picnum: " + Engine.board.wall[hitwall].picnum, 0);
                Engine.printext16(0, ypos + 45, 15, -1, "Pal: " + Engine.board.wall[hitwall].pal, 0);
                Engine.printext16(0, ypos + 55, 15, -1, "Shade: " + Engine.board.wall[hitwall].shade, 0);
                Engine.rotatesprite(125 << 16, (ypos + 20) << 16, 65536, 0, Engine.board.wall[hitwall].picnum, Engine.board.wall[hitwall].shade, Engine.board.wall[hitwall].pal, 8 | 16, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 30);
            }
        }

        private void draw3dcursorinfo()
        {
            
            int dax = 16384;
            int day = pragmas.divscale14(mousx2 - (Engine._device.xdim >> 1), Engine._device.xdim >> 1);
            Engine.rotatepoint(0, 0, dax, day, ang, ref dax, ref day);


            Engine.board.hitscan(posx, posy, posz, cursectnum,              //Start position
                    dax, day, (pragmas.scale(mousy2, 200, Engine._device.ydim) - 100) * 2000, //vector of 3D ang
                    ref mouseTrace.hitsector, ref mouseTrace.hitwall, ref mouseTrace.hitsprite, ref mouseTrace.hitx, ref mouseTrace.hity, ref mouseTrace.hitz, Engine.CLIPMASK1);


            if (mouseTrace.hitsprite >= 0)
            {
                drawspriteinfo(mouseTrace.hitsprite, 0);
                mouseTrace.hittype = MouseSectorHitType.MOUSE_SECTORHIT_NONE;
            }
            else if (mouseTrace.hitwall >= 0)
            {
                drawwallinfo(mouseTrace.hitwall, 0);
                mouseTrace.hittype = MouseSectorHitType.MOUSE_SECTORHIT_NONE;
            }
            else if (editorState == EditorState.STATE_3DVIEW)
            {
                if (Engine.board.getflorzofslope((short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity) <= mouseTrace.hitz)
                {
                    int picnum = Engine.board.wall[mouseTrace.hitsector].picnum;
                    Engine.printext16(0, 0, 15, -1, "Sector(floor) " + mouseTrace.hitsector + " Stats", 0);
                    Engine.printext16(0, 15, 15, -1, "Hitag: " + Engine.board.sector[mouseTrace.hitsector].hitag, 0);
                    Engine.printext16(0, 25, 15, -1, "Lotag: " + Engine.board.sector[mouseTrace.hitsector].lotag, 0);
                    Engine.printext16(0, 35, 15, -1, "Picnum: " + Engine.board.sector[mouseTrace.hitsector].floorpicnum, 0);
                    Engine.printext16(0, 45, 15, -1, "Pal: " + Engine.board.sector[mouseTrace.hitsector].floorpal, 0);
                    Engine.printext16(0, 55, 15, -1, "Shade: " + Engine.board.sector[mouseTrace.hitsector].floorshade, 0);
                    Engine.rotatesprite(125 << 16, (20) << 16, 65536, 0, Engine.board.sector[mouseTrace.hitsector].floorpicnum, Engine.board.sector[mouseTrace.hitsector].floorshade, Engine.board.sector[mouseTrace.hitsector].floorpal, 8 | 16, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 30);
                    mouseTrace.hittype = MouseSectorHitType.MOUSE_SECTORHIT_FLOOR;
                }
                else if (Engine.board.getceilzofslope((short)mouseTrace.hitsector, mouseTrace.hitx, mouseTrace.hity) >= mouseTrace.hitz)
                {
                    int picnum = Engine.board.wall[mouseTrace.hitsector].picnum;
                    Engine.printext16(0, 0, 15, -1, "Sector(ceiling) " + mouseTrace.hitsector + " Stats", 0);
                    Engine.printext16(0, 15, 15, -1, "Hitag: " + Engine.board.sector[mouseTrace.hitsector].hitag, 0);
                    Engine.printext16(0, 25, 15, -1, "Lotag: " + Engine.board.sector[mouseTrace.hitsector].lotag, 0);
                    Engine.printext16(0, 35, 15, -1, "Picnum: " + Engine.board.sector[mouseTrace.hitsector].ceilingpicnum, 0);
                    Engine.printext16(0, 45, 15, -1, "Pal: " + Engine.board.sector[mouseTrace.hitsector].ceilingpal, 0);
                    Engine.printext16(0, 55, 15, -1, "Shade: " + Engine.board.sector[mouseTrace.hitsector].ceilingshade, 0);
                    Engine.rotatesprite(125 << 16, (20) << 16, 65536, 0, Engine.board.sector[mouseTrace.hitsector].ceilingpicnum, Engine.board.sector[mouseTrace.hitsector].ceilingshade, Engine.board.sector[mouseTrace.hitsector].ceilingpal, 8 | 16, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 30);

                    mouseTrace.hittype = MouseSectorHitType.MOUSE_SECTORHIT_CEILING;
                }
            }
            else
            {
                mouseTrace.hittype = MouseSectorHitType.MOUSE_SECTORHIT_NONE;
            }
        }

        private void draw3dview()
        {
            Engine.board.drawrooms(posx, posy, posz  - 768, ang, 100, cursectnum);
            Engine.board.drawmasks();


            draw3dcursorinfo();
        }

        public void Frame()
        {
            int topleft = 0, tilenum = 0;

            if(cursectnum >= 0)
                MoveViewer();

            switch (editorState)
            {
                case EditorState.STATE_2DVIEW:
                    overheadeditor();
                    break;

                case EditorState.STATE_3DVIEW:
                    draw3dview();
                    break;

                case EditorState.STATE_TILESELECT:
                    {
                        int xtiles = (Engine._device.xdim >> 6);
                        int ytiles = (Engine._device.ydim >> 6);

                        if (localartlookupnum < 0)
                            localartlookupnum = 0;

                        if (localartlookupnum >= bMap.MAXTILES)
                            localartlookupnum = bMap.MAXTILES;

                        tilenum = localartlookupnum;
                        topleft = ((tilenum / (xtiles << gettilezoom)) * (xtiles << gettilezoom)) - (xtiles << gettilezoom);
                        drawtilescreen(topleft, tilenum);
                    }
                    break;

                default:
                    throw new Exception("Unknown EditorState");
            }

            ChangeObjectZ(objzvel);   

            showmouse();

            Engine.NextPage();
        }
    }
}
