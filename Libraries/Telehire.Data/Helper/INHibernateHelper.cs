using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Telehire.Data.Helper
{
    public interface INHibernateHelper
    {
        ISession GetCurrentSession();
        void CloseSession();
        //void CloseSessionFactory();
    }
}
