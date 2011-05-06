using System;

public static class GlobalMembersPLAYER
{
	//-------------------------------------------------------------------------
	/*
	Copyright (C) 1996, 2003 - 3D Realms Entertainment
	
	This file is part of Duke Nukem 3D version 1.5 - Atomic Edition
	
	Duke Nukem 3D is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.
	
	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
	
	See the GNU General Public License for more details.
	
	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
	
	Original Source: 1996 - Todd Replogle
	Prepared for public release: 03/21/2003 - Charlie Wiederhold, 3D Realms
	*/
	//-------------------------------------------------------------------------

	// Savage Baggage Masters


	public static int32 turnheldtime; //MED
	public static int32 lastcontroltime; //MED

	public static void setpal(ref player_struct p)
	{
		if(p.heat_on)
			p.palette = GlobalMembersGAME.slimepal;
		else
			switch(sector[p.cursectnum].ceilingpicnum)
		{
			case FLOORSLIME:
			case FLOORSLIME+1:
			case FLOORSLIME+2:
				p.palette = GlobalMembersGAME.slimepal;
				break;
			default:
				if(sector[p.cursectnum].lotag == 2)
					p.palette = GlobalMembersGAME.waterpal;
				else
					p.palette = palette;
				break;
		}
		GlobalMembersGAME.restorepalette = 1;
	}

	public static void incur_damage(ref player_struct p)
	{
		int damage = 0;
		int shield_damage = 0;
		short i;
		short damage_source;

		sprite[p.i].extra -= p.extra_extra8>>8;

		damage = sprite[p.i].extra - p.last_extra;

		if (damage < 0)
		{
			p.extra_extra8 = 0;

			if (p.shield_amount > 0)
			{
				shield_damage = damage * (20 + (TRAND%30)) / 100;
				damage -= shield_damage;

				p.shield_amount += shield_damage;

				if (p.shield_amount < 0)
				{
					damage += p.shield_amount;
					p.shield_amount = 0;
				}
			}

			sprite[p.i].extra = p.last_extra + damage;
		}
	}

	public static void quickkill(ref player_struct p)
	{
		p.pals[0] = 48;
		p.pals[1] = 48;
		p.pals[2] = 48;
		p.pals_time = 48;

		sprite[p.i].extra = 0;
		sprite[p.i].cstat |= 32768;
		if(GlobalMembersGLOBAL.ud.god == 0)
			GlobalMembersACTORS.guts(ref sprite[p.i], JIBS6, 8, myconnectindex);
		return;
	}

	public static void forceplayerangle(ref player_struct p)
	{
		short n;

		n = 128-(TRAND &255);

		p.horiz += 64;
		p.return_to_center = 9;
		p.look_ang = n>>1;
		p.rotscrnang = n>>1;
	}

	public static void tracers(int x1,int y1,int z1,int x2,int y2,int z2,int n)
	{
		 int i;
		 int xv;
		 int yv;
		 int zv;
		 short sect = -1;

		 i = n+1;
		 xv = (x2-x1)/i;
		 yv = (y2-y1)/i;
		 zv = (z2-z1)/i;

		 if((klabs(x1-x2)+klabs(y1-y2)) < 3084)
			 return;

		 for(i = n;i>0;i--)
		 {
			  x1 += xv;
			  y1 += yv;
			  z1 += zv;
			  updatesector(x1, y1, sect);
			  if(sect >= 0)
			  {
				  if(sector[sect].lotag == 2)
					  GlobalMembersGAME.EGS(sect, x1, y1, z1, WATERBUBBLE, -32, 4+(TRAND &3), 4+(TRAND &3), TRAND &2047, 0, 0, GlobalMembersGLOBAL.ps[0].i, 5);
				  else
					  GlobalMembersGAME.EGS(sect, x1, y1, z1, SMALLSMOKE, -32, 14, 14, 0, 0, 0, GlobalMembersGLOBAL.ps[0].i, 5);
			  }
		 }
	}

	public static int hits(short i)
	{
		int sx;
		int sy;
		int sz;
		short sect;
		short hw;
		short hs;
		int zoff;

		if(PN == APLAYER)
			zoff = (40<<8);
		else
			zoff = 0;

		hitscan(SX, SY, SZ-zoff, SECT, sintable[(SA+512)&2047], sintable[SA &2047], 0, sect, hw, hs, sx, sy, sz, CLIPMASK1);

		return (FindDistance2D(sx-SX,sy-SY));
	}

	public static int hitasprite(short i, ref short hitsp)
	{
		int sx;
		int sy;
		int sz;
		int zoff;
		short sect;
		short hw;

		if (GlobalMembersGAME.badguy(ref sprite[i]) != 0)
			zoff = (42<<8);
		else if(PN == APLAYER)
			zoff = (39<<8);
		else
			zoff = 0;

		hitscan(SX, SY, SZ-zoff, SECT, sintable[(SA+512)&2047], sintable[SA &2047], 0, sect, hw, hitsp, sx, sy, sz, CLIPMASK1);

		if(hw >= 0 && (wall[hw].cstat &16) && GlobalMembersGAME.badguy(ref sprite[i]) != 0)
			return((1<<30));

		return (FindDistance2D(sx-SX,sy-SY));
	}

	/*
	long hitaspriteandwall(short i,short *hitsp,short *hitw,short *x, short *y)
	{
	    long sz;
	    short sect;
	
	    hitscan(SX,SY,SZ,SECT,
	        sintable[(SA+512)&2047],
	        sintable[SA&2047],
	        0,&sect,hitw,hitsp,x,y,&sz,CLIPMASK1);
	
	    return ( FindDistance2D(*x-SX,*y-SY) );
	}
	*/


	public static int hitawall(ref player_struct p, ref short hitw)
	{
		int sx;
		int sy;
		int sz;
		short sect;
		short hs;

		hitscan(p.posx, p.posy, p.posz, p.cursectnum, sintable[(p.ang+512)&2047], sintable[p.ang &2047], 0, sect, hitw, hs, sx, sy, sz, CLIPMASK0);

		return (FindDistance2D(sx-p.posx,sy-p.posy));
	}

	public static short aim(ref spritetype s, short aang)
	{
		sbyte gotshrinker;
		sbyte gotfreezer;
		short i;
		short j;
		short a;
		short k;
		short cans;
		short[] aimstats = {10,13,1,2};
		int dx1;
		int dy1;
		int dx2;
		int dy2;
		int dx3;
		int dy3;
		int smax;
		int sdist;
		int xv;
		int yv;

		a = s.ang;

		j = -1;
	//    if(s->picnum == APLAYER && ps[s->yvel].aim_mode) return -1;

		gotshrinker = s.picnum == APLAYER && GlobalMembersGLOBAL.ps[s.yvel].curr_weapon == SHRINKER_WEAPON;
		gotfreezer = s.picnum == APLAYER && GlobalMembersGLOBAL.ps[s.yvel].curr_weapon == FREEZE_WEAPON;

		smax = 0x7fffffff;

		dx1 = sintable[(a+512-aang)&2047];
		dy1 = sintable[(a-aang)&2047];
		dx2 = sintable[(a+512+aang)&2047];
		dy2 = sintable[(a+aang)&2047];

		dx3 = sintable[(a+512)&2047];
		dy3 = sintable[a &2047];

		for(k = 0;k<4;k++)
		{
			if(j >= 0)
				break;
			for(i = headspritestat[aimstats[k]];i >= 0;i = nextspritestat[i])
				if(sprite[i].xrepeat > 0 && sprite[i].extra >= 0 && (sprite[i].cstat&(257+32768)) == 257)
					if(GlobalMembersGAME.badguy(ref sprite[i]) != 0 || k < 2)
				{
					if(GlobalMembersGAME.badguy(ref sprite[i]) != 0 || PN == APLAYER || PN == SHARK)
					{
						if(PN == APLAYER && GlobalMembersGLOBAL.ud.coop == 1 && s.picnum == APLAYER && s != &sprite[i])
	//                        ud.ffire == 0 &&
								continue;

						if(gotshrinker != 0 && sprite[i].xrepeat < 30)
						{
							switch(PN)
							{
								case SHARK:
									if(sprite[i].xrepeat < 20)
										continue;
										continue;
								case GREENSLIME:
								case GREENSLIME+1:
								case GREENSLIME+2:
								case GREENSLIME+3:
								case GREENSLIME+4:
								case GREENSLIME+5:
								case GREENSLIME+6:
								case GREENSLIME+7:
									break;
								default:
									continue;
							}
						}
						if(gotfreezer != 0 && sprite[i].pal == 1)
							continue;
					}

					xv = (SX-s.x);
					yv = (SY-s.y);

					if((dy1 *xv) <= (dx1 *yv))
						if((dy2 *xv) >= (dx2 *yv))
					{
						sdist = mulscale(dx3,xv,14) + mulscale(dy3,yv,14);
						if(sdist > 512 && sdist < smax)
						{
							if(s.picnum == APLAYER)
								a = (klabs(scale(SZ-s.z,10,sdist)-(GlobalMembersGLOBAL.ps[s.yvel].horiz+GlobalMembersGLOBAL.ps[s.yvel].horizoff-100)) < 100);
							else
								a = 1;

							if(PN == ORGANTIC || PN == ROTATEGUN)
								cans = cansee(SX,SY,SZ,SECT,s.x,s.y,s.z-(32<<8),s.sectnum);
							else
								cans = cansee(SX,SY,SZ-(32<<8),SECT,s.x,s.y,s.z-(32<<8),s.sectnum);

							if(a != 0 && cans != 0)
							{
								smax = sdist;
								j = i;
							}
						}
					}
				}
		}

		return j;
	}




	public static void shoot(short i,short atwith)
	{
		short sect;
		short hitsect;
		short hitspr;
		short hitwall;
		short l;
		short sa;
		short p;
		short j;
		short k;
		short scount;
		int sx;
		int sy;
		int sz;
		int vel;
		int zvel;
		int hitx;
		int hity;
		int hitz;
		int x;
		int oldzvel;
		int dal;
		byte sizx;
		byte sizy;
		spritetype s;

		s = sprite[i];
		sect = s.sectnum;
		zvel = 0;

		if(s.picnum == APLAYER)
		{
			p = s.yvel;

			sx = GlobalMembersGLOBAL.ps[p].posx;
			sy = GlobalMembersGLOBAL.ps[p].posy;
			sz = GlobalMembersGLOBAL.ps[p].posz+GlobalMembersGLOBAL.ps[p].pyoff+(4<<8);
			sa = GlobalMembersGLOBAL.ps[p].ang;

			GlobalMembersGLOBAL.ps[p].crack_time = 777;

		}
		else
		{
			p = -1;
			sa = s.ang;
			sx = s.x;
			sy = s.y;
			sz = s.z-((s.yrepeat *tilesizy[s.picnum])<<1)+(4<<8);
			if(s.picnum != ROTATEGUN)
			{
				sz -= (7<<8);
				if(GlobalMembersGAME.badguy(ref s) != 0 && PN != COMMANDER)
				{
					sx += (sintable[(sa+1024+96)&2047]>>7);
					sy += (sintable[(sa+512+96)&2047]>>7);
				}
			}
		}

		switch(atwith)
		{
			case BLOODSPLAT1:
			case BLOODSPLAT2:
			case BLOODSPLAT3:
			case BLOODSPLAT4:

				if(p >= 0)
					sa += 64 - (TRAND &127);
				else
					sa += 1024 + 64 - (TRAND &127);
				zvel = 1024-(TRAND &2047);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case KNEE:
				if(atwith == KNEE)
				{
					if(p >= 0)
					{
						zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)<<5;
						sz += (6<<8);
						sa += 15;
					}
					else
					{
						j = GlobalMembersGLOBAL.ps[GlobalMembersSECTOR.findplayer(ref s, ref x)].i;
						zvel = ((sprite[j].z-sz)<<8) / (x+1);
						sa = getangle(sprite[j].x-sx,sprite[j].y-sy);
					}
				}

	//            writestring(sx,sy,sz,sect,sintable[(sa+512)&2047],sintable[sa&2047],zvel<<6);

				hitscan(sx, sy, sz, sect, sintable[(sa+512)&2047], sintable[sa &2047], zvel<<6, hitsect, hitwall, hitspr, hitx, hity, hitz, CLIPMASK1);

				if(atwith == BLOODSPLAT1 || atwith == BLOODSPLAT2 || atwith == BLOODSPLAT3 || atwith == BLOODSPLAT4)
				{
					if(FindDistance2D(sx-hitx,sy-hity) < 1024)
						if(hitwall >= 0 && wall[hitwall].overpicnum != BIGFORCE)
							if((wall[hitwall].nextsector >= 0 && hitsect >= 0 && sector[wall[hitwall].nextsector].lotag == 0 && sector[hitsect].lotag == 0 && sector[wall[hitwall].nextsector].lotag == 0 && (sector[hitsect].floorz-sector[wall[hitwall].nextsector].floorz) > (16<<8)) || (wall[hitwall].nextsector == -1 && sector[hitsect].lotag == 0))
													if((wall[hitwall].cstat &16) == 0)
					{
						if(wall[hitwall].nextsector >= 0)
						{
							k = headspritesect[wall[hitwall].nextsector];
							while(k >= 0)
							{
								if(sprite[k].statnum == 3 && sprite[k].lotag == 13)
									return;
								k = nextspritesect[k];
							}
						}

						if(wall[hitwall].nextwall >= 0 && wall[wall[hitwall].nextwall].hitag != 0)
								return;

						if(wall[hitwall].hitag == 0)
						{
							k = GlobalMembersGAME.spawn(i, atwith);
							sprite[k].xvel = -12;
							sprite[k].ang = getangle(wall[hitwall].x-wall[wall[hitwall].point2].x, wall[hitwall].y-wall[wall[hitwall].point2].y)+512;
							sprite[k].x = hitx;
							sprite[k].y = hity;
							sprite[k].z = hitz;
							sprite[k].cstat |= (TRAND &4);
							GlobalMembersACTORS.ssp(k, CLIPMASK0);
							setsprite(k,sprite[k].x,sprite[k].y,sprite[k].z);
							if(PN == OOZFILTER || PN == NEWBEAST)
								sprite[k].pal = 6;
						}
					}
					return;
				}

				if(hitsect < 0)
					break;

				if((klabs(sx-hitx)+klabs(sy-hity)) < 1024)
				{
					if(hitwall >= 0 || hitspr >= 0)
					{
						j = GlobalMembersGAME.EGS(hitsect, hitx, hity, hitz, KNEE, -15, 0, 0, sa, 32, 0, i, 4);
						sprite[j].extra += (TRAND &7);
						if(p >= 0)
						{
							k = GlobalMembersGAME.spawn(j, SMALLSMOKE);
							sprite[k].z -= (8<<8);
							GlobalMembersSOUNDS.spritesound(KICK_HIT, j);
						}

						if (p >= 0 && GlobalMembersGLOBAL.ps[p].steroids_amount > 0 && GlobalMembersGLOBAL.ps[p].steroids_amount < 400)
							sprite[j].extra += (GlobalMembersGLOBAL.max_player_health>>2);

						if(hitspr >= 0 && sprite[hitspr].picnum != ACCESSSWITCH && sprite[hitspr].picnum != ACCESSSWITCH2)
						{
							GlobalMembersSECTOR.checkhitsprite(hitspr, j);
							if(p >= 0)
								GlobalMembersSECTOR.checkhitswitch(p, hitspr, 1);
						}

						else if(hitwall >= 0)
						{
							if(wall[hitwall].cstat &2)
								if(wall[hitwall].nextsector >= 0)
									if(hitz >= (sector[wall[hitwall].nextsector].floorz))
										hitwall = wall[hitwall].nextwall;

							if(hitwall >= 0 && wall[hitwall].picnum != ACCESSSWITCH && wall[hitwall].picnum != ACCESSSWITCH2)
							{
								GlobalMembersSECTOR.checkhitwall(j, hitwall, hitx, hity, hitz, atwith);
								if(p >= 0)
									GlobalMembersSECTOR.checkhitswitch(p, hitwall, 0);
							}
						}
					}
					else if(p >= 0 && zvel > 0 && sector[hitsect].lotag == 1)
					{
						j = GlobalMembersGAME.spawn(GlobalMembersGLOBAL.ps[p].i, WATERSPLASH2);
						sprite[j].x = hitx;
						sprite[j].y = hity;
						sprite[j].ang = GlobalMembersGLOBAL.ps[p].ang; // Total tweek
						sprite[j].xvel = 32;
						GlobalMembersACTORS.ssp(i, CLIPMASK0);
						sprite[j].xvel = 0;

					}
				}

				break;

			case SHOTSPARK1:
			case SHOTGUN:
			case CHAINGUN:

				if(s.extra >= 0)
					s.shade = -96;

				if(p >= 0)
				{
					j = GlobalMembersPLAYER.aim(ref s, AUTO_AIM_ANGLE);
					if(j >= 0)
					{
						dal = ((sprite[j].xrepeat *tilesizy[sprite[j].picnum])<<1)+(5<<8);
						switch(sprite[j].picnum)
						{
							case GREENSLIME:
							case GREENSLIME+1:
							case GREENSLIME+2:
							case GREENSLIME+3:
							case GREENSLIME+4:
							case GREENSLIME+5:
							case GREENSLIME+6:
							case GREENSLIME+7:
							case ROTATEGUN:
								dal -= (8<<8);
								break;
						}
						zvel = ((sprite[j].z-sz-dal)<<8) / GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[p].i], ref sprite[j]);
						sa = getangle(sprite[j].x-sx,sprite[j].y-sy);
					}

					if(atwith == SHOTSPARK1)
					{
						if(j == -1)
						{
							sa += 16-(TRAND &31);
							zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)<<5;
							zvel += 128-(TRAND &255);
						}
					}
					else
					{
						sa += 16-(TRAND &31);
						if(j == -1)
							zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)<<5;
						zvel += 128-(TRAND &255);
					}
					sz -= (2<<8);
				}
				else
				{
					j = GlobalMembersSECTOR.findplayer(ref s, ref x);
					sz -= (4<<8);
					zvel = ((GlobalMembersGLOBAL.ps[j].posz-sz) <<8) / (GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[j].i], ref s));
					if(s.picnum != BOSS1)
					{
						zvel += 128-(TRAND &255);
						sa += 32-(TRAND &63);
					}
					else
					{
						zvel += 128-(TRAND &255);
						sa = getangle(GlobalMembersGLOBAL.ps[j].posx-sx,GlobalMembersGLOBAL.ps[j].posy-sy)+64-(TRAND &127);
					}
				}

				s.cstat &= ~257;
				hitscan(sx, sy, sz, sect, sintable[(sa+512)&2047], sintable[sa &2047], zvel<<6, hitsect, hitwall, hitspr, hitx, hity, hitz, CLIPMASK1);
				s.cstat |= 257;

				if(hitsect < 0)
					return;

				if((TRAND &15) == 0 && sector[hitsect].lotag == 2)
					GlobalMembersPLAYER.tracers(hitx, hity, hitz, sx, sy, sz, 8-(GlobalMembersGLOBAL.ud.multimode>>1));

				if(p >= 0)
				{
					k = GlobalMembersGAME.EGS(hitsect, hitx, hity, hitz, SHOTSPARK1, -15, 10, 10, sa, 0, 0, i, 4);
					sprite[k].extra = GlobalMembersGLOBAL.actorscrptr[atwith];
					sprite[k].extra += (TRAND%6);

					if(hitwall == -1 && hitspr == -1)
					{
						if(zvel < 0)
						{
							if(sector[hitsect].ceilingstat &1)
							{
								sprite[k].xrepeat = 0;
								sprite[k].yrepeat = 0;
								return;
							}
							else
								GlobalMembersSECTOR.checkhitceiling(hitsect);
						}
						GlobalMembersGAME.spawn(k, SMALLSMOKE);
					}

					if(hitspr >= 0)
					{
						GlobalMembersSECTOR.checkhitsprite(hitspr, k);
						if(sprite[hitspr].picnum == APLAYER && (GlobalMembersGLOBAL.ud.coop != 1 || GlobalMembersGLOBAL.ud.ffire == 1))
						{
							l = GlobalMembersGAME.spawn(k, JIBS6);
							sprite[k].xrepeat = sprite[k].yrepeat = 0;
							sprite[l].z += (4<<8);
							sprite[l].xvel = 16;
							sprite[l].xrepeat = sprite[l].yrepeat = 24;
							sprite[l].ang += 64-(TRAND &127);
						}
						else
							GlobalMembersGAME.spawn(k, SMALLSMOKE);

						if(p >= 0 && (sprite[hitspr].picnum == DIPSWITCH || sprite[hitspr].picnum == DIPSWITCH+1 || sprite[hitspr].picnum == DIPSWITCH2 || sprite[hitspr].picnum == DIPSWITCH2+1 || sprite[hitspr].picnum == DIPSWITCH3 || sprite[hitspr].picnum == DIPSWITCH3+1 || sprite[hitspr].picnum == HANDSWITCH || sprite[hitspr].picnum == HANDSWITCH+1))
						{
							GlobalMembersSECTOR.checkhitswitch(p, hitspr, 1);
							return;
						}
					}
					else if(hitwall >= 0)
					{
						GlobalMembersGAME.spawn(k, SMALLSMOKE);

						if(GlobalMembersSECTOR.isadoorwall(wall[hitwall].picnum) == 1)
							goto SKIPBULLETHOLE;
						if(p >= 0 && (wall[hitwall].picnum == DIPSWITCH || wall[hitwall].picnum == DIPSWITCH+1 || wall[hitwall].picnum == DIPSWITCH2 || wall[hitwall].picnum == DIPSWITCH2+1 || wall[hitwall].picnum == DIPSWITCH3 || wall[hitwall].picnum == DIPSWITCH3+1 || wall[hitwall].picnum == HANDSWITCH || wall[hitwall].picnum == HANDSWITCH+1))
						{
							GlobalMembersSECTOR.checkhitswitch(p, hitwall, 0);
							return;
						}

						if(wall[hitwall].hitag != 0 || (wall[hitwall].nextwall >= 0 && wall[wall[hitwall].nextwall].hitag != 0))
							goto SKIPBULLETHOLE;

						if(hitsect >= 0 && sector[hitsect].lotag == 0)
							if(wall[hitwall].overpicnum != BIGFORCE)
								if((wall[hitwall].nextsector >= 0 && sector[wall[hitwall].nextsector].lotag == 0) || (wall[hitwall].nextsector == -1 && sector[hitsect].lotag == 0))
										if((wall[hitwall].cstat &16) == 0)
						{
							if(wall[hitwall].nextsector >= 0)
							{
								l = headspritesect[wall[hitwall].nextsector];
								while(l >= 0)
								{
									if(sprite[l].statnum == 3 && sprite[l].lotag == 13)
										goto SKIPBULLETHOLE;
									l = nextspritesect[l];
								}
							}

							l = headspritestat[5];
							while(l >= 0)
							{
								if(sprite[l].picnum == BULLETHOLE)
									if(GlobalMembersSECTOR.dist(ref sprite[l], ref sprite[k]) < (12+(TRAND &7)))
										goto SKIPBULLETHOLE;
								l = nextspritestat[l];
							}
							l = GlobalMembersGAME.spawn(k, BULLETHOLE);
							sprite[l].xvel = -1;
							sprite[l].ang = getangle(wall[hitwall].x-wall[wall[hitwall].point2].x, wall[hitwall].y-wall[wall[hitwall].point2].y)+512;
							GlobalMembersACTORS.ssp(l, CLIPMASK0);
						}

						SKIPBULLETHOLE:

						if(wall[hitwall].cstat &2)
							if(wall[hitwall].nextsector >= 0)
								if(hitz >= (sector[wall[hitwall].nextsector].floorz))
									hitwall = wall[hitwall].nextwall;

						GlobalMembersSECTOR.checkhitwall(k, hitwall, hitx, hity, hitz, SHOTSPARK1);
					}
				}
				else
				{
					k = GlobalMembersGAME.EGS(hitsect, hitx, hity, hitz, SHOTSPARK1, -15, 24, 24, sa, 0, 0, i, 4);
					sprite[k].extra = GlobalMembersGLOBAL.actorscrptr[atwith];

					if(hitspr >= 0)
					{
						GlobalMembersSECTOR.checkhitsprite(hitspr, k);
						if(sprite[hitspr].picnum != APLAYER)
							GlobalMembersGAME.spawn(k, SMALLSMOKE);
						else
							sprite[k].xrepeat = sprite[k].yrepeat = 0;
					}
					else if(hitwall >= 0)
						GlobalMembersSECTOR.checkhitwall(k, hitwall, hitx, hity, hitz, SHOTSPARK1);
				}

				if((TRAND &255) < 4)
					GlobalMembersSOUNDS.xyzsound(PISTOL_RICOCHET, k, hitx, hity, hitz);

				return;

			case FIRELASER:
			case SPIT:
			case COOLEXPLOSION1:

				if(s.extra >= 0)
					s.shade = -96;

				scount = 1;
				if(atwith == SPIT)
					vel = 292;
				else
				{
					if(atwith == COOLEXPLOSION1)
					{
						if(s.picnum == BOSS2)
							vel = 644;
						else
							vel = 348;
						sz -= (4<<7);
					}
					else
					{
						vel = 840;
						sz -= (4<<7);
					}
				}

				if(p >= 0)
				{
					j = GlobalMembersPLAYER.aim(ref s, AUTO_AIM_ANGLE);

					if(j >= 0)
					{
						dal = ((sprite[j].xrepeat *tilesizy[sprite[j].picnum])<<1)-(12<<8);
						zvel = ((sprite[j].z-sz-dal)*vel) / GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[p].i], ref sprite[j]);
						sa = getangle(sprite[j].x-sx,sprite[j].y-sy);
					}
					else
						zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)*98;
				}
				else
				{
					j = GlobalMembersSECTOR.findplayer(ref s, ref x);
	//                sa = getangle(ps[j].oposx-sx,ps[j].oposy-sy);
					sa += 16-(TRAND &31);
					zvel = (((GlobalMembersGLOBAL.ps[j].oposz - sz + (3<<8)))*vel) / GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[j].i], ref s);
				}

				oldzvel = zvel;

				if(atwith == SPIT)
				{
					sizx = 18;
					sizy = 18,sz -= (10<<8);
				}
				else
				{
					if(atwith == FIRELASER)
					{
						if(p >= 0)
						{

							sizx = 34;
							sizy = 34;
						}
						else
						{
							sizx = 18;
							sizy = 18;
						}
					}
					else
					{
						sizx = 18;
						sizy = 18;
					}
				}

				if(p >= 0)
					sizx = 7,sizy = 7;

				while(scount > 0)
				{
					j = GlobalMembersGAME.EGS(sect, sx, sy, sz, atwith, -127, sizx, sizy, sa, vel, zvel, i, 4);
					sprite[j].extra += (TRAND &7);

					if(atwith == COOLEXPLOSION1)
					{
						sprite[j].shade = 0;
						if(PN == BOSS2)
						{
							l = sprite[j].xvel;
							sprite[j].xvel = 1024;
							GlobalMembersACTORS.ssp(j, CLIPMASK0);
							sprite[j].xvel = l;
							sprite[j].ang += 128-(TRAND &255);
						}
					}

					sprite[j].cstat = 128;
					sprite[j].clipdist = 4;

					sa = s.ang+32-(TRAND &63);
					zvel = oldzvel+512-(TRAND &1023);

					scount--;
				}

				return;

			case FREEZEBLAST:
				sz += (3<<8);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case RPG:

				if(s.extra >= 0)
					s.shade = -96;

				scount = 1;
				vel = 644;

				j = -1;

				if(p >= 0)
				{
					j = GlobalMembersPLAYER.aim(ref s, 48);
					if(j >= 0)
					{
						dal = ((sprite[j].xrepeat *tilesizy[sprite[j].picnum])<<1)+(8<<8);
						zvel = ((sprite[j].z-sz-dal)*vel) / GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[p].i], ref sprite[j]);
						if(sprite[j].picnum != RECON)
							sa = getangle(sprite[j].x-sx,sprite[j].y-sy);
					}
					else
						zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)*81;
					if(atwith == RPG)
						GlobalMembersSOUNDS.spritesound(RPG_SHOOT, i);

				}
				else
				{
					j = GlobalMembersSECTOR.findplayer(ref s, ref x);
					sa = getangle(GlobalMembersGLOBAL.ps[j].oposx-sx,GlobalMembersGLOBAL.ps[j].oposy-sy);
					if(PN == BOSS3)
						sz -= (32<<8);
					else if(PN == BOSS2)
					{
						vel += 128;
						sz += 24<<8;
					}

					l = GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[j].i], ref s);
					zvel = ((GlobalMembersGLOBAL.ps[j].oposz-sz)*vel) / l;

					if(GlobalMembersGAME.badguy(ref s) != 0 && (s.hitag &face_player_smart))
						sa = s.ang+(TRAND &31)-16;
				}

				if(p >= 0 && j >= 0)
				   l = j;
				else
					l = -1;

				j = GlobalMembersGAME.EGS(sect, sx+(sintable[(348+sa+512)&2047]/448), sy+(sintable[(sa+348)&2047]/448), sz-(1<<8), atwith, 0, 14, 14, sa, vel, zvel, i, 4);

				sprite[j].extra += (TRAND &7);
				if(atwith != FREEZEBLAST)
					sprite[j].yvel = l;
				else
				{
					sprite[j].yvel = GlobalMembersGLOBAL.numfreezebounces;
					sprite[j].xrepeat >>= 1;
					sprite[j].yrepeat >>= 1;
					sprite[j].zvel -= (2<<4);
				}

				if(p == -1)
				{
					if(PN == BOSS3)
					{
						if(TRAND &1)
						{
							sprite[j].x -= sintable[sa &2047]>>6;
							sprite[j].y -= sintable[(sa+1024+512)&2047]>>6;
							sprite[j].ang -= 8;
						}
						else
						{
							sprite[j].x += sintable[sa &2047]>>6;
							sprite[j].y += sintable[(sa+1024+512)&2047]>>6;
							sprite[j].ang += 4;
						}
						sprite[j].xrepeat = 42;
						sprite[j].yrepeat = 42;
					}
					else if(PN == BOSS2)
					{
						sprite[j].x -= sintable[sa &2047]/56;
						sprite[j].y -= sintable[(sa+1024+512)&2047]/56;
						sprite[j].ang -= 8+(TRAND &255)-128;
						sprite[j].xrepeat = 24;
						sprite[j].yrepeat = 24;
					}
					else if(atwith != FREEZEBLAST)
					{
						sprite[j].xrepeat = 30;
						sprite[j].yrepeat = 30;
						sprite[j].extra >>= 2;
					}
				}
				else if(GlobalMembersGLOBAL.ps[p].curr_weapon == DEVISTATOR_WEAPON)
				{
					sprite[j].extra >>= 2;
					sprite[j].ang += 16-(TRAND &31);
					sprite[j].zvel += 256-(TRAND &511);

					if(GlobalMembersGLOBAL.ps[p].hbomb_hold_delay)
					{
						sprite[j].x -= sintable[sa &2047]/644;
						sprite[j].y -= sintable[(sa+1024+512)&2047]/644;
					}
					else
					{
						sprite[j].x += sintable[sa &2047]>>8;
						sprite[j].y += sintable[(sa+1024+512)&2047]>>8;
					}
					sprite[j].xrepeat >>= 1;
					sprite[j].yrepeat >>= 1;
				}

				sprite[j].cstat = 128;
				if(atwith == RPG)
					sprite[j].clipdist = 4;
				else
					sprite[j].clipdist = 40;

				break;

			case HANDHOLDINGLASER:

				if(p >= 0)
					zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)*32;
				else
					zvel = 0;

				hitscan(sx, sy, sz-GlobalMembersGLOBAL.ps[p].pyoff, sect, sintable[(sa+512)&2047], sintable[sa &2047], zvel<<6, hitsect, hitwall, hitspr, hitx, hity, hitz, CLIPMASK1);

				j = 0;
				if(hitspr >= 0)
					break;

				if(hitwall >= 0 && hitsect >= 0)
					if(((hitx-sx)*(hitx-sx)+(hity-sy)*(hity-sy)) < (290 *290))
				{
					if(wall[hitwall].nextsector >= 0)
					{
						if(sector[wall[hitwall].nextsector].lotag <= 2 && sector[hitsect].lotag <= 2)
							j = 1;
					}
					else if(sector[hitsect].lotag <= 2)
						j = 1;
				}

				if(j == 1)
				{
					k = GlobalMembersGAME.EGS(hitsect, hitx, hity, hitz, TRIPBOMB, -16, 4, 5, sa, 0, 0, i, 6);

					sprite[k].hitag = k;
					GlobalMembersSOUNDS.spritesound(LASERTRIP_ONWALL, k);
					sprite[k].xvel = -20;
					GlobalMembersACTORS.ssp(k, CLIPMASK0);
					sprite[k].cstat = 16;
					GlobalMembersGLOBAL.hittype[k].temp_data[5] = sprite[k].ang = getangle(wall[hitwall].x-wall[wall[hitwall].point2].x,wall[hitwall].y-wall[wall[hitwall].point2].y)-512;

					if(p >= 0)
						GlobalMembersGLOBAL.ps[p].ammo_amount[TRIPBOMB_WEAPON]--;

				}
				return;

			case BOUNCEMINE:
			case MORTER:

				if(s.extra >= 0)
					s.shade = -96;

				j = GlobalMembersGLOBAL.ps[GlobalMembersSECTOR.findplayer(ref s, ref x)].i;
				x = GlobalMembersSECTOR.ldist(ref sprite[j], ref s);

				zvel = -x>>1;

				if(zvel < -4096)
					zvel = -2048;
				vel = x>>4;

				GlobalMembersGAME.EGS(sect, sx+(sintable[(512+sa+512)&2047]>>8), sy+(sintable[(sa+512)&2047]>>8), sz+(6<<8), atwith, -64, 32, 32, sa, vel, zvel, i, 1);
				break;

			case GROWSPARK:

				if(p >= 0)
				{
					j = GlobalMembersPLAYER.aim(ref s, AUTO_AIM_ANGLE);
					if(j >= 0)
					{
						dal = ((sprite[j].xrepeat *tilesizy[sprite[j].picnum])<<1)+(5<<8);
						switch(sprite[j].picnum)
						{
							case GREENSLIME:
							case GREENSLIME+1:
							case GREENSLIME+2:
							case GREENSLIME+3:
							case GREENSLIME+4:
							case GREENSLIME+5:
							case GREENSLIME+6:
							case GREENSLIME+7:
							case ROTATEGUN:
								dal -= (8<<8);
								break;
						}
						zvel = ((sprite[j].z-sz-dal)<<8) / (GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[p].i], ref sprite[j]));
						sa = getangle(sprite[j].x-sx,sprite[j].y-sy);
					}
					else
					{
						sa += 16-(TRAND &31);
						zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)<<5;
						zvel += 128-(TRAND &255);
					}

					sz -= (2<<8);
				}
				else
				{
					j = GlobalMembersSECTOR.findplayer(ref s, ref x);
					sz -= (4<<8);
					zvel = ((GlobalMembersGLOBAL.ps[j].posz-sz) <<8) / (GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[j].i], ref s));
					zvel += 128-(TRAND &255);
					sa += 32-(TRAND &63);
				}

				k = 0;

	//            RESHOOTGROW:

				s.cstat &= ~257;
				hitscan(sx, sy, sz, sect, sintable[(sa+512)&2047], sintable[sa &2047], zvel<<6, hitsect, hitwall, hitspr, hitx, hity, hitz, CLIPMASK1);

				s.cstat |= 257;

				j = GlobalMembersGAME.EGS(sect, hitx, hity, hitz, GROWSPARK, -16, 28, 28, sa, 0, 0, i, 1);

				sprite[j].pal = 2;
				sprite[j].cstat |= 130;
				sprite[j].xrepeat = sprite[j].yrepeat = 1;

				if(hitwall == -1 && hitspr == -1 && hitsect >= 0)
				{
					if(zvel < 0 && (sector[hitsect].ceilingstat &1) == 0)
						GlobalMembersSECTOR.checkhitceiling(hitsect);
				}
				else if(hitspr >= 0)
					GlobalMembersSECTOR.checkhitsprite(hitspr, j);
				else if(hitwall >= 0 && wall[hitwall].picnum != ACCESSSWITCH && wall[hitwall].picnum != ACCESSSWITCH2)
				{
				/*    if(wall[hitwall].overpicnum == MIRROR && k == 0)
				    {
				        l = getangle(
				            wall[wall[hitwall].point2].x-wall[hitwall].x,
				            wall[wall[hitwall].point2].y-wall[hitwall].y);
	
				        sx = hitx;
				        sy = hity;
				        sz = hitz;
				        sect = hitsect;
				        sa = ((l<<1) - sa)&2047;
				        sx += sintable[(sa+512)&2047]>>12;
				        sy += sintable[sa&2047]>>12;
	
				        k++;
				        goto RESHOOTGROW;
				    }
				    else */
						GlobalMembersSECTOR.checkhitwall(j, hitwall, hitx, hity, hitz, atwith);
				}

				break;
			case SHRINKER:
				if(s.extra >= 0)
					s.shade = -96;
				if(p >= 0)
				{
					j = GlobalMembersPLAYER.aim(ref s, AUTO_AIM_ANGLE);
					if(j >= 0)
					{
						dal = ((sprite[j].xrepeat *tilesizy[sprite[j].picnum])<<1);
						zvel = ((sprite[j].z-sz-dal-(4<<8))*768) / (GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[p].i], ref sprite[j]));
						sa = getangle(sprite[j].x-sx,sprite[j].y-sy);
					}
					else
						zvel = (100-GlobalMembersGLOBAL.ps[p].horiz-GlobalMembersGLOBAL.ps[p].horizoff)*98;
				}
				else if(s.statnum != 3)
				{
					j = GlobalMembersSECTOR.findplayer(ref s, ref x);
					l = GlobalMembersSECTOR.ldist(ref sprite[GlobalMembersGLOBAL.ps[j].i], ref s);
					zvel = ((GlobalMembersGLOBAL.ps[j].oposz-sz)*512) / l;
				}
				else
					zvel = 0;

				j = GlobalMembersGAME.EGS(sect, sx+(sintable[(512+sa+512)&2047]>>12), sy+(sintable[(sa+512)&2047]>>12), sz+(2<<8), SHRINKSPARK, -16, 28, 28, sa, 768, zvel, i, 4);

				sprite[j].cstat = 128;
				sprite[j].clipdist = 32;


				return;
		}
		return;
	}



	public static void displayloogie(short snum)
	{
		int i;
		int a;
		int x;
		int y;
		int z;

		if(GlobalMembersGLOBAL.ps[snum].loogcnt == 0)
			return;

		y = (GlobalMembersGLOBAL.ps[snum].loogcnt<<2);
		for(i = 0;i<GlobalMembersGLOBAL.ps[snum].numloogs;i++)
		{
			a = klabs(sintable[((GlobalMembersGLOBAL.ps[snum].loogcnt+i)<<5)&2047])>>5;
			z = 4096+((GlobalMembersGLOBAL.ps[snum].loogcnt+i)<<9);
			x = (-GlobalMembersGLOBAL.sync[snum].avel)+(sintable[((GlobalMembersGLOBAL.ps[snum].loogcnt+i)<<6)&2047]>>10);

			rotatesprite((GlobalMembersGLOBAL.ps[snum].loogiex[i]+x)<<16,(200+GlobalMembersGLOBAL.ps[snum].loogiey[i]-y)<<16,z-(i<<8),256-a, LOOGIE,0,0,2,0,0,xdim-1,ydim-1);
		}
	}

	public static sbyte animatefist(short gs,short snum)
	{
		short looking_arc;
		short fisti;
		short fistpal;
		int fistzoom;
		int fistz;

		fisti = GlobalMembersGLOBAL.ps[snum].fist_incs;
		if(fisti > 32)
			fisti = 32;
		if(fisti <= 0)
			return 0;

		looking_arc = klabs(GlobalMembersGLOBAL.ps[snum].look_ang)/9;

		fistzoom = 65536 - (sintable[(512+(fisti<<6))&2047]<<2);
		if(fistzoom > 90612)
			fistzoom = 90612;
		if(fistzoom < 40920)
			fistzoom = 40290;
		fistz = 194 + (sintable[((6+fisti)<<7)&2047]>>9);

		if(sprite[GlobalMembersGLOBAL.ps[snum].i].pal == 1)
			fistpal = 1;
		else
			fistpal = sector[GlobalMembersGLOBAL.ps[snum].cursectnum].floorpal;

		rotatesprite((-fisti+222+(GlobalMembersGLOBAL.sync[snum].avel>>4))<<16, (looking_arc+fistz)<<16, fistzoom,0,FIST,gs,fistpal,2,0,0,xdim-1,ydim-1);

		return 1;
	}

	public static sbyte animateknee(short gs,short snum)
	{
		short[] knee_y = {0,-8,-16,-32,-64,-84,-108,-108,-108,-72,-32,-8};
		short looking_arc;
		short pal;

		if(GlobalMembersGLOBAL.ps[snum].knee_incs > 11 || GlobalMembersGLOBAL.ps[snum].knee_incs == 0 || sprite[GlobalMembersGLOBAL.ps[snum].i].extra <= 0)
			return 0;

		looking_arc = knee_y[GlobalMembersGLOBAL.ps[snum].knee_incs] + klabs(GlobalMembersGLOBAL.ps[snum].look_ang)/9;

		looking_arc -= (GlobalMembersGLOBAL.ps[snum].hard_landing<<3);

		if(sprite[GlobalMembersGLOBAL.ps[snum].i].pal == 1)
			pal = 1;
		else
		{
			pal = sector[GlobalMembersGLOBAL.ps[snum].cursectnum].floorpal;
			if(pal == 0)
				pal = GlobalMembersGLOBAL.ps[snum].palookup;
		}

		GlobalMembersGAME.myospal(105+(GlobalMembersGLOBAL.sync[snum].avel>>4)-(GlobalMembersGLOBAL.ps[snum].look_ang>>1)+(knee_y[GlobalMembersGLOBAL.ps[snum].knee_incs]>>2), looking_arc+280-((GlobalMembersGLOBAL.ps[snum].horiz-GlobalMembersGLOBAL.ps[snum].horizoff)>>4), KNEE, gs, 4, pal);

		return 1;
	}

	public static sbyte animateknuckles(short gs,short snum)
	{
		short[] knuckle_frames = {0,1,2,2,3,3,3,2,2,1,0};
		short looking_arc;
		short pal;

		if(GlobalMembersGLOBAL.ps[snum].knuckle_incs == 0 || sprite[GlobalMembersGLOBAL.ps[snum].i].extra <= 0)
			return 0;

		looking_arc = klabs(GlobalMembersGLOBAL.ps[snum].look_ang)/9;

		looking_arc -= (GlobalMembersGLOBAL.ps[snum].hard_landing<<3);

		if(sprite[GlobalMembersGLOBAL.ps[snum].i].pal == 1)
			pal = 1;
		else
			pal = sector[GlobalMembersGLOBAL.ps[snum].cursectnum].floorpal;

		GlobalMembersGAME.myospal(160+(GlobalMembersGLOBAL.sync[snum].avel>>4)-(GlobalMembersGLOBAL.ps[snum].look_ang>>1), looking_arc+180-((GlobalMembersGLOBAL.ps[snum].horiz-GlobalMembersGLOBAL.ps[snum].horizoff)>>4), CRACKKNUCKLES+knuckle_frames[GlobalMembersGLOBAL.ps[snum].knuckle_incs>>1], gs, 4, pal);

		return 1;
	}



	public static int lastvisinc;

	public static void displaymasks(short snum)
	{
		short i;
		short p;

		if(sprite[GlobalMembersGLOBAL.ps[snum].i].pal == 1)
			p = 1;
		else
			p = sector[GlobalMembersGLOBAL.ps[snum].cursectnum].floorpal;

		 if(GlobalMembersGLOBAL.ps[snum].scuba_on)
		 {
			if(GlobalMembersGLOBAL.ud.screen_size > 4)
			{
				rotatesprite(43<<16,(200-8-(tilesizy[SCUBAMASK])<<16),65536,0,SCUBAMASK,0,p,2+16,windowx1,windowy1,windowx2,windowy2);
				rotatesprite((320-43)<<16,(200-8-(tilesizy[SCUBAMASK])<<16),65536,1024,SCUBAMASK,0,p,2+4+16,windowx1,windowy1,windowx2,windowy2);
			}
			else
			{
				rotatesprite(43<<16,(200-(tilesizy[SCUBAMASK])<<16),65536,0,SCUBAMASK,0,p,2+16,windowx1,windowy1,windowx2,windowy2);
				rotatesprite((320-43)<<16,(200-(tilesizy[SCUBAMASK])<<16),65536,1024,SCUBAMASK,0,p,2+4+16,windowx1,windowy1,windowx2,windowy2);
			}
		 }
	}

	public static sbyte animatetip(short gs,short snum)
	{
		short p;
		short looking_arc;
		short[] tip_y = {0,-8,-16,-32,-64,-84,-108,-108,-108,-108,-108,-108,-108,-108,-108,-108,-96,-72,-64,-32,-16};

		if(GlobalMembersGLOBAL.ps[snum].tipincs == 0)
			return 0;

		looking_arc = klabs(GlobalMembersGLOBAL.ps[snum].look_ang)/9;
		looking_arc -= (GlobalMembersGLOBAL.ps[snum].hard_landing<<3);

		if(sprite[GlobalMembersGLOBAL.ps[snum].i].pal == 1)
			p = 1;
		else
			p = sector[GlobalMembersGLOBAL.ps[snum].cursectnum].floorpal;

	/*    if(ps[snum].access_spritenum >= 0)
	        p = sprite[ps[snum].access_spritenum].pal;
	    else
	        p = wall[ps[snum].access_wallnum].pal;
	  */
		GlobalMembersGAME.myospal(170+(GlobalMembersGLOBAL.sync[snum].avel>>4)-(GlobalMembersGLOBAL.ps[snum].look_ang>>1), (tip_y[GlobalMembersGLOBAL.ps[snum].tipincs]>>1)+looking_arc+240-((GlobalMembersGLOBAL.ps[snum].horiz-GlobalMembersGLOBAL.ps[snum].horizoff)>>4), TIP+((26-GlobalMembersGLOBAL.ps[snum].tipincs)>>4), gs, 0, p);

		return 1;
	}

	public static sbyte animateaccess(short gs,short snum)
	{
		short[] access_y = {0,-8,-16,-32,-64,-84,-108,-108,-108,-108,-108,-108,-108,-108,-108,-108,-96,-72,-64,-32,-16};
		short looking_arc;
		sbyte p;

		if(GlobalMembersGLOBAL.ps[snum].access_incs == 0 || sprite[GlobalMembersGLOBAL.ps[snum].i].extra <= 0)
			return 0;

		looking_arc = access_y[GlobalMembersGLOBAL.ps[snum].access_incs] + klabs(GlobalMembersGLOBAL.ps[snum].look_ang)/9;
		looking_arc -= (GlobalMembersGLOBAL.ps[snum].hard_landing<<3);

		if(GlobalMembersGLOBAL.ps[snum].access_spritenum >= 0)
			p = sprite[GlobalMembersGLOBAL.ps[snum].access_spritenum].pal;
		else
			p = 0;
	//    else
	//        p = wall[ps[snum].access_wallnum].pal;

		if((GlobalMembersGLOBAL.ps[snum].access_incs-3) > 0 && (GlobalMembersGLOBAL.ps[snum].access_incs-3)>>3)
			GlobalMembersGAME.myospal(170+(GlobalMembersGLOBAL.sync[snum].avel>>4)-(GlobalMembersGLOBAL.ps[snum].look_ang>>1)+(access_y[GlobalMembersGLOBAL.ps[snum].access_incs]>>2), looking_arc+266-((GlobalMembersGLOBAL.ps[snum].horiz-GlobalMembersGLOBAL.ps[snum].horizoff)>>4), HANDHOLDINGLASER+(GlobalMembersGLOBAL.ps[snum].access_incs>>3), gs, 0, p);
		else
			GlobalMembersGAME.myospal(170+(GlobalMembersGLOBAL.sync[snum].avel>>4)-(GlobalMembersGLOBAL.ps[snum].look_ang>>1)+(access_y[GlobalMembersGLOBAL.ps[snum].access_incs]>>2), looking_arc+266-((GlobalMembersGLOBAL.ps[snum].horiz-GlobalMembersGLOBAL.ps[snum].horizoff)>>4), HANDHOLDINGACCESS, gs, 4, p);

		return 1;
	}

	public static short fistsign;

	public static void displayweapon(short snum)
	{
		int gun_pos;
		int looking_arc;
		int cw;
		int weapon_xoffset;
		int i;
		int j;
		int x1;
		int y1;
		int x2;
		sbyte o;
		sbyte pal;
		sbyte gs;
		player_struct p;
		short kb;

		p = GlobalMembersGLOBAL.ps[snum];
		kb = p.kickback_pic;

		o = 0;

		looking_arc = klabs(p.look_ang)/9;

		gs = sprite[p.i].shade;
		if(gs > 24)
			gs = 24;

		if(p.newowner >= 0 || GlobalMembersGLOBAL.ud.camerasprite >= 0 || p.over_shoulder_on > 0 || (sprite[p.i].pal != 1 && sprite[p.i].extra <= 0) || GlobalMembersPLAYER.animatefist(gs, snum) || GlobalMembersPLAYER.animateknuckles(gs, snum) || GlobalMembersPLAYER.animatetip(gs, snum) || GlobalMembersPLAYER.animateaccess(gs, snum) != 0)
			return;

		GlobalMembersPLAYER.animateknee(gs, snum);

		gun_pos = 80-(p.weapon_pos *p.weapon_pos);

		weapon_xoffset = (160)-90;
		weapon_xoffset -= (sintable[((p.weapon_sway>>1)+512)&2047]/(1024+512));
		weapon_xoffset -= 58 + p.weapon_ang;
		if(sprite[p.i].xrepeat < 32)
			gun_pos -= klabs(sintable[(p.weapon_sway<<2)&2047]>>9);
		else
			gun_pos -= klabs(sintable[(p.weapon_sway>>1)&2047]>>10);

		gun_pos -= (p.hard_landing<<3);

		if(p.last_weapon >= 0)
			cw = p.last_weapon;
		else
			cw = p.curr_weapon;

		j = 14-p.quick_kick;
		if(j != 14)
		{
			if(sprite[p.i].pal == 1)
				pal = 1;
			else
			{
				pal = sector[p.cursectnum].floorpal;
				if(pal == 0)
					pal = p.palookup;
			}


			if(j < 5 || j > 9)
				GlobalMembersGAME.myospal(weapon_xoffset+80-(p.look_ang>>1), looking_arc+250-gun_pos, KNEE, gs, o|4, pal);
			else
				GlobalMembersGAME.myospal(weapon_xoffset+160-16-(p.look_ang>>1), looking_arc+214-gun_pos, KNEE+1, gs, o|4, pal);
		}

		if(sprite[p.i].xrepeat < 40)
		{
			if(p.jetpack_on == 0)
			{
				i = sprite[p.i].xvel;
				looking_arc += 32-(i>>1);
				fistsign += i>>1;
			}
			cw = weapon_xoffset;
			weapon_xoffset += sintable[(fistsign)&2047]>>10;
			GlobalMembersGAME.myos(weapon_xoffset+250-(p.look_ang>>1), looking_arc+258-(klabs(sintable[(fistsign)&2047]>>8)), FIST, gs, o);
			weapon_xoffset = cw;
			weapon_xoffset -= sintable[(fistsign)&2047]>>10;
			GlobalMembersGAME.myos(weapon_xoffset+40-(p.look_ang>>1), looking_arc+200+(klabs(sintable[(fistsign)&2047]>>8)), FIST, gs, o|4);
		}
		else
			switch(cw)
		{
			case KNEE_WEAPON:
				if((kb) > 0)
				{
					if(sprite[p.i].pal == 1)
						pal = 1;
					else
					{
						pal = sector[p.cursectnum].floorpal;
						if(pal == 0)
							pal = p.palookup;
					}

					if((kb) < 5 || (kb) > 9)
						GlobalMembersGAME.myospal(weapon_xoffset+220-(p.look_ang>>1), looking_arc+250-gun_pos, KNEE, gs, o, pal);
					else
						GlobalMembersGAME.myospal(weapon_xoffset+160-(p.look_ang>>1), looking_arc+214-gun_pos, KNEE+1, gs, o, pal);
				}
				break;

			case TRIPBOMB_WEAPON:
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				weapon_xoffset += 8;
				gun_pos -= 10;

				if((kb) > 6)
					looking_arc += ((kb)<<3);
				else if((kb) < 4)
					GlobalMembersGAME.myospal(weapon_xoffset+142-(p.look_ang>>1), looking_arc+234-gun_pos, HANDHOLDINGLASER+3, gs, o, pal);

				GlobalMembersGAME.myospal(weapon_xoffset+130-(p.look_ang>>1), looking_arc+249-gun_pos, HANDHOLDINGLASER+((kb)>>2), gs, o, pal);
				GlobalMembersGAME.myospal(weapon_xoffset+152-(p.look_ang>>1), looking_arc+249-gun_pos, HANDHOLDINGLASER+((kb)>>2), gs, o|4, pal);

				break;

			case RPG_WEAPON:
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				weapon_xoffset -= sintable[(768+((kb)<<7))&2047]>>11;
				gun_pos += sintable[(768+((kb)<<7)&2047)]>>11;

				if(kb > 0)
				{
					if(kb < 8)
					{
						GlobalMembersGAME.myospal(weapon_xoffset+164, (looking_arc<<1)+176-gun_pos, RPGGUN+((kb)>>1), gs, o, pal);
					}
				}

				GlobalMembersGAME.myospal(weapon_xoffset+164, (looking_arc<<1)+176-gun_pos, RPGGUN, gs, o, pal);

				break;

			case SHOTGUN_WEAPON:
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				weapon_xoffset -= 8;

				switch(kb)
				{
					case 1:
					case 2:
						GlobalMembersGAME.myospal(weapon_xoffset+168-(p.look_ang>>1), looking_arc+201-gun_pos, SHOTGUN+2, -128, o, pal);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
					case 0:
					case 6:
					case 7:
					case 8:
						GlobalMembersGAME.myospal(weapon_xoffset+146-(p.look_ang>>1), looking_arc+202-gun_pos, SHOTGUN, gs, o, pal);
						break;
					case 3:
					case 4:
					case 5:
					case 9:
					case 10:
					case 11:
					case 12:
						if(kb > 1 && kb < 5)
						{
							gun_pos -= 40;
							weapon_xoffset += 20;

							GlobalMembersGAME.myospal(weapon_xoffset+178-(p.look_ang>>1), looking_arc+194-gun_pos, SHOTGUN+1+((*(kb)-1)>>1), -128, o, pal);
						}

						GlobalMembersGAME.myospal(weapon_xoffset+158-(p.look_ang>>1), looking_arc+220-gun_pos, SHOTGUN+3, gs, o, pal);

						break;
					case 13:
					case 14:
					case 15:
						GlobalMembersGAME.myospal(32+weapon_xoffset+166-(p.look_ang>>1), looking_arc+210-gun_pos, SHOTGUN+4, gs, o, pal);
						break;
					case 16:
					case 17:
					case 18:
					case 19:
						GlobalMembersGAME.myospal(64+weapon_xoffset+170-(p.look_ang>>1), looking_arc+196-gun_pos, SHOTGUN+5, gs, o, pal);
						break;
					case 20:
					case 21:
					case 22:
					case 23:
						GlobalMembersGAME.myospal(64+weapon_xoffset+176-(p.look_ang>>1), looking_arc+196-gun_pos, SHOTGUN+6, gs, o, pal);
						break;
					case 24:
					case 25:
					case 26:
					case 27:
						GlobalMembersGAME.myospal(64+weapon_xoffset+170-(p.look_ang>>1), looking_arc+196-gun_pos, SHOTGUN+5, gs, o, pal);
						break;
					case 28:
					case 29:
					case 30:
						GlobalMembersGAME.myospal(32+weapon_xoffset+156-(p.look_ang>>1), looking_arc+206-gun_pos, SHOTGUN+4, gs, o, pal);
						break;
				}
				break;



			case CHAINGUN_WEAPON:
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				if(kb > 0)
					gun_pos -= sintable[(kb)<<7]>>12;

				if(kb > 0 && sprite[p.i].pal != 1)
					weapon_xoffset += 1-(RandomNumbers.NextNumber()&3);

				GlobalMembersGAME.myospal(weapon_xoffset+168-(p.look_ang>>1), looking_arc+260-gun_pos, CHAINGUN, gs, o, pal);
				switch(kb)
				{
					case 0:
						GlobalMembersGAME.myospal(weapon_xoffset+178-(p.look_ang>>1), looking_arc+233-gun_pos, CHAINGUN+1, gs, o, pal);
						break;
					default:
						if(kb > 4 && kb < 12)
						{
							i = 0;
							if(sprite[p.i].pal != 1)
								i = RandomNumbers.NextNumber()&7;
							GlobalMembersGAME.myospal(i+weapon_xoffset-4+140-(p.look_ang>>1), i+looking_arc-((kb)>>1)+208-gun_pos, CHAINGUN+5+((kb-4)/5), gs, o, pal);
							if(sprite[p.i].pal != 1)
								i = RandomNumbers.NextNumber()&7;
							GlobalMembersGAME.myospal(i+weapon_xoffset-4+184-(p.look_ang>>1), i+looking_arc-((kb)>>1)+208-gun_pos, CHAINGUN+5+((kb-4)/5), gs, o, pal);
						}
						if(kb < 8)
						{
							i = RandomNumbers.NextNumber()&7;
							GlobalMembersGAME.myospal(i+weapon_xoffset-4+162-(p.look_ang>>1), i+looking_arc-((kb)>>1)+208-gun_pos, CHAINGUN+5+((kb-2)/5), gs, o, pal);
							GlobalMembersGAME.myospal(weapon_xoffset+178-(p.look_ang>>1), looking_arc+233-gun_pos, CHAINGUN+1+((kb)>>1), gs, o, pal);
						}
						else
							GlobalMembersGAME.myospal(weapon_xoffset+178-(p.look_ang>>1), looking_arc+233-gun_pos, CHAINGUN+1, gs, o, pal);
						break;
				}
				break;
			 case PISTOL_WEAPON:
				 if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				if((kb) < 5)
				{
					short[] kb_frames = {0,1,2,0,0};
					short l;

					l = 195-12+weapon_xoffset;

					if((kb) == 2)
						l -= 3;
					GlobalMembersGAME.myospal((l-(p.look_ang>>1)), (looking_arc+244-gun_pos), FIRSTGUN+kb_frames[kb], gs, 2, pal);
				}
				else
				{
					if((kb) < 10)
						GlobalMembersGAME.myospal(194-(p.look_ang>>1), looking_arc+230-gun_pos, FIRSTGUN+4, gs, o, pal);
					else if((kb) < 15)
					{
						GlobalMembersGAME.myospal(244-((kb)<<3)-(p.look_ang>>1), looking_arc+130-gun_pos+((kb)<<4), FIRSTGUN+6, gs, o, pal);
						GlobalMembersGAME.myospal(224-(p.look_ang>>1), looking_arc+220-gun_pos, FIRSTGUN+5, gs, o, pal);
					}
					else if((kb) < 20)
					{
						GlobalMembersGAME.myospal(124+((kb)<<1)-(p.look_ang>>1), looking_arc+430-gun_pos-((kb)<<3), FIRSTGUN+6, gs, o, pal);
						GlobalMembersGAME.myospal(224-(p.look_ang>>1), looking_arc+220-gun_pos, FIRSTGUN+5, gs, o, pal);
					}
					else if((kb) < 23)
					{
						GlobalMembersGAME.myospal(184-(p.look_ang>>1), looking_arc+235-gun_pos, FIRSTGUN+8, gs, o, pal);
						GlobalMembersGAME.myospal(224-(p.look_ang>>1), looking_arc+210-gun_pos, FIRSTGUN+5, gs, o, pal);
					}
					else if((kb) < 25)
					{
						GlobalMembersGAME.myospal(164-(p.look_ang>>1), looking_arc+245-gun_pos, FIRSTGUN+8, gs, o, pal);
						GlobalMembersGAME.myospal(224-(p.look_ang>>1), looking_arc+220-gun_pos, FIRSTGUN+5, gs, o, pal);
					}
					else if((kb) < 27)
						GlobalMembersGAME.myospal(194-(p.look_ang>>1), looking_arc+235-gun_pos, FIRSTGUN+5, gs, o, pal);
				}

				break;
			case HANDBOMB_WEAPON:
			{
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				if((kb))
				{
					string throw_frames = {0,0,0,0,0,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2};

					if((kb) < 7)
						gun_pos -= 10*(kb); //D
					else if((kb) < 12)
						gun_pos += 20*((kb)-10); //U
					else if((kb) < 20)
						gun_pos -= 9*((kb)-14); //D

					GlobalMembersGAME.myospal(weapon_xoffset+190-(p.look_ang>>1), looking_arc+250-gun_pos, HANDTHROW+throw_frames[(kb)], gs, o, pal);
				}
				else
					GlobalMembersGAME.myospal(weapon_xoffset+190-(p.look_ang>>1), looking_arc+260-gun_pos, HANDTHROW, gs, o, pal);
			}
			break;

			case HANDREMOTE_WEAPON:
			{
					string remote_frames = {0,1,1,2,1,1,0,0,0,0,0};
					if(sprite[p.i].pal == 1)
						pal = 1;
					else
						pal = sector[p.cursectnum].floorpal;

					weapon_xoffset = -48;

					if((kb))
						GlobalMembersGAME.myospal(weapon_xoffset+150-(p.look_ang>>1), looking_arc+258-gun_pos, HANDREMOTE+remote_frames[(kb)], gs, o, pal);
					else
						GlobalMembersGAME.myospal(weapon_xoffset+150-(p.look_ang>>1), looking_arc+258-gun_pos, HANDREMOTE, gs, o, pal);
				}
				break;
			case DEVISTATOR_WEAPON:
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				if((kb))
				{
					string cycloidy = {0,4,12,24,12,4,0};

					i = sgn((kb)>>2);

					if(p.hbomb_hold_delay)
					{
						GlobalMembersGAME.myospal((cycloidy[kb]>>1)+weapon_xoffset+268-(p.look_ang>>1), cycloidy[kb]+looking_arc+238-gun_pos, DEVISTATOR+i, -32, o, pal);
						GlobalMembersGAME.myospal(weapon_xoffset+30-(p.look_ang>>1), looking_arc+240-gun_pos, DEVISTATOR, gs, o|4, pal);
					}
					else
					{
						GlobalMembersGAME.myospal(-(cycloidy[kb]>>1)+weapon_xoffset+30-(p.look_ang>>1), cycloidy[kb]+looking_arc+240-gun_pos, DEVISTATOR+i, -32, o|4, pal);
						GlobalMembersGAME.myospal(weapon_xoffset+268-(p.look_ang>>1), looking_arc+238-gun_pos, DEVISTATOR, gs, o, pal);
					}
				}
				else
				{
					GlobalMembersGAME.myospal(weapon_xoffset+268-(p.look_ang>>1), looking_arc+238-gun_pos, DEVISTATOR, gs, o, pal);
					GlobalMembersGAME.myospal(weapon_xoffset+30-(p.look_ang>>1), looking_arc+240-gun_pos, DEVISTATOR, gs, o|4, pal);
				}
				break;

			case FREEZE_WEAPON:
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;

				if((kb))
				{
					string cat_frames = { 0,0,1,1,2,2 };

					if(sprite[p.i].pal != 1)
					{
						weapon_xoffset += RandomNumbers.NextNumber()&3;
						looking_arc += RandomNumbers.NextNumber()&3;
					}
					gun_pos -= 16;
					GlobalMembersGAME.myospal(weapon_xoffset+210-(p.look_ang>>1), looking_arc+261-gun_pos, FREEZE+2, -32, o, pal);
					GlobalMembersGAME.myospal(weapon_xoffset+210-(p.look_ang>>1), looking_arc+235-gun_pos, FREEZE+3+cat_frames[kb%6], -32, o, pal);
				}
				else
					GlobalMembersGAME.myospal(weapon_xoffset+210-(p.look_ang>>1), looking_arc+261-gun_pos, FREEZE, gs, o, pal);

				break;

			case SHRINKER_WEAPON:
			case GROW_WEAPON:
				weapon_xoffset += 28;
				looking_arc += 18;
				if(sprite[p.i].pal == 1)
					pal = 1;
				else
					pal = sector[p.cursectnum].floorpal;
				if((kb) == 0)
				{
					if(cw == GROW_WEAPON)
					{
						 GlobalMembersGAME.myospal(weapon_xoffset+184-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER+2, 16-(sintable[p.random_club_frame &2047]>>10), o, 2);

						 GlobalMembersGAME.myospal(weapon_xoffset+188-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER-2, gs, o, pal);
					}
					else
					{
						GlobalMembersGAME.myospal(weapon_xoffset+184-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER+2, 16-(sintable[p.random_club_frame &2047]>>10), o, 0);

						GlobalMembersGAME.myospal(weapon_xoffset+188-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER, gs, o, pal);
					}
				}
				else
				{
					if(sprite[p.i].pal != 1)
					{
						weapon_xoffset += RandomNumbers.NextNumber()&3;
						gun_pos += (RandomNumbers.NextNumber()&3);
					}

					if(cw == GROW_WEAPON)
					{
						GlobalMembersGAME.myospal(weapon_xoffset+184-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER+3+((kb)&3), -32, o, 2);

						GlobalMembersGAME.myospal(weapon_xoffset+188-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER-1, gs, o, pal);

					}
					else
					{
						GlobalMembersGAME.myospal(weapon_xoffset+184-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER+3+((kb)&3), -32, o, 0);

						 GlobalMembersGAME.myospal(weapon_xoffset+188-(p.look_ang>>1), looking_arc+240-gun_pos, SHRINKER+1, gs, o, pal);
					}
				}
				break;
		}

		GlobalMembersPLAYER.displayloogie(snum);

	}

	#define TURBOTURNTIME
	#define NORMALTURN
	#define PREAMBLETURN
	#define NORMALKEYMOVE
	#define MAXVEL
	#define MAXSVEL
	#define MAXANGVEL
	#define MAXHORIZ

	public static int myaimmode = 0;
	public static int myaimstat = 0;
	public static int omyaimstat = 0;

	public static void getinput(short snum)
	{

		short j;
		short daang;
	// MED
		ControlInfo info = new ControlInfo();
		int32 tics;
		boolean running = new boolean();
		int32 turnamount;
		int32 keymove;
		int32 momx;
		int32 momy;
		player_struct p;

		momx = momy = 0;
		p = GlobalMembersGLOBAL.ps[snum];

		CONTROL_GetInput(info);

		if((p.gm &MODE_MENU) || (p.gm &MODE_TYPE) || (GlobalMembersGLOBAL.ud.pause_on && !KB_KeyPressed(sc_Pause)))
		{
			 GlobalMembersGLOBAL.loc.fvel = GlobalMembersGLOBAL.vel = 0;
			 GlobalMembersGLOBAL.loc.svel = GlobalMembersGLOBAL.svel = 0;
			 GlobalMembersGLOBAL.loc.avel = GlobalMembersGLOBAL.angvel = 0;
			 GlobalMembersGLOBAL.loc.horz = GlobalMembersGLOBAL.horiz = 0;
			 GlobalMembersGLOBAL.loc.bits = (((int)GlobalMembersGLOBAL.gamequit)<<26);
			 info.dz = info.dyaw = 0;
			 return;
		}

		tics = totalclock-lastcontroltime;
		lastcontroltime = totalclock;

		if (GlobalMembersCONFIG.MouseAiming != 0)
			  myaimmode = BUTTON(gamefunc_Mouse_Aiming);
		 else
		 {
			  omyaimstat = myaimstat;
			  myaimstat = BUTTON(gamefunc_Mouse_Aiming);
			  if (myaimstat > omyaimstat)
			  {
					myaimmode ^= 1;
					GlobalMembersGAME.FTA(44+myaimmode, ref p);
			  }
		 }

		if(GlobalMembersGLOBAL.multiflag == 1)
		{
			GlobalMembersGLOBAL.loc.bits = 1<<17;
			GlobalMembersGLOBAL.loc.bits |= GlobalMembersGLOBAL.multiwhat<<18;
			GlobalMembersGLOBAL.loc.bits |= GlobalMembersGLOBAL.multipos<<19;
			GlobalMembersGLOBAL.multiflag = 0;
			return;
		}

		GlobalMembersGLOBAL.loc.bits = BUTTON(gamefunc_Jump);
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Crouch)<<1;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Fire)<<2;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Aim_Up)<<3;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Aim_Down)<<4;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Run)<<5;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Look_Left)<<6;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Look_Right)<<7;

		j = 0;
		if (BUTTON(gamefunc_Weapon_1))
		   j = 1;
		if (BUTTON(gamefunc_Weapon_2))
		   j = 2;
		if (BUTTON(gamefunc_Weapon_3))
		   j = 3;
		if (BUTTON(gamefunc_Weapon_4))
		   j = 4;
		if (BUTTON(gamefunc_Weapon_5))
		   j = 5;
		if (BUTTON(gamefunc_Weapon_6))
		   j = 6;

		if (BUTTON(gamefunc_Previous_Weapon))
			j = 11;
		if (BUTTON(gamefunc_Next_Weapon))
			j = 12;

		#if ! VOLUMEONE
		if (BUTTON(gamefunc_Weapon_7))
			j = 7;
		if (BUTTON(gamefunc_Weapon_8))
		   j = 8;
		if (BUTTON(gamefunc_Weapon_9))
		   j = 9;
		if (BUTTON(gamefunc_Weapon_10))
		   j = 10;
		#endif

		GlobalMembersGLOBAL.loc.bits |= j<<8;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Steroids)<<12;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Look_Up)<<13;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Look_Down)<<14;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_NightVision)<<15;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_MedKit)<<16;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Center_View)<<18;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Holster_Weapon)<<19;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Inventory_Left)<<20;
		GlobalMembersGLOBAL.loc.bits |= KB_KeyPressed(sc_Pause)<<21;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Quick_Kick)<<22;
		GlobalMembersGLOBAL.loc.bits |= myaimmode<<23;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Holo_Duke)<<24;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Jetpack)<<25;
		GlobalMembersGLOBAL.loc.bits |= (((int)GlobalMembersGLOBAL.gamequit)<<26);
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Inventory_Right)<<27;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_TurnAround)<<28;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Open)<<29;
		GlobalMembersGLOBAL.loc.bits |= BUTTON(gamefunc_Inventory)<<30;
		GlobalMembersGLOBAL.loc.bits |= KB_KeyPressed(sc_Escape)<<31;

		running = BUTTON(gamefunc_Run)|GlobalMembersGLOBAL.ud.auto_run;
		GlobalMembersGLOBAL.svel = GlobalMembersGLOBAL.vel = GlobalMembersGLOBAL.angvel = GlobalMembersGLOBAL.horiz = 0;

		if(CONTROL_JoystickEnabled)
			if (running != null)
				info.dz *= 2;

		if(BUTTON(gamefunc_Strafe))
		   GlobalMembersGLOBAL.svel = -info.dyaw/8;
		else
			GlobalMembersGLOBAL.angvel = info.dyaw/64;

		if(myaimmode)
		{
			if(GlobalMembersGLOBAL.ud.mouseflip)
				GlobalMembersGLOBAL.horiz -= info.dz/(314-128);
			else
				GlobalMembersGLOBAL.horiz += info.dz/(314-128);

			info.dz = 0;
		}

		GlobalMembersGLOBAL.svel -= info.dx;
		GlobalMembersGLOBAL.vel = -info.dz>>6;

		if (running != null)
		{
			turnamount = DefineConstants.NORMALTURN<<1;
			keymove = DefineConstants.NORMALKEYMOVE<<1;
		}
		else
		{
			turnamount = DefineConstants.NORMALTURN;
			keymove = DefineConstants.NORMALKEYMOVE;
		}

		if (BUTTON(gamefunc_Strafe))
		{
			if (BUTTON(gamefunc_Turn_Left))
			   {
			   GlobalMembersGLOBAL.svel -= -keymove;
			   }
			if (BUTTON(gamefunc_Turn_Right))
			   {
			   GlobalMembersGLOBAL.svel -= keymove;
			   }
		}
		else
		{
			if (BUTTON(gamefunc_Turn_Left))
			   {
			   turnheldtime += tics;
			   if (turnheldtime>=TICRATE/8)
				  {
				  GlobalMembersGLOBAL.angvel -= turnamount;
				  }
			   else
				  {
				  GlobalMembersGLOBAL.angvel -= DefineConstants.PREAMBLETURN;
				  }
			   }
			else if (BUTTON(gamefunc_Turn_Right))
			   {
			   turnheldtime += tics;
			   if (turnheldtime>=TICRATE/8)
				  {
				  GlobalMembersGLOBAL.angvel += turnamount;
				  }
			   else
				  {
				  GlobalMembersGLOBAL.angvel += DefineConstants.PREAMBLETURN;
				  }
			   }
			else
			   {
			   turnheldtime=0;
			   }
		}

		if (BUTTON(gamefunc_Strafe_Left))
			GlobalMembersGLOBAL.svel += keymove;

		if (BUTTON(gamefunc_Strafe_Right))
			GlobalMembersGLOBAL.svel += -keymove;

		if (BUTTON(gamefunc_Move_Forward))
			GlobalMembersGLOBAL.vel += keymove;

		if (BUTTON(gamefunc_Move_Backward))
			GlobalMembersGLOBAL.vel += -keymove;

		if(GlobalMembersGLOBAL.vel < -(DefineConstants.NORMALKEYMOVE *2)+10)
			GlobalMembersGLOBAL.vel = -(DefineConstants.NORMALKEYMOVE *2)+10;
		if(GlobalMembersGLOBAL.vel > (DefineConstants.NORMALKEYMOVE *2)+10)
			GlobalMembersGLOBAL.vel = (DefineConstants.NORMALKEYMOVE *2)+10;
		if(GlobalMembersGLOBAL.svel < -(DefineConstants.NORMALKEYMOVE *2)+10)
			GlobalMembersGLOBAL.svel = -(DefineConstants.NORMALKEYMOVE *2)+10;
		if(GlobalMembersGLOBAL.svel > (DefineConstants.NORMALKEYMOVE *2)+10)
			GlobalMembersGLOBAL.svel = (DefineConstants.NORMALKEYMOVE *2)+10;
		if(GlobalMembersGLOBAL.angvel < -DefineConstants.MAXANGVEL)
			GlobalMembersGLOBAL.angvel = -DefineConstants.MAXANGVEL;
		if(GlobalMembersGLOBAL.angvel > DefineConstants.MAXANGVEL)
			GlobalMembersGLOBAL.angvel = DefineConstants.MAXANGVEL;
		if(GlobalMembersGLOBAL.horiz < -DefineConstants.MAXHORIZ)
			GlobalMembersGLOBAL.horiz = -DefineConstants.MAXHORIZ;
		if(GlobalMembersGLOBAL.horiz > DefineConstants.MAXHORIZ)
			GlobalMembersGLOBAL.horiz = DefineConstants.MAXHORIZ;

		if(GlobalMembersGLOBAL.ud.scrollmode && GlobalMembersGLOBAL.ud.overhead_on)
		{
			GlobalMembersGLOBAL.ud.folfvel = GlobalMembersGLOBAL.vel;
			GlobalMembersGLOBAL.ud.folavel = GlobalMembersGLOBAL.angvel;
			GlobalMembersGLOBAL.loc.fvel = 0;
			GlobalMembersGLOBAL.loc.svel = 0;
			GlobalMembersGLOBAL.loc.avel = 0;
			GlobalMembersGLOBAL.loc.horz = 0;
			return;
		}

		if(numplayers > 1)
			daang = GlobalMembersGLOBAL.myang;
		else
			daang = p.ang;

		momx = mulscale9(GlobalMembersGLOBAL.vel, sintable[(daang+2560)&2047]);
		momy = mulscale9(GlobalMembersGLOBAL.vel, sintable[(daang+2048)&2047]);

		momx += mulscale9(GlobalMembersGLOBAL.svel, sintable[(daang+2048)&2047]);
		momy += mulscale9(GlobalMembersGLOBAL.svel, sintable[(daang+1536)&2047]);

		momx += GlobalMembersGLOBAL.fricxv;
		momy += GlobalMembersGLOBAL.fricyv;

		GlobalMembersGLOBAL.loc.fvel = momx;
		GlobalMembersGLOBAL.loc.svel = momy;
		GlobalMembersGLOBAL.loc.avel = GlobalMembersGLOBAL.angvel;
		GlobalMembersGLOBAL.loc.horz = GlobalMembersGLOBAL.horiz;
	}


	public static sbyte doincrements(ref player_struct p)
	{
		int i; //j,
		int snum;

		snum = sprite[p.i].yvel;
	//    j = sync[snum].avel;
	//    p->weapon_ang = -(j/5);

		p.player_par++;

		if(p.invdisptime > 0)
			p.invdisptime--;

		if(p.tipincs > 0)
			p.tipincs--;

		if(p.last_pissed_time > 0)
		{
			p.last_pissed_time--;

			if(p.last_pissed_time == (26 *219))
			{
				GlobalMembersSOUNDS.spritesound(FLUSH_TOILET, p.i);
				if(snum == GlobalMembersGLOBAL.screenpeek || GlobalMembersGLOBAL.ud.coop == 1)
					GlobalMembersSOUNDS.spritesound(DUKE_PISSRELIEF, p.i);
			}

			if(p.last_pissed_time == (26 *218))
			{
				p.holster_weapon = 0;
				p.weapon_pos = 10;
			}
		}

		if(p.crack_time > 0)
		{
			p.crack_time--;
			if(p.crack_time == 0)
			{
				p.knuckle_incs = 1;
				p.crack_time = 777;
			}
		}

		if(p.steroids_amount > 0 && p.steroids_amount < 400)
		{
			p.steroids_amount--;
			if(p.steroids_amount == 0)
				GlobalMembersACTORS.checkavailinven(ref p);
			if(!(p.steroids_amount &7))
				if(snum == GlobalMembersGLOBAL.screenpeek || GlobalMembersGLOBAL.ud.coop == 1)
					GlobalMembersSOUNDS.spritesound(DUKE_HARTBEAT, p.i);
		}

		if(p.heat_on && p.heat_amount > 0)
		{
			p.heat_amount--;
			if(p.heat_amount == 0)
			{
				p.heat_on = 0;
				GlobalMembersACTORS.checkavailinven(ref p);
				GlobalMembersSOUNDS.spritesound(NITEVISION_ONOFF, p.i);
				GlobalMembersPLAYER.setpal(ref p);
			}
		}

		if(p.holoduke_on >= 0)
		{
			p.holoduke_amount--;
			if(p.holoduke_amount <= 0)
			{
				GlobalMembersSOUNDS.spritesound(TELEPORTER, p.i);
				p.holoduke_on = -1;
				GlobalMembersACTORS.checkavailinven(ref p);
			}
		}

		if(p.jetpack_on && p.jetpack_amount > 0)
		{
			p.jetpack_amount--;
			if(p.jetpack_amount <= 0)
			{
				p.jetpack_on = 0;
				GlobalMembersACTORS.checkavailinven(ref p);
				GlobalMembersSOUNDS.spritesound(DUKE_JETPACK_OFF, p.i);
				GlobalMembersSOUNDS.stopsound(DUKE_JETPACK_IDLE);
				GlobalMembersSOUNDS.stopsound(DUKE_JETPACK_ON);
			}
		}

		if(p.quick_kick > 0 && sprite[p.i].pal != 1)
		{
			p.quick_kick--;
			if(p.quick_kick == 8)
				GlobalMembersPLAYER.shoot(p.i, KNEE);
		}

		if(p.access_incs && sprite[p.i].pal != 1)
		{
			p.access_incs++;
			if(sprite[p.i].extra <= 0)
				p.access_incs = 12;
			if(p.access_incs == 12)
			{
				if(p.access_spritenum >= 0)
				{
					GlobalMembersSECTOR.checkhitswitch(snum, p.access_spritenum, 1);
					switch(sprite[p.access_spritenum].pal)
					{
						case 0:
							p.got_access &= (0xffff-0x1);
							break;
						case 21:
							p.got_access &= (0xffff-0x2);
							break;
						case 23:
							p.got_access &= (0xffff-0x4);
							break;
					}
					p.access_spritenum = -1;
				}
				else
				{
					GlobalMembersSECTOR.checkhitswitch(snum, p.access_wallnum, 0);
					switch(wall[p.access_wallnum].pal)
					{
						case 0:
							p.got_access &= (0xffff-0x1);
							break;
						case 21:
							p.got_access &= (0xffff-0x2);
							break;
						case 23:
							p.got_access &= (0xffff-0x4);
							break;
					}
				}
			}

			if(p.access_incs > 20)
			{
				p.access_incs = 0;
				p.weapon_pos = 10;
				p.kickback_pic = 0;
			}
		}

		if(p.scuba_on == 0 && sector[p.cursectnum].lotag == 2)
		{
			if(p.scuba_amount > 0)
			{
				p.scuba_on = 1;
				p.inven_icon = 6;
				GlobalMembersGAME.FTA(76, ref p);
			}
			else
			{
				if(p.airleft > 0)
					p.airleft--;
				else
				{
					p.extra_extra8 += 32;
					if(p.last_extra < (GlobalMembersGLOBAL.max_player_health>>1) && (p.last_extra &3) == 0)
						GlobalMembersSOUNDS.spritesound(DUKE_LONGTERM_PAIN, p.i);
				}
			}
		}
		else if(p.scuba_amount > 0 && p.scuba_on)
		{
			p.scuba_amount--;
			if(p.scuba_amount == 0)
			{
				p.scuba_on = 0;
				GlobalMembersACTORS.checkavailinven(ref p);
			}
		}

		if(p.knuckle_incs)
		{
			p.knuckle_incs ++;
			if(p.knuckle_incs==10)
			{
				if(totalclock > 1024)
					if(snum == GlobalMembersGLOBAL.screenpeek || GlobalMembersGLOBAL.ud.coop == 1)
				{
					if(RandomNumbers.NextNumber()&1)
						GlobalMembersSOUNDS.spritesound(DUKE_CRACK, p.i);
					else
						GlobalMembersSOUNDS.spritesound(DUKE_CRACK2, p.i);
				}
				GlobalMembersSOUNDS.spritesound(DUKE_CRACK_FIRST, p.i);
			}
			else if(p.knuckle_incs == 22 || (GlobalMembersGLOBAL.sync[snum].bits&(1<<2)))
				p.knuckle_incs=0;

			return 1;
		}
		return 0;
	}

	public static short[] weapon_sprites = { KNEE, FIRSTGUNSPRITE, SHOTGUNSPRITE, CHAINGUNSPRITE, RPGSPRITE, HEAVYHBOMB, SHRINKERSPRITE, DEVISTATORSPRITE, TRIPBOMBSPRITE, FREEZESPRITE, HEAVYHBOMB, SHRINKERSPRITE};

	public static void checkweapons(ref player_struct p)
	{
		short j;
		short cw;

		cw = p.curr_weapon;

		if(cw < 1 || cw >= MAX_WEAPONS)
			return;

		if (cw != 0)
		{
			if(TRAND &1)
				GlobalMembersGAME.spawn(p.i, weapon_sprites[cw]);
			else
				switch(cw)
			{
				case RPG_WEAPON:
				case HANDBOMB_WEAPON:
					GlobalMembersGAME.spawn(p.i, EXPLOSION2);
					break;
			}
		}
	}

	public static void processinput(short snum)
	{
		int j;
		int i;
		int k;
		int doubvel;
		int fz;
		int cz;
		int hz;
		int lz;
		int truefdist;
		int x;
		int y;
		sbyte shrunk;
		uint sb_snum;
		short psect;
		short psectlotag;
		short kb;
		short tempsect;
		short pi;
		player_struct p;
		spritetype s;

		p = GlobalMembersGLOBAL.ps[snum];
		pi = p.i;
		s = sprite[pi];

		kb = p.kickback_pic;

		if(p.cheat_phase <= 0)
			sb_snum = GlobalMembersGLOBAL.sync[snum].bits;
		else
			sb_snum = 0;

		psect = p.cursectnum;
		if(psect == -1)
		{
			if(s.extra > 0 && GlobalMembersGLOBAL.ud.clipping == 0)
			{
				GlobalMembersPLAYER.quickkill(ref p);
				GlobalMembersSOUNDS.spritesound(SQUISHED, pi);
			}
			psect = 0;
		}

		psectlotag = sector[psect].lotag;
		p.spritebridge = 0;

		shrunk = (s.yrepeat < 32);
		getzrange(p.posx, p.posy, p.posz, psect, cz, hz, fz, lz, 163, CLIPMASK0);

		j = getflorzofslope(psect,p.posx,p.posy);

		p.truefz = j;
		p.truecz = getceilzofslope(psect,p.posx,p.posy);

		truefdist = klabs(p.posz-j);
		if((lz &49152) == 16384 && psectlotag == 1 && truefdist > PHEIGHT+(16<<8))
			psectlotag = 0;

		GlobalMembersGLOBAL.hittype[pi].floorz = fz;
		GlobalMembersGLOBAL.hittype[pi].ceilingz = cz;

		p.ohoriz = p.horiz;
		p.ohorizoff = p.horizoff;

		if(p.aim_mode == 0 && p.on_ground && psectlotag != 2 && (sector[psect].floorstat &2))
		{
			  x = p.posx+(sintable[(p.ang+512)&2047]>>5);
			  y = p.posy+(sintable[p.ang &2047]>>5);
			  tempsect = psect;
			  updatesector(x, y, tempsect);
			  if (tempsect >= 0)
			  {
				  k = getflorzofslope(psect,x,y);
				  if (psect == tempsect)
					  p.horizoff += mulscale16(j-k,160);
				  else if (klabs(getflorzofslope(tempsect,x,y)-k) <= (4<<8))
					  p.horizoff += mulscale16(j-k,160);
			  }
		 }
		 if (p.horizoff > 0)
			 p.horizoff -= ((p.horizoff>>3)+1);
		 else if (p.horizoff < 0)
			 p.horizoff += (((-p.horizoff)>>3)+1);

		if(hz >= 0 && (hz &49152) == 49152)
		{
			hz &= (MAXSPRITES-1);

			if(sprite[hz].statnum == 1 && sprite[hz].extra >= 0)
			{
				hz = 0;
				cz = p.truecz;
			}
		}

		if(lz >= 0 && (lz &49152) == 49152)
		{
			j = lz&(MAXSPRITES-1);

			if((sprite[j].cstat &33) == 33)
			{
				psectlotag = 0;
				p.footprintcount = 0;
				p.spritebridge = 1;
			}
			else if(GlobalMembersGAME.badguy(ref sprite[j]) != 0 && sprite[j].xrepeat > 24 && klabs(s.z-sprite[j].z) < (84<<8))
			{
				j = getangle(sprite[j].x-p.posx,sprite[j].y-p.posy);
				p.posxv -= sintable[(j+512)&2047]<<4;
				p.posyv -= sintable[j &2047]<<4;
			}
		}


		if (s.extra > 0)
			GlobalMembersPLAYER.incur_damage(ref p);
		else
		{
			s.extra = 0;
			p.shield_amount = 0;
		}

		p.last_extra = s.extra;

		if(p.loogcnt > 0)
			p.loogcnt--;
		else
			p.loogcnt = 0;

		if(p.fist_incs)
		{
			p.fist_incs++;
			if(p.fist_incs == 28)
			{
				if(GlobalMembersGLOBAL.ud.recstat == 1)
					GlobalMembersGAME.closedemowrite();
				GlobalMembersSOUNDS.sound(PIPEBOMB_EXPLODE);
				p.pals[0] = 64;
				p.pals[1] = 64;
				p.pals[2] = 64;
				p.pals_time = 48;
			}
			if(p.fist_incs > 42)
			{
				if(p.buttonpalette && GlobalMembersGLOBAL.ud.from_bonus == 0)
				{
					GlobalMembersGLOBAL.ud.from_bonus = GlobalMembersGLOBAL.ud.level_number+1;
					if(GlobalMembersGLOBAL.ud.secretlevel > 0 && GlobalMembersGLOBAL.ud.secretlevel < 12)
						GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.ud.secretlevel-1;
					GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
				}
				else
				{
					if(GlobalMembersGLOBAL.ud.from_bonus)
					{
						GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.ud.from_bonus;
						GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
						GlobalMembersGLOBAL.ud.from_bonus = 0;
					}
					else
					{
						if(GlobalMembersGLOBAL.ud.level_number == GlobalMembersGLOBAL.ud.secretlevel && GlobalMembersGLOBAL.ud.from_bonus > 0)
							GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.ud.from_bonus;
						else
							GlobalMembersGLOBAL.ud.level_number++;

						if(GlobalMembersGLOBAL.ud.level_number > 10)
							GlobalMembersGLOBAL.ud.level_number = 0;
						GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;

					}
				}
				for(i = connecthead;i>=0;i = connectpoint2[i])
					GlobalMembersGLOBAL.ps[i].gm = MODE_EOL;
				p.fist_incs = 0;

				return;
			}
		}

		if(p.timebeforeexit > 1 && p.last_extra > 0)
		{
			p.timebeforeexit--;
			if(p.timebeforeexit == 26 *5)
			{
				FX_StopAllSounds();
				GlobalMembersSOUNDS.clearsoundlocks();
				if(p.customexitsound >= 0)
				{
					GlobalMembersSOUNDS.sound(p.customexitsound);
					GlobalMembersGAME.FTA(102, ref p);
				}
			}
			else if(p.timebeforeexit == 1)
			{
				for(i = connecthead;i>=0;i = connectpoint2[i])
					GlobalMembersGLOBAL.ps[i].gm = MODE_EOL;
				if(GlobalMembersGLOBAL.ud.from_bonus)
				{
					GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.ud.from_bonus;
					GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
					GlobalMembersGLOBAL.ud.from_bonus = 0;
				}
				else
				{
					GlobalMembersGLOBAL.ud.level_number++;
					GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
				}
				return;
			}
		}
	/*
	    if(p->select_dir)
	    {
	        if(psectlotag != 15 || (sb_snum&(1<<31)) )
	            p->select_dir = 0;
	        else
	        {
	            if(sync[snum].fvel > 127)
	            {
	                p->select_dir = 0;
	                activatewarpelevators(pi,-1);
	            }
	            else if(sync[snum].fvel <= -127)
	            {
	                p->select_dir = 0;
	                activatewarpelevators(pi,1);
	            }
	            return;
	        }
	    }
	  */
		if(p.pals_time > 0)
			p.pals_time--;

		if(p.fta > 0)
		{
			p.fta--;
			if(p.fta == 0)
			{
				GlobalMembersGLOBAL.pub = NUMPAGES;
				GlobalMembersGLOBAL.pus = NUMPAGES;
				p.ftq = 0;
			}
		}

		if(s.extra <= 0)
		{
			if(p.dead_flag == 0)
			{
				if(s.pal != 1)
				{
					p.pals[0] = 63;
					p.pals[1] = 0;
					p.pals[2] = 0;
					p.pals_time = 63;
					p.posz -= (16<<8);
					s.z -= (16<<8);
				}

				if(GlobalMembersGLOBAL.ud.recstat == 1 && GlobalMembersGLOBAL.ud.multimode < 2)
					GlobalMembersGAME.closedemowrite();

				if(s.pal != 1)
					p.dead_flag = (512-((TRAND &1)<<10)+(TRAND &255)-512)&2047;

				p.jetpack_on = 0;
				p.holoduke_on = -1;

				GlobalMembersSOUNDS.stopsound(DUKE_JETPACK_IDLE);
				if(p.scream_voice > FX_Ok)
				{
					FX_StopSound(p.scream_voice);
					testcallback(DUKE_SCREAM);
					p.scream_voice = FX_Ok;
				}

				if(s.pal != 1 && (s.cstat &32768) == 0)
					s.cstat = 0;

				if(GlobalMembersGLOBAL.ud.multimode > 1 && (s.pal != 1 || (s.cstat &32768)))
				{
					if(p.frag_ps != snum)
					{
						GlobalMembersGLOBAL.ps[p.frag_ps].frag++;
						GlobalMembersGLOBAL.frags[p.frag_ps, snum]++;

						if(GlobalMembersGLOBAL.ud.user_name[p.frag_ps][0] != 0)
						{
							if(snum == GlobalMembersGLOBAL.screenpeek)
							{
								GlobalMembersGLOBAL.fta_quotes[115, 0] = string.Format("KILLED BY {0}", GlobalMembersGLOBAL.ud.user_name[p.frag_ps][0]);
								GlobalMembersGAME.FTA(115, ref p);
							}
							else
							{
								GlobalMembersGLOBAL.fta_quotes[116, 0] = string.Format("KILLED {0}", GlobalMembersGLOBAL.ud.user_name[snum][0]);
								GlobalMembersGAME.FTA(116, ref GlobalMembersGLOBAL.ps[p.frag_ps]);
							}
						}
						else
						{
							if(snum == GlobalMembersGLOBAL.screenpeek)
							{
								GlobalMembersGLOBAL.fta_quotes[115, 0] = string.Format("KILLED BY PLAYER {0:D}", 1+p.frag_ps);
								GlobalMembersGAME.FTA(115, ref p);
							}
							else
							{
								GlobalMembersGLOBAL.fta_quotes[116, 0] = string.Format("KILLED PLAYER {0:D}", 1+snum);
								GlobalMembersGAME.FTA(116, ref GlobalMembersGLOBAL.ps[p.frag_ps]);
							}
						}
					}
					else
						p.fraggedself++;

					if(myconnectindex == connecthead)
					{
						GlobalMembersGLOBAL.tempbuf = string.Format("frag {0:D} killed {1:D}\n", p.frag_ps+1, snum+1);
						GlobalMembersGAME.sendscore(ref GlobalMembersGLOBAL.tempbuf);
	//                    printf(tempbuf);
					}

					p.frag_ps = snum;
					GlobalMembersGLOBAL.pus = NUMPAGES;
				}
			}

			if(psectlotag == 2)
			{
				if(p.on_warping_sector == 0)
				{
					if(klabs(p.posz-fz) > (PHEIGHT>>1))
						p.posz += 348;
				}
				else
				{
					s.z -= 512;
					s.zvel = -348;
				}

				clipmove(p.posx, p.posy, p.posz, p.cursectnum, 0, 0, 164, (4<<8), (4<<8), CLIPMASK0);
	//            p->bobcounter += 32;
			}

			p.oposx = p.posx;
			p.oposy = p.posy;
			p.oposz = p.posz;
			p.oang = p.ang;
			p.opyoff = p.pyoff;

			p.horiz = 100;
			p.horizoff = 0;

			updatesector(p.posx, p.posy, p.cursectnum);

			pushmove(p.posx, p.posy, p.posz, p.cursectnum, 128, (4<<8), (20<<8), CLIPMASK0);

			if(fz > cz+(16<<8) && s.pal != 1)
				p.rotscrnang = (p.dead_flag + ((fz+p.posz)>>7))&2047;

			p.on_warping_sector = 0;

			return;
		}

		if(p.transporter_hold > 0)
		{
			p.transporter_hold--;
			if(p.transporter_hold == 0 && p.on_warping_sector)
				p.transporter_hold = 2;
		}
		if(p.transporter_hold < 0)
			p.transporter_hold++;

		if(p.newowner >= 0)
		{
			i = p.newowner;
			p.posx = SX;
			p.posy = SY;
			p.posz = SZ;
			p.ang = SA;
			p.posxv = p.posyv = s.xvel = 0;
			p.look_ang = 0;
			p.rotscrnang = 0;

			GlobalMembersPLAYER.doincrements(ref p);

			if(p.curr_weapon == HANDREMOTE_WEAPON)
				goto SHOOTINCODE;

			return;
		}

		doubvel = TICSPERFRAME;

		if (p.rotscrnang > 0)
			p.rotscrnang -= ((p.rotscrnang>>1)+1);
		else if (p.rotscrnang < 0)
			p.rotscrnang += (((-p.rotscrnang)>>1)+1);

		p.look_ang -= (p.look_ang>>2);

		if(sb_snum&(1<<6))
		{
			p.look_ang -= 152;
			p.rotscrnang += 24;
		}

		if(sb_snum&(1<<7))
		{
			p.look_ang += 152;
			p.rotscrnang -= 24;
		}

		if(p.on_crane >= 0)
			goto HORIZONLY;

		j = ksgn(GlobalMembersGLOBAL.sync[snum].avel);
		/*
		if( j && ud.screen_tilting == 2)
		{
		    k = 4;
		    if(sb_snum&(1<<5)) k <<= 2;
		    p->rotscrnang -= k*j;
		    p->look_ang += k*j;
		}
		*/

		if(s.xvel < 32 || p.on_ground == 0 || p.bobcounter == 1024)
		{
			if((p.weapon_sway &2047) > (1024+96))
				p.weapon_sway -= 96;
			else if((p.weapon_sway &2047) < (1024-96))
				p.weapon_sway += 96;
			else
				p.weapon_sway = 1024;
		}
		else
			p.weapon_sway = p.bobcounter;

		s.xvel = ksqrt((p.posx-p.bobposx)*(p.posx-p.bobposx)+(p.posy-p.bobposy)*(p.posy-p.bobposy));
		if(p.on_ground)
			p.bobcounter += sprite[p.i].xvel>>1;

		if(GlobalMembersGLOBAL.ud.clipping == 0 && (sector[p.cursectnum].floorpicnum == MIRROR || p.cursectnum < 0 || p.cursectnum >= MAXSECTORS))
		{
			p.posx = p.oposx;
			p.posy = p.oposy;
		}
		else
		{
			p.oposx = p.posx;
			p.oposy = p.posy;
		}

		p.bobposx = p.posx;
		p.bobposy = p.posy;

		p.oposz = p.posz;
		p.opyoff = p.pyoff;
		p.oang = p.ang;

		if(p.one_eighty_count < 0)
		{
			p.one_eighty_count += 128;
			p.ang += 128;
		}

		// Shrinking code

		i = 40;

		if(psectlotag == 2)
		{
			p.jumping_counter = 0;

			p.pycount += 32;
			p.pycount &= 2047;
			p.pyoff = sintable[p.pycount]>>7;

			if(GlobalMembersGLOBAL.Sound[DUKE_UNDERWATER].num == 0)
				GlobalMembersSOUNDS.spritesound(DUKE_UNDERWATER, pi);

			if (sb_snum &1 != 0)
			{
				if(p.poszv > 0)
					p.poszv = 0;
				p.poszv -= 348;
				if(p.poszv < -(256 *6))
					p.poszv = -(256 *6);
			}
			else if (sb_snum&(1<<1))
			{
				if(p.poszv < 0)
					p.poszv = 0;
				p.poszv += 348;
				if(p.poszv > (256 *6))
					p.poszv = (256 *6);
			}
			else
			{
				if(p.poszv < 0)
				{
					p.poszv += 256;
					if(p.poszv > 0)
						p.poszv = 0;
				}
				if(p.poszv > 0)
				{
					p.poszv -= 256;
					if(p.poszv < 0)
						p.poszv = 0;
				}
			}

			if(p.poszv > 2048)
				p.poszv >>= 1;

			p.posz += p.poszv;

			if(p.posz > (fz-(15<<8)))
				p.posz += ((fz-(15<<8))-p.posz)>>1;

			if(p.posz < (cz+(4<<8)))
			{
				p.posz = cz+(4<<8);
				p.poszv = 0;
			}

			if(p.scuba_on && (TRAND &255) < 8)
			{
				j = GlobalMembersGAME.spawn(pi, WATERBUBBLE);
				sprite[j].x += sintable[(p.ang+512+64-(GlobalMembersGLOBAL.global_random &128))&2047]>>6;
				sprite[j].y += sintable[(p.ang+64-(GlobalMembersGLOBAL.global_random &128))&2047]>>6;
				sprite[j].xrepeat = 3;
				sprite[j].yrepeat = 2;
				sprite[j].z = p.posz+(8<<8);
			}
		}

		else if(p.jetpack_on)
		{
			p.on_ground = 0;
			p.jumping_counter = 0;
			p.hard_landing = 0;
			p.falling_counter = 0;

			p.pycount += 32;
			p.pycount &= 2047;
			p.pyoff = sintable[p.pycount]>>7;

			if(p.jetpack_on < 11)
			{
				p.jetpack_on++;
				p.posz -= (p.jetpack_on<<7); //Goin up
			}
			else if(p.jetpack_on == 11 && GlobalMembersGLOBAL.Sound[DUKE_JETPACK_IDLE].num < 1)
				GlobalMembersSOUNDS.spritesound(DUKE_JETPACK_IDLE, pi);

			if (shrunk != 0)
				j = 512;
			else
				j = 2048;

			if (sb_snum &1 != 0) //A (soar high)
			{
				p.posz -= j;
				p.crack_time = 777;
			}

			if (sb_snum&(1<<1)) //Z (soar low)
			{
				p.posz += j;
				p.crack_time = 777;
			}

			if(shrunk == 0 && (psectlotag == 0 || psectlotag == 2))
				k = 32;
			else
				k = 16;

			if(psectlotag != 2 && p.scuba_on == 1)
				p.scuba_on = 0;

			if(p.posz > (fz-(k<<8)))
				p.posz += ((fz-(k<<8))-p.posz)>>1;
			if(p.posz < (GlobalMembersGLOBAL.hittype[pi].ceilingz+(18<<8)))
				p.posz = GlobalMembersGLOBAL.hittype[pi].ceilingz+(18<<8);

		}
		else if(psectlotag != 2)
		{
			if(p.airleft != 15 *26)
				p.airleft = 15 *26; //Aprox twenty seconds.

			if(p.scuba_on == 1)
				p.scuba_on = 0;

			if(psectlotag == 1 && p.spritebridge == 0)
			{
				if(shrunk == 0)
				{
					i = 34;
					p.pycount += 32;
					p.pycount &= 2047;
					p.pyoff = sintable[p.pycount]>>6;
				}
				else
					i = 12;

				if(shrunk == 0 && truefdist <= PHEIGHT)
				{
					if(p.on_ground == 1)
					{
						if(p.dummyplayersprite == -1)
							p.dummyplayersprite = GlobalMembersGAME.spawn(pi, PLAYERONWATER);

						p.footprintcount = 6;
						if(sector[p.cursectnum].floorpicnum == FLOORSLIME)
							p.footprintpal = 8;
						else
							p.footprintpal = 0;
						p.footprintshade = 0;
					}
				}
			}
			else
			{
				if(p.footprintcount > 0 && p.on_ground)
					if((sector[p.cursectnum].floorstat &2) != 2)
				{
					for(j = headspritesect[psect];j>=0;j = nextspritesect[j])
						if(sprite[j].picnum == FOOTPRINTS || sprite[j].picnum == FOOTPRINTS2 || sprite[j].picnum == FOOTPRINTS3 || sprite[j].picnum == FOOTPRINTS4)
							if (klabs(sprite[j].x-p.posx) < 384)
								if (klabs(sprite[j].y-p.posy) < 384)
									break;
					if(j < 0)
					{
						p.footprintcount--;
						if(sector[p.cursectnum].lotag == 0 && sector[p.cursectnum].hitag == 0)
						{
							switch(TRAND &3)
							{
								case 0:
									j = GlobalMembersGAME.spawn(pi, FOOTPRINTS);
									break;
								case 1:
									j = GlobalMembersGAME.spawn(pi, FOOTPRINTS2);
									break;
								case 2:
									j = GlobalMembersGAME.spawn(pi, FOOTPRINTS3);
									break;
								default:
									j = GlobalMembersGAME.spawn(pi, FOOTPRINTS4);
									break;
							}
							sprite[j].pal = p.footprintpal;
							sprite[j].shade = p.footprintshade;
						}
					}
				}
			}

			if(p.posz < (fz-(i<<8))) //falling
			{
				if((sb_snum &3) == 0 && p.on_ground && (sector[psect].floorstat &2) && p.posz >= (fz-(i<<8)-(16<<8)))
					p.posz = fz-(i<<8);
				else
				{
					p.on_ground = 0;
					p.poszv += (GlobalMembersGLOBAL.gc+80); // (TICSPERFRAME<<6);
					if(p.poszv >= (4096+2048))
						p.poszv = (4096+2048);
					if(p.poszv > 2400 && p.falling_counter < 255)
					{
						p.falling_counter++;
						if(p.falling_counter == 38)
							p.scream_voice = GlobalMembersSOUNDS.spritesound(DUKE_SCREAM, pi);
					}

					if((p.posz+p.poszv) >= (fz-(i<<8))) // hit the ground
						if(sector[p.cursectnum].lotag != 1)
						{
							if(p.falling_counter > 62)
								GlobalMembersPLAYER.quickkill(ref p);

							else if(p.falling_counter > 9)
							{
								j = p.falling_counter;
								s.extra -= j-(TRAND &3);
								if(s.extra <= 0)
								{
									GlobalMembersSOUNDS.spritesound(SQUISHED, pi);
									p.pals[0] = 63;
									p.pals[1] = 0;
									p.pals[2] = 0;
									p.pals_time = 63;
								}
								else
								{
									GlobalMembersSOUNDS.spritesound(DUKE_LAND, pi);
									GlobalMembersSOUNDS.spritesound(DUKE_LAND_HURT, pi);
								}

								p.pals[0] = 16;
								p.pals[1] = 0;
								p.pals[2] = 0;
								p.pals_time = 32;
							}
							else if(p.poszv > 2048)
								GlobalMembersSOUNDS.spritesound(DUKE_LAND, pi);
						}
				}
			}

			else
			{
				p.falling_counter = 0;
				if(p.scream_voice > FX_Ok)
				{
					FX_StopSound(p.scream_voice);
					p.scream_voice = FX_Ok;
				}

				if(psectlotag != 1 && psectlotag != 2 && p.on_ground == 0 && p.poszv > (6144>>1))
					p.hard_landing = p.poszv>>10;

				p.on_ground = 1;

				if(i == 40)
				{
					//Smooth on the ground

					k = ((fz-(i<<8))-p.posz)>>1;
					if(klabs(k) < 256)
						k = 0;
					p.posz += k;
					p.poszv -= 768;
					if(p.poszv < 0)
						p.poszv = 0;
				}
				else if(p.jumping_counter == 0)
				{
					p.posz += ((fz-(i<<7))-p.posz)>>1; //Smooth on the water
					if(p.on_warping_sector == 0 && p.posz > fz-(16<<8))
					{
						p.posz = fz-(16<<8);
						p.poszv >>= 1;
					}
				}

				p.on_warping_sector = 0;

				if((sb_snum &2))
				{
					p.posz += (2048+768);
					p.crack_time = 777;
				}

				if((sb_snum &1) == 0 && p.jumping_toggle == 1)
					p.jumping_toggle = 0;

				else if((sb_snum &1) && p.jumping_toggle == 0)
				{
					if(p.jumping_counter == 0)
						if((fz-cz) > (56<<8))
						{
							p.jumping_counter = 1;
							p.jumping_toggle = 1;
						}
				}

				if(p.jumping_counter && (sb_snum &1) == 0)
					p.jumping_toggle = 0;
			}

			if(p.jumping_counter)
			{
				if((sb_snum &1) == 0 && p.jumping_toggle == 1)
					p.jumping_toggle = 0;

				if(p.jumping_counter < (1024+256))
				{
					if(psectlotag == 1 && p.jumping_counter > 768)
					{
						p.jumping_counter = 0;
						p.poszv = -512;
					}
					else
					{
						p.poszv -= (sintable[(2048-128+p.jumping_counter)&2047])/12;
						p.jumping_counter += 180;
						p.on_ground = 0;
					}
				}
				else
				{
					p.jumping_counter = 0;
					p.poszv = 0;
				}
			}

			p.posz += p.poszv;

			if(p.posz < (cz+(4<<8)))
			{
				p.jumping_counter = 0;
				if(p.poszv < 0)
					p.posxv = p.posyv = 0;
				p.poszv = 128;
				p.posz = cz+(4<<8);
			}
		}

		//Do the quick lefts and rights

		if (p.fist_incs || p.transporter_hold > 2 || p.hard_landing || p.access_incs > 0 || p.knee_incs > 0 || (p.curr_weapon == TRIPBOMB_WEAPON && kb > 1 && kb < 4))
		{
			doubvel = 0;
			p.posxv = 0;
			p.posyv = 0;
		}
		else if (GlobalMembersGLOBAL.sync[snum].avel) //p->ang += syncangvel * constant
		{ //ENGINE calculates angvel for you
			int tempang;

			tempang = GlobalMembersGLOBAL.sync[snum].avel<<1;

			if(psectlotag == 2)
				p.angvel =(tempang-(tempang>>3))*sgn(doubvel);
			else
				p.angvel = tempang *sgn(doubvel);

			p.ang += p.angvel;
			p.ang &= 2047;
			p.crack_time = 777;
		}

		if(p.spritebridge == 0)
		{
			j = sector[s.sectnum].floorpicnum;

			if(j == PURPLELAVA || sector[s.sectnum].ceilingpicnum == PURPLELAVA)
			{
				if(p.boot_amount > 0)
				{
					p.boot_amount--;
					p.inven_icon = 7;
					if(p.boot_amount <= 0)
						GlobalMembersACTORS.checkavailinven(ref p);
				}
				else
				{
					if(GlobalMembersGLOBAL.Sound[DUKE_LONGTERM_PAIN].num < 1)
						GlobalMembersSOUNDS.spritesound(DUKE_LONGTERM_PAIN, pi);
					p.pals[0] = 0;
					p.pals[1] = 8;
					p.pals[2] = 0;
					p.pals_time = 32;
					s.extra--;
				}
			}

			k = 0;

			if(p.on_ground && truefdist <= PHEIGHT+(16<<8))
			{
				switch(j)
				{
					case HURTRAIL:
						if(rnd(32))
						{
							if(p.boot_amount > 0)
								k = 1;
							else
							{
								if(GlobalMembersGLOBAL.Sound[DUKE_LONGTERM_PAIN].num < 1)
									GlobalMembersSOUNDS.spritesound(DUKE_LONGTERM_PAIN, pi);
								p.pals[0] = 64;
								p.pals[1] = 64;
								p.pals[2] = 64;
								p.pals_time = 32;
								s.extra -= 1+(TRAND &3);
								if(GlobalMembersGLOBAL.Sound[SHORT_CIRCUIT].num < 1)
									GlobalMembersSOUNDS.spritesound(SHORT_CIRCUIT, pi);
							}
						}
						break;
					case FLOORSLIME:
						if(rnd(16))
						{
							if(p.boot_amount > 0)
								k = 1;
							else
							{
								if(GlobalMembersGLOBAL.Sound[DUKE_LONGTERM_PAIN].num < 1)
									GlobalMembersSOUNDS.spritesound(DUKE_LONGTERM_PAIN, pi);
								p.pals[0] = 0;
								p.pals[1] = 8;
								p.pals[2] = 0;
								p.pals_time = 32;
								s.extra -= 1+(TRAND &3);
							}
						}
						break;
					case FLOORPLASMA:
						if(rnd(32))
						{
							if(p.boot_amount > 0)
								k = 1;
							else
							{
								if(GlobalMembersGLOBAL.Sound[DUKE_LONGTERM_PAIN].num < 1)
									GlobalMembersSOUNDS.spritesound(DUKE_LONGTERM_PAIN, pi);
								p.pals[0] = 8;
								p.pals[1] = 0;
								p.pals[2] = 0;
								p.pals_time = 32;
								s.extra -= 1+(TRAND &3);
							}
						}
						break;
				}
			}

			if (k != 0)
			{
				GlobalMembersGAME.FTA(75, ref p);
				p.boot_amount -= 2;
				if(p.boot_amount <= 0)
					GlobalMembersACTORS.checkavailinven(ref p);
			}
		}

		if (p.posxv || p.posyv || GlobalMembersGLOBAL.sync[snum].fvel || GlobalMembersGLOBAL.sync[snum].svel)
		{
			p.crack_time = 777;

			k = sintable[p.bobcounter &2047]>>12;

			if(truefdist < PHEIGHT+(8<<8))
				if(k == 1 || k == 3)
			{
				if(p.spritebridge == 0 && p.walking_snd_toggle == 0 && p.on_ground)
				{
					switch(psectlotag)
					{
						case 0:

							if(lz >= 0 && (lz&(MAXSPRITES-1))==49152)
								j = sprite[lz&(MAXSPRITES-1)].picnum;
							else
								j = sector[psect].floorpicnum;

							switch(j)
							{
								case PANNEL1:
								case PANNEL2:
									GlobalMembersSOUNDS.spritesound(DUKE_WALKINDUCTS, pi);
									p.walking_snd_toggle = 1;
									break;
							}
							break;
						case 1:
							if((TRAND &1) == 0)
								GlobalMembersSOUNDS.spritesound(DUKE_ONWATER, pi);
							p.walking_snd_toggle = 1;
							break;
					}
				}
			}
			else if(p.walking_snd_toggle > 0)
				p.walking_snd_toggle --;

			if(p.jetpack_on == 0 && p.steroids_amount > 0 && p.steroids_amount < 400)
				doubvel <<= 1;

			p.posxv += ((GlobalMembersGLOBAL.sync[snum].fvel *doubvel)<<6);
			p.posyv += ((GlobalMembersGLOBAL.sync[snum].svel *doubvel)<<6);

			if((p.curr_weapon == KNEE_WEAPON && kb > 10 && p.on_ground) || (p.on_ground && (sb_snum &2)))
			{
				p.posxv = mulscale(p.posxv,GlobalMembersGLOBAL.dukefriction-0x2000,16);
				p.posyv = mulscale(p.posyv,GlobalMembersGLOBAL.dukefriction-0x2000,16);
			}
			else
			{
				if(psectlotag == 2)
				{
					p.posxv = mulscale(p.posxv,GlobalMembersGLOBAL.dukefriction-0x1400,16);
					p.posyv = mulscale(p.posyv,GlobalMembersGLOBAL.dukefriction-0x1400,16);
				}
				else
				{
					p.posxv = mulscale(p.posxv,GlobalMembersGLOBAL.dukefriction,16);
					p.posyv = mulscale(p.posyv,GlobalMembersGLOBAL.dukefriction,16);
				}
			}

			if(Math.Abs(p.posxv) < 2048 && Math.Abs(p.posyv) < 2048)
				p.posxv = p.posyv = 0;

			if (shrunk != 0)
			{
				p.posxv = mulscale16(p.posxv,GlobalMembersGLOBAL.dukefriction-(GlobalMembersGLOBAL.dukefriction>>1)+(GlobalMembersGLOBAL.dukefriction>>2));
				p.posyv = mulscale16(p.posyv,GlobalMembersGLOBAL.dukefriction-(GlobalMembersGLOBAL.dukefriction>>1)+(GlobalMembersGLOBAL.dukefriction>>2));
			}
		}

		HORIZONLY:

			if(psectlotag == 1 || p.spritebridge == 1)
				i = (4<<8);
			else
				i = (20<<8);

			if(sector[p.cursectnum].lotag == 2)
				k = 0;
			else
				k = 1;

			if(GlobalMembersGLOBAL.ud.clipping)
			{
				j = 0;
				p.posx += p.posxv>>14;
				p.posy += p.posyv>>14;
				updatesector(p.posx, p.posy, p.cursectnum);
				changespritesect(pi,p.cursectnum);
			}
			else
				j = clipmove(p.posx, p.posy, p.posz, p.cursectnum, p.posxv, p.posyv, 164, (4<<8), i, CLIPMASK0);

			if(p.jetpack_on == 0 && psectlotag != 2 && psectlotag != 1 && shrunk != 0)
				p.posz += 32<<8;

			if (j != 0)
				GlobalMembersSECTOR.checkplayerhurt(ref p, j);

			if(p.jetpack_on == 0)
			{
				if(s.xvel > 16)
				{
					if(psectlotag != 1 && psectlotag != 2 && p.on_ground)
					{
						p.pycount += 52;
						p.pycount &= 2047;
						p.pyoff = klabs(s.xvel *sintable[p.pycount])/1596;
					}
				}
				else if(psectlotag != 2 && psectlotag != 1)
					p.pyoff = 0;
			}

			// RBG***
			setsprite(pi,p.posx,p.posy,p.posz+PHEIGHT);

			if(psectlotag < 3)
			{
				psect = s.sectnum;
				if(GlobalMembersGLOBAL.ud.clipping == 0 && sector[psect].lotag == 31)
				{
					if(sprite[sector[psect].hitag].xvel && GlobalMembersGLOBAL.hittype[sector[psect].hitag].temp_data[0] == 0)
					{
						GlobalMembersPLAYER.quickkill(ref p);
						return;
					}
				}
			}

			if(truefdist < PHEIGHT && p.on_ground && psectlotag != 1 && shrunk == 0 && sector[p.cursectnum].lotag == 1)
				if(GlobalMembersGLOBAL.Sound[DUKE_ONWATER].num == 0)
					GlobalMembersSOUNDS.spritesound(DUKE_ONWATER, pi);

			if (p.cursectnum != s.sectnum)
				changespritesect(pi,p.cursectnum);

			if(GlobalMembersGLOBAL.ud.clipping == 0)
				j = (pushmove(p.posx, p.posy, p.posz, p.cursectnum, 164, (4<<8), (4<<8), CLIPMASK0) < 0 && GlobalMembersGAMEDEF.furthestangle(pi, 8) < 512);
			else
				j = 0;

			if(GlobalMembersGLOBAL.ud.clipping == 0)
			{
				if(klabs(GlobalMembersGLOBAL.hittype[pi].floorz-GlobalMembersGLOBAL.hittype[pi].ceilingz) < (48<<8) || j != 0)
				{
					if (!(sector[s.sectnum].lotag &0x8000) && (GlobalMembersSECTOR.isanunderoperator(sector[s.sectnum].lotag) != 0 || GlobalMembersSECTOR.isanearoperator(sector[s.sectnum].lotag) != 0))
							GlobalMembersSECTOR.activatebysector(s.sectnum, pi);
					if (j != 0)
					{
						GlobalMembersPLAYER.quickkill(ref p);
						return;
					}
				}
				else if(klabs(fz-cz) < (32<<8) && GlobalMembersSECTOR.isanunderoperator(sector[psect].lotag) != 0)
					GlobalMembersSECTOR.activatebysector(psect, pi);
			}

			if(sb_snum&(1<<18) || p.hard_landing)
				p.return_to_center = 9;

			if(sb_snum&(1<<13))
			{
				p.return_to_center = 9;
				if(sb_snum&(1<<5))
					p.horiz += 12;
				p.horiz += 12;
			}

			else if(sb_snum&(1<<14))
			{
				p.return_to_center = 9;
				if(sb_snum&(1<<5))
					p.horiz -= 12;
				p.horiz -= 12;
			}

			else if(sb_snum&(1<<3))
			{
				if(sb_snum&(1<<5))
					p.horiz += 6;
				p.horiz += 6;
			}

			else if(sb_snum&(1<<4))
			{
				if(sb_snum&(1<<5))
					p.horiz -= 6;
				p.horiz -= 6;
			}
			if(p.return_to_center > 0)
				if((sb_snum&(1<<13)) == 0 && (sb_snum&(1<<14)) == 0)
			{
				p.return_to_center--;
				p.horiz += 33-(p.horiz/3);
			}

			if(p.hard_landing > 0)
			{
				p.hard_landing--;
				p.horiz -= (p.hard_landing<<4);
			}

			if(p.aim_mode)
				p.horiz += GlobalMembersGLOBAL.sync[snum].horz>>1;
			else
			{
				 if(p.horiz > 95 && p.horiz < 105)
					 p.horiz = 100;
				 if(p.horizoff > -5 && p.horizoff < 5)
					 p.horizoff = 0;
			}

			if(p.horiz > 299)
				p.horiz = 299;
			else if(p.horiz < -99)
				p.horiz = -99;

		//Shooting code/changes

		if(p.show_empty_weapon > 0)
		{
			p.show_empty_weapon--;
			if(p.show_empty_weapon == 0)
			{
				if(p.last_full_weapon == GROW_WEAPON)
					p.subweapon |= (1<<GROW_WEAPON);
				else if(p.last_full_weapon == SHRINKER_WEAPON)
					p.subweapon &= ~(1<<GROW_WEAPON);
				GlobalMembersACTORS.addweapon(ref p, p.last_full_weapon);
				return;
			}
		}

		if(p.knee_incs > 0)
		{
			p.knee_incs++;
			p.horiz -= 48;
			p.return_to_center = 9;
			if(p.knee_incs > 15)
			{
				p.knee_incs = 0;
				p.holster_weapon = 0;
				if(p.weapon_pos < 0)
					p.weapon_pos = -p.weapon_pos;
				if(p.actorsqu >= 0 && GlobalMembersSECTOR.dist(ref sprite[pi], ref sprite[p.actorsqu]) < 1400)
				{
					GlobalMembersACTORS.guts(ref sprite[p.actorsqu], JIBS6, 7, myconnectindex);
					GlobalMembersGAME.spawn(p.actorsqu, BLOODPOOL);
					GlobalMembersSOUNDS.spritesound(SQUISHED, p.actorsqu);
					switch(sprite[p.actorsqu].picnum)
					{
						case FEM1:
						case FEM2:
						case FEM3:
						case FEM4:
						case FEM5:
						case FEM6:
						case FEM7:
						case FEM8:
						case FEM9:
						case FEM10:
						case PODFEM1:
						case NAKED1:
						case STATUE:
							if(sprite[p.actorsqu].yvel)
								GlobalMembersSECTOR.operaterespawns(sprite[p.actorsqu].yvel);
							break;
					}

					if(sprite[p.actorsqu].picnum == APLAYER)
					{
						GlobalMembersPLAYER.quickkill(ref GlobalMembersGLOBAL.ps[sprite[p.actorsqu].yvel]);
						GlobalMembersGLOBAL.ps[sprite[p.actorsqu].yvel].frag_ps = snum;
					}
					else if (GlobalMembersGAME.badguy(ref sprite[p.actorsqu]) != 0)
					{
						deletesprite(p.actorsqu);
						p.actors_killed++;
					}
					else
						deletesprite(p.actorsqu);
				}
				p.actorsqu = -1;
			}
			else if(p.actorsqu >= 0)
				p.ang += GlobalMembersGAMEDEF.getincangle(p.ang, getangle(sprite[p.actorsqu].x-p.posx,sprite[p.actorsqu].y-p.posy))>>2;
		}

		if (GlobalMembersPLAYER.doincrements(ref p) != 0)
			return;

		if(p.weapon_pos != 0)
		{
			if(p.weapon_pos == -9)
			{
				if(p.last_weapon >= 0)
				{
					p.weapon_pos = 10;
	//                if(p->curr_weapon == KNEE_WEAPON) *kb = 1;
					p.last_weapon = -1;
				}
				else if(p.holster_weapon == 0)
					p.weapon_pos = 10;
			}
			else
				p.weapon_pos--;
		}

		// HACKS

		SHOOTINCODE:

		if(p.curr_weapon == SHRINKER_WEAPON || p.curr_weapon == GROW_WEAPON)
			p.random_club_frame += 64; // Glowing

		if(p.rapid_fire_hold == 1)
		{
			if(sb_snum&(1<<2))
				return;
			p.rapid_fire_hold = 0;
		}

		if(shrunk != 0 || p.tipincs || p.access_incs)
			sb_snum &= ~(1<<2);
		else if (shrunk == 0 && (sb_snum&(1<<2)) && (kb) == 0 && p.fist_incs == 0 && p.last_weapon == -1 && (p.weapon_pos == 0 || p.holster_weapon == 1))
		{

			p.crack_time = 777;

			if(p.holster_weapon == 1)
			{
				if(p.last_pissed_time <= (26 *218) && p.weapon_pos == -9)
				{
					p.holster_weapon = 0;
					p.weapon_pos = 10;
					GlobalMembersGAME.FTA(74, ref p);
				}
			}
			else
				switch(p.curr_weapon)
			{
				case HANDBOMB_WEAPON:
					p.hbomb_hold_delay = 0;
					if(p.ammo_amount[HANDBOMB_WEAPON] > 0)
						kb = 1;
					break;
				case HANDREMOTE_WEAPON:
					p.hbomb_hold_delay = 0;
					kb = 1;
					break;

				case PISTOL_WEAPON:
					if(p.ammo_amount[PISTOL_WEAPON] > 0)
					{
						p.ammo_amount[PISTOL_WEAPON]--;
						kb = 1;
					}
					break;


				case CHAINGUN_WEAPON:
					if(p.ammo_amount[CHAINGUN_WEAPON] > 0) // && p->random_club_frame == 0)
						kb = 1;
					break;

				case SHOTGUN_WEAPON:
					if(p.ammo_amount[SHOTGUN_WEAPON] > 0 && p.random_club_frame == 0)
						kb = 1;
					break;
	#if ! VOLUMEONE
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case TRIPBOMB_WEAPON:
					if (p.ammo_amount[TRIPBOMB_WEAPON] > 0)
					{
						int sx;
						int sy;
						int sz;
						short sect;
						short hw;
						short hitsp;

						hitscan(p.posx, p.posy, p.posz, p.cursectnum, sintable[(p.ang+512)&2047], sintable[p.ang &2047], (100-p.horiz-p.horizoff)*32, sect, hw, hitsp, sx, sy, sz, CLIPMASK1);

						if(sect < 0 || hitsp >= 0)
							break;

						if(hw >= 0 && sector[sect].lotag > 2)
							break;

						if(hw >= 0 && wall[hw].overpicnum >= 0)
							if(wall[hw].overpicnum == BIGFORCE)
								break;

						j = headspritesect[sect];
						while(j >= 0)
						{
							if(sprite[j].picnum == TRIPBOMB && klabs(sprite[j].z-sz) < (12<<8) && ((sprite[j].x-sx)*(sprite[j].x-sx)+(sprite[j].y-sy)*(sprite[j].y-sy)) < (290 *290))
										break;
							j = nextspritesect[j];
						}

						if(j == -1 && hw >= 0 && (wall[hw].cstat &16) == 0)
							if((wall[hw].nextsector >= 0 && sector[wall[hw].nextsector].lotag <= 2) || (wall[hw].nextsector == -1 && sector[sect].lotag <= 2))
								if(((sx-p.posx)*(sx-p.posx) + (sy-p.posy)*(sy-p.posy)) < (290 *290))
						{
							p.posz = p.oposz;
							p.poszv = 0;
							kb = 1;
						}
					}
					break;

				case SHRINKER_WEAPON:
				case GROW_WEAPON:
					if(p.curr_weapon == GROW_WEAPON)
					{
						if(p.ammo_amount[GROW_WEAPON] > 0)
						{
							kb = 1;
							GlobalMembersSOUNDS.spritesound(EXPANDERSHOOT, pi);
						}
					}
					else if(p.ammo_amount[SHRINKER_WEAPON] > 0)
					{
						kb = 1;
						GlobalMembersSOUNDS.spritesound(SHRINKER_FIRE, pi);
					}
					break;

				case FREEZE_WEAPON:
					if(p.ammo_amount[FREEZE_WEAPON] > 0)
					{
						kb = 1;
						GlobalMembersSOUNDS.spritesound(CAT_FIRE, pi);
					}
					break;
				case DEVISTATOR_WEAPON:
					if(p.ammo_amount[DEVISTATOR_WEAPON] > 0)
					{
						kb = 1;
						p.hbomb_hold_delay = !p.hbomb_hold_delay;
						GlobalMembersSOUNDS.spritesound(CAT_FIRE, pi);
					}
					break;

	#endif
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case RPG_WEAPON:
					if (p.ammo_amount[RPG_WEAPON] > 0)
						kb = 1;
					break;

				case KNEE_WEAPON:
					if(p.quick_kick == 0)
						kb = 1;
					break;
			}
		}
		else if((kb))
		{
			switch(p.curr_weapon)
			{
				case HANDBOMB_WEAPON:

					if((kb) == 6 && (sb_snum&(1<<2)))
					{
						p.rapid_fire_hold = 1;
						break;
					}
					kb++;
					if((kb)==12)
					{
						p.ammo_amount[HANDBOMB_WEAPON]--;

						if(p.on_ground && (sb_snum &2))
						{
							k = 15;
							i = ((p.horiz+p.horizoff-100)*20);
						}
						else
						{
							k = 140;
							i = -512-((p.horiz+p.horizoff-100)*20);
						}

						j = GlobalMembersGAME.EGS(p.cursectnum, p.posx+(sintable[(p.ang+512)&2047]>>6), p.posy+(sintable[p.ang &2047]>>6), p.posz, HEAVYHBOMB, -16, 9, 9, p.ang, (k+(p.hbomb_hold_delay<<5)), i, pi, 1);

						if(k == 15)
						{
							sprite[j].yvel = 3;
							sprite[j].z += (8<<8);
						}

						k = GlobalMembersPLAYER.hits(pi);
						if(k < 512)
						{
							sprite[j].ang += 1024;
							sprite[j].zvel /= 3;
							sprite[j].xvel /= 3;
						}

						p.hbomb_on = 1;

					}
					else if((kb) < 12 && (sb_snum&(1<<2)))
						p.hbomb_hold_delay++;
					else if((kb) > 19)
					{
						kb = 0;
						p.curr_weapon = HANDREMOTE_WEAPON;
						p.last_weapon = -1;
						p.weapon_pos = 10;
					}

					break;


				case HANDREMOTE_WEAPON:

					kb++;

					if((kb) == 2)
					{
						p.hbomb_on = 0;
					}

					if((kb) == 10)
					{
						kb = 0;
						if(p.ammo_amount[HANDBOMB_WEAPON] > 0)
							GlobalMembersACTORS.addweapon(ref p, HANDBOMB_WEAPON);
						else
							GlobalMembersACTORS.checkavailweapon(ref p);
					}
					break;

				case PISTOL_WEAPON:
					if((kb)==1)
					{
						GlobalMembersPLAYER.shoot(pi, SHOTSPARK1);
						GlobalMembersSOUNDS.spritesound(PISTOL_FIRE, pi);

						lastvisinc = totalclock+32;
						p.visibility = 0;
					}
					else if((kb) == 2)
						GlobalMembersGAME.spawn(pi, SHELL);

					kb++;

					if((kb) >= 5)
					{
						if(p.ammo_amount[PISTOL_WEAPON] <= 0 || (p.ammo_amount[PISTOL_WEAPON]%12))
						{
							kb = 0;
							GlobalMembersACTORS.checkavailweapon(ref p);
						}
						else
						{
							switch((kb))
							{
								case 5:
									GlobalMembersSOUNDS.spritesound(EJECT_CLIP, pi);
									break;
								case 8:
									GlobalMembersSOUNDS.spritesound(INSERT_CLIP, pi);
									break;
							}
						}
					}

					if((kb) == 27)
					{
						kb = 0;
						GlobalMembersACTORS.checkavailweapon(ref p);
					}

					break;

				case SHOTGUN_WEAPON:

					kb++;

					if(kb == 4)
					{
						GlobalMembersPLAYER.shoot(pi, SHOTGUN);
						GlobalMembersPLAYER.shoot(pi, SHOTGUN);
						GlobalMembersPLAYER.shoot(pi, SHOTGUN);
						GlobalMembersPLAYER.shoot(pi, SHOTGUN);
						GlobalMembersPLAYER.shoot(pi, SHOTGUN);
						GlobalMembersPLAYER.shoot(pi, SHOTGUN);
						GlobalMembersPLAYER.shoot(pi, SHOTGUN);

						p.ammo_amount[SHOTGUN_WEAPON]--;

						GlobalMembersSOUNDS.spritesound(SHOTGUN_FIRE, pi);

						lastvisinc = totalclock+32;
						p.visibility = 0;
					}

					switch(kb)
					{
						case 13:
							GlobalMembersACTORS.checkavailweapon(ref p);
							break;
						case 15:
							GlobalMembersSOUNDS.spritesound(SHOTGUN_COCK, pi);
							break;
						case 17:
						case 20:
							p.kickback_pic++;
							break;
						case 24:
							j = GlobalMembersGAME.spawn(pi, SHOTGUNSHELL);
							sprite[j].ang += 1024;
							GlobalMembersACTORS.ssp(j, CLIPMASK0);
							sprite[j].ang += 1024;
							p.kickback_pic++;
							break;
						case 31:
							kb = 0;
							return;
					}
					break;

				case CHAINGUN_WEAPON:

					kb++;

					if(*(kb) <= 12)
					{
						if(((*(kb))%3) == 0)
						{
							p.ammo_amount[CHAINGUN_WEAPON]--;

							if((*(kb)%3) == 0)
							{
								j = GlobalMembersGAME.spawn(pi, SHELL);

								sprite[j].ang += 1024;
								sprite[j].ang &= 2047;
								sprite[j].xvel += 32;
								sprite[j].z += (3<<8);
								GlobalMembersACTORS.ssp(j, CLIPMASK0);
							}

							GlobalMembersSOUNDS.spritesound(CHAINGUN_FIRE, pi);
							GlobalMembersPLAYER.shoot(pi, CHAINGUN);
							lastvisinc = totalclock+32;
							p.visibility = 0;
							GlobalMembersACTORS.checkavailweapon(ref p);

							if((sb_snum&(1<<2)) == 0)
							{
								kb = 0;
								break;
							}
						}
					}
					else if((kb) > 10)
					{
						if(sb_snum&(1<<2))
							kb = 1;
						else
							kb = 0;
					}

					break;

				case SHRINKER_WEAPON:
				case GROW_WEAPON:

					if(p.curr_weapon == GROW_WEAPON)
					{
						if((kb) > 3)
						{
							kb = 0;
							if(GlobalMembersGLOBAL.screenpeek == snum)
								GlobalMembersGLOBAL.pus = 1;
							p.ammo_amount[GROW_WEAPON]--;
							GlobalMembersPLAYER.shoot(pi, GROWSPARK);

							p.visibility = 0;
							lastvisinc = totalclock+32;
							GlobalMembersACTORS.checkavailweapon(ref p);
						}
						else
							kb++;
					}
					else
					{
						if((kb) > 10)
						{
							kb = 0;

							p.ammo_amount[SHRINKER_WEAPON]--;
							GlobalMembersPLAYER.shoot(pi, SHRINKER);

							p.visibility = 0;
							lastvisinc = totalclock+32;
							GlobalMembersACTORS.checkavailweapon(ref p);
						}
						else
							kb++;
					}
					break;

				case DEVISTATOR_WEAPON:
					if (kb != 0)
					{
						kb++;

						if((kb) & 1)
						{
							p.visibility = 0;
							lastvisinc = totalclock+32;
							GlobalMembersPLAYER.shoot(pi, RPG);
							p.ammo_amount[DEVISTATOR_WEAPON]--;
							GlobalMembersACTORS.checkavailweapon(ref p);
						}
						if((kb) > 5)
							kb = 0;
					}
					break;
				case FREEZE_WEAPON:

					if((kb) < 4)
					{
						kb++;
						if((kb) == 3)
						{
							p.ammo_amount[FREEZE_WEAPON]--;
							p.visibility = 0;
							lastvisinc = totalclock+32;
							GlobalMembersPLAYER.shoot(pi, FREEZEBLAST);
							GlobalMembersACTORS.checkavailweapon(ref p);
						}
						if(s.xrepeat < 32)
							{
								kb = 0;
								break;
							}
					}
					else
					{
						if(sb_snum&(1<<2))
						{
							kb = 1;
							GlobalMembersSOUNDS.spritesound(CAT_FIRE, pi);
						}
						else
							kb = 0;
					}
					break;

				case TRIPBOMB_WEAPON:
					if(kb < 4)
					{
						p.posz = p.oposz;
						p.poszv = 0;
						if((kb) == 3)
							GlobalMembersPLAYER.shoot(pi, HANDHOLDINGLASER);
					}
					if((kb) == 16)
					{
						kb = 0;
						GlobalMembersACTORS.checkavailweapon(ref p);
						p.weapon_pos = -9;
					}
					else
						kb++;
					break;
				case KNEE_WEAPON:
					kb++;

					if((kb) == 7)
						GlobalMembersPLAYER.shoot(pi, KNEE);
					else if((kb) == 14)
					{
						if(sb_snum&(1<<2))
							kb = 1+(TRAND &3);
						else
							kb = 0;
					}

					if(p.wantweaponfire >= 0)
						GlobalMembersACTORS.checkavailweapon(ref p);
					break;

				case RPG_WEAPON:
					kb++;
					if((kb) == 4)
					{
						p.ammo_amount[RPG_WEAPON]--;
						lastvisinc = totalclock+32;
						p.visibility = 0;
						GlobalMembersPLAYER.shoot(pi, RPG);
						GlobalMembersACTORS.checkavailweapon(ref p);
					}
					else if(kb == 20)
						kb = 0;
					break;
			}
		}
	}



	//UPDATE THIS FILE OVER THE OLD GETSPRITESCORE/COMPUTERGETINPUT FUNCTIONS
	public static getspritescore(int snum, int dapicnum)
	{
		switch(dapicnum)
		{
			case FIRSTGUNSPRITE:
				return(5);
			case CHAINGUNSPRITE:
				return(50);
			case RPGSPRITE:
				return(200);
			case FREEZESPRITE:
				return(25);
			case SHRINKERSPRITE:
				return(80);
			case HEAVYHBOMB:
				return(60);
			case TRIPBOMBSPRITE:
				return(50);
			case SHOTGUNSPRITE:
				return(120);
			case DEVISTATORSPRITE:
				return(120);

			case FREEZEAMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[FREEZE_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[FREEZE_WEAPON])
					return(10);
					else
						return(0);
			case AMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[SHOTGUN_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[SHOTGUN_WEAPON])
					return(10);
					else
						return(0);
			case BATTERYAMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[CHAINGUN_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[CHAINGUN_WEAPON])
					return(20);
					else
						return(0);
			case DEVISTATORAMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[DEVISTATOR_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[DEVISTATOR_WEAPON])
					return(25);
					else
						return(0);
			case RPGAMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[RPG_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[RPG_WEAPON])
					return(50);
					else
						return(0);
			case CRYSTALAMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[SHRINKER_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[SHRINKER_WEAPON])
					return(10);
					else
						return(0);
			case HBOMBAMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[HANDBOMB_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[HANDBOMB_WEAPON])
					return(30);
					else
						return(0);
			case SHOTGUNAMMO:
				if (GlobalMembersGLOBAL.ps[snum].ammo_amount[SHOTGUN_WEAPON] < GlobalMembersGLOBAL.max_ammo_amount[SHOTGUN_WEAPON])
					return(25);
					else
						return(0);

			case COLA:
				if (sprite[GlobalMembersGLOBAL.ps[snum].i].extra < 100)
					return(10);
					else
						return(0);
			case SIXPAK:
				if (sprite[GlobalMembersGLOBAL.ps[snum].i].extra < 100)
					return(30);
					else
						return(0);
			case FIRSTAID:
				if (GlobalMembersGLOBAL.ps[snum].firstaid_amount < 100)
					return(100);
					else
						return(0);
			case SHIELD:
				if (GlobalMembersGLOBAL.ps[snum].shield_amount < 100)
					return(50);
					else
						return(0);
			case STEROIDS:
				if (GlobalMembersGLOBAL.ps[snum].steroids_amount < 400)
					return(30);
					else
						return(0);
			case AIRTANK:
				if (GlobalMembersGLOBAL.ps[snum].scuba_amount < 6400)
					return(30);
					else
						return(0);
			case JETPACK:
				if (GlobalMembersGLOBAL.ps[snum].jetpack_amount < 1600)
					return(100);
					else
						return(0);
			case HEATSENSOR:
				if (GlobalMembersGLOBAL.ps[snum].heat_amount < 1200)
					return(10);
					else
						return(0);
			case ACCESSCARD:
				return(1);
			case BOOTS:
				if (GlobalMembersGLOBAL.ps[snum].boot_amount < 200)
					return(50);
					else
						return(0);
			case ATOMICHEALTH:
				if (sprite[GlobalMembersGLOBAL.ps[snum].i].extra < GlobalMembersGLOBAL.max_player_health)
					return(50);
					else
						return(0);
			case HOLODUKE:
				if (GlobalMembersGLOBAL.ps[snum].holoduke_amount < 2400)
					return(30);
					else
						return(0);
		}
		return(0);
	}

	internal static int[,] fdmatrix = {{128, -1, -1, -1, 128, -1, -1, -1, 128, -1, 128, -1}, {1024, 1024, 1024, 1024, 2560, 128, 2560, 2560, 1024, 2560, 2560, 2560}, {512, 512, 512, 512, 2560, 128, 2560, 2560, 1024, 2560, 2560, 2560}, {512, 512, 512, 512, 2560, 128, 2560, 2560, 1024, 2560, 2560, 2560}, {2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560}, {512, 512, 512, 512, 2048, 512, 2560, 2560, 512, 2560, 2560, 2560}, {128, 128, 128, 128, 2560, 128, 2560, 2560, 128, 128, 128, 128}, {1536, 1536, 1536, 1536, 2560, 1536, 1536, 1536, 1536, 1536, 1536, 1536}, {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}, {128, 128, 128, 128, 2560, 128, 2560, 2560, 128, 128, 128, 128}, {2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560, 2560}, {128, 128, 128, 128, 2560, 128, 2560, 2560, 128, 128, 128, 128}};
	 //KNEE PIST SHOT CHAIN RPG PIPE SHRI DEVI WALL FREE HAND EXPA

	internal static int[] goalx = new int[MAXPLAYERS];
	internal static int[] goaly = new int[MAXPLAYERS];
	internal static int[] goalz = new int[MAXPLAYERS];
	internal static int[] goalsect = new int[MAXPLAYERS];
	internal static int[] goalwall = new int[MAXPLAYERS];
	internal static int[] goalsprite = new int[MAXPLAYERS];
	internal static int[] goalplayer = new int[MAXPLAYERS];
	internal static int[] clipmovecount = new int[MAXPLAYERS];
	public static short[] searchsect = new short[MAXSECTORS];
	public static short[] searchparent = new short[MAXSECTORS];
	sbyte dashow2dsector[(MAXSECTORS+7)>>3];
	public static void computergetinput(int snum, ref input syn)
	{
		int i;
		int j;
		int k;
		int l;
		int x1;
		int y1;
		int z1;
		int x2;
		int y2;
		int z2;
		int x3;
		int y3;
		int z3;
		int dx;
		int dy;
		int dist;
		int daang;
		int zang;
		int fightdist;
		int damyang;
		int damysect;
		int startsect;
		int endsect;
		int splc;
		int send;
		int startwall;
		int endwall;
		short dasect;
		short dawall;
		short daspr;
		player_struct p;
		walltype wal;

		p = GlobalMembersGLOBAL.ps[snum];
		syn.fvel = 0;
		syn.svel = 0;
		syn.avel = 0;
		syn.horz = 0;
		syn.bits = 0;

		x1 = sprite[p.i].x;
		y1 = sprite[p.i].y;
		z1 = sprite[p.i].z;
		damyang = sprite[p.i].ang;
		damysect = sprite[p.i].sectnum;
		if ((numplayers >= 2) && (snum == myconnectindex))
			{
				x1 = GlobalMembersGLOBAL.myx;
				y1 = GlobalMembersGLOBAL.myy;
				z1 = GlobalMembersGLOBAL.myz+PHEIGHT;
				damyang = GlobalMembersGLOBAL.myang;
				damysect = GlobalMembersGLOBAL.mycursectnum;
			}

		if (!(numframes &7))
		{
			if (!cansee(x1,y1,z1-(48<<8),damysect,x2,y2,z2-(48<<8),sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].sectnum))
				goalplayer[snum] = snum;
		}

		if ((goalplayer[snum] == snum) || (GlobalMembersGLOBAL.ps[goalplayer[snum]].dead_flag != 0))
		{
			j = 0x7fffffff;
			for(i = connecthead;i>=0;i = connectpoint2[i])
				if (i != snum)
				{
					dist = ksqrt((sprite[GlobalMembersGLOBAL.ps[i].i].x-x1)*(sprite[GlobalMembersGLOBAL.ps[i].i].x-x1)+(sprite[GlobalMembersGLOBAL.ps[i].i].y-y1)*(sprite[GlobalMembersGLOBAL.ps[i].i].y-y1));

					x2 = sprite[GlobalMembersGLOBAL.ps[i].i].x;
					y2 = sprite[GlobalMembersGLOBAL.ps[i].i].y;
					z2 = sprite[GlobalMembersGLOBAL.ps[i].i].z;
					if (!cansee(x1,y1,z1-(48<<8),damysect,x2,y2,z2-(48<<8),sprite[GlobalMembersGLOBAL.ps[i].i].sectnum))
						dist <<= 1;

					if (dist < j)
					{
						j = dist;
						goalplayer[snum] = i;
					}
				}
		}

		x2 = sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].x;
		y2 = sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].y;
		z2 = sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].z;

		if (p.dead_flag)
			syn.bits |= (1<<29);
		if ((p.firstaid_amount > 0) && (p.last_extra < 100))
			syn.bits |= (1<<16);

		for(j = headspritestat[4];j>=0;j = nextspritestat[j])
		{
			switch (sprite[j].picnum)
			{
				case TONGUE:
					k = 4;
					break;
				case FREEZEBLAST:
					k = 4;
					break;
				case SHRINKSPARK:
					k = 16;
					break;
				case RPG:
					k = 16;
					break;
				default:
					k = 0;
					break;
			}
			if (k != 0)
			{
				x3 = sprite[j].x;
				y3 = sprite[j].y;
				z3 = sprite[j].z;
				for(l = 0;l<=8;l++)
				{
					if (tmulscale11(x3-x1,x3-x1,y3-y1,y3-y1,(z3-z1)>>4,(z3-z1)>>4) < 3072)
					{
						dx = sintable[(sprite[j].ang+512)&2047];
						dy = sintable[sprite[j].ang &2047];
						if ((x1-x3)*dy > (y1-y3)*dx)
							i = -k *512;
							else
								i = k *512;
						syn.fvel -= mulscale17(dy,i);
						syn.svel += mulscale17(dx,i);
					}
					if (l < 7)
					{
						x3 += (mulscale14(sprite[j].xvel, sintable[(sprite[j].ang+512)&2047])<<2);
						y3 += (mulscale14(sprite[j].xvel, sintable[sprite[j].ang &2047])<<2);
						z3 += (sprite[j].zvel<<2);
					}
					else
					{
						hitscan(sprite[j].x, sprite[j].y, sprite[j].z, sprite[j].sectnum, mulscale14(sprite[j].xvel, sintable[(sprite[j].ang+512)&2047]), mulscale14(sprite[j].xvel, sintable[sprite[j].ang &2047]), (int)sprite[j].zvel, dasect, dawall, daspr, x3, y3, z3, CLIPMASK1);
					}
				}
			}
		}

		if ((GlobalMembersGLOBAL.ps[goalplayer[snum]].dead_flag == 0) && ((cansee(x1,y1,z1,damysect,x2,y2,z2,sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].sectnum)) || (cansee(x1,y1,z1-(24<<8),damysect,x2,y2,z2-(24<<8),sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].sectnum)) || (cansee(x1,y1,z1-(48<<8),damysect,x2,y2,z2-(48<<8),sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].sectnum))))
		{
			syn.bits |= (1<<2);

			if ((p.curr_weapon == HANDBOMB_WEAPON) && (!(RandomNumbers.NextNumber()&7)))
				syn.bits &= ~(1<<2);

			if (p.curr_weapon == TRIPBOMB_WEAPON)
				syn.bits |= ((RandomNumbers.NextNumber()%MAX_WEAPONS)<<8);

			if (p.curr_weapon == RPG_WEAPON)
			{
				hitscan(x1, y1, z1-PHEIGHT, damysect, sintable[(damyang+512)&2047], sintable[damyang &2047], (100-p.horiz-p.horizoff)*32, dasect, dawall, daspr, x3, y3, z3, CLIPMASK1);
				if ((x3-x1)*(x3-x1)+(y3-y1)*(y3-y1) < 2560 *2560)
					syn.bits &= ~(1<<2);
			}


			fightdist = fdmatrix[p.curr_weapon][GlobalMembersGLOBAL.ps[goalplayer[snum]].curr_weapon];
			if (fightdist < 128)
				fightdist = 128;
			dist = ksqrt((x2-x1)*(x2-x1)+(y2-y1)*(y2-y1));
			if (dist == 0)
				dist = 1;
			daang = getangle(x2+(GlobalMembersGLOBAL.ps[goalplayer[snum]].posxv>>14)-x1,y2+(GlobalMembersGLOBAL.ps[goalplayer[snum]].posyv>>14)-y1);
			zang = 100-((z2-z1)*8)/dist;
			fightdist = max(fightdist,(klabs(z2-z1)>>4));

			if (sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].yrepeat < 32)
				{
					fightdist = 0;
					syn.bits &= ~(1<<2);
				}
			if (sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].pal == 1)
				{
					fightdist = 0;
					syn.bits &= ~(1<<2);
				}

			if (dist < 256)
				syn.bits |= (1<<22);

			x3 = x2+((x1-x2)*fightdist/dist);
			y3 = y2+((y1-y2)*fightdist/dist);
			syn.fvel += (x3-x1)*2047/dist;
			syn.svel += (y3-y1)*2047/dist;

				//Strafe attack
			if (fightdist != 0)
			{
				j = totalclock+snum *13468;
				i = sintable[(j<<6)&2047];
				i += sintable[((j+4245)<<5)&2047];
				i += sintable[((j+6745)<<4)&2047];
				i += sintable[((j+15685)<<3)&2047];
				dx = sintable[(sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].ang+512)&2047];
				dy = sintable[sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].ang &2047];
				if ((x1-x2)*dy > (y1-y2)*dx)
					i += 8192;
					else
						i -= 8192;
				syn.fvel += ((sintable[(daang+1024)&2047]*i)>>17);
				syn.svel += ((sintable[(daang+512)&2047]*i)>>17);
			}

			syn.avel = min(max((((daang+1024-damyang)&2047)-1024)>>1, -127), 127);
			syn.horz = min(max((zang-p.horiz)>>1,-DefineConstants.MAXHORIZ),DefineConstants.MAXHORIZ);
			syn.bits |= (1<<23);
			return;
		}

		goalsect[snum] = -1;
		if (goalsect[snum] < 0)
		{
			goalwall[snum] = -1;
			startsect = sprite[p.i].sectnum;
			endsect = sprite[GlobalMembersGLOBAL.ps[goalplayer[snum]].i].sectnum;

			clearbufbyte(dashow2dsector,(MAXSECTORS+7)>>3,0);
			searchsect[0] = startsect;
			searchparent[0] = -1;
			dashow2dsector[startsect>>3] |= (1<<(startsect &7));
			for(splc = 0,send = 1;splc<send;splc++)
			{
				startwall = sector[searchsect[splc]].wallptr;
				endwall = startwall + sector[searchsect[splc]].wallnum;
				for(i = startwall, wal = wall[startwall];i<endwall;i++, wal++)
				{
					j = wal.nextsector;
					if (j < 0)
						continue;

					dx = ((wall[wal.point2].x+wal.x)>>1);
					dy = ((wall[wal.point2].y+wal.y)>>1);
					if ((getceilzofslope(j,dx,dy) > getflorzofslope(j,dx,dy)-(28<<8)) && ((sector[j].lotag < 15) || (sector[j].lotag > 22)))
						continue;
					if (getflorzofslope(j,dx,dy) < getflorzofslope(searchsect[splc],dx,dy)-(72<<8))
						continue;
					if ((dashow2dsector[j>>3]&(1<<(j &7))) == 0)
					{
						dashow2dsector[j>>3] |= (1<<(j &7));
						searchsect[send] = (short)j;
						searchparent[send] = (short)splc;
						send++;
						if (j == endsect)
						{
							clearbufbyte(dashow2dsector,(MAXSECTORS+7)>>3,0);
							for(k = send-1;k>=0;k = searchparent[k])
								dashow2dsector[searchsect[k]>>3] |= (1<<(searchsect[k]&7));

							for(k = send-1;k>=0;k = searchparent[k])
								if (!searchparent[k])
									break;

							goalsect[snum] = searchsect[k];
							startwall = sector[goalsect[snum]].wallptr;
							endwall = startwall+sector[goalsect[snum]].wallnum;
							x3 = y3 = 0;
							for(i = startwall;i<endwall;i++)
							{
								x3 += wall[i].x;
								y3 += wall[i].y;
							}
							x3 /= (endwall-startwall);
							y3 /= (endwall-startwall);

							startwall = sector[startsect].wallptr;
							endwall = startwall+sector[startsect].wallnum;
							l = 0;
							k = startwall;
							for(i = startwall;i<endwall;i++)
							{
								if (wall[i].nextsector != goalsect[snum])
									continue;
								dx = wall[wall[i].point2].x-wall[i].x;
								dy = wall[wall[i].point2].y-wall[i].y;

								//if (dx*(y1-wall[i].y) <= dy*(x1-wall[i].x))
								//   if (dx*(y2-wall[i].y) >= dy*(x2-wall[i].x))
										if ((x3-x1)*(wall[i].y-y1) <= (y3-y1)*(wall[i].x-x1))
											if ((x3-x1)*(wall[wall[i].point2].y-y1) >= (y3-y1)*(wall[wall[i].point2].x-x1))
												{
													k = i;
													break;
												}

								dist = ksqrt(dx *dx+dy *dy);
								if (dist > l)
								{
									l = dist;
									k = i;
								}
							}
							goalwall[snum] = k;
							daang = ((getangle(wall[wall[k].point2].x-wall[k].x,wall[wall[k].point2].y-wall[k].y)+1536)&2047);
							goalx[snum] = ((wall[k].x+wall[wall[k].point2].x)>>1)+(sintable[(daang+512)&2047]>>8);
							goaly[snum] = ((wall[k].y+wall[wall[k].point2].y)>>1)+(sintable[daang &2047]>>8);
							goalz[snum] = sector[goalsect[snum]].floorz-(32<<8);
							break;
						}
					}
				}

				for(i = headspritesect[searchsect[splc]];i>=0;i = nextspritesect[i])
					if (sprite[i].lotag == 7)
					{
						j = sprite[sprite[i].owner].sectnum;
						if ((dashow2dsector[j>>3]&(1<<(j &7))) == 0)
						{
							dashow2dsector[j>>3] |= (1<<(j &7));
							searchsect[send] = (short)j;
							searchparent[send] = (short)splc;
							send++;
							if (j == endsect)
							{
								clearbufbyte(dashow2dsector,(MAXSECTORS+7)>>3,0);
								for(k = send-1;k>=0;k = searchparent[k])
									dashow2dsector[searchsect[k]>>3] |= (1<<(searchsect[k]&7));

								for(k = send-1;k>=0;k = searchparent[k])
									if (!searchparent[k])
										break;

								goalsect[snum] = searchsect[k];
								startwall = sector[startsect].wallptr;
								endwall = startwall+sector[startsect].wallnum;
								l = 0;
								k = startwall;
								for(i = startwall;i<endwall;i++)
								{
									dx = wall[wall[i].point2].x-wall[i].x;
									dy = wall[wall[i].point2].y-wall[i].y;
									dist = ksqrt(dx *dx+dy *dy);
									if ((wall[i].nextsector == goalsect[snum]) && (dist > l))
										{
											l = dist;
											k = i;
										}
								}
								goalwall[snum] = k;
								daang = ((getangle(wall[wall[k].point2].x-wall[k].x,wall[wall[k].point2].y-wall[k].y)+1536)&2047);
								goalx[snum] = ((wall[k].x+wall[wall[k].point2].x)>>1)+(sintable[(daang+512)&2047]>>8);
								goaly[snum] = ((wall[k].y+wall[wall[k].point2].y)>>1)+(sintable[daang &2047]>>8);
								goalz[snum] = sector[goalsect[snum]].floorz-(32<<8);
								break;
							}
						}
					}
				if (goalwall[snum] >= 0)
					break;
			}
		}

		if ((goalsect[snum] < 0) || (goalwall[snum] < 0))
		{
			if (goalsprite[snum] < 0)
			{
				for(k = 0;k<4;k++)
				{
					i = (RandomNumbers.NextNumber()%numsectors);
					for(j = headspritesect[i];j>=0;j = nextspritesect[j])
					{
						if ((sprite[j].xrepeat <= 0) || (sprite[j].yrepeat <= 0))
							continue;
						if (GlobalMembersPLAYER.getspritescore(snum,sprite[j].picnum) <= 0)
							continue;
						if (cansee(x1,y1,z1-(32<<8),damysect,sprite[j].x,sprite[j].y,sprite[j].z-(4<<8),i))
							{
								goalx[snum] = sprite[j].x;
								goaly[snum] = sprite[j].y;
								goalz[snum] = sprite[j].z;
								goalsprite[snum] = j;
								break;
							}
					}
				}
			}
			x2 = goalx[snum];
			y2 = goaly[snum];
			dist = ksqrt((x2-x1)*(x2-x1)+(y2-y1)*(y2-y1));
			if (dist == 0)
				return;
			daang = getangle(x2-x1,y2-y1);
			syn.fvel += (x2-x1)*2047/dist;
			syn.svel += (y2-y1)*2047/dist;
			syn.avel = min(max((((daang+1024-damyang)&2047)-1024)>>3, -127), 127);
		}
		else
			goalsprite[snum] = -1;

		x3 = p.posx;
		y3 = p.posy;
		z3 = p.posz;
		dasect = p.cursectnum;
		i = clipmove(x3, y3, z3, dasect, p.posxv, p.posyv, 164, 4<<8, 4<<8, CLIPMASK0);
		if (i == 0)
		{
			x3 = p.posx;
			y3 = p.posy;
			z3 = p.posz+(24<<8);
			dasect = p.cursectnum;
			i = clipmove(x3, y3, z3, dasect, p.posxv, p.posyv, 164, 4<<8, 4<<8, CLIPMASK0);
		}
		if (i != 0)
		{
			clipmovecount[snum]++;

			j = 0;
			if ((i &0xc000) == 32768) //Hit a wall (49152 for sprite)
				if (wall[i&(MAXWALLS-1)].nextsector >= 0)
				{
					if (getflorzofslope(wall[i&(MAXWALLS-1)].nextsector, p.posx, p.posy) <= p.posz+(24<<8))
						j |= 1;
					if (getceilzofslope(wall[i&(MAXWALLS-1)].nextsector, p.posx, p.posy) >= p.posz-(24<<8))
						j |= 2;
				}
			if ((i &0xc000) == 49152)
				j = 1;
			if (j &1 != 0)
				if (clipmovecount[snum] == 4)
					syn.bits |= (1<<0);
			if (j &2 != 0)
				syn.bits |= (1<<1);

				//Strafe attack
			daang = getangle(x2-x1,y2-y1);
			if ((i &0xc000) == 32768)
				daang = getangle(wall[wall[i&(MAXWALLS-1)].point2].x-wall[i&(MAXWALLS-1)].x, wall[wall[i&(MAXWALLS-1)].point2].y-wall[i&(MAXWALLS-1)].y);
			j = totalclock+snum *13468;
			i = sintable[(j<<6)&2047];
			i += sintable[((j+4245)<<5)&2047];
			i += sintable[((j+6745)<<4)&2047];
			i += sintable[((j+15685)<<3)&2047];
			syn.fvel += ((sintable[(daang+1024)&2047]*i)>>17);
			syn.svel += ((sintable[(daang+512)&2047]*i)>>17);

			if ((clipmovecount[snum]&31) == 2)
				syn.bits |= (1<<29);
			if ((clipmovecount[snum]&31) == 17)
				syn.bits |= (1<<22);
			if (clipmovecount[snum] > 32)
			{
				goalsect[snum] = -1;
				goalwall[snum] = -1;
				clipmovecount[snum] = 0;
			}

			goalsprite[snum] = -1;
		}
		else
			clipmovecount[snum] = 0;

		if ((goalsect[snum] >= 0) && (goalwall[snum] >= 0))
		{
			x2 = goalx[snum];
			y2 = goaly[snum];
			dist = ksqrt((x2-x1)*(x2-x1)+(y2-y1)*(y2-y1));
			if (dist == 0)
				return;
			daang = getangle(x2-x1,y2-y1);
			if ((goalwall[snum] >= 0) && (dist < 4096))
				daang = ((getangle(wall[wall[goalwall[snum]].point2].x-wall[goalwall[snum]].x,wall[wall[goalwall[snum]].point2].y-wall[goalwall[snum]].y)+1536)&2047);
			syn.fvel += (x2-x1)*2047/dist;
			syn.svel += (y2-y1)*2047/dist;
			syn.avel = min(max((((daang+1024-damyang)&2047)-1024)>>3, -127), 127);
		}
	}
}

