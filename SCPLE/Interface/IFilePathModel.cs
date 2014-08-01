using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE.Interface
{
    public interface IFilePathModel
    {
        bool IsCorrect(string path);
        string FilePath { get; set; }

        void FileService(string path);
    }
}
