using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSW.Data.Interfaces;
using SSW.Data.Tests.DomainModel.Entities;
using SSW.Data.Tests.Integration.RepositoryInterfaces;

namespace SSW.Data.Tests.Integration.RepositoryTests
{

    public class Query
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ITestEntity1Repository repo;
        
        IEnumerable<TestEntity1> queryResult;

        public Query(IUnitOfWork unitOfWork, ITestEntity1Repository repo)
        {
            this.repo = repo;
            this.unitOfWork = unitOfWork;
        }


        void GivenANewDb()
        {
            Assert.IsFalse(repo.Get().Any());
        }

        void AndGivenSomeEntitiesAreAdded()
        {
            repo.Add(new TestEntity1()
            {
                Name = "Test1"
            });

            repo.Add(new TestEntity1()
            {
                Name = "Test1"
            });

            repo.Add(new TestEntity1()
            {
                Name = "Test2"
            });
            unitOfWork.SaveChanges();
        }


        void WhenIRunAQueryWithFilter()
        {
            queryResult = repo.Get().Where(e => e.Name == "Test1");
        }


        void ThenEntitiesAreReturned()
        {
            Assert.IsTrue(queryResult.Any());
        }


        void AndThenAllEntitiesMeetFilterRequirements()
        {
            queryResult.ToList().ForEach(e => {
                Assert.AreSame("Test1", e.Name);
            });

        }

    }
}
