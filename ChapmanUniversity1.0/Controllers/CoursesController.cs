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
    public class CoursesController : Controller
    {
        private readonly ICourseOperations _courseContext;

        private readonly ISemesterOperations _semesterContext;

        public CoursesController(ICourseOperations courseContext,ISemesterOperations semesterContext)
        {
            _courseContext = courseContext;
            _semesterContext = semesterContext;
        }

        public IActionResult Index()
        {

            var semesters = _semesterContext.SemestersList().Result;

            if (semesters.Count == 0)
            {
                return View(semesters);
            }


            return View(semesters);
        }

        public ActionResult Details(int? id)
        {

            return View();
        }

        public ActionResult Create()
        { 
            List<String> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["SemesterSeasons"] = new SelectList(seasonList);
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            TempData.Remove("CourseCreatedSuccessfullyAlert");
            TempData.Remove("CourseAlreadyCreatedAlert");

            List<String> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["SemesterSeasons"] = new SelectList(seasonList);

            var courseExists = _courseContext.CourseExists(course.CourseNumber);
            var semesterExists = _semesterContext.SemesterExists(course.CourseNumber, course.CourseSeason);

            Semester newSemester = new();
            

            if (!courseExists && !semesterExists)
            {
                Course  newCourse = new Course()
                {
                    CourseNumber = course.CourseNumber,
                    CourseName = course.CourseName,
                    CourseDescription = course.CourseDescription,
                    Credits = course.Credits
                };



                newSemester.Course = newCourse;
                newSemester.CourseSeason = course.CourseSeason;

                await _semesterContext.CreateSemester(newSemester);
                return View();
            }
            if (!semesterExists)
            {
                TempData.Remove("CourseCreatedSuccessfullyAlert");
                TempData.Add("CourseCreatedSuccessfullyAlert", null);
                return View();
            }
            TempData.Add("CourseAlreadyCreatedAlert", null);
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _semesterContext.FindSemesterById(id);
            
            CourseViewModel editedCourse = new CourseViewModel()
            {
                CourseNumber = semester.Course.CourseNumber,
                CourseName = semester.Course.CourseName,
                CourseDescription = semester.Course.CourseDescription,
                Credits = semester.Course.Credits,
                CourseSeason = semester.CourseSeason,
            };
            return View(editedCourse);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseViewModel course){

            if (ModelState.IsValid)
            {
                try
                {
                    Course editedCourse = new Course
                    {
                        CourseNumber = course.CourseNumber,
                        CourseName = course.CourseName,
                        CourseDescription = course.CourseDescription,
                        Credits = course.Credits,
                        Id = course.Id
                    };

                    Semester editedSemester = new Semester()
                    {
                        Course = editedCourse,
                        CourseSeason = course.CourseSeason
                    };

                   

                    await _semesterContext.UpdateSemester(editedSemester);
                    await _courseContext.UpdateCourse(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_courseContext.CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _semesterContext.FindSemesterById(id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _semesterContext.DeleteSemester(id);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
