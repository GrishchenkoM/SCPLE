using System.Collections.Generic;

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
            _singularProduct = new Dictionary<string, string>();
            _singularProduct.Add("детали",           "деталь");
            _singularProduct.Add("стандартные",      "стандартное");
            _singularProduct.Add("прочие изделия",   "прочее изделие");
            _singularProduct.Add("конденсаторы",     "конденсатор");
            _singularProduct.Add("микросхемы",       "микросхема");
            _singularProduct.Add("резисторы",        "резистор");
            _singularProduct.Add("транзисторы",      "транзистор");
            _singularProduct.Add("диоды",            "диод");
            _singularProduct.Add("аналоговые",       "аналоговая");
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
        /// <summary>
        /// Возврат имени в ед. числе продукта, соотв. его множеств. числу
        /// </summary>
        /// <param name="key">Множественное число продукта</param>
        /// <returns>Единственное число продукта</returns>
        private string RespSingularProductName(string key)
        {
            foreach (KeyValuePair<string, string> keyValuePair in _singularProduct)
                if (keyValuePair.Key == key)
                    return keyValuePair.Value;
            return "";
        }
        /// <summary>
        /// Имя продукта в единичном числе
        /// </summary>
        public string SingularName
        {
            get { return RespSingularProductName(_name.ToLower()); }
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
        private readonly Dictionary<string, string> _singularProduct;
#endregion

    }
}
