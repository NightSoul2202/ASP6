using ASP6.Models;
using ASP6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ASP6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!_orderService.ValidateUserAge(user.Age))
            {
                return Content("Вам повинно бути більше 16 років для замовлення.");
            }

            TempData["User"] = JsonConvert.SerializeObject(user);

            return RedirectToAction("OrderQuantity");
        }

        public IActionResult OrderQuantity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                ModelState.AddModelError("", "Кількість товарів має бути додатною.");
                return View();
            }

            TempData["Quantity"] = quantity;
            return RedirectToAction("OrderForm");
        }

        public IActionResult OrderForm()
        {
            int quantity = (int)TempData["Quantity"];
            var products = _orderService.InitializeOrderForms(quantity);

            return View(products);
        }

        public IActionResult SubmitOrder(List<Product> products)
        {
            return View("OrderSummary", products);
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
    }
}
