using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebShop.Domain.Entities.Identity;
using WebShop.Models;

namespace WebShop.ViewComponents
{
    [Authorize]
    public class NavBarUserViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
       
        public NavBarUserViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
           
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserNavbarInfoViewModel model = new UserNavbarInfoViewModel
            {
                Name = user.UserName,
                Image = user.ImageProfile
            };           
                     
            return View("_UserNavbarInfo", model);
        }

    }
}
