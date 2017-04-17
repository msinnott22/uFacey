namespace uFacey.Helpers
{
    public static class uFaceyHelpers
    {
        public static string UmbracoVersion
        {
            get { return Umbraco.Core.Configuration.UmbracoVersion.Current.ToString(); }
        }
    }
}
