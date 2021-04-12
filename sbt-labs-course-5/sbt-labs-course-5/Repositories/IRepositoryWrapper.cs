using sbt_labs_course_5.Repositories;
using sbt_labs_course_5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ISessionTokenRepository SessionToken { get; }
        Task SaveAsync();
    }
}
