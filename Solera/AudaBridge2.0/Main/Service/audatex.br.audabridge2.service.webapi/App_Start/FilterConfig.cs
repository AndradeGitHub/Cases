using System.Web;
using System.Web.Mvc;

namespace audatex.br.audabridge2.service.webapi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
