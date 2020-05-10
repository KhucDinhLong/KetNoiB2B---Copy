using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETA.Common.Constants;
using Twilio;

namespace SETA.Common.Helper
{
    public class SMSNotifyHelper
    {
        public static string SMSSendNotify(string AccountSid, string AuthToken, string From, string To, string Content)
        {
            string error = "";
            //ThangND [2016-10-10] [Add config environment for SMS to phone number]
            var environment = Utility.Utils.GetSetting(AppKeys.CURRENT_ENVIRONMENT, EnvironmentData.Live);
            AccountSid = ConfigurationManager.AppSettings["SMSAccountSid"];
            AuthToken = ConfigurationManager.AppSettings["SMSAuthToken"];
            From = ConfigurationManager.AppSettings["SMSFrom"];
            var twilio = new TwilioRestClient(AccountSid, AuthToken);

            
                if (string.CompareOrdinal(environment, EnvironmentData.Live) == 0)
                {
                    var message = twilio.SendMessage(From, To, Content);

                    if (message.RestException != null)
                    {
                        error = message.RestException.Message;
                        // handle the error ...
                    }
                }
                else
                {
                    var enabledSend = Utility.Utils.GetSetting(AppKeys.SEND_TO_ADDRESS_CONFIG_ENABLED, "0");
                    var listPhone = Utility.Utils.GetSetting(AppKeys.ENVIRONMENT_SMS_TO, string.Empty);

                    if (string.CompareOrdinal(enabledSend, "1") == 0)
                    {
                        if (!string.IsNullOrEmpty(listPhone))
                        {
                            var arrPhone = listPhone.Split('|');

                            foreach (var s in arrPhone)
                            {
                                var message = twilio.SendMessage(From, s, Content);

                                if (message.RestException != null)
                                {
                                    error = message.RestException.Message;
                                    // handle the error ...
                                }
                            }
                        }
                    }
                    else
                    {
                        var message = twilio.SendMessage(From, To, Content);

                        if (message.RestException != null)
                        {
                            error = message.RestException.Message;
                            // handle the error ...
                        }
                    }
                }
                //End  

            return error;
        }
    }
}

