using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SETA.BusinessLogic;
using SETA.Core.Singleton;
using SETA.Entity;

namespace KetNoiB2B.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var countAllProduct = SingletonIpl.GetInstance<ProductBll>().GetCountAllProduct();
            //var countAllCategory = SingletonIpl.GetInstance<CategoryBll>().GetCountAllCategory();
            //ViewBag.CountAllProduct = countAllProduct;
            //ViewBag.CountAllCategory = countAllCategory;
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

        public ActionResult GetHeaderMenu()
        {
            var param = new BaseListParam()
            {
                PageIndex = 1,
                PageSize = int.MaxValue

            };
            int? totalRecord;
            var categories = SingletonIpl.GetInstance<CategoryBll>().Get(param, out totalRecord);

            return View("_Partial/_HeaderMenu", categories);
        }
    }
}