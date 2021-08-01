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
    public partial class Form3 : Form
    {

        SqlConnection con = new SqlConnection(@"Server=DESKTOP-4PR9AGR\SQLEXPRESS;DataBase=omar and ahmed;Integrated Security=True");
      
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControl1 u = new UserControl1(this, username);
            flowLayoutPanel1.Controls.Add(u);

        }
        string username;
        public void Form3_Load(object sender, EventArgs e,string user_name)
        {

            con.Open();
            username = user_name;
            string query = "select * from Employee ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                UserControl1 us = new UserControl1(this, username);
                int empeid = (int)rdr["EmployeeID"];
                var empname = (string)rdr["EmployeeName"];
              //  var start = (DateTime)rdr["StartDate"];
              //  var end = (DateTime)rdr["EndDate"];
                us.button1.Text = empname;
                flowLayoutPanel1.Controls.Add(us);
                //MessageBox.Show("CourseID" + courseid.ToString() +"CourseName" + coursename);
            }
            con.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            adminFUN ad = new adminFUN();
            con.Open();
            SqlCommand cmd = new SqlCommand("getdataByNAme", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@user_name", "ahmed gamal"));


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
                    this.Hide();
                    ad.Show();

                }
                con.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
