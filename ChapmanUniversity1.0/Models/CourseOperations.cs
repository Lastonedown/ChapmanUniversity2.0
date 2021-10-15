using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Models
{
    public interface ICourseOperations
    {
        Task<Course> FindCourseById(int? id);
        Task<List<Course>> CourseList();
        Task CreateCourse(Course course);
        Task UpdateCourse(CourseViewModel course);
        Task DeleteCourse(int id);
        bool CourseExists(int courseNumber);
        Task<Course> FindCourse(int courseNumber);
    }

    public class CourseOperations : ICourseOperations
    {
        private readonly SchoolContext _context;

        public CourseOperations(SchoolContext dbContext)
        {
            _context = dbContext;
        }

        public Task<Course> FindCourseById(int? id)
        {
            return _context.Courses.FirstOrDefaultAsync(s => s.Id == id);
        }
        public Task<Course>FindCourse(int courseNumber)
        {
            var foundCourse = _context.Courses.FirstOrDefaultAsync(b => b.CourseNumber == courseNumber);

            return foundCourse;
        }
        public Task<List<Course>> CourseList()
        {
            return _context.Courses.ToListAsync();
        }

        public Task CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            return _context.SaveChangesAsync();
        }
        public Task UpdateCourse(CourseViewModel course)
        {
            _context.Entry(course).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
        public async Task DeleteCourse(int id)
        {
            var r = await FindCourseById(id);
            _context.Remove(r);
            await _context.SaveChangesAsync();
        }

        public bool CourseExists(int courseNumber)
        {
           return _context.Courses.Any(e => e.CourseNumber == courseNumber);
        }
    }
}
