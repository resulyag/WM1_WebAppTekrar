using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WM1_WebAppTekrar.Models;
using WM1_WebAppTekrar.ViewModels;

namespace WM1_WebAppTekrar.Controllers
{
    public class ProductController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }
        private int _pageSize = 10;
        public IActionResult Index(int? page = 1)
        {
            var model = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .OrderBy(x => x.Category.CategoryName)
                .ThenBy(x => x.ProductName)
                .Skip((page.GetValueOrDefault() - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();
            ViewBag.Page = page.GetValueOrDefault(1);
            ViewBag.Limit = (int)Math.Ceiling(_context.Products.Count() / (double)_pageSize);
            return View(model);
        }
        public IActionResult Detail(int? id)
        {
            var data = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .FirstOrDefault(x => x.ProductId == id);
            if (data==null)
            {
                return RedirectToAction("Index");
            }
            var model = new ProductViewModel
            {
                ProductId = data.ProductId,
                CategoryName = data.Category?.CategoryName,
                CategoryId = data.CategoryId,
                CompanyName = data.Supplier?.CompanyName,
                ProductName = data.ProductName,
                UnitPrice = data.UnitPrice,
                SupplierId = data.SupplierId
            };

            return View(model);
        }
    }
}
