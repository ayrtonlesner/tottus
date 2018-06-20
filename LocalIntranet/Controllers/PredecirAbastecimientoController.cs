using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Controllers
{
    public class PredecirAbastecimientoController : Controller
    {
        //
        // GET: /PredecirAbastecimiento/
        //public ActionResult PredecirAbastecimiento()
        //{
        //    return View();
        //}

        public ActionResult MantPredecirAbastecimiento()
        {
            ViewBag.fechActual = System.DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Title = "PREDECIR ABASTECIMIENTO";
            return View();
        }

        public ActionResult MantDashboard()
        {
            ViewBag.fechActual = System.DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Title = "DASHBOARD";
            return View();
        }
        

	}
}