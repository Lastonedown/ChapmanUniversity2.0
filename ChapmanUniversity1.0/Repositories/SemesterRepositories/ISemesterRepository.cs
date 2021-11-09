using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.SemesterRepositories
{
    public interface ISemesterRepository : IRepository<Semester>
    {
        public int GetSemesterId(int courseId, string courseSeason);
        public bool ValidateSemester(int courseId, string courseSeason);
    }
}
