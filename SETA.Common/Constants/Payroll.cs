using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    public class PayrollAttendanceTypeId
    {
        public const short Attend = 1;
        public const short TeacherCancel = 2;
        public const short StudentCancel = 3;
        public const short NoShow = 4;
        public const short Banked = 5;
        public const short HavingBilling = 6;
    }
    public class PayrollRateType
    {
        public const short Lesson = 1;
        public const short Hourly = 2;        
    }
}
