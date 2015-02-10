using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SSW.Data.Entities;

namespace Northwind.FruitStore.DomainModel.Entities
{
    
    public class Order: BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public string OrderCode { get; set; }
        public ICollection<OrderLineItem> OrderLineItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
    public class OrderLineItem:BaseEntity
    {
        public virtual Product Product { get; set; }
        public int Qty { get; set; }
        public decimal PriceDecimal { get; set; }
        public virtual Order Order { get; set; }
    }
    public class Product:NamedEntity
    {
        public decimal PriceDecimal { get; set; }
        public string Sku { get; set; }
        public virtual Category Category { get; set; }
    }
    public class Category : LookupEntity
    {
    }

    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
