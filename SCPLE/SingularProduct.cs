using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE
{
    public class SingularProduct
    {
        public SingularProduct()
        {
            _singularProduct = new Dictionary<string, string>();
            _singularProduct.Add("детали", "деталь");
            _singularProduct.Add("стандартные", "стандартное");
            _singularProduct.Add("прочие", "прочее");
            _singularProduct.Add("изделия", "изделие");
            _singularProduct.Add("прочие изделия", "прочее изделие");
            _singularProduct.Add("конденсаторы", "конденсатор");
            _singularProduct.Add("микросхемы", "микросхема");
            _singularProduct.Add("резисторы", "резистор");
            _singularProduct.Add("транзисторы", "транзистор");
            _singularProduct.Add("диоды", "диод");
            _singularProduct.Add("светодиоды", "светодиод");
            _singularProduct.Add("аналоговые", "аналоговая");
            _singularProduct.Add("индикаторы", "индикатор");
            _singularProduct.Add("светодиодные", "светодиодный");
            _singularProduct.Add("индикаторы светодиодные", "индикатор светодиодный");
            _singularProduct.Add("светодиодные индикаторы", "индикатор светодиодный");
            _singularProduct.Add("дроссели", "дроссель");
            _singularProduct.Add("стабилитроны", "стабилитрон");
            _singularProduct.Add("супрессоры", "супрессор");
            _singularProduct.Add("материалы", "материал");
            _singularProduct.Add("резонаторы", "резонатор");
            _singularProduct.Add("ионисторы", "ионистор");
            _singularProduct.Add("лампы осветительные", "лампа осветительная");
            _singularProduct.Add("предохранители", "предохранитель");
            _singularProduct.Add("варисторы", "варистор");
            _singularProduct.Add("диодные мосты", "диодный мост");
            _singularProduct.Add("держатели", "держатель");
            _singularProduct.Add("вилки", "вилка");
            _singularProduct.Add("колодки", "колодка");
            _singularProduct.Add("клеммы", "клемма");
            _singularProduct.Add("соединители", "соединитель");
            _singularProduct.Add("джамперы", "джампер");
            _singularProduct.Add("розетки", "розетка");
            _singularProduct.Add("кнопки", "кнопка");
            _singularProduct.Add("микрокнопки", "микрокнопка");
            _singularProduct.Add("микропереключатели", "микропереключатель");
            _singularProduct.Add("трансформаторы", "трансформатор");
        }
        /// <summary>
        /// Возврат имени в ед. числе продукта, соотв. его множеств. числу
        /// </summary>
        /// <param name="key">Множественное число продукта</param>
        /// <returns>Единственное число продукта</returns>
        public string ReturnSingularProductName(string key)
        {
            key = key.ToLower();
            foreach (KeyValuePair<string, string> keyValuePair in _singularProduct)
                if (keyValuePair.Key == key)
                    return keyValuePair.Value;
            return "";
        }

        private readonly Dictionary<string, string> _singularProduct;
    }
}
