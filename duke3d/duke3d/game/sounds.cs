//-------------------------------------------------------------------------
/*
Copyright (c) 2010 - JV Software
Copyright (C) 1996, 2003 - 3D Realms Entertainment

This file is part of the XNA Duke Nukem 3D Atomic Edition Port

Duke Nukem 3D is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  

See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License
aint with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

Original Source: 1996 - Todd Replogle
Prepared for public release: 03/21/2003 - Charlie Wiederhold, 3D Realms
Ported to Silverlight/XNA C# 05/03/2011 - Justin Marshall, JV Software
*/
//-------------------------------------------------------------------------

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


using mact;

using build;
namespace duke3d.game
{
    public static class SoundSystem
    {
        private const int LOUDESTVOLUME = 150;
        private static bSoundManager soundManager;
        private static bSoundEffect song;
        private static MediaElement _musicDevice;
        static int backflag,numenvsnds;

        /*
        ===================
        =
        = SoundStartup
        =
        ===================
        */

        public static void SoundStartup()
        {
            soundManager = new bSoundManager();
        }

        /*
        ===================
        =
        = SoundShutdown
        =
        ===================
        */

        public static void SoundShutdown()
        {
           
        }

        /*
        ===================
        =
        = MusicStartup
        =
        ===================
        */

        public static void MusicStartup(MediaElement musicElement)
        {
            _musicDevice = musicElement;
        }

        /*
        ===================
        =
        = MusicShutdown
        =
        ===================
        */

        public static void MusicShutdown()
        {

        }

        public static USRHOOKS_Errors USRHOOKS_GetMem(out byte[] ptr, uint size )
        {
           ptr = new byte[size];

           return USRHOOKS_Errors.USRHOOKS_Ok;
        }

        public static USRHOOKS_Errors USRHOOKS_FreeMem(byte[] ptr)
        {
           ptr = null;
           return USRHOOKS_Errors.USRHOOKS_Ok;
        }

        private static byte menunum=0;

        private static short[] menusnds = new short[]
        {
            SoundId.LASERTRIP_EXPLODE,
            SoundId.DUKE_GRUNT,
            SoundId.DUKE_LAND_HURT,
            SoundId.CHAINGUN_FIRE,
            SoundId.SQUISHED,
            SoundId.KICK_HIT,
            SoundId.PISTOL_RICOCHET,
            SoundId.PISTOL_BODYHIT,
            SoundId.PISTOL_FIRE,
            SoundId.SHOTGUN_FIRE,
            SoundId.BOS1_WALK,
            SoundId.RPG_EXPLODE,
            SoundId.PIPEBOMB_BOUNCE,
            SoundId.PIPEBOMB_EXPLODE,
            SoundId.NITEVISION_ONOFF,
            SoundId.RPG_SHOOT,
            SoundId.SELECT_WEAPON
        };

        public static void intomenusounds()
        {
            short i;
            
            sound(menusnds[menunum++]);
            menunum %= 17;
        }

        private static void ResetMusicDevice()
        {
            _musicDevice.Stop();
            _musicDevice.Position = TimeSpan.FromSeconds(0);
            _musicDevice.Play();
        }

        public static void playmusic(string filename)
        {
            //song = soundManager.LoadMusic(name);
            //song.PlaySound();
#if !WINDOWS_PHONE
            filename = System.IO.Path.GetFileNameWithoutExtension(filename);
            _musicDevice.AutoPlay = true;
            _musicDevice.MediaEnded += new RoutedEventHandler(_musicDevice_MediaEnded);
            _musicDevice.SetSource(Engine.filesystem.ReadContentFileStream("music/" + filename + ".mp3"));

            ResetMusicDevice();
#else
            filename = System.IO.Path.GetFileNameWithoutExtension(filename);
            _musicDevice.Stop();
            _musicDevice.Source = new Uri("base/music/" + filename + ".mp3", UriKind.Relative);
            _musicDevice.Position = System.TimeSpan.FromSeconds(0); 
            _musicDevice.AutoPlay = true;
            _musicDevice.MediaEnded += new RoutedEventHandler(_musicDevice_MediaEnded);
            _musicDevice.Play();
#endif
        }

        public static void stopmusic()
        {
            _musicDevice.Stop();
        }

        static void _musicDevice_MediaEnded(object sender, RoutedEventArgs e)
        {
            ResetMusicDevice();
        }

        public static bool loadsound(short num)
        {
            int   fp, l;

            if (num >= Globals.NUM_SOUNDS) return false;

            Globals.Sound[num].sndeffect = soundManager.LoadSound(Globals.script.sounds[num].filename);

            /*
            fp = kopen4load(sounds[num],loadfromgrouponly);
            if(fp == -1)
            {
                sprintf(&fta_quotes[113][0],"Sound %s(#%ld) not found.",sounds[num],num);
                FTA(113,&ps[myconnectindex]);
                return 0;
            }

            l = kfilelength( fp );
            soundsiz[num] = l;

            Duke3d.Sound[num].lock = 200;

            allocache((int *)&Sound[num].ptr,l,&Sound[num].lock);
            kread( fp, Sound[num].ptr , l);
            kclose( fp );
            */
            return true;
        }

        public static int xyzsound(short num,short i,int x,int y,int z)
        {
#if false
            int sndist, cx, cy, cz, j,k;
            short pitche,pitchs,cs;
            int voice, sndang, ca, pitch;

        //    if(num != 358) return 0;

            if( num >= NUM_SOUNDS ||
                FXDevice == NumSoundCards ||
                ( (soundm[num]&8) && ud.lockout ) ||
                SoundToggle == 0 ||
                Sound[num].num > 3 ||
                FX_VoiceAvailable(soundpr[num]) == 0 ||
                (ps[myconnectindex].timebeforeexit > 0 && ps[myconnectindex].timebeforeexit <= 26*3) ||
                ps[myconnectindex].gm&MODE_MENU) return -1;

            if( soundm[num]&128 )
            {
                sound(num);
                return 0;
            }

            if( soundm[num]&4 )
            {
                if(VoiceToggle==0 || (ud.multimode > 1 && PN == APLAYER && sprite[i].yvel != screenpeek && ud.coop != 1) ) return -1;

                for(j=0;j<NUM_SOUNDS;j++)
                  for(k=0;k<Sound[j].num;k++)
                    if( (Sound[j].num > 0) && (soundm[j]&4) )
                      return -1;
            }

            cx = ps[screenpeek].oposx;
            cy = ps[screenpeek].oposy;
            cz = ps[screenpeek].oposz;
            cs = ps[screenpeek].cursectnum;
            ca = ps[screenpeek].ang+ps[screenpeek].look_ang;

            sndist = FindDistance3D((cx-x),(cy-y),(cz-z)>>4);

            if( i >= 0 && (soundm[num]&16) == 0 && PN == MUSICANDSFX && SLT < 999 && (sector[SECT].lotag&0xff) < 9 )
                sndist = divscale14(sndist,(SHT+1));

            pitchs = soundps[num];
            pitche = soundpe[num];
            cx = klabs(pitche-pitchs);

            if(cx)
            {
                if( pitchs < pitche )
                     pitch = pitchs + ( rand()%cx );
                else pitch = pitche + ( rand()%cx );
            }
            else pitch = pitchs;

            sndist += soundvo[num];
            if(sndist < 0) sndist = 0;
            if( sndist && PN != MUSICANDSFX && !cansee(cx,cy,cz-(24<<8),cs,SX,SY,SZ-(24<<8),SECT) )
                sndist += sndist>>5;

            switch(num)
            {
                case PIPEBOMB_EXPLODE:
                case LASERTRIP_EXPLODE:
                case RPG_EXPLODE:
                    if(sndist > (6144) )
                        sndist = 6144;
                    if(sector[ps[screenpeek].cursectnum].lotag == 2)
                        pitch -= 1024;
                    break;
                default:
                    if(sector[ps[screenpeek].cursectnum].lotag == 2 && (soundm[num]&4) == 0)
                        pitch = -768;
                    if( sndist > 31444 && PN != MUSICANDSFX)
                        return -1;
                    break;
            }


            if( Sound[num].num > 0 && PN != MUSICANDSFX )
            {
                if( SoundOwner[num][0].i == i ) stopsound(num);
                else if( Sound[num].num > 1 ) stopsound(num);
                else if( badguy(&sprite[i]) && sprite[i].extra <= 0 ) stopsound(num);
            }

            if( PN == APLAYER && sprite[i].yvel == screenpeek )
            {
                sndang = 0;
                sndist = 0;
            }
            else
            {
                sndang = 2048 + ca - getangle(cx-x,cy-y);
                sndang &= 2047;
            }

            if(Sound[num].ptr == 0) { if( loadsound(num) == 0 ) return 0; }
            else
            {
               if (Sound[num].lock < 200)
                  Sound[num].lock = 200;
               else Sound[num].lock++;
            }

            if( soundm[num]&16 ) sndist = 0;

            if(sndist < ((255-LOUDESTVOLUME)<<6) )
                sndist = ((255-LOUDESTVOLUME)<<6);

            if( soundm[num]&1 )
            {
                unsigned short start;

                if(Sound[num].num > 0) return -1;

                start = *(unsigned short *)(Sound[num].ptr + 0x14);

                if(*Sound[num].ptr == 'C')
                    voice = FX_PlayLoopedVOC( Sound[num].ptr, start, start + soundsiz[num],
                            pitch,sndist>>6,sndist>>6,0,soundpr[num],num);
                else
                    voice = FX_PlayLoopedWAV( Sound[num].ptr, start, start + soundsiz[num],
                            pitch,sndist>>6,sndist>>6,0,soundpr[num],num);
            }
            else
            {
                if( *Sound[num].ptr == 'C')
                    voice = FX_PlayVOC3D( Sound[ num ].ptr,pitch,sndang>>6,sndist>>6, soundpr[num], num );
                else voice = FX_PlayWAV3D( Sound[ num ].ptr,pitch,sndang>>6,sndist>>6, soundpr[num], num );
            }

            if ( voice > FX_Ok )
            {
                SoundOwner[num][Sound[num].num].i = i;
                SoundOwner[num][Sound[num].num].voice = voice;
                Sound[num].num++;
            }
            else Sound[num].lock--;

            return (voice);
#else
            Globals.Sound[num].sndeffect.PlaySound();
            return 1;
#endif
        }

        public static void sound(short num)
        {
#if false
            short pitch,pitche,pitchs,cx;
            int voice;
            int start;

            if (FXDevice == NumSoundCards) return;
            if(SoundToggle==0) return;
            if(VoiceToggle==0 && (soundm[num]&4) ) return;
            if( (soundm[num]&8) && ud.lockout ) return;
            if(FX_VoiceAvailable(soundpr[num]) == 0) return;

            pitchs = soundps[num];
            pitche = soundpe[num];
            cx = klabs(pitche-pitchs);

            if(cx)
            {
                if( pitchs < pitche )
                     pitch = pitchs + ( rand()%cx );
                else pitch = pitche + ( rand()%cx );
            }
            else pitch = pitchs;

            if(Sound[num].ptr == 0) { if( loadsound(num) == 0 ) return; }
            else
            {
               if (Sound[num].lock < 200)
                  Sound[num].lock = 200;
               else Sound[num].lock++;
            }

            if( soundm[num]&1 )
            {
                if(*Sound[num].ptr == 'C')
                {
                    start = (int)*(unsigned short *)(Sound[num].ptr + 0x14);
                    voice = FX_PlayLoopedVOC( Sound[num].ptr, start, start + soundsiz[num],
                            pitch,LOUDESTVOLUME,LOUDESTVOLUME,LOUDESTVOLUME,soundpr[num],num);
                }
                else
                {
                    start = (int)*(unsigned short *)(Sound[num].ptr + 0x14);
                    voice = FX_PlayLoopedWAV( Sound[num].ptr, start, start + soundsiz[num],
                            pitch,LOUDESTVOLUME,LOUDESTVOLUME,LOUDESTVOLUME,soundpr[num],num);
                }
            }
            else
            {
                if(*Sound[num].ptr == 'C')
                    voice = FX_PlayVOC3D( Sound[ num ].ptr, pitch,0,255-LOUDESTVOLUME,soundpr[num], num );
                else
                    voice = FX_PlayWAV3D( Sound[ num ].ptr, pitch,0,255-LOUDESTVOLUME,soundpr[num], num );
            }

            if(voice > FX_Ok) return;
            Sound[num].lock--;
#else
            if (Globals.Sound[num].sndeffect == null)
                loadsound(num);
            Globals.Sound[num].sndeffect.PlaySound();
#endif
        }

        public static bool SoundsActive
        {
            get
            {
                for (int i = 0; i < Globals.Sound.Length; i++)
                {
                    if (Globals.Sound[i].sndeffect != null)
                        if (Globals.Sound[i].sndeffect.isPlaying())
                            return true;
                }

                return false;
            }
        }

        public static int spritesound(ushort num, short i)
        {
            if (num >= Globals.NUM_SOUNDS) return -1;
            return xyzsound((short)num, i, Globals.SX(i), Globals.SY(i), Globals.SZ(i));
        }

        public static void stopsound(short num)
        {
            //if(Sound[num].num > 0)
            {
              //  FX_StopSound(SoundOwner[num][Sound[num].num-1].voice);
//                testcallback(num);
            }
        }

        public static void stopenvsound(short num,short i)
        {
           
        }

        public static void pan3dsound()
        {
           
        }

        public static void TestCallBack(int num)
        {
            
        }

        public static void clearsoundlocks()
        {
           
        }


    }
}
