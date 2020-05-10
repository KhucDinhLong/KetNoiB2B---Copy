using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace SETA.Core.Session
{
    public class MongoSessionIdManager : SessionIDManager
    {
        public override string CreateSessionID(HttpContext context)
        {
            return base.CreateSessionID(context);
        }
    }
}
