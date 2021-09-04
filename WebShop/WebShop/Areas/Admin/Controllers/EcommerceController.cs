//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Areas.Admin.Models;
using WebShop.Domain;
using WebShop.Domain.Entities.Identity;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EcommerceController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppEFContext _appEF;
        private IPasswordHasher<AppUser> _passwordHasher;

        public EcommerceController(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            AppEFContext appEF,
            IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appEF = appEF;
            _passwordHasher = passwordHasher;
        }

       
        public async Task <IActionResult> Orders()
        {
            #region Перший варіант виводу на сторінці всіх користувачів.
            //всі ролі.
            //var rol = _roleManager.Roles.ToList();           
            //var allusers = _userManager.Users.ToList();
            //var t = new List<AppRole>();

            //foreach (var rolea in rol)
            //{            
            //    var rolesa = await _userManager.GetUsersInRoleAsync(rolea.Name);              
            //    t.Add(rolea);             

            //}          

            //RolesViewModel model = new RolesViewModel();            

            //var roless = new List<string>();
            //var userss = new List<AppUser>();
            //for (int i = 0; i < t.Count; i++)
            //{
            //    var role = _appEF.Roles.SingleOrDefault(s => s.Name.Contains((t[i].Name)));
            //    var usersInRole = _appEF.Users.Where(m => m.UserRoles.Any(r => r.RoleId == role.Id)).ToList();
            //    roless.Add(role.Name);
            //    userss.AddRange(usersInRole);    

            //}
            //model.Name = userss;
            //model.Role = roless;  
            //return View(model);

            //=============До уваги не брати============================
            // var role = _appEF.Roles.SingleOrDefault(m => m.Name == "User");
            //var usersInRole = _appEF.Users.Where(m => m.UserRoles.Any(r => r.RoleId == ro.Id)).ToList();
            //===========================================

            #endregion

            #region Display all users with their roles.

            var usersWithRoles = (from user in _appEF.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.UserRoles
                                                   join role in _appEF.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new ViewUserRolesModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            return await Task.FromResult(View(usersWithRoles));

            #endregion
        }




        #region Delete

        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Orders", "Ecommerce", new { area = "Admin" }, null);
                else
                    ModelState.AddModelError("", "Не вдалося видалити");
            }
            else               
                ModelState.AddModelError("", "Користувач не знайдений");

            return View("Orders", _userManager.Users);
        }

        #endregion

        #region Add new user
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddUserByAdminModel addUser)
        {
            var user = await _userManager.FindByEmailAsync(addUser.Email);
            
            //строчка для файлу фото профіля.            
            string fileNameUser = string.Empty;

            //якщо фото обрано:
            if (addUser.Image != null)
            {
                //розширення
                var ext = Path.GetExtension(addUser.Image.FileName);
                //імя файла з розширенням.
                fileNameUser = Path.GetRandomFileName() + ext;
                //директорія,де знаходитиметься файл.
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                //повний шлях до фото.
                var filePath = Path.Combine(dir, fileNameUser);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await addUser.Image.CopyToAsync(stream);
                }
            }
           
            //якщо користувач вже існує,то виводимо попередження.
            if (user != null)
            {
                ModelState.AddModelError("Email", " Користувач з такими даними вже існує ");
            }
            //якщо введені валідні дані для реєстрації користувача:
            if (ModelState.IsValid)
            {
                user = new AppUser
                {
                    Email = addUser.Email,
                    UserName = addUser.Email,
                    ImageProfile = fileNameUser
                };
                var role = new AppRole
                {
                    Name = "User"
                };

                //створили користувача.
                var result = await _userManager.CreateAsync(user, addUser.Password);

                if (result.Succeeded)
                {
                    //додали роль користувачу.
                    await _userManager.AddToRoleAsync(user, "User");
                    //і перепаравили на головну сторінку з переліком користувачів.
                    return RedirectToAction("Orders", "Ecommerce", new { area = "Admin" }, null);
                }
                else
                {
                    ModelState.AddModelError("", "Помилка.");
                }
            }
            return View(addUser);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            EditUserModel editUser = new EditUserModel();

            var usersWithRoles = (from usera in _appEF.Users
                                  select new
                                  {
                                      UserId = usera.Id,
                                      Username = usera.UserName,
                                      Email = usera.Email,
                                      RoleNames = (from userRole in usera.UserRoles
                                                   join role in _appEF.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new ViewUserRolesModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            foreach (var item in usersWithRoles)
            {
                
                editUser.RoleUser = item.Role;
            }

            if (user != null)
            {
                editUser.NameUser = user.NormalizedUserName;
                editUser.Email = user.Email;
                editUser.Image = user.ImageProfile;

                return View(editUser);
            }
               
            else
                return RedirectToAction("Orders","Ecommerce", new { area = "admin" });
        }


        [HttpPost]        
        public async Task<IActionResult> Edit(string id, EditUserModel edit)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            
            if (user != null)
            {
                if (!string.IsNullOrEmpty(edit.Email))
                    user.Email = edit.Email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(edit.NameUser))
                    user.UserName = edit.NameUser;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (user.ImageProfile != null)
                    user.ImageProfile = edit.Image;
                else
                    ModelState.AddModelError("", "Photo cannot be empty");

                if (!string.IsNullOrEmpty(edit.Email)&& !string.IsNullOrEmpty(edit.NameUser))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Orders","Ecommerce",new { area="admin"});
                    else
                        ModelState.AddModelError("", "Error");
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");

            return View(user);

        }
        #endregion
    }
}
