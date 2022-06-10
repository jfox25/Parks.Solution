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
using System.Security.Claims;

namespace Park.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateParksController : ControllerBase
    {
        private readonly ParkContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StateParksController(ParkContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/StateParks
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatePark>>> GetStateParks()
        {
            return await _context.StateParks.ToListAsync();
        }

        // GET: api/stateparks/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<StatePark>> GetStatePark(int id)
        {
            var statePark = await _context.StateParks.FirstOrDefaultAsync(park => park.StateParkId == id);
                
            if (statePark == null)
            {
                return NotFound();
            }

            return statePark;
        }

        // PUT: api/stateparks/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatePark(int id, StatePark statePark)
        {
            string name = User.Claims.First().Value;
            var currentUser = await _userManager.FindByNameAsync(name);
            if(statePark.UserId != currentUser.Id)
            {
                return BadRequest();
            }
            if (id != statePark.StateParkId)
            {
                return BadRequest();
            }

            _context.Entry(statePark).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateParkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/stateparks
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<StatePark>> PostStatePark(AddStateParkDTO model)
        {
          string name = User.Claims.First().Value;
          var currentUser = await _userManager.FindByNameAsync(name);
  
          StatePark statePark = new StatePark() {
              Name = model.Name,
              TotalNumberofVisitors = model.TotalNumberofVisitors,
              StateName = model.StateName,
              UserId = currentUser.Id,
          };
          _context.StateParks.Add(statePark);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetStateParks", new { id = statePark.StateParkId });
        }

        // DELETE: api/stateparks/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatePark(int id)
        {
            var statePark = await _context.StateParks.FirstOrDefaultAsync(park => park.StateParkId == id);
            if(statePark == null)
            {
                return NotFound();
            }
           
            _context.StateParks.Remove(statePark);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool StateParkExists(int id)
        {
            return _context.StateParks.Any(park => park.StateParkId == id);
        }
    }
}
