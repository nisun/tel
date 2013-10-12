using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Data.Domain;

namespace Telehire.Services.Abstract
{
    public interface IPermissionService
    {
        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordById(int permissionId);

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="systemName">Permission system name</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordBySystemName(string systemName);

        /// <summary>
        /// Gets all permissions
        /// </summary>
        /// <returns>Permissions</returns>
        IList<PermissionRecord> GetAllPermissionRecords();

        IEnumerable<UserPermission> GetUserPermissions(Guid UserId, long permissionId);

        IEnumerable<PermissionRecord> GetSpecificPermissions(Guid UserId);

        IEnumerable<PermissionRecord> GetPermissions(long permissionId);

        void SaveUserPermission(UserPermission userPermission);

        bool Authorize(PermissionRecord permission, PersonalInformation person);


        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(PermissionRecord permission);

        bool Authorize(string permission);

        void DeleteUserPermission(UserPermission userPermission);
    }
}
