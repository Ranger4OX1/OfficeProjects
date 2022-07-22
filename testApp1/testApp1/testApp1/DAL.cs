using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//NEW
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;


namespace testApp1
{
    internal class DAL
    {
        public static SqlConnection GetConnection()
        {
            string sql = @"Data Source = .\SQLEXPRESS;
                            Initial Catalog = test;
                            Integrated Security = true ";
            SqlConnection conn = new SqlConnection(sql);
            try
            {
                conn.Open();
                MessageBox.Show("Sql Connected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connection error\n" + ex.Message,"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //throw;
            }
            return conn;
        }
    }
}
