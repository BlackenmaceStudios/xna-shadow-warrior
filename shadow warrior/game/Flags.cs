using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sw
{
    static class Flags
    {
        public readonly static int ROTATE_SPRITE_TRANSLUCENT = (MyTypes.BIT(0));
        public readonly static int ROTATE_SPRITE_VIEW_CLIP = (MyTypes.BIT(1)); // clip to view
        public readonly static int ROTATE_SPRITE_YFLIP = (MyTypes.BIT(2));
        public readonly static int ROTATE_SPRITE_IGNORE_START_MOST = (MyTypes.BIT(3)); // don't clip to startumost
        public readonly static int ROTATE_SPRITE_SCREEN_CLIP = (MyTypes.BIT(1) | MyTypes.BIT(3)); // use window
        public readonly static int ROTATE_SPRITE_CORNER = (MyTypes.BIT(4)); // place sprite from upper left corner
        public readonly static int ROTATE_SPRITE_TRANS_FLIP = (MyTypes.BIT(5));
        public readonly static int ROTATE_SPRITE_NON_MASK = (MyTypes.BIT(6)); // non masked sprites
        public readonly static int ROTATE_SPRITE_ALL_PAGES = (MyTypes.BIT(7)); // copies to all pages

        public readonly static int RS_SCALE = MyTypes.BIT(16);

        //cstat, bit 0: 1 = Blocking sprite (use with clipmove, getzrange)    "B"
        //       bit 1: 1 = 50/50 transluscence, 0 = normal                   "T"
        //       bit 2: 1 = x-flipped, 0 = normal                             "F"
        //       bit 3: 1 = y-flipped, 0 = normal                             "F"
        //       bits 5-4: 00 = FACE sprite (default)                         "R"
        //                 01 = WALL sprite (like masked walls)
        //                 10 = FLOOR sprite (parallel to ceilings&floors)
        //                 11 = SPIN sprite (face sprite that can spin 2draw style - not done yet)
        //       bit 6: 1 = 1-sided sprite, 0 = normal                        "1"
        //       bit 7: 1 = Real centered centering, 0 = foot center          "C"
        //       bit 8: 1 = Blocking sprite (use with hitscan)                "H"
        //       bit 9: reserved
        //       bit 10: reserved
        //       bit 11: reserved
        //       bit 12: reserved
        //       bit 13: reserved
        //       bit 14: reserved
        //       bit 15: 1 = Invisible sprite, 0 = not invisible

        public readonly static int CSTAT_SPRITE_BLOCK = MyTypes.BIT(0);
        public readonly static int CSTAT_SPRITE_TRANSLUCENT = MyTypes.BIT(1);
        public readonly static int CSTAT_SPRITE_XFLIP = MyTypes.BIT(2);
        public readonly static int CSTAT_SPRITE_YFLIP = MyTypes.BIT(3);
        public readonly static int CSTAT_SPRITE_WALL = MyTypes.BIT(4);
        public readonly static int CSTAT_SPRITE_FLOOR = MyTypes.BIT(5);
        public readonly static int CSTAT_SPRITE_SLAB = (MyTypes.BIT(4) | MyTypes.BIT(5));
        public readonly static int CSTAT_SPRITE_ONE_SIDE = MyTypes.BIT(6);
        public readonly static int CSTAT_SPRITE_YCENTER = MyTypes.BIT(7);
        public readonly static int CSTAT_SPRITE_BLOCK_HITSCAN = MyTypes.BIT(8);
        public readonly static int CSTAT_SPRITE_TRANS_FLIP = MyTypes.BIT(9);

        public readonly static int CSTAT_SPRITE_RESTORE = MyTypes.BIT(12); // my def
        public readonly static int CSTAT_SPRITE_CLOSE_FLOOR = MyTypes.BIT(13); // my def - tells whether a sprite
                                                    // started out close to a ceiling or floor
        public readonly static int CSTAT_SPRITE_BLOCK_MISSILE = MyTypes.BIT(14); // my def
        public readonly static int CSTAT_SPRITE_INVISIBLE = MyTypes.BIT(15);

        public readonly static int CSTAT_SPRITE_BREAKABLE = (CSTAT_SPRITE_BLOCK_HITSCAN|CSTAT_SPRITE_BLOCK_MISSILE);
    }
}
