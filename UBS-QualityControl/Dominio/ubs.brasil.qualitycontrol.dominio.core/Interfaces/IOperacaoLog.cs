using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ubs.brasil.qualitycontrol.comum.entidade;

namespace ubs.brasil.qualitycontrol.dominio.core.interfaces
{
    public interface IOperacaoLog
    {
        int Gravar(List<LogOperacao> entidade);

        List<LogOperacao> SelecionarRegistro(LogOperacao entidade);
    }
}
