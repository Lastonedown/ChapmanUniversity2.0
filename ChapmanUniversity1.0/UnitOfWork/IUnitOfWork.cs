using System;
using ChapmanUniversity1._0.Repositories;

namespace ChapmanUniversity1._0.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        ISemesterRepository Semesters { get; }
        IStudentRepository Students { get;}
        int Complete();
    }
}
