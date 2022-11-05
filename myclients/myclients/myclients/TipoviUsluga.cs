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
    public partial class TipoviUsluga : Form
    {
        public TipoviUsluga()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = myClients; Integrated Security = True");
        void GetTipList()
        {
            SqlCommand c = new SqlCommand("exec ListaTip", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView.DataSource = dt;
        }
        
        private void TipoviUsluga_Load(object sender, EventArgs e)
        {
            GetTipList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //dodavanje novog Tipa
            if (txtID.Text != "" && txtNaziv.Text != "")
            {
                int TipID = int.Parse(txtID.Text);
                string Naziv = txtNaziv.Text;
                con.Open();
                SqlCommand c = new SqlCommand("exec Insert_TU'" + TipID + "','" + Naziv + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                txtID.Text = "";
                txtNaziv.Text = "";
                MessageBox.Show("Uspješno dodan novi Tip Usluge!");
                GetTipList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUredi_Click(object sender, EventArgs e)
        {
            //uređivanje tipova
            if (txtID.Text != "" && txtNaziv.Text != "")
            {
                int TipID = int.Parse(txtID.Text);
                string Naziv = txtNaziv.Text;
                con.Open();
                SqlCommand c = new SqlCommand("exec Edit_Tipovi '" + TipID + "','" + Naziv + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                txtID.Text = "";
                txtNaziv.Text = "";
                MessageBox.Show("Uspješno izmijenjen Tip Usluge!");
                GetTipList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Izbriši Tip Usluge
            if (txtID.Text != "" && txtNaziv.Text != "")
            {
                if (MessageBox.Show("Jeste li sigurni da želite izbrisati?", "Izbriši Tip Usluge.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int TipID = int.Parse(txtID.Text);
                    con.Open();
                    SqlCommand c = new SqlCommand("exec Delete_Tip'" + TipID + "'", con);
                    c.ExecuteNonQuery();
                    con.Close();
                    txtID.Text = "";
                    txtNaziv.Text = "";
                    MessageBox.Show("Uspješno izbrisan Tip Usluge!");
                    GetTipList();
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
            SqlDataAdapter sd = new SqlDataAdapter("SELECT * FROM TipoviUsluga WHERE Naziv LIKE '" + txtFilter.Text + "%'", con);
            sd.Fill(dt);
            dataGridView.DataSource = dt;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //selektiraj
            dataGridView.CurrentRow.Selected = true;
            txtID.Text = dataGridView.Rows[e.RowIndex].Cells["TipID"].Value.ToString();
            txtNaziv.Text = dataGridView.Rows[e.RowIndex].Cells["Naziv"].Value.ToString();
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
