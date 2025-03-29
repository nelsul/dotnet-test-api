using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTestApi.DTOs.User;
using MyTestApi.Interfaces;
using MyTestApi.Mappers;
using MyTestApi.Models;

namespace MyTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService
        )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = createUserDTO.ToUser();

                var createdUser = await _userManager.CreateAsync(appUser, createUserDTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(appUser.ToUserDTO(_tokenService.CreateToken(appUser)));
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(u =>
                    u.Email == loginUserDTO.Email
                );

                if (user == null)
                {
                    return Unauthorized("Email or Password invalid");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(
                    user,
                    loginUserDTO.Password,
                    false
                );

                if (!result.Succeeded)
                {
                    return Unauthorized("Email or Password invalid");
                }

                return Ok(user.ToUserDTO(_tokenService.CreateToken(user)));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
