using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DatatableCRUD.Models;

namespace DatatableCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
            using (DatatableCRUDEntities db = new DatatableCRUDEntities())
            {
                var employees = db.Employees.OrderBy(e => e.FirstName).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (DatatableCRUDEntities db = new DatatableCRUDEntities())
            {
                var employee = db.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
                return View(employee);
            }
        }

        [HttpPost]
        public ActionResult Save(Employee employee)
        {
            bool status = false;
            if(ModelState.IsValid)
            {
                using (DatatableCRUDEntities db = new DatatableCRUDEntities())
                {
                    if (employee.EmployeeId != 0)
                    {
                        db.Entry(employee).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Employees.Add(employee);
                    }

                    db.SaveChanges();
                    status = true;
                }
            }

            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (DatatableCRUDEntities db = new DatatableCRUDEntities())
            {
                var employee = db.Employees.OrderBy(e => e.EmployeeId == id).FirstOrDefault();
                if(employee != null)
                {
                    return View(employee);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;

            using (DatatableCRUDEntities db = new DatatableCRUDEntities())
            {
                Employee emp = db.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
                if(emp != null)
                {
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                    status = true;
                }
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}