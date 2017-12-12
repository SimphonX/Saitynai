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
        public string Storyline { get; set; } = "";
        public double Rating { get; set; } = 0;
        public double Total_rating { get; set; } = 0;
        private int Category { set; get; }
        public string Cate { get => Categorys[Category]; }
        /*public int[] Game_modes { get; set; } = { };
        private int[] Keywords { get; set; } = { };
        private int[] Themes { get; set; } = { };
        public int[] Genres { get; set; } = { };
        public int[] Developers { get; set; } = { };
        public int[] Publishers { get; set; } = { };*/
        public double Popularity { get; set; } = 0;
        public Platform[] platforms { get; set; }
        public Release_dates[] Release_dates { get; set; } = { };
        public Game_Mode[] Game_modes { get; set; } = { };
        public Keywords[] Keywords { get; set; } = { };
        public Themes[] Themes { get; set; } = { };
        public Genres[] Genres { get; set; } = { };
        public Developers[] Developers { get; set; } = { };
        public Publishers[] Publishers { get; set; } = { };
        public GameEngine[] Game_engines { get; set; } = { };
        public Perspective[] Player_perspectives { get; set; } = { };
        public int[] Games { get; set; } = { };
        public Screenshots[] Screenshots { get; set; }
        public Cover Cover { get; set; } = new Cover();
        public Pegi Pegi { get; set; } = new Pegi();
    }
    public class Release_dates
    {
        private readonly string[] reg = { "","Europe (EU)", "North America (NA)", "Australia (AU)", "New Zealand (NZ)", "Japan (JP)", "China (CH)", "Asia (AS)", "Worldwide" };
        public int Platform { get; set; } = 0;
        public int Region { get; set; } = 0;
        public string Human { get; set; } = "";
        public string Regions { get => reg[Region];}
    }
    public class Game_Mode{ public string name { get; set; } }
    public class Keywords { public string name { get; set; } }
    public class Themes { public string name { get; set; } }
    public class Genres { public string name { get; set; } }
    public class Developers { public string name { get; set; } }
    public class Publishers { public string name { get; set; } }
    public class Platform { public string name { get; set; } }
    public class GameEngine { public string name { get; set; } }
    public class Perspective { public string name { get; set; } }
    public class Screenshots { public string url { get; set; } }
    public class Cover { public string url { get; set; } }
    public class Pegi { public string synopsis { get; set; } public int rating { get; set; } }
}
