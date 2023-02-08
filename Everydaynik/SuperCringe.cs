using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Everydaynik
{
    internal static class SuperCringe
    {
        public static void SaveInfo<T>(T dateinfo)
        {
            String json = JsonConvert.SerializeObject(dateinfo);
            File.WriteAllText("C:\\Users\\gastf\\source\\repos\\Everydaynik\\Everydaynik\\data.json",json);
        }
        public static T getInfo<T>()
        {
            T listinfo = JsonConvert.DeserializeObject<T>(File.ReadAllText("C:\\Users\\gastf\\source\\repos\\Everydaynik\\Everydaynik\\data.json"));
            return listinfo;
        }

    }
}
