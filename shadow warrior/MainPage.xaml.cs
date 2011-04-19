using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Reflection;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;

using build;
using sw;

namespace shadow_warrior
{
    public partial class MainPage : PhoneApplicationPage
    {
        Game swgame;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            buildtag.Text = "Shadow Warrior Build " + GetBuildTime();
        }

        private string GetBuildTime()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            String version = assembly.FullName.Split(',')[1];
            string verstr = version.Split('=')[1];

            string[] assemblyver = verstr.Split('.');

            return assemblyver[0] + "." + "110" + assemblyver[2] + "-" + assemblyver[3];
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            

            MediaElement music;

            music = new MediaElement();
            music.Stop();
            gameviewpanelbackground.Children.Add(music);

            // Set focus on the page
            this.Focus();

            // Init the game
            swgame = new Game();
            swgame.Init(ref viewportimg, music);

            gameviewpanelbackground.Visibility = System.Windows.Visibility.Collapsed;

            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
        }

        void Game_GetInput()
        {
            int fvel = 0, svel = 0, angvel = 0;

            if (strafeleft_button.IsPressed) svel += 200;
            if (straferight_button.IsPressed) svel -= 200;
            if (moveup_button.IsPressed) fvel += 200;
            if (moveback_button.IsPressed) fvel -= 200;
            if (turnright_button.IsPressed) angvel += 35;
            if (turnleft_button.IsPressed) angvel -= 35;

            swgame.HandleLocalPlayerInput(fvel, svel, angvel);

            if (firebutton.IsPressed) 
                swgame.HandleLocalPlayerInputFire();

            if (jumpbutton.IsPressed)
                swgame.HandleLocalPlayerInputJump();
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            FrameworkDispatcher.Update();

            Game_GetInput();
            swgame.Frame();
        }

        private void viewportimg_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            
        }

   
    }
}