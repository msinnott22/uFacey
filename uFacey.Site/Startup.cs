using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using umbraco;
using umbraco.BusinessLogic;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Web.Security.Providers;

namespace uFacey.Site
{
    public class Startup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            var resetAdminPassword = WebConfigurationManager.AppSettings["ResetAdminPassword"];

            if (resetAdminPassword == true.ToString())
            {
                var userService = applicationContext.Services.UserService;

                var adminUser = userService.GetUserById(0);

                if (adminUser != null)
                {
                    adminUser.IsLockedOut = false;
                    adminUser.IsApproved = true;
                    
                    userService.Save(adminUser);

                    //userService.SavePassword(adminUser, "Admin1234!");
                }
            }
        }
    }
}