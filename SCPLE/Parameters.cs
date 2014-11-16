using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Scple
{
    /// <summary>
    /// Параметры создания файла спецификации и всей программы
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Сборочный чертеж
        /// </summary>
        public bool AssemblyDrawing;
        /// <summary>
        /// Схема электрическая
        /// </summary>
        public bool ElectricalCircuit;
        /// <summary>
        /// Перечень элементов
        /// </summary>
        public bool ListOfitems;
        /// <summary>
        /// Плата. Данные конструкции
        /// </summary>
        public bool Pcb;
        /// <summary>
        /// Удостоверяющий лист
        /// </summary>
        public bool CertifyingSheet;
        /// <summary>
        /// Элементы SMD монтажа
        /// </summary>
        public bool ElementsOfSmdMounting;
        /// <summary>
        /// Заимствованные изделия
        /// </summary>
        public bool BorrowedItems;
        /// <summary>
        /// Создать файл .doc
        /// </summary>
        public bool FileDoc;
        /// <summary>
        /// Создать файл .xls
        /// </summary>
        public bool FileXls;
        /// <summary>
        /// Первая часть децимального номера спецификации
        /// </summary>
        public string DesignDocFirstString;
        /// <summary>
        /// Вторая часть децимального номера спецификации
        /// </summary>
        public string DesignDocSecondString;
        /// <summary>
        /// Первая часть децимального номера платы
        /// </summary>
        public string DesignPcbFirstString;
        /// <summary>
        /// Вторая часть децимального номера платы
        /// </summary>
        public string DesignPcbSecondString;
        /// <summary>
        /// Формат документа "Плата. Данные конструкции"
        /// </summary>
        public string PcbFormat;
        /// <summary>
        /// Формат документа "Сборочный чертеж"
        /// </summary>
        public string AssemblyDrawingFormat;
        /// <summary>
        /// Формат документа "Схема электрическая"
        /// </summary>
        public string ElectricalCircuitFormat;
        /// <summary>
        /// Формат документа "Удостоверяющий лист"
        /// </summary>
        public string CertifyingSheetFormat;
        /// <summary>
        /// Формат документа "Перечень элементов"
        /// </summary>
        public string ListOfitemsFormat;
        /// <summary>
        /// Адрес файла-шаблонга спецификации
        /// </summary>
        public string TemplateFilePath;
        /// <summary>
        /// Адрес файла настроек программы
        /// </summary>
        public string SettingsFilePath;
        /// <summary>
        /// Список идентификаторов SMD элемента
        /// </summary>
        public Collection<string> SmdIdentificators;
        /// <summary>
        /// Адрес файла со списком SMD идентификаторв
        /// </summary>
        public string SmdIdentificatorsFilePath;
        /// <summary>
        /// Начальная позиция прочих изделий
        /// </summary>
        public string SourcePosition;
        /// <summary>
        /// Печатать шапку в файле Excell
        /// </summary>
        public bool Hat;
        /// <summary>
        /// Печатать первый лист спецификации в файле Excell
        /// </summary>
        public bool FirstPage;
        /// <summary>
        /// Добавлять к каждому элементу его название
        /// </summary>
        public bool RatingPlusName;
    }
}
