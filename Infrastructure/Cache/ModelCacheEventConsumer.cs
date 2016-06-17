using Nop.Core.Caching;
using Nop.Core.Domain.Configuration;
using Nop.Core.Events;
using Nop.Core.Infrastructure;
using Nop.Services.Events;

namespace Nop.Plugin.Widgets.TypeProducts.Infrastructure.Cache
{
    /// <summary>
    /// Models cache event consumer (used for caching of presentation layer models)
    /// </summary>
    public partial class ModelCacheEventConsumer: 
        IConsumer<EntityInserted<Setting>>,
        IConsumer<EntityUpdated<Setting>>,
        IConsumer<EntityDeleted<Setting>>
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} :paging id
        /// {1}: store id
        /// </remarks>
        public const string Pattern = "Nop.plugins.widgets.typeProducts";
        public const string HomePageProduct = "Nop.plugins.widgets.typeProducts.homepage-{0}-{1}";
        public const string BestSellerProduct = "Nop.plugins.widgets.typeProducts.bestseller-{0}-{1}";
        public const string NewProduct = "Nop.plugins.widgets.typeProducts.newproduct-{0}-{1}";

        private readonly ICacheManager _cacheManager;

        public ModelCacheEventConsumer()
        {
            //TODO inject static cache manager using constructor
            this._cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");
        }

        public void HandleEvent(EntityInserted<Setting> eventMessage)
        {
            _cacheManager.RemoveByPattern(Pattern);
        }
        public void HandleEvent(EntityUpdated<Setting> eventMessage)
        {
            _cacheManager.RemoveByPattern(Pattern);
        }
        public void HandleEvent(EntityDeleted<Setting> eventMessage)
        {
            _cacheManager.RemoveByPattern(Pattern);
        }
    }
}
