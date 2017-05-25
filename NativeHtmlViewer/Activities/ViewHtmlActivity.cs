using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HtmlAgilityPack;

namespace NativeHtmlViewer.Activities
{
    [Activity(Label = "Html Native Viewer", MainLauncher = false, Icon = "@drawable/icon")]
    class ViewHtmlActivity : Activity
    {
        public string HtmlDoc { get; private set; }
        public List<string> LinesWithoutSpace { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.ViewHtml);

            this.HtmlDoc = Intent.GetStringExtra("html");
            this.LinesWithoutSpace = new List<string>();
            string[] lines = this.HtmlDoc.Split('\n');
            foreach (string line in lines)
            {
                string l = line.Trim();
                if ("".Equals(l) == false)
                {
                    this.LinesWithoutSpace.Add(l);
                }
            }

            TextView tvHtml = (TextView)this.FindViewById(Resource.Id.tvHtml);
            foreach (string line in this.LinesWithoutSpace)
            {
                tvHtml.Text += line + '\n';
            }

            HtmlDocument html = new HtmlDocument();
        }
    }
}