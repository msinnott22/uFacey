using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Configuration;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace uFacey.Controllers
{
    [PluginController("uFacey")]
    public class SettingsApiController : UmbracoAuthorizedApiController
    {
        public string GetUmbracoVersion()
        {
            return UmbracoVersion.Current.ToString();
        }
    }
}
