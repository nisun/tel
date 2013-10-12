using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Telehire.Core.Helpers;
using Telehire.Core.Infrastructure;
using Telehire.Data.Domain;
using Telehire.Data.Helper;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public partial class UserService : IUserService
    {
        private readonly IRepository<PersonalInformation, long> _personalInformation;
        private readonly IEmailSender _EmailSender;
        private readonly IAsyncRunner _IAsyncRunner;
        private readonly HttpContextBase _IHttpContextBase;

        public UserService(IRepository<PersonalInformation, long> personalInfo, IEmailSender emailSender, IAsyncRunner iAsyhcRunner, HttpContextBase httpContextBase)
        {
            this._personalInformation = personalInfo;
            this._EmailSender = emailSender;
            this._IAsyncRunner = iAsyhcRunner;
            this._IHttpContextBase = httpContextBase;
        }
        public string UpdatePersonalInformation(string firstName, string LastName, string PhoneNumber, string Email)
        {
            string msg = "";
            var person = GetPersonalInformation(Email);
            if (person != null)
            {
                person.PhoneNumber = PhoneNumber;
                person.FirstName = firstName;
                person.LastName = LastName;
                _personalInformation.SaveOrUpdate(person);
                return msg;
            }
            else
            {
                msg = "User does not exist";
                return msg;
            }
        }

        private string GetErrorMessage(MembershipCreateStatus membershipCreateStatus)
        {


            switch (membershipCreateStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";
                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";
                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";
                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }

        }



        public void UpdateUser(PersonalInformation pInfo)
        {
            throw new NotImplementedException();
        }

        public PersonalInformation GetPersonalInformation(string Email)
        {
            var query = from p in _personalInformation.Table
                        where p.Email.ToLower() == Email.ToLower()
                        select p;

            return query.FirstOrDefault();
        }

        public string CreateUser(PersonalInformation pInfo, string UserRole, bool isApproved, string password)
        {
            string msg = "";
            MembershipCreateStatus status = new MembershipCreateStatus();
            Membership.CreateUser(pInfo.Email, password, pInfo.Email, "What's your Email Address?", pInfo.Email, true, out status);
            if (status == MembershipCreateStatus.Success)
            {
                Roles.AddUserToRole(pInfo.Email, UserRole.ToString());
                var user = Membership.GetUser(pInfo.Email);



                Membership.UpdateUser(user);

                pInfo.UserRole = UserRole.ToString();
                pInfo.UserId = (Guid)user.ProviderUserKey;
                pInfo.EmailVerified = true;
                _personalInformation.SaveOrUpdate(pInfo);

                //string body = _EmailSender.NewAcountMail(pInfo.FirstName, "", "", pInfo.Email, pInfo.Email, password, "", "", "");
                //send user Mail:

                try
                {
                    //_IAsyncRunner.Run<IEmailSender>(sender =>
                    //{
                    //    sender.SendEmail(SystemEmailTypes.NEW_ACCOUNT, body, pInfo.Email, pInfo.FullName);
                    //});

                }
                catch (Exception ex)
                {

                }



            }
            else
            {
                msg = GetErrorMessage(status);

            }

            return msg;

        }

        public string ChangePassword(string password, string confirmPassword, string OldPassword, string username)
        {
            string msg = ""; MembershipUser user = null;
            if (password != confirmPassword)
            {
                return msg = "your new password does not match";
            }

            if (!string.IsNullOrWhiteSpace(username))
                user = Membership.GetUser(username);

            if (user != null)
            {
                // if (user.GetPassword() != OldPassword)
                //  {
                //     return msg = "You Old Password was wrong.";
                // }
                try
                {

                    if (!user.ChangePassword(OldPassword, password))
                        return msg = "Old password is not correct";
                    Membership.UpdateUser(user);


                    var person = GetPersonalInformation(username);
                    string logo = "";

                    string body = _EmailSender.ChangePasswordMail(person.FullName, username, password);

                    try
                    {
                        _IAsyncRunner.Run<IEmailSender>(sender =>
                        {
                            sender.SendEmail(SystemEmailTypes.CHANGE_PASSWORD, body, person.Email, person.LastName);
                        });

                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (Exception ecx)
                {
                    return msg = ecx.Message;
                }

            }
            else
                msg = "You do not have a valid username.";

            return msg;
        }

        public string VerifyUser(string password, string username)
        {
            throw new NotImplementedException();
        }

        public string RecoverPassword(string username)
        {
            string msg = "";

            var user = Membership.GetUser(username);
            if (user != null)
            {
                string password = user.GetPassword();
                // Membership.UpdateUser(user);
                //send a mail from here:
                //send a mail here:

                var person = GetPersonalInformation(username);


                string body = _EmailSender.PasswordRecoveryEmail("", person.FullName, username, password);

                try
                {
                    _IAsyncRunner.Run<IEmailSender>(sender =>
                    {
                        sender.SendEmail(SystemEmailTypes.PASSWORD_RECOVERY, body, person.Email, person.LastName);
                    });

                }
                catch (Exception ex)
                {

                }

            }
            else
                msg = "This username does not exist on this system.";
            return msg;

        }

        public string UpdateUser(PersonalInformation pInfo, string UserRole, bool isApproved, string password)
        {
            string msg = "";
            if (pInfo != null)
            {
                //if (pInfo.EmailVerified)
                //{
                var user = Membership.GetUser(pInfo.Email);
                if (user != null)
                {
                    if (Roles.GetRolesForUser(pInfo.Email).FirstOrDefault().ToLower() != UserRole.ToString().ToLower())
                    {
                        Roles.RemoveUserFromRole(pInfo.Email, Roles.GetRolesForUser(pInfo.Email).FirstOrDefault());
                        Roles.AddUserToRole(pInfo.Email, UserRole.ToString());

                    }

                    pInfo.EmailVerified = isApproved;
                    pInfo.UserRole = UserRole.ToString();
                    _personalInformation.SaveOrUpdate(pInfo);


                    user.IsApproved = isApproved;

                    //if(isLocked)
                    //user.UnlockUser();

                    Membership.UpdateUser(user);



                    //    string body = "";// _EmailSender.NewAcountMail(pInfo.FirstName, "", "", pInfo.Email, pInfo.Email, password, "", "", "");
                    ////send user Mail:

                    //       try
                    //       {
                    //           _IAsyncRunner.Run<IEmailSender>(sender =>
                    //           {
                    //               sender.SendEmail(VATCollect.BussinessLogic.Utility.SystemEnums.EmailTypes.USER_UPDATED, body, pInfo.Email, pInfo.FullName);
                    //           });

                    //       }
                    //       catch (Exception ex)
                    //       {

                    //       }

                }
                else
                    msg = "This person is not valid on the system.";

            }
            else
                msg = "This person is not valid on the system.";


            return msg;
        }

        public IList<PersonalInformation> GetPersonalInformation(string systemRole, string username, string permissionKey, int permissionType)
        {
            var table = _personalInformation.Table;
            if (!string.IsNullOrWhiteSpace(systemRole))
                table = table.Where(x => x.UserRole.ToLower() == systemRole.ToLower());
            if (!string.IsNullOrWhiteSpace(username))
                table = table.Where(x => x.Email.ToLower() == username.ToLower());




            return table.ToList();

        }

        public string ResetPassword(string username)
        {
            string msg = "";

            var user = Membership.GetUser(username);
            if (user != null)
            {
                //var newPassword = username.Split('@')[0];
                string password = user.ResetPassword();
                Membership.UpdateUser(user);
                var person = GetPersonalInformation(username);


                string body = _EmailSender.PasswordRecoveryEmail(person.FullName, person.Email, person.Email, password);
                //send user Mail:

                try
                {
                    _IAsyncRunner.Run<IEmailSender>(sender =>
                    {
                        sender.SendEmail(SystemEmailTypes.PASSWORD_RECOVERY, body, person.Email, person.FullName);
                    });

                }
                catch (Exception ex)
                {

                }



            }
            else
                msg = "This username does not exist on this system.";
            return msg;
        }


        public PersonalInformation GetUserPersonalInformation(string EmailAddress, Guid? UserId)
        {
            IQueryable<PersonalInformation> table = _personalInformation.Table;

            if (!string.IsNullOrWhiteSpace(EmailAddress))
                table = table.Where(x => x.Email.ToLower() == EmailAddress.ToLower());
            if (UserId.HasValue)
                table = table.Where(x => x.UserId == UserId.Value);



            return table.FirstOrDefault();
        }

        public IPagedList<PersonalInformation> GetPersonalInformation(
            string systemRole, string username, string nameKeyword, int verified, int pageIndex, int pageSize)
        {
            var table = _personalInformation.Table;
            if (!string.IsNullOrWhiteSpace(systemRole))
                table = table.Where(x => x.UserRole.ToLower() == systemRole.ToLower());
            var _worker = EngineContext.Current.Resolve<IWorkContext>();
            if (string.IsNullOrWhiteSpace(systemRole) && _worker.CurrentUserPersonalInformation.UserRole.ToLower() != SystemRoleNames.Administrators.ToString().ToLower())
            {
                table = table.Where(x => x.UserRole.ToLower() != SystemRoleNames.Administrators.ToString().ToLower());
            }


            if (!string.IsNullOrWhiteSpace(username))
                table = table.Where(x => x.Email.ToLower() == username.ToLower());
            if (!string.IsNullOrWhiteSpace(nameKeyword))
                table = table.Where(x => x.FirstName.ToLower().Contains(nameKeyword.ToLower()) || x.LastName.ToLower().Contains(nameKeyword.ToLower()));
            if (verified > 0)
            {

                bool very = verified == 1 ? true : false;
                table = table.Where(x => x.EmailVerified == very);
            }



            return new PagedList<PersonalInformation>(table, pageIndex, pageSize);
            //throw new NotImplementedException();
        }

        public IQueryable<PersonalInformation> GetPersonalInformationQueryable(
            string systemRole, string username, string nameKeyword, int verified, int pageIndex, int pageSize)
        {
            var table = _personalInformation.Table;
            if (!string.IsNullOrWhiteSpace(systemRole))
                table = table.Where(x => x.UserRole.ToLower() == systemRole.ToLower());
            var _worker = EngineContext.Current.Resolve<IWorkContext>();
            if (string.IsNullOrWhiteSpace(systemRole) && _worker.CurrentUserPersonalInformation.UserRole.ToLower() != SystemRoleNames.Administrators.ToString().ToLower())
            {
                table = table.Where(x => x.UserRole.ToLower() != SystemRoleNames.Administrators.ToString().ToLower());
            }


            if (!string.IsNullOrWhiteSpace(username))
                table = table.Where(x => x.Email.ToLower() == username.ToLower());
            if (!string.IsNullOrWhiteSpace(nameKeyword))
                table = table.Where(x => x.FirstName.ToLower().Contains(nameKeyword.ToLower()) || x.LastName.ToLower().Contains(nameKeyword.ToLower()));
            if (verified > 0)
            {

                bool very = verified == 1 ? true : false;
                table = table.Where(x => x.EmailVerified == very);
            }

            var s = table.Take(pageSize).Skip(pageIndex);


            return s;
            //throw new NotImplementedException();
        }
    }
}
