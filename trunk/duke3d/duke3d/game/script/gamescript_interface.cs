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
using duke3d.mact;
namespace duke3d.game.script
{
    static class GameScriptInterface
    {
        public static bool actor(object actor, params object[] parms ) { return false; }        // 1    [#]
        public static bool addammo(object actor, params object[] parms ) { 
            return false; 
        }   // 2    [#]
        public static bool ifrnd(object actor, params object[] parms ) { return false; }        // 3    [C]
        public static bool ifcansee(object actor, params object[] parms ) { return false; }         // 5    [C]
        public static bool ifhitweapon(object actor, params object[] parms ) { return false; }      // 6    [#]
        public static bool action(object actor, params object[] parms ) { return false; }           // 7    [#]
        public static bool ifpdistl(object actor, params object[] parms ) { 
            Actor _actor = actor as Actor;

            if (Globals.ldist3d(_actor._sprite, Globals.ps[0]._sprite) < (int)parms[0])
                return true;

            return false; 
        }

        //
        // moveactortosprite
        //
        public static bool moveactortosprite(object actor, params object[] parms)
        {
            spritetype childactor = Engine.board.sprite[((int[])parms[0])[0]];

            Globals.ps[0].SetPosition(childactor.x, childactor.y, childactor.z, -9999, childactor.sectnum);

            return false;
        }

        public static bool damagesprite(object actor, params object[] parms)
        {
            int spritenum = ((int[])parms[0])[0];
            int damage = (int)parms[1];

            Actor _actor = Engine.board.sprite[spritenum].obj as Actor;

            if(_actor._health > -999)
                _actor.Damage((Actor)actor, damage);

            return false;
        }

        public static bool iffindchildsprite(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;
            int[] var = parms[0] as int[];
            
            for (int i = 0; i < Engine.board.sprite.Length; i++)
            {
                spritetype spr = Engine.board.sprite[i];
                

                if (spr == null)
                    continue;

                Actor spractor = (Actor)spr.obj;
                if (spractor == null)
                    continue;

                if (spr.picnum != _actor._sprite.picnum && ((int)parms[1]) == 1)
                {
                    continue;
                }

                if (spr.lotag != _actor._sprite.lotag && ((int)parms[2]) == 1)
                {
                    continue;
                }

                if (spr.hitag != _actor._sprite.hitag && ((int)parms[3]) == 1)
                {
                    continue;
                }

                if (spr == _actor._sprite || spractor._health <= 0 )
                {
                    continue;
                }

                var[0] = i;
                return true;
            }

            return false;
        }

        public static bool ifpwselectkeydown(object actor, params object[] parms)
        {
            Player player = actor as Player;

            if (player.weaponselected == (int)parms[0])
            {
                return true;
            }

            return false;
        }

        public static bool ifpdistg(object actor, params object[] parms) {
            Actor _actor = actor as Actor;

            if (_actor.Distance(Globals.ps[0]) > (int)parms[0])
                return true;

            return false; 
        }         // 9    [#]
        
        public static bool strength(object actor, params object[] parms ) {
            Actor _actor = actor as Actor;
            _actor.SetHealth((int)parms[0]);
            return false; 
        }         // 11   [#]

        public static bool ifhealthl(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;

            if (_actor._health <= (int)parms[0])
                return true;

            return false;
        }



        public static bool setpicnum(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;
            int picnum = (int)parms[0];
            _actor._sprite.picnum = (short)picnum;

            return false;
        }

        public static bool setspritehittestdisabled(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;

            _actor._sprite.cstat = MyTypes.RESET(_actor._sprite.cstat, Flags.CSTAT_SPRITE_BLOCK);
            _actor._sprite.cstat = MyTypes.RESET(_actor._sprite.cstat, Flags.CSTAT_SPRITE_BLOCK_HITSCAN);
            _actor._sprite.cstat = MyTypes.RESET(_actor._sprite.cstat, Flags.CSTAT_SPRITE_BLOCK_MISSILE);

            return false;
        }

        public static bool setspritehittestenabled(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;

            _actor._sprite.cstat = MyTypes.SET(_actor._sprite.cstat, Flags.CSTAT_SPRITE_BLOCK);
            _actor._sprite.cstat = MyTypes.SET(_actor._sprite.cstat, Flags.CSTAT_SPRITE_BLOCK_HITSCAN);
            _actor._sprite.cstat = MyTypes.SET(_actor._sprite.cstat, Flags.CSTAT_SPRITE_BLOCK_MISSILE);

            return false;
        }

        public static bool declotag(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;

            _actor._sprite.lotag -= 1;

            return false;
        }

        public static bool shoot(object actor, params object[] parms ) {
            Actor _actor = actor as Actor;
            spritetype s = _actor._sprite;
            spritetype k = null;
            Player player = null;
            int atwith = (int)parms[0];
            short sect = 0, hitspr = 0, hitwall = 0, l, sa = 0, j, scount;
            int hitsect = 0;
            int sx = 0, sy = 0, sz = 0, vel, zvel = 0, hitx = 0, hity = 0, hitz = 0, x, oldzvel, dal;
            byte sizx,sizy;
            int damage = 10;

            if (parms.Length > 1)
                damage = (int)parms[1];

            if (_actor.ActorType == typeof(Player))
            {
                player = _actor as Player;
                _actor.GetActorPosition(out sx, out sy, out sz, out sa);
                Engine.board.updatesector(sx, sy, ref sect);
                //p = s->yvel;
                sz += player.pyoff + (4 << 8);
            }
            else
            {

            }
            if (atwith == Names.SHOTSPARK1 || atwith == Names.SHOTGUN || atwith == Names.CHAINGUN)
            {
                if (s.extra >= 0) s.shade = -96;

                if (player != null)
                {
                    j = _actor.aim(Globals.AUTO_AIM_ANGLE);
                    if (j >= 0)
                    {
                        dal = ((Engine.board.sprite[j].xrepeat * Engine.tilesizy[Engine.board.sprite[j].picnum]) << 1) + (5 << 8);
                        switch (Engine.board.sprite[j].picnum)
                        {
                            case Names.GREENSLIME:
                            case Names.GREENSLIME + 1:
                            case Names.GREENSLIME + 2:
                            case Names.GREENSLIME + 3:
                            case Names.GREENSLIME + 4:
                            case Names.GREENSLIME + 5:
                            case Names.GREENSLIME + 6:
                            case Names.GREENSLIME + 7:
                            case Names.ROTATEGUN:
                                dal -= (8 << 8);
                                break;
                        }
                        zvel = ((Engine.board.sprite[j].z - sz - dal) << 8) / Globals.ldist(player._sprite, Engine.board.sprite[j]);
                        sa = (short)Engine.getangle(Engine.board.sprite[j].x - sx, Engine.board.sprite[j].y - sy);
                    }

                    if (atwith == Names.SHOTSPARK1)
                    {
                        if (j == -1)
                        {
                            sa += (short)(16 - (Globals.TRAND() & 31));
                            zvel = (100 - 100) << 5;
                            zvel += (short)(128 - (Globals.TRAND() & 255));
                        }
                    }
                    else
                    {
                        sa += (short)(16 - (Globals.TRAND() & 31));
                        if (j == -1) zvel = (100 - 100) << 5;
                        zvel += (short)(128 - (Globals.TRAND() & 255));
                    }
                    sz -= (2 << 8);
                }
                    /*
                else
                {
                    j = findplayer(s, &x);
                    sz -= (4 << 8);
                    zvel = ((ps[j].posz - sz) << 8) / (ldist(&sprite[ps[j].i], s));
                    if (s->picnum != BOSS1)
                    {
                        zvel += 128 - (TRAND & 255);
                        sa += 32 - (TRAND & 63);
                    }
                    else
                    {
                        zvel += 128 - (TRAND & 255);
                        sa = getangle(ps[j].posx - sx, ps[j].posy - sy) + 64 - (TRAND & 127);
                    }
                }
                */

                _actor._sprite.cstat &= ~257;
                Engine.board.hitscan(sx, sy, sz, sect,
                    Engine.table.sintable[(sa + 512) & 2047],
                    Engine.table.sintable[sa & 2047],
                    zvel << 6, ref hitsect, ref hitwall, ref hitspr, ref hitx, ref hity, ref hitz, Engine.CLIPMASK1);
                _actor._sprite.cstat |= 257;

                if (hitsect < 0) return false;

                //if ((TRAND & 15) == 0 && sector[hitsect].lotag == 2)
                //    tracers(hitx, hity, hitz, sx, sy, sz, 8 - (ud.multimode >> 1));

                if ( true )
                {
                    k = Game.EGS((short)hitsect, hitx, hity, hitz, Names.SHOTSPARK1, -15, 10, 10, sa, 0, 0, 0, 4);
                   // sprite[k].extra = *actorscrptr[atwith];
                   // sprite[k].extra += (TRAND % 6);

                    if (hitwall == -1 && hitspr == -1)
                    {
                        if (zvel < 0)
                        {
                            if ((Engine.board.sector[hitsect].ceilingstat & 1) != 0)
                            {
                                k.xrepeat = 0;
                                k.yrepeat = 0;
                                return false;
                            }
                           // else
                            //    checkhitceiling(hitsect);
                        }
                        spawn(k, Names.SMALLSMOKE);
                    }

                    if (hitspr >= 0)
                    {
                        Actor _hitactor = Engine.board.sprite[hitspr].obj as Actor;
                        _hitactor.Damage(_actor, damage);
                        /*
                        checkhitsprite(hitspr, k);
                        if (sprite[hitspr].picnum == APLAYER && (ud.coop != 1 || ud.ffire == 1))
                        {
                            l = spawn(k, JIBS6);
                            sprite[k].xrepeat = sprite[k].yrepeat = 0;
                            sprite[l].z += (4 << 8);
                            sprite[l].xvel = 16;
                            sprite[l].xrepeat = sprite[l].yrepeat = 24;
                            sprite[l].ang += 64 - (TRAND & 127);
                        }
                        else spawn(k, SMALLSMOKE);

                        if (p >= 0 && (
                            sprite[hitspr].picnum == DIPSWITCH ||
                            sprite[hitspr].picnum == DIPSWITCH + 1 ||
                            sprite[hitspr].picnum == DIPSWITCH2 ||
                            sprite[hitspr].picnum == DIPSWITCH2 + 1 ||
                            sprite[hitspr].picnum == DIPSWITCH3 ||
                            sprite[hitspr].picnum == DIPSWITCH3 + 1 ||
                            sprite[hitspr].picnum == HANDSWITCH ||
                            sprite[hitspr].picnum == HANDSWITCH + 1))
                        {
                            checkhitswitch(p, hitspr, 1);
                            return;
                        }
                        */
                    }
#if false
                    else if (hitwall >= 0)
                    {
                        spawn(_actor, Names.SMALLSMOKE);

                        //if (isadoorwall(wall[hitwall].picnum) == 1)
                        //    goto SKIPBULLETHOLE;
                        /*
                        if (p >= 0 && (
                            wall[hitwall].picnum == DIPSWITCH ||
                            wall[hitwall].picnum == DIPSWITCH + 1 ||
                            wall[hitwall].picnum == DIPSWITCH2 ||
                            wall[hitwall].picnum == DIPSWITCH2 + 1 ||
                            wall[hitwall].picnum == DIPSWITCH3 ||
                            wall[hitwall].picnum == DIPSWITCH3 + 1 ||
                            wall[hitwall].picnum == HANDSWITCH ||
                            wall[hitwall].picnum == HANDSWITCH + 1))
                        {
                            checkhitswitch(p, hitwall, 0);
                            return;
                        }

                        if (wall[hitwall].hitag != 0 || (wall[hitwall].nextwall >= 0 && wall[wall[hitwall].nextwall].hitag != 0))
                            goto SKIPBULLETHOLE;
                        */
                        if (hitsect >= 0 && Engine.board.sector[hitsect].lotag == 0)
                            if (Engine.board.wall[hitwall].overpicnum != Names.BIGFORCE)
                                if ((Engine.board.wall[hitwall].nextsector >= 0 && Engine.board.sector[Engine.board.wall[hitwall].nextsector].lotag == 0) ||
                                    (Engine.board.wall[hitwall].nextsector == -1 && Engine.board.sector[hitsect].lotag == 0))
                                    if ((Engine.board.wall[hitwall].cstat & 16) == 0)
                                    {
                                        if (Engine.board.wall[hitwall].nextsector >= 0)
                                        {
                                            l = (short)Engine.board.headspritesect[Engine.board.wall[hitwall].nextsector];
                                            while (l >= 0)
                                            {
                                                if (Engine.board.sprite[l].statnum == 3 && Engine.board.sprite[l].lotag == 13)
                                                    goto SKIPBULLETHOLE;
                                                l = (short)Engine.board.nextspritesect[l];
                                            }
                                        }

                                        l = (short)Engine.board.headspritestat[5];
                                        /*
                                        while (l >= 0)
                                        {
                                            if (Engine.board.sprite[l].picnum == BULLETHOLE)
                                                if (dist(&sprite[l], &sprite[k]) < (12 + (TRAND & 7)))
                                                    goto SKIPBULLETHOLE;
                                            l = nextspritestat[l];
                                        }
                                        */
                                        spawn(_actor, Names.BULLETHOLE);
                                        Globals.lastspawnedsprite.xvel = -1;
                                        Globals.lastspawnedsprite.ang = (short)(Engine.getangle(Engine.board.wall[hitwall].x - Engine.board.wall[Engine.board.wall[hitwall].point2].x,
                                            Engine.board.wall[hitwall].y - Engine.board.wall[Engine.board.wall[hitwall].point2].y) + 512);
                                       // ssp(l, CLIPMASK0);
                                    }

                    SKIPBULLETHOLE:

                        if ((Engine.board.wall[hitwall].cstat & 2) != 0)
                            if (Engine.board.wall[hitwall].nextsector >= 0)
                                if (hitz >= (Engine.board.sector[Engine.board.wall[hitwall].nextsector].floorz))
                                    hitwall = Engine.board.wall[hitwall].nextwall;

                        //checkhitwall(k, hitwall, hitx, hity, hitz, Names.SHOTSPARK1);
                    }
#endif
                }
                /*
                else
                {
                    k = EGS(hitsect, hitx, hity, hitz, SHOTSPARK1, -15, 24, 24, sa, 0, 0, i, 4);
                    sprite[k].extra = *actorscrptr[atwith];

                    if (hitspr >= 0)
                    {
                        checkhitsprite(hitspr, k);
                        if (sprite[hitspr].picnum != APLAYER)
                            spawn(k, SMALLSMOKE);
                        else sprite[k].xrepeat = sprite[k].yrepeat = 0;
                    }
                    else if (hitwall >= 0)
                        checkhitwall(k, hitwall, hitx, hity, hitz, SHOTSPARK1);
                }
                */
                if ((Globals.TRAND() & 255) < 4)
                    SoundSystem.sound(SoundId.PISTOL_RICOCHET/*, k, hitx, hity, hitz*/);
            }

            return false; 
        }   
        public static bool palfrom(object actor, params object[] parms ) { return false; }   
        public static bool sound(object actor, params object[] parms ) 
        {
            int p = (int)parms[0];
            SoundSystem.sound((short)p);
            return false; 
        }
        public static bool setstateawake(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;
            _actor.ForceAwake();
            return false;
        }
        public static bool fall(object actor, params object[] parms ) { return false; }          // 16   []
        public static bool state(object actor, params object[] parms ) { return false; }        // 17
        public static bool ends(object actor, params object[] parms ) { return false; }         // 18
        public static bool define(object actor, params object[] parms ) { return false; }           // 19
        public static bool ifai(object actor, params object[] parms ) { return false; }         // 21
        public static bool killit(object actor, params object[] parms ) 
        {
            Game.KillActor((Actor)actor);
            return false; 
        }

        public static bool ifxrepeatl(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;

            if (_actor._sprite.xrepeat < (int)parms[0])
                return true;

            return false;
        }

        public static bool iflotag(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;

            if(_actor.Lotag == (int)parms[0])
                return true;

            return false;
        }

        public static bool addweapon(object actor, params object[] parms ) {
            Player player = actor as Player;
            player.GiveWeapon((int)parms[0], (int)parms[1]);
            return false; 
        }        // 23
        public static bool ai(object actor, params object[] parms ) { return false; }          // 24
        public static bool addphealth(object actor, params object[] parms ) { return false; }       // 25
        public static bool ifdead(object actor, params object[] parms ) { return false; }           // 26
        public static bool ifsquished(object actor, params object[] parms ) { return false; }       // 27
        public static bool sizeto(object actor, params object[] parms ) { return false; }           // 28
        public static bool _openbracket(object actor, params object[] parms ) { return false; }            // 29
        public static bool _closebracket(object actor, params object[] parms ) { return false; }            // 30
        public static bool spawn(object actor, params object[] parms ) {
            Actor _actor = actor as Actor;
            int picnum = (int)parms[0];

            if(_actor.frameskip == 0)
                Globals.lastspawnedsprite = Game.SpawnActor(_actor._sprite.x, _actor._sprite.y, _actor._sprite.z, _actor._sprite.sectnum, (short)picnum);

            _actor.frameskip++;

            if (_actor.frameskip > 2)
                _actor.frameskip = 0;

            return false; 
        }         // 31
        public static bool move(object actor, params object[] parms ) { return false; }         // 32
        public static bool ifwasweapon(object actor, params object[] parms ) { return false; }      // 33
        public static bool ifaction(object actor, params object[] parms ) { return false; }         // 34
        public static bool ifactioncount(object actor, params object[] parms ) {
            Actor _actor = actor as Actor;
            if (((int)parms[0]) == _actor.Tics)
                return true;

            return false;
        }    // 35
        public static bool animate(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;
            _actor.AnimateFrame(((int[])parms[0])[1]);
            return false;
        }
        public static bool resetactioncount(object actor, params object[] parms ) { return false; } // 36
        public static bool debris(object actor, params object[] parms ) { return false; }           // 37
        public static bool pstomp(object actor, params object[] parms ) { return false; }           // 38
        public static bool cstat(object actor, params object[] parms ) { return false; }         // 40
        public static bool ifmove(object actor, params object[] parms ) { return false; }           // 41
        public static bool resetplayer(object actor, params object[] parms ) { return false; }      // 42
        public static bool ifonwater(object actor, params object[] parms ) { return false; }        // 43
        public static bool ifinwater(object actor, params object[] parms ) { return false; }        // 44
        public static bool ifcanshoottarget(object actor, params object[] parms ) { return false; } // 45
        public static bool resetcount(object actor, params object[] parms ) { return false; }       // 47
        public static bool addinventory(object actor, params object[] parms ) { return false; }     // 48
        public static bool ifactornotstayput(object actor, params object[] parms ) { return false; }// 49
        public static bool hitradius(object actor, params object[] parms ) { return false; }        // 50
        public static bool ifp(object actor, params object[] parms ) {
            Actor _actor = (Actor)actor;
            if (((int)parms[0]) == 0)
            {
                if(_actor.cansee(Globals.ps[0]))
                    return true;
            }
            else if (((int)parms[0]) == 1)
            {
                if (_actor._sprite.sectnum == Globals.ps[0].SectorNum)
                    return true;
            }
            return false; 
        }

        public static bool ifpweapon(object actor, params object[] parms)
        {
            Player player = actor as Player;

            if (player.curr_weapon == (int)parms[0])
                return true;

            return false;
        }

        public static bool ifpselectedweapon(object actor, params object[] parms)
        {
            Player player = actor as Player;

            if (player.curr_weapon == (int)parms[0])
                return true;

            return false;
        }

        public static bool ifpammol(object actor, params object[] parms)
        {
            Player player = actor as Player;
            if (parms.Length > 1)
            {
                int weaponnum = (int)parms[0];
                if (weaponnum > 0 && player.gotweapon[weaponnum] == false)
                    return false;

                if ((player.ammo_amount[weaponnum] <= (int)parms[1]))
                {
                    return true;
                }
            }
            else
            {
                if (player.curr_weapon == 0)
                    return false;

                if ((player.ammo_amount[player.curr_weapon] <= (int)parms[0]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool setpweapon(object actor, params object[] parms)
        {
            Player player = actor as Player;
            player.curr_weapon = (int)parms[0];
            return false;
        }

        public static bool ifweaponframe(object actor, params object[] parms)
        {
            Player player = actor as Player;

            if (player.kickback_pic == (int)parms[0])
                return true;

            return false;
        }

        public static bool ifpclipcnt(object actor, params object[] parms)
        {
            Player player = actor as Player;
            if ((player.ammo_amount[(int)parms[0]] % (int)parms[1]) == 0)
            {
                return true;
            }

            return false;
        }

        public static bool removebullets(object actor, params object[] parms)
        {
            Player player = actor as Player;
            player.ammo_amount[(int)parms[0]] -= (int)parms[1];

            return false;
        }

        public static bool setfiredelay(object actor, params object[] parms)
        {
            Player player = actor as Player;
            player.firedelay = (int)parms[0];

            return false;
        }

        public static bool weaponanimate(object actor, params object[] parms)
        {
            Player player = actor as Player;

            player.AnimateFirstPersonWeapon((int)parms[0], (int)parms[1]);

            return false;
        }

        public static bool ifcount(object actor, params object[] parms) {
            Actor _actor = actor as Actor;
            if (parms.Length > 1)
            {
                if (((int)parms[1]) == 0)
                {
                    if (((int)parms[0]) == -1)
                    {
                        if (_actor.Hitag < _actor.Tics)
                            return true;
                    }
                    else if (((int)parms[0]) == 0)
                    {
                        if (_actor.Hitag == _actor.Tics)
                            return true;
                    }
                    else if (((int)parms[0]) == 1)
                    {
                        if (_actor.Hitag > _actor.Tics)
                            return true;
                    }
                }
            }
            else
            {
                if (((int)parms[0]) == _actor.Tics)
                    return true;
            }
            return false; 
        }

        public static bool count(object actor, params object[] parms ) { return false; }         // 52
        public static bool ifactor(object actor, params object[] parms ) { return false; }          // 53
        public static bool music(object actor, params object[] parms ) { return false; }         // 54
        public static bool include(object actor, params object[] parms ) { return false; }          // 55
        public static bool ifstrength(object actor, params object[] parms ) { return false; }       // 56
        public static bool definesound(object actor, params object[] parms ) { return false; }      // 57
        public static bool guts(object actor, params object[] parms ) { return false; }          // 58
        public static bool ifspawnedby(object actor, params object[] parms ) { return false; }      // 59
        public static bool gamestartup(object actor, params object[] parms ) { return false; }      // 60
        public static bool wackplayer(object actor, params object[] parms ) { return false; }       // 61
        public static bool ifgapzl(object actor, params object[] parms ) { return false; }          // 62
        public static bool ifhitspace(object actor, params object[] parms ) { return false; }       // 63
        public static bool ifoutside(object actor, params object[] parms ) { return false; }        // 64
        public static bool ifmultiplayer(object actor, params object[] parms ) { return false; }    // 65
        public static bool operate(object actor, params object[] parms ) { return false; }          // 66
        public static bool ifinspace(object actor, params object[] parms ) { return false; }        // 67
        public static bool debug(object actor, params object[] parms ) { return false; }         // 68
        public static bool endofgame(object actor, params object[] parms ) { return false; }        // 69
        public static bool ifbulletnear(object actor, params object[] parms ) { return false; }     // 70
        public static bool ifrespawn(object actor, params object[] parms ) { return false; }        // 71
        public static bool iffloordistl(object actor, params object[] parms ) { return false; }     // 72
        public static bool ifceilingdistl(object actor, params object[] parms ) { return false; }   // 73
        public static bool spritepal(object actor, params object[] parms ) {
            if (parms.Length > 1)
            {
                int spritenum = ((int[])parms[0])[0];
                Actor _actor = (Actor)Engine.board.sprite[spritenum].obj;
                int pal = (int)parms[1];
                _actor._sprite.pal = (byte)pal;
            }
            else
            {
                Actor _actor = actor as Actor;
                _actor._sprite.pal = (byte)parms[0];
            }
            return false; 
        }        // 74
        public static bool ifpinventory(object actor, params object[] parms ) { return false; }     // 75
        public static bool betaname(object actor, params object[] parms ) { return false; }         // 76
        public static bool cactor(object actor, params object[] parms ) { return false; }           // 77
        public static bool ifphealthl(object actor, params object[] parms ) { return false; }       // 78
        public static bool definequote(object actor, params object[] parms ) { return false; }      // 79
        public static bool quote(object actor, params object[] parms ) { return false; }         // 80
        public static bool ifinouterspace(object actor, params object[] parms ) { return false; }   // 81
        public static bool ifnotmoving(object actor, params object[] parms ) { return false; }      // 82
        public static bool respawnhitag(object actor, params object[] parms ) { return false; }        // 83
        public static bool tip(object actor, params object[] parms ) { return false; }          // 84
        public static bool ifspritepal(object actor, params object[] parms) {
            Actor _actor = (Actor)actor;

            if (_actor.GetCurrentPalette() == (int)parms[0])
                return true;

            return false; 
        }      // 85

        public static bool movevel(object actor, params object[] parms)
        {
            Actor _actor = (Actor)actor;

            _actor.SetVelocity((int)parms[0], (int)parms[1], (int)parms[2]);
            return false;
        }

        public static bool money(object actor, params object[] parms ) { return false; }         // 86
        public static bool soundonce(object actor, params object[] parms ) { return false; }         // 87
        public static bool addkills(object actor, params object[] parms ) { return false; }         // 88
        public static bool stopsound(object actor, params object[] parms ) { return false; }        // 89
        public static bool ifawayfromwall(object actor, params object[] parms ) { return false; }       // 90
        public static bool ifcanseetarget(object actor, params object[] parms ) { return false; }   // 91
        public static bool globalsound(object actor, params object[] parms ) { return false; }  // 92
        public static bool lotsofglass(object actor, params object[] parms ) { return false; } // 93
        public static bool ifgotweaponce(object actor, params object[] parms ) { return false; } // 94
        public static bool getlastpal(object actor, params object[] parms ) { return false; } // 95
        public static bool pkick(object actor, params object[] parms ) { return false; }  // 96
        public static bool mikesnd(object actor, params object[] parms ) { return false; } // 97
        public static bool useractor(object actor, params object[] parms ) { return false; }  // 98
        public static bool sizeat(object actor, params object[] parms ) { return false; }  // 99
        public static bool addstrength(object actor, params object[] parms ) { return false; } // 100   [#]
        public static bool cstator(object actor, params object[] parms ) { return false; } // 101
        public static bool mail(object actor, params object[] parms ) { return false; } // 102
        public static bool paper(object actor, params object[] parms ) { return false; } // 103
        public static bool tossweapon(object actor, params object[] parms ) { return false; } // 104
        public static bool sleeptime(object actor, params object[] parms ) { return false; } // 105
        public static bool nullop(object actor, params object[] parms ) { return false; } // 106
        public static bool definevolumename(object actor, params object[] parms ) { return false; } // 107
        public static bool defineskillname(object actor, params object[] parms ) { return false; } // 108
        public static bool ifnosounds(object actor, params object[] parms ) { return false; } // 109
        public static bool clipdist(object actor, params object[] parms ) { return false; } // 110
        public static bool ifangdiffl(object actor, params object[] parms ) { return false; } // 111

        public static bool hidesprite(object actor, params object[] parms ) {
            Actor _actor = (Actor)actor;

            _actor.Hide();

            return false; 
        }

        public static bool playlotagsound(object actor, params object[] parms)
        {
            Actor _actor = actor as Actor;
            SoundSystem.sound((short)_actor.Lotag);
            return false;
        }
    }
}
