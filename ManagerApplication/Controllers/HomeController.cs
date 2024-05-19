using ManagerApplication.Domain;
using ManagerApplication.Domain.Entities;
using ManagerApplication.Models;
using ManagerApplication.Mvc.Controllers;
using ManagerApplication.Mvc.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ManagerApplication.Controllers
{
    public class HomeController : ManagerBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ManagerAppContext _context;

        public HomeController(ILogger<HomeController> logger, ManagerAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SubmitOrder(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                // Создаем новый объект Order на основе данных из OrderViewModel
                var newOrder = new Order
                {
                    FullName = order.FullName,
                    Address = order.Address,
                    Email = order.Email,
                    PhoneNumber = order.PhoneNumber,
                    MealPlan = order.MealPlan,
                    Status = OrderStatus.Pending, // Устанавливаем статус заказа по умолчанию
                    CreationDate = DateTime.Now // Устанавливаем текущую дату и время создания заказа
                };

                // Добавляем новый заказ в контекст базы данных
                _context.Orders.Add(newOrder);

                // Сохраняем изменения в базе данных
                _context.SaveChanges();

                // Перенаправляем пользователя на главную страницу после успешного заказа
                return RedirectToAction("Index");
            }

            // Если модель недействительна, возвращаемся на главную страницу и отображаем форму с данными заказа для исправления ошибок
            return View("Index", order);
        }
    }
}
