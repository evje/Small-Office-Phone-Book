using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using SOPB.Domain.Abstract;
using SOPB.Domain.Concrete;
using SOPB.Domain.Entities;
using SOPB.WebUI.Models;


namespace SOPB.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository repository;

        public ContactController(IContactRepository contactRepository)
        {
            this.repository = contactRepository;
        }

        // GET: Contact
        public ViewResult List(string city = null, string function = null, int items = 3, int page = 1)
        {
            ContactListViewModel model = new ContactListViewModel
            {

                Contacts = repository.Contacts
                .Where(c => city == null || c.City == city).Where(c => function == null || c.Function == function)
                .OrderBy(c => c.ContactId).Skip((page - 1) * items).Take(items),
                PagingInfo = new PagingInfo
                {
                    CurrenPage = page,
                    ItemsPerPage = items,
                    TotalItems = (city == null & function == null) ? repository.Contacts.Count() :
                    repository.Contacts.Where(e => city == null || e.City == city).Count(e => function == null || e.Function == function)
                },
                CurrentCity = city,
                CurrentFunction = function
            };
            if (city == null & function == null)
            {
                return View(model);
            }
            else return View("ListByCityOrFunction", model);
        }

        //GET: /Contact/ListAllForCRUD
        [Authorize(Roles = "admin")]
        public ViewResult ListAllForCRUD()
        {
            return View(repository.Contacts);
        }

        // GET: /Contact/Create
        [Authorize(Roles = "admin")]
        public ViewResult Create()
        {
            return View("Edit", new Contact());
        }

        // GET: /Contact/Edit
        [Authorize(Roles = "admin")]
        public ViewResult Edit(int contactId)
        {
            Contact contact = repository.Contacts.FirstOrDefault(c => c.ContactId == contactId);
            return View(contact);
        }

        // POST: /Contact/Edit
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                repository.SaveContact(contact);
                TempData["message"] = string.Format("{0} {1} {2} в {3} был сохранен", contact.Function, contact.LastName, contact.FirstName, contact.City);

                return RedirectToAction("ListAllForCRUD");
            }
            else
            {
                return View(contact);
            }
        }

        //
        // POST: /Contact/Delete
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int contactId)
        {
            Contact deletedContact = repository.DeleteContact(contactId);
            if (deletedContact != null)
            {
                TempData["message"] = string.Format("{0} {1} {2} в {3} был удален", deletedContact.Function, deletedContact.LastName, deletedContact.FirstName, deletedContact.City);
            }
            return RedirectToAction("ListAllForCRUD");
        }

        // /Contact/ListByName
        [Authorize]
        public ActionResult ListByName(string lastName)
        {
            ContactListViewModel model = new ContactListViewModel
            {

                Contacts = repository.Contacts
                .Where(c => lastName == null || c.LastName == lastName).OrderBy(c => c.LastName),
                PagingInfo = new PagingInfo
                {
                    CurrenPage = 1,
                    ItemsPerPage = 3,
                    TotalItems = repository.Contacts.Where(e => lastName == null || e.LastName == lastName).Count()
                },
                CurrentCity = null,
                CurrentFunction = null
            };
            if (lastName != "")
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("FindByLastName", "Menu");
            }
        }

        public ActionResult AutocompleteSearch(string term)
        {
            var lastNames = repository.Contacts.Where(a => a.LastName.Contains(term)).Select(a => new { value = a.LastName }).Distinct();

            return Json(lastNames, JsonRequestBehavior.AllowGet);
        }

    }
}