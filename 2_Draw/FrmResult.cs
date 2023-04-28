using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_Draw
{
    public partial class FrmResult : Form
    {
        public FrmResult()
        {
            InitializeComponent();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            txtTicketNo.Text = rnd.Next(100000, 1000000).ToString();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            Db.Get(txtTicketNo.Text);
            txtName.Text = Db.user[0];
            txtSurname.Text = Db.user[1];
        }
    }
}
