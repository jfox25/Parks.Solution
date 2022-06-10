using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Park.Models;
using Park.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Security.Claims;

namespace Park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private readonly ParkContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NationalParksController(ParkContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/NationalParks
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalPark>>> GetNationalParks()
        {
            return await _context.NationalParks.ToListAsync();
        }

        // GET: api/Threads/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<ThreadDTO>> GetThread(int id)
        // {
        //     var thread = await _context.Threads
        //         .Where(x => x.ThreadId == id)
        //         .ProjectTo<ThreadDTO>(_mapper.ConfigurationProvider)
        //         .SingleOrDefaultAsync();

        //     if (thread == null)
        //     {
        //         return NotFound();
        //     }

        //     return thread;
        // }

        // // PUT: api/Threads/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutThread(int id, ThreadDTO thread, string userId)
        // {
        //     var thisThread = await _context.Threads.FindAsync(id);
        //     if(thisThread == null) return NotFound();
        //     if(thread.UserId != userId)
        //     {
        //         return BadRequest();
        //     }
        //     if (id != thread.ThreadId)
        //     {
        //         return BadRequest();
        //     }
        //     _mapper.Map(thread, thisThread);

        //     _context.Threads.Update(thisThread);
        //     // _context.Entry(model).State = EntityState.Modified;
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ThreadExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return Ok();
        // }

        // POST: api/Threads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<NationalPark>> PostNationalPark(AddNationalParkDTO model)
        {
          Console.WriteLine("Name :" + User.Identity.Name);
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
    
            NationalPark nationalPark = new NationalPark() {
                Name = model.Name,
                TotalNumberofVisitors = model.TotalNumberofVisitors,
                StateName = model.StateName,
                UserId = currentUser.Id,
            };
            _context.NationalParks.Add(nationalPark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNationalPark", new { id = nationalPark.NationalParkId });
        }

        // // DELETE: api/Threads/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteThread(int id, string userId)
        // {
        //     var thread = await _context.Threads.FindAsync(id);
        //     if (thread == null)
        //     {
        //         return NotFound();
        //     }
        //     if (userId == thread.User.Id)
        //     {
        //         _context.Threads.Remove(thread);
        //         await _context.SaveChangesAsync();
        //         return NoContent();
        //     }

        //     return BadRequest();
        // }

        // private bool ThreadExists(int id)
        // {
        //     return _context.Threads.Any(e => e.ThreadId == id);
        // }
    }
}
