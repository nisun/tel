using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
//using System.Xml;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using Telehire.Core.Helpers;

namespace Telehire.Data.Helper
{
    public sealed class NHibernateHelper
    {
        private const string CURRENT_NHIBERNATE_SESSION_KEY = "VATCollect.NHIBERNATE.SESSION.KEY";
        private static readonly ISessionFactory sessionFactory;
        //private static readonly HttpContextBase _HttpContext;

        static NHibernateHelper()
        {
            sessionFactory = ConfigureNHibernate();
        }

        private static ISessionFactory ConfigureNHibernate()
        {
            var configure = new Configuration();
            var mapping = GetMappings();
            configure.Configure();
            configure.AddDeserializedMapping(mapping, "NHSchema");


            configure.Configure().SetProperty(NHibernate.Cfg.Environment.ConnectionStringName, "VATConnectionSetting").
                SetProperty(NHibernate.Cfg.Environment.ShowSql, "true").SetProperty(NHibernate.Cfg.Environment.BatchSize, "0");


            return configure.BuildSessionFactory();
        }

        private static HbmMapping GetMappings()
        {

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.Load("VATCollect.BussinessLogic").GetTypes());//.GetExportedTypes());
            //mapper.AddMappings(Assembly.Load("VATCollect.BussinessLogic").GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            return mapping;

        }


        public static ISession GetCurrentSession()
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = context.Items[CURRENT_NHIBERNATE_SESSION_KEY] as ISession;

            if (currentSession == null)
            {
                currentSession = sessionFactory.OpenSession();
                context.Items[CURRENT_NHIBERNATE_SESSION_KEY] = currentSession;
            }
            if (currentSession.Connection.State == System.Data.ConnectionState.Closed)
            {
                currentSession = sessionFactory.OpenSession();

            }
            if (!currentSession.IsConnected)
            {
                currentSession = sessionFactory.OpenSession();

            }
            if (!currentSession.IsOpen)
            {
                currentSession = sessionFactory.OpenSession();

            }
            if (currentSession.IsDirty())
            {
                currentSession.Clear();

            }


            return currentSession;
        }

        public static void CloseSession()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                ISession currentSession = context.Items[CURRENT_NHIBERNATE_SESSION_KEY] as ISession;

                if (currentSession == null)
                {
                    // No current session
                    return;
                }
                currentSession.Clear();
                currentSession.Close();


                context.Items.Remove(CURRENT_NHIBERNATE_SESSION_KEY);
            }
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}
