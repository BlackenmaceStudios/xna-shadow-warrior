﻿//-------------------------------------------------------------------------
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
along with this program; if not, write to the Free Software
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

namespace duke3d.game
{
    public class Names
    {
        public const int SECTOREFFECTOR = 1;
        public const int ACTIVATOR = 2;
        public const int TOUCHPLATE = 3;
        public const int ACTIVATORLOCKED = 4;
        public const int MUSICANDSFX = 5;
        public const int LOCATORS = 6;
        public const int CYCLER = 7;
        public const int MASTERSWITCH = 8;
        public const int RESPAWN = 9;
        public const int GPSPEED = 10;
        public const int FOF = 13;
        public const int ARROW = 20;
        public const int FIRSTGUNSPRITE = 21;
        public const int CHAINGUNSPRITE = 22;
        public const int RPGSPRITE = 23;
        public const int FREEZESPRITE = 24;
        public const int SHRINKERSPRITE = 25;
        public const int HEAVYHBOMB = 26;
        public const int TRIPBOMBSPRITE = 27;
        public const int SHOTGUNSPRITE = 28;
        public const int DEVISTATORSPRITE = 29;
        public const int HEALTHBOX = 30;
        public const int AMMOBOX = 31;
        public const int GROWSPRITEICON = 32;
        public const int INVENTORYBOX = 33;
        public const int FREEZEAMMO = 37;
        public const int AMMO = 40;
        public const int BATTERYAMMO = 41;
        public const int DEVISTATORAMMO = 42;
        public const int RPGAMMO = 44;
        public const int GROWAMMO = 45;
        public const int CRYSTALAMMO = 46;
        public const int HBOMBAMMO = 47;
        public const int AMMOLOTS = 48;
        public const int SHOTGUNAMMO = 49;
        public const int COLA = 51;
        public const int SIXPAK = 52;
        public const int FIRSTAID = 53;
        public const int SHIELD = 54;
        public const int STEROIDS = 55;
        public const int AIRTANK = 56;
        public const int JETPACK = 57;
        public const int HEATSENSOR = 59;
        public const int ACCESSCARD = 60;
        public const int BOOTS = 61;
        public const int MIRRORBROKE = 70;
        public const int CLOUDYOCEAN = 78;
        public const int CLOUDYSKIES = 79;
        public const int MOONSKY1 = 80;
        public const int MOONSKY2 = 81;
        public const int MOONSKY3 = 82;
        public const int MOONSKY4 = 83;
        public const int BIGORBIT1 = 84;
        public const int BIGORBIT2 = 85;
        public const int BIGORBIT3 = 86;
        public const int BIGORBIT4 = 87;
        public const int BIGORBIT5 = 88;
        public const int LA = 89;
        public const int REDSKY1 = 98;
        public const int REDSKY2 = 99;
        public const int ATOMICHEALTH = 100;
        public const int TECHLIGHT2 = 120;
        public const int TECHLIGHTBUST2 = 121;
        public const int TECHLIGHT4 = 122;
        public const int TECHLIGHTBUST4 = 123;
        public const int WALLLIGHT4 = 124;
        public const int WALLLIGHTBUST4 = 125;
        public const int ACCESSSWITCH = 130;
        public const int SLOTDOOR = 132;
        public const int LIGHTSWITCH = 134;
        public const int SPACEDOORSWITCH = 136;
        public const int SPACELIGHTSWITCH = 138;
        public const int FRANKENSTINESWITCH = 140;
        public const int NUKEBUTTON = 142;
        public const int MULTISWITCH = 146;
        public const int DOORTILE5 = 150;
        public const int DOORTILE6 = 151;
        public const int DOORTILE1 = 152;
        public const int DOORTILE2 = 153;
        public const int DOORTILE3 = 154;
        public const int DOORTILE4 = 155;
        public const int DOORTILE7 = 156;
        public const int DOORTILE8 = 157;
        public const int DOORTILE9 = 158;
        public const int DOORTILE10 = 159;
        public const int DOORSHOCK = 160;
        public const int DIPSWITCH = 162;
        public const int DIPSWITCH2 = 164;
        public const int TECHSWITCH = 166;
        public const int DIPSWITCH3 = 168;
        public const int ACCESSSWITCH2 = 170;
        public const int REFLECTWATERTILE = 180;
        public const int FLOORSLIME = 200;
        public const int BIGFORCE = 230;
        public const int EPISODE = 247;
        public const int MASKWALL9 = 255;
        public const int W_LIGHT = 260;
        public const int SCREENBREAK1 = 263;
        public const int SCREENBREAK2 = 264;
        public const int SCREENBREAK3 = 265;
        public const int SCREENBREAK4 = 266;
        public const int SCREENBREAK5 = 267;
        public const int SCREENBREAK6 = 268;
        public const int SCREENBREAK7 = 269;
        public const int SCREENBREAK8 = 270;
        public const int SCREENBREAK9 = 271;
        public const int SCREENBREAK10 = 272;
        public const int SCREENBREAK11 = 273;
        public const int SCREENBREAK12 = 274;
        public const int SCREENBREAK13 = 275;
        public const int MASKWALL1 = 285;
        public const int W_TECHWALL1 = 293;
        public const int W_TECHWALL2 = 297;
        public const int W_TECHWALL15 = 299;
        public const int W_TECHWALL3 = 301;
        public const int W_TECHWALL4 = 305;
        public const int W_TECHWALL10 = 306;
        public const int W_TECHWALL16 = 307;
        public const int WATERTILE2 = 336;
        public const int BPANNEL1 = 341;
        public const int PANNEL1 = 342;
        public const int PANNEL2 = 343;
        public const int WATERTILE = 344;
        public const int STATIC = 351;
        public const int W_SCREENBREAK = 357;
        public const int W_HITTECHWALL3 = 360;
        public const int W_HITTECHWALL4 = 361;
        public const int W_HITTECHWALL2 = 362;
        public const int W_HITTECHWALL1 = 363;
        public const int MASKWALL10 = 387;
        public const int MASKWALL11 = 391;
        public const int DOORTILE22 = 395;
        public const int FANSPRITE = 407;
        public const int FANSPRITEBROKE = 411;
        public const int FANSHADOW = 412;
        public const int FANSHADOWBROKE = 416;
        public const int DOORTILE18 = 447;
        public const int DOORTILE19 = 448;
        public const int DOORTILE20 = 449;
                // = public const int SPACESHUTTLE = 487
        public const int SATELLITE = 489;
        public const int VIEWSCREEN2 = 499;
        public const int VIEWSCREENBROKE = 501;
        public const int VIEWSCREEN = 502;
        public const int GLASS = 503;
        public const int GLASS2 = 504;
        public const int STAINGLASS1 = 510;
        public const int MASKWALL5 = 514;
        public const int SATELITE = 516;
        public const int FUELPOD = 517;
        public const int SLIMEPIPE = 538;
        public const int CRACK1 = 546;
        public const int CRACK2 = 547;
        public const int CRACK3 = 548;
        public const int CRACK4 = 549;
        public const int FOOTPRINTS = 550;
        public const int DOMELITE = 551;
        public const int CAMERAPOLE = 554;
        public const int CHAIR1 = 556;
        public const int CHAIR2 = 557;
        public const int BROKENCHAIR = 559;
        public const int MIRROR = 560;
        public const int WATERFOUNTAIN = 563;
        public const int WATERFOUNTAINBROKE = 567;
        public const int FEMMAG1 = 568;
        public const int TOILET = 569;
        public const int STALL = 571;
        public const int STALLBROKE = 573;
        public const int FEMMAG2 = 577;
        public const int REACTOR2 = 578;
        public const int REACTOR2BURNT = 579;
        public const int REACTOR2SPARK = 580;
        public const int GRATE1 = 595;
        public const int BGRATE1 = 596;
        public const int SOLARPANNEL = 602;
        public const int NAKED1 = 603;
        public const int ANTENNA = 607;
        public const int MASKWALL12 = 609;
        public const int TOILETBROKE = 615;
        public const int PIPE2 = 616;
        public const int PIPE1B = 617;
        public const int PIPE3 = 618;
        public const int PIPE1 = 619;
        public const int CAMERA1 = 621;
        public const int BRICK = 626;
        public const int SPLINTERWOOD = 630;
        public const int PIPE2B = 633;
        public const int BOLT1 = 634;
        public const int W_NUMBERS = 640;
        public const int WATERDRIP = 660;
        public const int WATERBUBBLE = 661;
        public const int WATERBUBBLEMAKER = 662;
        public const int W_FORCEFIELD = 663;
        public const int VACUUM = 669;
        public const int FOOTPRINTS2 = 672;
        public const int FOOTPRINTS3 = 673;
        public const int FOOTPRINTS4 = 674;
        public const int EGG = 675;
        public const int SCALE = 678;
        public const int CHAIR3 = 680;
        public const int CAMERALIGHT = 685;
        public const int MOVIECAMERA = 686;
        public const int IVUNIT = 689;
        public const int POT1 = 694;
        public const int POT2 = 695;
        public const int POT3 = 697;
        public const int PIPE3B = 700;
        public const int WALLLIGHT3 = 701;
        public const int WALLLIGHTBUST3 = 702;
        public const int WALLLIGHT1 = 703;
        public const int WALLLIGHTBUST1 = 704;
        public const int WALLLIGHT2 = 705;
        public const int WALLLIGHTBUST2 = 706;
        public const int LIGHTSWITCH2 = 712;
        public const int WAITTOBESEATED = 716;
        public const int DOORTILE14 = 717;
        public const int STATUE = 753;
        public const int MIKE = 762;
        public const int VASE = 765;
        public const int SUSHIPLATE1 = 768;
        public const int SUSHIPLATE2 = 769;
        public const int SUSHIPLATE3 = 774;
        public const int SUSHIPLATE4 = 779;
        public const int DOORTILE16 = 781;
        public const int SUSHIPLATE5 = 792;
        public const int OJ = 806;
        public const int MASKWALL13 = 830;
        public const int HURTRAIL = 859;
        public const int POWERSWITCH1 = 860;
        public const int LOCKSWITCH1 = 862;
        public const int POWERSWITCH2 = 864;
        public const int ATM = 867;
        public const int STATUEFLASH = 869;
        public const int ATMBROKE = 888;
        public const int BIGHOLE2 = 893;
        public const int STRIPEBALL = 901;
        public const int QUEBALL = 902;
        public const int POCKET = 903;
        public const int WOODENHORSE = 904;
        public const int TREE1 = 908;
        public const int TREE2 = 910;
        public const int CACTUS = 911;
        public const int MASKWALL2 = 913;
        public const int MASKWALL3 = 914;
        public const int MASKWALL4 = 915;
        public const int FIREEXT = 916;
        public const int TOILETWATER = 921;
        public const int NEON1 = 925;
        public const int NEON2 = 926;
        public const int CACTUSBROKE = 939;
        public const int BOUNCEMINE = 940;
        public const int BROKEFIREHYDRENT = 950;
        public const int BOX = 951;
        public const int BULLETHOLE = 952;
        public const int BOTTLE1 = 954;
        public const int BOTTLE2 = 955;
        public const int BOTTLE3 = 956;
        public const int BOTTLE4 = 957;
        public const int FEMPIC5 = 963;
        public const int FEMPIC6 = 964;
        public const int FEMPIC7 = 965;
        public const int HYDROPLANT = 969;
        public const int OCEANSPRITE1 = 971;
        public const int OCEANSPRITE2 = 972;
        public const int OCEANSPRITE3 = 973;
        public const int OCEANSPRITE4 = 974;
        public const int OCEANSPRITE5 = 975;
        public const int GENERICPOLE = 977;
        public const int CONE = 978;
        public const int HANGLIGHT = 979;
        public const int HYDRENT = 981;
        public const int MASKWALL14 = 988;
        public const int TIRE = 990;
        public const int PIPE5 = 994;
        public const int PIPE6 = 995;
        public const int PIPE4 = 996;
        public const int PIPE4B = 997;
        public const int BROKEHYDROPLANT = 1003;
        public const int PIPE5B = 1005;
        public const int NEON3 = 1007;
        public const int NEON4 = 1008;
        public const int NEON5 = 1009;
        public const int BOTTLE5 = 1012;
        public const int BOTTLE6 = 1013;
        public const int BOTTLE8 = 1014;
        public const int SPOTLITE = 1020;
        public const int HANGOOZ = 1022;
        public const int MASKWALL15 = 1024;
        public const int BOTTLE7 = 1025;
        public const int HORSEONSIDE = 1026;
        public const int GLASSPIECES = 1031;
        public const int HORSELITE = 1034;
        public const int DONUTS = 1045;
        public const int NEON6 = 1046;
        public const int MASKWALL6 = 1059;
        public const int CLOCK = 1060;
        public const int RUBBERCAN = 1062;
        public const int BROKENCLOCK = 1067;
        public const int PLUG = 1069;
        public const int OOZFILTER = 1079;
        public const int FLOORPLASMA = 1082;
        public const int REACTOR = 1088;
        public const int REACTORSPARK = 1092;
        public const int REACTORBURNT = 1096;
        public const int DOORTILE15 = 1102;
        public const int HANDSWITCH = 1111;
        public const int CIRCLEPANNEL = 1113;
        public const int CIRCLEPANNELBROKE = 1114;
        public const int PULLSWITCH = 1122;
        public const int MASKWALL8 = 1124;
        public const int BIGHOLE = 1141;
        public const int ALIENSWITCH = 1142;
        public const int DOORTILE21 = 1144;
        public const int HANDPRINTSWITCH = 1155;
        public const int BOTTLE10 = 1157;
        public const int BOTTLE11 = 1158;
        public const int BOTTLE12 = 1159;
        public const int BOTTLE13 = 1160;
        public const int BOTTLE14 = 1161;
        public const int BOTTLE15 = 1162;
        public const int BOTTLE16 = 1163;
        public const int BOTTLE17 = 1164;
        public const int BOTTLE18 = 1165;
        public const int BOTTLE19 = 1166;
        public const int DOORTILE17 = 1169;
        public const int MASKWALL7 = 1174;
        public const int JAILBARBREAK = 1175;
        public const int DOORTILE11 = 1178;
        public const int DOORTILE12 = 1179;
        public const int VENDMACHINE = 1212;
        public const int VENDMACHINEBROKE = 1214;
        public const int COLAMACHINE = 1215;
        public const int COLAMACHINEBROKE = 1217;
        public const int CRANEPOLE = 1221;
        public const int CRANE = 1222;
        public const int BARBROKE = 1225;
        public const int BLOODPOOL = 1226;
        public const int NUKEBARREL = 1227;
        public const int NUKEBARRELDENTED = 1228;
        public const int NUKEBARRELLEAKED = 1229;
        public const int CANWITHSOMETHING = 1232;
        public const int MONEY = 1233;
        public const int BANNER = 1236;
        public const int EXPLODINGBARREL = 1238;
        public const int EXPLODINGBARREL2 = 1239;
        public const int FIREBARREL = 1240;
        public const int SEENINE = 1247;
        public const int SEENINEDEAD = 1248;
        public const int STEAM = 1250;
        public const int CEILINGSTEAM = 1255;
        public const int PIPE6B = 1260;
        public const int TRANSPORTERBEAM = 1261;
        public const int RAT = 1267;
        public const int TRASH = 1272;
        public const int FEMPIC1 = 1280;
        public const int FEMPIC2 = 1289;
        public const int BLANKSCREEN = 1293;
        public const int PODFEM1 = 1294;
        public const int FEMPIC3 = 1298;
        public const int FEMPIC4 = 1306;
        public const int FEM1 = 1312;
        public const int FEM2 = 1317;
        public const int FEM3 = 1321;
        public const int FEM5 = 1323;
        public const int BLOODYPOLE = 1324;
        public const int FEM4 = 1325;
        public const int FEM6 = 1334;
        public const int FEM6PAD = 1335;
        public const int FEM8 = 1336;
        public const int HELECOPT = 1346;
        public const int FETUSJIB = 1347;
        public const int HOLODUKE = 1348;
        public const int SPACEMARINE = 1353;
        public const int INDY = 1355;
        public const int FETUS = 1358;
        public const int FETUSBROKE = 1359;
        public const int MONK = 1352;
        public const int LUKE = 1354;
        public const int COOLEXPLOSION1 = 1360;
        public const int WATERSPLASH2 = 1380;
        public const int FIREVASE = 1390;
        public const int SCRATCH = 1393;
        public const int FEM7 = 1395;
        public const int APLAYERTOP = 1400;
        public const int APLAYER = 1405;
        public const int PLAYERONWATER = 1420;
        public const int DUKELYINGDEAD = 1518;
        public const int DUKETORSO = 1520;
        public const int DUKEGUN = 1528;
        public const int DUKELEG = 1536;
        public const int SHARK = 1550;
        public const int BLOOD = 1620;
        public const int FIRELASER = 1625;
        public const int TRANSPORTERSTAR = 1630;
        public const int SPIT = 1636;
        public const int LOOGIE = 1637;
        public const int FIST = 1640;
        public const int FREEZEBLAST = 1641;
        public const int DEVISTATORBLAST = 1642;
        public const int SHRINKSPARK = 1646;
        public const int TONGUE = 1647;
        public const int MORTER = 1650;
        public const int SHRINKEREXPLOSION = 1656;
        public const int RADIUSEXPLOSION = 1670;
        public const int FORCERIPPLE = 1671;
        public const int LIZTROOP = 1680;
        public const int LIZTROOPRUNNING = 1681;
        public const int LIZTROOPSTAYPUT = 1682;
        public const int LIZTOP = 1705;
        public const int LIZTROOPSHOOT = 1715;
        public const int LIZTROOPJETPACK = 1725;
        public const int LIZTROOPDSPRITE = 1734;
        public const int LIZTROOPONTOILET = 1741;
        public const int LIZTROOPJUSTSIT = 1742;
        public const int LIZTROOPDUCKING = 1744;
        public const int HEADJIB1 = 1768;
        public const int ARMJIB1 = 1772;
        public const int LEGJIB1 = 1776;
        public const int CANNONBALL = 1817;
        public const int OCTABRAIN = 1820;
        public const int OCTABRAINSTAYPUT = 1821;
        public const int OCTATOP = 1845;
        public const int OCTADEADSPRITE = 1855;
        public const int INNERJAW = 1860;
        public const int DRONE = 1880;
        public const int EXPLOSION2 = 1890;
        public const int COMMANDER = 1920;
        public const int COMMANDERSTAYPUT = 1921;
        public const int RECON = 1960;
        public const int TANK = 1975;
        public const int PIGCOP = 2000;
        public const int PIGCOPSTAYPUT = 2001;
        public const int PIGCOPDIVE = 2045;
        public const int PIGCOPDEADSPRITE = 2060;
        public const int PIGTOP = 2061;
        public const int LIZMAN = 2120;
        public const int LIZMANSTAYPUT = 2121;
        public const int LIZMANSPITTING = 2150;
        public const int LIZMANFEEDING = 2160;
        public const int LIZMANJUMP = 2165;
        public const int LIZMANDEADSPRITE = 2185;
        public const int FECES = 2200;
        public const int LIZMANHEAD1 = 2201;
        public const int LIZMANARM1 = 2205;
        public const int LIZMANLEG1 = 2209;
        public const int EXPLOSION2BOT = 2219;
        public const int USERWEAPON = 2235;
        public const int HEADERBAR = 2242;
        public const int JIBS1 = 2245;
        public const int JIBS2 = 2250;
        public const int JIBS3 = 2255;
        public const int JIBS4 = 2260;
        public const int JIBS5 = 2265;
        public const int BURNING = 2270;
        public const int FIRE = 2271;
        public const int JIBS6 = 2286;
        public const int BLOODSPLAT1 = 2296;
        public const int BLOODSPLAT3 = 2297;
        public const int BLOODSPLAT2 = 2298;
        public const int BLOODSPLAT4 = 2299;
        public const int OOZ = 2300;
        public const int OOZ2 = 2309;
        public const int WALLBLOOD1 = 2301;
        public const int WALLBLOOD2 = 2302;
        public const int WALLBLOOD3 = 2303;
        public const int WALLBLOOD4 = 2304;
        public const int WALLBLOOD5 = 2305;
        public const int WALLBLOOD6 = 2306;
        public const int WALLBLOOD7 = 2307;
        public const int WALLBLOOD8 = 2308;
        public const int BURNING2 = 2310;
        public const int FIRE2 = 2311;
        public const int CRACKKNUCKLES = 2324;
        public const int SMALLSMOKE = 2329;
        public const int SMALLSMOKEMAKER = 2330;
        public const int FLOORFLAME = 2333;
        public const int ROTATEGUN = 2360;
        public const int GREENSLIME = 2370;
        public const int WATERDRIPSPLASH = 2380;
        public const int SCRAP6 = 2390;
        public const int SCRAP1 = 2400;
        public const int SCRAP2 = 2404;
        public const int SCRAP3 = 2408;
        public const int SCRAP4 = 2412;
        public const int SCRAP5 = 2416;
        public const int ORGANTIC = 2420;
        public const int BETAVERSION = 2440;
        public const int PLAYERISHERE = 2442;
        public const int PLAYERWASHERE = 2443;
        public const int SELECTDIR = 2444;
        public const int F1HELP = 2445;
        public const int NOTCHON = 2446;
        public const int NOTCHOFF = 2447;
        public const int GROWSPARK = 2448;
        public const int DUKEICON = 2452;
        public const int BADGUYICON = 2453;
        public const int FOODICON = 2454;
        public const int GETICON = 2455;
        public const int MENUSCREEN = 2456;
        public const int MENUBAR = 2457;
        public const int KILLSICON = 2458;
        public const int FIRSTAID_ICON = 2460;
        public const int HEAT_ICON = 2461;
        public const int BOTTOMSTATUSBAR = 2462;
        public const int BOOT_ICON = 2463;
        public const int FRAGBAR = 2465;
        public const int JETPACK_ICON = 2467;
        public const int AIRTANK_ICON = 2468;
        public const int STEROIDS_ICON = 2469;
        public const int HOLODUKE_ICON = 2470;
        public const int ACCESS_ICON = 2471;
        public const int DIGITALNUM = 2472;
        public const int DUKECAR = 2491;
        public const int CAMCORNER = 2482;
        public const int CAMLIGHT = 2484;
        public const int LOGO = 2485;
        public const int TITLE = 2486;
        public const int NUKEWARNINGICON = 2487;
        public const int MOUSECURSOR = 2488;
        public const int SLIDEBAR = 2489;
        public const int DREALMS = 2492;
        public const int BETASCREEN = 2493;
        public const int WINDOWBORDER1 = 2494;
        public const int TEXTBOX = 2495;
        public const int WINDOWBORDER2 = 2496;
        public const int DUKENUKEM = 2497;
        public const int THREEDEE = 2498;
        public const int INGAMEDUKETHREEDEE = 2499;
        public const int TENSCREEN = 2500;
        public const int PLUTOPAKSPRITE = 2501;
        public const int DEVISTATOR = 2510;
        public const int KNEE = 2521;
        public const int CROSSHAIR = 2523;
        public const int FIRSTGUN = 2524;
        public const int FIRSTGUNRELOAD = 2528;
        public const int FALLINGCLIP = 2530;
        public const int CLIPINHAND = 2531;
        public const int HAND = 2532;
        public const int SHELL = 2533;
        public const int SHOTGUNSHELL = 2535;
        public const int CHAINGUN = 2536;
        public const int RPGGUN = 2544;
        public const int RPGMUZZLEFLASH = 2545;
        public const int FREEZE = 2548;
        public const int CATLITE = 2552;
        public const int SHRINKER = 2556;
        public const int HANDHOLDINGLASER = 2563;
        public const int TRIPBOMB = 2566;
        public const int LASERLINE = 2567;
        public const int HANDHOLDINGACCESS = 2568;
        public const int HANDREMOTE = 2570;
        public const int HANDTHROW = 2573;
        public const int TIP = 2576;
        public const int GLAIR = 2578;
        public const int SCUBAMASK = 2581;
        public const int SPACEMASK = 2584;
        public const int FORCESPHERE = 2590;
        public const int SHOTSPARK1 = 2595;
        public const int RPG = 2605;
        public const int LASERSITE = 2612;
        public const int SHOTGUN = 2613;
        public const int BOSS1 = 2630;
        public const int BOSS1STAYPUT = 2631;
        public const int BOSS1SHOOT = 2660;
        public const int BOSS1LOB = 2670;
        public const int BOSSTOP = 2696;
        public const int BOSS2 = 2710;
        public const int BOSS3 = 2760;
        public const int SPINNINGNUKEICON = 2813;
        public const int BIGFNTCURSOR = 2820;
        public const int SMALLFNTCURSOR = 2821;
        public const int STARTALPHANUM = 2822;
        public const int ENDALPHANUM = 2915;
        public const int BIGALPHANUM = 2940;
        public const int BIGPERIOD = 3002;
        public const int BIGCOMMA = 3003;
        public const int BIGX = 3004;
        public const int BIGQ = 3005;
        public const int BIGSEMI = 3006;
        public const int BIGCOLIN = 3007;
        public const int THREEBYFIVE = 3010;
        public const int BIGAPPOS = 3022;
        public const int BLANK = 3026;
        public const int MINIFONT = 3072;
        public const int BUTTON1 = 3164;
        public const int GLASS3 = 3187;
        public const int RESPAWNMARKERRED = 3190;
        public const int RESPAWNMARKERYELLOW = 3200;
        public const int RESPAWNMARKERGREEN = 3210;
        public const int BONUSSCREEN = 3240;
        public const int VIEWBORDER = 3250;
        public const int VICTORY1 = 3260;
        public const int ORDERING = 3270;
        public const int TEXTSTORY = 3280;
        public const int LOADSCREEN = 3281;
        public const int BORNTOBEWILDSCREEN = 3370;
        public const int BLIMP = 3400;
        public const int FEM9 = 3450;
        public const int FOOTPRINT = 3701;
        public const int POOP = 4094;
        public const int FRAMEEFFECT1 = 4095;
        public const int PANNEL3 = 4099;
        public const int SCREENBREAK14 = 4120;
        public const int SCREENBREAK15 = 4123;
        public const int SCREENBREAK19 = 4125;
        public const int SCREENBREAK16 = 4127;
        public const int SCREENBREAK17 = 4128;
        public const int SCREENBREAK18 = 4129;
        public const int W_TECHWALL11 = 4130;
        public const int W_TECHWALL12 = 4131;
        public const int W_TECHWALL13 = 4132;
        public const int W_TECHWALL14 = 4133;
        public const int W_TECHWALL5 = 4134;
        public const int W_TECHWALL6 = 4136;
        public const int W_TECHWALL7 = 4138;
        public const int W_TECHWALL8 = 4140;
        public const int W_TECHWALL9 = 4142;
        public const int BPANNEL3 = 4100;
        public const int W_HITTECHWALL16 = 4144;
        public const int W_HITTECHWALL10 = 4145;
        public const int W_HITTECHWALL15 = 4147;
        public const int W_MILKSHELF = 4181;
        public const int W_MILKSHELFBROKE = 4203;
        public const int PURPLELAVA = 4240;
        public const int LAVABUBBLE = 4340;
        public const int DUKECUTOUT = 4352;
        public const int TARGET = 4359;
        public const int GUNPOWDERBARREL = 4360;
        public const int DUCK = 4361;
        public const int HATRACK = 4367;
        public const int DESKLAMP = 4370;
        public const int COFFEEMACHINE = 4372;
        public const int CUPS = 4373;
        public const int GAVALS = 4374;
        public const int GAVALS2 = 4375;
        public const int POLICELIGHTPOLE = 4377;
        public const int FLOORBASKET = 4388;
        public const int PUKE = 4389;
        public const int DOORTILE23 = 4391;
        public const int TOPSECRET = 4396;
        public const int SPEAKER = 4397;
        public const int TEDDYBEAR = 4400;
        public const int ROBOTDOG = 4402;
        public const int ROBOTPIRATE = 4404;
        public const int ROBOTMOUSE = 4407;
        public const int MAIL = 4410;
        public const int MAILBAG = 4413;
        public const int HOTMEAT = 4427;
        public const int COFFEEMUG = 4438;
        public const int DONUTS2 = 4440;
        public const int TRIPODCAMERA = 4444;
        public const int METER = 4453;
        public const int DESKPHONE = 4454;
        public const int GUMBALLMACHINE = 4458;
        public const int GUMBALLMACHINEBROKE = 4459;
        public const int PAPER = 4460;
        public const int MACE = 4464;
        public const int GENERICPOLE2 = 4465;
        public const int XXXSTACY = 4470;
        public const int WETFLOOR = 4495;
        public const int BROOM = 4496;
        public const int MOP = 4497;
        public const int LETTER = 4502;
        public const int PIRATE1A = 4510;
        public const int PIRATE4A = 4511;
        public const int PIRATE2A = 4512;
        public const int PIRATE5A = 4513;
        public const int PIRATE3A = 4514;
        public const int PIRATE6A = 4515;
        public const int PIRATEHALF = 4516;
        public const int CHESTOFGOLD = 4520;
        public const int SIDEBOLT1 = 4525;
        public const int FOODOBJECT1 = 4530;
        public const int FOODOBJECT2 = 4531;
        public const int FOODOBJECT3 = 4532;
        public const int FOODOBJECT4 = 4533;
        public const int FOODOBJECT5 = 4534;
        public const int FOODOBJECT6 = 4535;
        public const int FOODOBJECT7 = 4536;
        public const int FOODOBJECT8 = 4537;
        public const int FOODOBJECT9 = 4538;
        public const int FOODOBJECT10 = 4539;
        public const int FOODOBJECT11 = 4540;
        public const int FOODOBJECT12 = 4541;
        public const int FOODOBJECT13 = 4542;
        public const int FOODOBJECT14 = 4543;
        public const int FOODOBJECT15 = 4544;
        public const int FOODOBJECT16 = 4545;
        public const int FOODOBJECT17 = 4546;
        public const int FOODOBJECT18 = 4547;
        public const int FOODOBJECT19 = 4548;
        public const int FOODOBJECT20 = 4549;
        public const int HEADLAMP = 4550;
        public const int TAMPON = 4557;
        public const int SKINNEDCHICKEN = 4554;
        public const int FEATHEREDCHICKEN = 4555;
        public const int ROBOTDOG2 = 4560;
        public const int JOLLYMEAL = 4569;
        public const int DUKEBURGER = 4570;
        public const int SHOPPINGCART = 4576;
        public const int CANWITHSOMETHING2 = 4580;
        public const int CANWITHSOMETHING3 = 4581;
        public const int CANWITHSOMETHING4 = 4582;
        public const int SNAKEP = 4590;
        public const int DOLPHIN1 = 4591;
        public const int DOLPHIN2 = 4592;
        public const int NEWBEAST = 4610;
        public const int NEWBEASTSTAYPUT = 4611;
        public const int NEWBEASTJUMP = 4690;
        public const int NEWBEASTHANG = 4670;
        public const int NEWBEASTHANGDEAD = 4671;
        public const int BOSS4 = 4740;
        public const int BOSS4STAYPUT = 4741;
        public const int FEM10 = 4864;
        public const int TOUGHGAL = 4866;
        public const int MAN = 4871;
        public const int MAN2 = 4872;
        public const int WOMAN = 4874;
        public const int PLEASEWAIT = 4887;
        public const int NATURALLIGHTNING = 4890;
        public const int WEATHERWARN = 4893;
        public const int DUKETAG = 4900;
        public const int SIGN1 = 4909;
        public const int SIGN2 = 4912;
        public const int JURYGUY = 4943;
    }
}