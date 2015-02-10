using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Northwind.FruitStore.Data;
using Northwind.FruitStore.Data.Repositories;
using Northwind.FruitStore.Interfaces.Repositories;
using SSW.Data.EF;
using SSW.Data.Interfaces;

namespace Northwind.FruitStore.DependencyResolvr
{
    public class RegisterApplicationIoC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            // ef's database initializer
            builder.RegisterType<DropCreateInitializer<YourDbContext>>()
                .As<IDatabaseInitializer<YourDbContext>>();

            // factory makes new dbcontext instances
            builder.RegisterType<DbContextFactory<YourDbContext>>()
                .WithParameter("connectionString", System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
                .As<IDbContextFactory<YourDbContext>>();

            // context manager looks after the lifycycle of the dbcontext. call dispose to dispose the dbcontext
            builder.RegisterType<DbContextManager<YourDbContext>>()
                .As<IDbContextManager<YourDbContext>>()
                .As<IDbContextManager>()
                .InstancePerRequest();


            builder.RegisterType<ProductRepository>()
                   .As<IProductRepository>()
                   .InstancePerHttpRequest();


            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerHttpRequest();



        }
    }
}
