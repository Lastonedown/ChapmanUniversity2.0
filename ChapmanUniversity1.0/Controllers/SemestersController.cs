using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
namespace ChapmanUniversity1._0.Controllers
{
    public class SemestersController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new();
        public IActionResult Index()
        {

            var semesterList = _unitOfWork.SemesterRepository.Get(includeProperties: "Course");

            return View(semesterList);
        }
        public IActionResult Details(int id)
        {

            var semester =  _unitOfWork.SemesterRepository.GetById(id);
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

            var semesters = _unitOfWork.SemesterRepository.Get();

            var enumerable = semesters as Semester[] ?? semesters.ToArray();

            if (!enumerable.Any())
            {
                Semester newSemester = new Semester()
                {
                    CourseId = semester.CourseId,
                    CourseSeason = semester.CourseSeason
                };

                _unitOfWork.SemesterRepository.Add(newSemester);
                _unitOfWork.Complete();

                return RedirectToAction(nameof(Create));
            }
            foreach (var row in enumerable)
            {
                if (row != semester)
                {
                    Semester newSemester = new Semester()
                    {
                        CourseId = semester.CourseId,
                        CourseSeason = semester.CourseSeason
                    };

                    _unitOfWork.SemesterRepository.Add(newSemester);
                    _unitOfWork.Complete();

                    return RedirectToAction(nameof(Create));
                }
            } 
            return RedirectToAction(nameof(Create));
        }

            public IActionResult Edit(int id)
        {
            var semester = _unitOfWork.SemesterRepository.GetById(id);
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

            var semester = _unitOfWork.SemesterRepository.GetById(id);
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
            var semester = _unitOfWork.SemesterRepository.GetById(id);
            _unitOfWork.SemesterRepository.Remove(semester);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}