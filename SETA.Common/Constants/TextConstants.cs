using System;
using System.ComponentModel;

namespace SETA.Common.Constants
{
    public partial class Constants
    {
        public const string Text_Yes = "Yes";
        public const string Text_No = "No";
        public const string Text_Save = "Save";
        public const string Text_Cancel = "Cancel";

        public const string SEPARATOR_COMMA = ",";

        //Difinication message
        public const string MSG_SUCCESSFUL = "Successfully.";
        public const string MSG_SUCCESSFUL_PARAM = "{0} successfully.";
        public const string MSG_UNSUCCESSFUL = "Unsuccessfully.";
        public const string MSG_UNSUCCESSFUL_PARAM = "{0} unsuccessfully.";
        public const string MSG_SAVE_SUCCESSFUL = "Successfully Saved!";
        //ThangND [2016-10-11] [TZ-2880: Update Member Contacts - School/Teacher can send an email and SMS notification]
        public const string MSG_SEND_SUCCESSFUL = "Send Successfully!";
        //End
        //SuNV updated 2016-10-05
        public const string MSG_TRANS_INPROCESSING = "Transaction is in processing";
        //End
        public const string MSG_DELETE_UNSUCCESSFUL = "Unsuccessfully Deleted.";
        public const string MSG_DELETE_SUCCESSFUL = "Successfully Deleted.";
        public const string MSG_SAVE_UNSUCCESSFUL = "Unsuccessfully Saved!";
        public const string MSG_SAVE_UNSUCCESSFUL_PRECISEPAY_APPROVED = "Unsuccessfully Saved! Merchant Information is approved.";
        public const string MSG_SAVE_FOLLOWING_SUCCESSFUL = "Successfully saved with following!";
        public const string MSG_DUPLICATE = "{0} is duplicated.";
        public const string MSG_UNSUCCESSFUL_CHANGE_PAYMENT = "Unsuccessfully! You only can change Payment Method before Due Date";
        public const string MSG_UNSUCCESSFUL_NEED_PLAN = "Please choose Plan!";
        public const string MSG_SUCCESSFUL_GENERATE = "Generate Sucessfully!";
        public const string MSG_UNSUCCESSFUL_GENERATE = "Generate Unsucessfully!";
        public const string MSG_SCHEDULE_CONFLICK_WITH_BLOCKED_OFF_TIME = "This recurring Schedule conflicts with another Blocked Off Time. Do you still want to add this, the Schedule conflicts will be hidden?";
        public const string MSG_SCHEDULE_NO_LESSON = "There are no Lesson Schedule.";

        //public const string MSG_LOGIN_FAIL = "The Username, Password is incorrect.";
        public const string MSG_LOGIN_FAIL = "The {0}, Password is incorrect.";
        public const string MSG_FORGOT_FAIL = "Can't Forgot Password because email of user empty";
        public const string MSG_NOT_EXIST = "The {0} does not exist";
        public const string MSG_EXPIRED = "{0} has expired.";
        public const string MSG_CURRENT_PWD_FAIL = "The current password is incorrect.";
        public const string MSG_ALREADY_INVITED = "You have already invited this {0}. Please select another {0}.";
        public const string MSG_CANNOT_INVITE = "This {0} isn't a student email. Please input another {0}.";
        public const string MSG_SCHEDULE_OUT_OPEN_TIME = "Length Of Time must be less than or equal to Open time";
        public const string MSG_RESTORE_OUT_OPEN_TIME = "Restore Lesson not equal to Open time";

        public const string MAIL_TITLE_FORGET_PWD = "Forgot Password";
        public const string MAIL_ACTIVATE_ACCOUNT = "Welcome to TeacherZone";
        public const string MAIL_VERIFY_ACCOUNT = "TeacherZone: Verify your children account.";
        public const string MAIL_TITLE_INVITE = "Invitation from TeacherZone";
        public const string MAIL_FEEDBACK = "TeacherZone: new feedback";
        public const string MAIL_TITLE_REQUEST_DEMO = "TeacherZone: Request a Demo";
        public const string MAIL_SEND_FOR_USER = "TeacherZone: {0} has sent you an email: ";
        public const string MAIL_SEND_FOR_USER_REMINDER = "TeacherZone Reminder: {0} has sent you an email remind: ";
        public const string MAIL_SEND_FOR_PARENT_NOTIFY_CONNECT = "TeacherZone Notifier: {0} has connected to his/her teacher ";
        public const string MSG_LOGIN_FAIL_NOT_ACTIVE = "This user is not active.";
        public const string MAIL_TITLE_CONFIRM_SCHEDULE = "TeacherZone: You have new Schedule.";
        public const string MAIL_TITLE_DELETE_SCHEDULE = "TeacherZone: Your schedule has been removed.";
        public const string MAIL_TITLE_EDIT_SCHEDULE = "TeacherZone: Your schedule has been changed.";
        public const string MAIL_TITLE_BANK_LESSON = "TeacherZone: Banked lesson";
        public const string MAIL_TITLE_RESTORE_BANK_LESSON = "TeacherZone: Restored lesson";        

        public const string MSG_STUDENT_ADD_PLAN = "Register a plan";
        public const string MSG_STUDENT_ADD_PLAN_RECURRING = "Register recurring a plan";
        public const string MAIL_TITLE_CONFIRM_REGISTER_PLAN = "TeacherZone: Confirm re-register plan {0}";

        //Add/edit parent confirm msg
        public const string MSG_CONFIRM_ADDEDIT_PARENT_SPADMIN = "<b>This parent account already exists in TeacherZone. Do you want to import that account information?</b>";
        public const string MSG_CONFIRM_ADDEDIT_PARENT_OTHER = "<b>This parent account already exists in TeacherZone. Do you want to import that account information?</b>";
        public const string MSG_ERROR_DISCOUNT_GREATER_PRICE = "Discount must be less than or equal price";
        public const string MSG_ERROR_DISCOUNT_GREATER_PRICE2 = "Discount must be less than or equal to ${0}";

        //for homepage
        public const string HOME_STUDENT = "HOME_STUDENT";
        public const string HOME_TEACHER = "HOME_TEACHER";
        public const string HOME_ABOUT = "HOME_ABOUT";
        public const string HOME_PRICING = "HOME_PRICING";
        public const string HOME_MAP = "HOME_MAP";

        //for library
        public const string LIBRARY_TERM_CONDITION = "LIBRARY_TERM_CONDITION";

        public const string CACHING_HOME = "TZ_HOMEPAGE";


        // add type in assign page
        public const int ASSIGN_FILTER_TYPE_COURSE = 1;
        public const int ASSIGN_FILTER_TYPE_STUDENT = 2;
        public const int ASSIGN_FILTER_TYPE_GROUP = 3;

        //images config
        public const int IMAGE_SIZE_THUMB_WIDTH = 300;
        public const int IMAGE_SIZE_THUMB_HEIGHT = 300;
        public const int IMAGE_SIZE_TIPS_WIDTH = 300;
        public const int IMAGE_SIZE_TIPS_HEIGHT = 300;
        public const int IMAGE_SIZE_AVATAR_WIDTH = 150;
        public const int IMAGE_SIZE_AVATAR_HEIGHT = 150;
        public const int EDITOR_MAX_LENGTH = 1000;

        //notify email from admin
        public const long EMAIL_NOTIFY_FROM_ADMIN_ID = 6;

        public const string ORIGINAL_IMAGE = "original";
        //last conversation chat
        public const string CHAT_LAST_CONVERSATION_TODAY = "Today";
        public const string CHAT_LAST_CONVERSATION_YESTERDAY = "Yesterday";

        //Sort Homework
        public const int HOMEWORK_SORT_DUEDATE_DESC = 1;
        public const int HOMEWORK_SORT_NAME_DESC = 2;
        public const int HOMEWORK_SORT_DUEDATE_ASC = 3;
        public const int HOMEWORK_SORT_NAME_ASC = 4;
        public const string HOMEWORK_DUEDATE_COLUMN = "HomeWorkDue";
        public const string HOMEWORK_NAME_COLUMN = "AssignmentName";

        //Sort Course
        public const int COURSE_SORT_RECENT_DESC = 1;
        public const int COURSE_SORT_NAME_DESC = 2;
        public const int COURSE_SORT_RECENT_ASC = 3;
        public const int COURSE_SORT_NAME_ASC = 4;
        public const string COURSE_RECENT_COLUMN = "CreatedDate";
        public const string COURSE_NAME_COLUMN = "SongTitle";

        //Sort Member
        public const int MEMBER_SORT_LASTLOGIN_DESC = 1;
        public const int MEMBER_SORT_LASTLOGIN_ASC = 2;
        public const int MEMBER_SORT_NAME_DESC = 3;
        public const int MEMBER_SORT_NAME_ASC = 4;
        public const string MEMBER_LASTLOGIN_COLUMN = "LastLogin";
        public const string MEMBER_NAME_COLUMN = "Name";

        public const string HOMEWORK_SORT_DESC = "DESC";
        public const string HOMEWORK_SORT_ASC = "ASC";

        //Sort Group
        public const int GROUP_RECENT_DESC = 1;
        public const int GROUP_RECENT_ASC = 2;
        public const int GROUP_NAME_DESC = 3;
        public const int GROUP_NAME_ASC = 4;
        public const string GROUP_RECENT_COLUMN = "CreatedDate";
        public const string GROUP_NAME_COLUMN = "Name";

        public const string GROUP_SORT_DESC = "DESC";
        public const string GROUP_SORT_ASC = "ASC";
        //Gender
        public const string PROFILE_GENDER_MALE = "Male";
        public const string PROFILE_GENDER_FEMALE = "Female";
        public const string PROFILE_GENDER_OTHER = "Other";

        //Waitting for approved
        public const int WAITTING_FOR_APPROVE = 1;
        public const int DONE_FOR_APPROVE = 0;


        public const short HOMEWORK_FINISHED_7DAYS = 1;
        public const short HOMEWORK_DUE_NEXT_7DAYS = 2;
        public const short HOMEWORK_OVERDUE = 3;
        //instrument
        public const string ERROR_DEL_INSTRUMENT = "Can't remove this Instrument/Genre! This is Global Instrument/Genre";
        //Valid Email (remove unicode)
        public const string EMAIL_VALID = @"^\w+([_.-]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public const string LAST_NAME_VALID = @"^[\w- ]+$"; //@"^[\w ]+$";
        public const string VALID_NO_HTML_TAG = @"^(?!.*<[^>]+>).*";

        //Share course
        public const string YOU_ONLY = "Me Only";
        public const string YOUR_STUDENT = "My Students";
        public const string YOUR_SCHOOL = "My School";
        public const string WORLD = "World";

        //SMS Message
        public const string SMSVERIFIEDPHONE = "This phome number verified";
        public const string SMSSENTCODEVERIFY = "Sent verification Code to your phone successfully";
        public const string SMSSENTCODEVERIFYFAIL = "Sent Code verify phone fail";
        public const string SMSVERIFYCODECONTENT = "[TeacherZone] Your verification code is: {0}.";
        public const string SMSVERIFYCODESUCCSESS = "Your phone number verified successfully";
        public const string SMSVERIFYCODEINVALID = "Invalid code";
        public const string SMSEXPIREDCODE = "Your code expired time";
        //SuNV added 2016-10-24
        public const string SMS_HEADER = "[From {0}] ";


        //Youtube
        public const int YOUTUBE_COUNT_RESULT_HOMEWORK = 10;

        //Dashboard
        public const int DASHBOARD_SIZE_PAGE_RECENT_ACTIVITY_SUPERADMIN = 11;
        public const int DASHBOARD_SIZE_PAGE_RECENT_ACTIVITY_ADMIN = 6;

        //Calendar
        public const string TEACHER_SCHEDULE_OPEN_TIME = "Open";
        public const string BOX_CONFIRM_FOR_SHEDULE_ACTION_EDIT = "edit";
        public const string BOX_CONFIRM_FOR_SHEDULE_ACTION_DELETE = "delete";
        public const string BOX_CONFIRM_FOR_SHEDULE_ACTION_EDIT_BLOCKOFTIME = "editBlockTime";
        public const string BOX_CONFIRM_FOR_SHEDULE_ACTION_DELETE_BLOCKOFTIME = "deleteBlockTime";

        //Transaction template email key
        public const string KEY_TEMPLATE_REMINDER_TRANSACTION_OVERDUE = "TEMPLATE_REMINDER_TRANSACTION_OVERDUE";
        public const string KEY_TEMPLATE_REMINDER_TRANSACTION_OVERDUE_IS = "TEMPLATE_REMINDER_TRANSACTION_OVERDUE_IS";

        //Dwolla
        public const string MSG_PROCESS_SUCCESSFUL = "Process successful";
        public const string MSG_PROCESS_UNSUCCESSFUL = "Unsuccessful Process";

        //Custom define
        public const string MSG_UNSUCCESSFUL_ID_UNDEFINE = "Save unseccessful by Id parameter is undefined";

        //Allow module constants
        public const string MODULE_ALLOW = "1";
        public const string MODULE_NO_ALLOW = "0";

        //Students Lesson Notification Reminder
        public const int LESSON_NOTIFICATION_BY_EMAIL = 0;
        public const int LESSON_NOTIFICATION_BY_SMS = 1;

        //schedule-nofity: 
        public const string SCHEDULE_NOTIFY_NEW_SCHEDULE = "You have new Schedules from {0} to {1} on {2}";
        public const string SCHEDULE_NOTIFY_EDIT_SCHEDULE = "Your schedule has been changed from {0} to {1} on {2}";
        public const string SCHEDULE_NOTIFY_DEL_SCHEDULE = "Schedule from {0} to {1} on {2} has been removed";
        public const string SCHEDULE_NOTIFY_EDIT_SCHEDULE_NEW = "Your schedule at {0} has been changed to {1}";


        //Invalid param
        public const string MSG_INVALID_TEACHERID = "Invalid Teacher";
        public const string MSG_INVALID_FREQUENCY = "Invalid Frequency";
        public const string MSG_INVALID_TIME = "Invalid Time";

		public const string WORLDPAY_SUCCESSFULL = "APPROVED";
        public const string WORLDPAY_BAD_REQUEST = "BAD_REQUEST";
        //SuNV added 2016-10-05
        public const string MSG_TRANS_FAIL = "Error: This transaction has changed or deleted";

        //sunv added 2016-10-12
        public const string MSG_ERROR_DUPLICATE = "The {0} is duplicate";
        public const string MSG_ERROR_CANNOT_ADD_IN_THE_PAST = "Don't allow add {0} in the past";

        //Constants for Chat Group
        public const string CHAT_GROUP_NAME_PREFIX_GROUP = "Group ";
        public const string CHAT_GROUP_NAME_GROUP_ALL_STUDENT = "All Students";
        public const string CHAT_GROUP_NAME_GROUP_ALL_TEACHER = "All Teachers";
        public const string MSG_INSTRUMENT_NO_DATA = "There is no data yet";
        public const string MSG_INSTRUMENT_IS_SELECTED_ALL = "You've selected all Instruments/Genres of school ";

        //Config ECS gateway: PrecisePayKey
        public const string ECS_STATUS_SUCCESSFULL = "APPROVED";
        public const string ECS_STATUS_FAIL = "ERROR";

        //Const Payment plan default is TZPay
        public const int DEFAULT_PAYMENT_TZPAY_ID = 0;
        public const string DEFAULT_PAYMENT_TZPAY_NAME = "TZPay";
        //
        public const int PLan_IS_Not_REQUIRED_TEACHER = 1;
        public const int PLan_IS_Not_REQUIRED_INSTRUMENT = 2;
        public const int PLan_IS_Not_REQUIRED_OTHER = 3;

        public const string RECURRING_METHOD_DESCRIPTION_MONTHLY =
            "This will auto bill the customer on the same day every month.";
        public const string RECURRING_METHOD_DESCRIPTION_28DAYS =
            "This will auto bill the customer every 28 days (four weeks).";
        public const string RECURRING_METHOD_DESCRIPTION_PACKAGE =
            "This plan only auto bills after the teacher has marked ATTENDED or BANKED for the prescribed number of lessons in plan using our scheduler.";

        public const string MSG_MEMBER_MISS_CARD = "Sorry, a valid credit card does not exist. Please input a valid card for this student.";

        //Precise Pay Error
        public const string MSG_ERROR_PAYMENT_PRECISE_PAY = "Sorry, we were unable to process your credit cart payment. If the problem persists, please contact Admin for help.";
        public const string MSG_INVALID = "{0} invalid";
        public const string MSG_FIELD_REQUIRED = "This field is required.";

        public const string MSG_TRANS_INVALID = "Transaction Invalid!";
        public const string MSG_TRANS_REFUND_CANNOT_VOID = "Transaction refunded cannot void!";
        public const string MSG_TRANS_REFUND_AMOUNT_INVALID = "Refund amount may not exceed the transaction balance.";
        public const string MSG_TRANS_REFUND_SUCESSFULLY = "Transaction Successfully Refunded.";
        public const string MSG_TRANS_REFUND_UNSUCESSFULLY = "Transaction Unsuccessful Refunded.";

        public const long TimerDefault = 90000; //90000 = 15'

        public const string MSG_FEEDBACK_SAVE_SUCCESFULLY = "Phản hồi của bạn đã được gửi thành công.";
    }

    public class ActivityConstants
    {
        public const string LOGIN = "LogIn";
        public const string LOGOUT = "LogOut";
        public const string DELETE = "Delete";
        public const string REJECT = "Reject";
        public const string RESET_PWD = "Reset password";
        public const string UPDATE_INFO = "Update account";
        public const string UPDATE_CONTENT = "Update landing page";
        public const string VIEW_MODE = "View mode";
        public const string INVITE_SOMEONE = "Invite someone";
        public const string VIDEO_CALL_USER = "Call video user";
        public const string ANSWER_CALL_USER = "Answer video user";
        public const string HANGUP_CALL_USER = "Hangup video user";

        public string MSG_LOGIN = "You logged in on " + String.Format("{0:D}", DateTime.Now);
        public string MSG_LOGOLUT = "You logged out on " + String.Format("{0:D}", DateTime.Now);
        public const string MSG_RESET_PWD = "Reset password for {0}.";
        public const string MSG_UPDATE_INFO = "Updated my infomation";
        public const string MSG_UPDATE_PWD = "Changed password";
        public const string MSG_UPDATE_CONTENT = "Updated landing page [{0}]";
        public const string MSG_DEL = "Deleted {0}: {1}";
        public const string MSG_VIEW_MODE = "Mode view as  {0}";
        public const string MSG_CLOSE_VIEW = "Close view mode";
        public const string MSG_INVITE_SOMEONE = "You have to send invitations to: {0}";
        public const string MSG_INCOMING_CALL = "Incoming call from member {0} to member {1}";
        public const string MSG_ANSWER_CALL = "Member {0} answer call from member {1}";
        public const string MSG_HANGUP_CALL = "Member {0} hang up a call";
        public const string MSG_UPDATE_PROFILE = "Update profile information.";
        public const string MSG_CHANGE_AVATAR = "Update profile picture.";
        public const string MSG_CHANGE_PASSWORD = "Update Password.";
        public const string MSG_CONNECT = "{0} and {1} are connected";
        //for dashboard
        public const string MSG_VIEW_DETAIL = "You watched {0}";
        public const string MSG_ASSIGN = "Teacher {0} assigned new practice {1} to you";
        public const string MSG_ASSIGN_TOPARENT = "Teacher {0} assigned new practice {1} to {2}";
        public const string MSG_ASSIGN_GROUP = "Teacher {0} assigned new practice {1} to group {2}";
        public string MSG_COMPLETE = "You completed {0} on " + String.Format("{0:D}", DateTime.Now);

        public const string ADD_NEW_ASSIGNMENT = "{0} assigned new practice {1}";
        public const string UPDATE_ASSIGNMENT = "{0} Updated practice {1}";
        public const string MSG_START_HOMEWORK = "You started practice {0}";
        public const string MSG_SEND_MESSAGE = "You sent a message {0}";
        public const string MSG_INVITE = "You invited {0}";
        public const string MSG_INVITE_EMAIL = "You sent an invitation to {0}";

        public const string MSG_COMPLETE_LESSON = "You completed lesson {0} in practice {1}";
        public const string MSG_STARTED_LESSON = "You started lesson {0} in practice {1}";

        public const string MSG_COMPLETE_HOMEWORK = "You completed practice {0}";
        //for admin
        public const string MSG_APPROVED_SONG = "{0} approved Song/Performance {1}";
        public const string MSG_APPROVED_SKILL = "{0} approved Skill/Technique {1}";
        public const string MSG_DECLINED_SONG = "{0} declined Song/Performance {1}";
        public const string MSG_DECLINED_SKILL = "{0} declined Skill/Technique {1}";

        //for teacher
        public const string MSG_SUBMITTED_SONG = "Teacher {0} submitted Song/Performance {1}";
        public const string MSG_SUBMITTED_SKILL = "Teacher {0} submitted Skill/Technique {1}";
        public const string MSG_UPDATED_SONG = "Teacher {0} updated Song/Performance {1}";
        public const string MSG_UPDATED_SKILL = "Teacher {0} updated Skill/Technique {1}";
        public const string MSG_SUBMITTED_LESSON = "Teacher {0} added lesson {1} to {2}";
        public const string MSG_UPDATED_LESSON = "Teacher {0} updated lesson {1} in {2}";

        //Member working
        public const string MSG_UPDATE_MEMBER = "{0} updated {1}";
        public const string MSG_ADD_NEW_MEMBER = "{0} added new {1}";
        public const string MSG_DELETE_MEMBER = "{0} deleted {1}";

        //Dwolla 
        public const string MSG_DWOLLA_TRANFER_SUCCESSFULL = "You have received {0} from TeacherZone";       
 
        //Plan
        public const string MSG_ADD_PLAN = "Teacher {0} Add new plan {1}";
        public const string NOTIF_APPROVAL_PLAN = "Plan {0} is waiting for approval";
        public const string NOTIF_APPROVED_PLAN = "{0} approved Plan {1}";
        public const string NOTIF_DECLINE_PLAN = "{0} decline Plan {1}";
    }
    public class NotifyConstants
    {
        public const string NOTIF_HOMEWORK = "{0} assigned new practice {1} to you";
        public const string NOTIF_REMINDER_DUE_HOMEWORK = "The practice {0} must be completed, due {1}";
        public const string NOTIF_REMINDER_OVERDUE_HOMEWORK = "Duration of your practice {0} has been exceeded from {1}";
        public const string NOTIF_MISS_CALL = "You have 1 missed call from {0}";
        public const string NOTIF_MISS_CALL_STUDENT_OF_PARENT = "{0} have 1 missed call from {1}";
        public const string NOTIF_UPDATE_ASSIGNMENT = "{0} updated practice {1}";
        public const string NOTIF_COMPLETE_HOMEWORK = "Student {0} completed practice {1}";
        public const string NOTIF_COMPLETE_HOMEWORK_STUDENT_OF_PARENT = "{0} completed practice {1}";
        public const string NOTIF_UPDATE_HOMEWORK = "The practice {0} was updated";
        public const string NOTIF_UPDATE_HOMEWORK_TOPARENT = "The practice {0} of {1} was updated";
        //for parent
        public const string NOTIF_ASSIGNMENT = "{0} assigned practice {1} to {2}";

        //for admin 
        public const string NOTIF_APPROVED_SONG = "Song/Performance {0} is waiting for approval";
        public const string NOTIF_APPROVED_SKILL = "Skill/Technique {0} is waiting for approval";

        //for teacher
        public const string NOTIF_DELETE_LESSON = "{0} deleted {1} of {2}";

        // for SMS
        public const string NOTIF_SMS_REMINDER_DUE_HOMEWORK = "[TeacherZone] You have a practice that needs to finish by {0}";
        public const string NOTIF_SMS_REMINDER_OVERDUE_HOMEWORK = "[TeacherZone] You have a practice has been exceeded from {0}";
        public const string SMSASSIGNHOMEWORK = "[TeacherZone] {0} {1} {2} assigned new practice to you";
        public const string SMSASSIGNHOMEWORKUPDATE = "[TeacherZone] {0} {1} {2} updated your practice";
        public const string SMSCHATSUBJECT = "[TeacherZone] You have a chat message from {0} {1} {2}";
        public const string SMSHOMEWORKREMINDER = "[TeacherZone] Hey {0}, it’s time to practice";
        public const string EMAILHOMEWORKREMINDER = "Hey {0}, it’s time to practice";
        public const string SMSASSIGNHOMEWORKCHILD = "[TeacherZone] {0} {1} {2} assigned new practice to {3}";
        public const string SMSASSIGNHOMEWORKUPDATECHILD = " [TeacherZone] {0} {1} {2} updated your children {3} practice";

        // Lesson Notification
        public const string EMAIL_LESSON_NOTIFICATION = "Hey {0}, You have an appointment with {1}, {2} at {3}";
        //public const string EMAIL_LESSON_NOTIFICATION_FOR_PARENT = "Hey {0}, Your child {1} has an appointment with {2}, {3} at {4}";
        public const string EMAIL_LESSON_NOTIFICATION_FOR_PARENT = "Reminder that {1} has an appointment with {2}, {3} at {4}";
        public const string EMAIL_LESSON_CANCEL_NOTIFICATION = "TeacherZone - Cancelled lesson";
        public const string SMS_LESSON_NOTIFICATION = "[TeacherZone] {0} you have an appointment with {1}, {2} at {3}";
        public const string SMS_LESSON_NOTIFICATION_FOR_PARENT = "[TeacherZone] {0}, Your child {1} has an appointment with {2}, {3} at {4}";
        public const string EMAIL_CHAT_OFFLINE ="Please do not reply to this email because we are not monitoring this inbox. To reply to this message, {0}.";

        public const string ERROR_EDIT_BILLING_TODATE = "To date field should be greater or equal to the start date and current date";
    }

    public class AppKeys
    {
        public const string UPLOAD_FOLDER_PRODUCT = "folder-upload-image-product";

        public const string MAIL_HOST = "mail-host";
        public const string MAIL_PORT = "mail-port";
        public const string MAIL_SENDER = "mail-sender";
        public const string MAIL_USERNAME = "mail-username";
        public const string MAIL_PASSWORD = "mail-password";
        public const string MAIL_DISPLAY = "mail-display";
        public const string MAIL_ADMIN_SUPPORT = "mail-contact";//receive mail from form contact
        public const string MAIL_FOR_DEMO = "mail-for-demo";
        public const string RESET_PWD_TIME_LIMIT = "expiredHour";
        public const string CATID_SONG = "cat-type-song";
        public const string CATID_SKILL = "cat-type-skill";
        public const string FOLDER_UPLOAD_S3_SITE = "S3FolderSite";
        public const string FOLDER_UPLOAD_COURSE_SONG = "FolderCourseSong";
        public const string FOLDER_UPLOAD_COURSE_THUMBNAIL = "FolderCourseThumbnail";
        public const string FOLDER_UPLOAD_LESSON_THUMBNAIL = "FolderLessonThumbnail";
        public const string FOLDER_UPLOAD_AVATAR = "FolderAvatar";
        public const string FOLDER_UPLOAD_PLAN_THUMBNAIL = "FolderPlanThumbnail";
        public const string FOLDER_UPLOAD_STORE_THUMBNAIL = "FolderStoreThumbnail";
        public const string UPLOAD_VIDEO_MAX_SIZE = "upload-video-max-size";
        public const string UPLOAD_AUDIO_MAX_SIZE = "upload-audio-max-size";
        public const string UPLOAD_DOCUMENT_MAX_SIZE = "upload-document-max-size";
        public const string UPLOAD_IMAGE_MAX_SIZE = "upload-image-max-size";
        public const string MESS_TYPE_INBOX = "message-type-inbox";
        public const string MESS_TYPE_SENT = "message-type-sent";
        public const string UPLOAD_MEMBER = "FolderMember";

        //Editor
        public const string EDITOR_MAX_LENGTH = "editor-max-length";

        //AWS config
        public const string PATH_ROOT_S3 = "PathRootS3";
        public const string AWS_FOLDER_FILE = "AWSFolderFile";
        public const string AWS_FOLDER_COURSE = "AWSFolderCourse";
        public const string AWS_FOLDER_Library = "AWSFolderLibrary";
        public const string SIZE_MAX_SONG = "SizeMaxSong";
        public const string SIZE_MAX_FILE = "SizeMaxFile";
        public const string SIZE_MAX_IMAGE = "SizeMaxImage";
        public const string UPLOAD_SONG_SUPPORT = "Upload_Song_Support";
        public const string UPLOAD_LESSON_SUPPORT = "Upload_Lesson_Support";
        public const string UPLOAD_FILE_SUPPORT = "Upload_File_Support";
        public const string UPLOAD_IMAGE_SUPPORT = "Upload_Image_Support";
        public const string S3_AMAZONE_URL = "S3AmazonUrl";
        public const string WOWZA_SECRET_KEY = "WowzaSecretKey";
        public const string JWPLAYER_KEY = "JWPlayerKey";
        public const string PathWOWZA = "PathWOWZA";
        public const string PathWOWZAMP3 = "PathWOWZAMP3";
        public const string PathWOWZA_DASH = "PathWOWZA_DASH";
        public const string PathWOWZA_RTMP_MP3 = "PathWOWZA_RTMP_MP3";
        public const string PathWOWZA_M3U8 = "PathWOWZA_M3U8";

        //Activity
        public const string ACTIVITY_TOTAL_DETAIL = "total-activity-detail";
        public const string ACTIVITY_TOTAL_ASSIGN = "total-activity-assign";
        public const string ACTIVITY_TOTAL_COMPLETE = "total-activity-complete";
        public const string ACTIVITY_TOTAL_NOTIFY = "total-activity-notify";
        public const string ACTIVITY_TOTAL_DASHBOARD = "total-activity-dashboard";
        public const string ACTIVITY_PAGE_SIZE = "activity-page-size";

        //library
        public const string LIBRARY_PAGE_SIZE = "lib-page-size";
        public const string LIBRARY_NUMBER_ITEM = "lib-number-item";
        public const string LIBRARY_MAX_RECENT = "lib-max-recent";
        public const string LIBRARY_MAX_RECOMMENDED = "lib-max-recommended";

        //Config sent email
        public const string SEND_EMAIL_ENABLE = "sendmail-enable";
        public const string SEND_EMAIL_DEFAULT_PASSWORD = "sendmail-default-password";

        //Config Url Default Video for Course and Homework Detail
        public const string COURSE_VIDEO_DEFAULT = "CourseVideoDefault";
        public const string CONVERT_VIDEO_FAILD = "ConvertVideoFaild";

        //Youtube Data APIs: API Key
        public const string YOUTUBE_API_KEY = "YoutubeApiKey";

        //Config SMS 
        public const string SMSEXPIREDCODE = "SMSExpiredCode";
        public const string SMS_ACCOUNT_SID = "SMSAccountSid";
        public const string SMS_AUTHTOKEN = "SMSAuthToken";
        public const string SMS_FROM_NUMBER = "SMSFromNumber";
        public const string SMS_MAX_NUMBER_SEND = "SMSMaxNumberSend";
        public const string SMS_DEFAULT_ISO_COUNTRY = "SMSDefaultIsoCountry";
        public const string SMS_DEFAULT_PHONE_CODE = "SMSDefaultPhoneCode";
        public const string SMS_MOD_VERIFY = "ModSMSVerify";




        //Enable Redirect SSL when login
        public const string ENABLE_REDIRECT_SSL = "EnableRedirectSSL";

        //Report config
        public const string REPORT_ROLE_TO_LOG = "RoleToLog";

        //Google Tag Manager code
        public const string GOOGLE_TAG_MANAGER_CODE = "GTMCode";

        public const string MODSMSVERIFY = "ModSMSVerify";

        //Build version config
        public const string PROJECT_BUILD_VERSION = "ProjectBuildVersion";

        //Yelp API
        public const string Yelp_AccessToken = "Yelp-AccessToken";
        public const string Yelp_AccessTokenSecret = "Yelp-AccessTokenSecret";
        public const string Yelp_ConsumerKey = "Yelp-ConsumerKey";
        public const string Yelp_ConsumerSecret = "Yelp-ConsumerSecret";
        public const string Yelp_Search_TopN = "Yelp-Search-TopN";

        //WP
        public const string WP_VERIFY_CARD_ZIPCODE = "wp-verify-card-zipcode";

        //Calendar
        public const string EVENT_BG_COLOR_ATTEND = "event-bg-color-attend";
        public const string EVENT_BG_COLOR_TEACHER_CANCEL = "event-bg-color-teacher-cancel";
        public const string EVENT_BG_COLOR_STUDENT_CANCEL = "event-bg-color-student-cancel";
        public const string EVENT_BG_COLOR_NOT_SHOW = "event-bg-color-not-show";
        public const string EVENT_BG_COLOR_STUDENT_NOT_ACTIVE = "event-bg-color-student-not-active"; ////ThangND 2016-09-07 add background color for Student Inactive Schedule
        public const string EVENT_BG_COLOR_STUDENT_UNPAID = "event-bg-color-student-unpaid"; ////ThangND 2016-09-07 add background color for Student Inactive Schedule
        public const string EVENT_BG_COLOR_STUDENT_PROCESSING = "event-bg-color-student-processing";
        public const string EVENT_BG_COLOR_BANK = "event-bg-color-bank";
        public const string EVENT_BG_COLOR_HIGHLIGHT_SEARCH_STUDENT = "event-bg-color-highlight-search-student";

        //Help link
        public const string HELP_LINK_DB_STUDENT = "help-link-db-student";
        public const string HELP_LINK_DB_TEACHER = "help-link-db-teacher";
        public const string HELP_LINK_DB_SCHOOL = "help-link-db-school";
        public const string HELP_LINK_DB_OTHER = "help-link-db-other";

        //Environment
        public const string CURRENT_ENVIRONMENT = "CurrentEnvironment";
        public const string SEND_TO_ADDRESS_CONFIG_ENABLED = "SendToAddressConfigEnabled"; //ThangND [2016-10-10] [Add config environment for SMS to phone number]
        public const string ENVIRONMENT_EMAIL_TO = "EnvironmentEmailTo";
        public const string ENVIRONMENT_SMS_TO = "EnvironmentSMSTo"; //ThangND [2016-10-10] [Add config environment for SMS to phone number]

        /// <summary>
        /// Dwolla application config
        /// </summary>
        public const string DWOLLA_CONFIG_CLIENT_ID = "dwolla_client_id";
        public const string DWOLLA_CONFIG_CLIENT_SECRET = "dwolla_client_secret";
        public const string DWOLLA_CONFIG_PIN = "dwolla_pin";
        public const string DWOLLA_CONFIG_ACCESS_TOKEN = "dwolla_access_token";
        public const string DWOLLA_CONFIG_OAUTH_SCOPE = "dwolla_oauth_scope";
        public const string DWOLLA_CONFIG_PRODUCTION_HOST = "dwolla_production_host";
        public const string DWOLLA_CONFIG_SANDBOX_HOST = "dwolla_sandbox_host";
        public const string DWOLLA_CONFIG_DEFAULT_POSTFIX = "dwolla_default_postfix";
        public const string DWOLLA_CONFIG_SANDBOX = "dwolla_sandbox";
        public const string DWOLLA_CONFIG_DEBUG = "dwolla_debug";
        public const string DWOLLA_CONFIG_ACCOUNT_ID = "dwolla_account_id";

        //Service project
        public const string SERVICE_DOMAIN_PROJECT = "SignalRDomain";
        public const string TIMEOUT_LOGIN_AS = "timeout-login-as";
        public const string TIMEOUT_COOKIE = "timeout-cookie";
        public const string TIMEOUT_REMEMBER_USER = "timeout-remember-user-by-day";
        public const string COOKIE_REMEMBER = "TZ.CookieRemember";

        //Number Of NoEndDate Recurring Generate
        public const string NUMBER_NO_END_DATE_FIRST_GENERATE = "number-no-end-date-first-generate";
        public const string NUMBER_NO_END_DATE_GENERATE = "number-no-end-date-generate";
        public const string NUMBER_DAY_RECURRING_SCHEDULE = "number-day-recurring-schedule";
        public const string NUMBER_WEEK_NO_END_DATE_NEED_GENERATE_BEFORE = "number-week-no-end-date-need-gen-before";

        //schedule
        public const string STUDENT_SCHEDULE_REPEAT_N = "student-schedule-repeat-n";
        public const string STUDENT_SCHEDULE_NUMBER_MONTH_CHECK_GENERATE_DATA = "student-schedule-number-month-check-generate-data";
        public const string STUDENT_SCHEDULE_TIME_RUN_BG = "student-schedule-time-generate-month";
        public const string STUDENT_SCHEDULE_DAY_OVER = "student-schedule-day-over";

        //allow module
        public const string ALLOW_MODULE_DWOLLA = "allow-module-dwolla";

        public const string CALENDAR_NUMBER_DAY_SHOW_IN_A_MONTH = "calendar-number-day-show-in-a-month";
        public const string INSTRUMENT_LEVEL_DEFAULT = "instrument-level-default";

        //Config ECS gateway: PrecisePayKey
        public const string ECS_API_KEY = "PrecisePayKey";
        public const string ECS_API_URL = "PrecisePayGatetwayUrl";
        public const string ECS_API_DIRECT_POST = "PrecisePayGatetwayDirectPostUrl";
        public const string ECS_API_USERNAME = "PrecisePay-username";
        public const string ECS_API_PASSWORD = "PrecisePay-password";

        public const string PRACTICE_TIMER_WARNING = "PracticeTimer-Warning";
    }

    public class Files
    {
        public const string TEMPLATE_FORGET_PASSWORD = "~/Templates/ForgotPassword.txt";
        public const string TEMPLATE_FORGET_PASSWORD_TO_PARENT = "~/Templates/ForgotPasswordToParent.txt";
        public const string TEMPLATE_INVITE = "~/Templates/InviseSomeone.txt";
        public const string TEMPLATE_ACCEPT = "~/Templates/AcceptInvite.txt";
        public const string TEMPLATE_ACTIVE_ACCOUNT = "~/Templates/RegisterMember.txt";
        public const string TEMPLATE_ACTIVE_ACCOUNT_UNDER18 = "~/Templates/RegisterMember18.txt";
        public const string TEMPLATE_ACTIVE_ACCOUNT_WITH_PASS = "~/Templates/RegisterMemberWithPass.txt";
        public const string TEMPLATE_ADD_NEW_MEMBER_WITH_PASS = "~/Templates/AddNewMemberWithPass.txt";
        public const string TEMPLATE_CONTACT = "~/Templates/Contact.txt";
        public const string TEMPLATE_ASSIGNMENT_COURSE = "~/Templates/AssignmentCourse.txt";
        public const string TEMPLATE_VERIFY_CHILD = "~/Templates/VerifyChild.txt";
        public const string TEMPLATE_VERIFY_CHILD_NOPASS = "~/Templates/VerifyChildNoPass.txt";
        public const string TEMPLATE_NOTIFY_PARENT_CHILD_CONNECT = "~/Templates/NotifierParentStudentCennect.txt";

        public const string TEMPLATE_HOMEWORK_DETAIL_FILE_LIST = "~/Templates/RenderHomeworkDetail/FileHomeworkDetailList.txt";
        public const string TEMPLATE_HOMEWORK_DETAIL_FILE_LIST_ITEM = "~/Templates/RenderHomeworkDetail/FileHomeworkDetailItem.txt";
        public const string TEMPLATE_HW_LESSON_LIST = "~/Templates/Homework/HomeworkDetail/LessonList.txt";
        public const string TEMPLATE_HW_COURSE_ITEM = "~/Templates/Homework/HomeworkDetail/CourseItem.txt";
        public const string TEMPLATE_HW_LESSON_ITEM = "~/Templates/Homework/HomeworkDetail/LessonItem.txt";
        public const string TEMPLATE_COURSE_LESSON_ITEM = "~/Templates/CourseDetail/LessonItem.txt";
        public const string TEMPLATE_COURSE_LESSON_LIST = "~/Templates/CourseDetail/LessonList.txt";
        public const string TEMPLATE_APPROVE_LESSON_ITEM = "~/Templates/ApproveDetail/LessonItem.txt";
        public const string TEMPLATE_APPROVE_LESSON_LIST = "~/Templates/ApproveDetail/LessonList.txt";


        public const string TEMPLATE_STUDENT_ADD_PLAN = "~/Templates/StudentAddPlan.txt";
        public const string TEMPLATE_STUDENT_PAYMENT_REMINDER = "~/Templates/PaymentReminder.txt";

        //Schedule
        public const string TEMPLATE_SCHEDULE_MAIL_CONFIRM = "/Templates/Schedule/ConfirmSchedule.txt";
        public const string TEMPLATE_SCHEDULE_MAIL_CONFIRM_ONCE = "/Templates/Schedule/ConfirmOnceSchedule.txt";
        public const string TEMPLATE_SCHEDULE_MAIL_DELETE = "/Templates/Schedule/NotifierDeleteSchedule.txt";
        public const string TEMPLATE_SCHEDULE_MAIL_DELETE_ONCE = "/Templates/Schedule/NotifierDeleteOnceSchedule.txt";
        public const string TEMPLATE_SCHEDULE_MAIL_EDIT = "/Templates/Schedule/NotifierEditSchedule.txt";
        public const string TEMPLATE_SCHEDULE_MAIL_EDIT_ONCE = "/Templates/Schedule/NotifierEditOnceSchedule.txt";
        public const string TEMPLATE_ACTIVE_ACCOUNT_STUDENT = "/Templates/RegisterMemberStudent.txt";

        public const string TEMPLATE_PLAN_CONFIRM_RECURRING = "~/Templates/AutoRecurring.txt";
        public const string TEMPLATE_ADD_PLAN_RECURRING = "~/Templates/StudentRecurringAddPlan.txt";
        public const string TEMPLATE_BANK_LESSON = "~/Templates/Schedule/NotifierBankLesson.txt";
        public const string TEMPLATE_RESTORE_BANK_LESSON = "~/Templates/Schedule/NotifierRestoreBankLesson.txt";
        public const string TEMPLATE_RESEND_ACOUNT_INFO = "/Templates/ReSendAcountInfomation.txt";
        public const string TEMPLATE_LESSON_CANCEL_NOTIFICATION = "/Templates/LessonCancelNotification.txt";
        public const string TEMPLATE_LESSON_CANCEL_NOTIFICATION_PARENT = "/Templates/LessonCancelNotificationParent.txt";
        public const string TEMPLATE_REMINDER_SCHEDULE_WEEKLY = "~/Templates/AutoScheduleReminderWeekly.txt";
        public const string TEMPLATE_REMINDER_SCHEDULE_DAILY = "~/Templates/AutoScheduleReminderDaily.txt";

        public const string TEMPLATE_COURSE_UPLOAD_SUCCESSFULLY = "~/Templates/CourseUploadSucessfully.txt";
    }

    public static class History
    {
        public static readonly string[] LessonIgnoreStrings =
            {
                "LessonID", "CourseID", "StatusID", "SortOrder", "CreatedDate", "CreatedUserID",
                "UpdatedDate", "UpdatedUserID,", "TokenResponseCovert", "StartDateElapsed","VersionID","UpdatedUserID","CourseFileID","CheckboxTipsTricks","IsOveride","FileEncode","FileStatusID","ThumbnailUrl","ListFile","ListLink","FileTokenID"
            };
    }

    public class MemberStatus
    {
        public const string MEMBER_STATUS_INACTIVE_LABEL = "InActive";
        public const string MEMBER_STATUS_INACTIVE_VALUE = "0";
        public const string MEMBER_STATUS_ACTIVE_LABEL = "Active";
        public const string MEMBER_STATUS_ACTIVE_VALUE = "1";
        public const string MEMBER_STATUS_SUSPENDED_LABEL = "Suspended";
        public const string MEMBER_STATUS_SUSPENDED_VALUE = "2";
    }

    public class AllVideoLogConstants
    {
        public const string MSG_VIDEO_PLAY = "Video begin play";
        public const string MSG_VIDEO_SELECTED = "Video selected";
        public const string MSG_HOMEWORK_VIDEO_PLAY = "[HW] Video begin play in practice";
        public const string MSG_HOMEWORK_VIDEO_SELECTED = "[HW] Video selected in practice";
        public const string MSG_VIDEO_SLOW_DOWN = "Video slow down";
        public const string MSG_VIDIO_LOOP = "Video Loop";
        public const string MSG_HOMEWORK_VIDEO_SLOW_DOWN = "[HW] Video slow down in practice";
        public const string MSG_DONE_HOMEWORK = "[HW] Practice done";
        public const string MSG_DONE_LESSON = "[HW] Lesson done";
        public const string MSG_VIEW_HOMEWORK = "[HW] Practice view";
        public const string MSG_START_LESSON = "[HW] Lesson start";
    }

    public class Report
    {
        public const string TOTAL_HW_ASSIGNED = "Total practice assigned";
        public const string TOTAL_HW_DUE = "Total practice due";
        public const string TOTAL_HW_DONE = "Total practice done";
        public const string TOTAL_VIDEO_PLAY = "Total video plays";
        public const string TOTAL_USED_SLOW = "Total videos that used slow motion";
        public const string TOTAL_USED_LOOPING = "Total video plays that used looping";
        public const string TOTAL_STUDENT_VIEWED = "Total student views";
        public const string TOTAL_VIDEO_SELECTED = "Total video selects";
        public const string HW_COMPLETED = "Completed";
        public const string HW_INCOMPLETED = "Incomplete";
        public const string HW_NOT_START = "Didn\'t Start";
        public const string VIDEO_COURSE_YOUTUBE_NAME = "Course from youtube";
        public const string HW_DATA_EMPTY = "There is no practice";
    }

    public class PlanType
    {
        public const int SingleLesson = 1;
        public const int Weekly = 2;
        public const int BeWeekly = 3;
        public const int Monthly = 4;
        public const int Quarterly = 5;
        public const int BiAnnually = 6;
        public const int Annually = 7;
    }

    public class PlanTypeName
    {
        public const string SingleLesson = "Single Lesson";
        public const string Weekly = "Weekly";
        public const string BeWeekly = "Bi-Weekly";
        public const string Monthly = "Monthly";
        public const string Quarterly = "Quarterly";
        public const string BiAnnually = "Bi-Annually";
        public const string Annually = "Annually";
    }

    public class DwollaApiErrorMessageConstant
    {
        public const string Success = "Success";

        public const string AccountNotFound = "ID not found.";
        public const string AccountNotAuthorized = "Not authorized";

        public const string CustomerDuplicateOrValidate = "Duplicate or validation error.";
        public const string CustomerNotAuthorized = "Not authorized";

        public const string TransferFail = "Transfer failed.";
    }

    public class RecuringMethod
    {
        public const int SingleLesson = 0;
        public const int Monthly = 1;
        public const int Package = 2;        
        public const int Days = 3;
    }

    public class RecuringStoreMethod
    {
        public const int Weekly = 0;
        public const int BiWeekly = 1;
        public const int Monthly = 2;
        public const int Quarterly = 3;
    }

    public class SendNotifyType
    {
        public const int Email = 0;
        public const int SMS = 1;
        public const string AccessDenied = "Access denied";
        
    }

    public class PlanStatus
    {
        public const int WattingApproval = 0;
        public const int Aprroved = 1;
        public const int Decline = 2;
        public const int Delete = 3;
    }

    public class MimeMapFileType
    {
        public const int Other = 0;
        public const int Image = 1;
        public const int Sound = 2;
        public const int Video = 3;
    }

    public class Currency
    {
        public const string USD = "USD";
    }
}
