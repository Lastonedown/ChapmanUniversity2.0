using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories
{
    public class SemesterRepository : Repository<Semester>, ISemesterRepository

    {
        private readonly SchoolContext _semesterDbEntities;

        public SemesterRepository(SchoolContext context) : base(context)
        {
            _semesterDbEntities = context;
        }
    }
}
