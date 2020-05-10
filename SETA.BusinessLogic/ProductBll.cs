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
    public class ProductBll : BaseBll, IBusinessLogic<Product>
    {
        public Product Get(long id)
        {
            return SingletonIpl.GetInstance<ProductDal>().Get(id);
        }

        public IList<Product> Get(BaseListParam param, out int? totalRecord)
        {
            return SingletonIpl.GetInstance<ProductDal>().Get(param, out totalRecord);
        }

        public IList<Product> GetProductHomeCarousel()
        {
            return SingletonIpl.GetInstance<ProductDal>().GetProductHomeCarousel();
        }

        public IList<Product> GetProductHomeNewest()
        {
            return SingletonIpl.GetInstance<ProductDal>().GetProductHomeNewest();
        }

        public int GetCountAllProduct()
        {
            return SingletonIpl.GetInstance<ProductDal>().GetCountAllProduct();
        }

        public long Save(Product obj, long userID)
        {
            return SingletonIpl.GetInstance<ProductDal>().Save(obj, userID);
        }

        public long SaveUnicode(Product obj, long userID)
        {
            return SingletonIpl.GetInstance<ProductDal>().SaveUnicode(obj, userID);
        }

        public bool Delete(long objId, long userID)
        {
            return SingletonIpl.GetInstance<ProductDal>().Delete(objId, userID);
        }
    }
}
