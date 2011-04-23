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
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;


namespace build
{
    public unsafe static class Utility
    {
        public static IBUtility lib;
        public static void Init()
        {
            DllImport.Init();
            DllImport.Register("buildnative.dll", "F0D5AFD8-DA24-4e85-9335-BEBCADE5B92A");

            var c = new BUtilityClass();
            lib = c as IBUtility;
/*
            int[] buffer = new int[1000000];

            object ptrtesthandle = (object)GCServices.Alloc(buffer, GCHandleType.Pinned);
            IntPtr ptrtest = GCServices.AddrOfPinnedObject(ptrtesthandle);

            Stopwatch test1 = new Stopwatch();
            test1.Start();
            for (int i = 0; i < 1000000; i++)
            {
                buffer[i] = 1;
            }
            test1.Stop();

            Stopwatch test2 = new Stopwatch();
            test2.Start();

            utility.ClearVideoBuffer(1000000 * sizeof(int), ptrtest);
            test2.Stop();

            TimeSpan netcopy = new TimeSpan( test1.GetElapsedDateTimeTicks() );
            TimeSpan nativecopy = new TimeSpan(test2.GetElapsedDateTimeTicks() );

            Engine.Printf("---Elapsed Time for setting 1,000,000 ints to 0---");
            Engine.Printf(".NET Array Iteration took "  + netcopy.Milliseconds + " Milliseconds");
            Engine.Printf("C++ Memset " + nativecopy.Milliseconds + " Milliseconds");
            

            ptrtest = ptrtest;
*/
        }
    }

    [ComImport, Guid("F0D5AFD8-DA24-4e85-9335-BEBCADE5B92A"), ClassInterface(ClassInterfaceType.None)]
    public class BUtilityClass { }

    [ComImport, Guid("2C49FA3D-C6B7-4168-BE80-D044A9C0D9DD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe partial interface IBUtility
    {
        [PreserveSig]
        int BAlloc(int size, out IntPtr handle);

        [PreserveSig]
        int ClearVideoBuffer(int size, IntPtr ptr);

        [PreserveSig]
        int krecipasm(int i);

        [PreserveSig]
        int InitTables(int* recripttableptr);
    }
}
