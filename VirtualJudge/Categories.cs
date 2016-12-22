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
    }
}
