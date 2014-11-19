using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using NUnit.Framework;
using Word = Microsoft.Office.Interop.Word;

namespace SCPLETestProject
{
    public static class InitWord
    {
        private static Word._Application _applicationWord;
        private static Word._Document _documentWord;
        //private Parameters _parameters;
        private static Object _missingObj = System.Reflection.Missing.Value;
        private static Object _falseObj = false;
        private static Word.Table _table;

        private static string[] _filePath =
        {
            "D:\\ПЭ 122Х6.doc",
            "D:\\AAOT.687254.005.doc",
            "D:\\Unknown.doc",
            "D:\\NotCreated.doc"
        };
        
        public enum IsDocument
        {
            LIST = 0, SPECIFICATION, UNKNOWN, NOTCREATED
        }
        public static string FilePath(IsDocument isDocument)
        {
            return _filePath[(int) isDocument];
        }
        public static Word.Table Table
        {
            get { return _table; }
        }

        public static void InitializeTable(IsDocument isDocument)
        {
            Object path = FilePath(isDocument);

            _applicationWord = new Word.Application();
            _documentWord = _applicationWord.Documents.Add
                (ref path, ref _missingObj, ref _missingObj, ref _missingObj);
            try
            {
                _table = _documentWord.Tables[1];
            }
            catch (Exception e)
            {
                _table = null;
            }
        }

        public static void Close()
        {
            _documentWord.Close(ref _falseObj, ref _missingObj, ref _missingObj);
            _applicationWord.Quit(ref _missingObj, ref _missingObj, ref _missingObj);
        }
    }
}
