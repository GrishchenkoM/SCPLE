using System;

namespace Scple.Interfaces
{
    /// <summary>
    /// Интерфейс обработки исключений
    /// </summary>
    public interface IHandling
    {
        /// <summary>
        /// Обработка программной ошибки с последующей записью в log-файл
        /// </summary>
        /// <param name="ex">Исключение</param>
        void Handling(Exception ex);
        /// <summary>
        /// Выдача сообщения пользователю об ошибке, 
        /// возникшей в конкретном месте обрабатываемого документа
        /// </summary>
        /// <param name="ex">Исключение</param>
        /// <param name="row">Номер строки, в которой возникло исключение</param>
        void Error(Exception ex, int row);
    }
}
