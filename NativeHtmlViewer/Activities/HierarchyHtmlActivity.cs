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
using NativeHtmlViewer.Addapters;
using static Android.Widget.AdapterView;

namespace NativeHtmlViewer.Activities
{
    [Activity(Label = "Hierarchy Html Viewer", MainLauncher = false, Icon = "@drawable/icon")]
    class HierarchyHtmlActivity : Activity
    {
        public string HtmlDoc { get; private set; }
        public HtmlDocument HtmlParser { get; private set; }
        public List<HtmlNode> HtmlTracer { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.HierarchyHtml);

            this.HtmlDoc = Intent.GetStringExtra("html");
            this.HtmlParser = new HtmlDocument();
            this.HtmlParser.LoadHtml(this.HtmlDoc);
            this.HtmlTracer = new List<HtmlNode>();

            var rootHtml = this.HtmlParser.
                DocumentNode.Descendants().Where(e => e.Name.Equals("html")).FirstOrDefault();
            this.ListItems(rootHtml);
            this.HtmlTracer.Add(rootHtml);

            this.InitView();
        }

        private void ListItems(HtmlNode htmlNode)
        {
            var listView = (ListView)this.FindViewById(Resource.Id.lvHtmlNode);
            var htmlNodeList = new List<HtmlNode>();
            foreach (var n in htmlNode.ChildNodes)
            {
                if ("#text".Equals(n.Name) == false)
                {
                    htmlNodeList.Add(n);
                }
            }
            var adapter = new HtmlNodeArrayAdapter(htmlNodeList, this);
            listView.Adapter = adapter;
        }

        private void InitView()
        {
            var lvNodes = (ListView)this.FindViewById(Resource.Id.lvHtmlNode);
            lvNodes.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) =>
            {
                var adapter = lvNodes.Adapter as HtmlNodeArrayAdapter;
                if (adapter != null)
                {
                    var htmlNode = adapter.GetHtmlNode(args.Position);
                    this.HtmlTracer.Add(htmlNode);
                    this.ListItems(htmlNode);
                }
            };
        }
    }
}   