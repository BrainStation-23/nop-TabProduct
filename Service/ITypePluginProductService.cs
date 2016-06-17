using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;

namespace Nop.Plugin.Widgets.TypeProducts.Service
{
    public partial interface ITypePluginProductService
    {
        IPagedList<Product> GetHomePageProductsDisplayedOnHomePage(int pageIndex = 0, int pageSize = Int32.MaxValue);

        IPagedList<Product> GetNewProductsDisplayedOnHomePage( IProductService productService , IStoreContext storeContext,int pageIndex = 0, int pageSize = Int32.MaxValue);
    }
}
