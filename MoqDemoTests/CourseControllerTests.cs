using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Controllers;
using Xunit;
using Xunit.Sdk;

namespace CourseControllerTest
{
    [TestClass]
    public class CourseControllerTests {
        
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new ProductController();
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);

        }
    }
}