[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TasksApp.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(TasksApp.NinjectWebCommon), "Stop")]

namespace TasksApp
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Models;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using AutoMapper;
    using System.Web.Http;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<TasksContext>()
                .ToMethod(ctx => HttpContext.Current.GetOwinContext().Get<TasksContext>())
                .InRequestScope();

            kernel.Bind<AppUserManager>()
                .ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>())
                .InRequestScope();

            kernel.Bind<IAuthenticationManager>()
                .ToMethod(ctx => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();

            kernel.Bind<IMapper>()
                .ToMethod(ctx => AutoMapperConfig.GetMapper())
                .InSingletonScope();
        }        
    }
}
