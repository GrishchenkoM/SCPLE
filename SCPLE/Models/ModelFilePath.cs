using System.IO;
using Scple.Interface;

namespace Scple.Models
{
    public class ModelFilePath : IModelFilePath
    {
        #region IModelFilePath
        public bool IsCorrect(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }
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
