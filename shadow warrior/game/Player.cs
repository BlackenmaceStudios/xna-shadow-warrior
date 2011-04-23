using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using build;
using mact;

namespace sw
{
    class Player : ActorSprite
    {
        private Hud _hud;
        private int fvel = 0, svel = 0, angvel = 0;

        private bool playervoicefire = true;
        private int poszv = 0;
        private bSoundEffect DIGI_ZILLAREGARDS;
        private bSoundEffect[] DIGI_SWORDGOTU1 = new bSoundEffect[3];
        private bSoundEffect DIGI_KUNGFU;
        private int jumping_counter = 0;
        private int cameradist = 0;
        private int cameraclock = 0;

        private bSoundEffect DIGI_SWORDSWOOSH;
        private bool playenterlevelsound = true;
        private bool usebloodysword = false;

        public Player() : base(null)
        {
            _hud = new Hud(this);
            health = 100;
        }

        private void UpdateSprite()
        {
            _sprite.x = daposx;
            _sprite.y = daposy;
            _sprite.z = daposz;
            _sprite.ang = daang;
            _sprite.picnum = 1094;
            _sprite.sectnum = dacursectnum;
        }

        public override void Spawn()
        {
            _sprite = Engine.board.sprite[Engine.board.insertsprite(dacursectnum, 0)];
            _sprite.cstat = 1 + 256;
            _sprite.clipdist = 32;
            _sprite.xrepeat = 64;
            _sprite.yrepeat = 64;
            _sprite.statnum = 8;

            _sprite.cstat = MyTypes.SET(_sprite.cstat, Flags.CSTAT_SPRITE_INVISIBLE);

            UpdateSprite();
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

        private void view(ref int vx, ref int vy, ref int vz, ref short vsectnum, short ang, int horiz)
        {
	        int i, nx, ny, nz, hx, hy, hz, hitx = 0, hity = 0, hitz = 0;
	        short bakcstat, hitwall = 0, hitsprite = 0, daang;
            int hitsect = 0;

	        nx = (Engine.table.sintable[(ang+1536)&2047]>>4);
            ny = (Engine.table.sintable[(ang + 1024) & 2047] >> 4);
            nz = 0;// (horiz - 100) * 128;

            bakcstat = _sprite.cstat;
	        _sprite.cstat &= (short)~0x101;

	        Engine.board.updatesectorz(vx,vy,vz,ref vsectnum);
            Engine.board.hitscan(vx, vy, vz, vsectnum, nx, ny, nz, ref hitsect, ref hitwall, ref hitsprite, ref hitx, ref hity, ref hitz, Engine.CLIPMASK1);
	        hx = hitx-(vx); hy = hity-(vy);
            if (pragmas.klabs(nx) + pragmas.klabs(ny) > pragmas.klabs(hx) + pragmas.klabs(hy))
	        {
		        vsectnum = (short)hitsect;
		        if (hitwall >= 0)
		        {
                    daang = (short)Engine.getangle(Engine.board.wall[Engine.board.wall[hitwall].point2].x - Engine.board.wall[hitwall].x,
                                          Engine.board.wall[Engine.board.wall[hitwall].point2].y - Engine.board.wall[hitwall].y);

                    i = nx * Engine.table.sintable[daang] + ny * Engine.table.sintable[(daang + 1536) & 2047];
                    if (pragmas.klabs(nx) > pragmas.klabs(ny)) hx -= pragmas.mulscale28(nx, i);
                    else hy -= pragmas.mulscale28(ny, i);
		        }
		        else if (hitsprite < 0)
		        {
                    if (pragmas.klabs(nx) > pragmas.klabs(ny)) hx -= (nx >> 5);
										         else hy -= (ny>>5);
		        }
                if (pragmas.klabs(nx) > pragmas.klabs(ny)) i = pragmas.divscale16(hx, nx);
                                        else i = pragmas.divscale16(hy, ny);
		        if (i < cameradist) 
                    cameradist = i;
	        }
	        vx = (vx) + pragmas.mulscale16(nx, cameradist);
            vy = (vy) - pragmas.mulscale16(ny, cameradist);
            vz = (vz) + pragmas.mulscale16(nz, cameradist);

	        Engine.board.updatesectorz(vx,vy,vz,ref vsectnum);

            cameradist = Math.Min(cameradist + ((Game.totalclock - cameraclock) << 10), 65536);
            cameraclock = Game.totalclock;

            _sprite.cstat = bakcstat;
        }

        //
        // Think
        // 
        public override void Think()
        {
            if (_hud.state == WEAPON_STATE.WEAPON_IDLE)
                playervoicefire = true;

            

            Engine.board.drawrooms(daposx, daposy, daposz - (38 << 8), daang, 100, dacursectnum);
           // Engine.board.drawmasks();

            if (Mirrors.IsMirrorVisible())
            {
                int cposx = daposx, cposy = daposy, cposz = daposz - (38 << 8);
                short sectornum = 0;
                view(ref cposx, ref cposy, ref cposz, ref sectornum, daang, 100);
                Mirrors.MirrorsThink(cposx, cposy + 400, cposz, daang, 100);
            }

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

            UpdateSprite();
        }       
    }
}
