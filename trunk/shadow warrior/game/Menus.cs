using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using build;

namespace sw
{
    class Menus
    {
        private const int MZ = 65536;
        private short MenuTextShade = 0;
        byte[] lg_xlat_num = new byte[]{0,1,2,3,4,5,6,7,8,9};
        const int FONT_LARGE_ALPHA = 3706;
        const int FONT_LARGE_DIGIT = 3732;
        const int STARTALPHANUM = 4608;
        const int ENDALPHANUM = 4701;
        const int asc_Space = 32;

        public int TEXT_TEST_LINE() { return (200 / 2); }
        public int TEXT_XCENTER(int width) { return((320 - width)/2);}
        public int TEXT_YCENTER(int height) { return((200 - height)/2);}
        public int TEXT_TEST_COL(int width) { return TEXT_XCENTER(width); }


        private readonly int MenuDrawFlags = (Flags.ROTATE_SPRITE_SCREEN_CLIP);

        

        public void MNU_MeasureStringLarge(string str, ref short w, ref short h)
            {
            short ndx, width, height;
            char c;
            short pic;

            width = 0;
            height = h;

            for (ndx = 0; ndx < str.Length; ndx++)
                {
                c = str[ndx];
                if (Char.IsLetter(c))
                    {
                    c = Char.ToUpper(c);
                    pic = (short)(FONT_LARGE_ALPHA + (c - 'A'));
                    }
                else    
                if (Char.IsNumber(c))
                    {
                    pic = (short)(FONT_LARGE_DIGIT + lg_xlat_num[(c - '0')]);
                    }
                else
                if (c == ' ')
                    {
                    width += 10;                 // Special case for space char
                    continue;
                    }
                else        
                    {
                    continue;
                    }    
        
                width += (short)(Engine.tilesizx[pic]+1);
                if (height < Engine.tilesizx[pic])
                    height = Engine.tilesizy[pic];
                }

            w = width;
            h = height;
            }

        ////////////////////////////////////////////////
        // Draw a string using a graphic font
        ////////////////////////////////////////////////
        public void MNU_DrawStringLarge(short x, short y, string str)
            {
            int ndx, offset;
            char c;
            short pic;
    
            offset = x;

            for (ndx = 0; ndx < str.Length; ndx++)
                {
                    c = str[ndx];
                if (Char.IsLetter(c))
                    {
                    c = Char.ToUpper(c);
                    pic = (short)(FONT_LARGE_ALPHA + (c - 'A'));
                    }
                else    
                if (Char.IsNumber(c))
                    {
                    pic = (short)(FONT_LARGE_DIGIT + lg_xlat_num[(c - '0')]);
                    }
                else
                if (c == ' ')
                    {
                    offset += 10;
                    continue;
                    }
                else        
                    {
                    continue;
                    }

                Engine.rotatesprite(offset << 16, y << 16, MZ, 0, pic, (sbyte)MenuTextShade, 0, (byte)(MenuDrawFlags | Flags.ROTATE_SPRITE_CORNER), 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                offset += Engine.tilesizx[pic] + 1;
                }

            }

    
        ////////////////////////////////////////////////
        // Measure the pixel width of a graphic string
        ////////////////////////////////////////////////
        public void MNU_MeasureString(string str, ref short w, ref short h)
        {
            short ndx, width, height;
            char c;
            short ac;
    
            if (str[0] == '^')
            {
                string str2 = new string(str.ToCharArray(), 1, str.Length - 1 );
                MNU_MeasureStringLarge(str2, ref w, ref h);
                return;
            }

            width = 0;
            height = h;

            for (ndx = 0; ndx < str.Length; ndx++)
                {
                    c = str[ndx];
                ac = (short)(c - '!' + STARTALPHANUM);
                if( (ac < STARTALPHANUM || ac > ENDALPHANUM)  && c != asc_Space )
                    break;

                if (c > asc_Space && c < 127)
                    {
                    width += Engine.tilesizx[ac];
                    if (height < Engine.tilesizx[ac])
                        height = Engine.tilesizy[ac];
                    }
                else 
                if (c == asc_Space)
                    width += 4;                 // Special case for space char
                }

            w = width;
            h = height;
        }

        ////////////////////////////////////////////////
        // Draw a string using a graphic font
        //
        // MenuTextShade and MenuDrawFlags
        ////////////////////////////////////////////////
        public void  MNU_DrawString(short x, short y, string str, short shade, short pal)
        {
            int ndx, offset;
            char c;
            short ac;

            if (str[0] == '^')
            {
                string str2 = new string(str.ToCharArray(), 1, str.Length - 1 );
                MNU_DrawStringLarge(x, y, str2);
                return;
            }
        
            offset = x;

            for (ndx = 0; ndx < str.Length; ndx++)
            {
                c = str[ndx];
                ac = (short)(c - '!' + STARTALPHANUM);
                if( (ac < STARTALPHANUM || ac > ENDALPHANUM)  && c != asc_Space )
                    break;

                if (c > asc_Space && c < 127)
                {
                    Engine.rotatesprite(offset << 16, y << 16, MZ, 0, ac, (sbyte)shade, (byte)pal, (byte)MenuDrawFlags, 0, 0, Engine.xdim - 1, Engine.ydim - 1);
                    offset += Engine.tilesizx[ac];
                } else 
                if (c == asc_Space)
                    offset += 4;                // Special case for space char
            }

        }
    }
}
