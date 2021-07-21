using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models.Validation
{
    public class CatsValidationModel
    {
        [Display(Name = "Кличка")]
        public string Name { get; set; }
        [Display(Name = "Дата народження")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Фото")]
        public string Image { get; set; }
        [Display(Name = "Роздрібна ціна за кота")]
        public decimal Price { get; set; }
    }

    public class CatValidator : AbstractValidator<CatsValidationModel>
    {
        
        public CatValidator()
        {
            
            RuleFor(x => x.Name).NotNull().WithMessage("Поле не може бути пустим");

            RuleFor(x => x.BirthDay).Must(BeValidDate).WithMessage("Дата народження не може бути більша поточної дати");

            RuleFor(x=>x.Price).NotNull()
                .WithMessage("Поле не може бути пустим")
                .InclusiveBetween(1, 1000)
                .WithMessage("Ціна повинна бути в діапазоні від 1 до 1000");
            
        }

        public bool BeValidDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int chooseYear = date.Year;

            if (chooseYear <= currentYear)
            {
                return true;
            }

            return false;
        }



    }


}
