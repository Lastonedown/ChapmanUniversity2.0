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
        private readonly ISemesterOperations _semesterContext;

        private readonly ICourseOperations _courseContext;

        private readonly IStudentOperations _studentContext;

        private readonly IStudentSemesterEnrollmentOperations _enrollmentContext;

        public StudentSemesterEnrollmentController(ICourseOperations courseContext, ISemesterOperations semesterContext,IStudentOperations studentContext,IStudentSemesterEnrollmentOperations enrollmentContext)
        {
            _courseContext = courseContext;
            _semesterContext = semesterContext;
            _studentContext = studentContext;
            _enrollmentContext = enrollmentContext;
        }

        // GET: StudentCourseEnrollments
        public ActionResult Index()
        {
            TempData.Remove("CourseEnrollmentSuccessAlert");
            TempData.Remove("CourseAlreadyRegisteredAlert");
            TempData.Remove("CourseRemovedSuccessfullyAlert");

            if (TempData["StudentId"] == null)
            {
                return NotFound();
            }

            var id = (int) TempData["StudentId"];
            TempData.Keep("StudentId");

            var student = _studentContext.FindStudent(id).Result;


            var studentEnrollments = _enrollmentContext.StudentSemesterEnrollmentList().Result;
            
            List<StudentSemesterEnrollment> enrollmentList = new List<StudentSemesterEnrollment>();

            if (studentEnrollments.Count == 0)
            {
                student.IsStudentActive = "N";
                _studentContext.UpdateStudent(student);
            }

            foreach (var t in enrollmentList)
            {
                if (id == t.Student.Id)
                {
                    StudentSemesterEnrollment newStudentSemesterEnrollment = new StudentSemesterEnrollment()
                    {
                        Course = t.Course,
                        Student = t.Student,
                        Semester = t.Semester,
                        Id = t.Id
                    };
                    enrollmentList.Add(newStudentSemesterEnrollment);
                }
            }

            return View(enrollmentList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            TempData.Remove("CourseEnrollmentSuccessAlert");
            TempData.Remove("CourseAlreadyRegisteredAlert");
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentContext.FindSemesterEnrollmentById(id);

            Semester enrolledCourse = new Semester()
            {
                Course = enrollment.Course,
                Id = enrollment.Id
            };
            StudentSemesterEnrollment enrollmentDetails = new StudentSemesterEnrollment()
            {
                Course = enrolledCourse.Course
            };
            return View(enrollmentDetails);
        }

        public ActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_courseContext.CourseList().Result, "Id", "CourseName");
            List<String> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["SemesterSeasons"] = new SelectList(seasonList);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentSemesterEnrollment studentSemesterEnrollment)
        {
            ViewData["CourseId"] = new SelectList(_courseContext.CourseList().Result, "Id", "CourseName");
            List<String> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["SemesterSeasons"] = new SelectList(seasonList);

            if (TempData["StudentId"] == null)
            {
                return NotFound();
            }

            var id = (int) TempData["StudentId"];
            TempData.Keep("StudentId");

            var student = _studentContext.FindStudent(id).Result;

            var semester = _semesterContext.FindSemester(studentSemesterEnrollment.Course.Id,studentSemesterEnrollment.Semester.CourseSeason);


            StudentSemesterEnrollment newStudentSemesterEnrollment = new StudentSemesterEnrollment
            {

                Student = student,
                Semester = semester,
                Course = semester.Course
            };

            var enrollmentExists = _enrollmentContext.StudentSemesterEnrollmentExists(semester.Course, semester, student);
            var semesterExists = _semesterContext.SemesterExists(semester.Course.Id, semester.CourseSeason);

            var studentEnrollments = _semesterContext.SemestersList().Result;

               await _enrollmentContext.CreateStudentSemesterEnrollment(newStudentSemesterEnrollment);
            
                TempData.Add("CourseEnrollmentSuccessAlert", null);
                return RedirectToAction("Create", "StudentSemesterEnrollment");
            }

        
        public ActionResult Delete(int id)
        {
            var enrollment = _courseContext.FindCourseById(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrollmentContext.DeleteStudentSemesterEnrollmentById(id);
            TempData.Add("CourseRemovedSuccessfullyAlert", null);
            return RedirectToAction(nameof(Index));
        }

    }
}
