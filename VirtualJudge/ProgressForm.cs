using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualJudge
{
    public partial class ProgressForm : MetroFramework.Forms.MetroForm
    {
        private string judgeName; 
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void Progress(string judge)
        {
            this.judgeName = judge;

            if (judgeName == "Codeforces")
            {
                backgroundWorker1.RunWorkerAsync();
                RefreshDatabaseUVA("Codeforces");
            }
            else if (judgeName == "UVA")
            {
                backgroundWorker1.RunWorkerAsync();
                RefreshDatabaseUVA("UVA");

            }
        }

        private void RefreshDatabaseCodeforces(string Judge)
        {
            try
            {

                WebClient client = new WebClient();
                string reply = client.DownloadString("http://codeforces.com/api/problemset.problems?");
                System.IO.File.WriteAllText(@"E:\MY_WORKPLACE\C#\VirtualJudge\problems\Codeforces.json", reply);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RefreshDatabaseUVA(string Judge)
        {
            try
            {
                WebClient client = new WebClient();
                string reply = client.DownloadString("http://uhunt.felix-halim.net/api/p");
                System.IO.File.WriteAllText(@"E:\MY_WORKPLACE\C#\VirtualJudge\problems\UVA.json", reply);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (i == 0)
                    {
                        simulateHeavyJob();
                    }
                        
                    backgroundWorker1.ReportProgress(i);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBar1.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metroLabel1.Text="Problem Database Updated!";
        }

        private void simulateHeavyJob()
        {
            RefreshDatabaseCodeforces(judgeName);
        }
    }
}
