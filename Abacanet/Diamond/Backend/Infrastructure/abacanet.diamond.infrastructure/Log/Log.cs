using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace abacanet.diamond.infrastructure.log
{
    public class Log
    {
        private static readonly ILog Logger = LogManager.GetLogger(string.Empty);

        public static void RecordError(Exception ex)
        {
            Logger.Error("# ERROR: ", ex);
        }

        public static void RecordInfo(string info)
        {
            Logger.Info(info);
        }

        public static void RecordWarning(string warning)
        {
            Logger.Warn(warning);
        }
        public static void RecordDebug(Exception debug)
        {
            Logger.Debug(debug);
        }        
    }
}
