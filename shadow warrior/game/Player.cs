using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using build;
using mact;

namespace sw
{
    class Player : Actor
    {
        private Hud _hud;
        private int fvel = 0, svel = 0, angvel = 0;

        private bool playervoicefire = true;
        private int poszv = 0;
        private bSoundEffect DIGI_ZILLAREGARDS;
        private bSoundEffect DIGI_SWORDGOTU1;
        private bSoundEffect DIGI_KUNGFU;
        private int jumping_counter = 0;

        private bSoundEffect DIGI_SWORDSWOOSH;
        private bool playenterlevelsound = true;

        public Player()
        {
            _hud = new Hud(this);
            health = 100;
        }

        //
        // Precache
        // 
        public void Precache(bSoundManager soundManager)
        {
            DIGI_ZILLAREGARDS = soundManager.LoadSound("JGEN06.VOC");
            DIGI_SWORDGOTU1 = soundManager.LoadSound("TSWORD05.VOC");
            DIGI_KUNGFU = soundManager.LoadSound("KUNGFU06.VOC");
            DIGI_SWORDSWOOSH = soundManager.LoadSound("SWRDSTR1.VOC");
        }

        //
        // Jump
        //
        public void Jump()
        {
            if (jumping_counter > 0)
                return;

            jumping_counter = 1;
        }

        //
        // FireWeapon
        //
        public void FireWeapon()
        {
            if (_hud.state == WEAPON_STATE.WEAPON_IDLE)
            {
                DIGI_SWORDSWOOSH.PlaySound();
                _hud.state = WEAPON_STATE.WEAPON_FIRING;
            }

            if (playervoicefire == true)
            {
               // DIGI_KUNGFU.PlaySound();
                playervoicefire = false;
            }
        }

        //
        // ProcessInput
        //
        public void ProcessInput(int fvel, int svel, int angvel)
        {
            this.fvel += fvel;
            this.svel += svel;
            this.angvel += angvel;
        }

        //
        // ProcessMovement
        //
        private void ProcessMovement( int xvect, int yvect )
        {
            int i = 40;
            int fz = 0, cz = 0, hz = 0, lz = 0;
            int k;

            Engine.board.getzrange(daposx, daposy, daposz, dacursectnum, ref cz, ref hz, ref fz, ref lz, 163, Engine.CLIPMASK0);

            if (jumping_counter > 0)
            {
                if (jumping_counter < (1024 + 512))
                {
                    if (jumping_counter > 768)
                    {
                        jumping_counter = 0;
                        poszv = -2048;
                    }
                    else
                    {
                        poszv -= (Engine.table.sintable[(2048 - 128 + jumping_counter) & 2047]) / 12;
                        jumping_counter += 180;
                       // onground = false;
                    }
                }
                else
                {
                    jumping_counter = 0;
                    poszv = 0;
                }
            }
            else if (daposz < (fz - (i << 8)))
            {
                poszv += (176 + 80);

                if ((daposz + poszv) >= (fz - (i << 8))) // hit the ground
                {
                    poszv = 0;
                }
            }
            else
            {
                //Smooth on the ground

                k = ((fz - (i << 8)) - daposz) >> 1;
                if (pragmas.klabs(k) < 256) k = 0;
                daposz += k;
                poszv -= 768;
                if (poszv < 0) poszv = 0;
            }

            daposz += poszv;

            Engine.board.clipmove(ref daposx, ref daposy, ref daposz, ref dacursectnum, xvect, yvect, 164, (4 << 8), (4 << 8), Engine.CLIPMASK0);
        }

        private void MovePlayer()
        {
            int xvect = 0, yvect = 0;

            daang += (short)angvel;

            int doubvel = 3;

            xvect = 0; yvect = 0;
            if (fvel != 0)
            {
                xvect += ((((int)fvel) * doubvel * (int)Engine.table.sintable[(daang + 512) & 2047]) >> 3);
                yvect += ((((int)fvel) * doubvel * (int)Engine.table.sintable[daang & 2047]) >> 3);
            }
            if (svel != 0)
            {
                xvect += ((((int)svel) * doubvel * (int)Engine.table.sintable[daang & 2047]) >> 3);
                yvect += ((((int)svel) * doubvel * (int)Engine.table.sintable[(daang + 1536) & 2047]) >> 3);
            }

            ProcessMovement(xvect, yvect);

            svel = 0;
            fvel = 0;
            angvel = 0;
        }

        //
        // DrawWeapon
        //
        private void DrawWeapon()
        {
            _hud.DrawWeaponSword();
        }

        //
        // Think
        // 
        public override void Think()
        {
            if (_hud.state == WEAPON_STATE.WEAPON_IDLE)
                playervoicefire = true;

            Engine.board.drawrooms(daposx, daposy, daposz - 20, daang, 100, dacursectnum);
            Engine.board.drawmasks();

            DrawWeapon();

            _hud.DrawFullStatusBar();

            MovePlayer();

            if (playenterlevelsound == true)
            {
                DIGI_ZILLAREGARDS.PlaySound();
                playenterlevelsound = false;
            }
        }       
    }
}
