using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace Scple
{
    /// <summary>
    /// Раздел элементов
    /// </summary>
    public class Product
    {
#region Constructor
        /// <summary>
        /// Конструтор
        /// </summary>
        /// <param name="name"></param>
        public Product(string name)
        {
            _name = name;
            ElementsName = new List<ElementNameObject>();
            Manufacturers = new List<string>();
        }
#endregion

#region Auxiliary
        /// <summary>
        /// Инкапсуляция имени продукта
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        
#endregion

#region Variables
        /// <summary>
        /// Контейнер производителей
        /// </summary>
        public List<string> Manufacturers;
        /// <summary>
        /// Контейнер наименований элементов
        /// </summary>
        public List<ElementNameObject> ElementsName;
        /// <summary>
        /// Контейнер позиций элементов
        /// </summary>
        private readonly string _name;
        #endregion

    }
}
