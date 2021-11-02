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
using ChapmanUniversity1._0.Repositories;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace ChapmanUniversity1._0.Controllers
{

    public class CoursesController : Controller
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork = new(new SchoolContext());

        public IActionResult Index()
        {
            TempData.Remove("CourseCreatedSuccessfullyAlert");
            TempData.Remove("CourseAlreadyCreatedAlert");
            var courses =   _unitOfWork.Courses.GetAll();
           
           return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            
            return View(course);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            TempData.Remove("CourseCreatedSuccessfullyAlert");
            TempData.Remove("CourseAlreadyCreatedAlert");

            var courseExists = _unitOfWork.Courses.CourseExists(course);


            if (!courseExists)
            {
                 _unitOfWork.Courses.Add(course); 
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
                TempData.Add("CourseCreatedSuccessfullyAlert", null);

                return RedirectToAction(nameof(Create));
            }

            TempData.Add("CourseAlreadyCreatedAlert", null);
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Edit(int id)
        {
            var course =  _unitOfWork.Courses.GetById(id);
            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Edit));
        }


        public IActionResult Delete(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Course course)
        {

            _unitOfWork.Courses.Remove(course);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

    }
}
