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
    public enum PaletteType
    {
        PALETTE_DEFAULTPAL,
        PALETTE_WATERPAL,
        PALETTE_SLIMEPAL,
        PALETTE_TITLEPAL,
        PALETTE_DREALMS,
        PALETTE_ENDINGPAL,
    }
    public class LookupTable
    {
        private int[] waterpal;
        private int[] slimepal;
        private int[] titlepal;
        private int[] drealms;
        private int[] endingpal;

        private int[] defaultpal;

        private int[] ReadPalette(kFile fp)
        {
            int[] newpal = new int[256];
            byte[] palette = fp.kread(768);

            for (int i = 0; i < 256; i++)
            {
                byte r, g, b;
                r = palette[(i * 3) + 0];
                g = palette[(i * 3) + 1];
                b = palette[(i * 3) + 2];
                newpal[i] = (255 << 24) | ((r * 4) << 16) | ((g * 4) << 8) | ((b * 4));
            }

            return newpal;
        }

        public void SetPalette(PaletteType type)
        {
            switch (type)
            {
                case PaletteType.PALETTE_DEFAULTPAL:
                    Engine.palette._palettebuffer = defaultpal;
                    break;

                case PaletteType.PALETTE_DREALMS:
                    Engine.palette._palettebuffer = drealms;
                    break;

                case PaletteType.PALETTE_ENDINGPAL:
                    Engine.palette._palettebuffer = endingpal;
                    break;

                case PaletteType.PALETTE_SLIMEPAL:
                    Engine.palette._palettebuffer = slimepal;
                    break;

                case PaletteType.PALETTE_TITLEPAL:
                    Engine.palette._palettebuffer = titlepal;
                    break;

                case PaletteType.PALETTE_WATERPAL:
                    Engine.palette._palettebuffer = waterpal;
                    break;
            }
        }

        public void Init()
        {
            int j;
            kFile fp;
            sbyte look_pos;
            byte numl;

            fp = Engine.filesystem.kopen4load("lookup.dat");
            if(fp != null)
                numl = fp.kreadbyte();
            else
                throw new Exception("\nERROR: File 'LOOKUP.DAT' not found.");

            for(j=0;j < numl;j++)
            {
                look_pos = fp.kreadsbyte();
                byte[] lookup = fp.kread(256);
                Engine.palette.makepalookup((int)look_pos, lookup, 0, 0, 0, true);
            }

            waterpal = ReadPalette(fp);
            slimepal = ReadPalette(fp);
            titlepal = ReadPalette(fp);
            drealms = ReadPalette(fp);
            endingpal = ReadPalette(fp);

            defaultpal = Engine.palette._palettebuffer;

            fp.Close();
        }
    }
}
