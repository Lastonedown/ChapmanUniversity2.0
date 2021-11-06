using System.Linq;
using ChapmanUniversity1._0.DAL;

namespace ChapmanUniversity1._0.Validators
{
    public static class CourseValidator
    {
        private static readonly UnitOfWork UnitOfWork = new();

        public static bool Validate(int courseNumber)
        {

           var courseList = UnitOfWork.Courses.Get().ToList();

           if (!courseList.Any())
           {
               return false;
           }

           foreach (var course in courseList)
           {
               if (course.CourseNumber == courseNumber)
               {
                   return true;
               }
           }

           return false;
        }
    }
}
