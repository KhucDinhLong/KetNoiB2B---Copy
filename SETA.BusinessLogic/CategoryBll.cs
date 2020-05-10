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
    public class CategoryBll : BaseBll, IBusinessLogic<Category>
    {
        public Category Get(long id)
        {
            return SingletonIpl.GetInstance<CategoryDal>().Get(id);
        }

        public IList<Category> Get(BaseListParam param, out int? totalRecord)
        {
            return SingletonIpl.GetInstance<CategoryDal>().Get(param, out totalRecord);
        }

        public int GetCountAllCategory()
        {
            return SingletonIpl.GetInstance<CategoryDal>().GetCountAllCategory();
        }

        public IList<Category> GetAllCateWithCountProduct()
        {
            return SingletonIpl.GetInstance<CategoryDal>().GetAllCateWithCountProduct();
        }

        public long Save(Category obj, long userID)
        {
            return SingletonIpl.GetInstance<CategoryDal>().Save(obj, userID);
        }

        public long SaveVietnamese(Category obj, long userId)
        {
            return SingletonIpl.GetInstance<CategoryDal>().SaveVietnamese(obj, userId);
        }

        public bool Delete(long objId, long userID)
        {
            return SingletonIpl.GetInstance<CategoryDal>().Delete(objId, userID);
        }
    }
}
