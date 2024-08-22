using QLNSFAHASA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNSFAHASA.Controllers
{
    public class HomeController : Controller
    {
        private QLNSFAHASAEntities db = new QLNSFAHASAEntities();
        [ChildActionOnly]
        public ActionResult RenderMenu()
        {

            return PartialView("_Menu", db.SanPhams.ToList());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}