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

using build;

namespace buildlite
{
    public class bSaveDialog
    {
        private SaveFileDialog dialog;

        public void OpenFileWriteDialog(string name, string ext)
        {
            dialog = new SaveFileDialog();

            dialog.DefaultExt = ext;
            dialog.Filter = name + "|*" + ext;
            dialog.FilterIndex = 1;
        }

        public void SaveFile(System.IO.BinaryWriter file)
        {
            bool? ret = dialog.ShowDialog();
            if (ret.Value == false)
                return;

            System.IO.Stream stream = dialog.OpenFile();

            if (stream != null)
            {
                byte[] buffer = new byte[file.BaseStream.Length];
                file.BaseStream.Position = 0;
                file.BaseStream.Read(buffer, 0, (int)file.BaseStream.Length);
                stream.Write(buffer, 0, (int)file.BaseStream.Length);
                stream.Close();
            }
        }
    }
}
