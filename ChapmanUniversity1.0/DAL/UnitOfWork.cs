using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Repositories.CourseRepositories;
using ChapmanUniversity1._0.Repositories.FacultyRepositories;
using ChapmanUniversity1._0.Repositories.SemesterRepositories;
using ChapmanUniversity1._0.Repositories.StudentEnrollmentRepositories;
using ChapmanUniversity1._0.Repositories.StudentRepositories;

namespace ChapmanUniversity1._0.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;

        public UnitOfWork(SchoolContext context)
        {
            this._context = context;
        }


        private ISemesterRepository semesterRepository;
        public ISemesterRepository SemesterRepository => semesterRepository ?? new SemesterRepository(_context);
        
        private ICourseRepository courseRepository;
        public ICourseRepository CourseRepository => courseRepository ?? new CourseRepository(_context);

        private IFacultyRepository facultyRepository;
        public IFacultyRepository FacultyRepository => facultyRepository ?? new FacultyRepository(_context);

        private IStudentEnrollmentsRepository studentEnrollmentsRepository;
        public IStudentEnrollmentsRepository StudentEnrollmentsRepository => studentEnrollmentsRepository ?? new StudentEnrollmentsRepository(_context);

        private IStudentRepository studentRepository;
        public IStudentRepository StudentRepository => studentRepository ?? new StudentRepository(_context);

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
