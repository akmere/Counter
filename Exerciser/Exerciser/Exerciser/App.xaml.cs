using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Exerciser
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new AddPage();
            MainPage = new NavigationPage(new ListViewPage());
        }

        protected override void OnStart()
        {

            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            MainPage = new ListViewPage();
            // Handle when your app resumes
        }
    }
}
