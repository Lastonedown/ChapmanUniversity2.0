using System.Linq;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Repositories.FacultyRepositories
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(SchoolContext context) : base(context)
        {
        }

        public Faculty ValidateFacultyLogin(string facultyId, string password)
        {
            var facultyMembers = Context.FacultyMembers.ToList();
            bool isPasswordValid = false;

            foreach (var t in facultyMembers)
            {
                string trimmedFacultyId = t.FacultyUserName.Trim();

                if (trimmedFacultyId.Equals(facultyId) && password == t.Password)
                {
                    isPasswordValid = true;
                }

                if (isPasswordValid)
                {
                    return t;
                }
            }

            return null;
        }

    }
}
