using System.Collections.Generic;
using System.IO;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;

namespace Nop.Plugin.Widgets.TypeProducts
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class TypeProductsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public TypeProductsPlugin(IPictureService pictureService, 
            ISettingService settingService, IWebHelper webHelper)
        {
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "home_page_top" };
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "TypeProducts";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.TypeProducts.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "TypeProducts";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.TypeProducts.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }
        
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //pictures
            var sampleImagesPath = _webHelper.MapPath("~/Plugins/Widgets.TypeProducts/Content/nivoslider/sample-images/");


            //settings
            var settings = new TypeProductsSettings
            {
                NumberOfBestsellersOnHomepage = 4,
                NumberOfHomePageProductOnHomepage = 4,
                NumberOfNewProductOnHomepage = 4,
                ShowBestSellerProduct=true,
                ShowHomePageProduct=true,
                ShowNewProduct=true
            };
            _settingService.SaveSetting(settings);


            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture1", "Picture 1");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture2", "Picture 2");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture3", "Picture 3");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture4", "Picture 4");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture5", "Picture 5");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture.Hint", "Upload picture.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Text", "Comment");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Text.Hint", "Enter comment for picture. Leave empty if you don't want to display any text.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Link", "URL");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.TypeProducts.Link.Hint", "Enter URL. Leave empty if you don't want this picture to be clickable.");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<TypeProductsSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture1");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture2");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture3");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture4");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture5");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Picture.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Text");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Text.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Link");
            this.DeletePluginLocaleResource("Plugins.Widgets.TypeProducts.Link.Hint");
            
            base.Uninstall();
        }
    }
}
