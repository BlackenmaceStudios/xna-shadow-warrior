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
        

        private int poszv = 0;
        private bSoundEffect DIGI_ZILLAREGARDS;
        private bool playenterlevelsound = true;

        public Player()
        {
            _hud = new Hud(this);
        }

        //
        // Precache
        // 
        public void Precache(bSoundManager soundManager)
        {
            DIGI_ZILLAREGARDS = soundManager.LoadSound("JGEN06.VOC");
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

            if (daposz < (fz - (i << 8)))
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
