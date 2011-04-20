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
        private bSoundEffect[] DIGI_SWORDGOTU1 = new bSoundEffect[3];
        private bSoundEffect DIGI_KUNGFU;
        private int jumping_counter = 0;

        private bSoundEffect DIGI_SWORDSWOOSH;
        private bool playenterlevelsound = true;
        private bool usebloodysword = false;

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
            DIGI_SWORDGOTU1[0] = soundManager.LoadSound("TSWORD05.VOC");
            DIGI_SWORDGOTU1[1] = soundManager.LoadSound("TSWORD08.VOC");
            DIGI_SWORDGOTU1[2] = soundManager.LoadSound("TSWORD01.VOC");
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
        private Random rnd = new Random();
        public void FireWeapon()
        {
            if (_hud.state == WEAPON_STATE.WEAPON_IDLE)
            {
                int dasect = 0;
                short dawall = 0, daspr = 0;
                int x4 = 0, y4 = 0, z4 = 0;
                DIGI_SWORDSWOOSH.PlaySound();
                _hud.state = WEAPON_STATE.WEAPON_FIRING;

                int daz2 = ((100 - 100) * 2000) + (((int)Engine.krand() - 32768) >> 1);

                Engine.board.hitscan(daposx, daposy, daposz, dacursectnum, Engine.table.sintable[(daang + 512) & 2047], Engine.table.sintable[daang & 2047],
                                    daz2, ref dasect, ref dawall, ref daspr, ref x4, ref y4, ref z4, Engine.CLIPMASK1);

                if (daspr != -1 && Engine.board.sprite[daspr].obj != null)
                {
                    Actor actor = (Actor)Engine.board.sprite[daspr].obj;

                    actor.Damage(40);

                    if (actor.Health <= 0)
                    {
                        if (usebloodysword == false)
                        {
                            usebloodysword = true;
                            _hud.state = WEAPON_STATE.WEAPON_IDLE;
                        }
                        DIGI_SWORDGOTU1[rnd.Next(3)].PlaySound();
                    }
                }
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
            if (usebloodysword)
            {
                _hud.DrawWeaponSwordBloody();
            }
            else
            {
                _hud.DrawWeaponSword();
            }
        }

        //
        // Think
        // 
        public override void Think()
        {
            if (_hud.state == WEAPON_STATE.WEAPON_IDLE)
                playervoicefire = true;

            Engine.board.drawrooms(daposx, daposy, daposz - (38 << 8), daang, 100, dacursectnum);
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
