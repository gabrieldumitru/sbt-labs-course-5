using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Models.Base
{
    interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}
