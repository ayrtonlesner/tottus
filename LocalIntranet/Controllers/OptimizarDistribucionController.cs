using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Controllers
{
    public class OptimizarDistribucionController : Controller
    {
        //
        // GET: /OptimizarDistribucion/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MantOptimizarDistribucion()
        {
            ViewBag.fechActual = System.DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Title = "OPTIMIZAR DISTRIBUCIÓN DE MERCADERÍA";
            return View();
        }

	}
}