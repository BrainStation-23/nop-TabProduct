using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;

namespace Nop.Plugin.Widgets.TypeProducts.Service
{
    public class TypePluginProductService : ITypePluginProductService
    {
        private readonly IRepository<Product> _productRepository;
        public TypePluginProductService(IRepository<Product> productRepository)
        {
            this._productRepository = productRepository;
        }
        public IPagedList<Product> GetHomePageProductsDisplayedOnHomePage(int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            var query = from p in _productRepository.Table
                        orderby p.DisplayOrder, p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnHomePage 
                        select p;
            query = query.OrderBy(x => x.Id);
            var products = new PagedList<Product>(query, pageIndex, pageSize);
            return products;
        }




        public IPagedList<Product> GetNewProductsDisplayedOnHomePage(IProductService productService , IStoreContext storeContext ,int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            var products = productService.SearchProducts(
                storeId: storeContext.CurrentStore.Id,
                visibleIndividuallyOnly: true,
                markedAsNewOnly: true,
                orderBy: ProductSortingEnum.CreatedOn,
                pageSize: pageSize,
                pageIndex: pageIndex);
            return products;
        }
    }
}
