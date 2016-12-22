using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Threading.Tasks;

using AxFornecimento;

using audatex.br.centralpublisher.infrastructure.invoke.interfaces;
using audatex.br.centralpublisher.infrastructure.log;

namespace audatex.br.centralpublisher.infrastructure.invoke
{
    public class ComponentInvoke
    {
        private clsFornecimento _axFornecimento;

        public ComponentInvoke()
        {
            _axFornecimento = new clsFornecimento();
        }

        public string Processar(XDocument xml)
        {
            var retAxFornecimento = string.Empty;

            try
            {
                var tsk = new Task<string>(() =>
                {
                    retAxFornecimento = _axFornecimento.strProcessar(xml.ToString());

                    return retAxFornecimento;
                });

                tsk.Start();

                if (tsk.Wait(30000))
                    throw new TimeoutException();
            }
            catch (TimeoutException exT)
            {
                retAxFornecimento = "3"; //ERRO TIMEOUT

                Log.RecordInfo(string.Empty);
                Log.RecordError(exT, string.Concat("XML: ", xml));
            }
            catch (Exception ex)
            {
                retAxFornecimento = "4"; //ERRO EXCEPTION

                Log.RecordInfo(string.Empty);
                Log.RecordError(ex, string.Concat("XML: ", xml));
            }

            return retAxFornecimento;
        }
    }
}