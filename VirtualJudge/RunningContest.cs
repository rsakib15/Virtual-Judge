using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json.Linq;

namespace VirtualJudge
{
    public partial class RunningContest : MetroFramework.Forms.MetroForm
    {
        public RunningContest()
        {
            InitializeComponent();
            ProblemJSON();
        }

        private void ProblemJSON()
        {
            WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("http://codeforces.com/api/contest.list");
            object webData = System.Text.Encoding.UTF8.GetString(raw);
            JObject results = JObject.Parse(webData.ToString());
            JObject res = new JObject();

            for (int i = 0; i < 100; i++)
            {
                res = JObject.Parse(results["result"][i].ToString());
              
                if (Convert.ToInt64(res["relativeTimeSeconds"].ToString()) >= 0 && Convert.ToInt32(res["relativeTimeSeconds"].ToString()) <= Convert.ToInt32(res["durationSeconds"].ToString()))
                {
                    metroGrid1.Rows.Add(res["id"].ToString(), res["name"].ToString(), Functions.calculateDuration(res["relativeTimeSeconds"].ToString()));
                }
            }

            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                col.Width = (metroGrid1.Width - 6) / metroGrid1.Columns.Count;
            }













































        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Contest contest = new Contest();
            contest.Show();
            this.Hide();
        }
    }
}
