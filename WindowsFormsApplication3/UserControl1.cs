using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApplication3
{
    public partial class UserControl1 : UserControl
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-4PR9AGR\\SQLEXPRESS;Initial Catalog=omar and ahmed;Integrated Security=True");
        Form3 f33 = new Form3();
        string user;
        public UserControl1(Form3 f3,string user_name)
        {
            f33 = f3;
            user = user_name;
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

      

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.WhiteSmoke;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {

            button1.BackColor = Color.Transparent;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            adminFUN ad = new adminFUN();
            con.Open();
            SqlCommand cmd = new SqlCommand("getdataByNAme", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@user_name", this.button1.Text));


            //SqlDataReader rdr = cmd.ExecuteReader();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                // iterate through results, printing each to console
                string xx = "non";
                string yy = "non";

                while (rdr.Read())
                {
                    string x = (string)rdr["UserName"];
                    xx = x;
                    string y = (string)rdr["Password"];
                    yy = y;
                    string pic = (string)rdr["Picture_path"];
                    string empname = (string)rdr["EmployeeName"];
                    string dep = (string)rdr["Department"];
                    string currpos = (string)rdr["CurrentPostion"];
                    string email = (string)rdr["Email"];
                    string phone = (string)rdr["Phone"];
                    string qualif = (string)rdr["Qualification"];
                    ad.adminFUN_Load(sender, e, empname, dep, currpos, email, phone, qualif, pic);
                    f33.Hide();
                    ad.Show();
                 ad.button4.Visible = true;
                }
                con.Close();



            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int id=0;
            con.Open();
            SqlCommand cmd2 = new SqlCommand(@"

  select EmployeeID from Employee
  where EmployeeName=@empName", con);           
            cmd2.Parameters.Add(new SqlParameter("@empName", this.button1.Text));
            using (SqlDataReader rdr1 = cmd2.ExecuteReader())
                {

                    while (rdr1.Read())
                    {
                        id = (int)rdr1["EmployeeID"];
                        
                    }
            }
          //  cmd3.ExecuteReader();
            con.Close();
            con.Open();
            SqlCommand cmd3 = new SqlCommand("deleteEmployee", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@empName", this.button1.Text));
            cmd3.Parameters.Add(new SqlParameter("@id", id));
            cmd3.ExecuteNonQuery();
            MessageBox.Show("deleted Successfully ");
            con.Close();
            Form3 f2=new Form3();
            f2.Show();
            f2.Form3_Load(sender, e, user);

        }
    }
}
