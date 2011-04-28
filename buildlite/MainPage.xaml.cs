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
namespace buildlite
{
    public partial class MainPage : UserControl
    {
        EditorPage editorPage;
        EventHandler saveDataEvent;
        bSaveDialog saveDialog = new bSaveDialog();

        public MainPage()
        {
            InitializeComponent();

            saveDataEvent += SaveData;

            // This needs to get the hell out of here....
            saveDialog.OpenFileWriteDialog("Build Map Fil", ".map");
            EditorPage.saveDialogEvent = saveDataEvent;

            editorPage = new EditorPage(this);

            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
            //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;
        }

        public void SaveData(object sender, EventArgs e)
        {
            string myTextFile = "This is a a test";
            HtmlDocument doc = HtmlPage.Document;
            HtmlElement downloadData = doc.GetElementById("downloadData");
            downloadData.SetAttribute("value", myTextFile);

            HtmlElement fileName = doc.GetElementById("fileName");
            fileName.SetAttribute("value", "myFile.txt");
            doc.Submit("generateFileForm");
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
