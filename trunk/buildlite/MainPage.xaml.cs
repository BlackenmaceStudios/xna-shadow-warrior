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
        EditorPage editorPage;

        public MainPage()
        {
            InitializeComponent();

             editorPage = new EditorPage(this);

            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
            //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;
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
