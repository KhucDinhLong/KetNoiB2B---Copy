using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    public class ReminderType
    {
        public const int TEXT = 1;
        public const int EMAIL = 2;
        public const int BOTH_TEXT_AND_EMAIL = 3;

    }
    public class FrequencyType
    {
        public const int EVERY_DAY = 1;
        public const int ONCE_A_WEEK = 2;
        public const int BOTH_DAY_AND_WEEK = 3;
    }

    public class FrequencyDay
    {
        public const int SUNDAY = 1;
        public const int MONDAY = 2;
        public const int TUESDAY = 3;
        public const int WEDNESDAY = 4;
        public const int THURSDAY = 5;
        public const int FRIDAY = 6;
        public const int SATURDAY = 7;
    }
    public class CourseType
    {
        public const int MostRecent = 1;
        public const int Recommended=2;
        public const int SONG = 3;
        public const int SKILL = 4;
        public const int Favorite = 5;
        public const int SongInProcess = 6;
        public const int SkillInProcess = 7;
        public const int CourseInComplete = 8;
        public const int CourseComplete = 9;
        public const int CourseHomework = 10;
    }

    public class CommunicationLogType
    {
        public const int SMS = 0;
        public const int Email = 1;
        public const int VideoCall = 2;
    }
    public class VideoCallType
    {
        public const int MissCall = 0;
        public const int AcceptCall = 1;
        public const int DeclineCall = 2;
    }
}
