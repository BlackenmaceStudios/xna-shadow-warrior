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
using build;
using SilverlightMenu.Library;

namespace Editor
{
    public partial class EditorPage : UserControl
    {
        BuildEditor _editor;
        UserControl _parent;
        Point mousepoint = new Point(-1, -1);
        private bool clickinmenu = false;
        public static EventHandler openDialogEvent;
        public static EventHandler saveDialogEvent;
        public static EventHandler quitDialogEvent;
        public static EventHandler saveGrpEvent;

        public EditorPage(UserControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _editor = new BuildEditor();

            _editor.Init(ref viewportimg);

           
            _parent.MouseMove += new MouseEventHandler(MainPage_MouseMove);
            _parent.MouseLeftButtonDown += new MouseButtonEventHandler(MainPage_MouseLeftButtonDown);
            _parent.MouseLeftButtonUp += new MouseButtonEventHandler(MainPage_MouseLeftButtonUp);
            _parent.KeyDown += new KeyEventHandler(MainPage_KeyDown);
            _parent.KeyUp += new KeyEventHandler(MainPage_KeyUp);
            viewportimg.Cursor = Cursors.None;

            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
            _parent.Focus();
            _parent.CaptureMouse();
        }

        public void InitMenuBar(ref Menu root)
        {
            PageMenuItem rootMenu = new PageMenuItem("mnuRoot", "mnuRoot");
            PageMenuItem buildFileMenu = new PageMenuItem("mnuFile", "File");

            buildFileMenu.AddItem("mnuNewBoard", "New Board");
            buildFileMenu.AddItem("mnuSeparator1", "-");
            buildFileMenu.AddItem("mnuLoadBoard", "Load Board");
            buildFileMenu.AddItem("mnuLoadBoardFromHD", "Load Board From Harddrive");
            buildFileMenu.AddItem("mnuSeparator2", "-");
            buildFileMenu.AddItem("mnuSaveBoard", "Save Board");
            buildFileMenu.AddItem("mnuSaveBoardToHD", "Save Board To HardDrive");
            buildFileMenu.AddItem("mnuSaveProject", "Save Project");
            buildFileMenu.AddItem("mnuSeparator3", "-");
            buildFileMenu.AddItem("mnuQuit", "Exit");

            root.MenuItem.Add(buildFileMenu.root);
        }

        public void MenuEvent(string eventname)
        {
            switch (eventname)
            {
                case "mnuNewBoard":
                    {
                         if(MessageBox.Show("Are you sure you want to make a new board?", "New Board", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                            _editor.initnewboard();
                    }
                    break;
                case "mnuLoadBoard":
                    _editor.inloadmenu = true;
                    break;

                case "mnuLoadBoardFromHD":
                    EditorPage.openDialogEvent.Method.Invoke(EditorPage.openDialogEvent.Target, new object[] { null, null });
                    break;

                case "mnuSaveBoard":
                    _editor.insavemenu = true;
                    break;

                case "mnuSaveBoardToHD":
                    System.IO.BinaryWriter stream = new System.IO.BinaryWriter(new System.IO.MemoryStream());
                    if (Engine.board != null)
                    {
                        short sectnum = -1;
                        Engine.board.updatesector(_editor.startposx, _editor.startposy, ref sectnum);
                        Engine.board.saveboard(stream, _editor.startposx, _editor.startposy, _editor.startposz, _editor.startang, sectnum);
                        EditorPage.saveDialogEvent.Method.Invoke(EditorPage.saveDialogEvent.Target, new object[] { stream.BaseStream, null });
                    }
                    stream.Dispose();
                    break;
            }
        }

        public void OpenMap(System.IO.Stream stream)
        {
            _editor.LoadMapFromStream(stream);
        }

        void MainPage_KeyUp(object sender, KeyEventArgs e)
        {
            _editor.editinputkeyup(false, false, e.Key);
            e.Handled = true;
        }

        void MainPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (mousepoint.Y < 20)
                return;
            _editor.editinputkeyup(false, true, Key.None);
        }

        void MainPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mousepoint.Y < 20)
            {
                clickinmenu = true;
                return;
            }
            else if (clickinmenu)
            {
                clickinmenu = false;
                return;
            }
            _editor.editinputkey(false, true, Key.None);
        }

        void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            _editor.editinputkey(false, false, e.Key);
            e.Handled = true;
        }

        void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
         //   if (_editor.inbuildmenu || _editor.inloadmenu || _editor.insavemenu)
          //      return;

            mousepoint = e.GetPosition(viewportimg);
            _editor.editinputmouse(mousepoint.X, mousepoint.Y);
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
           
            _editor.Frame();
        }
    }
}
