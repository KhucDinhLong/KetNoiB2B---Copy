using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    public class PaymentReportType
    {
        public const short All = 0;
        public const short History = 1;
        public const short Future = 2;
    }

    public class PaymentReportPlanType
    {
        public const short SinglePayment = 0;
        public const short Monthly = 1;
        public const short Package = 2;
        public const short Days = 3;
        public const short StoreItem = 4;
    }
}
