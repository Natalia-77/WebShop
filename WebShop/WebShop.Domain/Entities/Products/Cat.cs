using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Entities.Products
{
    public class Cat:BaseEntity<long>
    {
        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
    }
}
