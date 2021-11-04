using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Controllers
{
    public class StudentSemesterEnrollmentController : Controller
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork = new(new SchoolContext());

        public IActionResult Index()
        {
            TempData.Keep("studentId");
            var studentId = (int)TempData["studentId"];

            var studentEnrollments = _unitOfWork.StudentEnrollments.GetEnrollmentListWithStudentAndSemesters();
            List<StudentSemesterEnrollment> semesterEnrollments = new List<StudentSemesterEnrollment>();
            foreach (var studentEnrollment in studentEnrollments)
            {
                if (studentEnrollment.StudentId == studentId)
                {
                    semesterEnrollments.Add(studentEnrollment);
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
            ViewData["CourseId"] = new SelectList(_unitOfWork.Courses.GetAll(), "Id", "CourseName");
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
            TempData.Remove("CourseAlreadyRegisteredAlert");
            var studentId = (int) TempData["studentId"];
            var semesterExists = _unitOfWork.Semesters.SemesterExists(studentSemesterEnrollment.Semester.Course.Id
                ,studentSemesterEnrollment.Semester.CourseSeason);
            var semesterId = _unitOfWork.Semesters.FindSemesterId(studentSemesterEnrollment.Semester.Course.Id
                ,studentSemesterEnrollment.Semester.CourseSeason);
            var enrollmentExists = _unitOfWork.StudentEnrollments.EnrollmentExists(semesterId, studentId);

            if (!semesterExists)
            {
                TempData.Add("SemesterNotAvailable", null);
                return RedirectToAction(nameof(Create));
            }

            if (!enrollmentExists)
            {
                StudentSemesterEnrollment newEnrollment = new StudentSemesterEnrollment()
                {
                    SemesterId = semesterId,
                    StudentId = studentId,
                };
                _unitOfWork.StudentEnrollments.Add(newEnrollment);
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
