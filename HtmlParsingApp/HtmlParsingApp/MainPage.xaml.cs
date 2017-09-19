using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HtmlParsingApp
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
        {
            InitializeComponent();

        }

        void OnParsingBtnClick(object sender, EventHandler e)
        {
            String relatives = "";
            DisplayAlert("Wait", "Parsing Activity has been started.", "OK");

            targetLabel.BindingContext = relatives;

            HtmlAgilityPack.HtmlDocument myDoc = new HtmlAgilityPack.HtmlDocument();
            myDoc.LoadHtml("https://search.naver.com/search.naver?where=nexearch&sm=top_hty&fbm=0&ie=utf8&query=%EC%A0%95%EC%95%84");


            foreach (HtmlNode parentNode1 in myDoc.DocumentNode.SelectNodes("//dd[@class='1st_relate']"))
            {
               
                foreach (HtmlNode childNode1 in parentNode1.SelectNodes(".//a"))
                {
                    //string attributeValue = childNode.GetAttributeValue("href", "");
                    relatives += childNode1.InnerText + "\n";
                }
            }

            foreach (HtmlNode parentNode2 in myDoc.DocumentNode.SelectNodes("//div[@class='realtime_srch']"))
            {
                foreach (HtmlNode childNode2 in parentNode2.SelectNodes(".//span[@class='tit']"))
                {
                    //string attributeValue = childNode.GetAttributeValue("href", "");
                    relatives += childNode2.InnerText + "\n";
                }
            }

            //MainPageViewModel mainPageViewModel = new MainPageViewModel();
            //this.BindingContext = mainPageViewModel;
        }
	}
}
