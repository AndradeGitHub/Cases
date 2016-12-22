using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ubs.brasil.qualitycontrol.dominio.core.interfaces
{
    public interface IOperacao<T>
    {
        int Gravar(List<T> entidade);
        int Alterar(List<T> entidade);
        int Apagar(List<T> entidade);
        int Processar(List<T> entidade);
        int GravarLog(List<T> entidade);

        List<T> SelecionarLogProcessamentoEmEspera(T entidade);
        List<T> SelecionarLogCargaEmEspera(T entidade);           
    }
}
