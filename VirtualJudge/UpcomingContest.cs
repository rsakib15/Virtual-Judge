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
    public partial class UpcomingContest : MetroFramework.Forms.MetroForm
    {

        public UpcomingContest()
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

            DataGridViewColumn columnzero = metroGrid1.Columns[0];
            DataGridViewColumn columnone= metroGrid1.Columns[1];
            DataGridViewColumn columntwo = metroGrid1.Columns[2];
            DataGridViewColumn columnthree = metroGrid1.Columns[3];

            columnzero.Width = 130;
            columnone.Width = 400;
            columntwo.Width = 213;
            columnthree.Width = 213;

            for (int i = 0; i <8; i++)
            {
                res = JObject.Parse(results["result"][i].ToString());

                if (Convert.ToInt32(res["relativeTimeSeconds"].ToString()) < 0)
                {
                    metroGrid1.Rows.Add
                    (
                        res["id"].ToString(), 
                        res["name"].ToString(),
                        Functions.calculateDuration(res["durationSeconds"].ToString()),
                        Functions.calculateDuration(res["relativeTimeSeconds"].ToString())
                    );
                }
                else
                {
                    break;
                }                                              
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
