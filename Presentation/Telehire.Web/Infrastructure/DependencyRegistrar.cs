using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac;
using Telehire.Core;
using Telehire.Core.Helpers;
using Telehire.Core.Infrastructure;
using Telehire.Core.Infrastructure.Caching;
using Telehire.Core.Infrastructure.Fake;
using Telehire.Core.Plugins;
using Telehire.Data.Helper;
using Telehire.Services.Abstract;
using Telehire.Services.Logic;

namespace Telehire.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public void Register(Autofac.ContainerBuilder builder, ITypeLocator typeLocator)
        {


            //HTTP context and other related stuff
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>().InstancePerHttpRequest();

            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerHttpRequest();

            //NhibernateHelper
            builder.RegisterType<NHibernateHelper>().As<INHibernateHelper>().InstancePerLifetimeScope();//.InstancePerHttpRequest();//.SingleInstance();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest();

            builder.RegisterType<WorkerContext>().As<IWorkContext>().InstancePerHttpRequest();



            //controllers
            builder.RegisterControllers(typeLocator.GetAssemblies().ToArray());


            //Register All Controllers
            // builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(NHibernateRepository<,>)).As(typeof(IRepository<,>)).InstancePerHttpRequest();

            builder.RegisterType<StandardPermissionProvider>().As<IPermissionProvider>().InstancePerHttpRequest();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerHttpRequest();


            builder.RegisterType<AsyncRunner>().As<IAsyncRunner>().InstancePerHttpRequest();
            //
            //builder.RegisterType<SchoolContext>().As<ISchoolContext>().InstancePerHttpRequest();
            //builder.RegisterType<SchoolService>().As<ISchoolService>().InstancePerHttpRequest();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerHttpRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerHttpRequest();
            builder.RegisterType<AuditService>().As<IAuditService>().InstancePerHttpRequest();

            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerHttpRequest();
            builder.RegisterType<AuditService>().As<IAuditService>().InstancePerHttpRequest();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerHttpRequest();

            //builder.RegisterType<ThemeContext>().As<IThemeContext>().InstancePerHttpRequest();

            builder.RegisterType<EmailAccount>().As<IEmailAccount>().InstancePerHttpRequest();

            builder.RegisterType<UtilityService>().As<IUtilityService>().InstancePerHttpRequest();

            //builder.RegisterType<AutoMapperStartupTask>().Named<IStartupTask>("AutoMapper").InstancePerHttpRequest();

            builder.RegisterType<Telehire.Web.Framework.Infrastructure.EmbeddedViewResolver>().As<Telehire.Web.Framework.Infrastructure.IEmbeddedViewResolver>().InstancePerLifetimeScope(); //somthing is wrong with the single instance from autofac


            //plugins
            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerHttpRequest();

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("AppForm_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("AppForm_cache_per_request").InstancePerHttpRequest();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();


            builder.RegisterSource(new SettingsSource());
        }
    }

    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            //RegistrationBuilder

            var reg = Autofac.Builder.RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    // var SchoolCode = c.Resolve<ISchoolContext>().CurrentSchool.Code;
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>();


                    //  return c.Resolve<ISettingService>().LoadSetting<TSettings>(SchoolCode);
                }).InstancePerHttpRequest();

            return Autofac.Builder.RegistrationBuilder.CreateRegistration(reg);


        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }
}