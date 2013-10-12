using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Core.Utility
{
    public class SystemEnums
    {
        public enum AppFormRoles
        {
            Administrators = 1,
            Schools = 2,
            Applicants = 3
        }

        public enum AuditSectionEnum
        {
            Login = 1,
            ManageUsers = 2

        }

        public enum AuditActionEnum
        {
            User_LogOut = 1,
            User_Login = 2,
            Create_User = 3,
            Update_user = 4,
            Create_Permission = 5,
            Update_Permission = 6,
            Deleted_Permission = 7,
            Delete_UserPermission = 8,
            Added_UserPermission = 9


        }



        public class EmailTypes
        {
            public static string NEW_ACCOUNT = "New Account Creation";
            public static string CHANGE_PASSWORD = "Password Changed";
            public static string PASSWORD_RECOVERY = "Password Recovery";
            public static string USER_UPDATED = "User Information Update";

        }
    }
}
