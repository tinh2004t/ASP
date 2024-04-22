using System.Web;
using System.Web.Mvc;

namespace Lab03_3_LeTuanTinh_K22CNTT1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
