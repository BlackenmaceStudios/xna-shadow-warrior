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

        void MainPage_KeyUp(object sender, KeyEventArgs e)
        {
            _editor.editinputkeyup(false, false, e.Key);
            e.Handled = true;
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
            Point p = e.GetPosition(viewportimg);
            _editor.editinputmouse(p.X, p.Y);
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            _editor.Frame();
        }
    }
}
