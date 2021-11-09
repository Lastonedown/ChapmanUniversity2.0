using System;
using ChapmanUniversity1._0.Repositories.CourseRepositories;
using ChapmanUniversity1._0.Repositories.FacultyRepositories;
using ChapmanUniversity1._0.Repositories.SemesterRepositories;
using ChapmanUniversity1._0.Repositories.StudentEnrollmentRepositories;
using ChapmanUniversity1._0.Repositories.StudentRepositories;

namespace ChapmanUniversity1._0.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ISemesterRepository SemesterRepository { get; }
        ICourseRepository CourseRepository { get; }
        IFacultyRepository FacultyRepository { get; }
        IStudentEnrollmentsRepository StudentEnrollmentsRepository { get; }
        IStudentRepository StudentRepository { get; }
        int Complete();
    }
}