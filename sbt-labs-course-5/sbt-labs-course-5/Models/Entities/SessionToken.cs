using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Models.Entities
{
    public class SessionToken
    {
        protected SessionToken() { }
        public SessionToken(string jti, Guid userId, DateTime expirationDate)
        {
            this.Jti = jti;
            this.UserId = userId;
            this.ExpirationDate = expirationDate;
        }

        [Key]
        public string Jti { get; protected set; }
        public DateTime ExpirationDate { get; protected set; }

        public Guid UserId { get; protected set; }
        public User User { get; protected set; }
    }
}
