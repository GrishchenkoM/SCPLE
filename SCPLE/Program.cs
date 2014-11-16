using System;
using System.Threading;
using System.Windows.Forms;
using Scple.Presenters;


[assembly: CLSCompliant(true)]
namespace Scple
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool onlyInstance;

            Mutex mtx = new Mutex(true, "Spec-Creator", out onlyInstance);

            // If no other process owns the mutex, this is the
            // only instance of the application.
            if (onlyInstance)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                MainFormView _mainFormView = new MainFormView();
                Presenter _presenter = new Presenter(_mainFormView);
                Application.Run(_mainFormView);
            }
            else
            {
                MessageBox.Show("Приложение уже запущено", "Сообщение", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
