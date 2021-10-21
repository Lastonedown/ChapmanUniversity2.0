using System;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Queries
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        ISemesterRepository Semesters { get; }

        int SaveChanges();
    }

}
