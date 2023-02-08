using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everydaynik
{
    internal interface CRUD
    {   
        public void Create(string Name, string Desc);
        public void Update();
        public void Delete();
        public void Read(DateTime Display);
    }
}
