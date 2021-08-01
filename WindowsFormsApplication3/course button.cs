using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class course_button : UserControl
    {
        string empname;
        Form1 ffff;

        public course_button( string user,Form1 f1)
        {
            InitializeComponent();
            empname = user;
            ffff = f1;
        }
      /*  public string get_course()
        {
            return this.button1.Text;

        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            ffff.Hide();
            Form2 f2 = new Form2();
            f2.Show();
            f2.Form2_Load(sender, e, this.button1.Text, empname);
           

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ffff.Hide();
            Form2 f2 = new Form2();
            f2.Show();
            f2.Form2_Load(sender, e, this.button1.Text, empname);
           
        }
    }
}
