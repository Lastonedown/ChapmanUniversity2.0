using System.Collections.Generic;
using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Validators
{
    public static class SemesterValidator
    {
        public static bool Validate(List<Semester> semesters,int courseId, string courseSeason)
        {
            if (!semesters.Any())
            {
                return false;
            }
            foreach (var semester in semesters)
            {
                if (semester.CourseId == courseId && semester.CourseSeason == courseSeason)
                {
                    return true;
                }
            }

            return false;
        }

        public static int FindSemesterId(List<Semester> semesters, int courseId, string courseSeason)
        {
            foreach (var semester in semesters)
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
