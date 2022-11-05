using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myclients
{
    public partial class Usluge : Form
    {
        public Usluge()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = myClients; Integrated Security = True");
        void GetUslugaList()
        {
            SqlCommand c = new SqlCommand("exec ListaUsluga", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView.DataSource = dt;
        }
        private void Usluge_Load(object sender, EventArgs e)
        {
            GetUslugaList();
            FillCombo();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //dodavanje nove usluge
            if (txtID.Text != "" && txtNaziv.Text != "" && comboBox1.Text != "")
            {
                int UslugaID = int.Parse(txtID.Text);
                string Naziv = txtNaziv.Text, TipUsluge = comboBox1.Text;
                con.Open();
                SqlCommand c = new SqlCommand("exec Insert_Usluga '" + UslugaID + "','" + Naziv + "','" + TipUsluge + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                txtID.Text = "";
                txtNaziv.Text = "";
                comboBox1.Text = "";
                MessageBox.Show("Uspješno dodana nova usluga!");
                GetUslugaList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
       void FillCombo()
        {   
            //COMBOBOX
            SqlCommand c = new SqlCommand("Select * from Usluge", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "TipUsluge";
            comboBox1.ValueMember = "UslugaID";
        }
        private void btnUredi_Click(object sender, EventArgs e)
        {
            //Uređivanje usluge
            if (txtID.Text != "" && txtNaziv.Text != "" && comboBox1.Text != "")
            {
                int UslugaID = int.Parse(txtID.Text);
                string Naziv = txtNaziv.Text;
                string TipUsluge = comboBox1.Text;
                con.Open();
                SqlCommand c = new SqlCommand("exec Edit_Usluga'" + UslugaID + "','" + Naziv + "','" + TipUsluge + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                txtID.Text = "";
                txtNaziv.Text = "";
                comboBox1.Text = "";
                MessageBox.Show("Uspješno izmijenjena Usluga!");
                GetUslugaList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Izbriši   Uslugu
            if (txtID.Text != "" && txtNaziv.Text != "" && comboBox1.Text != "")
            {
                if (MessageBox.Show("Jeste li sigurni da želite izbrisati?", "Izbriši Uslugu.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int UslugaID = int.Parse(txtID.Text);
                    con.Open();
                    SqlCommand c = new SqlCommand("exec Delete_Usluga '" + UslugaID + "'", con);
                    c.ExecuteNonQuery();
                    con.Close();
                    txtID.Text = "";
                    txtNaziv.Text = "";
                    comboBox1.Text = "";
                    MessageBox.Show("Uspješno izbrisna Usluga!");
                    GetUslugaList();
                }
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Filtriraj prema nazivu
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = myClients; Integrated Security = True");
            DataTable dt = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter("SELECT * FROM Usluge WHERE Naziv LIKE '" + txtFilter.Text + "%'", con);
            sd.Fill(dt);
            dataGridView.DataSource = dt;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //selektiranje
            dataGridView.CurrentRow.Selected = true;
            txtID.Text = dataGridView.Rows[e.RowIndex].Cells["UslugaID"].Value.ToString();
            txtNaziv.Text = dataGridView.Rows[e.RowIndex].Cells["Naziv"].Value.ToString();
            comboBox1.Text = dataGridView.Rows[e.RowIndex].Cells["TipUsluge"].Value.ToString();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 back = new Form1();
            back.Show();
        }
    }
}
