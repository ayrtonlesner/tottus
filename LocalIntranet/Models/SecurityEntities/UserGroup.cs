using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace LocalIntranet.Models.SecurityEntities
{
    [Serializable]
    public class UserGroup : ComplexObject
    {
        [Display(Name="Usuario")]
        public String UserId { get; set; }
        [Display(Name = "Nombre")]
        public String UserName { get; set; }
        [Display(Name = "Incluir")]
        public Boolean Incluido { get; set; }
    }
}