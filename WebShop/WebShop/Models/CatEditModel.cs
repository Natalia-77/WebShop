using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CatEditModel
    {
        public long Id { get; set; }
        [Display(Name = "Кличка")]
        public string Name { get; set; }
        [Display(Name = "Дата народження")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Фото")]
        public string Image { get; set; }
        [Display(Name = "Роздрібна ціна за кота")]
        public decimal Price { get; set; }
    }
}
