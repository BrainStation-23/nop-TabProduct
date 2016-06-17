
using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.TypeProducts
{
    public class TypeProductsSettings : ISettings
    {

        public int NumberOfBestsellersOnHomepage { get; set; }

        public int NumberOfNewProductOnHomepage { get; set; }

        public int NumberOfHomePageProductOnHomepage { get; set; }

        public bool ShowBestSellerProduct { get; set; }

        public bool ShowNewProduct { get; set; }

        public bool ShowHomePageProduct { get; set; }

        public int CacheTime { get; set; }
    }
}