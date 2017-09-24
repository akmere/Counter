using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Projecter
{
    class DatabaseManager1
    {
        SQLiteConnection db;
        public DatabaseManager1(string dbPath)
        {
            db = new SQLiteConnection(DependencyService.Get<IFileHelper1>().GetLocalFilePath(dbPath));
            db.CreateTable<Projecto>();
        }

        ~DatabaseManager1()
        {
            db.Close();
        }

        public class Projecto
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            public string Name { get; set; }
            public int Priority { get; set; }
        }

        public void AddProjecto(string name, int priority)
        {
            var it = new Projecto()
            {
                Name = name,
                Priority = priority
            };
            db.Insert(it);
        }

        public int GetProjectoIdByName(string name)
        {
            var result = db.Query<Projecto>("SELECT * FROM Projecto WHERE Name=?", name);
            return result[0].Id;
        }

        public int GetProjectoPriorityByName(string name)
        {
            var result = db.Query<Projecto>("SELECT * FROM Projecto WHERE Name=?", name);
            return result[0].Priority;
        }

        public void EditProjecto(string oldName, string newName, int priority)
        {
            var it = new Projecto()
            {
                Name = newName,
                Priority = priority,
                Id = GetProjectoIdByName(oldName)
            };
            db.Update(it);
        }

        public void DeleteProjecto(string name)
        {
            db.Delete(new Projecto()
            {
                Id = GetProjectoIdByName(name),
                Name = name

            });
        }

        public ObservableCollection<Project> GetProjects()
        {
            var result = db.Query<Projecto>("Select * FROM Projecto");
            ObservableCollection<Project> roc = new ObservableCollection<Project>();
            foreach (Projecto x in result)
            {
                roc.Add(new Project()
                {
                    Name = x.Name,
                    Priority = x.Priority
                });
            }
            return roc;
        }
    }
}
