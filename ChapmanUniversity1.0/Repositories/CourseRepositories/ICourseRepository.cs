using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.CourseRepositories
{
    public interface ICourseRepository :IRepository<Course>
    {
    public bool ValidateCourse(int courseNumber);

    }


}