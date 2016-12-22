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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Homepage home = new Homepage();
            home.Show();
            this.Hide();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Database d = new Database();
                d.AddUser(metroTextBox1.Text, metroTextBox2.Text, metroTextBox3.Text, metroTextBox4.Text, metroTextBox5.Text, metroTextBox6.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            
        }
    }
}
