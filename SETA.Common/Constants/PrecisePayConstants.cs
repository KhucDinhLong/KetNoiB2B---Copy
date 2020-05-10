using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    public class Result
    {
        public static short Success = 1;
        public static short Declined = 2;
        public static short Error = 3;
    }

    public class ResultCode
    {
        public static string Code100 = "Transaction was approved.";
        public static string Code200 = "Transaction was declined by processor.";
        public static string Code201 = "Do not honor.";
        public static string Code202 = "Insufficient funds.";
        public static string Code203 = "Over limit.";
        public static string Code204 = "Transaction not allowed.";
        public static string Code220 = "Incorrect payment information.";
        public static string Code221 = "No such card issuer.";
        public static string Code222 = "No card number on file with issuer.";
        public static string Code223 = "Expired card.";
        public static string Code224 = "Invalid expiration date.";
        public static string Code225 = "Invalid card security code.";
        public static string Code240 = "Call issuer for further information.";
        public static string Code250 = "Pick up card.";
        public static string Code251 = "Lost card.";
        public static string Code252 = "Stolen card.";
        public static string Code253 = "Fraudulent card.";
        public static string Code260 = "Declined with further instructions available. (See response text)";
        public static string Code261 = "Declined-Stop all recurring payments.";
        public static string Code262 = "Declined-Stop this recurring program.";
        public static string Code263 = "Declined-Update cardholder data available.";
        public static string Code264 = "Declined-Retry in a few days.";
        public static string Code300 = "Transaction was rejected by gateway.";
        public static string Code400 = "Transaction error returned by processor.";
        public static string Code410 = "Invalid merchant configuration.";
        public static string Code411 = "Merchant account is inactive.";
        public static string Code420 = "Communication error.";
        public static string Code421 = "Communication error with issuer.";
        public static string Code430 = "Duplicate transaction at processor.";
        public static string Code440 = "Processor format error.";
        public static string Code441 = "Invalid transaction information.";
        public static string Code460 = "Processor feature not available.";
        public static string Code461 = "Unsupported card type.";
        public static string Code900 = "Step 2 fail.";
        public static string Code901 = "Exception from PreciseSDK.";
    }

    public class ApiCallType
    {
        public static short ThreeStep = 1;
        public static short DirectPost = 2;
    }
}
