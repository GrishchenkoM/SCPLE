using System;
using System.IO;
using System.Windows.Forms;

namespace Scple
{
    public class Helper
    {
        public Helper()
        {
            string executablePath = FindExePath.ExePath();
            string pathOfHelper = executablePath.Substring(0, executablePath.LastIndexOf('\\') + 1) + "Resources\\" + "Spec-Creator_manual.chm";
            try
            {
                if (File.Exists(pathOfHelper))
                    //File.Open(pathOfHelper, FileMode.Open);
                    Help.ShowHelp(new Control(), pathOfHelper);
                else
                    MessageBox.Show("Файл не существует");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
