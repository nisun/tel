using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Security;
using Telehire.Core.Infrastructure;

namespace Telehire.Core.Providers
{
    public class TenantMembershipProvider : SqlMembershipProvider
    {
        private void SetDatabase()
        {
            // Get the connection string that you want, thsi could be from existing Db or Session.
            string connectionString = EngineContext.Current.Resolve<ISchoolContext>().CurrentSchool.ConnectionString;


            // Set private property of Membership provider.
            FieldInfo connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);

        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            SetDatabase();
            return base.ChangePassword(username, oldPassword, newPassword);

        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            SetDatabase();
            return base.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            SetDatabase();
            return base.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            SetDatabase();
            return base.DeleteUser(username, deleteAllRelatedData);
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SetDatabase();
            return base.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SetDatabase();
            return base.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }
        public virtual string GeneratePassword()
        {
            SetDatabase();
            return base.GeneratePassword();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            SetDatabase();
            return base.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }
        public override int GetNumberOfUsersOnline()
        {
            SetDatabase();
            return base.GetNumberOfUsersOnline();
        }
        public override string GetPassword(string username, string passwordAnswer)
        {
            SetDatabase();
            return base.GetPassword(username, passwordAnswer);
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            SetDatabase();
            return base.GetUser(providerUserKey, userIsOnline);
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            SetDatabase();
            return base.GetUser(username, userIsOnline);
        }
        public override string GetUserNameByEmail(string email)
        {
            SetDatabase();
            return base.GetUserNameByEmail(email);
        }
        public override string ResetPassword(string username, string passwordAnswer)
        {
            SetDatabase();
            return base.ResetPassword(username, passwordAnswer);
        }
        public override bool UnlockUser(string username)
        {
            SetDatabase();
            return base.UnlockUser(username);
        }
        public override void UpdateUser(MembershipUser user)
        {
            SetDatabase();
            base.UpdateUser(user);
        }
        public override bool ValidateUser(string username, string password)
        {
            SetDatabase();
            return base.ValidateUser(username, password);

        }


        //public override void Initialize(string name, NameValueCollection config)
        //{
        //    base.Initialize(name, config);
        //     // Update the stupid private connection string field in the base class.
        //    string connectionString = Tenancy.CurrentTenant.ConnectionString;
        //    Type type = GetType().BaseType;
        //    // Set private property of Membership provider.

        //    FieldInfo connectionStringField = type.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
        //    connectionStringField.SetValue(this, connectionString);




        //}

    }
}
