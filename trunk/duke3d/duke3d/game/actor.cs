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
        internal spritetype _sprite;

        public void SetAIScript(Gamescript.ActorScriptFunction script)
        {
            _script = script;
        }

        public virtual void Spawn(spritetype sprite)
        {
            _sprite = sprite;
        }

        public virtual void Frame()
        {
            if (_script != null)
                _script.Invoke(this);
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

        public void SetPosition(int posx, int posy, int posz, short ang, short sectnum)
        {
            _posx = posx;
            _posy = posy;
            _posz = posz;
            _ang = ang;
            _cursectnum = sectnum;
        }
    }
}
