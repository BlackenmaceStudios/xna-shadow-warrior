using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using build;

using Microsoft.Xna.Framework.Audio;

namespace mact.sound
{
    public class bMusic : bSoundEffect
    {
        //
        // LoadMusic
        //
        public override void LoadMusic(string filename)
        {
            _soundEffect = SoundEffect.FromStream(Engine.filesystem.ReadContentFileStream("music/" + filename + ".mp3"));

            _instance = _soundEffect.CreateInstance();
            _instance.IsLooped = true;
        }
    }
}
