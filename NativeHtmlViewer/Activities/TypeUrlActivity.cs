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
using Java.Net;
using System.Net;
using System.IO;
using Android.Graphics;
using System.Threading;
using System.Threading.Tasks;

namespace NativeHtmlViewer.Activities
{
    [Activity(Label = "Native Html Viewer", MainLauncher = true, Icon = "@drawable/icon")]
    class TypeUrlActivity : Activity
    {
        public string HttpProtocol { get; set; }
        private readonly WebClient webClient = new WebClient();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.TypeUrl);

            this.InitView();
            var imageView = (ImageView)this.FindViewById(Resource.Id.ivImage);

            // 이미지 다운로드 방법
            /*
            var webClient = new WebClient();
            byte[] bytes = webClient.
                DownloadData("http://www.google.co.kr/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png");
            var bitmap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
            imageView.SetImageBitmap(bitmap);
            */

            // drawable에서 이미지를 가져오는 방법
            int imageId = Resource.Drawable.naver;
            imageView.SetImageResource(imageId);
        }

        private void InitView()
        {
            var viewHtmlBtn = (Button)this.FindViewById(Resource.Id.btnViewHtml);
            viewHtmlBtn.Click += delegate
            {
                try
                {
                    // 진행상황 표시를 위한 Progress Dialog
                    var progressDlg = ProgressDialog.Show(this,
                        "잠시만 기다려 주세요", "문서를 가져오고 있는 중입니다...", true);
                    /*
                     * RunOnUniThread 사용에 대한 참조는 아래 페이지에서 확인해본다
                     * http://itmining.tistory.com/6
                     * 
                     * 여기서 Task.Run을 사용할 수 있는지 여부도 체크한다
                     */
                    new Thread(new ThreadStart(() =>
                    {
                        RunOnUiThread(() =>
                        {
                            // Toast.MakeText(this, "Toast within progress dialog", ToastLength.Long).Show();
                            progressDlg.Hide();
                        });
                    }));
                    // 입력받은 Url에서 Html 다큐먼트를 다운받는다
                    var urlEditText = (EditText)this.FindViewById(Resource.Id.etUrl);
                    string url = HttpProtocol + urlEditText.Text;
                    string html = this.webClient.DownloadString(url);

                    // ViewHtml 액티비티로 이동한다
                    Intent intent = new Intent(this, typeof(HierarchyHtmlActivity));
                    intent.PutExtra("html", html);
                    this.StartActivity(intent);
                } 
                catch (Exception e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Short).Show();
                }
            };

            var httpRadio = (RadioButton)this.FindViewById(Resource.Id.rdbHttp);
            httpRadio.Click += (sender, e) =>
            {
                HttpProtocol = httpRadio.Text;
            };

            var httpsRadio = (RadioButton)this.FindViewById(Resource.Id.rdbHttps);
            httpsRadio.Click += (sender, e) =>
            {
                HttpProtocol = httpsRadio.Text;
            };

            // default로 http 프로토콜을 선택한다
            httpRadio.Checked = true;
            this.HttpProtocol = httpRadio.Text;
        }
    }
}