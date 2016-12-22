using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace audatex.br.centralpublisher.ws
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
 
            static void Main()
        {
#if (!DEBUG)

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new CentralPublisher()
            };
            ServiceBase.Run(ServicesToRun);
#else

                CentralPublisher CentralPublisher = new CentralPublisher();
                // Chamada do seu método para Debug.
                CentralPublisher.Start();
                // Coloque sempre um breakpoint para o ponto de parada do seu código.
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);


#endif
            }
        }
}
