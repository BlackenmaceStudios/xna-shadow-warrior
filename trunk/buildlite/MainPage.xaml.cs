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

namespace buildlite
{
    public partial class MainPage : UserControl
    {
        BuildEditor _editor;

        public MainPage()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _editor = new BuildEditor();

            _editor.Init( ref viewportimg );

            this.MouseMove += new MouseEventHandler(MainPage_MouseMove);
            viewportimg.Cursor = Cursors.None;

            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
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
