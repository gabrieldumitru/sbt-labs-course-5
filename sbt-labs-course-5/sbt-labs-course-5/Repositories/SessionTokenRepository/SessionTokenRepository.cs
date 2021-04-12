using Microsoft.EntityFrameworkCore;
using sbt_labs_course_5.Models;
using sbt_labs_course_5.Models.Entities;
using sbt_labs_course_5.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Repositories
{
    public class SessionTokenRepository : GenericRepository<SessionToken>, ISessionTokenRepository
    {
        public SessionTokenRepository(ApplicationDbContext context) : base(context) { }


        public async Task<SessionToken> GetByJTI(string jti)
        {
            return await _context.SessionTokens
            .FirstOrDefaultAsync(t => t.Jti.Equals(jti));
        }

        public async Task<List<SessionToken>> GetByUserId(Guid userId)
        {
            return await _context.SessionTokens
           .Where(t => t.UserId.Equals(userId))
            .ToListAsync();
        }
    }
}
