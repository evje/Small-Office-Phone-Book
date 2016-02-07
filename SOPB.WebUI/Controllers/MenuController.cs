using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOPB.Domain.Abstract;

namespace SOPB.WebUI.Controllers
{
    public class MenuController : Controller
    {
        private IContactRepository repository;

        public MenuController(IContactRepository repo)
        {
            repository = repo;
        }
        
        //
        // GET: /Menu/
        public PartialViewResult PossibleOperations()
        {

            return PartialView();
        }

        //
        // GET: /SortByCity/
        public ViewResult SortByCity()
        {
            IEnumerable<string> cities = repository.Contacts.Select(x => x.City).Distinct().OrderBy(x => x);
            return View(cities);
        }

        //
        // GET: /SortByFunction/
        public ViewResult SortByFunction()
        {
            IEnumerable<string> functions = repository.Contacts.Select(y => y.Function).Distinct().OrderBy(y => y);
            return View(functions);
        }

        //
        // GET: /FindByLastName/
        [Authorize]
        public ViewResult FindByLastName()
        {
            return View();
        }
    }
}