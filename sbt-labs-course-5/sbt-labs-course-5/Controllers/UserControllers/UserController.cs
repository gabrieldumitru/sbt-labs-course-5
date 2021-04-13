using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public UsersController(IRepositoryWrapper repository,
                               UserManager<User> userManager,
                               IUserService userService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [Authorize(Roles="Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repository.User.GetAllUsers();

            return Ok(new { users });
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _repository.User.GetByEmailAsync(email);

            return Ok(new { user });
        }

        [Route("{email}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string email, [FromBody] RegisterUserDTO updatedUserDto)
        {
            try
            {
                var user = new User(updatedUserDto);
                var updatedUser = await _userManager.UpdateAsync(user);

                if (updatedUser.Succeeded)
                    return Ok(new { user });
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDTO registerUserDto)
        {
            try
            {
                var newUser = await _userService.RegisterUserAsync(registerUserDto);

                return Ok(new { newUser });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                await _userManager.DeleteAsync(user);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
