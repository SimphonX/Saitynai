using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RateAPI.Model;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?Linkid=397860

namespace RateMeAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class GameController : Controller
    {
        private readonly string Urls = "https://api-2445582011268.apicast.io/";
        private string key = "90237340f896bd6d566b94ac30ee30aa";
        private readonly RateMeContext _context;
        public GameController(RateMeContext context)
        {
            _context = context;
            
        }
        private string Json(string URL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("user-key", key);
                using (HttpResponseMessage response = client.GetAsync(URL).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        return content.ReadAsStringAsync().Result;
                    }
                }
            }
        }
        private IGDBGame[] Builder(string url)
        {
            IGDBGame[] games = JsonConvert.DeserializeObject<IGDBGame[]>(Json(url));
            string str;
            foreach (IGDBGame game in games)
            {
                str = Urls + "game_modes/";
                foreach (int sk in game.Game_modes)
                    str += sk.ToString()+',';
                Game_Mode[] mode = JsonConvert.DeserializeObject<Game_Mode[]>(Json(str.Substring(0, str.Length - 1)));
                game.GameModes = mode;
                /*str = Urls + "keywords/";
                foreach (int sk in game.Keywords)
                    str += sk.ToString()+',';
                Keywords[] key = JsonConvert.DeserializeObject<Keywords[]>(Json(str.Substring(0, str.Length-1)));
                game.Keyword = key;
                str = Urls + "themes/";
                foreach (int sk in game.Themes)
                    str += sk.ToString() + ',';
                Themes[] theme = JsonConvert.DeserializeObject<Themes[]>(Json(str.Substring(0, str.Length - 1)));
                game.Theme = theme;
                */str = Urls + "genres/";
                foreach (int sk in game.Genres)
                    str += sk.ToString() + ',';
                Genres[] genre = JsonConvert.DeserializeObject<Genres[]>(Json(str.Substring(0, str.Length - 1)));
                game.Genre = genre;
                str = Urls + "companies/";
                foreach (int sk in game.Developers)
                    str += sk.ToString() + ',';
                Developers[] dev = JsonConvert.DeserializeObject<Developers[]>(Json(str.Substring(0, str.Length - 1)));
                game.Developer = dev;
                str = Urls + "companies/";
                foreach (int sk in game.Publishers)
                    str += sk.ToString() + ',';
                Publishers[] pub = JsonConvert.DeserializeObject<Publishers[]>(Json(str.Substring(0, str.Length - 1)));
                game.Publisher = pub;
                foreach(Release_dates data in game.Release_dates)
                {
                    str = Urls + "platforms/" + data.Platform;
                    Platform[] plat = JsonConvert.DeserializeObject<Platform[]>(Json(str));
                    data.Platforms = plat[0].name;
                }
            }
            return games;
        }
        private IGDBGame[] IGDBCallId(int id)
        {
            return Builder(Urls + "games/" + id.ToString());
            
        }
        private IGDBGame[] IGDBCallName(string name)
        {

            GamesID[] list = JsonConvert.DeserializeObject<GamesID[]>(Json(Urls + "games/?search=" + name));
            List<IGDBGame> games = new List<IGDBGame>();
            foreach(GamesID sk in list)
            {
                games.Add(IGDBCallId(sk.Id)[0]);
            }
            return games.ToArray();
        }

        [HttpGet]
        public IEnumerable<IGDBGame> Get()
        {
            Games[] game = _context.Games.ToArray();
            DateTime baseDate = new DateTime(1970, 1, 1);
            TimeSpan diff = DateTime.Now - baseDate;
            Console.WriteLine(Math.Floor(diff.TotalMilliseconds));
            List<IGDBGame> games = IGDBCallName("&order=date:asc").ToList<IGDBGame>();
            foreach (Games g in game)
                games.Add(IGDBCallId(g.Id)[0]);
            return games;
        }

        [HttpGet("GetByName")]
        public IActionResult GetName(string name)
        {
            IGDBGame[] IGDBGame = IGDBCallName(name);

            if (IGDBGame == null)
            {
                return NotFound();
            }

            return new ObjectResult(IGDBGame);
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetByid(int id)
        {
            IGDBGame[] IGDBGame = IGDBCallId(id);
            
            if(IGDBGame == null)
            {
                return NotFound();
            }

            return new ObjectResult(IGDBGame);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Games value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _context.Games.Add(value);
            _context.SaveChanges();
            return CreatedAtRoute("GetGame", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Games item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var comments = _context.Games.FirstOrDefault(t => t.Id == id);
            if (comments == null)
            {
                return NotFound();
            }
            comments.Id = item.Id;
            _context.Games.Update(comments);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comments = _context.Games.FirstOrDefault(t => t.Id == id);

            if (comments == null)
            {
                return NotFound();
            }

            _context.Games.Remove(comments);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
