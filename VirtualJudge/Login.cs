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

namespace VirtualJudge
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
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

            Database d = new Database();
            d.UserLogin(metroTextBox1.Text,metroTextBox2.Text);
            Homepage home = new Homepage();
            home.Show();
            this.Hide();
        }
    }
}
