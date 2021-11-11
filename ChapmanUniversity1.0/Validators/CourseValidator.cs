using System.Collections.Generic;
using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Validators
{
    public static class CourseValidator
    {
        public static bool Validate(List<Course> courses,int courseNumber)
        {

            if (!courses.Any())
            {
                return false;
            }

            foreach (var course in courses)
            {
                if (course.CourseNumber == courseNumber )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
