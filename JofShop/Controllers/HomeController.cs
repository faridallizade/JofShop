
using JofShop.DAL;
using JofShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JofShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbcontext _context;

        public HomeController(AppDbcontext context)
        {
            _context = context;
        }
         public async Task<IActionResult> Index()
        {
            List<Fruit> fruit = await _context.fruits.ToListAsync();
            return View(fruit);
        }

    }
}
