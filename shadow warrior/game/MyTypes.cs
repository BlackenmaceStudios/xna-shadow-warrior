using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sw
{
    static class MyTypes
    {
        public static int BIT(int shift)
        {
            return (1 << (shift));
        }

        public static int TEST(int flags, int mask) { return ((flags) & (mask)); }
        public static short SET(int flags, int mask) { return (short)((flags) |= (mask)); }
        public static short RESET(int flags, int mask) { return (short)((flags) &= ~(mask)); }
        public static int FLIP(int flags, int mask) { return ((flags) ^= (mask)); }
    }
}
