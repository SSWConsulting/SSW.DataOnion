using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.FruitStore.DomainModel.Entities;
using Northwind.FruitStore.Interfaces.Business;
using Northwind.FruitStore.Interfaces.Repositories;
using SSW.Data.Interfaces;

namespace Northwind.FruitStore.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Product> GetProducts()
        {
            return _productRepository.Get().OrderBy(p => p.Name).ToList();
        }

        public Product Add(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.SaveChanges();
            return product;
        }
    }

}
