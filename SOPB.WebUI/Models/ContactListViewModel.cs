using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOPB.Domain.Entities;

namespace SOPB.WebUI.Models
{
    public class ContactListViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCity { get; set; }

        public string CurrentFunction { get; set; }
    }
}