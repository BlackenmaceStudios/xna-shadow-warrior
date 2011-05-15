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

        internal int _fvel = 0, _svel = 0, _angvel = 0;
        public spritetype _sprite;
        internal int ticks = 0;
        internal int basepic = -1;
        internal bool awake = false;
        public int frameskip = 0;
        
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
            }
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
