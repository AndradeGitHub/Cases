using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public static class RepositorioFabrica
    {
        public static IRepositorio<T> CriarRepositorio<T, R>(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();

            container.RegisterType(typeof(IRepositorio<T>), typeof(R)).RegisterInstance(repositorioModeloQC)
                                                                      .RegisterInstance(repositorioModeloWM_DB);

            return container.Resolve<IRepositorio<T>>();
        }
    }
}
