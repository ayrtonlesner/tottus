using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Controllers
{
    public class ProyectarNecesidadTransporteController : Controller
    {
        //
        // GET: /ProyectarNecesidadTransporte/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MantProyectarNecesidadTransporte()
        {
            ViewBag.fechActual = System.DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Title = "PROYECTAR NECESIDAD DE TRANSPORTE";
            return View();
        }

	}
}