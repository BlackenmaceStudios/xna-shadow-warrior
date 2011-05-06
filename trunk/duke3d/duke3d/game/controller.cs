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
    public static class Controller
    {
       public static bool BUTTON(Key key)
       {
           return GameKeys.KB_KeyPressed(key);
       }

       public static class Buttons
       {
           
           public static Key gamefunc_Move_Forward = Key.Up;
           public static Key gamefunc_Move_Backward = Key.Down;
           public static Key gamefunc_Turn_Left = Key.Left;
           public static Key gamefunc_Turn_Right = Key.Right;
           public static Key gamefunc_Strafe = Key.S;
           public static Key gamefunc_Fire = Key.Ctrl;
           public static Key gamefunc_Open = Key.Space;
           public static Key gamefunc_Run = Key.Shift;
           public static Key gamefunc_AutoRun = Key.Unknown;
           public static Key gamefunc_Jump = Key.Space;
           public static Key gamefunc_Crouch = Key.C;
           public static Key gamefunc_Look_Up = Key.Unknown;
           public static Key gamefunc_Look_Down = Key.Unknown;
           public static Key gamefunc_Look_Left = Key.Unknown;
           public static Key gamefunc_Look_Right = Key.Unknown;
           public static Key gamefunc_Strafe_Left = Key.Unknown;
           public static Key gamefunc_Strafe_Right = Key.Unknown;
           public static Key gamefunc_Aim_Up = Key.Unknown;
           public static Key gamefunc_Aim_Down = Key.Unknown;
           public static Key gamefunc_Weapon_1 = Key.D0;
           public static Key gamefunc_Weapon_2 = Key.D1;
           public static Key gamefunc_Weapon_3 = Key.D2;
           public static Key gamefunc_Weapon_4 = Key.D3;
           public static Key gamefunc_Weapon_5 = Key.D4;
           public static Key gamefunc_Weapon_6 = Key.D5;
           public static Key gamefunc_Weapon_7 = Key.D6;
           public static Key gamefunc_Weapon_8 = Key.D7;
           public static Key gamefunc_Weapon_9 = Key.D8;
           public static Key gamefunc_Weapon_10 = Key.D9;
           public static Key gamefunc_Inventory = Key.Tab;
           public static Key gamefunc_Inventory_Left = Key.Left;
           public static Key gamefunc_Inventory_Right = Key.Right;
           public static Key gamefunc_Holo_Duke = Key.Unknown;
           public static Key gamefunc_Jetpack = Key.Unknown;
           public static Key gamefunc_NightVision = Key.Unknown;
           public static Key gamefunc_MedKit = Key.Unknown;
           public static Key gamefunc_TurnAround = Key.Unknown;
           public static Key gamefunc_SendMessage = Key.Unknown;
           public static Key gamefunc_Map = Key.Unknown;
           public static Key gamefunc_Shrink_Screen = Key.Unknown;
           public static Key gamefunc_Enlarge_Screen = Key.Unknown;
           public static Key gamefunc_Center_View = Key.Unknown;
           public static Key gamefunc_Holster_Weapon = Key.Unknown;
           public static Key gamefunc_Show_Opponents_Weapon = Key.Unknown;
           public static Key gamefunc_Map_Follow_Mode = Key.Unknown;
           public static Key gamefunc_See_Coop_View = Key.Unknown;
           public static Key gamefunc_Mouse_Aiming = Key.Unknown;
           public static Key gamefunc_Toggle_Crosshair = Key.Unknown;
           public static Key gamefunc_Steroids = Key.Unknown;
           public static Key gamefunc_Quick_Kick = Key.Unknown;
           public static Key gamefunc_Next_Weapon = Key.Unknown;
           public static Key gamefunc_Previous_Weapon = Key.Unknown;
       };
    }
}
