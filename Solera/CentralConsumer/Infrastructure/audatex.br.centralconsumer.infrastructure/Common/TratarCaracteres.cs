using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.infrastructure.common
{
    public class TratarCaracteres
    {
        public static string RemoverCaracteresEspeciais(dynamic texto)
        {
            if (texto == null)
                return String.Empty;            

            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto.ToString());
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
