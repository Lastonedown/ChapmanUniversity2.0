using System.Linq;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.CourseRepositories
{

    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext context) : base(context)
        {
        }

        public bool ValidateCourse(int courseNumber)
        {

            var courseList = Context.Courses.ToList();

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
