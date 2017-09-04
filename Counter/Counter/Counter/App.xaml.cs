using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Counter
{
    public partial class App : Application
    {
        Label CountingLabel, CurrentLabel, StartLabel;
        Button ResetButton;
        DateTime CurrentTime, SavedTime;
        public App()
        {
            InitializeComponent();
            MainPage = new Counter.MainPage();
        }

        protected override void OnStart()
        {
            SavedTime = System.DateTime.UtcNow;
            CurrentTime = System.DateTime.Now;
            CountingLabel = MainPage.FindByName<Label>("label1");
            CountingLabel.Text = (CurrentTime - SavedTime).ToString();
            CurrentLabel = MainPage.FindByName<Label>("label2");
            CurrentLabel.Text = "Current Time: " + CurrentTime.ToString();
            StartLabel = MainPage.FindByName<Label>("label3");
            StartLabel.Text = "Initial Time: " + SavedTime.ToString();
            ResetButton = MainPage.FindByName<Button>("button1");
            ResetButton.Clicked += delegate { Lol(); };
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public void Lol()
        {
            MainPage.DisplayAlert("Hello", "LOL", "Accept");
        }
    }
}
