using sbt_labs_course_5.Models;
using sbt_labs_course_5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;
        private IUserRepository _user;
        private ISessionTokenRepository _sessionToken;
        private IBookRepository _bookRepository;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null) _user = new UserRepository(_context);
                return _user;
            }
        }

        public ISessionTokenRepository SessionToken
        {
            get
            {
                if (_sessionToken == null) _sessionToken = new SessionTokenRepository(_context);
                return _sessionToken;
            }
        }

        public IBookRepository Book
        {
            get
            {
                if (_bookRepository == null) _bookRepository = new BookRepository(_context);
                return _bookRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
