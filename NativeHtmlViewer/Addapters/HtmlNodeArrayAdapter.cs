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
using Java.Lang;
using HtmlAgilityPack;
using Android.Text;
using NativeHtmlViewer.Activities;
using Android.Graphics;
using NativeHtmlViewer.Util;

namespace NativeHtmlViewer.Addapters
{
    class HtmlNodeArrayAdapter : BaseAdapter
    {
        private readonly List<HtmlNode> htmlNodes = null;
        private readonly Context context = null;

        public HtmlNodeArrayAdapter(List<HtmlNode> htmlNodes, Context context)
        {
            this.htmlNodes = htmlNodes;
            this.context = context;
        }

        public override int Count => this.htmlNodes.Count();

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public HtmlNode GetHtmlNode(int position)
        {
            if (this.htmlNodes != null)
            {
                int cntNode = this.htmlNodes.Count();
                if (position >= 0 && position < cntNode)
                {
                    return this.htmlNodes[position];
                }
            }
            return null;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.ItemHtml, null);
            }

            var textView = (TextView)convertView.FindViewById(Resource.Id.tvTag);
            var htmlNode = this.GetHtmlNode(position);

            var builder = HtmlTextPainter.PaintHtml(htmlNode);
            textView.TextFormatted = builder;
            return convertView;
        }
    }
}