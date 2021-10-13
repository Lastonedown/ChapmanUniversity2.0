using Xunit;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;

using Moq;
using ChapmanUniversity1._0.Controllers;
using ChapmanUniversity1._0.Models;

namespace CourseControllerTest
{
    public class TestCourse
    {
        [Fact]
        public void Test_Create_GET_ReturnsViewResultNullModel()
        {
            // Arrange
            ICourseOperations courseContext = null;
            var controller = new CoursesController(courseContext);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public void Test_Create_POST_InvalidModelState()
        {
            // Arrange
            var r = new CourseViewModel()
            {
                Id = 4,
                CourseName = "Test Four",
                CourseNumber = 59
            };
            var mockCourseOperation = new Mock<ICourseOperations>();
            var mockSemesterOperation = new Mock<ISemesterOperations>();
            mockCourseOperation.Setup(course => course.CreateAsync(It.IsAny<Course>()));
            mockSemesterOperation.Setup(semester => semester.CreateAsync(It.IsAny<Semester>()));
            var controller = new CoursesController(mockCourseOperation.Object,mockSemesterOperation.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = controller.Create(r);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            mockCourseOperation.Verify();
        }
        [Fact]
        public async Task Test_Create_POST_ValidModelState()
        {
            // Arrange

            var r = new Course()
            {
                Id = 4,
                CourseName= "Test Four",
                CourseNumber = 499,
                CourseDescription = "Hello World",
                Credits = 3
            };

            var mockCourseOperation = new Mock<ICourseOperations>();
            mockCourseOperation.Setup(repo => repo.CreateAsync(It.IsAny<Course>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new CoursesController(mockCourseOperation.Object);

            // Act
            var result = await controller.Create(r);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Read", redirectToActionResult.ActionName);
            mockCourseOperation.Verify();
        }
    }
}
