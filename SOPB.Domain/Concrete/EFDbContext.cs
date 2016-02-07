using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOPB.Domain.Entities;
using System.Data.Entity;

namespace SOPB.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}
