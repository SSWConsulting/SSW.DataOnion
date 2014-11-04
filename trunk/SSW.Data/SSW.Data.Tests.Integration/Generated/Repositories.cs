 

using SSW.Data.Tests.DomainModel.Entities;
using SSW.Data.Tests.Integration.RepositoryInterfaces;

using SSW.Data.EF;

namespace SSW.Data.Tests.Integration.Repositories
{
	public partial class Entity2Repository : BaseRepository<Entity2>, IEntity2Repository
	{
		public Entity2Repository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class Entity3Repository : BaseRepository<Entity3>, IEntity3Repository
	{
		public Entity3Repository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class Entity4Repository : BaseRepository<Entity4>, IEntity4Repository
	{
		public Entity4Repository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
}

