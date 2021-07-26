using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Products;

namespace WebShop.Models.MapAnimals
{
    public class AnimalProfile:Profile
    {
        public AnimalProfile()
        {
            CreateMap<Cat, CatVM>()
                .ForMember(bday => bday.Birthday, opt => opt.MapFrom(bday => bday.Birthday.ToString("dd MMMM yyyy",
                     CultureInfo.CreateSpecificCulture("uk"))));
        }
    }
}
