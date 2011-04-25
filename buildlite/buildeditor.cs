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
        int cursectnum = -1;
        short grid = 3, gridlock = 1, showtags = 1;
        int zoom = 768, gettilezoom = 1;

        int mousx2 = 8;
        int mousy2 = 8;

        public const int STATUS2DSIZ = 144;
        private readonly string kensig = "BUILD by Ken Silverman";

        public void Init(ref Image canvasimage)
        {
            // Init the build engine.
            Engine.Init();

            // Load in the game data.
            Engine.initgroupfile("sw.grp");

            Engine.LoadTables();

            // Init the device
            Engine.setgamemode(0, 640, 480, 8, ref canvasimage);

            // Load in the tiles.
            Engine.loadpics("tiles000.art");

            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
            //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;

            
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
        }


        public void Frame()
        {
            // Clear all the status bar positions to the correct color;
            clearmidstatbar16();

            overheadeditor();

            Engine.clear2dscreen();
            Engine.draw2dgrid(posx, posy, ang, zoom, grid);

            showmouse();

            Engine.NextPage();
        }
    }
}
