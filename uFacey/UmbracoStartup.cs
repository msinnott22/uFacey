using System;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.packager;
using Umbraco.Core;
using Umbraco.Web.Trees;

namespace uFacey
{
    public class UmbracoStartup : ApplicationEventHandler
    {
        private const string AppSettingKey = "uFaceyStartupInstalled";

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            var installAppSetting = WebConfigurationManager.AppSettings[AppSettingKey];

            if (string.IsNullOrEmpty(installAppSetting) || installAppSetting != true.ToString())
            {
                var install = new InstallHelpers();

                install.AddSection(applicationContext);

                //TODO: Build Dashboard Section
                //install.AddSectionDashboard();

                var webConfig = WebConfigurationManager.OpenWebConfiguration("/");
                webConfig.AppSettings.Settings.Add(AppSettingKey, true.ToString());
                webConfig.Save();
            }

            InstalledPackage.BeforeDelete += InstalledPackage_BeforeDelete;

            //TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            //Get Current User
            var currentUser = User.GetCurrent();

            //This will only run on the uFaceyTree & if the user is NOT admin
            if (sender.TreeAlias == "uFaceyTree" && !currentUser.IsAdmin())
            {
                //setting node to remove
                var settingNode = e.Nodes.SingleOrDefault(x => x.Id.ToString() == "settings");

                //Ensure we found the node
                if (settingNode != null)
                {
                    //Remove the settings node from the collection
                    e.Nodes.Remove(settingNode);
                }
            }
        }



        /// <summary>
        /// Uninstall Package - Before Delete (Old style events, no V6/V7 equivelant)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InstalledPackage_BeforeDelete(InstalledPackage sender, EventArgs e)
        {
            //Check which package is being uninstalled
            if (sender.Data.Name == "uFacey")
            {
                var uninstall = new UninstallHelpers();

                //Start Uninstall - clean up process...
                uninstall.RemoveSection();
                //uninstall.RemoveSectionDashboard();

                //Remove AppSetting key when all done
                ConfigurationManager.AppSettings.Remove(AppSettingKey);
            }
        }
    }
}
