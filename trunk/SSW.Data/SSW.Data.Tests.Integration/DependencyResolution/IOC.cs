using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace SSW.Data.Tests.Integration.DependencyResolution
{
    public class IOC
    {

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new SSW.Data.Tests.Integration.DependencyResolution.TestModule()
            {
                //ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CmsEntities"].ConnectionString
            });


            var container = builder.Build();

            return container;
        }

    }
}
