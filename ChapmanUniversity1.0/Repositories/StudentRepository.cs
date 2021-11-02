using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Repositories
{
    public class StudentRepository : Repository<Student>,IStudentRepository
    {
        public StudentRepository(DbContext context) : base(context)
        {
        }

        public SchoolContext SchoolContext => Context as SchoolContext;


        public bool StudentEmailExists(string emailAddress)
        {
            foreach (var student in SchoolContext.Students )
            {
                if (student.EmailAddress == emailAddress)
                {
                    return true;
                }
            }  return false;
        }
    }
}
