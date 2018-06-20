using LocalIntranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LocalIntranet.Filters
{
    public class AuthorizeSecurity : AuthorizeAttribute
    {
        private bool _skipAuthorization = false;
        private bool _allowLoggedUser = false;
        private string _controllerName = "";
        private string _actionName = "";

        private SecurityContext dbSeguridad = new SecurityContext();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // ¿El método o el controlador posee el atributo AllowAnonymous?
            _skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            // ¿El método o el controlador posee el atributo AllowLoggedUser?
            _allowLoggedUser = filterContext.ActionDescriptor.IsDefined(typeof(AllowLoggedUser), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowLoggedUser), inherit: true);

            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAccessAllowed = base.AuthorizeCore(httpContext);

            // Si no pasa la prueba inicial de autenticación (usuario logueado), ya falló
            if (!isAccessAllowed) return isAccessAllowed;

            // Si el método NO tenía el acceso [AllowAnonymous] ni tampoco [AllowLoggedUser] proceder con la logica de autorización
            if (!_skipAuthorization && !_allowLoggedUser)
            {
                var routeData = httpContext.Request.RequestContext.RouteData;

                _controllerName = routeData.GetRequiredString("controller");
                _actionName = routeData.GetRequiredString("action");

                string _user = httpContext.User.Identity.Name;

                isAccessAllowed = dbSeguridad.isAccessGranted(_controllerName, _actionName, _user);
            }

            return isAccessAllowed;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            // Si el usuario no ha sido autenticado, sigue con el flujo normal
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(context);
            }
            else // Si el usuario ya esta autenticado y llegó hasta aquí (Handle Unauthorized) quiere decir que no tiene acceso puntual
            {
                if (!context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new ViewResult { ViewName = "Forbidden" };
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
                else
                {
                    context.Result = new JsonResult
                    {
                        Data = new { ErrorMessage = "Permisos Insuficientes, solicitar acceso" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                
            }
            
        }
    }
}