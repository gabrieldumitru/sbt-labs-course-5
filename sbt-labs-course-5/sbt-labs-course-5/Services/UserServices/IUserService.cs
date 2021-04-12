using sbt_labs_course_5.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDTO registerUserDto);
        Task<string> LoginUser(LoginUserDTO loginUserDto);
    }
}
