using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class SearchCatModel
    {
        public string Name { get; set; }
    }

    #region Model which contains data searching
    public class CatsIndexModel
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public List<CatVM> Cats { get; set; }
        public SearchCatModel Search { get; set; }

    }
    #endregion
}
