using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Queries;
using ChapmanUniversity1._0.Repositories;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace ChapmanUniversity1._0.Controllers
{

    public class CoursesController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new SchoolContext());

        public IActionResult Index()
        {
            var courses =  _unitOfWork.Courses.GetAll().Result;
           
           return View(courses);
        }

        //public async Task<ActionResult> Details(int id)
        //{

        //    Course courseDetails = new Course()
        //   {
        //       CourseNumber = course.CourseNumber,
        //       CourseDescription = course.CourseDescription,
        //       CourseName = course.CourseName,
        //       Credits = course.Credits,
        //       Id = course.Id
        //   };
        //   return View(courseDetails);
        //}

        public ActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Course course)
        //{
        //    TempData.Remove("CourseCreatedSuccessfullyAlert");
        //    TempData.Remove("CourseAlreadyCreatedAlert");

        //    var courseExists = _courseContext.CourseExists(course.CourseNumber);


        //    if (!courseExists)
        //    {
        //        Course newCourse = new Course()
        //        {
        //            CourseNumber = course.CourseNumber,
        //            CourseName = course.CourseName,
        //            CourseDescription = course.CourseDescription,
        //            Credits = course.Credits
        //        };


        //        await _courseContext.CreateCourse(newCourse);

        //        TempData.Add("CourseCreatedSuccessfullyAlert", null);

        //        return View();
        //    }

        //    TempData.Add("CourseAlreadyCreatedAlert", null);
        //    return View();
        //}

        public ActionResult Edit(int? id)
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Course course)
        //{

        //    Course editedCourse = new Course()
        //    {
        //        CourseName = course.CourseName,
        //        CourseDescription = course.CourseDescription,
        //        CourseNumber = course.CourseNumber,
        //        Credits = course.Credits,
        //        Id = course.Id
        //    };

        //        await _courseContext.UpdateCourse(editedCourse);
        //        return RedirectToAction(nameof(Edit));
        //}


        //public async Task<IActionResult> Delete(int id)
        //{
        //    var course = await _courseContext.FindCourseById(id);


        //    return View(course);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _courseContext.DeleteCourse(id);
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
