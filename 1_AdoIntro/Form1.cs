using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_AdoIntro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            /*
             Ado.Net : Projenin veritabanı bağlantısı kurarak Crud işlemlerler yapılması saylayan kütüphanedir.
            Kütüphane adı : 
            System.Data ve 
            System.Data.SqlClient(MSSQL).

            Ado.net projesi için adımlar
            1-kütüphane ekleniecek
             -System.Data ve 
             -System.Data.SqlClient(MSSQL).

            2-Sınıflarından nesneler üretilir
                -SqlConnection conn : connection(bağlantı)
                -SqlDataAdapter da : select tablosunu tutar
                -SqlCommand cmd : sql komutu çalışır
                -SqlDataReader dr : veri almaya yarar.

                DataSet ds;Veri tabloları
                DataTable dt:Veri Tablosu

             */

            /*
             DataGridView Property
            -AutoSizeColumnMode : Fill
            BackgrounColor : Control
            ContextMenuStrip : ContextMenuStrip1
            Modifiers : Public
            MultiSelect : false
            ReadOnly : true
            SelectionMode : FullRowSelect
            
             */
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles =  false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37,37,38);//Siyah
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            dgv.BackgroundColor = Color.FromArgb(240,240,240);//Açık gri
            groupBox1.Visible = false;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Db.GridFill();
        }

        bool isAdd = false;
        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isAdd = true;
            groupBox1.Visible = true;
            groupBox1.Text = "Ekle";
        }
        int id;
        private void değiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isAdd = false;
            groupBox1.Visible = true;
            groupBox1.Text = "Güncelle";
            id = (int)dgv.CurrentRow.Cells[0].Value;
            txtName.Text = dgv.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = dgv.CurrentRow.Cells[2].Value.ToString();
            txtCity.Text = dgv.CurrentRow.Cells[3].Value.ToString();
            txtCountry.Text = dgv.CurrentRow.Cells[4].Value.ToString();

        }

        private void çıkarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            id = (int)dgv.CurrentRow.Cells[0].Value;
            DialogResult dr = MessageBox.Show("Silmek İstediğinizden Emin misiniz?","Uayrı",MessageBoxButtons.YesNo,MessageBoxIcon.Error);
            if(dr == DialogResult.Yes)
                Db.DeleteEmployee(id);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isAdd)
                Db.AddEmployee(txtName.Text, txtSurname.Text, txtCity.Text, txtCountry.Text);
            else
                Db.UpdateEmployee(id, txtName.Text, txtSurname.Text, txtCity.Text, txtCountry.Text);

            txtName.Text = txtSurname.Text= txtCity.Text = txtCountry.Text = "";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string param = $"where FirstName like '%{txtSearch.Text}%' or LastName like '%{txtSearch.Text}%' or City like '%{txtSearch.Text}%' or Country like '%{txtSearch.Text}%'";
            Db.GridFill(param);
        }
    }
}
