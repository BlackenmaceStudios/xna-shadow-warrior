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
namespace Editor
{
    public partial class EditorPage : UserControl
    {
        BuildEditor _editor;
        UserControl _parent;

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

        public void OpenMap(System.IO.Stream stream)
        {
            _editor.LoadMapFromStream(stream);
        }

        void NewBoardClick(object sender, EventArgs e)
        {
            _editor.initnewboard();
            _editor.inbuildmenu = false;
        }

        void LoadBoardClick(object sender, EventArgs e)
        {
            EditorPage.openDialogEvent.Method.Invoke(EditorPage.openDialogEvent.Target, new object[] { null, null });
            _editor.inbuildmenu = false;
        }

        void LoadBoardXAPClick(object sender, EventArgs e)
        {
            _editor.inbuildmenu = false;
            _editor.inloadmenu = true;
        }

        void SaveBoardGrpClick(object sender, EventArgs e)
        {
            _editor.insavemenu = true;
            _editor.inbuildmenu = false;
        }

        void SaveBoardClick(object sender, EventArgs e)
        {
            System.IO.BinaryWriter stream = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            if (Engine.board != null)
            {
                short sectnum = -1;
                Engine.board.updatesector(_editor.startposx, _editor.startposy, ref sectnum);
                Engine.board.saveboard(stream, _editor.startposx, _editor.startposy, _editor.startposz, _editor.startang, sectnum);
                EditorPage.saveDialogEvent.Method.Invoke(EditorPage.saveDialogEvent.Target, new object[] { stream.BaseStream, null });
            }
            stream.Dispose();
            _editor.inbuildmenu = false;
        }

        void ExitBuildClick(object sender, EventArgs e)
        {
            EditorPage.quitDialogEvent.Method.Invoke(EditorPage.quitDialogEvent.Target, new object[] { null, null });
        }

        void MainPage_KeyUp(object sender, KeyEventArgs e)
        {
            _editor.editinputkeyup(false, false, e.Key);
            e.Handled = true;
        }

        void SaveGrpClick(object sender, EventArgs e)
        {
            EditorPage.saveGrpEvent.Method.Invoke(EditorPage.saveGrpEvent.Target, new object[] { null, null });
        }

        void MainPage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _editor.editinputkeyup(false, true, Key.None);
        }

        void MainPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _editor.editinputkey(false, true, Key.None);
        }

        void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            _editor.editinputkey(false, false, e.Key);
            e.Handled = true;
        }

        void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_editor.inbuildmenu || _editor.inloadmenu || _editor.insavemenu)
                return;

            Point p = e.GetPosition(viewportimg);
            _editor.editinputmouse(p.X, p.Y);
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (_editor.inbuildmenu)
            {
                if (buildmenu.Visibility == System.Windows.Visibility.Collapsed)
                {
                    buildmenu.Visibility = System.Windows.Visibility.Visible;
                    viewportimg.Cursor = Cursors.Arrow;
                }
            }
            else
            {
                if (buildmenu.Visibility == System.Windows.Visibility.Visible)
                {
                    buildmenu.Visibility = System.Windows.Visibility.Collapsed;
                    viewportimg.Cursor = Cursors.None;
                }
            }
            _editor.Frame();
        }
    }
}
