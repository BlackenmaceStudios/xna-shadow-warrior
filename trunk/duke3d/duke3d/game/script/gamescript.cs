using System;
using System.Net;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using build;

namespace duke3d.game.script
{
    
    public class Gamescript
    {
        private GamescriptAssembly _assembly;
        private string[] _quotes;
        
        public List<VolumeInfo> volumes = new List<VolumeInfo>();
        public List<SkillInfo> skills = new List<SkillInfo>();
        Sounddef[] sounddefs = new Sounddef[Globals.NUM_SOUNDS];

        string _titlesong, _endsong;
        private int numUserSounds = 396;
        
        public GameStartup gameStartup;
        public delegate void ActorScriptFunction(object actor);

        public Sounddef[] sounds
        {
            get
            {
                return sounddefs;
            }
        }

        public string Titlesong
        {
            get
            {
                return _titlesong;
            }
        }

        public string Endsong
        {
            get
            {
                return _endsong;
            }
        }

        //
        // GetFunctionForActor
        //
        public ActorScriptFunction GetFunctionForActor(int spritepicnum)
        {
            MethodInfo method = _assembly.FindMethod("ScriptFunction_" + spritepicnum);

            if (method == null)
                return null;
            
            return (ActorScriptFunction)_assembly.CreateDelegate(typeof(ActorScriptFunction), method);
        }

        //
        // GetFunction
        //
        public ActorScriptFunction GetFunction(string name)
        {
            MethodInfo method = _assembly.FindMethod("ScriptFunction_" + name);

            if (method == null)
                return null;

            return (ActorScriptFunction)_assembly.CreateDelegate(typeof(ActorScriptFunction), method);
        }

        //
        // GetSpawnFunctionForActor
        //
        public ActorScriptFunction GetSpawnFunctionForActor(int spritepicnum)
        {
            MethodInfo method = _assembly.FindMethod("ScriptFunctionSpawn_" + spritepicnum);

            if (method == null)
                return null;

            return (ActorScriptFunction)_assembly.CreateDelegate(typeof(ActorScriptFunction), method);
        }

        //
        // InitScriptHooks
        //
        private void InitScriptHooks()
        {
            MethodInfo[] methods = typeof(GameScriptInterface).GetMethods(BindingFlags.Static | BindingFlags.Public);
            Type _scriptfunction = _assembly.GetDelegateType("ScriptFunction");

            foreach (MethodInfo method in methods)
            {
                _assembly.SetScriptClassMethod(method.Name, Delegate.CreateDelegate(_scriptfunction, method));
            }
        }

        //
        // Init
        //
        public void Init(string filename)
        {
            _assembly = new GamescriptAssembly();
            _assembly.Init(filename);

            // Init our script hooks.
            InitScriptHooks();

            // Pre-load anything static that we can bind right away to avoid reflection during game time.
            _quotes = (string[])_assembly.GetObjectFromAssembly("quotes");
            _titlesong = _assembly.GetStringValueFromAssembly("titlesong");
            _endsong = _assembly.GetStringValueFromAssembly("endsong");

            ParseGameStartup(_assembly.GetIntArrayFromAssembly("gamestartupvals"));

            // Create the sound look up list.
            string[] _soundFilenameList = _assembly.GetStringArrayFromAssembly("SoundFileList");
            string[] _soundIdList = _assembly.GetStringArrayFromAssembly("soundIdList");

            int listnum = 0;
            foreach (string s in _soundIdList)
            {
                short value = 0;
                FieldInfo soundid = typeof(SoundId).GetField(s);

                value = (short)_assembly.GetValueFromAssembly(s);

                Sounddef snd = new Sounddef();
                snd.filename = _soundFilenameList[listnum];

                if (sounddefs[value] != null)
                    throw new Exception("Duplicate sound id found");

                sounddefs[value] = snd;

                if(soundid != null)
                    soundid.SetValue(null, value);

                listnum++;
            }

            // Create the volumes and map list.
            int volnum = 0, levelnum = 0, vollevelnum = 0;
            string[] _volumenames = _assembly.GetStringArrayFromAssembly("volumenames");
            string[] _levelfilenames = _assembly.GetStringArrayFromAssembly("levelfilenames");
            string[] _levelpartimes = _assembly.GetStringArrayFromAssembly("levelpartimes");
            string[] _leveldesignertimes = _assembly.GetStringArrayFromAssembly("leveldesignertimes");
            string[] _levelnames = _assembly.GetStringArrayFromAssembly("levelnames");

            foreach (string s in _volumenames)
            {
                VolumeInfo _volume = new VolumeInfo();

                string[] volmusic = _assembly.GetStringArrayFromAssembly("vol" + volnum + "_music");
                int numlevels = _assembly.GetValueFromAssembly("vol" + volnum + "_numlevels");

                _volume.volumename = s;
                _volume.volumenum = volumes.Count;

                for (int i = 0; i < numlevels; i++, levelnum++, vollevelnum++)
                {
                    LevelInfo _level = new LevelInfo();

                    _level.designertime = _leveldesignertimes[levelnum];
                    _level.episodenum = volnum;
                    _level.levelnum = vollevelnum;
                    _level.filename = _levelfilenames[levelnum];
                    _level.partime = _levelpartimes[levelnum];
                    _level.mapname = _levelnames[levelnum];
                    _level.musicfilename = volmusic[vollevelnum];
                    _volume.DefineLevel(_level);
                }
                
                vollevelnum = 0;
                volumes.Add(_volume);
                volnum++;
            }

            // Define all the skills.
            string[] skillnames = _assembly.GetStringArrayFromAssembly("skillnames");
            foreach (string s in skillnames)
            {
                SkillInfo skill = new SkillInfo();
                skill.skillname = s;
                skills.Add(skill);
            }
        }

        private void ParseGameStartup(int[] gamestartupvals)
        {
            gameStartup.max_ammo_amount = new int[14];

            for( int j = 0; j < 30; j++ )
            {
                switch (j)
                {
                    case 0:
                        gameStartup.const_visibility = gamestartupvals[j];
                        break;
                    case 1:
                        gameStartup.impact_damage = gamestartupvals[j];
                        break;
                    case 2:
                        gameStartup.max_player_health = gamestartupvals[j];
                        break;
                    case 3:
                        gameStartup.max_armour_amount = gamestartupvals[j];
                        break;
                    case 4:
                        gameStartup.respawnactortime = gamestartupvals[j]; break;
                    case 5:
                        gameStartup.respawnitemtime = gamestartupvals[j]; break;
                    case 6:
                        gameStartup.dukefriction = gamestartupvals[j]; break;
                    case 7:
                        gameStartup.gc = gamestartupvals[j]; 
                        break;
                    case 8: gameStartup.rpgblastradius = gamestartupvals[j]; break;
                    case 9: gameStartup.pipebombblastradius = gamestartupvals[j]; break;
                    case 10: gameStartup.shrinkerblastradius = gamestartupvals[j]; break;
                    case 11: gameStartup.tripbombblastradius = gamestartupvals[j]; break;
                    case 12: gameStartup.morterblastradius = gamestartupvals[j]; break;
                    case 13: gameStartup.bouncemineblastradius = gamestartupvals[j]; break;
                    case 14: gameStartup.seenineblastradius = gamestartupvals[j]; break;

                    case 15:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 16:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 17:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 18:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 19:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 20:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 21:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 22:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 23:
                        gameStartup.max_ammo_amount[j - 14] = gamestartupvals[j];
                        break;
                    case 24:
                        gameStartup.max_ammo_amount[11] = gamestartupvals[j];
                        break;
                    case 25:
                        gameStartup.camerashitable = gamestartupvals[j];
                        break;
                    case 26:
                        gameStartup.numfreezebounces = gamestartupvals[j];
                        break;
                    case 27:
                        gameStartup.freezerhurtowner = gamestartupvals[j];
                        break;
                    case 28:
                        gameStartup.spriteqamount = gamestartupvals[j];
                        if (gameStartup.spriteqamount > 1024) gameStartup.spriteqamount = 1024;
                        else if (gameStartup.spriteqamount < 0) gameStartup.spriteqamount = 0;
                        break;
                    case 29:
                        gameStartup.lasermode = gamestartupvals[j];
                        break;
                }
            }
        }

        //
        // GetIDFromSoundHandle
        //
        private int GetIDFromSoundHandle(string token)
        {
            FieldInfo[] fields = typeof(SoundId).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Name == token)
                {
                    return (int)fields[i].GetValue(null);
                }
            }

            if (numUserSounds >= Globals.NUM_SOUNDS)
                throw new Exception("Too many sounds defined");

            return numUserSounds++;
        }

        public static void HideSprite(spritetype sprite)
        {
            sprite.cstat = MyTypes.SET(sprite.cstat, Flags.CSTAT_SPRITE_INVISIBLE);
        }


        public class VolumeInfo
        {
            public int volumenum;
            public string volumename;
            private List<LevelInfo> levels;

            public LevelInfo this[int index]
            {
                get
                {
                    for (int i = 0; i < levels.Count; i++)
                    {
                        if (levels[i].levelnum == index)
                        {
                            return levels[i];
                        }
                    }
                    throw new Exception("VolumeInfo doesn't have a level defined for " + index);
                }

                set
                {
                    for (int i = 0; i < levels.Count; i++)
                    {
                        if (levels[i].levelnum == index)
                        {
                            levels[i] = value;
                            return;
                        }
                    }
                    throw new Exception("VolumeInfo doesn't have a level defined for " + index);
                }
            }

            public int LevelCount
            {
                get
                {
                    return levels.Count;
                }
            }

            public void DefineLevel(LevelInfo level)
            {
                if (levels == null)
                    levels = new List<LevelInfo>();

                levels.Add(level);
            }
        }

        public struct GameStartup
        {
            public int respawnitemtime;
            public int const_visibility;
            public int impact_damage;
            public int max_player_health;
            public int max_armour_amount;
            public int respawnactortime;
            public int dukefriction;
            public int gc;
            public int rpgblastradius;
            public int pipebombblastradius;
            public int shrinkerblastradius;
            public int tripbombblastradius;
            public int morterblastradius;
            public int bouncemineblastradius;
            public int seenineblastradius;
            public int[] max_ammo_amount;
            public int camerashitable;
            public int numfreezebounces;
            public int freezerhurtowner;
            public int spriteqamount;
            public int lasermode;
        }

        public struct SkillInfo
        {
            public int skillnum;
            public string skillname;
        }

        public class Sounddef
        {
            public string filename;
            //public int pitch1;
            //public int pitch2;
            //public int priority;
            //public int type;
            //public int volume;
        }

        public struct LevelInfo
        {
            public int episodenum;
            public int levelnum;
            public string filename;
            public string partime;
            public string designertime;
            public string mapname;
            public string musicfilename;

          
        }

        public struct Define
        {
            public string name;
            public int value;
        };
    }
}
