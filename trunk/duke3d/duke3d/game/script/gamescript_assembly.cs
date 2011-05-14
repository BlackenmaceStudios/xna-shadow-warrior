using System;
using System.Net;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace duke3d.game.script
{
    class GamescriptAssembly
    {
        private Assembly _scriptassembly;
        private Type _scriptclasstype;
        private object _scriptclass;
        private const string _scriptclassname = "Duke3d.Con.PrebuiltConScript";

        public void Init(string filename)
        {
#if !WINDOWS_PHONE
            Stream stream = build.Engine.filesystem.ReadContentFileStream(filename);

            AssemblyPart ap = new AssemblyPart();

            _scriptassembly = ap.Load(stream);
#else
            _scriptassembly = Assembly.Load("game");
#endif
            _scriptclasstype = _scriptassembly.GetType(_scriptclassname);
            _scriptclass = Activator.CreateInstance(_scriptclasstype);
        }

        public Delegate CreateDelegate(Type functype, MethodInfo method)
        {
            return Delegate.CreateDelegate(functype, _scriptclass, method);
        }

        //
        // FindMethod
        //
        public MethodInfo FindMethod(string name)
        {
            return _scriptclasstype.GetMethod(name);
        }

        //
        // GetDelegateType
        //
        public Type GetDelegateType(string name)
        {
            return _scriptassembly.GetType(_scriptclassname + "+" + name, true);
        }

        //
        // SetScriptClassMethod
        //
        public void SetScriptClassMethod(string name, object function)
        {
            _scriptclasstype.GetField(name).SetValue(_scriptclass, function);
        }

        //
        // GetObjectFromAssembly
        //
        public object GetObjectFromAssembly(string name)
        {
            FieldInfo field = _scriptclasstype.GetField(name);

            return field.GetValue(_scriptclass);
        }

        //
        // GetStringValueFromAssembly
        //
        public string GetStringValueFromAssembly(string name)
        {
            FieldInfo field = _scriptclasstype.GetField(name);

            return (string)field.GetValue(_scriptclass);
        }

        //
        // GetValueFromAssembly
        //
        public int GetValueFromAssembly(string name)
        {
            FieldInfo field = _scriptclasstype.GetField(name);

            return (int)field.GetValue(_scriptclass);
        }

        //
        // GetIntArrayFromAssembly
        //
        public int[] GetIntArrayFromAssembly(string name)
        {
            FieldInfo field = _scriptclasstype.GetField(name);

            return (int[])field.GetValue(_scriptclass);
        }

        //
        // GetStringArrayFromAssembly
        //
        public string[] GetStringArrayFromAssembly(string name)
        {
            FieldInfo field = _scriptclasstype.GetField(name);

            return (string [])field.GetValue(_scriptclass);
        }
    }
}
