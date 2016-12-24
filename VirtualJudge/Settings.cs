using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualJudge
{
    public partial class Settings : MetroFramework.Forms.MetroForm
    {
        public Settings()
        {
            InitializeComponent();
            GetElements();
        }

        private void GetElements()
        {
            Database d = new Database();
            string FirstName="";
            string userid = "";
            string lastName = "";
            string email= "";
            string sp = "";
            string cf = "";
            string uva = "";
            d.getUserInfo(ref userid, ref FirstName, ref lastName, ref email,ref sp,ref cf,ref uva);
            metroTextBox1.Text = userid;
            metroTextBox2.Text = FirstName;
            metroTextBox3.Text = lastName;
            metroTextBox6.Text = email;
            metroTextBox10.Text = sp;
            metroTextBox11.Text = cf;
            metroTextBox12.Text = uva;

            metroPanel1.Show();
            metroPanel10.Hide();

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Homepage home = new Homepage();
            this.Hide();
            home.Show();
        }


        private void metroPanel9_Click(object sender, EventArgs e)
        {
            Homepage home = new Homepage();
            this.Hide();
            home.Show();
        }

        private void metroPanel8_Click(object sender, EventArgs e)
        {
            metroPanel1.Hide();
            metroPanel10.Show();
        }

        private void metroPanel12_Click(object sender, EventArgs e)
        {
            if (metroTextBox2.Text != "")
            {
                if (metroTextBox3.Text != "")
                {
                    if (metroTextBox6.Text != "")
                    {
                        if (Functions.IsValidEmail(metroTextBox6.Text))
                        {
                            if (metroTextBox4.Text == metroTextBox5.Text)
                            {
                                Database d = new Database();
                                bool status = d.updateInformation(metroTextBox2.Text, metroTextBox3.Text, metroTextBox4.Text, metroTextBox5.Text, metroTextBox6.Text);

                                d.updateJudgeAccount(metroTextBox10.Text, metroTextBox11.Text, metroTextBox12.Text);

                                if (status == true)
                                {
                                    MessageBox.Show("Profile Updated Successfully !!");
                                    GetElements();
                                }
                                else
                                {
                                    MessageBox.Show("Not Updated !!");

                                }
                                this.Hide();
                                Homepage home = new Homepage();
                                home.Show();
                            }
                            else
                            {
                                MessageBox.Show("OOps!! Password Does't Match.");
                            }                          
                        }
                        else
                        {
                            MessageBox.Show("OOps!! Please Enter a Valid Email.");
                        }                   
                    }
                    else
                    {
                        MessageBox.Show("OOps!! Emails Cannot be Blank.");
                    }
                    
                }
                else
                {
                    MessageBox.Show("OOps!! Last Name Cannot be Blank.");
                }             
            }
            else
            {
                MessageBox.Show("OOps!! First Name Cannot be Blank.");
            }


            


        }

        private void metroPanel11_Click(object sender, EventArgs e)
        {
            metroPanel10.Hide();
            metroPanel1.Show();
        }
    }
}
