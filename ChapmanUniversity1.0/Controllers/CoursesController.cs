using System.Linq;
using ChapmanUniversity1._0.DAL;
using Microsoft.AspNetCore.Mvc;
using ChapmanUniversity1._0.Models;


namespace ChapmanUniversity1._0.Controllers
{

    public class CoursesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CoursesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            TempData.Remove("CourseCreatedSuccessfullyAlert");
            TempData.Remove("CourseAlreadyCreatedAlert");
            var courses =   _unitOfWork.Courses.Get().ToList();
           
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

            var courseExists = Validators.CourseValidator.Validate(course.CourseNumber);

            if (!courseExists)
            { 
                _unitOfWork.Courses.Add(course); 
                _unitOfWork.Complete(); 
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
