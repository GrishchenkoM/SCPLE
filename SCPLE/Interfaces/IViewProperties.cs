using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scple.Interface
{
    /// <summary>
    /// Интерфейс представления Настройки
    /// </summary>
    public interface IViewProperties
    {
        /// <summary>
        /// Инкапсуляция TextBox шаблона спецификации
        /// </summary>
        string SpecificationFileTemplateTxBx {get; set; }
        /// <summary>
        /// Инкапсуляция TextBox файла настроек программы
        /// </summary>
        string SettingsFileTxBx { get; set; }
        /// <summary>
        /// Отображает форму
        /// </summary>
        void ShowForm();
        /// <summary>
        /// Разрешение видимости формы
        /// </summary>
        void IsVisible(bool isVisible);
        /// <summary>
        /// Определение текущего документа
        /// </summary>
        event EventHandler<EventArgs> SetDocument;
        /// <summary>
        /// Определение существования текущего документа
        /// </summary>
        event EventHandler<EventArgs> SetFilePath;
        /// <summary>
        /// Закрытие формы
        /// </summary>
        event EventHandler<EventArgs> CloseForm;
    }
}
