
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
        }

        void GetStudent()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=dbSchool.accdb");
            da = new OleDbDataAdapter("SELECT *FROM Student", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Student");
            dataGridView1.DataSource = ds.Tables["Student"];
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudent();
        }

        private void btnInsert_Click(object sender, EventArgs e)//INSERT BUTTON
        {
            string query = "Insert into Student (FirstName,LastName) values (@fName,@lName)";
            cmd = new OleDbCommand(query, con);
            cmd.Parameters.AddWithValue("@fName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@lName", txtLastName.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GetStudent();
        }

        private void btnDelete_Click(object sender, EventArgs e)//DELETE BUTTON
        {
            string query = "Delete From Student Where Id=@id";
            cmd = new OleDbCommand(query, con);
            cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GetStudent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)//UPDATE BUTTON
        {
            string query = "Update Student Set FirstName=@fName,LastName=@lName Where Id=@id";
            cmd = new OleDbCommand(query, con);
            cmd.Parameters.AddWithValue("@ad", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@soyad", txtLastName.Text);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GetStudent();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtFirstName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
