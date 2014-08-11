using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE
{
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
        public bool ElementsOfSMDMounting;
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

        public string PcbFormat;
        public string AssemblyDrawingFormat;
        public string ElectricalCircuitFormat;
        public string CertifyingSheetFormat;
        public string ListOfitemsFormat;

        public string TemplateFilePath;
        public string SettingsFilePath;
    }
}
