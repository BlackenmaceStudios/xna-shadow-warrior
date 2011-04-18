using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

using build;
using mact.sound;

namespace mact
{
    //
    // bSoundEffect
    //
    public abstract class bSoundEffect
    {
        internal string _filename;
        internal SoundEffect _soundEffect;
        internal SoundEffectInstance _instance;

        public string filename
        {
            get
            {
                return _filename;
            }
        }

        public virtual void PlaySound()
        {
            _instance.Play();
        }

        public virtual void LoadSound(ref EndianBinaryReader _reader) { throw new NotImplementedException(); }
        public virtual void LoadMusic(string filename) { throw new NotImplementedException(); }
    }

    //
    // bSoundManager
    //
    public class bSoundManager
    {
        List<bSoundEffect> soundeffects = new List<bSoundEffect>();

        //
        // LoadMusic
        //
        public bSoundEffect LoadMusic(string filename)
        {
            bSoundEffect soundeffect = null;

            filename = Path.GetFileNameWithoutExtension( filename.ToLower() );

            // Check to see if the sound was already loaded.
            for (int i = 0; i < soundeffects.Count; i++)
            {
                if (soundeffects[i].filename == filename)
                {
                    return soundeffects[i];
                }
            }

            soundeffect = new bMusic();
            soundeffect.LoadMusic(filename);

            soundeffects.Add(soundeffect);

            return soundeffects[soundeffects.Count - 1];
        }

        //
        // LoadSound
        //
        public bSoundEffect LoadSound(string filename)
        {
            byte[] buffer;
            EndianBinaryReader _reader;
            bSoundEffect soundeffect = null;

            filename = filename.ToLower();

            // Check to see if the sound was already loaded.
            for (int i = 0; i < soundeffects.Count; i++)
            {
                if (soundeffects[i].filename == filename)
                {
                    return soundeffects[i];
                }
            }

            buffer = Engine.filesystem.kreadfile(filename);
            if (buffer == null)
            {
                throw new Exception("bSoundManager::LoadSound: Failed to load sound " + filename);
            }

            _reader = new EndianBinaryReader(new MemoryStream(buffer));

            if (bSoundEffectVoc.IsValidVocFile(ref _reader))
            {
                _reader.SetLittleEdian();

                soundeffect = new bSoundEffectVoc();
                soundeffect.LoadSound(ref _reader);
            }
            else
            {
                throw new Exception("bSoundManager::LoadSound: Invalid sound file " + filename);
            }

            _reader.Dispose();
            soundeffects.Add(soundeffect);

            return soundeffects[soundeffects.Count - 1];
        }
    }
}
