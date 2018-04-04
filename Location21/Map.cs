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
using Android.Webkit;

namespace Location21 {
    [Activity(Label = "Map")]
    public class Map : Activity {
        string latitude, longitude, findplace;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
                                            
            SetContentView(Resource.Layout.Map);

            //Optener el dato
            latitude=Intent.GetStringExtra("Latitude")??string.Empty;
            longitude=Intent.GetStringExtra("Longitude")??string.Empty;
            findplace=Intent.GetStringExtra("Findplace")??string.Empty;


            WebView localWebView = FindViewById<WebView>(Resource.Id.localWebView);
            localWebView.SetWebViewClient(new WebViewClient()); // stops request going to Web Browser
            localWebView.Settings.JavaScriptEnabled=true;
            localWebView.LoadUrl("http://retrorama.me/map/?lat="+latitude+"&long="+longitude+"&place="+findplace);
        }
    }
}