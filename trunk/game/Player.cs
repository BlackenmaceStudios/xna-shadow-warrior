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

namespace game
{
    public class Player
    {
        int _posx = 0, _posy = 0, _posz = 0;
        short _sectornum = 0, _ang = 0;

        private int fvel = 0, svel = 0, angvel = 0;

        private bool playervoicefire = true;
        private int poszv = 0;
        private int jumping_counter = 0;
        private int cameradist = 0;
        private int cameraclock = 0;

        public void Spawn(int posx, int posy, int posz, short sectornum, short ang)
        {
            _posx = posx;
            _posy = posy;
            _posz = posz;
            _sectornum = sectornum;
            _ang = ang;
        }

        //
        // ProcessInput
        //
        public void ProcessInput(int fvel, int svel, int angvel)
        {
            this.fvel += fvel;
            this.svel += svel;
            this.angvel += angvel;

        }

        //
        // Jump
        //
        public void Jump()
        {
            if (jumping_counter > 0)
                return;

            jumping_counter = 1;
        }

        //
        // ProcessMovement
        //
        private void ProcessMovement(int xvect, int yvect)
        {
            int i = 40;
            int fz = 0, cz = 0, hz = 0, lz = 0;
            int k;

            Engine.board.getzrange(_posx, _posy, _posz, _sectornum, ref cz, ref hz, ref fz, ref lz, 163, Engine.CLIPMASK0);

            if (jumping_counter > 0)
            {
                if (jumping_counter < (1024 + 512))
                {
                    if (jumping_counter > 768)
                    {
                        jumping_counter = 0;
                        poszv = -2048;
                    }
                    else
                    {
                        poszv -= (Engine.table.sintable[(2048 - 128 + jumping_counter) & 2047]) / 12;
                        jumping_counter += 180;
                        // onground = false;
                    }
                }
                else
                {
                    jumping_counter = 0;
                    poszv = 0;
                }
            }
            else if (_posz < (fz - (i << 8)))
            {
                poszv += (176 + 80);

                if ((_posz + poszv) >= (fz - (i << 8))) // hit the ground
                {
                    poszv = 0;
                }
            }
            else
            {
                //Smooth on the ground

                k = ((fz - (i << 8)) - _posz) >> 1;
                if (pragmas.klabs(k) < 256) k = 0;
                _posz += k;
                poszv -= 768;
                if (poszv < 0) poszv = 0;
            }

            _posz += poszv;

            Engine.board.clipmove(ref _posx, ref _posy, ref _posz, ref _sectornum, xvect, yvect, 164, (4 << 8), (4 << 8), Engine.CLIPMASK0);
        }

        private void MovePlayer()
        {
            int xvect = 0, yvect = 0;

            _ang += (short)angvel;

            int doubvel = 3;

            xvect = 0; yvect = 0;
            if (fvel != 0)
            {
                xvect += ((((int)fvel) * doubvel * (int)Engine.table.sintable[(_ang + 512) & 2047]) >> 3);
                yvect += ((((int)fvel) * doubvel * (int)Engine.table.sintable[_ang & 2047]) >> 3);
            }
            if (svel != 0)
            {
                xvect += ((((int)svel) * doubvel * (int)Engine.table.sintable[_ang & 2047]) >> 3);
                yvect += ((((int)svel) * doubvel * (int)Engine.table.sintable[(_ang + 1536) & 2047]) >> 3);
            }

            ProcessMovement(xvect, yvect);

            svel = 0;
            fvel = 0;
            angvel = 0;
        }

        public void Think()
        {
            MovePlayer();
            Engine.board.drawrooms(_posx, _posy, _posz, _ang, 100, _sectornum);
            Engine.board.drawmasks();
            Engine.NextPage();
        }
    }
}
