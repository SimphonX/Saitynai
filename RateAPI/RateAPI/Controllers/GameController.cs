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
        private readonly string Urls = "https://api-2445582011268.apicast.io";
        private string key = "42880a8f8ab7e2ba73141e0522b3dd87";
        private readonly RateMeContext _context;
        private int x_count = 0;
        private string next_page ="";
        private Boolean isList = false;
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
                    try
                    {
                        x_count = Int32.Parse(response.Headers.GetValues("x-count").ToArray()[0]);
                        next_page = response.Headers.GetValues("x-next-page").ToArray()[0];
                    }
                    catch (Exception e) { }


                    using (HttpContent content = response.Content)
                    {
                        return content.ReadAsStringAsync().Result;
                    }
                }
            }
        }
        
        private IGDBGame[] IGDBCallId(int id, string type)
        {
            string fields = "";
            if(type == "list")
            {
                fields += "?fields=id,name,summary,rating,total_rating,developers,publishers,category,cover,pegi,genres.name&expand=developers,publishers,platforms,genres";
            }
            else {
                fields += "?fields=id,name,summary,storyline,rating,total_rating,games,developers,publishers,game_engines,category,release_dates,screenshots,cover,pegi,player_perspectives.name,game_modes.name,keywords.name,themes.name,genres.name&expand=developers,publishers,game_engines,platforms,player_perspectives,game_modes,keywords,themes,genres";
            }
            return JsonConvert.DeserializeObject<IGDBGame[]>(Json(Urls + "/games/" + id.ToString()+fields));
        }
        private GamesID[] IGDBCallName(string name)
        {
            GamesID[] list = JsonConvert.DeserializeObject<GamesID[]>(Json(Urls + "/games/?search=" + name+ "&order=rating:desc,release_dates.date:desc&limit=20&scroll=1"));
            /*List<IGDBGame> games = new List<IGDBGame>();
            foreach(GamesID sk in list)
            {
                games.Add(IGDBCallId(sk.Id)[0]);
            }*/
            return list.ToArray();
        }

        [HttpGet]
        public IEnumerable<GamesID> Get()
        {
            GamesID[] data = IGDBCallName("");
            Request.HttpContext.Response.Headers.Add("size", x_count.ToString());
            Request.HttpContext.Response.Headers.Add("nextpage", next_page);
            Request.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "size, nextpage");
            return data;
            //return games.ToArray();
        }

        [HttpGet("GetByName")]
        public IActionResult GetName(string name)
        {
            GamesID[] IGDBGame = IGDBCallName(name);

            if (IGDBGame == null)
            {
                return NotFound();
            }

            return new ObjectResult(IGDBGame);
        }
        [HttpGet("NextPage")]
        public IActionResult NextPage()
        {
            GamesID[] IGDBGame = JsonConvert.DeserializeObject<GamesID[]>(Json(Urls + Request.Headers["key"]));

            if (IGDBGame == null)
            {
                return NotFound();
            }

            return new ObjectResult(IGDBGame);
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetByid(int id)
        {
            IGDBGame[] IGDBGame = IGDBCallId(id, Request.Headers["type"]);
            
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
