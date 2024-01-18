using JofShop.Areas.ViewModels;
using JofShop.DAL;
using JofShop.Helpers;
using JofShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;

namespace JofShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FruitController : Controller
    {
        private readonly AppDbcontext _context;
        private readonly IWebHostEnvironment _envy;

        public FruitController(AppDbcontext context, IWebHostEnvironment envy)
        {
            _context = context;
            _envy = envy;
        }

        public async Task<IActionResult> Index()
        {
            List<Fruit> fruits = await _context.fruits.ToListAsync();
            return View(fruits);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin,Moderator")]
        public async Task<IActionResult> Create(CreateFruitVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!vm.ImageFile.CheckImage())
            {
                ModelState.AddModelError("Image", "Please upload Image file that size less than 3 Mb.");
                return View();
            }
            Fruit fruit = new Fruit()
            {
                Name = vm.Name,
                CategoryName = vm.CategoryName,
                ImgUrl = vm.ImageFile.Upload(_envy.WebRootPath, @"/Upload/"),
            };
            await _context.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(int id)
        {
            Fruit fruit = await _context.fruits.Where(c=>c.Id == id).FirstOrDefaultAsync();
            if(fruit == null)
            {
                return View();
            }
            UpdateFruitVm vm = new UpdateFruitVm()
            {
                Id = fruit.Id,
                Name = fruit.Name,
                CategoryName = fruit.CategoryName,
                ImgUrl = fruit.ImgUrl

            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(UpdateFruitVm vm)
        {
            Fruit exist = await _context.fruits.Where(c=>c.Id == vm.Id).FirstOrDefaultAsync();
            if(exist == null) { return View(); }
            if(!ModelState.IsValid) { return View(); }
            if (!vm.ImageFile.CheckImage())
            {
                ModelState.AddModelError("Image", "Please upload Image file that size less than 3 Mb.");
                return View();
            }
            exist.Name = vm.Name;
            exist.CategoryName = vm.CategoryName;
            exist.ImgUrl = vm.ImageFile.Upload(_envy.WebRootPath, @"/Upload/");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Fruit fruit = await _context.fruits.Where(c=>c.Id == id).FirstOrDefaultAsync();
            _context.fruits.Remove(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
