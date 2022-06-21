using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActorApi.DAL;
using ActorApi.Models;

namespace ActorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult> GetActors()
        {
            return Ok(await _context.Actors.ToListAsync()) ;
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
                return NotFound();
            
            return Ok(actor);
        }

        // PUT: api/Actors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            Actor dbactor=await _context.Actors.FindAsync(id);
            if(dbactor == null) return BadRequest();

            dbactor.Name= actor.Name.Trim();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Actors
        [HttpPost]
        public async Task<ActionResult> PostActor(Actor actor)
        {
            if (actor == null) return BadRequest();
            actor.Name= actor.Name.Trim(); 
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return NotFound();

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
