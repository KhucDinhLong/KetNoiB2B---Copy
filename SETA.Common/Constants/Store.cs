using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    public class Store
    {
        public const int OnTimePayment = 0;
        public const int RecurringBilling = 1;        
    }

    public class StoreSource
    {
        public const short Widget = 1;
        public const short Profile = 2;
        public const short Booking = 3;
        public const short ServiceBooking = 4;
    }

    public class StoreGridViewType
    {
        public const short Profile = 0;
        public const short Main = 1;        
    }
}
