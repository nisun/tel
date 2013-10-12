using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public partial class EmailSender : IEmailSender
    {
        private readonly IEmailAccount _EmailAccount;
        private readonly HttpContextBase _httpContext;
        public EmailSender(IEmailAccount emailAcct, HttpContextBase context)
        {
            this._EmailAccount = emailAcct;
            this._httpContext = context;
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        public virtual void SendEmail(string subject, string body,
            MailAddress from, MailAddress to,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null)
        {
            var message = new MailMessage();
            message.From = from;
            message.To.Add(to);
            if (null != bcc)
            {
                foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(address.Trim());
                }
            }
            if (null != cc)
            {
                foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                {
                    message.CC.Add(address.Trim());
                }
            }
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = false; // emailAccount.UseDefaultCredentials;
                    smtpClient.Host = _EmailAccount.HostName;
                    smtpClient.Port = _EmailAccount.Port;
                    smtpClient.EnableSsl = _EmailAccount.EnableSSL;
                    //if (emailAccount.UseDefaultCredentials)
                    //    smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    //else
                    smtpClient.Credentials = new NetworkCredential(_EmailAccount.UserName, _EmailAccount.Password);
                    smtpClient.Send(message);
                }
            }
            catch
            {//but this is wrong

            }
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        public void SendEmail(string subject, string body,
            string toAddress, string toName,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null)
        {
            SendEmail(subject, body,
                new MailAddress(_EmailAccount.UserName, _EmailAccount.EmailName), new MailAddress(toAddress, toName),
                bcc, cc);
        }

        public string ReadEmail(string emailType)//protected
        {
            string filepath = _httpContext.Server.MapPath("~") + "\\Content\\Emails\\" + emailType + ".txt";
            if (File.Exists(filepath))
            {
                StreamReader reader = new StreamReader(filepath);
                string email = reader.ReadToEnd();
                return email;
            }
            return null;
        }

        public string NewAcountMail(string FullName, string VerificationUrl, string AccountLogo, string userName, string Email, string password, string AccountName, string AccountUrl, string ContactEmail)
        {

            return ReadEmail("NewAccountEmail").Replace("{SchoolLogo}", AccountLogo).Replace("{schoolName}", AccountName).Replace("{fullName}", FullName).Replace("{userName}", userName).Replace("{password}", password).Replace("{schoolUrl}", AccountUrl).Replace("{schoolEmail}", ContactEmail).Replace("{Verify}", VerificationUrl);

        }


        public string PasswordResetMail(string fullName, string userName, string Email, string password)
        {
            throw new NotImplementedException();
        }

        public string PasswordRecoveryEmail(string fullName, string userName, string Email, string password)
        {
            return ReadEmail("PasswordRecoveryEmail").Replace("{fullName}", fullName).Replace("{userName}", userName).Replace("{userEmail}", userName).Replace("{newPassword}", password);

        }

        public string ChangePasswordMail(string FullName, string UserName, string NewPassword)
        {
            return ReadEmail("ChangePasswordEmail").Replace("{fullName}", FullName).Replace("{userName}", UserName).Replace("{userEmail}", UserName).Replace("{newPassword}", NewPassword);

        }





    }
}
