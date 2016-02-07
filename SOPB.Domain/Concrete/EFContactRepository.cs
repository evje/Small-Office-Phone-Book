using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOPB.Domain.Abstract;
using SOPB.Domain.Entities;

namespace SOPB.Domain.Concrete
{
    public class EFContactRepository : IContactRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Contact> Contacts => context.Contacts;

        public void SaveContact(Contact contact)
        {
            if (contact.ContactId == 0)
            {
                context.Contacts.Add(contact);
            }
            else
            {
                Contact dbEntry = context.Contacts.Find(contact.ContactId);
                if (dbEntry != null)
                {
                    dbEntry.LastName = contact.LastName;
                    dbEntry.FirstName = contact.FirstName;
                    dbEntry.City = contact.City;
                    dbEntry.Function = contact.Function;
                    dbEntry.WorkNumber = contact.WorkNumber;
                    dbEntry.WorkEmail = contact.WorkEmail;
                    dbEntry.WorkAdress = contact.WorkAdress;
                    dbEntry.MobileNumber = contact.MobileNumber;
                    dbEntry.HomeNumber = contact.HomeNumber;
                    dbEntry.PersonalEmail = contact.PersonalEmail;
                    dbEntry.PersonalLink = contact.PersonalLink;
                }
            }
            context.SaveChanges();
        }

        public Contact DeleteContact(int contactId)
        {
            Contact dbEntry = context.Contacts.Find(contactId);
            if (dbEntry != null)
            {
                context.Contacts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
