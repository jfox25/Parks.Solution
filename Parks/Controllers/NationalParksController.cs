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
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalPark>>> GetNationalParks()
        {
            return await _context.NationalParks.ToListAsync();
        }

        // GET: api/nationalparks/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<NationalPark>> GetNationalPark(int id)
        {
            var nationalPark = await _context.NationalParks.FirstOrDefaultAsync(park => park.NationalParkId == id);
                
            if (nationalPark == null)
            {
                return NotFound();
            }

            return nationalPark;
        }

        // PUT: api/nationalparks/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThread(int id, NationalPark nationalPark)
        {
            string name = User.Claims.First().Value;
            var currentUser = await _userManager.FindByNameAsync(name);
            if(nationalPark.UserId != currentUser.Id)
            {
                return BadRequest();
            }
            if (id != nationalPark.NationalParkId)
            {
                return BadRequest();
            }

            _context.Entry(nationalPark).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalParkExists(id))
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

        // POST: api/nationalparks
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<NationalPark>> PostNationalPark(AddNationalParkDTO model)
        {
          Console.WriteLine("Name :" + User.Claims.First().Value);
          string name = User.Claims.First().Value;
          var currentUser = await _userManager.FindByNameAsync(name);
  
          NationalPark nationalPark = new NationalPark() {
              Name = model.Name,
              TotalNumberofVisitors = model.TotalNumberofVisitors,
              StateName = model.StateName,
              UserId = currentUser.Id,
          };
          _context.NationalParks.Add(nationalPark);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetNationalParks", new { id = nationalPark.NationalParkId });
        }

        // DELETE: api/nationalParks/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNationalPark(int id)
        {
            var nationalPark = await _context.NationalParks.FirstOrDefaultAsync(park => park.NationalParkId == id);
            if(nationalPark == null)
            {
                return NotFound();
            }
           
            _context.NationalParks.Remove(nationalPark);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool NationalParkExists(int id)
        {
            return _context.NationalParks.Any(park => park.NationalParkId == id);
        }
    }
}
