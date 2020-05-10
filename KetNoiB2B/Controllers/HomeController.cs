using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetNoiB2B.Models.Home;
using SETA.Core.Web;

namespace KetNoiB2B.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                //return RedirectToAction("Index", "Dashboard");
                var model = new LoginModel();
                return View(model);
            }
            else
            {
                var model = new LoginModel();
                return View(model);
            }
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