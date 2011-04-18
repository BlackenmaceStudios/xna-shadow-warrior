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
    public class ST1 : ActorSprite
    {
        //
        // ST1
        //
        public ST1(spritetype sprite) : base(sprite)
        {

        }

        //
        // Spawn
        //
        public override void Spawn()
        {
            SetBlocking(false);
            SetVisible(false);
        }
    }
}
