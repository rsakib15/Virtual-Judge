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

        private void getData()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(URL);
            
            HtmlNodeCollection header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[1]");

            metroLabel6.Text = header[0].InnerText;
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[2]");
            metroLabel7.Text = header[0].InnerText;
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[3]");
            metroLabel8.Text = header[0].InnerText;
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[4]");
            metroLabel10.Text = header[0].InnerText;
            header = doc.DocumentNode.SelectNodes("//div[contains(@class,'header')]/div[5]");
            metroLabel11.Text = header[0].InnerText;

            //webBrowser1.ScrollBarsEnabled = false;
            //HorizontalScroll.Enabled = false;

            foreach (HtmlNode cell in doc.DocumentNode.SelectNodes("//div[contains(@class,'problem-statement')]"))
            {
                webBrowser1.DocumentText = cell.InnerHtml;
            }

            

            //webBrowser1.ScrollBarsEnabled = false;
            

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            ProblemList problemlist = new ProblemList();
            problemlist.Show();
            this.Hide();
        }
    }


}
