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
    public partial class Categories : MetroFramework.Forms.MetroForm
    {
        public Categories()
        {
            InitializeComponent();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            this.Hide();
            homepage.Show();
            
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
