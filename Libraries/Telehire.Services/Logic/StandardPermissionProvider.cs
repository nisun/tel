using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Data.Domain;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public class StandardPermissionProvider : IPermissionProvider
    {
        //Admin
        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord { Name = "AccessAdminPanel", Description = "Access Admin Panel" };
        public static readonly PermissionRecord CanCreateUser = new PermissionRecord { Name = "CanCreateUser", Description = "Can Create User" };


        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[] 
            {
                AccessAdminPanel, 
                CanCreateUser

            };


        }


        public virtual IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[] 
            {
                new DefaultPermissionRecord 
                {
                    SystemRoleType = SystemRoleNames.Administrators,
                     PermissionRecords =new[]{

                         AccessAdminPanel
                     }

                },
                new DefaultPermissionRecord 
                {

                    SystemRoleType = SystemRoleNames.Contractors,
                    PermissionRecords =new[]{

                         AccessAdminPanel
                     }
                },
                new DefaultPermissionRecord 
                {
                     SystemRoleType = SystemRoleNames.Freelancers,
                    PermissionRecords =new[]{

                         AccessAdminPanel
                     }
                }

            };


        }



    }
}
