#if !WINDOWS_PHONE
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
#else
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
using Microsoft.Phone.Controls;
#endif

namespace buildlite
{
#if WINDOWS_PHONE
    public partial class MainPage : PhoneApplicationPage
#else
    public partial class MainPage : UserControl
#endif
    {
        BuildEditor _editor;

        public MainPage()
        {
            InitializeComponent();
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _editor = new BuildEditor();

            


            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
            //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;

            _editor.Init( ref viewportimg );

            this.MouseMove += new MouseEventHandler(MainPage_MouseMove);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(MainPage_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MainPage_MouseLeftButtonUp);
            this.KeyDown += new KeyEventHandler(MainPage_KeyDown);
            this.KeyUp += new KeyEventHandler(MainPage_KeyUp);
            viewportimg.Cursor = Cursors.None;

            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
            this.Focus();
            this.CaptureMouse();
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
