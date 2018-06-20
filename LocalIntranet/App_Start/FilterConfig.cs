using System.Web;
using System.Web.Mvc;

namespace LocalIntranet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //// old: Todo el contenido requiere autenticación y autorización (a menos que la acción permita anonimo)
            filters.Add(new LocalIntranet.Filters.AuthorizeSecurity());

            //// old: Todo el contenido requiere autenticación (a menos que la acción permita anonimo)
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute());

            ///* Todo el contenido debe ser pasado por SSL */
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
