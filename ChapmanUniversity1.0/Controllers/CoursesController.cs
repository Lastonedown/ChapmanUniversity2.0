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



        public CoursesController(ICourseOperations courseContext)
        {
            _courseContext = courseContext;
        }

        public async Task <IActionResult> Index()
        {
            var courseList = await _courseContext.CourseList();

            List<Course> indexCourseList = new List<Course>();
            foreach (var course in courseList)
            {
                {
                    Course newCourse = new Course()
                    {
                        CourseNumber = course.CourseNumber,
                        CourseName = course.CourseName,
                        CourseDescription = course.CourseDescription,
                        Credits = course.Credits,
                        Id = course.Id

                    };
                  indexCourseList.Add(newCourse);
                }

            }
            return View(indexCourseList);
        }

        public async Task<ActionResult> Details(int? id)
        {
            var course = await _courseContext.FindCourseById(id);

            Course courseDetails = new Course()
           {
               CourseNumber = course.CourseNumber,
               CourseDescription = course.CourseDescription,
               CourseName = course.CourseName,
               Credits = course.Credits,
               Id = course.Id
           };
           return View(courseDetails);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            TempData.Remove("CourseCreatedSuccessfullyAlert");
            TempData.Remove("CourseAlreadyCreatedAlert");

            var courseExists = _courseContext.CourseExists(course.CourseNumber);


            if (!courseExists)
            {
                Course newCourse = new Course()
                {
                    CourseNumber = course.CourseNumber,
                    CourseName = course.CourseName,
                    CourseDescription = course.CourseDescription,
                    Credits = course.Credits
                };


                await _courseContext.CreateCourse(newCourse);

                TempData.Add("CourseCreatedSuccessfullyAlert", null);

                return View();
            }

            TempData.Add("CourseAlreadyCreatedAlert", null);
            return View();
        }

        public ActionResult Edit(int? id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {

            Course editedCourse = new Course()
            {
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                CourseNumber = course.CourseNumber,
                Credits = course.Credits,
                Id = course.Id
            };

                await _courseContext.UpdateCourse(editedCourse);
                return RedirectToAction(nameof(Edit));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var course = await _courseContext.FindCourseById(id);


            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseContext.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
