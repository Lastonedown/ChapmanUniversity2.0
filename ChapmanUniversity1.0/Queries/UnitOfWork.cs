using System;
using ChapmanUniversity1._0.Controllers;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Queries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;

        public UnitOfWork(SchoolContext context)
        {
            _context = context;
            Courses = new CourseRepository(_context);
            Semesters = new SemesterRepository(_context);
        }

        public ICourseRepository Courses { get;}
        public ISemesterRepository Semesters { get; }
        
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
