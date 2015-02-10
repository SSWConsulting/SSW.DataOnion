using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.FruitStore.DomainModel.Entities;
using Northwind.FruitStore.Interfaces.Business;
using Northwind.FruitStore.Interfaces.Repositories;
using SSW.Data.Interfaces;

namespace Northwind.FruitStore.WebUI.Controllers
{
    public class ProductServicesController : Controller
    {      
        
        private readonly IProductService _productService;
        
        public ProductServicesController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var products = _productService.GetProducts();
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

                _productService.Add(product);
                
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return RedirectToAction("Index");
        }

    }
}