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

namespace NativeHtmlViewer.Addapters
{
    class HtmlNodeHistoryAdapter : BaseAdapter
    {
        public List<HtmlNode> HtmlHistories { get; private set; }
        public Context AppContext { get; private set; }

        public HtmlNodeHistoryAdapter(Context context, List<HtmlNode> htmlHistories)
        {
            this.HtmlHistories = htmlHistories;
            this.AppContext = context;
        }

        public override int Count => this.HtmlHistories.Count();

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public HtmlNode GetHtmlNode(int position)
        {
            if (position >= 0 && position < this.Count)
            {
                return this.HtmlHistories[position];
            }
            else
            {
                return null;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(AppContext).Inflate(Resource.Layout.ItemHistory, null);
            }

            var textView = (TextView)convertView.FindViewById(Resource.Id.tvHistory);
            var htmlNode = this.GetHtmlNode(position);
            textView.Text = htmlNode.Name;
            return convertView;
        }
    }
}