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
    public struct player_orig
    {
        public int ox, oy, oz;
        public short oa, os;
    };

    public class Player : Actor
    {
        private int jumping_counter = 0, poszv = 0;
        private int turnheldtime = 0;
        private bool on_ground = false;

        private const int TURBOTURNTIME = (Globals.TICRATE/8); // 7
        private const int NORMALTURN  =  15;
        private const int PREAMBLETURN = 5;
        private const int NORMALKEYMOVE = 40;
        private const int MAXVEL    =   ((NORMALKEYMOVE*2)+10);
        private const int MAXSVEL   =   ((NORMALKEYMOVE*2)+10);
        private const int MAXANGVEL =   127;
        private const int MAXHORIZ  =   127;

        private int subweapon = 0;
        private int curr_weapon = Globals.PISTOL_WEAPON;
        private int[] ammo_amount = new int[Globals.MAX_WEAPONS];
        private bool[] gotweapon = new bool[Globals.MAX_WEAPONS];

        //
        // Spawn
        //
        public void Spawn()
        {
            _health = 100;
            _armor = 0;
            GiveWeapon(Globals.PISTOL_WEAPON, 48);
        }

        private void GiveWeapon(int weaponnum, int ammo)
        {
            ammo_amount[weaponnum] += ammo;
            gotweapon[weaponnum] = true;
        }

        private void patchstatusbar(int x1,int y1,int x2,int y2)
        {                                                      
             Engine.rotatesprite(0,(200-34)<<16,65536,0,Names.BOTTOMSTATUSBAR,4,0,(byte)(10+16+64+128),
                    pragmas.scale(x1, Engine._device.xdim, 320), pragmas.scale(y1, Engine._device.ydim, 200),
                    pragmas.scale(x2, Engine._device.xdim, 320) - 1, pragmas.scale(y2, Engine._device.ydim, 200) - 1);                        
        }

        //
        // ProcessMovement
        //
        private void ProcessMovement(int xvect, int yvect)
        {
            int i = 40;
            int fz = 0, cz = 0, hz = 0, lz = 0;
            int k;

            Engine.board.getzrange(_posx, _posy, _posz, _cursectnum, ref cz, ref hz, ref fz, ref lz, 163, Engine.CLIPMASK0);

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
                        
                    }
                    on_ground = false;

                }
                else
                {
                    jumping_counter = 0;
                    poszv = 0;
                }
            }
            else if (_posz < (fz - (i << 8)))
            {
                poszv += (176 + 80);

                if ((_posz + poszv) >= (fz - (i << 8))) // hit the ground
                {
                    poszv = 0;
                    on_ground = true;
                }
            }
            else
            {
                //Smooth on the ground
                on_ground = true;
                k = ((fz - (i << 8)) - _posz) >> 1;
                if (pragmas.klabs(k) < 256) k = 0;
                _posz += k;
                poszv -= 768;
                if (poszv < 0) poszv = 0;
            }

            _posz += poszv;

            Engine.board.clipmove(ref _posx, ref _posy, ref _posz, ref _cursectnum, xvect, yvect, 164, (4 << 8), (4 << 8), Engine.CLIPMASK0);
        }

        //
        // InputThink
        //
        private void InputThink()
        {
            int xvect = 0, yvect = 0;

            _ang += (short)_angvel;

            int doubvel = 3;

            xvect = 0; yvect = 0;
            if (_fvel != 0)
            {
                xvect += ((((int)_fvel) * doubvel * (int)Engine.table.sintable[(_ang + 512) & 2047]) >> 3);
                yvect += ((((int)_fvel) * doubvel * (int)Engine.table.sintable[_ang & 2047]) >> 3);
            }
            if (_svel != 0)
            {
                xvect += ((((int)_svel) * doubvel * (int)Engine.table.sintable[_ang & 2047]) >> 3);
                yvect += ((((int)_svel) * doubvel * (int)Engine.table.sintable[(_ang + 1536) & 2047]) >> 3);
            }

            ProcessMovement(xvect, yvect);

            _svel = 0;
            _fvel = 0;
            _angvel = 0;
        }

        private void getinput()
        {
            bool running = false;

            running = Controller.BUTTON(Controller.Buttons.gamefunc_Run);

            int keymove = 0, turnamount = 0;
            if (running)
            {
                turnamount = NORMALTURN << 1;
                keymove = NORMALKEYMOVE << 1;
            }
            else
            {
                turnamount = NORMALTURN;
                keymove = NORMALKEYMOVE;
            }

            if (Controller.BUTTON(Controller.Buttons.gamefunc_Strafe))
            {
                if (Controller.BUTTON(Controller.Buttons.gamefunc_Turn_Left))
                {
                    _svel -= -keymove;
                }
                if (Controller.BUTTON(Controller.Buttons.gamefunc_Turn_Right))
                {
                    _svel -= keymove;
                }
            }
            else
            {
                if (Controller.BUTTON(Controller.Buttons.gamefunc_Turn_Left))
                {
                    turnheldtime += 1;
                    if (turnheldtime >= TURBOTURNTIME)
                    {
                        _angvel -= turnamount;
                    }
                    else
                    {
                        _angvel -= PREAMBLETURN;
                    }
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Turn_Right))
                {
                    turnheldtime += 1;
                    if (turnheldtime >= TURBOTURNTIME)
                    {
                        _angvel += turnamount;
                    }
                    else
                    {
                        _angvel += PREAMBLETURN;
                    }
                }
                else
                {
                    turnheldtime = 0;
                }
            }

            if (Controller.BUTTON(Controller.Buttons.gamefunc_Jump))
            {
                if (jumping_counter == 0 && on_ground == true)
                    jumping_counter = 1;
            }   

            if (Controller.BUTTON(Controller.Buttons.gamefunc_Strafe_Left))
                _svel += keymove;

            if (Controller.BUTTON(Controller.Buttons.gamefunc_Strafe_Right))
                _svel += -keymove;

            if (Controller.BUTTON(Controller.Buttons.gamefunc_Move_Forward))
                _fvel += keymove;

            if (Controller.BUTTON(Controller.Buttons.gamefunc_Move_Backward))
                _fvel += -keymove;

            if (_fvel < -MAXVEL) _fvel = -MAXVEL;
            if (_fvel > MAXVEL) _fvel = MAXVEL;
            if (_svel < -MAXSVEL) _svel = -MAXSVEL;
            if (_svel > MAXSVEL) _svel = MAXSVEL;
            if (_angvel < -MAXANGVEL) _angvel = -MAXANGVEL;
            if (_angvel > MAXANGVEL) _angvel = MAXANGVEL;
            //if (horiz < -MAXHORIZ) horiz = -MAXHORIZ;
           // if (horiz > MAXHORIZ) horiz = MAXHORIZ;

            InputThink();
        }

        private void DrawFullStatusBar()
        {
            patchstatusbar(0, 0, 320, 200);

            DrawWeaponAmounts(96, 182, (int)0xffff);

            Game.digitalnumber(32, 200 - 17, _health, -16, (byte)(10 + 16 + 128));
            Game.digitalnumber(64, 200 - 17, _armor, -16, (byte)(10 + 16 + 128));
            Game.digitalnumber(230 - 22, 200 - 17, ammo_amount[curr_weapon], -16, (byte)(10 + 16 + 128));
        }

        private void DrawWeaponAmounts(int x,int y,int u)
        {
             int cw;

             cw = curr_weapon;

             if ((u&4) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(96,178,96+12,178+6);
                 Game.weaponnum999(Globals.PISTOL_WEAPON,x,y,
                             ammo_amount[Globals.PISTOL_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.PISTOL_WEAPON],
                             (byte)(12 - 20 * ((cw == Globals.PISTOL_WEAPON) ? 1 : 0)));
             }
             if ((u&8) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(96,184,96+12,184+6);
                 Game.weaponnum999(Globals.SHOTGUN_WEAPON, x, y + 6,
                             ammo_amount[Globals.SHOTGUN_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.SHOTGUN_WEAPON],
                             (byte)((!gotweapon[Globals.SHOTGUN_WEAPON] ? 1 : 0 * 9) + 12 - 18 * ((cw == Globals.SHOTGUN_WEAPON) ? 1 : 0)));
             }
             if ((u&16) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(96,190,96+12,190+6);
                 Game.weaponnum999(Globals.CHAINGUN_WEAPON, x, y + 12,
                              ammo_amount[Globals.CHAINGUN_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.CHAINGUN_WEAPON],
                              (byte)((!gotweapon[Globals.CHAINGUN_WEAPON]? 1 : 0 * 9) + 12 - 18 *((cw == Globals.CHAINGUN_WEAPON)? 1 : 0) ));
             }
             if ((u&32) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(135,178,135+8,178+6);
                 Game.weaponnum(Globals.RPG_WEAPON, x + 39, y,
                          ammo_amount[Globals.RPG_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.RPG_WEAPON],
                          (byte)((!gotweapon[Globals.RPG_WEAPON] ? 1 : 0 * 9) + 12 - 19 * ((cw == Globals.RPG_WEAPON)? 1 : 0)));
             }
             if ((u&64) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(135,184,135+8,184+6);
                 Game.weaponnum(Globals.HANDBOMB_WEAPON, x + 39, y + 6,
                             ammo_amount[Globals.HANDBOMB_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.HANDBOMB_WEAPON],
                             (byte)(((((ammo_amount[Globals.HANDBOMB_WEAPON] == 0) ? 1 : 0) | ((gotweapon[Globals.HANDBOMB_WEAPON]) ? 1 : 0) * 9) + 12 - 19 * ((cw == Globals.HANDBOMB_WEAPON) || (cw == Globals.HANDREMOTE_WEAPON) ? 1 : 0))));
             }
             if ((u&128) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(135,190,135+8,190+6);

        #if VOLUMEONE
                 orderweaponnum(SHRINKER_WEAPON,x+39,y+12,
                             ammo_amount[SHRINKER_WEAPON],Globals.script.gameStartup.max_ammo_amount[SHRINKER_WEAPON],
                             (!gotweapon[SHRINKER_WEAPON]*9)+12-18*
                             (cw == SHRINKER_WEAPON) );
        #else
                 if((subweapon&(1<<Globals.GROW_WEAPON)) != 0)
                     Game.weaponnum(Globals.SHRINKER_WEAPON, x + 39, y + 12,
                         ammo_amount[Globals.GROW_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.GROW_WEAPON],
                         (byte)((!gotweapon[Globals.GROW_WEAPON]? 1 : 0* 9) + 12 - 18 * ((cw == Globals.GROW_WEAPON)? 1 : 0)));
                 else
                     Game.weaponnum(Globals.SHRINKER_WEAPON, x + 39, y + 12,
                         ammo_amount[Globals.SHRINKER_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.SHRINKER_WEAPON],
                         (byte)((!gotweapon[Globals.SHRINKER_WEAPON]? 1 : 0 * 9) + 12 - 18 * ((cw == Globals.SHRINKER_WEAPON)? 1 : 0)));
        #endif
             }
             if ((u&256) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(166,178,166+8,178+6);

        #if VOLUMEONE
                orderweaponnum(DEVISTATOR_WEAPON,x+70,y,
                             ammo_amount[DEVISTATOR_WEAPON],Globals.script.gameStartup.max_ammo_amount[DEVISTATOR_WEAPON],
                             (!gotweapon[DEVISTATOR_WEAPON]*9)+12-18*
                             (cw == DEVISTATOR_WEAPON) );
        #else
                 Game.weaponnum(Globals.DEVISTATOR_WEAPON, x + 70, y,
                             ammo_amount[Globals.DEVISTATOR_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.DEVISTATOR_WEAPON],
                             (byte)((!gotweapon[Globals.DEVISTATOR_WEAPON] ? 1 : 0 * 9) + 12 - 18 * ((cw == Globals.DEVISTATOR_WEAPON) ? 1 : 0)));
        #endif
             }
             if ((u&512) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(166,184,166+8,184+6);
        #if VOLUMEONE
                 orderweaponnum(TRIPBOMB_WEAPON,x+70,y+6,
                             ammo_amount[TRIPBOMB_WEAPON],max_ammo_amount[TRIPBOMB_WEAPON],
                             (!gotweapon[TRIPBOMB_WEAPON]*9)+12-18*
                             (cw == TRIPBOMB_WEAPON) );
        #else
                 Game.weaponnum(Globals.TRIPBOMB_WEAPON, x + 70, y + 6,
                             ammo_amount[Globals.TRIPBOMB_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.TRIPBOMB_WEAPON],
                             (byte)((!gotweapon[Globals.TRIPBOMB_WEAPON] ? 1 : 0 * 9) + 12 - 18 * ((cw == Globals.TRIPBOMB_WEAPON) ? 1 : 0)));
        #endif
             }

             if ((u&65536L) != 0)
             {
                 if (u != 0xffffffff) patchstatusbar(166,190,166+8,190+6);
        #if VOLUMEONE
                orderweaponnum(-1,x+70,y+12,
                             ammo_amount[FREEZE_WEAPON],Globals.script.gameStartup.max_ammo_amount[FREEZE_WEAPON],
                             (!gotweapon[FREEZE_WEAPON]*9)+12-18*
                             (cw == FREEZE_WEAPON) );
        #else
                 Game.weaponnum(-1, x + 70, y + 12,
                             ammo_amount[Globals.FREEZE_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.FREEZE_WEAPON],
                             (byte)((!gotweapon[Globals.FREEZE_WEAPON] ? 1 : 0 * 9) + 12 - 18 * ((cw == Globals.FREEZE_WEAPON) ? 1 : 0)));
        #endif
             }
        }

        public void Frame()
        {
            // Move the player if we have any pending commands.
            if (Game.State == GameState.GAMESTATE_INGAME)
            {
                getinput();
            }

            // Draw the board and the sprites
            Engine.board.drawrooms(_posx, _posy, _posz, _ang, 100, _cursectnum);
            Engine.board.drawmasks();

            // Draw the status bar.
            DrawFullStatusBar();
        }
    }
}
