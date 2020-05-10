using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SETA.BusinessLogic;
using SETA.Core.Singleton;

namespace KetNoiB2B.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(long id)
        {
            var category = SingletonIpl.GetInstance<CategoryBll>().Get(id);
            return View(category);
        }

        public ActionResult ListCategoryLeft(long activeCategoryId = 0)
        {
            var listAllCategory = SingletonIpl.GetInstance<CategoryBll>().GetAllCateWithCountProduct().ToList();
            ViewBag.ActiveCategory = activeCategoryId;
            return View("_Partial/_ListCategoryLeft", listAllCategory);
        }
    }
}