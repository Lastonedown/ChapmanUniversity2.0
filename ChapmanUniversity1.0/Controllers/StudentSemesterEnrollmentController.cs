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

        private readonly IStudentSemesterEnrollmentOperations _enrollmentContext;

        public StudentSemesterEnrollmentController(IStudentSemesterEnrollmentOperations enrollmentContext)
        {
            _enrollmentContext = enrollmentContext;
        }

        // GET: StudentCourseEnrollments
        public ActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            
            return View();
        }

        public ActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_enrollmentContext.StudentSemesterEnrollmentList().Result, "Id", "CourseName");
            List<String> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["SemesterSeasons"] = new SelectList(seasonList);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentSemesterEnrollment studentSemesterEnrollment)
        {
            return View();
        }

        
        public ActionResult Delete(int id)
        {
            return View();
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
