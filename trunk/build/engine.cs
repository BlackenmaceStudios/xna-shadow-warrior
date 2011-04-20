using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
namespace build
{
    public static class Engine
    {
        public const int CLIPMASK0 = (((1) << 16) + 1);
        public const int CLIPMASK1 = (((256) << 16) + 64);

        public const int MAXTILES = 9216;

        public static byte[] pow2char = new byte[]{1,2,4,8,16,32,64,128};
        public static int[] pow2long = new int[]
        {
	        1,2,4,8,
	        16,32,64,128,
	        256,512,1024,2048,
	        4096,8192,16384,32768,
	        65536,131072,262144,524288,
	        1048576,2097152,4194304,8388608,
	        16777216,33554432,67108864,134217728,
	        268435456,536870912,1073741824,2147483647,
        };

        public class tilecontainer
        {
            public byte[] memory;
        }

        private static int randomseed = 17;

        public static short[] tilesizx = new short[MAXTILES];
        public static short[] tilesizy = new short[MAXTILES];
        public static int[] picanm = new int[MAXTILES];
        internal static int[] lookups;
        public static int horizlookup;
        public static int horizlookup2;
        public static tilecontainer[] waloff = new tilecontainer[MAXTILES];
        private static short[] uplc = new short[VgaDevice.MAXXDIM];
        private static short[] dplc = new short[VgaDevice.MAXYDIM];

        private static kFile artfil;
        private static int artfilplc = -1;
        private static int artfilnum = -1;
        private static string artfilename;

        private static int totalclocklock = 0;

        private static short[] tilefilenum = new short[MAXTILES];
        private static int[] tilefileoffs = new int[MAXTILES];
        private static int artsize = 0;
        private static short numtilefiles = 0;
        public static byte[] picsiz = new byte[MAXTILES];

        public static int horizycent, oxyaspect, oxdimen, oviewingrange;

        internal static VgaDevice _device = new VgaDevice();

        public static kFileSystem filesystem;
        public static bTable table;
        public static bPalette palette;

        public static bool beforedrawrooms = false;

        public static int qsetmode = 0;
        public static int pageoffset = 0;
        public static int ydim16 = 0;

        public static int[] ylookup = new int[VgaDevice.MAXYDIM+1];
        public static int[] tiletovox = new int[MAXTILES];
        public static int[] voxscale = new int[MAXTILES];


        public static int frameplace;
        public static int searchx = 0, searchy=0;
        public static int searchit = 0;
        public static int searchsector, searchwall, searchstat;

        public static bMap board;
        public static bool novoxmips = false;

        private static int curbrightness = 0;

        public const int MAXVOXMIPS = 5;
#region voxformatdoc
/*
 * Each KVX file uses this structure for each of its mip-map levels:
   long xsiz, ysiz, zsiz, xpivot, ypivot, zpivot;
   long xoffset[xsiz+1];
   short xyoffset[xsiz][ysiz+1];
   char rawslabdata[?];

The file can be loaded like this:
   if ((fil = open("?.kvx",O_BINARY|O_RDWR,S_IREAD)) == -1) return(0);
   nummipmaplevels = 1;  //nummipmaplevels = 5 for unstripped KVX files
   for(i=0;i<nummipmaplevels;i++)
   {
      read(fil,&numbytes,4);
      read(fil,&xsiz,4);
      read(fil,&ysiz,4);
      read(fil,&zsiz,4);
      read(fil,&xpivot,4);
      read(fil,&ypivot,4);
      read(fil,&zpivot,4);
      read(fil,xoffset,(xsiz+1)*4);
      read(fil,xyoffset,xsiz*(ysiz+1)*2);
      read(fil,voxdata,numbytes-24-(xsiz+1)*4-xsiz*(ysiz+1)*2);
   }
   read(fil,palette,768);

numbytes: Total # of bytes (not including numbytes) in each mip-map level

xsiz, ysiz, zsiz: Dimensions of voxel. (zsiz is height)

xpivot, ypivot, zpivot: Centroid of voxel.  For extra precision, this
   location has been shifted up by 8 bits.

xoffset, xyoffset: For compression purposes, I store the column pointers
   in a way that offers quick access to the data, but with slightly more
   overhead in calculating the positions.  See example of usage in voxdata.
   NOTE: xoffset[0] = (xsiz+1)*4 + xsiz*(ysiz+1)*2 (ALWAYS)

voxdata: stored in sequential format.  Here's how you can get pointers to
   the start and end of any (x, y) column:

      //pointer to start of slabs on column (x, y):
   startptr = &voxdata[xoffset[x] + xyoffset[x][y]];

      //pointer to end of slabs on column (x, y):
   endptr = &voxdata[xoffset[x] + xyoffset[x][y+1]];

   Note: endptr is actually the first piece of data in the next column

   Once you get these pointers, you can run through all of the "slabs" in
   the column.  Each slab has 3 bytes of header, then an array of colors.
   Here's the format:

   char slabztop;             //Starting z coordinate of top of slab
   char slabzleng;            //# of bytes in the color array - slab height
   char slabbackfacecullinfo; //Low 6 bits tell which of 6 faces are exposed
   char col[slabzleng];       //The array of colors from top to bottom

palette:
   The last 768 bytes of the KVX file is a standard 256-color VGA palette.
   The palette is in (Red:0, Green:1, Blue:2) order and intensities range
   from 0-63.

   Note: To keep this ZIP size small, I have stripped out the lower mip-map
   levels.  KVX files from Shadow Warrior or Blood include this data.  To
   get the palette data, I recommend seeking 768 bytes before the end of the
   KVX file.
 */
#endregion
        internal struct bVoxelMipmap
        {
            public int numbytes;
            public int xsiz; 
            public int ysiz; 
            public int zsiz;
	        public int xpivot; 
            public int ypivot;
            public int zpivot;
            public int[] xoffset; // (xsiz+1);
            public short[,] xyoffset; // xsiz*(ysiz+1)
            public byte[] voxdata; // numbytes-24-(xsiz+1)*4-xsiz*(ysiz+1)
        }

        internal class bVoxel
        {
            public int nummipmaps = 0;
            public bVoxelMipmap[] mipmaps = new bVoxelMipmap[MAXVOXMIPS];

            public void AddMipMap(ref kFile fil)
            {
                bVoxelMipmap mipmaplevel = new bVoxelMipmap();
                mipmaplevel.numbytes = fil.kreadint();
                mipmaplevel.xsiz = fil.kreadint();
                mipmaplevel.ysiz = fil.kreadint();
                mipmaplevel.zsiz = fil.kreadint();

                mipmaplevel.xpivot = fil.kreadint();
                mipmaplevel.ypivot = fil.kreadint();
                mipmaplevel.zpivot = fil.kreadint();

                mipmaplevel.xoffset = new int[(mipmaplevel.xsiz + 1) << 2];
                for (int i = 0; i < mipmaplevel.xoffset.Length; i++)
                {
                    mipmaplevel.xoffset[i] = fil.kreadint();
                }

                mipmaplevel.xyoffset = new short[mipmaplevel.xsiz, (mipmaplevel.ysiz + 1) << 1];
                for (int i = 0; i < mipmaplevel.xsiz; i++)
                {
                    for (int d = 0; d < (mipmaplevel.ysiz + 1) << 1; d++)
                    {
                        mipmaplevel.xyoffset[i,d] = fil.kreadshort();
                    }
                }

                mipmaplevel.voxdata = fil.kread(mipmaplevel.numbytes - 24 - (mipmaplevel.xsiz + 1) * 4 - mipmaplevel.xsiz * (mipmaplevel.ysiz + 1) * 2);

                mipmaps[nummipmaps++] = mipmaplevel; 
            }
        }

        internal static bVoxel[] voxoff = new bVoxel[MAXTILES];

        public static bool EditStatus
        {
            get
            {
                return false;
            }
        }


        public static int viewingrange { get { return _device.viewingrange; }}
        public static int viewingrangerecip { get { return _device.viewingrangerecip; } }
        public static int yxaspect { get { return _device.yxaspect; } }
        public static int xyaspect { get { return _device.xyaspect; } }
        public static int xdimenscale { get { return _device.xdimenscale; } }
        public static int xdimscale { get { return _device.xdimscale; } }
        public static int xdimen { get { return _device.xdimen; } }
        public static int ydimen { get { return _device.ydimen; } } 
        public static int halfxdimen { get { return _device.halfxdimen; } }
        public static int xdimenrecip { get { return _device.xdimenrecip; } }
        public static int bytesperline { get { return _device.bytesperline; } }

        public static int[] ggxinc = new int[256+1];
        public static int[] ggyinc = new int[256 + 1];
        public static int[] lowrecip = new int[1024];
        public static int nytooclose, nytoofar;

        public static int windowx2
        {
            get
            {
                return _device.windowx2;
            }
        }

        public static int windowy2
        {
            get
            {
                return _device.windowy2;
            }
        }

        public static int windowy1
        {
            get
            {
                return _device.windowy1;
            }
        }

        public static int windowx1
        {
            get
            {
                return _device.windowx1;
            }
        }

        public static void BeginDrawing()
        {
            _device.BeginDrawing();
        }

        public static void EndDrawing()
        {
            _device.EndDrawing();
        }

        public static void initgroupfile(string filename)
        {
            filesystem.InitGrpFile(filename);
        }

        public static int getangle(int xvect, int yvect)
        {
            int xvectshift = 0, yvectshift = 0;
            int index;

            if (xvect < 0)
                xvectshift = 1 << 10;

            if (yvect < 0)
                yvectshift = 1 << 10;

            if ((xvect | yvect) == 0) return (0);
            if (xvect == 0) return (512 + (yvectshift));
            if (yvect == 0) return ((xvectshift));
            if (xvect == yvect) return (256 + (xvectshift));
            if (xvect == -yvect) return (768 + (xvectshift));
            if (pragmas.klabs(xvect) > pragmas.klabs(yvect))
            {
                index = 640 + (int)pragmas.scale(160, yvect, xvect);
                return (((table.radarang[index] >> 6) + (xvectshift)) & 2047);
            }

            index = 640 - (int)pragmas.scale(160, xvect, yvect);
            return (((table.radarang[index] >> 6) + 512 + (yvectshift)) & 2047);
        }

        //
        // animateoffs (internal)
        //
        public static int animateoffs(short tilenum, short fakevar)
        {
            int i, k, offs;

            offs = 0;
            i = (totalclocklock >> (((int)picanm[tilenum] >> 24) & 15));
            if (((int)picanm[tilenum] & 63) > 0)
            {
                switch ((int)picanm[tilenum] & 192)
                {
                    case 64:
                        k = (i % (((int)picanm[tilenum] & 63) << 1));
                        if (k < ((int)picanm[tilenum] & 63))
                            offs = k;
                        else
                            offs = ((((int)picanm[tilenum] & 63) << 1) - k);
                        break;
                    case 128:
                        offs = (i % (((int)picanm[tilenum] & 63) + 1));
                        break;
                    case 192:
                        offs = -(i % (((int)picanm[tilenum] & 63) + 1));
                        break;
                }
            }
            return (offs);
        }

        public static void setbrightness(byte dabrightness, bPalette dapal)
        {
	        int i, j, k;

	        curbrightness = Math.Min(Math.Max((int)dabrightness,0),15);

	        k = 0;

            _device.SetPalette(dapal);
        }

        public static void SetPalette(bPalette palette)
        {
            _device.SetPalette(palette);
        }

        public static void RestorePalette()
        {
            _device.RestorePalette();
        }

        //
        // loadboard
        //
        public static int loadboard(string filename, ref int daposx, ref int daposy, ref int daposz, ref short daang, ref short dacursectnum)
        {
            board = new bMap();
            if (board.loadboard(filename, ref daposx, ref daposy, ref daposz, ref daang, ref dacursectnum) == -1)
            {
                throw new Exception("loadboard: failed to load board " + filename);
            }

            return 0;
        }

        //
        // krand
        //
        public static uint krand()
        {
	        randomseed = (randomseed*27584621)+1;
	        return(((uint)randomseed)>>16);
        }

        //
        // Setgamemode
        //
        public static int setgamemode(byte davidoption, int daxdim, int daydim, int dabpp, ref Image viewportimage)
        {
            _device.Init((int)daxdim, (int)daydim, ref viewportimage);

            // jv - was * sizeof(int)
            int j = _device.ydim*4;  //Leave room for horizlookup&horizlookup2

	        if (lookups != null)
	        {
		        //lookups.Free();
		        lookups = null;
	        }
	        
            lookups = new int[ j << 1 ];

	        horizlookup = lookups[0];
	        horizlookup2 = lookups[0] + j;
	        horizycent = ((_device.ydim*4)>>1);

	        //Force drawrooms to call dosetaspect & recalculate stuff
	        oxyaspect = oxdimen = oviewingrange = -1;

	        A.setvlinebpl(_device.bytesperline);
	        j = 0;
            for (int i = 0; i <= _device.ydim; i++)
            {
                ylookup[i] = j;
                j += _device.bytesperline;
            }

            _device.setview(0, 0, _device.xdim - 1, _device.ydim - 1);
	        //_device.clearallviews(0L);
            setbrightness((byte)curbrightness, palette);

	        if (searchx < 0) 
            { 
                searchx = _device.halfxdimen; 
                searchy = (_device.ydimen>>1); 
            }

            return 0;
        }

        public static void LoadTables()
        {
            table = new bTable();
            palette = new bPalette();

            A.setpalookupaddress(palette.globalpalwritten, 0);
        }

        //
        // Init
        //
        public static void Init()
        {
            int i, j;
            pragmas.InitPragmas();

            filesystem = new kFileSystem();


            for (i = 1; i < 1024; i++) lowrecip[i] = ((1 << 24) - 1) / i;
            
            for (i = 0; i < MAXTILES; i++)
                tiletovox[i] = -1;

            for (i = 0; i < voxscale.Length >> 2; i++)
            {
                voxscale[i] = 65536;
            }
            qsetmode = 200;
        }


        public static void loadtile(short tilenume)
        {
            //PointerObject<byte> ptr;
            int i, dasiz;

            if (tilenume >= MAXTILES) 
                return;

            dasiz = tilesizx[tilenume] * tilesizy[tilenume];
            if (dasiz <= 0) return;

            i = tilefilenum[tilenume];
            if (i != artfilnum)
            {
                if (artfil != null)
                    artfil.Close();

                artfilnum = i;
                artfilplc = 0;

                artfilename = artfilename.Remove(5, 3);
                artfilename = artfilename.Insert(5, "" + (char)(((i / 100) % 10) + 48) + "" + (char)(((i / 10) % 10) + 48) + "" + (char)((i % 10) + 48));

                
                artfil = filesystem.kopen4load(artfilename);
                //faketimerhandler();
            }

            if (artfilplc != tilefileoffs[tilenume])
            {
                artfil.SetPosition((int)(tilefileoffs[tilenume] - artfilplc));
                //faketimerhandler();
            }
            waloff[tilenume] = new tilecontainer();
            waloff[tilenume].memory = artfil.kread((int)dasiz);

            //faketimerhandler();
            artfilplc = tilefileoffs[tilenume] + dasiz;
        }
        private static int clippoly4(int cx1, int cy1, int cx2, int cy2)
        {
	        int n, nn, z, zz, x, x1, x2, y, y1, y2, t;

	        nn = 0; z = 0;
	        do
	        {
		        zz = ((z+1)&3);
		        x1 = nrx1[z]; x2 = nrx1[zz]-x1;

		        if ((cx1 <= x1) && (x1 <= cx2))
                {
			        nrx2[nn] = x1; nry2[nn] = nry1[z]; nn++;
                }

		        if (x2 <= 0) x = cx2; else x = cx1;
		        t = x-x1;
		        if (((t-x2)^t) < 0)
                {
			        nrx2[nn] = x; nry2[nn] = nry1[z]+pragmas.scale(t,nry1[zz]-nry1[z],x2); nn++;
                }

		        if (x2 <= 0) x = cx1; else x = cx2;
		        t = x-x1;
		        if (((t-x2)^t) < 0)
                {
			        nrx2[nn] = x; nry2[nn] = nry1[z]+pragmas.scale(t,nry1[zz]-nry1[z],x2); nn++;
                }

		        z = zz;
	        } while (z != 0);
	        if (nn < 3) return(0);

	        n = 0; z = 0;
	        do
	        {
		        zz = z+1; if (zz == nn) zz = 0;
		        y1 = nry2[z]; y2 = nry2[zz]-y1;

		        if ((cy1 <= y1) && (y1 <= cy2))
                {
			        nry1[n] = y1; nrx1[n] = nrx2[z]; n++;
                }

		        if (y2 <= 0) y = cy2; else y = cy1;
		        t = y-y1;
		        if (((t-y2)^t) < 0)
                {
			        nry1[n] = y; nrx1[n] = nrx2[z]+pragmas.scale(t,nrx2[zz]-nrx2[z],y2); n++;
                }

		        if (y2 <= 0) y = cy1; else y = cy2;
		        t = y-y1;
		        if (((t-y2)^t) < 0)
                {
                    nry1[n] = y; nrx1[n] = nrx2[z] + pragmas.scale(t, nrx2[zz] - nrx2[z], y2); n++;
                }
		        z = zz;
	        } while (z != 0);
	        return(n);
        }

        public static bool clipinsideboxline(int x, int y, int x1, int y1, int x2, int y2, int walldist)
        {
            int r;

            r = (walldist << 1);

            x1 += walldist - x; x2 += walldist - x;
            if ((x1 < 0) && (x2 < 0)) return false;
            if ((x1 >= r) && (x2 >= r)) return false;

            y1 += walldist - y; y2 += walldist - y;
            if ((y1 < 0) && (y2 < 0)) return false;
            if ((y1 >= r) && (y2 >= r)) return false;

            x2 -= x1; y2 -= y1;
            if (x2 * (walldist - y1) >= y2 * (walldist - x1))  //Front
            {
                if (x2 > 0) x2 *= (0 - y1); else x2 *= (r - y1);
                if (y2 > 0) y2 *= (r - x1); else y2 *= (0 - x1);

                if (x2 < y2)
                    return true;

                return false;
            }
            if (x2 > 0) x2 *= (r - y1); else x2 *= (0 - y1);
            if (y2 > 0) y2 *= (0 - x1); else y2 *= (r - x1);

            if (x2 >= y2)
                return true;

            return false;
            //return((x2 >= y2)<<1);
        }

        //
        // rintersect (internal)
        //
        public static int rintersect(int x1, int y1, int z1, int vx, int vy, int vz, int x3, int y3, int x4, int y4, ref int intx, ref int inty, ref int intz)
        {     //p1 towards p2 is a ray
            int x34, y34, x31, y31, bot, topt, topu, t;

            x34 = x3 - x4; y34 = y3 - y4;
            bot = vx * y34 - vy * x34;
            if (bot >= 0)
            {
                if (bot == 0) return (0);
                x31 = x3 - x1; y31 = y3 - y1;
                topt = x31 * y34 - y31 * x34; if (topt < 0) return (0);
                topu = vx * y31 - vy * x31; if ((topu < 0) || (topu >= bot)) return (0);
            }
            else
            {
                x31 = x3 - x1; y31 = y3 - y1;
                topt = x31 * y34 - y31 * x34; if (topt > 0) return (0);
                topu = vx * y31 - vy * x31; if ((topu > 0) || (topu <= bot)) return (0);
            }
            t = pragmas.divscale16( topt, bot);
            intx = x1 + pragmas.mulscale16( vx, t);
            inty = y1 + pragmas.mulscale16( vy, t);
            intz = z1 + pragmas.mulscale16( vz, t);
            return (1);
        }

        public static int qloadkvx(long voxindex, string filename)
        {
	        int i;
            kFile fil;

	        if ((fil = filesystem.kopen4load(filename)) == null) return -1;

            fil.SetLittleEdian();

            voxoff[voxindex] = new bVoxel();

	        for(i=0;i<1; /*MAXVOXMIPS*/i++)
	        {
                voxoff[voxindex].AddMipMap(ref fil);
                if (fil.ReachedEndOfBuffer) break;
	        }
            fil.Close();

	        return 0;
        }

        //
        // dorotatesprite (internal)
        //
	    //JBF 20031206: Thanks to Ken's hunting, s/(rx1|ry1|rx2|ry2)/n\1/ in this function
        private static int[] y1ve = new int[4];
        private static int[] y2ve = new int[4];
        private static int[] nrx1 = new int[8];
        private static int[] nry1 = new int[8];
        private static int[] nrx2 = new int[8];
        private static int[] nry2 = new int[8];
        private static void dorotatesprite(int sx, int sy, int z, short a, short picnum, sbyte dashade, byte dapalnum, byte dastat, int cx1, int cy1, int cx2, int cy2, int uniqid)
        {
	        int cosang, sinang, v, nextv, dax1, dax2, oy, bx, by, ny1, ny2;
	        int i, x, y, x1, y1, x2, y2, gx1, gy1;
	        int xsiz, ysiz, xoff, yoff, npoints, yplc, yinc, lx, rx, xx, xend;
	        int xv, yv, xv2, yv2, obuffermode, qlinemode=0, u4, d4;
	        byte bad;
            int palookupoffs;
            int p;

            int bufplc = 0;

	        if (cx1 < 0) cx1 = 0;
	        if (cy1 < 0) cy1 = 0;
	        if (cx2 > _device.xres-1) cx2 = _device.xres-1;
	        if (cy2 > _device.yres-1) cy2 = _device.yres-1;

	        xsiz = tilesizx[picnum]; ysiz = tilesizy[picnum];
	        if ((dastat&16) != 0) { xoff = 0; yoff = 0; }
	        else
	        {
		        xoff = (int)((sbyte)((picanm[picnum]>>8)&255))+(xsiz>>1);
		        yoff = (int)((sbyte)((picanm[picnum]>>16)&255))+(ysiz>>1);
	        }

	        if ((dastat&4) != 0) yoff = ysiz-yoff;

	        cosang = table.sintable[(a+512)&2047]; sinang = table.sintable[a&2047];

	        if ((dastat&2) != 0)  //Auto window size scaling
	        {
		        if ((dastat&8) == 0)
		        {
			        x = _device.xdimenscale;   //= scale(xdimen,yxaspect,320);
                    sx = ((cx1 + cx2 + 2) << 15) + pragmas.scale(sx - (320 << 15), _device.xdimen, 320);
			        sy = ((cy1+cy2+2)<<15)+pragmas.mulscale16( sy-(200<<15),x);
		        }
		        else
		        {
			          //If not clipping to startmosts, & auto-scaling on, as a
			          //hard-coded bonus, scale to full screen instead
                    x = pragmas.scale(_device.xdim, _device.yxaspect, 320);
                    sx = (_device.xdim << 15) + 32768 + pragmas.scale(sx - (320 << 15), _device.xdim, 320);
                    sy = (_device.ydim << 15) + 32768 + pragmas.mulscale16(sy - (200 << 15), x);
		        }
		        z = pragmas.mulscale16( z,x);
	        }

	        xv = pragmas.mulscale14( cosang,z);
	        yv = pragmas.mulscale14( sinang,z);
	        if (((dastat&2) != 0) || ((dastat&8) == 0)) //Don't aspect unscaled perms
	        {
		        xv2 = pragmas.mulscale16( xv,_device.xyaspect);
                yv2 = pragmas.mulscale16( yv, _device.xyaspect);
	        }
	        else
	        {
		        xv2 = xv;
		        yv2 = yv;
	        }

	        nry1[0] = sy - (yv*xoff + xv*yoff);
	        nry1[1] = nry1[0] + yv*xsiz;
	        nry1[3] = nry1[0] + xv*ysiz;
	        nry1[2] = nry1[1]+nry1[3]-nry1[0];
	        i = (cy1<<16); if ((nry1[0]<i) && (nry1[1]<i) && (nry1[2]<i) && (nry1[3]<i)) return;
	        i = (cy2<<16); if ((nry1[0]>i) && (nry1[1]>i) && (nry1[2]>i) && (nry1[3]>i)) return;

	        nrx1[0] = sx - (xv2*xoff - yv2*yoff);
	        nrx1[1] = nrx1[0] + xv2*xsiz;
	        nrx1[3] = nrx1[0] - yv2*ysiz;
	        nrx1[2] = nrx1[1]+nrx1[3]-nrx1[0];
	        i = (cx1<<16); if ((nrx1[0]<i) && (nrx1[1]<i) && (nrx1[2]<i) && (nrx1[3]<i)) return;
	        i = (cx2<<16); if ((nrx1[0]>i) && (nrx1[1]>i) && (nrx1[2]>i) && (nrx1[3]>i)) return;

	        gx1 = nrx1[0]; gy1 = nry1[0];   //back up these before clipping

	        if ((npoints = clippoly4(cx1<<16,cy1<<16,(cx2+1)<<16,(cy2+1)<<16)) < 3) return;

	        lx = nrx1[0]; rx = nrx1[0];

	        nextv = 0;
	        for(v=npoints-1;v>=0;v--)
	        {
		        x1 = nrx1[v]; x2 = nrx1[nextv];
		        dax1 = (x1>>16); if (x1 < lx) lx = x1;
		        dax2 = (x2>>16); if (x1 > rx) rx = x1;
		        if (dax1 != dax2)
		        {
			        y1 = nry1[v]; y2 = nry1[nextv];
			        yinc = pragmas.divscale16( y2-y1,x2-x1);
			        if (dax2 > dax1)
			        {
                        yplc = y1 + pragmas.mulscale16( (dax1 << 16) + 65535 - x1, yinc);
                        pragmas.qinterpolatedown16short(dax1, ref uplc, dax2 - dax1, yplc, yinc);
			        }
			        else
			        {
                        yplc = y2 + pragmas.mulscale16( (dax2 << 16) + 65535 - x2, yinc);
                        pragmas.qinterpolatedown16short(dax2, ref dplc, dax1 - dax2, yplc, yinc);
			        }
		        }
		        nextv = v;
	        }

	        if (waloff[picnum] == null) loadtile(picnum);
	        //setgotpic(picnum);
            byte[] buf = waloff[picnum].memory;
	        bufplc = 0;

	        palookupoffs = palette.palookup[dapalnum] + (palette.getpalookup(0,(int)dashade)<<8);

	        i = pragmas.divscale32( 1,z);
            xv = pragmas.mulscale14( sinang, i);
            yv = pragmas.mulscale14( cosang, i);
	        if (((dastat&2) != 0) || ((dastat&8) == 0)) //Don't aspect unscaled perms
	        {
		        yv2 = pragmas.mulscale16( -xv,_device.yxaspect);
                xv2 = pragmas.mulscale16( yv, _device.yxaspect);
	        }
	        else
	        {
		        yv2 = -xv;
		        xv2 = yv;
	        }

	        x1 = (lx>>16); x2 = (rx>>16);

	        oy = 0;
	        x = (x1<<16)-1-gx1; y = (oy<<16)+65535-gy1;
	        bx = pragmas.dmulscale16( x,xv2,y,xv);
            by = pragmas.dmulscale16( x, yv2, y, yv);
	        if ((dastat&4) != 0) { yv = -yv; yv2 = -yv2; by = (ysiz<<16)-1-by; }

        /*	if (origbuffermode == 0)
	        {
		        if (dastat&128)
		        {
			        obuffermode = buffermode;
			        buffermode = 0;
			        setactivepage(activepage);
		        }
	        }
	        else if (dastat&8)
		         permanentupdate = 1; */

	        if ((dastat&1) == 0)
	        {
		        if ((dastat&64) != 0)
			        A.setupspritevline(palette.palookup, palookupoffs,xv,yv,ysiz);
		        else
                    A.msetupspritevline(palette.palookup, palookupoffs, xv, yv, ysiz);
	        }
	        else
	        {
                A.tsetupspritevline(palette.palookup, palookupoffs, xv, yv, ysiz);
		        if ((dastat&32)!=0) 
                    A.settransreverse(); 
                else 
                    A.settransnormal();
	        }
	        for(x=x1;x<x2;x++)
	        {
		        bx += xv2; by += yv2;

		        y1 = uplc[x]; y2 = dplc[x];
		        if ((dastat&8) == 0)
		        {
                    if (_device.startumost[x] > y1) y1 = _device.startumost[x];
                    if (_device.startdmost[x] < y2) y2 = _device.startdmost[x];
		        }
		        if (y2 <= y1) continue;

		        switch(y1-oy)
		        {
			        case -1: bx -= xv; by -= yv; oy = y1; break;
			        case 0: break;
			        case 1: bx += xv; by += yv; oy = y1; break;
			        default: bx += xv*(y1-oy); by += yv*(y1-oy); oy = y1; break;
		        }

                p = frameplace + (ylookup[y1] + x);

		        if ((dastat&1) == 0)
		        {
			        if ((dastat&64) != 0)
				        A.spritevline(bx&65535,by&65535,y2-y1+1,buf, ((bx>>16)*ysiz+(by>>16))+bufplc,p);
			        else
                        A.mspritevline(bx & 65535, by & 65535, y2 - y1 + 1, buf, (bx >> 16) * ysiz + (by >> 16) + bufplc, p);
		        }
		        else
		        {
			        A.tspritevline(bx&65535,by&65535,y2-y1+1,buf, (bx>>16)*ysiz+(by>>16)+bufplc,p);
			        //transarea += (y2-y1);
		        }
		    //    faketimerhandler();
	        }
        }

        public static int xdim
        {
            get
            {
                return _device.xdim;
            }
        }

        public static int ydim
        {
            get
            {
                return _device.ydim;
            }
        }

        public static void rotatesprite(int sx, int sy, int z, short a, short picnum, sbyte dashade, byte dapalnum, byte dastat, int cx1, int cy1, int cx2, int cy2)
        {
            _device.BeginDrawing();
            dorotatesprite(sx, sy, z, a, picnum, dashade, dapalnum, dastat, cx1, cy1, cx2, cy2, 0);
            _device.EndDrawing();
        }
        public static int loadpics(string filename)
        {
	        int offscount, siz, localtilestart, localtileend, dasiz;
	        int i, j, k;
            kFile fil;
            int artversion, mapversion;
            int numtiles;

	        for(i=0;i<MAXTILES;i++)
	        {
		        tilesizx[i] = 0;
		        tilesizy[i] = 0;
		        picanm[i] = 0;
	        }

	        artsize = 0;

	        numtilefiles = 0;
	        do
	        {
                artfilename = filename;
		        k = numtilefiles;

                artfilename = artfilename.Remove(5, 3);
                artfilename = artfilename.Insert(5, "" + (char)(((k / 100) % 10) + 48) + "" + (char)(((k / 10) % 10) + 48) + "" + (char)((k % 10) + 48));

		        if ((fil = filesystem.kopen4load(artfilename)) != null)
		        {
                    fil.kreadint(out artversion);
			        if (artversion != 1) return(-1);
                    fil.kreadint(out numtiles);
                    fil.kreadint(out localtilestart);
                    fil.kreadint(out localtileend);
                    fil.kread<short>(ref tilesizx, localtilestart, (localtileend - localtilestart + 1) << 1);
                    fil.kread<short>(ref tilesizy, localtilestart, (localtileend - localtilestart + 1) << 1);
                    fil.kread<int, int>(ref picanm, localtilestart, (localtileend - localtilestart + 1) << 2);

			        offscount = 4+4+4+4+((localtileend-localtilestart+1)<<3);
			        for(i=localtilestart;i<=localtileend;i++)
			        {
				        tilefilenum[i] = (short)k;
				        tilefileoffs[i] = offscount;
                        dasiz = (int)((int)tilesizx[i] * (int)tilesizy[i]);
				        offscount += dasiz;
                        artsize += (int)(((dasiz + 15) & 0xfffffff0));
			        }
                    fil.Close();

			        numtilefiles++;
		        }
	        }
	        while (k != numtilefiles);

	        for(i=0;i<MAXTILES;i++)
	        {
		        j = 15;
		        while ((j > 1) && (pow2long[j] > tilesizx[i])) j--;
		        picsiz[i] = ((byte)j);
		        j = 15;
		        while ((j > 1) && (pow2long[j] > tilesizy[i])) j--;
                picsiz[i] += ((byte)(j << 4));
	        }

	        return(0);
        }

        //
        // NextPage
        //
        public static void NextPage()
        {
            totalclocklock+=10;
            _device.Present();
        }

        public static void Printf(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
