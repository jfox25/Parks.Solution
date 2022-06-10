using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Park.Models;
using Park.DTO;
using Park.Services;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Linq;
using Park.Interfaces;

namespace Park.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly ParkContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountController (ParkContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
    {
      _context = context;
      _userManager = userManager;
      _signInManager = signInManager;
      _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserDTO>> Register (RegisterUserDTO model)
    {
      var user = new ApplicationUser { UserName = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return Ok(_tokenService.CreateToken(user));
      }
      else
      {
        return BadRequest(result.Errors);
      }
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserDTO model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
      
      if (result.Succeeded)
      {
        var user = await _userManager.FindByNameAsync(model.Email);
        return Ok(_tokenService.CreateToken(user));
      }
      else
      {
          return BadRequest();
      }
    }
    
    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }

}