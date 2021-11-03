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

        public bool SemesterExists(int courseId, string courseSeason)
        {
            foreach (var semester in SchoolContext.Semesters)
            {
                if (semester.CourseId == courseId && semester.CourseSeason == courseSeason)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Semester> GetSemestersWithCourses()
        {
            var semestersWithCourses = SchoolContext.Semesters.Include(course => course.Course).ToList();

            return semestersWithCourses;
        }

        public int FindSemesterId(int courseId, string courseSeason)
        {
            foreach (var semester in SchoolContext.Semesters)
            {
                if (semester.CourseId == courseId && semester.CourseSeason == courseSeason)
                {
                    return semester.Id;
                }
            }

            return 0;
        }
    }

    
   
}
