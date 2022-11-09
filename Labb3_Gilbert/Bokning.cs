using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_Gilbert
{
    public partial class MainWindow
    {
        public class Bokning : IBokning
        {
            public string Name { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Tablenumber { get; set; }


            public Bokning(string namn, string date, string time, string tablenumber)
            {
                Name = namn;
                Date = date;
                Time = time;
                Tablenumber = tablenumber;
            }

        }
    }
}
