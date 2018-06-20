using LocalIntranet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace LocalIntranet.webFrom
{
    public partial class MantConciliarMercaderia : System.Web.UI.Page
    {

        static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefContext"].ConnectionString;
        private DataTable dtFactura ;
        private DataTable dtDetalleFactura ;
        private Int32 idProveedor;
        string sFullPath = @"D:\Facturacion Elec\";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                try
                {

                    idProveedor = Convert.ToInt32(Request.QueryString["proveedor"].ToString());

                    if (!Directory.Exists(sFullPath))
                        Directory.CreateDirectory(sFullPath);

                    ListarFactura();
                }
                catch (Exception ex)
                {

                } 
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: openMensaje(); ", true);
            
        }

        protected void lkSi_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (GridViewRow row in gvFactura.Rows)
                {
                    CheckBox chkSel = (CheckBox)row.FindControl("CheckBox2");
                    if (chkSel.Checked)
                    {
                        string sArchivo = row.Cells[9].Text;
                        idProveedor = Convert.ToInt32(Request.QueryString["proveedor"].ToString());

                        RemoteData oRemote = new RemoteData();
                        DataSet dsProveedor = null;
                        string sRUC = "1234567890";
                        Invoice oInvoice;
                        DataRow drInvoice;
                        DataRow drInvoiceLine;
                        Int32 iLinea = 0;

                        oRemote.ConnectionString = connString;

                        dtFactura = new DataTable();
                        dtFactura = GetInvoiceTable();

                        oInvoice = DeserializeObject(sFullPath + sArchivo);
                        drInvoice = dtFactura.NewRow();
                        SetInvoiceRow(ref drInvoice, oInvoice);
                        drInvoice["idproveedor"] = idProveedor;
                        dtFactura.Rows.Add(drInvoice);

                        dtDetalleFactura = new DataTable();
                        dtDetalleFactura = GetInvoiceLineTable();

                        foreach (InvoiceLine oInvoiceLine in oInvoice.InvoiceLine)
                        {
                            drInvoiceLine = dtDetalleFactura.NewRow();
                            SetInvoiceLineRow(ref drInvoiceLine, oInvoiceLine);
                            drInvoiceLine["iddetalle"] = iLinea + 1;
                            dtDetalleFactura.Rows.Add(drInvoiceLine);
                            iLinea += 1;
                        }

                        InsertaNuevaFactura(dtFactura, dtDetalleFactura);


                        if (!Directory.Exists(sFullPath + "procesados"))
                            Directory.CreateDirectory(sFullPath + "procesados");

                        FileInfo oFileInfo = new FileInfo(sFullPath + sArchivo);
                        if (oFileInfo.Exists)
                            oFileInfo.MoveTo(sFullPath + @"procesados\" + oFileInfo.Name);

                    }

                }

                ListarFactura();

            }
            catch (Exception)
            {

            }
        }

        private void InsertaNuevaFactura(DataTable dtFactura, DataTable dtFacturaDetalle)
        {
            RemoteData oRemoteData = new RemoteData();
            DataSet dsNotaIngreso = null;
            DataSet dsNotaIngresoDetalle = null;
            DataRow drNotaIngreso;
            DataRow drNotaIngresoDetalle;
            string sQuery;
            decimal iIdNotaIngreso = 1;
            DataRow drFactura;
            Int32 iLinea = 0;
            try
            {
                oRemoteData.ConnectionString = connString;

                sQuery = "SELECT MAX(idnotaingreso) FROM glog_e25_nota_ingreso";
                oRemoteData.GetDataSetByQuery(sQuery, "glog_e25_nota_ingreso", ref dsNotaIngreso);

                if (dsNotaIngreso.Tables[0].Rows.Count > 0) 
                    if (dsNotaIngreso.Tables[0].Rows[0][0] != DBNull.Value)
                        iIdNotaIngreso = (decimal) dsNotaIngreso.Tables[0].Rows[0][0] + 1;


                sQuery = "SELECT * FROM glog_e25_nota_ingreso WHERE 1=0";
                oRemoteData.GetDataSetByQuery(sQuery, "glog_e25_nota_ingreso", ref dsNotaIngreso);
                sQuery = "SELECT * FROM glog_e26_notaingreso_detalle WHERE 1=0";
                oRemoteData.GetDataSetByQuery(sQuery, "glog_e26_notaingreso_detalle", ref dsNotaIngresoDetalle);

                drNotaIngreso = dsNotaIngreso.Tables[0].NewRow();
                drFactura = dtFactura.Rows[0];

                drNotaIngreso["idnotaingreso"] = iIdNotaIngreso;
                drNotaIngreso["fechaemi"] = drFactura["fechaemi"];
                drNotaIngreso["fecharegistro"] = oRemoteData.GetServerDate();
                drNotaIngreso["fechavcto"] = drNotaIngreso["fechaemi"];
                drNotaIngreso["tipodocumento"] = drFactura["codigotipodocumento"];
                drNotaIngreso["serie"] = drFactura["numerodocumento"].ToString().Split('-')[0];
                drNotaIngreso["Numero"] = drFactura["numerodocumento"].ToString().Split('-')[1];
                drNotaIngreso["Base"] = drFactura["Base"];
                drNotaIngreso["Exonerado"] = drFactura["Exonerado"];
                drNotaIngreso["Igv"] = drFactura["Igv"];
                drNotaIngreso["Total"] = drFactura["Total"];
                drNotaIngreso["GLOG_E24_Proveedor_idProveedor"] = drFactura["idProveedor"];
                
                dsNotaIngreso.Tables[0].Rows.Add(drNotaIngreso);

                foreach (DataRow drFacturaDetalle in dtFacturaDetalle.Rows)
                {
                    drNotaIngresoDetalle = dsNotaIngresoDetalle.Tables[0].NewRow();
                    drNotaIngresoDetalle["iddetallenotaingreso"] = iLinea +1;
                    drNotaIngresoDetalle["glog_e25_nota_ingreso_idnotaingreso"] = drNotaIngreso["idnotaingreso"];
                    drNotaIngresoDetalle["idcantidad"] = drFacturaDetalle["idcantidad"];
                    drNotaIngresoDetalle["idprecio"] = drFacturaDetalle["idprecioigv"];
                    drNotaIngresoDetalle["ginv_es013_unidadmedida_ginv_es007_producto_idproducto"] = GetUnidadIdProductoPorIdProveedor(drNotaIngreso["GLOG_E24_Proveedor_idProveedor"].ToString(), drFacturaDetalle["idproducto"].ToString(), connString);
                    drNotaIngresoDetalle["ginv_es013_unidadmedida_idunidadmedida"] = GetUnidadMedidaPorIdProducto(drNotaIngresoDetalle["ginv_es013_unidadmedida_ginv_es007_producto_idproducto"].ToString(), connString);
                    dsNotaIngresoDetalle.Tables[0].Rows.Add(drNotaIngresoDetalle);

                    iLinea += 1;
                }

                oRemoteData.UpdateDataSet("SELECT * FROM glog_e25_nota_ingreso", ref dsNotaIngreso, "glog_e25_nota_ingreso");
                oRemoteData.UpdateDataSet("SELECT * FROM glog_e26_notaingreso_detalle", ref dsNotaIngresoDetalle, "glog_e26_notaingreso_detalle");

                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showSuccessModal('Conciliación generada.'); ", true);


            }
            catch (Exception e)
            {
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showErrorModal('Ocurrio un error'); ", true);
                throw e;
            }

        }

        private decimal GetUnidadMedidaPorIdProducto(string idProducto, string sconn)
        {
            decimal iUnidadMedida;
            try
            {
                using (SqlConnection conn = new SqlConnection(sconn))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select idunidadmedida from ginv_es013_unidadmedida WHERE GINV_ES007_Producto_IdProducto = " + idProducto, conn))
                    {
                        command.CommandType = CommandType.Text;
                        iUnidadMedida = (decimal)command.ExecuteScalar();

                    }
                    conn.Close();

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return iUnidadMedida;
        }

        private decimal GetUnidadIdProductoPorIdProveedor(string idProveedor, string idProductproveedor, string sconn)
        {
            decimal idProducto;
            try
            {
                using (SqlConnection conn = new SqlConnection(sconn))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select ginv_es007_producto_idproducto from glog_e29_productoxproveedor WHERE idProductoProveedor = " + idProductproveedor + " and glog_e24_proveedor_idproveedor = " + idProveedor, conn))
                    {
                        command.CommandType = CommandType.Text;
                        idProducto = (decimal)command.ExecuteScalar();

                    }
                    conn.Close();

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return idProducto;
        }

        private DataTable GetInvoiceTable()
        {
            DataTable dtInvoice = new DataTable();

            dtInvoice.Columns.Add("nombrearchivo");
            dtInvoice.Columns.Add("fechaemi");
            dtInvoice.Columns.Add("tipodocumento");
            dtInvoice.Columns.Add("codigotipodocumento"); 
            dtInvoice.Columns.Add("numerodocumento");
            dtInvoice.Columns.Add("Base");
            dtInvoice.Columns.Add("Exonerado");
            dtInvoice.Columns.Add("Igv");
            dtInvoice.Columns.Add("Total");
            dtInvoice.Columns.Add("idProveedor");
            
            return dtInvoice;
        }

        private DataTable GetInvoiceLineTable()
        {
            DataTable dtInvoice = new DataTable();

            dtInvoice.Columns.Add("iddetalle");
            dtInvoice.Columns.Add("idproducto");
            dtInvoice.Columns.Add("nombre"); 
            dtInvoice.Columns.Add("idcantidad");
            dtInvoice.Columns.Add("idprecio");
            dtInvoice.Columns.Add("igv");
            dtInvoice.Columns.Add("porcentaje");
            dtInvoice.Columns.Add("idprecioigv");
            dtInvoice.Columns.Add("total");

            return dtInvoice;
        }

        private void SetInvoiceRow(ref DataRow drInvoice, Invoice oInvoice)
        {
            drInvoice["fechaemi"] = oInvoice.IssueDate;
            drInvoice["codigotipodocumento"] = oInvoice.InvoiceTypeCode;
            switch (oInvoice.InvoiceTypeCode)
            {
                case "01":
                    drInvoice["tipodocumento"] = "FACTURA";
                    break;
                case "03":
                    drInvoice["tipodocumento"] = "BOLETA";
                    break;
                case "07":
                    drInvoice["tipodocumento"] = "NOTA DE CREDITO";
                    break;
                default:
                    drInvoice["tipodocumento"] = "FACTURA";
                    break;
            }
            drInvoice["numerodocumento"] = oInvoice.ID; 
            List<UBLExtension> uBLExtensions = oInvoice.UBLExtensions.UBLExtension;
            List < AdditionalMonetaryTotal > oAdditionalMonetaryTotals  = uBLExtensions[1].ExtensionContent.AdditionalInformation.AdditionalMonetaryTotal;
            //drInvoice["nombrearchivo"] = uBLExtensions[0].ExtensionContent.DatosAdicionales.Documento.Nombre;
            drInvoice["Base"] = oAdditionalMonetaryTotals[0].PayableAmount.Text;
            drInvoice["Exonerado"] = 0;
            drInvoice["Igv"] = oInvoice.TaxTotal.TaxAmount.Text;
            drInvoice["Total"] = oInvoice.LegalMonetaryTotal.PayableAmount.Text;
        }
        
        private void SetInvoiceLineRow(ref DataRow drInvoiceLine, InvoiceLine oInvoiceLine)
        { 
            drInvoiceLine["idproducto"] = oInvoiceLine.ID;
            drInvoiceLine["nombre"] = oInvoiceLine.Item.Description;
            drInvoiceLine["idcantidad"] = oInvoiceLine.InvoicedQuantity.Text;
            drInvoiceLine["idprecio"] = oInvoiceLine.Price.PriceAmount.Text;
            drInvoiceLine["idprecioigv"] = oInvoiceLine.PricingReference.AlternativeConditionPrice.PriceAmount.Text;
            drInvoiceLine["igv"] = oInvoiceLine.TaxTotal.TaxAmount.Text;
            drInvoiceLine["porcentaje"] = oInvoiceLine.TaxTotal.TaxSubtotal.Percent;
            drInvoiceLine["total"] = Convert.ToDecimal(drInvoiceLine["idprecioigv"]) * Convert.ToDecimal(drInvoiceLine["idcantidad"]);

        }

        private Invoice DeserializeObject(string sFilename)
        {
            Invoice i;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Invoice));
                FileStream fs = new FileStream(sFilename, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                
                i = (Invoice)serializer.Deserialize(reader);

                fs.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return i;
        }

        private void ListarFactura()
        {
            RemoteData oRemote = new RemoteData();
            DataSet dsProveedor = null; 
            string sRUC = "1234567890";
            Invoice oInvoice;
            DataRow drInvoice;

            //modificado
            DataSet dsFactura = null;
            string sQuery = string.Empty;
            //modificado


            oRemote.ConnectionString = connString;

            oRemote.GetDataSetByQuery("SELECT * FROM glog_e24_proveedor WHERE idproveedor = " + idProveedor, "glog_e24_proveedors", ref dsProveedor);

            if (dsProveedor.Tables[0].Rows.Count > 0)
            {
                sRUC = dsProveedor.Tables[0].Rows[0]["ruc"].ToString();
            }
            
            dtFactura = new DataTable();
            dtFactura = GetInvoiceTable();

            foreach (string sFileName in Directory.GetFiles(sFullPath, sRUC + "*"))
            {
                FileInfo oFileInfo = new FileInfo(sFileName);
                oInvoice = DeserializeObject(sFileName);
                drInvoice = dtFactura.NewRow();
                SetInvoiceRow(ref drInvoice, oInvoice);

                //modificado
                sQuery = "SELECT * FROM glog_e25_nota_ingreso WHERE GLOG_E24_Proveedor_idProveedor = " + idProveedor + " and Serie ='" + drInvoice["numerodocumento"].ToString().Split('-')[0] + "' and numero = " + Convert.ToInt32(drInvoice["numerodocumento"].ToString().Split('-')[1]) + "";

                oRemote.GetDataSetByQuery(sQuery, "glog_e25_nota_ingreso", ref dsFactura);

                if (dsFactura.Tables[0].Rows.Count == 0)
                {
                    dtFactura.Rows.Add(drInvoice);
                    drInvoice["nombrearchivo"] = oFileInfo.Name;
                    drInvoice["idproveedor"] = idProveedor;
                }
                //modificado
            }
            

            this.gvFactura.DataSource = dtFactura;
            this.gvFactura.DataBind();
            
            if (dtFactura.Rows.Count <= 0)
            {
                this.gvFacturaDetalle.DataSource = null;
                this.gvFacturaDetalle.DataBind();
                
                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: showError('No Hay Registros'); ", true);
            }
            
        }
        
        private void ListarDetalleFactura(string sFileName)
        {
            Invoice oInvoice;
            DataRow drInvoiceLine;
            Int32 iLinea = 0;

            dtDetalleFactura = new DataTable();
            dtDetalleFactura = GetInvoiceLineTable();

            try
            {
                oInvoice = DeserializeObject(sFileName);

                foreach (InvoiceLine oInvoiceLine in oInvoice.InvoiceLine)
                {
                    drInvoiceLine = dtDetalleFactura.NewRow();
                    SetInvoiceLineRow(ref drInvoiceLine, oInvoiceLine);
                    drInvoiceLine["iddetalle"] = iLinea + 1;
                    dtDetalleFactura.Rows.Add(drInvoiceLine);
                    iLinea += 1;
                }
            }
            catch (Exception)
            { 
            }

            //dtOrdenes = dataTable;
            this.gvFacturaDetalle.DataSource = dtDetalleFactura;
            this.gvFacturaDetalle.DataBind();
        }

        #region Controles
        protected void gvFactura_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                //e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Haga clic en la última columna para ver detalle.";
            }
        }

        protected void gvFacturaDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = gvFactura.Columns.Count - 2;
            string pName = gvFactura.SelectedRow.Cells[9].Text;

            ListarDetalleFactura(sFullPath + pName);


        }
        #endregion

    }

    #region AccesoDatos

    public class RemoteData
    {
        public string ConnectionString { get; set; } 
         
        public void ExecuteQuery(string sQuery)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(sQuery, conn))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();

                    }  
                    conn.Close();

                }

            }catch(Exception e)
            {
                throw;
            }

        }

        public void GetDataSetByQuery(string sQuery, string sTable, ref DataSet dsResult)
        {
            SqlConnection DataConn = new SqlConnection(ConnectionString);

            DataConn.Open();

            SqlCommand DataCmd = new SqlCommand(sQuery, DataConn);
            DataCmd.CommandTimeout = 0;
            DataCmd.CommandType = CommandType.Text;

            dsResult = new DataSet();
            SqlDataAdapter DataAdapterResult;

            DataAdapterResult = new SqlDataAdapter(DataCmd);

            if (sTable.Length == 0)
                DataAdapterResult.Fill(dsResult);
            else
                DataAdapterResult.Fill(dsResult, sTable);

            DataAdapterResult = null;
            DataCmd.Dispose();
            DataConn.Close();
            DataConn.Dispose();

        }

        public void UpdateDataSet(string SQLQuery, ref DataSet DataSetSource, string RecordSource)
        {
            SqlConnection DataConn = new SqlConnection(ConnectionString);

            DataConn.Open();

            SqlTransaction DataTrans;
            DataTrans = DataConn.BeginTransaction();

            SqlCommand DataCmd = new SqlCommand(SQLQuery, DataConn);
            DataCmd.CommandType = CommandType.Text;
            DataCmd.Transaction = DataTrans;
            DataCmd.CommandTimeout = 0;
            try
            {
                SqlDataAdapter DataAdapterResult;

                DataAdapterResult = new SqlDataAdapter(DataCmd);

                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(DataAdapterResult);
                 
                DataAdapterResult.Update(DataSetSource, RecordSource);

                DataTrans.Commit();

                DataAdapterResult = null;
            }
            catch (Exception e)
            {
                DataTrans.Rollback();
                throw e;
            }
            finally
            {
                DataTrans.Dispose();
                DataConn.Close();
                DataCmd.Dispose();
                DataConn.Close();
            }
        }

        public DateTime GetServerDate()
        {
            DateTime dServerDate;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT GETDATE()",conn))
                    {
                        command.CommandType = CommandType.Text;
                        dServerDate = (DateTime)command.ExecuteScalar();

                    }
                    conn.Close();

                }

            }
            catch (Exception e)
            {
                throw;
            }
            return dServerDate;
        }



    }

    #endregion


}