using ChapmanUniversity1._0.Models;
using System;
using ChapmanUniversity1._0.Controllers;
using ChapmanUniversity1._0.Data;
using Xunit;

namespace CourseModelTest
{
    public class UnitTest1
    {

        private readonly SchoolContext _context;

        public UnitTest1(SchoolContext context)
        {
            _context = context;
        }

        [Fact]
        public void InvalidModelTest()
        {

            // Arrange
            var model = new CourseViewModel {CourseName = ""}; // Invalid model
            var controller = new CoursesController(_context);

            // Have to explictly add this
            controller.ModelState.AddModelError("Slug", "Required");

            // Act
            var result = controller.Create(model);

            // Assert etc

           
        }
    }
}
