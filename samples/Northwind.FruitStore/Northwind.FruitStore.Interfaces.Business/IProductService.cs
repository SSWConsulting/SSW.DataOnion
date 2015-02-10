using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.FruitStore.DomainModel.Entities;

namespace Northwind.FruitStore.Interfaces.Business
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product Add(Product product);
        Product Update(Product product);
    }
}
