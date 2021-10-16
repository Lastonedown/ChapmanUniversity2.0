using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;

namespace ChapmanUniversity1._0.Controllers
{
    public class SemestersController : Controller
    {
        private readonly ISemesterOperations _semestersContext;
        private readonly ICourseOperations _courseContext;

        public SemestersController(ISemesterOperations semesterContext,ICourseOperations courseContext )
        {
            _semestersContext = semesterContext;
            _courseContext = courseContext;
        }


        // GET: Semesters
        public async Task<IActionResult> Index()
        {

            var semesterList = await _semestersContext.SemestersList();


            return View(semesterList);
        }

        // GET: Semesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _semestersContext.FindSemesterById(id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // GET: Semesters/Create
        public async Task<IActionResult> Create(int? id)
        {
            List<string> seasonsList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["Seasons"] = new SelectList(seasonsList);
 
            var foundCourse = await _courseContext.FindCourseById(id);

            Semester semester = new Semester()
            {
                Course = foundCourse
            };

            return View(semester);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Semester semester)
        {
            List<string> seasonsList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["Seasons"] = new SelectList(seasonsList);

            var foundCourse = await _courseContext.FindCourseById(semester.Course.Id);

            Semester newSemester = new Semester()
            {
                Course = foundCourse,
                CourseSeason = semester.CourseSeason
            };

            var semesterExists = _semestersContext.SemesterExists(newSemester.Course.Id, newSemester.CourseSeason);
            if (!semesterExists)
            { 
                await _semestersContext.CreateSemester(newSemester);
                return RedirectToAction(nameof(Create));
            }
            
            return RedirectToAction(nameof(Create));
        }

        // GET: Semesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _semestersContext.FindSemesterById(id);
            if (semester == null)
            {
                return NotFound();
            }
            return View(semester);
        }

        // POST: Semesters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseSeason")] Semester semester)
        {
            if (id != semester.Id)
            {
                return NotFound();
            }

            return View();
        }

        // GET: Semesters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _semestersContext.FindSemesterById(id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // POST: Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _semestersContext.DeleteSemester(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
