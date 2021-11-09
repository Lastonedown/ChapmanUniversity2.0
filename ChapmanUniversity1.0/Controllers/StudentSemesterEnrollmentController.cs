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
        private readonly IUnitOfWork _unitOfWork;

        public StudentSemesterEnrollmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            TempData.Keep("studentId");
            var studentId = (int) TempData["studentId"];

            var studentEnrollmentList = _unitOfWork.StudentEnrollmentsRepository.Get().ToList();
            var semesterList = _unitOfWork.SemesterRepository.Get(includeProperties: "Course").ToList();

            List<StudentSemesterEnrollment> enrolledCourses = new();

            foreach (var enrollment in studentEnrollmentList)
            {
                if (enrollment.StudentId == studentId)
                {
                    foreach (var semester in semesterList)
                    {
                        if (enrollment.SemesterId == semester.Id)
                        {
                            enrolledCourses.Add(new StudentSemesterEnrollment {Semester = semester,Id = enrollment.Id});
                        }
                    }

                    return View(enrolledCourses);
                }
            }

            var updatedStudent = _unitOfWork.StudentRepository.GetById(studentId);
            updatedStudent.IsStudentActive = "N";
            _unitOfWork.StudentRepository.Update(updatedStudent);
            _unitOfWork.Complete();
            return View(enrolledCourses);
        }

        public IActionResult Details(int id)
        {

            var semesterList = _unitOfWork.SemesterRepository.Get(includeProperties: "Course").ToList();
            foreach (var semester in semesterList)
            {
                if (semester.Id == id)
                {
                    StudentSemesterEnrollment newSemester = new StudentSemesterEnrollment()
                    {
                        Semester = semester
                    }; 
                    return View(newSemester);
                }
            }
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
            TempData.Remove("NoCoursesAvailable");
            TempData.Remove("CourseAlreadyRegisteredAlert");
            var studentId = (int) TempData["studentId"];
            if (studentSemesterEnrollment.Semester.Course == null)
            {
                TempData.Add("NoCoursesAvailable", null);
                return RedirectToAction(nameof(Create));

            }
            var semesterExists = _unitOfWork.SemesterRepository.ValidateSemester(
                studentSemesterEnrollment.Semester.Course.Id,
                studentSemesterEnrollment.Semester.CourseSeason);

            var semesterId = _unitOfWork.SemesterRepository.GetSemesterId(studentSemesterEnrollment.Semester.CourseId,
                studentSemesterEnrollment.Semester.CourseSeason);
            
            var studentEnrollmentExists = _unitOfWork.StudentEnrollmentsRepository.ValidateStudentEnrollment(studentId,semesterId);
            
            if (!semesterExists)
            {
                TempData.Add("SemesterNotAvailable", null);
                return RedirectToAction(nameof(Create));
            }


            if (!studentEnrollmentExists)
            {
                StudentSemesterEnrollment newEnrollment = new StudentSemesterEnrollment()
                {
                    StudentId = studentId,
                    SemesterId = semesterId
                };
                _unitOfWork.StudentEnrollmentsRepository.Add(newEnrollment);
                var updatedStudent = _unitOfWork.StudentRepository.GetById(studentId);
                updatedStudent.IsStudentActive = "Y";
                _unitOfWork.StudentRepository.Update(updatedStudent);
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
            var enrolledCourseToBeDeleted = _unitOfWork.StudentEnrollmentsRepository.GetById(id);
            var semesterList = _unitOfWork.SemesterRepository.Get(includeProperties: "Course").ToList();
            foreach (var semester in semesterList)
            {
                if (enrolledCourseToBeDeleted.SemesterId == semester.Id)
                {
                    StudentSemesterEnrollment enrollmentToBeDeleted = new StudentSemesterEnrollment()
                    {
                        Semester = semester
                    };
                    return View(enrollmentToBeDeleted);
                }
            }
            return View();
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult  DeleteConfirmed(int id)
            {
                
                _unitOfWork.StudentEnrollmentsRepository.Remove(id);
                _unitOfWork.Complete();
                TempData.Add("CourseRemovedSuccessfullyAlert", null);
                return RedirectToAction(nameof(Index));
            }

    }
}
