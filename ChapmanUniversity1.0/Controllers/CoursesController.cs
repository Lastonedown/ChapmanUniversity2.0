using System.Linq;
using ChapmanUniversity1._0.DAL;
using Microsoft.AspNetCore.Mvc;
using ChapmanUniversity1._0.Models;


namespace ChapmanUniversity1._0.Controllers
{

    public class CoursesController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new();

        public IActionResult Index()
        {
            TempData.Remove("CourseCreatedSuccessfullyAlert");
            TempData.Remove("CourseAlreadyCreatedAlert");
            var courses =   _unitOfWork.CourseRepository.Get();
           
           return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _unitOfWork.CourseRepository.GetById(id);
            
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
            var courses = _unitOfWork.CourseRepository.Get();

            var courseList= courses as Course[] ?? courses.ToArray();
            if (!courseList.Any())
            {
                _unitOfWork.CourseRepository.Add(course);
                _unitOfWork.Complete();
                TempData.Add("CourseCreatedSuccessfullyAlert", null);
                return RedirectToAction(nameof(Create));
            }

            foreach (var row in courseList)
            {
                if (row.CourseNumber != course.CourseNumber)
                { 
                    _unitOfWork.CourseRepository.Add(course); 
                    _unitOfWork.Complete(); 
                    TempData.Add("CourseCreatedSuccessfullyAlert", null);
                    return RedirectToAction(nameof(Create));
                }
            }
            TempData.Add("CourseAlreadyCreatedAlert", null);
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Edit(int id)
        {
            var course =  _unitOfWork.CourseRepository.GetById(id);
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
            var course = _unitOfWork.CourseRepository.GetById(id);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Course course)
        {

            _unitOfWork.CourseRepository.Remove(course);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

    }
}
