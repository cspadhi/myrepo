using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCFAuthenticationClient.Models;

namespace WCFAuthenticationClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HelloClient hc = new HelloClient();
            ViewBag.Result = hc.HelloWorld() ?? "Access Denied";

            return View();
        }
    }
}