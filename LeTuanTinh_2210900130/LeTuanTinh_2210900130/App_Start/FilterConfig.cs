using System.Web;
using System.Web.Mvc;

namespace LeTuanTinh_2210900130
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
