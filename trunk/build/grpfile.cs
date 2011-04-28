using System;
using System.IO;
using System.Collections.Generic;

namespace build
{
    class bGrpArchive
    {
        private BinaryReader grpFile;
        public BinaryReader grpBuffer;

        private int numFilesInGrp;

        private bGrpLump[] lumps;

        private int KENGRP_HEADERSIZE = 16;
        private int KENGRP_LUMPSIZE = 16;

        struct bGrpLump
        {
            public string lumpName; // 12 bytes
            public int lumpOffset;
            public int lumpSize;
        };

        public bGrpArchive(string grpFilePath)
        {
            Init(null, grpFilePath);
        }

        public bGrpArchive(Stream grpFileStream)
        {
            Init(grpFileStream, "");
        }

        public byte[] ReadFile(string filename)
        {
            filename = filename.ToLower();
            for (int i = 0; i < numFilesInGrp; i++)
            {
                if (lumps[i].lumpName == filename)
                {
                    grpBuffer.BaseStream.Position = lumps[i].lumpOffset;
                    return grpBuffer.ReadBytes(lumps[i].lumpSize);
                }
            }

            return null;
        }

        private void Init(Stream memstream, string grpFilePath)
        {
            Engine.Printf("bGrpArchive::Init: Loading " + grpFilePath);

            if (memstream != null)
            {
                grpFile = new BinaryReader(memstream);
            }
            else
            {
                grpFile = new BinaryReader(new MemoryStream(Engine.filesystem.ReadContentFile(grpFilePath)));
            }
            if (grpFile == null)
            {
                throw new Exception("bGrpArchive::Init Failed to load " + grpFilePath);
            }

            grpBuffer = new BinaryReader(new MemoryStream(grpFile.ReadBytes((int)grpFile.BaseStream.Length)));

            // Dispose of the HD binary reader since its now loaded into memory.
            grpFile.Dispose();

            // Check the file header to ensure its a valid grp file.
            Engine.Printf("...Checking Integrity");
            string header = new string(grpBuffer.ReadChars(12));
            if (header != "KenSilverman")
            {
                throw new Exception("bGrpArchive::Init Invalid Archive");
            }

            numFilesInGrp = grpBuffer.ReadInt32();
            Engine.Printf("..." + numFilesInGrp + " files");

            int offset = KENGRP_HEADERSIZE + (KENGRP_LUMPSIZE * numFilesInGrp);

            lumps = new bGrpLump[numFilesInGrp];
            for (int i = 0; i < numFilesInGrp; i++)
            {
                lumps[i].lumpName = new string(grpBuffer.ReadChars(12)).ToLower().Trim('\0');
                lumps[i].lumpSize = grpBuffer.ReadInt32();
                lumps[i].lumpOffset = offset;

                offset += lumps[i].lumpSize;
            }
        }
    }
}
