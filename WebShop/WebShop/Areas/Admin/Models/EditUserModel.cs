using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Areas.Admin.Models
{
    public class EditUserModel
    {
        
        [Required(ErrorMessage = "Обов'язкове поле")]
        [EmailAddress(ErrorMessage = "Невалідна пошта")]
        public string Email { get; set; }

        
       
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string NameUser { get; set; }

        
        [Required(ErrorMessage = "Обовєязкове полу")]
        public string RoleUser { get; set; }


        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Image { get; set; }
    }
}
