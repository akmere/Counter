using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoingConsole
{
    public class DataManager
    {
        SQLiteConnection MainConnection;
        static Random r = new Random((int)DateTime.Now.Ticks);

        public DataManager(string file)
        {
            //MainConnection = new SQLiteConnection(getPath(file));
            MainConnection = new SQLiteConnection(file);
            MainConnection.CreateTable<Naming>();
            MainConnection.CreateTable<Field>();
        }

        public void InsertName(string name, string kind, int frequency)
        {
            MainConnection.Insert(new Naming() { Frequency = frequency, Kind = kind, Name = name });

        }

        public string drawFullName(string kind)
        {
            var x = MainConnection.Query<Naming>("SELECT Name FROM Naming WHERE Kind=? ORDER BY Frequency DESC LIMIT 50", kind);
            var y = MainConnection.Query<Naming>("SELECT Name FROM Naming WHERE Kind='S' ORDER BY Frequency DESC LIMIT 50");
            return x[r.Next(50)].Name + " " + y[r.Next(50)].Name;
        }

        public List<string> drawAllFullName(string kind, int quantity)
        {
            var x = MainConnection.Query<Naming>("SELECT Name FROM Naming WHERE Kind=? ORDER BY Frequency DESC LIMIT 50", kind);
            var y = MainConnection.Query<Naming>("SELECT Name FROM Naming WHERE Kind='S' ORDER BY Frequency DESC LIMIT 50");
            List<string> list = new List<string>();
            for (int i = 0; i < quantity; i++)
            {
                list.Add(x[r.Next(50)].Name + " " + y[r.Next(50)].Name);
            }
            return list;
        }

        public List<string> drawAllSurname(int quantity)
        {
            var x = MainConnection.Query<Naming>("SELECT Name FROM Naming WHERE Kind='S' ORDER BY Frequency DESC LIMIT 150");
            List<string> list = new List<string>();
            for (int i = 0; i < quantity; i++)
            {
                list.Add(x[r.Next(50)].Name);
            }
            return list;
        }

        public List<string> drawAllFirstName(string kind, int quantity)
        {
            var x = MainConnection.Query<Naming>("SELECT Name FROM Naming WHERE Kind=? ORDER BY Frequency DESC LIMIT 150", kind);
            List<string> list = new List<string>();
            for (int i = 0; i < quantity; i++)
            {
                list.Add(x[r.Next(50)].Name);
            }
            return list;
        }

        public string drawName(string kind)
        {
            var x = MainConnection.Query<Naming>("SELECT Name FROM Naming WHERE Kind=? ORDER BY Frequency DESC LIMIT 50", kind);
            return x[r.Next(50)].Name;
        }

        public class Naming
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Kind { get; set; }
            public int Frequency { get; set; }
        }

        string getPath(string name)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "Databases" + "\\" + name + ".sqlite";
        }

        public void AddFields(List<Field> list)
        {
            MainConnection.InsertAll(list);
        }

        public Field GetField(int x, int y)
        {
            return MainConnection.Query<Field>("SELECT * FROM Field WHERE X=? AND Y=?", x, y).First();
        }
    }
}
