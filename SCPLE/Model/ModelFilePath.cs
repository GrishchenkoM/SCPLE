using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using  System.Reflection;
using SCPLE.Interface;



namespace SCPLE.Model
{
    public class ModelFilePath : IFilePathModel
    {
        #region Existence of a file
        /// <summary>
        /// Checking the existence of a file by path
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <returns>Existing or not</returns>
        public bool IsCorrect(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }
        /// <summary>
        /// Getter/Setter of file path
        /// </summary>
        public string FilePath
        {
            set { _filePath = value; }
            get { return _filePath; }
        }
        #endregion

        

        #region Variables
        

        //Object missingObj = System.Reflection.Missing.Value;
        //Object trueObj = true;
        //Object falseObj = false;
        private bool _beforeCapacitors;
        /// <summary>
        /// Path of file
        /// </summary>
        private string _filePath;
        //private bool _isSame;
        //private int _sameNamePosition;

        #endregion
    }
}
