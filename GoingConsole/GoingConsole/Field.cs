using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace GoingConsole
{
    public class Field
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Available { get; set; }

        public static List<Field> GetStandardFields(int x, int y)
        {
            List<Field> list = new List<Field>();
            for (int i = 1; i <= x; i++)
            {
                for (int j = 1; j <= y; j++)
                {
                    list.Add(new Field { X = i, Y = j, Available = 1 });
                }
            }
            return list;
        }
    }
}
