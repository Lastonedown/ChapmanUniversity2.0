using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        bool CourseExists(Course course);
    }
}

