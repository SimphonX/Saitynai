using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RateAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?Linkid=397860

namespace RateAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly RateMeContext _context;
        public CommentController (RateMeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Comments> Get()
        {
            return _context.Comments.ToList();
        }

        [HttpGet("{id}", Name = "GetComments")]
        public IActionResult GetByid(int id)
        {
            var item = _context.Comments.FirstOrDefault(t => t.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet("GetRate")]
        public IActionResult GetRate(int id)
        {
            double sk = 0;
            var item = _context.Comments.ToList().Where(t=>t.Game == id);
            if (item == null)
            {
                return NotFound();
            }
            foreach (Comments comm in item)
                sk += comm.Rate;
            return new ObjectResult(sk/item.Count());
        }
        [HttpGet("GetUserName")]
        public IActionResult GetUser(int id)
        {
            Users item = _context.Users.First<Users>(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item.Username);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Comments value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            _context.Comments.Add(value);
            _context.SaveChanges();
            return CreatedAtRoute("GetComment", new { id = value.Id }, value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Comments item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var comments = _context.Comments.FirstOrDefault(t => t.Id == id);
            if (comments == null)
            {
                return NotFound();
            }
            

            _context.Comments.Update(item);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPut("inc")]
        public IActionResult Inc(string id)
        {
            var comments = _context.Comments.FirstOrDefault(t => t.Id == int.Parse(id));
            if (comments == null)
            {
                return NotFound();
            }

            comments.Score++;

            _context.Comments.Update(comments);
            _context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPut("dec")]
        public IActionResult dec(string id)
        {
            var comments = _context.Comments.FirstOrDefault(t => t.Id == int.Parse(id));
            if (comments == null)
            {
                return NotFound();
            }

            comments.Score--;

            _context.Comments.Update(comments);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var comments = _context.Comments.FirstOrDefault(t => t.Id == id);

            if (comments == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comments);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
