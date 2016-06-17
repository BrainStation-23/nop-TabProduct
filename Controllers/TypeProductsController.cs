using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Web.Extensions;
using Nop.Plugin.Widgets.TypeProducts.Infrastructure.Cache;
using Nop.Plugin.Widgets.TypeProducts.Models;
using Nop.Plugin.Widgets.TypeProducts.Service;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Framework.Controllers;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.TypeProducts.Controllers
{
    public class TypeProductsController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IPermissionService _permissionService;
        private readonly ITaxService _taxService;
        private readonly ICurrencyService _currencyService;
        private readonly IWebHelper _webHelper;
        private readonly CatalogSettings _catalogSettings;
        private readonly MediaSettings _mediaSettings;
        private readonly IOrderReportService _orderReportService;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ITypePluginProductService _typePluginProductService;

        public TypeProductsController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            ICategoryService categoryService,
            IProductService productService,
            ISpecificationAttributeService specificationAttributeService,
            IPriceCalculationService priceCalculationService,
            IPriceFormatter priceFormatter,
            IPermissionService permissionService,
            ITaxService taxService,
            ICurrencyService currencyService,
            IWebHelper webHelper,
            CatalogSettings catalogSettings,
            MediaSettings mediaSettings,
            IOrderReportService orderReportService,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            ITypePluginProductService typePluginProductService)
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._cacheManager = cacheManager;
            this._localizationService = localizationService;
            this._categoryService = categoryService;
            this._productService = productService;
            this._categoryService = categoryService;
            this._specificationAttributeService = specificationAttributeService;
            this._priceCalculationService = priceCalculationService;
            this._priceFormatter = priceFormatter;
            this._permissionService = permissionService;
            this._taxService = taxService;
            this._currencyService = currencyService;
            this._webHelper = webHelper;
            this._catalogSettings = catalogSettings;
            this._mediaSettings = mediaSettings;
            this._orderReportService = orderReportService;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._typePluginProductService = typePluginProductService;
        }

        #region Utility
        [NonAction]
        protected virtual IEnumerable<ProductOverviewModel> PrepareProductOverviewModels(IEnumerable<Product> products,
            bool preparePriceModel = true, bool preparePictureModel = true,
            int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
            bool forceRedirectionAfterAddingToCart = false)
        {
            return this.PrepareProductOverviewModels(_workContext,
                _storeContext, _categoryService, _productService, _specificationAttributeService,
                _priceCalculationService, _priceFormatter, _permissionService,
                _localizationService, _taxService, _currencyService,
                _pictureService, _webHelper, _cacheManager,
                _catalogSettings, _mediaSettings, products,
                preparePriceModel, preparePictureModel,
                productThumbPictureSize, prepareSpecificationAttributes,
                forceRedirectionAfterAddingToCart);
        }

        protected virtual TypeProductsSettings GetTypeProductsSettings()
        {
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            return _settingService.LoadSetting<TypeProductsSettings>(storeScope);
        }
        #endregion

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings for a chosen store scope
            var settings = GetTypeProductsSettings();
            var model = new ConfigurationModel();
            model.NumberOfBestsellersOnHomepage = settings.NumberOfBestsellersOnHomepage;
            model.NumberOfHomePageProductOnHomepage = settings.NumberOfHomePageProductOnHomepage;
            model.NumberOfNewProductOnHomepage = settings.NumberOfNewProductOnHomepage;
            model.ShowNewProduct = settings.ShowNewProduct;
            model.ShowHomePageProduct = settings.ShowHomePageProduct;
            model.ShowBestSellerProduct = settings.ShowBestSellerProduct;
            model.CacheTime = settings.CacheTime;
            return View("~/Plugins/Widgets.TypeProducts/Views/TypeProducts/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            //load settings for a chosen store scope
            var settings = GetTypeProductsSettings();
            settings.NumberOfBestsellersOnHomepage= model.NumberOfBestsellersOnHomepage ;
            settings.NumberOfHomePageProductOnHomepage = model.NumberOfHomePageProductOnHomepage;
            settings.NumberOfNewProductOnHomepage = model.NumberOfNewProductOnHomepage;
            settings.ShowNewProduct = model.ShowNewProduct;
            settings.ShowHomePageProduct = model.ShowHomePageProduct;
            settings.ShowBestSellerProduct = model.ShowBestSellerProduct;
            settings.CacheTime = model.CacheTime;

            _settingService.SaveSetting(settings, x => x.NumberOfHomePageProductOnHomepage, storeScope, true);
            _settingService.SaveSetting(settings, x => x.NumberOfBestsellersOnHomepage, storeScope, true);
            _settingService.SaveSetting(settings, x => x.NumberOfNewProductOnHomepage, storeScope, true);
            _settingService.SaveSetting(settings, x => x.ShowBestSellerProduct, storeScope, true);
            _settingService.SaveSetting(settings, x => x.ShowHomePageProduct, storeScope, true);
            _settingService.SaveSetting(settings, x => x.ShowNewProduct, storeScope, true);
            _settingService.SaveSetting(settings, x => x.CacheTime, storeScope, true);
            //now clear settings cache
            _settingService.ClearCache();
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            HomePageProductInitModel model = new HomePageProductInitModel();
            var settings = GetTypeProductsSettings();

            if(settings.ShowHomePageProduct)
            {
                var productsHomePage = _cacheManager.Get(string.Format(ModelCacheEventConsumer.HomePageProduct, 0, _storeContext.CurrentStore.Id),settings.CacheTime,
               () =>
                   _typePluginProductService.GetHomePageProductsDisplayedOnHomePage(0, settings.NumberOfHomePageProductOnHomepage));
                model.HomePageProductPageCount = productsHomePage.TotalPages;
                //ACL and store mapping
                var productList = productsHomePage.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
                //availability dates
                productList = productList.Where(p => p.IsAvailable()).ToList();

                model.HomePageProduct = PrepareProductOverviewModels(productList, true, true, null).ToList();
            }


            if (settings.ShowBestSellerProduct)
            {
                var report = _cacheManager.Get(string.Format(ModelCacheEventConsumer.BestSellerProduct, 0, _storeContext.CurrentStore.Id), settings.CacheTime,
               () =>
                   _orderReportService.BestSellersReport(storeId: _storeContext.CurrentStore.Id,
                   pageSize: settings.NumberOfBestsellersOnHomepage,
                   pageIndex: 0));

                model.BestSellerProductPageCount = report.TotalPages;
                //load products
                var productsBestSeller = _productService.GetProductsByIds(report.Select(x => x.ProductId).ToArray());
                //ACL and store mapping
                productsBestSeller = productsBestSeller.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
                //availability dates
                productsBestSeller = productsBestSeller.Where(p => p.IsAvailable()).ToList();

                model.BestSellerProduct = PrepareProductOverviewModels(productsBestSeller, true, true, null).ToList();
            }
            
            if(settings.ShowNewProduct)
            {
                var productsNewProduct = _cacheManager.Get(string.Format(ModelCacheEventConsumer.NewProduct, 0, _storeContext.CurrentStore.Id), settings.CacheTime,
               () =>
                   _typePluginProductService.GetNewProductsDisplayedOnHomePage(_productService, _storeContext, 0, settings.NumberOfHomePageProductOnHomepage));
                model.NewProductProductPageCount = productsNewProduct.TotalPages;
                model.NewProduct = PrepareProductOverviewModels(productsNewProduct, true, true, null).ToList();
            }
            

            return View("~/Plugins/Widgets.TypeProducts/Views/TypeProducts/PublicInfo.cshtml",model);
        }


        [HttpPost]
        public ActionResult HomePageProductsPaging(int pageIndex)
        {
            var settings = GetTypeProductsSettings();
            var products = _cacheManager.Get(string.Format(ModelCacheEventConsumer.HomePageProduct, pageIndex, _storeContext.CurrentStore.Id), settings.CacheTime,
                () =>
                    _typePluginProductService.GetHomePageProductsDisplayedOnHomePage(pageIndex, settings.NumberOfHomePageProductOnHomepage));
            var totalPage = products.TotalPages;
            //ACL and store mapping
            var productList = products.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
            //availability dates
            productList = productList.Where(p => p.IsAvailable()).ToList();

            var model = PrepareProductOverviewModels(productList, true, true, null).ToList();
            return Json(new
            {
                html = products.Count != 0 ? this.RenderPartialViewToString("~/Plugins/Widgets.TypeProducts/Views/TypeProducts/Products.cshtml", model) : "",
                pageCount = totalPage
            });
        }

        [HttpPost]
        public ActionResult HomepageBestSellersPaging(int pageIndex)
        {

            var settings = GetTypeProductsSettings();
            //load and cache report
            var report = _cacheManager.Get(string.Format(ModelCacheEventConsumer.BestSellerProduct, pageIndex, _storeContext.CurrentStore.Id), settings.CacheTime,
                () =>
                    _orderReportService.BestSellersReport(storeId: _storeContext.CurrentStore.Id,
                    pageSize: settings.NumberOfBestsellersOnHomepage,
                    pageIndex: pageIndex));

            var totalPage = report.TotalPages;
            //load products
            var products = _productService.GetProductsByIds(report.Select(x => x.ProductId).ToArray());
            //ACL and store mapping
            products = products.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
            //availability dates
            products = products.Where(p => p.IsAvailable()).ToList();

            var model = PrepareProductOverviewModels(products, true, true, null).ToList();
            return Json(new
            {
                html = products.Count != 0 ? this.RenderPartialViewToString("~/Plugins/Widgets.TypeProducts/Views/TypeProducts/Products.cshtml", model) : "",
                pageCount = totalPage
            });
        }

        [HttpPost]
        public ActionResult NewProductsOnHomePagePaging(int pageIndex)
        {

            var settings = GetTypeProductsSettings();
            //load and cache 
            var products = _cacheManager.Get(string.Format(ModelCacheEventConsumer.NewProduct, pageIndex, _storeContext.CurrentStore.Id), settings.CacheTime,
                () =>
                    _typePluginProductService.GetNewProductsDisplayedOnHomePage(_productService, _storeContext, pageIndex, settings.NumberOfHomePageProductOnHomepage));
            var totalPage = products.TotalPages;
            var model = PrepareProductOverviewModels(products, true, true, null).ToList();
            return Json(new
            {
                html = products.Count != 0 ? this.RenderPartialViewToString("~/Plugins/Widgets.TypeProducts/Views/TypeProducts/Products.cshtml", model) : "",
                pageCount = totalPage
            });
        }


    }
}