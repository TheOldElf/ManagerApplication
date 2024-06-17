using ManagerApplication.Domain;
using ManagerApplication.Domain.Entities;
using ManagerApplication.Mvc.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManagerApplication.Mvc.Controllers
{
    [Authorize]
    public class OrdersController : ManagerBaseController
    {
        private readonly ManagerAppContext _context;

        public OrdersController(ManagerAppContext context)
        {
            _context = context;
        }

        public IActionResult Index(string sortBy)
        {
            IQueryable<Order> orders = _context.Orders;

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "DateAsc":
                        orders = orders.OrderBy(o => o.CreationDate);
                        break;
                    case "DateDesc":
                        orders = orders.OrderByDescending(o => o.CreationDate);
                        break;
                    case "Keto":
                        orders = orders.Where(o => o.MealPlan == "Keto");
                        break;
                    case "LactoseFree":
                        orders = orders.Where(o => o.MealPlan == "Lactose-Free");
                        break;
                    case "HighCarb":
                        orders = orders.Where(o => o.MealPlan == "High Carb");
                        break;
                    case "Pending":
                        orders = orders.Where(o => o.Status == OrderStatus.Pending);
                        break;
                    case "InProgress":
                        orders = orders.Where(o => o.Status == OrderStatus.InProgress);
                        break;
                    case "Completed":
                        orders = orders.Where(o => o.Status == OrderStatus.Completed);
                        break;
                    case "Rejected":
                        orders = orders.Where(o => o.Status == OrderStatus.Rejected);
                        break;
                }
            }

            var orderedOrders = orders.ToList();
            return View(orderedOrders);
        }


        public IActionResult Create()
        {
            return PartialView("_Order");
        }

        [HttpPost]
        public IActionResult SubmitOrder(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var newOrder = new Order
                {
                    FullName = order.FullName,
                    Address = order.Address,
                    Email = order.Email,
                    PhoneNumber = order.PhoneNumber,
                    MealPlan = order.MealPlan,
                    Status = OrderStatus.Pending,
                    CreationDate = DateTime.Now
                };

                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Index", _context.Orders.ToList());
        }

        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, OrderStatus status)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}


