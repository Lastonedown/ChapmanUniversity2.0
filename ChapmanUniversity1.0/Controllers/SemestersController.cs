using System;
using System.Collections.Generic;
using System.Linq;
using ChapmanUniversity1._0.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChapmanUniversity1._0.Models;
namespace ChapmanUniversity1._0.Controllers
{
    public class SemestersController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public SemestersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            TempData.Clear();
            var semesterList = GetSemesters();

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
            TempData.Remove("SemesterCreatedSuccessfullyAlert");
            TempData.Remove("SemesterExistsAlert");
            List<string> seasonsList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["Seasons"] = new SelectList(seasonsList);

            var semesters = GetSemesters();
            var semesterExists = Validators.SemesterValidator.Validate(semesters,semester.CourseId, semester.CourseSeason);

            Semester newSemester = new Semester()
            {
                CourseId = semester.Id,
                CourseSeason = semester.CourseSeason
            };

            if (!semesterExists)
            {
                _unitOfWork.Semesters.Add(newSemester);
                _unitOfWork.Complete();
                TempData.Add("SemesterCreatedSuccessfullyAlert",null);
                return RedirectToAction(nameof(Create));
            }
            TempData.Add("SemesterExistsAlert", null);
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

        public List<Semester> GetSemesters()
        {
            return _unitOfWork.Semesters.Get(includeProperties: "Course").ToList();
        }
    }
}