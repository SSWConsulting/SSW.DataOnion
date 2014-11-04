using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SSW.Data.EF;
using SSW.Data.Interfaces;

namespace SSW.Data.Tests.Integration.DependencyResolution
{
    public class TestModule : Autofac.Module
    {

        public string ConnectionString { get; set; }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<DropCreateInitializer<TestDbContext>>()
                .As<IDatabaseInitializer<TestDbContext>>();

            builder.RegisterType<EffortConnectionFactory>().As<IDbConnectionFactory>();

            builder.Register(c => new DbContextFactory<TestDbContext>(c.Resolve<IDatabaseInitializer<TestDbContext>>(), c.Resolve<IDbConnectionFactory>().CreateConnection("")))
                .As<IDbContextFactory<TestDbContext>>();


            builder.RegisterType<DbContextManager<TestDbContext>>()
                .As<IDbContextManager<TestDbContext>>()
                .As<IDbContextManager>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // Local Assembly scan for Repositories
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();



            // scan local assembly for all our bdd classes
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace.EndsWith("RepositoryTests"));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(t => t.Namespace.EndsWith("UnitOfWorkTests"));  

      

        }
    }
}
