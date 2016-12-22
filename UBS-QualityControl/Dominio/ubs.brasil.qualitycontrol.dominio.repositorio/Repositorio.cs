using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class Repositorio<T> : IRepositorio<T>
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

        public virtual List<T> SelecionarRegistro(T entidade)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> SelecionarRegistroDetalhe(T entidade)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> SelecionarRegistroDetalheItem(T entidade)
        {
            throw new NotImplementedException();
        } 

        public virtual List<T> SelecionarRegistroPorId(List<int> id)
        {
            throw new NotImplementedException();
        }        

        public virtual List<T> SelecionarTudo()
        {
            throw new NotImplementedException();
        }                
    }
}
