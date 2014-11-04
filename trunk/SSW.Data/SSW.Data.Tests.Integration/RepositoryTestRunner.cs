using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSW.Data.Tests.Integration.DependencyResolution;
using SSW.Data.Tests.Integration.RepositoryTests;
using TestStack.BDDfy;

namespace SSW.Data.Tests.Integration
{
    [TestClass]
    public class RepositoryTestRunner
    {

        

        [TestMethod]
        public void SaveAndFind()
        {
            using(var container = IOC.Configure())
            {
                container.Resolve<SSW.Data.Tests.Integration.RepositoryTests.SaveAndFind>().BDDfy();
            }
        }


        [TestMethod]
        public void Query()
        {
            using (var container = IOC.Configure())
            {
                container.Resolve<SSW.Data.Tests.Integration.RepositoryTests.Query>().BDDfy();
            }
        }



        [TestMethod]
        public void Delete()
        {
            using (var container = IOC.Configure())
            {
                container.Resolve<SSW.Data.Tests.Integration.RepositoryTests.Delete>().BDDfy();
            }
        }


    }
}
