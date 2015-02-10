using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Northwind.FruitStore.DependencyResolvr;

namespace Northwind.FruitStore.WebUI.App_Start
{
    public class IoCConfig
{

    /// <summary>
    /// from http://stackoverflow.com/questions/21614253/how-do-i-properly-register-autofac-in-a-basic-mvc5-1-website
    /// For more info see 
    /// :https://code.google.com/p/autofac/wiki/MvcIntegration (mvc4 instructions)
    /// </summary>
    public static void RegisterDependencies()
    {
        #region Create the builder
        var builder = new ContainerBuilder();
        #endregion

        #region Setup a common pattern
        // placed here before RegisterControllers as last one wins
        builder.RegisterAssemblyTypes()
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerHttpRequest();
        builder.RegisterAssemblyTypes()
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerHttpRequest();
        #endregion

        #region Register all controllers for the assembly
        // Note that ASP.NET MVC requests controllers by their concrete types, 
        // so registering them As<IController>() is incorrect. 
        // Also, if you register controllers manually and choose to specify 
        // lifetimes, you must register them as InstancePerDependency() or 
        // InstancePerHttpRequest() - ASP.NET MVC will throw an exception if 
        // you try to reuse a controller instance for multiple requests. 
        builder.RegisterControllers(typeof(MvcApplication).Assembly)
               .InstancePerHttpRequest();

        #endregion

        #region Register modules
        builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
        builder.RegisterAssemblyModules(typeof(RegisterApplicationIoC).Assembly);
        #endregion

        #region Model binder providers - excluded - not sure if need
        //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
        //builder.RegisterModelBinderProvider();
        #endregion

        #region Inject HTTP Abstractions
        /*
         The MVC Integration includes an Autofac module that will add HTTP request 
         lifetime scoped registrations for the HTTP abstraction classes. The 
         following abstract classes are included: 
        -- HttpContextBase 
        -- HttpRequestBase 
        -- HttpResponseBase 
        -- HttpServerUtilityBase 
        -- HttpSessionStateBase 
        -- HttpApplicationStateBase 
        -- HttpBrowserCapabilitiesBase 
        -- HttpCachePolicyBase 
        -- VirtualPathProvider 

        To use these abstractions add the AutofacWebTypesModule to the container 
        using the standard RegisterModule method. 
        */
        builder.RegisterModule<AutofacWebTypesModule>();

        #endregion

        #region Set the MVC dependency resolver to use Autofac
        var container = builder.Build();
        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        #endregion

    }

 }

}
