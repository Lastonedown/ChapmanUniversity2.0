using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Repositories;

namespace ChapmanUniversity1._0.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;

        public UnitOfWork(SchoolContext context)
        {
            _context = context;
            Courses = new CourseRepository(_context);
            Semesters = new SemesterRepository(_context);
            Students = new StudentRepository(_context);
            StudentEnrollments = new StudentEnrollmentsRepository(_context);

        }

        public ICourseRepository Courses { get; private set; }
        public ISemesterRepository Semesters { get;private set; } 
        public  IStudentRepository Students { get; private set; }
        public IStudentEnrollmentsRepository StudentEnrollments { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
