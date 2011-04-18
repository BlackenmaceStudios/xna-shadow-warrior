using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sw
{
    public class Actor
    {
        internal int daposx, daposy, daposz;
        internal short daang, dacursectnum;

        public virtual void Spawn()
        {

        }

        //
        // UpdatePosition
        //
        public void UpdatePosition(int x, int y, int z, short ang, short cursectnum)
        {
            daposx = x;
            daposy = y;
            daposz = z;
            daang = ang;
            dacursectnum = cursectnum;
        }

        public virtual void Think()
        {

        }
    }
}
