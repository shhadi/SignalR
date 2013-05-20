using System;
using System.Collections.ObjectModel;

namespace Microsoft.AspNet.SignalR.Client.WP71.Sample
{
    public partial class MainPage
    {
        public ObservableCollection<string> Items { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>();

            // Set the data context of the listbox control to the sample data
            DataContext = this;
        }

        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var connection = new Connection("http://192.168.100.109:40476/raw-connection");

            connection.Received += Report;

            connection.Reconnected += () => Report("[{0}]: Connection restablished", DateTime.Now);

            connection.StateChanged += change => Report(change.OldState + " => " + change.NewState);

            connection.Error += ex =>
            {
                Report("========ERROR==========" + ex.Message + "=======================");
            };

            Items.Add("Starting...");
            await connection.Start();
        }

        void Report(string message)
        {
            Dispatcher.BeginInvoke(() => Items.Add(message));
        }

        void Report(string format, params object[] args)
        {
            Dispatcher.BeginInvoke(() => Items.Insert(0, string.Format(format, args)));
        }
    }
}