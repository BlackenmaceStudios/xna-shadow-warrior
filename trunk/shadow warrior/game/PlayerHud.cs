using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using build;    

namespace sw
{
    class Hud
    {
        private Player _player;
        private const short STATUS_BAR = 2434;
        private readonly int HUD_ROT_FLAGS = (Flags.ROTATE_SPRITE_SCREEN_CLIP | Flags.ROTATE_SPRITE_NON_MASK);

        //
        // Hud
        //
        public Hud(Player player)
        {
            _player = player;
        }

        private void DrawFullscreenSprite(short x, short y, short pic)
        {
            Engine.rotatesprite(x << 16, y << 16, 1 << 16, 0, pic, 0, 0, (byte)HUD_ROT_FLAGS, 0, 0, Engine.xdim + 1, Engine.ydim + 1);
        }

        //
        // DrawWeaponSword
        //
        public void DrawWeaponSword()
        {
            //DrawFullscreenSprite(320, 210, 2080);
        }

        //
        // DrawFullStatusBar
        //
        public void DrawFullStatusBar()
        {
            DrawFullscreenSprite((short)(Engine.tilesizx[STATUS_BAR] / 2), (short)(Engine.ydim - (Engine.tilesizy[STATUS_BAR] * 0.3f)), STATUS_BAR);
        }
    }
}
