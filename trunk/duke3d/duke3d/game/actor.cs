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
using duke3d.game.script;
namespace duke3d.game
{
    public class Actor
    {
        internal int _posx, _posy, _posz;
        internal short _ang;
        internal short _cursectnum;
        internal int _health;
        internal int _armor;
        internal Gamescript.ActorScriptFunction _script;
        public int curr_weapon;

        internal int _fvel = 0, _svel = 0, _angvel = 0;
        public spritetype _sprite;
        internal int ticks = 0;
        internal int basepic = -1;
        internal bool awake = false;
        public int frameskip = 0;
        public Type ActorType = typeof(Actor);

        public void GetActorPosition(out int posx, out int posy, out int posz, out short ang)
        {
            posx = _posx;
            posy = _posy;
            posz = _posz;
            ang = _ang;
        }
        
        public int GetCurrentPalette()
        {
            return _sprite.pal;
        }

        public int Tics
        {
            get
            {
                return ticks;
            }
        }

        public int Hitag
        {
            get
            {
                return _sprite.hitag;
            }
        }

        public int Lotag
        {
            get
            {
                return _sprite.lotag;
            }
        }

        public void SetHealth(int amt)
        {
            _health = amt;
        }

        public double Distance(Actor actor)
        {
            return Math.Sqrt(((actor._posx - _posx) * (actor._posx - _posx)) + ((actor._posy - _posy) * (actor._posy - _posy)));
        }

        internal virtual void ProcessMovement(int xvect, int yvect) {
            if (xvect != 0 || yvect != 0)
            {
                Engine.board.clipmove(ref _posx, ref _posy, ref _posz, ref _cursectnum, xvect, yvect, 164, (4 << 8), (4 << 8), Engine.CLIPMASK0);
                _sprite.x = _posx;
                _sprite.y = _posy;
                _sprite.z = _posz;
                _sprite.ang = _ang;
            }
        }

        public virtual void Damage(Actor inflictor, int damage)
        {
            _health -= damage;
        }

        private short[] aimstats = new short[] { 10, 13, 1, 2 };

        public short aim(short aang)
        {
            bool gotshrinker,gotfreezer;
            short j, a, k;
            bool cans = false;
            int i, dx1, dy1, dx2, dy2, dx3, dy3, smax, sdist;
            int xv, yv;

            a = _sprite.ang;

            j = -1;
        //    if(s->picnum == APLAYER && ps[s->yvel].aim_mode) return -1;

            gotshrinker = curr_weapon == Globals.SHRINKER_WEAPON;
            gotfreezer = curr_weapon == Globals.FREEZE_WEAPON;

            smax = 0x7fffffff;

            dx1 = Engine.table.sintable[(a+512-aang)&2047];
            dy1 = Engine.table.sintable[(a - aang) & 2047];
            dx2 = Engine.table.sintable[(a + 512 + aang) & 2047];
            dy2 = Engine.table.sintable[(a + aang) & 2047];

            dx3 = Engine.table.sintable[(a + 512) & 2047];
            dy3 = Engine.table.sintable[a & 2047];

            for(k=0;k<4;k++)
            {
                if( j >= 0 )
                    break;
                for (i = Engine.board.headspritestat[aimstats[k]]; i >= 0; i = Engine.board.nextspritestat[i])
                {
                    int SX = Engine.board.sprite[i].x;
                    int SY = Engine.board.sprite[i].y;
                    int SZ = Engine.board.sprite[i].z;
                    short SECT = Engine.board.sprite[i].sectnum;
                    if (Engine.board.sprite[i].xrepeat > 0 && Engine.board.sprite[i].extra >= 0 && (Engine.board.sprite[i].cstat & (257 + 32768)) == 257)
                        if (Globals.badguy(Engine.board.sprite[i]) || k < 2)
                        {
                            if (Globals.badguy(Engine.board.sprite[i]) /* || PN == APLAYER || PN == SHARK */)
                            {
                                /*
                                if( PN == APLAYER &&
            //                        ud.ffire == 0 &&
                                    ud.coop == 1 &&
                                    s->picnum == APLAYER &&
                                    s != &sprite[i])
                                        continue;

                                if(gotshrinker && sprite[i].xrepeat < 30 )
                                {
                                    switch(PN)
                                    {
                                        case SHARK:
                                            if(sprite[i].xrepeat < 20) continue;
                                                continue;
                                        case GREENSLIME:
                                        case GREENSLIME+1:
                                        case GREENSLIME+2:
                                        case GREENSLIME+3:
                                        case GREENSLIME+4:
                                        case GREENSLIME+5:
                                        case GREENSLIME+6:
                                        case GREENSLIME+7:
                                            break;
                                        default:
                                            continue;
                                    }
                                }
                                if(gotfreezer && sprite[i].pal == 1) continue;
                                */
                            }

                            xv = (SX - _sprite.x);
                            yv = (SY - _sprite.y);

                            if ((dy1 * xv) <= (dx1 * yv))
                                if ((dy2 * xv) >= (dx2 * yv))
                                {
                                    sdist = pragmas.mulscale(dx3, xv, 14) + pragmas.mulscale(dy3, yv, 14);
                                    if (sdist > 512 && sdist < smax)
                                    {
                                        a = 0;
                                        if (ActorType == typeof(Player))
                                        {
                                            if ((pragmas.klabs(pragmas.scale(SZ - _sprite.z, 10, sdist) - 0) < 100))
                                                a = 1;
                                        }
                                        else
                                        {
                                            a = 1;
                                        }

                                        //if(PN == ORGANTIC || PN == ROTATEGUN )
                                        //  cans = cansee(SX,SY,SZ,SECT,s->x,s->y,s->z-(32<<8),s->sectnum);
                                        //else cans = cansee(SX,SY,SZ-(32<<8),SECT,s->x,s->y,s->z-(32<<8),s->sectnum);
                                        cans = Engine.board.cansee(SX, SY, SZ - (32 << 8), SECT, _sprite.x, _sprite.y, _sprite.z - (32 << 8), _sprite.sectnum);

                                        if (a == 1 && cans)
                                        {
                                            smax = sdist;
                                            j = (short)i;
                                        }
                                    }
                                }
                        }
                }
            }

            return j;
        }

        //
        // InputThink
        //
        internal void InputThink()
        {
            int xvect = 0, yvect = 0;

            _ang += (short)_angvel;
#if !WINDOWS_PHONE
            int doubvel = 3;
#else
            int doubvel = 9;
#endif
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

        public void SetVelocity(int fvel, int svel, int zvel)
        {
            _fvel = fvel;
            _svel = svel;
            _sprite.z += zvel;
            _posz = _sprite.z;
        }

        public bool cansee(Actor actor)
        {
            if (_cursectnum == -1)
                return false;
            return Engine.board.cansee(_posx, _posy, _posz, _cursectnum, actor._posx, actor._posy, actor._posz, actor._cursectnum);
        }

        public void Destroy()
        {
            Engine.board.deletespritebyhandle(_sprite);
        }

        public void SetAIScript(Gamescript.ActorScriptFunction script)
        {
            _script = script;
            
        }

        public void AnimateFrame( int maxframes )
        {
            _sprite.picnum++;

            if (_sprite.picnum >= basepic + maxframes)
                _sprite.picnum = (short)basepic;
        }

        public virtual void Spawn(spritetype sprite)
        {
            _sprite = sprite;
            _sprite.obj = this;

            if (_sprite != null)
            {
                basepic = sprite.picnum;
                SetPosition(sprite.x, sprite.y, sprite.z, sprite.ang, sprite.sectnum);
            }
        }

        public void ForceAwake()
        {
            awake = true;
        }

        public virtual void Frame()
        {
            if (_script != null)
            {
                if (awake == false)
                {
                    if (cansee(Globals.ps[0]))
                    {
                        awake = true;
                    }
                }
                else
                {
                    _script.Invoke(this);
                }
            }

            InputThink();

            ticks++;
        }

        public void Hide()
        {
            _sprite.cstat = MyTypes.SET(_sprite.cstat, Flags.CSTAT_SPRITE_INVISIBLE);
        }

        //
        // MoveActor
        //
        public void MoveActor(int fvel, int svel, int angvel)
        {
            _fvel += fvel;
            _svel += svel;
            _angvel += angvel;
        }

        public short SectorNum
        {
            get
            {
                return _cursectnum;
            }
        }

        public void SetPosition(int posx, int posy, int posz, short ang, short sectnum)
        {
            _posx = posx;
            _posy = posy;
            _posz = posz;

            if(ang != -9999)
                _ang = ang;
            _cursectnum = sectnum;

            if (_sprite != null)
            {
                _sprite.x = _posx;
                _sprite.y = _posy;
                _sprite.z = _posz;
                if (ang != -9999)
                    _sprite.ang = _ang;

                _sprite.sectnum = _cursectnum;
            }
        }
    }
}
