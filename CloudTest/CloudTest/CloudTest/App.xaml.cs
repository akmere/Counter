using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Dropbox.Api;

namespace CloudTest
{
    public partial class App : Application
    {
        const string TOKEN = "KbTGj5WuJkAAAAAAAAAAaR0fsnvBqgSSMDZX0mKmdpJwQGzzDtP31mF9-Kz99A_1";

        public App()
        {
            InitializeComponent();

            MainPage = new CloudTest.MainPage();
        }

        protected override void OnStart()
        {
            var task = Task.Run(Run);
            task.Wait();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        static async Task Run()
        {
            using (var dbx = new DropboxClient(TOKEN))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
            }
        }

        static async Task Download(DropboxClient dbx, string folder, string file)
        {
            using (var response = await dbx.Files.DownloadAsync(folder + "/" + file))
            {
            }
        }
    }
}
