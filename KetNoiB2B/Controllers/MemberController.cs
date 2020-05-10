using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetNoiB2B.Models.Home;
using Microsoft.AspNet.Identity;
using SETA.BusinessLogic;
using SETA.Core.SecurityServices;
using SETA.Core.Singleton;
using SETA.Core.Web;
using SETA.Entity;

namespace KetNoiB2B.Controllers
{
    public class MemberController : BaseController
    {
        // GET: Member
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult SignIn(LoginModel model)
        {
            try
            {
                if (SecurityService.Login(model.UserName, model.Password, true))
                {                                        
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }

                return Json(string.Format(SETA.Common.Constants.Constants.MSG_LOGIN_FAIL, model.UserName.Contains("@") ? "Email" : "UserName"),
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(string.Format(SETA.Common.Constants.Constants.MSG_LOGIN_FAIL, model.UserName.Contains("@") ? "Email" : "UserName"),
                    JsonRequestBehavior.AllowGet);
            }
        }
    }
}