using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace SOPB.Domain.Entities
{
    public class Contact
    {
        [HiddenInput(DisplayValue = false)]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Необходимо ввести фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Необходимо ввести должность")]
        public string Function { get; set; }

        public string WorkNumber { get; set; }

        public string WorkEmail { get; set; }

        public string WorkAdress { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public string PersonalEmail { get; set; }

        public string PersonalLink { get; set; }
    }
}
