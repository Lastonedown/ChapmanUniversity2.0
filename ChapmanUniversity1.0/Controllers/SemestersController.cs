using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Queries;

namespace ChapmanUniversity1._0.Controllers
{
    public class SemestersController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new SchoolContext());

        public IActionResult Index()
        {

            var semesterList =  _unitOfWork.Semesters.GetAll();


            return View(semesterList);
        }
        public IActionResult Details(int id)
        {

            var semester =  _unitOfWork.Semesters.GetById(id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        public IActionResult Create(int id)
        {
            List<string> seasonsList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["Seasons"] = new SelectList(seasonsList);
            var course = _unitOfWork.Courses.GetById(id);

            Semester semester = new Semester()
            {
                Course = course
            };
            return View(semester);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Semester semester)
        {
            List<string> seasonsList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["Seasons"] = new SelectList(seasonsList);

            var course = _unitOfWork.Courses.GetById(semester.Course.Id);
            var semesterExists = _unitOfWork.Semesters.SemesterExists(course.Id, semester.CourseSeason);

           
            Semester newSemester = new Semester()
            {
                Course = course,
                CourseSeason = semester.CourseSeason
            };
                    _unitOfWork.Semesters.Add(newSemester);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Create));
        }

            public IActionResult Edit(int id)
        {
            var semester = _unitOfWork.Semesters.GetById(id);
            if (semester == null)
            {
                return NotFound();
            }
            return View(semester);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Semester semester)
        {
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Edit));
        }

        public IActionResult Delete(int id)
        {

            var semester = _unitOfWork.Semesters.GetById(id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var semester = _unitOfWork.Semesters.GetById(id);
            _unitOfWork.Semesters.Remove(semester);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}