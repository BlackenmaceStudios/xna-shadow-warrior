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
using Editor;
namespace buildlite
{
    public partial class MainPage : UserControl
    {
        EditorPage editorPage = new EditorPage();

        public MainPage()
        {
            InitializeComponent();
        }

        void LaunchEditor(object sender, EventArgs e)
        {
            LayoutRoot.Children.Add(editorPage);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
