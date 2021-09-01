using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Areas.Admin.Models;
using WebShop.Domain.Entities.Identity;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EcommerceController : Controller
    {
        private UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public EcommerceController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> OrdersAsync()
        {           

            return View( _userManager.Users);
        }
    }
}
