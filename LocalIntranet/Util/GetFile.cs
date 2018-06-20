using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Util
{
    public class GetFile : ActionResult
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Directorio { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            int ptoExt = FileName.IndexOf(".");
            string extension = FileName.Substring(ptoExt);
            string contType = "";

            switch (extension)
            {
                case ".pdf":
                    contType = "application/pdf";
                    break;
                case ".doc":
                    contType = "application/msword";
                    break;
                case ".docx":
                    contType = "application/vnd.ms-word.document.12";
                    break;
                // Plantilla .dot de WORD
                case ".dot":
                    contType = "application/msword";
                    break;
                case ".xls":
                    contType = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    contType = "application/vnd.ms-excel.12";
                    break;
                // Plantilla .pot de PowerPoint
                case ".pot":
                    contType = "application/x-mspowerpoint";
                    break;
                case ".jpeg":
                    contType = "image/jpeg";
                    break;
            }
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            //context.HttpContext.Response.ContentType = "application/vnd.ms-excel";
            //Valida si el ContenType esta vacio, para poder visualizar por defecto el programa que necesita.
            if (!contType.Equals(""))
            {
                context.HttpContext.Response.ContentType = "" + contType + "";
            }
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.WriteFile(Path);
        }
    }
}