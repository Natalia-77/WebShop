using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class CatVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public String BirthDay { get; set; }
        public string Image { get; set; }
    }
}
