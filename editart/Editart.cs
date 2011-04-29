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

namespace editart
{
    public class Editart
    {
        int gettilezoom = 1, localartlookupnum = 0;
        int edittileselect = -1;
        int panyoff = 0;
        int panxdim = 1024;
        byte[] blackmasklookup;
        byte[] previmage;

        public int SelectedArtTile
        {
            get
            {
                return localartlookupnum;
            }
        }

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

            blackmasklookup = Engine.palette.palookup;
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
            for (cnt = 0; cnt < (tottiles << (gettilezoom << 1)); cnt++)         //draw the 5*3 grid of tiles
            {
                wallnum = cnt + pictopleft;
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

                    dax = ((cnt % (xtiles << gettilezoom)) << (6 - gettilezoom));
                    day = ((cnt / (xtiles << gettilezoom)) << (6 - gettilezoom));
                    vidpos = Engine.ylookup[day] + dax + Engine.frameplace;
                    if ((xdime <= (64 >> gettilezoom)) && (ydime <= (64 >> gettilezoom)))
                    {
                        for (i = 0; i < xdime; i++)
                        {
                            vidpos2 = vidpos + i;
                            for (j = 0; j < ydime; j++)
                            {
                                A.DrawPixelPallete(vidpos2, picptr[picptrpos++]);
                                vidpos2 += pinc;
                            }
                        }
                    }
                    else                          //if 1 dimension > 64
                    {
                        if (xdime > ydime)
                            scaledown = ((xdime + (63 >> gettilezoom)) >> (6 - gettilezoom));
                        else
                            scaledown = ((ydime + (63 >> gettilezoom)) >> (6 - gettilezoom));

                        for (i = 0; i < xdime; i += scaledown)
                        {
                            if (Engine.waloff[wallnum] == null) Engine.loadtile((short)wallnum);
                            picptr = Engine.waloff[wallnum].memory;
                            picptrpos = ydime * i;
                            vidpos2 = vidpos;

                            for (j = 0; j < ydime; j += scaledown)
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
                        dax = ((cnt % (xtiles << gettilezoom)) << (6 - gettilezoom));
                        day = ((cnt / (xtiles << gettilezoom)) << (6 - gettilezoom));
                        //  sprintf(snotbuf,"%ld",localartfreq[cnt+pictopleft]);
                        //  printext256(dax,day,whitecol,-1,snotbuf,1);
                    }
                }
                //  }
            }

            cnt = picbox - pictopleft;    //draw open white box
            dax = ((cnt % (xtiles << gettilezoom)) << (6 - gettilezoom));
            day = ((cnt / (xtiles << gettilezoom)) << (6 - gettilezoom));

            for (i = 0; i < (64 >> gettilezoom); i++)
            {
                Engine.plotpixel(dax + i, day, 15);
                Engine.plotpixel(dax + i, day + (63 >> gettilezoom), 15);
                Engine.plotpixel(dax, day + i, 15);
                Engine.plotpixel(dax + (63 >> gettilezoom), day + i, 15);
            }

            i = picbox; // localartlookup[picbox];
            //sprintf(snotbuf,"%ld",i);
            snotbuf = "" + i;
            Engine.printext256(0, Engine._device.ydim - 8, 15, -1, snotbuf, 0);
            //Engine.printext256(Engine._device.xdim - (strlen(names[i]) << 3), ydim - 8, 15, -1, names[i], 0);
        }

        struct Pixel
        {
            public byte r;
            public byte g;
            public byte b;
            public byte a;
        }

        private void GetRGBFromPixel(int pixel, ref Pixel colpixel)
        {
            byte[] bits = BitConverter.GetBytes(pixel);

            colpixel.r = bits[0];
            colpixel.g = bits[1];
            colpixel.b = bits[2];
            colpixel.a = bits[3];
        }



        private byte GetPixelFromPalette(Pixel colpixel)
        {
            int dist = int.MaxValue, pixelnum = -1;
            Pixel palpixel = new Pixel();


            for (int i = 0; i < 255; i++)
            {
                int paldist;

                GetRGBFromPixel(Engine.palette._palettebuffer[i], ref palpixel);

                paldist = 30 * (colpixel.r - palpixel.r) * (colpixel.r - palpixel.r);
                paldist += 59 * (colpixel.g - palpixel.g) * (colpixel.g - palpixel.g);
                paldist += 11 * (colpixel.b - palpixel.b) * (colpixel.b - palpixel.b);

                if (paldist < dist)
                {
                    dist = paldist;
                    pixelnum = i;
                }
            }

            return (byte)pixelnum;
        }

        public byte[] ConvertTileToImage(int tilenum, int width, int height)
        {
            byte[] imagedata;
            int imagesize;
            int readpos = 0;
            Pixel colpixel = new Pixel();

            imagesize = Engine.waloff[tilenum].memory.Length;
            imagedata = new byte[imagesize * 4];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // widthpos + (heightpos * imgwidth)
                    readpos = i + (j * width);              

                    byte col = Engine.waloff[tilenum].memory[readpos];
                    GetRGBFromPixel(Engine.palette._palettebuffer[col], ref colpixel);

                    int _rowLength = width * 4 + 1;
                    int start = _rowLength * i + j * 4 + 1;
                    imagedata[start + 2] = colpixel.r;
                    imagedata[start + 1] = colpixel.g;
                    imagedata[start + 0] = colpixel.b;
                    imagedata[start + 3] = 255;
                }
            }

            return imagedata;
        }

        public void ReplaceCurrentTile(int[] pixels, int width, int height)
        {
            Pixel colpixel = new Pixel();
            byte color = 0;
            int tilemempos = 0, tilerow=0, writepos = 0;

            previmage = Engine.waloff[localartlookupnum].memory;

            Engine.waloff[localartlookupnum].memory = null;
            Engine.waloff[localartlookupnum].memory = new byte[width * height];
            Engine.tilesizx[localartlookupnum] = (short)width;
            Engine.tilesizy[localartlookupnum] = (short)height;

            foreach (int pixel in pixels)
            {
                GetRGBFromPixel(pixel, ref colpixel);

                // If the image has an alpha less than 255 than assume its transparent.
                if (colpixel.a < 255)
                {
                    color = 255;
                }
                else
                {
                    color = GetPixelFromPalette(colpixel);
                }

                writepos = tilerow + (tilemempos * width);
                if (writepos >= Engine.waloff[localartlookupnum].memory.Length)
                {
                    tilerow++;
                    tilemempos = 0;
                    writepos = tilerow + (tilemempos * width);
                }

                Engine.waloff[localartlookupnum].memory[writepos] = color;
                tilemempos++;
            }
        }

        public void editinputkey(Key key)
        {
            if (edittileselect >= 0)
            {
                if (key == Key.Escape)
                {
                    edittileselect = -1;
                    DrawTileEditMenu = false;
                }
                return;
            }

            if (key == Key.Down)
            {
                localartlookupnum += (short)((Engine._device.xdim >> 6) * 2);
            }
            else if (key == Key.Up)
            {
                localartlookupnum -= (short)((Engine._device.xdim >> 6) * 2);
            }
            else if (key == Key.Right)
            {
                localartlookupnum++;
            }
            else if (key == Key.Left)
            {
                localartlookupnum--;
            }
            else if (key == Key.Enter)
            {
                edittileselect = localartlookupnum;
                DrawTileEditMenu = true;
            }

            if (localartlookupnum < 0)
                localartlookupnum = 0;

            if (localartlookupnum >= bMap.MAXTILES)
                localartlookupnum = bMap.MAXTILES;
        }

        private void copywalltoscreen(byte[] bufptr, int dapicnum, int picscale, byte maskmode)
        {
            int bufptrpos = 0;
            int pinc = Engine.ylookup[1];
	        int i, j, f, h;

            int xoff = (Engine._device.xdim >> 1) - (Engine.tilesizx[dapicnum] >> 1);
            int yoff = (Engine._device.ydim >> 1) - (Engine.tilesizy[dapicnum] >> 1);


            for (i = 0; i < Engine.tilesizx[dapicnum]; i++)
            {
                int vidpos2 = Engine.ylookup[yoff] + (i + xoff) * picscale;
                for (j = 0; j < Engine.tilesizy[dapicnum]; j++)
                {
                    for (f = 0; f < picscale; f++)
                    {
                        for (h = 0; h < picscale; h++)
                        {
                            A.DrawPixelPallete((vidpos2 + h), bufptr[(int)bufptrpos]);
                        }
                        vidpos2 += pinc;
                    }
                    bufptrpos++;
                }
            }
        }

        public void editinputkeyup(Key key)
        {

        }

        public bool DrawTileEditMenu { get; set; }

        public void DrawTileEditScreen()
        {
            Array.Clear(Engine._device._screenbuffer.Pixels, 0, Engine._device._screenbuffer.Pixels.Length);
            if (Engine.tilesizx[localartlookupnum] > 0 && Engine.tilesizy[localartlookupnum] > 0)
            {
                copywalltoscreen(Engine.waloff[localartlookupnum].memory, localartlookupnum, 1, 0);
            }

            Engine.printext256(0, 0, 15, 0, "Tile " + localartlookupnum + ":", 0);
            Engine.printext256(0, 15, 15, 0, "   Width: " + Engine.tilesizx[localartlookupnum], 0);
            Engine.printext256(0, 25, 15, 0, "   Height: " + Engine.tilesizy[localartlookupnum], 0);
        }

        public void Frame()
        {
            if (edittileselect == -1)
            {
                int tilenum, topleft;
                int xtiles = (Engine._device.xdim >> 6);
                int ytiles = (Engine._device.ydim >> 6);

                tilenum = localartlookupnum;
                topleft = ((tilenum / (xtiles << gettilezoom)) * (xtiles << gettilezoom)) - (xtiles << gettilezoom);
                drawtilescreen(topleft, tilenum);
            }
            else
            {
                DrawTileEditScreen();
            }

            Engine.NextPage();
        }
    }
}
