using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RateAPI.Model
{
    public class IGDBGame
    {
        private readonly string[] Categorys = { "Main game", "DLC / Addon", "Expansion", "Bundle", "Standalone expansion" };
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; } = "";
        public double Rating { get; set; } = 0;
        public double Aggregated_rating { get; set; } = 0;
        public double Total_rating { get; set; } = 0;
        public int Category { get; set; } = 0;
        public string Cate { get => Categorys[Category]; }
        public int[] Game_modes { get; set; } = { };
        //private int[] Keywords { get; set; } = { };
        //private int[] Themes { get; set; } = { };
        public int[] Genres { get; set; } = { };
        public int[] Developers { get; set; } = { };
        public int[] Publishers { get; set; } = { };
        //public double Popularity { get; set; } = 0;
        public Release_dates[] Release_dates { get; set; } = { };
        public Game_Mode[] GameModes { get; set; } = { };
        //public Keywords[] Keyword { get; set; } = { };
        //public Themes[] Theme { get; set; } = { };
        public Genres[] Genre { get; set; } = { };
        public Developers[] Developer { get; set; } = { };
        public Publishers[] Publisher { get; set; } = { };
    }
    public class Release_dates
    {
        private readonly string[] reg = { "","Europe (EU)", "North America (NA)", "Australia (AU)", "New Zealand (NZ)", "Japan (JP)", "China (CH)", "Asia (AS)", "Worldwide" };
        public int Platform { get; set; } = 0;
        public int Region { get; set; } = 0;
        public string Human { get; set; } = "";
        public string Platforms { get; set; } = "";
        public string Regions { get => reg[Region];}
    }
    public class Game_Mode{ public string name { get; set; } }
    public class Keywords { public string name { get; set; } }
    public class Themes { public string name { get; set; } }
    public class Genres { public string name { get; set; } }
    public class Developers { public string name { get; set; } }
    public class Publishers { public string name { get; set; } }
    public class Platform { public string name { get; set; } }
}
