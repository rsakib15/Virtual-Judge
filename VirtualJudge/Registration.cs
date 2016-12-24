using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using System.Data.SqlClient;

namespace VirtualJudge
{
    public partial class Registration : MetroFramework.Forms.MetroForm
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Homepage home = new Homepage();
            home.Show();
            this.Hide();
        }

        
        bool IsValidUserName(string Username)
        {
            try
            {
                Database d = new Database();
                return d.CheckAvailableUser(Username);
            }
            catch
            {
                return false;
            }
        }

        private void metroPanel9_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserNameBox.Text != "")
                {
                    if (IsValidUserName(UserNameBox.Text))
                    {
                        if (FirstNameBox.Text != "")
                        {
                            if (LastNameBox.Text != "")
                            {
                                if (PasswordOneBox.Text != "" || PasswordTwoBox.Text !="")
                                {
                                    if (PasswordOneBox.Text == PasswordTwoBox.Text)
                                    {
                                        if (Functions.IsValidEmail(EmailBox.Text))
                                        {
                                            Database d = new Database();
                                            d.AddUser(UserNameBox.Text, FirstNameBox.Text, LastNameBox.Text, PasswordOneBox.Text, PasswordTwoBox.Text, EmailBox.Text);
                                            MessageBox.Show("Registration Successful !!!");
                                            Homepage home = new Homepage();
                                            d.createAccount(UserNameBox.Text);                                       
                                            this.Hide();
                                            home.Show();
                                        }
                                        else
                                        {
                                            MessageBox.Show("OOps!! Please Enter a Valid Email.");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("OOps!! Password Doesn't Match.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("OOps!! Please Enter a Password.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("OOps!! Please Enter a Last Name.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("OOps!! Please Enter a First Name.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("OOps!! UserName Already Taken.");
                    }
                }
                else
                {
                    MessageBox.Show("OOps!! Please Enter a Valid Username");
                }                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Homepage home = new Homepage();
            home.Show();
            this.Hide();
        }
    }
}
