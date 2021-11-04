using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories
{
    public interface IStudentEnrollmentsRepository : IRepository<StudentSemesterEnrollment>
    {
        bool EnrollmentExists(int semesterId, int studentId);

        List<StudentSemesterEnrollment> GetEnrollmentListWithStudentAndSemesters();
    }
}
