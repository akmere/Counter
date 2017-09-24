using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Concepter
{
    class DatabaseManager1
    {
        SQLiteConnection db;
        public DatabaseManager1(string dbPath)
        {
            db = new SQLiteConnection(DependencyService.Get<IFileHelper1>().GetLocalFilePath(dbPath));
            db.CreateTable<Kind>();
            db.CreateTable<Data>();

            if (db.Query<Kind>("SELECT * FROM Kind WHERE _id = 1").ToList().Count == 0) db.Insert(new Kind()
            {
                Id = 1,
                Name = "root",
                ParentId = 0
            });
        }

        ~DatabaseManager1()
        {
            db.Close();
        }

        public class Kind
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [Indexed]
            public int ParentId { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
        }

        public class Data
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public int Index { get; set; }
        }

        public void SaveIndex(int index)
        {
            db.CreateCommand("DELETE FROM Data").ExecuteNonQuery();
            db.Insert(new Data()
            {
                Id = 1,
                Index = index,
            });
        }

        public int GetSavedIndex()
        {
            var list = db.Query<Data>("SELECT * FROM Data");
            if (list.Count == 1) return list[0].Index; else return 1;
        }

        public ObservableCollection<Kinda> GetKindas(int parentId)
        {
            ObservableCollection<Kinda> list = new ObservableCollection<Kinda>();
            foreach (Kind kind in db.Query<Kind>("SELECT * FROM Kind WHERE ParentId = ? ORDER BY Date", parentId))
            {
                list.Add(new Concepter.Kinda() { Name = kind.Name, Id = kind.Id, PId = parentId });
            }
            return list;
        }

        public void DeleteKind(int id)
        {
            db.CreateCommand("DELETE FROM Kind WHERE _id = ? OR ParentId = ?", id, id).ExecuteNonQuery();
        }

        public void AddKind(int parentId, string name, DateTime date)
        {
            db.Insert(new Kind()
            {
                Name = name,
                ParentId = parentId,
                Date = date
            });
        }

        public void EditKind(int id, string newName)
        {
            db.CreateCommand("UPDATE Kind SET Name = ? WHERE _id = ?", newName, id).ExecuteNonQuery();
        }

        public string FindNameById(int id)
        {
            return db.Query<Kind>("SELECT * FROM Kind WHERE _id = ?", id).ToList()[0].Name;
        }

        public int GetParentId(int id)
        {
            return db.Query<Kind>("SELECT * FROM Kind WHERE _id = ?", id).ToList()[0].ParentId;
        }



    }
}
