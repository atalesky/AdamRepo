using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdamT_CodingHW.API;
using AdamT_CodingHW.API.Controllers;

namespace AdamT_CodingHW.API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
