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
    public partial class MantOptimizarDistribucion : System.Web.UI.Page
    {
        private DataTable dataTable = new DataTable();
        //string connString = "data source=192.168.0.10; User id=sa; password=Sql123456; Initial Catalog=tottus";
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefContext"].ConnectionString;

        //DataTable dtOrdenes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //String anio = Request.QueryString["anio"].ToString();
                //String mes = Request.QueryString["mes"].ToString();
                listarInformacion();


            }
        }


        void listarInformacion()
        {

            string query = "  select  a.idordendespacho, b.idsucursal, b.descripcionSucursal, b.responsable, fecharegistro = CONVERT(varchar(10),a.fecharegistro,103), carga = CAST(a.totaltoneladas AS DECIMAL(18,2)),     ";
            query = query + " conductor =  f.apellidoPaterno + ' ' +  f.apellidoMaterno + ' ' + f.nombreTransportista,  ";
            query = query + " vehiculo = e.placa,  estado = (CASE WHEN a.estado = 0 THEN 'Pendiente' ELSE 'Asignado' END ), idtransportista = f.idtransportista, idvehiculo = e.idvehiculo,     ";
            query = query + "  estadoGrabacion  = (CASE WHEN a.estado = 0 THEN 'Pendiente' ELSE 'Asignado' END )  ";
            query = query + "  from [dbo].[glog_e04_ordendespacho] a  ";
            query = query + "  INNER JOIN [dbo].[ginv_es002_sucursal] b on a.GINV_ES002_Sucursal_IdSucursal = b.idsucursal  ";
            query = query + "  LEFT JOIN [glog_e07_detalleprogramacion] c on a.idordendespacho = c.[glog_e04_ordendespacho_idordendespacho]  ";
            query = query + "  LEFT JOIN glog_e06_programaciondespacho d on c.[glog_e06_programaciondespacho_idprogramacion] = d.idprogramacion  ";
            query = query + "  LEFT JOIN [dbo].[glog_e09_vehiculo] e on d.glog_e12_vehiculotransportista_glog_e08_transportista_idtransportista = e.idvehiculo   ";
            query = query + "  LEFT JOIN [dbo].[glog_e08_transportista]  f on d.glog_e12_vehiculotransportista_glog_e08_transportista_idtransportista = f.idtransportista   ";
            query = query + "  INNER JOIN [dbo].[glog_e23_zona] g on b.glog_e23_zona_idzona = g.idzona ";
            query = query + "  WHERE glog_e22_departamento_iddepartamento =  '" + Convert.ToInt32(Request.QueryString["departamento"].ToString()) + "'     ";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);

            //dtOrdenes = dataTable;
            this.gvPronostico.DataSource = dataTable;
            this.gvPronostico.DataBind();

            if (dataTable.Rows.Count <= 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "myScript", ("<script>javascript:showError(\'" + ("No existen datos" + "\');</script>")), true);
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showError('No se encontraron registros'); ", true);
            }

            //this.gvListar.DataSource = dataTable;
            //this.gvListar.DataBind();

            conn.Close();
            da.Dispose();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openModal(); ", true);

            string query = " SELECT b.idtransportista, nombreTransportista = b.apellidoPaterno + ' ' +  b.apellidoMaterno + ' ' + b.nombreTransportista, c.idvehiculo, c.placa, c.pesobruto, estado = (case when a.estadovehiculotransportista = '0' THEN 'Disponible' else 'Asignado' END) ";
            query = query + " FROM [dbo].[glog_e12_vehiculotransportista] a ";
            query = query + " INNER JOIN [glog_e08_transportista] b on a.glog_e08_transportista_idtransportista = b.idtransportista ";
            query = query + " INNER JOIN [glog_e09_vehiculo] c on a.glog_e09_vehiculo_idvehiculo = c.idvehiculo ";
            query = query + " ORDER BY b.apellidoPaterno ASC ";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);

            this.gvTransportista.DataSource = dataTable;
            this.gvTransportista.DataBind();

            conn.Close();
            da.Dispose();
        }

        String conductor, vehiculo;
        Int32 valueidtransportista, valueidvehiculo;
        protected void lkOk_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvTransportista.Rows)
            {
                CheckBox check = row.FindControl("CheckBox1") as CheckBox;

                if (check.Checked)
                {
                    conductor = Convert.ToString(gvTransportista.DataKeys[row.RowIndex].Values["nombreTransportista"]);
                    vehiculo = Convert.ToString(gvTransportista.DataKeys[row.RowIndex].Values["placa"]);

                    valueidtransportista = Convert.ToInt32(gvTransportista.DataKeys[row.RowIndex].Values["idtransportista"]);
                    valueidvehiculo = Convert.ToInt32(gvTransportista.DataKeys[row.RowIndex].Values["idvehiculo"]);
                    //DataRow row1;
                    //row1 = tbFormulaAgregar.NewRow();
                    //row1["iddetallepronistico"] = valueiddetallepronistico;
                    //tbFormulaAgregar.Rows.Add(row1);
                }
            }

            DataTable tbFinal = new DataTable();
            tbFinal.Rows.Clear();
            tbFinal.Columns.Add("idordendespacho");
            tbFinal.Columns.Add("idsucursal");
            tbFinal.Columns.Add("descripcionSucursal");
            tbFinal.Columns.Add("responsable");
            tbFinal.Columns.Add("fecharegistro");
            tbFinal.Columns.Add("carga");
            tbFinal.Columns.Add("conductor");
            tbFinal.Columns.Add("vehiculo");
            tbFinal.Columns.Add("estado");
            tbFinal.Columns.Add("idtransportista");
            tbFinal.Columns.Add("idvehiculo");
            tbFinal.Columns.Add("estadoGrabacion");


            DataTable tbPadre = new DataTable();
            tbPadre.Rows.Clear();
            tbPadre.Columns.Add("idordendespacho");
            tbPadre.Columns.Add("idsucursal");
            tbPadre.Columns.Add("descripcionSucursal");
            tbPadre.Columns.Add("responsable");
            tbPadre.Columns.Add("fecharegistro");
            tbPadre.Columns.Add("carga");
            tbPadre.Columns.Add("conductor");
            tbPadre.Columns.Add("vehiculo");
            tbPadre.Columns.Add("estado");
            tbPadre.Columns.Add("idtransportista");
            tbPadre.Columns.Add("idvehiculo");
            tbPadre.Columns.Add("estadoGrabacion");

            DataTable tbHijo = new DataTable();
            tbHijo.Rows.Clear();
            tbHijo.Columns.Add("idordendespacho");

            foreach (GridViewRow row in gvPronostico.Rows)
            {
                DataRow row1;
                row1 = tbPadre.NewRow();
                row1["idordendespacho"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idordendespacho"]);
                row1["idsucursal"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idsucursal"]);
                row1["descripcionSucursal"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["descripcionSucursal"]);
                row1["responsable"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["responsable"]);
                row1["fecharegistro"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["fecharegistro"]);
                row1["carga"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["carga"]);
                row1["conductor"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["conductor"]);
                row1["vehiculo"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["vehiculo"]);
                row1["estado"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estado"]);
                row1["idtransportista"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idtransportista"]);
                row1["idvehiculo"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idvehiculo"]);
                row1["estadoGrabacion"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estadoGrabacion"]);
                tbPadre.Rows.Add(row1);
            }



            foreach (GridViewRow row in gvPronostico.Rows)
            {
                CheckBox check = row.FindControl("CheckBox1") as CheckBox;
                if (check.Checked)
                {
                    //if (Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estado"]) == "Asignado")
                    //{
                    //    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal('Conductor ya Asignado'); ", true);
                       

                    //    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openModal(); ", true);
                    //    return;
                    //}

                    DataRow row1;
                    row1 = tbHijo.NewRow();
                    row1["idordendespacho"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idordendespacho"]);
                    tbHijo.Rows.Add(row1);
                }
            }

            DataRow rowLista;

            foreach (DataRow row in tbPadre.Rows) /*el padre*/
            {
                Int32 codigoPadre = Convert.ToInt32(row["idordendespacho"].ToString());
                
                foreach (DataRow row1 in tbHijo.Rows) /*el padre*/
                   {

                     Int32 codigoHijo = Convert.ToInt32(row1["idordendespacho"].ToString());

                         if (codigoPadre == codigoHijo)
                         {
                           
                             rowLista = tbFinal.NewRow();
                             rowLista["idordendespacho"] = row["idordendespacho"].ToString();
                             rowLista["idsucursal"] = row["idsucursal"].ToString();
                             rowLista["descripcionSucursal"] = row["descripcionSucursal"].ToString();
                             rowLista["responsable"] = row["responsable"].ToString();
                             rowLista["fecharegistro"] = row["fecharegistro"].ToString();
                             rowLista["carga"] = row["carga"].ToString();
                             rowLista["conductor"] = conductor;
                             rowLista["vehiculo"] = vehiculo;
                             rowLista["estado"] = "Asignado";
                             rowLista["idtransportista"] = valueidtransportista;
                             rowLista["idvehiculo"] = valueidvehiculo;
                             rowLista["estadoGrabacion"] = row["estadoGrabacion"].ToString();
                             tbFinal.Rows.Add(rowLista);
                         }
                   }

                       DataRow[] foundAuthors = tbFinal.Select("idordendespacho = '" + row["idordendespacho"].ToString() + "'");
                       if (foundAuthors.Length == 0)
                       {

                rowLista = tbFinal.NewRow();
                rowLista["idordendespacho"] = row["idordendespacho"].ToString();
                rowLista["idsucursal"] = row["idsucursal"].ToString();
                rowLista["descripcionSucursal"] = row["descripcionSucursal"].ToString();
                rowLista["responsable"] = row["responsable"].ToString();
                rowLista["fecharegistro"] = row["fecharegistro"].ToString();
                rowLista["carga"] = row["carga"].ToString();
                rowLista["conductor"] = row["conductor"].ToString();
                rowLista["vehiculo"] = row["vehiculo"].ToString();
                rowLista["estado"] = row["estado"].ToString();
                rowLista["idtransportista"] = row["idtransportista"].ToString();
                rowLista["idvehiculo"] = row["idvehiculo"].ToString();
                rowLista["estadoGrabacion"] = row["estadoGrabacion"].ToString();
                tbFinal.Rows.Add(rowLista);

                       }
                                       
            }
  
            gvPronostico.DataSource = tbFinal;
            gvPronostico.DataBind();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            DataTable tbFormulaAgregar = new DataTable();
            tbFormulaAgregar.Rows.Clear();
            tbFormulaAgregar.Columns.Add("idordendespacho");
            tbFormulaAgregar.Columns.Add("idtransportista");
            tbFormulaAgregar.Columns.Add("idvehiculo");

            foreach (GridViewRow row in gvPronostico.Rows)
            {
                //CheckBox check = row.FindControl("CheckBox1") as CheckBox;
                String estado = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estado"]);
                String estadoGrabacion = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estadoGrabacion"]);

                //if (check.Checked)
                //{
                if ((estado == "Asignado") && (estadoGrabacion == "Pendiente"))
                {
                    DataRow row1;
                    row1 = tbFormulaAgregar.NewRow();
                    row1["idordendespacho"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idordendespacho"]);
                    row1["idtransportista"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idtransportista"]);
                    row1["idvehiculo"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idvehiculo"]);
                    tbFormulaAgregar.Rows.Add(row1);
                }

                //}
            }

            if (tbFormulaAgregar.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal('Debe Indicar disponibilidad de Transporte'); ", true);
            }else {
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
                tbFormulaAgregar.Columns.Add("idordendespacho");
                tbFormulaAgregar.Columns.Add("idtransportista");
                tbFormulaAgregar.Columns.Add("idvehiculo");

                foreach (GridViewRow row in gvPronostico.Rows)
                {
                    //CheckBox check = row.FindControl("CheckBox1") as CheckBox;

                    //if (check.Checked)
                    //{
                    String estado = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estado"]);
                    String estadoGrabacion = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["estadoGrabacion"]);

                    if ((estado == "Asignado") && (estadoGrabacion == "Pendiente"))
                    {
                         DataRow row1;
                        row1 = tbFormulaAgregar.NewRow();
                        row1["idordendespacho"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idordendespacho"]);
                        row1["idtransportista"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idtransportista"]);
                        row1["idvehiculo"] = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idvehiculo"]);
                        tbFormulaAgregar.Rows.Add(row1);
                    }
                    
                    //}
                }

                if (tbFormulaAgregar.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showError('Debe Indicar disponibilidad de Tranporte'); ", true);
                }

                resultado = Mant_Programacion("GRABAR", tbFormulaAgregar);
                if (Convert.ToInt32(resultado[2]) > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showSuccessModal('Optimización Generada'); ", true);
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


        public string[] Mant_Programacion(string cadena, DataTable tabMateriales)
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

            SqlCommand cmd = new SqlCommand("[Mant_Programacion]", cn/*, tr*/);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@accion", SqlDbType.VarChar, 100).Value = cadena;

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

        protected void gvPronostico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkbox = (CheckBox)e.Row.FindControl("CheckBox1");
                if (e.Row.Cells[10].Text == "Asignado")
                {
                    chkbox.Enabled = false;
                }
                else
                {
                    chkbox.Enabled = true;
                }
            }
        }

        //protected void gvTransportista_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chkbox = (CheckBox)e.Row.FindControl("CheckBox1");
        //        if (e.Row.Cells[6].Text == "Asignado")
        //        {
        //            chkbox.Enabled = false;
        //        }
        //        else
        //        {
        //            chkbox.Enabled = true;
        //        }
        //    }

        //}

        protected void gvTransportista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkbox = (CheckBox)e.Row.FindControl("CheckBox1");
                if (e.Row.Cells[6].Text == "Asignado")
                {
                    chkbox.Enabled = false;
                }
                else
                {
                    chkbox.Enabled = true;
                }
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int a = row.RowIndex;

            foreach (GridViewRow row1 in gvTransportista.Rows)
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
        }

        protected void gvPronostico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = ((GridViewRow)(((Button)(e.CommandSource)).NamingContainer));

                ////String estado = this.gvPronostico.DataKeys(row.RowIndex).Values["idpronostico"].ToString;
                Int32 codiidordendespacho = Convert.ToInt32(gvPronostico.DataKeys[row.RowIndex].Values["idordendespacho"].ToString());


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