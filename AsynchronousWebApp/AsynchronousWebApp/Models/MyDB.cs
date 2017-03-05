using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AsynchronousWebApp.Models
{
    public class MyDB : DbContext
    {
        public DbSet<Department> Departments { get; set; }
    }
}