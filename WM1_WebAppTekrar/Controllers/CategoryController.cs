using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WM1_WebAppTekrar.Models;

namespace WM1_WebAppTekrar.Controllers
{
    public class CategoryController : Controller
    {
        private readonly NorthwindContext _context;

        public CategoryController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Categories.ToList();
            return View(data);
        }
        public IActionResult CategoryAdd()
        {
            return View();
        }
    }
}
