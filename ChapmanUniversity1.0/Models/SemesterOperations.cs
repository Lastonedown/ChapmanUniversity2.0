using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Models
{
    public interface ISemesterOperations
        {
            Task<Semester> FindSemesterById(int? id);
            Semester FindSemester(int id, string semesterSeason);
            Task<List<Semester>> SemestersList();
            Task CreateSemester(Semester semester);
            Task UpdateSemester(Semester semester);
            Task DeleteSemester(int id);
            bool SemesterExists(int courseId, string courseSeason);
        }
    public class SemesterOperations : ISemesterOperations
        {
            private readonly SchoolContext _context;

            public SemesterOperations(SchoolContext dbContext)
            {
                _context = dbContext;
            }

            public Task<Semester> FindSemesterById(int? id)
            {
                return _context.Semesters.FirstOrDefaultAsync(s => s.Id == id);
            }
            public Task<List<Semester>> SemestersList()
            {
                var semesterList = _context.Semesters.Include(x => x.Course).ToListAsync();



            return (semesterList);
            }

            public Task CreateSemester(Semester semester)
            {
                _context.Semesters.Add(semester);
                return _context.SaveChangesAsync();
            }
            public Task UpdateSemester(Semester semester)
            {
                _context.Entry(semester).State = EntityState.Modified;
                return _context.SaveChangesAsync();
            }
            public async Task DeleteSemester(int id)
            {
                var r = await FindSemesterById(id);
                _context.Remove(r);
                await _context.SaveChangesAsync();
            }

            public bool SemesterExists (int courseId, string courseSeason)
            {
                var courseNumberExists = _context.Semesters.Any(e => e.Course.Id == courseId);

                var courseSeasonExists = _context.Semesters.Any(e => e.CourseSeason == courseSeason);

                if (courseNumberExists && courseSeasonExists)
                {
                    return true;
                }
                return false;
            }

        public Semester FindSemester(int courseId, string semesterSeason)
        {
            foreach (var semester in SemestersList().Result)
            {
                if (semester.Course.Id == courseId && semester.CourseSeason == semesterSeason)
                {

                    return semester;
                }
            }

            return null;
        }
    }
}