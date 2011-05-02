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
using SilverlightMenu.Library;
using Codexcite.SyntaxHighlighting;
using build;
using System.Reflection.Emit;
using System.CodeDom.Compiler;

namespace codeeditor
{
    public partial class CodeEditorPage : UserControl
    {
        SyntaxHighlightingTextBox codeEditorTextbox;
        ListBox codeProjectList;
        double codeEditpos = 0;
        int linepos = 0;

        public CodeEditorPage()
        {
            InitializeComponent();
        }


        private void CreateCodeEditor()
        {
            codeEditorTextbox = new SyntaxHighlightingTextBox();
            codeEditorTextbox.Width = 640;
            codeEditorTextbox.Height = 480;
            codeEditorTextbox.SourceLanguage = SourceLanguageType.CSharp;
            codeEditorTextbox.UseLayoutRounding = true;
            codeEditorTextbox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;


            codeProjectList = new ListBox();
            codeProjectList.Width = 160;
            codeProjectList.Height = 480;
            codeProjectList.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            codeProjectList.Items.Add("Project List:");

            Thickness margin = codeProjectList.Margin;
            margin.Left = 640;
            codeProjectList.Margin = margin;
            
            LayoutRoot.Children.Add(codeEditorTextbox);
            LayoutRoot.Children.Add(codeProjectList);
        }

       

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the code editor
            CreateCodeEditor();
        }

        public void InitMenuBar(ref Menu root)
        {
            PageMenuItem rootMenu = new PageMenuItem("mnuRoot", "mnuRoot");
            PageMenuItem fileMenu = new PageMenuItem("mnuFile", "File");
            PageMenuItem buildMenu = new PageMenuItem("mnuBuild", "Build");

            fileMenu.AddItem("mnuSaveProject", "Save Project");
            fileMenu.AddItem("mnuSeparator1", "-");
            fileMenu.AddItem("mnuQuit", "Exit");

            buildMenu.AddItem("mnuCompile", "Compile");

            root.MenuItem.Add(fileMenu.root);
            root.MenuItem.Add(buildMenu.root);
           
        }
        bool refreshed = false;
       
        public void MenuEvent(string eventname)
        {
            
        }
    }
}
