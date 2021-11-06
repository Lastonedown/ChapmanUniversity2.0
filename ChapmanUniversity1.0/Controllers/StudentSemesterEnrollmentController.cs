using System;
using System.Collections.Generic;
using System.Linq;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChapmanUniversity1._0.Controllers
{
    public class StudentSemesterEnrollmentController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentSemesterEnrollmentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            TempData.Keep("studentId");
            var studentId = (int) TempData["studentId"];

            var studentSemesterEnrollments = _unitOfWork.StudentSemesterEnrollments.Get(includeProperties:"Semester").ToList();

            

            List<StudentSemesterEnrollment> semesterEnrollments = new List<StudentSemesterEnrollment>();

            foreach (var studentEnrollment in studentSemesterEnrollments)
            {
                if (studentEnrollment.StudentId == studentId)
                {
                    var course = _unitOfWork.Courses.GetById(studentEnrollment.Semester.CourseId);
                    StudentSemesterEnrollment semester = new StudentSemesterEnrollment()
                        {
                            Semester = studentEnrollment.Semester,
                            Course = course
                        };

                        semesterEnrollments.Add(semester);
                }
            }

            return View(semesterEnrollments);

        }

        public IActionResult Details(int? id)
        {

            return View();
        }

        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_unitOfWork.Courses.Get(), "Id", "CourseName");
            List<string> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["SemesterSeasons"] = new SelectList(seasonList);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentSemesterEnrollment studentSemesterEnrollment)
        {
            TempData.Remove("SemesterNotAvailable");
            TempData.Remove("CourseEnrollmentSuccessAlert");
            TempData.Remove("NoCoursesAvailable");
            TempData.Remove("CourseAlreadyRegisteredAlert");
            var studentId = (int) TempData["studentId"];
            if (studentSemesterEnrollment.Semester.Course == null)
            {
                TempData.Add("NoCoursesAvailable", null);
                return RedirectToAction(nameof(Create));

            }
            var semesterExists = Validators.SemesterValidator.Validate(
                studentSemesterEnrollment.Semester.Course.Id,
                studentSemesterEnrollment.Semester.CourseSeason);
            

            var semesterId = BusinessLogic.SemesterBusinessLogic.FindSemesterId(
                studentSemesterEnrollment.Semester.Course.Id, studentSemesterEnrollment.Semester.CourseSeason);

            var studentEnrollmentExists = Validators.StudentEnrollmentValidator.Validate(studentId, semesterId);
            
            if (!semesterExists)
            {
                TempData.Add("SemesterNotAvailable", null);
                return RedirectToAction(nameof(Create));
            }

            if (!studentEnrollmentExists)
            {
                StudentSemesterEnrollment newEnrollment = new StudentSemesterEnrollment()
                {
                    SemesterId = semesterId,
                    StudentId = studentId
                };

                _unitOfWork.StudentSemesterEnrollments.Add(newEnrollment);
                _unitOfWork.Complete();
                TempData.Add("CourseEnrollmentSuccessAlert", null);
                TempData.Keep("studentId");
                return RedirectToAction(nameof(Create));
            }

            TempData.Keep("studentId");
            TempData.Add("CourseAlreadyRegisteredAlert", null);
            return RedirectToAction(nameof(Create));
        }

        public ActionResult Delete(int id)
            {
                return View();
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult  DeleteConfirmed(int id)
            {
                TempData.Add("CourseRemovedSuccessfullyAlert", null);
                return RedirectToAction(nameof(Index));
            }

    }
}
