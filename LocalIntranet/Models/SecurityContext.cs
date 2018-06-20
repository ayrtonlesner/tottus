using LocalIntranet.Models.SecurityEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LocalIntranet.Models
{
    public class SecurityContext : DbContext
    {
        public SecurityContext() : base("name=DefContext") { }

        public DbSet<Usuario> Usuarios { get; set; }

        // Verificar si el acceso a cierta carpeta/opcion/accion esta dado para un usario
        public bool isAccessGranted(string controllerName, string actionName, string userName)
        {
            // var c_aplicacionParam = new SqlParameter("c_aplicacion", "IN"); Verificar opciones del módulo Intranet (IN)
            //var c_codigousuarioParam = new SqlParameter("UserId", userName);
            //var c_controladorParam = new SqlParameter("ControllerName", controllerName);
            //var c_accionParam = new SqlParameter("ActionName", actionName);

            //string strSQL = "EXEC seg_ver_accesopcion @UserId = @UserId, @ControllerName = @ControllerName, @ActionName = @ActionName";

            //string strSQL = "EXEC seg_ver_accesopcion @UserId = 1, @ControllerName = 2, @ActioName = 3";
            return true ;
            //return Database.SqlQuery<bool>(strSQL, c_codigousuarioParam, c_controladorParam, c_accionParam).FirstOrDefault();
        }

        // Generar el menu de opciones
        //public IEnumerable<MenuItem> listMenuItems(string userName)
        //{
        //    var c_codigousuarioParam = new SqlParameter("UserId", userName);

        //    string strSQL = "EXEC seg_ver_useroptions @UserId = @UserId";
        //    return Database.SqlQuery<MenuItem>(strSQL, c_codigousuarioParam);
        //}

        // Listar Grupos
        public IEnumerable<Usuario> listGroups()
        {
            var DivQry = from u in this.Usuarios
                         where u.PerfilId.Equals("GP")
                         select u;

            return DivQry.ToList();
        }

        public IEnumerable<UserGroup> listUsersGroup(string sCodigoGrupo)
        {
            // Accion: Usuario pertenecientes al grupo (S)elect, Usuarios NO pertenecientes al grupo (R)esto
            var c_accionParam = new SqlParameter("c_accion", "S");
            var c_codigogrupoParam = new SqlParameter("c_codigogrupo", sCodigoGrupo);

            string strSQL = "EXEC sp_ma_man_ma_grupousuario @c_accion = @c_accion, @c_codigogrupo = @c_codigogrupo";
            return Database.SqlQuery<UserGroup>(strSQL, c_accionParam, c_codigogrupoParam);
        }

        public void alterUsersGroup(string c_accion, string c_codigogrupo, string c_codigousuario,
            string c_usuariomod)
        {
            // Accion: Agregar usuario a Grupo (I)nsert, Quitar usuario del grupo (D)elete
            var c_accionParam = new SqlParameter("c_accion", c_accion);
            var c_codigogrupoParam = new SqlParameter("c_codigogrupo", c_codigogrupo);
            var c_codigousuarioParam = new SqlParameter("c_codigousuario", c_codigousuario);
            var c_usuariomodParam = new SqlParameter("c_usuariomod", c_usuariomod);

            string strSQL = "EXEC sp_ma_man_ma_grupousuario @c_accion = @c_accion, @c_codigogrupo = @c_codigogrupo, " +
                "@c_codigousuario = @c_codigousuario, @c_usuariomod = @c_usuariomod";
            Database.ExecuteSqlCommand(strSQL, c_accionParam, c_codigogrupoParam,
                c_codigousuarioParam, c_usuariomodParam);
        }

    }
}