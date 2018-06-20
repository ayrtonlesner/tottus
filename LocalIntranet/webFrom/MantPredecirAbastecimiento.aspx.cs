using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace LocalIntranet.Views.PredecirAbastecimiento
{
    public partial class MantPredecirAbastecimiento : System.Web.UI.Page
    {

        private DataTable dataTable = new DataTable();
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefContext"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarInformacion();
            }
        }
        void listarInformacion(){

            string query = " SELECT  idpronostico, b.[idsucursal], b.nombresucursal,  estado = (CASE WHEN a.estado = 0 THEN 'Pendiente' ELSE 'Procesado' END )  , b.responsable, b.direccionSucursal, fecharegistro = CONVERT(varchar(10),fecharegistro,103) FROM [dbo].[glog_e20_pronostico] a INNER JOIN [dbo].[ginv_es002_sucursal] b on a.ginv_es002_sucursal_idsucursal = b.[idsucursal] ";
            query = query + " WHERE anio =  '" + Request.QueryString["anio"].ToString() + "'     ";
            query = query + " AND mes =  '" + Convert.ToInt32(Request.QueryString["mes"].ToString()) + "'     ";
            query = query + " ORDER BY nombresucursal ASC ";

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
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showError('No se encontraron registros'); ", true);
            }

            //this.gvListar.DataSource = dataTable;
            //this.gvListar.DataBind();

            conn.Close();
            da.Dispose();
        
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showError('No Hay Registros'); ", true);
            //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openModal(); ", true);
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openMensaje(); ", true);
        }

        protected void gvPronostico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridViewRow row = ((GridViewRow)(((Button)(e.CommandSource)).NamingContainer));

                //String estado = this.gvPronostico.DataKeys(row.RowIndex).Values["idpronostico"].ToString;
                Int32 codiPronostico = Convert.ToInt32(gvPronostico.DataKeys[row.RowIndex].Values["idpronostico"].ToString());

                lblSucursal.Text = gvPronostico.DataKeys[row.RowIndex].Values["nombresucursal"].ToString();
                lblResponsable.Text = gvPronostico.DataKeys[row.RowIndex].Values["responsable"].ToString();
                lblFechaRegistro.Text = gvPronostico.DataKeys[row.RowIndex].Values["fecharegistro"].ToString();

                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openModal(); ", true);

                string query = " SELECT  a.iddetallepronistico, b.idproducto, B.descripcion, c.descripcionUnidadMedida, a.cantidad  FROM [dbo].[glog_e27_pronosticodetalle] a INNER JOIN [dbo].[ginv_es007_producto] b on a.ginv_es007_producto_idproducto = b.idproducto INNER JOIN [dbo].[ginv_es013_unidadmedida] c on b.idproducto = c.GINV_ES007_Producto_IdProducto";
                query = query + " WHERE a.estado in (1,3) AND [glog_e20_pronostico_idpronostico] =  '" + Convert.ToString(codiPronostico) + "'     ";
                query = query + " ORDER BY B.descripcion ASC ";

                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);

                this.gvProducto.DataSource = dataTable;
                this.gvProducto.DataBind();
                
               conn.Close();
               da.Dispose();

            }
        }
        private DataTable tabMateriales1;

        protected void lkSi_Click(object sender, EventArgs e)
        {
            try
            {
                string[] resultado;
                //o_devolucionBE.accion = accion;
                //o_devolucionBE.ATDEVcodi = Convert.ToInt32(0);
                //o_devolucionBE.ATDEVndoc = this.txtNumeroDocumento.Text;
                //o_devolucionBE.ATDEVfdoc = this.dtpFecMovimiento.Value.ToString();
                //o_devolucionBE.AMTMOVcodi = Convert.ToInt32(this.cboDescripcionMovimiento.SelectCodigo);
                //o_devolucionBE.LMLOC1orige = Convert.ToInt32(_CodigoAlmacenOrigen);
                //o_devolucionBE.NMPROcodi = Convert.ToInt32(_NMPROcodi);
                //o_devolucionBE.Materiales = tableMovimiento;
                //o_devolucionBE.usuario = Environment.UserName;

                DataTable tbFormulaAgregar = new DataTable();
                tbFormulaAgregar.Rows.Clear();
                tbFormulaAgregar.Columns.Add("idPronostico");
                tbFormulaAgregar.Columns.Add("idsucursal");

                
                string variable;

                foreach (GridViewRow row in gvPronostico.Rows)
                {
                    CheckBox check = row.FindControl("CheckBox1") as CheckBox;

                    string valueidPronostico = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idpronostico"]);
                    string valueidsucursal = Convert.ToString(gvPronostico.DataKeys[row.RowIndex].Values["idsucursal"]);

                    if (check.Checked)
                    {
                        DataRow row1;
                        row1 = tbFormulaAgregar.NewRow();
                        row1["idPronostico"] = valueidPronostico;
                        row1["idsucursal"] = valueidsucursal;
                        tbFormulaAgregar.Rows.Add(row1);
                    }
                }

                resultado = Mant_OrdenDespacho("GRABAR", tbFormulaAgregar);
                if (Convert.ToInt32(resultado[2]) > 0)
                {
                    listarInformacion();
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showSuccessModal('ÓRDENES GENERADAS SATISFACTORIAMENTE'); ", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal(' " + resultado[1] + "'); ", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal(' " + ex.Message + "'); ", true);
            }
        }

        public string[] Mant_OrdenDespacho(string cadena, DataTable tabMateriales)
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

            SqlCommand cmd = new SqlCommand("[Mant_OrdenDespacho]", cn/*, tr*/);
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
                if (e.Row.Cells[7].Text == "Procesado")
                {
                    chkbox.Enabled = false;
                }
                else
                {
                    chkbox.Enabled = true;
                }
            }

        }

        protected void lkEliminar_Click(object sender, EventArgs e)
        {
            string[] resultado;

            DataTable tbFormulaAgregar = new DataTable();
            tbFormulaAgregar.Rows.Clear();
            tbFormulaAgregar.Columns.Add("iddetallepronistico");
            
            foreach (GridViewRow row in gvProducto.Rows)
            {
                CheckBox check = row.FindControl("CheckBox2") as CheckBox;

                string valueiddetallepronistico = Convert.ToString(gvProducto.DataKeys[row.RowIndex].Values["iddetallepronistico"]);

                if (check.Checked)
                {
                    DataRow row1;
                    row1 = tbFormulaAgregar.NewRow();
                    row1["iddetallepronistico"] = valueiddetallepronistico;
                    tbFormulaAgregar.Rows.Add(row1);
                }
            }

            resultado = Mant_OrdenDespacho("ELIMINAR", tbFormulaAgregar);
            if (Convert.ToInt32(resultado[2]) > 0)
            {
                //MessageBox.Show(resultado[1].ToString(), MyGlobals._glbNameSistema, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //listarTransaccion();
                //TabControl1.TabPages[0].Enabled = true;
                //TabControl1.TabPages[1].Enabled = false;
                //this.TabControl1.SelectedIndex = 0;
                listarInformacion();
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showSuccessModal('Productos eliminados del detalle'); ", true);

            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal(' " + resultado[1] + "'); ", true);

                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openModal(); ", true);

            }




        }



    }
}