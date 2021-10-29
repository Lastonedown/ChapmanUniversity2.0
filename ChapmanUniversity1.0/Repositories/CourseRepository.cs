using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Queries;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Repositories
{
    public class CourseRepository :Repository<Course>,ICourseRepository
    {

        public CourseRepository(SchoolContext context) : base(context)
        {
        }
        public SchoolContext SchoolContext => Context as SchoolContext;

        public bool CourseExists(Course course)
        {
            foreach (var row in SchoolContext.Courses)
            {
                if (row.CourseNumber == course.CourseNumber)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

      
    }
}
