using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories
{
    public interface ISemesterRepository : IRepository<Semester>
    {

        bool SemesterExists(int courseNumber, string courseSeason);

        IEnumerable<Semester> GetSemestersWithCourses();
        int FindSemesterId(int courseId, string courseSeason);
    }
}

