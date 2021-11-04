using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading.Tasks;
using ChapmanUniversity1._0.DAL;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChapmanUniversity1._0.Controllers
{
    public class StudentSemesterEnrollmentController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new();
        public IActionResult Index()
        {
            TempData.Keep("studentId");
            var studentId = (int)TempData["studentId"];

            var studentEnrollments = _unitOfWork.StudentSemesterEnrollmentRepository.Get(includeProperties: "Semester");
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
            ViewData["CourseId"] = new SelectList(_unitOfWork.CourseRepository.Get(), "Id", "CourseName");
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

            var semesters = _unitOfWork.SemesterRepository.Get();
            var studentEnrollments = _unitOfWork.StudentSemesterEnrollmentRepository.Get();
           
            var semesterList = semesters as Semester[] ?? semesters.ToArray();
            var studentSemesterEnrollments = studentEnrollments as StudentSemesterEnrollment[] ?? studentEnrollments.ToArray();

            if (semesterList.Any() && studentSemesterEnrollments.Any())
            {
                foreach (var row in semesterList)
                {
                    if (row.CourseSeason != studentSemesterEnrollment.Semester.CourseSeason)
                    {
                        TempData.Add("SemesterNotAvailable", null);
                        return RedirectToAction(nameof(Create));
                    }

                    if (studentSemesterEnrollments.Any())
                        foreach (var enrollment in studentSemesterEnrollments)
                        {
                            if (enrollment.Semester.CourseSeason != studentSemesterEnrollment.Semester.CourseSeason)
                            {
                                StudentSemesterEnrollment newEnrollment = new StudentSemesterEnrollment()
                                {
                                    SemesterId = row.Id,
                                    StudentId = studentId,
                                };
                                _unitOfWork.StudentSemesterEnrollmentRepository.Add(newEnrollment);
                                _unitOfWork.Complete();
                                TempData.Add("CourseEnrollmentSuccessAlert", null);
                                TempData.Keep("studentId");
                                return RedirectToAction(nameof(Create));
                            }
                        }

                    TempData.Keep("studentId");
                    TempData.Add("CourseAlreadyRegisteredAlert", null);
                    return RedirectToAction(nameof(Create));
                }
            }
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
