using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChapmanUniversity1._0.Controllers;
using ChapmanUniversity1._0.Data;
using ChapmanUniversity1._0.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CourseTests
{
    public class CourseTest
    {

        [Fact]
        public void Create_TestAddCourseToDatabase()
        {
            // Arrange
            var mockLeagueService = new MockLeagueService().MockGetAll(new List<League>());

            var controller = new LeagueController(mockLeagueService.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            mockLeagueService.VerifyGetAll(Times.Once());

        }
    }

   
}
