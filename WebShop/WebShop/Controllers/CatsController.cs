using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain;
using WebShop.Domain.Entities.Products;
using WebShop.Models;
using WebShop.Models.Validation;

namespace WebShop.Controllers
{
    public class CatsController : Controller
    {
        private readonly AppEFContext _context;
        public CatsController(AppEFContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CatVM> model =
                _context.Cats.Select(x => new CatVM
                {
                    Id = x.Id,
                    Birthday = x.Birthday,
                    Image = x.Image,
                    Name = x.Name
                }).ToList();

            return View(model);

            
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CatsValidationModel cats)
        {

            if (!ModelState.IsValid)
                return View(cats);

            Cat cat = new Cat
            {
                Name = cats.Name,
                Price=cats.Price,
                Birthday=cats.BirthDay,
                Image=cats.Image,
                DateCreate=DateTime.Now
            };

            _context.Cats.Add(cat);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
