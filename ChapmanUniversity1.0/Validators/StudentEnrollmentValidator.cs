using System.Collections.Generic;
using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Validators
{
    public static class StudentEnrollmentValidator
    {
        public static bool Validate(List<StudentSemesterEnrollment> studentSemesterEnrollments,int studentId,int semesterId)
        {
            if (!studentSemesterEnrollments.Any())
            {
                return false;
            }

            foreach (var enrolledStudent in studentSemesterEnrollments)
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
