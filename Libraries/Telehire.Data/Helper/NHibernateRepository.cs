using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Telehire.Data.Domain;

namespace Telehire.Data.Helper
{
    public partial class NHibernateRepository<T, TId> : IRepository<T, TId>
        where T : BaseEntity<TId>
        where TId : struct
    {
        private readonly INHibernateHelper _helper;
        ISession currentSession = null;//NHibernateHelper.GetCurrentSession();
        public NHibernateRepository(INHibernateHelper helper)
        {
            this._helper = helper;
            currentSession = _helper.GetCurrentSession();

        }
        public T GetById(object id)
        {

            return currentSession.Get<T>(id);
        }

        public void ExecuteStoredProcedureUpdate(string StoredName, params object[] parameters)
        {
            List<string> str = new List<string>();
            for (int j = 0; j < parameters.Length; j++)
                str.Add("?");
            var St = String.Join(" , ", str);

            var query = currentSession.CreateSQLQuery("exec " + StoredName + " " + St);
            int i = 0;
            foreach (object obj in parameters)
            {
                query.SetParameter(i, obj);
                ++i;
            }

            query.ExecuteUpdate();

        }

        public void SaveOrUpdate(T entity)
        {
            //using (ITransaction trans = currentSession.BeginTransaction())
            //{
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("Invalid Object " + entity.GetType().Name);



                currentSession.SaveOrUpdate(entity);
                currentSession.Flush();
                currentSession.Refresh(entity);


                //trans.Commit();


            }
            catch (Exception ex)
            {

                // trans.Rollback();
                _helper.CloseSession();
                throw new DataException("Unable to save Entity of type : " + entity.GetType().Name + " REASON::: " + ex.Message);


            }
            // }
        }


        public void Delete(T entity)
        {
            using (ITransaction trans = currentSession.BeginTransaction())
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("Invalid Object " + entity.GetType().Name);
                    currentSession.Delete(entity);

                    currentSession.Flush();

                    trans.Commit();
                }
                catch (Exception ex)
                {

                    trans.Rollback();
                    _helper.CloseSession();
                    throw new DataException("Unable to Delete Entity of type : " + entity.GetType().Name + " REASON::: " + ex.Message);

                }
            }
        }
        /// <summary>
        /// This returns all the Entities in a particular Type and transform to IQueryable
        /// </summary>
        public IQueryable<T> Table
        {

            //using (ITransaction trans = currentSession.BeginTransaction())
            // {

            //try
            //{
            get
            {
                ICriteria icrit = currentSession.CreateCriteria<T>();
                return icrit.Future<T>().AsQueryable<T>();

            }

            // var result =  icrit.List<T>().AsQueryable<T>();

            //// trans.Commit();
            // return result;
            //return result.AsQueryable<T>();


            //}
            //catch (Exception exxx)
            //{
            //  //  trans.Rollback();
            //    NHibernateHelper.CloseSession();
            //    return null;
            //}


            //
            // }
        }




        public void Refresh(T entity)
        {
            currentSession.Refresh(entity);
        }
    }
}
