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
    public partial class Form1 : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-4PR9AGR\\SQLEXPRESS;Initial Catalog=omar and ahmed;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }
        string username;
        public void Form1_Load(object sender, EventArgs e, string user)
        {

            username = user;
            con.Open();
            string query = "select * from course ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                course_button CB = new course_button(user, this);
                int courseid = (int)rdr["CourseID"];
                var coursename = (string)rdr["CourseName"];
                var start = (DateTime)rdr["StartDate"];
                var end = (DateTime)rdr["EndDate"];
                CB.button1.Name = coursename;
                CB.button1.Text = coursename;
                flowLayoutPanel2.Controls.Add(CB);
                //MessageBox.Show("CourseID" + courseid.ToString() +"CourseName" + coursename);
            }

            con.Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {



        }

        int i = 1;
        private void button2_Click(object sender, EventArgs e)
        {


            course_button CB = new course_button(username, this);
            CB.button1.Text = "New Course" + i.ToString();
            flowLayoutPanel2.Controls.Add(CB);
            con.Open();
            SqlCommand cmd3 = new SqlCommand("AddingCourse", con);
            cmd3.Parameters.Add(new SqlParameter ("@CourseName",CB.button1.Text));
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.ExecuteNonQuery();
            MessageBox.Show("Added Successfully");
            this.Hide();
            Form1 f1 = new Form1();
            f1.Form1_Load(sender, e, username);
            f1.Show();
            con.Close();
            i++;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminFUN ad = new adminFUN();
            con.Open();
            SqlCommand cmd = new SqlCommand("getdataByNAme", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@user_name", username));


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

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
