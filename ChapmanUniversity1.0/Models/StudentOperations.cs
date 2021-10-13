using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Models
{
    public interface IStudentOperations
    {
        Task<Student> FindStudent(int? id);
        Task<List<Student>> StudentList();
        Task CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int id);
        bool StudentExists(string studentUserName);
    }
    public class StudentOperations : IStudentOperations
    {
        private readonly SchoolContext _context;

        public StudentOperations(SchoolContext dbContext)
        {
            _context = dbContext;
        }

        public Task<Student> FindStudent(int? id)
        {
            return _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<Student>> StudentList()
        {
            return _context.Students.ToListAsync();
        }

        public Task CreateStudent(Student student)
        {
            _context.Students.Add(student);
            return _context.SaveChangesAsync();
        }
        public Task UpdateStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
        public async Task DeleteStudent(int id)
        {
            var r = await FindStudent(id);
            _context.Remove(r);
            await _context.SaveChangesAsync();
        }

        public bool StudentExists(string studentUserName)
        {
            var studentExists = _context.Students.Any(e => e.StudentUserName == studentUserName);


            if (studentExists)
            {
                return true;
            }
            return false;
        }

    }
}
