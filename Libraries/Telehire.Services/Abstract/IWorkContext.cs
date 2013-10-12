using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Data.Domain;

namespace Telehire.Services.Abstract
{
    public interface IWorkContext
    {
        /// <summary>
        /// User's Personal Information
        /// </summary>
        PersonalInformation CurrentUserPersonalInformation { get; set; }

    }
}
