using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Identity;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //якщо введені всі валідні дані.
            if (ModelState.IsValid)
            {
                //пошук користувача.
                var user = await _userManager.FindByEmailAsync(model.Email);
                //якщо знайшли:
                if (user != null)
                {
                    //підтвердження введеного користувачем пароля і пошти.
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    //якщо все ок:
                    if (result.Succeeded)
                    {
                        //входимо в систему.
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Cats");
                    }
                }
            }
            ModelState.AddModelError("", "Немає такого користувача...");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Cats");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
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
                    Email = model.Email,
                    UserName=model.Email
                    
                };
                var role = new AppRole
                {
                    Name = "User"
                };
                //var rrole =  _roleManager.CreateAsync(role).Result;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user,"User");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Щось пішло не так.");
                }
            }
            return View(model);
        }
    }
}
