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
    public partial class login : Form
    {


        SqlConnection con = new SqlConnection(@"Server=DESKTOP-4PR9AGR\SQLEXPRESS;DataBase=omar and ahmed;Integrated Security=True");
        public login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            txt_password.PasswordChar = '*';
            txt_password.ForeColor = Color.Black;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txt_user_name.ForeColor = Color.Black;

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            txt_user_name.Text = "";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            txt_password.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // bool isvalid = false;

            con.Open();
            SqlCommand cmd = new SqlCommand("login_Validation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@user_name", txt_user_name.Text));
           // cmd.Parameters.Add(new SqlParameter("@password", txt_password.Text));
           
            //SqlDataReader rdr = cmd.ExecuteReader();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                // iterate through results, printing each to console
                string xx="non";
                string yy="non";

                while (rdr.Read())
                {
                 string   x = (string)rdr["UserName"];
                 xx = x;
                 string   y = (string)rdr["Password"];
                 yy = y;
                 string pic = (string)rdr["Picture_path"];
                 string empname = (string)rdr["EmployeeName"];
                 string dep = (string)rdr["Department"];
                 string currpos = (string)rdr["CurrentPostion"];
                 string email = (string)rdr["Email"];
                 string phone = (string)rdr["Phone"];
                 string qualif = (string)rdr["Qualification"];
                    if ((x == txt_user_name.Text &&phone==txt_password.Text)||(x == txt_user_name.Text&&y == txt_password.Text))
                    {

                        adminFUN ad = new adminFUN();
                        this.Hide();
                        ad.Show();
                        ad.adminFUN_Load(sender, e, empname, dep, currpos, email, phone, qualif,pic);
                         con.Close();
                         break;
                    }
                   
                }
                if (xx != txt_user_name.Text && yy != txt_password.Text)
                    {
                        label2.Visible = true;
                        txt_user_name.Text = "";
                        txt_password.Text = "";
                    }
            }
             //UserControl2 CB = new UserControl2();
                // int Epmid = (int)rdr["EmployeeID"];
               //  x = (string)rdr["UserName"];
                 // y = (string)rdr["Password"];
                //    var end = (DateTime)rdr["EndDate"];
            //    CB.label1.Text = epmName;
            //    flowLayoutPanel1.Controls.Add(CB);
                //MessageBox.Show("CourseID" + courseid.ToString() +"CourseName" + coursename);
                 // cmd.ExecuteScalar();
           
         
            con.Close();
        }

       

       
   
        private void login_Load(object sender, EventArgs e)
        {   
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            txt_user_name.Text = "";
            txt_user_name.ForeColor = Color.Black;
        }

        private void textBox2_Enter_1(object sender, EventArgs e)
        {
            txt_password.Text = "";
            txt_password.ForeColor = Color.Black;
            txt_password.PasswordChar='*';
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter your Phone Instead Of Password ..");

        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Gold;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Honeydew;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {

            label4.ForeColor = Color.Gold;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Honeydew;
        }
    }
}
