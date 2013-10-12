using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Telehire.Data.Domain;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TimeSpan _expirationTimeSpan;
        private readonly HttpContextBase _httpContext;
        private readonly IUserService _userService;
        private IWorkContext _worker;

        public AuthenticationService(HttpContextBase context, IUserService userService, IWorkContext worker)
        {
            this._httpContext = context;
            this._userService = userService;
            this._worker = worker;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }
        public string SignIn(string Email, string password, bool createPersistentCookie)
        {
            string msg = "";
            var user = Membership.GetUser(Email);
            if (user != null)
            {
                if (user.IsApproved)
                {
                    var valid = Membership.ValidateUser(Email, password);
                    if (valid)
                    {
                        var person = _userService.GetUserPersonalInformation(Email, null);
                        FormsAuthentication.SetAuthCookie(Email, false);

                        _worker.CurrentUserPersonalInformation = person;

                    }
                    else
                        msg = "Invalid Password. Please enter a valid password!";

                }
                else
                    msg = "You are not authorized to login";
            }
            else
            {
                msg = "The Username is not valid. Please check again!";
            }


            return msg;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public PersonalInformation GetAuthenticatedUser()
        {


            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var person = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);

            return person;
        }


        public virtual PersonalInformation GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var usernameOrEmail = ticket.Name;

            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;

            return _userService.GetUserPersonalInformation(usernameOrEmail, null);
        }


        public bool IsUserEmailValid(string Email)
        {
            bool valid = false;
            var user = Membership.GetUser(Email);
            if (user == null)
                valid = true;
            return valid;
        }
    }
}
