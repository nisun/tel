using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Security;
using Telehire.Core.Infrastructure;

namespace Telehire.Core.Providers
{
    public class TenantRoleProvider : SqlRoleProvider
    {
        private void SetDatabase()
        {
            // Get the connection string that you want, thsi could be from existing Db or Session.
            //string connectionString = EngineContext.Current.Resolve<ISchoolContext>().CurrentSchool.ConnectionString;


            // Set private property of Membership provider.
            FieldInfo connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            //connectionStringField.SetValue(this, connectionString);

        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            SetDatabase();
            base.AddUsersToRoles(usernames, roleNames);
        }
        public override void CreateRole(string roleName)
        {
            SetDatabase();
            base.CreateRole(roleName);
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            SetDatabase();
            return base.DeleteRole(roleName, throwOnPopulatedRole);
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            SetDatabase();
            return base.FindUsersInRole(roleName, usernameToMatch);

        }
        public override string[] GetAllRoles()
        {
            SetDatabase();
            return base.GetAllRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            SetDatabase();
            return base.GetRolesForUser(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            SetDatabase();
            return base.GetUsersInRole(roleName);
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            SetDatabase();
            return base.IsUserInRole(username, roleName);
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            SetDatabase();
            base.RemoveUsersFromRoles(usernames, roleNames);
        }
        public override bool RoleExists(string roleName)
        {
            SetDatabase();
            return base.RoleExists(roleName);
        }





        //public override void Initialize(string name, NameValueCollection config)
        //{
        //    base.Initialize(name, config);
        //    // Update the stupid private connection string field in the base class.
        //    string connectionString = Tenancy.CurrentTenant.ConnectionString;

        //    // Set private property of Membership provider.
        //    FieldInfo connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
        //    connectionStringField.SetValue(this, connectionString);


        //}
    }
}
