using System;
using System.Net;
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
        enum Scriptkeyword
        {
            definelevelname = 0,  // 0
            actor,            // 1    [#]
            addammo,   // 2    [#]
            ifrnd,            // 3    [C]
            enda,             // 4    [:]
            ifcansee,         // 5    [C]
            ifhitweapon,      // 6    [#]
            action,           // 7    [#]
            ifpdistl,         // 8    [#]
            ifpdistg,         // 9    [#]
            _else,             // 10   [#]
            strength,         // 11   [#]
            _break,            // 12   [#]
            shoot,            // 13   [#]
            palfrom,          // 14   [#]
            sound,            // 15   [filename.voc]
            fall,             // 16   []
            state,            // 17
            ends,             // 18
            define,           // 19
            //   //,               // 20
            ifai,             // 21
            killit,           // 22
            addweapon,        // 23
            ai,               // 24
            addphealth,       // 25
            ifdead,           // 26
            ifsquished,       // 27
            sizeto,           // 28
            _openbracket,                // 29
            _closebracket,                // 30
            spawn,            // 31
            move,             // 32
            ifwasweapon,      // 33
            ifaction,         // 34
            ifactioncount,    // 35
            resetactioncount, // 36
            debris,           // 37
            pstomp,           // 38
            // /*,               // 39
            cstat,            // 40
            ifmove,           // 41
            resetplayer,      // 42
            ifonwater,        // 43
            ifinwater,        // 44
            ifcanshoottarget, // 45
            ifcount,          // 46
            resetcount,       // 47
            addinventory,     // 48
            ifactornotstayput,// 49
            hitradius,        // 50
            ifp,              // 51
            count,            // 52
            ifactor,          // 53
            music,            // 54
            include,          // 55
            ifstrength,       // 56
            definesound,      // 57
            guts,             // 58
            ifspawnedby,      // 59
            gamestartup,      // 60
            wackplayer,       // 61
            ifgapzl,          // 62
            ifhitspace,       // 63
            ifoutside,        // 64
            ifmultiplayer,    // 65
            operate,          // 66
            ifinspace,        // 67
            debug,            // 68
            endofgame,        // 69
            ifbulletnear,     // 70
            ifrespawn,        // 71
            iffloordistl,     // 72
            ifceilingdistl,   // 73
            spritepal,        // 74
            ifpinventory,     // 75
            betaname,         // 76
            cactor,           // 77
            ifphealthl,       // 78
            definequote,      // 79
            quote,            // 80
            ifinouterspace,   // 81
            ifnotmoving,      // 82
            respawnhitag,        // 83
            tip,             // 84
            ifspritepal,      // 85
            money,         // 86
            soundonce,         // 87
            addkills,         // 88
            stopsound,        // 89
            ifawayfromwall,       // 90
            ifcanseetarget,   // 91
            globalsound,  // 92
            lotsofglass, // 93
            ifgotweaponce, // 94
            getlastpal, // 95
            pkick,  // 96
            mikesnd, // 97
            useractor,  // 98
            sizeat,  // 99
            addstrength, // 100   [#]
            cstator, // 101
            mail, // 102
            paper, // 103
            tossweapon, // 104
            sleeptime, // 105
            nullop, // 106
            definevolumename, // 107
            defineskillname, // 108
            ifnosounds, // 109
            clipdist, // 110
            ifangdiffl, // 111

            NUMKEYWORDS
        };

        List<Define> defines = new List<Define>();
        List<Quote> quotes = new List<Quote>();
        public List<VolumeInfo> volumes = new List<VolumeInfo>();
        public List<SkillInfo> skills = new List<SkillInfo>();
        Sounddef[] sounddefs = new Sounddef[Globals.NUM_SOUNDS];

        string _titlesong, _endsong;
        private int numUserSounds = 396;
        
        public GameStartup gameStartup;

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

        public int GetConstantValue(string name)
        {
            for (int i = 0; i < defines.Count; i++)
            {
                if (defines[i].name == name)
                    return defines[i].value;
            }

            throw new Exception(name + " is undefined");
        }

        public void DefineQuote(string name, string value)
        {
            Quote quote = new Quote();

            quote.name = name;
            quote.quote = value;

            quotes.Add(quote);
        }

        public void DefineValue(string name, string value)
        {
            Define define = new Define();
            int val = 0;

            define.name = name;

            // Check to see if this define should have a value of a previous define.
            if (Int32.TryParse(value, out val) == false)
            {
                for (int i = 0; i < defines.Count; i++)
                {
                    if (value == defines[i].name)
                    {
                        val = defines[i].value;
                        break;
                    }
                }
            }

            define.value = val;

            defines.Add(define);
        }

        private Scriptkeyword GetkeywordFromToken(string token)
        {
            Type keywordtype = typeof(Scriptkeyword);

            for (Scriptkeyword i = 0; i < Scriptkeyword.NUMKEYWORDS; i++)
            {
                if (i == Scriptkeyword._else && token == "else")
                    return i;

                if (i == Scriptkeyword._break && token == "break")
                    return i;

                if (i == Scriptkeyword._openbracket && token == "{")
                    return i;

                if (i == Scriptkeyword._closebracket && token == "}")
                    return i;

                if (token == Enum.GetName(keywordtype, i))
                {
                    return i;
                }
            }

            throw new Exception("Unknown or invalid token " + token);
        }

        //
        // Init
        //
        public void Init(string buffer)
        {
            Parse(buffer);
        }

        

        private void ParseGameStartup(bParser parser)
        {
            gameStartup.max_ammo_amount = new int[14];

            for( int j = 0; j < 30; j++ )
            {
                string token = parser.NextToken;
                switch (j)
                {
                    case 0:
                        gameStartup.const_visibility = GetConstantValue(token);
                        break;
                    case 1:
                        gameStartup.impact_damage = GetConstantValue(token);
                        break;
                    case 2:
                        gameStartup.max_player_health = GetConstantValue(token);
                        break;
                    case 3:
                        gameStartup.max_armour_amount = GetConstantValue(token);
                        break;
                    case 4:
                        gameStartup.respawnactortime = GetConstantValue(token); break;
                    case 5:
                        gameStartup.respawnitemtime = GetConstantValue(token); break;
                    case 6:
                        gameStartup.dukefriction = GetConstantValue(token); break;
                    case 7:
                        gameStartup.gc = GetConstantValue(token); 
                        break;
                    case 8: gameStartup.rpgblastradius = GetConstantValue(token); break;
                    case 9: gameStartup.pipebombblastradius = GetConstantValue(token); break;
                    case 10: gameStartup.shrinkerblastradius = GetConstantValue(token); break;
                    case 11: gameStartup.tripbombblastradius = GetConstantValue(token); break;
                    case 12: gameStartup.morterblastradius = GetConstantValue(token); break;
                    case 13: gameStartup.bouncemineblastradius = GetConstantValue(token); break;
                    case 14: gameStartup.seenineblastradius = GetConstantValue(token); break;

                    case 15:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 16:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 17:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 18:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 19:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 20:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 21:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 22:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 23:
                        gameStartup.max_ammo_amount[j - 14] = GetConstantValue(token);
                        break;
                    case 24:
                        gameStartup.max_ammo_amount[11] = GetConstantValue(token);
                        break;
                    case 25:
                        gameStartup.camerashitable = GetConstantValue(token);
                        break;
                    case 26:
                        gameStartup.numfreezebounces = GetConstantValue(token);
                        break;
                    case 27:
                        gameStartup.freezerhurtowner = GetConstantValue(token);
                        break;
                    case 28:
                        gameStartup.spriteqamount = GetConstantValue(token);
                        if (gameStartup.spriteqamount > 1024) gameStartup.spriteqamount = 1024;
                        else if (gameStartup.spriteqamount < 0) gameStartup.spriteqamount = 0;
                        break;
                    case 29:
                        gameStartup.lasermode = GetConstantValue(token);
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

        //
        // Parse
        //
        private void Parse(string buffer)
        {
            Scriptkeyword keyw;
            bParser parser = new bParser( buffer );

            while (true)
            {
                string token = parser.NextToken;

                if (token == null || token.Length <= 0)
                    break;

                keyw = GetkeywordFromToken(token);

                switch (keyw)
                {
                    case Scriptkeyword.definesound:
                        {
                            Sounddef sounddef = new Sounddef();
                            int id = GetIDFromSoundHandle(parser.NextToken);

                            sounddef.filename = parser.NextToken;
                            sounddef.pitch1 = parser.NextInt;
                            sounddef.pitch2 = parser.NextInt;
                            sounddef.priority = parser.NextInt;
                            sounddef.type = parser.NextInt;
                            sounddef.volume = parser.NextInt;

                            sounddefs[id] = sounddef;
                        }
                        break;
                    case Scriptkeyword.define:
                        {
                            string val;
                            
                            token = parser.NextToken;
                            val = parser.NextToken;

                            DefineValue(token, val);
                        }
                        break;

                    case Scriptkeyword.gamestartup:
                        ParseGameStartup(parser);
                        break;

                    case Scriptkeyword.definequote:
                        {
                            string val;

                            token = parser.NextToken;
                            val = parser.ParseRestOfLine();

                            DefineQuote(token, val);
                        }
                        break;

                    case Scriptkeyword.music:
                        {
                            int volumenum = parser.NextInt - 1;

                            if (volumenum == -1)
                            {
                                _titlesong = parser.NextToken;
                                _endsong = parser.NextToken;
                            }
                            else
                            {
                                for (int i = 0; i < volumes[volumenum].LevelCount; i++)
                                {
                                    LevelInfo li = volumes[volumenum][i];
                                    li.musicfilename = parser.NextToken;
                                    volumes[volumenum][i] = li;
                                }
                            }
                        }
                        break;

                    case Scriptkeyword.definevolumename:
                        {
                            VolumeInfo volume = new VolumeInfo();

                            volume.volumenum = parser.NextInt;
                            volume.volumename = parser.ParseRestOfLine();

                            volumes.Add(volume);
                        }
                        break;

                    case Scriptkeyword.defineskillname:
                        {
                            SkillInfo skill = new SkillInfo();

                            skill.skillnum = parser.NextInt;
                            skill.skillname = parser.ParseRestOfLine();

                            skills.Add(skill);
                        }
                        break;

                    case Scriptkeyword.definelevelname:
                        {
                            LevelInfo level = new LevelInfo();

                            level.episodenum = parser.NextInt;
                            level.levelnum = parser.NextInt;
                            level.filename = parser.NextToken;
                            level.partime = parser.NextToken;
                            level.designertime = parser.NextToken;
                            level.mapname = parser.ParseRestOfLine();

                            volumes[level.episodenum].DefineLevel(level);
                        }
                        break;



                    case Scriptkeyword.include:
                        {
                            string scriptname = parser.NextToken;

                            kFile fil = Engine.filesystem.kopen4load(scriptname);
                            if (fil == null)
                                throw new Exception("GameScript::Parse: Failed to open " + scriptname);

                            string buf = fil.ReadFile();

                            fil.Close();

                            Parse(buf);
                        }
                        break;

                    default:
                        //parser.ParseRestOfLine();
                        //break;
                        System.Diagnostics.Debug.WriteLine("WARNING: Keyword not implemented aborting parsing");
                        return;
                }
            }
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

        public struct Sounddef
        {
            public string filename;
            public int pitch1;
            public int pitch2;
            public int priority;
            public int type;
            public int volume;
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

        public struct Quote
        {
            public string name;
            public string quote;
        }
    }
}
