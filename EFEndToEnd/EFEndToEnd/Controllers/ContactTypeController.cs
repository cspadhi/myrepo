using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EFEndToEnd.Business.Models;
using EFEndToEnd.Models.JqGrid;
using EFEndToEnd.Business.Services;
using StructureMap;

namespace EFEndToEnd.Controllers
{
    public class ContactTypeController : Controller
    {
        private readonly IContactManagementService _managementService;

        public ContactTypeController(IContactManagementService managementService)
        {
            _managementService = managementService;
        }

        //
        // GET: /ContactType/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new ContactType());
        }

        [HttpPost]
        public ActionResult Create(ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                _managementService.CreateContactType(contactType);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(_managementService.GetContactType(id));
        }

        [HttpPost]
        public ActionResult Edit(ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                _managementService.UpdateContactType(contactType);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_managementService.GetContactType(id));
        }

        [HttpPost]
        public ActionResult Delete(ContactType contactType)
        {
            _managementService.DeleteContactType(contactType);
            return RedirectToAction("Index");
        }

        public ActionResult GetTypes(JqGridQueryRequest criteria)
        {
            var totalRecords = _managementService.GetTotalContactTypes();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)criteria.PageSize);
            var data = _managementService.GetContactTypes(criteria);
            var json = new JqGridData
            {
                page = criteria.PageIndex,
                records = totalRecords,
                total = totalPages,
                rows =
                    (from oneRow in data
                     select new JqGridRowData
                     {
                         id = oneRow.ContactTypeId.ToString(CultureInfo.InvariantCulture),
                         cell = new[]
							{
                                oneRow.ContactTypeId.ToString(CultureInfo.InvariantCulture),
                                oneRow.Name,
                                oneRow.ContactTypeId.ToString(CultureInfo.InvariantCulture),
                                oneRow.ContactTypeId.ToString(CultureInfo.InvariantCulture)
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
