using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

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
    }
}