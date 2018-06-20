using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace LocalIntranet.webFrom
{
    public partial class MantProyectarNecesidadTransporte : System.Web.UI.Page
    {
        private DataTable dataTable = new DataTable();
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefContext"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //String anio = Request.QueryString["anio"].ToString();
                //String mes = Request.QueryString["mes"].ToString();
                listarInformacion();
                this.tx_fechahora.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

            }
        }

        void listarInformacion()
        {

            //string query = "  SELECT a.idprogramacion, a.estadoprogramacion, b.idvehiculo, vehiculo = b.placa, c.idtransportista,    ";
            //query = query + " conductor =  c.apellidoPaterno + ' ' +  c.apellidoMaterno + ' ' + c.nombreTransportista, cantidadOrden = COUNT(*), estado = (CASE WHEN a.estadoprogramacion = 0 THEN 'Pendiente' ELSE 'Procesado' END )  ";
            //query = query + " FROM  [dbo].[glog_e06_programaciondespacho] a     ";
            //query = query + "  INNER JOIN [dbo].[glog_e09_vehiculo] b on a.glog_e12_vehiculotransportista_glog_e09_vehiculo_idvehiculo = b.idvehiculo  ";
            //query = query + "  INNER JOIN [dbo].[glog_e08_transportista] c on a.glog_e12_vehiculotransportista_glog_e09_vehiculo_idvehiculo = c.idtransportista  ";
            //query = query + "  INNER JOIN [dbo].[glog_e07_detalleprogramacion] d on a.idprogramacion = d.glog_e06_programaciondespacho_idprogramacion  ";
            //query = query + "  GROUP BY a.idprogramacion, a.estadoprogramacion, b.idvehiculo, b.placa, c.idtransportista, c.apellidoPaterno, c.apellidoMaterno, c.nombreTransportista, a.estadoprogramacion    ";

            string query = "  SELECT a.idprogramacion, a.estadoprogramacion, b.idvehiculo, vehiculo = b.placa, c.idtransportista,    ";
            query = query + " conductor =  c.apellidoPaterno + ' ' +  c.apellidoMaterno + ' ' + c.nombreTransportista, cantidadOrden = COUNT(*), estado = (CASE WHEN a.estadoprogramacion = 0 THEN 'Pendiente' ELSE 'Procesado' END )  ";
            query = query + " FROM  [dbo].[glog_e06_programaciondespacho] a     ";
            query = query + "  INNER JOIN [dbo].[glog_e09_vehiculo] b on a.glog_e12_vehiculotransportista_glog_e09_vehiculo_idvehiculo = b.idvehiculo  ";
            query = query + "  INNER JOIN [dbo].[glog_e08_transportista] c on a.glog_e12_vehiculotransportista_glog_e09_vehiculo_idvehiculo = c.idtransportista  ";
            query = query + "  INNER JOIN [dbo].[glog_e07_detalleprogramacion] d on a.idprogramacion = d.glog_e06_programaciondespacho_idprogramacion  ";

            query = query + "   INNER JOIN glog_e04_ordendespacho e on d.glog_e04_ordendespacho_idordendespacho = e.idordendespacho  ";
            query = query + "   INNER JOIN ginv_es002_sucursal f on e.GINV_ES002_Sucursal_IdSucursal = f.idsucursal  ";
            query = query + "    INNER JOIN glog_e23_zona g on f.glog_e23_zona_idzona = g.idzona  ";
            query = query + "  WHERE glog_e22_departamento_iddepartamento =  '" + Convert.ToInt32(Request.QueryString["departamento"].ToString()) + "'     ";

            query = query + "  GROUP BY a.idprogramacion, a.estadoprogramacion, b.idvehiculo, b.placa, c.idtransportista, c.apellidoPaterno, c.apellidoMaterno, c.nombreTransportista, a.estadoprogramacion    ";
           



            //query = query + "  WHERE glog_e22_departamento_iddepartamento =  '" + Convert.ToInt32(Request.QueryString["departamento"].ToString()) + "'     ";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);

            this.gvPronostico.DataSource = dataTable;
            this.gvPronostico.DataBind();

            if (dataTable.Rows.Count <= 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "myScript", ("<script>javascript:showError(\'" + ("No existen datos" + "\');</script>")), true);
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showError('No Hay Registros'); ", true);
            }

            //this.gvListar.DataSource = dataTable;
            //this.gvListar.DataBind();

            conn.Close();
            da.Dispose();
        }

        String valueidprogramacion;

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox chk = (CheckBox)sender;
            //GridViewRow row = (GridViewRow)chk.Parent.Parent;

            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int a = row.RowIndex;

            foreach (GridViewRow row1 in gvPronostico.Rows)
            {
                if (chk.Checked)
                {
                    if ((row1.RowIndex != a))
                    {
                        //CheckBox rd = rw.FindControl("CheckBox1"), CheckBox);
                        CheckBox check = row1.FindControl("CheckBox1") as CheckBox;
                        check.Checked = false;
                    }

                }
            }

          
            foreach (GridViewRow row2 in gvPronostico.Rows)
            {
                CheckBox check = row2.FindControl("CheckBox1") as CheckBox;

                if (check.Checked)
                {
                    valueidprogramacion = gvPronostico.DataKeys[row2.RowIndex].Values["idprogramacion"].ToString();
                    //LinkButton2.Text = valueidprogramacion;
                    lblVehiculo.Text = gvPronostico.DataKeys[row2.RowIndex].Values["vehiculo"].ToString();
                    lblConductor.Text = gvPronostico.DataKeys[row2.RowIndex].Values["conductor"].ToString();
                }
            }

            string query = "  SELECT iddetalleprogramacion, b.idordendespacho, fecharegistro = CONVERT(varchar(10),b.fecharegistro,103), estado = (CASE WHEN b.estado = 0 THEN 'Pendiente' ELSE 'Asignado' END ),";
            query = query + " c.idsucursal, c.descripcionSucursal, c.responsable  ";
            query = query + " FROM  [dbo].[glog_e07_detalleprogramacion] a     ";
            query = query + "  INNER JOIN glog_e04_ordendespacho b on a.glog_e04_ordendespacho_idordendespacho = b.idordendespacho  ";
            query = query + "  INNER JOIN ginv_es002_sucursal c on b.GINV_ES002_Sucursal_IdSucursal = c.idsucursal  ";
            query = query + "  WHERE glog_e06_programaciondespacho_idprogramacion =  '" + Convert.ToInt32(valueidprogramacion) + "'     ";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);

            this.gvOrdenDespacho.DataSource = dataTable;
            this.gvOrdenDespacho.DataBind();

            if (dataTable.Rows.Count <= 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "myScript", ("<script>javascript:showError(\'" + ("No existen datos" + "\');</script>")), true);
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showError('No Hay Registros'); ", true);
            }

            //this.gvListar.DataSource = dataTable;
            //this.gvListar.DataBind();

            conn.Close();
            da.Dispose();



            //LinkButton link = (LinkButton)row.FindControl("link");
            //link.Visible = chk.Checked ? true : false;

        }

        protected void lbGrabar_Click(object sender, EventArgs e)
        {
            //string valor = Request.Form["horaProgramacion"];

            //lblVehiculo.Text = valor;

            DataTable tbFormulaAgregar = new DataTable();
            tbFormulaAgregar.Rows.Clear();
            tbFormulaAgregar.Columns.Add("idprogramacion");

            foreach (GridViewRow row in gvPronostico.Rows)
            {
                CheckBox check = row.FindControl("CheckBox1") as CheckBox;
               
                if (check.Checked)
                {
                    if (Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estado"]) == "Procesado")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal('REGISTRO YA SE ENCUENTRA PROCESADO'); ", true);
                    }
                    DataRow row1;
                    row1 = tbFormulaAgregar.NewRow();
                    row1["idprogramacion"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idprogramacion"]);
                    tbFormulaAgregar.Rows.Add(row1);

                }
            }

            if (tbFormulaAgregar.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal('Debe Seleccionar Vehiculos Programados'); ", true);
            }
            else
            {
               ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openMensaje(); ", true);
            }
        }

        protected void lkSi_Click(object sender, EventArgs e)
        {
            try
            {
                string[] resultado;

                DataTable tbFormulaAgregar = new DataTable();
                tbFormulaAgregar.Rows.Clear();
                tbFormulaAgregar.Columns.Add("idprogramacion");
     
                foreach (GridViewRow row in gvPronostico.Rows)
                {
                    CheckBox check = row.FindControl("CheckBox1") as CheckBox;

                    if (check.Checked)
                    {
                        DataRow row1;
                        row1 = tbFormulaAgregar.NewRow();
                        row1["idprogramacion"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idprogramacion"]);
                        tbFormulaAgregar.Rows.Add(row1);
                    }
                }


                resultado = Mant_ProyectarTransporte("GRABAR", tbFormulaAgregar);
                if (Convert.ToInt32(resultado[2]) > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showSuccessModal('Datos registrados'); ", true);
                    listarInformacion();
                }
                else
                {
                    //MessageBox.Show(resultado[1].ToString(), MyGlobals._glbNameSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal(' " + resultado[1] + "'); ", true);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(o_devolucionBL.Errores(), MyGlobals._glbNameSistema, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal(' " + ex.Message + "'); ", true);
            }
        }



        public string[] Mant_ProyectarTransporte(string cadena, DataTable tabMateriales)
        {
            string[] resultado = new string[5];

            DataTable dtMateriales;
            dtMateriales = tabMateriales;
            System.IO.StringWriter swMateriales = new System.IO.StringWriter();
            dtMateriales.TableName = "Nombre";
            dtMateriales.WriteXml(swMateriales);

            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["DefContext"].ConnectionString;
            SqlConnection cn = new SqlConnection(conString);
            SqlTransaction tr;

            cn.Open();

            //tr = cn.BeginTransaction(IsolationLevel.Serializable);

            SqlCommand cmd = new SqlCommand("[Mant_ProyectarTransporte]", cn/*, tr*/);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", SqlDbType.VarChar, 100).Value = cadena;

            cmd.Parameters.Add("@fecha", SqlDbType.VarChar, 10).Value = this.tx_fechahora.Text;
            cmd.Parameters.Add("@hora", SqlDbType.VarChar, 10).Value = Request.Form["horaProgramacion"];

            cmd.Parameters.Add("@Materiales", SqlDbType.Xml).Value = swMateriales.ToString();

            cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 100).Value = "";

            cmd.Parameters.Add("@Codigo", SqlDbType.Int);
            cmd.Parameters["@Codigo"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 1000);
            cmd.Parameters["@Mensaje"].Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@piint_rows", SqlDbType.Int);
            cmd.Parameters["@piint_rows"].Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                //resultado = 1;
                //tr.Commit();
                resultado[0] = cmd.Parameters["@Codigo"].Value.ToString();
                resultado[1] = cmd.Parameters["@Mensaje"].Value.ToString();
                resultado[2] = cmd.Parameters["@piint_rows"].Value.ToString();
            }
            catch (Exception ex)
            {
                //tr.Rollback();
                //resultado = 0;
                //str_error = ex.Message;
                resultado[0] = "0";
                resultado[1] = ex.Message;
                resultado[2] = "0";
            }
            finally
            {
                cn.Dispose();
                cmd.Dispose();
            }
            return resultado;
        }

        protected void gvOrdenDespacho_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = ((GridViewRow)(((Button)(e.CommandSource)).NamingContainer));

                ////String estado = this.gvPronostico.DataKeys(row.RowIndex).Values["idpronostico"].ToString;
                Int32 codiidordendespacho = Convert.ToInt32(gvOrdenDespacho.DataKeys[row.RowIndex].Values["idordendespacho"].ToString());


                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openDetalle(); ", true);

                string query = " SELECT  b.descripcion, a.cantidadorden, c.[nombreunidadmedida], b.peso, ";
                query = query + "  tonelada = CAST((CASE WHEN c.[nombreunidadmedida] = 'Kilo' THEN  b.peso * a.cantidadorden  WHEN c.[nombreunidadmedida] = 'Gramo' THEN  (b.peso * a.cantidadorden) / 1000   ELSE a.cantidadorden END ) / 10000 AS decimal(18,6)) ";
                query = query + "  FROM [dbo].[glog_e05_detalleordendespacho] a ";
                query = query + "  INNER JOIN [dbo].[ginv_es007_producto] b on a.ginv_es013_unidadmedida_ginv_es007_producto_idproducto = b.idproducto ";
                query = query + "  INNER JOIN [dbo].[ginv_es013_unidadmedida] c on b.idproducto = c.GINV_ES007_Producto_IdProducto ";
                query = query + " WHERE a.glog_e04_ordendespacho_idordendespacho  =  '" + Convert.ToString(codiidordendespacho) + "'     ";


                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);

                this.gvDetalleOrdenDespacho.DataSource = dataTable;
                this.gvDetalleOrdenDespacho.DataBind();

                conn.Close();
                da.Dispose();

            }
        }




    }
}