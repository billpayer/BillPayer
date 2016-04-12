using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BillPayingWebsite;
using BillPayingWebsite.Controllers;

namespace BillPayingWebsite.Tests.Controllers
{
    [TestClass]
    public class BillControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            BillController controller = new BillController();

            // Act
            ViewResult result = controller.Index(6) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            BillController controller = new BillController();

            // Act
            ViewResult result = controller.Details(6,1) as ViewResult;

            Assert.IsNotNull(result);
            // Assert
        }

    }
}
