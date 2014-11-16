using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scple.Interface
{
    /// <summary>
    /// Интерфейс работы с формой
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Отображает форму
        /// </summary>
        void Show();
        /// <summary>
        /// Закрывает форму
        /// </summary>
        void Close();
        /// <summary>
        /// Отображение сообщения об ошибке в MessageBox
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        void ShowError(string errorMessage);
    }
}
