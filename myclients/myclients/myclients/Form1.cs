using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myclients
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnKlijent_Click(object sender, EventArgs e)
        {
            Klijent myForm1 = new Klijent();
            myForm1.Show();
            Visible = false;
        }

        private void btnUsluge_Click(object sender, EventArgs e)
        {
            Usluge myForm2 = new Usluge();
            myForm2.Show();
            Visible = false;
        }

        private void btnTipovi_Click(object sender, EventArgs e)
        {
            TipoviUsluga myForm3 = new TipoviUsluga();
            myForm3.Show();
            Visible = false;
        }
    }
}
