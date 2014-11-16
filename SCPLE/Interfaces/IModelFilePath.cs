using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scple.Interface
{
    /// <summary>
    /// Интерфейс класса "Модель" для его использования в "Presenter"
    /// </summary>
    public interface IModelFilePath
    {
        /// <summary>
        /// Getter/Setter пути файла
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// Проверка существования файла по данному пути
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Существует или нет</returns>
        bool IsCorrect(string path);
    }
}
