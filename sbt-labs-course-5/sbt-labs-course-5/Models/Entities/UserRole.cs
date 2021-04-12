using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Models.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
