using System;
using System.Data.Entity.Infrastructure;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSW.Data.EF;
using SSW.Data.Interfaces;
using SSW.Data.Tests.Integration.DependencyResolution;
using TestStack.BDDfy;
using SSW.Data.Tests.DomainModel.Entities;

namespace SSW.Data.Tests.Integration
{
    [TestClass]
    public class ConfigurationTests
    {

        protected IContainer container;


        void WhenTheConfigurationIsLoaded()
        {
            container = IOC.Configure();
        }
        

        void ThenIHaveADbContextFactory()
        {
            Assert.IsNotNull(container.Resolve<IDbContextFactory<TestDbContext>>());
        }

        void AndThenIHaveADbContextManager()
        {
            Assert.IsNotNull(container.Resolve<IDbContextManager<TestDbContext>>());
        }

        void AndThenIHaveAUnitOfWork()
        {
            Assert.IsNotNull(container.Resolve<IUnitOfWork>());
        }



        void AndThenIHaveARepository()
        {
            Assert.IsNotNull(container.Resolve<IRepository<Entity2>>());
        }


        [TestMethod]
        public void Execute()
        {
            this.BDDfy();
        }

    }
}
