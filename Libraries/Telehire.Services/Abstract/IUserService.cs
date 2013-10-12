using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Core.Helpers;
using Telehire.Data.Domain;

namespace Telehire.Services.Abstract
{
    public interface IUserService
    {

        string UpdatePersonalInformation(string firstName, string LastName, string PhoneNumber, string Email);

        void UpdateUser(PersonalInformation pInfo);

        PersonalInformation GetPersonalInformation(string Email);
        string CreateUser(PersonalInformation pInfo, string UserRole, bool isApproved, string password);
        string ChangePassword(string password, string confirmPassword, string OldPassword, string username);
        string VerifyUser(string password, string username);

        string RecoverPassword(string username);
        string UpdateUser(PersonalInformation pInfo, string UserRole, bool isApproved, string password);

        string ResetPassword(string username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemRole"></param>
        /// <param name="username"></param>
        /// <param name="accountId"></param>
        /// <param name="permissionKey"></param>
        /// <param name="permissionType">/param>
        /// <returns></returns>
        IList<PersonalInformation> GetPersonalInformation(string systemRole, string username, string permissionKey, int permissionType);
        PersonalInformation GetUserPersonalInformation(string EmailAddress, Guid? UserId);
        IPagedList<PersonalInformation> GetPersonalInformation(
            string systemRole, string username, string nameKeyword, int verified, int pageIndex, int pageSize);

        IQueryable<PersonalInformation> GetPersonalInformationQueryable(string systemRole, string username, string nameKeyword, int verified, int pageIndex, int pageSize);

    }
}
