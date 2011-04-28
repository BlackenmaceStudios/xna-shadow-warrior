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
using System.Windows.Browser;

using build;
using Editor;
namespace buildlite
{
    public partial class MainPage : UserControl
    {
        EditorPage editorPage;
        
        bSaveDialog saveDialog = new bSaveDialog();

        public MainPage()
        {
            InitializeComponent();

            // This needs to get the hell out of here....
            saveDialog.OpenFileWriteDialog("Build Map Fil", ".map");
            EditorPage.saveDialogEvent = new EventHandler(SaveData);
            EditorPage.quitDialogEvent = new EventHandler(QuitBuildEvent);
            EditorPage.openDialogEvent = new EventHandler(OpenMapEvent);

            editorPage = new EditorPage(this);

            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
            //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;
        }

        public void OpenMapEvent(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Build Map File" + "|*" + ".map";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            bool? fileopen = dialog.ShowDialog();
            if (!fileopen.Value)
                return;

            editorPage.OpenMap(dialog.File.OpenRead());
        }

        public void QuitBuildEvent(object sender, EventArgs e)
        {
            System.Windows.Browser.HtmlPage.Document.Submit();
        }

        public void SaveData(object sender, EventArgs e)
        {
            saveDialog.SaveFile((System.IO.BinaryWriter)sender);
        }

        void LaunchEditor(object sender, EventArgs e)
        {
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(editorPage);
            editorPage.Focus();


        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
