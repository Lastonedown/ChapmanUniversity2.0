using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.FacultyRepositories
{
    public interface IFacultyRepository : IRepository<Faculty>
    {
        Faculty ValidateFacultyLogin(string facultyId, string password);
    }
}