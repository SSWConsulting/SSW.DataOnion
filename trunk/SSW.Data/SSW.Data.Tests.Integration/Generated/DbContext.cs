 

namespace SSW.Data.Tests.Integration
{
	using System.Data.Entity;
	using SSW.Data.Tests.DomainModel.Entities;

	using SSW.Data.Entities;

	public partial class TestDbContext
	{
		public IDbSet<TestEntity1> TestEntity1s { get; set; }

	}
}

