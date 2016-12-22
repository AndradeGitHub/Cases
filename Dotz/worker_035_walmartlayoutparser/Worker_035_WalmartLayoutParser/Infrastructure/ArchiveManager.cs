using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace worker_WalmartLayoutParser.infrastructure
{
    public static class ArchiveManager
    {
        public static List<string> Get(string directory, string filter)
        {
            try
            {
                return Directory.GetFiles(directory, filter, SearchOption.AllDirectories).ToList();
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);
            }

            return new List<string>();
        }

        public static void Move(string originPath, string destinyPath)
        {
            try
            {
                if (File.Exists(originPath))
                    File.Move(originPath, destinyPath);
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);
            }
        }

        public static void Copy(string originPath, string destinyPath)
        {
            try
            {
                var directory = destinyPath.Substring(0, destinyPath.LastIndexOf('\\'));
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.Copy(originPath, destinyPath);
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);
            }
        }
    }
}