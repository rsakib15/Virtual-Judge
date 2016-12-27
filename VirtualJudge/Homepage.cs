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
    public partial class Homepage : MetroFramework.Forms.MetroForm
    {

        public Homepage()
        {
            InitializeComponent();
            IntialElements();
        }

        public void IntialElements()
        {
            if (Session.getLoggedName() == null)
            {
                metroTile13.Hide();
                metroTile14.Hide();
                metroPanel1.Hide();
            }

            else if (Session.getLoggedName() != null)
            {
                metroTile13.Show();
                metroTile14.Show();
                metroTile11.Hide();
                metroTile12.Hide();
                metroPanel1.Show();
                metroLink1.Text = "Username : " + Session.getLoggedName();
                metroLink2.Text = "Rank : "+ " 0";
            }
        }
     
        private void metroTile1_Click(object sender, EventArgs e)
        {
            ProblemList problemlist = new ProblemList();
            problemlist.Show();
            this.Hide();
        }

        private void metroTile12_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Show();
            this.Hide();
        }


        private void metroTile6_Click(object sender, EventArgs e)
        {
            Codes codes = new Codes();
            codes.Show();
            this.Hide();
        }

        private void metroTile10_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            this.Hide();
        }

        private void metroTile11_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Contest contest = new Contest();
            contest.Show();
            this.Hide();
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Hide();
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();form.Show();
            this.Hide();

        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            Categories categories = new Categories();
            this.Hide();
            categories.Show();
        }

        private void metroTile13_Click(object sender, EventArgs e)
        {
            Session.setUserName(null);
            Homepage home = new Homepage();
            home.Show();
            this.Hide();
        }

        private void metroTile9_Click(object sender, EventArgs e)
        {
            if (Session.getLoggedName() == null)
            {
                MessageBox.Show("Please Login First !!");
            }
            else
            {
                Settings settings = new Settings();
                settings.Show();
                this.Hide();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            const string message = "Are you sure to Exit the Application?";
            const string caption = "Exit Virtual Judge";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Environment.Exit(0);
            else
                e.Cancel = (result == DialogResult.No);
        }

        private void metroTile8_Click(object sender, EventArgs e)
        {
            Rank rank = new Rank();
            rank.Show();
            this.Hide();
        }
    }
}