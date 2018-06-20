using LocalIntranet.Models.SecurityEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LocalIntranet.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext()
            : base("name=DefContext")
        {
        }

        public IEnumerable<Usuario> Cal_Ver_Usuarios(Usuario eUsuario)
        {
            var c_accionParam = new SqlParameter("accion", "S");
            var c_displayParam = new SqlParameter("display", eUsuario.UserName);

            string strSQL = "EXEC Cal_Ver_Usuarios @accion = @accion, @display = @display";
            return Database.SqlQuery<Usuario>(strSQL, c_accionParam, c_displayParam);
        }

        public IEnumerable<Usuario> Cal_Ver_UsuariosNAV(String accion , Usuario eUsuario)
        {
            var c_accionParam = new SqlParameter("accion", accion);
            var c_displayParam = new SqlParameter("display", eUsuario.UserName);

            string strSQL = "EXEC Cal_Ver_Usuarios @accion = @accion, @display = @display";
            return Database.SqlQuery<Usuario>(strSQL, c_accionParam, c_displayParam);
        }





    }
}