using System;

public static class GlobalMembersGAMEDEF
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


	//extern short otherp;

	internal static short total_lines;
	internal static short line_number;
	internal static sbyte checking_ifelse;
	internal static sbyte parsing_state;
	internal static string last_used_text;
	internal static short num_squigilly_brackets;
	internal static int last_used_size;

	internal static short g_i;
	internal static short g_p;
	internal static int g_x;
	internal static int *g_t;
	internal static spritetype g_sp;

	#define NUMKEYWORDS

	public static string[] keyw = { "definelevelname", "actor", "addammo", "ifrnd", "enda", "ifcansee", "ifhitweapon", "action", "ifpdistl", "ifpdistg", "else", "strength", "break", "shoot", "palfrom", "sound", "fall", "state", "ends", "define", "//", "ifai", "killit", "addweapon", "ai", "addphealth", "ifdead", "ifsquished", "sizeto", "{", "}", "spawn", "move", "ifwasweapon", "ifaction", "ifactioncount", "resetactioncount", "debris", "pstomp", "/*", "cstat", "ifmove", "resetplayer", "ifonwater", "ifinwater", "ifcanshoottarget", "ifcount", "resetcount", "addinventory", "ifactornotstayput", "hitradius", "ifp", "count", "ifactor", "music", "include", "ifstrength", "definesound", "guts", "ifspawnedby", "gamestartup", "wackplayer", "ifgapzl", "ifhitspace", "ifoutside", "ifmultiplayer", "operate", "ifinspace", "debug", "endofgame", "ifbulletnear", "ifrespawn", "iffloordistl", "ifceilingdistl", "spritepal", "ifpinventory", "betaname", "cactor", "ifphealthl", "definequote", "quote", "ifinouterspace", "ifnotmoving", "respawnhitag", "tip", "ifspritepal", "money", "soundonce", "addkills", "stopsound", "ifawayfromwall", "ifcanseetarget", "globalsound", "lotsofglass", "ifgotweaponce", "getlastpal", "pkick", "mikesnd", "useractor", "sizeat", "addstrength", "cstator", "mail", "paper", "tossweapon", "sleeptime", "nullop", "definevolumename", "defineskillname", "ifnosounds", "clipdist", "ifangdiffl" };


	public static short getincangle(short a,short na)
	{
		a &= 2047;
		na &= 2047;

		if(klabs(a-na) < 1024)
			return (na-a);
		else
		{
			if(na > 1024)
				na -= 2048;
			if(a > 1024)
				a -= 2048;

			na -= 2048;
			a -= 2048;
			return (na-a);
		}
	}

	public static sbyte ispecial(sbyte c)
	{
		if(c == 0x0a)
		{
			line_number++;
			return 1;
		}

		if(c == ' ' || c == 0x0d)
			return 1;

		return 0;
	}

	public static sbyte isaltok(sbyte c)
	{
		return (char.IsLetterOrDigit(c) || c == '{' || c == '}' || c == '/' || c == '*' || c == '-' || c == '_' || c == '.');
	}

	public static void getglobalz(short i)
	{
		int hz;
		int lz;
		int zr;

		spritetype s = sprite[i];

		if(s.statnum == 10 || s.statnum == 6 || s.statnum == 2 || s.statnum == 1 || s.statnum == 4)
		{
			if(s.statnum == 4)
				zr = 4;
			else
				zr = 127;

			getzrange(s.x, s.y, s.z-(FOURSLEIGHT), s.sectnum, GlobalMembersGLOBAL.hittype[i].ceilingz, hz, GlobalMembersGLOBAL.hittype[i].floorz, lz, zr, CLIPMASK0);

			if((lz &49152) == 49152 && (sprite[lz&(MAXSPRITES-1)].cstat &48) == 0)
			{
				lz &= (MAXSPRITES-1);
				if(GlobalMembersGAME.badguy(ref sprite[lz]) != 0 && sprite[lz].pal != 1)
				{
					if(s.statnum != 4)
					{
						GlobalMembersGLOBAL.hittype[i].dispicnum = -4; // No shadows on actors
						s.xvel = -256;
						GlobalMembersACTORS.ssp(i, CLIPMASK0);
					}
				}
				else if(sprite[lz].picnum == APLAYER && GlobalMembersGAME.badguy(ref s) != 0)
				{
					GlobalMembersGLOBAL.hittype[i].dispicnum = -4; // No shadows on actors
					s.xvel = -256;
					GlobalMembersACTORS.ssp(i, CLIPMASK0);
				}
				else if(s.statnum == 4 && sprite[lz].picnum == APLAYER)
					if(s.owner == lz)
				{
					GlobalMembersGLOBAL.hittype[i].ceilingz = sector[s.sectnum].ceilingz;
					GlobalMembersGLOBAL.hittype[i].floorz = sector[s.sectnum].floorz;
				}
			}
		}
		else
		{
			GlobalMembersGLOBAL.hittype[i].ceilingz = sector[s.sectnum].ceilingz;
			GlobalMembersGLOBAL.hittype[i].floorz = sector[s.sectnum].floorz;
		}
	}


	public static void makeitfall(short i)
	{
		spritetype s = sprite[i];
		int hz;
		int lz;
		int c;

		if (GlobalMembersACTORS.floorspace(s.sectnum) != 0)
			c = 0;
		else
		{
			if(GlobalMembersACTORS.ceilingspace(s.sectnum) != 0 || sector[s.sectnum].lotag == 2)
				c = GlobalMembersGLOBAL.gc/6;
			else
				c = GlobalMembersGLOBAL.gc;
		}

		if((s.statnum == 1 || s.statnum == 10 || s.statnum == 2 || s.statnum == 6))
			getzrange(s.x, s.y, s.z-(FOURSLEIGHT), s.sectnum, GlobalMembersGLOBAL.hittype[i].ceilingz, hz, GlobalMembersGLOBAL.hittype[i].floorz, lz, 127, CLIPMASK0);
		else
		{
			GlobalMembersGLOBAL.hittype[i].ceilingz = sector[s.sectnum].ceilingz;
			GlobalMembersGLOBAL.hittype[i].floorz = sector[s.sectnum].floorz;
		}

		if(s.z < GlobalMembersGLOBAL.hittype[i].floorz-(FOURSLEIGHT))
		{
			if(sector[s.sectnum].lotag == 2 && s.zvel > 3122)
				s.zvel = 3144;
			if(s.zvel < 6144)
				s.zvel += c;
			else
				s.zvel = 6144;
			s.z += s.zvel;
		}
		if(s.z >= GlobalMembersGLOBAL.hittype[i].floorz-(FOURSLEIGHT))
		{
			s.z = GlobalMembersGLOBAL.hittype[i].floorz - FOURSLEIGHT;
			s.zvel = 0;
		}
	}


	public static void getlabel()
	{
		int i;

		while(char.IsLetterOrDigit(GlobalMembersGLOBAL.textptr) == 0)
		{
			if(GlobalMembersGLOBAL.textptr == 0x0a)
				line_number++;
			GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
			if(GlobalMembersGLOBAL.textptr == 0)
				return;
		}

		i = 0;
		while(GlobalMembersGAMEDEF.ispecial(GlobalMembersGLOBAL.textptr) == 0)
			GlobalMembersGLOBAL.label[(GlobalMembersGLOBAL.labelcnt<<6)+i++] = *(GlobalMembersGLOBAL.textptr ++);

		GlobalMembersGLOBAL.label = GlobalMembersGLOBAL.label.Substring(0, (GlobalMembersGLOBAL.labelcnt<<6)+i);
	}

	public static int keyword()
	{
		int i;
//C++ TO C# CONVERTER TODO TASK: Pointer arithmetic is detected on this variable, so pointers on this variable are left unchanged.
		sbyte *temptextptr;

		temptextptr = GlobalMembersGLOBAL.textptr;

		while(GlobalMembersGAMEDEF.isaltok(*temptextptr) == 0)
		{
			temptextptr++;
			if(*temptextptr == 0)
				return 0;
		}

		i = 0;
		while (GlobalMembersGAMEDEF.isaltok(*temptextptr) != 0)
		{
			GlobalMembersGLOBAL.tempbuf[i] = *(temptextptr++);
			i++;
		}
		GlobalMembersGLOBAL.tempbuf[i] = 0;

		for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
			if(string.Compare(GlobalMembersGLOBAL.tempbuf,keyw[i]) == 0)
				return i;

		return -1;
	}

	public static int transword() //Returns its code #
	{
		int i;
		int l;

		while(GlobalMembersGAMEDEF.isaltok(GlobalMembersGLOBAL.textptr) == 0)
		{
			if(GlobalMembersGLOBAL.textptr == 0x0a)
				line_number++;
			if(GlobalMembersGLOBAL.textptr == 0)
				return -1;
			GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
		}

		l = 0;
		while (GlobalMembersGAMEDEF.isaltok(*GlobalMembersGLOBAL.textptr.Substring(l)) != 0)
		{
			GlobalMembersGLOBAL.tempbuf[l] = GlobalMembersGLOBAL.textptr[l];
			l++;
		}
		GlobalMembersGLOBAL.tempbuf[l] = 0;

		for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
		{
			if(string.Compare(GlobalMembersGLOBAL.tempbuf,keyw[i]) == 0)
			{
				GlobalMembersGLOBAL.scriptptr = i;
				GlobalMembersGLOBAL.textptr += l;
				GlobalMembersGLOBAL.scriptptr++;
				return i;
			}
		}

		GlobalMembersGLOBAL.textptr += l;

		if(GlobalMembersGLOBAL.tempbuf[0] == '{' && GlobalMembersGLOBAL.tempbuf[1] != 0)
			Console.Write("  * ERROR!(L{0:D}) Expecting a SPACE or CR between '{{' and '{1}'.\n",line_number,GlobalMembersGLOBAL.tempbuf+1);
		else if(GlobalMembersGLOBAL.tempbuf[0] == '}' && GlobalMembersGLOBAL.tempbuf[1] != 0)
			Console.Write("  * ERROR!(L{0:D}) Expecting a SPACE or CR between '}}' and '{1}'.\n",line_number,GlobalMembersGLOBAL.tempbuf+1);
		else if(GlobalMembersGLOBAL.tempbuf[0] == '/' && GlobalMembersGLOBAL.tempbuf[1] == '/' && GlobalMembersGLOBAL.tempbuf[2] != 0)
			Console.Write("  * ERROR!(L{0:D}) Expecting a SPACE between '//' and '{1}'.\n",line_number,GlobalMembersGLOBAL.tempbuf+2);
		else if(GlobalMembersGLOBAL.tempbuf[0] == '/' && GlobalMembersGLOBAL.tempbuf[1] == '*' && GlobalMembersGLOBAL.tempbuf[2] != 0)
			Console.Write("  * ERROR!(L{0:D}) Expecting a SPACE between '/*' and '{1}'.\n",line_number,GlobalMembersGLOBAL.tempbuf+2);
		else if(GlobalMembersGLOBAL.tempbuf[0] == '*' && GlobalMembersGLOBAL.tempbuf[1] == '/' && GlobalMembersGLOBAL.tempbuf[2] != 0)
			Console.Write("  * ERROR!(L{0:D}) Expecting a SPACE between '*/' and '{1}'.\n",line_number,GlobalMembersGLOBAL.tempbuf+2);
		else
			Console.Write("  * ERROR!(L{0:D}) Expecting key word, but found '{1}'.\n",line_number,GlobalMembersGLOBAL.tempbuf);

		GlobalMembersGLOBAL.error++;
		return -1;
	}

	public static void transnum()
	{
		int i;
		int l;

		while(GlobalMembersGAMEDEF.isaltok(GlobalMembersGLOBAL.textptr) == 0)
		{
			if(GlobalMembersGLOBAL.textptr == 0x0a)
				line_number++;
			GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
			if(GlobalMembersGLOBAL.textptr == 0)
				return;
		}


		l = 0;
		while (GlobalMembersGAMEDEF.isaltok(*GlobalMembersGLOBAL.textptr.Substring(l)) != 0)
		{
			GlobalMembersGLOBAL.tempbuf[l] = GlobalMembersGLOBAL.textptr[l];
			l++;
		}
		GlobalMembersGLOBAL.tempbuf[l] = 0;

		for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
			if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),keyw[i]) == 0)
		{
			GlobalMembersGLOBAL.error++;
			Console.Write("  * ERROR!(L{0:D}) Symbol '{1}' is a key word.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
			GlobalMembersGLOBAL.textptr +=l;
		}


		for(i = 0;i<GlobalMembersGLOBAL.labelcnt;i++)
		{
			if(string.Compare(GlobalMembersGLOBAL.tempbuf,GlobalMembersGLOBAL.label + (i<<6)) == 0)
			{
				GlobalMembersGLOBAL.scriptptr = GlobalMembersGLOBAL.labelcode[i];
				GlobalMembersGLOBAL.scriptptr++;
				GlobalMembersGLOBAL.textptr += l;
				return;
			}
		}

		if(char.IsDigit(GlobalMembersGLOBAL.textptr) == 0 && GlobalMembersGLOBAL.textptr != '-')
		{
			Console.Write("  * ERROR!(L{0:D}) Parameter '{1}' is undefined.\n",line_number,GlobalMembersGLOBAL.tempbuf);
			GlobalMembersGLOBAL.error++;
			GlobalMembersGLOBAL.textptr +=l;
			return;
		}

		GlobalMembersGLOBAL.scriptptr = Convert.ToInt64(GlobalMembersGLOBAL.textptr);
		GlobalMembersGLOBAL.scriptptr++;

		GlobalMembersGLOBAL.textptr += l;
	}


	public static sbyte parsecommand()
	{
		int i;
		int j;
		int k;
		int tempscrptr;
		sbyte done;
		string origtptr;
		sbyte temp_ifelse_check;
		sbyte tw;
		short temp_line_number;
		int fp;

		if(GlobalMembersGLOBAL.error > 12 || (GlobalMembersGLOBAL.textptr == '\0') || (*GlobalMembersGLOBAL.textptr.Substring(1) == '\0'))
			return 1;

		tw = GlobalMembersGAMEDEF.transword();

		switch(tw)
		{
			default:
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case -1:
				return 0; //End
			case 39: //Rem endrem
				GlobalMembersGLOBAL.scriptptr--;
				j = line_number;
				do
				{
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
					if(GlobalMembersGLOBAL.textptr == 0x0a)
						line_number++;
					if(GlobalMembersGLOBAL.textptr == 0)
					{
						Console.Write("  * ERROR!(L{0:D}) Found '/*' with no '*/'.\n",j,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
						GlobalMembersGLOBAL.error++;
						return 0;
					}
				}
				while(GlobalMembersGLOBAL.textptr != '*' || *GlobalMembersGLOBAL.textptr.Substring(1) != '/');
				GlobalMembersGLOBAL.textptr +=2;
				return 0;
			case 17:
				if(GlobalMembersGLOBAL.parsing_actor == 0 && parsing_state == 0)
				{
					GlobalMembersGAMEDEF.getlabel();
					GlobalMembersGLOBAL.scriptptr--;
					GlobalMembersGLOBAL.labelcode[GlobalMembersGLOBAL.labelcnt] = (int) GlobalMembersGLOBAL.scriptptr;
					GlobalMembersGLOBAL.labelcnt++;

					parsing_state = 1;

					return 0;
				}

				GlobalMembersGAMEDEF.getlabel();

				for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
					if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),keyw[i]) == 0)
					{
						GlobalMembersGLOBAL.error++;
						Console.Write("  * ERROR!(L{0:D}) Symbol '{1}' is a key word.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
						return 0;
					}

				for(j = 0;j<GlobalMembersGLOBAL.labelcnt;j++)
				{
					if(string.Compare(GlobalMembersGLOBAL.label + (j<<6),GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6)) == 0)
					{
						GlobalMembersGLOBAL.scriptptr = GlobalMembersGLOBAL.labelcode[j];
						break;
					}
				}

				if(j == GlobalMembersGLOBAL.labelcnt)
				{
					Console.Write("  * ERROR!(L{0:D}) State '{1}' not found.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
					GlobalMembersGLOBAL.error++;
				}
				GlobalMembersGLOBAL.scriptptr++;
				return 0;

			case 15:
			case 92:
			case 87:
			case 89:
			case 93:
				GlobalMembersGAMEDEF.transnum();
				return 0;

			case 18:
				if(parsing_state == 0)
				{
					Console.Write("  * ERROR!(L{0:D}) Found 'ends' with no 'state'.\n",line_number);
					GlobalMembersGLOBAL.error++;
				}
	//            else
				{
					if(num_squigilly_brackets > 0)
					{
						Console.Write("  * ERROR!(L{0:D}) Found more '{{' than '}}' before 'ends'.\n",line_number);
						GlobalMembersGLOBAL.error++;
					}
					if(num_squigilly_brackets < 0)
					{
						Console.Write("  * ERROR!(L{0:D}) Found more '}}' than '{{' before 'ends'.\n",line_number);
						GlobalMembersGLOBAL.error++;
					}
					parsing_state = 0;
				}
				return 0;
			case 19:
				GlobalMembersGAMEDEF.getlabel();
				// Check to see it's already defined

				for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
					if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),keyw[i]) == 0)
					{
						GlobalMembersGLOBAL.error++;
						Console.Write("  * ERROR!(L{0:D}) Symbol '{1}' is a key word.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
						return 0;
					}

				for(i = 0;i<GlobalMembersGLOBAL.labelcnt;i++)
				{
					if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),GlobalMembersGLOBAL.label + (i<<6)) == 0)
					{
						GlobalMembersGLOBAL.warning++;
						Console.Write("  * WARNING.(L{0:D}) Duplicate definition '{1}' ignored.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
						break;
					}
				}

				GlobalMembersGAMEDEF.transnum();
				if(i == GlobalMembersGLOBAL.labelcnt)
					GlobalMembersGLOBAL.labelcode[GlobalMembersGLOBAL.labelcnt++] = *(GlobalMembersGLOBAL.scriptptr-1);
				GlobalMembersGLOBAL.scriptptr -= 2;
				return 0;
			case 14:

				for(j = 0;j < 4;j++)
				{
					if(GlobalMembersGAMEDEF.keyword() == -1)
						GlobalMembersGAMEDEF.transnum();
					else
						break;
				}

				while(j < 4)
				{
					GlobalMembersGLOBAL.scriptptr = 0;
					GlobalMembersGLOBAL.scriptptr++;
					j++;
				}
				return 0;

			case 32:
				if(GlobalMembersGLOBAL.parsing_actor != 0 || parsing_state)
				{
					GlobalMembersGAMEDEF.transnum();

					j = 0;
					while(GlobalMembersGAMEDEF.keyword() == -1)
					{
						GlobalMembersGAMEDEF.transnum();
						GlobalMembersGLOBAL.scriptptr--;
						j |= GlobalMembersGLOBAL.scriptptr;
					}
					GlobalMembersGLOBAL.scriptptr = j;
					GlobalMembersGLOBAL.scriptptr++;
				}
				else
				{
					GlobalMembersGLOBAL.scriptptr--;
					GlobalMembersGAMEDEF.getlabel();
					// Check to see it's already defined

					for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
						if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),keyw[i]) == 0)
					{
						GlobalMembersGLOBAL.error++;
						Console.Write("  * ERROR!(L{0:D}) Symbol '{1}' is a key word.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
						return 0;
					}

					for(i = 0;i<GlobalMembersGLOBAL.labelcnt;i++)
						if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),GlobalMembersGLOBAL.label + (i<<6)) == 0)
						{
							GlobalMembersGLOBAL.warning++;
							Console.Write("  * WARNING.(L{0:D}) Duplicate move '{1}' ignored.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
							break;
						}
					if(i == GlobalMembersGLOBAL.labelcnt)
						GlobalMembersGLOBAL.labelcode[GlobalMembersGLOBAL.labelcnt++] = (int) GlobalMembersGLOBAL.scriptptr;
					for(j = 0;j<2;j++)
					{
						if(GlobalMembersGAMEDEF.keyword() >= 0)
							break;
						GlobalMembersGAMEDEF.transnum();
					}
					for(k = j;k<2;k++)
					{
						GlobalMembersGLOBAL.scriptptr = 0;
						GlobalMembersGLOBAL.scriptptr++;
					}
				}
				return 0;

			case 54:
				{
					GlobalMembersGLOBAL.scriptptr--;
					GlobalMembersGAMEDEF.transnum(); // Volume Number (0/4)
					GlobalMembersGLOBAL.scriptptr--;

					k = GlobalMembersGLOBAL.scriptptr-1;

					if(k >= 0) // if it's background music
					{
						i = 0;
						while(GlobalMembersGAMEDEF.keyword() == -1)
						{
							while(GlobalMembersGAMEDEF.isaltok(GlobalMembersGLOBAL.textptr) == 0)
							{
								if(GlobalMembersGLOBAL.textptr == 0x0a)
									line_number++;
								GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
								if(GlobalMembersGLOBAL.textptr == 0)
									break;
							}
							j = 0;
							while (GlobalMembersGAMEDEF.isaltok(*GlobalMembersGLOBAL.textptr.Substring(j)) != 0)
							{
								GlobalMembersGLOBAL.music_fn[k, i, j] = GlobalMembersGLOBAL.textptr[j];
								j++;
							}
							GlobalMembersGLOBAL.music_fn[k, i, j] = (sbyte)'\0';
							GlobalMembersGLOBAL.textptr += j;
							if(i > 9)
								break;
							i++;
						}
					}
					else
					{
						i = 0;
						while(GlobalMembersGAMEDEF.keyword() == -1)
						{
							while(GlobalMembersGAMEDEF.isaltok(GlobalMembersGLOBAL.textptr) == 0)
							{
								if(GlobalMembersGLOBAL.textptr == 0x0a)
									line_number++;
								GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
								if(GlobalMembersGLOBAL.textptr == 0)
									break;
							}
							j = 0;
							while (GlobalMembersGAMEDEF.isaltok(*GlobalMembersGLOBAL.textptr.Substring(j)) != 0)
							{
								GlobalMembersGLOBAL.env_music_fn[i, j] = GlobalMembersGLOBAL.textptr[j];
								j++;
							}
							GlobalMembersGLOBAL.env_music_fn[i, j] = (sbyte)'\0';

							GlobalMembersGLOBAL.textptr += j;
							if(i > 9)
								break;
							i++;
						}
					}
				}
				return 0;
			case 55:
				GlobalMembersGLOBAL.scriptptr--;
				while(GlobalMembersGAMEDEF.isaltok(GlobalMembersGLOBAL.textptr) == 0)
				{
					if(GlobalMembersGLOBAL.textptr == 0x0a)
						line_number++;
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
					if(GlobalMembersGLOBAL.textptr == 0)
						break;
				}
				j = 0;
				while (GlobalMembersGAMEDEF.isaltok(GlobalMembersGLOBAL.textptr) != 0)
				{
					GlobalMembersGLOBAL.tempbuf[j] = *(GlobalMembersGLOBAL.textptr ++);
					j++;
				}
				GlobalMembersGLOBAL.tempbuf[j] = (byte)'\0';

				fp = kopen4load(GlobalMembersGLOBAL.tempbuf,GlobalMembersGLOBAL.loadfromgrouponly);
				if(fp == 0)
				{
					GlobalMembersGLOBAL.error++;
					Console.Write("  * ERROR!(L{0:D}) Could not find '{1}'.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
					return 0;
				}

				j = kfilelength(fp);

				Console.Write("Including: '{0}'.\n",GlobalMembersGLOBAL.tempbuf);

				temp_line_number = line_number;
				line_number = 1;
				temp_ifelse_check = checking_ifelse;
				checking_ifelse = 0;
				origtptr = GlobalMembersGLOBAL.textptr;
				GlobalMembersGLOBAL.textptr = last_used_text+last_used_size;

				*GlobalMembersGLOBAL.textptr.Substring(j) = 0;

				kread(fp,(string)GlobalMembersGLOBAL.textptr,j);
				kclose(fp);

				do
					done = GlobalMembersGAMEDEF.parsecommand();
				while(done == 0);

				GlobalMembersGLOBAL.textptr = origtptr;
				total_lines += line_number;
				line_number = temp_line_number;
				checking_ifelse = temp_ifelse_check;

				return 0;
			case 24:
				if(GlobalMembersGLOBAL.parsing_actor != 0 || parsing_state)
					GlobalMembersGAMEDEF.transnum();
				else
				{
					GlobalMembersGLOBAL.scriptptr--;
					GlobalMembersGAMEDEF.getlabel();

					for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
						if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),keyw[i]) == 0)
						{
							GlobalMembersGLOBAL.error++;
							Console.Write("  * ERROR!(L{0:D}) Symbol '{1}' is a key word.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
							return 0;
						}

					for(i = 0;i<GlobalMembersGLOBAL.labelcnt;i++)
						if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),GlobalMembersGLOBAL.label + (i<<6)) == 0)
						{
							GlobalMembersGLOBAL.warning++;
							Console.Write("  * WARNING.(L{0:D}) Duplicate ai '{1}' ignored.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
							break;
						}

					if(i == GlobalMembersGLOBAL.labelcnt)
						GlobalMembersGLOBAL.labelcode[GlobalMembersGLOBAL.labelcnt++] = (int) GlobalMembersGLOBAL.scriptptr;

					for(j = 0;j<3;j++)
					{
						if(GlobalMembersGAMEDEF.keyword() >= 0)
							break;
						if(j == 2)
						{
							k = 0;
							while(GlobalMembersGAMEDEF.keyword() == -1)
							{
								GlobalMembersGAMEDEF.transnum();
								GlobalMembersGLOBAL.scriptptr--;
								k |= GlobalMembersGLOBAL.scriptptr;
							}
							GlobalMembersGLOBAL.scriptptr = k;
							GlobalMembersGLOBAL.scriptptr++;
							return 0;
						}
						else
							GlobalMembersGAMEDEF.transnum();
					}
					for(k = j;k<3;k++)
					{
						GlobalMembersGLOBAL.scriptptr = 0;
						GlobalMembersGLOBAL.scriptptr++;
					}
				}
				return 0;

			case 7:
				if(GlobalMembersGLOBAL.parsing_actor != 0 || parsing_state)
					GlobalMembersGAMEDEF.transnum();
				else
				{
					GlobalMembersGLOBAL.scriptptr--;
					GlobalMembersGAMEDEF.getlabel();
					// Check to see it's already defined

					for(i = 0;i<DefineConstants.NUMKEYWORDS;i++)
						if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),keyw[i]) == 0)
						{
							GlobalMembersGLOBAL.error++;
							Console.Write("  * ERROR!(L{0:D}) Symbol '{1}' is a key word.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
							return 0;
						}

					for(i = 0;i<GlobalMembersGLOBAL.labelcnt;i++)
						if(string.Compare(GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6),GlobalMembersGLOBAL.label + (i<<6)) == 0)
						{
							GlobalMembersGLOBAL.warning++;
							Console.Write("  * WARNING.(L{0:D}) Duplicate action '{1}' ignored.\n",line_number,GlobalMembersGLOBAL.label + (GlobalMembersGLOBAL.labelcnt<<6));
							break;
						}

					if(i == GlobalMembersGLOBAL.labelcnt)
						GlobalMembersGLOBAL.labelcode[GlobalMembersGLOBAL.labelcnt++] = (int) GlobalMembersGLOBAL.scriptptr;

					for(j = 0;j<5;j++)
					{
						if(GlobalMembersGAMEDEF.keyword() >= 0)
							break;
						GlobalMembersGAMEDEF.transnum();
					}
					for(k = j;k<5;k++)
					{
						GlobalMembersGLOBAL.scriptptr = 0;
						GlobalMembersGLOBAL.scriptptr++;
					}
				}
				return 0;

			case 1:
				if(parsing_state)
				{
					Console.Write("  * ERROR!(L{0:D}) Found 'actor' within 'state'.\n",line_number);
					GlobalMembersGLOBAL.error++;
				}

				if (GlobalMembersGLOBAL.parsing_actor != 0)
				{
					Console.Write("  * ERROR!(L{0:D}) Found 'actor' within 'actor'.\n",line_number);
					GlobalMembersGLOBAL.error++;
				}

				num_squigilly_brackets = 0;
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGLOBAL.parsing_actor = GlobalMembersGLOBAL.scriptptr;

				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGLOBAL.actorscrptr[GlobalMembersGLOBAL.scriptptr] = GlobalMembersGLOBAL.parsing_actor;

				for(j = 0;j<4;j++)
				{
					*(GlobalMembersGLOBAL.parsing_actor+j) = 0;
					if(j == 3)
					{
						j = 0;
						while(GlobalMembersGAMEDEF.keyword() == -1)
						{
							GlobalMembersGAMEDEF.transnum();
							GlobalMembersGLOBAL.scriptptr--;
							j |= GlobalMembersGLOBAL.scriptptr;
						}
						GlobalMembersGLOBAL.scriptptr = j;
						GlobalMembersGLOBAL.scriptptr++;
						break;
					}
					else
					{
						if(GlobalMembersGAMEDEF.keyword() >= 0)
						{
							GlobalMembersGLOBAL.scriptptr += (4-j);
							break;
						}
						GlobalMembersGAMEDEF.transnum();

						*(GlobalMembersGLOBAL.parsing_actor+j) = *(GlobalMembersGLOBAL.scriptptr-1);
					}
				}

				checking_ifelse = 0;

				return 0;

			case 98:

				if(parsing_state)
				{
					Console.Write("  * ERROR!(L{0:D}) Found 'useritem' within 'state'.\n",line_number);
					GlobalMembersGLOBAL.error++;
				}

				if (GlobalMembersGLOBAL.parsing_actor != 0)
				{
					Console.Write("  * ERROR!(L{0:D}) Found 'useritem' within 'actor'.\n",line_number);
					GlobalMembersGLOBAL.error++;
				}

				num_squigilly_brackets = 0;
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGLOBAL.parsing_actor = GlobalMembersGLOBAL.scriptptr;

				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.scriptptr--;
				j = GlobalMembersGLOBAL.scriptptr;

				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGLOBAL.actorscrptr[GlobalMembersGLOBAL.scriptptr] = GlobalMembersGLOBAL.parsing_actor;
				GlobalMembersGLOBAL.actortype = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.actortype, GlobalMembersGLOBAL.scriptptr, j);

				for(j = 0;j<4;j++)
				{
					*(GlobalMembersGLOBAL.parsing_actor+j) = 0;
					if(j == 3)
					{
						j = 0;
						while(GlobalMembersGAMEDEF.keyword() == -1)
						{
							GlobalMembersGAMEDEF.transnum();
							GlobalMembersGLOBAL.scriptptr--;
							j |= GlobalMembersGLOBAL.scriptptr;
						}
						GlobalMembersGLOBAL.scriptptr = j;
						GlobalMembersGLOBAL.scriptptr++;
						break;
					}
					else
					{
						if(GlobalMembersGAMEDEF.keyword() >= 0)
						{
							GlobalMembersGLOBAL.scriptptr += (4-j);
							break;
						}
						GlobalMembersGAMEDEF.transnum();

						*(GlobalMembersGLOBAL.parsing_actor+j) = *(GlobalMembersGLOBAL.scriptptr-1);
					}
				}

				checking_ifelse = 0;

				return 0;



			case 11:
			case 13:
			case 25:
			case 31:
			case 40:
			case 52:
			case 69:
			case 74:
			case 77:
			case 80:
			case 86:
			case 88:
			case 68:
			case 100:
			case 101:
			case 102:
			case 103:
			case 105:
			case 110:
				GlobalMembersGAMEDEF.transnum();
				return 0;

			case 2:
			case 23:
			case 28:
			case 99:
			case 37:
			case 48:
			case 58:
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGAMEDEF.transnum();
				break;
			case 50:
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGAMEDEF.transnum();
				break;
			case 10:
				if(checking_ifelse)
				{
					checking_ifelse--;
					tempscrptr = GlobalMembersGLOBAL.scriptptr;
					GlobalMembersGLOBAL.scriptptr++; //Leave a spot for the fail location
					GlobalMembersGAMEDEF.parsecommand();
					tempscrptr = (int) GlobalMembersGLOBAL.scriptptr;
				}
				else
				{
					GlobalMembersGLOBAL.scriptptr--;
					GlobalMembersGLOBAL.error++;
					Console.Write("  * ERROR!(L{0:D}) Found 'else' with no 'if'.\n",line_number);
				}

				return 0;

			case 75:
				GlobalMembersGAMEDEF.transnum();
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case 3:
			case 8:
			case 9:
			case 21:
			case 33:
			case 34:
			case 35:
			case 41:
			case 46:
			case 53:
			case 56:
			case 59:
			case 62:
			case 72:
			case 73:
	//        case 74:
			case 78:
			case 85:
			case 94:
			case 111:
				GlobalMembersGAMEDEF.transnum();
//C++ TO C# CONVERTER TODO TASK: C# does not allow fall-through from a non-empty 'case':
			case 43:
			case 44:
			case 49:
			case 5:
			case 6:
			case 27:
			case 26:
			case 45:
			case 51:
			case 63:
			case 64:
			case 65:
			case 67:
			case 70:
			case 71:
			case 81:
			case 82:
			case 90:
			case 91:
			case 109:

				if(tw == 51)
				{
					j = 0;
					do
					{
						GlobalMembersGAMEDEF.transnum();
						GlobalMembersGLOBAL.scriptptr--;
						j |= GlobalMembersGLOBAL.scriptptr;
					}
					while(GlobalMembersGAMEDEF.keyword() == -1);
					GlobalMembersGLOBAL.scriptptr = j;
					GlobalMembersGLOBAL.scriptptr++;
				}

				tempscrptr = GlobalMembersGLOBAL.scriptptr;
				GlobalMembersGLOBAL.scriptptr++; //Leave a spot for the fail location

				do
				{
					j = GlobalMembersGAMEDEF.keyword();
					if(j == 20 || j == 39)
						GlobalMembersGAMEDEF.parsecommand();
				} while(j == 20 || j == 39);

				GlobalMembersGAMEDEF.parsecommand();

				tempscrptr = (int) GlobalMembersGLOBAL.scriptptr;

				checking_ifelse++;
				return 0;
			case 29:
				num_squigilly_brackets++;
				do
					done = GlobalMembersGAMEDEF.parsecommand();
				while(done == 0);
				return 0;
			case 30:
				num_squigilly_brackets--;
				if(num_squigilly_brackets < 0)
				{
					Console.Write("  * ERROR!(L{0:D}) Found more '}}' than '{{'.\n",line_number);
					GlobalMembersGLOBAL.error++;
				}
				return 1;
			case 76:
				GlobalMembersGLOBAL.scriptptr--;
				j = 0;
				while(GlobalMembersGLOBAL.textptr != 0x0a)
				{
					GlobalMembersGLOBAL.betaname = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.betaname, j, GlobalMembersGLOBAL.textptr);
					j++;
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
				}
				GlobalMembersGLOBAL.betaname = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.betaname, j, 0);
				return 0;
			case 20:
				GlobalMembersGLOBAL.scriptptr--; //Negate the rem
				while(GlobalMembersGLOBAL.textptr != 0x0a)
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				// line_number++;
				return 0;

			case 107:
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.scriptptr--;
				j = GlobalMembersGLOBAL.scriptptr;
				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				i = 0;

				while(GlobalMembersGLOBAL.textptr != 0x0a)
				{
					GlobalMembersGLOBAL.volume_names[j, i] = char.ToUpper(GlobalMembersGLOBAL.textptr);
					GlobalMembersGLOBAL.textptr ++,i++;
					if(i >= 32)
					{
						Console.Write("  * ERROR!(L{0:D}) Volume name exceeds character size limit of 32.\n",line_number);
						GlobalMembersGLOBAL.error++;
						while(GlobalMembersGLOBAL.textptr != 0x0a)
							GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
						break;
					}
				}
				GlobalMembersGLOBAL.volume_names[j, i-1] = (sbyte)'\0';
				return 0;
			case 108:
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.scriptptr--;
				j = GlobalMembersGLOBAL.scriptptr;
				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				i = 0;

				while(GlobalMembersGLOBAL.textptr != 0x0a)
				{
					GlobalMembersGLOBAL.skill_names[j, i] = char.ToUpper(GlobalMembersGLOBAL.textptr);
					GlobalMembersGLOBAL.textptr ++,i++;
					if(i >= 32)
					{
						Console.Write("  * ERROR!(L{0:D}) Skill name exceeds character size limit of 32.\n",line_number);
						GlobalMembersGLOBAL.error++;
						while(GlobalMembersGLOBAL.textptr != 0x0a)
							GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
						break;
					}
				}
				GlobalMembersGLOBAL.skill_names[j, i-1] = (sbyte)'\0';
				return 0;

			case 0:
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.scriptptr--;
				j = GlobalMembersGLOBAL.scriptptr;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.scriptptr--;
				k = GlobalMembersGLOBAL.scriptptr;
				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				i = 0;
				while(GlobalMembersGLOBAL.textptr != ' ' && GlobalMembersGLOBAL.textptr != 0x0a)
				{
					GlobalMembersGLOBAL.level_file_names[j *11+k, i] = GlobalMembersGLOBAL.textptr;
					GlobalMembersGLOBAL.textptr ++,i++;
					if(i > 127)
					{
						Console.Write("  * ERROR!(L{0:D}) Level file name exceeds character size limit of 128.\n",line_number);
						GlobalMembersGLOBAL.error++;
						while(GlobalMembersGLOBAL.textptr != ' ')
							GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
						break;
					}
				}
				GlobalMembersGLOBAL.level_names[j *11+k, i-1] = (sbyte)'\0';

				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				GlobalMembersGLOBAL.partime[j *11+k] = (((*GlobalMembersGLOBAL.textptr.Substring(0)-'0')*10+(*GlobalMembersGLOBAL.textptr.Substring(1)-'0'))*26 *60)+ (((*GlobalMembersGLOBAL.textptr.Substring(3)-'0')*10+(*GlobalMembersGLOBAL.textptr.Substring(4)-'0'))*26);

				GlobalMembersGLOBAL.textptr += 5;
				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				GlobalMembersGLOBAL.designertime[j *11+k] = (((*GlobalMembersGLOBAL.textptr.Substring(0)-'0')*10+(*GlobalMembersGLOBAL.textptr.Substring(1)-'0'))*26 *60)+ (((*GlobalMembersGLOBAL.textptr.Substring(3)-'0')*10+(*GlobalMembersGLOBAL.textptr.Substring(4)-'0'))*26);

				GlobalMembersGLOBAL.textptr += 5;
				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				i = 0;

				while(GlobalMembersGLOBAL.textptr != 0x0a)
				{
					GlobalMembersGLOBAL.level_names[j *11+k, i] = char.ToUpper(GlobalMembersGLOBAL.textptr);
					GlobalMembersGLOBAL.textptr ++,i++;
					if(i >= 32)
					{
						Console.Write("  * ERROR!(L{0:D}) Level name exceeds character size limit of 32.\n",line_number);
						GlobalMembersGLOBAL.error++;
						while(GlobalMembersGLOBAL.textptr != 0x0a)
							GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
						break;
					}
				}
				GlobalMembersGLOBAL.level_names[j *11+k, i-1] = (sbyte)'\0';
				return 0;

			case 79:
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				k = *(GlobalMembersGLOBAL.scriptptr-1);
				if(k >= NUMOFFIRSTTIMEACTIVE)
				{
					Console.Write("  * ERROR!(L{0:D}) Quote amount exceeds limit of {1:D} characters.\n",line_number,NUMOFFIRSTTIMEACTIVE);
					GlobalMembersGLOBAL.error++;
				}
				GlobalMembersGLOBAL.scriptptr--;
				i = 0;
				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				while(GlobalMembersGLOBAL.textptr != 0x0a)
				{
					GlobalMembersGLOBAL.fta_quotes[k, i] = GlobalMembersGLOBAL.textptr;
					GlobalMembersGLOBAL.textptr ++,i++;
					if(i >= 64)
					{
						Console.Write("  * ERROR!(L{0:D}) Quote exceeds character size limit of 64.\n",line_number);
						GlobalMembersGLOBAL.error++;
						while(GlobalMembersGLOBAL.textptr != 0x0a)
							GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
						break;
					}
				}
				GlobalMembersGLOBAL.fta_quotes[k, i] = (sbyte)'\0';
				return 0;
			case 57:
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				k = *(GlobalMembersGLOBAL.scriptptr-1);
				if(k >= NUM_SOUNDS)
				{
					Console.Write("  * ERROR!(L{0:D}) Exceeded sound limit of {1:D}.\n",line_number,NUM_SOUNDS);
					GlobalMembersGLOBAL.error++;
				}
				GlobalMembersGLOBAL.scriptptr--;
				i = 0;
				while(GlobalMembersGLOBAL.textptr == ' ')
					GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);

				while(GlobalMembersGLOBAL.textptr != ' ')
				{
					GlobalMembersGLOBAL.sounds[k, i] = GlobalMembersGLOBAL.textptr;
					GlobalMembersGLOBAL.textptr ++,i++;
					if(i >= 13)
					{
						Console.WriteLine(GlobalMembersGLOBAL.sounds[k]);
						Console.Write("  * ERROR!(L{0:D}) Sound filename exceeded limit of 13 characters.\n",line_number);
						GlobalMembersGLOBAL.error++;
						while(GlobalMembersGLOBAL.textptr != ' ')
							GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(1);
						break;
					}
				}
				GlobalMembersGLOBAL.sounds[k, i] = (sbyte)'\0';

				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.soundps[k] = *(GlobalMembersGLOBAL.scriptptr-1);
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.soundpe[k] = *(GlobalMembersGLOBAL.scriptptr-1);
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.soundpr = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.soundpr, k, *(GlobalMembersGLOBAL.scriptptr-1));
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.soundm = StringFunctions.ChangeCharacter(GlobalMembersGLOBAL.soundm, k, *(GlobalMembersGLOBAL.scriptptr-1));
				GlobalMembersGLOBAL.scriptptr--;
				GlobalMembersGAMEDEF.transnum();
				GlobalMembersGLOBAL.soundvo[k] = *(GlobalMembersGLOBAL.scriptptr-1);
				GlobalMembersGLOBAL.scriptptr--;
				return 0;

			case 4:
				if(GlobalMembersGLOBAL.parsing_actor == 0)
				{
					Console.Write("  * ERROR!(L{0:D}) Found 'enda' without defining 'actor'.\n",line_number);
					GlobalMembersGLOBAL.error++;
				}
	//            else
				{
					if(num_squigilly_brackets > 0)
					{
						Console.Write("  * ERROR!(L{0:D}) Found more '{{' than '}}' before 'enda'.\n",line_number);
						GlobalMembersGLOBAL.error++;
					}
					GlobalMembersGLOBAL.parsing_actor = 0;
				}

				return 0;
			case 12:
			case 16:
			case 84:
	//        case 21:
			case 22: //KILLIT
			case 36:
			case 38:
			case 42:
			case 47:
			case 61:
			case 66:
			case 83:
			case 95:
			case 96:
			case 97:
			case 104:
			case 106:
				return 0;
			case 60:
				j = 0;
				while(j < 30)
				{
					GlobalMembersGAMEDEF.transnum();
					GlobalMembersGLOBAL.scriptptr--;

					switch(j)
					{
						case 0:
							GlobalMembersGLOBAL.ud.const_visibility = GlobalMembersGLOBAL.scriptptr;
							break;
						case 1:
							GlobalMembersGLOBAL.impact_damage = GlobalMembersGLOBAL.scriptptr;
							break;
						case 2:
							GlobalMembersGLOBAL.max_player_health = GlobalMembersGLOBAL.scriptptr;
							break;
						case 3:
							GlobalMembersGLOBAL.max_armour_amount = GlobalMembersGLOBAL.scriptptr;
							break;
						case 4:
							GlobalMembersGLOBAL.respawnactortime = GlobalMembersGLOBAL.scriptptr;
							break;
						case 5:
							GlobalMembersGLOBAL.respawnitemtime = GlobalMembersGLOBAL.scriptptr;
							break;
						case 6:
							GlobalMembersGLOBAL.dukefriction = GlobalMembersGLOBAL.scriptptr;
							break;
						case 7:
							GlobalMembersGLOBAL.gc = GlobalMembersGLOBAL.scriptptr;
							break;
						case 8:
							GlobalMembersGLOBAL.rpgblastradius = GlobalMembersGLOBAL.scriptptr;
							break;
						case 9:
							GlobalMembersGLOBAL.pipebombblastradius = GlobalMembersGLOBAL.scriptptr;
							break;
						case 10:
							GlobalMembersGLOBAL.shrinkerblastradius = GlobalMembersGLOBAL.scriptptr;
							break;
						case 11:
							GlobalMembersGLOBAL.tripbombblastradius = GlobalMembersGLOBAL.scriptptr;
							break;
						case 12:
							GlobalMembersGLOBAL.morterblastradius = GlobalMembersGLOBAL.scriptptr;
							break;
						case 13:
							GlobalMembersGLOBAL.bouncemineblastradius = GlobalMembersGLOBAL.scriptptr;
							break;
						case 14:
							GlobalMembersGLOBAL.seenineblastradius = GlobalMembersGLOBAL.scriptptr;
							break;

						case 15:
						case 16:
						case 17:
						case 18:
						case 19:
						case 20:
						case 21:
						case 22:
						case 23:
						case 24:
							if(j == 24)
								GlobalMembersGLOBAL.max_ammo_amount[11] = GlobalMembersGLOBAL.scriptptr;
							else
								GlobalMembersGLOBAL.max_ammo_amount[j-14] = GlobalMembersGLOBAL.scriptptr;
							break;
						case 25:
							GlobalMembersGLOBAL.camerashitable = GlobalMembersGLOBAL.scriptptr;
							break;
						case 26:
							GlobalMembersGLOBAL.numfreezebounces = GlobalMembersGLOBAL.scriptptr;
							break;
						case 27:
							GlobalMembersGLOBAL.freezerhurtowner = GlobalMembersGLOBAL.scriptptr;
							break;
						case 28:
							GlobalMembersGLOBAL.spriteqamount = GlobalMembersGLOBAL.scriptptr;
							if(GlobalMembersGLOBAL.spriteqamount > 1024)
								GlobalMembersGLOBAL.spriteqamount = 1024;
							else if(GlobalMembersGLOBAL.spriteqamount < 0)
								GlobalMembersGLOBAL.spriteqamount = 0;
							break;
						case 29:
							GlobalMembersGLOBAL.lasermode = GlobalMembersGLOBAL.scriptptr;
							break;
					}
					j++;
				}
				GlobalMembersGLOBAL.scriptptr++;
				return 0;
		}
		return 0;
	}


	public static void passone()
	{

		while(GlobalMembersGAMEDEF.parsecommand() == 0);

		if((GlobalMembersGLOBAL.error+GlobalMembersGLOBAL.warning) > 12)
			Console.WriteLine("  * ERROR! Too many warnings or errors.");

	}

	public static string[] defaultcons = { {"GAME.CON"}, {"USER.CON"}, {"DEFS.CON"} };

	public static void copydefaultcons()
	{
		int i;
		int fs;
		int fpi;
		FILE fpo;

		for(i = 0;i<3;i++)
		{
			fpi = kopen4load(defaultcons[i], 1);
			fpo = fopen(defaultcons[i],"wb");

			if(fpi == 0)
			{
	// CTW - MODIFICATION
	//          if(fpo == -1) fclose(fpo);
				if(fpo == null)
					fclose(fpo);
	// CTW END - MODIFICATION
				continue;
			}
	// CTW - MODIFICATION
	//      if(fpo == -1)
			if(fpo == null)
	// CTW END - MODIFICATION
			{
				if(fpi == 0)
					kclose(fpi);
				continue;
			}

			fs = kfilelength(fpi);

			kread(fpi,GlobalMembersGLOBAL.hittype[0],fs);
			fwrite(GlobalMembersGLOBAL.hittype[0],fs,1,fpo);

			kclose(fpi);
			fclose(fpo);
		}
	}

	public static void loadefs(ref string filenam, ref string mptr)
	{
		int i;
		int fs;
		int fp;

		if(!SafeFileExists(filenam) && GlobalMembersGLOBAL.loadfromgrouponly == 0)
		{
			Console.WriteLine("Missing external con file(s).");
			Console.WriteLine("COPY INTERNAL DEFAULTS TO DIRECTORY(Y/n)?");

			KB_FlushKeyboardQueue();
			while(KB_KeyWaiting());

			i = KB_Getch();
			if(i == 'y' || i == 'Y')
			{
				Console.WriteLine(" Yes");
				GlobalMembersGAMEDEF.copydefaultcons();
			}
		}

		fp = kopen4load(filenam,GlobalMembersGLOBAL.loadfromgrouponly);
		if(fp == 0)
		{
			if(GlobalMembersGLOBAL.loadfromgrouponly == 1)
				GlobalMembersGAME.gameexit("\nMissing con file(s).");

			GlobalMembersGLOBAL.loadfromgrouponly = 1;
			return; //Not there
		}
		else
		{
			Console.Write("Compiling: '{0}'.\n",filenam);

			fs = kfilelength(fp);

			last_used_text = GlobalMembersGLOBAL.textptr = (string) mptr;
			last_used_size = fs;

			kread(fp,(string)GlobalMembersGLOBAL.textptr,fs);
			kclose(fp);
		}

		GlobalMembersGLOBAL.textptr = GlobalMembersGLOBAL.textptr.Substring(0, fs - 2);

		clearbuf(GlobalMembersGLOBAL.actorscrptr,MAXSPRITES,0);
		clearbufbyte(GlobalMembersGLOBAL.actortype,MAXSPRITES,0);

		GlobalMembersGLOBAL.labelcnt = 0;
		GlobalMembersGLOBAL.scriptptr = GlobalMembersGLOBAL.script+1;
		GlobalMembersGLOBAL.warning = 0;
		GlobalMembersGLOBAL.error = 0;
		line_number = 1;
		total_lines = 0;

		GlobalMembersGAMEDEF.passone(); //Tokenize
		*GlobalMembersGLOBAL.script = (int) GlobalMembersGLOBAL.scriptptr;

		if(GlobalMembersGLOBAL.warning|GlobalMembersGLOBAL.error)
			Console.Write("Found {0:D} warning(s), {1:D} error(s).\n",GlobalMembersGLOBAL.warning,GlobalMembersGLOBAL.error);

		if(GlobalMembersGLOBAL.error == 0 && GlobalMembersGLOBAL.warning != 0)
		{
			if(GlobalMembersGLOBAL.groupfile != -1 && GlobalMembersGLOBAL.loadfromgrouponly == 0)
			{
				Console.Write("\nWarnings found in {0} file.  You should backup the original copies before\n",filenam);
				Console.WriteLine("before attempting to modify them.  Do you want to use the");
				Console.WriteLine("INTERNAL DEFAULTS (y/N)?");

				KB_FlushKeyboardQueue();
				while(KB_KeyWaiting());
				i = KB_Getch();
				if(i == 'y' || i == 'Y')
				{
					GlobalMembersGLOBAL.loadfromgrouponly = 1;
					Console.WriteLine(" Yes");
					return;
				}
			}
		}

		if (GlobalMembersGLOBAL.error != 0)
		{
			if (GlobalMembersGLOBAL.loadfromgrouponly != 0)
			{
				GlobalMembersGLOBAL.buf = string.Format("\nError in {0}.", filenam);
				GlobalMembersGAME.gameexit(ref GlobalMembersGLOBAL.buf);
			}
			else
			{
				if(GlobalMembersGLOBAL.groupfile != -1 && GlobalMembersGLOBAL.loadfromgrouponly == 0)
				{
					Console.Write("\nErrors found in {0} file.  You should backup the original copies\n",filenam);
					Console.WriteLine("before attempting to modify them.  Do you want to use the");
					Console.WriteLine("internal defaults (Y/N)?");

					KB_FlushKeyboardQueue();
					while(!KB_KeyWaiting());

					i = KB_Getch();
					if(i == 'y' || i == 'Y')
					{
						Console.WriteLine(" Yes");
						GlobalMembersGLOBAL.loadfromgrouponly = 1;
						return;
					}
					else
						GlobalMembersGAME.gameexit("");
				}
			}
		}
		else
		{
			total_lines += line_number;
			Console.Write("Code Size:{0:D} bytes({1:D} labels).\n",(int)((GlobalMembersGLOBAL.scriptptr-GlobalMembersGLOBAL.script)<<2)-4,GlobalMembersGLOBAL.labelcnt);
		}
	}

	public static sbyte dodge(ref spritetype s)
	{
		short i;
		int bx;
		int by;
		int mx;
		int my;
		int bxvect;
		int byvect;
		int mxvect;
		int myvect;
		int d;

		mx = s.x;
		my = s.y;
		mxvect = sintable[(s.ang+512)&2047];
		myvect = sintable[s.ang &2047];

		for(i = headspritestat[4];i>=0;i = nextspritestat[i]) //weapons list
		{
			if(OW == i || SECT != s.sectnum)
				continue;

			bx = SX-mx;
			by = SY-my;
			bxvect = sintable[(SA+512)&2047];
			byvect = sintable[SA &2047];

			if (mxvect *bx + myvect *by >= 0)
				if (bxvect *bx + byvect *by < 0)
			{
				d = bxvect *by - byvect *bx;
				if (klabs(d) < 65536 *64)
				{
					s.ang -= 512+(TRAND &1024);
					return 1;
				}
			}
		}
		return 0;
	}

	public static short furthestangle(short i,short angs)
	{
		short j;
		short hitsect;
		short hitwall;
		short hitspr;
		short furthest_angle;
		short angincs;
		int hx;
		int hy;
		int hz;
		int d;
		int greatestd;
		spritetype s = sprite[i];

		greatestd = -(1<<30);
		angincs = 2048/angs;

		if(s.picnum != APLAYER)
			if((g_t[0]&63) > 2)
				return(s.ang + 1024);

		for(j = s.ang;j<(2048+s.ang);j+=angincs)
		{
			hitscan(s.x, s.y, s.z-(8<<8), s.sectnum, sintable[(j+512)&2047], sintable[j &2047], 0, hitsect, hitwall, hitspr, hx, hy, hz, CLIPMASK1);

			d = klabs(hx-s.x) + klabs(hy-s.y);

			if(d > greatestd)
			{
				greatestd = d;
				furthest_angle = j;
			}
		}
		return (furthest_angle &2047);
	}

	public static short furthestcanseepoint(short i, ref spritetype ts, ref int dax, ref int day)
	{
		short j;
		short hitsect;
		short hitwall;
		short hitspr;
		short angincs;
		short tempang;
		int hx; //, d, cd, ca,tempx,tempy,cx,cy;
		int hy;
		int hz;
		int d;
		int da;
		spritetype s = sprite[i];

		if((g_t[0]&63))
			return -1;

		if(GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.player_skill < 3)
			angincs = 2048/2;
		else
			angincs = 2048/(1+(TRAND &1));

		for(j = ts.ang;j<(2048+ts.ang);j+=(angincs-(TRAND &511)))
		{
			hitscan(ts.x, ts.y, ts.z-(16<<8), ts.sectnum, sintable[(j+512)&2047], sintable[j &2047], 16384-(TRAND &32767), hitsect, hitwall, hitspr, hx, hy, hz, CLIPMASK1);

			d = klabs(hx-ts.x)+klabs(hy-ts.y);
			da = klabs(hx-s.x)+klabs(hy-s.y);

			if(d < da)
				if(cansee(hx,hy,hz,hitsect,s.x,s.y,s.z-(16<<8),s.sectnum))
			{
				dax = hx;
				day = hy;
				return hitsect;
			}
		}
		return -1;
	}




	public static void alterang(short a)
	{
		short aang;
		short angdif;
		short goalang;
		short j;
		int ticselapsed;
		int moveptr;

		moveptr = (int)g_t[1];

		ticselapsed = (g_t[0])&31;

		aang = g_sp.ang;

		g_sp.xvel += (moveptr-g_sp.xvel)/5;
		if(g_sp.zvel < 648)
			g_sp.zvel += ((*(moveptr+1)<<4)-g_sp.zvel)/5;

		if(a &seekplayer)
		{
			j = GlobalMembersGLOBAL.ps[g_p].holoduke_on;

			if(j >= 0 && cansee(sprite[j].x,sprite[j].y,sprite[j].z,sprite[j].sectnum,g_sp.x,g_sp.y,g_sp.z,g_sp.sectnum))
				g_sp.owner = j;
			else
				g_sp.owner = GlobalMembersGLOBAL.ps[g_p].i;

			if(sprite[g_sp.owner].picnum == APLAYER)
				goalang = getangle(GlobalMembersGLOBAL.hittype[g_i].lastvx-g_sp.x,GlobalMembersGLOBAL.hittype[g_i].lastvy-g_sp.y);
			else
				goalang = getangle(sprite[g_sp.owner].x-g_sp.x,sprite[g_sp.owner].y-g_sp.y);

			if(g_sp.xvel && g_sp.picnum != DRONE)
			{
				angdif = GlobalMembersGAMEDEF.getincangle(aang, goalang);

				if(ticselapsed < 2)
				{
					if(klabs(angdif) < 256)
					{
						j = 128-(TRAND &256);
						g_sp.ang += j;
						if(GlobalMembersPLAYER.hits(g_i) < 844)
							g_sp.ang -= j;
					}
				}
				else if(ticselapsed > 18 && ticselapsed < 26) // choose
				{
					if(klabs(angdif>>2) < 128)
						g_sp.ang = goalang;
					else
						g_sp.ang += angdif>>2;
				}
			}
			else
				g_sp.ang = goalang;
		}

		if(ticselapsed < 1)
		{
			j = 2;
			if(a &furthestdir)
			{
				goalang = GlobalMembersGAMEDEF.furthestangle(g_i, j);
				g_sp.ang = goalang;
				g_sp.owner = GlobalMembersGLOBAL.ps[g_p].i;
			}

			if(a &fleeenemy)
			{
				goalang = GlobalMembersGAMEDEF.furthestangle(g_i, j);
				g_sp.ang = goalang; // += angdif; // = getincangle(aang,goalang)>>1;
			}
		}
	}

	public static void move()
	{
		int l;
		int moveptr;
		short j;
		short a;
		short goalang;
		short angdif;
		int daxvel;

		a = g_sp.hitag;

		if(a == -1)
			a = 0;

		g_t[0]++;

		if(a &face_player)
		{
			if(GlobalMembersGLOBAL.ps[g_p].newowner >= 0)
				goalang = getangle(GlobalMembersGLOBAL.ps[g_p].oposx-g_sp.x,GlobalMembersGLOBAL.ps[g_p].oposy-g_sp.y);
			else
				goalang = getangle(GlobalMembersGLOBAL.ps[g_p].posx-g_sp.x,GlobalMembersGLOBAL.ps[g_p].posy-g_sp.y);
			angdif = GlobalMembersGAMEDEF.getincangle(g_sp.ang, goalang)>>2;
			if(angdif > -8 && angdif < 0)
				angdif = 0;
			g_sp.ang += angdif;
		}

		if(a &spin)
			g_sp.ang += sintable[((g_t[0]<<3)&2047)]>>6;

		if(a &face_player_slow)
		{
			if(GlobalMembersGLOBAL.ps[g_p].newowner >= 0)
				goalang = getangle(GlobalMembersGLOBAL.ps[g_p].oposx-g_sp.x,GlobalMembersGLOBAL.ps[g_p].oposy-g_sp.y);
			else
				goalang = getangle(GlobalMembersGLOBAL.ps[g_p].posx-g_sp.x,GlobalMembersGLOBAL.ps[g_p].posy-g_sp.y);
			angdif = ksgn(GlobalMembersGAMEDEF.getincangle(g_sp.ang, goalang))<<5;
			if(angdif > -32 && angdif < 0)
			{
				angdif = 0;
				g_sp.ang = goalang;
			}
			g_sp.ang += angdif;
		}


		if((a &jumptoplayer) == jumptoplayer)
		{
			if(g_t[0] < 16)
				g_sp.zvel -= (sintable[(512+(g_t[0]<<4))&2047]>>5);
		}

		if(a &face_player_smart)
		{
			int newx;
			int newy;

			newx = GlobalMembersGLOBAL.ps[g_p].posx+(GlobalMembersGLOBAL.ps[g_p].posxv/768);
			newy = GlobalMembersGLOBAL.ps[g_p].posy+(GlobalMembersGLOBAL.ps[g_p].posyv/768);
			goalang = getangle(newx-g_sp.x,newy-g_sp.y);
			angdif = GlobalMembersGAMEDEF.getincangle(g_sp.ang, goalang)>>2;
			if(angdif > -8 && angdif < 0)
				angdif = 0;
			g_sp.ang += angdif;
		}

		if(g_t[1] == 0 || a == 0)
		{
			if((GlobalMembersGAME.badguy(ref g_sp) != 0 && g_sp.extra <= 0) || (GlobalMembersGLOBAL.hittype[g_i].bposx != g_sp.x) || (GlobalMembersGLOBAL.hittype[g_i].bposy != g_sp.y))
			{
				GlobalMembersGLOBAL.hittype[g_i].bposx = g_sp.x;
				GlobalMembersGLOBAL.hittype[g_i].bposy = g_sp.y;
				setsprite(g_i,g_sp.x,g_sp.y,g_sp.z);
			}
			return;
		}

		moveptr = (int)g_t[1];

		if(a &geth)
			g_sp.xvel += (moveptr-g_sp.xvel)>>1;
		if(a &getv)
			g_sp.zvel += ((*(moveptr+1)<<4)-g_sp.zvel)>>1;

		if(a &dodgebullet)
			GlobalMembersGAMEDEF.dodge(ref g_sp);

		if(g_sp.picnum != APLAYER)
			GlobalMembersGAMEDEF.alterang(a);

		if(g_sp.xvel > -6 && g_sp.xvel < 6)
			g_sp.xvel = 0;

		a = GlobalMembersGAME.badguy(ref g_sp);

		if(g_sp.xvel || g_sp.zvel)
		{
			if(a != 0 && g_sp.picnum != ROTATEGUN)
			{
				if((g_sp.picnum == DRONE || g_sp.picnum == COMMANDER) && g_sp.extra > 0)
				{
					if(g_sp.picnum == COMMANDER)
					{
						GlobalMembersGLOBAL.hittype[g_i].floorz = l = getflorzofslope(g_sp.sectnum,g_sp.x,g_sp.y);
						if(g_sp.z > (l-(8<<8)))
						{
							if(g_sp.z > (l-(8<<8)))
								g_sp.z = l-(8<<8);
							g_sp.zvel = 0;
						}

						GlobalMembersGLOBAL.hittype[g_i].ceilingz = l = getceilzofslope(g_sp.sectnum,g_sp.x,g_sp.y);
						if((g_sp.z-l) < (80<<8))
						{
							g_sp.z = l+(80<<8);
							g_sp.zvel = 0;
						}
					}
					else
					{
						if(g_sp.zvel > 0)
						{
							GlobalMembersGLOBAL.hittype[g_i].floorz = l = getflorzofslope(g_sp.sectnum,g_sp.x,g_sp.y);
							if(g_sp.z > (l-(30<<8)))
								g_sp.z = l-(30<<8);
						}
						else
						{
							GlobalMembersGLOBAL.hittype[g_i].ceilingz = l = getceilzofslope(g_sp.sectnum,g_sp.x,g_sp.y);
							if((g_sp.z-l) < (50<<8))
							{
								g_sp.z = l+(50<<8);
								g_sp.zvel = 0;
							}
						}
					}
				}
				else if(g_sp.picnum != ORGANTIC)
				{
					if(g_sp.zvel > 0 && GlobalMembersGLOBAL.hittype[g_i].floorz < g_sp.z)
						g_sp.z = GlobalMembersGLOBAL.hittype[g_i].floorz;
					if(g_sp.zvel < 0)
					{
						l = getceilzofslope(g_sp.sectnum,g_sp.x,g_sp.y);
						if((g_sp.z-l) < (66<<8))
						{
							g_sp.z = l+(66<<8);
							g_sp.zvel >>= 1;
						}
					}
				}
			}
			else if(g_sp.picnum == APLAYER)
				if((g_sp.z-GlobalMembersGLOBAL.hittype[g_i].ceilingz) < (32<<8))
					g_sp.z = GlobalMembersGLOBAL.hittype[g_i].ceilingz+(32<<8);

			daxvel = g_sp.xvel;
			angdif = g_sp.ang;

			if(a != 0 && g_sp.picnum != ROTATEGUN)
			{
				if(g_x < 960 && g_sp.xrepeat > 16)
				{

					daxvel = -(1024-g_x);
					angdif = getangle(GlobalMembersGLOBAL.ps[g_p].posx-g_sp.x,GlobalMembersGLOBAL.ps[g_p].posy-g_sp.y);

					if(g_x < 512)
					{
						GlobalMembersGLOBAL.ps[g_p].posxv = 0;
						GlobalMembersGLOBAL.ps[g_p].posyv = 0;
					}
					else
					{
						GlobalMembersGLOBAL.ps[g_p].posxv = mulscale(GlobalMembersGLOBAL.ps[g_p].posxv,GlobalMembersGLOBAL.dukefriction-0x2000,16);
						GlobalMembersGLOBAL.ps[g_p].posyv = mulscale(GlobalMembersGLOBAL.ps[g_p].posyv,GlobalMembersGLOBAL.dukefriction-0x2000,16);
					}
				}
				else if(g_sp.picnum != DRONE && g_sp.picnum != SHARK && g_sp.picnum != COMMANDER)
				{
					if(GlobalMembersGLOBAL.hittype[g_i].bposz != g_sp.z || (GlobalMembersGLOBAL.ud.multimode < 2 && GlobalMembersGLOBAL.ud.player_skill < 2))
					{
						if((g_t[0]&1) || GlobalMembersGLOBAL.ps[g_p].actorsqu == g_i)
							return;
						else
							daxvel <<= 1;
					}
					else
					{
						if((g_t[0]&3) || GlobalMembersGLOBAL.ps[g_p].actorsqu == g_i)
							return;
						else
							daxvel <<= 2;
					}
				}
			}

			GlobalMembersGLOBAL.hittype[g_i].movflag = GlobalMembersACTORS.movesprite(g_i, (daxvel*(sintable[(angdif+512)&2047]))>>14, (daxvel*(sintable[angdif &2047]))>>14, g_sp.zvel, CLIPMASK0);
	   }

	   if (a != 0)
	   {
		   if (sector[g_sp.sectnum].ceilingstat &1)
			   g_sp.shade += (sector[g_sp.sectnum].ceilingshade-g_sp.shade)>>1;
		   else
			   g_sp.shade += (sector[g_sp.sectnum].floorshade-g_sp.shade)>>1;

		   if(sector[g_sp.sectnum].floorpicnum == MIRROR)
			   deletesprite(g_i);
	   }
	}

// long *it = 0x00589a04;


	public static sbyte parse()
	{
		int j;
		int l;
		int s;

		if (GlobalMembersGLOBAL.killit_flag != 0)
			return 1;

	//    if(*it == 1668249134L) gameexit("\nERR");

		switch(GlobalMembersGLOBAL.insptr)
		{
			case 3:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(rnd(GlobalMembersGLOBAL.insptr));
				break;
			case 45:

				if(g_x > 1024)
				{
					short temphit;
					short sclip;
					short angdif;

					if(GlobalMembersGAME.badguy(ref g_sp) != 0 && g_sp.xrepeat > 56)
					{
						sclip = 3084;
						angdif = 48;
					}
					else
					{
						sclip = 768;
						angdif = 16;
					}

					j = GlobalMembersPLAYER.hitasprite(g_i, ref temphit);
					if(j == (1<<30))
					{
						GlobalMembersGAMEDEF.parseifelse(1);
						break;
					}
					if(j > sclip)
					{
						if(temphit >= 0 && sprite[temphit].picnum == g_sp.picnum)
							j = 0;
						else
						{
							g_sp.ang += angdif;
							j = GlobalMembersPLAYER.hitasprite(g_i, ref temphit);
							g_sp.ang -= angdif;
							if(j > sclip)
							{
								if(temphit >= 0 && sprite[temphit].picnum == g_sp.picnum)
									j = 0;
								else
								{
									g_sp.ang -= angdif;
									j = GlobalMembersPLAYER.hitasprite(g_i, ref temphit);
									g_sp.ang += angdif;
									if(j > 768)
									{
										if(temphit >= 0 && sprite[temphit].picnum == g_sp.picnum)
											j = 0;
										else
											j = 1;
									}
									else
										j = 0;
								}
							}
							else
								j = 0;
						}
					}
					else
						j = 0;
				}
				else
					j = 1;

				GlobalMembersGAMEDEF.parseifelse(j);
				break;
			case 91:
				j = cansee(g_sp.x, g_sp.y, g_sp.z-((TRAND &41)<<8), g_sp.sectnum, GlobalMembersGLOBAL.ps[g_p].posx, GlobalMembersGLOBAL.ps[g_p].posy, GlobalMembersGLOBAL.ps[g_p].posz, sprite[GlobalMembersGLOBAL.ps[g_p].i].sectnum); //-((TRAND&41)<<8)
				GlobalMembersGAMEDEF.parseifelse(j);
				if (j != 0)
					GlobalMembersGLOBAL.hittype[g_i].timetosleep = SLEEPTIME;
				break;

			case 49:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.hittype[g_i].actorstayput == -1);
				break;
			case 5:
			{
				spritetype s;
				short sect;

				if(GlobalMembersGLOBAL.ps[g_p].holoduke_on >= 0)
				{
					s = sprite[GlobalMembersGLOBAL.ps[g_p].holoduke_on];
					j = cansee(g_sp.x, g_sp.y, g_sp.z-(TRAND&((32<<8)-1)), g_sp.sectnum, s.x, s.y, s.z, s.sectnum);
					if(j == 0)
						s = sprite[GlobalMembersGLOBAL.ps[g_p].i];
				}
				else
					s = sprite[GlobalMembersGLOBAL.ps[g_p].i];

				j = cansee(g_sp.x, g_sp.y, g_sp.z-(TRAND&((47<<8))), g_sp.sectnum, s.x, s.y, s.z-(24<<8), s.sectnum);

				if(j == 0)
				{
					if((klabs(GlobalMembersGLOBAL.hittype[g_i].lastvx-g_sp.x)+klabs(GlobalMembersGLOBAL.hittype[g_i].lastvy-g_sp.y)) < (klabs(GlobalMembersGLOBAL.hittype[g_i].lastvx-s.x)+klabs(GlobalMembersGLOBAL.hittype[g_i].lastvy-s.y)))
							j = 0;

					if(j == 0)
					{
						j = GlobalMembersGAMEDEF.furthestcanseepoint(g_i, ref s, ref GlobalMembersGLOBAL.hittype[g_i].lastvx, ref GlobalMembersGLOBAL.hittype[g_i].lastvy);

						if(j == -1)
							j = 0;
						else
							j = 1;
					}
				}
				else
				{
					GlobalMembersGLOBAL.hittype[g_i].lastvx = s.x;
					GlobalMembersGLOBAL.hittype[g_i].lastvy = s.y;
				}

				if(j == 1 && (g_sp.statnum == 1 || g_sp.statnum == 6))
					GlobalMembersGLOBAL.hittype[g_i].timetosleep = SLEEPTIME;

				GlobalMembersGAMEDEF.parseifelse(j == 1);
				break;
			}

			case 6:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersACTORS.ifhitbyweapon(g_i) >= 0);
				break;
			case 27:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersACTORS.ifsquished(g_i, g_p) == 1);
				break;
			case 26:
				{
					j = g_sp.extra;
					if(g_sp.picnum == APLAYER)
						j--;
					GlobalMembersGAMEDEF.parseifelse(j < 0);
				}
				break;
			case 24:
				GlobalMembersGLOBAL.insptr++;
				g_t[5] = GlobalMembersGLOBAL.insptr;
				g_t[4] = (int)(g_t[5]); // Action
				g_t[1] = (int)(g_t[5]+4); // move
				g_sp.hitag = (int)(g_t[5]+8); // Ai
				g_t[0] = g_t[2] = g_t[3] = 0;
				if(g_sp.hitag &random_angle)
					g_sp.ang = TRAND &2047;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 7:
				GlobalMembersGLOBAL.insptr++;
				g_t[2] = 0;
				g_t[3] = 0;
				g_t[4] = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;

			case 8:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_x < GlobalMembersGLOBAL.insptr);
				if(g_x > MAXSLEEPDIST && GlobalMembersGLOBAL.hittype[g_i].timetosleep == 0)
					GlobalMembersGLOBAL.hittype[g_i].timetosleep = SLEEPTIME;
				break;
			case 9:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_x > GlobalMembersGLOBAL.insptr);
				if(g_x > MAXSLEEPDIST && GlobalMembersGLOBAL.hittype[g_i].timetosleep == 0)
					GlobalMembersGLOBAL.hittype[g_i].timetosleep = SLEEPTIME;
				break;
			case 10:
				GlobalMembersGLOBAL.insptr = (int) *(GlobalMembersGLOBAL.insptr+1);
				break;
			case 100:
				GlobalMembersGLOBAL.insptr++;
				g_sp.extra += GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 11:
				GlobalMembersGLOBAL.insptr++;
				g_sp.extra = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 94:
				GlobalMembersGLOBAL.insptr++;

				if(GlobalMembersGLOBAL.ud.coop >= 1 && GlobalMembersGLOBAL.ud.multimode > 1)
				{
					if(GlobalMembersGLOBAL.insptr == 0)
					{
						for(j = 0;j < GlobalMembersGLOBAL.ps[g_p].weapreccnt;j++)
							if(GlobalMembersGLOBAL.ps[g_p].weaprecs[j] == g_sp.picnum)
								break;

						GlobalMembersGAMEDEF.parseifelse(j < GlobalMembersGLOBAL.ps[g_p].weapreccnt && g_sp.owner == g_i);
					}
					else if(GlobalMembersGLOBAL.ps[g_p].weapreccnt < 16)
					{
						GlobalMembersGLOBAL.ps[g_p].weaprecs[GlobalMembersGLOBAL.ps[g_p].weapreccnt++] = g_sp.picnum;
						GlobalMembersGAMEDEF.parseifelse(g_sp.owner == g_i);
					}
				}
				else
					GlobalMembersGAMEDEF.parseifelse(0);
				break;
			case 95:
				GlobalMembersGLOBAL.insptr++;
				if(g_sp.picnum == APLAYER)
					g_sp.pal = GlobalMembersGLOBAL.ps[g_sp.yvel].palookup;
				else
					g_sp.pal = GlobalMembersGLOBAL.hittype[g_i].tempang;
				GlobalMembersGLOBAL.hittype[g_i].tempang = 0;
				break;
			case 104:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersPLAYER.checkweapons(ref GlobalMembersGLOBAL.ps[g_sp.yvel]);
				break;
			case 106:
				GlobalMembersGLOBAL.insptr++;
				break;
			case 97:
				GlobalMembersGLOBAL.insptr++;
				if(GlobalMembersGLOBAL.Sound[g_sp.yvel].num == 0)
					GlobalMembersSOUNDS.spritesound(g_sp.yvel, g_i);
				break;
			case 96:
				GlobalMembersGLOBAL.insptr++;

				if(GlobalMembersGLOBAL.ud.multimode > 1 && g_sp.picnum == APLAYER)
				{
					if(GlobalMembersGLOBAL.ps[GlobalMembersACTORS.otherp].quick_kick == 0)
						GlobalMembersGLOBAL.ps[GlobalMembersACTORS.otherp].quick_kick = 14;
				}
				else if(g_sp.picnum != APLAYER && GlobalMembersGLOBAL.ps[g_p].quick_kick == 0)
					GlobalMembersGLOBAL.ps[g_p].quick_kick = 14;
				break;
			case 28:
				GlobalMembersGLOBAL.insptr++;

				j = ((GlobalMembersGLOBAL.insptr)-g_sp.xrepeat)<<1;
				g_sp.xrepeat += ksgn(j);

				GlobalMembersGLOBAL.insptr++;

				if((g_sp.picnum == APLAYER && g_sp.yrepeat < 36) || GlobalMembersGLOBAL.insptr < g_sp.yrepeat || ((g_sp.yrepeat*(tilesizy[g_sp.picnum]+8))<<2) < (GlobalMembersGLOBAL.hittype[g_i].floorz - GlobalMembersGLOBAL.hittype[g_i].ceilingz))
				{
					j = ((GlobalMembersGLOBAL.insptr)-g_sp.yrepeat)<<1;
					if(klabs(j))
						g_sp.yrepeat += ksgn(j);
				}

				GlobalMembersGLOBAL.insptr++;

				break;
			case 99:
				GlobalMembersGLOBAL.insptr++;
				g_sp.xrepeat = (sbyte) GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				g_sp.yrepeat = (sbyte) GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 13:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersPLAYER.shoot(g_i, (short) GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 87:
				GlobalMembersGLOBAL.insptr++;
				if(GlobalMembersGLOBAL.Sound[GlobalMembersGLOBAL.insptr].num == 0)
					GlobalMembersSOUNDS.spritesound((short) GlobalMembersGLOBAL.insptr, g_i);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 89:
				GlobalMembersGLOBAL.insptr++;
				if(GlobalMembersGLOBAL.Sound[GlobalMembersGLOBAL.insptr].num > 0)
					GlobalMembersSOUNDS.stopsound((short) GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 92:
				GlobalMembersGLOBAL.insptr++;
				if(g_p == GlobalMembersGLOBAL.screenpeek || GlobalMembersGLOBAL.ud.coop==1)
					GlobalMembersSOUNDS.spritesound((short) GlobalMembersGLOBAL.insptr, GlobalMembersGLOBAL.ps[GlobalMembersGLOBAL.screenpeek].i);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 15:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersSOUNDS.spritesound((short) GlobalMembersGLOBAL.insptr, g_i);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 84:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGLOBAL.ps[g_p].tipincs = 26;
				break;
			case 16:
				GlobalMembersGLOBAL.insptr++;
				g_sp.xoffset = 0;
				g_sp.yoffset = 0;
	//            if(!gotz)
				{
					int c;

					if (GlobalMembersACTORS.floorspace(g_sp.sectnum) != 0)
						c = 0;
					else
					{
						if(GlobalMembersACTORS.ceilingspace(g_sp.sectnum) != 0 || sector[g_sp.sectnum].lotag == 2)
							c = GlobalMembersGLOBAL.gc/6;
						else
							c = GlobalMembersGLOBAL.gc;
					}

					if(GlobalMembersGLOBAL.hittype[g_i].cgg <= 0 || (sector[g_sp.sectnum].floorstat &2))
					{
						GlobalMembersGAMEDEF.getglobalz(g_i);
						GlobalMembersGLOBAL.hittype[g_i].cgg = 6;
					}
					else
						GlobalMembersGLOBAL.hittype[g_i].cgg --;

					if(g_sp.z < (GlobalMembersGLOBAL.hittype[g_i].floorz-FOURSLEIGHT))
					{
						g_sp.zvel += c;
						g_sp.z+=g_sp.zvel;

						if(g_sp.zvel > 6144)
							g_sp.zvel = 6144;
					}
					else
					{
						g_sp.z = GlobalMembersGLOBAL.hittype[g_i].floorz - FOURSLEIGHT;

						if(GlobalMembersGAME.badguy(ref g_sp) != 0 || (g_sp.picnum == APLAYER && g_sp.owner >= 0))
						{

							if(g_sp.zvel > 3084 && g_sp.extra <= 1)
							{
								if(g_sp.pal != 1 && g_sp.picnum != DRONE)
								{
									if(g_sp.picnum == APLAYER && g_sp.extra > 0)
										goto SKIPJIBS;
									GlobalMembersACTORS.guts(ref g_sp, JIBS6, 15, g_p);
									GlobalMembersSOUNDS.spritesound(SQUISHED, g_i);
									GlobalMembersGAME.spawn(g_i, BLOODPOOL);
								}

								SKIPJIBS:

								GlobalMembersGLOBAL.hittype[g_i].picnum = SHOTSPARK1;
								GlobalMembersGLOBAL.hittype[g_i].extra = 1;
								g_sp.zvel = 0;
							}
							else if(g_sp.zvel > 2048 && sector[g_sp.sectnum].lotag != 1)
							{

								j = g_sp.sectnum;
								pushmove(g_sp.x, g_sp.y, g_sp.z, j, 128, (4<<8), (4<<8), CLIPMASK0);
								if(j != g_sp.sectnum && j >= 0 && j < MAXSECTORS)
									changespritesect(g_i,j);

								GlobalMembersSOUNDS.spritesound(THUD, g_i);
							}
						}
						if(sector[g_sp.sectnum].lotag == 1)
							switch (g_sp.picnum)
							{
								case OCTABRAIN:
								case COMMANDER:
								case DRONE:
									break;
								default:
									g_sp.z += (24<<8);
									break;
							}
						else
							g_sp.zvel = 0;
					}
				}

				break;
			case 4:
			case 12:
			case 18:
				return 1;
			case 30:
				GlobalMembersGLOBAL.insptr++;
				return 1;
			case 2:
				GlobalMembersGLOBAL.insptr++;
				if(GlobalMembersGLOBAL.ps[g_p].ammo_amount[GlobalMembersGLOBAL.insptr] >= GlobalMembersGLOBAL.max_ammo_amount[GlobalMembersGLOBAL.insptr])
				{
					GlobalMembersGLOBAL.killit_flag = 2;
					break;
				}
				GlobalMembersACTORS.addammo(GlobalMembersGLOBAL.insptr, ref GlobalMembersGLOBAL.ps[g_p], *(GlobalMembersGLOBAL.insptr+1));
				if(GlobalMembersGLOBAL.ps[g_p].curr_weapon == KNEE_WEAPON)
					if(GlobalMembersGLOBAL.ps[g_p].gotweapon[GlobalMembersGLOBAL.insptr])
						GlobalMembersACTORS.addweapon(ref GlobalMembersGLOBAL.ps[g_p], GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr += 2;
				break;
			case 86:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersACTORS.lotsofmoney(ref g_sp, GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 102:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersACTORS.lotsofmail(ref g_sp, GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 105:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGLOBAL.hittype[g_i].timetosleep = (short) GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 103:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersACTORS.lotsofpaper(ref g_sp, GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 88:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGLOBAL.ps[g_p].actors_killed += GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.hittype[g_i].actorstayput = -1;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 93:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAME.spriteglass(g_i, GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 22:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGLOBAL.killit_flag = 1;
				break;
			case 23:
				GlobalMembersGLOBAL.insptr++;
				if(GlobalMembersGLOBAL.ps[g_p].gotweapon[GlobalMembersGLOBAL.insptr] == 0)
					GlobalMembersACTORS.addweapon(ref GlobalMembersGLOBAL.ps[g_p], GlobalMembersGLOBAL.insptr);
				else if(GlobalMembersGLOBAL.ps[g_p].ammo_amount[GlobalMembersGLOBAL.insptr] >= GlobalMembersGLOBAL.max_ammo_amount[GlobalMembersGLOBAL.insptr])
				{
					 GlobalMembersGLOBAL.killit_flag = 2;
					 break;
				}
				GlobalMembersACTORS.addammo(GlobalMembersGLOBAL.insptr, ref GlobalMembersGLOBAL.ps[g_p], *(GlobalMembersGLOBAL.insptr+1));
				if(GlobalMembersGLOBAL.ps[g_p].curr_weapon == KNEE_WEAPON)
					if(GlobalMembersGLOBAL.ps[g_p].gotweapon[GlobalMembersGLOBAL.insptr])
						GlobalMembersACTORS.addweapon(ref GlobalMembersGLOBAL.ps[g_p], GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr+=2;
				break;
			case 68:
				GlobalMembersGLOBAL.insptr++;
				Console.Write("{0:D}\n", GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 69:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGLOBAL.ps[g_p].timebeforeexit = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.ps[g_p].customexitsound = -1;
				GlobalMembersGLOBAL.ud.eog = 1;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 25:
				GlobalMembersGLOBAL.insptr++;

				if(GlobalMembersGLOBAL.ps[g_p].newowner >= 0)
				{
					GlobalMembersGLOBAL.ps[g_p].newowner = -1;
					GlobalMembersGLOBAL.ps[g_p].posx = GlobalMembersGLOBAL.ps[g_p].oposx;
					GlobalMembersGLOBAL.ps[g_p].posy = GlobalMembersGLOBAL.ps[g_p].oposy;
					GlobalMembersGLOBAL.ps[g_p].posz = GlobalMembersGLOBAL.ps[g_p].oposz;
					GlobalMembersGLOBAL.ps[g_p].ang = GlobalMembersGLOBAL.ps[g_p].oang;
					updatesector(GlobalMembersGLOBAL.ps[g_p].posx,GlobalMembersGLOBAL.ps[g_p].posy,GlobalMembersGLOBAL.ps[g_p].cursectnum);
					GlobalMembersPLAYER.setpal(ref GlobalMembersGLOBAL.ps[g_p]);

					j = headspritestat[1];
					while(j >= 0)
					{
						if(sprite[j].picnum==CAMERA1)
							sprite[j].yvel = 0;
						j = nextspritestat[j];
					}
				}

				j = sprite[GlobalMembersGLOBAL.ps[g_p].i].extra;

				if(g_sp.picnum != ATOMICHEALTH)
				{
					if(j > GlobalMembersGLOBAL.max_player_health && GlobalMembersGLOBAL.insptr > 0)
					{
						GlobalMembersGLOBAL.insptr++;
						break;
					}
					else
					{
						if(j > 0)
							j += GlobalMembersGLOBAL.insptr;
						if (j > GlobalMembersGLOBAL.max_player_health && GlobalMembersGLOBAL.insptr > 0)
							j = GlobalMembersGLOBAL.max_player_health;
					}
				}
				else
				{
					if(j > 0)
						j += GlobalMembersGLOBAL.insptr;
					if (j > (GlobalMembersGLOBAL.max_player_health<<1))
						j = (GlobalMembersGLOBAL.max_player_health<<1);
				}

				if(j < 0)
					j = 0;

				if(GlobalMembersGLOBAL.ud.god == 0)
				{
					if(GlobalMembersGLOBAL.insptr > 0)
					{
						if((j - GlobalMembersGLOBAL.insptr) < (GlobalMembersGLOBAL.max_player_health>>2) && j >= (GlobalMembersGLOBAL.max_player_health>>2))
								GlobalMembersSOUNDS.spritesound(DUKE_GOTHEALTHATLOW, GlobalMembersGLOBAL.ps[g_p].i);

						GlobalMembersGLOBAL.ps[g_p].last_extra = j;
					}

					sprite[GlobalMembersGLOBAL.ps[g_p].i].extra = j;
				}

				GlobalMembersGLOBAL.insptr++;
				break;
			case 17:
				{
					int tempscrptr;

					tempscrptr = GlobalMembersGLOBAL.insptr+2;

					GlobalMembersGLOBAL.insptr = (int) *(GlobalMembersGLOBAL.insptr+1);
					while (true)
						if (GlobalMembersGAMEDEF.parse() != 0)
							break;
					GlobalMembersGLOBAL.insptr = tempscrptr;
				}
				break;
			case 29:
				GlobalMembersGLOBAL.insptr++;
				while (true)
					if (GlobalMembersGAMEDEF.parse() != 0)
						break;
				break;
			case 32:
				g_t[0]=0;
				GlobalMembersGLOBAL.insptr++;
				g_t[1] = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				g_sp.hitag = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				if(g_sp.hitag &random_angle)
					g_sp.ang = TRAND &2047;
				break;
			case 31:
				GlobalMembersGLOBAL.insptr++;
				if(g_sp.sectnum >= 0 && g_sp.sectnum < MAXSECTORS)
					GlobalMembersGAME.spawn(g_i, GlobalMembersGLOBAL.insptr);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 33:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.hittype[g_i].picnum == GlobalMembersGLOBAL.insptr);
				break;
			case 21:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_t[5] == GlobalMembersGLOBAL.insptr);
				break;
			case 34:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_t[4] == GlobalMembersGLOBAL.insptr);
				break;
			case 35:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_t[2] >= GlobalMembersGLOBAL.insptr);
				break;
			case 36:
				GlobalMembersGLOBAL.insptr++;
				g_t[2] = 0;
				break;
			case 37:
				{
					short dnum;

					GlobalMembersGLOBAL.insptr++;
					dnum = GlobalMembersGLOBAL.insptr;
					GlobalMembersGLOBAL.insptr++;

					if(g_sp.sectnum >= 0 && g_sp.sectnum < MAXSECTORS)
						for(j = (GlobalMembersGLOBAL.insptr)-1;j>=0;j--)
					{
						if(g_sp.picnum == BLIMP && dnum == SCRAP1)
							s = 0;
						else
							s = (TRAND%3);

						l = GlobalMembersGAME.EGS(g_sp.sectnum, g_sp.x+(TRAND &255)-128, g_sp.y+(TRAND &255)-128, g_sp.z-(8<<8)-(TRAND &8191), dnum+s, g_sp.shade, 32+(TRAND &15), 32+(TRAND &15), TRAND &2047, (TRAND &127)+32, -(TRAND &2047), g_i, 5);
						if(g_sp.picnum == BLIMP && dnum == SCRAP1)
							sprite[l].yvel = GlobalMembersGLOBAL.weaponsandammosprites[j%14];
						else
							sprite[l].yvel = -1;
						sprite[l].pal = g_sp.pal;
					}
					GlobalMembersGLOBAL.insptr++;
				}
				break;
			case 52:
				GlobalMembersGLOBAL.insptr++;
				g_t[0] = (short) GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 101:
				GlobalMembersGLOBAL.insptr++;
				g_sp.cstat |= (short) GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 110:
				GlobalMembersGLOBAL.insptr++;
				g_sp.clipdist = (short) GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 40:
				GlobalMembersGLOBAL.insptr++;
				g_sp.cstat = (short) GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;
			case 41:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_t[1] == GlobalMembersGLOBAL.insptr);
				break;
			case 42:
				GlobalMembersGLOBAL.insptr++;

				if(GlobalMembersGLOBAL.ud.multimode < 2)
				{
					if(GlobalMembersMENUES.lastsavedpos >= 0 && GlobalMembersGLOBAL.ud.recstat != 2)
					{
						GlobalMembersGLOBAL.ps[g_p].gm = MODE_MENU;
						KB_ClearKeyDown(sc_Space);
						GlobalMembersMENUES.cmenu(15000);
					}
					else
						GlobalMembersGLOBAL.ps[g_p].gm = MODE_RESTART;
					GlobalMembersGLOBAL.killit_flag = 2;
				}
				else
				{
					GlobalMembersPREMAP.pickrandomspot(g_p);
					g_sp.x = GlobalMembersGLOBAL.hittype[g_i].bposx = GlobalMembersGLOBAL.ps[g_p].bobposx = GlobalMembersGLOBAL.ps[g_p].oposx = GlobalMembersGLOBAL.ps[g_p].posx;
					g_sp.y = GlobalMembersGLOBAL.hittype[g_i].bposy = GlobalMembersGLOBAL.ps[g_p].bobposy = GlobalMembersGLOBAL.ps[g_p].oposy =GlobalMembersGLOBAL.ps[g_p].posy;
					g_sp.z = GlobalMembersGLOBAL.hittype[g_i].bposy = GlobalMembersGLOBAL.ps[g_p].oposz =GlobalMembersGLOBAL.ps[g_p].posz;
					updatesector(GlobalMembersGLOBAL.ps[g_p].posx,GlobalMembersGLOBAL.ps[g_p].posy,GlobalMembersGLOBAL.ps[g_p].cursectnum);
					setsprite(GlobalMembersGLOBAL.ps[g_p].i,GlobalMembersGLOBAL.ps[g_p].posx,GlobalMembersGLOBAL.ps[g_p].posy,GlobalMembersGLOBAL.ps[g_p].posz+PHEIGHT);
					g_sp.cstat = 257;

					g_sp.shade = -12;
					g_sp.clipdist = 64;
					g_sp.xrepeat = 42;
					g_sp.yrepeat = 36;
					g_sp.owner = g_i;
					g_sp.xoffset = 0;
					g_sp.pal = GlobalMembersGLOBAL.ps[g_p].palookup;

					GlobalMembersGLOBAL.ps[g_p].last_extra = g_sp.extra = GlobalMembersGLOBAL.max_player_health;
					GlobalMembersGLOBAL.ps[g_p].wantweaponfire = -1;
					GlobalMembersGLOBAL.ps[g_p].horiz = 100;
					GlobalMembersGLOBAL.ps[g_p].on_crane = -1;
					GlobalMembersGLOBAL.ps[g_p].frag_ps = g_p;
					GlobalMembersGLOBAL.ps[g_p].horizoff = 0;
					GlobalMembersGLOBAL.ps[g_p].opyoff = 0;
					GlobalMembersGLOBAL.ps[g_p].wackedbyactor = -1;
					GlobalMembersGLOBAL.ps[g_p].shield_amount = GlobalMembersGLOBAL.max_armour_amount;
					GlobalMembersGLOBAL.ps[g_p].dead_flag = 0;
					GlobalMembersGLOBAL.ps[g_p].pals_time = 0;
					GlobalMembersGLOBAL.ps[g_p].footprintcount = 0;
					GlobalMembersGLOBAL.ps[g_p].weapreccnt = 0;
					GlobalMembersGLOBAL.ps[g_p].fta = 0;
					GlobalMembersGLOBAL.ps[g_p].ftq = 0;
					GlobalMembersGLOBAL.ps[g_p].posxv = GlobalMembersGLOBAL.ps[g_p].posyv = 0;
					GlobalMembersGLOBAL.ps[g_p].rotscrnang = 0;

					GlobalMembersGLOBAL.ps[g_p].falling_counter = 0;

					GlobalMembersGLOBAL.hittype[g_i].extra = -1;
					GlobalMembersGLOBAL.hittype[g_i].owner = g_i;

					GlobalMembersGLOBAL.hittype[g_i].cgg = 0;
					GlobalMembersGLOBAL.hittype[g_i].movflag = 0;
					GlobalMembersGLOBAL.hittype[g_i].tempang = 0;
					GlobalMembersGLOBAL.hittype[g_i].actorstayput = -1;
					GlobalMembersGLOBAL.hittype[g_i].dispicnum = 0;
					GlobalMembersGLOBAL.hittype[g_i].owner = GlobalMembersGLOBAL.ps[g_p].i;

					GlobalMembersPREMAP.resetinventory(g_p);
					GlobalMembersPREMAP.resetweapons(g_p);

					GlobalMembersGAME.cameradist = 0;
					GlobalMembersGAME.cameraclock = totalclock;
				}
				GlobalMembersPLAYER.setpal(ref GlobalMembersGLOBAL.ps[g_p]);

				break;
			case 43:
				GlobalMembersGAMEDEF.parseifelse(klabs(g_sp.z-sector[g_sp.sectnum].floorz) < (32<<8) && sector[g_sp.sectnum].lotag == 1);
				break;
			case 44:
				GlobalMembersGAMEDEF.parseifelse(sector[g_sp.sectnum].lotag == 2);
				break;
			case 46:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_t[0] >= GlobalMembersGLOBAL.insptr);
				break;
			case 53:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_sp.picnum == GlobalMembersGLOBAL.insptr);
				break;
			case 47:
				GlobalMembersGLOBAL.insptr++;
				g_t[0] = 0;
				break;
			case 48:
				GlobalMembersGLOBAL.insptr+=2;
				switch(*(GlobalMembersGLOBAL.insptr-1))
				{
					case 0:
						GlobalMembersGLOBAL.ps[g_p].steroids_amount = GlobalMembersGLOBAL.insptr;
						GlobalMembersGLOBAL.ps[g_p].inven_icon = 2;
						break;
					case 1:
						GlobalMembersGLOBAL.ps[g_p].shield_amount += GlobalMembersGLOBAL.insptr; // 100;
						if(GlobalMembersGLOBAL.ps[g_p].shield_amount > GlobalMembersGLOBAL.max_player_health)
							GlobalMembersGLOBAL.ps[g_p].shield_amount = GlobalMembersGLOBAL.max_player_health;
						break;
					case 2:
						GlobalMembersGLOBAL.ps[g_p].scuba_amount = GlobalMembersGLOBAL.insptr; // 1600;
						GlobalMembersGLOBAL.ps[g_p].inven_icon = 6;
						break;
					case 3:
						GlobalMembersGLOBAL.ps[g_p].holoduke_amount = GlobalMembersGLOBAL.insptr; // 1600;
						GlobalMembersGLOBAL.ps[g_p].inven_icon = 3;
						break;
					case 4:
						GlobalMembersGLOBAL.ps[g_p].jetpack_amount = GlobalMembersGLOBAL.insptr; // 1600;
						GlobalMembersGLOBAL.ps[g_p].inven_icon = 4;
						break;
					case 6:
						switch(g_sp.pal)
						{
							case 0:
								GlobalMembersGLOBAL.ps[g_p].got_access |= 1;
								break;
							case 21:
								GlobalMembersGLOBAL.ps[g_p].got_access |= 2;
								break;
							case 23:
								GlobalMembersGLOBAL.ps[g_p].got_access |= 4;
								break;
						}
						break;
					case 7:
						GlobalMembersGLOBAL.ps[g_p].heat_amount = GlobalMembersGLOBAL.insptr;
						GlobalMembersGLOBAL.ps[g_p].inven_icon = 5;
						break;
					case 9:
						GlobalMembersGLOBAL.ps[g_p].inven_icon = 1;
						GlobalMembersGLOBAL.ps[g_p].firstaid_amount = GlobalMembersGLOBAL.insptr;
						break;
					case 10:
						GlobalMembersGLOBAL.ps[g_p].inven_icon = 7;
						GlobalMembersGLOBAL.ps[g_p].boot_amount = GlobalMembersGLOBAL.insptr;
						break;
				}
				GlobalMembersGLOBAL.insptr++;
				break;
			case 50:
				GlobalMembersACTORS.hitradius(g_i, *(GlobalMembersGLOBAL.insptr+1), *(GlobalMembersGLOBAL.insptr+2), *(GlobalMembersGLOBAL.insptr+3), *(GlobalMembersGLOBAL.insptr+4), *(GlobalMembersGLOBAL.insptr+5));
				GlobalMembersGLOBAL.insptr+=6;
				break;
			case 51:
				{
					GlobalMembersGLOBAL.insptr++;

					l = GlobalMembersGLOBAL.insptr;
					j = 0;

					s = g_sp.xvel;

					if((l &8) && GlobalMembersGLOBAL.ps[g_p].on_ground && (GlobalMembersGLOBAL.sync[g_p].bits &2))
						   j = 1;
					else if((l &16) && GlobalMembersGLOBAL.ps[g_p].jumping_counter == 0 && !GlobalMembersGLOBAL.ps[g_p].on_ground && GlobalMembersGLOBAL.ps[g_p].poszv > 2048)
							j = 1;
					else if((l &32) && GlobalMembersGLOBAL.ps[g_p].jumping_counter > 348)
						   j = 1;
					else if((l &1) && s >= 0 && s < 8)
						   j = 1;
					else if((l &2) && s >= 8 && !(GlobalMembersGLOBAL.sync[g_p].bits&(1<<5)))
						   j = 1;
					else if((l &4) && s >= 8 && GlobalMembersGLOBAL.sync[g_p].bits&(1<<5))
						   j = 1;
					else if((l &64) && GlobalMembersGLOBAL.ps[g_p].posz < (g_sp.z-(48<<8)))
						   j = 1;
					else if((l &128) && s <= -8 && !(GlobalMembersGLOBAL.sync[g_p].bits&(1<<5)))
						   j = 1;
					else if((l &256) && s <= -8 && (GlobalMembersGLOBAL.sync[g_p].bits&(1<<5)))
						   j = 1;
					else if((l &512) && (GlobalMembersGLOBAL.ps[g_p].quick_kick > 0 || (GlobalMembersGLOBAL.ps[g_p].curr_weapon == KNEE_WEAPON && GlobalMembersGLOBAL.ps[g_p].kickback_pic > 0)))
						   j = 1;
					else if((l &1024) && sprite[GlobalMembersGLOBAL.ps[g_p].i].xrepeat < 32)
						   j = 1;
					else if((l &2048) && GlobalMembersGLOBAL.ps[g_p].jetpack_on)
						   j = 1;
					else if((l &4096) && GlobalMembersGLOBAL.ps[g_p].steroids_amount > 0 && GlobalMembersGLOBAL.ps[g_p].steroids_amount < 400)
						   j = 1;
					else if((l &8192) && GlobalMembersGLOBAL.ps[g_p].on_ground)
						   j = 1;
					else if((l &16384) && sprite[GlobalMembersGLOBAL.ps[g_p].i].xrepeat > 32 && sprite[GlobalMembersGLOBAL.ps[g_p].i].extra > 0 && GlobalMembersGLOBAL.ps[g_p].timebeforeexit == 0)
						   j = 1;
					else if((l &32768) && sprite[GlobalMembersGLOBAL.ps[g_p].i].extra <= 0)
						   j = 1;
					else if((l &65536))
					{
						if(g_sp.picnum == APLAYER && GlobalMembersGLOBAL.ud.multimode > 1)
							j = GlobalMembersGAMEDEF.getincangle(GlobalMembersGLOBAL.ps[GlobalMembersACTORS.otherp].ang, getangle(GlobalMembersGLOBAL.ps[g_p].posx-GlobalMembersGLOBAL.ps[GlobalMembersACTORS.otherp].posx,GlobalMembersGLOBAL.ps[g_p].posy-GlobalMembersGLOBAL.ps[GlobalMembersACTORS.otherp].posy));
						else
							j = GlobalMembersGAMEDEF.getincangle(GlobalMembersGLOBAL.ps[g_p].ang, getangle(g_sp.x-GlobalMembersGLOBAL.ps[g_p].posx,g_sp.y-GlobalMembersGLOBAL.ps[g_p].posy));

						if(j > -128 && j < 128)
							j = 1;
						else
							j = 0;
					}

					GlobalMembersGAMEDEF.parseifelse((int) j);

				}
				break;
			case 56:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_sp.extra <= GlobalMembersGLOBAL.insptr);
				break;
			case 58:
				GlobalMembersGLOBAL.insptr += 2;
				GlobalMembersACTORS.guts(ref g_sp, *(GlobalMembersGLOBAL.insptr-1), GlobalMembersGLOBAL.insptr, g_p);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 59:
				GlobalMembersGLOBAL.insptr++;
	//            if(g_sp->owner >= 0 && sprite[g_sp->owner].picnum == *insptr)
	  //              parseifelse(1);
	//            else
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.hittype[g_i].picnum == GlobalMembersGLOBAL.insptr);
				break;
			case 61:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersPLAYER.forceplayerangle(ref GlobalMembersGLOBAL.ps[g_p]);
				return 0;
			case 62:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(((GlobalMembersGLOBAL.hittype[g_i].floorz - GlobalMembersGLOBAL.hittype[g_i].ceilingz) >> 8) < GlobalMembersGLOBAL.insptr);
				break;
			case 63:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.sync[g_p].bits&(1<<29));
				break;
			case 64:
				GlobalMembersGAMEDEF.parseifelse(sector[g_sp.sectnum].ceilingstat &1);
				break;
			case 65:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.ud.multimode > 1);
				break;
			case 66:
				GlobalMembersGLOBAL.insptr++;
				if(sector[g_sp.sectnum].lotag == 0)
				{
					neartag(g_sp.x,g_sp.y,g_sp.z-(32<<8),g_sp.sectnum,g_sp.ang,GlobalMembersGLOBAL.neartagsector,GlobalMembersGLOBAL.neartagwall,GlobalMembersGLOBAL.neartagsprite,GlobalMembersGLOBAL.neartaghitdist,768,1);
					if(GlobalMembersGLOBAL.neartagsector >= 0 && GlobalMembersSECTOR.isanearoperator(sector[GlobalMembersGLOBAL.neartagsector].lotag) != 0)
						if((sector[GlobalMembersGLOBAL.neartagsector].lotag &0xff) == 23 || sector[GlobalMembersGLOBAL.neartagsector].floorz == sector[GlobalMembersGLOBAL.neartagsector].ceilingz)
							if((sector[GlobalMembersGLOBAL.neartagsector].lotag &16384) == 0)
								if((sector[GlobalMembersGLOBAL.neartagsector].lotag &32768) == 0)
							{
								j = headspritesect[GlobalMembersGLOBAL.neartagsector];
								while(j >= 0)
								{
									if(sprite[j].picnum == ACTIVATOR)
										break;
									j = nextspritesect[j];
								}
								if(j == -1)
									GlobalMembersSECTOR.operatesectors(GlobalMembersGLOBAL.neartagsector, g_i);
							}
				}
				break;
			case 67:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersACTORS.ceilingspace(g_sp.sectnum));
				break;

			case 74:
				GlobalMembersGLOBAL.insptr++;
				if(g_sp.picnum != APLAYER)
					GlobalMembersGLOBAL.hittype[g_i].tempang = g_sp.pal;
				g_sp.pal = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;

			case 77:
				GlobalMembersGLOBAL.insptr++;
				g_sp.picnum = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				break;

			case 70:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersGAMEDEF.dodge(ref g_sp) == 1);
				break;
			case 71:
				if (GlobalMembersGAME.badguy(ref g_sp) != 0)
					GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.ud.respawn_monsters);
				else if (GlobalMembersGAME.inventory(ref g_sp) != 0)
					GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.ud.respawn_inventory);
				else
					GlobalMembersGAMEDEF.parseifelse(GlobalMembersGLOBAL.ud.respawn_items);
				break;
			case 72:
				GlobalMembersGLOBAL.insptr++;
	//            getglobalz(g_i);
				GlobalMembersGAMEDEF.parseifelse((GlobalMembersGLOBAL.hittype[g_i].floorz - g_sp.z) <= ((GlobalMembersGLOBAL.insptr)<<8));
				break;
			case 73:
				GlobalMembersGLOBAL.insptr++;
	//            getglobalz(g_i);
				GlobalMembersGAMEDEF.parseifelse((g_sp.z - GlobalMembersGLOBAL.hittype[g_i].ceilingz) <= ((GlobalMembersGLOBAL.insptr)<<8));
				break;
			case 14:

				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGLOBAL.ps[g_p].pals_time = GlobalMembersGLOBAL.insptr;
				GlobalMembersGLOBAL.insptr++;
				for(j = 0;j<3;j++)
				{
					GlobalMembersGLOBAL.ps[g_p].pals[j] = GlobalMembersGLOBAL.insptr;
					GlobalMembersGLOBAL.insptr++;
				}
				break;

	/*        case 74:
	            insptr++;
	            getglobalz(g_i);
	            parseifelse( (( hittype[g_i].floorz - hittype[g_i].ceilingz ) >> 8 ) >= *insptr);
	            break;
	*/
			case 78:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(sprite[GlobalMembersGLOBAL.ps[g_p].i].extra < GlobalMembersGLOBAL.insptr);
				break;

			case 75:
				{
					GlobalMembersGLOBAL.insptr++;
					j = 0;
					switch(*(GlobalMembersGLOBAL.insptr++))
					{
						case 0:
							if(GlobalMembersGLOBAL.ps[g_p].steroids_amount != GlobalMembersGLOBAL.insptr)
							   j = 1;
							break;
						case 1:
							if(GlobalMembersGLOBAL.ps[g_p].shield_amount != GlobalMembersGLOBAL.max_player_health)
								j = 1;
							break;
						case 2:
							if(GlobalMembersGLOBAL.ps[g_p].scuba_amount != GlobalMembersGLOBAL.insptr)
								j = 1;
								break;
						case 3:
							if(GlobalMembersGLOBAL.ps[g_p].holoduke_amount != GlobalMembersGLOBAL.insptr)
								j = 1;
								break;
						case 4:
							if(GlobalMembersGLOBAL.ps[g_p].jetpack_amount != GlobalMembersGLOBAL.insptr)
								j = 1;
								break;
						case 6:
							switch(g_sp.pal)
							{
								case 0:
									if(GlobalMembersGLOBAL.ps[g_p].got_access &1)
										j = 1;
										break;
								case 21:
									if(GlobalMembersGLOBAL.ps[g_p].got_access &2)
										j = 1;
										break;
								case 23:
									if(GlobalMembersGLOBAL.ps[g_p].got_access &4)
										j = 1;
										break;
							}
							break;
						case 7:
							if(GlobalMembersGLOBAL.ps[g_p].heat_amount != GlobalMembersGLOBAL.insptr)
								j = 1;
								break;
						case 9:
							if(GlobalMembersGLOBAL.ps[g_p].firstaid_amount != GlobalMembersGLOBAL.insptr)
								j = 1;
								break;
						case 10:
							if(GlobalMembersGLOBAL.ps[g_p].boot_amount != GlobalMembersGLOBAL.insptr)
								j = 1;
								break;
					}

					GlobalMembersGAMEDEF.parseifelse(j);
					break;
				}
			case 38:
				GlobalMembersGLOBAL.insptr++;
				if(GlobalMembersGLOBAL.ps[g_p].knee_incs == 0 && sprite[GlobalMembersGLOBAL.ps[g_p].i].xrepeat >= 40)
					if(cansee(g_sp.x,g_sp.y,g_sp.z-(4<<8),g_sp.sectnum,GlobalMembersGLOBAL.ps[g_p].posx,GlobalMembersGLOBAL.ps[g_p].posy,GlobalMembersGLOBAL.ps[g_p].posz+(16<<8),sprite[GlobalMembersGLOBAL.ps[g_p].i].sectnum))
				{
					GlobalMembersGLOBAL.ps[g_p].knee_incs = 1;
					if(GlobalMembersGLOBAL.ps[g_p].weapon_pos == 0)
						GlobalMembersGLOBAL.ps[g_p].weapon_pos = -1;
					GlobalMembersGLOBAL.ps[g_p].actorsqu = g_i;
				}
				break;
			case 90:
				{
					short s1;

					s1 = g_sp.sectnum;

					j = 0;

						updatesector(g_sp.x+108, g_sp.y+108, s1);
						if(s1 == g_sp.sectnum)
						{
							updatesector(g_sp.x-108, g_sp.y-108, s1);
							if(s1 == g_sp.sectnum)
							{
								updatesector(g_sp.x+108, g_sp.y-108, s1);
								if(s1 == g_sp.sectnum)
								{
									updatesector(g_sp.x-108, g_sp.y+108, s1);
									if(s1 == g_sp.sectnum)
										j = 1;
								}
							}
						}
						GlobalMembersGAMEDEF.parseifelse(j);
				}

				break;
			case 80:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAME.FTA(GlobalMembersGLOBAL.insptr, ref GlobalMembersGLOBAL.ps[g_p]);
				GlobalMembersGLOBAL.insptr++;
				break;
			case 81:
				GlobalMembersGAMEDEF.parseifelse(GlobalMembersACTORS.floorspace(g_sp.sectnum));
				break;
			case 82:
				GlobalMembersGAMEDEF.parseifelse((GlobalMembersGLOBAL.hittype[g_i].movflag &49152) > 16384);
				break;
			case 83:
				GlobalMembersGLOBAL.insptr++;
				switch(g_sp.picnum)
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
						if(g_sp.yvel)
							GlobalMembersSECTOR.operaterespawns(g_sp.yvel);
						break;
					default:
						if(g_sp.hitag >= 0)
							GlobalMembersSECTOR.operaterespawns(g_sp.hitag);
						break;
				}
				break;
			case 85:
				GlobalMembersGLOBAL.insptr++;
				GlobalMembersGAMEDEF.parseifelse(g_sp.pal == GlobalMembersGLOBAL.insptr);
				break;

			case 111:
				GlobalMembersGLOBAL.insptr++;
				j = klabs(GlobalMembersGAMEDEF.getincangle(GlobalMembersGLOBAL.ps[g_p].ang, g_sp.ang));
				GlobalMembersGAMEDEF.parseifelse(j <= GlobalMembersGLOBAL.insptr);
				break;

			case 109:

				for(j = 1;j<NUM_SOUNDS;j++)
					if(GlobalMembersGLOBAL.SoundOwner[j, 0].i == g_i)
						break;

				GlobalMembersGAMEDEF.parseifelse(j == NUM_SOUNDS);
				break;
			default:
				GlobalMembersGLOBAL.killit_flag = 1;
				break;
		}
		return 0;
	}

	public static void parseifelse(int condition)
	{
		if (condition != 0)
		{
			GlobalMembersGLOBAL.insptr+=2;
			GlobalMembersGAMEDEF.parse();
		}
		else
		{
			GlobalMembersGLOBAL.insptr = (int) *(GlobalMembersGLOBAL.insptr+1);
			if(GlobalMembersGLOBAL.insptr == 10)
			{
				GlobalMembersGLOBAL.insptr+=2;
				GlobalMembersGAMEDEF.parse();
			}
		}
	}

	public static void execute(short i,short p,int x)
	{
		sbyte done;

		g_i = i;
		g_p = p;
		g_x = x;
		g_sp = &sprite[g_i];
		g_t = GlobalMembersGLOBAL.hittype[g_i].temp_data[0];

		if(GlobalMembersGLOBAL.actorscrptr[g_sp.picnum] == 0)
			return;

		GlobalMembersGLOBAL.insptr = 4 + (GlobalMembersGLOBAL.actorscrptr[g_sp.picnum]);

		GlobalMembersGLOBAL.killit_flag = 0;

		if(g_sp.sectnum < 0 || g_sp.sectnum >= MAXSECTORS)
		{
			if (GlobalMembersGAME.badguy(ref g_sp) != 0)
				GlobalMembersGLOBAL.ps[g_p].actors_killed++;
			deletesprite(g_i);
			return;
		}

		if(g_t[4])
		{
			g_sp.lotag += TICSPERFRAME;
			if(g_sp.lotag > (int)(g_t[4]+16))
			{
				g_t[2]++;
				g_sp.lotag = 0;
				g_t[3] += (int)(g_t[4]+12);
			}
			if(klabs(g_t[3]) >= klabs((int)(g_t[4]+4) *(int)(g_t[4]+12)))
				g_t[3] = 0;
		}

		do
			done = GlobalMembersGAMEDEF.parse();
		while(done == 0);

		if(GlobalMembersGLOBAL.killit_flag == 1)
		{
			if(GlobalMembersGLOBAL.ps[g_p].actorsqu == g_i)
				GlobalMembersGLOBAL.ps[g_p].actorsqu = -1;
			deletesprite(g_i);
		}
		else
		{
			GlobalMembersGAMEDEF.move();

			if(g_sp.statnum == 1)
			{
				if (GlobalMembersGAME.badguy(ref g_sp) != 0)
				{
					if(g_sp.xrepeat > 60)
						return;
					if(GlobalMembersGLOBAL.ud.respawn_monsters == 1 && g_sp.extra <= 0)
						return;
				}
				else if(GlobalMembersGLOBAL.ud.respawn_items == 1 && (g_sp.cstat &32768))
					return;

				if(GlobalMembersGLOBAL.hittype[g_i].timetosleep > 1)
					GlobalMembersGLOBAL.hittype[g_i].timetosleep--;
				else if(GlobalMembersGLOBAL.hittype[g_i].timetosleep == 1)
					 changespritestat(g_i,2);
			}

			else if(g_sp.statnum == 6)
				switch(g_sp.picnum)
				{
					case RUBBERCAN:
					case EXPLODINGBARREL:
					case WOODENHORSE:
					case HORSEONSIDE:
					case CANWITHSOMETHING:
					case FIREBARREL:
					case NUKEBARREL:
					case NUKEBARRELDENTED:
					case NUKEBARRELLEAKED:
					case TRIPBOMB:
					case EGG:
						if(GlobalMembersGLOBAL.hittype[g_i].timetosleep > 1)
							GlobalMembersGLOBAL.hittype[g_i].timetosleep--;
						else if(GlobalMembersGLOBAL.hittype[g_i].timetosleep == 1)
							changespritestat(g_i,2);
						break;
				}
		}
	}
}

// "Duke 2000"
// "Virchua Duke"
// "Son of Death
// "Cromium"
// "Potent"
// "Flotsom"

// Volume One
// "Duke is brain dead",
// "BOOT TO THE HEAD"
// Damage too duke
// Weapons are computer cont.  Only logical thinking
// is disappearing.
// " Flips! "
// Flash on screen, inst.
// "BUMS"
// "JAIL"/"MENTAL WARD (Cop code for looney?  T. asks Cop.)"
// "GUTS OR GLORY"

// ( Duke's Mission

// Duke:    "Looks like some kind of transporter...?"
// Byte:    "...YES"

// Duke:    "Waa, here goes nuthin'. "
// (Duke puts r. arm in device)

// Duke:    AAAAHHHHHHHHHHHHHHHHHHHHHHHHH!!!
// (Duke's arm is seved.)
// Byte:    NO.NO.NO.NO.NO.NO.NO...
// ( Byte directs duke to the nearest heat source)
// (Shut Up Mode)
// ( Duke Staggers, end of arm bleeding, usual oozing arm guts. )
// Byte: Left, Left, Left, Left, Right.
// ( Duke, loozing consc, trips on broken pipe, )
// ( hits temple on edge of step. )
// ( Rats everywhere, byte pushing them away with weapon,
// ( eventually covered, show usual groosums, Duke appears dead
// ( Duke wakes up, in hospital, vision less blurry
// ( Hospital doing brain scan, 1/3 cran. mass MISSING!
// Doc: Hummm?  ( Grabbing upper lip to "appear" smart. )

// Stand back boys

// Schrapnel has busted my scull!
// Now I'm insane, Mental ward, got to escape.
// Search light everywhere.

// (M)Mendor, The Tree Dweller.
// (M)BashMan, The Destructor.
// (M)Lash, The Scavenger.
// (F)Mag, The Slut.
// (F)
// NRA OR SOMETHIN'

// Duke Nukem
// 5th Dimention
// Pentagon Man!


// I Hope your not stupid!
// The 70's meet the future.
// Dirty Harry style.  70's music with futuristic edge
// The Instant De-Welder(tm)
// I think I'm going to puke...
// Badge attitude.
// He's got a Badge(LA 3322), a Bulldog, a Bronco (beat up/bondoed).
// Gfx:
// Lite rail systems
// A church.  Large cross
// Sniper Scope,
// Really use the phone
// The Boiler Room
// The IRS, nuking other government buildings?
// You wouldn't have a belt of booz, would ya?
// Slow turning signes
// More persise shooting/descructions
// Faces, use phoneoms and its lookup.  Talking, getting in fights.
// Drug dealers, pimps, and all galore
// Weapons, Anything lying around.
// Trees to clime, burning trees.
// Sledge Hammer, Sledge hammer with Spike
// sancurary, get away from it all.
// Goodlife = ( War + Greed ) / Peace
// Monsterism           (ACTION)
// Global Hunter        (RPG)
// Slick a Wick         (PUZZLE)
// Roach Condo          (FUNNY)
// AntiProfit           (RPG)
// Pen Patrol           (TD SIM)
// 97.5 KPIG! - Wanker County
// "Fauna" - Native Indiginouns Animal Life

