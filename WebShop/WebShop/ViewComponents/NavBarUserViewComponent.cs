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
    //[Authorize]
    public class NavBarUserViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private IHostEnvironment _host;
        public NavBarUserViewComponent(UserManager<AppUser> userManager, IHostEnvironment host)
        {
            _userManager = userManager;
            _host = host;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var name = user.ImageProfile;
            string folder = "\\userImages\\";
            string contentRootPath = _host.ContentRootPath + folder + name;

            UserNavbarInfoViewModel model = new UserNavbarInfoViewModel
                {

                    Name = user.UserName,
                    Image = name
            };
            
            return View("_UserNavbarInfo", model);



        }

    }
}
