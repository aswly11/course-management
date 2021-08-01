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
    public partial class adminFUN : Form
    {


        SqlConnection con = new SqlConnection(@"Server=DESKTOP-4PR9AGR\SQLEXPRESS;DataBase=omar and ahmed;Integrated Security=True");
       
        string old_name;
        string path="A";
        public adminFUN()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void adminFUN_Load(object sender, EventArgs e,string name,string dep,string currpos,string email,string phone,string qualif,string pic)
        {
            if( pic!="A")
            pictureBox1.Image = Image.FromFile(pic);

            txt_name.Text = name;
            txt_dep.Text = dep;
            txt_cr_position.Text = currpos;
            txt_email.Text = email;
            txt_phone.Text = phone;
            txt_qualifi.Text = qualif;
            old_name = txt_name.Text;
            con.Open();
            string query = "select EmployeeID from Employee where EmployeeName = @empname";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@empname", txt_name.Text));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {

                while (rdr.Read())
                {
                    int id = (int)rdr["EmployeeID"];
                    if (id == 18)
                    {
                        cmb_functions.Items.Add("Show All Courses");
                        cmb_functions.Items.Add("Show All Employees");
                        
                    }
                    else
                    {
                        cmb_functions.Items.Add("Show All Courses");
                    }
                }
                con.Close();
            }
         
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            string [] arr1=new string[7];
            string[] arr2 = new string[7];
            if (button3.Text == "Edit") {

                button2.Visible = true;
                button3.Text = "Save";
                txt_name.ReadOnly = false;
                txt_dep.ReadOnly = false;
                txt_email.ReadOnly = false;
                txt_cr_position.ReadOnly = false;
                txt_phone.ReadOnly = false;
                txt_qualifi.ReadOnly = false;
                txt_name.BackColor = Color.Gold;
                txt_dep.BackColor = Color.Gold;
                txt_email.BackColor = Color.Gold;
                txt_cr_position.BackColor = Color.Gold;
                txt_phone.BackColor = Color.Gold;
                txt_qualifi.BackColor = Color.Gold;
                arr1[0] = txt_name.Text;
                arr1[1] = txt_dep.Text;
                arr1[2] = txt_email.Text;
                arr1[3] = txt_cr_position.Text;
                arr1[4] = txt_phone.Text;
                arr1[5] = txt_qualifi.Text;
                arr1[6] = path;
            }
            else
            {

                button3.Text = "Edit";
                button2.Visible = false;
                txt_name.ReadOnly =true;
                txt_dep.ReadOnly = true;
                txt_email.ReadOnly = true;
                txt_cr_position.ReadOnly = true;
                txt_phone.ReadOnly = true;
                txt_qualifi.ReadOnly = true;
                txt_name.BackColor = Color.PaleTurquoise;
                txt_dep.BackColor = Color.PaleTurquoise;
                txt_email.BackColor = Color.PaleTurquoise;
                txt_cr_position.BackColor = Color.PaleTurquoise;
                txt_phone.BackColor = Color.PaleTurquoise;
                txt_qualifi.BackColor = Color.PaleTurquoise;
                arr2[0] = txt_name.Text;
                arr2[1] = txt_dep.Text;
                arr2[2] = txt_email.Text;
                arr2[3] = txt_cr_position.Text;
                arr2[4] = txt_phone.Text;
                arr2[5] = txt_qualifi.Text;
                arr2[6] = path;
                bool is_change=false;
                for (int i=0; i < 7; i++)
                {

                    if (arr1[0]!=arr2[0])
                    {
                        is_change = true;
                    }

                }
                if (is_change)
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("updatemployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@oldname", old_name));
                    cmd.Parameters.Add(new SqlParameter("@empName", txt_name.Text));
                    cmd.Parameters.Add(new SqlParameter("@dep", txt_dep.Text));
                    cmd.Parameters.Add(new SqlParameter("@currentPosition", txt_cr_position.Text));
                    cmd.Parameters.Add(new SqlParameter("@Email", txt_email.Text));
                    cmd.Parameters.Add(new SqlParameter("@phone", txt_phone.Text));
                    cmd.Parameters.Add(new SqlParameter("@qualifications", txt_qualifi.Text));
                    cmd.Parameters.Add(new SqlParameter("@pic", path));
                    //   cmd.Parameters.Add(new SqlParameter("@password", txt_password.Text));

                    cmd.ExecuteNonQuery();
                    con.Close();
  
                }       
            }
        
        }
        public string getName()
        {
            return txt_name.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmb_functions.Text == "Show All Courses")
            {
                
                this.Hide();
                Form1 f1 = new Form1();
                f1.Form1_Load(sender, e, this.getName());
                f1.Show();
                con.Open();
                 string name = this.getName();
                string query1 = "select EmployeeID from Employee where EmployeeName = @empname";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.Parameters.Add(new SqlParameter("@empname", name));
                int id = 0;
                using (SqlDataReader rdr1 = cmd1.ExecuteReader())
                {

                    while (rdr1.Read())
                    {
                        id = (int)rdr1["EmployeeID"];
                        if (id == 18)
                        {
                          f1.btnADDCourse.Visible = true;
                        
                           
                        }

                    }
                    if (id != 18)
                    {
                        
                       f1. btnADDCourse.Visible = false;

                    }

                    con.Close();
                }
         

              //  f1.Form1_Load(sender,e);

            }
            else if (cmb_functions.Text == "Show All Employees")
            {
                Form3 f3 = new Form3();
                f3.Form3_Load(sender, e, txt_name.Text);
                f3.Show();
                this.Hide();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Image|*.jpg;*.png";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(of.FileName);
                path = of.FileName.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Form3_Load(sender, e, txt_name.Text);
            f3.Show();
            this.Hide();
        }

        private void adminFUN_Load(object sender, EventArgs e)
        {

        }

       

       
    }
}
