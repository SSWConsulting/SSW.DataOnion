 

namespace SSW.Data.Tests.Integration
{
	using System.Data.Entity;
	using SSW.Data.Tests.DomainModel.Entities;

	using SSW.Data.Entities;

	public partial class TestDbContext
	{
		public IDbSet<Entity2> Entity2s { get; set; }

		public IDbSet<Entity3> Entity3s { get; set; }

		public IDbSet<Entity4> Entity4s { get; set; }

	}
}

