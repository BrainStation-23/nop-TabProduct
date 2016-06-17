using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.TypeProducts.Models
{
    public class ConfigurationModel : BaseNopModel
    {
     
        [NopResourceDisplayName("Plugins.Widgets.TypeProducts.NumberOfBestsellersOnHomepage")]
        [AllowHtml]
        public int NumberOfBestsellersOnHomepage { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.TypeProducts.NumberOfNewProductOnHomepage")]
        [AllowHtml]
        public int NumberOfNewProductOnHomepage { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.TypeProducts.NumberOfHomePageProductOnHomepage")]
        [AllowHtml]
        public int NumberOfHomePageProductOnHomepage { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.TypeProducts.ShowBestSellerProduct")]
        [AllowHtml]
        public bool ShowBestSellerProduct { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.TypeProducts.ShowNewProduct")]
        [AllowHtml]
        public bool ShowNewProduct { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.TypeProducts.ShowHomePageProduct")]
        [AllowHtml]
        public bool ShowHomePageProduct { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.TypeProducts.CacheTime")]
        [AllowHtml]
        public int CacheTime { get; set; }
    }
}