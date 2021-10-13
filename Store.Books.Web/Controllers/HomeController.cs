using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Web.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreService _service;
        public HomeController(
            IStoreService service,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _service.GetBooks();
            ViewBag.Authors = books.Any() ? await _service.GetAuthors() : null;
            return View(books);
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
