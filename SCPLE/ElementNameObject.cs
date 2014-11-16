using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Scple
{
    /// <summary>
    /// Наименование элементов
    /// </summary>
    public class ElementNameObject
    {
        #region Constructor
        /// <summary>
        /// Конструктор наименования элементов
        /// </summary>
        /// <param name="name"></param>
        public ElementNameObject(string name)
        {
            ElementsDesignator = new List<string>();
            _name = name;
            _designatorsCount = 0;
        }
        #endregion
        
        /// <summary>
        /// Инкапсуляция имени наименования элементов
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// Инкапсуляция количества элементов
        /// данного наименования
        /// </summary>
        public int DesignatorsCount
        {
            get { return _designatorsCount; }
            set { _designatorsCount = value; }
        }
        /// <summary>
        /// Инкапсуляция позиции данного элемента
        /// </summary>
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }
        /// <summary>
        /// Инкапсуляция формата данного элемента
        /// </summary>
        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }
        /// <summary>
        /// Инкапсуляция Наименования
        /// </summary>
        public string Designation
        {
            get { return _designation; }
            set { _designation = value; }
        }
        
        #region Variables
        /// <summary>
        /// Контейнер обозначений элементов
        /// </summary>
        public List<string> ElementsDesignator;
        private string _name;
        private int _designatorsCount;
        private string _position;
        private string _format;
        private string _designation;
        
        #endregion
    }

}
