using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Models
{
    public interface IStudentSemesterEnrollmentOperations
    {
        Task<StudentSemesterEnrollment> FindSemesterEnrollmentById(int? id);

        Task<StudentSemesterEnrollment> FindStudentSemesterEnrollment(int courseNumber,string semesterSeason, Student student);

        Task<List<StudentSemesterEnrollment>> StudentSemesterEnrollmentList();
        Task CreateStudentSemesterEnrollment(StudentSemesterEnrollment enrollment);
        Task UpdateStudentSemesterEnrollment(StudentSemesterEnrollment enrollment);
        Task DeleteStudentSemesterEnrollmentById(int id);
        bool StudentSemesterEnrollmentExists(Course course, Semester semester, Student student);
    }

    public class StudentStudentSemesterEnrollmentOperations : IStudentSemesterEnrollmentOperations
    {
        private readonly SchoolContext _context;

        public StudentStudentSemesterEnrollmentOperations(SchoolContext dbContext)
        {
            _context = dbContext;
        }

        public Task<StudentSemesterEnrollment> FindSemesterEnrollmentById(int? id)
        {
            return _context.StudentCourseEnrollments.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<StudentSemesterEnrollment> FindStudentSemesterEnrollment(int courseNumber, string semesterSeason, Student student)
        {
            throw new NotImplementedException();
        }


        public async Task<List<StudentSemesterEnrollment>> StudentSemesterEnrollmentList()
        {
            return await _context.StudentCourseEnrollments.Include(d => d.Student).Include(d => d.Semester).Include(d => d.Course).ToListAsync(); ;
        }

        public Task CreateStudentSemesterEnrollment(StudentSemesterEnrollment enrollment)
        {
            _context.StudentCourseEnrollments.Add(enrollment);
           
            return _context.SaveChangesAsync();
        }

        public Task UpdateStudentSemesterEnrollment(StudentSemesterEnrollment enrollment)
        {
            _context.Entry(enrollment).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public async Task DeleteStudentSemesterEnrollmentById(int id)
        {
            var r = await FindSemesterEnrollmentById(id);
            _context.Remove(r);
            await _context.SaveChangesAsync();
        }

        public bool StudentSemesterEnrollmentExists(Course course, Semester semester, Student student)
        {
            var studentEnrollments = StudentSemesterEnrollmentList().Result;

            foreach (var enrollment in studentEnrollments)
            {
                if (course == enrollment.Course && semester == enrollment.Semester && enrollment.Student == student)
                {
                    return true;
                }
            }

            return false;
        }

        public Task<StudentSemesterEnrollment> FindStudentSemesterEnrollment(Course course, Semester semester,
            Student student)
        {
            var studentEnrollments = StudentSemesterEnrollmentList().Result;

            foreach (var enrollment in studentEnrollments)
            {
                if (course == enrollment.Course && enrollment.Semester == semester && enrollment.Student == student)
                {


                    var foundEnrollment = FindSemesterEnrollmentById(enrollment.Id);

                    return foundEnrollment;
                }

                return null;
            }

            return null;
        }
    }
}