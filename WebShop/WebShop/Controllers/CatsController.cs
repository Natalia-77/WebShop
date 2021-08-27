using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
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
        private IHostEnvironment _host;

        public CatsController(AppEFContext context, IMapper mapper, IHostEnvironment host)
        {
            _context = context;
            _mapper = mapper;
            _host = host;
        }
        public IActionResult Index(SearchCatModel searchCat,int currentPage=0)
        {
            CatsIndexModel model = new();
           
            //кількість записів на сторінці.
            model.Pagination.ItemOnPage = 2;
            var query = _context.Cats.AsQueryable();

            if(!string.IsNullOrEmpty(searchCat.Name))
            {
                query = query.Where(y => y.Name.Contains(searchCat.Name));
            }

            //var model = _context.Cats.Select(x => _mapper.Map<CatVM>(x)).ToList();

            //загальна кількість елементів.
            model.Pagination.Count = query.Count();
            //поточна сторінка.
            model.Pagination.CurrentPage = currentPage == 0 ? 1 : currentPage;
           
            query = query.Skip((model.Pagination.CurrentPage - 1) * model.Pagination.ItemOnPage).Take(model.Pagination.ItemOnPage).OrderBy(n=>n.Name);

            //список елементів,які будуть розбиті по сторінкам.
            model.Cats= query.Select(x => _mapper.Map<CatVM>(x)).ToList();
           
           
            model.Search = searchCat;

            if (model.Pagination.CurrentPage > model.Pagination.TotalPages)
            {
                model.Pagination.CurrentPage = model.Pagination.TotalPages;
            }
            return View(model);
            
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(CatsValidationModel cats)
        {

            if (!ModelState.IsValid)
                return View(cats);

            string fileName = string.Empty;

            if(cats.Image!=null)
            {
                var ext = Path.GetExtension(cats.Image.FileName);
                fileName = Path.GetRandomFileName() + ext;
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                var filePath = Path.Combine(dir, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await cats.Image.CopyToAsync(stream);
                }
            }

            Cat cat = new Cat
            {
                Name = cats.Name,
                Price=cats.Price,
                Birthday=cats.BirthDay,
                Image=fileName,
                DateCreate=DateTime.Now
            };


            _context.Cats.Add(cat);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

       [HttpGet]
       //Повертає форму з даними об"єкта,який буде відредагований.
        public IActionResult Edit(long id)
        {
            CatsValidationModel cat = new CatsValidationModel();
            var res = _context.Cats.FirstOrDefault(x => x.Id == id);
            if (res.Image != null)
            {
                //var ext = Path.GetExtension(res.Image);
                var nam = Path.GetFileName(res.Image);
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                var filePath = Path.Combine(dir, nam);
                using (var stream = System.IO.File.OpenRead($"{filePath}"))
                {
                    var res_image = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));                  

                    cat.Name = res.Name;
                    cat.Price = res.Price;
                    cat.BirthDay = res.Birthday;
                    cat.Image = res_image;                    
                }            
                               
            }

            return View(cat);
        }

        [HttpPost]
        //Отримує відредаговані дані у вигляді об"єкта CatsValidationModel.
        public async Task <IActionResult> Edit(long id, CatsValidationModel cat)
        {

            if (ModelState.IsValid)
            {
                var res = _context.Cats.FirstOrDefault(x => x.Id == id);
                res.Name = cat.Name;
                res.Birthday = cat.BirthDay;

                string fileName = string.Empty;

                if (cat.Image != null)
                {
                    var ext = Path.GetExtension(cat.Image.FileName);
                    fileName = Path.GetRandomFileName() + ext;
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                    var filePath = Path.Combine(dir, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await cat.Image.CopyToAsync(stream);
                    }
                    var oldImage = res.Image;
                    string fol = "\\images\\";
                    string contentRootPath = _host.ContentRootPath + fol + oldImage;

                    if (System.IO.File.Exists(contentRootPath))
                    {
                        System.IO.File.Delete(contentRootPath);
                    }
                    res.Image = fileName;
                }             

                res.Price = cat.Price;
                _context.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }


        //[HttpGet]
        //[ActionName("Delete")]

        //public IActionResult DeleteV(long id)
        //{
        //    var catToDelete = _context.Cats.FirstOrDefault(x => x.Id == id);
        //    if (catToDelete != null)
        //    {
        //        return View(catToDelete);
        //    }
        //    return NotFound();
        //}

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var catDel = _context.Cats.FirstOrDefault(x => x.Id == id);
            if(catDel!=null)
            {
                var name = catDel.Image;
               // var exta = Path.GetExtension(catDel.Image);
                string folder = "\\images\\";
                string contentRootPath = _host.ContentRootPath + folder + name;
               
                if (System.IO.File.Exists(contentRootPath))
                {
                    System.IO.File.Delete(contentRootPath);
                }

                _context.Cats.Remove(catDel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           return NotFound();
        }
    }
}
