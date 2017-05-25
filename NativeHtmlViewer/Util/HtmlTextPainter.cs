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
using Android.Text;
using HtmlAgilityPack;
using Android.Graphics;
using NativeHtmlViewer.Activities;

namespace NativeHtmlViewer.Util
{
    static class HtmlTextPainter
    {
        private static readonly Color TAG_COLOR = Color.ParseColor("#A72D2D");
        private static readonly Color ATTR_KEY_COLOR = Color.ParseColor("#2D55A7");
        private static readonly Color ATTR_VAL_COLOR = Color.ParseColor("#0D720D");
        private static readonly Color COMMENT_COLOR = Color.ParseColor("#8B4513");
        public static SpannableStringBuilder PaintHtml(HtmlNode htmlNode)
        {
            var builder = new SpannableStringBuilder();
            if ("#comment".Equals(htmlNode.Name))
            {
                builder.Append(htmlNode.InnerText, new ForegroundPaintedSpan(COMMENT_COLOR), SpanTypes.ExclusiveExclusive);
                return builder;
            }
            else
            {
                builder.Append("<");
                builder.Append(htmlNode.Name, new ForegroundPaintedSpan(TAG_COLOR), SpanTypes.ExclusiveExclusive);
                builder.Append(" ");
                foreach (var attr in htmlNode.Attributes)
                {
                    builder.Append(attr.Name, new ForegroundPaintedSpan(ATTR_KEY_COLOR), SpanTypes.ExclusiveExclusive);
                    builder.Append("=");
                    builder.Append(attr.Value, new ForegroundPaintedSpan(ATTR_VAL_COLOR), SpanTypes.ExclusiveExclusive);
                    builder.Append(" ");
                }
                // 마지막 공백 문자를 제거한다
                builder.Delete(builder.Length() - 1, builder.Length());
                builder.Append(">");
                return builder;
            }
        }
    }
}