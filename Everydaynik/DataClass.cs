using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everydaynik
{
    internal class DataClass
    {
        public DateOnly Date;
        public string Name { get; set; }
        public string Description;
        
        public DataClass(DateOnly date, string _name, string _d)
        {
            Date = date;
            Name = _name;
            Description = _d;
        }
    }
}
