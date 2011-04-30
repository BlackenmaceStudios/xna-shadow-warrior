using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightMenu.Library;
using build;
namespace grpeditor
{
    public partial class GrpEditor : UserControl
    {
        List<string> filenametable = new List<string>();
        public GrpEditor()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Engine.filesystem.InitGrpFile("data.grp");

            string[] list = Engine.filesystem.FindFilesInGrp( 0 );

            foreach(string filename in list)
            {
                filelist.Items.Add(filename);
                filenametable.Add(filename);
            }
        }

        public void InitMenuBar(ref Menu root)
        {
            PageMenuItem rootMenu = new PageMenuItem("mnuRoot", "mnuRoot");
            PageMenuItem fileMenu = new PageMenuItem("mnuFile", "File");
            PageMenuItem grpOptionsMenu = new PageMenuItem("mnuGrpOptions", "Grp Archive");

            fileMenu.AddItem("mnuSaveProject", "Save Project");
            fileMenu.AddItem("mnuSeparator1", "-");
            fileMenu.AddItem("mnuQuit", "Exit");

            grpOptionsMenu.AddItem("mnuExportFile", "Export File");
            grpOptionsMenu.AddItem("mnuImportFile", "Import File");
            grpOptionsMenu.AddItem("mnuDeleteFile", "Delete File");

            root.MenuItem.Add(fileMenu.root);
            root.MenuItem.Add(grpOptionsMenu.root);
        }

         public void MenuEvent(string eventname)
         {
             switch (eventname)
             {
                 case "mnuDeleteFile":
                     if (filelist.SelectedIndex < 0)
                     {
                          return;
                     }

                     Engine.filesystem.DeleteFileFromGrp(filenametable[filelist.SelectedIndex], 0);
                     filelist.Items.RemoveAt(filelist.SelectedIndex);
                     break;
                 case "mnuImportFile":
                     {
                         OpenFileDialog dialog = new OpenFileDialog();
                         dialog.Filter = "All Files|*.*";
                         dialog.FilterIndex = 1;

                         if (dialog.ShowDialog().Value == false)
                             return;

                         System.IO.FileStream fileStream = dialog.File.OpenRead();
                         kFileWrite fil = Engine.filesystem.OpenFileWrite(dialog.File.Name, 0);

                         byte[] buffer = new byte[fileStream.Length];
                         fileStream.Read(buffer, 0, (int)fileStream.Length);
                         fil.io.Write(buffer);
                         fil.Close();
                         fileStream.Close();
                         buffer = null;

                         filelist.Items.Add(dialog.File.Name);
                         filenametable.Add(dialog.File.Name);
                     }
                     break;

                 case "mnuExportFile":
                     {
                         if (filelist.SelectedIndex < 0)
                         {
                             return;
                         }
                         SaveFileDialog dialog = new SaveFileDialog();
                         string filename = filenametable[filelist.SelectedIndex];

                         dialog.DefaultExt = System.IO.Path.GetExtension(filename);
                         dialog.Filter = dialog.DefaultExt + "|*." + dialog.DefaultExt;
                         dialog.FilterIndex = 1;

                         if (!dialog.ShowDialog().Value)
                         {
                             return;
                         }

                         kFile fil = Engine.filesystem.kopen4load(filename);

                         byte[] buffer = fil.kread(fil.Length);
                         fil.Close();
                         System.IO.Stream stream = dialog.OpenFile();

                         if (stream != null)
                         {
                             stream.Write(buffer, 0, (int)buffer.Length);
                             stream.Close();
                         }
                     }
                     break;
             }
         }
    }
}
