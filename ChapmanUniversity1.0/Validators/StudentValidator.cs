using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;
using PasswordEncryptDecrypt;

namespace ChapmanUniversity1._0.Validators
{
    public static class StudentValidator
    {
        private static readonly UnitOfWork UnitOfWork = new();

        public static bool Validate(string email)
        {
            var studentList = UnitOfWork.Students.Get().ToList();
            
            if (!studentList.Any())
            {
                return false;
            }
            foreach (var student in studentList)
            {
                if(student.EmailAddress == email)
                {
                    return true;
                }
            }
            return false;
        }

        public static Student ValidateStudentLogin(string studentId, string password)
        {
            var students = UnitOfWork.Students.Get().ToList();
            bool isPasswordValid = false;

            foreach (var t in students)
            {
                string trimmedStudentId = t.StudentUserName.Trim();

                if (trimmedStudentId.Equals(studentId))
                {

                    string savedPasswordHash = t.Password;

                    isPasswordValid = DecryptPassword.Decrypt(savedPasswordHash, password);
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
