 

using Northwind.FruitStore.DomainModel.Entities;
using SSW.Data.Interfaces;

namespace Northwind.FruitStore.Interfaces.Repositories
{
	public partial interface IOrderRepository : IRepository<Order>
	{
	}
	public partial interface IOrderLineItemRepository : IRepository<OrderLineItem>
	{
	}
	public partial interface IProductRepository : IRepository<Product>
	{
	}
	public partial interface ICategoryRepository : IRepository<Category>
	{
	}
	public partial interface ICustomerRepository : IRepository<Customer>
	{
	}
}

