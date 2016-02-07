using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOPB.Domain.Entities;

namespace SOPB.Domain.Abstract
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Contacts { get; }

        void SaveContact(Contact contact);

        Contact DeleteContact(int contactId);
    }
}
