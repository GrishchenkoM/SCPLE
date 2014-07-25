using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SCPLE.Model;

namespace SCPLE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainFormView _mainFormView = new MainFormView();
            Presenter.Presenter _presenter = new Presenter.Presenter(_mainFormView);
            Application.Run(_mainFormView);
        }
    }
}
