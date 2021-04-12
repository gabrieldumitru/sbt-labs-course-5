using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sbt_labs_course_5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role,
        Guid, IdentityUserClaim<Guid>,
        UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<SessionToken> SessionTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();
                userRole.HasOne(ur => ur.User).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
            });
        }
    }
}
