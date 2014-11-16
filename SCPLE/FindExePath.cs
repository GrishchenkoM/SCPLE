using System.Diagnostics;

namespace Scple
{
    public static class FindExePath
    {
        public static string ExePath()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }
    }
}
