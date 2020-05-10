using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    public class TeacherScheduleType
    {
        public const int OpenTime = 1;
        public const int BlockOffTime = 2;
    }

    public class AttendanceReportStatus
    {
        public const int Undefined = 0;
        public const int Attend = 1;
        public const int TeacherCancel = 2;
        public const int StudentCancel = 3;
        public const int NoShow = 4;
    }

    public class BankLessonStatus
    {
        public const int TeacherCancel = 1;
        public const int StudentCancel = 2;
    }

    public class RepeatScheduleType
    {
        public const short NoEndDate = 1;
        public const short HasEndDate = 2;
    }

    public class StudentScheduleUserType
    {
        public const short Student = 1;
        public const short Group = 2;
    }
}
