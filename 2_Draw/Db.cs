using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_Draw
{
    internal static class Db
    {
        static SqlConnection conn = new SqlConnection("Data Source=103S-EGTMN\\MSSQLSERVER01;Initial Catalog=Draw;Integrated Security=True");
        static SqlDataAdapter da;
        static SqlCommand cmd;
        static SqlDataReader dr;

        static DataTable dt;
        static FrmSignup fs = (FrmSignup)Application.OpenForms["FrmSignup"];

        static string cmdText;

        public static void GridFill()
        {
            conn.Open();
            cmdText = "select * from Info ";
            da = new SqlDataAdapter(cmdText,conn);
            dt = new DataTable();
            da.Fill(dt);
            fs.dgv.DataSource = dt;
            conn.Close();
        }

        public static void Add(string name,string surname,string ticketNo)
        {
            conn.Open();
            cmdText = $"insert Info values('{name}','{surname}','{ticketNo}')";
            cmd = new SqlCommand(cmdText,conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            GridFill();
        }

        public static bool Check(string ticketNo)
        {
            int count = 0;
            conn.Open();
            cmdText = $"select count(*) from Info where TicketNo = '{ticketNo}'";

            cmd = new SqlCommand(cmdText,conn);
            dr = cmd.ExecuteReader();//cmd.ExecuteScalar()

            while(dr.Read())
            {
                count = dr.GetInt32(0);//(int)dr[0].ToString()
            }
            conn.Close();

            if (count > 0) 
                return false;
            else
                return true;
            
        }
        public static List<string> user = new List<string>();
        public static void Get(string ticketNo)
        {
            conn.Open();
            cmdText = $"select Name,Surname from Info where TicketNo = '{ticketNo}'";

            cmd = new SqlCommand(cmdText, conn);
            dr = cmd.ExecuteReader();//cmd.ExecuteScalar()

            while (dr.Read())
            {
                user.Add(dr.GetString(0));
                user.Add(dr.GetString(1));
            }
            conn.Close();
        }
    }
}
