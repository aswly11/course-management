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
    public partial class Form2 : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-4PR9AGR\\SQLEXPRESS;Initial Catalog=omar and ahmed;Integrated Security=True");
        int userid = 0;
        int id1=0;
        string user_name;
        string course_name;
        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e,string namecaours,string user)
        {
            user_name = user;
            course_name = namecaours;
            con.Open();
           
            SqlCommand cmd2 = new SqlCommand(@"select EmployeeID from Employee
where EmployeeName= @user", con);
            cmd2.Parameters.Add(new SqlParameter("@user", user));
            using (SqlDataReader rdr2 = cmd2.ExecuteReader())
            {
                while (rdr2.Read())
                {
                    int id = (int)rdr2["EmployeeID"];
                    userid = id;

                }
            }
            if(userid==18)
            {
                txtdelORADD.Visible = true;
                btn_delete.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible=true;
            }
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand(@"select CourseID,CourseName ,StartDate,EndDate from Course
where CourseName= @cName;", con);
            cmd1.Parameters.Add(new SqlParameter("@cName", namecaours));
            using (SqlDataReader rdr1 = cmd1.ExecuteReader())
            {
                while (rdr1.Read())
                {
                    int cId = (int)rdr1["CourseID"];
                    id1 = cId;
                    string cName = (string)rdr1["CourseName"];
                    DateTime start = (DateTime)rdr1["StartDate"];
                    DateTime end = (DateTime)rdr1["EndDate"];
                    txt_cousre_Name.Text = cName;
                   dateTimePickerstart.Value = start;
                    dateTimePickerend.Value= end;
                }
               
            }
            con.Close();
    
         
            con.Open();

            int i = 0;
            SqlCommand cmd = new SqlCommand(@"select EmployeeName from Employee
inner join Emps_Courses
on Course_ID=@id
where emp_ID=EmployeeID", con);
            cmd.Parameters.Add(new SqlParameter("@id", id1));

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                UserControl2 CB = new UserControl2();
               // int Epmid = (int)rdr["EmployeeID"];
                var epmName = (string)rdr["EmployeeName"];
              //  var start = (DateTime)rdr["StartDate"];
            //    var end = (DateTime)rdr["EndDate"];
                CB.label1.Text = epmName;
                flowLayoutPanel1.Controls.Add(CB);
                i++;
                //MessageBox.Show("CourseID" + courseid.ToString() +"CourseName" + coursename);
            }

            con.Close();
            txt_num_of_emps.Text = i.ToString();
        }

 

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
      {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            int check_course_id = 0;
            int check_user_id = 0;
            SqlCommand cm = new SqlCommand(@"select emp_ID,Course_ID  from Emps_Courses
where emp_ID=@userid and Course_ID=@cid;", con);
            cm.Parameters.Add(new SqlParameter("@userid", userid));
            cm.Parameters.Add(new SqlParameter("@cid",id1 ));
            using (SqlDataReader rdr1 = cm.ExecuteReader())
            {
                while (rdr1.Read())
                {
                    int x = (int)rdr1["emp_ID"];
                    int y = (int)rdr1["Course_ID"];
                    check_course_id = x;
                    check_user_id = y;                    
                }
            }
            con.Close();
            if (check_course_id == 0 && check_user_id==0)
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("addEmpinCourse", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add(new SqlParameter("@namecourse",course_name));
                cmd3.Parameters.Add(new SqlParameter("@nameemployee",user_name ));
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Registed Successfully ^_^");
                con.Close();
                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();
                f2.Form2_Load( sender,  e, course_name, user_name);
            }
            else
            {
                MessageBox.Show("Already Involved !");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string x= txtdelORADD.Text;
            con.Open();

            SqlCommand cmd2 = new SqlCommand(@"select EmployeeID from Employee
where EmployeeName= @user", con);
            cmd2.Parameters.Add(new SqlParameter("@user", x));
            using (SqlDataReader rdr2 = cmd2.ExecuteReader())
            {
                while (rdr2.Read())
                {
                    int id = (int)rdr2["EmployeeID"];
                    userid = id;

                }
            }
            con.Close();
           con.Open();
           SqlCommand cmd4 = new SqlCommand("deleteempincourse", con);
           cmd4.CommandType = CommandType.StoredProcedure;
           cmd4.Parameters.Add(new SqlParameter("@cid", id1));
           cmd4.Parameters.Add(new SqlParameter("@user_id",userid));
           cmd4.ExecuteNonQuery();
           MessageBox.Show("Deleted Successfully ");
            con.Close();
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
            f2.Form2_Load(sender, e, course_name, user_name);

            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            bool isExist = false;
            con.Open(); 
            SqlCommand c = new SqlCommand(@"select EmployeeName from Employee
where EmployeeName= @emp_name;", con);
            c.Parameters.Add(new SqlParameter("@emp_name", txtdelORADD.Text));
            using (SqlDataReader rdr2 = c.ExecuteReader())
            {
                while (rdr2.Read())
                {
                    isExist = true;
                }
            }
            con.Close();
            if (!isExist)
            {
                MessageBox.Show("This Employee Not Exist !");
                return;
            }
           
            con.Open();
            SqlCommand cmd3 = new SqlCommand("addEmpinCourse", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@namecourse", course_name));
            cmd3.Parameters.Add(new SqlParameter("@nameemployee", txtdelORADD.Text));
            cmd3.ExecuteNonQuery();
            MessageBox.Show("Added Successfully ^_^");
            con.Close();
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
            f2.Form2_Load(sender, e, course_name, user_name);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            
            con.Open();
            SqlCommand cmd3 = new SqlCommand("deleteingcourse", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@Coursename", course_name));
            cmd3.Parameters.Add(new SqlParameter("@courseid",id1));
            cmd3.ExecuteNonQuery();
            MessageBox.Show("deleted Successfully ");
            con.Close();
            this.Hide();
            Form1 f1 = new Form1();
            f1.Form1_Load(sender, e, user_name);
            f1.Show();
        }
        string xx="";
        private void button5_Click(object sender, EventArgs e)
        {

           
            if(button5.Text=="Edit Course")
            {
                
            xx= txt_cousre_Name.Text;
            
            txt_cousre_Name.ReadOnly = false;
            txt_cousre_Name.BackColor = Color.LightGreen;
            dateTimePickerstart.IsAccessible = true;
           // txt_start_data.BackColor = Color.LightGreen;
            dateTimePickerend.IsAccessible = true;
          //  txt_End_date.BackColor = Color.LightGreen;
            button5.Text = "Save";
            
            }else{
                button5.Text = "Edit Course";
                DateTime s=dateTimePickerstart.Value;
                DateTime end = dateTimePickerend.Value;
                if (txt_cousre_Name.Text == "")
                {
                    MessageBox.Show("Full All Feilds please ..");
                    return;
                
                }
               con.Open();
               SqlCommand cmd3 = new SqlCommand("updateCourse", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@oldcourseName",xx));
            cmd3.Parameters.Add(new SqlParameter("@CourseName", txt_cousre_Name.Text));
            cmd3.Parameters.Add(new SqlParameter("@startdate",s ));
            cmd3.Parameters.Add(new SqlParameter("@enddate",end ));
            cmd3.ExecuteNonQuery();
            con.Close();
            this.Hide();
            Form2 f2 = new Form2();
            f2 = this;           
            f2.Form2_Load(sender, e, course_name, user_name);
            f2.Show();
       
            }
               }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Form1_Load(sender, e, user_name);
            f1.Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
