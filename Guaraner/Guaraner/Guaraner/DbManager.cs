using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Guaraner
{
    class DbManager
    {
        SQLiteConnection db;
        public DbManager(string dbPath)
        {
            db = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath(dbPath));
            db.CreateTable<Guaranax>();
        }

        ~DbManager()
        {
            db.Close();
        }

        public class Guaranax
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string Hour { get; set; }
        }

        public void AddDose(DateTime d)
        {
            db.Insert(new Guaranax()
            {
                Date = d.Date,
                Hour = d.ToString("HH:mm:ss")
            });
        }

        public void RemoveDose(DateTime d, string h)
        {
            db.CreateCommand("DELETE FROM Guaranax WHERE Date=? AND Hour=?", d, h).ExecuteNonQuery();
        }

        public ObservableCollection<Dosie> GetDosies(DateTime d)
        {
            var list = db.Query<Guaranax>("SELECT * FROM GUARANAX WHERE Date=? ORDER BY Hour ASC", d).ToList();
            ObservableCollection<Dosie> listek = new ObservableCollection<Dosie>();
            foreach (Guaranax g in list)
            {
                listek.Add(new Dosie() { Date = g.Date.ToString("dd.MM.yy"), Hour = g.Hour });
            }
            return listek;
        }


    }
}
