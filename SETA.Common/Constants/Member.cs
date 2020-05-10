using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    public class MemberNotificationTypeId
    {
        public const short Email = 1;
        public const short Sms = 2;
    }

    public class NotificationSettingTypeId
    {
        public const int Default = 0;
        public const int SystemNotification = 1;

        public const int ChatIndividualMessage = 2;
        public const int ChatGroupMessage = 3;
        public const int ChatDailyReminder = 4;

        public const int PracticeNewAssignment = 5;
        public const int PracticeDailyReminder = 6;

        public const int ScheduleNewLesson = 7;
        public const int ScheduleLessonReminder = 8;

    }
}
