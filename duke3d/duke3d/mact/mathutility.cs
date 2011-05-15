/*
 * mathutil.c
 * Mathematical utility functions to emulate MACT
 *
 * by Jonathon Fowler
 *
 * Since we weren't given the source for MACT386.LIB so I've had to do some
 * creative interpolation here.
 *
 */

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

using build;

namespace duke3d.mact
{
    public static class MathUtility
    {
        public static Int32 FindDistance2D(Int32 x, Int32 y)
        {
            if ((x = pragmas.klabs(x)) < (y = pragmas.klabs(y))) 

            {
                Int32 t = y + (y >> 1);
                return (x - (x >> 5) - (x >> 7) + (t >> 2) + (t >> 6));
            }

            return 0;
        }

        public static Int32 FindDistance3D(Int32 x, Int32 y, Int32 z)
        {
            if ((x = pragmas.klabs(x)) < (y = pragmas.klabs(y)))
                if (x < (z = pragmas.klabs(z))) 

            {
                Int32 t = y + z;
                return (x - (x >> 4) + (t >> 2) + (t >> 3));
            }
            return 0;

        }

    }
}
