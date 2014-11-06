 

using SSW.Data.Tests.DomainModel.Entities;
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
}

