using Microsoft.AspNetCore.Mvc;
using System;

namespace CourseServices
{
    public static class CreateCourse
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel course)
        {
            TempData.Remove("CourseCreatedSuccessfullyAlert");
            TempData.Remove("CourseAlreadyCreatedAlert");

            List<String> seasonList = new List<string>(Enum.GetNames(typeof(Seasons)));
            ViewData["SemesterSeasons"] = new SelectList(seasonList);

            var courseExists = CourseExists(course.CourseNumber);
            var semesterExists = SemesterExists(course.CourseNumber, course.CourseSeason);

            Course newCourse = new Course();
            Semester newSemester = new Semester();

            if (!courseExists && !semesterExists)
            {
                newCourse.CourseNumber = course.CourseNumber;
                newCourse.CourseName = course.CourseName;
                newCourse.CourseDescription = course.CourseDescription;
                newCourse.Credits = course.Credits;

                _context.Add(newCourse);
                await _context.SaveChangesAsync();
                TempData.Add("CourseCreatedSuccessfullyAlert", null);
            }


            if (!semesterExists)
            {
                TempData.Remove("CourseCreatedSuccessfullyAlert");
                var foundCourse = FindCourse(course);
                newSemester.CourseSeason = course.CourseSeason;
                newSemester.Course = foundCourse;

                _context.Add(newSemester);
                await _context.SaveChangesAsync();
                TempData.Add("CourseCreatedSuccessfullyAlert", null);
                return View();
            }


            TempData.Add("CourseAlreadyCreatedAlert", null);
            return View();
        }
    }
}
