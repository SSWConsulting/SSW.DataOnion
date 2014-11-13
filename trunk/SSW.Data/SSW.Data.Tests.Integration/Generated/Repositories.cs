 


using SSW.Data.Tests.DomainModel.Entities;
using SSW.Data.Tests.DomainModel.MoreEntities;

using SSW.Data.Tests.Integration.RepositoryInterfaces;
using SSW.Data.EF;

namespace SSW.Data.Tests.Integration.Repositories
{
	public partial class TestEntity1Repository : SSW.Data.EF.BaseRepository<TestEntity1>, ITestEntity1Repository
	{
		public TestEntity1Repository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class TestEntity2Repository : SSW.Data.EF.BaseRepository<TestEntity2>, ITestEntity2Repository
	{
		public TestEntity2Repository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
}

