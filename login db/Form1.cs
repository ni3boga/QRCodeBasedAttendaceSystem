using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace login_db
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            OleDbConnection cn = new OleDbConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //change source location if not working 
             OleDbConnection cn=new  OleDbConnection(@"provider=microsoft.ace.oledb.12.0;data source=C:\Users\nitin\Desktop\QRCode_Attendance\test.accdb");
            cn.Open();
            OleDbCommand cm = new OleDbCommand("select * from login", cn);
            OleDbDataReader dr = cm.ExecuteReader();
            if(dr.Read())
            {
                string a = dr["id"].ToString();
                string b = dr["password"].ToString();
                if (textBox1.Text==a&& textBox2.Text ==b)
                {
                    //MessageBox.Show("successfully logged in");
                    this.Hide();
                    main m = new main();
                    m.Show();
                }
                else
                {
                    MessageBox.Show("password incore");
                }

            }
            else
            {
                MessageBox.Show("something wrong");
            } 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
