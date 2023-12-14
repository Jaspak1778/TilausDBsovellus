using System.Web;
using System.Web.Mvc;

namespace MVC_TKsovellus_1001
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
