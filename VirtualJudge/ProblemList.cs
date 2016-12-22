using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using PagedList;
using System.IO;


namespace VirtualJudge
{
    public partial class ProblemList : MetroFramework.Forms.MetroForm
    {
        List<string> list = new List<string>();
        public ProblemList()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            metroComboBox1.Text = "Codeforces";
            ProblemCodeforces();
        }

        private void ProblemCodeforces()
        {
            string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/problems.json");
            JObject results = JObject.Parse(Json.ToString());

            JObject res = new JObject();

            for (int i = 0; i < 3000; i++)
            {
                res = JObject.Parse(results["result"]["problems"][i].ToString());
                metroGrid1.Rows.Add(res["contestId"].ToString(), res["index"].ToString(), res["name"].ToString());
                //Database d = new Database();
            }


            foreach (DataGridViewColumn col in metroGrid1.Columns)
            {
                col.Width = (metroGrid1.Width - 4) / metroGrid1.Columns.Count;
            }
        }


        private void ProblemJSON()
        {
            WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("http://codeforces.com/api/problemset.problems?");
            object webData = System.Text.Encoding.UTF8.GetString(raw);
            JObject results = JObject.Parse(webData.ToString());

            JObject res = new JObject();
            
            for (int i = 0;i<300; i++)
            {
                res = JObject.Parse(results["result"]["problems"][i].ToString());
                metroGrid1.Rows.Add(res["contestId"].ToString(), res["index"].ToString(), res["name"].ToString());
                //Database d = new Database();
            }


            foreach(DataGridViewColumn col in metroGrid1.Columns)
            {
                col.Width = (metroGrid1.Width - 4) / metroGrid1.Columns.Count;     
            }
        }

        private void ProblemUVA()
        {    
            try
            {
                string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/uva.json");
                string JSarray_1 = @Json;
                JObject j = JObject.Parse("{\"problems\":" + JSarray_1 + "}");

                for(int i = 0; i < 100; i++)
                {
                    metroGrid2.Rows.Add(j["problems"][i][1],j["problems"][i][2],j["problems"][i][3], j["problems"][i][4]);
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

        private void panel2_Click(object sender, EventArgs e)
        {
            Homepage home = new Homepage();
            home.Show();
            this.Hide();
        }


        private void metroComboBox1_TextChanged(object sender, EventArgs e)
        {

            if (metroComboBox1.Text == "UVA")
            {          
                metroGrid1.Hide();
                metroGrid2.Show();
                metroGrid2.Rows.Clear();
                ProblemUVA();
            }
            else if (metroComboBox1.Text == "Codeforces")
            {

                metroGrid1.Show();
                metroGrid2.Hide();
                metroGrid1.Rows.Clear();
                ProblemCodeforces();
            }
        }

        private void metroGrid1_DoubleClick(object sender, EventArgs e)
        {
            string firstCellValue = metroGrid1[0, metroGrid1.CurrentRow.Index].Value.ToString();
            string secondCellValue = metroGrid1[1, metroGrid1.CurrentRow.Index].Value.ToString();
            string url = "http://codeforces.com/contest/" + firstCellValue + "/problem/" + secondCellValue;
            ProblemDescription problemdescription = new ProblemDescription(url);
            problemdescription.Show();
            this.Hide();
        }
    }
}
