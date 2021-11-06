using System;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Repositories;

namespace ChapmanUniversity1._0.DAL
{
    public class UnitOfWork : IDisposable
    {

        private readonly SchoolContext _context = new();
        private Repository<Course> _courseRepository;
        private Repository<Semester> _semesterRepository;
        private Repository<Student> _studentRepository;
        private Repository<Faculty> _facultyRepository;
        private Repository<StudentSemesterEnrollment> _studentSemesterRepository;

        public Repository<Course> Courses
        {
            get
            {
                _courseRepository ??= new Repository<Course>(_context);
                return _courseRepository;
            }
        }

        public Repository<Semester> Semesters
        {
            get
            {
                _semesterRepository ??= new Repository<Semester>(_context);
                return _semesterRepository;
            }
        }

        public Repository<Student> Students
        {
            get
            {
                _studentRepository ??= new Repository<Student>(_context);
                return _studentRepository;
            }
        }
        public Repository<Faculty> FacultyMembers
        {
            get
            {
                _facultyRepository ??= new Repository<Faculty>(_context);
                return _facultyRepository;
            }
        }
        public Repository<StudentSemesterEnrollment> StudentSemesterEnrollments
        {
            get
            {
                _studentSemesterRepository ??= new Repository<StudentSemesterEnrollment>(_context);
                return _studentSemesterRepository;
            }
        }
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
