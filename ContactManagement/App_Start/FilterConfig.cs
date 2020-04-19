using System.Web.Mvc;

namespace ContactManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); // global level error / exception handling
        }
    }
}
