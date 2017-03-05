using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFAuthenticationClient.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string Email { get; set; } // example, not necessary
    }
}