using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActorApi.DAL;
using ActorApi.Models;
using ActorApi.DTOs;

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
        [HttpGet("{name}")]
        public ActionResult GetActor(string name)
        {
            var actor =  _context.Actors.Where(actor=>actor.Name.ToLower().Contains(name.ToLower()));

            if (actor == null)
                return NotFound();
            
            return Ok(actor);
        }

        // PUT: api/Actors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, UpdateActorDto dtoactor)
        {
            Actor dbactor=await _context.Actors.FindAsync(id);
            if(dbactor == null) return BadRequest();

            dbactor.Name= dtoactor.Name==""? dbactor.Name.Trim():dtoactor.Name.Trim();
            dbactor.Url=dtoactor.Url==""?dbactor.Url:dtoactor.Url;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Actors
        [HttpPost]
        public async Task<ActionResult> PostActor(CreateActorDto dtoactor)
        {
            if(!ModelState.IsValid) return BadRequest();
            Actor actor=new Actor()
            {
                Name= dtoactor.Name.Trim(),
                Url=dtoactor.Url
            };
            
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return Ok();
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

    }
}
