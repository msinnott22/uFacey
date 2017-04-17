using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace uFacey.Controllers
{
    using System.Web.Hosting;
    using System.Xml;
    using Umbraco.Core;

    [umbraco.businesslogic.Tree("uFacey", "uFaceyTree", "uFacey")]
    [PluginController("uFacey")]
    public class uFaceyTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            if (id == Constants.System.Root.ToInvariantString())
            {

                var nodes = new TreeNodeCollection();

                var nodeToAdd = CreateTreeNode("settings", null, queryStrings, "Settings", "icon-settings", false,
                    "/uFacey/uFaceyTree/edit/settings");

                nodes.Add(nodeToAdd);

                return nodes;
            }

            throw new NotSupportedException();
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            //If the node is the root node (top of tree)
            if (id == Constants.System.Root.ToInvariantString())
            {
                //Add in refresh action
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), false);
            }

            return menu;
        }
    }
}
