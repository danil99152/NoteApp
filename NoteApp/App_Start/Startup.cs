using Autofac;
using Autofac.Integration.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using NoteApp.App_Start;
using NoteApp.Auth;
using NoteApp.Controllers;
using NoteApp.Models.Listeners;
using NoteApp.Models.Models;
using NoteApp.Models.Repositories;
using NoteApp.Permission;
using Owin;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Startup))]
namespace NoteApp.App_Start
{
    public partial class Startup
    {
        [Obsolete]
        public static void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var connectionString = ConfigurationManager.ConnectionStrings["MSSQL"];
            if (connectionString == null)
            {
                throw new Exception("Не найдена строка соединения");
            }

            var assembly = Assembly.GetAssembly(typeof(User));
            var builder = new ContainerBuilder();

            foreach (var type in assembly.GetTypes())
            {
                var attr = (ListenerAttribute)type.GetCustomAttribute(typeof(ListenerAttribute));
                if (attr != null)
                {
                    var interfaces = type.GetInterfaces();
                    var b = builder.RegisterType(type);
                    foreach (var inter in interfaces)
                    {
                        b = b.As(inter);
                    }
                }
            }

            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString.ConnectionString)
                        .Dialect<MsSql2012Dialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                    .ExposeConfiguration(c =>
                    {
                        SchemaMetadataUpdater.QuoteTableAndColumns(c);
                       // c.EventListeners.PreInsertEventListeners = x.Resolve<IPreInsertEventListener[]>();
                       // c.EventListeners.PreUpdateEventListeners = x.Resolve<IPreUpdateEventListener[]>();
                    })
                    .CurrentSessionContext("call");
                var conf = cfg.BuildConfiguration();
                var schemaExport = new SchemaUpdate(conf);
                schemaExport.Execute(true, true);
                ISessionFactory session = conf.BuildSessionFactory();
                InitialData(session);
                return session;
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
            builder.RegisterControllers(Assembly.GetAssembly(typeof(BaseController)));
            builder.RegisterModule(new AutofacWebTypesModule());
            foreach (var type in assembly.GetTypes())
            {
                var attr = type.GetCustomAttribute(typeof(RepositoryAttribute));
                if (attr != null)
                {
                    builder.RegisterType(type);
                }
            }
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);

            app.CreatePerOwinContext(() =>
                new UserManager(new IdentityStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext(() =>
                new RoleManager(new RoleStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext<SignInManager>((options, context) =>
                new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Signin"),
                Provider = new CookieAuthenticationProvider()
            });
        }

        public static void InitialData(ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                var roleAdmin = new Role("Admin");
                var roleCandidate = new Role("Candidate");
                var roleEmployer = new Role("Employer");
                var roleManager = new RoleManager(new RoleStore(session));
                var userManager = new UserManager(new IdentityStore(session));
                roleManager.CreateAsync(roleCandidate);
                roleManager.CreateAsync(roleEmployer);
                roleManager.CreateAsync(roleAdmin);                                 //Аккаунт admin:
                var userAdmin = new User("root");                                  //Логин: root;
                var result = userManager.CreateAsync(userAdmin, "toor");           //Пароль: toor; 
                if (result.Result.Succeeded)
                {
                    userManager.AddToRoleAsync(userAdmin.Id, roleAdmin.Name);
                    userManager.AddToRoleAsync(userAdmin.Id, roleCandidate.Name);
                    userManager.AddToRoleAsync(userAdmin.Id, roleEmployer.Name);
                }
            }
        }
    }
}