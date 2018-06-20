using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace LocalIntranet.Models.SecurityEntities
{
    [Serializable]
    public class MenuItem: ComplexObject
    {
        [Display(Name = "Controlador")]
        public String ControllerName { get; set; }
        [Display(Name = "Acción")]
        public String ActionName { get; set; }
        [Display(Name = "Descripción")]
        public String OptionName { get; set; }
        [Display(Name = "Ícono")]
        public String Icon { get; set; }
        [Display(Name = "Tipo")] /* opcion/carpeta/accion */
        public String OptionType { get; set; }
        [Display(Name = "Clave")]
        public String OptionId { get; set; }
        [Display(Name= "Nivel")]
        public Int32 OptionLevel { get; set; }
        [Display(Name = "Orden")]
        public Int32 OptionOrder { get; set; }
    }
}