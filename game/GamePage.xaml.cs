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

namespace game
{
    public partial class GamePage : UserControl
    {
        Game game = new Game();
        string _userboardwap = "";
        System.IO.Stream _userboardstream;
        int fvel = 0, svel = 0, angvel = 0;
        UserControl _parent;
        public GamePage(UserControl parent,string userboardwap, System.IO.Stream userboardstream )
        {
            InitializeComponent();
            _parent = parent;
            _userboardwap = userboardwap;
            _userboardstream = userboardstream;
            _parent.KeyDown += keyHandler;
            _parent.KeyUp += keyUpHandler;
            
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
                    game.HandleLocalPlayerInputJump();
                    break;
                default:
                    return;
            }
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewportimg.Cursor = Cursors.None;

            game.Init(ref viewportimg);
            game.SpawnGame(_userboardwap, _userboardstream);


            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            game.HandleLocalPlayerInput(fvel, svel, angvel);
            game.Frame();
        }


    }
}
