﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SOPB.WebUI.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
            
        }

        public ApplicationRole(string name) : base(name)
        {
            
        }
    }
}