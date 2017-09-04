using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;
using Counter.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FileManager))]
namespace Counter.Droid
{

    class FileManager : IFileManager
    {
        string dbPath;
        public FileManager()
        {
            dbPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "db3.sqlite");
            var db = new SQLiteAsyncConnection("foofoo");
        }

    }
}