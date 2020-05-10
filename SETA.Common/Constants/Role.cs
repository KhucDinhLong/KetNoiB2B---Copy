namespace SETA.Common.Constants
{
    /// <summary>
    /// Security Levels:
    /// <para>100: Students</para>
    /// <para>200: Teachers</para>
    /// <para>300: School Administrators</para>
    /// <para>400: Admin - Super User</para>
    /// <para>500: Super Admin (God)</para>
    /// </summary>
    public class RoleGroup
    {
        public const string Student = "100";
        public const string Teacher = "200";
        public const string School = "300";
        public const string Admin = "400";
        public const string AdminSuper = "500";
        public const string PrecisePay = "600";
    }

    //for import member
    public class MemberTypeImport
    {
        public const int Admin = 1;
        public const int School = 2;
        public const int Teacher = 3;
        public const int Student = 4;
        public const int Parent = 5;
    }
    public class CategoryGroup
    {
        public const byte LearningCenter = 1;
    }

    public class CategoryType
    {
        public const byte Instrument = 1;
        public const byte Level = 2;
        public const byte Type = 3;
        public const byte Genre = 4;
    }
    public class GlobalStatus
    {
        public const short InActive = 0;
        public const short Active = 1;
        public const short Suspended = 2;
        public const short Deleted = 3;
        //Using for Course
        public const short Reject = 4;
        //Using for Schedule
        public const short Blocked = 5;
        public const short WattingConfirm = 6;
    }

    public class HistoryStatus
    {
        public const short Pending = 0;
        public const short Approved = 1;
        public const short Deleted = 3;
        public const short Rejected = 4;
    }

    public class MessageType
    {
        public const int Notify = 0;
        public const int Message = 1;
        public const int Email = 2;
        public const int All = 3;
    }

    public class MemberGender
    {
        public const short Male = 1;
        public const short Female = 2;
        public const short Other = 3;
    }

    public class MemberMessageStatus
    {
        public const short Deleted = 0;
        public const short Enable = 1;        
    }

    public class NotificationType
    {
        public const int NewAssignment = 1;
        public const int AssignmentReminder = 2;
        public const int StudentConnect = 3;
        public const int PaymentDue = 4;
	}
	
    public class MemberLessonStatus
    {
        public const short NotStart = 0;
        public const short Started = 1;
        public const short Practice = 2;
        public const short Completed = 3;
        public const short InComplete = 4; 
    }

    public class MemberInviteStatus
    {
        public const short InActive = 0;
        public const short Active = 1;
    }

    /// <summary>
    /// Custom Activity type: Define at here
    /// </summary>
    public static class ActivityType
    {
        public const short LogIn = 1;
        public const short LogOut = 2;
        public const short CourseBegin = 3;
        public const short CourseComplete = 4;
        public const short LessonBegin = 5;
        public const short LessonComplete = 6;        

        public const short HomeworkBegin = 7;
        public const short HomeworkComplete = 8;
        public const short Assign = 9;
        public const short HomeworkRemind = 15;

        public const short NewMessage = 10;
        public const short Chat = 11;
        public const short VideoCall = 12;
        public const short Invite = 13;
        public const short UpdateProfile = 14;
        public const short MissCall = 16;

        //admin - learningcenter
        public const short ApproveSong = 17;
        public const short ApproveSkill = 18;
        public const short ApproveInstrument = 19;
        public const short RejectSong = 20;
        public const short RejectSkill = 21;

        //teacher
        public const short AddNewSong = 22;
        public const short AddNewSkill = 23;
        public const short UpdateSong = 24;
        public const short UpdateSkill = 25;

        //admin - dashboard
        public const short NewStudent = 26;
        public const short NewTeacher = 27;
        public const short NewSchool = 30;
        public const short WaitForApprove = 28;

        //Lesson
        public const short LessonDelete = 29;

        public const short UpdateHomework = 31;

        //Member working
        public const short AddMember = 32;
        public const short UpdateMember = 33;
        public const short DeleteMember = 34;

        //Dwolla
        public const short DwollaTranferSuccessfull = 35;

        //Schedule SuNV-added TZ-2868 2016-09-20
        public const short UpdateSchedule = 36;

        //Plan
        public const short CreatePlan = 37;
        public const short PlanWattingForApproval = 38;
        public const short PlanApproved = 39;
        public const short PlanDecline = 40;
    }

    public static class ReportType
    {
        public const short Member = 1;
        public const short Group = 2;
    }
    
    /// <summary>
    /// Define status of Assignment
    /// </summary>
    public static class AssignmentStatus
    {
        public const short PendingSync = 0;
        public const short InProgess = 1;
        public const short Complete = 2;
    }

    public static class AdminDashboardLearningCenter
    {
        public const int PageSizeDashboard = 8;
        public const int PageSizeApproved = 12;
    }

    /// <summary>
    /// using for Table INVITE
    /// </summary>
    public class MemberTypeID
    {
        public const string Student = "0";
        public const string Teacher = "1";
        public const string School = "2";
        public const string Admin = "3";
        public const string SupperAdmin = "4";        
    }   

    public static class ShareWith
    {
        public const short YouOnly = 0;
        public const short YourStudent = 1;
        public const short YourSchool = 2;
        public const short World = 3;
    }

    public static class CourseStatus
    {
        public const short WaitingForApproved = 0;
        public const short Active = 1;
    }

    public static class SMSType
    {
        public const int Chat = 0;
        public const int EditHomeWork = 1;
        public const int AssigHomeWork = 2;
        public const int DueDateNotification = 3;
        public const int ModSMSVerifyOn = 1;
        public const int ModSMSVerifyOff = 0;
    }

    public static class AllVideoLogType
    {
        //Log for video
        public const int VideoPlay = 1;
        public const int VideoPlayCompleted = 2;
        public const int VideoSlowDown = 3;
        public const int VideoSelected = 4;
        public const int VideoPlayTime = 5;
        public const int VideoLoop = 13;

        //Log for Homework
        public const int HomeworkDone = 6;
        public const int HomeworkVideoPlay = 7;        
        public const int HomeworkSelected = 8;
        public const int HomeworkSlowDown = 11;
        public const int HomeworkVideoSelected = 12;

        //for student
        public const int LessonStart = 9;  
        public const int LessonDone = 10;
        
    }

    public static class PricePayment
    {
        public const decimal RegisterTeacherPrice = 1;
        public const decimal RegisterShoolPrice = 1;
        public const decimal RegisterAdminPrice = 500;
        // Next month 
        public const decimal StudentPrice = 4;
        public const decimal TeacherMaxPrice = 200;
        public const decimal SchoolMaxPrice = 500;
        public const decimal NumberSchoolMax = 3;
        public const decimal AdminMaxPrice = 1500;

    }

    public static class BoxConfirmForScheduleType
    {
        public const short OnlyThisSchedule = 1;
        public const short FollowingSchedule = 2;
        public const short Cancel = 3;
    }

    public static class EnvironmentData
    {
        public const string Dev = "Dev";
        public const string Release = "Release";
        public const string Staging = "Staging";
        public const string Demo = "Demo"; //ThangND [2016-10-10] [Add config environment for SMS to phone number]
        public const string Live = "Live";        
    }

    public static class TransactionStatus
    {
        public const short UnProcess = 1;
        public const short Pending = 2;
        public const short Processed = 3;
        public const short Failed = 4;
    }

    public static class DwollaFundingSourceType
    {
        public const string Checking = "checking";
        public const string Savings = "savings";
    }

    public static class DwollaTransferDestinationType
    {
        public const string Account = "Account";
        public const string Customer = "Customer";
        public const string Email = "Email";
        public const string FundingSource = "Funding source";
    }

    public static class DwollaEndPointType
    {
        public const short Account = 1;
        public const short Customer = 2;
        public const short FundingSource = 3;
        public const short Transfer = 4;
        public const short Event = 5;
        public const short Webhook = 6;
    }

    public static class DwollaTransferStatus
    {
        public const string Processed = "processed";
        public const string Pending = "pending";
        public const string Cancelled = "cancelled";
        public const string Failed = "failed";
        public const string Reclaimed = "reclaimed";
    }

    public static class ErrorType
    {
        public const short PlanRecurringReRegister = 1;
        public const short PlanRecurringLinkFail = 2;
    }

    //SuNV add 2016-10-06
    public static class PaymentMethod
    {
        public const short ALL = -1;
        public const short TZ_PAY = 0;
        public const short TZ_Other = 1;
    }

    public static class DailyView
    {
        public const int PageSize = 7;
    }

    public static class GroupCreatedByTypeID
    {
        public const short CreatedByTeacher = 0;
        public const short SyncGroup = 1;
    }

    //Use for table: msg_Message_Chat
    public static class ContactTypeID
    {
        public const short Member = 0;
        public const short Group = 1;
    }

    public static class PlanNotificationStatus
    {
        public const short Notified = 0;
        public const short UnProcess = 1;
        public const short Pending = 2;
        public const short RePlanCompleted = 3;
        public const short Deleted = 4;
    }
    public static class PrecisePayStatus
    {
        public const short UnProcess = 1;
        public const short Approved = 2;
        public const short Decline = 4;
        public const short Updated = 5;
    }

    public static class PaymentStatusId
    {
        public const short Unpaid = 0;
        public const short Paid = 1;
        public const short Processing = 2;
        public const short Cancel = 5;
    }

    public static class BookingStatusId
    {
        public const short Active = 1;
        public const short Processed = 2;
        public const short Failed = 3;
    }

    public static class LessonReminderId
    {
        public const short LessonNormal = 1;
        public const short LessonCancel = 2;
    }

    public static class AchAccountHolderTypeID
    {
        public const short Personal = 1;
        public const short Business = 2;
    }

    public static class AchAccountTypeID
    {
        public const short Checking = 1;
        public const short Savings = 2;
    }
}
