 

using SSW.Data.Tests.DomainModel.Entities;
using SSW.Data.Tests.DomainModel.MoreEntities;
using SSW.Data.Interfaces;

namespace SSW.Data.Tests.Integration.RepositoryInterfaces
{
	public partial interface ITestEntity1Repository : IRepository<TestEntity1>
	{
	}
	public partial interface ITestEntity2Repository : IRepository<TestEntity2>
	{
	}
}

