using System.Web.Hosting;
using System.Xml;
using Umbraco.Web;

namespace uFacey
{
    public class UninstallHelpers
    {
        public void RemoveSection()
        {
            //Get the Umbraco Service's Api's
            var services = UmbracoContext.Current.Application.Services;

            //Check to see if the section is still here (should be)
            var uFaceySection = services.SectionService.GetByAlias("uFacey");

            if (uFaceySection != null)
            {
                //Delete the section from the application
                services.SectionService.DeleteSection(uFaceySection);
            }
        }

        /// <summary>
        /// Removes the XML for the Section Dashboard from the XML file
        /// </summary>
        public void RemoveSectionDashboard()
        {
            bool saveFile = false;

            //Open up language file
            //umbraco/config/lang/en.xml
            var dashboardPath = "~/config/dashboard.config";

            //Path to the file resolved
            var dashboardFilePath = HostingEnvironment.MapPath(dashboardPath);

            //Load settings.config XML file
            XmlDocument dashboardXml = new XmlDocument();
            dashboardXml.Load(dashboardFilePath);

            // Dashboard Root Node
            // <dashboard>
            XmlNode dashboardNode = dashboardXml.SelectSingleNode("//dashboard");

            if (dashboardNode != null)
            {
                XmlNode findSectionKey = dashboardNode.SelectSingleNode("./section [@alias='uFaceyDashboardSection']");

                if (findSectionKey != null)
                {
                    //Let's remove the key from XML...
                    dashboardNode.RemoveChild(findSectionKey);

                    //Save the file flag to true
                    saveFile = true;
                }
            }

            //If saveFile flag is true then save the file
            if (saveFile)
            {
                //Save the XML file
                dashboardXml.Save(dashboardFilePath);
            }
        }
    }
}
