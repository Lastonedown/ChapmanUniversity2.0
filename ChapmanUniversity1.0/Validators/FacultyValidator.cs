using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Validators
{
    public static class FacultyValidator
    {
        private static readonly UnitOfWork UnitOfWork = new();

        public static Faculty ValidateFacultyLogin(string facultyId, string password)
        {
            var facultyMembers = UnitOfWork.FacultyMembers.Get().ToList();
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
