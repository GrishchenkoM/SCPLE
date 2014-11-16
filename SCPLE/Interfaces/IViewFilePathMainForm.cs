using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace Scple.Interface
{
    /// <summary>
    /// Интерфейс класса "Представление" для его использования в "Presenter"
    /// </summary>
    public interface IViewFilePathMainForm : IView
    {
        /// <summary>
        /// Инкапсуляция TextBox шаблона спецификации
        /// </summary>
        string SpecificationTemplateFileNameTxbx { get; set; }
        /// <summary>
        /// Инкапсуляция TextBox файла-перечня элементов
        /// </summary>
        string ListFileNameTxbx { get; set; }
        /// <summary>
        /// Определение существования документов списка DocumentList
        /// </summary>
        /// <param name="isOk">Существование документа</param>
        /// <param name="documentList">Наименование документа</param>
        void IsOk(bool isOk, DocumentList documentList);
        /// <summary>
        /// Разрешение видимости формы
        /// </summary>
        /// <param name="isVisible">Ключ разрешения</param>
        void IsVisible(bool isVisible);
        /// <summary>
        /// Обновление переменной настроек прогроаммы
        /// </summary>
        /// <param name="propertiesView"></param>
        void SetPropertiesView(IViewProperties propertiesView);
        /// <summary>
        /// Определение текущего документа
        /// </summary>
        event EventHandler<EventArgs> SetDocument;
        /// <summary>
        /// Определение существования текущего документа
        /// </summary>
        event EventHandler<EventArgs> SetFilePath;
        /// <summary>
        /// Инициализация в Presenter нового представления перед его вызовом
        /// </summary>
        event EventHandler<EventArgs> CreateScView;
    }
}
