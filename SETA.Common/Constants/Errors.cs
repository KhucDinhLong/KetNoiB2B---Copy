namespace SETA.Common.Constants
{
    public class Errors
    {
        public const string Validate_Required = "The {0} field is required.";
        public const string Validate_Duplicate = "{0} is duplicated.";
        public const string Validate_NotEnoughRequiredField = "You are missing some require fields. Please include them";
        public const string Validate_NotExistUser= "{0} is not exist.";
        public const string Validate_InvalidRoleToSendMessage = "You cannot send to user {0}.";
        //Fixed: TZ-1347
        public const string Validate_NotInvalidByParentUserName = "Parent username has been existed. You should enter appropriate parent email"; //"Invalid {0} By Parent UserName {1}";

        public const string Validate_ToTimeNoValid = "The {0} must higher than {1}.";
        public const string Validate_Calendar_AtLeastChooseOneDay = "Must select at least one day";
        public const string Validate_Calendar_AtLeastChooseOneAttendanceType = "You must select at least one attendance type. Please re-select";

        public const string Validate_Date_Lower_Than_Cur = "{0} must be greater than or equal to Schedule start date.";
        public const string Validate_Length_Of_Time_Invalid = "{0} invalid.";
        public const string Validate_Compare_LengthOfTime_OpenTime = "{0} must be less than or equal to Open time";
        public const string Validate_Date_Cannot_in_Future = "{0} must be a past Date";
        public const string Validate_Date_Cannot_in_Past = "{0} must be a future Date";
        public const string Validate_Date_Must_Be_Greater_Than = "{0} must be greater than {1}";
        public const string Validate_Conflick_With_Other_Schedule = "{0} conflict with other Lesson Schedule";

        public const string Validate_Payroll_Teacher_Need_Payroll_Wage_Rate = "The payroll information of selected teacher/s can not be found: {0}. Please configure their payroll information before generating Payroll.";

        public const string Validate_Group_Schedule_Exist_in_ManyTeacher =
            "Class/Group {0} has an exist Lesson Schedule with other Teacher at this Time. Please set another Time.";

        public const string Validate_Refund_Amount_Invalid = "Refund amount may not exceed the transaction balance.";
        public const string Validate_Refund_Amount_Invalid_Over_Zero = "Invalid {0}.";

        public const string Validate_Cannot_Input_Character = "{0} not allow character {1}";
    }

    public class DwollaErrorCode
    {
        public const int DwollaCreateCustomerFail = 1001;
        public const int DwollaCreateFundingSourceFail = 1002;
        public const int DwollaSaveFSUnsucessful = 1003;
        public const int DwollaTeacherMissCusAndDeposit = 1004;
        public const int DwollaTeacherNotHasFS = 1005;
        public const int DwollaCreateTransferFail = 1006;
        public const int DwollaSaveTransferToTZFail = 1007;
        public const int DwollaCallApiNotAuthorized = 1008;
        public const int DwollaApiAccountNotFound = 1009;
        /// <summary>
        /// Duplicate customer or validation error.
        /// </summary>
        public const int DwollaApiDuplicateOrValidateErrorCustomer = 1010;
        /// <summary>
        /// Customer not found.
        /// </summary>
        public const int DwollaApiCustomerNotFound = 1011;
        /// <summary>
        /// Can be: Duplicate funding source or validation error. Authorization already associated to a funding source.
        /// </summary>
        public const int DwollaApiFsDupOrNotValid = 1012;
        /// <summary>
        /// Transfer failed.
        /// </summary>
        public const int DwollaApiTransferFail = 1013;
        /// <summary>
        /// Transfer not found.
        /// </summary>
        public const int DwollaApiTransferNotFound = 1014;        
    }
}
