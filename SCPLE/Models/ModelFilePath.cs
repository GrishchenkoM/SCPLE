using System.IO;
using Scple.Interface;

namespace Scple.Models
{
    /// <summary>
    /// Определение корректности адреса файла
    /// </summary>
    public class ModelFilePath : IModelFilePath
    {
        #region IModelFilePath
        /// <summary>
        /// Проверка существования файла по данному пути
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Существует или нет</returns>
        public bool IsCorrect(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }
        /// <summary>
        /// Getter/Setter пути файла
        /// </summary>
        public string FilePath
        {
            set { _filePath = value; }
            get { return _filePath; }
        }
        #endregion
        
        #region Variables
        private string _filePath;
        #endregion
    }
}
