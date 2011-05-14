using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace conscriptbuilder
{
    class Program
    {
        private static ConCompiler _compiler = new ConCompiler();

        //
        // Printf
        //
        public static void Printf(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White; 
            PrintMessage(msg);
        }

        //
        // PrintMessage
        //
        private static void PrintMessage(string msg)
        {
            Console.Write(msg);
            System.Diagnostics.Debug.Write(msg);
        }

        //
        // Warning
        //
        public static void Warning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintMessage("WARNING: ");
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMessage(msg);
        }

        //
        // Error
        //
        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMessage("\n\n" + msg);

            throw new Exception(msg);
        }

        //
        // Main
        //
        private static void Main(string[] args)
        {
            string _workdir, _gameconfilename;

            Printf("CON Script Compiler for " + ConCompiler.VERSION + " Con Files\n");
            Printf("by Justin Marshall\n\n");

            if (args.Length < 1)
            {
                Warning("Call with arguement to game.con path\n");
                return;
            }

            _workdir = Path.GetDirectoryName(args[0]);
            _gameconfilename = Path.GetFileName(args[0]);

            Printf("Path: " + _workdir + "\n");
            Directory.SetCurrentDirectory(_workdir + "\n");

            _compiler.Compile(_gameconfilename);
        }
    }
}
