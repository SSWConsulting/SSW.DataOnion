using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.FruitStore.DomainModel.Entities;
using Northwind.FruitStore.Interfaces.Repositories;
using SSW.Data.Interfaces;

namespace Northwind.FruitStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IProductRepository _productRepository;
        private readonly IOrderLineItemRepository _orderLineItemRepository;


        public OrderController(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository, IOrderLineItemRepository orderLineItemRepository
            )
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderLineItemRepository = orderLineItemRepository;
        }

        public ActionResult Index()
        {
            var list = _orderRepository.Get().ToList();

            return View(list);
        }

        

        public ActionResult Create()
        {
            using (_unitOfWork)
            {
                var order = new Order{};
                _orderRepository.Add(order);

                var product = new Product();
                _productRepository.Add(product);

                _unitOfWork.SaveChanges();
            }
            return Content("it worked");
        }
    }
}