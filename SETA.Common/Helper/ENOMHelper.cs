using System;
using System.Net;
using System.Xml;

namespace SETA.Common.Helper
{

    public class ENOMHelper
    {
        private const bool IS_MODE_TEST = true; //!todo: get from config
        private const string PW = "resellpw";
        private const string UID = "resellid";
        private const string COMMAND = "check";
        private const string SERVER_TEST = "resellertest.enom.com";
        private const string SERVER = "reseller.enom.com";
        private const string RESPONSE_TYPE = "XML";
        private const string ERROR_PARAM_INPUT = "Param UnAvailable.";

        private const string PARAM_API_CHECK_DOMAIN = "command={0}&sld={1}&tld={2}&responsetype=xml&uid={3}&pw={4}";
        private const string URL_API = "";
        //url register new domain:
        //https://resellertest.enom.com/interface.asp?
        //command=Purchase&uid=resellid&pw=resellpw
        //&sld=resellerdocs2&tld=net
        //&RegistrantOrganizationName=Reseller%20Documents%20Inc.
        //&RegistrantFirstName=john&RegistrantLastName=doe
        //&RegistrantAddress1=111%20Main%20St.
        //&RegistrantCity=Hometown&RegistrantStateProvince=WA
        //&RegistrantStateProvinceChoice=S&RegistrantPostalCode=98003
        //&RegistrantCountry=United+States
        //&RegistrantEmailAddress=john%2Edoe%40resellerdocs%2Ecom
        //&RegistrantPhone=%2B1.5555555555
        //&RegistrantFax=%2B1.5555555556&ResponseType=XML

        /// <summary>
        /// Check domain is available/unavailable?
        /// </summary>
        /// <param name="domain">domain</param>
        /// <param name="domainExternal">domainExternal</param>
        /// <returns>true/false</returns>
        //URL Guide: http://www.enom.com/resellers/resources-apidemo.aspx (Guide)
        //URL for API request: http://resellertest.enom.com/interface.asp?PW=resellpw&UID=resellerid&COMMAND=check&SLD=adomainname&TLD=COM&responsetype=XML
        //!<Key - Value>
        //PW	resellpw
        //UID	resellid
        //COMMAND	check
        //SLD	adomainname
        //TLD	com
        public static bool CheckDomain(string domain, out string mess)
        {
            try
            {
                mess = string.Empty;
                var retValue = false;
                string apiRegister = GetUrlApi();
                string[] arrDomain = domain.Split('.'); //domain format: dantri.com
                string urlApi = string.Format(GetUrlApi() + PARAM_API_CHECK_DOMAIN, COMMAND, arrDomain[0], arrDomain[1], UID, PW);

                // Load the API results into an XmlDocument object
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(urlApi);

                // Read the results
                var rrpCode = xmlDoc.SelectSingleNode("/interface-response/RRPCode");//.InnerText;
                var rrpText = xmlDoc.SelectSingleNode("/interface-response/RRPText");//.InnerText;

                if (rrpCode != null)
                {
                    // Perform actions based on results
                    switch (rrpCode.InnerText)
                    {
                        case "210":
                            //Domain available
                            mess = "Domain available";
                            retValue = true;
                            break;
                        case "211":
                            //Domain not available
                            mess = string.Format("{0} - {1}", rrpCode.InnerText, rrpText.InnerText);
                            break;
                        default:
                            mess = string.Format("Error: {0} - {1}", rrpCode.InnerText, rrpText.InnerText);
                            break;
                    }
                }
                else
                {
                    mess = ERROR_PARAM_INPUT;
                }
                return retValue;
            }
            catch (Exception ex)
            {
                mess = "Fail: " + ex.Message;
                return false;
                //throw new ApplicationException("operation failed!", ex);
            }
        }

        /// <summary>
        /// Register new domain width eNOM.com
        /// </summary>
        /// <param name="domain">dantri.com</param>
        /// <param name="mess"></param>
        /// <returns></returns>
        public static bool Register(DomainRegister obj, out string mess)
        {
            try
            {
                mess = string.Empty;
                bool retValue = false;
                string apiRegister = GetUrlApi();
                string[] arrDomain = obj.Domain.Split('.'); //domain format: dantri.com

                apiRegister += "command=Purchase";
                apiRegister += AddParamToQuery("uid", UID);
                apiRegister += AddParamToQuery("pw", PW);
                apiRegister += AddParamToQuery("sld", arrDomain[0]);
                apiRegister += AddParamToQuery("tld", arrDomain[1]);
                apiRegister += AddParamToQuery("RegistrantOrganizationName", obj.RegistrantOrganizationName);
                apiRegister += AddParamToQuery("RegistrantFirstName", obj.RegistrantFirstName);
                apiRegister += AddParamToQuery("RegistrantLastName", obj.RegistrantLastName);
                apiRegister += AddParamToQuery("RegistrantAddress1", obj.RegistrantAddress1);
                apiRegister += AddParamToQuery("RegistrantCity", obj.RegistrantCity);
                apiRegister += AddParamToQuery("RegistrantStateProvince", obj.RegistrantStateProvince);
                apiRegister += AddParamToQuery("RegistrantStateProvinceChoice", obj.RegistrantStateProvinceChoice);
                apiRegister += AddParamToQuery("RegistrantPostalCode", obj.RegistrantPostalCode);
                apiRegister += AddParamToQuery("RegistrantCountry", obj.RegistrantCountry);
                apiRegister += AddParamToQuery("RegistrantEmailAddress", obj.RegistrantEmailAddress);
                apiRegister += AddParamToQuery("RegistrantPhone", obj.RegistrantPhone);
                apiRegister += AddParamToQuery("RegistrantFax", obj.RegistrantFax);
                apiRegister += AddParamToQuery("ResponseType", RESPONSE_TYPE);


                // Load the API results into an XmlDocument object
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(apiRegister);

                // Read the results
                var rrpCode = xmlDoc.SelectSingleNode("/interface-response/RRPCode"); //.InnerText;
                var rrpText = xmlDoc.SelectSingleNode("/interface-response/RRPText"); //.InnerText;

                if (rrpCode != null)
                {
                    // Perform actions based on results
                    switch (rrpCode.InnerText)
                    {
                        case "200":
                            //register successfully
                            mess = "Register successfully";
                            retValue = true;
                            break;
                        case "540":
                            //register fail
                            mess = string.Format("Error: {0} - {1}", rrpCode.InnerText, rrpText.InnerText);
                            break;
                        default:
                            mess = string.Format("Error: {0} - {1}", rrpCode.InnerText, rrpText.InnerText);
                            break;
                    }
                }
                else
                {
                    mess = ERROR_PARAM_INPUT;
                }
                return retValue;
            }
            catch (Exception ex)
            {
                mess = "Fail: " + ex.Message;
                return false;
                //throw new ApplicationException("operation failed!" + ex.Message, ex);
            }
        }

        #region Mehod private

        private static string AddParamToQuery(string paramName, string value)
        {
            return "&" + paramName + "=" + WebUtility.HtmlEncode(value);
        }
        
        private static string GetUrlApi()
        {
            //!todo: get from config
            bool isTest = IS_MODE_TEST;

            if (isTest) 
            {
                return "http://" + SERVER_TEST + "/interface.asp?";
            }
            else
            {
                return "http://" + SERVER + "/interface.asp?";
            }
        }

        #endregion
    }
    public class DomainRegister
    {
        //!link: http://www.enom.com/APICommandCatalog/API%20topics/api_Purchase.htm?Highlight=purchase#input
        public string Domain { get; set; }
        public string RegistrantOrganizationName { get; set; }
        public string RegistrantFirstName { get; set; }
        public string RegistrantLastName { get; set; }
        public string RegistrantAddress1 { get; set; }
        public string RegistrantCity { get; set; }
        public string RegistrantStateProvince { get; set; }
        public string RegistrantStateProvinceChoice { get; set; }
        public string RegistrantPostalCode { get; set; }
        public string RegistrantCountry { get; set; }
        public string RegistrantEmailAddress { get; set; }
        public string RegistrantPhone { get; set; }
        public string RegistrantFax { get; set; }

        //!Option for payment by card
        //If UseCreditCard=Yes, use our credit card processing services. 
        public string UseCreditCard { get; set; }
        //Type of credit card. Permitted values are Visa, Mastercard, AmEx, Discover
        public string CardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpMonth { get; set; }
        public string CreditCardExpYear { get; set; }
        public string CVV2 { get; set; }
        public string CCName { get; set; }
        // Amount to charge per year for the registration (this value will be multiplied by NumYears 
        //to calculate the total charge to the credit card). Required format is DD.cc 
        public string ChargeAmount { get; set; }
    }
}
