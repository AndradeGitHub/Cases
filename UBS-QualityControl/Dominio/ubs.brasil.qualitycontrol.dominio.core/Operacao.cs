using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ubs.brasil.qualitycontrol.dominio.core.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class Operacao<T> : IOperacao<T>
    {                
        public virtual int Gravar(List<T> entidade)
        {
            throw new NotImplementedException();
        }

        public virtual int Alterar(List<T> entidade)
        {
            throw new NotImplementedException();
        }

        public virtual int Apagar(List<T> entidade)
        {
            throw new NotImplementedException();
        }

        public virtual int Processar(List<T> entidade)
        {
            throw new NotImplementedException();
        }

        public virtual int GravarLog(List<T> entidade)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> SelecionarLogProcessamentoEmEspera(T entidade)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> SelecionarLogCargaEmEspera(T entidade)
        {
            throw new NotImplementedException();
        }        
    }
}
