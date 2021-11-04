using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Repositories
{
    public class StudentEnrollmentsRepository : Repository<StudentSemesterEnrollment>, IStudentEnrollmentsRepository
    {
        public StudentEnrollmentsRepository(SchoolContext context) : base(context)
        {
        }
    }
}
