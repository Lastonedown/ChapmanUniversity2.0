using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.StudentRepositories
{
    public interface IStudentRepository :IRepository<Student>
    {
        bool ValidateStudent(string email);
        Student ValidateStudentLogin(string studentId, string password);
    }
}