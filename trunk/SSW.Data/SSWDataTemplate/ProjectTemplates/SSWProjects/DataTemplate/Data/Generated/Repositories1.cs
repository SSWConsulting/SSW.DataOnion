 

using SSW.Data1.DomainModel.Entities;
using SSW.Data1.Interfaces.Repositories;

using SSW.Data.EF;

namespace SSW.Data1.Data
{
	public partial class Entity2Repository : BaseRepository<Entity2>, IEntity2Repository
	{
		public Entity2Repository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class Entity1Repository : BaseRepository<Entity1>, IEntity1Repository
	{
		public Entity1Repository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
}

