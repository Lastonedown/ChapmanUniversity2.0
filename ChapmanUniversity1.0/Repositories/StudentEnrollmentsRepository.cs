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
        public SchoolContext SchoolContext => Context as SchoolContext;
        public bool EnrollmentExists(int semesterId, int studentId)
        {
            foreach (var enrollment in SchoolContext.StudentCourseEnrollments)
            {
                if (enrollment.SemesterId == semesterId && enrollment.StudentId == studentId)
                {
                    return true;
                }
            }
            return false;
        }

        public List<StudentSemesterEnrollment> GetEnrollmentListWithStudentAndSemesters()
        { 
            return SchoolContext.StudentCourseEnrollments.Include(student => student.Student)
                .Include(semester => semester.Semester.Course).ToList();
        }
    }
}
