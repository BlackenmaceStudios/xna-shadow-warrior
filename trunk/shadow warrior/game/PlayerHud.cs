using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using build;    

namespace sw
{
    enum WEAPON_STATE
    {
        WEAPON_IDLE = 0,
        WEAPON_FIRING
    }
    class Hud
    {
        public WEAPON_STATE state = WEAPON_STATE.WEAPON_IDLE;

        private float framenum = 0;
        private Player _player;
        private const short STATUS_BAR = 2434;
        private readonly int HUD_ROT_FLAGS = (Flags.ROTATE_SPRITE_SCREEN_CLIP | Flags.ROTATE_SPRITE_NON_MASK);


        public int PANEL_FONT_G = 3636;
        public int PANEL_FONT_Y = 3646;
        public int PANEL_FONT_R = 3656;

        private const int PRI_FRONT_MAX = 250;

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

        public void DisplayPanelNumber(short xs, short ys, int number)
        {
            string buffer;
            short x = xs, size;
            int ptr = 0;

            buffer = "" + number;

            for (ptr = 0; ptr < buffer.Length; ptr++, x += size)
            {
                DrawFullscreenSprite(x, ys, (short)(PANEL_FONT_G + (buffer[ptr] - '0')));

                size = (short)(Engine.tilesizx[PANEL_FONT_G + (buffer[ptr] - '0')] + 1);
            }
        }

        //
        // DrawWeaponSword
        //
        public void DrawWeaponSword()
        {
            if (state == WEAPON_STATE.WEAPON_IDLE)
            {
                framenum = 2080;
            }
            else if(state == WEAPON_STATE.WEAPON_FIRING)
            {
                framenum+=0.5f;

                if (framenum >= 2084)
                {
                    framenum = 2081;
                    state = WEAPON_STATE.WEAPON_IDLE;
                }
            }

            DrawFullscreenSprite(320, 210, (short)framenum);
        }


        private const int PANEL_HEALTH_BOX_X = 22;
        private const int PANEL_BOX_Y = (189-6);
        private const int PANEL_HEALTH_XOFF = 2;
        private const int PANEL_HEALTH_YOFF = 4;
        private void UpdatePlayerHealth()
        {
            short x = PANEL_HEALTH_BOX_X + PANEL_HEALTH_XOFF;
            short y = PANEL_BOX_Y + PANEL_HEALTH_YOFF;

            DisplayPanelNumber(x, y, _player.Health);
        }

        //
        // DrawFullStatusBar
        //
        public void DrawFullStatusBar()
        {
            DrawFullscreenSprite((short)(Engine.tilesizx[STATUS_BAR] / 2), (short)(Engine.ydim - (Engine.tilesizy[STATUS_BAR] * 0.3f)), STATUS_BAR);
            UpdatePlayerHealth();
        }
    }
}
