using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sbt_labs_course_5.Models.Constants;
using sbt_labs_course_5.Models.DTOs;
using sbt_labs_course_5.Models.Entities;
using sbt_labs_course_5.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryWrapper _repository;

        public UserService(IConfiguration configuration,
                           UserManager<User> userManager,
                           IRepositoryWrapper repository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<string> LoginUser(LoginUserDTO loginUserDto)
        {
            User user = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (user != null)
            {
                user = await _repository.User.GetByIdAsyncWithRoles(user.Id);
                List<string> roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

                var newJti = Guid.NewGuid().ToString();
                var tokenHandler = new JwtSecurityTokenHandler();
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtTokenKey").Value));

                var token = GenerateJwtToken(signinKey, user, roles, tokenHandler, newJti);

                _repository.SessionToken.Create(new SessionToken(newJti, user.Id, token.ValidTo));
                await _repository.SaveAsync();

                return tokenHandler.WriteToken(token);
            }

            return null;
        }

        private static SecurityToken GenerateJwtToken(SymmetricSecurityKey signinKey, User user, List<string> roles, JwtSecurityTokenHandler tokenHandler,string newJti)
        {
            var subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, newJti)
            });

            foreach (var role in roles)
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDTO registerUserDto)
        {
            var registerUser = new User(registerUserDto);

            var result = await _userManager.CreateAsync(registerUser, registerUserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, UserRoleType.Admin);

                return true;
            }

            return false;
        }
    }
}
