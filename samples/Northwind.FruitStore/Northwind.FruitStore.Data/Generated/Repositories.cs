 


using Northwind.FruitStore.DomainModel.Entities;

using Northwind.FruitStore.Interfaces.Repositories;
using SSW.Data.EF;

namespace Northwind.FruitStore.Data.Repositories
{
	public partial class OrderRepository : SSW.Data.EF.BaseRepository<Order>, IOrderRepository
	{
		public OrderRepository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class OrderLineItemRepository : SSW.Data.EF.BaseRepository<OrderLineItem>, IOrderLineItemRepository
	{
		public OrderLineItemRepository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class ProductRepository : SSW.Data.EF.BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class CategoryRepository : SSW.Data.EF.BaseRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
	public partial class CustomerRepository : SSW.Data.EF.BaseRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(IDbContextManager contextmanager)
			: base(contextmanager)
		{
		}
	}
}

