using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Repositories;

namespace ChapmanUniversity1._0.Queries
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        ISemesterRepository Semesters { get; }
        int Complete();
    }
}
