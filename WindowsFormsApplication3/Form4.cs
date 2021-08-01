using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApplication3
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-4PR9AGR\\SQLEXPRESS;Initial Catalog=omar and ahmed;Integrated Security=True");

      
        public Form4()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
          
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            con.Open();
            SqlCommand c = new SqlCommand(@"select EmployeeName,UserName from Employee
where EmployeeName=@emp_name or UserName=@u_mame;", con);
            c.Parameters.Add(new SqlParameter("@emp_name", txt_Name.Text));
            c.Parameters.Add(new SqlParameter("@u_mame", txt_user_name.Text));

            string xxx = "";
            string yyy = "";
            using (SqlDataReader rdr = c.ExecuteReader())
            {

                while (rdr.Read())
                {
                   xxx = (string)rdr["EmployeeName"];
                   yyy = (string)rdr["UserName"];
                }
            }
            con.Close();

            if(xxx!=""||yyy!="")
            {

                MessageBox.Show("This Username Or Name Is Already Exist ..");
                return;
            }
            if (txt_Name.Text==""||txt_dep.Text==""||txt_cr_pos.Text==""||txt_email.Text==""||txt_phone.Text==""||txt_qualifi.Text==""||txt_phone.Text==""||txt_user_name.Text==""||txt_password.Text=="")
            {
                MessageBox.Show("Full All Feilds , Please !");
                return;
            }
           
            if (txt_password.Text != txt_pass_confirm.Text)
            {
                MessageBox.Show("Enter The Same Password");
                return;
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("AddingEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@empName", txt_Name.Text));
            cmd.Parameters.Add(new SqlParameter("@dep", txt_dep.Text));
            cmd.Parameters.Add(new SqlParameter("@currentPosition", txt_cr_pos.Text));
            cmd.Parameters.Add(new SqlParameter("@Email", txt_email.Text));
            cmd.Parameters.Add(new SqlParameter("@phone", txt_phone.Text));
            cmd.Parameters.Add(new SqlParameter("@qualifications", txt_qualifi.Text));
            cmd.Parameters.Add(new SqlParameter("@userName", txt_user_name.Text));
            cmd.Parameters.Add(new SqlParameter("@password", txt_password.Text));
           
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Registered Successfully  ^_^");
            login lg = new login();
            this.Hide();
            lg.Show();

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login lg = new login();
            lg.Show();

        }
    }
}
