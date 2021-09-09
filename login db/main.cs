using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Imaging;

namespace login_db
{
    public partial class main : Form
    {
        OleDbConnection cr = new OleDbConnection(@"provider=microsoft.ace.oledb.12.0;data source=C:\Users\nitin\Desktop\QRCode_Attendance\Details.accdb");
        public main()
        {

            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            module ms = new module();

            ms.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void btn_Show_Click(object sender, EventArgs e)
        {
            //String a = DateTime.Now.ToString("h:mm:ss tt");
            //MessageBox.Show(a);

            pannel_del.Visible = false;
            Newu.Visible = false;
            panel2.Visible = true;
            
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            pannel_del.Visible = false;
            panel2.Visible = false;
            Newu.Visible = true;
        }

      

        

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                cr.Open();   
                OleDbCommand crm = cr.CreateCommand();
                    crm.CommandText = "Insert into Edetails(emp_id,Emp_name,Phone_no,sal,Date_of_join) values('" + tb_id.Text + "','" + tbname.Text + "','" + tbphone.Text + "','" + tbsal.Text + "','" + tbdate.Text + "')";
                    crm.ExecuteNonQuery();

                    MessageBox.Show("Record Submitted", "Congrats");

                    cr.Close();
                }
            catch (Exception exp)
            {
                MessageBox.Show("invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            tb_id.Text = "";
            tbname.Text = "";
            tbphone.Text = "";
            tbsal.Text = "";
            tbdate.Text = "";

           

        }

        

        
        private void main_Load(object sender, EventArgs e)
        {
            datepickatt.Format = DateTimePickerFormat.Custom;
            datepickatt.CustomFormat = "MM/dd/yyyy";
        }

        private void Show_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("main", datepickatt.Text);
            pannel_del.Visible = false;
           
            try
            {
                cr.Open();
                OleDbCommand crm = cr.CreateCommand();

                crm.CommandText = "Select * from Attendance where date_of_entering='" + datepickatt.Text+"'";
                
                crm.ExecuteNonQuery();
                OleDbDataAdapter da = new OleDbDataAdapter(crm);
                DataTable dt = new DataTable();
               
                da.Fill(dt);
                view.DataSource = dt;
              
                cr.Close();
            }
            catch (Exception r)
            {
                MessageBox.Show("invalid credantial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            Newu.Visible = false;
            pannel_del.Visible = true;


        }

        private void datepickatt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



            //cr.Open();
            //OleDbCommand crm = cr.CreateCommand();

            //crm.CommandText = "Select * from Edetails where Date_of_join='" + datepickatt.Text + "'";
            //crm.ExecuteNonQuery();
            //OleDbDataAdapter da = new OleDbDataAdapter(crm);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
            //view.DataSource = dt;
            //string sa = dt.Rows.Count.ToString();                             
            //MessageBox.Show(sa); calculate rows
            //cr.Close();


            cr.Open();
            String z = "SELECT * FROM Edetails WHERE emp_id =" + tbget.Text;
            try
            {
                OleDbCommand cm = new OleDbCommand(z, cr);
                OleDbDataReader dr = cm.ExecuteReader();
                if (dr.Read() == true)
                {
                    string a = dr["emp_id"].ToString();

                    if (tbget.Text == a)
                    {
                        try
                        {
                            String ca = "DELETE FROM Edetails WHERE emp_id = " + tbget.Text;
                            OleDbCommand cmc = new OleDbCommand(ca, cr);
                            cmc.ExecuteNonQuery();
                            MessageBox.Show("Successfully Deleted ", "Confirmation ", MessageBoxButtons.OK);
                            tbget.Text = "";
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("enter correct id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }


                }
                else
                {
                    MessageBox.Show("enter correct id");
                }
            }
            catch (Exception eq)
            {
                MessageBox.Show("empty ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
                cr.Close();
               
                
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String z = "SELECT * FROM Edetails WHERE emp_id =" + tb_id.Text;

            try
            {
                cr.Open();
                OleDbCommand cm = new OleDbCommand(z, cr);
                OleDbDataReader dr = cm.ExecuteReader();
                if (dr.Read() != true)
                {
                    if (tb_id.Text != "" && tbsal.Text != "" && tbphone.Text != "" && tbname.Text != "")
                    {
                        using (SaveFileDialog sdf = new SaveFileDialog() { Filter = "BMP|*,*", ValidateNames = true })
                        {
                            if (sdf.ShowDialog() == DialogResult.OK)
                            {
                                Newu.Visible = false;

                                MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
                                encoder.QRCodeScale = 8;
                                Bitmap bmp = encoder.Encode(tb_id.Text);
                                pictureBox2.Image = bmp;
                                bmp.Save(sdf.FileName, ImageFormat.Bmp);

                            }
                            panelShow.Visible = true;
                            cr.Close();
                        }
                        btnSave.Enabled = true;
                    }

                    else
                    {
                        MessageBox.Show("fill information properly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("user id already exist");
                }
            }
            catch (Exception asa)
            {
                MessageBox.Show("Error in values");
            }
            }

        private void Exit1_Click(object sender, EventArgs e)
        {
            panelShow.Hide();
            Newu.Visible = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //}
    }
    
