using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Counter
{
    public partial class App : Application
    {
        Label RecordLabel, CountingLabel, CurrentLabel, StartLabel;
        Button SetButton, ResetButton, RefreshButton;
        DateTime CurrentTime, SavedTime;
        string dbPath = "rywal.db";
        public App()
        {
            InitializeComponent();
            MainPage = new Counter.MainPage();
        }

        protected override void OnStart()
        {
            CurrentTime = System.DateTime.Now;
            CountingLabel = MainPage.FindByName<Label>("label1");
            CurrentLabel = MainPage.FindByName<Label>("label2");
            StartLabel = MainPage.FindByName<Label>("label3");
            RecordLabel = MainPage.FindByName<Label>("label4");
            Refresh();
            SetButton = MainPage.FindByName<Button>("button1");
            SetButton.Clicked += delegate { Set(); };
            SetButton.IsEnabled = false;
            ResetButton = MainPage.FindByName<Button>("button2");
            ResetButton.Clicked += delegate { Reset(); };
            RefreshButton = MainPage.FindByName<Button>("button3");
            RefreshButton.Clicked += delegate { Refresh(); };
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

        public void Refresh()
        {

            CurrentTime = DateTime.Now;
            SavedTime = GetSavedTime();
            TimeSpan deduction = CurrentTime - SavedTime;
            var recordTime = GetRecordTime();
            CountingLabel.Text = Math.Round(deduction.TotalMinutes, 2) + " minutes";
            StartLabel.Text = "Initial Time: " + GetSavedTime().ToString();
            CurrentLabel.Text = "Current Time: " + CurrentTime.ToString();
            RecordLabel.Text = "Record Time: " + Math.Round(recordTime.TotalMinutes, 2) + " minutes";

        }

        public void Set()
        {
            DbManager db = new DbManager(dbPath);
            Refresh();
        }
        async public void Reset()
        {
            DbManager db = new DbManager(dbPath);
            string answer = await MainPage.DisplayActionSheet("Attention", "Confirm", "Cancel", "Are you sure to restart the clock?");
            if (answer == "Confirm")
            {
                db.InsertEndTime();
                Refresh();
            }

        }

        public DateTime GetSavedTime()
        {
            DbManager db = new DbManager(dbPath);
            return db.GetStartTime();
        }

        public TimeSpan GetRecordTime()
        {
            DbManager db = new DbManager(dbPath);
            //return TimeSpan.FromSeconds(5);
            return TimeSpan.FromTicks(db.GetRecordTime());
        }
    }
}
