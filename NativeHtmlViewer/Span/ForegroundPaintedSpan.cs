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
using Android.Text.Style;
using Android.Graphics;
using Java.Lang;

namespace NativeHtmlViewer.Activities
{
    class ForegroundPaintedSpan : ReplacementSpan
    {
        public Android.Graphics.Color FontColor { get; private set; }

        public ForegroundPaintedSpan(Android.Graphics.Color fontColor) : base()
        {
            this.FontColor = fontColor;
        }
        public override void Draw(Canvas canvas, ICharSequence text,
            int start, int end, float x, int top, int y, int bottom, Paint paint)
        {
            paint.Color = this.FontColor;
            canvas.DrawText(text, start, end, x, y, paint);
        }

        public override int GetSize(Paint paint, ICharSequence text, int start, int end, Paint.FontMetricsInt fm)
        {
            return Java.Lang.Math.Round(paint.MeasureText(text, start, end));
        }

        private float MeasureText(Paint paint, ICharSequence charSequence, int start, int end)
        {
            return paint.MeasureText(charSequence, start, end);
        }
    }
}