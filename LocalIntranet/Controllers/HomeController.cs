using LocalIntranet.Filters;
using LocalIntranet.Models;
using LocalIntranet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Controllers
{
    /* [Authorize()] */
    public class HomeController : Controller
    {
       
        [AllowLoggedUser]
        public ActionResult Index()
        {
             return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowLoggedUser]
        public ActionResult ManualesList(string path)
        {
            // Listado Manuales
            if (path == null) path = LocalIntranet.Models.GlobalVars.rutaFileManuales;
            //ViewBag.atras = GlobalVars.DownloadFileMan;
            ViewBag.Metodo = "ManualesList";
            ViewBag.titulo = "Lista de Manuales";
            ViewBag.Directorio = path;

            var FileList = FileUtil.listaContenidoFileSystem(path);

            return View(viewName: "FileList", model: FileList);
        }

        public GetFile DownloadFile(String File, String Directorio)
        {
            return new GetFile
            {
                FileName = File,
                Directorio = Directorio,
                Path = Directorio + "/" + File,
            };
        }






    }
}