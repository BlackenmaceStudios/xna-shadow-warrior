using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace duke3d.game
{
   

    //
    // GameKeys
    //
    public static class GameKeys
    {
        public static Key KB_GetLastScanCode() { return KB_LastScan; }
        public static void KB_SetLastScanCode( Key scancode ) { KB_LastScan = ( scancode ); }
        public static void KB_ClearLastScanCode() { KB_SetLastScanCode( sc_None ); }
        public static bool KB_KeyPressed( Key scan ) 
        {
            if (scan == Key.Unknown)
                return false;

            return Game.keysdown[ (int)( scan ) ] != false; 
        }

        public static void KB_FlushKeyboardQueue()
        {
            for (int i = 0; i < Game.keysdown.Length; i++)
            {
                Game.keysdown[i] = false;
            }
        }

        public static void KB_ClearKeyDown(Key scan) { Game.keysdown[(int)(scan)] = false; }

        private static Key KB_LastScan = sc_None;

        public static readonly Key sc_None     =    Key.None;
        public static readonly Key sc_Bad      =    Key.None;
        public static readonly Key sc_Comma    =    Key.Unknown;
        public static readonly Key sc_Period   =    Key.Unknown;
        public static readonly Key sc_Return   =    Key.Enter;
        public static readonly Key sc_Enter    =    Key.Enter;
        public static readonly Key sc_Escape   =    Key.Escape;
        public static readonly Key sc_Space    =    Key.Space;
        public static readonly Key sc_BackSpace =   Key.Back;
        public static readonly Key sc_Tab       =   Key.Tab;
        public static readonly Key sc_LeftAlt   =   Key.Alt;
        public static readonly Key sc_LeftControl = Key.Ctrl;
        public static readonly Key sc_CapsLock    = Key.CapsLock;
        public static readonly Key sc_LeftShift   = Key.Shift;
        public static readonly Key sc_RightShift  = Key.Shift;
        public static readonly Key sc_F1          = Key.F1;
        public static readonly Key sc_F2          = Key.F2;
        public static readonly Key sc_F3          = Key.F3;
        public static readonly Key sc_F4          = Key.F4;
        public static readonly Key sc_F5          = Key.F5;
        public static readonly Key sc_F6          = Key.F6;
        public static readonly Key sc_F7          = Key.F7;
        public static readonly Key sc_F8          = Key.F8;
        public static readonly Key sc_F9          = Key.F9;
        public static readonly Key sc_F10         = Key.F10;
        public static readonly Key sc_F11         = Key.F11;
        public static readonly Key sc_F12         = Key.F12;
        public static readonly Key sc_Kpad_Star   = Key.Unknown;
        public static readonly Key sc_Pause       = Key.Unknown;
        public static readonly Key sc_ScrollLock  = Key.Unknown;
        public static readonly Key sc_NumLock     = Key.Unknown;
        public static readonly Key sc_Slash       = Key.Unknown;
        public static readonly Key sc_SemiColon   = Key.Unknown;
        public static readonly Key sc_Quote       = Key.Unknown;
        public static readonly Key sc_Tilde       = Key.Unknown;
        public static readonly Key sc_BackSlash   = Key.Unknown;

        public static readonly Key sc_OpenBracket  = Key.Unknown;
        public static readonly Key sc_CloseBracket = Key.Unknown;

        public static readonly Key sc_1            = Key.D1;
        public static readonly Key sc_2            = Key.D2;
        public static readonly Key sc_3            = Key.D3;
        public static readonly Key sc_4            = Key.D4;
        public static readonly Key sc_5            = Key.D5;
        public static readonly Key sc_6            = Key.D6;
        public static readonly Key sc_7            = Key.D7;
        public static readonly Key sc_8            = Key.D8;
        public static readonly Key sc_9            = Key.D9;
        public static readonly Key sc_0            = Key.D0;
        public static readonly Key sc_Minus        = Key.Unknown;
        public static readonly Key sc_Equals       = Key.Unknown;
        public static readonly Key sc_Plus         = Key.Unknown;

        public static readonly Key sc_kpad_1       = Key.NumPad1;
        public static readonly Key sc_kpad_2       = Key.NumPad2;
        public static readonly Key sc_kpad_3       = Key.NumPad3;
        public static readonly Key sc_kpad_4       = Key.NumPad4;
        public static readonly Key sc_kpad_5       = Key.NumPad5;
        public static readonly Key sc_kpad_6       = Key.NumPad6;
        public static readonly Key sc_kpad_7       = Key.NumPad7;
        public static readonly Key sc_kpad_8       = Key.NumPad8;
        public static readonly Key sc_kpad_9       = Key.NumPad9;
        public static readonly Key sc_kpad_0       = Key.NumPad0;
        public static readonly Key sc_kpad_Minus   = Key.Unknown;
        public static readonly Key sc_kpad_Plus    = Key.Unknown;
        public static readonly Key sc_kpad_Period  = Key.Unknown;

        public static readonly Key sc_A            = Key.A;
        public static readonly Key sc_B            = Key.B;
        public static readonly Key sc_C            = Key.C;
        public static readonly Key sc_D            = Key.D;
        public static readonly Key sc_E            = Key.E;
        public static readonly Key sc_F            = Key.F;
        public static readonly Key sc_G            = Key.G;
        public static readonly Key sc_H            = Key.H;
        public static readonly Key sc_I            = Key.I;
        public static readonly Key sc_J            = Key.J;
        public static readonly Key sc_K            = Key.K;
        public static readonly Key sc_L            = Key.L;
        public static readonly Key sc_M            = Key.M;
        public static readonly Key sc_N            = Key.N;
        public static readonly Key sc_O            = Key.O;
        public static readonly Key sc_P            = Key.P;
        public static readonly Key sc_Q            = Key.Q;
        public static readonly Key sc_R            = Key.R;
        public static readonly Key sc_S            = Key.S;
        public static readonly Key sc_T            = Key.T;
        public static readonly Key sc_U            = Key.U;
        public static readonly Key sc_V            = Key.V;
        public static readonly Key sc_W            = Key.W;
        public static readonly Key sc_X            = Key.X;
        public static readonly Key sc_Y            = Key.Y;
        public static readonly Key sc_Z            = Key.Z;

        // Extended scan codes

        public static readonly Key sc_UpArrow      = Key.Up;
        public static readonly Key sc_DownArrow    = Key.Down;
        public static readonly Key sc_LeftArrow    = Key.Left;
        public static readonly Key sc_RightArrow   = Key.Right;
        public static readonly Key sc_Insert       = Key.Insert;
        public static readonly Key sc_Delete       = Key.Delete;
        public static readonly Key sc_Home         = Key.Home;
        public static readonly Key sc_End          = Key.End;
        public static readonly Key sc_PgUp         = Key.PageUp;
        public static readonly Key sc_PgDn         = Key.PageDown;
        public static readonly Key sc_RightAlt     = Key.Alt;
        public static readonly Key sc_RightControl = Key.Ctrl;
        public static readonly Key sc_kpad_Slash   = Key.Unknown;
        public static readonly Key sc_kpad_Enter   = Key.Unknown;
        public static readonly Key sc_PrintScreen  = Key.Unknown;
        public static readonly Key sc_LastScanCode = Key.Unknown;
    }
}
