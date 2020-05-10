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
    public class ProductController : Controller
    {
        public ActionResult GetProductHomeCarousel()
        {
            var listProduct = SingletonIpl.GetInstance<ProductBll>().GetProductHomeCarousel().ToList();

            return View("_Partial/_GetProductHomeCarousel", listProduct);
        }

        public ActionResult GetProductHomeFeature()
        {
            var listProduct = SingletonIpl.GetInstance<ProductBll>().GetProductHomeNewest().ToList();
            ViewBag.ProductFeatured = listProduct.FirstOrDefault();

            return View("_Partial/_GetProductHomeFeature", listProduct);
        }

        public ActionResult GetProductByCategoryId(long categoryId, int pageNum, int pageSize)
        {
            var param = new BaseListParam()
            {
                Keyword = categoryId.ToString(),
                PageIndex = pageNum,
                PageSize = pageSize
            };
            int? totalRecord;

            var listProduct = SingletonIpl.GetInstance<ProductBll>().Get(param, out totalRecord).ToList();

            ViewBag.Total = totalRecord;
            ViewBag.Page = pageNum;
            ViewBag.CategoryId = categoryId;
            return View("_Partial/_GetProductByCategoryId", listProduct);
        }

        public ActionResult GetProductHomePageNewest(int pageNum, int pageSize)
        {
            var param = new BaseListParam()
            {                
                PageIndex = pageNum,
                PageSize = pageSize
            };
            int? totalRecord;

            var listProduct = SingletonIpl.GetInstance<ProductBll>().Get(param, out totalRecord).ToList();

            ViewBag.Total = totalRecord;
            ViewBag.Page = pageNum;
            return View("_Partial/_GetProductHomePageNewest", listProduct);
        }

        public ActionResult GetListImageProductDetail(long productId)
        {
            var listImage = SingletonIpl.GetInstance<ProductImageBll>().GetByProductId(productId).ToList();
            return View("_Partial/_GetListImageProductDetail", listImage);
        }

        public ActionResult Detail(long id)
        {
            var product = SingletonIpl.GetInstance<ProductBll>().Get(id);
            return View(product);
        }
    }
}