using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Services.Abstract;

namespace Telehire.Services.Logic
{
    public partial class EmailAccount : IEmailAccount
    {
        public int Port
        {
            get
            {
                return 587;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool EnableSSL
        {
            get
            {
                return true;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string HostName
        {
            get
            {
                return "smtp.gmail.com";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserName
        {
            get
            {
                return "portal@splasherstech.com";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Password
        {
            get
            {
                return "admin)(09";
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public string EmailName
        {
            get
            {
                return "VATCollect Portal";
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
