using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_AdoIntro
{
    internal static class Db
    {
        /*Server ->Data Source
         Database ->Initial Catolog
        Trusted_Connection -> integrated Security 
        true->SSPI

        sa(server Authonticationb) ile bağlanmak miçin
        UserId/UID = sa
        Password = 1234

        conn = new SqlConnection("Server = 103S-EGTMN\\MSSQLSERVER01;Database = Northwind;User Id = sa;Password=1234");
         */
        static SqlConnection conn = new SqlConnection("Data Source=103S-EGTMN\\MSSQLSERVER01;Initial Catalog=Northwind;Integrated Security=True");
        
        static SqlDataAdapter da;
        static DataTable dt;
        static DataSet ds;
        static SqlCommand cmd;
        static SqlDataReader dr;

        static string cmdText;
        static Form1 f = (Form1)Application.OpenForms["Form1"];

        public static void GridFill(string param = null)
        {
            conn.Open();
            cmdText = $"select EmployeeID,FirstName,LastName,City,Country from Employees {param}";
            da = new SqlDataAdapter(cmdText,conn);
            //ds = new DataSet();
            dt = new DataTable();

            //da.Fill(ds);//da.Fill(ds,"Calısanlar")
            da.Fill(dt);

            //f.dgv.DataSource = ds.Tables[0];//ds.Tables["Calısanlar"]
            f.dgv.DataSource = dt;
            conn.Close();
            GridEdit();
        }

        private static void GridEdit()
        {
            DataGridViewColumn column_id = f.dgv.Columns[0],column_name = f.dgv.Columns[1], column_surname = f.dgv.Columns[2], column_city = f.dgv.Columns[3], column_country = f.dgv.Columns[4];
            column_id.HeaderText ="ID";
            column_name.HeaderText ="ADI";
            column_surname.HeaderText ="SOYAD";
            column_city.HeaderText ="ŞEHİR";
            column_country.HeaderText ="ÜLKE";

            column_id.DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleCenter;

            column_id.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        public static void AddEmployee(string name,string surname,string city,string country)
        {
            conn.Open();
            cmdText = "insert Employees(FirstName,LastName,City,Country)\r\nvalues(@name,@surname,@city,@country)";

            //cmdText = $"insert Employees(FirstName,LastName,City,Country)\r\nvalues('{name}','{surname}','{city}','{country}')";

            cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@name",name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@country", country);

            cmd.ExecuteNonQuery();
            conn.Close();
            GridFill();
        }

        public static void UpdateEmployee(int id, string name, string surname, string city, string country)
        {
            conn.Open();
            cmdText = "update Employees set FirstName = @name,LastName = @surname,City=@city,Country = @country where EmployeeID = @id";

            cmd= new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@id",id); 
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@country", country);

            cmd.ExecuteNonQuery();
            conn.Close();
            GridFill();
        }

        public static void DeleteEmployee(int id)
        {
            conn.Open();
            cmdText = $"delete Employees where EmployeeID = {id}";
            cmd = new SqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            GridFill();
        }

    }
}
