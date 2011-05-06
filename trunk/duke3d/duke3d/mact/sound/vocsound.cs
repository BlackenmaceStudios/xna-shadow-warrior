using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using build;

using Microsoft.Xna.Framework.Audio;

namespace mact.sound
{
    //
    // bSoundEffectVoc
    //
    public class bSoundEffectVoc : bSoundEffect
    {
        private VocHeader _header = new VocHeader();
        private List<VocBlockData> blocks = new List<VocBlockData>();

        private const byte VOC_8BIT = 0x0;
        private const byte VOC_16BIT = 0x4;

        //
        // IsValidVocFile
        //
        public static bool IsValidVocFile(ref EndianBinaryReader _reader)
        {
            string iden = new string(_reader.ReadChars(19));

            if (iden != "Creative Voice File" || _reader.ReadByte() != 0x1A)
            {
                _reader.BaseStream.Position = 0;
                return false;
            }

            return true;
        }

        //
        // LoadSound
        //
        public override void LoadSound(ref EndianBinaryReader _reader)
        {
            bool _eof = false;

            _header.ParseHeader(ref _reader);

            if (_header.version != 0x010A && _header.version != 0x0114)
            {
                //Supports version 110 and 120
                throw new Exception("bSoundEffectVoc::LoadSound: Non support version");
            }

            // In Duke3d it seems to be ok to ignore the checksum?. 
            if (_header.version + 0x1234 != _header.IDCode)
            {
                //   throw new Exception("bSoundEffectVoc::LoadSound: Non support version(idcode invalid)");
            }

            while (_reader.BaseStream.Position < _reader.BaseStream.Length && !_eof)
            {
                int len;
                int blocktype = _reader.ReadByte();

                long len2 = _reader.BaseStream.Length - _reader.BaseStream.Position;

                if (blocktype == 0 || (len2 < 10))
                    break;

                len = (int)ReadBlockLen(ref _reader);

                switch (blocktype)
                {
                    case 1:
                        ParseBlockTypeOne(ref _reader, len);
                        break;
                    case 9:
                        ParseBlockTypeNine(ref _reader, len);
                        break;

                    default:
                        {
                            Engine.Printf("bSoundEffectVoc::LoadSound: Invalid or unsupported block type");
                            _reader.ReadBytes(len);
                            continue;
                        }
                }
            }

            MemoryStream stream = new MemoryStream();
            for (int i = 0; i < blocks.Count; i++)
            {
                stream.Write(blocks[i].sample, 0, blocks[i].sample.Length);
            }

            _soundEffect = new SoundEffect(stream.ToArray(), (int)blocks[0].samplerate, AudioChannels.Mono);
            //_instance = _soundEffect.CreateInstance();

            stream.Dispose();
            blocks.Clear();
        }

        private ulong ReadBlockLen(ref EndianBinaryReader _reader)
        {
            ulong len = 0;
            int pos = (int)_reader.BaseStream.Position;

            len = (_reader.ReadUInt64() & 0x00ffffff);

            _reader.BaseStream.Position = pos + 3;
            return len;
        }

        private void ConvertBlock(AudioChannels channel, int bits, byte[] insample, ref int samplerate, out byte[] samplebuffer)
        {
            int length = insample.Length;
            int samplescale = 0;
            bool resample = false;

            if (bits != 16)
            {
                length = length * 2;
                resample = true;
            }

            if (samplerate < 8000)
            {
                samplescale = (int)Math.Ceiling(8000.0f / (float)insample.Length);
            }

            if (resample || samplescale != 0)
            {
                byte[] buffer = new byte[length];
                BinaryWriter writer = new BinaryWriter(new MemoryStream(buffer));

                for (int i = 0; i < insample.Length; i++)
                {
                    if (bits != 16)
                    {
                        writer.Write((short)((insample[i] - 128) << 8));
                    }
                    else
                    {
                        writer.Write(insample[i]);
                    }
                }

                samplebuffer = buffer;
                samplerate = 8000;

                writer.Dispose();
                return;
            }

            samplebuffer = insample;
        }

        public override bool isPlaying()
        {
            if (_instance == null)
                return false;

            return (_instance.State == SoundState.Playing);
        }

        private void ParseBlockTypeNine(ref EndianBinaryReader _reader, int len)
        {
            VocBlockNineHeader _blockheader = new VocBlockNineHeader();
            VocBlockData _block;

            _blockheader.Parse(ref _reader);

            _block.samplerate = _blockheader.samplerate;
            _block.pcmtype = 0;
            _block.bits = _blockheader.bitspersample;

            if (_blockheader.numchannels > 1 && _blockheader.codec == VOC_16BIT)
            {
                _block.channel = AudioChannels.Stereo;
            }
            else if (_blockheader.numchannels == 1 && _blockheader.codec == VOC_8BIT)
            {
                _block.channel = AudioChannels.Mono;
            }
            else
            {
                throw new Exception("BlockNine invalid voc block");
            }

            ConvertBlock(_block.channel, (int)_block.bits, _reader.ReadBytes(len), ref _block.samplerate, out _block.sample);

            blocks.Add(_block);
        }

        private void ParseBlockTypeOne(ref EndianBinaryReader _reader, int len)
        {
            VocBlockOneHeader _blockheader;
            VocBlockData _block;

            _blockheader.len = len;
            _blockheader.tc = (uint)_reader.ReadByte() << 8;
            _blockheader.pack = _reader.ReadByte();

            _block.samplerate = (int)((ulong)256000000L / (65536 - _blockheader.tc));
            _block.pcmtype = _blockheader.pack & 0xFF;
            _block.bits = 8;
            _block.channel = AudioChannels.Mono;

            ConvertBlock(_block.channel, (int)_block.bits, _reader.ReadBytes(_blockheader.len - 2), ref _block.samplerate, out _block.sample);

            blocks.Add(_block);
        }
    }

    //
    // VocBlockData
    //
    struct VocBlockData
    {
        public int pcmtype;
        public int samplerate;
        public byte[] sample;
        public int bits;
        public AudioChannels channel;
    }

    struct VocBlockOneHeader
    {
        public int len;

        public uint tc;
        public byte pack;
    }

    /*
     * bytes 0-3    sample rate
byte  4      bits per sample
byte  5      channels number
bytes 6-7    codec_id
bytes 8-11   reserved
bytes 12..n  the audio data

     */
    struct VocBlockNineHeader
    {
        public int samplerate;
        public byte bitspersample;
        public byte numchannels;
        public short codec;
        public byte reserved0;
        public byte reserved1;
        public byte reserved2;

        public void Parse(ref EndianBinaryReader _reader)
        {
            samplerate = _reader.ReadInt32();
            bitspersample = _reader.ReadByte();
            numchannels = _reader.ReadByte();
            codec = _reader.ReadInt16();
            reserved0 = _reader.ReadByte();
            reserved1 = _reader.ReadByte();
            reserved2 = _reader.ReadByte();
        }
    }

    struct VocHeader
    {
        public short headersize;
        public short version;
        public short IDCode;

        //
        // ParseHeader
        //
        public void ParseHeader(ref EndianBinaryReader _reader)
        {
            headersize = _reader.ReadInt16();
            version = _reader.ReadInt16();
            IDCode = _reader.ReadInt16();
        }
    };
}
