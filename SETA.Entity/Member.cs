using System;
using System.Collections.Generic;
using SETA.Common.Constants;
using SETA.Common.Enums;
using SETA.Common.Helper;

namespace SETA.Entity
{
    [Serializable]
    public class Member : AuditableEntity
    {
        private string isoCountry = Common.Utility.Utils.GetSetting(AppKeys.SMS_DEFAULT_ISO_COUNTRY, "us");
        private int phoneCode = Common.Utility.Utils.GetSetting(AppKeys.SMS_DEFAULT_PHONE_CODE, 1);
        public Member()
        {
            //!Todo ....
            //this.AssignedCourses = new HashSet<AssignedCourses>();
            //this.Courses = new HashSet<Courses>();
            //this.MemberInRoles = new HashSet<MemberInRoles>();
            this.Phonecode = this.phoneCode;
            this.IsoCountry = this.isoCountry;
        }

        public long MemberID { get; set; }
        public long MemberViewID { get; set; }
        public string MemberViewName { get; set; }
        public bool IsParent { get; set; }
        public int ViewType { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int? RoleID { get; set; }
        //public int? GroupID { get; set; }
        public short? Status { get; set; }
        public DateTime? BirthDay { get; set; }
        public string BirthDayString { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string ThumbnailUrl { get; set; }

        public byte? StatusNotification { get; set; }
        public Guid ForgotCode { get; set; }
        public DateTime ForgotExpired { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public short? GenderID { get; set; }
        public long? AddByTeacherID { get; set; }

        public string Country { get; set; }
        public int? Login { get; set; } //for display
        public string Address { get; set; } //for display
        public string Phone { get; set; } //for display
        public string PayType { get; set; } //for display
        public string ParentUserName { get; set; }
        public string ParentEmail { get; set; }
        public short MemStatus { get; set; }
        public int CountUnread { get; set; }
        public int TimeZoneID { get; set; }
        public string LevelInstrument { get; set; }
        public DateTime LastConversation { get; set; }
        public string MainDetailLevel { get; set; }
        public DateTime? LastDateConversation { get; set; }
        public string OriginTeacherEmail { get; set; } //used for session
        public string OriginTeacherUserName { get; set; } //used for session
        public int CountInvited { get; set; }
        public int CountStudent { get; set; }
        public int CountProvides { get; set; }
        public string Instruments { get; set; }

        public long? SchoolID { get; set; }
        public DateTime? EnrollDate { get; set; }
        public string EnrollDateString { get; set; }
        //report
        public int HomeworkFinished { get; set; }
        public int HomeworkNext { get; set; }
        public int HomeworkOverdue { get; set; }
        //public Guid? CreatedBy { get; set; }

        //Feedback: TZ-1418
        public string MemberSchoolID { get; set; }    //memberID(user School)
        public int IsChangeSchool { get; set; }
        public string MemberAdminID { get; set; }    //memberID(user admin)

        public string ListSchool { get; set; }
        public int VerifySMS { get; set; }
        public long VerifyID { get; set; }
        public int NotifyBy { get; set; }
        public string Code { get; set; }
        public string IsoCountry { get; set; }
        public int Phonecode { get; set; }
        public string ClientTime { get; set; }
        public string ClientDate { get; set; }
        public TimeSpan ReminderTime { get; set; }
        public int FrequencyType { get; set; }
        public string StrFrequencyData { get; set; }

        //Student Schedule
        public int? LengthOfTime { get; set; }
        public long PlanID { get; set; }
        public long ScheduleTypeID { get; set; }
        public int LessonPackage { get; set; }
        public string PlanName { get; set; }

        public int? FrequencyDay { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? FrequencyTypeID { get; set; }

        public Guid PlanVerifyCode { get; set; }
        public String FullName { get; set; }
        
        //bank lesson
        public DateTime BankedDate { get; set; }
        public int BankStatusID { get; set; }
        public int BankTotal { get; set; }
        public string BankDetail { get; set; }
        public int MinLengthOfTime { get; set; }
        //public virtual ICollection<AssignedCourses> AssignedCourses { get; set; }
        //public virtual ICollection<Courses> Courses { get; set; }
        //public virtual ICollection<mem_MemberInRoles> mem_MemberInRoles { get; set; }

        //ThangND [TZ-2952: Scheduling of Groups] 2016-09-29
        //extend for group column
        public long GroupID { get; set; }
        public short UserType { get; set; }
        //end

        public short ContactTypeID { get; set; }

        public string ProcessorID { get; set; }
        public long PaymentHistoryID { get; set; }
        public long MemberBillingInfoID { get; set; }
        public int? TotalPaid { get; set; }
        public int? TotalUnPaid { get; set; }

        public int? PromoPercentage { get; set; }
        public DateTime? StartBillingDate { get; set; }

        //booking
        public long? BookingID { get; set; }
        public long? BookingPlanID { get; set; }
        public string BookingPlanName { get; set; }
        public DateTime? BookingStartBillingDate { get; set; }
        public bool IsNotShowStudentInfo { get; set; }
        //public IList<BookingPaymentPlan> ListPlanBooked { get; set; }

        //payroll
        public short RateTypeID { get; set; }
        public decimal Rate { get; set; }
        public int? CurrentRoleID { get; set; }
        public bool MassTurnOffNotifications { get; set; }

        public int Age { get; set; }
        public string CalAddress { get; set; }
        public string InstrumentNameLevel { get; set; }
        public string ListTeacherInvite { get; set; }
        public string TimeZone { get; set; }

        public short StatusID { get; set; }
        public short RecurringMethodID { get; set; }

        public string SchoolName { get; set; }
        public string Grade { get; set; }

        public string GenderName { get; set; }

        public string CreditCardExists { get; set; }

        public string StatusName { get; set; }
        public string ListRecurringMethodID { get; set; }
        public string ListPlanName28Days { get; set; }
        public string ListPlanNameMonthly { get; set; }
        public string ListPlanNamePackage { get; set; }
        public string ListPlanNameOneTime { get; set; }

        public bool IsUnassignedOfSchool { get; set; }

        //for student report
        public string MemberName { get; set; }

        public long StudentId { get; set; }
        public string SourceFree { get; set; }
        public int? IsHasPlan { get; set; }
        public DateTime? LastCreatedCard { get; set; }
        public string LastCreatedCardFormatted { get; set; }

        public DateTime? InActiveDate { get; set; }
        public long MemberParentID { get; set; }

        public string ListStrStudentID { get; set; }
        public string ListStrTeacherID { get; set; }
        public string ListStrSchoolID { get; set; }
        public string ListStrAdminID { get; set; }
        public string ListIntrusments { get; set; }


        public long StopWatch { get; set; }
        public long NextTimeWarning{ get; set; }
        public int StopWatchIsRunning { get; set; }
        public int StopWatchIsPause { get; set; }
        public int StopWatchIsStop { get; set; }
        public long MemberTimerLogID { get; set; }
        public int IsTimerConfirmPause { get; set; }
        public DateTime? StartTimer { get; set; }

        /*** USER GROUP ***/
        #region User Group...

        //public GroupInfo UserGroup { get; set; }

        //// Get Group of CurrentUser
        //public string UserGroupName
        //{
        //    get
        //    {
        //        return UserGroup != null && !string.IsNullOrEmpty(UserGroup.GroupName)
        //            ? UserGroup.GroupName
        //            : string.Empty;
        //    }
        //}

        //// Check Is In UserGroup
        //public bool IsInGroup(string groupId)
        //{

        //    return GroupID == ConvertHelper.ConvertObj<string, int>(groupId);
        //}

        // Check Is In UserRole
        public bool IsInRole(string roleId)
        {
            return RoleID == ConvertHelper.ToInt32(roleId); //ConvertHelper.ConvertObj<string, int>(roleId);
        }

        //public Role CurrentRole { get; set; }

        // //Check Is In Role
        //public bool IsInRole(string role)
        //{
        //    return CurrentRole != null && !string.IsNullOrEmpty(CurrentRole.DisplayName) && CurrentRole.DisplayName == role;
        //}

        //// Current User Is Student
        //public bool IsGroupStudent
        //{
        //    get { return UserGroupEnum.Student.GetHashCode() == GroupID; }
        //}

        //// Current User Is Teacher
        //public bool IsGroupTeacher
        //{
        //    get { return UserGroupEnum.Teacher.GetHashCode() == GroupID; }
        //}

        //// Current User Is School
        //public bool IsGroupSchool
        //{
        //    get { return UserGroupEnum.School.GetHashCode() == GroupID; }
        //}

        //// Current User Is AdminSuper
        //public bool IsGroupAdminSuper
        //{
        //    get { return UserGroupEnum.AdminSuper.GetHashCode() == GroupID; }
        //}

        //// Current User Is Admin
        //public bool IsGroupAdmin
        //{
        //    get { return UserGroupEnum.Admin.GetHashCode() == GroupID; }
        //}
        #endregion

    }

    [Serializable]
    public class GroupInfo : BaseEntity
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }

    [Serializable]
    public class MemberParent : BaseEntity
    {
        public long MemberParentID { get; set; }
        public long MemberID { get; set; }
        public long ParentID { get; set; }
        public bool IsVerified { get; set; }
    }

    [Serializable]
    public class MemberDashboardInfo : BaseEntity
    {
        public int TotalSong { get; set; }
        public int TotalSongPending { get; set; }
        public int TotalSkill { get; set; }
        public int TotalSkillPending { get; set; }
        public int TotalStudent { get; set; }
        public int TotalTeacher { get; set; }
        public int TotalSchool { get; set; }
        public int TotalAdmin { get; set; }
    }

    public class MemberRole
    {
        public int Role { get; set; }
        public string Name { get; set; }
    }
}
