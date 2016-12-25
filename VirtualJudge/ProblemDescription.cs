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
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace VirtualJudge
{
    public partial class ProblemDescription : MetroFramework.Forms.MetroForm
    {
        private string URL;

        public ProblemDescription(string url)
        {
            URL = url;
            InitializeComponent();   
            getData();
        }


        public string StringWordsRemove(string stringToClean, string wordsToRemove)
        {
            string[] splitWords = wordsToRemove.Split(new Char[] { ' ' });

            string pattern = "";

            foreach (string word in splitWords)
            {
                pattern = @"\b" + word + "\b";
                stringToClean = Regex.Replace(stringToClean, pattern, "");
            }

            return stringToClean;
        }

        private void getData()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(URL);
         
            HtmlNodeCollection header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[1]");


            metroLabel6.Text = header[0].InnerText.Remove(0, 3);
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[2]");
            metroLabel7.Text = header[0].InnerText.Remove(0,19);
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[3]");
            metroLabel8.Text = header[0].InnerText.Remove(0, 21);
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[4]");
            metroLabel10.Text = header[0].InnerText.Remove(0, 5); ;
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[5]");
            metroLabel11.Text = header[0].InnerText.Remove(0, 6); ;

            foreach (HtmlNode cell in doc.DocumentNode.SelectNodes("//div[contains(@class,'problem-statement')]"))
            {
                webBrowser1.DocumentText = cell.InnerHtml;
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            ProblemList problemlist = new ProblemList();
            problemlist.Show();
            this.Hide();
        }

        private void metroPanel2_Click(object sender, EventArgs e)
        {
            //download
        }

        private void metroPanel3_Click(object sender, EventArgs e)
        {
            //submit
        }

        private void metroPanel4_Click(object sender, EventArgs e)
        {
            //favour
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                axAcroPDF1.src = open.FileName;
            }


        }

        private void metroPanel5_Click(object sender, EventArgs e)
        {
            //relead
            getData();
        }

        private void metroPanel10_Click(object sender, EventArgs e)
        {
             this.WindowState = FormWindowState.Minimized;
        }

        private void metroPanel9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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
        }

    }
}
