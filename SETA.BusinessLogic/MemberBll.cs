using System;
using System.Collections.Generic;
using System.Linq;
using SETA.BusinessLogic.Interface;
using SETA.Common.Constants;
using SETA.Common.Helper;
using SETA.Core.Base;
using SETA.Core.Helper.Session;
using SETA.Core.Security.Crypt;
using SETA.Core.Singleton;
using SETA.DataAccess;
using SETA.Entity;

namespace SETA.BusinessLogic
{
    public class MemberBll : BaseBll, IMemberBusinessLogic<Member>
    {
        /// <summary>
        /// Get Member by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Member Get(long id)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var obj = ctx.Get(id);
            return obj;
        }

        public Member GetDetailPage(long id)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetDetailPage(id);
        }

        public bool IsNotShowStudentInfo(long id, int roleId)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var obj = ctx.IsNotShowStudentInfo(id, roleId);
            return obj;
        }

        public string GetSchoolNameByTeacherId(long teacherid)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetSchoolNameByTeacherId(teacherid);            
        }

        /// <summary>
        /// Get Member by UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Member Get(string userName)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var obj = ctx.Get(userName);
            return obj;
        }

        public Member GetAllStatusNoDeleted(string userName)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllStatusNoDeleted(userName);
        }

        public Member GetParentByUserName(string userName)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetParentByUserName(userName);
        }
        public int CheckUserExists(string userName)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckUserExists(userName);
        }

        /// <summary>
        /// Get Member by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member GetByEmail(string email)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var obj = ctx.GetByEmail(email);
            return obj;
        }

        public Member GetByUserName(string userName)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var obj = ctx.GetByUserName(userName);
            return obj;
        }
        public Member GetByUserNameOrEmailExits(string userName)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var obj = ctx.GetByUserNameOrEmailExits(userName);
            return obj;
        }
        public Member GetByUserNameAndEmail(string userName, long memberId = 0)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetByUserNameAndEmail(userName, memberId);
        }
        /// <summary>
        /// Get user by forgotCode
        /// </summary>
        /// <param name="forgotCode"></param>
        /// <returns></returns>
        public Member GetUser(Guid forgotCode)
        {
            try
            {
                var ctx = SingletonIpl.GetInstance<MemberDal>();
                return ctx.GetUser(forgotCode);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<Member> GetList(List<long> listMemberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetList(string.Join(",", listMemberId));
        }

        /// <summary>
        /// Get List of Member
        /// </summary>
        /// <param name="listParam"></param>
        /// <param name="totalRecord"></param>
        /// <returns>List of Member</returns>
        public IList<Member> Get(MemberListParam listParam, out int? totalRecord)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.Get(listParam, out totalRecord);
            return values;
        }

        public int GetMemberCountByStatus(MemberListParam listParam)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.GetMemberCountByStatus(listParam);
            return values;
        }

        public IList<Member> GetLogInAs(MemberListParam listParam, out int? totalRecord)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.GetLogInAs(listParam, out totalRecord);
            return values;
        }

        public IList<Member> AdminGetMembers(MemberListParam listParam, out int? totalRecord)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.AdminGet(listParam, out totalRecord);
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
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.GetByRole(listParam, out totalRecord, roleId);
            return values;
        }

        public IList<Member> GetInstructor(long memberid, string listSchool = "")
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.GetInstructor(memberid, listSchool);
            return values;
        }

        public IList<Member> GetMemberInRole(int roleID)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetMemberInRole(roleID);
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
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.GetByRole(listParam, out totalRecord, roleId, currentId);
            return values;
        }

        public int GetCountMembersByCurrentUserStatus(MemberListParam listParam, int roleId, long currentId)
        {
            return SingletonIpl.GetInstance<MemberDal>()
                .GetCountMembersByCurrentUserStatus(listParam, roleId, currentId);
        }
        public IList<Member> GetByRoleAndCurrentUser(MemberListParam listParam, out int? totalRecord, long currentId, string listSchool = "")
        {
            return SingletonIpl.GetInstance<MemberDal>().GetByRoleAndCurrentUser(listParam, out totalRecord, currentId, listSchool);
        }

        public int GetCountMembersByStatusId(MemberListParam listParam, long currentId,
            string listSchool = "")
        {
            return SingletonIpl.GetInstance<MemberDal>().GetCountMembersByStatusId(listParam, currentId, listSchool);
        }

        public IList<Member> GetByRoleAndCurrentUserLogInAs(MemberListParam listParam, out int? totalRecord, long currentId, string listSchool = "")
        {
            return SingletonIpl.GetInstance<MemberDal>().GetByRoleAndCurrentUserLogInAs(listParam, out totalRecord, currentId, listSchool);
        }

        public IList<Member> GetSchoolList(MemberListParam listParam)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetSchoolList(listParam);
        }
        public Member CheckMemberUnderAdmin(long adminId, long memberId, int roleCheck)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckMemberUnderAdmin(adminId, memberId, roleCheck);
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
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            var values = ctx.GetByRoleStudent(listParam, out totalRecord, roleId, currentId);
            return values;
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
            int smsVerify = Common.Utility.Utils.GetSetting(AppKeys.SMS_MOD_VERIFY, 0);
            if (!String.IsNullOrEmpty(obj.CellPhone) && smsVerify == 0)
            {
                obj.VerifySMS = 1;
            }
            if (obj.PromoPercentage == null)
            {
                obj.PromoPercentage = 0;
            }
            return SingletonIpl.GetInstance<MemberDal>().Save(obj, userID);
        }

        public bool SaveStartBillingDate(long memberId, DateTime? startBillingDate, long userId)
        {
            return SingletonIpl.GetInstance<MemberDal>().SaveStartBillingDate(memberId, startBillingDate, userId);
        }

        #endregion

        public long UpdateMostRecent(long userID, long courseID)
        {
            return SingletonIpl.GetInstance<MemberDal>().UpdateMostRecent(userID, courseID);
        }

        /// <summary>
        /// ResetExpireDate
        /// </summary>
        /// <param name="memId"></param>
        /// <param name="forgotExpired"></param>
        /// <param name="forgotCode"></param>
        public void ResetExpireDate(long memId, DateTime forgotExpired, Guid forgotCode)
        {
            SingletonIpl.GetInstance<MemberDal>().ResetExpireDate(memId, forgotExpired, forgotCode);
        }

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public void ResetPassword(long userId, string password)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            ctx.ResetPassword(userId, Md5Util.Md5EnCrypt(password));
        }

        public bool ChangePassword(long memberId, string curPassword, string password)
        {
            var ctx = SingletonIpl.GetInstance<MemberDal>();
            return ctx.ChangePassword(memberId, Md5Util.Md5EnCrypt(curPassword), Md5Util.Md5EnCrypt(password));
        }

        /// <summary>
        /// Delete Member by ID
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool Delete(long memberID, long userID)
        {
            return SingletonIpl.GetInstance<MemberDal>().Delete(memberID, userID);
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
            return SingletonIpl.GetInstance<MemberDal>().CheckExist(obj);
        }

        public object Filter(long memberID, long roleID, string name, out int? totalRecord)
        {
            return SingletonIpl.GetInstance<MemberDal>().Filter(memberID, roleID, name, out totalRecord);
        }

        public IList<Member> GetAllParent(MemberListParam listParam, out int? totalRecord)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllParent(listParam, out totalRecord);
        }

        public IList<Member> GetAllParentBySchoolId(MemberListParam listParam, out int? totalRecord, long schoolId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllParentBySchoolId(listParam, out totalRecord, schoolId);
        }

        public IList<Member> GetAllParentByTeacherId(MemberListParam listParam, out int? totalRecord, long teacherId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllParentByTeacherId(listParam, out totalRecord, teacherId);
        }

        public IList<Member> GetAllChildren(long parentID)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllChildren(parentID);
        }

        public IList<long> GetParents(string listStudents)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetParents(listStudents);
        }

        public IList<long> GetParentsByType(string listStudents, int type)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetParentsByType(listStudents, type);
        }

        public IList<Member> GetParentsByUserID(long userID)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetParentsByUserID(userID);
        }

        public IList<Member> GetParentsVerifiedByUserId(long userID)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetParentsVerifiedByUserId(userID);
        }

        public bool CheckExistEmail(string email)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckExistEmail(email);
        }

        public bool UpdateFavorite(long courseID, long memberID)
        {
            return SingletonIpl.GetInstance<MemberDal>().UpdateFavorite(courseID, memberID);
        }

        public bool CheckCourseIsFavorite(long courseId, long memberID)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckCourseIsFavorite(courseId, memberID);
        }

        public List<Member> GetByGroupId(int groupId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetByGroupId(groupId);
        }

        public IList<Member> GetByAssignmentId(long assignmentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetByAssignmentId(assignmentId);
        }

        public IList<Member> GetForDashboardTeacher(long memberId, out int? totalRecord)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetForDashboardTeacher(memberId, out totalRecord);
        }

        public IList<Member> GetForDashboardTeacher(long memberId, int? isSplitActive , out int? totalRecord,
            out int? totalRecordStatus, int statusId = 1)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetForDashboardTeacher(memberId, isSplitActive, out totalRecord,out totalRecordStatus,statusId);
        }

        public IList<Member> GetMemberByUserNameOrEmail(string UserName, string Email)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetMemberByUserNameOrEmail(UserName, Email);
        }

        public IList<Member> GetMemberByUserNameOrEmailInActive(string UserName, string Email)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetMemberByUserNameOrEmail(UserName, Email, GlobalStatus.InActive);
        }

        public int CountMemberInRole(int RoleID, out int? totalRecord, long currentMemberId = 0)
        {
            return SingletonIpl.GetInstance<MemberDal>().CountMemberInRole(currentMemberId, RoleID, out totalRecord);
        }

        public IList<Member> GetListTeacherInviteByStudent(long memberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListTeacherInviteByStudent(memberId);
        }
        public IList<Member> GetListMemberInvite(long memberId, int memberTypeID)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListMemberInvite(memberId, memberTypeID);
        }        
        public IList<Member> GetListSchoolInviteByTeacher(long memberId, IList<Member> listMember)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListSchoolInviteByTeacher(memberId, listMember);
        }
        public IList<Member> GetListSchoolInviteByStudent(long memberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListSchoolInviteByStudent(memberId);
        }
        public IList<Member> GetListTeacherBySchool(long memberId, IList<long> listMember)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListTeacherBySchool(memberId, listMember);
        }

        public IList<Member> GetListTeacherBySchool(long memberId, IList<Member> listMember)
        {
            var listLongMember = new List<long>();
            if (listMember != null && listMember.Count > 0)
            {
                foreach (var member in listMember)
                {
                    listLongMember.Add(member.MemberID);
                }
            }
            return SingletonIpl.GetInstance<MemberDal>().GetListTeacherBySchool(memberId, listLongMember);
        }

        public int ViewHomeworkDetail(long homeworkID, long memberID)
        {
            return SingletonIpl.GetInstance<MemberDal>().ViewHomeworkDetail(homeworkID, memberID);
        }

        public IList<Member> GetAllAdmin(long curentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllAdmin(curentId);
        }

        public IList<Member> GetMembersByRoleByCurrentUser(int getRole, long currentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetMembersByRoleByCurrentUser(getRole, currentId);
        }

        public int checkStudentOfTeacher(long teacherId, long memberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().checkStudentOfTeacher(teacherId, memberId);
        }
        public IList<Member> GetSchoolMembers(int roleId, long curUserID)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetSchoolMembers(roleId, curUserID);
        }

        public void UpdateListTeacher(long TeacherID, long memberID)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateListTeacher(TeacherID,memberID);
        }
        public void UpdateListTeacherWithOwnerSchool(long TeacherID, long memberID)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateListTeacherWithOwnerSchool(TeacherID, memberID);
        }
        public void UpdateListMember(long TeacherID, long memberID, string memberTypeID)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateListMember(TeacherID, memberID, memberTypeID);
        }
        public void DeleteListTeacher(long memberID, long curMemberID = 0, int curRoleID = 0)
        {
            SingletonIpl.GetInstance<MemberDal>().DeleteListTeacher(memberID, curMemberID, curRoleID);
        }
        public void DeleteListMember(long memberID, string memberTypeID)
        {
            SingletonIpl.GetInstance<MemberDal>().DeleteListMember(memberID, memberTypeID);
        }
        public void UpdateMemberAdmin(long adminID, long memberID, string membertypeID)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateMemberAdmin(adminID, memberID, membertypeID);
        }
        public void UpdateTimeZoneUser(long memberID)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateTimeZoneUser(memberID);
        }
        public void UpdateTeacherNameForOrder(long studentId)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateTeacherNameForOrder(studentId);
        }
        public void UpdateLogTimeZoneUser(long memberId, int timeZoneId, long currentId)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateLogTimeZoneUser(memberId, timeZoneId, currentId);
        }
        public void UpdateTimeZoneAllUserUnderFirstSchool(long memberId)
        {
            SingletonIpl.GetInstance<MemberDal>().UpdateTimeZoneAllUserUnderFirstSchool(memberId);
        }

        public int checkParentStudentAndTeacher(long teacherId, long parentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().checkParentStudentAndTeacher(teacherId, parentId);
        }

        public int CountAgeMember(DateTime bday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;
            if (bday > today.AddYears(-age)) age--;
            return age;
        }

        public IList<Member> GetMembers(MemberListParam model, ref int? totalRecord, Member currentUser)
        {
            IList<Member> dataResult = new List<Member>();
            if (currentUser != null && model != null && totalRecord != null)
            {
                if (currentUser.IsInRole(RoleGroup.Teacher))
                {
                    //filter by Current User
                    dataResult = SingletonIpl.GetInstance<MemberBll>().GetByRole(model, out totalRecord, ConvertHelper.ToInt32(currentUser.RoleID), currentUser.MemberID);
                }
                else if (currentUser.IsInRole(RoleGroup.School))
                {
                    dataResult = SingletonIpl.GetInstance<MemberBll>()
                        .GetByRoleAndCurrentUser(model, out totalRecord, currentUser.MemberID);
                }
                else
                {
                    dataResult = SingletonIpl.GetInstance<MemberBll>().Get(model, out totalRecord);
                }
            }            
            return dataResult;
        }

        public Member GetByCellPhone(string cellPhone, int phoneCode, long currentID)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetByCellPhone(cellPhone, phoneCode, currentID);
        }
        public Member GetCellPhoneVerify(string cellPhone, int phoneCode)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetCellPhoneVerify(cellPhone, phoneCode);
        }
        public long SaveCodeVerifyPhone(string cellPhone, long currentID, string code)
        {
            return SingletonIpl.GetInstance<MemberDal>().SaveCodeVerifyPhone(cellPhone, currentID, code);
        }
        public Member CheckCodeVerifyPhone(string cellPhone, string sameCellPhone, long currentID, string code)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckCodeVerifyPhone(cellPhone, sameCellPhone, currentID, code);
        }
        public void SaveVeifySMSPhone(string cellPhone, long memberID, string flag, string phoneCode, int verifySMS)
        {
            SingletonIpl.GetInstance<MemberDal>().SaveVeifySMSPhone(cellPhone, memberID, flag, phoneCode, verifySMS);
        }
        public void SaveConfigNotify(long memberID, int frequencyType, string frequencyTime, string frequencyData)
        {
            SingletonIpl.GetInstance<MemberDal>().SaveConfigNotify(memberID, frequencyType, frequencyTime, frequencyData);
        }
        public DateTime? GetDateTimeLogSMSLast(long userIDSent, long userIDReceive, int type)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetDateTimeLogSMSLast(userIDSent, userIDReceive, type);
        }
        public DateTime? GetDateTimeLoEmailLast(long userIDSent, long userIDReceive, int type)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetDateTimeLoEmailLast(userIDSent, userIDReceive, type);
        }
        public void SaveLogSMSSent(long userIDSent, long userIDReceive,string content, int type)
        {
            SingletonIpl.GetInstance<MemberDal>().SaveLogSMSSent(userIDSent, userIDReceive, content, type);
        }
        public void SaveLogEmailSent(long userIDSent, long userIDReceive, string email, string content, int type)
        {
            SingletonIpl.GetInstance<MemberDal>().SaveLogEmailSent(userIDSent, userIDReceive, email, content, type);
        }
        #region Get For Chat

        public IList<Member> GetListUserForChat(long currentId, int pageNumber = 1, int pageSize = 20)
        {
            return SingletonIpl.GetInstance<MemberDal>()
                .GetListUserForChat(currentId, pageNumber, pageSize);
        }
        public IList<Member> GetListContactForChat(long currentId, int pageNumber = 1, int pageSize = 20, int currentRoleId = 0)
        {
            return SingletonIpl.GetInstance<MemberDal>()
                .GetListContactForChat(currentId, pageNumber, pageSize, currentRoleId);
        }
        public Member GetInforMember(long currentId, long memberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetInforMember(currentId, memberId);
        }
        public Member GetInforReceiveMember(long currentId, long memberId, int contactTypeId = 0)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetInforReceiveMember(currentId, memberId, contactTypeId);
        }

        public Member GetTotalUnreadMessage(long currentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetTotalUnreadMessage(currentId);
        }

        #endregion

        #region Get For Dashboard

        public MemberDashboardInfo GetDataDashboard(Member currentMember)
        {
            if (currentMember != null && currentMember.MemberID > 0)
            {
                if (currentMember.IsInRole(RoleGroup.AdminSuper))
                {
                    return SingletonIpl.GetInstance<MemberDal>().GetDataDashboardForSuperAdmin();
                }
                if (currentMember.IsInRole(RoleGroup.Admin))
                {
                    return SingletonIpl.GetInstance<MemberDal>().GetDataDashboardForAdmin(currentMember.MemberID);
                }
                if (currentMember.IsInRole(RoleGroup.School))
                {
                    return SingletonIpl.GetInstance<MemberDal>().GetDataDashboardForSchool(currentMember.MemberID);
                }
            }
            return new MemberDashboardInfo();
        }

        #endregion


        #region Get List School

        public Member GetFirstSchoolOfTeacher(long teacherId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetFirstSchoolOfTeacher(teacherId);
        }

        //Get List School Member
        public IList<Member> GetListSchoolOfTeacherId(long teacherId)
        {
            try
            {                
                return SingletonIpl.GetInstance<MemberDal>()
                    .GetMembersByRoleByCurrentUser(ConvertHelper.ToInt32(RoleGroup.School), teacherId);
            }
            catch (Exception)
            {
                return new List<Member>();                
            }            
        }

        #endregion


        #region Get List Teacher

        public IList<Member> GetListTeacherIsAssignedInGroup(int groupId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListTeacherIsAssignedInGroup(groupId);
        }
        public IList<Member> GetListTeacherOfCurrentRole(int currentRole, long currentUserId)
        {
            try
            {
                var result = new List<Member>();
                if (currentRole == ConvertHelper.ToInt32(RoleGroup.AdminSuper))
                {
                    var model = new MemberListParam
                    {
                        PageIndex = 1,
                        PageSize = int.MaxValue,
                        CurrentID = currentUserId,
                        RoleCurrentUser = currentRole,
                        ShowRole = ConvertHelper.ToInt32(RoleGroup.Teacher),
                        SortColumn = "Name",
                        SortDirection = "ASC"
                    };
                    int? totalRecord = 0;
                    result = SingletonIpl.GetInstance<MemberBll>().Get(model, out totalRecord).Where(m => m.Status == GlobalStatus.Active).ToList();
                }
                if (currentRole == ConvertHelper.ToInt32(RoleGroup.Admin))
                {
                    result = GetListTeacherOfAdmin(currentUserId).ToList();
                }
                if (currentRole == ConvertHelper.ToInt32(RoleGroup.School))
                {
                    result = GetListTeacherOfSchool(currentUserId).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                return new List<Member>();
            }
        } 
        public IList<Member> GetListTeacherOfAdmin(long adminId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>()
                    .GetMembersByRoleByCurrentUser(ConvertHelper.ToInt32(RoleGroup.Teacher), adminId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        public IList<Member> GetListTeacherOfSchool(long schoolId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>()
                    .GetMembersByRoleByCurrentUser(ConvertHelper.ToInt32(RoleGroup.Teacher), schoolId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        public IList<Member> GetListTeacherIdOfSchool(long schoolId, int page, int pageSize, out int? numPage)
        {
            try
            {
                
                return SingletonIpl.GetInstance<MemberDal>().GetListTeacherIdOfSchool(schoolId, page, pageSize, out numPage);
            }
            catch (Exception)
            {
                numPage = 0;
                return new List<Member>();
            }
        }
        public IList<Member> GetListTeacherIdOfSchool(long schoolId)
        {
            try
            {

                return SingletonIpl.GetInstance<MemberDal>().GetListTeacherIdOfSchool(schoolId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        public IList<Member> GetListTeacherIdByInstrumentId(long schoolId, int page, int pageSize, out int? numPage)
        {
            try
            {

                return SingletonIpl.GetInstance<MemberDal>().GetListTeacherIdByInstrumentId(schoolId, page, pageSize, out numPage);
            }
            catch (Exception)
            {
                numPage = 0;
                return new List<Member>();
            }
        }
        public IList<Member> GetListByInstrument(long instrumentId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>()
                    .GetListByInstrument(instrumentId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        public IList<Member> WgGetListTeacherByMember(long memberId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>()
                    .WgGetListTeacherByMember(memberId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }

        public IList<Member> GetListTeacherFree()
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListTeacherFree();
        }

        public IList<Member> GetListTeacherOfOnlyThisStudent(long studentId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>()
                    .GetMembersByRoleByCurrentUser(ConvertHelper.ToInt32(RoleGroup.Teacher), studentId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }

        #endregion


        #region Get List Student

        //Get List Student
        public IList<Member> GetListStudentOfTeacher(long teacherId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>()
                    .GetMembersByRoleByCurrentUser(ConvertHelper.ToInt32(RoleGroup.Student), teacherId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        public IList<Member> GetListStudentByTeacher(long teacherId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>().GetListStudentByTeacher(teacherId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        public IList<Member> GetListStudentOfSchool(long schoolId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>()
                    .GetMembersByRoleByCurrentUser(ConvertHelper.ToInt32(RoleGroup.Student), schoolId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }

        public IList<Member> GetListTeacherOfStudent(long studentId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberDal>().GetListTeacherOfStudent(studentId);
            }
            catch (Exception)
            {
                return new List<Member>();
            }
        }
        
        public int UpdateTeacherPay(long studentId, string lstTeacherPay, long schoolId)
        {
            return SingletonIpl.GetInstance<MemberDal>().UpdateTeacherPay(studentId, lstTeacherPay, schoolId);
        }
        public IList<Member> GetListStudentOfTeacherForSchedule(bool isAssigned, long teacherId, long schoolId = 0)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListStudentOfTeacherForSchedule(isAssigned, teacherId, schoolId);
        }

        public long SavePlanHistory(long planID, long studentId, long teacherID, bool isConfirmOK = false,
            string creditCardType = "", string creditCardTypeMain = "", string transactionid = "",
            bool planIsRequired = false, long recurringPaymentHistoryId = 0, long schoolID = 0, long memberBillingInfoId = 0, 
            DateTime paidDate = default(DateTime), DateTime dueDate = default(DateTime), int paymentStatusID = 0, 
            long memberOwnerId = 0, long instrumentId = 0, long paymentMethodId = 0, int unassign = 0, decimal ProratedFee = 0, long receiverMemberId = 0, bool isOneTimePaymentFee = false, long bookingId = 0, long paymentHistoryIDOnTimeFee = 0,string promoCode="")
        {
            if (planID > 0 && studentId > 0)
            {
                //SuNV 2016-09-29 TZ-2875: Add transactionID(for refund)
                return SingletonIpl.GetInstance<MemberDal>()
                    .SavePlanHistory(planID, studentId, teacherID, isConfirmOK, creditCardType, creditCardTypeMain,
                        default(DateTime), default(DateTime), transactionid, planIsRequired, recurringPaymentHistoryId,
                        schoolID, memberBillingInfoId, paidDate, dueDate, paymentStatusID, memberOwnerId, instrumentId,
                        paymentMethodId, unassign, ProratedFee, receiverMemberId, isOneTimePaymentFee, bookingId, paymentHistoryIDOnTimeFee,promoCode);
                //End
            }
            return 0;
        }

        public long CreateOrderOnTimePaymentFee(long planID, long studentId, long teacherID, bool isConfirmOK = false,
            string creditCardType = "", string creditCardTypeMain = "", string transactionid = "",
            bool planIsRequired = false, long recurringPaymentHistoryId = 0, long schoolID = 0, long memberBillingInfoId = 0,
            DateTime paidDate = default(DateTime), DateTime dueDate = default(DateTime), int paymentStatusID = 0,
            long memberOwnerId = 0, long instrumentId = 0, long paymentMethodId = 0, int unassign = 0, decimal ProratedFee = 0, long receiverMemberId = 0, bool isOneTimePaymentFee = false, long bookingId = 0, long paymentHistoryIDOnTimeFee = 0)
        {
            if (planID > 0 && studentId > 0)
            {
                //SuNV 2016-09-29 TZ-2875: Add transactionID(for refund)
                return SingletonIpl.GetInstance<MemberDal>()
                    .CreateOrderOnTimePaymentFee(planID, studentId, teacherID, isConfirmOK, creditCardType, creditCardTypeMain,
                        default(DateTime), default(DateTime), transactionid, planIsRequired, recurringPaymentHistoryId,
                        schoolID, memberBillingInfoId, paidDate, dueDate, paymentStatusID, memberOwnerId, instrumentId,
                        paymentMethodId, unassign, ProratedFee, receiverMemberId, isOneTimePaymentFee, bookingId, paymentHistoryIDOnTimeFee);
                //End
            }
            return 0;
        }

        public long SavePlanHistoryWithAmount(long planID, long studentId, long teacherID, bool isConfirmOK = false,
            string creditCardType = "", string creditCardTypeMain = "", string transactionid = "",
            bool planIsRequired = false, long recurringPaymentHistoryId = 0, long schoolID = 0,
            long memberBillingInfoId = 0, DateTime paidDate = default(DateTime), DateTime dueDate = default(DateTime),
            int paymentStatusID = 0, long memberOwnerId = 0, long instrumentId = 0, int paymentMethodId = 0,
            int unassign = 0, decimal amount = 0, string note = "", long receiverMemberId = 0)
        {
            if (planID > 0 && studentId > 0)
            {
                //SuNV 2016-09-29 TZ-2875: Add transactionID(for refund)
                return SingletonIpl.GetInstance<MemberDal>()
                    .SavePlanHistoryWithAmount(planID, studentId, teacherID, isConfirmOK, creditCardType, creditCardTypeMain,
                        default(DateTime), default(DateTime), transactionid, planIsRequired, recurringPaymentHistoryId,
                        schoolID, memberBillingInfoId, paidDate, dueDate, paymentStatusID, memberOwnerId, instrumentId,
                        paymentMethodId, unassign, amount, note, receiverMemberId);
                //End
            }
            return 0;
        }

        public bool UpdatePlanHistory(long paymentHistoryID, string transactionID, long memberID, long memberBillingInfoID, long receiverMemberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().UpdatePlanHistory(paymentHistoryID, transactionID, memberID, memberBillingInfoID, receiverMemberId);
        }

        public bool UpdateCalendarByPayment(long paymentHistoryID, long memberID, short statusid = 1)
        {
            return SingletonIpl.GetInstance<MemberDal>().UpdateCalendarByPayment(paymentHistoryID, memberID, statusid);
        }

        #endregion


        #region Get For Parents

        public IList<Member> GetPagingChildrenByParentID(MemberListParam listParam, out int? totalRecord, long currentId)
        {
            return SingletonIpl.GetInstance<MemberDal>()
                .GetPagingChildrenByParentID(listParam, out totalRecord, currentId);
        }

        #endregion



        #region Check for Student

        //Check Student
        public bool CheckStudentCanViewCourse(long createCourseUserId, short optionShare, long studentId)
        {
            try
            {
                if (createCourseUserId > 0 && studentId > 0)
                {
                    switch (optionShare)
                    {
                        case ShareWith.YourSchool:
                            var listStudentOfCreateTeacher = GetListStudentOfTeacher(createCourseUserId);
                            if (listStudentOfCreateTeacher.FirstOrDefault(student => student.MemberID == studentId) != null)
                            {
                                return true;
                            }

                            var listSchoolOfCreateTeacher = GetListSchoolOfTeacherId(createCourseUserId);
                            if (listSchoolOfCreateTeacher != null && listSchoolOfCreateTeacher.Count > 0)
                            {
                                return listSchoolOfCreateTeacher.Select(school => GetListStudentOfSchool(school.MemberID)).Where(listStudentOfSchool => listStudentOfSchool != null && listStudentOfSchool.Count > 0).Any(listStudentOfSchool => listStudentOfSchool.Any(student => student.MemberID == studentId));
                            }                            
                            return false;
                    }
                    return false;
                }
                return false;                
            }
            catch (Exception)
            {
                return false;                
            }            
        }

        #endregion


        #region Check for Teacher

        public bool CheckTeacherCanViewCourse(long createCourseUserId, short optionShare, long teacherId)
        {
            try
            {
                if (createCourseUserId > 0 && teacherId > 0)
                {
                    switch (optionShare)
                    {
                        case ShareWith.YouOnly:
                            return createCourseUserId == teacherId;
                        case ShareWith.YourStudent:
                            return createCourseUserId == teacherId;
                        case ShareWith.YourSchool:
                            var listSchoolOfThisTeacher = GetListSchoolOfTeacherId(teacherId);
                            var listSchoolOfCreateTeacher = GetListSchoolOfTeacherId(createCourseUserId);
                            if (listSchoolOfThisTeacher != null && listSchoolOfThisTeacher.Count > 0)
                            {
                                if (listSchoolOfThisTeacher.Any(schoolThisTeacher => listSchoolOfCreateTeacher.Any(schoolCreateTeacher => schoolThisTeacher.MemberID == schoolCreateTeacher.MemberID)))
                                {
                                    return true;
                                }
                            }
                            return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion


        #region Check for School

        public bool CheckSchoolCanViewCourse(long createCourseUserId, short optionShare, long schoolId)
        {
            try
            {
                if (createCourseUserId > 0 && schoolId > 0)
                {
                    switch (optionShare)
                    {
                        case ShareWith.YouOnly:
                            return false;
                        case ShareWith.YourStudent:
                            return false;
                        case ShareWith.YourSchool:                            
                            var listSchoolOfCreateTeacher = GetListSchoolOfTeacherId(createCourseUserId);
                            if (listSchoolOfCreateTeacher != null && listSchoolOfCreateTeacher.Count > 0)
                            {
                                return listSchoolOfCreateTeacher.Select(school => school.MemberID == schoolId).FirstOrDefault();
                            }
                            return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

 
        #region Check for Admin

        public bool CheckAdminCanViewCourse(long createCourseUserId, short optionShare, long adminId)
        {
            try
            {
                if (createCourseUserId > 0 && adminId > 0)
                {
                    switch (optionShare)
                    {
                        case ShareWith.YouOnly:
                            return false;
                        case ShareWith.YourStudent:
                            return false;
                        case ShareWith.YourSchool:
                            var listTecherOfAdmin = GetListTeacherOfAdmin(adminId);
                            if (listTecherOfAdmin != null && listTecherOfAdmin.Count > 0)
                            {
                                return listTecherOfAdmin.Any(teacher => teacher.MemberID == createCourseUserId);
                            }
                            return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }        

        #endregion

        //ThangND [2016-10-11] [TZ-2880: Update Member Contacts - School/Teacher can send an email and SMS notification]
        #region search

        public IList<Member> GetMemberForSuggestSearch(string keyword, int roleId, long schoolId = 0, long schoolIDSelected = 0, int typeSearch = 1, long teacherId = 0)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetMemberForSuggestSearch(keyword, roleId, schoolId, schoolIDSelected, typeSearch, teacherId);
        }

        public IList<Member> GetTeachersBySchool(string keyword, long schoolId)
        { 
            return SingletonIpl.GetInstance<MemberDal>().GetTeachersBySchool(keyword, schoolId);
        }

        public List<Member> SearchAllMembersByCurrentUser(long currentId, int currentRole, string keyword)
        {
            return SingletonIpl.GetInstance<MemberDal>().SearchAllMembersByCurrentUser(currentId, currentRole, keyword);
        }

        #endregion
        //End

        public bool DeleteMemberContact(List<long> lstMemberId, long contactId)
        {
            return SingletonIpl.GetInstance<MemberDal>().DeleteMemberContact(lstMemberId, contactId);
        }
        public void AddMemberContact(List<long> lstMemberId, long contactId)
        {
            SingletonIpl.GetInstance<MemberDal>().AddMemberContact(lstMemberId, contactId);
        }                       

        public int CheckOwnerOfStudent(long studentId, long memberId, int roleId)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckOwnerOfStudent(studentId, memberId, roleId);
        }

        public bool IsTeacherFree(long teacherId)
        {
            return SingletonIpl.GetInstance<MemberDal>().IsTeacherFree(teacherId);
        }

        public bool CheckTeacherHasSchool(long memberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckTeacherHasSchool(memberId);
        }

        //ThangND [2016-10-28] [TZ-3146: [Chat][Email] Send to is clear after change role]
        public List<Member> GetMemberByRole(long memberId, int currentRoleId, int fillterRole, int checkType, string memberIdMore = "")
        {
            return SingletonIpl.GetInstance<MemberDal>().GetMemberByRole(memberId, currentRoleId, fillterRole, checkType, memberIdMore);
        }
        //End

        public IList<Member> GetTeacherOrSchoolOfStudent(long memberId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetTeacherOrSchoolOfStudent(memberId);
        }
        public IList<Member> GetAllStudentByRoleAndMemberId(long memberId, int roleId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllStudentByRoleAndMemberId(memberId, roleId);
        }
        public void SetNotificationSettingForMember(long currentMemberId, long memberId, int memberType, long schoolId = 0, long teacherId= 0, long studentId = 0)
        {
            SingletonIpl.GetInstance<MemberDal>().SetNotificationSettingForMember(currentMemberId, memberId, memberType, schoolId, teacherId, studentId);
        }
        public Member GetInfor(long id)
        {
            return SingletonIpl.GetInstance<MemberDal>().Get(id);
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
            return SingletonIpl.GetInstance<MemberDal>().GetListStudentReportBySchoolId(listParam,out totalRecord,currentId, timezone);
		}

        public IList<Member> GetAllStudentReport(MemberListParam listParam, long currentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetAllStudentReport(listParam, currentId);
        }

        public IList<Member> GetListTeacherByListStudentId(string listStudentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListTeacherByListStudentId(listStudentId);
        }

        public IList<Member> GetListParentByListStudentId(string listStudentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListParentByListStudentId(listStudentId);
        }

        public int CheckExistsAchApproved(long memberId, int roleId)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckExistsAchApproved(memberId,roleId);
        }
        public IList<Member> GetMembersByParentId(long parentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetMembersByParentId(parentId);
        }

        public IList<Member> GetListMemberChildrenByParentId(long parentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListMemberChildrenByParentId(parentId);
        }

        public IList<Member> GetListChildrenByParentIdWithStatus(long parentId, int isVerified)
        {
            return SingletonIpl.GetInstance<MemberDal>().GetListChildrenByParentIdWithStatus(parentId, isVerified);
        }

        public bool SaveListStudentByParentId(long parentId, string listStudentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().SaveListStudentByParentId(parentId, listStudentId);
        }

        public bool CheckParentOfSchool(long schoolId, long parentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckParentOfSchool(schoolId, parentId);
        }

        public bool CheckParentOfTeacher(long teacherId, long parentId)
        {
            return SingletonIpl.GetInstance<MemberDal>().CheckParentOfTeacher(teacherId, parentId);
        }
    }

    public class MemberParentBll : BaseBll, IBusinessLogic<MemberParent>
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
                return SingletonIpl.GetInstance<MemberParentDal>().Save(obj, userID);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Delete(long objId, long userID)
        {
            return SingletonIpl.GetInstance<MemberParentDal>().Delete(objId, userID);
        }

        public IList<MemberParent> GetByMemberID(long memberId)
        {
            try
            {
                return SingletonIpl.GetInstance<MemberParentDal>().GetByMemberID(memberId);
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
                return SingletonIpl.GetInstance<MemberParentDal>().GetByMemberID(memberId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RemoveParent(long parentID, long memberID)
        {
            return SingletonIpl.GetInstance<MemberParentDal>().RemoveParent(parentID, memberID);
        }

        public bool DeleteAllParents(long memberID)
        {
            return SingletonIpl.GetInstance<MemberParentDal>().DeleteAllParents(memberID);
        }
        public bool VerifiedParent(long parentID, long memberID)
        {
            return SingletonIpl.GetInstance<MemberParentDal>().VerifiedParent(parentID, memberID);
        }

        public bool VerifiedChildOfParent(long memberParentId, long userId)
        {
            return SingletonIpl.GetInstance<MemberParentDal>().VerifiedChildOfParent(memberParentId, userId);
        }

    }
}
