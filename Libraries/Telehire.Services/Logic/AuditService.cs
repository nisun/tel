using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Core.Helpers;
using Telehire.Core.Infrastructure;
using Telehire.Data.Domain;
using Telehire.Data.Helper;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public partial class AuditService : IAuditService
    {

        private readonly IRepository<AuditTrail, long> _AuditTrailRep;
        public readonly IWorkContext _worker;
        public readonly IDateTimeHelper _date;
        public readonly IUtilityService _UtilityService;
        private readonly IRepository<AuditAction, int> _AuditActionRep;
        private readonly IRepository<AuditSection, int> _AuditSectionRep;
        public AuditService(IRepository<AuditAction, int> action, IRepository<AuditSection, int> section, IUtilityService utilityService, IRepository<AuditTrail, long> auditTrail, IWorkContext worker, IDateTimeHelper datetime)
        {
            this._AuditTrailRep = auditTrail;
            this._worker = worker;
            this._date = datetime;
            this._UtilityService = utilityService;
            this._AuditSectionRep = section;
            this._AuditActionRep = action;
        }



        private DateTime CurrentDate
        {
            get
            {
                return _date.ConvertToUserTime(DateTime.Now);
            }
        }

        private PersonalInformation PersonalInformation
        {
            get
            {
                return _worker.CurrentUserPersonalInformation;
            }
        }


        public void UserLogin()
        {
            try
            {
                AuditTrail trail = new AuditTrail();

                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.User_Login;
                trail.Details = PersonalInformation.FullName + " logged in.";
                trail.UserIP = _UtilityService.RemoteIP;
                //trail.AirlineId = PersonalInformation.AirlineId;
                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }

        }

        public void UserLogOut()
        {
            try
            {

                AuditTrail trail = new AuditTrail();

                trail.TimeStamp = CurrentDate;
                trail.UserIP = _UtilityService.RemoteIP;
                trail.UserId = PersonalInformation.UserId;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.User_LogOut;
                trail.Details = PersonalInformation.FullName + " logged out.";
                //trail.AirlineId = PersonalInformation.AirlineId;
                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }

        }


        public void CreateUser(PersonalInformation userInfo)
        {
            try
            {
                AuditTrail trail = new AuditTrail();

                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.UserIP = _UtilityService.RemoteIP;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.Create_User;
                trail.Details = PersonalInformation.FullName + " created a user: " + userInfo.FullName;
                //trail.AirlineId = PersonalInformation.AirlineId;
                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }
        }

        public void UpdateUser(PersonalInformation userInfo)
        {
            try
            {
                AuditTrail trail = new AuditTrail();

                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.UserIP = _UtilityService.RemoteIP;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.Update_user;
                trail.Details = PersonalInformation.FullName + " updated a user: " + userInfo.FullName;
                //trail.AirlineId = PersonalInformation.AirlineId;

                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }
        }

        public void CreatePermission(PermissionRecord permmision)
        {
            try
            {
                AuditTrail trail = new AuditTrail();


                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.UserIP = _UtilityService.RemoteIP;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.Create_Permission;
                trail.Details = PersonalInformation.FullName + " created Permission: " + permmision.Name;
                //trail.AirlineId = PersonalInformation.AirlineId;
                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }
        }

        public void UpdatePermission(PermissionRecord permmission)
        {
            try
            {
                AuditTrail trail = new AuditTrail();

                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.UserIP = _UtilityService.RemoteIP;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.Update_Permission;
                trail.Details = PersonalInformation.FullName + " updated a Permission: " + permmission.Name;
                //trail.AirlineId = PersonalInformation.AirlineId;
                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }
        }


        public void DeletePermission(PermissionRecord permmission)
        {

            try
            {
                AuditTrail trail = new AuditTrail();


                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.UserIP = _UtilityService.RemoteIP;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.Deleted_Permission;
                trail.Details = PersonalInformation.FullName + " removed a Permission: " + permmission.Name;
                //trail.AirlineId = PersonalInformation.AirlineId;
                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }
        }


        public void DeleteUserPermission(UserPermission permmission)
        {
            try
            {

                AuditTrail trail = new AuditTrail();


                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.UserIP = _UtilityService.RemoteIP;
                var personIn = EngineContext.Current.Resolve<IUserService>();
                var pp = personIn.GetUserPersonalInformation("", permmission.UserId);
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.Delete_UserPermission;
                trail.Details = PersonalInformation.FullName + " removed a Permission: " + permmission.Permission.Name + " for user: " + pp.FullName;
                //trail.AirlineId = PersonalInformation.AirlineId;
                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }
        }

        public void CreateUserPermission(UserPermission permmision)
        {
            try
            {
                AuditTrail trail = new AuditTrail();


                trail.TimeStamp = CurrentDate;
                trail.UserId = PersonalInformation.UserId;
                trail.UserIP = _UtilityService.RemoteIP;
                trail.AuditActionId = (int)Telehire.Core.Utility.SystemEnums.AuditActionEnum.Added_UserPermission;
                var personIn = EngineContext.Current.Resolve<IUserService>();
                var pp = personIn.GetUserPersonalInformation("", permmision.UserId);
                trail.Details = PersonalInformation.FullName + " added a Permission: " + permmision.Permission.Name + " for user: " + pp.FullName;

                //trail.AirlineId = PersonalInformation.AirlineId;

                _AuditTrailRep.SaveOrUpdate(trail);
            }
            catch
            {

            }
        }


        public IEnumerable<AuditAction> GetAuditAction(long actionId, long sectionId)
        {
            var query = _AuditActionRep.Table;
            if (actionId > 0)
                query = query.Where(x => x.Id == actionId);
            if (sectionId > 0)
                query = query.Where(x => x.AuditSection.Id == sectionId);


            return query;
        }

        public IEnumerable<AuditSection> GetAuditSection(long sectionId)
        {
            var query = _AuditSectionRep.Table;
            if (sectionId > 0)
                query = query.Where(x => x.Id == sectionId);
            return query;
        }

        public IPagedList<AuditTrail> GetAuditTrail(long? airlineId, string userRole, long actionId, long sectionId, string CurrentUserRole, string searchKeyword, DateTime startDate, DateTime endDate, int pageIndex, int pageSize)
        {
            var query = _AuditTrailRep.Table;
            var IUserService = EngineContext.Current.Resolve<IUserService>();




            if (!string.IsNullOrWhiteSpace(userRole) && userRole != "-1")
                query = query.Where(x => IUserService.GetUserPersonalInformation("", x.UserId).UserRole.ToLower() == userRole.ToLower());
            if (airlineId > 0)
            {
                query = query.Where(x => x.AirlineId.HasValue);
                query = query.Where(x => x.AirlineId == airlineId);
            }
            if (actionId > 0)
                query = query.Where(x => x.AuditActionId == actionId);
            if (sectionId > 0)
                query = query.Where(x => x.AuditAction.AuditSectionId == sectionId);

            query = query.Where(x => x.TimeStamp >= startDate && x.TimeStamp <= endDate.AddHours(23.99));

            if (!string.IsNullOrEmpty(CurrentUserRole))
                query = query.Where(x => IUserService.GetUserPersonalInformation("", x.UserId).UserRole.ToLower() != SystemRoleNames.Administrators.ToString().ToLower());
            if (!string.IsNullOrWhiteSpace(searchKeyword))
                query = query.Where(x => x.Details.Contains(searchKeyword.ToLower()));
            query = query.OrderByDescending(x => x.TimeStamp);
            return new PagedList<AuditTrail>(query, pageIndex, pageSize);
        }
    }
}
