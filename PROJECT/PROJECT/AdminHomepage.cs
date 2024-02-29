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
    public partial class AdminHomepage : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();

        public AdminHomepage()
        {
            InitializeComponent();
        }

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
            cmd.CommandText = "select F_Nr, F_Abflug, F_Ankunft, F_Datum from Flüge where F_Datum >= DATE()";
            cmd.Connection = con;
            da.SelectCommand = cmd;
            ds.Clear();
            try
            {
                da.Fill(ds, "Accounts");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Accounts";
                spaltenformatierung();
            }
            catch (Exception a)
            {
                MessageBox.Show("Datenbanköffnungsfehler\n" + a);
                this.Close();
            }
        }


        private void AdminHomepage_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = FlughafenDB.accdb";
                con.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show("Datenbanköffnungsfehler\n" + a);
                this.Close();
            }
            nachsteflugedatagridwiew();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var Flugzeuge_Verwalten = new Flugzeuge_Verwalten();
            Flugzeuge_Verwalten.Location = this.Location;
            Flugzeuge_Verwalten.StartPosition = FormStartPosition.Manual;
            Flugzeuge_Verwalten.FormClosing += delegate { this.Show(); };
            Flugzeuge_Verwalten.Show();
            this.Hide();
        }
    }
}
