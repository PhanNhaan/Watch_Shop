using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using APIWatchShop.Models;

namespace APIWatchShop.Database
{
    public class Database
    {
        public static DataTable ReadTable(string StoredProcedureName, Dictionary<string, object> para = null)
        {
            try
            {
                DataTable resultTable = new DataTable();

                string SQLConnectionString = ConfigurationManager.ConnectionStrings["WSConnection"].ConnectionString;
                SqlConnection connection = new SqlConnection(SQLConnectionString);

                connection.Open();

                SqlCommand sqlcmd = connection.CreateCommand();
                sqlcmd.Connection = connection;
                sqlcmd.CommandText = StoredProcedureName;
                sqlcmd.CommandType = CommandType.StoredProcedure;

                if (para != null)
                {
                    foreach (KeyValuePair<string, object> data in para)
                    {
                        if (data.Value == null)
                        {
                            sqlcmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            sqlcmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = sqlcmd;
                sqlDA.Fill(resultTable);

                connection.Close();

                return resultTable;
            }
            catch
            {
                return null;
            }
        }

        public static object Exec_Command(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["WSConnection"].ConnectionString;
            object result = null;
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Start a local transaction.

                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                cmd.Parameters.Add("@CurrentID", SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["@CurrentID"].Value;
                    // Attempt to commit the transaction.
                }
                catch 
                {
                    result = null;
                }
                conn.Close();

            }
            
            return result;
        }

        public static NguoiDung DangNhap(string TenDangNhap, string MatKhau)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("TenDangNhap", TenDangNhap);
            param.Add("MatKhau", MatKhau);

            DataTable tb = Database.ReadTable("Proc_DangNhap", param);
            Models.NguoiDung kq = new Models.NguoiDung();

            if (tb.Rows.Count > 0)
            {
                kq.MAND = tb.Rows[0]["MAND"].ToString();
                kq.TENND = tb.Rows[0]["TENND"].ToString();
                kq.TENDN = tb.Rows[0]["TENDN"].ToString();
                kq.EMAIL = tb.Rows[0]["EMAIL"].ToString();
                kq.QUYEN = tb.Rows[0]["QUYEN"].ToString();
                kq.PASS = tb.Rows[0]["PASS"].ToString();
                kq.SDT = tb.Rows[0]["SDT"].ToString();

            }
            else
                kq.MAND = null;
            //return Ok("Chào mừng bạn đến với Web API!");
            return kq;


        }
    }
}