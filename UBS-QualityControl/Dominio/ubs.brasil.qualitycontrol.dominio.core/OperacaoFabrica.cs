using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.core
{
    public static class OperacaoFabrica 
    {
        public static IOperacao<T> CriarOperacao<T, O>(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {              
            IUnityContainer container = new UnityContainer();            

            container.LoadConfiguration();            

            container.RegisterType(typeof(IOperacao<T>), typeof(O)).RegisterInstance(repositorioModeloQC)
                                                                   .RegisterInstance(repositorioModeloWM_DB);                        
            
            return container.Resolve<IOperacao<T>>();
        }

        public static IOperacaoLog CriarOperacaoLog<O>(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();

            container.RegisterType(typeof(IOperacaoLog), typeof(O)).RegisterInstance(repositorioModeloWM_DB);

            return container.Resolve<IOperacaoLog>();
        }
    }
}
