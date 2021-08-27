using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    #region Клас для пошуку.
    public class SearchCatModel
    {
        public string Name { get; set; }
    }
    #endregion

    #region Клас для пагінації.
    public class Pagination
    {
        public int CurrentPage { get; set; } = 1;//поточна сторінка,по дефолту починається з 1.
        public int Count { get; set; }//загальна кількість записів,яку буде розбито по сторінкам.
        public int ItemOnPage { get; set; } = 2;//кількість записів,що буде виведено на одну сторінку.
        public int TotalPages => (int)Math.Ceiling(Count/(double)ItemOnPage);//кількість сторінок пагінації.
        public bool EnablePrevious => CurrentPage > 1;//повертає true,якщо поточна стоніка більша 1.
        public bool EnableNext => CurrentPage < TotalPages;//повертає true ,якщо номер поточної сторінки менше максимального номера сторінок пагінації.
    }
    #endregion

    #region Клас буде містити дані про список елементів для виводу,дані,що отримані були під час пошуку і дані,що потрібні для пагінації
    public class CatsIndexModel
    {
       
        public List<CatVM> Cats { get; set; }
        public SearchCatModel Search { get; set; }

        public Pagination Pagination = new Pagination();//  { get; set; }
    }
    #endregion
}
