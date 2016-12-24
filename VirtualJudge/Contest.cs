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
    public partial class Contest : MetroFramework.Forms.MetroForm
    {
        public Contest()
        {
            InitializeComponent();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            UpcomingContest uc = new UpcomingContest();
            uc.Show();
            this.Hide();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            ContestHistory contesthistory = new ContestHistory();
            contesthistory.Show();
            this.Hide();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            RunningContest runningcontest = new RunningContest();
            runningcontest.Show();
            this.Hide();
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
    }
}
