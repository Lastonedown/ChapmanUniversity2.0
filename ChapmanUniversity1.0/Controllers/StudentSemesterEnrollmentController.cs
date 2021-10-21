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

        //public StudentSemesterEnrollmentController(IStudentSemesterEnrollmentOperations enrollmentContext, ICourseOperations courseContext,IStudentOperations studentContext,ISemesterOperations semesterContext)
        //{
        //    _enrollmentContext = enrollmentContext;
        //    _courseContext = courseContext;
        //    _studentContext = studentContext;
        //    _semesterContext = semesterContext;
        //}

        //// GET: StudentCourseEnrollments
        //public async Task<IActionResult> Index()
        //{
        //    TempData.Keep("studentId");
        //    var studentId = (int)TempData["studentId"];


        //    var studentEnrollments = await _enrollmentContext.StudentSemesterEnrollmentList();

        //    List<StudentSemesterEnrollment> enrollmentList = new List<StudentSemesterEnrollment>();

        //    foreach (var t in studentEnrollments)
        //    {
        //        if (studentId == t.Student.Id)
        //        {
        //            StudentSemesterEnrollment newStudentSemesterEnrollment = new StudentSemesterEnrollment()
        //            {
        //                Course = t.Course,
        //                Student = t.Student,
        //                Semester = t.Semester,
        //                Id = t.Id
        //            };
        //            enrollmentList.Add(newStudentSemesterEnrollment);
        //        }

        //        return View(enrollmentList);
        //    }

        //    return View(enrollmentList);
        //}

        //public async Task<IActionResult> Details(int? id)
        //{
            
        //    return View();
        //}

        //public async Task<IActionResult> Create()
        //{
        //    ViewData["CourseId"] = new SelectList(await _courseContext.CourseList(), "Id", "CourseName");
        //    List<string> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
        //    ViewData["SemesterSeasons"] = new SelectList(seasonList);

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(StudentSemesterEnrollment studentSemesterEnrollment)
        //{
        //    TempData.Remove("SemesterNotAvailable");
        //    var studentId = (int)TempData["studentId"];
        //    var course = await _courseContext.FindCourseById(studentSemesterEnrollment.Course.Id);
        //    var student = await _studentContext.FindStudent(studentId);
        //    var semester = await _semesterContext.FindSemester(course.Id, studentSemesterEnrollment.Semester.CourseSeason);

        //    if (semester == null)
        //    {
        //        TempData.Add("SemesterNotAvailable",null);
        //        return RedirectToAction(nameof(Create));
        //    }
        //    StudentSemesterEnrollment newEnrollment = new StudentSemesterEnrollment()
        //    {
        //        Course = course,
        //        Semester = semester,
        //        Student = student
        //    };

        //    await _enrollmentContext.CreateStudentSemesterEnrollment(newEnrollment);
        //    TempData.Keep("studentId");
        //    return RedirectToAction(nameof(Create));
        //}

        
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _enrollmentContext.DeleteStudentSemesterEnrollmentById(id);
        //    TempData.Add("CourseRemovedSuccessfullyAlert", null);
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
