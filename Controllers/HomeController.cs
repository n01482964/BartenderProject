using BartenderProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BartenderProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		// Global static variables
		private static Queue<Order> _orders = new Queue<Order>();
		private static List<Drink> _menu = new List<Drink>
		{
			new Drink { Id = 1, Name = "Martini", Price = 8.50M },
			new Drink { Id = 2, Name = "Margarita", Price = 7.00M },
			new Drink { Id = 3, Name = "Old Fashioned", Price = 9.00M },
		};
		private static int _nextOrderId = 1;

		// Bartender dashboard to show current orders
		public IActionResult Bartender()
		{
			return View(_orders);
		}

		// Menu page for customers
		public IActionResult Menu()
		{
			return View(_menu);
		}

		// Place an order
		[HttpPost]
		public IActionResult PlaceOrder(int drinkId)
		{
			var drink = _menu.Find(d => d.Id == drinkId);
			if (drink != null)
			{
				_orders.Enqueue(new Order
				{
					orderId = _nextOrderId++,
					drinkName = drink.Name,
					isCompleted = false
				});
			}
			return RedirectToAction("Menu");
		}

		// Mark an order as completed
		public IActionResult CompleteOrder()
		{
			if (_orders.Count > 0)
			{
				var order = _orders.Dequeue();
				order.isCompleted = true;
				// Simulate handling the completed order
			}
			return RedirectToAction("Bartender");
		}

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
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
	}
}
