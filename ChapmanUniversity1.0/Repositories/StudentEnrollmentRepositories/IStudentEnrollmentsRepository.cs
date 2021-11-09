using System.Security.Principal;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.StudentEnrollmentRepositories
{
    public interface IStudentEnrollmentsRepository : IRepository<StudentSemesterEnrollment>
    {
        bool ValidateStudentEnrollment(int studentId, int semesterId);
    }
}