using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChapmanUniversity1._0.Data;
using PasswordEncryptDecrypt;
using ChapmanUniversity1._0.Models;
using Newtonsoft.Json;

namespace ChapmanUniversity1._0.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork = new(new SchoolContext());

        public IActionResult Details()
        {
            if (TempData["StudentId"] == null)
            {
                return NotFound();
            }

            var id = (int)TempData["StudentId"]; 
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
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentRegistration student1)
        { 
            TempData.Clear();
            Random random = new Random();
            Student student = new Student();

            if (ModelState.IsValid && !_unitOfWork.Students.StudentEmailExists(student1.EmailAddress))
            {
               
                string studentId = student1.FirstName.Substring(0, 2) + student1.LastName.Substring(0, 4) +
                                   random.Next(00000, 99999);

                string encryptedPassword = EncryptPassword.Encrypt(student1.Password);

                student.StudentUserName= studentId.Trim();
                student.FirstName = student1.FirstName;
                student.LastName = student1.LastName;
                student.PhoneNumber = student1.PhoneNumber;
                student.EmailAddress = student1.EmailAddress;
                student.DateOfBirth = student1.DateOfBirth;
                student.EnrollmentDate = DateTime.Now;
                student.Password = encryptedPassword;


                _unitOfWork.Students.Add(student);
                _unitOfWork.Complete();
                TempData.Add("RegistrationSuccessAlert",studentId);
                
                return RedirectToAction(nameof(Create));
            }
            if (ModelState.IsValid && _unitOfWork.Students.StudentEmailExists(student1.EmailAddress))
            {
                TempData.Add("EmailExistsAlert", student1.EmailAddress);
            }

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
            var student = ValidateStudentLogin(studentLogin.UserName.Trim(), studentLogin.Password);
            
            TempData.Clear();
            if (student == null)
            {
                TempData.Add("InvalidStudentAlert",null);
                return View();
            }
            TempData.Add("StudentId",student.Id);
            return RedirectToAction("Details");
        }

        public Student ValidateStudentLogin(string studentId, string password)
        {
            var students = _unitOfWork.Students.GetAll();
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
