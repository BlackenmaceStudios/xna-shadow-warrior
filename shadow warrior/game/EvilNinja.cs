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
    public class EvilNinja : ActorSprite
    {
        int animframe = 0;

        //
        // EvilNinja
        //
        public EvilNinja(spritetype sprite)
            : base(sprite)
        {

        }

        //
        // Damage
        //
        public override void Damage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                _sprite.picnum = 4227;
                SetBlocking(false);
                SetHitscan(false);
            }
        }

        //
        // Spawn
        //
        public override void Spawn()
        {
           
            SetVisible(true);
            SetBlocking(true);
            SetHitscan(true);

            health = 20;
        }

        public override void Think()
        {
            if (health <= 0 && _sprite.picnum < 4236)
            {
                _sprite.picnum++;
            }
        }
    }
}
