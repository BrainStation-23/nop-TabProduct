using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.TypeProducts.Models
{
    public class HomePageProductInitModel
    {
        public HomePageProductInitModel()
        {
            HomePageProduct = BestSellerProduct = NewProduct = new List<ProductOverviewModel>();
        }
        public IList<ProductOverviewModel> HomePageProduct { get; set; }
        public IList<ProductOverviewModel> BestSellerProduct { get; set; }
        public IList<ProductOverviewModel> NewProduct { get; set; }

        public int HomePageProductPageCount { get; set; }
        public int BestSellerProductPageCount { get; set; }
        public int NewProductProductPageCount { get; set; }

    }
}
