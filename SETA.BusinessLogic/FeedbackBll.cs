using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETA.BusinessLogic.Interface;
using SETA.Core.Base;
using SETA.Core.Singleton;
using SETA.DataAccess;
using SETA.Entity;

namespace SETA.BusinessLogic
{
    public class FeedbackBll : BaseBll, IBusinessLogic<Feedback>
    {
        public Feedback Get(long id)
        {
            return SingletonIpl.GetInstance<FeedbackDal>().Get(id);
        }

        public IList<Feedback> Get(BaseListParam listParam, out int? totalRecord)
        {
            return SingletonIpl.GetInstance<FeedbackDal>().Get(listParam, out totalRecord);
        }

        public long Save(Feedback obj, long userID)
        {
            return SingletonIpl.GetInstance<FeedbackDal>().Save(obj, userID);
        }

        public bool Delete(long objId, long userID)
        {
            return SingletonIpl.GetInstance<FeedbackDal>().Delete(objId, userID);
        }
    }
}
