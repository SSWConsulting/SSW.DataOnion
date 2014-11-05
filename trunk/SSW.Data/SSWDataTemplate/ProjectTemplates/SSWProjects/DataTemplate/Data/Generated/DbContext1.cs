 

namespace SSW.Data1.Data
{
	using System.Data.Entity;
	using SSW.Data1.DomainModel.Entities;

	using SSW.Data.Entities;

	public partial class YourDbContext
	{
		public IDbSet<Entity2> Entity2s { get; set; }

		public IDbSet<Entity1> Entity1s { get; set; }

	}
}

