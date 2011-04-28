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
using game;
using editart;

namespace buildlite
{
    public partial class MainPage : UserControl
    {
        EditorPage editorPage;
        GamePage gamePage;
        EditartPage editartPage;

        bSaveDialog projectSaveDialog = new bSaveDialog();
        bSaveDialog saveDialog = new bSaveDialog();

        public MainPage()
        {
            InitializeComponent();

            // This needs to get the hell out of here....
            saveDialog.OpenFileWriteDialog("Build Map File", ".map");
            projectSaveDialog.OpenFileWriteDialog("Build Project File", ".grp");

            EditorPage.saveDialogEvent = new EventHandler(SaveData);
            EditorPage.quitDialogEvent = new EventHandler(QuitBuildEvent);
            EditorPage.openDialogEvent = new EventHandler(OpenMapEvent);

            Engine.filesystem.allowOneGrpFileOnly = true;

            editorPage = new EditorPage(this);
            editartPage = new EditartPage(this);
            paneloptions.Visibility = System.Windows.Visibility.Collapsed;

            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
            //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;
        }

        public void NewProjectClick(object sender, EventArgs e)
        {
            projectSaveDialog.SaveFile(Application.GetResourceStream(new Uri("base/data.grp", UriKind.RelativeOrAbsolute)).Stream);
            paneloptions.Visibility = System.Windows.Visibility.Visible;
        }

        public void LoadProjectClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Build Project File" + "|*" + ".grp";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            bool? fileopen = dialog.ShowDialog();
            if (!fileopen.Value)
                return;

            Engine.filesystem.InitGrpFile(dialog.File.OpenRead());
            paneloptions.Visibility = System.Windows.Visibility.Visible;
        }

        public void SaveProjectClick(object sender, EventArgs e)
        {
            projectSaveDialog.SaveFile(Engine.filesystem.GetGrpFileStream(0));
        }

        public void LaunchGame(object sender, EventArgs e)
        {
            gamePage = new GamePage(this, "nukeland.map", null);
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(gamePage);
            gamePage.Focus();
        }

        public void LaunchUserMap(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Build Map File" + "|*" + ".map";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            bool? fileopen = dialog.ShowDialog();
            if (!fileopen.Value)
                return;

            gamePage = new GamePage(this, "", dialog.File.OpenRead());
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(gamePage);
            gamePage.Focus();
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
            saveDialog.SaveFile((System.IO.Stream)sender);
        }

        void LaunchEditArtClick(object sender, EventArgs e)
        {
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(editartPage);
            editartPage.Focus();
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
