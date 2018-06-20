using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocalIntranet.Models.SecurityEntities
{
    [Serializable()]
    [Table("SegUsuario")]
    public class Usuario
    {
        [Key] [Display(Name="Usuario")]
        public String UserId { get; set; }
        [Display(Name = "C.Perfil")]
        public String PerfilId { get; set; }
        [Display(Name = "Nombre")]
        public String UserName { get; set; }
        [Display(Name = "Estado")]
        public String EstadoId { get; set; }


    }
}