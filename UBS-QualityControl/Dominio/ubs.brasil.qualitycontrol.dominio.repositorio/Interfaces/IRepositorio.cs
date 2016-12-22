using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ubs.brasil.qualitycontrol.dominio.repositorio.interfaces
{
    public interface IRepositorio<T>
    {
        int Gravar(List<T> entidade);
        int Alterar(List<T> entidade);
        int Apagar(List<T> entidade);
        int Processar(List<T> entidade);
        List<T> SelecionarRegistro(T entidade);
        List<T> SelecionarRegistroDetalhe(T entidade);
        List<T> SelecionarRegistroDetalheItem(T entidade);
        List<T> SelecionarRegistroPorId(List<int> id);
        List<T> SelecionarTudo();        
    }
}
