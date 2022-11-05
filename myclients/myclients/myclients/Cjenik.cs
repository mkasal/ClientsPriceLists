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
    public partial class Cjenik : Form
    {
        int klijentId;
        public Cjenik(int y)
        {
            InitializeComponent();
            klijentId = y;
        }
        SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = myClients; Integrated Security = True");
        
        private void Cjenik_Load(object sender, EventArgs e)
        {
            GetCijenikList();
        }
        void GetCijenikList()
        {
            SqlCommand c = new SqlCommand("exec ListaCijena'" +klijentId+ "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //selektiranje cijene u textbox
            dataGridView1.CurrentRow.Selected = true;
            txtCijena.Text = dataGridView1.Rows[e.RowIndex].Cells["Cijena"].Value.ToString();
            txtUsluga.Text = dataGridView1.Rows[e.RowIndex].Cells["UslugaID"].Value.ToString();
            txtKlijent.Text = dataGridView1.Rows[e.RowIndex].Cells["KlijentID"].Value.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //dodavanje nove cijene
            if (txtCijena.Text != "" && txtKlijent.Text != "" && txtUsluga.Text != "" )
            {
                int cijena = int.Parse(txtCijena.Text);
                int klijent = int.Parse(txtKlijent.Text);
                int usluga = int.Parse(txtUsluga.Text);
                con.Open();
                SqlCommand c = new SqlCommand("exec DodajCijenu'" + klijent + "','" + usluga + "', '" + cijena + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                txtCijena.Text = "";
                txtUsluga.Text = "";
                txtKlijent.Text = "";
                MessageBox.Show("Uspješno dodana nova cijena!");
                GetCijenikList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUredi_Click(object sender, EventArgs e)
        {
            //uređivanje cijene
            if (txtCijena.Text != "" && txtKlijent.Text != "" && txtUsluga.Text != "")
            {
                int cijena = int.Parse(txtCijena.Text);
                int klijent = int.Parse(txtKlijent.Text);
                int usluga = int.Parse(txtUsluga.Text);
                con.Open();
                SqlCommand c = new SqlCommand("exec EditCijena '" + klijent + "','" + usluga + "', '" + cijena + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                txtCijena.Text = "";
                txtUsluga.Text = "";
                txtKlijent.Text = "";
                MessageBox.Show("Uspješno izmijenjena cijena!");
                GetCijenikList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Izbriši cijenu
            if (txtCijena.Text != "" && txtKlijent.Text != "" && txtUsluga.Text != "")
            {
                if (MessageBox.Show("Jeste li sigurni da želite izbrisati?", "Izbriši Cijenu.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int cijena = int.Parse(txtCijena.Text);
                    int klijent = int.Parse(txtKlijent.Text);
                    int usluga = int.Parse(txtUsluga.Text);
                    con.Open();
                    SqlCommand c = new SqlCommand("exec Deletecijena '" + cijena + "', '" + klijent + "', '" + usluga + "'", con);
                    c.ExecuteNonQuery();
                    con.Close();
                    txtCijena.Text = "";
                    txtUsluga.Text = "";
                    txtKlijent.Text = "";
                    MessageBox.Show("Uspješno izbrisana cijena!");
                    GetCijenikList();
                }
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Klijent back = new Klijent();
            back.Show();
        }

      
    }
}
