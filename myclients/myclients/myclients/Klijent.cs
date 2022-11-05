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
    public partial class Klijent : Form
    {
        public Klijent()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = myClients; Integrated Security = True");
    
        void GetKlList()
        {
            SqlCommand c = new SqlCommand("exec ListaKlijent", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView.DataSource = dt;
        }
        private void Klijent_Load(object sender, EventArgs e)
        {
            GetKlList();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            //dodavanje novog klijenta
            if (textID.Text != "" && txtOib.Text != "" && txtName.Text != "" && textAdresa.Text != "")
            {
                int klijentid = int.Parse(textID.Text);
                long klijentOib = long.Parse(txtOib.Text);
                string klnaziv = txtName.Text, kladresa = textAdresa.Text, klmail = textMail.Text;
                con.Open();
                SqlCommand c = new SqlCommand("exec InsertKlijent'" + klijentid + "','" + klnaziv + "','" + klijentOib + "','" + kladresa + "','" + klmail + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                textID.Text = "";
                txtName.Text = "";
                txtOib.Text = "";
                textAdresa.Text = "";
                textMail.Text = "";
                MessageBox.Show("Uspješno dodan novi Klijent!");
                GetKlList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnUredi_Click(object sender, EventArgs e)
        {
            //Uređivanje podataka
            if (textID.Text != "" && txtOib.Text != "" && txtName.Text != "" && textAdresa.Text != "")
            {
                int klijentid = int.Parse(textID.Text);
                long klijentOib = long.Parse(txtOib.Text);
                string klnaziv = txtName.Text, kladresa = textAdresa.Text, klmail = textMail.Text;
                con.Open();
                SqlCommand c = new SqlCommand("exec Editklijent'" + klijentid + "','" + klnaziv + "','" + klijentOib + "','" + kladresa + "','" + klmail + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                textID.Text = "";
                txtName.Text = "";
                txtOib.Text = "";
                textAdresa.Text = "";
                textMail.Text = "";
                MessageBox.Show("Uspješno izmijenjen Klijent!");
                GetKlList();
            }
            else
            {
                MessageBox.Show("Niste ispunili sva polja!", "Prazno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Izbriši klijenta
            if (textID.Text != "" && txtOib.Text != "" && txtName.Text != "" && textAdresa.Text != "")
            {
                if (MessageBox.Show("Jeste li sigurni da želite izbrisati?", "Izbriši Klijenta.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int klijentid = int.Parse(textID.Text);
                    con.Open();
                    SqlCommand c = new SqlCommand("exec Deleteklijent '" + klijentid + "'", con);
                    c.ExecuteNonQuery();
                    con.Close();
                    textID.Text = "";
                    txtName.Text = "";
                    txtOib.Text = "";
                    textAdresa.Text = "";
                    textMail.Text = "";
                    MessageBox.Show("Uspješno izbrisan Klijent!");
                    GetKlList();
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
            SqlDataAdapter sd = new SqlDataAdapter("SELECT * FROM Klijenti WHERE Naziv LIKE '" + txtFilter.Text + "%'", con);
            sd.Fill(dt);
            dataGridView.DataSource = dt;
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //selektiranje iz tablice, prikazivanje u textbox
            dataGridView.CurrentRow.Selected = true;
            textID.Text = dataGridView.Rows[e.RowIndex].Cells["KlijentID"].Value.ToString();
            txtName.Text = dataGridView.Rows[e.RowIndex].Cells["Naziv"].Value.ToString();
            txtOib.Text = dataGridView.Rows[e.RowIndex].Cells["OIB"].Value.ToString();
            textAdresa.Text = dataGridView.Rows[e.RowIndex].Cells["Adresa"].Value.ToString();
            textMail.Text = dataGridView.Rows[e.RowIndex].Cells["Mail"].Value.ToString();
        }
        private void btnCjenik_Click(object sender, EventArgs e)
        {
            //prikazivanje cjenika u novoj formi
            int klijentid = int.Parse(textID.Text);
            Cjenik myForm1 = new Cjenik(klijentid);
            myForm1.Show();
            Visible = false;
        }
    

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 back = new Form1();
            back.Show();
        }
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
 }
