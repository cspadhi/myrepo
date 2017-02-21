using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EFEndToEnd.Business.Models;
using EFEndToEnd.Business.Services;
using EFEndToEnd.Models.JqGrid;
using StructureMap;

namespace EFEndToEnd.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactManagementService _managementService;

        public ContactController(IContactManagementService managementService)
        {
            _managementService = managementService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            CreateDropDown(0);
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _managementService.CreateContact(contact);
                return RedirectToAction("Index");
            }
            CreateDropDown(contact.ContactTypeId);
            return View();
        }

        public ActionResult Edit(int id)
        {
            var contact = _managementService.GetContact(id);
            CreateDropDown(contact.ContactTypeId);
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _managementService.UpdateContact(contact);
                return RedirectToAction("Index");
            }
            CreateDropDown(contact.ContactTypeId);
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_managementService.GetContact(id));
        }

        [HttpPost]
        public ActionResult Delete(Contact contact)
        {
            _managementService.DeleteContact(contact);
            return RedirectToAction("Index");
        }

        private void CreateDropDown(int id)
        {
            ViewData["ContactTypeId"] = _managementService.GetAllContactTypes().ToList();
        }

        public ActionResult GetContacts(JqGridQueryRequest criteria)
        {
            var totalRecords = _managementService.GetTotalContacts();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)criteria.PageSize);
            var data = _managementService.GetContacts(criteria);
            var json = new JqGridData
            {
                page = criteria.PageIndex,
                records = totalRecords,
                total = totalPages,
                rows =
                    (from oneRow in data
                     select new JqGridRowData
                     {
                         id = oneRow.ContactId.ToString(CultureInfo.InvariantCulture),
                         cell = new[]
							{
                                oneRow.ContactId.ToString(CultureInfo.InvariantCulture),
                                oneRow.LastName,
                                oneRow.FirstName,
                                oneRow.ContactId.ToString(CultureInfo.InvariantCulture),
                                oneRow.ContactId.ToString(CultureInfo.InvariantCulture)
							}
                     }
                    ).ToArray()

            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _managementService.Dispose();
            base.Dispose(disposing);
        }

    }
}
