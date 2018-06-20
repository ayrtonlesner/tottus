using System;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

namespace LocalIntranet.reportes.gestionRequerimiento
{
    public partial class MantDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               
                DataTable dt = new DataTable();
                //SqlTransaction tr;
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["DefContext"].ConnectionString;

                SqlConnection cn = new SqlConnection(conString);
                cn.Open();

                //tr = cn.BeginTransaction(IsolationLevel.Serializable);

                SqlCommand cmd = new SqlCommand("Ver_Dashboard", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@FecDesde", SqlDbType.DateTime).Value = Convert.ToDateTime(Request.QueryString["anio"].ToString());
                //cmd.Parameters.Add("@FecHasta", SqlDbType.DateTime).Value = Convert.ToDateTime(Request.QueryString["mes"].ToString());

                cmd.Parameters.Add("@accion", SqlDbType.VarChar, 10).Value = "CABECERA";
                cmd.Parameters.Add("@anio", SqlDbType.VarChar, 10).Value = Request.QueryString["anio"].ToString();
                cmd.Parameters.Add("@mes", SqlDbType.VarChar, 5).Value = Request.QueryString["mes"].ToString();
           

                //string cadena, cadena1;
                //cadena = Request.QueryString["flgProveedor"].ToString();
                //cadena1 = Request.QueryString["nombreProveedor"].ToString();

                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    //resultado = 1;
                    //tr.Commit();

                    this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reportes/gestionRequerimiento/rptMantDashboard.rdlc");
                    
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = dt;

                    this.ReportViewer1.LocalReport.EnableHyperlinks = true;

                    //ReportParameter p1 = new ReportParameter("param1", DateTime.Now.ToLongTimeString());
                    //ReportParameter p2 = new ReportParameter("param2", Request.QueryString["fechaInicio"].ToString());
                    //ReportParameter p3 = new ReportParameter("param3", Request.QueryString["fechaFin"].ToString());


                    //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });

                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
                    this.ReportViewer1.LocalReport.Refresh();

                    //this.ReportViewer1.Drillthrough += new DrillthroughEventHandler(resumenResumenMateriaPrimaDetalle);

                }
                catch (Exception ex)
                {
                    //tr.Rollback();
                    //this.lblMensajeError.Text = ex.Message;
                    //resultado = 0;
                    //str_error = ex.Message;
                }
                finally
                {
                    cn.Dispose();
                    cmd.Dispose();
                }
            }

        }
    }
}