using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories
{
    public class FacultyRepository : Repository<Faculty>
    {
        public FacultyRepository(SchoolContext context) : base(context)
        {
        }
    }
}
