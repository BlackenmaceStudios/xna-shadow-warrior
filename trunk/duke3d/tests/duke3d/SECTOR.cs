public static class GlobalMembersSECTOR
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


	// PRIMITIVE


	public static sbyte haltsoundhack;
	public static short callsound(short sn,short whatsprite)
	{
		short i;

		if(haltsoundhack)
		{
			haltsoundhack = 0;
			return -1;
		}

		i = headspritesect[sn];
		while(i >= 0)
		{
			if(PN == MUSICANDSFX && SLT < 1000)
			{
				if(whatsprite == -1)
					whatsprite = i;

				if(T1 == 0)
				{
					if((GlobalMembersGLOBAL.soundm[SLT]&16) == 0)
					{
						if(SLT)
						{
							GlobalMembersSOUNDS.spritesound(SLT, whatsprite);
							if(SHT && SLT != SHT && SHT < NUM_SOUNDS)
								GlobalMembersSOUNDS.stopsound(SHT);
						}

						if((sector[SECT].lotag &0xff) != 22)
							T1 = 1;
					}
				}
				else if(SHT < NUM_SOUNDS)
				{
					if(SHT)
						GlobalMembersSOUNDS.spritesound(SHT, whatsprite);
					if((GlobalMembersGLOBAL.soundm[SLT]&1) || (SHT && SHT != SLT))
						GlobalMembersSOUNDS.stopsound(SLT);
					T1 = 0;
				}
				return SLT;
			}
			i = nextspritesect[i];
		}
		return -1;
	}


	public static short check_activator_motion(short lotag)
	{
		short i;
		short j;
		spritetype s;

		i = headspritestat[8];
		while (i >= 0)
		{
			if (sprite[i].lotag == lotag)
			{
				s = sprite[i];

				for (j = GlobalMembersGLOBAL.animatecnt-1; j >= 0; j--)
					if (s.sectnum == GlobalMembersGLOBAL.animatesect[j])
						return(1);

				j = headspritestat[3];
				while (j >= 0)
				{
					if(s.sectnum == sprite[j].sectnum)
						switch(sprite[j].lotag)
						{
							case 11:
							case 30:
								if (GlobalMembersGLOBAL.hittype[j].temp_data[4])
									return(1);
								break;
							case 20:
							case 31:
							case 32:
							case 18:
								if (GlobalMembersGLOBAL.hittype[j].temp_data[0])
									return(1);
								break;
						}

					j = nextspritestat[j];
				}
			}
			i = nextspritestat[i];
		}
		return(0);
	}

	public static sbyte isadoorwall(short dapic)
	{
		switch(dapic)
		{
			case DOORTILE1:
			case DOORTILE2:
			case DOORTILE3:
			case DOORTILE4:
			case DOORTILE5:
			case DOORTILE6:
			case DOORTILE7:
			case DOORTILE8:
			case DOORTILE9:
			case DOORTILE10:
			case DOORTILE11:
			case DOORTILE12:
			case DOORTILE14:
			case DOORTILE15:
			case DOORTILE16:
			case DOORTILE17:
			case DOORTILE18:
			case DOORTILE19:
			case DOORTILE20:
			case DOORTILE21:
			case DOORTILE22:
			case DOORTILE23:
				return 1;
		}
		return 0;
	}


	public static sbyte isanunderoperator(short lotag)
	{
		switch(lotag &0xff)
		{
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 22:
			case 26:
				return 1;
		}
		return 0;
	}

	public static sbyte isanearoperator(short lotag)
	{
		switch(lotag &0xff)
		{
			case 9:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 25:
			case 26:
			case 29: //Toothed door
				return 1;
		}
		return 0;
	}

	public static short checkcursectnums(short sect)
	{
		short i;
		for(i = connecthead;i>=0;i = connectpoint2[i])
			if(sprite[GlobalMembersGLOBAL.ps[i].i].sectnum == sect)
				return i;
		return -1;
	}

	public static int ldist(ref spritetype s1, ref spritetype s2)
	{
		int vx;
		int vy;
		vx = s1.x - s2.x;
		vy = s1.y - s2.y;
		return(FindDistance2D(vx,vy) + 1);
	}

	public static int dist(ref spritetype s1, ref spritetype s2)
	{
		int vx;
		int vy;
		int vz;
		vx = s1.x - s2.x;
		vy = s1.y - s2.y;
		vz = s1.z - s2.z;
		return(FindDistance3D(vx,vy,vz>>4));
	}

	public static short findplayer(ref spritetype s, ref int d)
	{
		short j;
		short closest_player;
		int x;
		int closest;

		if(GlobalMembersGLOBAL.ud.multimode < 2)
		{
			d = klabs(GlobalMembersGLOBAL.ps[myconnectindex].oposx-s.x) + klabs(GlobalMembersGLOBAL.ps[myconnectindex].oposy-s.y) + ((klabs(GlobalMembersGLOBAL.ps[myconnectindex].oposz-s.z+(28<<8)))>>4);
			return myconnectindex;
		}

		closest = 0x7fffffff;
		closest_player = 0;

		for(j = connecthead;j>=0;j = connectpoint2[j])
		{
			x = klabs(GlobalMembersGLOBAL.ps[j].oposx-s.x) + klabs(GlobalMembersGLOBAL.ps[j].oposy-s.y) + ((klabs(GlobalMembersGLOBAL.ps[j].oposz-s.z+(28<<8)))>>4);
			if(x < closest && sprite[GlobalMembersGLOBAL.ps[j].i].extra > 0)
			{
				closest_player = j;
				closest = x;
			}
		}

		d = closest;
		return closest_player;
	}

	public static short findotherplayer(short p, ref int d)
	{
		short j;
		short closest_player;
		int x;
		int closest;

		closest = 0x7fffffff;
		closest_player = p;

		for(j = connecthead;j>=0;j = connectpoint2[j])
			if(p != j && sprite[GlobalMembersGLOBAL.ps[j].i].extra > 0)
		{
			x = klabs(GlobalMembersGLOBAL.ps[j].oposx-GlobalMembersGLOBAL.ps[p].posx) + klabs(GlobalMembersGLOBAL.ps[j].oposy-GlobalMembersGLOBAL.ps[p].posy) + (klabs(GlobalMembersGLOBAL.ps[j].oposz-GlobalMembersGLOBAL.ps[p].posz)>>4);

			if(x < closest)
			{
				closest_player = j;
				closest = x;
			}
		}

		d = closest;
		return closest_player;
	}



	public static void doanimations()
	{
		int i;
		int j;
		int a;
		int p;
		int v;
		int dasect;

		for(i = GlobalMembersGLOBAL.animatecnt-1;i>=0;i--)
		{
			a = GlobalMembersGLOBAL.animateptr[i];
			v = GlobalMembersGLOBAL.animatevel[i]*TICSPERFRAME;
			dasect = GlobalMembersGLOBAL.animatesect[i];

			if (a == GlobalMembersGLOBAL.animategoal[i])
			{
				GlobalMembersACTORS.stopinterpolation(ref GlobalMembersGLOBAL.animateptr[i]);

				GlobalMembersGLOBAL.animatecnt--;
				GlobalMembersGLOBAL.animateptr[i] = GlobalMembersGLOBAL.animateptr[GlobalMembersGLOBAL.animatecnt];
				GlobalMembersGLOBAL.animategoal[i] = GlobalMembersGLOBAL.animategoal[GlobalMembersGLOBAL.animatecnt];
				GlobalMembersGLOBAL.animatevel[i] = GlobalMembersGLOBAL.animatevel[GlobalMembersGLOBAL.animatecnt];
				GlobalMembersGLOBAL.animatesect[i] = GlobalMembersGLOBAL.animatesect[GlobalMembersGLOBAL.animatecnt];
				if(sector[GlobalMembersGLOBAL.animatesect[i]].lotag == 18 || sector[GlobalMembersGLOBAL.animatesect[i]].lotag == 19)
					if(GlobalMembersGLOBAL.animateptr[i] == &sector[GlobalMembersGLOBAL.animatesect[i]].ceilingz)
						continue;

			   if((sector[dasect].lotag &0xff) != 22)
					GlobalMembersSECTOR.callsound(dasect, -1);

				continue;
			}

			if (v > 0)
			{
				a = min(a+v,GlobalMembersGLOBAL.animategoal[i]);
			}
				  else
				  {
					  a = max(a+v,GlobalMembersGLOBAL.animategoal[i]);
				  }

			if(GlobalMembersGLOBAL.animateptr[i] == &sector[GlobalMembersGLOBAL.animatesect[i]].floorz)
			{
				for(p = connecthead;p>=0;p = connectpoint2[p])
					if (GlobalMembersGLOBAL.ps[p].cursectnum == dasect)
						if ((sector[dasect].floorz-GlobalMembersGLOBAL.ps[p].posz) < (64<<8))
							if (sprite[GlobalMembersGLOBAL.ps[p].i].owner >= 0)
				{
					GlobalMembersGLOBAL.ps[p].posz += v;
					GlobalMembersGLOBAL.ps[p].poszv = 0;
					if (p == myconnectindex)
					{
						GlobalMembersGLOBAL.myz += v;
						GlobalMembersGLOBAL.myzvel = 0;
						GlobalMembersGLOBAL.myzbak[((GlobalMembersGLOBAL.movefifoplc-1)&(MOVEFIFOSIZ-1))] = GlobalMembersGLOBAL.ps[p].posz;
					}
				}

				for(j = headspritesect[dasect];j>=0;j = nextspritesect[j])
					if (sprite[j].statnum != 3)
					{
						GlobalMembersGLOBAL.hittype[j].bposz = sprite[j].z;
						sprite[j].z += v;
						GlobalMembersGLOBAL.hittype[j].floorz = sector[dasect].floorz+v;
					}
			}

			GlobalMembersGLOBAL.animateptr[i] = a;
		}
	}

	public static getanimationgoal(ref int animptr)
	{
		int i;
		int j;

		j = -1;
		for(i = GlobalMembersGLOBAL.animatecnt-1;i>=0;i--)
			if (animptr == (int)GlobalMembersGLOBAL.animateptr[i])
			{
				j = i;
				break;
			}
		return(j);
	}

	public static setanimation(short animsect, ref int animptr, int thegoal, int thevel)
	{
		int i;
		int j;

		if (GlobalMembersGLOBAL.animatecnt >= MAXANIMATES-1)
			return(-1);

		j = GlobalMembersGLOBAL.animatecnt;
		for(i = 0;i<GlobalMembersGLOBAL.animatecnt;i++)
			if (animptr == GlobalMembersGLOBAL.animateptr[i])
			{
				j = i;
				break;
			}

		GlobalMembersGLOBAL.animatesect[j] = animsect;
		GlobalMembersGLOBAL.animateptr[j] = animptr;
		GlobalMembersGLOBAL.animategoal[j] = thegoal;
		if (thegoal >= animptr)
		   GlobalMembersGLOBAL.animatevel[j] = thevel;
		else
		   GlobalMembersGLOBAL.animatevel[j] = -thevel;

		if (j == GlobalMembersGLOBAL.animatecnt)
			GlobalMembersGLOBAL.animatecnt++;

		GlobalMembersACTORS.setinterpolation(ref animptr);

		return(j);
	}




	public static void animatecamsprite()
	{
		short i;

		if(GlobalMembersGLOBAL.camsprite <= 0)
			return;

		i = GlobalMembersGLOBAL.camsprite;

		if(T1 >= 11)
		{
			T1 = 0;

			if(GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].newowner >= 0)
				OW = GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].newowner;

			else if(OW >= 0 && GlobalMembersSECTOR.dist(ref sprite[GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].i], ref sprite[i]) < 2048)
				GlobalMembersPREMAP.xyzmirror(OW, PN);
		}
		else
			T1++;
	}

	public static void animatewalls()
	{
		int i;
		int j;
		int p;
		int t;

		for(p = 0;p < GlobalMembersGLOBAL.numanimwalls ;p++)
	//    for(p=numanimwalls-1;p>=0;p--)
		{
			i = GlobalMembersGLOBAL.animwall[p].wallnum;
			j = wall[i].picnum;

				switch(j)
				{
					case SCREENBREAK1:
					case SCREENBREAK2:
					case SCREENBREAK3:
					case SCREENBREAK4:
					case SCREENBREAK5:

					case SCREENBREAK9:
					case SCREENBREAK10:
					case SCREENBREAK11:
					case SCREENBREAK12:
					case SCREENBREAK13:
					case SCREENBREAK14:
					case SCREENBREAK15:
					case SCREENBREAK16:
					case SCREENBREAK17:
					case SCREENBREAK18:
					case SCREENBREAK19:

						if((TRAND &255) < 16)
						{
							GlobalMembersGLOBAL.animwall[p].tag = wall[i].picnum;
							wall[i].picnum = SCREENBREAK6;
						}

						continue;

					case SCREENBREAK6:
					case SCREENBREAK7:
					case SCREENBREAK8:

						if(GlobalMembersGLOBAL.animwall[p].tag >= 0 && wall[i].extra != FEMPIC2 && wall[i].extra != FEMPIC3)
							wall[i].picnum = GlobalMembersGLOBAL.animwall[p].tag;
						else
						{
							wall[i].picnum++;
							if(wall[i].picnum == (SCREENBREAK6+3))
								wall[i].picnum = SCREENBREAK6;
						}
						continue;

				}

			if(wall[i].cstat &16)
				switch(wall[i].overpicnum)
			{
				case W_FORCEFIELD:
				case W_FORCEFIELD+1:
				case W_FORCEFIELD+2:

					t = GlobalMembersGLOBAL.animwall[p].tag;

					if(wall[i].cstat &254)
					{
						wall[i].xpanning -= t>>10; // sintable[(t+512)&2047]>>12;
						wall[i].ypanning -= t>>10; // sintable[t&2047]>>12;

						if(wall[i].extra == 1)
						{
							wall[i].extra = 0;
							GlobalMembersGLOBAL.animwall[p].tag = 0;
						}
						else
							GlobalMembersGLOBAL.animwall[p].tag+=128;

						if(GlobalMembersGLOBAL.animwall[p].tag < (128<<4))
						{
							if(GlobalMembersGLOBAL.animwall[p].tag &128)
								wall[i].overpicnum = W_FORCEFIELD;
							else
								wall[i].overpicnum = W_FORCEFIELD+1;
						}
						else
						{
							if((TRAND &255) < 32)
								GlobalMembersGLOBAL.animwall[p].tag = 128<<(TRAND &3);
							else
								wall[i].overpicnum = W_FORCEFIELD+1;
						}
					}

					break;
			}
		}
	}

	public static sbyte activatewarpelevators(short s,short d) //Parm = sectoreffectornum
	{
		short i;
		short sn;

		sn = sprite[s].sectnum;

		// See if the sector exists

		i = headspritestat[3];
		while(i >= 0)
		{
			if(SLT == 17)
				if(SHT == sprite[s].hitag)
					if((klabs(sector[sn].floorz-GlobalMembersGLOBAL.hittype[s].temp_data[2]) > SP) || (sector[SECT].hitag == (sector[sn].hitag-d)))
							break;
			i = nextspritestat[i];
		}

		if(i == -1)
		{
			d = 0;
			return 1; // No find
		}
		else
		{
			if(d == 0)
				GlobalMembersSOUNDS.spritesound(ELEVATOR_OFF, s);
			else
				GlobalMembersSOUNDS.spritesound(ELEVATOR_ON, s);
		}


		i = headspritestat[3];
		while(i >= 0)
		{
			if(SLT == 17)
				if(SHT == sprite[s].hitag)
				{
					T1 = d;
					T2 = d; //Make all check warp
				}
			i = nextspritestat[i];
		}
		return 0;
	}



	public static void operatesectors(short sn,short ii)
	{
		int j;
		int l;
		int q;
		int startwall;
		int endwall;
		short i;
		sbyte sect_error;
		sectortype sptr;

		sect_error = 0;
		sptr = sector[sn];

		switch(sptr.lotag&(0xffff-49152))
		{

			case 30:
				j = sector[sn].hitag;
				if(GlobalMembersGLOBAL.hittype[j].tempang == 0 || GlobalMembersGLOBAL.hittype[j].tempang == 256)
						GlobalMembersSECTOR.callsound(sn, ii);
				if(sprite[j].extra == 1)
					sprite[j].extra = 3;
				else
					sprite[j].extra = 1;
				break;

			case 31:

				j = sector[sn].hitag;
				if(GlobalMembersGLOBAL.hittype[j].temp_data[4] == 0)
					GlobalMembersGLOBAL.hittype[j].temp_data[4] = 1;

				GlobalMembersSECTOR.callsound(sn, ii);
				break;

			case 26: //The split doors
				i = GlobalMembersSECTOR.getanimationgoal(sptr.ceilingz);
				if(i == -1) //if the door has stopped
				{
					haltsoundhack = 1;
					sptr.lotag &= 0xff00;
					sptr.lotag |= 22;
					GlobalMembersSECTOR.operatesectors(sn, ii);
					sptr.lotag &= 0xff00;
					sptr.lotag |= 9;
					GlobalMembersSECTOR.operatesectors(sn, ii);
					sptr.lotag &= 0xff00;
					sptr.lotag |= 26;
				}
				return;

			case 9:
			{
				int dax;
				int day;
				int dax2;
				int day2;
				int sp;
				int[] wallfind = new int[2];

				startwall = sptr.wallptr;
				endwall = startwall+sptr.wallnum-1;

				sp = sptr.extra>>4;

				//first find center point by averaging all points
				dax = 0, day = 0;
				for(i = startwall;i<=endwall;i++)
				{
					dax += wall[i].x;
					day += wall[i].y;
				}
				dax /= (endwall-startwall+1);
				day /= (endwall-startwall+1);

				//find any points with either same x or same y coordinate
				//  as center (dax, day) - should be 2 points found.
				wallfind[0] = -1;
				wallfind[1] = -1;
				for(i = startwall;i<=endwall;i++)
					if ((wall[i].x == dax) || (wall[i].y == day))
					{
						if (wallfind[0] == -1)
							wallfind[0] = i;
						else
							wallfind[1] = i;
					}

				for(j = 0;j<2;j++)
				{
					if ((wall[wallfind[j]].x == dax) && (wall[wallfind[j]].y == day))
					{
						//find what direction door should open by averaging the
						//  2 neighboring points of wallfind[0] & wallfind[1].
						i = wallfind[j]-1;
						if (i < startwall)
							i = endwall;
						dax2 = ((wall[i].x+wall[wall[wallfind[j]].point2].x)>>1)-wall[wallfind[j]].x;
						day2 = ((wall[i].y+wall[wall[wallfind[j]].point2].y)>>1)-wall[wallfind[j]].y;
						if (dax2 != 0)
						{
							dax2 = wall[wall[wall[wallfind[j]].point2].point2].x;
							dax2 -= wall[wall[wallfind[j]].point2].x;
							GlobalMembersSECTOR.setanimation(sn, ref wall[wallfind[j]].x, wall[wallfind[j]].x+dax2, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[i].x, wall[i].x+dax2, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[wall[wallfind[j]].point2].x, wall[wall[wallfind[j]].point2].x+dax2, sp);
							GlobalMembersSECTOR.callsound(sn, ii);
						}
						else if (day2 != 0)
						{
							day2 = wall[wall[wall[wallfind[j]].point2].point2].y;
							day2 -= wall[wall[wallfind[j]].point2].y;
							GlobalMembersSECTOR.setanimation(sn, ref wall[wallfind[j]].y, wall[wallfind[j]].y+day2, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[i].y, wall[i].y+day2, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[wall[wallfind[j]].point2].y, wall[wall[wallfind[j]].point2].y+day2, sp);
							GlobalMembersSECTOR.callsound(sn, ii);
						}
					}
					else
					{
						i = wallfind[j]-1;
						if (i < startwall)
							i = endwall;
						dax2 = ((wall[i].x+wall[wall[wallfind[j]].point2].x)>>1)-wall[wallfind[j]].x;
						day2 = ((wall[i].y+wall[wall[wallfind[j]].point2].y)>>1)-wall[wallfind[j]].y;
						if (dax2 != 0)
						{
							GlobalMembersSECTOR.setanimation(sn, ref wall[wallfind[j]].x, dax, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[i].x, dax+dax2, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[wall[wallfind[j]].point2].x, dax+dax2, sp);
							GlobalMembersSECTOR.callsound(sn, ii);
						}
						else if (day2 != 0)
						{
							GlobalMembersSECTOR.setanimation(sn, ref wall[wallfind[j]].y, day, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[i].y, day+day2, sp);
							GlobalMembersSECTOR.setanimation(sn, ref wall[wall[wallfind[j]].point2].y, day+day2, sp);
							GlobalMembersSECTOR.callsound(sn, ii);
						}
					}
				}

			}
			return;

			case 15: //Warping elevators

				if(sprite[ii].picnum != APLAYER)
					return;
	//            if(ps[sprite[ii].yvel].select_dir == 1) return;

				i = headspritesect[sn];
				while(i >= 0)
				{
					if(PN==SECTOREFFECTOR && SLT == 17)
						break;
					i = nextspritesect[i];
				}

				if(sprite[ii].sectnum == sn)
				{
					if (GlobalMembersSECTOR.activatewarpelevators(i, -1) != 0)
						GlobalMembersSECTOR.activatewarpelevators(i, 1);
					else if (GlobalMembersSECTOR.activatewarpelevators(i, 1) != 0)
						GlobalMembersSECTOR.activatewarpelevators(i, -1);
					return;
				}
				else
				{
					if(sptr.floorz > SZ)
						GlobalMembersSECTOR.activatewarpelevators(i, -1);
					else
						GlobalMembersSECTOR.activatewarpelevators(i, 1);
				}

				return;

			case 16:
			case 17:

				i = GlobalMembersSECTOR.getanimationgoal(sptr.floorz);

				if(i == -1)
				{
					i = nextsectorneighborz(sn,sptr.floorz,1,1);
					if(i == -1)
					{
						i = nextsectorneighborz(sn,sptr.floorz,1,-1);
						if(i == -1)
							return;
						j = sector[i].floorz;
						GlobalMembersSECTOR.setanimation(sn, ref sptr.floorz, j, sptr.extra);
					}
					else
					{
						j = sector[i].floorz;
						GlobalMembersSECTOR.setanimation(sn, ref sptr.floorz, j, sptr.extra);
					}
					GlobalMembersSECTOR.callsound(sn, ii);
				}

				return;

			case 18:
			case 19:

				i = GlobalMembersSECTOR.getanimationgoal(sptr.floorz);

				if(i == -1)
				{
					i = nextsectorneighborz(sn,sptr.floorz,1,-1);
					if(i == -1)
						i = nextsectorneighborz(sn,sptr.floorz,1,1);
					if(i == -1)
						return;
					j = sector[i].floorz;
					q = sptr.extra;
					l = sptr.ceilingz-sptr.floorz;
					GlobalMembersSECTOR.setanimation(sn, ref sptr.floorz, j, q);
					GlobalMembersSECTOR.setanimation(sn, ref sptr.ceilingz, j+l, q);
					GlobalMembersSECTOR.callsound(sn, ii);
				}
				return;

			case 29:

				if(sptr.lotag &0x8000)
					j = sector[nextsectorneighborz(sn,sptr.ceilingz,1,1)].floorz;
				else
					j = sector[nextsectorneighborz(sn,sptr.ceilingz,-1,-1)].ceilingz;

				i = headspritestat[3]; //Effectors
				while(i >= 0)
				{
					if((SLT == 22) && (SHT == sptr.hitag))
					{
						sector[SECT].extra = -sector[SECT].extra;

						T1 = sn;
						T2 = 1;
					}
					i = nextspritestat[i];
				}

				sptr.lotag ^= 0x8000;

				GlobalMembersSECTOR.setanimation(sn, ref sptr.ceilingz, j, sptr.extra);

				GlobalMembersSECTOR.callsound(sn, ii);

				return;

			case 20:

				REDODOOR:

				if(sptr.lotag &0x8000)
				{
					i = headspritesect[sn];
					while(i >= 0)
					{
						if(sprite[i].statnum == 3 && SLT==9)
						{
							j = SZ;
							break;
						}
						i = nextspritesect[i];
					}
					if(i == -1)
						j = sptr.floorz;
				}
				else
				{
					j = nextsectorneighborz(sn,sptr.ceilingz,-1,-1);

					if(j >= 0)
						j = sector[j].ceilingz;
					else
					{
						sptr.lotag |= 32768;
						goto REDODOOR;
					}
				}

				sptr.lotag ^= 0x8000;

				GlobalMembersSECTOR.setanimation(sn, ref sptr.ceilingz, j, sptr.extra);
				GlobalMembersSECTOR.callsound(sn, ii);

				return;

			case 21:
				i = GlobalMembersSECTOR.getanimationgoal(sptr.floorz);
				if (i >= 0)
				{
					if (GlobalMembersGLOBAL.animategoal[sn] == sptr.ceilingz)
						GlobalMembersGLOBAL.animategoal[i] = sector[nextsectorneighborz(sn,sptr.ceilingz,1,1)].floorz;
					else
						GlobalMembersGLOBAL.animategoal[i] = sptr.ceilingz;
					j = GlobalMembersGLOBAL.animategoal[i];
				}
				else
				{
					if (sptr.ceilingz == sptr.floorz)
						j = sector[nextsectorneighborz(sn,sptr.ceilingz,1,1)].floorz;
					else
						j = sptr.ceilingz;

					sptr.lotag ^= 0x8000;

					if(GlobalMembersSECTOR.setanimation(sn, sptr.floorz, j, sptr.extra) >= 0)
						GlobalMembersSECTOR.callsound(sn, ii);
				}
				return;

			case 22:

				// REDODOOR22:

				if ((sptr.lotag &0x8000))
				{
					q = (sptr.ceilingz+sptr.floorz)>>1;
					j = GlobalMembersSECTOR.setanimation(sn, sptr.floorz, q, sptr.extra);
					j = GlobalMembersSECTOR.setanimation(sn, sptr.ceilingz, q, sptr.extra);
				}
				else
				{
					q = sector[nextsectorneighborz(sn,sptr.floorz,1,1)].floorz;
					j = GlobalMembersSECTOR.setanimation(sn, sptr.floorz, q, sptr.extra);
					q = sector[nextsectorneighborz(sn,sptr.ceilingz,-1,-1)].ceilingz;
					j = GlobalMembersSECTOR.setanimation(sn, sptr.ceilingz, q, sptr.extra);
				}

				sptr.lotag ^= 0x8000;

				GlobalMembersSECTOR.callsound(sn, ii);

				return;

			case 23: //Swingdoor

				j = -1;
				q = 0;

				i = headspritestat[3];
				while(i >= 0)
				{
					if(SLT == 11 && SECT == sn && !T5)
					{
						j = i;
						break;
					}
					i = nextspritestat[i];
				}

				l = sector[SECT].lotag &0x8000;

				if(j >= 0)
				{
					i = headspritestat[3];
					while(i >= 0)
					{
						if(l == (sector[SECT].lotag &0x8000) && SLT == 11 && sprite[j].hitag == SHT && !T5)
						{
							if(sector[SECT].lotag &0x8000)
								sector[SECT].lotag &= 0x7fff;
							else
								sector[SECT].lotag |= 0x8000;
							T5 = 1;
							T4 = -T4;
							if(q == 0)
							{
								GlobalMembersSECTOR.callsound(sn, i);
								q = 1;
							}
						}
						i = nextspritestat[i];
					}
				}
				return;

			case 25: //Subway type sliding doors

				j = headspritestat[3];
				while(j >= 0) //Find the sprite
				{
					if((sprite[j].lotag) == 15 && sprite[j].sectnum == sn)
						break; //Found the sectoreffector.
					j = nextspritestat[j];
				}

				if(j < 0)
					return;

				i = headspritestat[3];
				while(i >= 0)
				{
					if(SHT==sprite[j].hitag)
					{
						if(SLT == 15)
						{
							sector[SECT].lotag ^= 0x8000; // Toggle the open or close
							SA += 1024;
							if(T5)
								GlobalMembersSECTOR.callsound(SECT, i);
							GlobalMembersSECTOR.callsound(SECT, i);
							if(sector[SECT].lotag &0x8000)
								T5 = 1;
							else
								T5 = 2;
						}
					}
					i = nextspritestat[i];
				}
				return;

			case 27: //Extended bridge

				j = headspritestat[3];
				while(j >= 0)
				{
					if((sprite[j].lotag &0xff)==20 && sprite[j].sectnum == sn) //Bridge
					{

						sector[sn].lotag ^= 0x8000;
						if(sector[sn].lotag &0x8000) //OPENING
							GlobalMembersGLOBAL.hittype[j].temp_data[0] = 1;
						else
							GlobalMembersGLOBAL.hittype[j].temp_data[0] = 2;
						GlobalMembersSECTOR.callsound(sn, ii);
						break;
					}
					j = nextspritestat[j];
				}
				return;


			case 28:
				//activate the rest of them

				j = headspritesect[sn];
				while(j >= 0)
				{
					if(sprite[j].statnum==3 && (sprite[j].lotag &0xff)==21)
						break; //Found it
					j = nextspritesect[j];
				}

				j = sprite[j].hitag;

				l = headspritestat[3];
				while(l >= 0)
				{
					if((sprite[l].lotag &0xff)==21 && !GlobalMembersGLOBAL.hittype[l].temp_data[0] && (sprite[l].hitag) == j)
						GlobalMembersGLOBAL.hittype[l].temp_data[0] = 1;
					l = nextspritestat[l];
				}
				GlobalMembersSECTOR.callsound(sn, ii);

				return;
		}
	}



	public static void operaterespawns(short low)
	{
		short i;
		short j;
		short nexti;

		i = headspritestat[11];
		while(i >= 0)
		{
			nexti = nextspritestat[i];
			if(SLT == low)
				switch(PN)
			{
				case RESPAWN:
					if(GlobalMembersGAME.badguypic(SHT) != 0 && GlobalMembersGLOBAL.ud.monsters_off)
						break;

					j = GlobalMembersGAME.spawn(i, TRANSPORTERSTAR);
					sprite[j].z -= (32<<8);

					sprite[i].extra = 66-12; // Just a way to killit
					break;
			}
			i = nexti;
		}
	}

	public static void operateactivators(short low,short snum)
	{
		short i;
		short j;
		short k;
		short[] p;
		short nexti;
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
		walltype *wal = new walltype();

		for(i = GlobalMembersGLOBAL.numcyclers-1;i>=0;i--)
		{
			p = GlobalMembersGLOBAL.cyclers[i, 0];

			if(p[4] == low)
			{
				p[5] = !p[5];

				sector[p[0]].floorshade = sector[p[0]].ceilingshade = p[3];
				wal = &wall[sector[p[0]].wallptr];
				for(j = sector[p[0]].wallnum;j > 0;j--,wal++)
					wal.shade = p[3];
			}
		}

		i = headspritestat[8];
		k = -1;
		while(i >= 0)
		{
			if(sprite[i].lotag == low)
			{
				if(sprite[i].picnum == ACTIVATORLOCKED)
				{
					if(sector[SECT].lotag &16384)
						sector[SECT].lotag &= 65535-16384;
					else
						sector[SECT].lotag |= 16384;

					if(snum >= 0)
					{
						if(sector[SECT].lotag &16384)
							GlobalMembersGAME.FTA(4, ref GlobalMembersGLOBAL.ps[snum]);
						else
							GlobalMembersGAME.FTA(8, ref GlobalMembersGLOBAL.ps[snum]);
					}
				}
				else
				{
					switch(SHT)
					{
						case 0:
							break;
						case 1:
							if(sector[SECT].floorz != sector[SECT].ceilingz)
							{
								i = nextspritestat[i];
								continue;
							}
							break;
						case 2:
							if(sector[SECT].floorz == sector[SECT].ceilingz)
							{
								i = nextspritestat[i];
								continue;
							}
							break;
					}

					if(sector[sprite[i].sectnum].lotag < 3)
					{
						j = headspritesect[sprite[i].sectnum];
						while(j >= 0)
						{
							if(sprite[j].statnum == 3)
								switch(sprite[j].lotag)
							{
								case 36:
								case 31:
								case 32:
								case 18:
									GlobalMembersGLOBAL.hittype[j].temp_data[0] = 1-GlobalMembersGLOBAL.hittype[j].temp_data[0];
									GlobalMembersSECTOR.callsound(SECT, j);
									break;
							}
							j = nextspritesect[j];
						}
					}

					if(k == -1 && (sector[SECT].lotag &0xff) == 22)
						k = GlobalMembersSECTOR.callsound(SECT, i);

					GlobalMembersSECTOR.operatesectors(SECT, i);
				}
			}
			i = nextspritestat[i];
		 }

		GlobalMembersSECTOR.operaterespawns(low);
	}

	public static void operatemasterswitches(short low)
	{
		short i;

		i = headspritestat[6];
		while(i >= 0)
		{
			if(PN == MASTERSWITCH && SLT == low && SP == 0)
				SP = 1;
			i = nextspritestat[i];
		}
	}



	public static void operateforcefields(short s, short low)
	{
		short i;
		short p;

		for(p = GlobalMembersGLOBAL.numanimwalls;p>=0;p--)
		{
			i = GlobalMembersGLOBAL.animwall[p].wallnum;

			if(low == wall[i].lotag || low == -1)
				switch(wall[i].overpicnum)
			{
				case W_FORCEFIELD :
				case W_FORCEFIELD+1:
				case W_FORCEFIELD+2:
				case BIGFORCE:

					GlobalMembersGLOBAL.animwall[p].tag = 0;

					if(wall[i].cstat)
					{
						wall[i].cstat = 0;

						if(s >= 0 && sprite[s].picnum == SECTOREFFECTOR && sprite[s].lotag == 30)
								wall[i].lotag = 0;
					}
					else
						wall[i].cstat = 85;
					break;
			}
		}
	}


	public static sbyte checkhitswitch(short snum,int w,sbyte switchtype)
	{
		sbyte switchpal;
		short i;
		short x;
		short lotag;
		short hitag;
		short picnum;
		short correctdips;
		short numdips;
		int sx;
		int sy;

		if(w < 0)
			return 0;
		correctdips = 1;
		numdips = 0;

		if(switchtype == 1) // A wall sprite
		{
			lotag = sprite[w].lotag;
			if(lotag == 0)
				return 0;
			hitag = sprite[w].hitag;
			sx = sprite[w].x;
			sy = sprite[w].y;
			picnum = sprite[w].picnum;
			switchpal = sprite[w].pal;
		}
		else
		{
			lotag = wall[w].lotag;
			if(lotag == 0)
				return 0;
			hitag = wall[w].hitag;
			sx = wall[w].x;
			sy = wall[w].y;
			picnum = wall[w].picnum;
			switchpal = wall[w].pal;
		}

		switch(picnum)
		{
			case DIPSWITCH:
			case DIPSWITCH+1:
			case TECHSWITCH:
			case TECHSWITCH+1:
			case ALIENSWITCH:
			case ALIENSWITCH+1:
				break;
			case ACCESSSWITCH:
			case ACCESSSWITCH2:
				if(GlobalMembersGLOBAL.ps[snum].access_incs == 0)
				{
					if(switchpal == 0)
					{
						if((GlobalMembersGLOBAL.ps[snum].got_access &1))
							GlobalMembersGLOBAL.ps[snum].access_incs = 1;
						else
							GlobalMembersGAME.FTA(70, ref GlobalMembersGLOBAL.ps[snum]);
					}

					else if(switchpal == 21)
					{
						if(GlobalMembersGLOBAL.ps[snum].got_access &2)
							GlobalMembersGLOBAL.ps[snum].access_incs = 1;
						else
							GlobalMembersGAME.FTA(71, ref GlobalMembersGLOBAL.ps[snum]);
					}

					else if(switchpal == 23)
					{
						if(GlobalMembersGLOBAL.ps[snum].got_access &4)
							GlobalMembersGLOBAL.ps[snum].access_incs = 1;
						else
							GlobalMembersGAME.FTA(72, ref GlobalMembersGLOBAL.ps[snum]);
					}

					if(GlobalMembersGLOBAL.ps[snum].access_incs == 1)
					{
						if(switchtype == 0)
							GlobalMembersGLOBAL.ps[snum].access_wallnum = w;
						else
							GlobalMembersGLOBAL.ps[snum].access_spritenum = w;
					}

					return 0;
				}
			case DIPSWITCH2:
			case DIPSWITCH2+1:
			case DIPSWITCH3:
			case DIPSWITCH3+1:
			case MULTISWITCH:
			case MULTISWITCH+1:
			case MULTISWITCH+2:
			case MULTISWITCH+3:
			case PULLSWITCH:
			case PULLSWITCH+1:
			case HANDSWITCH:
			case HANDSWITCH+1:
			case SLOTDOOR:
			case SLOTDOOR+1:
			case LIGHTSWITCH:
			case LIGHTSWITCH+1:
			case SPACELIGHTSWITCH:
			case SPACELIGHTSWITCH+1:
			case SPACEDOORSWITCH:
			case SPACEDOORSWITCH+1:
			case FRANKENSTINESWITCH:
			case FRANKENSTINESWITCH+1:
			case LIGHTSWITCH2:
			case LIGHTSWITCH2+1:
			case POWERSWITCH1:
			case POWERSWITCH1+1:
			case LOCKSWITCH1:
			case LOCKSWITCH1+1:
			case POWERSWITCH2:
			case POWERSWITCH2+1:
				if (GlobalMembersSECTOR.check_activator_motion(lotag) != 0)
					return 0;
				break;
			default:
				if(GlobalMembersSECTOR.isadoorwall(picnum) == 0)
					return 0;
				break;
		}

		i = headspritestat[0];
		while(i >= 0)
		{
			if(lotag == SLT)
				switch(PN)
			{
				case DIPSWITCH:
				case TECHSWITCH:
				case ALIENSWITCH:
					if(switchtype == 1 && w == i)
						PN++;
					else if(SHT == 0)
						correctdips++;
					numdips++;
					break;
				case TECHSWITCH+1:
				case DIPSWITCH+1:
				case ALIENSWITCH+1:
					if(switchtype == 1 && w == i)
						PN--;
					else if(SHT == 1)
						correctdips++;
					numdips++;
					break;
				case MULTISWITCH:
				case MULTISWITCH+1:
				case MULTISWITCH+2:
				case MULTISWITCH+3:
					sprite[i].picnum++;
					if(sprite[i].picnum > (MULTISWITCH+3))
						sprite[i].picnum = MULTISWITCH;
					break;
				case ACCESSSWITCH:
				case ACCESSSWITCH2:
				case SLOTDOOR:
				case LIGHTSWITCH:
				case SPACELIGHTSWITCH:
				case SPACEDOORSWITCH:
				case FRANKENSTINESWITCH:
				case LIGHTSWITCH2:
				case POWERSWITCH1:
				case LOCKSWITCH1:
				case POWERSWITCH2:
				case HANDSWITCH:
				case PULLSWITCH:
				case DIPSWITCH2:
				case DIPSWITCH3:
					sprite[i].picnum++;
					break;
				case PULLSWITCH+1:
				case HANDSWITCH+1:
				case LIGHTSWITCH2+1:
				case POWERSWITCH1+1:
				case LOCKSWITCH1+1:
				case POWERSWITCH2+1:
				case SLOTDOOR+1:
				case LIGHTSWITCH+1:
				case SPACELIGHTSWITCH+1:
				case SPACEDOORSWITCH+1:
				case FRANKENSTINESWITCH+1:
				case DIPSWITCH2+1:
				case DIPSWITCH3+1:
					sprite[i].picnum--;
					break;
			}
			i = nextspritestat[i];
		}

		for(i = 0;i<numwalls;i++)
		{
			x = i;
			if(lotag == wall[x].lotag)
				switch(wall[x].picnum)
			{
				case DIPSWITCH:
				case TECHSWITCH:
				case ALIENSWITCH:
					if(switchtype == 0 && i == w)
						wall[x].picnum++;
					else if(wall[x].hitag == 0)
						correctdips++;
					numdips++;
					break;
				case DIPSWITCH+1:
				case TECHSWITCH+1:
				case ALIENSWITCH+1:
					if(switchtype == 0 && i == w)
						wall[x].picnum--;
					else if(wall[x].hitag == 1)
						correctdips++;
					numdips++;
					break;
				case MULTISWITCH:
				case MULTISWITCH+1:
				case MULTISWITCH+2:
				case MULTISWITCH+3:
					wall[x].picnum++;
					if(wall[x].picnum > (MULTISWITCH+3))
						wall[x].picnum = MULTISWITCH;
					break;
				case ACCESSSWITCH:
				case ACCESSSWITCH2:
				case SLOTDOOR:
				case LIGHTSWITCH:
				case SPACELIGHTSWITCH:
				case SPACEDOORSWITCH:
				case LIGHTSWITCH2:
				case POWERSWITCH1:
				case LOCKSWITCH1:
				case POWERSWITCH2:
				case PULLSWITCH:
				case HANDSWITCH:
				case DIPSWITCH2:
				case DIPSWITCH3:
					wall[x].picnum++;
					break;
				case HANDSWITCH+1:
				case PULLSWITCH+1:
				case LIGHTSWITCH2+1:
				case POWERSWITCH1+1:
				case LOCKSWITCH1+1:
				case POWERSWITCH2+1:
				case SLOTDOOR+1:
				case LIGHTSWITCH+1:
				case SPACELIGHTSWITCH+1:
				case SPACEDOORSWITCH+1:
				case DIPSWITCH2+1:
				case DIPSWITCH3+1:
					wall[x].picnum--;
					break;
			}
		}

		if(lotag == (short) 65535)
		{
			GlobalMembersGLOBAL.ps[myconnectindex].gm = MODE_EOL;
			if(GlobalMembersGLOBAL.ud.from_bonus)
			{
				GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.ud.from_bonus;
				GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
				GlobalMembersGLOBAL.ud.from_bonus = 0;
			}
			else
			{
				GlobalMembersGLOBAL.ud.level_number++;
				if((GlobalMembersGLOBAL.ud.volume_number && GlobalMembersGLOBAL.ud.level_number > 10) || (GlobalMembersGLOBAL.ud.volume_number == 0 && GlobalMembersGLOBAL.ud.level_number > 5))
					GlobalMembersGLOBAL.ud.level_number = 0;
				GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
			}
			return 1;
		}

		switch(picnum)
		{
			default:
				if(GlobalMembersSECTOR.isadoorwall(picnum) == 0)
					break;
			case DIPSWITCH:
			case DIPSWITCH+1:
			case TECHSWITCH:
			case TECHSWITCH+1:
			case ALIENSWITCH:
			case ALIENSWITCH+1:
				if(picnum == DIPSWITCH || picnum == DIPSWITCH+1 || picnum == ALIENSWITCH || picnum == ALIENSWITCH+1 || picnum == TECHSWITCH || picnum == TECHSWITCH+1)
				{
					if(picnum == ALIENSWITCH || picnum == ALIENSWITCH+1)
					{
						if(switchtype == 1)
							GlobalMembersSOUNDS.xyzsound(ALIEN_SWITCH1, w, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
						else
							GlobalMembersSOUNDS.xyzsound(ALIEN_SWITCH1, GlobalMembersGLOBAL.ps[snum].i, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
					}
					else
					{
						if(switchtype == 1)
							GlobalMembersSOUNDS.xyzsound(SWITCH_ON, w, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
						else
							GlobalMembersSOUNDS.xyzsound(SWITCH_ON, GlobalMembersGLOBAL.ps[snum].i, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
					}
					if(numdips != correctdips)
						break;
					GlobalMembersSOUNDS.xyzsound(END_OF_LEVEL_WARN, GlobalMembersGLOBAL.ps[snum].i, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
				}
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case DIPSWITCH2:
			case DIPSWITCH2+1:
			case DIPSWITCH3:
			case DIPSWITCH3+1:
			case MULTISWITCH:
			case MULTISWITCH+1:
			case MULTISWITCH+2:
			case MULTISWITCH+3:
			case ACCESSSWITCH:
			case ACCESSSWITCH2:
			case SLOTDOOR:
			case SLOTDOOR+1:
			case LIGHTSWITCH:
			case LIGHTSWITCH+1:
			case SPACELIGHTSWITCH:
			case SPACELIGHTSWITCH+1:
			case SPACEDOORSWITCH:
			case SPACEDOORSWITCH+1:
			case FRANKENSTINESWITCH:
			case FRANKENSTINESWITCH+1:
			case LIGHTSWITCH2:
			case LIGHTSWITCH2+1:
			case POWERSWITCH1:
			case POWERSWITCH1+1:
			case LOCKSWITCH1:
			case LOCKSWITCH1+1:
			case POWERSWITCH2:
			case POWERSWITCH2+1:
			case HANDSWITCH:
			case HANDSWITCH+1:
			case PULLSWITCH:
			case PULLSWITCH+1:

					if(picnum == MULTISWITCH || picnum == (MULTISWITCH+1) || picnum == (MULTISWITCH+2) || picnum == (MULTISWITCH+3))
							lotag += picnum-MULTISWITCH;

					x = headspritestat[3];
					while(x >= 0)
					{
					   if(((sprite[x].hitag) == lotag))
					   {
						   switch(sprite[x].lotag)
						   {
							   case 12:
									sector[sprite[x].sectnum].floorpal = 0;
									GlobalMembersGLOBAL.hittype[x].temp_data[0]++;
									if(GlobalMembersGLOBAL.hittype[x].temp_data[0] == 2)
									   GlobalMembersGLOBAL.hittype[x].temp_data[0]++;

									break;
							   case 24:
							   case 34:
							   case 25:
								   GlobalMembersGLOBAL.hittype[x].temp_data[4] = !GlobalMembersGLOBAL.hittype[x].temp_data[4];
								   if(GlobalMembersGLOBAL.hittype[x].temp_data[4])
									   GlobalMembersGAME.FTA(15, ref GlobalMembersGLOBAL.ps[snum]);
								   else
									   GlobalMembersGAME.FTA(2, ref GlobalMembersGLOBAL.ps[snum]);
								   break;
							   case 21:
								   GlobalMembersGAME.FTA(2, ref GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek]);
								   break;
						   }
					   }
					   x = nextspritestat[x];
					}

					GlobalMembersSECTOR.operateactivators(lotag, snum);
					GlobalMembersSECTOR.operateforcefields(GlobalMembersGLOBAL.ps[snum].i, lotag);
					GlobalMembersSECTOR.operatemasterswitches(lotag);

					if(picnum == DIPSWITCH || picnum == DIPSWITCH+1 || picnum == ALIENSWITCH || picnum == ALIENSWITCH+1 || picnum == TECHSWITCH || picnum == TECHSWITCH+1)
						return 1;

					if(hitag == 0 && GlobalMembersSECTOR.isadoorwall(picnum) == 0)
					{
						if(switchtype == 1)
							GlobalMembersSOUNDS.xyzsound(SWITCH_ON, w, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
						else
							GlobalMembersSOUNDS.xyzsound(SWITCH_ON, GlobalMembersGLOBAL.ps[snum].i, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
					}
					else if(hitag != 0)
					{
						if(switchtype == 1 && (GlobalMembersGLOBAL.soundm[hitag]&4) == 0)
							GlobalMembersSOUNDS.xyzsound(hitag, w, sx, sy, GlobalMembersGLOBAL.ps[snum].posz);
						else
							GlobalMembersSOUNDS.spritesound(hitag, GlobalMembersGLOBAL.ps[snum].i);
					}

				   return 1;
		}
		return 0;
	}


	public static void activatebysector(short sect,short j)
	{
		short i;
		short didit;

		didit = 0;

		i = headspritesect[sect];
		while(i >= 0)
		{
			if(PN == ACTIVATOR)
			{
				GlobalMembersSECTOR.operateactivators(SLT, -1);
				didit = 1;
	//            return;
			}
			i = nextspritesect[i];
		}

		if(didit == 0)
			GlobalMembersSECTOR.operatesectors(sect, j);
	}

	public static void breakwall(short newpn,short spr,short dawallnum)
	{
		wall[dawallnum].picnum = newpn;
		GlobalMembersSOUNDS.spritesound(VENT_BUST, spr);
		GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, spr);
		GlobalMembersGAME.lotsofglass(spr, dawallnum, 10);
	}

	public static void checkhitwall(short spr,short dawallnum,int x,int y,int z,short atwith)
	{
		short j;
		short i;
		short sn = -1;
		short darkestwall;
		sbyte nfloors;
		sbyte nceilings;
		short nextj;
		walltype wal;
		spritetype s;

		wal = wall[dawallnum];

		if(wal.overpicnum == MIRROR)
		{
			switch(atwith)
			{
				case HEAVYHBOMB:
				case RADIUSEXPLOSION:
				case RPG:
				case HYDRENT:
				case SEENINE:
				case OOZFILTER:
				case EXPLODINGBARREL:
					GlobalMembersGAME.lotsofglass(spr, dawallnum, 70);
					wal.cstat &= ~16;
					wal.overpicnum = MIRRORBROKE;
					GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, spr);
					return;
			}
		}

	   if(((wal.cstat &16) || wal.overpicnum == BIGFORCE) && wal.nextsector >= 0)
		   if(sector[wal.nextsector].floorz > z)
			   if(sector[wal.nextsector].floorz-sector[wal.nextsector].ceilingz)
				   switch(wal.overpicnum)
		{
			case W_FORCEFIELD:
			case W_FORCEFIELD+1:
			case W_FORCEFIELD+2:
				wal.extra = 1; // tell the forces to animate
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case BIGFORCE:
				updatesector(x, y, sn);
				if(sn < 0)
					return;

				if(atwith == -1)
					i = GlobalMembersGAME.EGS(sn, x, y, z, FORCERIPPLE, -127, 8, 8, 0, 0, 0, spr, 5);
				else
				{
					if(atwith == CHAINGUN)
						i = GlobalMembersGAME.EGS(sn, x, y, z, FORCERIPPLE, -127, 16+sprite[spr].xrepeat, 16+sprite[spr].yrepeat, 0, 0, 0, spr, 5);
					else
						i = GlobalMembersGAME.EGS(sn, x, y, z, FORCERIPPLE, -127, 32, 32, 0, 0, 0, spr, 5);
				}

				CS |= 18+128;
				SA = getangle(wal.x-wall[wal.point2].x, wal.y-wall[wal.point2].y)-512;

				GlobalMembersSOUNDS.spritesound(SOMETHINGHITFORCE, i);

				return;

			case FANSPRITE:
				wal.overpicnum = FANSPRITEBROKE;
				wal.cstat &= 65535-65;
				if(wal.nextwall >= 0)
				{
					wall[wal.nextwall].overpicnum = FANSPRITEBROKE;
					wall[wal.nextwall].cstat &= 65535-65;
				}
				GlobalMembersSOUNDS.spritesound(VENT_BUST, spr);
				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, spr);
				return;

			case GLASS:
				updatesector(x, y, sn);
				if(sn < 0)
					return;
				wal.overpicnum=GLASS2;
				GlobalMembersGAME.lotsofglass(spr, dawallnum, 10);
				wal.cstat = 0;

				if(wal.nextwall >= 0)
					wall[wal.nextwall].cstat = 0;

				i = GlobalMembersGAME.EGS(sn, x, y, z, SECTOREFFECTOR, 0, 0, 0, GlobalMembersGLOBAL.ps[0].ang, 0, 0, spr, 3);
				SLT = 128;
				T2 = 5;
				T3 = dawallnum;
				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, i);
				return;
			case STAINGLASS1:
				updatesector(x, y, sn);
				if(sn < 0)
					return;
				GlobalMembersGAME.lotsofcolourglass(spr, dawallnum, 80);
				wal.cstat = 0;
				if(wal.nextwall >= 0)
					wall[wal.nextwall].cstat = 0;
				GlobalMembersSOUNDS.spritesound(VENT_BUST, spr);
				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, spr);
				return;
		}

		switch(wal.picnum)
		{
				case COLAMACHINE:
				case VENDMACHINE:
					GlobalMembersSECTOR.breakwall(wal.picnum+2, spr, dawallnum);
					GlobalMembersSOUNDS.spritesound(VENT_BUST, spr);
					return;

				case OJ:
				case FEMPIC2:
				case FEMPIC3:

				case SCREENBREAK6:
				case SCREENBREAK7:
				case SCREENBREAK8:

				case SCREENBREAK1:
				case SCREENBREAK2:
				case SCREENBREAK3:
				case SCREENBREAK4:
				case SCREENBREAK5:

				case SCREENBREAK9:
				case SCREENBREAK10:
				case SCREENBREAK11:
				case SCREENBREAK12:
				case SCREENBREAK13:
				case SCREENBREAK14:
				case SCREENBREAK15:
				case SCREENBREAK16:
				case SCREENBREAK17:
				case SCREENBREAK18:
				case SCREENBREAK19:
				case BORNTOBEWILDSCREEN:

					GlobalMembersGAME.lotsofglass(spr, dawallnum, 30);
					wal.picnum=W_SCREENBREAK+(TRAND%3);
					GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, spr);
					return;

				case W_TECHWALL5:
				case W_TECHWALL6:
				case W_TECHWALL7:
				case W_TECHWALL8:
				case W_TECHWALL9:
					GlobalMembersSECTOR.breakwall(wal.picnum+1, spr, dawallnum);
					return;
				case W_MILKSHELF:
					GlobalMembersSECTOR.breakwall(W_MILKSHELFBROKE, spr, dawallnum);
					return;

				case W_TECHWALL10:
					GlobalMembersSECTOR.breakwall(W_HITTECHWALL10, spr, dawallnum);
					return;

				case W_TECHWALL1:
				case W_TECHWALL11:
				case W_TECHWALL12:
				case W_TECHWALL13:
				case W_TECHWALL14:
					GlobalMembersSECTOR.breakwall(W_HITTECHWALL1, spr, dawallnum);
					return;

				case W_TECHWALL15:
					GlobalMembersSECTOR.breakwall(W_HITTECHWALL15, spr, dawallnum);
					return;

				case W_TECHWALL16:
					GlobalMembersSECTOR.breakwall(W_HITTECHWALL16, spr, dawallnum);
					return;

				case W_TECHWALL2:
					GlobalMembersSECTOR.breakwall(W_HITTECHWALL2, spr, dawallnum);
					return;

				case W_TECHWALL3:
					GlobalMembersSECTOR.breakwall(W_HITTECHWALL3, spr, dawallnum);
					return;

				case W_TECHWALL4:
					GlobalMembersSECTOR.breakwall(W_HITTECHWALL4, spr, dawallnum);
					return;

				case ATM:
					wal.picnum = ATMBROKE;
					GlobalMembersACTORS.lotsofmoney(ref sprite[spr], 1+(TRAND &7));
					GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, spr);
					break;

				case WALLLIGHT1:
				case WALLLIGHT2:
				case WALLLIGHT3:
				case WALLLIGHT4:
				case TECHLIGHT2:
				case TECHLIGHT4:

					if(rnd(128))
						GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, spr);
					else
						GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, spr);
					GlobalMembersGAME.lotsofglass(spr, dawallnum, 30);

					if(wal.picnum == WALLLIGHT1)
						wal.picnum = WALLLIGHTBUST1;

					if(wal.picnum == WALLLIGHT2)
						wal.picnum = WALLLIGHTBUST2;

					if(wal.picnum == WALLLIGHT3)
						wal.picnum = WALLLIGHTBUST3;

					if(wal.picnum == WALLLIGHT4)
						wal.picnum = WALLLIGHTBUST4;

					if(wal.picnum == TECHLIGHT2)
						wal.picnum = TECHLIGHTBUST2;

					if(wal.picnum == TECHLIGHT4)
						wal.picnum = TECHLIGHTBUST4;

					if(!wal.lotag)
						return;

					sn = wal.nextsector;
					if(sn < 0)
						return;
					darkestwall = 0;

					wal = wall[sector[sn].wallptr];
					for(i = sector[sn].wallnum;i > 0;i--,wal++)
						if(wal.shade > darkestwall)
							darkestwall = wal.shade;

					j = TRAND &1;
					i = headspritestat[3];
					while(i >= 0)
					{
						if(SHT == wall[dawallnum].lotag && SLT == 3)
						{
							T3 = j;
							T4 = darkestwall;
							T5 = 1;
						}
						i = nextspritestat[i];
					}
					break;
		}
	}


	public static void checkplayerhurt(ref player_struct p, short j)
	{
		if((j &49152) == 49152)
		{
			j &= (MAXSPRITES-1);

			switch(sprite[j].picnum)
			{
				case CACTUS:
					if(p.hurt_delay < 8)
					{
						sprite[p.i].extra -= 5;

						p.hurt_delay = 16;
						p.pals_time = 32;
						p.pals[0] = 32;
						p.pals[1] = 0;
						p.pals[2] = 0;
						GlobalMembersSOUNDS.spritesound(DUKE_LONGTERM_PAIN, p.i);
					}
					break;
			}
			return;
		}

		if((j &49152) != 32768)
			return;
		j &= (MAXWALLS-1);

		if(p.hurt_delay > 0)
			p.hurt_delay--;
		else if(wall[j].cstat &85)
			switch(wall[j].overpicnum)
		{
			case W_FORCEFIELD:
			case W_FORCEFIELD+1:
			case W_FORCEFIELD+2:
				 sprite[p.i].extra -= 5;

				 p.hurt_delay = 16;
				 p.pals_time = 32;
				 p.pals[0] = 32;
				 p.pals[1] = 0;
				 p.pals[2] = 0;

				 p.posxv = -(sintable[(p.ang+512)&2047]<<8);
				 p.posyv = -(sintable[(p.ang)&2047]<<8);
				 GlobalMembersSOUNDS.spritesound(DUKE_LONGTERM_PAIN, p.i);

				 GlobalMembersSECTOR.checkhitwall(p.i, j, p.posx+(sintable[(p.ang+512)&2047]>>9), p.posy+(sintable[p.ang &2047]>>9), p.posz, -1);

				break;

			case BIGFORCE:
				p.hurt_delay = 26;
				GlobalMembersSECTOR.checkhitwall(p.i, j, p.posx+(sintable[(p.ang+512)&2047]>>9), p.posy+(sintable[p.ang &2047]>>9), p.posz, -1);
				break;

		}
	}


	public static sbyte checkhitceiling(short sn)
	{
		short i;
		short j;
		short q;
		short darkestwall;
		short darkestceiling;
		sbyte nfloors;
		sbyte nceilings;
		walltype wal;

		switch(sector[sn].ceilingpicnum)
		{
			case WALLLIGHT1:
			case WALLLIGHT2:
			case WALLLIGHT3:
			case WALLLIGHT4:
			case TECHLIGHT2:
			case TECHLIGHT4:

					GlobalMembersGAME.ceilingglass(GlobalMembersGLOBAL.ps[myconnectindex].i, sn, 10);
					GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].i);

					if(sector[sn].ceilingpicnum == WALLLIGHT1)
						sector[sn].ceilingpicnum = WALLLIGHTBUST1;

					if(sector[sn].ceilingpicnum == WALLLIGHT2)
						sector[sn].ceilingpicnum = WALLLIGHTBUST2;

					if(sector[sn].ceilingpicnum == WALLLIGHT3)
						sector[sn].ceilingpicnum = WALLLIGHTBUST3;

					if(sector[sn].ceilingpicnum == WALLLIGHT4)
						sector[sn].ceilingpicnum = WALLLIGHTBUST4;

					if(sector[sn].ceilingpicnum == TECHLIGHT2)
						sector[sn].ceilingpicnum = TECHLIGHTBUST2;

					if(sector[sn].ceilingpicnum == TECHLIGHT4)
						sector[sn].ceilingpicnum = TECHLIGHTBUST4;


					if(!sector[sn].hitag)
					{
						i = headspritesect[sn];
						while(i >= 0)
						{
							if(PN == SECTOREFFECTOR && SLT == 12)
							{
								j = headspritestat[3];
								while(j >= 0)
								{
									if(sprite[j].hitag == SHT)
										GlobalMembersGLOBAL.hittype[j].temp_data[3] = 1;
									j = nextspritestat[j];
								}
								break;
							}
							i = nextspritesect[i];
						}
					}

					i = headspritestat[3];
					j = TRAND &1;
					while(i >= 0)
					{
						if(SHT == (sector[sn].hitag) && SLT == 3)
						{
							T3 = j;
							T5 = 1;
						}
						i = nextspritestat[i];
					}

					return 1;
		}

		return 0;
	}

	public static void checkhitsprite(short i,short sn)
	{
		short j;
		short k;
		short l;
		short nextj;
		short p;
		spritetype s;

		i &= (MAXSPRITES-1);

		switch(PN)
		{
			case OCEANSPRITE1:
			case OCEANSPRITE2:
			case OCEANSPRITE3:
			case OCEANSPRITE4:
			case OCEANSPRITE5:
				GlobalMembersGAME.spawn(i, SMALLSMOKE);
				deletesprite(i);
				break;
			case QUEBALL:
			case STRIPEBALL:
				if(sprite[sn].picnum == QUEBALL || sprite[sn].picnum == STRIPEBALL)
				{
					sprite[sn].xvel = (sprite[i].xvel>>1)+(sprite[i].xvel>>2);
					sprite[sn].ang -= (SA<<1)+1024;
					SA = getangle(SX-sprite[sn].x,SY-sprite[sn].y)-512;
					if(GlobalMembersGLOBAL.Sound[POOLBALLHIT].num < 2)
						GlobalMembersSOUNDS.spritesound(POOLBALLHIT, i);
				}
				else
				{
					if(TRAND &3)
					{
						sprite[i].xvel = 164;
						sprite[i].ang = sprite[sn].ang;
					}
					else
					{
						GlobalMembersGAME.lotsofglass(i, -1, 3);
						deletesprite(i);
					}
				}
				break;
			case TREE1:
			case TREE2:
			case TIRE:
			case CONE:
			case BOX:
				switch(sprite[sn].picnum)
				{
					case RADIUSEXPLOSION:
					case RPG:
					case FIRELASER:
					case HYDRENT:
					case HEAVYHBOMB:
						if(T1 == 0)
						{
							CS &= ~257;
							T1 = 1;
							GlobalMembersGAME.spawn(i, BURNING);
						}
						break;
				}
				break;
			case CACTUS:
	//        case CACTUSBROKE:
				switch(sprite[sn].picnum)
				{
					case RADIUSEXPLOSION:
					case RPG:
					case FIRELASER:
					case HYDRENT:
					case HEAVYHBOMB:
						for(k = 0;k<64;k++)
						{
							j = GlobalMembersGAME.EGS(SECT, SX, SY, SZ-(TRAND%(48<<8)), SCRAP3+(TRAND &3), -8, 48, 48, TRAND &2047, (TRAND &63)+64, -(TRAND &4095)-(sprite[i].zvel>>2), i, 5);
							sprite[j].pal = 8;
						}

						if(PN == CACTUS)
							PN = CACTUSBROKE;
						CS &= ~257;
				 //       else deletesprite(i);
						break;
				}
				break;

			case HANGLIGHT:
			case GENERICPOLE2:
				for(k = 0;k<6;k++)
					GlobalMembersGAME.EGS(SECT, SX, SY, SZ-(8<<8), SCRAP1+(TRAND &15), -8, 48, 48, TRAND &2047, (TRAND &63)+64, -(TRAND &4095)-(sprite[i].zvel>>2), i, 5);
				GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, i);
				deletesprite(i);
				break;


			case FANSPRITE:
				PN = FANSPRITEBROKE;
				CS &= (65535-257);
				if(sector[SECT].floorpicnum == FANSHADOW)
					sector[SECT].floorpicnum = FANSHADOWBROKE;

				GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, i);
				s = sprite[i];
				for(j = 0;j<16;j++)
					RANDOMSCRAP;

				break;
			case WATERFOUNTAIN:
			case WATERFOUNTAIN+1:
			case WATERFOUNTAIN+2:
			case WATERFOUNTAIN+3:
				PN = WATERFOUNTAINBROKE;
				GlobalMembersGAME.spawn(i, TOILETWATER);
				break;
			case SATELITE:
			case FUELPOD:
			case SOLARPANNEL:
			case ANTENNA:
				if(sprite[sn].extra != GlobalMembersGLOBAL.actorscrptr[SHOTSPARK1])
				{
					for(j = 0;j<15;j++)
						GlobalMembersGAME.EGS(SECT, SX, SY, sector[SECT].floorz-(12<<8)-(j<<9), SCRAP1+(TRAND &15), -8, 64, 64, TRAND &2047, (TRAND &127)+64, -(TRAND &511)-256, i, 5);
					GlobalMembersGAME.spawn(i, EXPLOSION2);
					deletesprite(i);
				}
				break;
			case BOTTLE1:
			case BOTTLE2:
			case BOTTLE3:
			case BOTTLE4:
			case BOTTLE5:
			case BOTTLE6:
			case BOTTLE8:
			case BOTTLE10:
			case BOTTLE11:
			case BOTTLE12:
			case BOTTLE13:
			case BOTTLE14:
			case BOTTLE15:
			case BOTTLE16:
			case BOTTLE17:
			case BOTTLE18:
			case BOTTLE19:
			case WATERFOUNTAINBROKE:
			case DOMELITE:
			case SUSHIPLATE1:
			case SUSHIPLATE2:
			case SUSHIPLATE3:
			case SUSHIPLATE4:
			case SUSHIPLATE5:
			case WAITTOBESEATED:
			case VASE:
			case STATUEFLASH:
			case STATUE:
				if(PN == BOTTLE10)
					GlobalMembersACTORS.lotsofmoney(ref sprite[i], 4+(TRAND &3));
				else if(PN == STATUE || PN == STATUEFLASH)
				{
					GlobalMembersGAME.lotsofcolourglass(i, -1, 40);
					GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, i);
				}
				else if(PN == VASE)
					GlobalMembersGAME.lotsofglass(i, -1, 40);

				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, i);
				SA = TRAND &2047;
				GlobalMembersGAME.lotsofglass(i, -1, 8);
				deletesprite(i);
				break;
			case FETUS:
				PN = FETUSBROKE;
				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, i);
				GlobalMembersGAME.lotsofglass(i, -1, 10);
				break;
			case FETUSBROKE:
				for(j = 0;j<48;j++)
				{
					GlobalMembersPLAYER.shoot(i, BLOODSPLAT1);
					SA += 333;
				}
				GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, i);
				GlobalMembersSOUNDS.spritesound(SQUISHED, i);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case BOTTLE7:
				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, i);
				GlobalMembersGAME.lotsofglass(i, -1, 10);
				deletesprite(i);
				break;
			case HYDROPLANT:
				PN = BROKEHYDROPLANT;
				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, i);
				GlobalMembersGAME.lotsofglass(i, -1, 10);
				break;

			case FORCESPHERE:
				sprite[i].xrepeat = 0;
				GlobalMembersGLOBAL.hittype[OW].temp_data[0] = 32;
				GlobalMembersGLOBAL.hittype[OW].temp_data[1] = !GlobalMembersGLOBAL.hittype[OW].temp_data[1];
				GlobalMembersGLOBAL.hittype[OW].temp_data[2] ++;
				GlobalMembersGAME.spawn(i, EXPLOSION2);
				break;

			case BROKEHYDROPLANT:
				if(CS &1)
				{
					GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, i);
					SZ += 16<<8;
					CS = 0;
					GlobalMembersGAME.lotsofglass(i, -1, 5);
				}
				break;

			case TOILET:
				PN = TOILETBROKE;
				CS |= (TRAND &1)<<2;
				CS &= ~257;
				GlobalMembersGAME.spawn(i, TOILETWATER);
				GlobalMembersSOUNDS.spritesound(GLASS_BREAKING, i);
				break;

			case STALL:
				PN = STALLBROKE;
				CS |= (TRAND &1)<<2;
				CS &= ~257;
				GlobalMembersGAME.spawn(i, TOILETWATER);
				GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, i);
				break;

			case HYDRENT:
				PN = BROKEFIREHYDRENT;
				GlobalMembersGAME.spawn(i, TOILETWATER);

	//            for(k=0;k<5;k++)
	  //          {
		//            j = EGS(SECT,SX,SY,SZ-(TRAND%(48<<8)),SCRAP3+(TRAND&3),-8,48,48,TRAND&2047,(TRAND&63)+64,-(TRAND&4095)-(sprite[i].zvel>>2),i,5);
		  //          sprite[j].pal = 2;
			//    }
				GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, i);
				break;

			case GRATE1:
				PN = BGRATE1;
				CS &= (65535-256-1);
				GlobalMembersSOUNDS.spritesound(VENT_BUST, i);
				break;

			case CIRCLEPANNEL:
				PN = CIRCLEPANNELBROKE;
				CS &= (65535-256-1);
				GlobalMembersSOUNDS.spritesound(VENT_BUST, i);
				break;
			case PANNEL1:
			case PANNEL2:
				PN = BPANNEL1;
				CS &= (65535-256-1);
				GlobalMembersSOUNDS.spritesound(VENT_BUST, i);
				break;
			case PANNEL3:
				PN = BPANNEL3;
				CS &= (65535-256-1);
				GlobalMembersSOUNDS.spritesound(VENT_BUST, i);
				break;
			case PIPE1:
			case PIPE2:
			case PIPE3:
			case PIPE4:
			case PIPE5:
			case PIPE6:
				switch(PN)
				{
					case PIPE1:
						PN=PIPE1B;
						break;
					case PIPE2:
						PN=PIPE2B;
						break;
					case PIPE3:
						PN=PIPE3B;
						break;
					case PIPE4:
						PN=PIPE4B;
						break;
					case PIPE5:
						PN=PIPE5B;
						break;
					case PIPE6:
						PN=PIPE6B;
						break;
				}

				j = GlobalMembersGAME.spawn(i, STEAM);
				sprite[j].z = sector[SECT].floorz-(32<<8);
				break;

			case MONK:
			case LUKE:
			case INDY:
			case JURYGUY:
				GlobalMembersSOUNDS.spritesound(SLT, i);
				GlobalMembersGAME.spawn(i, SHT);
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case SPACEMARINE:
				sprite[i].extra -= sprite[sn].extra;
				if(sprite[i].extra > 0)
					break;
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT1);
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT2);
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT3);
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT4);
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT1);
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT2);
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT3);
				SA = TRAND &2047;
				GlobalMembersPLAYER.shoot(i, BLOODSPLAT4);
				GlobalMembersACTORS.guts(ref sprite[i], JIBS1, 1, myconnectindex);
				GlobalMembersACTORS.guts(ref sprite[i], JIBS2, 2, myconnectindex);
				GlobalMembersACTORS.guts(ref sprite[i], JIBS3, 3, myconnectindex);
				GlobalMembersACTORS.guts(ref sprite[i], JIBS4, 4, myconnectindex);
				GlobalMembersACTORS.guts(ref sprite[i], JIBS5, 1, myconnectindex);
				GlobalMembersACTORS.guts(ref sprite[i], JIBS3, 6, myconnectindex);
				GlobalMembersSOUNDS.sound(SQUISHED);
				deletesprite(i);
				break;
			case CHAIR1:
			case CHAIR2:
				PN = BROKENCHAIR;
				CS = 0;
				break;
			case CHAIR3:
			case MOVIECAMERA:
			case SCALE:
			case VACUUM:
			case CAMERALIGHT:
			case IVUNIT:
			case POT1:
			case POT2:
			case POT3:
			case TRIPODCAMERA:
				GlobalMembersSOUNDS.spritesound(GLASS_HEAVYBREAK, i);
				s = sprite[i];
				for(j = 0;j<16;j++)
					RANDOMSCRAP;
				deletesprite(i);
				break;
			case PLAYERONWATER:
				i = OW;
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			default:
				if((sprite[i].cstat &16) && SHT == 0 && SLT == 0 && sprite[i].statnum == 0)
					break;

				if((sprite[sn].picnum == FREEZEBLAST || sprite[sn].owner != i) && sprite[i].statnum != 4)
				{
					if(GlobalMembersGAME.badguy(ref sprite[i]) == 1)
					{
						if(sprite[sn].picnum == RPG)
							sprite[sn].extra <<= 1;

						if((PN != DRONE) && (PN != ROTATEGUN) && (PN != COMMANDER) && (PN < GREENSLIME || PN > GREENSLIME+7))
							if(sprite[sn].picnum != FREEZEBLAST)
								if(GlobalMembersGLOBAL.actortype[PN] == 0)
						{
							j = GlobalMembersGAME.spawn(sn, JIBS6);
							if(sprite[sn].pal == 6)
								sprite[j].pal = 6;
							sprite[j].z += (4<<8);
							sprite[j].xvel = 16;
							sprite[j].xrepeat = sprite[j].yrepeat = 24;
							sprite[j].ang += 32-(TRAND &63);
						}

						j = sprite[sn].owner;

						if(j >= 0 && sprite[j].picnum == APLAYER && PN != ROTATEGUN && PN != DRONE)
							if(GlobalMembersGLOBAL.ps[sprite[j].yvel].curr_weapon == SHOTGUN_WEAPON)
						{
							GlobalMembersPLAYER.shoot(i, BLOODSPLAT3);
							GlobalMembersPLAYER.shoot(i, BLOODSPLAT1);
							GlobalMembersPLAYER.shoot(i, BLOODSPLAT2);
							GlobalMembersPLAYER.shoot(i, BLOODSPLAT4);
						}

						if(PN != TANK && PN != BOSS1 && PN != BOSS4 && PN != BOSS2 && PN != BOSS3 && PN != RECON && PN != ROTATEGUN)
						{
							if((sprite[i].cstat &48) == 0)
								SA = (sprite[sn].ang+1024)&2047;
							sprite[i].xvel = -(sprite[sn].extra<<2);
							j = SECT;
							pushmove(SX, SY, SZ, j, 128, (4<<8), (4<<8), CLIPMASK0);
							if(j != SECT && j >= 0 && j < MAXSECTORS)
								changespritesect(i,j);
						}

						if(sprite[i].statnum == 2)
						{
							changespritestat(i,1);
							GlobalMembersGLOBAL.hittype[i].timetosleep = SLEEPTIME;
						}
						if((RX < 24 || PN == SHARK) && sprite[sn].picnum == SHRINKSPARK)
							return;
					}

					if(sprite[i].statnum != 2)
					{
						if(sprite[sn].picnum == FREEZEBLAST && ((PN == APLAYER && sprite[i].pal == 1) || (GlobalMembersGLOBAL.freezerhurtowner == 0 && sprite[sn].owner == i)))
							return;

						GlobalMembersGLOBAL.hittype[i].picnum = sprite[sn].picnum;
						GlobalMembersGLOBAL.hittype[i].extra += sprite[sn].extra;
						GlobalMembersGLOBAL.hittype[i].ang = sprite[sn].ang;
						GlobalMembersGLOBAL.hittype[i].owner = sprite[sn].owner;
					}

					if(sprite[i].statnum == 10)
					{
						p = sprite[i].yvel;
						if(GlobalMembersGLOBAL.ps[p].newowner >= 0)
						{
							GlobalMembersGLOBAL.ps[p].newowner = -1;
							GlobalMembersGLOBAL.ps[p].posx = GlobalMembersGLOBAL.ps[p].oposx;
							GlobalMembersGLOBAL.ps[p].posy = GlobalMembersGLOBAL.ps[p].oposy;
							GlobalMembersGLOBAL.ps[p].posz = GlobalMembersGLOBAL.ps[p].oposz;
							GlobalMembersGLOBAL.ps[p].ang = GlobalMembersGLOBAL.ps[p].oang;

							updatesector(GlobalMembersGLOBAL.ps[p].posx,GlobalMembersGLOBAL.ps[p].posy,GlobalMembersGLOBAL.ps[p].cursectnum);
							GlobalMembersPLAYER.setpal(ref GlobalMembersGLOBAL.ps[p]);

							j = headspritestat[1];
							while(j >= 0)
							{
								if(sprite[j].picnum==CAMERA1)
									sprite[j].yvel = 0;
								j = nextspritestat[j];
							}
						}

						if(RX < 24 && sprite[sn].picnum == SHRINKSPARK)
							return;

						if(sprite[GlobalMembersGLOBAL.hittype[i].owner].picnum != APLAYER)
							if(GlobalMembersGLOBAL.ud.player_skill >= 3)
								sprite[sn].extra += (sprite[sn].extra>>1);
					}

				}
				break;
		}
	}


	public static void allignwarpelevators()
	{
		short i;
		short j;

		i = headspritestat[3];
		while(i >= 0)
		{
			if(SLT == 17 && SS > 16)
			{
				j = headspritestat[3];
				while(j >= 0)
				{
					if((sprite[j].lotag) == 17 && i != j && (SHT) == (sprite[j].hitag))
					{
						sector[sprite[j].sectnum].floorz = sector[SECT].floorz;
						sector[sprite[j].sectnum].ceilingz = sector[SECT].ceilingz;
					}

					j = nextspritestat[j];
				}
			}
			i = nextspritestat[i];
		}
	}




	public static void cheatkeys(short snum)
	{
		short i;
		short k;
		sbyte dainv;
		uint sb_snum;
		uint j;
		player_struct p;

		sb_snum = GlobalMembersGLOBAL.sync[snum].bits;
		p = GlobalMembersGLOBAL.ps[snum];

		if(p.cheat_phase == 1)
			return;

		i = p.aim_mode;
		p.aim_mode = (sb_snum>>23)&1;
		if(p.aim_mode < i)
			p.return_to_center = 9;

		if((sb_snum&(1<<22)) && p.quick_kick == 0)
			if(p.curr_weapon != KNEE_WEAPON || p.kickback_pic == 0)
		{
			p.quick_kick = 14;
			GlobalMembersGAME.FTA(80, ref p);
		}

		if(!(sb_snum&((15<<8)|(1<<12)|(1<<15)|(1<<16)|(1<<22)|(1<<19)|(1<<20)|(1<<21)|(1<<24)|(1<<25)|(1<<27)|(1<<28)|(1<<29)|(1<<30)|(1<<31))))
			p.interface_toggle_flag = 0;
		else if(p.interface_toggle_flag == 0 && (sb_snum&(1<<17)) == 0)
		{
			p.interface_toggle_flag = 1;

			if(sb_snum&(1<<21))
			{
				KB_ClearKeyDown(sc_Pause);
				GlobalMembersGLOBAL.ud.pause_on = !GlobalMembersGLOBAL.ud.pause_on;
				if(GlobalMembersGLOBAL.ud.pause_on == 1 && sb_snum&(1<<5))
					GlobalMembersGLOBAL.ud.pause_on = 2;
				if(GlobalMembersGLOBAL.ud.pause_on)
				{
					MUSIC_Pause();
					FX_StopAllSounds();
					GlobalMembersSOUNDS.clearsoundlocks();
				}
				else
				{
					if (GlobalMembersCONFIG.MusicToggle != 0)
						MUSIC_Continue();
					GlobalMembersGLOBAL.pub = NUMPAGES;
					GlobalMembersGLOBAL.pus = NUMPAGES;
				}
			}

			if(GlobalMembersGLOBAL.ud.pause_on)
				return;

			if(sprite[p.i].extra <= 0)
				return;

			if(sb_snum&(1<<30) && p.newowner == -1)
			{
				switch(p.inven_icon)
				{
					case 4:
						sb_snum |= (1<<25);
						break;
					case 3:
						sb_snum |= (1<<24);
						break;
					case 5:
						sb_snum |= (1<<15);
						break;
					case 1:
						sb_snum |= (1<<16);
						break;
					case 2:
						sb_snum |= (1<<12);
						break;
				}
			}

			if(sb_snum&(1<<15) && p.heat_amount > 0)
			{
				p.heat_on = !p.heat_on;
				GlobalMembersPLAYER.setpal(ref p);
				p.inven_icon = 5;
				GlobalMembersSOUNDS.spritesound(NITEVISION_ONOFF, p.i);
				GlobalMembersGAME.FTA(106+(!p.heat_on), ref p);
			}

			if((sb_snum&(1<<12)))
			{
				if(p.steroids_amount == 400)
				{
					p.steroids_amount--;
					GlobalMembersSOUNDS.spritesound(DUKE_TAKEPILLS, p.i);
					p.inven_icon = 2;
					GlobalMembersGAME.FTA(12, ref p);
				}
				return;
			}

			if(p.newowner == -1)
				if(sb_snum&(1<<20) || sb_snum&(1<<27) || p.refresh_inventory)
			{
				p.invdisptime = 26 *2;

				if(sb_snum&(1<<27))
					k = 1;
				else
					k = 0;

				if(p.refresh_inventory)
					p.refresh_inventory = 0;
				dainv = p.inven_icon;

				i = 0;
				CHECKINV1:

				if(i < 9)
				{
					i++;

					switch(dainv)
					{
						case 4:
							if(p.jetpack_amount > 0 && i > 1)
								break;
							if (k != 0)
								dainv = 5;
							else
								dainv = 3;
							goto CHECKINV1;
						case 6:
							if(p.scuba_amount > 0 && i > 1)
								break;
							if (k != 0)
								dainv = 7;
							else
								dainv = 5;
							goto CHECKINV1;
						case 2:
							if(p.steroids_amount > 0 && i > 1)
								break;
							if (k != 0)
								dainv = 3;
							else
								dainv = 1;
							goto CHECKINV1;
						case 3:
							if(p.holoduke_amount > 0 && i > 1)
								break;
							if (k != 0)
								dainv = 4;
							else
								dainv = 2;
							goto CHECKINV1;
						case 0:
						case 1:
							if(p.firstaid_amount > 0 && i > 1)
								break;
							if (k != 0)
								dainv = 2;
							else
								dainv = 7;
							goto CHECKINV1;
						case 5:
							if(p.heat_amount > 0 && i > 1)
								break;
							if (k != 0)
								dainv = 6;
							else
								dainv = 4;
							goto CHECKINV1;
						case 7:
							if(p.boot_amount > 0 && i > 1)
								break;
							if (k != 0)
								dainv = 1;
							else
								dainv = 6;
							goto CHECKINV1;
					}
				}
				else
					dainv = 0;
				p.inven_icon = dainv;

				switch(dainv)
				{
					case 1:
						GlobalMembersGAME.FTA(3, ref p);
						break;
					case 2:
						GlobalMembersGAME.FTA(90, ref p);
						break;
					case 3:
						GlobalMembersGAME.FTA(91, ref p);
						break;
					case 4:
						GlobalMembersGAME.FTA(88, ref p);
						break;
					case 5:
						GlobalMembersGAME.FTA(101, ref p);
						break;
					case 6:
						GlobalMembersGAME.FTA(89, ref p);
						break;
					case 7:
						GlobalMembersGAME.FTA(6, ref p);
						break;
				}
			}

			j = ((sb_snum&(15<<8))>>8) - 1;

			if(j > 0 && p.kickback_pic > 0)
				p.wantweaponfire = j;

			if(p.last_pissed_time <= (26 *218) && p.show_empty_weapon == 0 && p.kickback_pic == 0 && p.quick_kick == 0 && sprite[p.i].xrepeat > 32 && p.access_incs == 0 && p.knee_incs == 0)
			{
				if((p.weapon_pos == 0 || (p.holster_weapon && p.weapon_pos == -9)))
				{
					if(j == 10 || j == 11)
					{
						k = p.curr_weapon;
						j = (j == 10 ? -1 : 1);
						i = 0;

						while((k >= 0 && k < 10) || (k == GROW_WEAPON && (p.subweapon&(1<<GROW_WEAPON))))
						{
							if(k == GROW_WEAPON)
							{
								if(j == -1)
									k = 5;
								else
									k = 7;

							}
							else
							{
							   k += j;
							   if(k == 6 && p.subweapon&(1<<GROW_WEAPON))
								   k = GROW_WEAPON;
							}

							if(k == -1)
								k = 9;
							else if(k == 10)
								k = 0;

							if(p.gotweapon[k] && p.ammo_amount[k] > 0)
							{
								if(k == SHRINKER_WEAPON && p.subweapon&(1<<GROW_WEAPON))
									k = GROW_WEAPON;
								j = k;
								break;
							}
							else
								if(k == GROW_WEAPON && p.ammo_amount[GROW_WEAPON] == 0 && p.gotweapon[SHRINKER_WEAPON] && p.ammo_amount[SHRINKER_WEAPON] > 0)
							{
								j = SHRINKER_WEAPON;
								p.subweapon &= ~(1<<GROW_WEAPON);
								break;
							}
							else
								if(k == SHRINKER_WEAPON && p.ammo_amount[SHRINKER_WEAPON] == 0 && p.gotweapon[SHRINKER_WEAPON] && p.ammo_amount[GROW_WEAPON] > 0)
							{
								j = GROW_WEAPON;
								p.subweapon |= (1<<GROW_WEAPON);
								break;
							}

							i++;
							if(i == 10)
							{
								GlobalMembersACTORS.addweapon(ref p, KNEE_WEAPON);
								break;
							}
						}
					}

					k = -1;


					if(j == HANDBOMB_WEAPON && p.ammo_amount[HANDBOMB_WEAPON] == 0)
					{
						k = headspritestat[1];
						while(k >= 0)
						{
							if(sprite[k].picnum == HEAVYHBOMB && sprite[k].owner == p.i)
							{
								p.gotweapon[HANDBOMB_WEAPON] = 1;
								j = HANDREMOTE_WEAPON;
								break;
							}
							k = nextspritestat[k];
						}
					}

					if(j == SHRINKER_WEAPON)
					{
						if(GlobalMembersGLOBAL.screenpeek == snum)
							GlobalMembersGLOBAL.pus = NUMPAGES;

						if(p.curr_weapon != GROW_WEAPON && p.curr_weapon != SHRINKER_WEAPON)
						{
							if(p.ammo_amount[GROW_WEAPON] > 0)
							{
								if((p.subweapon&(1<<GROW_WEAPON)) == (1<<GROW_WEAPON))
									j = GROW_WEAPON;
								else if(p.ammo_amount[SHRINKER_WEAPON] == 0)
								{
									j = GROW_WEAPON;
									p.subweapon |= (1<<GROW_WEAPON);
								}
							}
							else if(p.ammo_amount[SHRINKER_WEAPON] > 0)
								p.subweapon &= ~(1<<GROW_WEAPON);
						}
						else if(p.curr_weapon == SHRINKER_WEAPON)
						{
							p.subweapon |= (1<<GROW_WEAPON);
							j = GROW_WEAPON;
						}
						else
							p.subweapon &= ~(1<<GROW_WEAPON);
					}

					if(p.holster_weapon)
					{
						sb_snum |= 1<<19;
						p.weapon_pos = -9;
					}
					else if(p.gotweapon[j] && p.curr_weapon != j)
						switch(j)
					{
						case KNEE_WEAPON:
							GlobalMembersACTORS.addweapon(ref p, KNEE_WEAPON);
							break;
						case PISTOL_WEAPON:
							if (p.ammo_amount[PISTOL_WEAPON] == 0)
								if(p.show_empty_weapon == 0)
							{
								p.last_full_weapon = p.curr_weapon;
								p.show_empty_weapon = 32;
							}
							GlobalMembersACTORS.addweapon(ref p, PISTOL_WEAPON);
							break;
						case SHOTGUN_WEAPON:
							if(p.ammo_amount[SHOTGUN_WEAPON] == 0 && p.show_empty_weapon == 0)
							{
								p.last_full_weapon = p.curr_weapon;
								p.show_empty_weapon = 32;
							}
							GlobalMembersACTORS.addweapon(ref p, SHOTGUN_WEAPON);
							break;
						case CHAINGUN_WEAPON:
							if(p.ammo_amount[CHAINGUN_WEAPON] == 0 && p.show_empty_weapon == 0)
							{
								p.last_full_weapon = p.curr_weapon;
								p.show_empty_weapon = 32;
							}
							GlobalMembersACTORS.addweapon(ref p, CHAINGUN_WEAPON);
							break;
						case RPG_WEAPON:
							if(p.ammo_amount[RPG_WEAPON] == 0)
								if(p.show_empty_weapon == 0)
							{
								p.last_full_weapon = p.curr_weapon;
								p.show_empty_weapon = 32;
							}
							GlobalMembersACTORS.addweapon(ref p, RPG_WEAPON);
							break;
						case DEVISTATOR_WEAPON:
							if(p.ammo_amount[DEVISTATOR_WEAPON] == 0 && p.show_empty_weapon == 0)
							{
								p.last_full_weapon = p.curr_weapon;
								p.show_empty_weapon = 32;
							}
							GlobalMembersACTORS.addweapon(ref p, DEVISTATOR_WEAPON);
							break;
						case FREEZE_WEAPON:
							if(p.ammo_amount[FREEZE_WEAPON] == 0 && p.show_empty_weapon == 0)
							{
								p.last_full_weapon = p.curr_weapon;
								p.show_empty_weapon = 32;
							}
							GlobalMembersACTORS.addweapon(ref p, FREEZE_WEAPON);
							break;
						case GROW_WEAPON:
						case SHRINKER_WEAPON:

							if(p.ammo_amount[j] == 0 && p.show_empty_weapon == 0)
							{
								p.show_empty_weapon = 32;
								p.last_full_weapon = p.curr_weapon;
							}

							GlobalMembersACTORS.addweapon(ref p, j);
							break;
						case HANDREMOTE_WEAPON:
							if(k >= 0) // Found in list of [1]'s
							{
								p.curr_weapon = HANDREMOTE_WEAPON;
								p.last_weapon = -1;
								p.weapon_pos = 10;
							}
							break;
						case HANDBOMB_WEAPON:
							if(p.ammo_amount[HANDBOMB_WEAPON] > 0 && p.gotweapon[HANDBOMB_WEAPON])
								GlobalMembersACTORS.addweapon(ref p, HANDBOMB_WEAPON);
							break;
						case TRIPBOMB_WEAPON:
							if(p.ammo_amount[TRIPBOMB_WEAPON] > 0 && p.gotweapon[TRIPBOMB_WEAPON])
								GlobalMembersACTORS.addweapon(ref p, TRIPBOMB_WEAPON);
							break;
					}
				}

				if(sb_snum&(1<<19))
				{
					if(p.curr_weapon > KNEE_WEAPON)
					{
						if(p.holster_weapon == 0 && p.weapon_pos == 0)
						{
							p.holster_weapon = 1;
							p.weapon_pos = -1;
							GlobalMembersGAME.FTA(73, ref p);
						}
						else if(p.holster_weapon == 1 && p.weapon_pos == -9)
						{
							p.holster_weapon = 0;
							p.weapon_pos = 10;
							GlobalMembersGAME.FTA(74, ref p);
						}
					}
				}
			}

			if(sb_snum&(1<<24) && p.newowner == -1)
			{
				if(p.holoduke_on == -1)
				{

					if(p.holoduke_amount > 0)
					{
						p.inven_icon = 3;

						p.holoduke_on = i = GlobalMembersGAME.EGS(p.cursectnum, p.posx, p.posy, p.posz+(30<<8), APLAYER, -64, 0, 0, p.ang, 0, 0, -1, 10);
						T4 = T5 = 0;
						SP = snum;
						sprite[i].extra = 0;
						GlobalMembersGAME.FTA(47, ref p);
					}
					else
						GlobalMembersGAME.FTA(49, ref p);
					GlobalMembersSOUNDS.spritesound(TELEPORTER, p.holoduke_on);

				}
				else
				{
					GlobalMembersSOUNDS.spritesound(TELEPORTER, p.holoduke_on);
					p.holoduke_on = -1;
					GlobalMembersGAME.FTA(48, ref p);
				}
			}

			if(sb_snum&(1<<16))
			{
				if(p.firstaid_amount > 0 && sprite[p.i].extra < GlobalMembersGLOBAL.max_player_health)
				{
					j = GlobalMembersGLOBAL.max_player_health-sprite[p.i].extra;

					if(p.firstaid_amount > j)
					{
						p.firstaid_amount -= j;
						sprite[p.i].extra = GlobalMembersGLOBAL.max_player_health;
						p.inven_icon = 1;
					}
					else
					{
						sprite[p.i].extra += p.firstaid_amount;
						p.firstaid_amount = 0;
						GlobalMembersACTORS.checkavailinven(ref p);
					}
					GlobalMembersSOUNDS.spritesound(DUKE_USEMEDKIT, p.i);
				}
			}

			if(sb_snum&(1<<25) && p.newowner == -1)
			{
				if(p.jetpack_amount > 0)
				{
					p.jetpack_on = !p.jetpack_on;
					if(p.jetpack_on)
					{
						p.inven_icon = 4;
						if(p.scream_voice > FX_Ok)
						{
							FX_StopSound(p.scream_voice);
							testcallback(DUKE_SCREAM);
							p.scream_voice = FX_Ok;
						}

						GlobalMembersSOUNDS.spritesound(DUKE_JETPACK_ON, p.i);

						GlobalMembersGAME.FTA(52, ref p);
					}
					else
					{
						p.hard_landing = 0;
						p.poszv = 0;
						GlobalMembersSOUNDS.spritesound(DUKE_JETPACK_OFF, p.i);
						GlobalMembersSOUNDS.stopsound(DUKE_JETPACK_IDLE);
						GlobalMembersSOUNDS.stopsound(DUKE_JETPACK_ON);
						GlobalMembersGAME.FTA(53, ref p);
					}
				}
				else
					GlobalMembersGAME.FTA(50, ref p);
			}

			if(sb_snum&(1<<28) && p.one_eighty_count == 0)
				p.one_eighty_count = -1024;
		}
	}

	public static void checksectors(short snum)
	{
		int i = -1;
		int oldz;
		player_struct p;
		short j;
		short hitscanwall;

		p = GlobalMembersGLOBAL.ps[snum];

		switch(sector[p.cursectnum].lotag)
		{

			case 32767:
				sector[p.cursectnum].lotag = 0;
				GlobalMembersGAME.FTA(9, ref p);
				p.secret_rooms++;
				return;
			case -1:
				for(i = connecthead;i>=0;i = connectpoint2[i])
					GlobalMembersGLOBAL.ps[i].gm = MODE_EOL;
				sector[p.cursectnum].lotag = 0;
				if(GlobalMembersGLOBAL.ud.from_bonus)
				{
					GlobalMembersGLOBAL.ud.level_number = GlobalMembersGLOBAL.ud.from_bonus;
					GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
					GlobalMembersGLOBAL.ud.from_bonus = 0;
				}
				else
				{
					GlobalMembersGLOBAL.ud.level_number++;
					if((GlobalMembersGLOBAL.ud.volume_number && GlobalMembersGLOBAL.ud.level_number > 10) || GlobalMembersGLOBAL.ud.level_number > 5)
						GlobalMembersGLOBAL.ud.level_number = 0;
					GlobalMembersGLOBAL.ud.m_level_number = GlobalMembersGLOBAL.ud.level_number;
				}
				return;
			case -2:
				sector[p.cursectnum].lotag = 0;
				p.timebeforeexit = 26 *8;
				p.customexitsound = sector[p.cursectnum].hitag;
				return;
			default:
				if(sector[p.cursectnum].lotag >= 10000 && sector[p.cursectnum].lotag < 16383)
				{
					if(snum == GlobalMembersGLOBAL.screenpeek || GlobalMembersGLOBAL.ud.coop == 1)
						GlobalMembersSOUNDS.spritesound(sector[p.cursectnum].lotag-10000, p.i);
					sector[p.cursectnum].lotag = 0;
				}
				break;

		}

		//After this point the the player effects the map with space

		if(p.gm &MODE_TYPE || sprite[p.i].extra <= 0)
			return;

		if(GlobalMembersGLOBAL.ud.cashman && GlobalMembersGLOBAL.sync[snum].bits&(1<<29))
			GlobalMembersACTORS.lotsofmoney(ref sprite[p.i], 2);

		if(p.newowner >= 0)
		{
			if(klabs(GlobalMembersGLOBAL.sync[snum].svel) > 768 || klabs(GlobalMembersGLOBAL.sync[snum].fvel) > 768)
			{
				i = -1;
				goto CLEARCAMERAS;
			}
		}

		if(!(GlobalMembersGLOBAL.sync[snum].bits&(1<<29)) && !(GlobalMembersGLOBAL.sync[snum].bits&(1<<31)))
			p.toggle_key_flag = 0;

		else if(!p.toggle_key_flag)
		{

			if((GlobalMembersGLOBAL.sync[snum].bits&(1<<31)))
			{
				if(p.newowner >= 0)
				{
					i = -1;
					goto CLEARCAMERAS;
				}
				return;
			}

			GlobalMembersGLOBAL.neartagsprite = -1;
			p.toggle_key_flag = 1;
			hitscanwall = -1;

			i = GlobalMembersPLAYER.hitawall(ref p, ref hitscanwall);

			if(i < 1280 && hitscanwall >= 0 && wall[hitscanwall].overpicnum == MIRROR)
				if(wall[hitscanwall].lotag > 0 && GlobalMembersGLOBAL.Sound[wall[hitscanwall].lotag].num == 0 && snum == GlobalMembersGLOBAL.screenpeek)
			{
				GlobalMembersSOUNDS.spritesound(wall[hitscanwall].lotag, p.i);
				return;
			}

			if(hitscanwall >= 0 && (wall[hitscanwall].cstat &16))
				switch(wall[hitscanwall].overpicnum)
			{
				default:
					if(wall[hitscanwall].lotag)
						return;
			}

			if(p.newowner >= 0)
				neartag(p.oposx,p.oposy,p.oposz,sprite[p.i].sectnum,p.oang,GlobalMembersGLOBAL.neartagsector,GlobalMembersGLOBAL.neartagwall,GlobalMembersGLOBAL.neartagsprite,GlobalMembersGLOBAL.neartaghitdist,1280,1);
			else
			{
				neartag(p.posx,p.posy,p.posz,sprite[p.i].sectnum,p.oang,GlobalMembersGLOBAL.neartagsector,GlobalMembersGLOBAL.neartagwall,GlobalMembersGLOBAL.neartagsprite,GlobalMembersGLOBAL.neartaghitdist,1280,1);
				if(GlobalMembersGLOBAL.neartagsprite == -1 && GlobalMembersGLOBAL.neartagwall == -1 && GlobalMembersGLOBAL.neartagsector == -1)
					neartag(p.posx,p.posy,p.posz+(8<<8),sprite[p.i].sectnum,p.oang,GlobalMembersGLOBAL.neartagsector,GlobalMembersGLOBAL.neartagwall,GlobalMembersGLOBAL.neartagsprite,GlobalMembersGLOBAL.neartaghitdist,1280,1);
				if(GlobalMembersGLOBAL.neartagsprite == -1 && GlobalMembersGLOBAL.neartagwall == -1 && GlobalMembersGLOBAL.neartagsector == -1)
					neartag(p.posx,p.posy,p.posz+(16<<8),sprite[p.i].sectnum,p.oang,GlobalMembersGLOBAL.neartagsector,GlobalMembersGLOBAL.neartagwall,GlobalMembersGLOBAL.neartagsprite,GlobalMembersGLOBAL.neartaghitdist,1280,1);
				if(GlobalMembersGLOBAL.neartagsprite == -1 && GlobalMembersGLOBAL.neartagwall == -1 && GlobalMembersGLOBAL.neartagsector == -1)
				{
					neartag(p.posx,p.posy,p.posz+(16<<8),sprite[p.i].sectnum,p.oang,GlobalMembersGLOBAL.neartagsector,GlobalMembersGLOBAL.neartagwall,GlobalMembersGLOBAL.neartagsprite,GlobalMembersGLOBAL.neartaghitdist,1280,3);
					if(GlobalMembersGLOBAL.neartagsprite >= 0)
					{
						switch(sprite[GlobalMembersGLOBAL.neartagsprite].picnum)
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
							case TOUGHGAL:
								return;
						}
					}

					GlobalMembersGLOBAL.neartagsprite = -1;
					GlobalMembersGLOBAL.neartagwall = -1;
					GlobalMembersGLOBAL.neartagsector = -1;
				}
			}

			if(p.newowner == -1 && GlobalMembersGLOBAL.neartagsprite == -1 && GlobalMembersGLOBAL.neartagsector == -1 && GlobalMembersGLOBAL.neartagwall == -1)
				if (GlobalMembersSECTOR.isanunderoperator(sector[sprite[p.i].sectnum].lotag) != 0)
					GlobalMembersGLOBAL.neartagsector = sprite[p.i].sectnum;

			if(GlobalMembersGLOBAL.neartagsector >= 0 && (sector[GlobalMembersGLOBAL.neartagsector].lotag &16384))
				return;

			if(GlobalMembersGLOBAL.neartagsprite == -1 && GlobalMembersGLOBAL.neartagwall == -1)
				if(sector[p.cursectnum].lotag == 2)
				{
					oldz = GlobalMembersPLAYER.hitasprite(p.i, ref GlobalMembersGLOBAL.neartagsprite);
					if(oldz > 1280)
						GlobalMembersGLOBAL.neartagsprite = -1;
				}

			if(GlobalMembersGLOBAL.neartagsprite >= 0)
			{
				if (GlobalMembersSECTOR.checkhitswitch(snum, GlobalMembersGLOBAL.neartagsprite, 1) != 0)
					return;

				switch(sprite[GlobalMembersGLOBAL.neartagsprite].picnum)
				{
					case TOILET:
					case STALL:
						if(p.last_pissed_time == 0)
						{
							if(GlobalMembersGLOBAL.ud.lockout == 0)
								GlobalMembersSOUNDS.spritesound(DUKE_URINATE, p.i);

							p.last_pissed_time = 26 *220;
							p.transporter_hold = 29 *2;
							if(p.holster_weapon == 0)
							{
								p.holster_weapon = 1;
								p.weapon_pos = -1;
							}
							if(sprite[p.i].extra <= (GlobalMembersGLOBAL.max_player_health-(GlobalMembersGLOBAL.max_player_health/10)))
							{
								sprite[p.i].extra += GlobalMembersGLOBAL.max_player_health/10;
								p.last_extra = sprite[p.i].extra;
							}
							else if(sprite[p.i].extra < GlobalMembersGLOBAL.max_player_health)
								 sprite[p.i].extra = GlobalMembersGLOBAL.max_player_health;
						}
						else if(GlobalMembersGLOBAL.Sound[FLUSH_TOILET].num == 0)
							GlobalMembersSOUNDS.spritesound(FLUSH_TOILET, p.i);
						return;

					case NUKEBUTTON:

						GlobalMembersPLAYER.hitawall(ref p, ref j);
						if(j >= 0 && wall[j].overpicnum == 0)
							if(GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.neartagsprite].temp_data[0] == 0)
						{
							GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.neartagsprite].temp_data[0] = 1;
							sprite[GlobalMembersGLOBAL.neartagsprite].owner = p.i;
							p.buttonpalette = sprite[GlobalMembersGLOBAL.neartagsprite].pal;
							if(p.buttonpalette)
								GlobalMembersGLOBAL.ud.secretlevel = sprite[GlobalMembersGLOBAL.neartagsprite].lotag;
							else
								GlobalMembersGLOBAL.ud.secretlevel = 0;
						}
						return;
					case WATERFOUNTAIN:
						if(GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.neartagsprite].temp_data[0] != 1)
						{
							GlobalMembersGLOBAL.hittype[GlobalMembersGLOBAL.neartagsprite].temp_data[0] = 1;
							sprite[GlobalMembersGLOBAL.neartagsprite].owner = p.i;

							if(sprite[p.i].extra < GlobalMembersGLOBAL.max_player_health)
							{
								sprite[p.i].extra++;
								GlobalMembersSOUNDS.spritesound(DUKE_DRINKING, p.i);
							}
						}
						return;
					case PLUG:
						GlobalMembersSOUNDS.spritesound(SHORT_CIRCUIT, p.i);
						sprite[p.i].extra -= 2+(TRAND &3);
						p.pals[0] = 48;
						p.pals[1] = 48;
						p.pals[2] = 64;
						p.pals_time = 32;
						break;
					case VIEWSCREEN:
					case VIEWSCREEN2:
						{
							i = headspritestat[1];

							while(i >= 0)
							{
								if(PN == CAMERA1 && SP == 0 && sprite[GlobalMembersGLOBAL.neartagsprite].hitag == SLT)
								{
									SP = 1; //Using this camera
									GlobalMembersSOUNDS.spritesound(MONITOR_ACTIVE, GlobalMembersGLOBAL.neartagsprite);

									sprite[GlobalMembersGLOBAL.neartagsprite].owner = i;
									sprite[GlobalMembersGLOBAL.neartagsprite].yvel = 1;


									j = p.cursectnum;
									p.cursectnum = SECT;
									GlobalMembersPLAYER.setpal(ref p);
									p.cursectnum = j;

									// parallaxtype = 2;
									p.newowner = i;
									return;
								}
								i = nextspritestat[i];
						   }
						}

						CLEARCAMERAS:

						if(i < 0)
						{
							p.posx = p.oposx;
							p.posy = p.oposy;
							p.posz = p.oposz;
							p.ang = p.oang;
							p.newowner = -1;

							updatesector(p.posx, p.posy, p.cursectnum);
							GlobalMembersPLAYER.setpal(ref p);


							i = headspritestat[1];
							while(i >= 0)
							{
								if(PN==CAMERA1)
									SP = 0;
								i = nextspritestat[i];
							}
						}
						else if(p.newowner >= 0)
							p.newowner = -1;

						if(KB_KeyPressed(sc_Escape))
							KB_ClearKeyDown(sc_Escape);

						return;
				}
			}

			if((GlobalMembersGLOBAL.sync[snum].bits&(1<<29)) == 0)
				return;
			else if(p.newowner >= 0)
			{
				i = -1;
				goto CLEARCAMERAS;
			}

			if(GlobalMembersGLOBAL.neartagwall == -1 && GlobalMembersGLOBAL.neartagsector == -1 && GlobalMembersGLOBAL.neartagsprite == -1)
				if(klabs(GlobalMembersPLAYER.hits(p.i)) < 512)
			{
				if((TRAND &255) < 16)
					GlobalMembersSOUNDS.spritesound(DUKE_SEARCH2, p.i);
				else
					GlobalMembersSOUNDS.spritesound(DUKE_SEARCH, p.i);
				return;
			}

			if(GlobalMembersGLOBAL.neartagwall >= 0)
			{
				if(wall[GlobalMembersGLOBAL.neartagwall].lotag > 0 && GlobalMembersSECTOR.isadoorwall(wall[GlobalMembersGLOBAL.neartagwall].picnum) != 0)
				{
					if(hitscanwall == GlobalMembersGLOBAL.neartagwall || hitscanwall == -1)
						GlobalMembersSECTOR.checkhitswitch(snum, GlobalMembersGLOBAL.neartagwall, 0);
					return;
				}
				else if(p.newowner >= 0)
				{
					i = -1;
					goto CLEARCAMERAS;
				}
			}

			if(GlobalMembersGLOBAL.neartagsector >= 0 && (sector[GlobalMembersGLOBAL.neartagsector].lotag &16384) == 0 && GlobalMembersSECTOR.isanearoperator(sector[GlobalMembersGLOBAL.neartagsector].lotag) != 0)
			{
				i = headspritesect[GlobalMembersGLOBAL.neartagsector];
				while(i >= 0)
				{
					if(PN == ACTIVATOR || PN == MASTERSWITCH)
						return;
					i = nextspritesect[i];
				}
				GlobalMembersSECTOR.operatesectors(GlobalMembersGLOBAL.neartagsector, p.i);
			}
			else if((sector[sprite[p.i].sectnum].lotag &16384) == 0)
			{
				if (GlobalMembersSECTOR.isanunderoperator(sector[sprite[p.i].sectnum].lotag) != 0)
				{
					i = headspritesect[sprite[p.i].sectnum];
					while(i >= 0)
					{
						if(PN == ACTIVATOR || PN == MASTERSWITCH)
							return;
						i = nextspritesect[i];
					}
					GlobalMembersSECTOR.operatesectors(sprite[p.i].sectnum, p.i);
				}
				else
					GlobalMembersSECTOR.checkhitswitch(snum, GlobalMembersGLOBAL.neartagwall, 0);
			}
		}
	}
}

