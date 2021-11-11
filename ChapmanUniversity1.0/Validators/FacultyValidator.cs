using System.Collections.Generic;
using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Validators
{
    public static class FacultyValidator
    {
        public static Faculty ValidateFacultyLogin(List<Faculty> facultyMembers,string facultyId, string password)
        {
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
