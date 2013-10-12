using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telehire.Data.Domain;

namespace Telehire.Data.Helper
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T, TId>
        where T : BaseEntity<TId>
        where TId : struct
    {
        T GetById(object id);
        void SaveOrUpdate(T entity);

        void Delete(T entity);
        void Refresh(T entity);
        IQueryable<T> Table { get; }

        void ExecuteStoredProcedureUpdate(string StoredName, params object[] parameters);
    }
}
