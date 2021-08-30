using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebShop.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Електронна адреса")]
        [Required(ErrorMessage = "Вкажіть пошту")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        public string Password { get; set; }
    }

    public class RegViewModel
    {
        [Display(Name = "Електронна адреса")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        [EmailAddress(ErrorMessage = "Невалідна пошта")]
        public string Email { get; set; }       

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Невалідний пароль")]
        public string Password { get; set; }

        [Display(Name = "Фото профіля")]
        [Required(ErrorMessage = "Обов'язкове поле")]        
        public IFormFile Image { get; set; }

    }

    //модель для відображення у компоненті.
    public class UserNavbarInfoViewModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}


