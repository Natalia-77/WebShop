using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Areas.Admin.Models
{
    public class AddUserByAdminModel
    {
        [Display(Name = "Електронна адреса")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        [EmailAddress(ErrorMessage = "Невалідна пошта")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Невалідний пароль")]
        public string Password { get; set; }

        //[Display(Name = "Фото профіля")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public IFormFile Image { get; set; }
    }
}
