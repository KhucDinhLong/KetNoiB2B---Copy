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
    public class ProductImageBll : BaseBll, IBusinessLogic<ProductImage>
    {
        public ProductImage Get(long id)
        {
            return SingletonIpl.GetInstance<ProductImageDal>().Get(id);
        }

        public IList<ProductImage> Get(BaseListParam param, out int? totalRecord)
        {
            return SingletonIpl.GetInstance<ProductImageDal>().Get(param, out totalRecord);
        }

        public IList<ProductImage> GetByProductId(long productId)
        {
            return SingletonIpl.GetInstance<ProductImageDal>().GetByProductId(productId);
        }

        public long Save(ProductImage obj, long userID)
        {
            return SingletonIpl.GetInstance<ProductImageDal>().Save(obj, userID);
        }

        public long SaveUnicode(ProductImage obj, long userID)
        {
            return SingletonIpl.GetInstance<ProductImageDal>().SaveUnicode(obj, userID);
        }

        public bool Delete(long objId, long userID)
        {
            return SingletonIpl.GetInstance<ProductImageDal>().Delete(objId, userID);
        }

        public bool DeleteByProductId(long productId, long userID)
        {
            return SingletonIpl.GetInstance<ProductImageDal>().DeleteByProductId(productId, userID);
        }
    }
}
