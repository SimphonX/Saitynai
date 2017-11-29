using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RateAPI.Model;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RateMeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly RateMeContext _context;
        public FavoritesController(RateMeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Followings> Get()
        {
            return _context.Followings.ToList();
        }

        [HttpGet("{id}", Name = "GetFavorites")]
        public IActionResult GetById(long id)
        {
            var item = _context.Followings.FirstOrDefault(t => t.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("GetFavorites")]
        public IActionResult GetRate(long id)
        {
            var item = _context.Followings.ToList().Where(t => t.Follower == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Followings value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _context.Followings.Add(value);
            _context.SaveChanges();
            return CreatedAtRoute("GetFavorite", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Followings item)
        {
            
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            _context.Followings.Update(item);
            _context.SaveChanges();
            return new NoContentResult();
        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comments = _context.Followings.FirstOrDefault(t => t.Id == id);

            if (comments == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(comments);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
