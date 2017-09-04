using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace Counter
{
    class DbManager
    {
        SQLiteConnection db;
        public DbManager(string dbPath)
        {
            db = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath(dbPath));
            db.CreateTable<Time>();
        }

        ~DbManager()
        {
            db.Close();
        }


        public class Time
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public long TimeInTicks { get; set; }
            public int IsActive { get; set; }
        }



        public DateTime GetStartTime()
        {
            var result = db.Query<Time>("SELECT * FROM Time WHERE IsActive=1");
            if (result.Count != 0)
                return result[0].StartTime;
            else
            {
                var time = System.DateTime.Now;
                InsertStartTime(time);
                return time;
            }
        }

        public long GetRecordTime()
        {
            var result = db.Query<Time>("SELECT * FROM Time ORDER BY TimeInTicks DESC");

            if (result.Count == 0)
                return 5324543;
            else
            {
                return result[0].TimeInTicks;
            }
        }

        public void InsertStartTime(DateTime startTime)
        {
            db.Insert(new Time()
            {
                StartTime = startTime,
                IsActive = 1
            });
        }

        public void InsertEndTime()
        {
            var nowTime = System.DateTime.Now;
            var Command = db.CreateCommand("UPDATE Time SET EndTime= ?, TimeInTicks=?, IsActive=0 WHERE IsActive = 1", nowTime, (nowTime - GetStartTime()).Ticks);
            Command.ExecuteNonQuery();
        }
    }
}
