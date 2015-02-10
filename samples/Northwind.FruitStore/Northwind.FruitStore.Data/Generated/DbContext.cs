 

namespace Northwind.FruitStore.Data
{
	using System.Data.Entity;
	
		using Northwind.FruitStore.DomainModel.Entities;


	using SSW.Data.Entities;

	public partial class YourDbContext
	{
		public IDbSet<Order> Orders { get; set; }

		public IDbSet<OrderLineItem> OrderLineItems { get; set; }

		public IDbSet<Product> Products { get; set; }

		public IDbSet<Category> Categorys { get; set; }

		public IDbSet<Customer> Customers { get; set; }

	}
}

