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
    public class PagingUnitTests
    {
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //arrange
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrenPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegae = i => "Page" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegae);

            //assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>" + @"<a class=""selected"" href=""Page2"">2</a>" + @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1"},
                new Contact {ContactId = 2, LastName = "2"},
                new Contact {ContactId = 3, LastName = "3"},
                new Contact {ContactId = 4, LastName = "4"},
                new Contact {ContactId = 5, LastName = "5"}
            }.AsQueryable());
            ContactController controller = new ContactController(mock.Object);

            //act
            ContactListViewModel result = (ContactListViewModel)controller.List(null, null, 3, 2).Model;

            //assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrenPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1"},
                new Contact {ContactId = 2, LastName = "2"},
                new Contact {ContactId = 3, LastName = "3"},
                new Contact {ContactId = 4, LastName = "4"},
                new Contact {ContactId = 5, LastName = "5"}
            }.AsQueryable());
            ContactController controller = new ContactController(mock.Object);

            //act
            ContactListViewModel result = (ContactListViewModel)controller.List(null, null, 3, 2).Model;

            //assert
            Contact[] contArray = result.Contacts.ToArray();
            Assert.IsTrue(contArray.Length == 2);
            Assert.AreEqual(contArray[0].LastName, "4");
            Assert.AreEqual(contArray[1].LastName, "5");
        }

        [TestMethod]
        public void Can_Filter_Contacts()
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
            ContactController controller = new ContactController(mock.Object);

            //act
            Contact[] result = ((ContactListViewModel)controller.List("Минск", "бухгалтер", 3, 1).Model).Contacts.ToArray();

            //assert
            Assert.IsTrue(result.Length == 1);
            Assert.AreEqual(result[0].LastName, "2");
            Assert.AreEqual(result[0].City, "Минск");
        }

        [TestMethod]
        public void Generate_City_Specified_Contact_Count()
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
            ContactController controller = new ContactController(mock.Object);

            //act
            int res1 = ((ContactListViewModel)controller.List("Минск").Model).PagingInfo.TotalItems;
            int res2 = ((ContactListViewModel)controller.List("Москва").Model).PagingInfo.TotalItems;
            int res3 = ((ContactListViewModel)controller.List("Саратов").Model).PagingInfo.TotalItems;
            int resAll = ((ContactListViewModel)controller.List().Model).PagingInfo.TotalItems;

            //assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        public void Generate_Function_Specified_Contact_Count()
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
            ContactController controller = new ContactController(mock.Object);

            //act
            int res1 = ((ContactListViewModel)controller.List(null, "директор").Model).PagingInfo.TotalItems;
            int res2 = ((ContactListViewModel)controller.List(null, "бухгалтер").Model).PagingInfo.TotalItems;
            int res3 = ((ContactListViewModel)controller.List(null, "уборщик").Model).PagingInfo.TotalItems;
            int resAll = ((ContactListViewModel)controller.List().Model).PagingInfo.TotalItems;

            //assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        public void ListAllForCRUD_Contains_All_Contacts()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1"},
                new Contact {ContactId = 2, LastName = "2"},
                new Contact {ContactId = 3, LastName = "3"},
                new Contact {ContactId = 4, LastName = "4"},
                new Contact {ContactId = 5, LastName = "5"}
            }.AsQueryable());
            ContactController controller = new ContactController(mock.Object);

            //act
            Contact[] result = ((IEnumerable<Contact>)controller.ListAllForCRUD().ViewData.Model).ToArray();

            //assert
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual("5", result[4].LastName);
            Assert.AreEqual("3", result[2].LastName);
            Assert.AreEqual("1", result[0].LastName);
        }

        [TestMethod]
        public void Can_Edit_Contact()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1"},
                new Contact {ContactId = 2, LastName = "2"},
                new Contact {ContactId = 3, LastName = "3"},
                new Contact {ContactId = 4, LastName = "4"},
                new Contact {ContactId = 5, LastName = "5"}
            }.AsQueryable());
            ContactController controller = new ContactController(mock.Object);

            //act
            Contact c1 = controller.Edit(1).ViewData.Model as Contact;
            Contact c2 = controller.Edit(3).ViewData.Model as Contact;
            Contact c3 = controller.Edit(5).ViewData.Model as Contact;

            //assert
            Assert.AreEqual(1, c1.ContactId);
            Assert.AreEqual(3, c2.ContactId);
            Assert.AreEqual(5, c3.ContactId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Contact()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1"},
                new Contact {ContactId = 2, LastName = "2"},
                new Contact {ContactId = 3, LastName = "3"},
                new Contact {ContactId = 4, LastName = "4"},
                new Contact {ContactId = 5, LastName = "5"}
            }.AsQueryable());
            ContactController controller = new ContactController(mock.Object);

            //act
            Contact result = (Contact)controller.Edit(6).ViewData.Model;

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            ContactController controller = new ContactController(mock.Object);
            Contact contact = new Contact { LastName = "Test" };

            //act
            ActionResult result = controller.Edit(contact);

            //assert
            mock.Verify(c => c.SaveContact(contact));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            //arrange
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            ContactController controller = new ContactController(mock.Object);
            Contact contact = new Contact { LastName = "Test" };
            controller.ModelState.AddModelError("error", "error");

            //act
            ActionResult result = controller.Edit(contact);

            //assert
            mock.Verify(c => c.SaveContact(It.IsAny<Contact>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Contact()
        {
            //arrange
            Contact contact = new Contact { ContactId = 2, LastName = "2" };
            Mock<IContactRepository> mock = new Mock<IContactRepository>();
            mock.Setup(m => m.Contacts).Returns(new Contact[]
            {
                new Contact {ContactId = 1, LastName = "1"},
                contact,
                new Contact {ContactId = 3, LastName = "3"},
            }.AsQueryable());
            ContactController controller = new ContactController(mock.Object);

            //act
            controller.Delete(contact.ContactId);

            //assert
            mock.Verify(m => m.DeleteContact(contact.ContactId));
        }
    }
}
