using System;
using System.IO;

namespace Scple
{
    /// <summary>
    /// Record unhandled exceptions in the log file
    /// </summary>
    public class UnhandledException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ex"></param>
        public UnhandledException(Exception ex)
        {
            StreamWriter sw = null;

            string executablePath = FindExePath.ExePath();
            string pathOfResources = executablePath.Substring(0, executablePath.LastIndexOf('\\') + 1) + "Resources";
            try
            {
                if (!Directory.Exists(pathOfResources))
                    Directory.CreateDirectory(pathOfResources);
                if (!File.Exists(pathOfResources + "\\log.txt"))
                    sw = File.CreateText(pathOfResources + "\\log.txt");
                else
                    sw = File.AppendText(pathOfResources + "\\log.txt");
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine("Описание: " + ex.Message);
                sw.WriteLine("Метод: " + ex.TargetSite.Name);
                sw.WriteLine("Расположение: " + ex.StackTrace);
                sw.WriteLine();
                sw.Close();
                sw = null;
            }
            catch (Exception){}
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
