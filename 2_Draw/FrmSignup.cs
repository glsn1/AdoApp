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
    public partial class FrmSignup : Form
    {
        public FrmSignup()
        {
            InitializeComponent();
            btnSave.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Db.Add(txtName.Text, txtSurname.Text, txtTicketNo.Text);
            btnSave.Enabled = false;
            btnCheck.BackgroundImage = Properties.Resources.close;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            txtTicketNo.Text = rnd.Next(100000, 1000000).ToString();
        }

        private void txtTicketNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTicketNo.Text.Length == 6)
            {
                if (Db.Check(txtTicketNo.Text))
                {
                    btnCheck.BackgroundImage = Properties.Resources.check;
                    btnSave.Enabled = true;

                }

                else
                {
                    btnCheck.BackgroundImage = Properties.Resources.close;
                    btnSave.Enabled = false;
                }

            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Db.GridFill();
        }

        private void btnFrmShow_Click(object sender, EventArgs e)
        {
            FrmResult frmResult = new FrmResult();
            frmResult.ShowDialog();
        }
    }
}
