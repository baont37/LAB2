using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace LAB2
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
        }
        string connectionStrings = "Server=localhost;Database=STUDENT_MANAGEMENT;User Id = sa; password=123456";
        SqlDataAdapter adapter = null;
        DataSet ds = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter("Select * from Student", connectionStrings);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "Student");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dtgStudent.DataSource = ds.Tables[0];
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataRow row = ds.Tables[0].NewRow();
            row[0] = txtStudentID.Text;
            row[1] = txtStudentName.Text;
            row[2] = txtClassID.Text;
            
            ds.Tables[0].Rows.Add(row);
            try
            {
                result = adapter.Update(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert failed \n" + ex.Message);
            }
            if (result > 0) MessageBox.Show("Insert Successfully");
        }

        int position = -1;
        private void dtgStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            position = e.RowIndex;
            if (position == -1)
            {
                MessageBox.Show("No row is selected");
                return;
            }

            DataRow row = ds.Tables[0].Rows[position];
            txtStudentID.Text = row[0].ToString();
            txtStudentName.Text = row[1].ToString();
            txtClassID.Text = row[2].ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (position == -1)
            {
                MessageBox.Show("No row is selected");
                return;
            }

            DataRow row = ds.Tables[0].Rows[position];
            row.BeginEdit();
            row[0] = txtStudentID.Text;
            row[1] = txtStudentName.Text;
            row[2] = txtClassID.Text;
            row.EndEdit();
            int result = adapter.Update(ds.Tables[0]);
            if (result > 0)
            {
                MessageBox.Show("Update successfully!!!");
            }
            else
            {
                MessageBox.Show("Update failed!!!");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (position == -1) return;

            DataRow row = ds.Tables[0].Rows[position];
            row.Delete();
            int result = adapter.Update(ds.Tables[0]);
            if (result > 0)
            {
                MessageBox.Show("Delete successfully!!!");
            }
            else
            {
                MessageBox.Show("Delete failed!!!");
            }
        }
    }
}
