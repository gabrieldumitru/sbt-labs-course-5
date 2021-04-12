﻿using sbt_labs_course_5.Models.Entities;
using sbt_labs_course_5.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Repositories
{
    public interface ISessionTokenRepository : IGenericRepository<SessionToken>
    {
        Task<SessionToken> GetByJTI(string jti);
        Task<List<SessionToken>> GetByUserId(Guid userId);
    }
}
