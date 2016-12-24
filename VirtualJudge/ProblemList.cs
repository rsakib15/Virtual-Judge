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
using System.Threading;
using System.Text.RegularExpressions;

namespace VirtualJudge
{
    public partial class ProblemList : MetroFramework.Forms.MetroForm
    {
        private int Codeforcescount;
        private int CodeforcesLastContest;
        private int UVAcount;
        List<int> UVAVolume = new List<int>();

        List<string> CFsSolved = new List<string>();
        List<string> USolved = new List<string>();

        public ProblemList()
        {
            InitializeComponent();
            InitialElements();
            CounterUVA();
            CounterCodeforces();        
            CodeforcesSolved();
            UVASolved();
            ProblemCodeforces();       
            metroGrid1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private void CounterCodeforces()
        {
            string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/Codeforces.json");
            JObject results = JObject.Parse(Json.ToString());
            JObject res = new JObject();
            Codeforcescount = results.SelectTokens("$.result.problems[*]").Count();

            for (int i = 0; i < Codeforcescount; i++)
            {
                res = JObject.Parse(results["result"]["problems"][i].ToString());
                if (i == 0)
                {
                    CodeforcesLastContest = Convert.ToInt32(res["contestId"]);
                    break;
                }
            }
            CodeforcesLastContest = CodeforcesLastContest / 100 + 1;

            for (int i = 0; i < CodeforcesLastContest; i++)
            {
                metroComboBox3.Items.Add(i * 100 + "---" + (i * 100 + 100));
            }
        }

        private void CodeforcesSolved()
        {
            try
            {
                WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("http://codeforces.com/api/user.status?handle=sakib15");
                object webData = System.Text.Encoding.UTF8.GetString(raw);
                JObject results = JObject.Parse(webData.ToString());
                JObject res = new JObject();
                int scount = results.SelectTokens("$.result[*]").Count();

                for (int i = 0; i < scount; i++)
                {
                    res = JObject.Parse(results["result"][i]["problem"].ToString());
                    if (results["result"][i]["verdict"].ToString() == "OK")
                    {
                        CFsSolved.Add(res["contestId"].ToString() + res["index"].ToString());
                    }
                    
                }             
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void UVASolved()
        {
            try
            {
                WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("http://uhunt.felix-halim.net/api/subs-user/712482");
                object webData = System.Text.Encoding.UTF8.GetString(raw);
                JObject results = JObject.Parse(webData.ToString());
                JObject res = new JObject();
                int scount = results.SelectTokens("$.subs[*]").Count();

                for(int i = 0; i < scount; i++)
                {
                    if(results["subs"][i][6].ToString()!="-1")
                        USolved.Add(results["subs"][i][1].ToString());
                }
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ProblemCodeforces()
        {
            string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/Codeforces.json");
            JObject results = JObject.Parse(Json.ToString());
            JObject res = new JObject();

            DataGridViewColumn columnzero = metroGrid1.Columns[0];
            DataGridViewColumn columnone = metroGrid1.Columns[1];
            DataGridViewColumn columntwo = metroGrid1.Columns[2];
            DataGridViewColumn columnthree = metroGrid1.Columns[3];
            DataGridViewColumn columnfour = metroGrid1.Columns[4];

            columnzero.Width = 130;
            columnone.Width = 70;
            columntwo.Width = 286;
            columnthree.Width = 100;
            columnfour.Width = 453;

            JObject j = new JObject();

            string num = metroComboBox3.Text;
            string r = "";
            if (num == "--Select Contest--")
            {
                r = "-1";
            }
            else
            {
                for (int i = 0; i < num.Length; i++)
                {
                    if (num[i] == '-')
                        break;
                    r = r + num[i];
                }
            }

            int p = Convert.ToInt32(r);

            for (int i = 0; i < Codeforcescount; i++)
            {
                res = JObject.Parse(results["result"]["problems"][i].ToString());
                if (p == -1)
                {
                    break;
                }

                string str = Regex.Replace(res["tags"].ToString(), @"\t|\n|\r", "");
                str = str.Replace("[", string.Empty).Replace("]", string.Empty);

                if ((Convert.ToInt32(res["contestId"])) - p < 100 && (Convert.ToInt32(res["contestId"])) - p >= 0)
                {
                    if (CFsSolved.Contains(res["contestId"].ToString() + res["index"].ToString()))
                    {
                        metroGrid1.Rows.Add
                         (
                        res["contestId"].ToString(),
                        res["index"].ToString(),
                        res["name"].ToString(),
                        "Solved",
                        str.Replace("\"", string.Empty)
                        );
                    }
                    else
                    {
                        metroGrid1.Rows.Add
                         (
                        res["contestId"].ToString(),
                        res["index"].ToString(),
                        res["name"].ToString(),
                        "Unsolved",
                        str.Replace("\"", string.Empty)

                        );

                    }

                }

            }
        }





        /// <summary>
        /// /*****************************************************************************************
        /// </summary>



        private void CounterUVA()
        {
            string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/UVA.json");
            string JSarray_1 = @Json;
            JObject j = JObject.Parse("{\"problems\":" + JSarray_1 + "}");

            UVAcount = j.SelectTokens("$.problems[*]").Count();

            for (int i = 0; i < UVAcount; i++)
            {
                string id = j["problems"][i][1].ToString();
                int res = Convert.ToInt32(id) / 100;
                if (UVAVolume.Contains(res) == false)
                {
                    UVAVolume.Add(res);
                }
            }

            UVAVolume.Sort();

            for (int i = 0; i < UVAVolume.Count; i++)
            {
                metroComboBox2.Items.Add(UVAVolume[i]);
            }
        }

        private void InitialElements()
        {
            metroComboBox1.SelectedIndex = 0;
        }

        

        private void ProblemUVA()
        {
            try
            {              
                string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/UVA.json");
                string JSarray_1 = @Json;
                JObject j = JObject.Parse("{\"problems\":" + JSarray_1 + "}");

                DataGridViewColumn columnzero = metroGrid2.Columns[0];
                DataGridViewColumn columnone = metroGrid2.Columns[1];
                DataGridViewColumn columntwo = metroGrid2.Columns[2];
                DataGridViewColumn columnthree = metroGrid2.Columns[3];
                DataGridViewColumn columnfour = metroGrid2.Columns[4];

                columnzero.Width = 150;
                columnone.Width = 456;
                columntwo.Width = 150;
                columnthree.Width = 150;
                columnfour.Width = 150;

                for (int i = 0; i < UVAcount; i++)
                {
                    string id = j["problems"][i][1].ToString();
                    int res = Convert.ToInt32(id) / 100;

                    if (res.ToString() == metroComboBox2.Text)
                    {
                        if (USolved.Contains(j["problems"][i][0].ToString()))
                        {
                            metroGrid2.Rows.Add
                               (
                               j["problems"][i][1],
                               j["problems"][i][2],
                               Convert.ToInt32(j["problems"][i][6]) + Convert.ToInt32(j["problems"][i][7]) + Convert.ToInt32(j["problems"][i][8]) +
                               Convert.ToInt32(j["problems"][i][9]) + Convert.ToInt32(j["problems"][i][10]) + Convert.ToInt32(j["problems"][i][11]) +
                               +Convert.ToInt32(j["problems"][i][12]) + Convert.ToInt32(j["problems"][i][13]) + Convert.ToInt32(j["problems"][i][14]) +
                               Convert.ToInt32(j["problems"][i][15]) + Convert.ToInt32(j["problems"][i][16]) + Convert.ToInt32(j["problems"][i][17]) +
                               Convert.ToInt32(j["problems"][i][18]),
                               j["problems"][i][18],
                               "Solved"
                                );
                        }
                        else
                        {
                            metroGrid2.Rows.Add
                            (
                               j["problems"][i][1],
                               j["problems"][i][2],
                               Convert.ToInt32(j["problems"][i][6]) + Convert.ToInt32(j["problems"][i][7]) + Convert.ToInt32(j["problems"][i][8]) +
                               Convert.ToInt32(j["problems"][i][9]) + Convert.ToInt32(j["problems"][i][10]) + Convert.ToInt32(j["problems"][i][11]) +
                               +Convert.ToInt32(j["problems"][i][12]) + Convert.ToInt32(j["problems"][i][13]) + Convert.ToInt32(j["problems"][i][14]) +
                               Convert.ToInt32(j["problems"][i][15]) + Convert.ToInt32(j["problems"][i][16]) + Convert.ToInt32(j["problems"][i][17]) +
                               Convert.ToInt32(j["problems"][i][18]),
                               j["problems"][i][18],
                               "Unsolved"
                            );
                        }
                        
                    }                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }
  
        private void SearchProblem(string SearchText, string Judge)
        {
            if (Judge == "Codeforces")
            {
                bool found = false;
                string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/Codeforces.json");
                JObject results = JObject.Parse(Json.ToString());

                JObject res = new JObject();
                metroGrid1.Rows.Clear();

                for (int i = 0; i < Codeforcescount; i++)
                {
                    res = JObject.Parse(results["result"]["problems"][i].ToString());
                    if(SearchText== res["contestId"].ToString() || SearchText == res["name"].ToString())
                    {
                        string str = Regex.Replace(res["tags"].ToString(), @"\t|\n|\r", "");
                        str = str.Replace("[", string.Empty).Replace("]", string.Empty);

                        if (CFsSolved.Contains(res["contestId"].ToString() + res["index"].ToString()))
                        {
                            metroGrid1.Rows.Add
                             (
                            res["contestId"].ToString(),
                            res["index"].ToString(),
                            res["name"].ToString(),
                            "Solved",
                            str.Replace("\"", string.Empty)
                            );
                        }
                        else
                        {
                            metroGrid1.Rows.Add
                             (
                            res["contestId"].ToString(),
                            res["index"].ToString(),
                            res["name"].ToString(),
                            "Unsolved",
                            str.Replace("\"", string.Empty)

                            );
                        }

                            found = true;
                    }                       
                }

                if (found == false)
                {
                    MessageBox.Show("Problem Not Found");
                }
            }
            else if(Judge == "UVA")
            {
                try
                {
                    string Json = System.IO.File.ReadAllText("E:/MY_WORKPLACE/C#/VirtualJudge/problems/UVA.json");
                    string JSarray_1 = @Json;
                    JObject j = JObject.Parse("{\"problems\":" + JSarray_1 + "}");
                    bool found = false;

                    metroGrid2.Rows.Clear();
                    for (int i = 0; i < 1002; i++)
                    {
                        if (SearchText == j["problems"][i][1].ToString() || SearchText == j["problems"][i][2].ToString())
                        {
                            if (USolved.Contains(j["problems"][i][0].ToString()))
                            {
                                metroGrid2.Rows.Add
                                   (
                                   j["problems"][i][1],
                                   j["problems"][i][2],
                                   Convert.ToInt32(j["problems"][i][6]) + Convert.ToInt32(j["problems"][i][7]) + Convert.ToInt32(j["problems"][i][8]) +
                                   Convert.ToInt32(j["problems"][i][9]) + Convert.ToInt32(j["problems"][i][10]) + Convert.ToInt32(j["problems"][i][11]) +
                                   +Convert.ToInt32(j["problems"][i][12]) + Convert.ToInt32(j["problems"][i][13]) + Convert.ToInt32(j["problems"][i][14]) +
                                   Convert.ToInt32(j["problems"][i][15]) + Convert.ToInt32(j["problems"][i][16]) + Convert.ToInt32(j["problems"][i][17]) +
                                   Convert.ToInt32(j["problems"][i][18]),
                                   j["problems"][i][18],
                                   "Solved"
                                    );
                            }
                            else
                            {
                                metroGrid2.Rows.Add
                                (
                                   j["problems"][i][1],
                                   j["problems"][i][2],
                                   Convert.ToInt32(j["problems"][i][6]) + Convert.ToInt32(j["problems"][i][7]) + Convert.ToInt32(j["problems"][i][8]) +
                                   Convert.ToInt32(j["problems"][i][9]) + Convert.ToInt32(j["problems"][i][10]) + Convert.ToInt32(j["problems"][i][11]) +
                                   +Convert.ToInt32(j["problems"][i][12]) + Convert.ToInt32(j["problems"][i][13]) + Convert.ToInt32(j["problems"][i][14]) +
                                   Convert.ToInt32(j["problems"][i][15]) + Convert.ToInt32(j["problems"][i][16]) + Convert.ToInt32(j["problems"][i][17]) +
                                   Convert.ToInt32(j["problems"][i][18]),
                                   j["problems"][i][18],
                                   "Unsolved"
                                );
                            }
                            found = true;
                        }
                       
                    }

                    if (found == false)
                    {
                        MessageBox.Show("Problem Not Found");
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

        private void metroPanel4_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "Codeforces")
            {
                SearchProblem(metroTextBox1.Text, "Codeforces");
            }
            else if (metroComboBox1.Text == "UVA")
            {
                SearchProblem(metroTextBox1.Text, "UVA");
            }
        }


        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "")
            {            
                if (metroComboBox1.Text == "Codeforces")
                {
                    metroGrid1.Rows.Clear();
                    ProblemCodeforces();
                   
                }
                if (metroComboBox1.Text == "UVA")
                {
                    metroGrid2.Rows.Clear();
                    ProblemUVA();
                }
            }
        }

        private void metroPanel5_Click(object sender, EventArgs e)
        {
            ProgressForm p = new ProgressForm();
            p.Progress(metroComboBox1.Text);
            p.Show();
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
                metroPanel6.BringToFront();
                metroPanel8.SendToBack();
                metroGrid1.Hide();
                metroGrid2.Show();
                metroGrid2.Rows.Clear();
                metroComboBox2.SelectedIndex = 0;

            }
            else if (metroComboBox1.Text == "Codeforces")
            {
                metroPanel8.BringToFront();
                metroPanel6.SendToBack();
                metroGrid1.Show();
                metroGrid2.Hide();
                metroGrid1.Rows.Clear();
                metroComboBox3.SelectedIndex = 0;
            }
        }


        private void metroComboBox2_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "")
            {
                if (metroComboBox1.Text == "UVA" && metroComboBox3.Text !="")
                {
                    metroGrid2.Rows.Clear();
                    ProblemUVA();
                }
                if (metroComboBox1.Text == "Codeforces" && metroComboBox2.Text != "")
                {
                    metroGrid1.Rows.Clear();
                    ProblemCodeforces();
                }
            }
       
        }

        private void metroComboBox3_TextChanged(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == "")
            {
                if (metroComboBox1.Text == "UVA")
                {
                    metroGrid2.Rows.Clear();
                    ProblemUVA();
                }
                if (metroComboBox1.Text == "Codeforces")
                {
                    metroGrid1.Rows.Clear();
                    ProblemCodeforces();
                }
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
    }
}