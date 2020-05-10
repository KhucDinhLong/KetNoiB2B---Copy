using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using SETA.Common.Constants;
using SETA.Common.Utility;

namespace SETA.Common
{
    public class EmailHelper
    {       
        public static bool SendEmail(string from, string to, string cc, string bcc, string subject, string body, string user, string password)
        {
            try
            {
                #region Send mail from SMTP Mail server

                MailMessage Mail = new MailMessage(from, to, subject, body);
                Mail.BodyEncoding = Encoding.UTF8;
                Mail.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(cc))
                {
                    Mail.CC.Add(cc);
                }
                if (!string.IsNullOrEmpty(bcc))
                {
                    Mail.Bcc.Add(bcc);
                }
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(user, password);
                smtp.Send(Mail);
                Mail.Dispose();
                #endregion
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static bool Send(string to, string subject, string body)
        {
            try
            {
                foreach (var address in to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string SMTPUser = Utils.GetSetting(AppKeys.MAIL_SENDER, "user@gmail.com");
                    string SMTPPassword = Utils.GetSetting(AppKeys.MAIL_PASSWORD, "password");

                    //Now instantiate a new instance of MailMessage
                    using (var mail = new MailMessage())
                    {
                        //set the sender address of the mail message
                        mail.From = new MailAddress(SMTPUser, Utils.GetSetting(AppKeys.MAIL_DISPLAY, "TeacherZone.com"));

                        //set the recepient addresses of the mail message
                        mail.To.Add(address);

                        //set the subject of the mail message
                        mail.Subject = subject;

                        //set the body of the mail message
                        mail.Body = body;

                        //leave as it is even if you are not sending HTML message
                        mail.IsBodyHtml = true;

                        //set the priority of the mail message to normal
                        mail.Priority = MailPriority.Normal;

                        mail.SubjectEncoding = Encoding.UTF8;
                        mail.BodyEncoding = Encoding.UTF8;

                        //instantiate a new instance of SmtpClient
                        using (var smtp = new SmtpClient())
                        {
                            //if you are using your smtp server, then change your host like "smtp.yourdomain.com"
                            smtp.Host = Utils.GetSetting(AppKeys.MAIL_HOST, "smtp.gmail.com");

                            //chnage your port for your host
                            smtp.Port = Utils.GetSetting(AppKeys.MAIL_PORT, 25); //or you can also use port# 587

                            //provide smtp credentials to authenticate to your account
                            smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);

                            //if you are using secure authentication using SSL/TLS then "true" else "false"
                            smtp.EnableSsl = true;

                            smtp.Send(mail);
                            mail.Dispose();                            
                        }
                    }   
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("operation failed!", ex);
                //return false;
            }
        }

        public static bool Send(string smptEmail, string smptPassword, string to, string subject, string body)
        {
            try
            {
                foreach (var address in to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {                    
                    //Now instantiate a new instance of MailMessage
                    using (var mail = new MailMessage())
                    {
                        //set the sender address of the mail message
                        mail.From = new MailAddress(Utils.GetSetting(AppKeys.MAIL_USERNAME, "AKIAJ5GCU7KXEKLRY6OA"), Utils.GetSetting(AppKeys.MAIL_DISPLAY, "TeacherZone.com"));

                        //set the recepient addresses of the mail message
                        mail.To.Add(address);

                        //set the subject of the mail message
                        mail.Subject = subject;

                        //set the body of the mail message
                        mail.Body = body;

                        //leave as it is even if you are not sending HTML message
                        mail.IsBodyHtml = true;

                        //set the priority of the mail message to normal
                        mail.Priority = MailPriority.Normal;

                        mail.SubjectEncoding = Encoding.UTF8;
                        mail.BodyEncoding = Encoding.UTF8;

                        //instantiate a new instance of SmtpClient
                        using (var smtp = new SmtpClient())
                        {
                            //if you are using your smtp server, then change your host like "smtp.yourdomain.com"
                            smtp.Host = Utils.GetSetting(AppKeys.MAIL_HOST, "smtp.gmail.com");

                            //chnage your port for your host
                            smtp.Port = Utils.GetSetting(AppKeys.MAIL_PORT, 25); //or you can also use port# 587

                            //provide smtp credentials to authenticate to your account
                            smtp.Credentials = new System.Net.NetworkCredential(smptEmail, smptPassword);

                            //if you are using secure authentication using SSL/TLS then "true" else "false"
                            smtp.EnableSsl = true;

                            smtp.Send(mail);
                            mail.Dispose();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("operation failed!", ex);
                //return false;
            }
        }

        public static bool Send(string smptEmail, string smtpUsername, string smptPassword, string to, string subject, string body)
        {
            try
            {
                foreach (var address in to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    //Now instantiate a new instance of MailMessage
                    using (var mail = new MailMessage())
                    {
                        //set the sender address of the mail message
                        mail.From = new MailAddress(smptEmail, Utils.GetSetting(AppKeys.MAIL_DISPLAY, "TeacherZone.com"));

                        //set the recepient addresses of the mail message
                        mail.To.Add(address);

                        //set the subject of the mail message
                        mail.Subject = subject;

                        //set the body of the mail message
                        mail.Body = body;

                        //leave as it is even if you are not sending HTML message
                        mail.IsBodyHtml = true;

                        //set the priority of the mail message to normal
                        mail.Priority = MailPriority.Normal;

                        mail.SubjectEncoding = Encoding.UTF8;
                        mail.BodyEncoding = Encoding.UTF8;

                        //instantiate a new instance of SmtpClient
                        using (var smtp = new SmtpClient())
                        {
                            //if you are using your smtp server, then change your host like "smtp.yourdomain.com"
                            smtp.Host = Utils.GetSetting(AppKeys.MAIL_HOST, "email-smtp.us-east-1.amazonaws.com");

                            //chnage your port for your host
                            smtp.Port = Utils.GetSetting(AppKeys.MAIL_PORT, 25); //or you can also use port# 587

                            //provide smtp credentials to authenticate to your account
                            smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smptPassword);

                            //if you are using secure authentication using SSL/TLS then "true" else "false"
                            smtp.EnableSsl = true;

                            smtp.Send(mail);
                            mail.Dispose();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("operation failed!", ex);
                //return false;
            }
        }

        /// <summary>
        /// length of paramList = length of valueList
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="paramList"></param>
        /// <param name="valueList"></param>
        /// <returns></returns>
        public static string RenderTemplateMail(string fileName, List<string> paramList, List<string> valueList)
        {
            try
            {
                string result;
                if (paramList.Count == valueList.Count)
                {                    
                    using (var reader = new StreamReader(fileName))
                    {
                        result = reader.ReadToEnd();
                        for (int i = 0; i < paramList.Count; i++)
                        {
                            result = result.Replace(paramList[i], valueList[i]);
                        }
                    }                    
                }
                else
                {
                    result = "Render fail: length of paramList != length of valueList";
                }
                return result;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("operation failed!", ex);
            }
        }

        public static string RenderTemplateMailFromString(string template, List<string> paramList,
            List<string> valueList)
        {
            var result = template;
            if (!string.IsNullOrEmpty(result) && paramList.Count == valueList.Count)
            {
                for (int i = 0; i < paramList.Count; i++)
                {
                    result = result.Replace(paramList[i], valueList[i]);
                }
            }
            else
            {
                result = "Render fail: length of paramList != length of valueList";
            }

            return result;
        }
    }
}
