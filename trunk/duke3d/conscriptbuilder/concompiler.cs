using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Reflection.Emit;
using Microsoft.CSharp;

namespace conscriptbuilder
{
    class ConCompiler
    {
        private CSharpCodeProvider codeprovider = new CSharpCodeProvider();
        public static string VERSION = "Atomic Edition 1.5";
        private static string _silverlightRootFolder = "C:\\Program Files (x86)\\Reference Assemblies\\Microsoft\\Framework\\Silverlight\\v4.0\\Profile\\WindowsPhone\\";
       // private static string _duke3dProjectFolder = "C:\\dev\\duke3d\\build\\Bin\\Debug\\";

        private readonly string[] _DefaultReferencedAssembliesSilverlight = new string[] { 
                    @_silverlightRootFolder + "mscorlib.dll",
                    @_silverlightRootFolder + "system.dll",
                //    @_silverlightRootFolder + "System.Core.dll",
                 //   @_silverlightRootFolder + "System.Net.dll",
                  //  @_silverlightRootFolder + "System.Windows.dll",
                  //  @_silverlightRootFolder + "System.Windows.Browser.dll",
                   // @_silverlightRootFolder + "System.Xml.dll",
                  //  @_duke3dProjectFolder + "build.dll",
                };

        private bool _infunction = false;
        private string codebuffer = "";
        private List<string> _quotes = new List<string>();
        private List<string> _volumenames = new List<string>();
        private List<string> _skillnames = new List<string>();
        private List<string>[] _music;
        private List<string> _sndnames = new List<string>();
        private List<string> _sndfilenames = new List<string>();
        private List<string> _mapfilenames = new List<string>();
        private List<string> _mapnames = new List<string>();
        private List<string> _mappartimes = new List<string>();
        private List<string> _mapdesignertimes = new List<string>();
        private Dictionary<string, int> _defines = new Dictionary<string, int>();

        //private string _mapblock = "";
        public string _buffer = "";
        public bool _elseifhack = false;
        private void WriteCodeLine(string line)
        {
            codebuffer += line + '\n';
        }
        private void WriteBufferCodeLine(string line)
        {
            _buffer += line + '\n';
        }

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

            // jv
            hidesprite,
         //   seekplayer,
            // jv end
            NUMKEYWORDS
        };

        private int isKeyword(string token)
        {
            Type keywordtype = typeof(Scriptkeyword);

            for (Scriptkeyword i = 0; i < Scriptkeyword.NUMKEYWORDS; i++)
            {
                if (i == Scriptkeyword._else && token == "else")
                    return (int)i;

                if (i == Scriptkeyword._break && token == "break")
                    return (int)i;

                if (i == Scriptkeyword._openbracket && token == "{")
                    return (int)i;

                if (i == Scriptkeyword._closebracket && token == "}")
                    return (int)i;

                if (token == Enum.GetName(keywordtype, i))
                {
                    return (int)i;
                }
            }

            return -1;
        }

        private Scriptkeyword GetkeywordFromToken(string token)
        {
            int val = isKeyword(token);

            if (val == -1)
            {
                Program.Error("Unknown or invalid token " + token);
                return (Scriptkeyword)0;
            }

            return (Scriptkeyword)val;
        }

        //
        // ReadScriptFile
        //
        private string ReadScriptFile(string filename)
        {
            StreamReader _reader = new StreamReader(filename);
            string buffer;

            if (_reader == null)
                Program.Error("ReadScriptFile: Unable to open file " + filename);

            buffer = _reader.ReadToEnd();

            _reader.Close();
            return buffer;
        }

        //
        // DefineConstant
        //
        private void DefineConstant(string name, string value)
        {
            int val = 0;

            if (int.TryParse(value, out val) == false)
            {
                val = _defines[value];
            }

            _defines.Add(name, val);
            WriteCodeLine(" public const int " + name + " = " + value + ";");
        }

        //
        // CreateFunctionCall
        //
        private void CreateFunctionCall(bParser parser, Scriptkeyword keyword)
        {
            string keywordstr = System.Enum.GetName(typeof(Scriptkeyword), keyword);
            List<string> parms = new List<string>();

            string codeline = null;
            bool _ifstatement = false;

            if ((keywordstr[0] == 'i' || keywordstr[0] == 'I') && (keywordstr[1] == 'f' || keywordstr[1] == 'F'))
            {
                _ifstatement = true;
            }

            if (_ifstatement)
            {
                codeline = "if(" + keywordstr + "(actor ";
            }
            else
            {
                codeline =  keywordstr + "(actor ";
            }
            
            while (true)
            {
                int val;
                string token = parser.NextToken;
                if (isKeyword(token) != -1)
                {
                    parser.UngetToken();
                    break;
                }

                if (int.TryParse(token, out val))
                {
                    parms.Add("\"" + val + "\"");
                }
                else
                {
                    parms.Add(token);
                }
            }

            for (int i = 0; i < parms.Count; i++)
            {
                codeline += ',' + parms[i];
            }

            if (_ifstatement)
            {
                codeline += "))";
            }
            else
            {
                codeline += ");";
            }

            WriteCodeLine(codeline);
        }

        //
        // KeywordHandler
        //
        private bool KeywordHandler(bParser parser, Scriptkeyword keyword)
        {
            switch (keyword)
            {
                case Scriptkeyword.include:
                    ParseScript(parser.NextToken);
                    break;

                case Scriptkeyword.define:
                    {
                        string _name, _value;

                        _name = parser.NextToken;
                        _value = parser.NextToken;

                        DefineConstant(_name, _value);
                    }
                    break;

                case Scriptkeyword.music:
                    {
                        int volumenum = parser.NextInt;

                        if (volumenum == 0)
                        {
                            WriteCodeLine("public readonly string titlesong = \"" + parser.NextToken + "\";");
                            WriteCodeLine("public readonly string endsong = \"" + parser.NextToken + "\";");
                        }
                        else
                        {
                            volumenum = volumenum - 1;

                            int numlevelsinvolume = _music[volumenum].Count;
                            _music[volumenum].Clear();

                            for (int i = 0; i < numlevelsinvolume; i++)
                            {
                                _music[volumenum].Add(parser.NextToken);
                            }
                        }
                    }
                    break;

                case Scriptkeyword.gamestartup:
                    {
                        WriteCodeLine("public int[] gamestartupvals = new int[] {");

                        for (int i = 0; i < 30; i++)
                        {
                            WriteCodeLine( parser.NextToken + "," );
                        }

                        WriteCodeLine("-1");
                        WriteCodeLine("};");
                    }
                    break;

                case Scriptkeyword.definequote:
                    {
                        int tokenum = parser.NextInt;

                        _quotes.Add(parser.ParseRestOfLine());
                    }
                    break;

                case Scriptkeyword.definevolumename:
                    {
                        int tokenum = parser.NextInt;

                        _volumenames.Add(parser.ParseRestOfLine());
                    }
                    break;

                case Scriptkeyword.defineskillname:
                    {
                        int tokenum = parser.NextInt;

                        _skillnames.Add(parser.ParseRestOfLine());
                    }
                    break;

                case Scriptkeyword.definesound:
                    string sndname = parser.NextToken;
                    string filename = parser.NextToken;

                    _sndnames.Add(sndname);
                    _sndfilenames.Add(filename);

                    parser.ParseRestOfLine();
                    break;

                case Scriptkeyword.state:
                    if (_infunction)
                    {
                        WriteCodeLine(parser.NextToken + "(actor);");
                    }
                    else
                    {
                        WriteCodeLine("public void " + parser.NextToken + " (object actor) { ");
                        _infunction = true;
                    }
                    break;

                case Scriptkeyword.ends:
                    WriteCodeLine("}");
                    _infunction = false;
                    break;

                // jv - not working yet.
                case Scriptkeyword.useractor:
                    {
                        string s = parser.NextToken;
                        if(s != "notenemy" && s != "enemy")
                           parser.UngetToken();
                        WriteCodeLine("public void ScriptFunction_" + parser.NextToken + " (object actor) { ");
                        int argnum = 0;
                        while (true)
                        {
                            string token = parser.GetNextTokenFromLine();

                            if (token == null || token.Length <= 0)
                            {
                                break;
                            }

                            if (isKeyword(token) != -1)
                            {
                                parser.UngetToken();
                                break;
                            }

                            WriteCodeLine("string arg_" + argnum + " = \"" + token + "\";");
                            argnum++;
                        }
                        _infunction = true;
                    }
                    break;
                case Scriptkeyword.ai:
                    if (_infunction)
                    {
                        CreateFunctionCall(parser, keyword);
                    }
                    else
                    {
                        WriteCodeLine("public static object[] " + parser.NextToken + " = new object[] {");
                        while (true)
                        {
                            string token = parser.NextToken;
                            if (isKeyword(token) != -1)
                            {
                                parser.UngetToken();
                                break;
                            }
                            WriteCodeLine(token + ",");
                        }
                        WriteCodeLine("};");
                    }
                    break;
                //case Scriptkeyword.seekplayer:

                  //  break;
                // jv end

                case Scriptkeyword.action:
                    if (_infunction)
                    {
                        CreateFunctionCall(parser, keyword);
                    }
                    else
                    {
                        WriteCodeLine("public static int[] " + parser.NextToken + " = new int[] {");
                        while (true)
                        {
                            string token = parser.GetNextTokenFromLine();

                            if (token == null || token.Length <= 0)
                            {
                                break;
                            }

                            if (isKeyword(token) != -1)
                            {
                                parser.UngetToken();
                                break;
                            }

                            WriteCodeLine("" + int.Parse(token) + ",");
                        }
                        WriteCodeLine("};");
                    }
                    break;

                case Scriptkeyword.move:
                    if (_infunction)
                    {
                        CreateFunctionCall(parser, keyword);
                    }
                    else
                    {
                        WriteCodeLine("public static string[] " + parser.NextToken + " = new string[] {");
                        while (true)
                        {
                            int val;
                            string token = parser.GetNextTokenFromLine();

                            if (token == null || token.Length <= 0)
                            {
                                break;
                            }

                            if (int.TryParse(token, out val))
                            {
                                WriteCodeLine("\"" + val + "\"" + ",");
                            }
                            else
                            {
                                WriteCodeLine(token + ",");
                            }
                        }
                        WriteCodeLine("};");
                    }
                    break;

                case Scriptkeyword.actor:
                    {
                        int picnum = _defines[parser.NextToken];
                        WriteCodeLine("public void ScriptFunction_" + picnum + " (object actor) { ");
                        int argnum = 0;
                        while (true)
                        {
                            string token = parser.GetNextTokenFromLine();

                            if (token == null || token.Length <= 0)
                            {
                                break;
                            }

                            if (isKeyword(token) != -1)
                            {
                                parser.UngetToken();
                                break;
                            }

                            WriteCodeLine("string arg_" + argnum + " = \"" + token + "\";");
                            argnum++;
                        }
                        _infunction = true;
                    }
                    break;

                case Scriptkeyword.enda:
                    WriteCodeLine("}");
                    _infunction = false;
                    break;

                case Scriptkeyword._break:
                    WriteCodeLine("return;");
                    break;

                case Scriptkeyword._else:
                    {
                        int position = parser.Position;
                        string token = parser.NextToken;
                        if ((token[0] == 'i' || token[0] == 'I') && (token[1] == 'f' || token[1] == 'F'))
                        {
                            codebuffer += "else ";
                            CreateFunctionCall(parser, GetkeywordFromToken(token));
                            //WriteCodeLine("{");
                            break;
                        }

                        parser.Position = position;

                        // jv - hack cause con scripts are littered with bad if/else statements.
                        WriteCodeLine("else if( true ) ");
                        // jv end
                    }
                    break;

                case Scriptkeyword._openbracket:
                    WriteCodeLine("{");
                    break;

                case Scriptkeyword._closebracket:
                    WriteCodeLine("}");
                    break;

                case Scriptkeyword.definelevelname:
                    {
                        int episodenum = parser.NextInt;
                        int levelnum = parser.NextInt;

                        /*
                        _mapblock += "if(episodenum == " + episodenum + @" && levelnum == " + levelnum + @" ) { " ;
                        _mapblock += "filename = \"" + parser.NextToken + "\";"  ;
                        _mapblock += "partime = \"" + parser.NextToken + "\";" ;
                        _mapblock += "designertime = \"" + parser.NextToken + "\";" ;
                        _mapblock += "levelname = \"" + parser.ParseRestOfLine() + "\";" ;
                        _mapblock += "return; \n }\n";
                        */

                        _mapfilenames.Add(parser.NextToken);
                        _mappartimes.Add(parser.NextToken);
                        _mapdesignertimes.Add(parser.NextToken);
                        _mapnames.Add(parser.ParseRestOfLine());

                        if (_music == null)
                        {
                            _music = new List<string>[_volumenames.Count];
                        }
                        if (_music[episodenum] == null)
                        {
                            _music[episodenum] = new List<string>();
                        }
                        _music[episodenum].Add("nomusic");
                    }
                    break;

                default:
                    CreateFunctionCall(parser, keyword);
                    break;
            }

            return true;
        }

        private void CreateStringArray(string name, string[] values)
        {
            int num = 0;

            WriteCodeLine("public string[] " + name + " = new string[] { ");
            foreach (string s in values)
            {
                if (num > 0)
                    codebuffer += "," + '\n';

                codebuffer += "\"" + s + "\"";
                num++;
            }
            WriteCodeLine(" }; ");
        }

        private void CreateGetLevelInfoFunction()
        {
            CreateStringArray("levelfilenames", _mapfilenames.ToArray());
            CreateStringArray("levelpartimes", _mappartimes.ToArray());
            CreateStringArray("leveldesignertimes", _mapdesignertimes.ToArray());
            CreateStringArray("levelnames", _mapnames.ToArray());
        }

        private void CreateGameFunctionDelegate(string name)
        {
            WriteCodeLine("public ScriptFunction " + name + ";");
        }

        private void DefineQuotes()
        {
            int num = 0;

            WriteCodeLine("public string[] quotes = new string[] { ");
            foreach (string s in _quotes)
            {
                if (num > 0)
                    codebuffer += "," + '\n';

                codebuffer += "\"" + s + "\"";
                num++;
            }
            WriteCodeLine(" }; ");
        }

        private void DefineVolumeNames()
        {
            int num = 0;

            WriteCodeLine("public string[] volumenames = new string[] { ");
            foreach (string s in _volumenames)
            {
                if (num > 0)
                    codebuffer += "," + '\n';

                codebuffer += "\"" + s + "\"";
                num++;
            }
            WriteCodeLine(" }; ");
        }

        private void DefineSkillNames()
        {
            int num = 0;

            WriteCodeLine("public string[] skillnames = new string[] { ");
            foreach (string s in _skillnames)
            {
                if (num > 0)
                    codebuffer += "," + '\n';

                codebuffer += "\"" + s + "\"";
                num++;
            }
            WriteCodeLine(" }; ");
        }

        private void CreateGetMusicFunction()
        {
            /*
            WriteCodeLine("public string GetMusicForLevel( int episodenum, int levelnum ) {");

            for( int i = 0; i < _music.Length; i++ )
            {
                for(int d = 0; d < _music[i].Count; d++ )
                {
                    WriteCodeLine("if(episodenum == " + i + " && levelnum == " + d + " ) { ");
                    WriteCodeLine("return \"" + _music[i][d] + "\";");
                    WriteCodeLine("}");
                }
            }
            WriteCodeLine("return null;");
            WriteCodeLine("}");
            */

            for (int i = 0; i < _music.Length; i++)
            {
                WriteCodeLine("public string[] vol" + i + "_music = new string[] {");
                for (int d = 0; d < _music[i].Count; d++)
                {
                    WriteCodeLine("\"" + _music[i][d] + "\",");
                }
                WriteCodeLine("};");
                WriteCodeLine("public int vol" + i + "_numlevels = " + _music[i].Count + ";");
            }
        }

        private void CreateGameFunctions()
        {
            Type keywordtype = typeof(Scriptkeyword);

            for (Scriptkeyword i = 0; i < Scriptkeyword.NUMKEYWORDS; i++)
            {
                CreateGameFunctionDelegate(Enum.GetName(keywordtype, i));
            }
        }

        private void CompileToAssembly()
        {
            CompilerResults _results;
            CompilerParameters _buildparms = new CompilerParameters();
            WriteBufferCodeLine("using System;");
           // WriteBufferCodeLine("using build;");
            WriteBufferCodeLine(" ");
            WriteBufferCodeLine("     namespace Duke3d.Con");
            WriteBufferCodeLine("    {");
            WriteBufferCodeLine("   public class PrebuiltConScript");
            WriteBufferCodeLine("   {");
            WriteBufferCodeLine("   public delegate bool ScriptFunction(object actor, params object[] parms );");
            WriteBufferCodeLine(codebuffer);
            WriteBufferCodeLine("   }");
            WriteBufferCodeLine("}");

            _buildparms.WarningLevel = 0;
            //_buildparms.GenerateInMemory = true;
            _buildparms.OutputAssembly = "../atomicgame.dll";
            _buildparms.IncludeDebugInformation = true;
            _buildparms.CompilerOptions = " /nostdlib ";
            _buildparms.ReferencedAssemblies.AddRange(_DefaultReferencedAssembliesSilverlight);

            _buffer = _buffer.Replace("\\n", "   ");
            _buffer = _buffer.Replace(";", "; ");

            StreamWriter _writer = new StreamWriter("atomicscript.cs");

            _writer.Write(_buffer);
            _writer.Close();

            _results = codeprovider.CompileAssemblyFromSource(_buildparms, _buffer);

            if (_results.Errors.Count > 0)
            {
                Program.Error(_results.Errors[0].ErrorText);
                return;
            }
        }

        //
        // DefineSoundList
        //
        private void DefineSoundList()
        {
            
            int num = 0;

            CreateStringArray("soundIdList", _sndnames.ToArray());
            /*
            //WriteCodeLine("enum SoundId {");
            foreach (string s in _sndnames)
            {
              //  if (num > 0)
                //    codebuffer += "," + '\n';

                //codebuffer += s;
                WriteCodeLine("public const int " + s + " = " + num + ";");
                num++;
            }
            //WriteCodeLine("};");
            */
            num = 0;
            WriteCodeLine("public string[] SoundFileList = new string[] {");
            foreach (string s in _sndfilenames)
            {
                if (num > 0)
                    codebuffer += "," + '\n';

                codebuffer += "\"" + s + "\"";
                num++;
            }
            WriteCodeLine("};");
        }

        //
        // Compile
        //
        public void Compile(string filename)
        {
            ParseScript(filename);

            CreateGameFunctions();

            DefineQuotes();

            DefineVolumeNames();

            DefineSkillNames();

            CreateGetLevelInfoFunction();

            CreateGetMusicFunction();

            DefineSoundList();

            CompileToAssembly();

            Program.Printf("Compile Complete" + "\n");
        }

        //
        // ParseScript
        //
        private void ParseScript(string filename)
        {
            bParser parser = new bParser(ReadScriptFile(filename));

            Program.Printf("Compiling " + filename + "..." + "\n");

            while (true)
            {
                string token = parser.NextToken;

                if (token == null || token.Length <= 0)
                    break;

                if (!KeywordHandler(parser, GetkeywordFromToken(token)))
                    return;
            }
        }
    }
}
