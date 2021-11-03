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
        private readonly UnitOfWork.UnitOfWork _unitOfWork = new UnitOfWork.UnitOfWork(new SchoolContext());

        public IActionResult Index()
        {

            var semesterList =  _unitOfWork.Semesters.GetSemestersWithCourses();

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

            Semester semester = new Semester
            {
                CourseId = id
            };
            return View(semester);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Semester semester)
        {
            List<string> seasonsList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["Seasons"] = new SelectList(seasonsList);

            var course = _unitOfWork.Courses.GetById(semester.CourseId);
            var semesterExists = _unitOfWork.Semesters.SemesterExists(course.Id, semester.CourseSeason);

            if (!semesterExists)
            {
                Semester newSemester = new Semester()
                {
                    CourseId = semester.CourseId,
                    CourseSeason = semester.CourseSeason
                };
                
                _unitOfWork.Semesters.Add(newSemester);
                _unitOfWork.Complete();
            }
            
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