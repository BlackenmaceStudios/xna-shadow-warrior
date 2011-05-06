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
using duke3d.game;
namespace duke3d
{
    public partial class MainPage : PhoneApplicationPage
    {
        Game game = new Game();

        public MainPage()
        {
            InitializeComponent();
        }

        void Game_GetInput()
        {
            if (escapebutton.IsPressed)
                game.SetKeyDown(Key.Escape);
            else
                game.SetKeyUp(Key.Escape);

            if (strafeleft_button.IsPressed) 
                game.SetKeyDown(Key.Left); 
            else 
                game.SetKeyUp(Key.Left);

            if (straferight_button.IsPressed) 
                game.SetKeyDown(Key.Right); 
            else 
                game.SetKeyUp(Key.Right);

            if (moveup_button.IsPressed) 
                game.SetKeyDown(Key.Up); 
            else 
                game.SetKeyUp(Key.Up);

            if (moveback_button.IsPressed) 
                game.SetKeyDown(Key.Down); 
            else 
                game.SetKeyUp(Key.Down);


            if (firebutton.IsPressed)
                game.SetKeyDown(Key.Enter);
            else
                game.SetKeyUp(Key.Enter);

            if (jumpbutton.IsPressed)
                game.SetKeyDown(Key.Space);
            else
                game.SetKeyUp(Key.Space);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MediaElement musicElement = new MediaElement();
            musicElement.Stop();
            gameviewpanel.Children.Add(musicElement);

            viewportimg.Cursor = Cursors.None;

            SoundSystem.MusicStartup(musicElement);

            game.Init(ref viewportimg);

            this.Focus();

            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Game_GetInput();
            game.Frame();
        }
   
    }
}