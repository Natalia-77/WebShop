using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Identity;

namespace WebShop.Areas.Admin.Models
{
    #region Для першого варіанта відображення списку користувачів з ролями.
    //public class RolesViewModel
    //{
    //    public List<AppUser> Name { get; set; }
    //    public List<string> Role { get; set; }
    //   // public List <AppUser> usersa{ get; set; }
    //}   
    #endregion

    public class ViewUserRolesModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
