using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCPLE.Interface;

namespace SCPLE.Model
{
    public class FilePathModel : IFilePathModel
    {
        public bool IsCorrect(string path)
        {
            if (path != "q")
                return true;
            return false;
        }

        public string FilePath
        {
            set { _filePath = value; }
            get { return _filePath; }
        }

        private string _filePath;
    }
}
