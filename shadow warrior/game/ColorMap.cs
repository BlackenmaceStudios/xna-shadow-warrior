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
    public static class ColorMapManager
    {
        private const int FOG_AMT = 15;
        private static byte[] tempbuf = new byte[256];
     //  private byte[] DefaultPalette;

        private static void MapColors(ColorMap.ColorMapTypes num, COLOR_MAP cm, bool create)
        {
            int i;
            float inc;

            if (create)
            {
                for (i = 0; i < 256; i++)
                    tempbuf[i] = (byte)i;
            }

            if (cm.FromRange == 0 || num <= 0 || ((int)num) >= 256)
            {
                return;
            }

            inc = cm.ToRange / ((float)cm.FromRange);

            for (i = 0; i < cm.FromRange; i++)
                tempbuf[i + cm.FromColor] = (byte)((i * inc) + cm.ToColor);
        }

        //
        // InitPalette
        //
        public static void InitPalette()
        {
            int i;

            //
            // Dive palettes
            //

            for (i = 0; i < 256; i++)
                tempbuf[i] = (byte)i;
            // palette for underwater
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_DIVE, tempbuf, 0, 0, 15, true);
    
        
            for (i = 0; i < 256; i++)
                tempbuf[i] = (byte)i;
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_FOG, tempbuf, FOG_AMT, FOG_AMT, FOG_AMT, true);

            for (i = 0; i < 256; i++)
                tempbuf[i] = (byte)i;
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_DIVE_LAVA, tempbuf, 11, 0, 0, true);

            //
            // 1 Range changes
            //

            MapColors(ColorMap.ColorMapTypes.PALETTE_BROWN_RIPPER, ColorMap.BrownRipper, true);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_BROWN_RIPPER, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_SKEL_GORE, ColorMap.SkelGore, true);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_SKEL_GORE, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_ELECTRO_GORE, ColorMap.ElectroGore, true);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_ELECTRO_GORE, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_MENU_HIGHLIGHT, ColorMap.MenuHighlight, true);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_MENU_HIGHLIGHT, tempbuf, 0, 0, 0, true);

            //
            // Multiple range changes
            //

            MapColors(ColorMap.ColorMapTypes.PALETTE_BASIC_NINJA, ColorMap.NinjaBasic[0], true);
            for (i = 1; i < ColorMap.NinjaBasic.Length; i++)
                MapColors(ColorMap.ColorMapTypes.PALETTE_BASIC_NINJA, ColorMap.NinjaBasic[i], false);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_BASIC_NINJA, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_RED_NINJA, ColorMap.NinjaRed[0], true);
            for (i = 1; i < ColorMap.NinjaRed.Length; i++)
                MapColors(ColorMap.ColorMapTypes.PALETTE_RED_NINJA, ColorMap.NinjaRed[i], false);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_RED_NINJA, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_GREEN_NINJA, ColorMap.NinjaGreen[0], true);
            for (i = 1; i < ColorMap.NinjaGreen.Length; i++)
                MapColors(ColorMap.ColorMapTypes.PALETTE_GREEN_NINJA, ColorMap.NinjaGreen[i], false);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_GREEN_NINJA, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_GREEN_LIGHTING, ColorMap.AllToGreen[0], true);
            for (i = 1; i < ColorMap.AllToGreen.Length; i++)
                MapColors(ColorMap.ColorMapTypes.PALETTE_GREEN_LIGHTING, ColorMap.AllToGreen[i], false);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_GREEN_LIGHTING, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_RED_LIGHTING, ColorMap.AllToRed[0], true);
            for (i = 1; i < ColorMap.AllToRed.Length; i++)
                MapColors(ColorMap.ColorMapTypes.PALETTE_RED_LIGHTING, ColorMap.AllToRed[i], false);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_RED_LIGHTING, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_BLUE_LIGHTING, ColorMap.AllToBlue[0], true);
            for (i = 1; i < ColorMap.AllToBlue.Length; i++)
                MapColors(ColorMap.ColorMapTypes.PALETTE_BLUE_LIGHTING, ColorMap.AllToBlue[i], false);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_BLUE_LIGHTING, tempbuf, 0, 0, 0, true);

            MapColors(ColorMap.ColorMapTypes.PALETTE_ILLUMINATE, ColorMap.Illuminate[0], true);
            for (i = 1; i < ColorMap.Illuminate.Length; i++)
                MapColors(ColorMap.ColorMapTypes.PALETTE_ILLUMINATE, ColorMap.Illuminate[i], false);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_ILLUMINATE, tempbuf, 0, 0, 0, true);

            // PLAYER COLORS - ALSO USED FOR OTHER THINGS
            for (int play = 0; play < ColorMap.PlayerColorMap.Length; play++)
            {
                MapColors(ColorMap.ColorMapTypes.PALETTE_PLAYER0 + play, ColorMap.PlayerColorMap[play], true);
                MapColors(ColorMap.ColorMapTypes.PALETTE_PLAYER0 + play, ColorMap.PlayerColorMap[play], false);
                Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_PLAYER0 + play, tempbuf, 0, 0, 0, true);
            }
    
            //
            // Special Brown sludge
            //

            for (i = 0; i < 256; i++)
                tempbuf[i] = (byte)i;
            // invert the brown palette
            for (i = 0; i < 32; i++)
                tempbuf[(int)ColorMap.ColorMapTypes.LT_BROWN + i] = (byte)((ColorMap.ColorMapTypes.LT_BROWN + 32) - i);
            Engine.palette.makepalookup((int)ColorMap.ColorMapTypes.PALETTE_SLUDGE, tempbuf, 0, 0, 0, true);
        }
    }

    public struct COLOR_MAP
    {
        public byte FromRange, ToRange, FromColor, ToColor;

        public COLOR_MAP(byte FromRange, byte ToRange, ColorMap.ColorMapTypes FromColor, ColorMap.ColorMapTypes ToColor)
        {
            this.FromRange = FromRange;
            this.ToRange = ToRange;
            this.FromColor = (byte)FromColor;
            this.ToColor = (byte)ToColor;
        }
    };

    public static class ColorMap
    {
        public static COLOR_MAP[] PlayerColorMap = new COLOR_MAP[]
        {
            new COLOR_MAP(32, 32, ColorMapTypes.LT_BLUE, ColorMapTypes.LT_BROWN),
            new COLOR_MAP(32, 31, ColorMapTypes.LT_BLUE, ColorMapTypes.LT_GREY),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.PURPLE),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.RUST_RED),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.YELLOW),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.DK_GREEN),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.GREEN),
            new COLOR_MAP(32, 32, ColorMapTypes.LT_BLUE, ColorMapTypes.LT_BLUE),  // Redundant, but has to be here for position
            new COLOR_MAP(32, 32, ColorMapTypes.LT_BLUE, ColorMapTypes.LT_TAN),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.RED),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.DK_GREY),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.DK_BLUE),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.FIRE),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.FIRE),
        };

        public static COLOR_MAP[] AllToRed = new COLOR_MAP[]
            {
            new COLOR_MAP(31, 16, ColorMapTypes.LT_GREY,ColorMapTypes.RED),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BROWN, ColorMapTypes.RED),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_TAN, ColorMapTypes.RED),
            new COLOR_MAP(16, 16, ColorMapTypes.RUST_RED, ColorMapTypes.RED),
            new COLOR_MAP(16, 16, ColorMapTypes.YELLOW, ColorMapTypes.RED),
            new COLOR_MAP(16, 16, ColorMapTypes.BRIGHT_GREEN, ColorMapTypes.RED),
            new COLOR_MAP(16, 16, ColorMapTypes.DK_GREEN, ColorMapTypes.RED),
            new COLOR_MAP(16, 16, ColorMapTypes.GREEN, ColorMapTypes.RED),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.RED),
            new COLOR_MAP(16, 16, ColorMapTypes.PURPLE, ColorMapTypes.RED),
            new COLOR_MAP(16, 16, ColorMapTypes.FIRE, ColorMapTypes.RED)
            };

        public static COLOR_MAP[] AllToBlue = new COLOR_MAP[]
            {
            new COLOR_MAP(31, 32, ColorMapTypes.LT_GREY, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(32, 32, ColorMapTypes.LT_BROWN, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(32, 32, ColorMapTypes.LT_TAN, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.RUST_RED, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.YELLOW, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.BRIGHT_GREEN, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.DK_GREEN, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.GREEN, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.RED, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.PURPLE, ColorMapTypes.LT_BLUE),
            new COLOR_MAP(16, 32, ColorMapTypes.FIRE, ColorMapTypes.LT_BLUE)
            };

        public static COLOR_MAP[] AllToGreen = new COLOR_MAP[]
            {
            new COLOR_MAP(31, 16, ColorMapTypes.LT_GREY, ColorMapTypes.GREEN),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BROWN, ColorMapTypes.GREEN),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_TAN, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.RUST_RED, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.YELLOW, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.BRIGHT_GREEN, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.DK_GREEN, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.GREEN, ColorMapTypes.GREEN),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.RED, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.PURPLE, ColorMapTypes.GREEN),
            new COLOR_MAP(16, 16, ColorMapTypes.FIRE, ColorMapTypes.GREEN)
            };

        public static COLOR_MAP[] NinjaBasic = new COLOR_MAP[]
            {
            new COLOR_MAP(32, 16, ColorMapTypes.LT_TAN, ColorMapTypes.DK_GREY),
            new COLOR_MAP(32, 16, ColorMapTypes.LT_BROWN,ColorMapTypes.DK_GREY),
            new COLOR_MAP(32, 31, ColorMapTypes.LT_BLUE,ColorMapTypes.LT_GREY),
            new COLOR_MAP(16, 16, ColorMapTypes.DK_GREEN,ColorMapTypes.DK_GREY),
            new COLOR_MAP(16, 16, ColorMapTypes.GREEN,  ColorMapTypes.DK_GREY),
            new COLOR_MAP(16, 16, ColorMapTypes.YELLOW, ColorMapTypes.DK_GREY)
            };

        public static COLOR_MAP[] NinjaRed = new COLOR_MAP[]
            {
            new COLOR_MAP(16, 16, ColorMapTypes.DK_TAN, ColorMapTypes.DK_GREY),
            new COLOR_MAP(16, 16, ColorMapTypes.GREEN, ColorMapTypes.DK_TAN),
            new COLOR_MAP(16, 8, ColorMapTypes.DK_BROWN, ColorMapTypes.RED + 8),

            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.RED)
            };

        public static COLOR_MAP[] NinjaGreen = new COLOR_MAP[]
            {
            new COLOR_MAP(16, 16, ColorMapTypes.DK_TAN, ColorMapTypes.DK_GREY),
            new COLOR_MAP(16, 16, ColorMapTypes.GREEN, ColorMapTypes.DK_TAN),
            new COLOR_MAP(16, 8, ColorMapTypes.DK_BROWN, ColorMapTypes.GREEN + 6),

            new COLOR_MAP(32, 16, ColorMapTypes.LT_BLUE, ColorMapTypes.GREEN)
            };

        public static COLOR_MAP[] Illuminate = new COLOR_MAP[]
            {
            new COLOR_MAP(16, 8, ColorMapTypes.LT_GREY, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.DK_GREY, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.LT_BROWN, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.DK_BROWN, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.LT_TAN, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.DK_TAN, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.RUST_RED, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.YELLOW, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.DK_GREEN, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.GREEN, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(32, 8, ColorMapTypes.LT_BLUE, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.RED, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.PURPLE, ColorMapTypes.BRIGHT_GREEN),
            new COLOR_MAP(16, 8, ColorMapTypes.FIRE, ColorMapTypes.BRIGHT_GREEN)
            };

        public static COLOR_MAP BrownRipper = new COLOR_MAP(31, 32, ColorMapTypes.LT_GREY, ColorMapTypes.LT_TAN);

        public static COLOR_MAP SkelGore = new COLOR_MAP(16, 16, ColorMapTypes.RED, ColorMapTypes.BRIGHT_GREEN);
        public static COLOR_MAP ElectroGore = new COLOR_MAP(16, 16, ColorMapTypes.RED, ColorMapTypes.DK_BLUE);

        public static COLOR_MAP MenuHighlight = new COLOR_MAP(16, 16, ColorMapTypes.RED, ColorMapTypes.FIRE);
        public enum ColorMapTypes
        {
            LT_GREY = (16 * 0 + 1),
            DK_GREY = (16 * 1),
            LT_BROWN = (16 * 2),
            DK_BROWN = (16 * 3),
            LT_TAN = (16 * 4),
            DK_TAN = (16 * 5),
            RUST_RED = (16 * 6),
            RED = (16 * 7),
            YELLOW = (16 * 8),
            BRIGHT_GREEN = (16 * 9),
            DK_GREEN = (16 * 10),
            GREEN = (16 * 11),
            LT_BLUE = (16 * 12),
            DK_BLUE = (16 * 13),
            PURPLE = (16 * 14),
            FIRE = (16 * 15),

            //
            // Palette numbers and meanings
            //

            PALETTE_DEFAULT = 0,
            PALETTE_FOG = 1,
            // blue sword blade test
            PALETTE_MENU_HIGHLIGHT = 2,
            // used for the elector gore pieces
            PALETTE_ELECTRO_GORE = 3,
            // turns ninjas belt and headband red
            PALETTE_BASIC_NINJA = 4,
            // diving in lava
            PALETTE_DIVE_LAVA = 5,
            // turns ninjas belt and headband red
            PALETTE_RED_NINJA = 6,
            // used for the mother ripper - she is bigger/stronger/brown
            PALETTE_BROWN_RIPPER = 7,
            // turns ninjas belt and headband red
            PALETTE_GREEN_NINJA = 8,
            // reserved diving palette this is copied over the default palette
            // when needed - NOTE: could move this to a normal memory buffer if palette
            // slot is needed.
            PALETTE_DIVE = 9,
            PALETTE_SKEL_GORE = 10,
            // turns ALL colors to shades of GREEN/BLUE/RED
            PALETTE_GREEN_LIGHTING = 11,
            PALETTE_BLUE_LIGHTING = 13,
            PALETTE_RED_LIGHTING = 14,

            // for brown bubbling sludge
            PALETTE_SLUDGE = 15,




            // Player 0 uses default palette - others use these
            // turns ninja's vests (when we get them) into different color ranges
            PALETTE_PLAYER0 = 16,
            PAL_XLAT_BROWN = 16,
            PALETTE_PLAYER1 = 17,
            PAL_XLAT_LT_GREY = 17,
            PALETTE_PLAYER2 = 18,
            PAL_XLAT_PURPLE = 18,
            PALETTE_PLAYER3 = 19,
            PAL_XLAT_RUST_RED = 19,
            PALETTE_PLAYER4 = 20,
            PAL_XLAT_YELLOW = 20,
            PALETTE_PLAYER5 = 21,
            PAL_XLAT_DK_GREEN = 21,
            PALETTE_PLAYER6 = 22,
            PAL_XLAT_GREEN = 22,
            PALETTE_PLAYER7 = 23,
            PAL_XLAT_LT_BLUE = 23,
            PALETTE_PLAYER8 = 24,
            PAL_XLAT_LT_TAN = 24,
            PALETTE_PLAYER9 = 25,
            PAL_XLAT_RED = 25,
            PALETTE_PLAYER10 = 26,
            PAL_XLAT_DK_GREY = 26,
            PALETTE_PLAYER11 = 27,
            PAL_XLAT_BRIGHT_GREEN = 27,
            PALETTE_PLAYER12 = 28,
            PAL_XLAT_DK_BLUE = 28,
            PALETTE_PLAYER13 = 29,
            PAL_XLAT_FIRE = 29,
            PALETTE_PLAYER14 = 30,
            PALETTE_PLAYER15 = 31,

            PALETTE_ILLUMINATE = 32  // Used to make sprites bright green in night vision
        }
    }
}
