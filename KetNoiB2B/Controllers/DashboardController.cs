using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SETA.Core.Web;

namespace KetNoiB2B.Controllers
{
    [SetaAuthorize()]
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var a = CurrentUser.MemberID;
            return View();
        }

        [AllowAnonymous]
        public ActionResult CheckAuthorize()
        {
            if (CurrentUser == null)
            {
                //timeout
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //pass
                return Json("1", JsonRequestBehavior.AllowGet);
            }
        }
    }
}