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

namespace build
{
    //
    // GCServices
    //
    public static class GCServices
    {
        public delegate object AllocDelegate(System.Object obj, System.Runtime.InteropServices.GCHandleType type);

        public static IntPtr AddrOfPinnedObject(object gc)
        {
            Type type = gc.GetType();

            System.Reflection.MethodInfo[] methods = type.GetMethods();

            return (IntPtr)methods[4].Invoke(gc, null);
        }

        public static AllocDelegate Alloc;

        public static void Init(System.Reflection.MethodInfo[] methods)
        {
            Alloc = (AllocDelegate)Delegate.CreateDelegate(typeof(AllocDelegate), methods[2]); 
        }
    }
    //
    // Marshal
    //
    public static class MarshalServices
    {
        public class MarshalDelegates
        {
            public delegate void CopyPtr(byte[] source, int startIndex, IntPtr destination, int length);
            public delegate void Copy(IntPtr source, byte[] destination, int startIndex, int length);
            public delegate int GetLastWin32Error();
            public delegate string PtrToStringUni(IntPtr ptr);
            public delegate object PtrToStructure(IntPtr ptr, Type structureType);
            public delegate byte ReadByte(IntPtr ptr, int ofs);
            public delegate short ReadInt16(IntPtr ptr, int ofs);
            public delegate int ReadInt32(IntPtr ptr, int ofs);
            public delegate long ReadInt64(IntPtr ptr, int ofs);
            public delegate IntPtr ReadIntPtr(IntPtr ptr);
            public delegate int SizeOf(object structure);
            public delegate void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);
            public delegate void WriteByte(IntPtr ptr, int ofs, byte val);
            public delegate void WriteInt16(IntPtr ptr, int ofs, short val);
            public delegate void WriteInt32(IntPtr ptr, int ofs, int val);
            public delegate void WriteInt64(IntPtr ptr, int ofs, long val);
            public delegate void WriteIntPtr(IntPtr ptr, IntPtr val);
        }

        public static MarshalDelegates.CopyPtr CopyPtr;
        public static MarshalDelegates.Copy Copy;
        public static MarshalDelegates.GetLastWin32Error GetLastWin32Error;
        public static MarshalDelegates.PtrToStringUni PtrToStringUni;
        public static MarshalDelegates.PtrToStructure PtrToStructure;
        public static MarshalDelegates.ReadByte ReadByte;
        public static MarshalDelegates.ReadInt16 ReadInt16;
        public static MarshalDelegates.ReadInt32 ReadInt32;
        public static MarshalDelegates.ReadInt64 ReadInt64;
        public static MarshalDelegates.ReadIntPtr ReadIntPtr;
        public static MarshalDelegates.SizeOf SizeOf;
        public static MarshalDelegates.StructureToPtr StructureToPtr;
        public static MarshalDelegates.WriteByte WriteByte;
        public static MarshalDelegates.WriteInt16 WriteInt16;
        public static MarshalDelegates.WriteInt32 WriteInt32;
        public static MarshalDelegates.WriteInt64 WriteInt64;
        public static MarshalDelegates.WriteIntPtr WriteIntPtr;

        public static void Init(System.Reflection.MethodInfo[] methods)
        {
            SizeOf = (MarshalDelegates.SizeOf)Delegate.CreateDelegate(typeof(MarshalDelegates.SizeOf), methods[0]); // {Int32 SizeOf(System.Object)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            ReadByte = (MarshalDelegates.ReadByte)Delegate.CreateDelegate(typeof(MarshalDelegates.ReadByte), methods[1]); // {Byte ReadByte(System.IntPtr, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            ReadInt16 = (MarshalDelegates.ReadInt16)Delegate.CreateDelegate(typeof(MarshalDelegates.ReadInt16), methods[2]); // {Int16 ReadInt16(System.IntPtr, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            ReadInt32 = (MarshalDelegates.ReadInt32)Delegate.CreateDelegate(typeof(MarshalDelegates.ReadInt32), methods[3]); // {Int32 ReadInt32(System.IntPtr, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            ReadInt64 = (MarshalDelegates.ReadInt64)Delegate.CreateDelegate(typeof(MarshalDelegates.ReadInt64), methods[4]); // {Int64 ReadInt64(System.IntPtr, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            ReadIntPtr = (MarshalDelegates.ReadIntPtr)Delegate.CreateDelegate(typeof(MarshalDelegates.ReadIntPtr), methods[5]); // {System.IntPtr ReadIntPtr(System.IntPtr)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            WriteByte = (MarshalDelegates.WriteByte)Delegate.CreateDelegate(typeof(MarshalDelegates.WriteByte), methods[6]); // {Void WriteByte(System.IntPtr, Int32, Byte)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            WriteInt16 = (MarshalDelegates.WriteInt16)Delegate.CreateDelegate(typeof(MarshalDelegates.WriteInt16), methods[7]); // {Void WriteInt16(System.IntPtr, Int32, Int16)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            WriteInt32 = (MarshalDelegates.WriteInt32)Delegate.CreateDelegate(typeof(MarshalDelegates.WriteInt32), methods[8]); // {Void WriteInt32(System.IntPtr, Int32, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            WriteInt64 = (MarshalDelegates.WriteInt64)Delegate.CreateDelegate(typeof(MarshalDelegates.WriteInt64), methods[9]); // {Void WriteInt64(System.IntPtr, Int32, Int64)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            WriteIntPtr = (MarshalDelegates.WriteIntPtr)Delegate.CreateDelegate(typeof(MarshalDelegates.WriteIntPtr), methods[10]); // {Void WriteIntPtr(System.IntPtr, System.IntPtr)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            CopyPtr = (MarshalDelegates.CopyPtr)Delegate.CreateDelegate(typeof(MarshalDelegates.CopyPtr), methods[11]); // {Void Copy(System.Byte[], Int32, System.IntPtr, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            Copy = (MarshalDelegates.Copy)Delegate.CreateDelegate(typeof(MarshalDelegates.Copy), methods[12]); // {Void Copy(System.IntPtr, System.Byte[], Int32, Int32)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            PtrToStringUni = (MarshalDelegates.PtrToStringUni)Delegate.CreateDelegate(typeof(MarshalDelegates.PtrToStringUni), methods[13]); // {System.String PtrToStringUni(System.IntPtr)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            PtrToStructure = (MarshalDelegates.PtrToStructure)Delegate.CreateDelegate(typeof(MarshalDelegates.PtrToStructure), methods[14]); // {System.Object PtrToStructure(System.IntPtr, System.Type)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            StructureToPtr = (MarshalDelegates.StructureToPtr)Delegate.CreateDelegate(typeof(MarshalDelegates.StructureToPtr), methods[15]); // {Void StructureToPtr(System.Object, System.IntPtr, Boolean)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
        }
    }

    //http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.marshal.getdelegateforfunctionpointer.aspx
    //http://stackoverflow.com/questions/3242331/difference-between-dllimport-and-getprocaddress
    public static class DllImport
    {
        private static uint last = 0;

        public static void Init()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.Load("Microsoft.Phone.InteropServices, Version=7.0.0.0, Culture=neutral, PublicKeyToken=24eec0d8c86cda1e");
            Type comBridgeType = null;
            comBridgeType = asm.GetType("Microsoft.Phone.InteropServices.Marshal");

            MarshalServices.Init(comBridgeType.GetMethods());

            comBridgeType = asm.GetType("Microsoft.Phone.InteropServices.GCHandle");
            GCServices.Init(comBridgeType.GetMethods());
        }

        public static uint GetLastRegisterCode()
        {
            return last;
        }
        public static uint Register(string assemblyDLL, string guid)
        {

            System.Reflection.Assembly asm = System.Reflection.Assembly.Load("Microsoft.Phone.InteropServices, Version=7.0.0.0, Culture=neutral, PublicKeyToken=24eec0d8c86cda1e");


            Type comBridgeType = null;

            comBridgeType = asm.GetType("Microsoft.Phone.InteropServices.ComBridge");
            var dynMethod = comBridgeType.GetMethod("RegisterComDll", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            return last = (uint)dynMethod.Invoke(null, new object[] { assemblyDLL, new Guid(guid) });
        }
    }
}