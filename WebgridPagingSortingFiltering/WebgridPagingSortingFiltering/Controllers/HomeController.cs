using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace WebgridPagingSortingFiltering.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int page = 1, string sort = "FirstName", string sortDir = "asc", string search = "")
        {
            int pageSize = 5;
            int totalRecords = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetEmployees(search, sort, sortDir, skip, pageSize, out totalRecords);

            ViewBag.TotalRows = totalRecords;
            ViewBag.search = search;

            return View(data);
        }

        public List<Employee> GetEmployees(string search, string sort, string sortDir, int skip, int pageSize, out int totalRecords)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = (from a in dc.Employees
                         where
                            a.FirstName.Contains(search) ||
                            a.LastName.Contains(search) ||
                            a.EmailId.Contains(search) ||
                            a.City.Contains(search) ||
                            a.Country.Contains(search)
                         select a
                        );

                totalRecords = v.Count();

                v = v.OrderBy(sort + " " + sortDir);

                if(pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }

                return v.ToList();
            }
        }
    }
}