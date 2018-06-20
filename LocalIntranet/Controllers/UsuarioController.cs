using LocalIntranet.Filters;
using LocalIntranet.Models;
using LocalIntranet.Models.SecurityEntities;
using LocalIntranet.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;


namespace LocalIntranet.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioContext usuarioContext = new UsuarioContext();
        
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        //[AjaxException]
        [AllowLoggedUser]
        public JsonResult ObtenerUsuario(string query)
        {
            Usuario usuario = new Usuario();
            usuario.UserName = query;

            var mySource = usuarioContext.Cal_Ver_Usuarios(usuario);

            return new JsonResult
            {
                Data = mySource,
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult ObtenerUsuarioNAV(string accion, string query)
        {
            Usuario usuario = new Usuario();
            usuario.UserName = query;

            var mySource = usuarioContext.Cal_Ver_UsuariosNAV(accion, usuario);

            return new JsonResult
            {
                Data = mySource,
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

    }
}