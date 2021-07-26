using AutoMapper;
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
        private readonly IMapper _mapper;
        public CatsController(AppEFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            //List<CatVM> model =
            //    _context.Cats.Select(x => new CatVM
            //    {
            //        Id = x.Id,
            //        Birthday = x.Birthday,
            //        Price=x.Price,
            //        Image = x.Image,
            //        Name = x.Name
            //    }).ToList();

            var model = _context.Cats.Select(x => _mapper.Map<CatVM>(x)).ToList();
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

       [HttpGet]
       //Повертає форму з даними об"єкта0який буде відредагований.
        public IActionResult Edit(long id)
        {
            var res = _context.Cats.FirstOrDefault(x => x.Id == id);
                   
                return View(new CatsValidationModel()
                {
                    //Id = res.Id,
                    Name = res.Name,
                    Price = res.Price,
                    BirthDay = res.Birthday,
                    Image = res.Image
                });         
                               

        }

        [HttpPost]
        //Отримує відредаговані дані у вигляді об"єкта CatsValidationModel.
        public IActionResult Edit(long id, CatsValidationModel cat)
        {

            if (ModelState.IsValid)
            {
                var res = _context.Cats.FirstOrDefault(x => x.Id == id);
                res.Name = cat.Name;
                res.Birthday = cat.BirthDay;
                res.Image = cat.Image;
                res.Price = cat.Price;
                _context.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        [ActionName("Delete")]

        public IActionResult DeleteV(long id)
        {
            var catToDelete = _context.Cats.FirstOrDefault(x => x.Id == id);
            if (catToDelete != null)
            {
                return View(catToDelete);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var catDel = _context.Cats.FirstOrDefault(x => x.Id == id);
            if(catDel!=null)
            {
                _context.Cats.Remove(catDel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           return NotFound();
        }
    }
}
