using LocalIntranet.Filters;
using LocalIntranet.Models;
using LocalIntranet.Models.SecurityEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Controllers
{
    public class SecurityController : Controller
    {
        private SecurityContext _db = new SecurityContext();
      
        //
        // GET: /Security/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Security/ListSideMenu
        [AjaxException][AllowAnonymous]
        public ActionResult ListSideMenu(String userId)
        {
            //var menu = _db.listMenuItems(userId);
            //return PartialView(menu);
            return PartialView();
        }

        public ActionResult AsignarGrupos()
        {
            ViewBag.Title = "Grupos Acceso por Usuario";

            var grupos = _db.listGroups();

            List<SelectListItem> listaGrupos = new List<SelectListItem>(
                                                from g in grupos select new SelectListItem {
                                                    Text = g.UserName, Value = g.UserId.Trim(), Selected = false
                                                }
                                               );
            ViewBag.depNo = listaGrupos;
            return View();
        }

        // POST: /AsignarGruposRes
        [HttpPost] [AjaxException]
        [AllowLoggedUser] [ValidateAntiForgeryToken] [AjaxOnly]
        public ActionResult AsignarGruposRes(String c_codigogrupo)
        {
            var listUsuariosNoAsignados = _db.listUsersGroup(c_codigogrupo);
            return PartialView(listUsuariosNoAsignados);
        }

        // POST: /AsignarGruposRes
        [HttpPost] [AjaxException] [AllowLoggedUser]
        [ValidateAntiForgeryToken] [AjaxOnly]
        public ActionResult AsignarGruposAccion(String c_accion, String c_codigogrupo, String c_codigousuario)
        {
            String c_usuariomod = User.Identity.Name;
            _db.alterUsersGroup(c_accion, c_codigogrupo, c_codigousuario, c_usuariomod);
            return null;
        }


        
	}
}