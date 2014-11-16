using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Scple.Interface
{
    /// <summary>
    /// Интерфейс Presenter
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Запуск главного Представления
        /// </summary>
        void Run();
        /// <summary>
        /// Изменение параметров по умолчанию
        /// </summary>
        /// <param name="newData">Новая информация</param>
        /// <param name="name1">Тэг внешний</param>
        /// <param name="name2">Тэг внутренний</param>
        void ChangeParameters(object newData, string name1, string name2);
        /// <summary>
        /// Запись измененного списка SMD идентификаторов в файл
        /// </summary>
        /// <param name="SmdIdentificators">Список SMD идентификаторов</param>
        void SaveSmdDesignatorsListToFile(Collection<string> SmdIdentificators);
    }
}
