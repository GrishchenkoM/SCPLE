using System;
using System.Drawing;

namespace Scple.Interface
{
    /// <summary>
    /// Интерфейс класса "Представление" для его использования в "Presenter"
    /// </summary>
    public interface IViewProcessing
    {
        /// <summary>
        /// Getter перечня Parameters
        /// </summary>
        Parameters Parameters { get; }
        /// <summary>
        /// Закрыть форму
        /// </summary>
        void CloseForm();
        /// <summary>
        /// Запуск создания спецификации
        /// </summary>
        void StartCreating();
        /// <summary>
        /// Изменение текущего значения ProgressBar
        /// </summary>
        /// <param name="i">Текущее значение</param>
        void ChangeProgressBar(int i);
        /// <summary>
        /// Изменение Label, отвечающего за "Чтение файла перечня элементов"
        /// </summary>
        /// <param name="status">Состояние готовности</param>
        /// <param name="color">Цвет, соответствующий состоянию готовности</param>
        void ChangeReadListStatusLabel(string status, Color color);
        /// <summary>
        /// Изменение Label, отвечающего за "Создание файла спецификации"
        /// </summary>
        /// <param name="status">Состояние готовности</param>
        /// <param name="color">Цвет, соответствующий состоянию готовности</param>
        void ChangeCreateSpecStatusLabel(string status, Color color);
        /// <summary>
        /// Изменение Label, отвечающего за общее состояние готовности
        /// </summary>
        /// <param name="status">Состояние готовности</param>
        /// <param name="color">Цвет, соответствующий состоянию готовности</param>
        void ChangeStatusLabel(string status, Color color);
        /// <summary>
        /// Изменение статуса (имени) кнопки "Отмена"
        /// </summary>
        /// <param name="name">Новое имя</param>
        void ChangeStatusButton(string name);
        /// <summary>
        /// Изменение Lable, отвечающее за отображение читаемого файла
        /// </summary>
        /// <param name="name"></param>
        void ChangeReadFileStatus(string name);
        /// <summary>
        /// Метод, отвечающий за запуск/остановку создания спецификации
        /// </summary>
        event EventHandler<EventArgs> OpenFile;
        /// <summary>
        /// Прекращение создания спецификации
        /// </summary>
        event EventHandler<EventArgs> StopCreating;
    }
}
