using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace audatex.br.audabridge2.infrastructure.common
{
    public static class Converter
    {
        public static T JsonToObject<T>(string strJson)
        {
            T obj = JsonConvert.DeserializeObject<T>(strJson);

            return obj;
        }
    }
}
