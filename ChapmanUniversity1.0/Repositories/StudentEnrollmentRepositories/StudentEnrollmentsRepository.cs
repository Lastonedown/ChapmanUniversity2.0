using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using PasswordEncryptDecrypt;

namespace ChapmanUniversity1._0.Repositories.StudentEnrollmentRepositories
{
    public class StudentEnrollmentsRepository : Repository<StudentSemesterEnrollment>, IStudentEnrollmentsRepository
    {
        public StudentEnrollmentsRepository(SchoolContext context) : base(context)
        {
        }


        public bool ValidateStudentEnrollment(int studentId, int semesterId)
        {
            var studentEnrollmentList = Context.StudentCourseEnrollments.ToList();
            if (!studentEnrollmentList.Any())
            {
                return false;
            }
            foreach (var enrollment in studentEnrollmentList)
            {
                if (enrollment.StudentId == studentId && enrollment.SemesterId == semesterId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
