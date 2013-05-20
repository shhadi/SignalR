using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace Microsoft.AspNet.SignalR.Client.Android.Sample
{
    [Activity(Label = "Microsoft.AspNet.SignalR.Client.Android.Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private TextView _textView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            _textView = FindViewById<TextView>(Resource.Id.textView1);

            var connection = new Connection("http://192.168.100.109:40476/raw-connection");

            connection.Received += Report;

            connection.Reconnected += () => Report("[{0}]: Connection restablished", DateTime.Now);

            connection.StateChanged += change => Report(change.OldState + " => " + change.NewState);

            connection.Error += ex =>
            {
                Report("========ERROR==========" + ex.Message + "=======================");
            };

            Report("Starting");
            connection.Start().ContinueWith(t => Report("Finished"));
        }

        void Report(string message)
        {
			message = message + "\r\n";
			RunOnUiThread (() => _textView.Text += message);
        }

        void Report(string format, params object[] args)
        {
			Report (string.Format(format, args));
        }
    }
}

