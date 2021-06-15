using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LogicLayer
{
    public class Connector
    {
        #region  singelton ctor
        private static Connector instance = null;
        private static readonly object padlock = new object();
        private Connector() { }
        public static Connector GetInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Connector();
                    }
                }
                return instance;
            }
        }
        #endregion

        /**
         * receive server name and two comboBoxs: "from" and "to" .
         * they denote the coins to exhange.
         * return value: string array of the minimum and maximum Rate change.
         */
        public String[] connectSqlServer(string serverName,string from,string to)
        {
            String[] rates=new String[2];
            try
            {
                SqlConnection sqlConnection = new SqlConnection(serverName);
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    if (from.Length == 0 || to.Length == 0)
                    {
                        rates[0] = "ERROR";
                        rates[1]= "please choosse coins from the list";
                        return rates;
                    }
                    to = "USD-" + to;

                    string query = @"select minimum,maximum" +
                        " from Currency.dbo.Currency_pairs" +
                        " where trading_pairs='" + to + "'";

                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        rates[0] = dr.GetSqlSingle(0).ToString();
                        rates[1] = dr.GetSqlSingle(1).ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                rates[0] = "ERROR";
                rates[1] = "ex.Message";
                return rates;
            }


            return rates;
        }
    }
}
