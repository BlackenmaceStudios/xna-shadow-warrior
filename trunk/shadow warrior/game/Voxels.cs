using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using build;

namespace sw
{
    struct Voxel
    {
        public int tilenum;
        public int voxelnum;
        public string voxelpath;
    };

    //
    // VoxelManager
    //
    public static class VoxelManager
    {
        private static List<Voxel> _voxels = new List<Voxel>();

        //
        // ParseVoxelList
        //
        private static void ParseVoxelList(string buffer)
        {
            bParser parser = new bParser(buffer);

            while (true)
            {
                string token = parser.NextToken;

                if (token == null || token.Length <= 0)
                    break;

                if (token[0] == '#')
                {
                    parser.ParseRestOfLine();
                    continue;
                }

                Voxel _voxel = new Voxel();

                _voxel.tilenum = int.Parse( token );
                _voxel.voxelnum = parser.NextInt;
                _voxel.voxelpath = parser.NextToken;

                _voxels.Add(_voxel);
            }

            parser.Dispose();
        }

        //
        // LoadVoxels
        //
        public static void LoadVoxels()
        {
            string buffer;
            kFile fil;
                
            fil = Engine.filesystem.kopen4load("SWVOXFIL.TXT");
            if (fil == null)
            {
                throw new Exception("SWVOXFIL.TXT not found");
            }

            buffer = fil.ReadFile();

            fil.Close();

            ParseVoxelList(buffer);

            for (int i = 0; i < _voxels.Count; i++)
            {
                Engine.qloadkvx(_voxels[i].voxelnum, _voxels[i].voxelpath);
                Engine.tiletovox[_voxels[i].tilenum] = _voxels[i].voxelnum;
            }
        }
    }
}
