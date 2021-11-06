using System;
using ChapmanUniversity1._0.DAL;
using Microsoft.AspNetCore.Mvc;
using PasswordEncryptDecrypt;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new();

        public IActionResult Details()
        {
            if (TempData["StudentId"] == null)
            {
                return NotFound();
            }

            var id = (int) TempData["StudentId"];
            TempData.Keep("StudentId");

            var student = _unitOfWork.Students.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            TempData.Clear();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentRegistration student1)
        {
            TempData.Clear();
            Random random = new Random();
            Student student = new Student();

            var studentExists = Validators.StudentValidator.Validate(student1.EmailAddress);
            if (ModelState.IsValid && !studentExists)
            {

                string studentId = student1.FirstName.Substring(0, 2) + student1.LastName.Substring(0, 4) +
                                   random.Next(00000, 99999);

                string encryptedPassword = EncryptPassword.Encrypt(student1.Password);

                student.StudentUserName = studentId.Trim();
                student.FirstName = student1.FirstName;
                student.LastName = student1.LastName;
                student.PhoneNumber = student1.PhoneNumber;
                student.EmailAddress = student1.EmailAddress;
                student.DateOfBirth = student1.DateOfBirth;
                student.EnrollmentDate = DateTime.Now;
                student.Password = encryptedPassword;


                _unitOfWork.Students.Add(student);
                _unitOfWork.Complete();
                TempData.Add("RegistrationSuccessAlert", studentId);

                return RedirectToAction(nameof(Create));
            }

            TempData.Add("EmailExistsAlert", null);
            return View();
        }


        public ActionResult Login()
        {
            TempData.Remove("FacultyId");
            TempData.Remove("StudentId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(StudentLogin studentLogin)
        {
            var student =
                Validators.StudentValidator.ValidateStudentLogin(studentLogin.UserName.Trim(), studentLogin.Password);

            TempData.Clear();
            if (student == null)
            {
                TempData.Add("InvalidStudentAlert", null);
                return View();
            }

            TempData.Add("StudentId", student.Id);
            return RedirectToAction("Details");
        }
    }
}
