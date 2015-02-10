using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.FruitStore.DomainModel.Entities;
using Northwind.FruitStore.Interfaces.Repositories;
using SSW.Data.Interfaces;

namespace Northwind.FruitStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {      
        private readonly IUnitOfWork _unitOfWork;
        private IProductRepository _productRepository;   

        public ProductsController(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public ActionResult Index()
        {
            var products = _productRepository.Get().OrderBy(p=>p.Name).ToList();
            return View(products);
        }

        public ActionResult Add(string id)
        {
            try
            {
                var product = new Product
                {
                    Name = id,
                    CreatedBy = "adamstephensen",
                    LastModifiedBy = "adamstephensen",
                };

                _productRepository.Add(product);
                _unitOfWork.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException deve)
            {
                IEnumerable<DbValidationError> errors = deve.EntityValidationErrors.SelectMany(c => c.ValidationErrors);
                
                var message = string.Join(",",errors.Select(c => c.ErrorMessage));

                return Content(message);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

            return RedirectToAction("Index");
        }

    }
}