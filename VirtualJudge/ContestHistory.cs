using Newtonsoft.Json.Linq;
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
    public partial class ContestHistory : MetroFramework.Forms.MetroForm
    {
        public ContestHistory()
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

            for (int i = 0; i < 723; i++)
            {
                res = JObject.Parse(results["result"][i].ToString());
                if (Convert.ToInt64(res["relativeTimeSeconds"].ToString()) >= 0)
                {
                    metroGrid1.Rows.Add(res["id"].ToString(), res["name"].ToString(), res["relativeTimeSeconds"].ToString());
                }
            }

            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                col.Width = (metroGrid1.Width - 4) / metroGrid1.Columns.Count;
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
