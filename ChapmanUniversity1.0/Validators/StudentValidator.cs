using System.Collections.Generic;
using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;
using PasswordEncryptDecrypt;

namespace ChapmanUniversity1._0.Validators
{
    public static class StudentValidator
    {
        public static bool Validate(List<Student>students,string email)
        {

            if (!students.Any())
            {
                return false;
            }
            foreach (var student in students)
            {
                if(student.EmailAddress == email)
                {
                    return true;
                }
            }
            return false;
        }

        public static Student ValidateStudentLogin(List<Student>students,string studentId, string password)
        {
            bool isPasswordValid = false;

            foreach (var student in students)
            {
                string trimmedStudentId = student.StudentUserName.Trim();

                if (trimmedStudentId.Equals(studentId))
                {

                    string savedPasswordHash = student.Password;

                    isPasswordValid = DecryptPassword.Decrypt(savedPasswordHash, password);
                }

                if (isPasswordValid)
                {
                    return student;
                }
            }

            return null;
        }
    }
}
