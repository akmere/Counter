using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Exerciser
{
    class DbManager
    {
        SQLiteConnection db;
        public DbManager(string dbPath)
        {
            db = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath(dbPath));
            db.CreateTable<ExerciseType>();
            db.CreateTable<SDay>();
            db.CreateTable<Rep>();

            //    List<ExerciseType> ListOfExercises = new List<ExerciseType>()
            //{
            //    new ExerciseType() {Name = "F-Pull-Up" },
            //    new ExerciseType() {Name = "B-Pull-Up" },
            //    new ExerciseType() {Name = "Triangle" },
            //};
            //try
            //{
            //    db.InsertAll(ListOfExercises);
            //}
            //catch (Exception)
            //{

            //}
        }



        ~DbManager()
        {
            db.Close();
        }


        public class Rep
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [Indexed]
            public int ExerciseTypeId { get; set; }
            [Indexed]
            public int SDayId { get; set; }
            public int Repetition { get; set; }
        }

        public class SDay
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [NotNull]
            public DateTime Day { get; set; }
            public int Point { get; set; }
        }

        public class ExerciseType
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [NotNull, Unique]
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public ObservableCollection<Exercise> GetExercises(DateTime day)
        {
            var result = db.Query<ExerciseType>("Select * FROM ExerciseType");
            ObservableCollection<Exercise> roc = new ObservableCollection<Exercise>();
            foreach (ExerciseType x in result)
            {
                roc.Add(new Exercise()
                {
                    Name = x.Name,
                    Repetitions = GetReps(x.Id, GetSDayId(day))
                });
            }
            return roc;
        }

        public int GetSDayId(DateTime day)
        {
            var result = db.Query<SDay>("SELECT _id FROM SDay WHERE Day=?", day);
            if (result.Count == 0)
            {
                SDay newDay = new SDay() { Day = day };
                db.Insert(newDay);
                return newDay.Id;
            }
            return result[0].Id;
        }

        public DateTime GetSDay(int SDayId)
        {
            return db.Query<SDay>("Select * FROM SDay WHERE _id=?", SDayId).First().Day;
        }

        public int GetReps(int ExerciseId, int SDayId)
        {
            var result = db.Query<Rep>("Select * FROM Rep WHERE ExerciseTypeId=? AND SDayId = ?", ExerciseId, SDayId);
            if (result.Count == 0)
            {
                Rep newRep = new Rep() { ExerciseTypeId = ExerciseId, SDayId = SDayId, Repetition = 0 };
                db.Insert(newRep);
                return newRep.Repetition;
            }
            return result[0].Repetition;
        }

        public int GetExerciseTypeIdByName(string name)
        {
            var result = db.Query<ExerciseType>("SELECT * FROM ExerciseType WHERE Name = ?", name);
            return result[0].Id;
        }

        public int GetValueByName(string name)
        {
            var result = db.Query<ExerciseType>("SELECT * FROM ExerciseType WHERE Name = ?", name);
            return result[0].Value;
        }

        public void SaveExercises(ObservableCollection<Exercise> list, DateTime day)
        {
            foreach (Exercise x in list)
            {
                GetReps(GetExerciseTypeIdByName(x.Name), GetSDayId(day));
                var Command = db.CreateCommand("UPDATE Rep SET Repetition=? WHERE ExerciseTypeId=? AND SDayId=?", x.Repetitions, GetExerciseTypeIdByName(x.Name), GetSDayId(day));
                Command.ExecuteNonQuery();
            }
        }

        public int GetScore(ObservableCollection<Exercise> collection)
        {
            int sum = 0;
            foreach (var x in collection)
            {
                sum += x.Repetitions * GetValueByName(x.Name);
            }
            return sum;
        }

        public void AddExerciseType(string name, int value)
        {
            var it = new ExerciseType()
            {
                Name = name,
                Value = value
            };
            db.Insert(it);
        }

        public void DeleteExerciseType(string name)
        {
            db.Delete(new ExerciseType { Id = GetExerciseTypeIdByName(name) });
        }

        public void EditExerciseType(string name, string newName, int newValue)
        {
            db.Update(new ExerciseType { Id = GetExerciseTypeIdByName(name), Name = newName, Value = newValue });
        }

        //public DateTime GetStartTime()
        //{
        //    var result = db.Query<Time>("SELECT * FROM Time WHERE IsActive=1");
        //    if (result.Count != 0)
        //        return result[0].StartTime;
        //    else
        //    {
        //        var time = System.DateTime.Now;
        //        InsertStartTime(time);
        //        return time;
        //    }
        //}

        //public long GetRecordTime()
        //{
        //    var result = db.Query<Time>("SELECT * FROM Time ORDER BY TimeInTicks DESC");

        //    if (result.Count == 0)
        //        return 5324543;
        //    else
        //    {
        //        return result[0].TimeInTicks;
        //    }
        //}

        //public void InsertStartTime(DateTime startTime)
        //{
        //    db.Insert(new Time()
        //    {
        //        StartTime = startTime,
        //        IsActive = 1
        //    });
        //}

        //public void InsertEndTime()
        //{
        //var nowTime = System.DateTime.Now;
        //var Command = db.CreateCommand("UPDATE Time SET EndTime= ?, TimeInTicks=?, IsActive=0 WHERE IsActive = 1", nowTime, (nowTime - GetStartTime()).Ticks);
        //Command.ExecuteNonQuery();
        //}
    }
}
