using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Core.Helpers;
using Telehire.Data.Domain;

namespace Telehire.Services.Abstract
{
    public interface IAuditService
    {
        void UserLogOut();
        void UserLogin();
        void CreateUser(PersonalInformation userInfo);
        void UpdateUser(PersonalInformation userInfo);

        void CreatePermission(PermissionRecord permmision);
        void UpdatePermission(PermissionRecord permmission);
        void DeletePermission(PermissionRecord permmission);


        void DeleteUserPermission(UserPermission permmission);
        void CreateUserPermission(UserPermission permmision);

        IEnumerable<AuditAction> GetAuditAction(long actionId, long sectionId);
        IEnumerable<AuditSection> GetAuditSection(long sectionId);


        IPagedList<AuditTrail> GetAuditTrail(long? airlineId, string userRole, long actionId, long sectionId, string CurrentUserRole, string searchKeyword, DateTime startDate, DateTime endDate, int pageIndex, int pageSize);


    }
}
