using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Data;
using Shop.Models;
using Shop.Models.ViewModels;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Products.Include(u => u.Category),
                Categories = _db.Categories
            };

            return View(homeVM);
        }

        //GET
        public IActionResult Details(int id)
        {
            List<Cart> shoppingCartsList = new List<Cart>();

            if (HttpContext.Session.Get<IEnumerable<Cart>>(ENV.SessinCart) != null &&
                HttpContext.Session.Get<IEnumerable<Cart>>(ENV.SessinCart).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<Cart>>(ENV.SessinCart);
            }

            DetailsVM detailsVM = new DetailsVM()
            {
                Product = _db.Products.Include(p => p.Category).Where(p => p.Id == id).FirstOrDefault(),
                InCart = false
            };

            foreach (var item in shoppingCartsList)
            {
                if (item.ProductId == id)
                {
                    detailsVM.InCart = true;
                }
            }

            return View(detailsVM);
        }

        [HttpPost]
        [ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<Cart> shoppingCartsList = new List<Cart>();

            if (HttpContext.Session.Get<IEnumerable<Cart>>(ENV.SessinCart) != null &&
                HttpContext.Session.Get<IEnumerable<Cart>>(ENV.SessinCart).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<Cart>>(ENV.SessinCart);
            }

            shoppingCartsList.Add(new Cart { ProductId = id });

            HttpContext.Session.Set(ENV.SessinCart, shoppingCartsList);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<Cart> shoppingCartsList = new List<Cart>();

            if (HttpContext.Session.Get<IEnumerable<Cart>>(ENV.SessinCart) != null &&
                HttpContext.Session.Get<IEnumerable<Cart>>(ENV.SessinCart).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<Cart>>(ENV.SessinCart);
            }

            var item = shoppingCartsList.SingleOrDefault(u => u.ProductId == id);

            if (item != null)
                shoppingCartsList.Remove(item);

            HttpContext.Session.Set(ENV.SessinCart, shoppingCartsList);

            return RedirectToAction(nameof(Index));
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
