using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.SqlCommand;

namespace Telehire.Data.Helper
{
    public class NHSQLInterceptor : EmptyInterceptor, IInterceptor
    {
        SqlString IInterceptor.OnPrepareStatement(SqlString sql)
        {
            //SqlString go = new SqlString("SET ARITHABORT ON \n\r GO \n\r");
            NHSQL.NHibernateSQL = sql.ToString();
            return sql;
        }
    }
}
