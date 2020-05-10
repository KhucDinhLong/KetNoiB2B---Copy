using System;
using System.Security.Cryptography.X509Certificates;

namespace SETA.Entity
{
    public class BaseListParam {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int TimeZoneClient { get; set; }
    }

    public class ListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public long MemberID { get; set; }
        public short TopN { get; set; }
        public short StatusID { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int FromTotalHomework { get; set; }
        public int ToTotalHomework { get; set; }
        public int FromTotalVideo { get; set; }
        public int ToTotalVideo { get; set; }
    }

    public class CourseListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public long MemberID { get; set; }
        public long CourseTypeID { get; set; }
        public short StatusID { get; set; }
        public short AssignmentStatusID { get; set; }
        public short TopN { get; set; }
        public string Keyword { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }

    public class GroupListParam
    {
        public long CurrentID { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
    public class MemberListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }        
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public long CurrentID { get; set; }

        public int RoleCurrentUser { get; set; }
        public int ShowRole { get; set; }
        public long InvitedBy { get; set; }

        public string SchoolName { get; set; }

        public string CellPhone { get; set; }

        public string BirthDate { get; set; }
        public int? StatusId { get; set; }
        public int? IsSplitActive { get; set; }
        public int? IsAdminUser { get; set; }
    }
    public class ContactListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public string Email { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
    public class LibraryListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        
        //fillter
        public int InstrumentID { get; set; }
        public int CourseTypeID { get; set; }
        public int GenreID { get; set; }
        public int LevelID { get; set; }
        public long MemberID { get; set; }
        public long StudentID { get; set; }
        public short StatusID { get; set; }
        public short StatusHistoryID { get; set; }
        public string Tags { get; set; }
        public string SongTitle { get; set; }
        public string Artist { get; set; }
        public int GroupID { get; set; }
        public int IsApprove { get; set; }  //=1-approve; =0-un-approve
        public int StatusIDComplete { get; set; }  //=1-un-complete; =2-complete
        public short GetType { get; set; }
        public long CurrentID { get; set; }
        public bool IsShare { get; set; }
		public int? ShareWith { get; set; }
    }

    public class MessageListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public string Email { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public long CurrentID { get; set; }
        public int ReadType { get; set; }
        public int TypeId { get; set; }
        public int ViewType { get; set; }
    }

    public class HomeworkReportListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public long MemberID { get; set; }
        public string DueDate { get; set; }
        public int TotalVideoFrom { get; set; }
        public int TotalVideoTo { get; set; }
    }
    public class PaymentTransactionListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public long SchoolID { get; set; }
        public string SchoolName { get; set; }
        public long TeacherID { get; set; }
        public string TeacherlName { get; set; }
        public int StatusID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long PaymentProcessID { get; set; }
        public string StudentName { get; set; }
        //SunV added 2016-10-05
        public long MemberPaymentID { get; set; }
        public long CurMemberID { get; set; }
        public int CurRoleID { get; set; }
        
        public long StudentID { get; set; }
        public short PaymentReportType { get; set; }
        public short PaymentReportPlanType { get; set; }
    }
    public class PlanListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public long PlanID { get; set; }
        public string TeacherName { get; set; }
        public int StatusID { get; set; }
    }
    public class CommunicationLogParam
    {
        public long MemberId { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public long CommunicationId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int TypeID { get; set; }
	}
	
    public class ChatAtachmentFileListParam
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Keyword { get; set; }
        public long MemberID { get; set; }
        public string FileName { get; set; }
    }
}

