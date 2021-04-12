using Microsoft.AspNetCore.Identity;
using sbt_labs_course_5.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User() : base() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public User(RegisterUserDTO registerUserDto)
        {
            Email = registerUserDto.Email;
            UserName = registerUserDto.Email;
            FirstName = registerUserDto.FirstName;
            LastName = registerUserDto.LastName;
            SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}
