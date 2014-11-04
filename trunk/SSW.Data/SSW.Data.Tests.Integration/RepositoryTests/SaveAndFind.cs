using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSW.Data.Interfaces;
using SSW.Data.Tests.DomainModel.Entities;
using SSW.Data.Tests.Integration.RepositoryInterfaces;

namespace SSW.Data.Tests.Integration.RepositoryTests
{

    public class SaveAndFind
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ITestEntity1Repository repo; 
        
        private TestEntity1 entity;

        public SaveAndFind(IUnitOfWork unitOfWork, ITestEntity1Repository repo)
        {
            this.repo = repo;
            this.unitOfWork = unitOfWork;
        }


        void GivenANewDb()
        {
            Assert.IsFalse(repo.Get().Any());
        }

        void WhenISaveAnEntity()
        {
            entity = new TestEntity1()
            {
                Name = "MyTestName"
            };

            repo.Add(entity);
            unitOfWork.SaveChanges();
        }

        void ThenEntityhasAnId()
        {
            Assert.IsTrue(entity.Id > 0);
        }


        void AndThenICanRetrieveById()
        {
            
            var entity2 = this.repo.Find(entity.Id);
            Assert.IsNotNull(entity2);
            Assert.AreSame("MyTestName", entity2.Name);
        }

    }
}
