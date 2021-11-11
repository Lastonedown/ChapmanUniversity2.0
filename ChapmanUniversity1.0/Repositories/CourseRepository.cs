using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Repositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(SchoolContext context) : base(context)
        {
        }
    }
}
