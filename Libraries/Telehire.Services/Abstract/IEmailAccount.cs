using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telehire.Services.Abstract
{
    public partial interface IEmailAccount
    {
        int Port { get; set; }
        bool EnableSSL { get; set; }
        string HostName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }

        string EmailName { get; set; }
    }
}
