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

namespace PROJECT
{
    public partial class Flugzeuge_Verwalten : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommandBuilder odcb = null;

        private void spaltenformatierung()
        {
            dataGridView1.Columns.Remove("F_Nr");
            DataGridViewTextBoxColumn tb0 = new DataGridViewTextBoxColumn();
            tb0.DataPropertyName = "F_Nr";
            tb0.HeaderText = "Flugnummer";
            tb0.DisplayIndex = 0;
            dataGridView1.Columns.Add(tb0);

            dataGridView1.Columns.Remove("F_Abflug");
            DataGridViewTextBoxColumn tb1 = new DataGridViewTextBoxColumn();
            tb1.DataPropertyName = "F_Abflug";
            tb1.HeaderText = "Abflug";
            tb1.DisplayIndex = 1;
            dataGridView1.Columns.Add(tb1);

            dataGridView1.Columns.Remove("F_Ankunft");
            DataGridViewTextBoxColumn tb2 = new DataGridViewTextBoxColumn();
            tb2.DataPropertyName = "F_Ankunft";
            tb2.HeaderText = "Ankunft";
            tb2.DisplayIndex = 2;
            dataGridView1.Columns.Add(tb2);

            dataGridView1.Columns.Remove("F_Datum");
            DataGridViewTextBoxColumn tb3 = new DataGridViewTextBoxColumn();
            tb3.DataPropertyName = "F_Datum";
            tb3.HeaderText = "Datum";
            tb3.DisplayIndex = 3;
            dataGridView1.Columns.Add(tb3);
        }

        void nachsteflugedatagridwiew()
        {
            cmd.CommandText = "select * from Flugzeuge";
            cmd.Connection = con;
            da.SelectCommand = cmd;
            ds.Clear();
            try
            {
                da.Fill(ds, "Accounts");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Accounts";
                //spaltenformatierung();
            }
            catch (Exception a)
            {
                MessageBox.Show("Datenbankfehler:\n" + a);
                this.Close();
            }
        }

        public Flugzeuge_Verwalten()
        {
            InitializeComponent();
        }

        private void Flugzeuge_Verwalten_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = FlughafenDB.accdb";
                con.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show("Updatefehler:\n" + a);
                this.Close();
            }        
                nachsteflugedatagridwiew();
                odcb = new OleDbCommandBuilder(da);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.Update(ds, "Accounts");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*bool txt1 = string.IsNullOrWhiteSpace(textBox1.Text);
            if (txt1 != true)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dataGridView1.DataSource;
                bs.Filter = "FZ_Maxpassagier like '%" + textBox1.Text + "%' " +
                              "OR FZ_Art like '%" + textBox1.Text + "%'" +
                              "OR FZ_ID like '%" + textBox1.Text + "%'" +
                              "OR FZ_Maxgewicht like '%" + textBox1.Text + "%'";
                dataGridView1.DataSource = bs;
                
            }*/
            /*try
            {               
                var bindData = (DataSet)dataGridView1.DataSource;
                var rows = bindData.Select(string.Format($"FZ_Art LIKE '%{textBox1.Text}%' AND FZ_Maxpassagier LIKE '%{textBox1.Text}%' AND FZ_ID LIKE '%{textBox1.Text}%' AND FZ_Maxgewicht LIKE '%{textBox1.Text}%'"));
                dataGridView1.DataSource = rows.CopyToDataTable();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
    }
}
