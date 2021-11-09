using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using PasswordEncryptDecrypt;

namespace ChapmanUniversity1._0.Repositories.StudentRepositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext context) : base(context)
        {
        }

        public bool ValidateStudent(string email)
        {
            var studentList = Context.Students.ToList();

            if (!studentList.Any())
            {
                return false;
            }
            foreach (var student in studentList)
            {
                if (student.EmailAddress == email)
                {
                    return true;
                }
            }
            return false;
        }

        public Student ValidateStudentLogin(string studentId, string password)
        {
            var studentList = Context.Students.ToList();
            bool isPasswordValid = false;

            foreach (var t in studentList)
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
