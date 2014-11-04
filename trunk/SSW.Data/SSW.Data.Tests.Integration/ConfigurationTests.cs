using System;
using System.Data.Entity.Infrastructure;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSW.Data.EF;
using SSW.Data.Interfaces;
using SSW.Data.Tests.Integration.DependencyResolution;
using SSW.Data.Tests.Integration.RepositoryInterfaces;
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
            Assert.IsNotNull(container);
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
            Assert.IsNotNull(container.Resolve<ITestEntity1Repository>());
        }


        [TestMethod]
        public void TestConfiguration()
        {
            using(container = IOC.Configure())
            {
                this.BDDfy();
            }
        }
    }
}
