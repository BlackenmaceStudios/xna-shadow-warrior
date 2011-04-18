using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using build;
namespace sw
{
    class Anim
    {
        byte[] palette = new byte[768];
        lpfileheader lpheader = new lpfileheader();
        lp_descriptor[] LpArray = new lp_descriptor[256];
        EndianBinaryReader file;

        public uint ANIM_NumFrames ()
        {
           return lpheader.nRecords;
        }


        //
        // Init
        //
        public void Init(string filename)
        {
            file = new EndianBinaryReader(new MemoryStream(Engine.filesystem.kreadfile(filename)));

            lpheader.InitFromFile(ref file);

            file.BaseStream.Position += 128;

            for (int i = 0; i < 768; i+=3)
            {
                palette[i + 0] = file.ReadByte();
                palette[i + 1] = file.ReadByte();
                palette[i + 2] = file.ReadByte();
                file.ReadByte();
            }

            for (int i = 0; i < 256; i++)
            {
                LpArray[i] = new lp_descriptor(ref file);
            }
        }
    }

#region animformat
   class lpfileheader
   {
       public UInt32 id;                 // 4 character ID == "LPF " */
       public UInt16 maxLps;                  // max # largePages allowed. 256 FOR NOW.   */
       public UInt16 nLps;            // # largePages in this file. */
       public UInt32 nRecords;        // # records in this file.  65534 is current limit plus */
                            // one for last-to-first delta for looping the animation */
       public UInt16 maxRecsPerLp; // # records permitted in an lp. 256 FOR NOW.   */
       public UInt16 lpfTableOffset; // Absolute Seek position of lpfTable.  1280 FOR NOW.
                                             // The lpf Table is an array of 256 large page structures
                                             // that is used to facilitate finding records in an anim
                                             // file without having to seek through all of the Large
                                             // Pages to find which one a specific record lives in. */
       public UInt32 contentType;  // 4 character ID == "ANIM" */
       public UInt16 width;                   // Width of screen in pixels. */
       public UInt16 height;                  // Height of screen in pixels. */
       public byte variant;              // 0==ANIM. */
       public byte version;              // 0==frame rate is multiple of 18 cycles/sec.
                                            // 1==frame rate is multiple of 70 cycles/sec.  */
       public byte hasLastDelta;   // 1==Last record is a delta from last-to-first frame. */
       public byte lastDeltaValid; // 0==The last-to-first delta (if present) hasn't been
                                      // updated to match the current first&last frames,    so it
                                      // should be ignored. */
       public byte pixelType;         //   /* 0==256 color. */
       public byte CompressionType;//      /* 1==(RunSkipDump) Only one used FOR NOW. */
       public byte otherRecsPerFrm;//      /* 0 FOR NOW. */
       public byte bitmaptype;     //   /* 1==320x200, 256-color.  Only one implemented so far. */
       public byte[] recordTypes = new byte[32];//      /* Not yet implemented. */
       public UInt32 nFrames;         //   /* In case future version adds other records at end of
                                    //      file, we still know how many actual frames.
                                      //    NOTE: DOES include last-to-first delta when present. */
       public UInt16 framesPerSecond;      // Number of frames to play per second. */
       public UInt16[] pad2 = new UInt16[29];           // 58 bytes of filler to round up to 128 bytes total. */

       public void InitFromFile(ref EndianBinaryReader file)
       {
            id = file.ReadUInt32();                 // 4 character ID == "LPF " */
            maxLps = file.ReadUInt16();                  // max # largePages allowed. 256 FOR NOW.   */
            nLps = file.ReadUInt16();            // # largePages in this file. */
            nRecords = file.ReadUInt32();        // # records in this file.  65534 is current limit plus */
                            // one for last-to-first delta for looping the animation */
            maxRecsPerLp = file.ReadUInt16(); // # records permitted in an lp. 256 FOR NOW.   */
            lpfTableOffset = file.ReadUInt16(); // Absolute Seek position of lpfTable.  1280 FOR NOW.
                                             // The lpf Table is an array of 256 large page structures
                                             // that is used to facilitate finding records in an anim
                                             // file without having to seek through all of the Large
                                             // Pages to find which one a specific record lives in. */
            file.ReadUInt32();  // 4 character ID == "ANIM" */
            file.ReadUInt16();                   // Width of screen in pixels. */
            file.ReadUInt16();                  // Height of screen in pixels. */
            variant = file.ReadByte();              // 0==ANIM. */
            version = file.ReadByte();              // 0==frame rate is multiple of 18 cycles/sec.
                                            // 1==frame rate is multiple of 70 cycles/sec.  */
            hasLastDelta = file.ReadByte();   // 1==Last record is a delta from last-to-first frame. */
            lastDeltaValid = file.ReadByte(); // 0==The last-to-first delta (if present) hasn't been
                                      // updated to match the current first&last frames,    so it
                                      // should be ignored. */
            pixelType = file.ReadByte();         //   /* 0==256 color. */
            CompressionType = file.ReadByte();//      /* 1==(RunSkipDump) Only one used FOR NOW. */
            otherRecsPerFrm = file.ReadByte();//      /* 0 FOR NOW. */
            bitmaptype = file.ReadByte();     //   /* 1==320x200, 256-color.  Only one implemented so far. */
            recordTypes = new byte[32];//      /* Not yet implemented. */
            nFrames = file.ReadUInt32();         //   /* In case future version adds other records at end of
                                    //      file, we still know how many actual frames.
                                      //    NOTE: DOES include last-to-first delta when present. */
            framesPerSecond = file.ReadUInt16();      // Number of frames to play per second. */
            pad2 = new UInt16[29];           // 58 bytes of filler to round up to 128 bytes total. */

            for (int i = 0; i < 29; i++)
            {
                pad2[i] = file.ReadUInt16();
            }
       }
   };

   // this is the format of a large page structure
   class lp_descriptor
   {
       public UInt16 baseRecord;   // Number of first record in this large page.
       public UInt16 nRecords;        // Number of records in lp.
                                                      // bit 15 of "nRecords" == "has continuation from previous lp".
                                              // bit 14 of "nRecords" == "final record continues on next lp".
       public UInt16 nBytes;                  // Total number of bytes of contents, excluding header.

       public lp_descriptor(ref EndianBinaryReader file)
       {
           baseRecord = file.ReadUInt16();
           nRecords = file.ReadUInt16();
           nBytes = file.ReadUInt16();
       }
   };
#endregion
}
