using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace audatex.br.centralpublisher.infrastructure.log
{
    public class Log
    {
        private static readonly ILog Logger = LogManager.GetLogger(string.Empty);

        public static void RecordError(Exception ex)
        {
            Console.Write(string.Concat("[", DateTime.Now, "] ERROR: ", ex));
            Console.WriteLine();

            Logger.Error(string.Concat("# ERROR: ", ex));
        }

        public static void RecordError(Exception ex, string msg)
        {
            Console.Write(string.Concat("[", DateTime.Now, "] ERROR: ", ex, " - ", msg));
            Console.WriteLine();

            Logger.Error(string.Concat("# ERROR: ", ex, " - ", msg));
        }

        public static void RecordInfo(string info)
        {
            Console.Write(string.Concat("[", DateTime.Now, "] INFO: ", info));
            Console.WriteLine();

            Logger.Info(info);
        }

        public static void RecordWarning(string warning)
        {
            Console.Write(string.Concat("[", DateTime.Now, "] WARNING: ", warning));
            Console.WriteLine();

            Logger.Warn(warning);
        }
        public static void RecordDebug(Exception debug)
        {
            Console.Write(string.Concat("[", DateTime.Now, "] DEBUG: ", debug));
            Console.WriteLine();

            Logger.Debug(debug);
        }
    }
}

