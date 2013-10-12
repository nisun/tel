using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Data.Domain;

namespace Telehire.Services.Abstract
{
    public interface IAuthenticationService
    {
        string SignIn(string Email, string password, bool createPersistentCookie);
        void SignOut();
        PersonalInformation GetAuthenticatedUser();
        bool IsUserEmailValid(string Email);
    }
}
