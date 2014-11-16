using System;

namespace Scple.Interface
{
    /// <summary>
    /// Интерфейс класса "Представление" для его использования в "Presenter"
    /// </summary>
    public interface IViewFileCreation
    {
        /// <summary>
        /// Инициализация внешних параметров
        /// </summary>
        /// <param name="parameters">Параметры</param>
        void Initialization(Parameters parameters);
        /// <summary>
        /// Внешний метод для предоставления доступа к форме
        /// </summary>
        void IsEnabled();
        /// <summary>
        /// Инициализация в Presenter нового представления перед его вызовом
        /// </summary>
        event EventHandler<EventArgs> ProcessingView;
        /// <summary>
        /// Запуск создания спецификации
        /// </summary>
        event EventHandler<EventArgs> StartCreating;
        /// <summary>
        /// Закрытие формы
        /// </summary>
        event EventHandler<EventArgs> CloseForm;
    }
}
