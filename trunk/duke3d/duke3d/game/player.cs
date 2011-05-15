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
using duke3d.game.script;

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
        public byte kickback_pic = 0;
        private int endkickback = 0;
        private short weapon_ang = 0;
        private int weapon_sway = 0, bobcounter = 0;
        private int weapon_pos = 6;
        private int look_ang = 0;
        private int hbomb_hold_delay = 0;
        public int firedelay = 0;
        private int _pyoff = 0;

        private const int TURBOTURNTIME = (Globals.TICRATE/8); // 7
        private const int NORMALTURN  =  15;
        private const int PREAMBLETURN = 5;
        private const int NORMALKEYMOVE = 40;
        private const int MAXVEL    =   ((NORMALKEYMOVE*2)+10);
        private const int MAXSVEL   =   ((NORMALKEYMOVE*2)+10);
        private const int MAXANGVEL =   127;
        private const int MAXHORIZ  =   127;

        private int subweapon = 0;
        
        public int[] ammo_amount = new int[Globals.MAX_WEAPONS];
        public bool[] gotweapon = new bool[Globals.MAX_WEAPONS];
        public int weaponselected = -1;

        private Gamescript.ActorScriptFunction script_fireweapon = null;
        private Gamescript.ActorScriptFunction script_weaponanim = null;
        private Gamescript.ActorScriptFunction script_spawn = null;
        private Gamescript.ActorScriptFunction script_chooseweapon = null;
        //
        // AnimateFirstPeraonWeapon
        //
        public void AnimateFirstPersonWeapon(int startframe, int endframe)
        {
            kickback_pic = (byte)startframe;
            endkickback = endframe;
        }

        private void UpdatePlayerSprite()
        {
            if (_sprite == null)
            {
                int spritenum = Engine.board.insertsprite(_cursectnum, bMap.MAXSTATUS);
                _sprite = Engine.board.sprite[spritenum];
                _sprite.cstat = MyTypes.SET(_sprite.cstat, Flags.CSTAT_SPRITE_INVISIBLE);

                _sprite.picnum = Names.APLAYER;
                _sprite.xrepeat = 47;
                _sprite.yrepeat = 47;
            }

            _sprite.x = _posx;
            _sprite.y = _posx;
            _sprite.z = _posx;
            _sprite.sectnum = _cursectnum;
            
        }

        //
        // Spawn
        //
        public override void Spawn(spritetype sprite)
        {
            script_fireweapon = Globals.script.GetFunction("PLAYER_FIREWEAPON");
            script_weaponanim = Globals.script.GetFunction("PLAYER_WEAPONANIM");
            script_spawn = Globals.script.GetFunction("PLAYER_SPAWN");
            script_chooseweapon = Globals.script.GetFunction("PLAYER_CHOOSEWEAPON");

            UpdatePlayerSprite();

            script_spawn(this);
            ActorType = typeof(Player);
        }

        public int pyoff
        {
            get
            {
                return _pyoff;
            }
        }

        public void GiveWeapon(int weaponnum, int ammo)
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
        internal override void ProcessMovement(int xvect, int yvect)
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

        private void myospal(int x, int y, short tilenum, sbyte shade, byte orientation, byte p)
        {
            byte fp;
            short a;

            if ((orientation & 4) != 0)
                a = 1024;
            else a = 0;

            fp = Engine.board.sector[_cursectnum].floorpal;

            Engine.rotatesprite(x << 16, y << 16, 65536, a, tilenum, shade, p, (byte)(2 | orientation), Engine._device.windowx1, Engine._device.windowy1, Engine._device.windowx2, Engine._device.windowy2);

        }

        private sbyte[] remote_frames = new sbyte[]{ 0, 1, 1, 2, 1, 1, 0, 0, 0, 0, 0 };
        private short[] kb_frames = new short[] { 0, 1, 2, 0, 0 };
        private byte[] throw_frames = new byte[]{0,0,0,0,0,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2};
        private byte[] cycloidy = new byte[]{0,4,12,24,12,4,0};

        private void displayweapon()
        {
            int gun_pos, looking_arc, cw;
            int weapon_xoffset, i, j, x1, y1, x2;
            byte o,pal;
            sbyte gs;
            
           // kb = &kickback_pic;
            i = 0;
            o = 0;

            looking_arc = pragmas.klabs(look_ang)/9;

            gs = 0;// Engine.board.sprite[i].shade;
            if(gs > 24) gs = 24;

            //if(newowner >= 0 || ud.camerasprite >= 0 || over_shoulder_on > 0 || (sprite[i].pal != 1 && sprite[i].extra <= 0) || animatefist(gs,snum) || animateknuckles(gs,snum) || animatetip(gs,snum) || animateaccess(gs,snum) )
              //  return;

            //animateknee(gs,snum);

            gun_pos = 140-(weapon_pos*weapon_pos);

            weapon_xoffset =  (160)-90;
            weapon_xoffset -= (Engine.table.sintable[((weapon_sway>>1)+512)&2047]/(1024+512));
            weapon_xoffset -= 58 + weapon_ang;
           // if( sprite[i].xrepeat < 32 )
           //     gun_pos -= klabs(sintable[(weapon_sway<<2)&2047]>>9);
           // else 
            gun_pos -= pragmas.klabs(Engine.table.sintable[(weapon_sway>>2)&2047]>>9);

           // gun_pos -= (hard_landing<<3);
            /*
            if(last_weapon >= 0)
                cw = last_weapon;
            else cw = curr_weapon;

            j = 14-quick_kick;
            if(j != 14)
            {
                if(sprite[i].pal == 1)
                    pal = 1;
                else
                {
                    pal = sector[cursectnum].floorpal;
                    if(pal == 0)
                        pal = palookup;
                }


                if( j < 5 || j > 9 )
                    myospal(weapon_xoffset+80-(look_ang>>1),
                        looking_arc+250-gun_pos,KNEE,gs,o|4,pal);
                else myospal(weapon_xoffset+160-16-(look_ang>>1),
                    looking_arc+214-gun_pos,KNEE+1,gs,o|4,pal);
            }

            if( sprite[i].xrepeat < 40 )
            {
                if(jetpack_on == 0 )
                {
                    i = sprite[i].xvel;
                    looking_arc += 32-(i>>1);
                    fistsign += i>>1;
                }
                cw = weapon_xoffset;
                weapon_xoffset += sintable[(fistsign)&2047]>>10;
                myos(weapon_xoffset+250-(look_ang>>1),
                     looking_arc+258-(klabs(sintable[(fistsign)&2047]>>8)),
                     FIST,gs,o);
                weapon_xoffset = cw;
                weapon_xoffset -= sintable[(fistsign)&2047]>>10;
                myos(weapon_xoffset+40-(look_ang>>1),
                     looking_arc+200+(klabs(sintable[(fistsign)&2047]>>8)),
                     FIST,gs,o|4);
            }
            
            else */

            cw = curr_weapon;
            switch(cw)
            {
                case Globals.KNEE_WEAPON:
                    if( (kickback_pic) > 0 )
                    {
                        if(Engine.board.sprite[i].pal == 1)
                            pal = 1;
                        else
                        {
                            pal = Engine.board.sector[_cursectnum].floorpal;
                            if(pal == 0)
                                pal = 0;
                        }

                        if( (kickback_pic) < 5 || (kickback_pic) > 9 )
                            myospal(weapon_xoffset+220-(look_ang>>1),
                                looking_arc+250-gun_pos,Names.KNEE,gs,o,pal);
                        else
                            myospal(weapon_xoffset+160-(look_ang>>1),
                               looking_arc+214-gun_pos,Names.KNEE+1,gs,o,pal);
                    }
                    break;

                case Globals.TRIPBOMB_WEAPON:
                    if(Engine.board.sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = Engine.board.sector[_cursectnum].floorpal;

                    weapon_xoffset += 8;
                    gun_pos -= 10;

                    if((kickback_pic) > 6)
                        looking_arc += ((kickback_pic)<<3);
                    else if((kickback_pic) < 4)
                        myospal(weapon_xoffset+142-(look_ang>>1),
                                looking_arc+234-gun_pos,Names.HANDHOLDINGLASER+3,gs,o,pal);

                    myospal(weapon_xoffset+130-(look_ang>>1),
                            looking_arc+249-gun_pos,
                            (short)(Names.HANDHOLDINGLASER+((kickback_pic)>>2)),gs,o,pal);
                    myospal(weapon_xoffset+152-(look_ang>>1),
                            looking_arc+249-gun_pos,
                            (short)(Names.HANDHOLDINGLASER+((kickback_pic)>>2)),gs,(byte)(o|4),pal);

                    break;

                case Globals.RPG_WEAPON:
                    if(Engine.board.sprite[i].pal == 1)
                        pal = 1;
                    else pal = Engine.board.sector[_cursectnum].floorpal;

                    weapon_xoffset -= Engine.table.sintable[(768+((kickback_pic)<<7))&2047]>>11;
                    gun_pos += Engine.table.sintable[(768+((kickback_pic)<<7)&2047)]>>11;

                    if(kickback_pic > 0)
                    {
                        if(kickback_pic < 8)
                        {
                            myospal(weapon_xoffset+164,(looking_arc<<1)+176-gun_pos,
                                    (short)(Names.RPGGUN+((kickback_pic)>>1)),gs,o,pal);
                        }
                    }

                    myospal(weapon_xoffset+164,(looking_arc<<1)+176-gun_pos,
                            Names.RPGGUN,gs,o,pal);

                    break;

                case Globals.SHOTGUN_WEAPON:
                    if(Engine.board.sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = Engine.board.sector[_cursectnum].floorpal;

                    weapon_xoffset -= 8;

                    // jv - moved this to a if statements from a switch case - c# doesn't support follow through switches.
                    if (kickback_pic == 1 || kickback_pic == 2)
                    {
                        myospal(weapon_xoffset + 168 - (look_ang >> 1), looking_arc + 201 - gun_pos,
                               Names.SHOTGUN + 2, -128, o, pal);

                        myospal(weapon_xoffset + 146 - (look_ang >> 1), looking_arc + 202 - gun_pos,
                                Names.SHOTGUN, gs, o, pal);
                    }
                    else if (kickback_pic == 0 || kickback_pic == 6 || kickback_pic == 7 || kickback_pic == 8)
                    {
                        myospal(weapon_xoffset + 146 - (look_ang >> 1), looking_arc + 202 - gun_pos,
                                Names.SHOTGUN, gs, o, pal);
                    }
                    else if(kickback_pic == 3 || kickback_pic == 4 || kickback_pic == 5 || kickback_pic == 9 || kickback_pic == 10 || kickback_pic == 11 || kickback_pic == 12)
                    {
                         if( kickback_pic > 1 && kickback_pic < 5 )
                            {
                                gun_pos -= 40;
                                weapon_xoffset += 20;

                                myospal(weapon_xoffset+178-(look_ang>>1),looking_arc+194-gun_pos,
                                    (short)(Names.SHOTGUN+1+(((kickback_pic)-1)>>1)),-128,o,pal);
                            }

                            myospal(weapon_xoffset+158-(look_ang>>1),looking_arc+220-gun_pos,
                                Names.SHOTGUN+3,gs,o,pal);
                    }
                    else if(kickback_pic == 13 || kickback_pic == 14 || kickback_pic == 15)
                    {
                         myospal(32+weapon_xoffset+166-(look_ang>>1),looking_arc+210-gun_pos,
                                Names.SHOTGUN+4,gs,o,pal);
                    }
                    else if(kickback_pic == 16 || kickback_pic == 17 || kickback_pic == 18 || kickback_pic == 19)
                    {
                        myospal(64+weapon_xoffset+170-(look_ang>>1),looking_arc+196-gun_pos,
                                Names.SHOTGUN+5,gs,o,pal);
                    }
                    else if(kickback_pic == 20 || kickback_pic == 21 || kickback_pic == 22 || kickback_pic == 23)
                    {
                       myospal(64+weapon_xoffset+176-(look_ang>>1),looking_arc+196-gun_pos,
                                Names.SHOTGUN+6,gs,o,pal);
                    }
                    else if(kickback_pic == 24 || kickback_pic == 25 || kickback_pic == 26 || kickback_pic == 27)
                    {
                      myospal(64+weapon_xoffset+170-(look_ang>>1),looking_arc+196-gun_pos,
                                Names.SHOTGUN+5,gs,o,pal);
                    }
                    else if(kickback_pic == 28 || kickback_pic == 29 || kickback_pic == 30)
                    {
                         myospal(32+weapon_xoffset+156-(look_ang>>1),looking_arc+206-gun_pos,
                                Names.SHOTGUN+4,gs,o,pal);
                    }

                    break;



                case Globals.CHAINGUN_WEAPON:
                    if(Engine.board.sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = Engine.board.sector[_cursectnum].floorpal;

                    if(kickback_pic > 0)
                        gun_pos -= Engine.table.sintable[(kickback_pic)<<7]>>12;

                    if(kickback_pic > 0 && Engine.board.sprite[i].pal != 1) weapon_xoffset += 1-(Globals.rand(3));

                    myospal(weapon_xoffset+168-(look_ang>>1),looking_arc+260-gun_pos,
                        Names.CHAINGUN,gs,o,pal);
                    switch(kickback_pic)
                    {
                        case 0:
                            myospal(weapon_xoffset+178-(look_ang>>1),looking_arc+233-gun_pos,
                                Names.CHAINGUN+1,gs,o,pal);
                            break;
                        default:
                            if(kickback_pic > 4 && kickback_pic < 12)
                            {
                                i = 0;
                                if(Engine.board.sprite[i].pal != 1) i = Globals.rand( 7 );
                                myospal(i+weapon_xoffset-4+140-(look_ang>>1),i+looking_arc-((kickback_pic)>>1)+208-gun_pos,
                                    (short)(Names.CHAINGUN+5+((kickback_pic-4)/5)),gs,o,pal);
                                if(Engine.board.sprite[i].pal != 1) i = Globals.rand( 7 );
                                myospal(i+weapon_xoffset-4+184-(look_ang>>1),i+looking_arc-((kickback_pic)>>1)+208-gun_pos,
                                    (short)(Names.CHAINGUN+5+((kickback_pic-4)/5)),gs,o,pal);
                            }
                            if(kickback_pic < 8)
                            {
                                i = Globals.rand( 7 );
                                myospal(i+weapon_xoffset-4+162-(look_ang>>1),i+looking_arc-((kickback_pic)>>1)+208-gun_pos,
                                    (short)(Names.CHAINGUN+5+((kickback_pic-2)/5)),gs,o,pal);
                                myospal(weapon_xoffset+178-(look_ang>>1),looking_arc+233-gun_pos,
                                    (short)(Names.CHAINGUN+1+((kickback_pic)>>1)),gs,o,pal);
                            }
                            else myospal(weapon_xoffset+178-(look_ang>>1),looking_arc+233-gun_pos,
                                Names.CHAINGUN+1,gs,o,pal);
                            break;
                    }
                    break;
                 case Globals.PISTOL_WEAPON:
                     if(Engine.board.sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = Engine.board.sector[_cursectnum].floorpal;

                    if( (kickback_pic) < 5)
                    {
                        short l;

                        l = (short)(195-12+weapon_xoffset);

                        if((kickback_pic) == 2)
                            l -= 3;
                        myospal(
                            (l-(look_ang>>1)),
                            (looking_arc+244-gun_pos),
                            (short)(Names.FIRSTGUN+kb_frames[kickback_pic]),
                            gs,2,pal);
                    }
                    else
                    {
                        if((kickback_pic) < 10)
                            myospal(194-(look_ang>>1),looking_arc+230-gun_pos,Names.FIRSTGUN+4,gs,o,pal);
                        else if((kickback_pic) < 15)
                        {
                            myospal(244-((kickback_pic)<<3)-(look_ang>>1),looking_arc+130-gun_pos+((kickback_pic)<<4),Names.FIRSTGUN+6,gs,o,pal);
                            myospal(224-(look_ang>>1),looking_arc+220-gun_pos,Names.FIRSTGUN+5,gs,o,pal);
                        }
                        else if((kickback_pic) < 20)
                        {
                            myospal(124+((kickback_pic)<<1)-(look_ang>>1),looking_arc+430-gun_pos-((kickback_pic)<<3),Names.FIRSTGUN+6,gs,o,pal);
                            myospal(224-(look_ang>>1),looking_arc+220-gun_pos,Names.FIRSTGUN+5,gs,o,pal);
                        }
                        else if((kickback_pic) < 23)
                        {
                            myospal(184-(look_ang>>1),looking_arc+235-gun_pos,Names.FIRSTGUN+8,gs,o,pal);
                            myospal(224-(look_ang>>1),looking_arc+210-gun_pos,Names.FIRSTGUN+5,gs,o,pal);
                        }
                        else if((kickback_pic) < 25)
                        {
                            myospal(164-(look_ang>>1),looking_arc+245-gun_pos,Names.FIRSTGUN+8,gs,o,pal);
                            myospal(224-(look_ang>>1),looking_arc+220-gun_pos,Names.FIRSTGUN+5,gs,o,pal);
                        }
                        else if((kickback_pic) < 27)
                            myospal(194-(look_ang>>1),looking_arc+235-gun_pos,Names.FIRSTGUN+5,gs,o,pal);
                    }

                    break;
                case Globals.HANDBOMB_WEAPON:
                {
                    if(Engine.board.sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = Engine.board.sector[_cursectnum].floorpal;

                    if((kickback_pic) != 0)
                    {
                        

                        if((kickback_pic) < 7)
                            gun_pos -= 10*(kickback_pic);        //D
                        else if((kickback_pic) < 12)
                            gun_pos += 20*((kickback_pic)-10); //U
                        else if((kickback_pic) < 20)
                            gun_pos -= 9*((kickback_pic)-14);  //D

                        myospal(weapon_xoffset+190-(look_ang>>1),looking_arc+250-gun_pos,(short)(Names.HANDTHROW+throw_frames[(kickback_pic)]),gs,o,pal);
                    }
                    else
                        myospal(weapon_xoffset+190-(look_ang>>1),looking_arc+260-gun_pos,Names.HANDTHROW,gs,o,pal);
                }
                break;

                case Globals.HANDREMOTE_WEAPON:
                {
                        
                        if(Engine.board.sprite[i].pal == 1)
                            pal = 1;
                        else
                            pal = Engine.board.sector[_cursectnum].floorpal;

                        weapon_xoffset = -48;

                        if((kickback_pic) != 0)
                            myospal(weapon_xoffset+150-(look_ang>>1),looking_arc+258-gun_pos,(short)(Names.HANDREMOTE+remote_frames[(kickback_pic)]),gs,o,pal);
                        else
                            myospal(weapon_xoffset+150-(look_ang>>1),looking_arc+258-gun_pos,Names.HANDREMOTE,gs,o,pal);
                    }
                    break;
                case Globals.DEVISTATOR_WEAPON:
                    if(Engine.board.sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = Engine.board.sector[_cursectnum].floorpal;

                    if((kickback_pic) != 0)
                    {
                        

                        i = Math.Sign((kickback_pic)>>2);

                        if(hbomb_hold_delay != 0)
                        {
                            myospal( (cycloidy[kickback_pic]>>1)+weapon_xoffset+268-(look_ang>>1),cycloidy[kickback_pic]+looking_arc+238-gun_pos,(short)(Names.DEVISTATOR+i),-32,o,pal);
                            myospal(weapon_xoffset+30-(look_ang>>1),looking_arc+240-gun_pos,Names.DEVISTATOR,gs,(byte)(o|4),pal);
                        }
                        else
                        {
                            myospal( -(cycloidy[kickback_pic]>>1)+weapon_xoffset+30-(look_ang>>1),cycloidy[kickback_pic]+looking_arc+240-gun_pos,(short)(Names.DEVISTATOR+i),-32,(byte)(o|4),pal);
                            myospal(weapon_xoffset+268-(look_ang>>1),looking_arc+238-gun_pos,Names.DEVISTATOR,gs,o,pal);
                        }
                    }
                    else
                    {
                        myospal(weapon_xoffset+268-(look_ang>>1),looking_arc+238-gun_pos,Names.DEVISTATOR,gs,o,pal);
                        myospal(weapon_xoffset+30-(look_ang>>1),looking_arc+240-gun_pos,Names.DEVISTATOR,gs,(byte)(o|4),pal);
                    }
                    break;
                            /*
                case FREEZE_WEAPON:
                    if(sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = sector[cursectnum].floorpal;

                    if((kickback_pic))
                    {
                        char cat_frames[] = { 0,0,1,1,2,2 };

                        if(sprite[i].pal != 1)
                        {
                            weapon_xoffset += rand()&3;
                            looking_arc += rand()&3;
                        }
                        gun_pos -= 16;
                        myospal(weapon_xoffset+210-(look_ang>>1),looking_arc+261-gun_pos,FREEZE+2,-32,o,pal);
                        myospal(weapon_xoffset+210-(look_ang>>1),looking_arc+235-gun_pos,FREEZE+3+cat_frames[kickback_pic%6],-32,o,pal);
                    }
                    else myospal(weapon_xoffset+210-(look_ang>>1),looking_arc+261-gun_pos,FREEZE,gs,o,pal);

                    break;

                case SHRINKER_WEAPON:
                case GROW_WEAPON:
                    weapon_xoffset += 28;
                    looking_arc += 18;
                    if(sprite[i].pal == 1)
                        pal = 1;
                    else
                        pal = sector[cursectnum].floorpal;
                    if((kickback_pic) == 0)
                    {
                        if(cw == GROW_WEAPON)
                        {
                             myospal(weapon_xoffset+184-(look_ang>>1),
                                looking_arc+240-gun_pos,SHRINKER+2,
                                16-(sintable[random_club_frame&2047]>>10),
                                o,2);

                             myospal(weapon_xoffset+188-(look_ang>>1),
                               looking_arc+240-gun_pos,SHRINKER-2,gs,o,pal);
                        }
                        else
                        {
                            myospal(weapon_xoffset+184-(look_ang>>1),
                             looking_arc+240-gun_pos,SHRINKER+2,
                             16-(sintable[random_club_frame&2047]>>10),
                             o,0);

                            myospal(weapon_xoffset+188-(look_ang>>1),
                             looking_arc+240-gun_pos,SHRINKER,gs,o,pal);
                        }
                    }
                    else
                    {
                        if(sprite[i].pal != 1)
                        {
                            weapon_xoffset += rand()&3;
                            gun_pos += (rand()&3);
                        }

                        if(cw == GROW_WEAPON)
                        {
                            myospal(weapon_xoffset+184-(look_ang>>1),
                                looking_arc+240-gun_pos,SHRINKER+3+((kickback_pic)&3),-32,
                                o,2);

                            myospal(weapon_xoffset+188-(look_ang>>1),
                               looking_arc+240-gun_pos,SHRINKER-1,gs,o,pal);

                        }
                        else
                        {
                            myospal(weapon_xoffset+184-(look_ang>>1),
                             looking_arc+240-gun_pos,SHRINKER+3+((kickback_pic)&3),-32,
                             o,0);

                             myospal(weapon_xoffset+188-(look_ang>>1),
                             looking_arc+240-gun_pos,SHRINKER+1,gs,o,pal);
                        }
                    }
                    break;
                             * */
            }

//            displayloogie(snum);

        }

        private void DrawWeaponAmounts(int x,int y,int u)
        {
             int cw;

             cw = curr_weapon;

         //    if ((u&4) != 0)
             {
               //  if (u != 0xffffffff) patchstatusbar(96,178,96+12,178+6);
                 Game.weaponnum999(Globals.PISTOL_WEAPON,x,y,
                             ammo_amount[Globals.PISTOL_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.PISTOL_WEAPON],
                             (byte)(12 - 20 * ((cw == Globals.PISTOL_WEAPON) ? 1 : 0)));
             }
          //   if ((u&8) != 0)
             {
               //  if (u != 0xffffffff) patchstatusbar(96,184,96+12,184+6);
                 Game.weaponnum999(Globals.SHOTGUN_WEAPON, x, y + 6,
                             ammo_amount[Globals.SHOTGUN_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.SHOTGUN_WEAPON],
                             (byte)((!gotweapon[Globals.SHOTGUN_WEAPON] ? 1 : 0 * 9) + 12 - 18 * ((cw == Globals.SHOTGUN_WEAPON) ? 1 : 0)));
             }
            // if ((u&16) != 0)
             {
               //  if (u != 0xffffffff) patchstatusbar(96,190,96+12,190+6);
                 Game.weaponnum999(Globals.CHAINGUN_WEAPON, x, y + 12,
                              ammo_amount[Globals.CHAINGUN_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.CHAINGUN_WEAPON],
                              (byte)((!gotweapon[Globals.CHAINGUN_WEAPON]? 1 : 0 * 9) + 12 - 18 *((cw == Globals.CHAINGUN_WEAPON)? 1 : 0) ));
             }
            // if ((u&32) != 0)
             {
               //  if (u != 0xffffffff) patchstatusbar(135,178,135+8,178+6);
                 Game.weaponnum(Globals.RPG_WEAPON, x + 39, y,
                          ammo_amount[Globals.RPG_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.RPG_WEAPON],
                          (byte)((!gotweapon[Globals.RPG_WEAPON] ? 1 : 0 * 9) + 12 - 19 * ((cw == Globals.RPG_WEAPON)? 1 : 0)));
             }
             if ((u&64) != 0)
             {
               //  if (u != 0xffffffff) patchstatusbar(135,184,135+8,184+6);
                 Game.weaponnum(Globals.HANDBOMB_WEAPON, x + 39, y + 6,
                             ammo_amount[Globals.HANDBOMB_WEAPON], Globals.script.gameStartup.max_ammo_amount[Globals.HANDBOMB_WEAPON],
                             (byte)(((((ammo_amount[Globals.HANDBOMB_WEAPON] == 0) ? 1 : 0) | ((gotweapon[Globals.HANDBOMB_WEAPON]) ? 1 : 0) * 9) + 12 - 19 * ((cw == Globals.HANDBOMB_WEAPON) || (cw == Globals.HANDREMOTE_WEAPON) ? 1 : 0))));
             }
            // if ((u&128) != 0)
             {
                // if (u != 0xffffffff) patchstatusbar(135,190,135+8,190+6);

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
             //if ((u&256) != 0)
             {
               //  if (u != 0xffffffff) patchstatusbar(166,178,166+8,178+6);

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
             //if ((u&512) != 0)
             {
               //  if (u != 0xffffffff) patchstatusbar(166,184,166+8,184+6);
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

             //if ((u&65536L) != 0)
             {
                // if (u != 0xffffffff) patchstatusbar(166,190,166+8,190+6);
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

        private void CheckSectorDukeVoice()
        {
            if (Engine.board.sector[_cursectnum].lotag >= 10000 && Engine.board.sector[_cursectnum].lotag < 16383)
            {
                SoundSystem.sound((short)(Engine.board.sector[_cursectnum].lotag - 10000));
                Engine.board.sector[_cursectnum].lotag = 0;
            }
        }

        public override void Frame()
        {
            // Move the player if we have any pending commands.
            if (Game.State == GameState.GAMESTATE_INGAME)
            {
                getinput();

                bobcounter = _fvel >> 1;

                if (_fvel < 32 || on_ground == false )
                {
                    if ((weapon_sway & 2047) > (1024 + 96))
                       weapon_sway -= 96;
                    else if ((weapon_sway & 2047) < (1024 - 96))
                       weapon_sway += 96;
                    else weapon_sway = 1024;
                }
                else weapon_sway = bobcounter;

                if (endkickback == 0)
                {
                    if (Controller.BUTTON(Controller.Buttons.gamefunc_Fire))
                    {
                        script_fireweapon.Invoke(this);
                    }
                }

                if (kickback_pic < endkickback)
                {
                    kickback_pic++;
                    script_weaponanim.Invoke(this);
                }
                else
                {
                    if (firedelay <= 0)
                    {
                        kickback_pic = 0;
                        endkickback = 0;
                    }
                    else
                    {
                        firedelay--;
                    }
                }

                if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_0))
                {
                    weaponselected = 0;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_1))
                {
                    weaponselected = 1;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_2))
                {
                    weaponselected = 2;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_3))
                {
                    weaponselected = 3;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_4))
                {
                    weaponselected = 4;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_5))
                {
                    weaponselected = 5;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_6))
                {
                    weaponselected = 6;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_7))
                {
                    weaponselected = 7;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_8))
                {
                    weaponselected = 8;
                    GameKeys.KB_ClearLastScanCode();
                }
                else if (Controller.BUTTON(Controller.Buttons.gamefunc_Weapon_9))
                {
                    weaponselected = 9;
                    GameKeys.KB_ClearLastScanCode();
                }


                if (weaponselected != -1)
                {
                    script_chooseweapon.Invoke(this);
                    weaponselected = -1;
                }

                UpdatePlayerSprite();
            }

            // Draw the board and the sprites
            Engine.board.drawrooms(_posx, _posy, _posz, _ang, 100, _cursectnum);
            Engine.board.drawmasks();

            // Draw the player weapon.
            displayweapon();

            // Draw the status bar.
            DrawFullStatusBar();

            // Check to see if we are in a sector that has a duke voice.
            CheckSectorDukeVoice();
        }
    }
}
