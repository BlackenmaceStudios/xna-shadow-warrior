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
using SilverlightMenu.Library;

using build;
using Editor;
using game;
using editart;
using grpeditor;
using codeeditor;

namespace buildlite
{
    public partial class MainPage : UserControl
    {
        EditorPage editorPage;
        GamePage gamePage;
        EditartPage editartPage;
        GrpEditor grpeditPage;
        CodeEditorPage codeeditPage;

        bSaveDialog projectSaveDialog = new bSaveDialog();
        bSaveDialog saveDialog = new bSaveDialog();

        bool projectloaded = false;

        void CreateMainMenuBar()
        {
            PageMenuItem rootMenu = new PageMenuItem("mnuRoot", "mnuRoot");
            PageMenuItem fileMenu = new PageMenuItem("mnuFile", "File");
            PageMenuItem EditorMenu = new PageMenuItem("mnuEditors", "Editors");
            PageMenuItem GameMenu = new PageMenuItem("mnuGame", "Game");

            EditorMenu.AddItem("mnuLaunchBuild", "Launch Build");
            EditorMenu.AddItem("mnuLaunchEditArt", "Launch Editart");
            EditorMenu.AddItem("mnuLaunchGrpEditor", "Launch Grp Editor");
            EditorMenu.AddItem("mnuLaunchCodeEditor", "Launch Code Editor");
            fileMenu.AddItem("mnuQuit", "Quit");

            GameMenu.AddItem("mnuLaunchGame", "Run Game");
            GameMenu.AddItem("mnuLaunchGameCustom", "Run Game Custom Map");

            // Clear out the old menu.
            Menu.menuDictionary.Clear();
            mnuTop.MenuItem.Clear();
            mnuTop.MenuItem.Add(fileMenu.root);
            mnuTop.MenuItem.Add(EditorMenu.root);
            mnuTop.MenuItem.Add(GameMenu.root);
            mnuTop.Repaint();
            mnuTop.Visibility = System.Windows.Visibility.Visible;
        }

        void CreateGameTestMenuBar()
        {
            PageMenuItem rootMenu = new PageMenuItem("mnuRoot", "mnuRoot");
            PageMenuItem fileMenu = new PageMenuItem("mnuFile", "File");

            fileMenu.AddItem("mnuQuit", "Exit");



            mnuTop.MenuItem.Add(fileMenu.root);

            // Clear out the old menu.
            Menu.menuDictionary.Clear();
            mnuTop.MenuItem.Clear();
            mnuTop.MenuItem.Add(fileMenu.root);
            mnuTop.Repaint();
            mnuTop.Visibility = System.Windows.Visibility.Visible;
        }

        void CreateBuildMenuBar()
        {
            // Clear out the old menu.
            Menu.menuDictionary.Clear();
            mnuTop.MenuItem.Clear();

            editorPage.InitMenuBar(ref mnuTop);
            
            mnuTop.Repaint();
            mnuTop.Visibility = System.Windows.Visibility.Visible;
        }

        void CreateGrpEditMenuBar()
        {
            // Clear out the old menu.
            Menu.menuDictionary.Clear();
            mnuTop.MenuItem.Clear();

            codeeditPage.InitMenuBar(ref mnuTop);

            mnuTop.Repaint();
            mnuTop.Visibility = System.Windows.Visibility.Visible;
        }

        void CreateCodeEditMenuBar()
        {
            // Clear out the old menu.
            Menu.menuDictionary.Clear();
            mnuTop.MenuItem.Clear();

            codeeditPage.InitMenuBar(ref mnuTop);

            mnuTop.Repaint();
            mnuTop.Visibility = System.Windows.Visibility.Visible;
        }

        void CreateEditArtMenuBar()
        {
            // Clear out the old menu.
            Menu.menuDictionary.Clear();
            mnuTop.MenuItem.Clear();

            editartPage.InitMenuBar(ref mnuTop);    
            
            mnuTop.Repaint();
            mnuTop.Visibility = System.Windows.Visibility.Visible;
        }

        public MainPage()
        {
            InitializeComponent();

            // This needs to get the hell out of here....
            saveDialog.OpenFileWriteDialog("Build Map File", ".map");
            projectSaveDialog.OpenFileWriteDialog("Build Project File", ".grp");

            EditorPage.saveDialogEvent = new EventHandler(SaveData);
            EditorPage.quitDialogEvent = new EventHandler(QuitBuildEvent);
            EditorPage.openDialogEvent = new EventHandler(OpenMapEvent);
            EditorPage.saveGrpEvent = new EventHandler(SaveProjectClick);
            EditartPage.saveGrpEvent = new EventHandler(SaveProjectClick);

            Engine.filesystem.allowOneGrpFileOnly = true;

            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
            //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;
        }

        private void Menu_MenuItemClicked(object sender, EventArgs e)
        {
            MenuItem clickedItem = (MenuItem)sender;

            if (projectloaded == false)
            {
                switch (clickedItem.Name)
                {
                    case "mnuNewProject":
                        if (projectSaveDialog.SaveFile(Application.GetResourceStream(new Uri("base/data.grp", UriKind.RelativeOrAbsolute)).Stream))
                        {
                            projectloaded = true;
                            CreateMainMenuBar();
                        }
                        break;

                    case "mnuLoadProject":
                        OpenFileDialog dialog = new OpenFileDialog();

                        dialog.Filter = "Build Project File" + "|*" + ".grp";
                        dialog.FilterIndex = 1;
                        dialog.Multiselect = false;

                        bool? fileopen = dialog.ShowDialog();
                        if (!fileopen.Value)
                            return;

                        Engine.filesystem.InitGrpFile(dialog.File.OpenRead());
                        projectloaded = true;
                        CreateMainMenuBar();
                        break;

                    default:
                        MessageBox.Show("You cannot use this option until you load in a project");
                        return;
                }
            }
            else
            {
                switch (clickedItem.Name)
                {
                    case "mnuLaunchGame":
                        CreateGameTestMenuBar();
                        gamePage = new GamePage(this, "nukeland.map", null);
                        viewportpanel.Children.Clear();
                        viewportpanel.Children.Add(gamePage);
                        gamePage.Focus();
                        break;

                    case "mnuLaunchGameCustom":
                         OpenFileDialog dialog = new OpenFileDialog();

                        dialog.Filter = "Build Map File" + "|*" + ".map";
                        dialog.FilterIndex = 1;
                        dialog.Multiselect = false;

                        bool? fileopen = dialog.ShowDialog();
                        if (!fileopen.Value)
                            return;

                        CreateGameTestMenuBar();
                        gamePage = new GamePage(this, "", dialog.File.OpenRead());
                        viewportpanel.Children.Clear();
                        viewportpanel.Children.Add(gamePage);
                        gamePage.Focus();
                        break;

                    case "mnuLaunchEditArt":
                        editartPage = new EditartPage(this);
                        CreateEditArtMenuBar();
                        viewportpanel.Children.Clear();
                        viewportpanel.Children.Add(editartPage);
                        editartPage.Focus();
                        
                        break;

                    case "mnuLaunchBuild":
                        editorPage = new EditorPage(this);
                        CreateBuildMenuBar();
                        viewportpanel.Children.Clear();
                        viewportpanel.Children.Add(editorPage);
                        editorPage.Focus();
                        break;
                    case "mnuLaunchGrpEditor":
                        grpeditPage = new GrpEditor();
                        CreateGrpEditMenuBar();
                        viewportpanel.Children.Clear();
                        viewportpanel.Children.Add(grpeditPage);
                        grpeditPage.Focus();
                        break;
                    case "mnuLaunchCodeEditor":
                        codeeditPage = new CodeEditorPage();
                        CreateGrpEditMenuBar();
                        viewportpanel.Children.Clear();
                        viewportpanel.Children.Add(codeeditPage);
                        codeeditPage.Focus();
                        break;
                    case "mnuSaveProject":
                        projectSaveDialog.SaveFile(Engine.filesystem.GetGrpFileStream(0));
                        break;

                    case "mnuQuit":
                     //   MessageBoxResult result;

                     //   if (gamePage == null)
                     //   {
                      //      result = MessageBox.Show("Are you sure you want to quit?\nHave you saved your grp yet?", "Are you sure you want to exit?", MessageBoxButton.OKCancel);
                     //   }
                      //  else
                     //   {
                     //       result = MessageBox.Show("Are you sure you want to quit?\n", "Are you sure you want to exit?", MessageBoxButton.OKCancel);
                     //   }
                      //  if (result == MessageBoxResult.OK)
                     //   {
                            //System.Windows.Browser.HtmlPage.Document.Submit();
                            HtmlPage.Window.Navigate(HtmlPage.Document.DocumentUri);
                     //   }
                        break;

                    default:
                        if (editartPage != null)
                        {
                            editartPage.MenuEvent(clickedItem.Name);
                        }
                        else if (editorPage != null)
                        {
                            editorPage.MenuEvent(clickedItem.Name);
                        }
                        else if (grpeditPage != null)
                        {
                            grpeditPage.MenuEvent(clickedItem.Name);
                        }
                        break;
                }
            }
        }


        public void SaveProjectClick(object sender, EventArgs e)
        {
            projectSaveDialog.SaveFile(Engine.filesystem.GetGrpFileStream(0));
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

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
