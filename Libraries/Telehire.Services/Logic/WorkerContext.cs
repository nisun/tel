using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Telehire.Core.Infrastructure;
using Telehire.Data.Domain;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public class WorkerContext : IWorkContext
    {
        private const string UserCookieName = "AppForms.User";
        private readonly HttpContextBase _httpContext;

        private PersonalInformation _cachedPersonalInfo;

        public WorkerContext(HttpContextBase context)
        {
            this._httpContext = context;
        }

        protected HttpCookie GetUserCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[UserCookieName];
        }


        public PersonalInformation CurrentUserPersonalInformation
        {
            get
            {
                return GetCurrentUserPersonalInfo();
            }
            set
            {
                // SetUserCookie(value.UserId);
                _cachedPersonalInfo = value;
            }
        }


        protected PersonalInformation GetCurrentUserPersonalInfo()
        {
            if (_cachedPersonalInfo != null)
                return _cachedPersonalInfo;
            // PersonalInformation info = null;
            if (_httpContext != null)
            {
                _cachedPersonalInfo = EngineContext.Current.Resolve<IAuthenticationService>().GetAuthenticatedUser();
            }


            return _cachedPersonalInfo;
        }



    }
}
