using System;
using System.Net;
using System.Web;

namespace SETA.Core.Web
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base()
        {
        }
        public NotFoundException(string msg)
            : base()
        {
            HttpContext.Current.AddError(new HttpException((int)HttpStatusCode.NotFound, msg));
        }

    }
}
