using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Telehire.Services.Abstract
{
    public partial interface IEmailSender
    {

        void SendEmail(string subject, string body,
            MailAddress from, MailAddress to,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null);



        void SendEmail(string subject, string body,
             string toAddress, string toName,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null);

        string NewAcountMail(string FullName, string VerificationUrl, string AccountLogo, string userName, string Email, string password, string AccountName, string AccountUrl, string ContactEmail);
        string PasswordResetMail(string fullName, string userName, string Email, string password);
        string PasswordRecoveryEmail(string fullName, string userName, string Email, string password);

        string ChangePasswordMail(string FullName, string UserName, string NewPassword);

    }
}
