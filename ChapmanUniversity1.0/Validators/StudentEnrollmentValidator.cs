using System.Linq;
using ChapmanUniversity1._0.DAL;

namespace ChapmanUniversity1._0.Validators
{
    public static class StudentEnrollmentValidator
    {
        private static readonly UnitOfWork UnitOfWork = new();

        
        public static bool Validate(int studentId,int semesterId)
        {
            var studentEnrollmentsList = UnitOfWork.StudentSemesterEnrollments.Get().ToList();

            if (!studentEnrollmentsList.Any())
            {
                return false;
            }

            foreach (var enrolledStudent in studentEnrollmentsList)
            {
                if (enrolledStudent.StudentId == studentId && enrolledStudent.SemesterId == semesterId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
