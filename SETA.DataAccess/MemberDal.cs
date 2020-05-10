using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SETA.Common;
using SETA.Common.Constants;
using SETA.Common.Helper;
using SETA.Core.Base;
using SETA.Core.Data;
using SETA.DataAccess.Interface;
using SETA.Entity;
using MemberStatus = SETA.Common.Constants.MemberStatus;

namespace SETA.DataAccess
{
    public class MemberDal : BaseDal<ADOProvider>, IMemberDataAccess<Member>, IMember
    {
        /// <summary>
        /// Get Member by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsNotShowStudentInfo(long id, int roleId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", id);
                param.Add("@RoleID", roleId);
                param.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Get_IsNotShowStudentInfo", param);
                return param.Get<bool>("@Result");
            }
            catch (Exception ex)
            {

            }            
            return false;
        }

        public string GetSchoolNameByTeacherId(long teacherId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", teacherId);
                var obj = unitOfWork.Procedure<string>("mem_Get_SchoolName_ByTeacherID", param).FirstOrDefault();
                if (obj != null)
                {
                    return obj.Trim();
                }                
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public Member Get(long id)
        {
            return unitOfWork.Procedure<Member>("mem_Get_MemberInfo",
                                             new { MemberID = id }).FirstOrDefault();
        }

        public Member GetDetailPage(long id)
        {
            try
            {
                return unitOfWork.Procedure<Member>("mem_Get_Member_DetailPageInfo",
                                             new
                                             {
                                                 MemberID = id,
                                                 RoleStudent = ConvertHelper.ToInt32(RoleGroup.Student),
                                                 RoleTeacher = ConvertHelper.ToInt32(RoleGroup.Teacher),
                                                 RoleSchool = ConvertHelper.ToInt32(RoleGroup.School),
                                                 RoleAdmin = ConvertHelper.ToInt32(RoleGroup.Admin),
                                                 RoleSuperAdmin = ConvertHelper.ToInt32(RoleGroup.AdminSuper),
                                                 MemberTypeIDTeacher = ConvertHelper.ToInt32(MemberTypeID.Teacher),
                                                 MemberTypeIDStudent = ConvertHelper.ToInt32(MemberTypeID.Student),
                                                 MemberTypeIDAdmin = ConvertHelper.ToInt32(MemberTypeID.Admin),
                                                 StatusIDActive = GlobalStatus.Active,
                                             }).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Member();
            }            
        }

        /// <summary>
        /// Get Member by UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Member Get(string userName)
        {
            return unitOfWork.Procedure<Member>("mem_Get_UserByUserName",
                                             new { UserName = userName }).FirstOrDefault();
        }

        public Member GetAllStatusNoDeleted(string userName)
        {
            return unitOfWork.Procedure<Member>("mem_Get_UserAllStatusNotDeletedByUserName",
                                             new { UserName = userName }).FirstOrDefault();
        }

        public int CheckUserExists(string userName)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", userName);
                param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_User_Exists_Under18", param);
                return param.Get<int>("@Result");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }
        public Member GetParentByUserName(string userName)
        {
            try
            {
                return unitOfWork.Procedure<Member>("mem_Get_ParentUser_ByUserName",
                                             new { UserName = userName }).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }

        }

        /// <summary>
        /// Get Member by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetByEmail(string email)
        {
            return unitOfWork.Procedure<Member>("mem_Get_UserByEmail",
                                             new { Email = email }).FirstOrDefault();
        }

        public Member GetByUserName(string userName)
        {
            return unitOfWork.Procedure<Member>("mem_Get_UserByUserName",
                                             new { UserName = userName }).FirstOrDefault();
        }

        public Member GetByUserNameOrEmailExits(string userName)
        {
            return unitOfWork.Procedure<Member>("mem_Get_UserByUserNameOREmailExits",
                                             new { UserName = userName }).FirstOrDefault();
        }

        public Member GetByUserNameAndEmail(string userName, long memberId = 0)
        {
            return unitOfWork.Procedure<Member>("mem_Get_UserByUserNameAndEmail",
                                             new { UserName = userName, MemberID  = memberId }).FirstOrDefault();
        }

        /// <summary>
        /// get user by forgot code
        /// </summary>
        /// <param name="forgotCode"></param>
        /// <returns></returns>
        public Member GetUser(Guid forgotCode)
        {
            try
            {
                return
                    unitOfWork.Procedure<Member>("mem_Members_GetUserForgotByCode", new { @ForgotCode = forgotCode })
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetList(string listMemberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ListMemberID", listMemberId);
                param.Add("@StatusIDDeleted", GlobalStatus.Deleted);
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_By_ListMemberID", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return new List<Member>();                                
            }
        } 

        public IList<Member> GetMemberInRole(int roleID)
        {
            var param = new DynamicParameters();
            param.Add("@StatusID", GlobalStatus.Active);
            param.Add("@RoleID", roleID);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("mem_Get_Member_InRole", param).ToList();
            return values;
        }

        /// <summary>
        /// Get List of Member
        /// </summary>
        /// <param name="listParam"></param>
        /// <param name="totalRecord"></param>
        /// <returns>List of Member</returns>
        public IList<Member> Get(MemberListParam listParam, out int? totalRecord)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@CurrentID", listParam.CurrentID);
            param.Add("@RoleCurrentUser", listParam.RoleCurrentUser);
            param.Add("@ShowRole", listParam.ShowRole);
            param.Add("@InvitedBy", listParam.InvitedBy);
            param.Add("@CourseTypeHw", Common.Constants.CourseType.CourseHomework);
            param.Add("@StatusId", listParam.StatusId ?? 1);
            param.Add("@IsSplitActive", listParam.IsSplitActive ?? 0);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        public int GetMemberCountByStatus(MemberListParam listParam)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@CurrentID", listParam.CurrentID);
            param.Add("@RoleCurrentUser", listParam.RoleCurrentUser);
            param.Add("@ShowRole", listParam.ShowRole);
            param.Add("@InvitedBy", listParam.InvitedBy);
            param.Add("@CourseTypeHw", Common.Constants.CourseType.CourseHomework);
            param.Add("@StatusId", listParam.StatusId);
            param.Add("@IsSplitActive", listParam.IsSplitActive);
            param.Add("@isAdminUser", listParam.IsAdminUser ?? 0);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            unitOfWork.ProcedureExecute("mem_Get_Members_Count_ByStatus", param);
            var totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return totalRecord;
        }

        public int GetCountMembersByStatusId(MemberListParam listParam, long currentId, string listSchool = "")
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@RoleID", listParam.ShowRole);
            param.Add("@CurrentID", currentId);
            if (listSchool != "")
            {
                param.Add("@ListUserSchool", listSchool);
            }
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@InvitedBy", listParam.InvitedBy);
            param.Add("@CourseTypeHw", Common.Constants.CourseType.CourseHomework);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@StatusId", listParam.StatusId ?? 1);
            param.Add("@IsSplitActive", listParam.IsSplitActive ?? 0);

            unitOfWork.ProcedureExecute("mem_Get_Count_Members_ByStatusId", param);
            var totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return totalRecord;
        }
        public IList<Member> GetLogInAs(MemberListParam listParam, out int? totalRecord)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@CurrentID", listParam.CurrentID);
            param.Add("@RoleCurrentUser", listParam.RoleCurrentUser);
            param.Add("@ShowRole", listParam.ShowRole);
            param.Add("@InvitedBy", listParam.InvitedBy);
            param.Add("@CourseTypeHw", Common.Constants.CourseType.CourseHomework);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members_LogInAs", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        public IList<Member> AdminGet(MemberListParam listParam, out int? totalRecord)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@CurrentID", listParam.CurrentID);
            param.Add("@RoleCurrentUser", listParam.RoleCurrentUser);
            param.Add("@ShowRole", listParam.ShowRole);
            param.Add("@InvitedBy", listParam.InvitedBy);
            param.Add("@CourseTypeHw", Common.Constants.CourseType.CourseHomework);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@isAdminUser", 1);
            param.Add("@StatusId", listParam.StatusId ?? 1);
            param.Add("@IsSplitActive", listParam.IsSplitActive ?? 0);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        /// <summary>
        /// Get List of Member by Role(for Teacher, School)
        /// </summary>
        /// <param name="listParam"></param>
        /// <param name="totalRecord"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<Member> GetByRole(MemberListParam listParam, out int? totalRecord, int roleId)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@RoleID", roleId);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members_ByRole", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        /// <summary>
        /// Get List of Teacher invite by Role(student)
        /// </summary>
        /// <param name="listParam"></param>
        /// <param name="totalRecord"></param>
        /// <param name="roleId"></param>
        /// <param name="currentId"></param>
        /// <returns></returns>
        public IList<Member> GetByRoleStudent(MemberListParam listParam, out int? totalRecord, int roleId, long currentId)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@RoleID", roleId);
            param.Add("@CurrentID", currentId);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members_ByCurrentUserStudent", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        /// <summary>
        /// Get instructor
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public IList<Member> GetInstructor(long memberid, string listSchool = "")
        {
            var param = new DynamicParameters();
            //param.Add("@StatusID", GlobalStatus.Active);
            //param.Add("@Role", RoleGroup.Teacher);
            param.Add("@Listschool", listSchool);
            param.Add("@MemberID", memberid);
            var values = unitOfWork.Procedure<Member>("lce_Get_List_Instructors", param).ToList();
            return values;
        }

        /// <summary>
        /// Get List of Member by Role(for Teacher, School), CurrentID
        /// </summary>
        /// <param name="listParam"></param>
        /// <param name="totalRecord"></param>
        /// <param name="roleId"></param>
        /// <param name="currentId"></param>
        /// <returns></returns>
        public IList<Member> GetByRole(MemberListParam listParam, out int? totalRecord, int roleId, long currentId)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@RoleID", roleId);
            param.Add("@CurrentID", currentId);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@StatusId", listParam.StatusId ?? 1);
            param.Add("@IsSplitActive", listParam.IsSplitActive ?? 0);
            param.Add("@ShowRole", listParam.ShowRole);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members_ByCurrentUser", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        public int GetCountMembersByCurrentUserStatus(MemberListParam listParam, int roleId, long currentId)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@RoleID", roleId);
            param.Add("@CurrentID", currentId);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@StatusId", listParam.StatusId ?? 1);
            param.Add("@IsSplitActive", listParam.IsSplitActive ?? 0);
            unitOfWork.ProcedureExecute("mem_Get_Count_Members_ByCurrentUserStatus", param);
            var totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return totalRecord;

        }
        public IList<Member> GetPagingChildrenByParentID(MemberListParam listParam, out int? totalRecord, long currentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", listParam.Keyword);
                param.Add("@FirstName", listParam.FirstName);
                param.Add("@Email", listParam.Email);
                param.Add("@CurrentID", currentId);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@SortColumn", listParam.SortColumn);
                param.Add("@SortDirection", listParam.SortDirection);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var values = unitOfWork.Procedure<Member>("mem_Get_Members_Children_By_ParentID", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception)
            {
                totalRecord = 0;
                return new List<Member>();
            }            
        }

        public IList<Member> GetByRoleAndCurrentUser(MemberListParam listParam, out int? totalRecord, long currentId, string listSchool = "")
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@RoleID", listParam.ShowRole);
            param.Add("@CurrentID", currentId);
            if (listSchool != "")
            {
                param.Add("@ListUserSchool", listSchool);
            }
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@InvitedBy", listParam.InvitedBy);
            param.Add("@CourseTypeHw", Common.Constants.CourseType.CourseHomework);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@StatusId", listParam.StatusId ?? 1);
            param.Add("@IsSplitActive", listParam.IsSplitActive ?? 0);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members_ByRoleAndCurrentUser", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        public IList<Member> GetByRoleAndCurrentUserLogInAs(MemberListParam listParam, out int? totalRecord, long currentId, string listSchool = "")
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@RoleID", listParam.ShowRole);
            param.Add("@CurrentID", currentId);
            if (listSchool != "")
            {
                param.Add("@ListUserSchool", listSchool);
            }
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@InvitedBy", listParam.InvitedBy);
            param.Add("@CourseTypeHw", Common.Constants.CourseType.CourseHomework);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("mem_Get_Members_ByRoleAndCurrentUser_LoginAs", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        public IList<Member> GetSchoolList(MemberListParam listParam)
        {
            var param = new DynamicParameters();
            param.Add("@CurrentRoleID", listParam.RoleCurrentUser);
            param.Add("@CurrentID", listParam.CurrentID);
            var values = unitOfWork.Procedure<Member>("mem_Get_Schools_ByRoleID", param).ToList();
            return values;
        }
        public Member CheckMemberUnderAdmin(long adminId, long memberId, int roleCheck)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@AdminID", adminId);
                param.Add("@MemberID", memberId);
                param.Add("@RoleCheck", roleCheck);
                var values = unitOfWork.Procedure<Member>("mem_Check_Member_Under_Admin", param).FirstOrDefault();
                return values;
            }
            catch (Exception)
            {
                return null;
            }           
        }

        #region Save

        /// <summary>
        /// Save Member
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long Save(Member obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userID);
                param.Add("@XML", XMLHelper.SerializeXML<Member>(obj));
                param.Add("@MemberID", obj.MemberID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                unitOfWork.ProcedureExecute("mem_Update_MemberInfo", param);
                return param.Get<long>("@MemberID");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        public bool SaveStartBillingDate(long memberId, DateTime? startBillingDate, long userId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@UserID", userId);
                if (startBillingDate != null)
                {
                    param.Add("@StartBillingDate", startBillingDate);
                }                
                return unitOfWork.ProcedureExecute("mem_Update_Member_StartBillingDate", param);                
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return false;
        }

        #endregion

        public void UpdateListTeacher(long TeacherID, long memberID)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_ListOwner", new { TeacherID = TeacherID, MemberTypeID = MemberTypeID.Teacher,MemberID = memberID });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        public void UpdateListTeacherWithOwnerSchool(long TeacherID, long memberID)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_Invite_TeacherForStudent", new { TeacherID = TeacherID, MemberTypeID = MemberTypeID.Teacher, MemberID = memberID });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        public void UpdateListMember(long TeacherID, long memberID, string memberTypeID)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_ListOwner", new { TeacherID = TeacherID, MemberTypeID = memberTypeID, MemberID = memberID });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        public void UpdateMemberAdmin(long adminID, long memberID, string membertypeID)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_MemberAdmin", new { OwnerID = adminID, MemberTypeID = membertypeID, MemberID = memberID });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }
        public void UpdateTimeZoneUser(long memberId)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_TimeZone_User", new { MemberID = memberId });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        public void UpdateTeacherNameForOrder(long studentId)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_TeacherName_For_Order", new { StudentdID = studentId });
            }
            catch (Exception exc1)
            {

            }            
        }

        public void UpdateLogTimeZoneUser(long memberId, int timeZoneID, long currentUserId)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_TimeZone_Change_User", new { MemberID = memberId, TimeZoneID = timeZoneID, CurrentUserID = currentUserId });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }
        public void UpdateTimeZoneAllUserUnderFirstSchool(long memberId)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Update_TimeZone_All_User_Under_Shool_First", new { MemberID = memberId });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        public void DeleteListTeacher(long memberID, long curMemberID = 0, int curRoleID = 0)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Delete_ListOwner", new {MemberTypeID = MemberTypeID.Teacher, MemberID = memberID, CurrentMemberID= curMemberID, CurrentRoleID = curRoleID });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }
        public void DeleteListMember(long memberID, string memberTypeID)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Delete_ListOwner", new { MemberTypeID = memberTypeID, MemberID = memberID });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        public long UpdateMostRecent(long userID, long courseID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", userID);
                param.Add("@CourseID", courseID);
                unitOfWork.ProcedureExecute("mem_Update_MemberCourse_MostRecent", param);
                return param.Get<long>("@MemberID");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        /// <summary>
        /// ResetExpireDate
        /// </summary>
        /// <param name="memId"></param>
        /// <param name="forgotExpired"></param>
        /// <param name="forgotCode"></param>
        public void ResetExpireDate(long memId, DateTime forgotExpired, Guid forgotCode)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memId);
                param.Add("@ForgotExpired", forgotExpired);
                param.Add("@ForgotCode", forgotCode);

                unitOfWork.ProcedureExecute("mem_Members_ResetExpireDate", param);
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        /// <summary>
        /// ResetExpireDate
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public void ResetPassword(long userId, string password)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", userId);
                param.Add("@PassWord", password);

                unitOfWork.ProcedureExecute("mem_Members_ResetPassword", param);
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        /// <summary>
        /// Change password for current user
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="curPassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ChangePassword(long memberId, string curPassword, string password)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@CurPassWord", curPassword);
                param.Add("@PassWord", password);
                param.Add("@Result", false, dbType: DbType.Boolean, direction: ParameterDirection.InputOutput);
                unitOfWork.ProcedureExecute("mem_Members_ChangePassword", param);
                return param.Get<bool>("@Result");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return false;
        }

        /// <summary>
        /// Delete Members
        /// </summary>
        /// <param name="memberID">MemberID</param>
        /// <param name="userID">UserID</param>
        /// <returns></returns>
        public bool Delete(long memberID, long userID)
        {
            try
            {
                return unitOfWork.ProcedureExecute("mem_Delete_Member", new { MemberID = memberID, UserID = userID, StatusID = GlobalStatus.Deleted });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return false;
        }

        /// <summary>
        /// Check Exist UserName, Email
        /// <para>  =0: Valid</para>
        /// <para>  =1: UserName is duplicate</para>
        /// <para>  =2: Email is duplicate</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>0,1,2</returns>
        public int CheckExist(Member obj)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", obj.MemberID);
                param.Add("@UserName", obj.UserName);
                param.Add("@Email", obj.Email);
                //param.Add("@IsValid", dbType: DbType.Boolean, direction: ParameterDirection.Output); @Result
                param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_CheckDuplicate", param);
                return param.Get<int>("@Result");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        public object Filter(long memberID, long roleID, string name, out int? totalRecord)
        {
            var param = new DynamicParameters();
            param.Add("@MemberID", memberID);
            param.Add("@RoleID", roleID);
            param.Add("@Name", name);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("lce_Get_Student_Filter", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }


        #region Parent&Children

        public IList<Member> GetAllParent(MemberListParam listParam, out int? totalRecord)
        {
            var result = new List<Member>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@FirstName", listParam.FirstName);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@SortColumn", listParam.SortColumn);
                param.Add("@SortDirection", listParam.SortDirection);
                param.Add("@DeleteStatusID", GlobalStatus.Deleted);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                result = unitOfWork.Procedure<Member>("mem_Get_All_Parent", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");                
            }
            catch (Exception)
            {
                totalRecord = 0;
            }

            return result;
        }
        public IList<Member> GetAllParentBySchoolId(MemberListParam listParam, out int? totalRecord, long schoolId)
        {
            var result = new List<Member>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@FirstName", listParam.FirstName);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@SortColumn", listParam.SortColumn);
                param.Add("@SortDirection", listParam.SortDirection);
                param.Add("@DeleteStatusID", GlobalStatus.Deleted);
                param.Add("@CurUserId", schoolId);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                result = unitOfWork.Procedure<Member>("mem_Get_All_Parent_By_UserId", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
            }
            catch (Exception)
            {
                totalRecord = 0;
            }

            return result;
        }

        public IList<Member> GetAllParentByTeacherId(MemberListParam listParam, out int? totalRecord, long teacherId)
        {
            var result = new List<Member>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@FirstName", listParam.FirstName);
                param.Add("@PageNumber", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@SortColumn", listParam.SortColumn);
                param.Add("@SortDirection", listParam.SortDirection);
                param.Add("@DeleteStatusID", GlobalStatus.Deleted);
                param.Add("@CurUserId", teacherId);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                result = unitOfWork.Procedure<Member>("mem_Get_All_Parent_By_TeacherId", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
            }
            catch (Exception)
            {
                totalRecord = 0;
            }

            return result;
        }

        public IList<Member> GetAllChildren(long parentID)
        {
            var values = unitOfWork.Procedure<Member>("mem_Get_All_Children", new { ParentID = parentID, StatusID = GlobalStatus.Active, Verified = true }).ToList();
            return values;
        }

        public IList<long> GetParents(string listStudents)
        {
            var value = unitOfWork.Procedure<long>("mem_Get_Parents", new { ListStudents = listStudents }).ToList();
            return value;
        }

        public IList<long> GetParentsByType(string listStudents, int type)
        {
            var value = unitOfWork.Procedure<long>("mem_Get_Parents_ByType", new { ListStudents = listStudents, Type = type }).ToList();
            return value;
        }
        public Member GetParentByUserNameOrEmail(string key)
        {
            try
            {
                return unitOfWork.Procedure<Member>("mem_Get_Parent_ByUserNameOrEmailInfo", new { Key = key }).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetParentsByUserID(long userID)
        {
            return unitOfWork.Procedure<Member>("mem_Get_Parents_ByUserID", new { UserID = userID, StatusID = GlobalStatus.Active }).ToList();
        }

        public IList<Member> GetParentsVerifiedByUserId(long userID)
        {
            return unitOfWork.Procedure<Member>("mem_Get_Parents_Verified_ByUserID", new { UserID = userID, StatusID = GlobalStatus.Active }).ToList();
        }

        #endregion

        public bool CheckExistEmail(string email)
        {
            var param = new DynamicParameters();
            param.Add("@Email", email);
            param.Add("@RoleID", RoleGroup.Student);
            param.Add("@StatusID", GlobalStatus.Active);
            param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            unitOfWork.ProcedureExecute("mem_Check_Exist_EmailStudent", param);
            var result = param.Get<int>("@Result");
            return result > 0 ? true : false;
        }

        public bool UpdateFavorite(long courseID, long memberID)
        {
            return unitOfWork.ProcedureExecute("mem_Update_Favourite_Course", new { CourseID = courseID, MemberID = memberID });
        }

        public bool CheckCourseIsFavorite(long courseId, long memberID)
        {
            var param = new DynamicParameters();
            param.Add("@CourseID", courseId);
            param.Add("@MemberID", memberID);
            param.Add("@IsFavourite", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            unitOfWork.ProcedureExecute("mem_Get_Favourite_Course", param);
            var rsl = param.Get<bool>("@IsFavourite");

            return rsl;
        }

        public List<Member> GetByGroupId(int groupId)
        {
            try
            {
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_ByGroupId", new { GroupID = groupId }).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetByAssignmentId(long assignmentId)
        {
            try
            {
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_ByAssignmentId", new { AssignmentID = assignmentId }).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetForDashboardTeacher(long memberId, out int? totalRecord)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_ForDashboardTeacher", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception)
            {
                totalRecord = 0;
                return null;
            }
        }

        public IList<Member> GetForDashboardTeacher(long memberId, int? isSplitActive, out int? totalRecord, out int? totalRecordStatus, int statusId = 1)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@StatusId", statusId);
                param.Add("@IsSplitActive", isSplitActive ?? (object)DBNull.Value);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                param.Add("@TotalRecordStatus", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_ForDashboardTeacher", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalRecordStatus = param.GetDataOutput<int>("@TotalRecordStatus");
                return values;
            }
            catch (Exception)
            {
                totalRecord = 0;
                totalRecordStatus = 0;
                return null;
            }
        }

        public IList<Member> GetMemberByUserNameOrEmail(string UserName, string Email)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", UserName);
                param.Add("@Email", Email);
                param.Add("@StatusID", GlobalStatus.Active);
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_ByUsernameOrEmail", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetMemberByUserNameOrEmail(string UserName, string Email, short statusId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", UserName);
                param.Add("@Email", Email);
                param.Add("@StatusID", statusId);
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_ByUsernameOrEmail", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int CountMemberInRole(int RoleID, out int? totalRecord)
        {
            var param = new DynamicParameters();
            param.Add("@RoleID", RoleID);
            param.Add("@StatusID", GlobalStatus.Deleted);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.ProcedureExecute("mem_Get_NumberMember_InRole", param);
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return (int)totalRecord;
        }

        public int CountMemberInRole(long currentMemberId, int RoleID, out int? totalRecord)
        {
            var param = new DynamicParameters();
            param.Add("@RoleID", RoleID);
            param.Add("@StatusID", GlobalStatus.Deleted);
            param.Add("@CurrentID", currentMemberId);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.ProcedureExecute("mem_Get_NumberMember_InRole", param);
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return (int)totalRecord;
        }

        public IList<Member> GetListTeacherInviteByStudent(long memberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                var values = unitOfWork.Procedure<Member>("mem_Get_Invite_TeacherByStudent", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<Member> GetListMemberInvite(long memberId, int memberTypeID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@MemberTypeID", memberTypeID);
                var values = unitOfWork.Procedure<Member>("mem_Get_Invite_TeacherByStudent", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetListSchoolInviteByTeacher(long memberId, IList<Member> listMember)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@MemberList", (listMember.Count>0)?string.Join(",", listMember.Select(m=>m.MemberID).ToList()):"-1");
                var values = unitOfWork.Procedure<Member>("mem_Get_Invite_School_By_Teacher", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetListSchoolInviteByStudent(long memberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StudentID", memberId);                
                var values = unitOfWork.Procedure<Member>("mem_Get_Invite_School_Student_ByStudentID", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<Member> GetListTeacherBySchool(long memberId, IList<long> lstSchool)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@MemberList", (lstSchool != null && lstSchool.Count > 0) ? string.Join(",", lstSchool) : "0");
                var values = unitOfWork.Procedure<Member>("mem_Get_Teachers_By_Shool", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int ViewHomeworkDetail(long homeworkId, long memberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@HomeworkID", homeworkId);
                param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_CheckView_HomeworkDetail", param);
                return param.Get<int>("@Result");
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList<Member> GetAllAdmin(long currentId)
        {
            try
            {
                return unitOfWork.Procedure<Member>("mem_Get_Member_GetAllAdmin", new { @CurrentUserID = currentId }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int checkStudentOfTeacher(long teacherId, long memberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@OwnerID", teacherId);
                param.Add("@StatusID", GlobalStatus.Active);
                param.Add("@Result", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_Exist_StudentInvite", param);
                return param.Get<int>("@Result");
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int checkParentStudentAndTeacher(long teacherId, long parentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ParentID", parentId);
                param.Add("@TeacherID", teacherId);
                param.Add("@StatusID", GlobalStatus.Active);
                param.Add("@Result", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_ParentStudent_InvitedBy_Teacher", param);
                return param.Get<int>("@Result");
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList<Member> GetSchoolMembers(int roleId, long CurUserID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RoleID", roleId);
                param.Add("@CurUserID", CurUserID);
                var values = unitOfWork.Procedure<Member>("mem_Get_Members_School", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }

        public IList<Member> GetMembersByRoleByCurrentUser(int getRole, long currentId)
        {
            try
            {
                return unitOfWork.Procedure<Member>("mem_Get_Member_GetAllMemberWithRoleByCurrentUser", new { @Role = getRole, @CurrentUserID = currentId }).ToList();
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        public IList<Member> GetListTeacherIdOfSchool(long currentId, int page, int pageSize, out int? numPage)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentID", currentId);
                param.Add("@PageNumber", page);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //param.Add("@PageSize", DailyView.PageSize);
                param.Add("@PageSize", pageSize);
                var values = unitOfWork.Procedure<Member>("mem_Get_All_Member_Of_School", param).ToList();
                numPage = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception)
            {
                numPage = 0;
                return null;

            }
        }
        public IList<Member> GetListTeacherIdOfSchool(long currentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentID", currentId);
                var values = unitOfWork.Procedure<Member>("mem_Get_All_Teacher_Of_School", param).ToList();
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<Member> GetListTeacherIdByInstrumentId(long instrumentId, int page, int pageSize, out int? numPage)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@InstrumentId", instrumentId);
                param.Add("@PageNumber", page);
                param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //param.Add("@PageSize", DailyView.PageSize);
                param.Add("@PageSize", pageSize);
                var values = unitOfWork.Procedure<Member>("mem_Get_All_Member_By_InstrumentId", param).ToList();
                numPage = param.GetDataOutput<int>("@TotalRecord");
                return values;
            }
            catch (Exception)
            {
                numPage = 0;
                return null;

            }
        }
        public IList<Member> GetListByInstrument(long instrumentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@InstrumentID", instrumentId);
                param.Add("@StatusActive", GlobalStatus.Active);
                //SuNV 2016-11-01 added
                param.Add("@RoleTeacher", int.Parse(RoleGroup.Teacher));
                //End
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_By_Instrument", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }
        public IList<Member> WgGetListTeacherByMember(long memberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                var values = unitOfWork.Procedure<Member>("mem_WG_Get_Member_By_MemberID", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }

        public IList<Member> GetListStudentByTeacher(long teacherId)
        {
            try
            {
                return unitOfWork.Procedure<Member>("mem_Get_List_Student_By_Teacher", new { RoleStudent = ConvertHelper.ToInt32(RoleGroup.Student), MemberID = teacherId }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<Member> GetListTeacherOfStudent(long studentId)
        {
            try
            {
                return unitOfWork.Procedure<Member>("mem_Get_All_Teacher_Of_Student", new { MemberID = studentId }).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
                
        public int UpdateTeacherPay(long studentId, string lstTeacherPay, long schoolId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StudentID", studentId);
                param.Add("@LstTeacherPay", lstTeacherPay);
                param.Add("@SchoolID", schoolId);
                var values = unitOfWork.Procedure<int>("mem_Update_Teacher_Pay_Of_Student", param).FirstOrDefault();
                return values;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Member GetByCellPhone(string cellPhone, int phoneCode, long currentID)
        {
            return unitOfWork.Procedure<Member>("mem_Get_GetCellPhoneUse", new { CellPhone = cellPhone, PhoneCode = phoneCode, MemberID = currentID }).FirstOrDefault();
        }

        public Member GetCellPhoneVerify(string cellPhone, int phoneCode)
        {
            var param = new DynamicParameters();
            param.Add("@CellPhone", cellPhone);
            param.Add("@PhoneCode", phoneCode);
            var values = unitOfWork.Procedure<Member>("mem_Get_Verify_Phone", param).FirstOrDefault();
            return values;
        }

        public long SaveCodeVerifyPhone(string cellPhone, long currentID, string code)
        {
            var param = new DynamicParameters();
            param.Add("@CellPhone", cellPhone);
            param.Add("@MemberID", currentID);
            param.Add("@Code", code);
            param.Add("@VerifyID", 0, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
            unitOfWork.Procedure<Object>("mem_Insert_Verify_Phone", param).FirstOrDefault();
            return param.Get<long>("@VerifyID");
        }
        public Member CheckCodeVerifyPhone(string cellPhone, string sameCellPhone, long currentID, string code)
        {
            var param = new DynamicParameters();
            param.Add("@CellPhone", cellPhone);
            param.Add("@SameCellPhone", sameCellPhone);
            param.Add("@MemberID", currentID);
            param.Add("@Code", code);
            return unitOfWork.Procedure<Member>("mem_Check_Code_Verify_Phone", param).FirstOrDefault();
            
        }
        public void SaveVeifySMSPhone(string cellPhone, long memberID, string flag, string phoneCode, int verifySMS)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Save_Veify_SMS_Phone", new { 
                    MemberID = memberID,
                    CellPhone = cellPhone, 
                    IsoCountry = flag,
                    Phonecode = phoneCode,
                    VerifySMS = verifySMS
                });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }

        }

        public void SaveLogSMSSent(long userIDSent, long userIDReceive,string content, int type)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Insert_SMS_log",
                    new { UserIDSent = userIDSent, 
                        UserIDReceive = userIDReceive,
                        SMSContent = content,
                        Type = type}
                        );
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }

        }
        public void SaveLogEmailSent(long userIDSent, long userIDReceive, string email, string content, int type)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Insert_Email_log",
                        new { 
                                UserIDSent = userIDSent, 
                                UserIDReceive = userIDReceive,
                                Email = email,
                                Content = content,
                                Type = type
                            }
                        );
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }

        }

        public DateTime? GetDateTimeLogSMSLast(long userIDSent, long userIDReceive, int type)
        {
            var param = new DynamicParameters();
            param.Add("@UserIDSent", userIDSent);
            param.Add("@UserIDReceive", userIDReceive);
            param.Add("@Type", type);
            param.Add("@CreateDate",null, dbType: DbType.DateTime2, direction: ParameterDirection.Output);
            unitOfWork.Procedure<Object>("mem_Get_DateTime_SMS_Last", param).FirstOrDefault();
            return param.Get<DateTime?>("@CreateDate");
        }
        public DateTime? GetDateTimeLoEmailLast(long userIDSent, long userIDReceive, int type)
        {
            var param = new DynamicParameters();
            param.Add("@UserIDSent", userIDSent);
            param.Add("@UserIDReceive", userIDReceive);
            param.Add("@Type", type);
            param.Add("@CreateDate",null, dbType: DbType.DateTime2, direction: ParameterDirection.Output);
            unitOfWork.Procedure<Object>("mem_Get_DateTime_Email_Last", param).FirstOrDefault();
            return param.Get<DateTime?>("@CreateDate");
        }

        public void SaveConfigNotify(long memberID, int frequencyType, string frequencyTime, string frequencyData)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Save_Config_Notification", 
                    new
                    {                         
                        MemberID = memberID,
                        FrequencyType = frequencyType,
                        FrequencyTime = frequencyTime,
                        FrequencyData = frequencyData
                    });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }

        }


        #region Get new

        public IList<Member> GetListUserForChat(long currentId, int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentMemberID", currentId);
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_Chat_ReCent", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }
        public IList<Member> GetListContactForChat(long currentId, int pageNumber = 1, int pageSize = 20, int currentRoleId = 0)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentMemberID", currentId);
                param.Add("@PageNumber", pageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@StatusID", MemberStatus.MEMBER_STATUS_ACTIVE_VALUE);
                param.Add("@ContactTypeGroup", ContactTypeID.Group);
                param.Add("@NamePrefixGroup", Constants.CHAT_GROUP_NAME_PREFIX_GROUP);
                param.Add("@NameGroupAllStudent", Constants.CHAT_GROUP_NAME_GROUP_ALL_STUDENT);
                param.Add("@NameGroupAllTeacher", Constants.CHAT_GROUP_NAME_GROUP_ALL_TEACHER);
                param.Add("@RoleIDTeacher", ConvertHelper.ToInt32(RoleGroup.Teacher));
                param.Add("@CurrentRoleID", currentRoleId);
                param.Add("@ContactTypeMember", ContactTypeID.Member);
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_Contact", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }
        public Member GetInforMember(long currentId, long memberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentMemberID", currentId);
                param.Add("@ContactID", memberId);
                param.Add("@StatusID", MemberStatus.MEMBER_STATUS_ACTIVE_VALUE);
                var values = unitOfWork.Procedure<Member>("mem_Get_Infor_Contact_Member", param).FirstOrDefault();
                return values;
            }
            catch (Exception ex)
            {
                return new Member();
            }
        }
        public Member GetInforReceiveMember(long currentId, long memberId, int contactTypeId = 0)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentMemberID", currentId);
                param.Add("@ContactID", memberId);
                param.Add("@StatusID", MemberStatus.MEMBER_STATUS_ACTIVE_VALUE);
                param.Add("@ContactTypeID", contactTypeId);
                var values = unitOfWork.Procedure<Member>("mem_Get_Infor_Receive_Contact", param).FirstOrDefault();
                return values;
            }
            catch (Exception ex)
            {
                return new Member();
            }
        }
        public Member GetTotalUnreadMessage(long currentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentMemberID", currentId);
                var values = unitOfWork.Procedure<Member>("mem_Get_Total_Unread_Message_Chat", param).FirstOrDefault();
                return values;
            }
            catch (Exception ex)
            {
                return new Member();
            }
        }

        public IList<Member> GetListTeacherFree()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RoleGroupTeacher", ConvertHelper.ToInt32(RoleGroup.Teacher));
                param.Add("@MemberTypeSchool", MemberTypeID.School);                
                var values = unitOfWork.Procedure<Member>("mem_Get_Teachers_Free", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }

        public IList<Member> GetListStudentOfTeacherForSchedule(bool isAssigned, long teacherId, long schoolId = 0)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@IsAssigned", isAssigned);
                param.Add("@TeacherID", teacherId);
                param.Add("@SchoolID", schoolId);
                var values = unitOfWork.Procedure<Member>("mem_Get_Student_By_Teacher_For_Schedule", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }

        public MemberDashboardInfo GetDataDashboardForSuperAdmin()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StatusCourseActive", GlobalStatus.Active);
                param.Add("@StatusCourseInActive", GlobalStatus.InActive);
                param.Add("@CourseTypeIDSong", Common.Constants.CourseType.SONG);
                param.Add("@CourseTypeIDSkill", Common.Constants.CourseType.SKILL);
                param.Add("@StatusMemberDelete", GlobalStatus.Deleted);
                return unitOfWork.Procedure<MemberDashboardInfo>("lce_Get_Dashboard_Info_SuperAdmin", param).FirstOrDefault();                
            }
            catch (Exception ex)
            {
                return new MemberDashboardInfo();
            }
        }

        public MemberDashboardInfo GetDataDashboardForAdmin(long currentMemberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentMemberID", currentMemberId);
                param.Add("@StatusCourseActive", GlobalStatus.Active);
                param.Add("@StatusCourseInActive", GlobalStatus.InActive);
                param.Add("@CourseTypeIDSong", Common.Constants.CourseType.SONG);
                param.Add("@CourseTypeIDSkill", Common.Constants.CourseType.SKILL);
                param.Add("@StatusMemberDelete", GlobalStatus.Deleted);
                return unitOfWork.Procedure<MemberDashboardInfo>("lce_Get_Dashboard_Info_Admin", param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new MemberDashboardInfo();
            }
        }

        public MemberDashboardInfo GetDataDashboardForSchool(long currentMemberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentMemberID", currentMemberId);
                param.Add("@StatusCourseActive", GlobalStatus.Active);
                param.Add("@StatusCourseInActive", GlobalStatus.InActive);
                param.Add("@CourseTypeIDSong", Common.Constants.CourseType.SONG);
                param.Add("@CourseTypeIDSkill", Common.Constants.CourseType.SKILL);
                param.Add("@StatusMemberDelete", GlobalStatus.Deleted);
                return unitOfWork.Procedure<MemberDashboardInfo>("lce_Get_Dashboard_Info_School", param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new MemberDashboardInfo();
            }
        }        

        public long SavePlanHistory(long planID, long studentID, long teacherID, bool isConfirmOK = false,
            string creditCardType = "", string creditCardTypeMain = "", DateTime startTime = default(DateTime),
            DateTime endTime = default(DateTime), string transactionid = "", bool planIsRequired = false,
            long recurringPaymentHistoryId = 0, long schoolID = 0, long memberBillingInfoId = 0, DateTime paidDate = default(DateTime), 
            DateTime dueDate = default(DateTime), int paymentStatusID = 0, long memberOwnerId = 0, long instrumentId = 0, 
            long paymentMethodId = 0, int unassign = 0, decimal ProratedFee = 0, long receiverMemberId = 0, bool isOneTimePaymentFee = false, long bookingId = 0, long paymentHistoryIDOnTimeFee = 0, string promoCode = "")
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PlanID", planID);
                param.Add("@StudentID", studentID);
                param.Add("@TeacherID", teacherID);
                param.Add("@StatusActive", GlobalStatus.Active);
                param.Add("@StatusIDDelete", GlobalStatus.Deleted);
                param.Add("@IsRecurringFinished", isConfirmOK ? 1 : 0);
                param.Add("@CreditCardType", creditCardType);
                param.Add("@CreditCardTypeMain", creditCardTypeMain);
                //SuNV 2016-09-29 TZ-2875 - Get ID of record, TransactionID(for refund)
                param.Add("@TransactionID", transactionid);
                param.Add("@StudentPlanHistoryID", 0, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                param.Add("@PlanIsRequired", planIsRequired);
                param.Add("@RecurringPaymentHistoryID", recurringPaymentHistoryId);
                param.Add("@SchoolID", schoolID);
                param.Add("@MemberBillingInfoID", memberBillingInfoId);
                if (paidDate != default(DateTime))
                {
                    param.Add("@PaidDate", paidDate);    
                }
                if (dueDate != default(DateTime))
                {
                    param.Add("@DueDate", dueDate);    
                }
                param.Add("@PaymentStatusID", paymentStatusID);
                param.Add("@MemberOwnerID", memberOwnerId);
                param.Add("@InstrumentID", instrumentId);
                param.Add("@MemberPaymentID", paymentMethodId);
                param.Add("@Unassign", unassign);
                param.Add("@ProratedFee", ProratedFee);
                param.Add("@ReceiverMemberID", receiverMemberId);
                param.Add("@IsOneTimePaymentFee", isOneTimePaymentFee);
                param.Add("@BookingID", bookingId);
                param.Add("@PaymentHistoryIDOnTimeFee", paymentHistoryIDOnTimeFee);
                param.Add("@InputPromoCode", promoCode);

                unitOfWork.ProcedureExecute("mem_Update_Plan_History", param);

                return param.Get<long>("@StudentPlanHistoryID");
                //End
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public long CreateOrderOnTimePaymentFee(long planID, long studentID, long teacherID, bool isConfirmOK = false,
            string creditCardType = "", string creditCardTypeMain = "", DateTime startTime = default(DateTime),
            DateTime endTime = default(DateTime), string transactionid = "", bool planIsRequired = false,
            long recurringPaymentHistoryId = 0, long schoolID = 0, long memberBillingInfoId = 0, DateTime paidDate = default(DateTime),
            DateTime dueDate = default(DateTime), int paymentStatusID = 0, long memberOwnerId = 0, long instrumentId = 0,
            long paymentMethodId = 0, int unassign = 0, decimal ProratedFee = 0, long receiverMemberId = 0, bool isOneTimePaymentFee = false, long bookingId = 0, long paymentHistoryIDOnTimeFee = 0)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PlanID", planID);
                param.Add("@StudentID", studentID);
                param.Add("@TeacherID", teacherID);
                param.Add("@StatusActive", GlobalStatus.Active);
                param.Add("@StatusIDDelete", GlobalStatus.Deleted);
                param.Add("@IsRecurringFinished", isConfirmOK ? 1 : 0);
                param.Add("@CreditCardType", creditCardType);
                param.Add("@CreditCardTypeMain", creditCardTypeMain);
                //SuNV 2016-09-29 TZ-2875 - Get ID of record, TransactionID(for refund)
                param.Add("@TransactionID", transactionid);
                param.Add("@StudentPlanHistoryID", 0, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                param.Add("@PlanIsRequired", planIsRequired);
                param.Add("@RecurringPaymentHistoryID", recurringPaymentHistoryId);
                param.Add("@SchoolID", schoolID);
                param.Add("@MemberBillingInfoID", memberBillingInfoId);
                if (paidDate != default(DateTime))
                {
                    param.Add("@PaidDate", paidDate);
                }
                if (dueDate != default(DateTime))
                {
                    param.Add("@DueDate", dueDate);
                }
                param.Add("@PaymentStatusID", paymentStatusID);
                param.Add("@MemberOwnerID", memberOwnerId);
                param.Add("@InstrumentID", instrumentId);
                param.Add("@MemberPaymentID", paymentMethodId);
                param.Add("@Unassign", unassign);
                param.Add("@ProratedFee", ProratedFee);
                param.Add("@ReceiverMemberID", receiverMemberId);
                param.Add("@IsOneTimePaymentFee", isOneTimePaymentFee);
                param.Add("@BookingID", bookingId);
                param.Add("@PaymentHistoryIDOnTimeFee", paymentHistoryIDOnTimeFee);

                unitOfWork.ProcedureExecute("mem_Create_Order_One_Time_Payment_Fee", param);

                return param.Get<long>("@StudentPlanHistoryID");
                //End
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public long SavePlanHistoryWithAmount(long planID, long studentID, long teacherID, bool isConfirmOK = false,
            string creditCardType = "", string creditCardTypeMain = "", DateTime startTime = default(DateTime),
            DateTime endTime = default(DateTime), string transactionid = "", bool planIsRequired = false,
            long recurringPaymentHistoryId = 0, long schoolID = 0, long memberBillingInfoId = 0,
            DateTime paidDate = default(DateTime), DateTime dueDate = default(DateTime), int paymentStatusID = 0,
            long memberOwnerId = 0, long instrumentId = 0, int paymentMethodId = 0, int unassign = 0, decimal amount = 0, string note = "", long receiverMemberId = 0)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PlanID", planID);
                param.Add("@StudentID", studentID);
                param.Add("@TeacherID", teacherID);
                param.Add("@StatusActive", GlobalStatus.Active);
                param.Add("@StatusIDDelete", GlobalStatus.Deleted);
                param.Add("@IsRecurringFinished", isConfirmOK ? 1 : 0);
                param.Add("@CreditCardType", creditCardType);
                param.Add("@CreditCardTypeMain", creditCardTypeMain);
                //SuNV 2016-09-29 TZ-2875 - Get ID of record, TransactionID(for refund)
                param.Add("@TransactionID", transactionid);
                param.Add("@StudentPlanHistoryID", 0, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                param.Add("@PlanIsRequired", planIsRequired);
                param.Add("@RecurringPaymentHistoryID", recurringPaymentHistoryId);
                param.Add("@SchoolID", schoolID);
                param.Add("@MemberBillingInfoID", memberBillingInfoId);
                if (paidDate != default(DateTime))
                {
                    param.Add("@PaidDate", paidDate);
                }
                if (dueDate != default(DateTime))
                {
                    param.Add("@DueDate", dueDate);
                }
                param.Add("@PaymentStatusID", paymentStatusID);
                param.Add("@MemberOwnerID", memberOwnerId);
                param.Add("@InstrumentID", instrumentId);
                param.Add("@MemberPaymentID", paymentMethodId);
                param.Add("@Unassign", unassign);
                param.Add("@Amount", amount);
                param.Add("@Note", note);
                param.Add("@ReceiverMemberID", receiverMemberId);
                unitOfWork.ProcedureExecute("mem_Update_Plan_History_WithAmount", param);

                return param.Get<long>("@StudentPlanHistoryID");
                //End
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public bool UpdatePlanHistory(long paymentHistoryID, string transactionID, long memberID, long memberBillingInfoID, long receiverMemberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PaymentHistoryID", paymentHistoryID);
                param.Add("@TransactionID", transactionID);
                param.Add("@TZPayID", PaymentMethod.TZ_PAY);
                param.Add("@MemberID", memberID);
                param.Add("@MemberBillingInfoID", memberBillingInfoID);
                param.Add("@ReceiverMemberID", receiverMemberId);

                unitOfWork.ProcedureExecute("mem_Update_Plan_History_For_Report", param);

                return true;
                //End
            }
            catch (Exception ex)
            {                
                //throw new NotImplementedException();
                return false;
            }

        }

        public bool UpdateCalendarByPayment(long paymentHistoryID, long memberID, short statusid = 1)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@PaymentHistoryID", paymentHistoryID);
                param.Add("@MemberID", memberID);
                param.Add("@StatusID", statusid);

                unitOfWork.ProcedureExecute("mem_Update_Calendar_By_Payment", param);

                return true;
                //End
            }
            catch (Exception ex)
            {
                //throw new NotImplementedException();
                return false;
            }

        }

        #endregion
        public bool DeleteMemberContact(List<long> lstMemberId, long contactId)
        {
            try
            {
                return unitOfWork.ProcedureExecute("mem_Delete_Member_Contact", new { MemberID = string.Join(",", lstMemberId.ToArray()), ContactID = contactId });
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void AddMemberContact(List<long> lstMemberId, long contactId)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Save_Veify_SMS_Phone", new
                {
                    MemberID = string.Join(",", lstMemberId.ToArray()),
                    ContactID = contactId
                });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }

        }

        public IList<Member> GetMemberForSuggestSearch(string keyword, int roleId, long schoolId = 0, long schoolIDSelected = 0, int typeSearch = 1, long teacherId = 0)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Keyword", keyword);
                param.Add("@RoleIDSearch", roleId);
                param.Add("@StatusActived", GlobalStatus.Active);
                param.Add("@SchoolID", schoolId);
                param.Add("@TeacherID", teacherId);
                param.Add("@IsSchoolSearch", schoolId > 0 ? 1 : 0);
                param.Add("@TypeID", typeSearch);
                param.Add("@SchoolIDSelected", schoolIDSelected);
                param.Add("@RoleIDSchool", int.Parse(RoleGroup.School));
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_For_Suggest_Search", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }

        public IList<Member> GetTeachersBySchool(string keyword, long schoolId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Keyword", keyword);
                param.Add("@StatusActived", GlobalStatus.Active);
                param.Add("@SchoolID", schoolId);
                var values = unitOfWork.Procedure<Member>("mem_Get_Teachers_By_SchoolId", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }        

        /// <summary>
        /// Owner: teacher/school
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="ownerId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int CheckOwnerOfStudent(long studentId, long ownerId, int roleId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StudentID", studentId);
                param.Add("@MemberID", ownerId);
                param.Add("@RoleID", roleId);
                param.Add("@RoleSchool", int.Parse(RoleGroup.School));
                param.Add("@RoleTeacher", int.Parse(RoleGroup.Teacher));
                param.Add("@StatusIDActive", GlobalStatus.Active);
                param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_Student_Of_Member", param);
                return param.Get<int>("@Result");
            }
            catch (SqlException exc1)
            {

            }
            return 0;
        }

        //ThangND [2016-10-11] [TZ-2880: Update Member Contacts - School/Teacher can send an email and SMS notification]
        public List<Member> SearchAllMembersByCurrentUser(long currentId, int currentRole, string keyword)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CurrentID", currentId);
                param.Add("@CurrentRole", currentRole);
                param.Add("@Keyword", keyword);
                param.Add("@RoleSchool", ConvertHelper.ToInt32(RoleGroup.School));
                param.Add("@RoleTeacher", ConvertHelper.ToInt32(RoleGroup.Teacher));
                param.Add("@StatusActive", GlobalStatus.Active);
                param.Add("@MemberTypeTeacher", ConvertHelper.ToInt32(MemberTypeID.Teacher));
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_Search_All_For_CurrentUser", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }
        //End 
        public bool CheckTeacherHasSchool(long memberId)
        {
            var param = new DynamicParameters();
            param.Add("@MemberID", memberId);
            param.Add("@StatusActive", GlobalStatus.Active);
            return unitOfWork.Procedure<bool>("mem_Check_Teacher_Has_School", param).FirstOrDefault();
        }

        //ThangND [2016-10-28] [TZ-3146: [Chat][Email] Send to is clear after change role]
        public List<Member> GetMemberByRole(long memberId, int currentRoleId, int fillterRole, int checkType = 0, string memberIdMore = "")
        {
        //End
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberId", memberId);
                param.Add("@CurrentRoleId", currentRoleId);
                param.Add("@FillterRole", fillterRole);
                param.Add("@CheckType", checkType);
                param.Add("@RoleSupperAdmin", ConvertHelper.ToInt32(RoleGroup.AdminSuper));
                param.Add("@RoleAdmin", ConvertHelper.ToInt32(RoleGroup.Admin));
                param.Add("@RoleSchool", ConvertHelper.ToInt32(RoleGroup.School));
                param.Add("@RoleTeacher", ConvertHelper.ToInt32(RoleGroup.Teacher));
                param.Add("@RoleStudent", ConvertHelper.ToInt32(RoleGroup.Student));
                param.Add("@StatusActive", GlobalStatus.Active);
                param.Add("@MemberTypeTeacher", ConvertHelper.ToInt32(MemberTypeID.Teacher));
                //ThangND [2016-10-28] [TZ-3146: [Chat][Email] Send to is clear after change role]
                param.Add("@MemberIDMore", memberIdMore);
                //End
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_By_Role", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        } 

        //SuNV check teacher free 2016-10-28            
        public bool IsTeacherFree(long teacherId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", teacherId);
                param.Add("@TypeTeacher", int.Parse(MemberTypeID.Teacher));
                param.Add("@StatusID", GlobalStatus.Active);
                param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_Teacher_Free", param);
                var result = param.Get<int>("@Result");
                return result > 0 ? false : true;
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }

        //SuNV [2016-12-14] [Add]
        public IList<Member> GetTeacherOrSchoolOfStudent(long memberID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberID);
                param.Add("@StatusID", GlobalStatus.Active);
                param.Add("@RoleTeacher", int.Parse(RoleGroup.Teacher));
                var values = unitOfWork.Procedure<Member>("mem_Get_All_School_Teacher_Of_Student", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }

        public IList<Member> GetAllStudentByRoleAndMemberId(long memberId, int roleId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@RoleID", roleId);
                var values = unitOfWork.Procedure<Member>("mem_Get_All_Student_By_Role_MemberID", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }

        public void SetNotificationSettingForMember(long currentMemberId, long memberId, int memberType = 0, long schoolId = 0, long teacherId = 0,long studentId = 0)
        {
            try
            {
                unitOfWork.ProcedureExecute("mem_Set_Notification_Setting_For_Member", new
                {
                    MemberID        = memberId,
                    MemberType      = memberType,
                    SchoolID        = schoolId,
                    TeacherID	    = teacherId,
                    StudentID	    = studentId,
                    CurrentMemberID = currentMemberId
                });
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
        }

        public Member GetFirstSchoolOfTeacher(long teacherId)
        {
            try
            {
                return
                    unitOfWork.Procedure<Member>("mem_Get_Invite_School_Member_FirstSchool_Of_Teacher",
                        new {TeacherID = teacherId}).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Member();                                
            }            
        }

        public IList<Member> GetListTeacherIsAssignedInGroup(int groupId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@GroupID", groupId);
                param.Add("@StatusIDActive", GlobalStatus.Active);
                param.Add("@StatusIDDeleted", GlobalStatus.Deleted);
                param.Add("@MemberTypeIDTeacher", ConvertHelper.ToInt32(MemberTypeID.Teacher));
                var values = unitOfWork.Procedure<Member>("mem_Get_Member_Teacher_Assigned_InGroup", param).ToList();
                return values;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        }
        public Member GetInfor(long id)
        {
            return unitOfWork.Procedure<Member>("mem_Get_MemberInfo",new { MemberID = id }).FirstOrDefault();
        }

        /// <summary>
        /// Get List student report by school id
        /// </summary>
        /// <param name="listParam"></param>
        /// <param name="totalRecord"></param>
        /// <param name="currentId"></param>
        /// <param name="timezone"></param>
        /// <returns>List of Member</returns>
        public IList<Member> GetListStudentReportBySchoolId(MemberListParam listParam, out int? totalRecord, long currentId, int timezone = 0)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@SchoolName", listParam.SchoolName);
            param.Add("@CellPhone", listParam.CellPhone);
            param.Add("@BirthDate", listParam.BirthDate);
            param.Add("@CurrentID", currentId);
            param.Add("@PageNumber", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@SortColumn", listParam.SortColumn);
            param.Add("@SortDirection", listParam.SortDirection);
            param.Add("@TimeZone", timezone);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var values = unitOfWork.Procedure<Member>("mem_Get_Student_Report_School", param).ToList();
            totalRecord = param.GetDataOutput<int>("@TotalRecord");
            return values;
        }

        public IList<Member> GetAllStudentReport(MemberListParam listParam, long currentId)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", listParam.Keyword);
            param.Add("@FirstName", listParam.FirstName);
            param.Add("@Email", listParam.Email);
            param.Add("@SchoolName", listParam.SchoolName);
            param.Add("@CellPhone", listParam.CellPhone);
            param.Add("@BirthDate", listParam.BirthDate);
            param.Add("@CurrentID", currentId);
        
            var values = unitOfWork.Procedure<Member>("mem_Get_All_Student_Report_School", param).ToList();
         
            return values;
        }

        public IList<Member> GetListTeacherByListStudentId(string listStudentId)
        {
            var param = new DynamicParameters();
            param.Add("@ListStringStudentId", listStudentId);

            var teachers = unitOfWork.Procedure<Member>("mem_Get_Invite_TeacherByStudentIDs", param);

            return teachers.ToList();
        }

        public IList<Member> GetListParentByListStudentId(string listStudentId)
        {
            var param = new DynamicParameters();
            param.Add("@ListStringStudentId", listStudentId);

            var teachers = unitOfWork.Procedure<Member>("mem_Get_Parents_ByUserIds", param);

            return teachers.ToList();
        } 

        public int CheckExistsAchApproved(long memberId, int roleId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@RoleID", roleId);
                param.Add("@IsExists", dbType: DbType.Int16, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_Exists_ACH_Approved", param);
                return param.Get<short>("@IsExists");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        /// <summary>
        /// <para>User : nghiatc</para>
        /// <para>2017/05/10</para>
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<Member> GetMembersByParentId(long parentId)
        {
            var param = new DynamicParameters();
            param.Add("@ParentId", parentId);
            var results = unitOfWork.Procedure<Member>("mem_Get_Students_ByParentId", param);

            return results.ToList();
        }

        public IList<Member> GetListMemberChildrenByParentId(long parentId)
        {
            var param = new DynamicParameters();
            param.Add("@ParentId", parentId);

            var result = unitOfWork.Procedure<Member>("mem_Get_Childs_ByParentId", param);
            return result.ToList();
        }

        public IList<Member> GetListChildrenByParentIdWithStatus(long parentId, int isVerified)
        {
            var param = new DynamicParameters();
            param.Add("@ParentId", parentId);
            param.Add("@IsVerified", isVerified);

            var result = unitOfWork.Procedure<Member>("mem_Get_Childs_ByParentId_AllStatus", param);
            return result.ToList();
        }

        public bool SaveListStudentByParentId(long parentId, string listStudentId)
        {
            var param = new DynamicParameters();
            param.Add("@ParentId",parentId);
            param.Add("@ListIdMember", listStudentId);
            param.Add("@Result", dbType:DbType.Boolean,direction: ParameterDirection.Output);
            unitOfWork.ProcedureExecute("mem_Save_ListIdChild_Member", param);
            var result = param.GetDataOutput<bool>("@Result");
            return result;
        }

        public bool CheckParentOfSchool(long schoolId, long parentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@SchoolId", schoolId);
                param.Add("@ParentId", parentId);
                param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_Parent_Of_School", param);
                var value = param.Get<int>("@Result");
                return (value > 0);
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool CheckParentOfTeacher(long teacherId, long parentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@TeacherId", teacherId);
                param.Add("@ParentId", parentId);
                param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("mem_Check_Parent_Of_Teacher", param);
                var value = param.Get<int>("@Result");
                return (value > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class MemberParentDal : BaseDal<ADOProvider>, IDataAccess<MemberParent>
    {
        public MemberParent Get(long id)
        {
            throw new NotImplementedException();
        }

        public IList<MemberParent> Get(BaseListParam param, out int? totalRecord)
        {
            throw new NotImplementedException();
        }

        public long Save(MemberParent obj, long userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@XML", XMLHelper.SerializeXML<MemberParent>(obj));
                param.Add("@MemberParentID", obj.MemberParentID, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
                unitOfWork.ProcedureExecute("mem_Update_Member_ParentInfo", param);
                return param.Get<long>("@MemberParentID");
            }
            catch (SqlException exc1)
            {

            }
            catch (System.InvalidOperationException exc2)
            {

            }
            return 0;
        }

        public bool Delete(long objId, long userID)
        {
            try
            {
                return unitOfWork.ProcedureExecute("mem_Delete_Member_Parents", new { MemberParentID = objId, UserID = userID });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<MemberParent> GetByMemberID(long memberId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                return unitOfWork.Procedure<MemberParent>("mem_Get_Member_Parents_ByMemberID", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IList<MemberParent> GetByMemberID(long memberId, long parentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@MemberID", memberId);
                param.Add("@ParentID", parentId);
                return unitOfWork.Procedure<MemberParent>("mem_Get_Member_Parents_ByMemberID", param).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RemoveParent(long parentID, long memberID)
        {
            try
            {
                return unitOfWork.ProcedureExecute("mem_Delete_Parent_ByUserID", new { ParentID = parentID, MemberID = memberID });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAllParents(long memberID)
        {
            try
            {
                return unitOfWork.ProcedureExecute("mem_Delete_AllParents_ByUserID", new { MemberID = memberID });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerifiedParent(long parentID, long memberID)
        {
            try
            {
                return unitOfWork.ProcedureExecute("mem_Update_VerifyParent", new { ParentID = parentID, MemberID = memberID });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerifiedChildOfParent(long memberParentId, long userId)
        {
            try
            {
                return unitOfWork.ProcedureExecute("mem_Member_Parents_VerifyParent", new { MemberParentID = memberParentId, UserID = userId });
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
