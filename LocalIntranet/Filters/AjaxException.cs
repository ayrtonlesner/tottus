using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Filters
{
    public class AjaxException : ActionFilterAttribute, IExceptionFilter
    {
        // En caso que la solicitud Ajax envie un error, retornar detalles de la excepcion como JSON
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest()) return;

            filterContext.Result = AjaxError(filterContext.Exception.Message, filterContext);

            // Hacer saber al sistema que la excepcion ha sido manejada
            filterContext.ExceptionHandled = true;

        }

        protected JsonResult AjaxError(string message, ExceptionContext filterContext)
        {
            message = "Hubo un error al procesar su solicitud, por favor intente de nuevo";

            if (filterContext.Exception.InnerException != null)
            {
                message = filterContext.Exception.InnerException.Message;
            }
            else
            {
                if (filterContext.Exception != null) message = filterContext.Exception.Message;
            }

            // Set the response status code to 500
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Se requiere para IIS 7.0
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            return new JsonResult
            {
                Data = new { ErrorMessage = message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}