using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Core.Infrastructure;
using Telehire.Core.Infrastructure.Caching;
using Telehire.Data.Domain;
using Telehire.Data.Helper;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public class PermissionService : IPermissionService
    {

        #region Fields

        private readonly IRepository<PermissionRecord, int> _permissionRecordRepository;
        private readonly IRepository<UserPermission, int> _permissionUserRepository;
        private readonly IRepository<RolePermission, int> _rolePermissionRep;
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="permissionPecordRepository">Permission repository</param>
        /// <param name="customerService">Customer service</param>
        /// <param name="workContext">Work context</param>
        /// <param name="cacheManager">Cache manager</param>
        public PermissionService(IRepository<UserPermission, int> userPermission, IRepository<RolePermission, int> rolePermission, IRepository<PermissionRecord, int> permissionPecordRepository,
            IUserService userService,
            IWorkContext workContext, ICacheManager cacheManager)
        {
            this._permissionUserRepository = userPermission;
            this._permissionRecordRepository = permissionPecordRepository;
            this._rolePermissionRep = rolePermission;
            this._userService = userService;
            this._workContext = workContext;
            this._cacheManager = cacheManager;
        }

        #endregion


        public PermissionRecord GetPermissionRecordById(int permissionId)
        {
            throw new NotImplementedException();
        }

        public PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            throw new NotImplementedException();
        }

        public IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }

        public IEnumerable<UserPermission> GetUserPermissions(Guid UserId, long permissionId)
        {
            var query = from u in _permissionUserRepository.Table
                        orderby u.Permission.Name
                        where u.UserId == UserId
                        select u;
            if (permissionId > 0)
                query = query.Where(x => x.Permission.Id == permissionId);

            return query;
        }

        public IEnumerable<PermissionRecord> GetSpecificPermissions(Guid UserId)
        {
            var perms = EngineContext.Current.Resolve<IRepository<PermissionRoleMapping, int>>();
            var userServ = EngineContext.Current.Resolve<IUserService>();
            var info = userServ.GetUserPersonalInformation("", UserId);

            var roleId = 0;
            if (info.UserRole.ToLower() == SystemRoleNames.Administrators.ToLower())
                roleId = 1;
            else if (info.UserRole.ToLower() == SystemRoleNames.Contractors.ToLower())
                roleId = 2;
            else if (info.UserRole.ToLower() == SystemRoleNames.Administrators.ToLower())
                roleId = 3;

            else
                roleId = 4;

            var query = from p in perms.Table
                        orderby p.Permission.Description
                        where p.RoleId == roleId && p.Permission.IsSpecific
                        select p.Permission;
            return query;
        }

        public IEnumerable<PermissionRecord> GetPermissions(long permissionId)
        {
            var perms = EngineContext.Current.Resolve<IRepository<PermissionRecord, int>>();
            var query = perms.Table;

            if (permissionId > 0)
                query = query.Where(x => x.Id == permissionId);


            return query;

        }

        public void SaveUserPermission(UserPermission userPermission)
        {
            var perms = EngineContext.Current.Resolve<IRepository<UserPermission, int>>();
            perms.SaveOrUpdate(userPermission);
        }

        public bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentUserPersonalInformation);
        }


        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(PermissionRecord permission, PersonalInformation person)
        {
            bool answer = false;
            if (permission == null)
                return answer;

            if (person == null)
                return answer;

            var query = from p in _permissionRecordRepository.Table
                        where p.Id == person.Id && p.Description.ToLower() == permission.Description.ToLower()
                        //where p.Name == permission.Name && p.Description.ToLower() == permission.Description.ToLower()
                        select p;

            if (query.Count() == 0)
            {
                query = from p in _rolePermissionRep.Table
                        where p.TelehireRole.RoleName.ToLower() == person.UserRole.ToLower() && p.Permission.Description.ToLower() == permission.Description.ToLower()
                        select p.Permission;

                if (query.Count() > 0)
                    answer = true;
            }

            else
                answer = true;

            return answer;

            //throw new NotImplementedException();
        }

        public bool Authorize(string permission)
        {
            bool answer = false;
            PersonalInformation info = _workContext.CurrentUserPersonalInformation;
            if (info == null)
                throw new ArgumentNullException("PersonalInformation");
            var query = from u in _permissionUserRepository.Table
                        where u.UserId == info.UserId && u.Permission.Description.ToLower() == permission.ToString().ToLower()
                        select u.Permission;
            if (query.Count() == 0)
            {
                query = from u in _rolePermissionRep.Table
                        where u.TelehireRole.RoleName.ToLower() == info.UserRole.ToLower() && u.Permission.Description.ToLower() == permission.ToString().ToLower()
                        select u.Permission;
                if (query.Count() > 0)
                    answer = true;

            }
            else
                answer = true;

            return answer;
        }

        public void DeleteUserPermission(UserPermission userPermission)
        {
            var perms = EngineContext.Current.Resolve<IRepository<UserPermission, int>>();
            perms.Delete(userPermission);
        }



    }
}
