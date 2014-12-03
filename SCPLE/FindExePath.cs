using System.Diagnostics;

namespace Scple
{
    /// <summary>
    /// Определение адреса исполняемого файла
    /// </summary>
    public static class FindExePath
    {
        /// <summary>
        /// Статический метод определения адреса исполняемого файла
        /// </summary>
        /// <returns></returns>
        public static string ExePath()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }
    }
}
