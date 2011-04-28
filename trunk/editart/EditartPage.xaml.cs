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
using System.Windows.Navigation;

namespace editart
{
    public partial class EditartPage : UserControl
    {
        Editart editart = new Editart();

        UserControl _parent;
        public EditartPage(UserControl parent)
        {
            InitializeComponent();
            _parent = parent;
            _parent.KeyDown += new KeyEventHandler(MainPage_KeyDown);
            _parent.KeyUp += new KeyEventHandler(MainPage_KeyUp);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewportimg.Cursor = Cursors.None;

            editart.Init(ref viewportimg);

            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
        }

        

        void MainPage_KeyUp(object sender, KeyEventArgs e)
        {
            editart.editinputkeyup(e.Key);
            e.Handled = true;
        }

        void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            editart.editinputkey(e.Key);
            e.Handled = true;
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            editart.Frame();
        }
    }
}
