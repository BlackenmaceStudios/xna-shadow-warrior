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
Ported to Silverlight/XNA C# 05/05/2011 - Justin Marshall, JV Software
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

using build;

namespace duke3d.game
{
    public enum MenuScreen
    {
        MENUSCREEN_MAINMENU = 0,
        MENUSCREEN_MAINMENUINGAME = 50,
        MENUSCREEN_SELECTEPISODE = 100,
        MENUSCREEN_SELECTSKILL = 110,
        MENUSCREEN_OPTIONS = 200,
        MENUSCREEN_LOADGAME = 1000,
        MENUSCREEN_ABORTGAME = 1500,
        MENUSCREEN_ADULTMODE = 10000,
        MENUSCREEN_ADULTMODEPASSWORD = 10001,
        MENUSCREEN_LOADLASTGAME = 15000,
        MENUSCREEN_ENDSHAREWARE = 20000,
        MENUSCREEN_QUICKRESTORE = 25000
    }
    public class Menu
    {
        short centre;
        int probey = 0, lastprobey = 0;
        sbyte sh = 0;
        bool RMB = false, LMB = false, onbar = false;
        MenuScreen current_menu;
        short last_zero,last_fifty,last_threehundred = 0;

        private short SHX(int X)
        {
            return 0;
        }
        private short PHX(int X)
        {
            return 0;
        }

        public void SetMenu(MenuScreen cm)
        {
            current_menu = cm;

            if( cm == MenuScreen.MENUSCREEN_LOADGAME )
                return;

            if( cm == 0 )
                probey = last_zero;
            else if(cm == MenuScreen.MENUSCREEN_MAINMENU)
                probey = last_fifty;
           // else if(cm >= 300 && cm < 400)
            //    probey = last_threehundred;
            else if(cm == MenuScreen.MENUSCREEN_SELECTSKILL)
                probey = 1;
            else probey = 0;
            lastprobey = -1;
        }

        public short probe(int x, int y, int i, int n)
        {
            short centr;

            if (x == (320 >> 1))
                centre = 320 >> 2;
            else 
                centre = 0;

           // if (!buttonstat)
            {
                if (GameKeys.KB_KeyPressed(GameKeys.sc_UpArrow) || GameKeys.KB_KeyPressed(GameKeys.sc_PgUp) || GameKeys.KB_KeyPressed(GameKeys.sc_kpad_8))
                {
                    //mi = 0;
                    GameKeys.KB_ClearKeyDown(GameKeys.sc_UpArrow);
                    GameKeys.KB_ClearKeyDown(GameKeys.sc_kpad_8);
                    GameKeys.KB_ClearKeyDown(GameKeys.sc_PgUp);
                    SoundSystem.sound(SoundId.KICK_HIT);

                    probey--;
                    if (probey < 0) probey = n - 1;
                   // minfo.dz = 0;
                }
                if (GameKeys.KB_KeyPressed(GameKeys.sc_DownArrow) || GameKeys.KB_KeyPressed(GameKeys.sc_PgDn) || GameKeys.KB_KeyPressed(GameKeys.sc_kpad_2))
                {
                    //mi = 0;
                    GameKeys.KB_ClearKeyDown(GameKeys.sc_DownArrow);
                    GameKeys.KB_ClearKeyDown(GameKeys.sc_kpad_2);
                    GameKeys.KB_ClearKeyDown(GameKeys.sc_PgDn);
                    SoundSystem.sound(SoundId.KICK_HIT);
                    probey++;
                   // minfo.dz = 0;
                }
            }

            if (probey >= n)
                probey = 0;

            if (centre != 0)
            {
                //        rotatesprite(((320>>1)+(centre)+54)<<16,(y+(probey*i)-4)<<16,65536,0,SPINNINGNUKEICON+6-((6+(totalclock>>3))%7),sh,0,10,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                //        rotatesprite(((320>>1)-(centre)-54)<<16,(y+(probey*i)-4)<<16,65536,0,SPINNINGNUKEICON+((totalclock>>3)%7),sh,0,10,0,0,Engine._device.xdim-1,Engine._device.ydim-1);

                Engine.rotatesprite(((320 >> 1) + (centre >> 1) + 70) << 16, (y + (probey * i) - 4) << 16, 65536, 0, (short)(Names.SPINNINGNUKEICON + 6 - ((6 + (Game.totalclock >> 3)) % 7)), sh, 0, 10, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
                Engine.rotatesprite(((320 >> 1) - (centre >> 1) - 70) << 16, (y + (probey * i) - 4) << 16, 65536, 0, (short)(Names.SPINNINGNUKEICON + ((Game.totalclock >> 3) % 7)), sh, 0, 10, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);
            }
            else
                Engine.rotatesprite((x - Engine.tilesizx[Names.BIGFNTCURSOR] - 4) << 16, (y + (probey * i) - 4) << 16, 65536, 0, (short)(Names.SPINNINGNUKEICON + (((Game.totalclock >> 3)) % 7)), sh, 0, 10, 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);

            if (GameKeys.KB_KeyPressed(GameKeys.sc_Space) || GameKeys.KB_KeyPressed(GameKeys.sc_kpad_Enter) || GameKeys.KB_KeyPressed(GameKeys.sc_Enter) || (LMB && !onbar))
            {
                if (current_menu != MenuScreen.MENUSCREEN_SELECTSKILL)
                    SoundSystem.sound(SoundId.PISTOL_BODYHIT);

                GameKeys.KB_ClearKeyDown(GameKeys.sc_Enter);
                GameKeys.KB_ClearKeyDown(GameKeys.sc_Space);
                GameKeys.KB_ClearKeyDown(GameKeys.sc_kpad_Enter);
                return (short)(probey);
            }
            else if (GameKeys.KB_KeyPressed(GameKeys.sc_Escape) || (RMB))
            {
                onbar = false;
                GameKeys.KB_ClearKeyDown(GameKeys.sc_Escape);
                SoundSystem.sound(SoundId.EXITMENUSOUND);
                return (-1);
            }
            else
            {
                if (onbar == false) return (short)(-probey - 2);
                if (GameKeys.KB_KeyPressed(GameKeys.sc_LeftArrow) || GameKeys.KB_KeyPressed(GameKeys.sc_kpad_4))
                    return (short)(probey);
                else if (GameKeys.KB_KeyPressed(GameKeys.sc_RightArrow))
                    return (short)(probey);
                else return (short)(-probey - 2);
            }
        }

        public int menutext(int x, int y, short s, short p, string t)
        {
            int i, ac, centre;

            y -= 12;

            i = centre = 0;

            if (x == (320 >> 1))
            {
                while (i < t.Length)
                {
                    if (t[i] == ' ')
                    {
                        centre += 5;
                        i++;
                        continue;
                    }
                    ac = 0;
                    if (t[i] >= '0' && t[i] <= '9')
                    {
                        ac = t[i] - '0' + Names.BIGALPHANUM - 10;
                    }
                    else if (t[i] >= 'a' && (t[i] <= 'z'))
                    {
                        ac = Char.ToUpper(t[i]) - 'A' + Names.BIGALPHANUM;
                    }
                    else if (t[i] >= 'A' && t[i] <= 'Z')
                    {
                        ac = t[i] - 'A' + Names.BIGALPHANUM;
                    }
                    else switch (t[i])
                        {
                            case '-':
                                ac = Names.BIGALPHANUM - 11;
                                break;
                            case '.':
                                ac = Names.BIGPERIOD;
                                break;
                            case '\'':
                                ac = Names.BIGAPPOS;
                                break;
                            case ',':
                                ac = Names.BIGCOMMA;
                                break;
                            case '!':
                                ac = Names.BIGX;
                                break;
                            case '?':
                                ac = Names.BIGQ;
                                break;
                            case ';':
                                ac = Names.BIGSEMI;
                                break;
                            case ':':
                                ac = Names.BIGSEMI;
                                break;
                            default:
                                centre += 5;
                                i++;
                                continue;
                        }

                    centre += Engine.tilesizx[ac] - 1;
                    i++;
                }
            }

            if (centre != 0)
                x = (320 - centre - 10) >> 1;

            int tpos = 0;
            while (tpos < t.Length)
            {
                if (t[tpos] == ' ') { x += 5; tpos++; continue; }
                ac = 0;
                if (t[tpos] >= '0' && t[tpos] <= '9')
                {
                    ac = t[tpos] - '0' + Names.BIGALPHANUM - 10;
                }
                else if (t[tpos] >= 'a' && t[tpos] <= 'z')
                {
                    ac = Char.ToUpper(t[tpos]) - 'A' + Names.BIGALPHANUM;
                }
                else if (t[tpos] >= 'A' && t[tpos] <= 'Z')
                {
                    ac = t[tpos] - 'A' + Names.BIGALPHANUM;
                }
                else switch (t[tpos])
                    {
                        case '-':
                            ac = Names.BIGALPHANUM - 11;
                            break;
                        case '.':
                            ac = Names.BIGPERIOD;
                            break;
                        case ',':
                            ac = Names.BIGCOMMA;
                            break;
                        case '!':
                            ac = Names.BIGX;
                            break;
                        case '\'':
                            ac = Names.BIGAPPOS;
                            break;
                        case '?':
                            ac = Names.BIGQ;
                            break;
                        case ';':
                            ac = Names.BIGSEMI;
                            break;
                        case ':':
                            ac = Names.BIGCOLIN;
                            break;
                        default:
                            x += 5;
                            tpos++;
                            continue;
                    }

                Engine.rotatesprite(x << 16, y << 16, 65536, 0, (short)ac, (sbyte)s, (byte)p, (byte)(10 + 16), 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);

                x += Engine.tilesizx[ac];
                tpos++;
            }
            return (x);
        }

        public int menutextc(int x, int y, short s, short p, string t)
        {
            int i, ac, centre;

            s += 8;
            y -= 12;

            i = centre = 0;

            //    if( x == (320>>1) )
            {
                while (i < t.Length)
                {
                    if (t[i] == ' ')
                    {
                        centre += 5;
                        i++;
                        continue;
                    }
                    ac = 0;
                    if (t[i] >= '0' && t[i] <= '9')
                    {
                        ac = t[i] - '0' + Names.BIGALPHANUM + 26 + 26;
                    }
                    else if (t[i] >= 'a' && t[i] <= 'z')
                    {
                        ac = t[i] - 'a' + Names.BIGALPHANUM + 26;
                    }
                    else if (t[i] >= 'A' && t[i] <= 'Z')
                        ac = t[i] - 'A' + Names.BIGALPHANUM;

                    else switch (t[i])
                        {
                            case '-':
                                ac = Names.BIGALPHANUM - 11;
                                break;
                            case '.':
                                ac = Names.BIGPERIOD;
                                break;
                            case ',':
                                ac = Names.BIGCOMMA;
                                break;
                            case '!':
                                ac = Names.BIGX;
                                break;
                            case '?':
                                ac = Names.BIGQ;
                                break;
                            case ';':
                                ac = Names.BIGSEMI;
                                break;
                            case ':':
                                ac = Names.BIGCOLIN;
                                break;
                        }

                    centre += Engine.tilesizx[ac] - 1;
                    i++;
                }
            }

            x -= centre >> 1;

            int tpos = 0;
            while (tpos < t.Length)
            {
                if (t[tpos] == ' ') { x += 5; tpos++; continue; }
                ac = 0;
                
                if (t[tpos] >= '0' && t[tpos] <= '9')
                {
                    ac = t[tpos] - '0' + Names.BIGALPHANUM + 26 + 26;
                }
                else if (t[tpos] >= 'a' && t[tpos] <= 'z')
                {
                    ac = t[tpos] - 'a' + Names.BIGALPHANUM + 26;
                }
                else if (t[tpos] >= 'A' && t[tpos] <= 'Z')
                {
                    ac = t[tpos] - 'A' + Names.BIGALPHANUM;
                }
                switch (t[tpos])
                {
                    case '-':
                        ac = Names.BIGALPHANUM - 11;
                        break;
                    case '.':
                        ac = Names.BIGPERIOD;
                        break;
                    case ',':
                        ac = Names.BIGCOMMA;
                        break;
                    case '!':
                        ac = Names.BIGX;
                        break;
                    case '?':
                        ac = Names.BIGQ;
                        break;
                    case ';':
                        ac = Names.BIGSEMI;
                        break;
                    case ':':
                        ac = Names.BIGCOLIN;
                        break;
                }

                Engine.rotatesprite(x << 16, y << 16, 65536, 0, (short)ac, (sbyte)s, (byte)p, (byte)(10 + 16), 0, 0, Engine._device.xdim - 1, Engine._device.ydim - 1);

                x += Engine.tilesizx[ac];
                tpos++;
            }
            return (x);
        }

        private void DrawSharewareNotice()
        {
            short x;
            x = probe(326,190,0,0);
            Game.gametext(160,50-8,"YOU ARE PLAYING THE SHAREWARE",0,2+8+16);
            Game.gametext(160,59-8,"VERSION OF DUKE NUKEM 3D.  WHILE",0,2+8+16);
            Game.gametext(160,68-8,"THIS VERSION IS REALLY COOL, YOU",0,2+8+16);
            Game.gametext(160,77-8,"ARE MISSING OVER 75% OF THE TOTAL",0,2+8+16);
            Game.gametext(160,86-8,"GAME, Aint WITH OTHER GREAT EXTRAS",0,2+8+16);
            Game.gametext(160,95-8,"AND GAMES, WHICH YOU'LL GET WHEN",0,2+8+16);
            Game.gametext(160,104-8,"YOU ORDER THE COMPLETE VERSION AND",0,2+8+16);
            Game.gametext(160,113-8,"GET THE FINAL TWO EPISODES.",0,2+8+16);

            Game.gametext(160,113+8,"PLEASE READ THE 'HOW TO ORDER' ITEM",0,2+8+16);
            Game.gametext(160,122+8,"ON THE MAIN MENU IF YOU WISH TO",0,2+8+16);
            Game.gametext(160,131+8,"UPGRADE TO THE FULL REGISTERED",0,2+8+16);
            Game.gametext(160,140+8,"VERSION OF DUKE NUKEM 3D.",0,2+8+16);
            Game.gametext(160,149+16,"PRESS ANY KEY...",0,2+8+16);

            if( x >= -1 ) SetMenu(MenuScreen.MENUSCREEN_SELECTEPISODE);
        }

        private void DrawScreenAdultMode()
        {
            short c, x;
            c = 60;
            Engine.rotatesprite(160<<16,19<<16,65536,0,Names.MENUBAR,16,0,10,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
            menutext(160,24,0,0,"ADULT MODE");

            x = probe(60,50+16,16,2);
            if(x == -1) 
            { 
                 SetMenu(MenuScreen.MENUSCREEN_OPTIONS); 
                 return;
            }

            menutext(c,50+16,SHX(-2),PHX(-2),"ADULT MODE");
            menutext(c,50+16+16,SHX(-3),PHX(-3),"ENTER PASSWORD");

            if(Globals.ud.lockout) menutext(c+160+40,50+16,0,0,"OFF");
            else menutext(c+160+40,50+16,0,0,"ON");
        }

        private void DrawScreenGameQuickRestore()
        {
            short x;
             Game.gametext(160,90,"SELECT A SAVE SPOT BEFORE",0,2+8+16);
             Game.gametext(160,90+9,"YOU QUICK RESTORE.",0,2+8+16);

             x = probe(186,124,0,0);
             if(x >= -1)
             {
// jv
                        //if(ud.multimode < 2 && ud.recstat != 2)
                        //{
                        //    ready2send = 1;
                       //     totalclock = ototalclock;
                       // }
                       // ps[myconnectindex].gm &= ~MODE_MENU;
// jv end
             }
        }

        private void DrawScreenAdultModeSetPassword()
        {
            /*
            short c, x;
            Game.gametext(160,50+16+16+16+16-12,"ENTER PASSWORD",0,2+8+16);
            x = Game.strget((320>>1),50+16+16+16+16,buf,19, 998);

            if( x != 0 )
            {
               if(ud.pwlockout[0] == 0 || ud.lockout == 0 )
                   strcpy(&ud.pwlockout[0],buf);
               else if( strcmp(buf,&ud.pwlockout[0]) == 0 )
               {
                   ud.lockout = 0;
                   buf[0] = 0;

                       for(x=0;x<numanimwalls;x++)
                            if( wall[animwall[x].wallnum].picnum != W_SCREENBREAK &&
                                 wall[animwall[x].wallnum].picnum != W_SCREENBREAK+1 &&
                                 wall[animwall[x].wallnum].picnum != W_SCREENBREAK+2 )
                                 if( wall[animwall[x].wallnum].extra >= 0 )
                                        wall[animwall[x].wallnum].picnum = wall[animwall[x].wallnum].extra;

                            }
                       current_menu = 10000;
                       KB_ClearKeyDown(sc_Enter);
                       KB_ClearKeyDown(sc_kpad_Enter);
                       KB_FlushKeyboardQueue();
                }
            }
            */
        }

        private void drawbackground()
        {
             short dapicnum;
             int x,y,x1,y1,x2,y2,topy;

             switch(Globals.ud.m_volume_number)
             {
                  default:dapicnum = Names.BIGHOLE;break;
                  case 1: dapicnum = Names.BIGHOLE; break;
                  case 2: dapicnum = Names.BIGHOLE; break;
             }

             y1 = 0; y2 = Engine._device.ydim;


             for(y=y1;y<y2;y+=64)
                  for(x=0;x<Engine._device.xdim;x+=64)
                        Engine.rotatesprite(x<<16,y<<16,65536,0,dapicnum,8,0,(byte)(8+16+64+128),0,y1,Engine._device.xdim-1,y2-1);

           
        }

        public void DrawMenus()
        {
            short c, x;
            int l;

            if (Game.State == GameState.GAMESTATE_DISCONNECTED)
            {
                drawbackground();
            }

            sh = (sbyte)(4-(Engine.table.sintable[(Game.totalclock<<4)&2047]>>11));
            x = 0;
            //if(!(current_menu >= MenuScreen.MENUSCREEN_LOADGAME && current_menu <= 2999 && current_menu >= 300 && current_menu <= 369))
              //  vscrn();

            switch(current_menu)
            {
                case MenuScreen.MENUSCREEN_QUICKRESTORE:
                    DrawScreenGameQuickRestore();
                    break;

                case MenuScreen.MENUSCREEN_ENDSHAREWARE:
                    DrawSharewareNotice();
                    break;

                case MenuScreen.MENUSCREEN_ADULTMODE:
                    DrawScreenAdultMode();
                    break;

                case MenuScreen.MENUSCREEN_ADULTMODEPASSWORD:
                    DrawScreenAdultMode();
                    DrawScreenAdultModeSetPassword();
                    break;

                case MenuScreen.MENUSCREEN_MAINMENU:
                    c = (320>>1);
                    Engine.rotatesprite(c<<16,28<<16,65536,0,Names.INGAMEDUKETHREEDEE,0,0,10,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                    Engine.rotatesprite((c+100)<<16,36<<16,65536,0,(short)(Names.PLUTOPAKSPRITE+2),(sbyte)(Engine.table.sintable[(Game.totalclock<<4)&2047]>>11),0,2+8,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
        // CTW - MODIFICATION
        //          x = probe(c,67,16,7);
                    x = probe(c,67,16,6);
        // CTW END - MODIFICATION
                    if(x >= 0)
                    {
                        if( Globals.ud.multimode > 1 && x == 0 && Globals.ud.recstat != 2)
                        {
                           // if( movesperpacket == 4 && myconnectindex != connecthead )
                           //     break;

                          //  last_zero = 0;
                          // cmenu( 600 );
                        }
                        else
                        {
                            last_zero = x;
                            switch(x)
                            {
                                case 0:
                                    SetMenu(MenuScreen.MENUSCREEN_SELECTEPISODE);
                                    break;
        // CTW - MODIFICATION
        // Shifted the entire menu input results up one.
        /*                      case 1:
                                    if(movesperpacket == 4 || numplayers > 1)
                                        break;

                                    tenBnSetExitRtn(dummyfunc);
                                    setDebugMsgRoutine(dummymess);
                                    tenerr = tenBnStart();

                                    switch(tenerr)
                                    {
                                        case eTenBnNotInWindows:
                                            cmenu(20001);
                                            break;
                                        case eTenBnBadGameIni:
                                            cmenu(20002);
                                            break;
                                        case eTenBnBadTenIni:
                                            cmenu(20003);
                                            break;
                                        case eTenBnBrowseCancel:
                                            cmenu(20004);
                                            break;
                                        case eTenBnBadTenInst:
                                            cmenu(20005);
                                            break;
                                        default:
                                            playonten = 1;
                                            gameexit(" ");
                                            break;
                                    }
                                    break;*/
                                case 1: 
                                    //SetMenu(MenuScreen.MENUSCREEN_OPTIONS);
                                    break;
                                case 2:
                                    //if(movesperpacket == 4 && connecthead != myconnectindex)
                                      //  break;
                                    //cmenu(300);
                                    break;
                                case 3: 
                                    //KB_FlushKeyboardQueue();
                                    //cmenu(400);
                                    break;
                                case 4: 
                                    //cmenu(990);
                                    break;
                                case 5: 
                                    //cmenu(500);
                                    break;
        // CTW END - MODIFICATION
                            }
                        }
                    }
#if false
                    if(KB_KeyPressed(sc_Q)) cmenu(500);

                    if(x == -1)
                    {
                        ps[myconnectindex].gm &= ~MODE_MENU;
                        if(ud.multimode < 2 && ud.recstat != 2)
                        {
                            ready2send = 1;
                            totalclock = ototalclock;
                        }
                    }

                    if(movesperpacket == 4)
                    {
                        if( myconnectindex == connecthead )
                            menutext(c,67,SHX(-2),PHX(-2),"NEW GAME");
                        else
                            menutext(c,67,SHX(-2),1,"NEW GAME");
                    }
                    else
                        menutext(c,67,SHX(-2),PHX(-2),"NEW GAME");
#else
                    menutext(c,67,SHX(-2),PHX(-2),"NEW GAME");
#endif
        // CTW - REMOVED
        /*          if(movesperpacket != 4 && numplayers < 2)
                        menutext(c,67+16,SHX(-3),PHX(-3),"PLAY ON TEN");
                    else
                        menutext(c,67+16,SHX(-3),1,"PLAY ON TEN");*/
        // CTW END - REMOVED

        // CTW - MODIFICATION - Not going to comment out the exact old code here.
        // I shifted up every menu option by 16.
        // I shifted up every menu result by 1.
                    menutext(c,67+16,SHX(-3),PHX(-3),"OPTIONS");

                   // if(movesperpacket == 4 && connecthead != myconnectindex)
                     //   menutext(c,67+16+16,SHX(-4),1,"LOAD GAME");
                    //else 
                    menutext(c,67+16+16,SHX(-4),PHX(-4),"LOAD GAME");

        #if        VOLUMEALL
                    menutext(c,67+16+16+16,SHX(-5),PHX(-5),"HOW TO ORDER");
        #else
                    menutext(c,67+16+16+16,SHX(-5),PHX(-5),"HELP");
        #endif
                    menutext(c,67+16+16+16+16,SHX(-6),PHX(-6),"CREDITS");

                    menutext(c,67+16+16+16+16+16,SHX(-7),PHX(-7),"QUIT");

                    break;
        // CTW END - MODIFICATION

                    case MenuScreen.MENUSCREEN_ABORTGAME:
                        if( GameKeys.KB_KeyPressed(GameKeys.sc_Space) || GameKeys.KB_KeyPressed(GameKeys.sc_Enter) || GameKeys.KB_KeyPressed(GameKeys.sc_kpad_Enter) || GameKeys.KB_KeyPressed(GameKeys.sc_Y) || LMB )
                        {
                            GameKeys.KB_FlushKeyboardQueue();
                            SetMenu( MenuScreen.MENUSCREEN_SELECTEPISODE );
                        }
                        if( GameKeys.KB_KeyPressed(GameKeys.sc_N) || GameKeys.KB_KeyPressed(GameKeys.sc_Escape) || RMB)
                        {
                            GameKeys.KB_ClearKeyDown(GameKeys.sc_N);
                            GameKeys.KB_ClearKeyDown(GameKeys.sc_Escape);
                          //  if(ud.multimode < 2 && ud.recstat != 2)
                            //{
                              //  ready2send = 1;
                                //totalclock = ototalclock;
                            //}
                            //ps[myconnectindex].gm &= ~MODE_MENU;
                            Game.SetGameState( GameState.GAMESTATE_INGAME );
                            SoundSystem.sound(SoundId.EXITMENUSOUND);
                            break;
                        }
                        probe(186,124,0,0);
                        Game.gametext(160,90,"ABORT this game?",0,2+8+16);
                        Game.gametext(160,90+9,"(Y/N)",0,2+8+16);

                        break;

                case MenuScreen.MENUSCREEN_MAINMENUINGAME:
                    c = (320>>1);
                    Engine.rotatesprite(c<<16,32<<16,65536,0,Names.INGAMEDUKETHREEDEE,0,0,10,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                    Engine.rotatesprite((c+100)<<16,36<<16,65536,0,(short)(Names.PLUTOPAKSPRITE+2),(sbyte)(Engine.table.sintable[(Game.totalclock<<4)&2047]>>11),0,2+8,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                    x = probe(c,67,16,7);
                    
                    switch(x)
                    {
                        case 0:
                            //if(movesperpacket == 4 && myconnectindex != connecthead)
                               // break;
                           // if(ud.multimode < 2 || ud.recstat == 2)
                             //   cmenu(1500);
                            //else
                            //{
                              //  cmenu(600);
//                                last_fifty = 0;
  //                          }
                            SetMenu( MenuScreen.MENUSCREEN_ABORTGAME );
                            break;
                        case 1:
                            /*
                            if(movesperpacket == 4 && connecthead != myconnectindex)
                                break;
                            if(ud.recstat != 2)
                            {
                                last_fifty = 1;
                                cmenu(350);
                                setview(0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                            }
                            */
                            break;
                        case 2:
                           // if(movesperpacket == 4 && connecthead != myconnectindex)
                           //     break;
                            last_fifty = 2;
                           // cmenu(300);
                            break;
                        case 3:
                            last_fifty = 3;
                            //SetMenu(MenuScreen.MENUSCREEN_OPTIONS);
                           // cmenu(200);
                            break;
                        case 4:
                            last_fifty = 4;
                            //KB_FlushKeyboardQueue();
                            //cmenu(400);
                            break;
                        case 5:
                           // if(numplayers < 2)
                           // {
                           //     last_fifty = 5;
                           //     cmenu(501);
                           // }
                            break;
                        case 6:
                            //last_fifty = 6;
                            //cmenu(500);
                            break;
                        case -1:
                            //ps[myconnectindex].gm &= ~MODE_MENU;
                            Game.SetGameState( GameState.GAMESTATE_INGAME );
                           // if(ud.multimode < 2 && ud.recstat != 2)
                            {
                            //    ready2send = 1;
                             //   totalclock = ototalclock;
                            }
                            break;
                    }

                  //  if( KB_KeyPressed(sc_Q) )
                    //    cmenu(500);

                    //if(movesperpacket == 4 && connecthead != myconnectindex)
                    //{
                    //    menutext(c,67                  ,SHX(-2),1,"NEW GAME");
                    //   menutext(c,67+16               ,SHX(-3),1,"SAVE GAME");
                     //   menutext(c,67+16+16            ,SHX(-4),1,"LOAD GAME");
                    //}
                    //else
                    {
                        menutext(c,67                  ,SHX(-2),PHX(-2),"NEW GAME");
                        menutext(c,67+16               ,SHX(-3),PHX(-3),"SAVE GAME");
                        menutext(c,67+16+16            ,SHX(-4),PHX(-4),"LOAD GAME");
                    }

                    menutext(c,67+16+16+16         ,SHX(-5),PHX(-5),"OPTIONS");
        #if VOLUMEALL
                    menutext(c,67+16+16+16+16      ,SHX(-6),PHX(-6),"HOW TO ORDER");
        #else
                    menutext(c,67+16+16+16+16      ,SHX(-6),PHX(-6)," HELP");
        #endif
                   // if(numplayers > 1)
                   //     menutext(c,67+16+16+16+16+16   ,SHX(-7),1,"QUIT TO TITLE");
                    menutext(c,67+16+16+16+16+16   ,SHX(-7),PHX(-7),"QUIT TO TITLE");
                    menutext(c,67+16+16+16+16+16+16,SHX(-8),PHX(-8),"QUIT GAME");
                    break;

                case MenuScreen.MENUSCREEN_SELECTEPISODE:
                    Engine.rotatesprite(160<<16,19<<16,65536,0,Names.MENUBAR,16,0,10,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                    menutext(160,24,0,0,"SELECT AN EPISODE");
        #if PLUTOPAK
           //         if(boardfilename[0])
             //           x = probe(160,60,20,5);
                    x = probe(160,60,20,4);
        #else
                    if(boardfilename[0])
                        x = probe(160,60,20,4);
                    else x = probe(160,60,20,3);
        #endif
                    if(x >= 0)
                    {
        #if VOLUMEONE
                        if(x > 0)
                            cmenu(20000);
                        else
                        {
                            ud.m_volume_number = x;
                            ud.m_level_number = 0;
                            cmenu(110);
                        }
        #endif

        #if !VOLUMEONE
        #if !PLUTOPAK

                        if(x == 3 && boardfilename[0])
                        {
                            ud.m_volume_number = 0;
                            ud.m_level_number = 7;
                        }
        #else
                    //    if(x == 4 && boardfilename[0])
                     //   {
                      //      ud.m_volume_number = 0;
                     //       ud.m_level_number = 7;
                     //   }
        #endif

                        //else
                        {
                            Globals.ud.m_volume_number = x;
                            Globals.ud.m_level_number = 0;
                        }
                        SetMenu(MenuScreen.MENUSCREEN_SELECTSKILL);
        #endif
                    }
                    else if(x == -1)
                    {
                        if(Game.State == GameState.GAMESTATE_INGAMEMENU) SetMenu(MenuScreen.MENUSCREEN_MAINMENUINGAME);
                        else SetMenu(MenuScreen.MENUSCREEN_MAINMENU);
                    }

                    for( int d = 0; d < Globals.script.volumes.Count; d++ )
                    {
                        menutext(160,60 + (d * 20),SHX(-2),PHX(-2),Globals.script.volumes[d].volumename);
                    }

                    break;

                case MenuScreen.MENUSCREEN_SELECTSKILL:
                    c = (320>>1);
                    Engine.rotatesprite(c<<16,19<<16,65536,0,Names.MENUBAR,16,0,10,0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                    menutext(c,24,0,0,"SELECT SKILL");
                    x = probe(c,70,19,4);
                    if(x >= 0)
                    {
                        switch(x)
                        {
                            case 0: SoundSystem.sound(SoundId.JIBBED_ACTOR6);break;
                            case 1: SoundSystem.sound(SoundId.BONUS_SPEECH1);break;
                            case 2: SoundSystem.sound(SoundId.DUKE_GETWEAPON2);break;
                            case 3: SoundSystem.sound(SoundId.JIBBED_ACTOR5);break;
                        }

                        Globals.ud.m_player_skill = x+1;
                        if(x == 3) Globals.ud.m_respawn_monsters = 1;
                        else Globals.ud.m_respawn_monsters = 0;

                        Globals.ud.m_monsters_off = Globals.ud.monsters_off = 0;

                        Globals.ud.m_respawn_items = 0;
                        Globals.ud.m_respawn_inventory = 0;

                        Globals.ud.multimode = 1;

                        if(Globals.ud.m_volume_number == 3)
                        {
                            //flushperms();
                           // setview(0,0,Engine._device.xdim-1,Engine._device.ydim-1);
                           // clearview(0L);
                           // nextpage();
                        }

                        Game.SetGameState(GameState.GAMESTATE_LOADING);
                    }
                    else if(x == -1)
                    {
                        //cmenu(100);
                        SetMenu(MenuScreen.MENUSCREEN_SELECTEPISODE);
                        GameKeys.KB_FlushKeyboardQueue();
                    }

                    menutext(c,70,SHX(-2),PHX(-2),Globals.script.skills[0].skillname);
                    menutext(c,70+19,SHX(-3),PHX(-3),Globals.script.skills[1].skillname);
                    menutext(c,70+19+19,SHX(-4),PHX(-4),Globals.script.skills[2].skillname);
                    menutext(c,70+19+19+19,SHX(-5),PHX(-5),Globals.script.skills[3].skillname);
                    break;
            }
        }
    }
}
