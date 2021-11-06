using ChapmanUniversity1._0.DAL;
using Microsoft.AspNetCore.Mvc;
using ChapmanUniversity1._0.Models;
using ChapmanUniversity1._0.Validators;

namespace ChapmanUniversity1._0.Controllers
{
    public class FacultyController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public FacultyController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Faculty facultyLogin)
        {
            TempData.Remove("InvalidFacultyLogin");
            var facultyMember = FacultyValidator.ValidateFacultyLogin(facultyLogin.FacultyUserName.Trim(), facultyLogin.Password);

            if (facultyMember == null)
            {
                TempData.Add("InvalidFacultyLogin", null);
                return View();
            }
            TempData.Add("FacultyId", facultyMember.Id);
            return RedirectToAction("Details");

        }


        public IActionResult Details()
        {
            var id = (int) TempData["FacultyId"];
            TempData.Keep("FacultyId");

            var facultyMember = _unitOfWork.FacultyMembers.GetById(id);

            return View(facultyMember);
        }
    }
}
