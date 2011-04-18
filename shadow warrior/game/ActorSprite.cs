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

namespace sw
{
    //
    // ActorSprite
    //
    public class ActorSprite : Actor
    {
        private spritetype _sprite;

        //
        // ActorSprite
        //
        public ActorSprite(spritetype sprite)
        {
            _sprite = sprite;
        }

        //
        // SetBlocking
        //
        public void SetBlocking(bool isBlocking)
        {
            if (!isBlocking)
            {
                _sprite.cstat = MyTypes.RESET(_sprite.cstat, Flags.CSTAT_SPRITE_BLOCK | Flags.CSTAT_SPRITE_BLOCK_HITSCAN);
                return;
            }

            MyTypes.SET(_sprite.cstat, Flags.CSTAT_SPRITE_BLOCK | Flags.CSTAT_SPRITE_BLOCK_HITSCAN);
        }

        //
        // SetVisible
        //
        public void SetVisible(bool isVisible)
        {
            if (isVisible)
            {
                _sprite.cstat = MyTypes.RESET(_sprite.cstat, Flags.CSTAT_SPRITE_INVISIBLE);
                return;
            }

            _sprite.cstat = MyTypes.SET(_sprite.cstat, Flags.CSTAT_SPRITE_INVISIBLE);
        }
    }
}
