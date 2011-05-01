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
namespace codeeditor
{
    public partial class CodeEditorPage : UserControl
    {
        SyntaxHighlightingTextBox codeEditorTextbox;
        string codeEditString;
        string[] codeEditStringLines;
        double codeEditpos = 0;
        int linepos = 0;

        public CodeEditorPage()
        {
            InitializeComponent();
        }


        private void CreateCodeEditor()
        {
            codeEditString = Engine.filesystem.ReadContentFileString("game.c");
            codeEditStringLines = codeEditString.Split('\r', '\n');
            codeEditorTextbox = new SyntaxHighlightingTextBox();
            codeEditorTextbox.Width = 640;
            codeEditorTextbox.Height = 480;
            codeEditorTextbox.SourceLanguage = SourceLanguageType.Cpp;
            codeEditorTextbox.UseLayoutRounding = true;


            string code = "";
            for (int i = 0; i < codeEditorTextbox.Width; i++)
            {
                if (codeEditStringLines[i].Length <= 0)
                {
                    code += '\n';
                }
                else
                {
                    code += codeEditStringLines[i];
                }
            }
            codeEditorTextbox.SourceCode = codeEditString;
            
            LayoutRoot.Children.Add(codeEditorTextbox);
        }

       

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the code editor
            CreateCodeEditor();

            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
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
        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            int scrollpos = (int)codeEditorTextbox.GetPosition();
            if (codeEditpos != scrollpos)
            {
                if (refreshed)
                {
                    codeEditpos = scrollpos;
                    refreshed = false;
                    return;
                }
                refreshed = true;
                double scrollamt = scrollpos - codeEditpos;
                if (codeEditpos < scrollpos)
                {
                    linepos += (int)((scrollamt / 16.0f));
                }
                else
                    linepos -= (int)((scrollamt / 16.0f));

                

                if (linepos < 0)
                    linepos = 0;

                codeEditpos = scrollpos;
                string code = "";
                for (int i = linepos; i < linepos + codeEditorTextbox.Width; i++)
                {
                    if (i > codeEditStringLines.Length)
                        break;
                    if (codeEditStringLines[i].Length <= 0)
                    {
                        code += '\n';
                    }
                    else
                    {
                        code += codeEditStringLines[i];
                    }
                }
                codeEditorTextbox.SourceCode = code;
                codeEditorTextbox.SetScrollbarPosition(scrollpos);
            }
        }
        public void MenuEvent(string eventname)
        {
            
        }
    }
}
