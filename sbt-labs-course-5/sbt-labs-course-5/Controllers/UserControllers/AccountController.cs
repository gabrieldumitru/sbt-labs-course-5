using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sbt_labs_course_5.Models;
using sbt_labs_course_5.Models.DTOs;
using sbt_labs_course_5.Models.Entities;
using sbt_labs_course_5.Repositories;
using sbt_labs_course_5.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryWrapper _repository;

        public AccountController(UserManager<User> userManager,
                                 IUserService userService,
                                 ApplicationDbContext context,
                                 IRepositoryWrapper repository)
        {
            _userManager = userManager;
            _userService = userService;
            _context = context;
            _repository = repository;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            if (ModelState.IsValid && registerUserDto != null)
            {
                try
                {
                    if (await _userManager.FindByEmailAsync(registerUserDto.Email) != null)
                        throw new Exception("A user with this email already exists!");

                    var result = await _userService.RegisterUserAsync(registerUserDto);

                    if (result)
                        return Ok(result);
                    else
                        return BadRequest("The user already exists!");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest("Registration data is not valid");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDto)
        {
            if (!ModelState.IsValid && loginUserDto != null)
                return BadRequest();

            var token = await _userService.LoginUser(loginUserDto);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
