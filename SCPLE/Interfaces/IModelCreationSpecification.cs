using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Scple.Interfaces
{
    /// <summary>
    /// Интерфейс класса "Модель" для его использования в "Presenter"
    /// </summary>
    public interface IModelCreationSpecification
    {
        /// <summary>
        /// Закрыть все документы
        /// </summary>
        void CloseAll();
        /// <summary>
        /// Работа с файлом
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        void FileService(string path);
        /// <summary>
        /// Обновление параметров
        /// </summary>
        /// <param name="parameters">Параметры</param>
        void SetParameters(Parameters parameters);
        /// <summary>
        /// Изменение величины заполнения ProgressBar
        /// </summary>
        event EventHandler<EventArgs> ChangeProgressBar;
        /// <summary>
        /// Изменение статуса ReadListStatusLabel
        /// </summary>
        event EventHandler<EventArgs> ChangeReadListStatusLabel;
        /// <summary>
        /// Изменение статуса CreateSpecStatusLabel
        /// </summary>
        event EventHandler<EventArgs> ChangeCreateSpecStatusLabel;
        /// <summary>
        /// Изменение общего статуса StatusLabel
        /// </summary>
        event EventHandler<EventArgs> ChangeStatusLabel;
        /// <summary>
        /// Изменение статуса кнопки "Отмена"
        /// </summary>
        event EventHandler<EventArgs> ChangeStatusButton;
        /// <summary>
        /// Изменение статуса файла
        /// </summary>
        event EventHandler<EventArgs> ChangeReadFileStatus;
    }
}
