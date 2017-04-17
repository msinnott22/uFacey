using System.Linq;
using System.Web.Hosting;
using System.Xml;
using Umbraco.Core;

namespace uFacey
{
    public class InstallHelpers
    {
        /// <summary>
        /// Adds the uFacey section to Umbraco
        /// </summary>
        /// <param name="applicationContext"></param>
        public void AddSection(ApplicationContext applicationContext)
        {
            var sectionService = applicationContext.Services.SectionService;
            var uFaceySection = sectionService.GetSections().SingleOrDefault(x => x.Alias == "uFacey");

            //Check if section exists
            if (uFaceySection == null)
            {
                //Doesn't Exits = Create it here
                sectionService.MakeNew("uFacey", "uFacey", "icon-smiley-inverted");
            }
        }

        /// <summary>
        /// Adds the required XML to the dashboard.config file
        /// </summary>
        public void AddSectionDashboard()
        {
            bool saveFile = false;

            var dashboardPath = "~/config/dashboard.config";

            var dashboardFilePath = HostingEnvironment.MapPath(dashboardPath);

            XmlDocument dashboardXml = new XmlDocument();
            dashboardXml.Load(dashboardFilePath);

            XmlNode findSection = dashboardXml.SelectSingleNode("//section [@alias='uFaceyDashboardSection']");

            if (findSection == null)
            {
                var xmlToAdd = "<section alias='uFaceyDashboardSection'>" +
                                    "<areas>" +
                                        "<area>uFacey</area>" +
                                    "</areas>" +
                                    "<tab caption='Last 7 Days'>" +
                                        "<control addPanel='true' panelCaption''>/App_Plugins/uFacey/backOffice/uFaceyTree/partials/dashboard.html</control>" +
                                    "</tab>" +
                                "</section>";

                XmlNode dashboardNode = dashboardXml.SelectSingleNode("//dashBoard");

                if (dashboardNode != null)
                {
                    XmlDocument xmlNodeToAdd = new XmlDocument();
                    xmlNodeToAdd.LoadXml(xmlToAdd);

                    var toAdd = xmlNodeToAdd.SelectSingleNode("");

                    dashboardNode.AppendChild(dashboardNode.OwnerDocument.ImportNode(toAdd, true));

                    saveFile = true;
                }
            }

            if (saveFile)
            {
                dashboardXml.Save(dashboardFilePath);
            }
        }
    }
}
