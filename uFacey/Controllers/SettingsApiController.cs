using System.Web.Http;
using Umbraco.Core.Configuration;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace uFacey.Controllers
{
    [PluginController("uFacey")]
    public class SettingsApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public string GetUmbracoVersion()
        {
            return UmbracoVersion.Current.ToString();
        }
    }
}
