using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoingConsole
{
    class Program
    {
        static DataManager DB;
        static void Main(string[] args)
        {
            DB = new DataManager(@"C:\Databases\going.sqlite");
            List<Field> list = new List<Field>();
            //DB.AddFields(Field.GetStandardFields(1000, 1000));
            Console.WriteLine(DB.GetField(1, 2).X);
        }
    }
}
