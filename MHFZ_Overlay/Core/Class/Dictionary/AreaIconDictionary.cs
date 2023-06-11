﻿// © 2023 The mhfz-overlay Authors.
// Use of this source code is governed by a MIT license that can be
// found in the LICENSE file.
using System.Collections.Generic;

namespace MHFZ_Overlay.Core.Class.Dictionary;

///<summary>
///The area icon list
///</summary>
public static class AreaIconDictionary
{
    public static IReadOnlyDictionary<List<int>, string> AreaIconID { get; } = new Dictionary<List<int>, string>
    {
        // Loading
        {new List<int>{0 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/entrance.png"},
        //Jungle areas
        {new List<int>{1,2,3,4,5,18,19,22,23,26,110,111,112,113,114,115,116,117,118,119,120,212,213 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/jungle.png"},
        //Snowy mountain areas
        {new List<int>{6,15,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,218,219 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/snowy_mountains.png"},
        //Desert areas
        {new List<int>{7,24,45,47,48,49,50,51,52,53,54,55,56,140,141,142,143,144,145,146,147,148,149,150,214,215 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/desert.png"},
        //Volcano areas
        {new List<int>{8,27,58,59,60,61,62,63,64,65,74,161,162,163,164,165,166,167,169,216,217,220,221,222,223 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/volcano.png"},
        //Swamp areas
        {new List<int>{9,16,29,44,67,68,69,70,71,72,73,75,151,152,153,154,155,156,157,158,159,160 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/swamp.png"},
        //Forest and Hills areas
        {new List<int>{21,32,33,34,35,36,37,38,39,40,41,42,43,184,185,186,187,188,189,190,191,192,193,194,195,196 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/forest_and_hills.png"},
        //Great Forest areas
        {new List<int>{224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/great_forest.png"},
        //Highlands areas
        {new List<int>{247,248,249,250,251,252,253,254,255,302,303,304,305,306,307,308 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/highlands.png"}, 
        //Tidal Island areas
        {new List<int>{322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/tidal_island.png"},
        //Polar Sea areas
        {new List<int>{345,346,347,348,349,350,351,352,353,354,355,356,357,358 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/polar_sea.png"},
        //Flower Field areas
        {new List<int>{361,362,363,364,365,366,367,368,369,370,371,372 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/flower_fields.png"},
        // Sky Corridor / Tower
        {new List<int>{390,391,392,393,394,415,416 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/tower.png"},
        // Duremudira Areas
        {new List<int>{399,414 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/dure.gif"},
        // White Lake
        {new List<int>{400,401,402,403,404,405,406,407,408,409,410,411,412,413 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/white_lake.png"},
        //Painted Falls areas
        {new List<int>{423,424,425,426,427,428,429,430,431,432,433,434,435,436 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/painted_falls.png"},
        // road
        {new List<int>{459 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/rengoku.png"},
        //Gorge areas
        {new List<int>{288,289,290,291,292,293,294,295,296,297,298,299,300,301 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/gorge.png"},
        //Mezeporta
        {new List<int>{200,397 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/cattleya.png"},
        //my houses
        {new List<int>{173,174,175 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/my_house.png"},
        //hairdresser
        {new List<int>{201 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/tent.png"},
        //guild halls
        {new List<int>{202,203,204 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/guild_hall.png"},
        // my tore / poogie farm
        {new List<int>{205 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/my_tore.png"},
        // bars
        {new List<int>{210,211 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/rasta_bar.png"},
        // caravan / pallone
        {new List<int>{256,260,261,262,263 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/pallone_caravan.png"},
        // blacksmith
        {new List<int>{257 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/blacksmith.png"},
        // gallery
        {new List<int>{264 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/my_gallery.png"},
        // guuku farm/garden
        {new List<int>{265 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/my_garden.png"},
        // halk area
        {new List<int>{283 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/my_support.png"},
        // PvP room
        {new List<int>{286 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/pvp.png"},
        // sr rooms
        {new List<int>{340,341 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/my_missions.png"},
        // diva halls/fountain
        {new List<int>{379,445 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/diva_fountain.png"},
        // MezFes areas
        {new List<int>{462,463,464,465,466,467,468,469 }, "https://raw.githubusercontent.com/DorielRivalet/mhfz-overlay/main/img/icon/mezfes.png"},
    };
}