using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using PasswordEncryptDecrypt;

namespace ChapmanUniversity1._0.Controllers
{
    public class FacultyController : Controller
    {
        private readonly SchoolContext _context;

        public FacultyController(SchoolContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Faculty facultyLogin)
        {
            var facultyMember = ValidateFacultyLogin(facultyLogin.FacultyUserName.Trim(), facultyLogin.Password);

            if (facultyMember == null)
            {
                TempData.Add("InvalidFacultyLogin", null);
                return View();
            }
            TempData.Add("FacultyId", facultyMember.Id);
            return RedirectToAction("Details");

        }


        public async Task<IActionResult> Details()
        {
            var id = (int)TempData["FacultyId"];
            TempData.Keep("FacultyId");

            var faculty = await _context.FacultyMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);


        }


        // GET: Faculty/Edit/5

        private bool FacultyExists(string userName)
        {
            return _context.FacultyMembers.Any(e => e.FacultyUserName == userName);
        }

        public Faculty ValidateFacultyLogin(string facultyId, string password)
        {
            var facultyMembers = _context.FacultyMembers.ToList();
            bool isPasswordValid = false;

            foreach (var t in facultyMembers)
            {
                string trimmedFacultyId = t.FacultyUserName.Trim();

                if (trimmedFacultyId.Equals(facultyId) && password == t.Password)
                {
                    isPasswordValid = true;
                }

                if (isPasswordValid)
                {
                    return t;
                }
            }

            return null;
        }
    }
}
