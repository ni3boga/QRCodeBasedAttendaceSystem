using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using System.Data.OleDb;

namespace login_db
{
    public partial class Emplyeelog : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        string decoded;
        OleDbConnection cr = new OleDbConnection(@"provider=microsoft.ace.oledb.12.0;data source=C:\Users\nitin\Desktop\QRCode_Attendance\Details.accdb");
        public Emplyeelog()
        {
            InitializeComponent();
        }

        private void Emplyeelog_Load(object sender, EventArgs e)
        {

            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            // comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        //    try
        //    {
        //        BarcodeReader Reader = new BarcodeReader();
        //        Result result = Reader.Decode((Bitmap)pictureBox1.Image);
        //        decoded = result.ToString().Trim();
        //        textBox1.Text = decoded;

        //        if (decoded != "")
        //        {
        //            timer1.Stop();
        //            runa();


        //        }


        //    }
        //    catch (Exception aa)
        //    {
        //    }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                BarcodeReader Reader = new BarcodeReader();
                Result result = Reader.Decode((Bitmap)pictureBox1.Image);
                decoded = result.ToString().Trim();
                //textBox1.Text = decoded;
                //MessageBox.Show(decoded);
                if (decoded != null)
                {

                    //runa();
                    cr.Open();

                    String z = "SELECT * FROM Edetails WHERE emp_id =" + decoded;
                    try
                    {
                        OleDbCommand cm = new OleDbCommand(z, cr);
                        OleDbDataReader dr = cm.ExecuteReader();

                        if (dr.Read() == true)
                        {
                            string a = dr["emp_name"].ToString();


                            string b = DateTime.Now.ToString("h:mm:ss tt");


                            String s = DateTime.Now.ToString("MM/dd/yyyy");

                            MessageBox.Show("" + "Name=" + a + "     " +" current time="+ b + "     "+" Date=" + s);
                            
                            try
                            {

                                OleDbCommand v = new OleDbCommand();
                                v.CommandType = CommandType.Text;
                             
                               // v.CommandText = "Insert into Attendance(emp_id,Name,date_of_entering,time) values('" + decoded + "','" + a + "','" + s + "','" + b + "') ";

                                v.CommandText = "Insert into Attendance(emp_id,Name,date_of_entering,Time1) values('" + decoded + "','" + a + "','" + s + "','" + b + "')";
                                v.Connection = cr;
                                v.ExecuteNonQuery();

                                MessageBox.Show("Record Submitted", "Congrats");

                                cr.Close();
                            }
                            catch (Exception exp)
                            {
                                MessageBox.Show("invalid" + exp, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            
                           

                        }
                        else
                        {
                            MessageBox.Show("not exist");
                        }

                        cr.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if(decoded ==null)
                {
                    MessageBox.Show("Error in qr code");
                    timer1.Stop();

                }


            }
            catch (Exception aa)
            {
            }
        }
        private void buttonshowa_Click(object sender, EventArgs e)
        {

        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();

            }
            catch (Exception e)
            {
                MessageBox.Show("invalid detection");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }




        private void runa()
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            module m = new module();
            m.Show();
        }
    }

}
