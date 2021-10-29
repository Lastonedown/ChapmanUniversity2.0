using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Repositories
{
    public class SemesterRepository : Repository<Semester>, ISemesterRepository
    {
        public SemesterRepository(SchoolContext context) : base(context)
        {
        }
        public SchoolContext SchoolContext => Context as SchoolContext;

        public bool SemesterExists(int courseNumber, string courseSeason)
        {
            return false;
        }
    }

    
   
}
