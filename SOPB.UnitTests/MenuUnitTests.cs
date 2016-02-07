using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SOPB.Domain.Abstract;
using SOPB.Domain.Concrete;
using SOPB.Domain.Entities;
using SOPB.WebUI.Models;
using SOPB.WebUI.HtmlHelpers;
using SOPB.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;

namespace SOPB.UnitTests
{
    [TestClass]
    public class MenuUnitTests
    {
        [TestMethod]
        public void Can_Create_Cities()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1", City = "Минск", Function = "директор"},
                new Contact {ContactId = 2, LastName = "2", City = "Минск", Function = "бухгалтер"},
                new Contact {ContactId = 3, LastName = "3", City = "Москва", Function = "директор"},
                new Contact {ContactId = 4, LastName = "4", City = "Москва", Function = "бухгалтер"},
                new Contact {ContactId = 5, LastName = "5", City = "Саратов", Function = "уборщик"}
            }.AsQueryable());
            MenuController controller = new MenuController(mock.Object);

            //act
            string[] result = ((IEnumerable<string>)controller.SortByCity().Model).ToArray();

            //assert
            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual(result[0], "Минск");
            Assert.AreEqual(result[1], "Москва");
            Assert.AreEqual(result[2], "Саратов");
        }

        [TestMethod]
        public void Can_Create_Functions()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1", City = "Минск", Function = "директор"},
                new Contact {ContactId = 2, LastName = "2", City = "Минск", Function = "бухгалтер"},
                new Contact {ContactId = 3, LastName = "3", City = "Москва", Function = "директор"},
                new Contact {ContactId = 4, LastName = "4", City = "Москва", Function = "бухгалтер"},
                new Contact {ContactId = 5, LastName = "5", City = "Саратов", Function = "уборщик"}
            }.AsQueryable());
            MenuController controller = new MenuController(mock.Object);

            //act
            string[] result = ((IEnumerable<string>)controller.SortByFunction().Model).ToArray();

            //assert
            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual(result[0], "бухгалтер");
            Assert.AreEqual(result[1], "директор");
            Assert.AreEqual(result[2], "уборщик");
        }
    }
}
