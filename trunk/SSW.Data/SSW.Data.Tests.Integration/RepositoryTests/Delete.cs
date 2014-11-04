using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSW.Data.Interfaces;
using SSW.Data.Tests.DomainModel.Entities;
using SSW.Data.Tests.Integration.RepositoryInterfaces;

namespace SSW.Data.Tests.Integration.RepositoryTests
{
    public class Delete
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITestEntity1Repository repo;


        
        public Delete(IUnitOfWork unitOfWork, ITestEntity1Repository repo)
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
                Name = "Test2"
            });

            repo.Add(new TestEntity1()
            {
                Name = "Test3"
            });
            unitOfWork.SaveChanges();

            Assert.AreEqual(3, repo.Get().Count());
        }



        void WhenIDeleteAnEntity()
        {
            var entity = repo.Get().First();
            repo.Delete(entity);
            unitOfWork.SaveChanges();
        }


        void ThenTheTotalCountIsReduced()
        {
            Assert.AreEqual(2, repo.Get().Count());
        }

    }
}
