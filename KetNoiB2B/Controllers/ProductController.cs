using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KetNoiB2B.Models.Product;
using SETA.BusinessLogic;
using SETA.Common.Constants;
using SETA.Core.Singleton;
using SETA.Core.Web;
using SETA.Entity;

namespace KetNoiB2B.Controllers
{
    [SetaAuthorize(Roles = RoleGroup.AdminSuper)]
    public class ProductController : BaseController
    {
        // GET: Product
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            //var param = new BaseListParam()
            //{
            //    PageIndex = 1,
            //    PageSize = 10
            //};
            //int? totalRecord;

            //var listProduct = SingletonIpl.GetInstance<ProductBll>().Get(param, out totalRecord).ToList();
            //var model = new IndexModel()
            //{
            //    listProduct = listProduct
            //};

            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult AddCategory()
        {            
            return View(new CategoryModel());
        }

        public ActionResult AddProduct()
        {
            var model = new ProductModel();
            model.StatusID = GlobalStatus.Active;
            model.Currency = 0;
            model.LengthUnit = "cm";
            model.WidthUnit = "cm";
            model.HeightUnit = "cm";
            return View("_Partial/_AddProduct", model);
        }

        public ActionResult EditCategory(long id)
        {
            var categoryEntity = SingletonIpl.GetInstance<CategoryBll>().Get(id);
            var model = new CategoryModel(categoryEntity);
            return View("AddCategory", model);
        }

        public ActionResult EditProduct(long id)
        {            
            var product = SingletonIpl.GetInstance<ProductBll>().Get(id);
            var listImages = SingletonIpl.GetInstance<ProductImageBll>().GetByProductId(id).ToList();
            var model = new ProductModel(product);

            model.Images = listImages;

            return View("_Partial/_AddProduct", model);
        }

        public ActionResult SaveCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = model.GetEntity();
                category.StatusID = GlobalStatus.Active;
                var id = SingletonIpl.GetInstance<CategoryBll>().SaveVietnamese(category, CurrentUser.MemberID);

                return Json(new
                {
                    Success = id > 0,
                    Message = id > 0 ? Constants.MSG_SAVE_SUCCESSFUL : Constants.MSG_SAVE_UNSUCCESSFUL,                    
                });
            }

            return Json(new
            {
                Success = false,
                Message = Constants.MSG_SAVE_UNSUCCESSFUL,
                Errors = ModelState.Where(modelState => modelState.Value.Errors.Count > 0).ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    )
            });
        }
        
        [ValidateInput(false)]
        public ActionResult SaveProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {                
                var product = model.GetEntity();                
                var id = SingletonIpl.GetInstance<ProductBll>().SaveUnicode(product, CurrentUser.MemberID);
                if (model.Images != null && model.Images.Count > 0)
                {
                    var listNewImages = model.Images.Where(m => m.ProductImageID == 0).ToList();

                    var listImagesCurrent = SingletonIpl.GetInstance<ProductImageBll>().GetByProductId(id);
                    if (listImagesCurrent != null && listImagesCurrent.Count > 0)
                    {
                        foreach (var productImage in listImagesCurrent)
                        {
                            var isExists = false;
                            foreach (var image in model.Images)
                            {
                                if (productImage.ProductImageID == image.ProductImageID)
                                {
                                    isExists = true;
                                }
                            }

                            if (!isExists)
                            {
                                var path = string.Format("{0}\\{1}", Server.MapPath(@"\"), productImage.ImageUrl);

                                try
                                {
                                    System.IO.File.Delete(path);
                                }
                                catch (Exception)
                                {                                                                     
                                }
                                
                                SingletonIpl.GetInstance<ProductImageBll>()
                                    .Delete(productImage.ProductImageID, CurrentUser.MemberID);
                            }
                        }
                    }

                    //Save ListImages new to ProductID
                    if (listNewImages != null && listNewImages.Count > 0)
                    {
                        foreach (var image in listNewImages)
                        {
                            var productImage = new ProductImage()
                            {
                                ProductImageID = image.ProductImageID,
                                ProductID = id,
                                FileName = image.FileName,
                                ImageUrl = image.ImageUrl,
                                StatusID = GlobalStatus.Active
                            };
                            SingletonIpl.GetInstance<ProductImageBll>().SaveUnicode(productImage, CurrentUser.MemberID);
                        }
                    }
                }                

                return Json(new
                {
                    Success = id > 0,
                    Message = id > 0 ? Constants.MSG_SAVE_SUCCESSFUL : Constants.MSG_SAVE_UNSUCCESSFUL,
                });
            }

            return Json(new
            {
                Success = false,
                Message = Constants.MSG_SAVE_UNSUCCESSFUL,
                Errors = ModelState.Where(modelState => modelState.Value.Errors.Count > 0).ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                    )
            });
        }

        [HttpPost]
        public ActionResult DeleteCategory(long id)
        {
            var result = SingletonIpl.GetInstance<CategoryBll>().Delete(id, CurrentUser.MemberID);

            return Json(new
            {
                Success = result,
                Message = result ? Constants.MSG_SAVE_SUCCESSFUL : Constants.MSG_SAVE_UNSUCCESSFUL
            });
        }

        [HttpPost]
        public ActionResult DeleteProduct(long id)
        {
            var result = SingletonIpl.GetInstance<ProductBll>().Delete(id, CurrentUser.MemberID);

            return Json(new
            {
                Success = result,
                Message = result ? Constants.MSG_SAVE_SUCCESSFUL : Constants.MSG_SAVE_UNSUCCESSFUL
            });
        }

        public ActionResult GetAllCategory_GridData([DataSourceRequest] DataSourceRequest request)
        {                       
            var param = new BaseListParam()
            {
                PageIndex = 1,
                PageSize = int.MaxValue

            };
            int? totalRecord;
            var categories = SingletonIpl.GetInstance<CategoryBll>().Get(param, out totalRecord);                       

            return Json(categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdownListCategory(long selected = 0)
        {
            var param = new BaseListParam()
            {
                PageIndex = 1,
                PageSize = int.MaxValue

            };
            int? totalRecord;
            var categories = SingletonIpl.GetInstance<CategoryBll>().Get(param, out totalRecord).ToList();

            ViewBag.SelectedCategoryId = selected;
            return View("_Partial/_DropdownCategory", categories);
        }

        public ActionResult GetListProduct(long categoryId = 0, int page = 1)
        {
            var param = new BaseListParam()
            {
                Keyword = categoryId.ToString(),
                PageIndex = page,
                PageSize = int.MaxValue
            };
            int? totalRecord;

            var listProduct = SingletonIpl.GetInstance<ProductBll>().Get(param, out totalRecord).ToList();

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            return View("_Partial/_ListProduct", listProduct);
        }
    }
}