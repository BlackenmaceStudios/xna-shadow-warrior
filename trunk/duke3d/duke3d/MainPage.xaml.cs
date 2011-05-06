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

using duke3d.game;

namespace duke3d
{
    public partial class MainPage : UserControl
    {
        Game game = new Game();

        public MainPage()
        {
            InitializeComponent();
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
            
            this.KeyDown += keyHandler;
            this.KeyUp += keyUpHandler;

            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
        }

        public void keyHandler(object sender, KeyEventArgs args)
        {
            Key key = args.Key;

            game.SetKeyDown(key);
        }

        public void keyUpHandler(object sender, KeyEventArgs args)
        {
            Key key = args.Key;

            game.SetKeyUp(key);
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            game.Frame();
        }
    }
}
