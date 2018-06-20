using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalIntranet.Controllers
{
    public class ConciliarMercaderiaController : Controller
    {
        static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefContext"].ConnectionString;

        // GET: ConciliarMercaderia
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MantConciliarMercaderia()
        {
            RemoteData oRemoteData = new RemoteData();
            DataSet dsProveedores = null;


            ViewBag.fechActual = System.DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Title = "CONCILIAR MERCADERIA PROVEEDOR";


            oRemoteData.ConnectionString = connString;

            oRemoteData.GetDataSetByQuery("SELECT idproveedor, RazonSocial FROM glog_e24_proveedor WHERE facelectronico = 1 ORDER BY RazonSocial", "glog_e24_proveedor", ref dsProveedores);

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = "0", Text = "-", Selected = false });
            foreach (DataRow drProveedor in dsProveedores.Tables[0].Rows)
            {
                items.Add(new SelectListItem() { Value = drProveedor["idproveedor"].ToString(), Text = drProveedor["RazonSocial"].ToString(), Selected = false });
            }

            ViewBag.Proveedores = items;


            return View();
        }
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

            }
            catch (Exception e)
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
                    using (SqlCommand command = new SqlCommand("SELECT GETDATE()", conn))
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