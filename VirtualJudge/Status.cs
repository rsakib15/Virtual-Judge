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
    public partial class Status : MetroFramework.Forms.MetroForm
    {
        public Status()
        {
            InitializeComponent();
            StatusCodeforces();
            metroComboBox1.Text = "Codeforces";
        }

        private void StatusCodeforces()
        {

            try
            {
                metroGrid1.Show();
                metroGrid2.Hide();
                WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("http://codeforces.com/api/problemset.recentStatus?count=50");
                object webData = System.Text.Encoding.UTF8.GetString(raw);
                JObject results = JObject.Parse(webData.ToString());
                JObject res = new JObject();

                for (int i = 0; i < 50; i++)
                {

                    res = JObject.Parse(results["result"][i].ToString());
                    metroGrid1.Rows.Add
                    (
                       res["id"].ToString(),
                       res["relativeTimeSeconds"].ToString(),
                       res["author"]["members"][0]["handle"].ToString(),
                       (res["problem"]["contestId"].ToString() + res["problem"]["index"].ToString() + " - " + res["problem"]["name"].ToString()),
                       res["programmingLanguage"].ToString(),
                       res["verdict"].ToString(),
                       (res["timeConsumedMillis"].ToString() + " ms"),
                       (Convert.ToInt32(res["memoryConsumedBytes"]) / 1024 + " KB")
                    );
                }

                foreach (DataGridViewColumn col in metroGrid1.Columns)
                {
                    col.Width = (metroGrid1.Width - 4) / metroGrid1.Columns.Count;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("The Juding Systems is Busy Now!!!");
                StatusCodeforces();
            }
        }

        private void StatusUVA()
        {

            try
            {
                metroGrid1.Hide();
                metroGrid2.Show();
                WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("http://uhunt.felix-halim.net/api/poll/0");
                string webData = System.Text.Encoding.UTF8.GetString(raw);


                string JSarray_1 = @webData;
                JObject j = JObject.Parse("{\"problems\":" + JSarray_1 + "}");

                //MessageBox.Show(j.ToString());
                 
                for (int i = 99; i >=0; i--)
                {
                    int ver = Convert.ToInt32(j["problems"][i]["msg"]["ver"]);
                    if (ver == 0)
                    {
                        //i++;
                        continue;
                    }
                        

                    metroGrid2.Rows.Add
                    (
                        j["problems"][i]["msg"]["sid"], 
                        j["problems"][i]["msg"]["sbt"], 
                        j["problems"][i]["msg"]["name"], 
                        j["problems"][i]["msg"]["pid"],
                        j["problems"][i]["msg"]["lan"],
                        j["problems"][i]["msg"]["ver"],
                        j["problems"][i]["msg"]["run"],
                        j["problems"][i]["msg"]["rank"]
                    );
            }
                foreach (DataGridViewColumn col in metroGrid2.Columns)
                {
                    col.Width = (metroGrid2.Width - 4) / metroGrid2.Columns.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
            
            if (metroComboBox1.Text == "UVA")
            {
                metroGrid2.Rows.Clear();
                StatusUVA();
            }
            else if (metroComboBox1.Text == "Codeforces")
            {
                metroGrid1.Rows.Clear();
                StatusCodeforces();
            }

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void metroComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "UVA")
            {
               
                StatusUVA();
            }
            else if (metroComboBox1.Text == "Codeforces")
            {

                
                StatusCodeforces();
            }
        }
    }
}
