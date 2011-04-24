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
using sw;
namespace swlite
{
    public partial class MainPage : UserControl
    {
        MediaElement music;
        Game swgame;
        int fvel = 0, svel = 0, angvel = 0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.KeyDown += keyHandler;
            this.KeyUp += keyUpHandler;

            System.Windows.Browser.HtmlPage.Plugin.Focus();
            this.Focus();

            App.Current.Host.Content.Resized += new EventHandler(Content_Resized);
        }

        void Content_Resized(object sender, EventArgs e) {
            LayoutRoot.Width = App.Current.Host.Content.ActualWidth;
            LayoutRoot.Height = App.Current.Host.Content.ActualHeight;
            //buildtag.Text = "Shadow Warrior Build " + GetBuildTime();
        }


        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            music = new MediaElement();
            LayoutRoot.Children.Add(music);

            System.Windows.Interop.SilverlightHost host = Application.Current.Host;
            // The Settings object, which represents Web browser settings.
            System.Windows.Interop.Settings settings = host.Settings;

            // Read/write properties of the Settings object.
            settings.EnableFrameRateCounter = true;
    //        settings.EnableRedrawRegions = true;
            settings.MaxFrameRate = 60;


            // Set focus on the page
            this.Focus();

            // Init the game
            swgame = new Game();
            swgame.Init(ref viewportimg, music);

            //gameviewpanelbackground.Visibility = System.Windows.Visibility.Collapsed;

            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
        }

        public void keyUpHandler(object sender, KeyEventArgs args)
        {
            Key key = args.Key;

            args.Handled = true;
            switch (key)
            {
                case Key.A:
                    //svel += 400;
                    angvel = 0;
                    break;
                case Key.W:
                    fvel = 0;
                    break;
                case Key.D:
                    //svel -= 400;
                    angvel = 0;
                    break;
                case Key.S:
                    fvel = 0;
                    break;

                case Key.Q:
                    svel = 0;
                    break;
                case Key.E:
                    svel = 0;
                    break;
                default:
                    return;
            }

            
        }
        public void keyHandler(object sender, KeyEventArgs args)
        {
            Key key = args.Key;
            
            args.Handled = true;
            switch (key)
            {
                case Key.A:
                    //svel += 400;
                    angvel = -15;
                    break;
                case Key.W:
                    fvel = 200;
                    break;
                case Key.D:
                    //svel -= 400;
                    angvel = 15;
                    break;
                case Key.S:
                    fvel = -200;
                    break;
                case Key.Q:
                    svel = 200;
                    break;
                case Key.E:
                    svel = -200;
                    break;
                case Key.Space:
                    swgame.HandleLocalPlayerInputJump();
                    break;
                case Key.Ctrl:
                    swgame.HandleLocalPlayerInputFire();
                    break;
                default:
                    return;
            }
        }

        /*
        void Game_GetInput()
        {
            int fvel = 0, svel = 0, angvel = 0;

            if (strafeleft_button.IsPressed) svel += 400;
            if (straferight_button.IsPressed) svel -= 400;
            if (moveup_button.IsPressed) fvel += 400;
            if (moveback_button.IsPressed) fvel -= 400;
            if (turnright_button.IsPressed) angvel += 35;
            if (turnleft_button.IsPressed) angvel -= 35;

            swgame.HandleLocalPlayerInput(fvel, svel, angvel);

            if (firebutton.IsPressed)
                swgame.HandleLocalPlayerInputFire();

            if (jumpbutton.IsPressed)
                swgame.HandleLocalPlayerInputJump();
        }
        */
        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
           // FrameworkDispatcher.Update();

            swgame.HandleLocalPlayerInput(fvel, svel, angvel);
            swgame.Frame();
        }

        private void viewportimg_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
