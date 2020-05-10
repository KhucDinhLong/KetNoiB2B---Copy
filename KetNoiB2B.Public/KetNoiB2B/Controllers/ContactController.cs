using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KetNoiB2B.Models.Contact;
using SETA.BusinessLogic;
using SETA.Common.Constants;
using SETA.Core.Singleton;

namespace KetNoiB2B.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveFeedback(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = model.GetEntity();
                feedback.FeedbackID = SingletonIpl.GetInstance<FeedbackBll>().Save(feedback, 0);

                return Json(new
                {
                    Success = feedback.FeedbackID > 0,
                    Message = feedback.FeedbackID > 0 ? Constants.MSG_SAVE_SUCCESSFUL : Constants.MSG_SAVE_UNSUCCESSFUL,
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
    }
}