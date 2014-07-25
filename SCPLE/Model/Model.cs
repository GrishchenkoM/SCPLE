using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using  System.Reflection;
using SCPLE.Interface;
using Word = Microsoft.Office.Interop.Word;

namespace SCPLE.Model
{
    public class Model : IFilePathModel
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

        #region File Service

        public void FileService(string path)
        {
            application = new Word.Application();
            Object templatePathObj = path;
            
            try
            {
                document = application.Documents.Add
                    (ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error)
            {
                CloseDocument(ref application, ref document);
                throw error;
            }
            //application.Visible = true;

            Word.Table _table = document.Tables[1];

            ProductRepository productRepository = new ProductRepository();
            Product p = new Product("Прочие изделия");
            productRepository.Products.Add(p);
            ReadFile(productRepository, _table);

            // закрываем перечень
            CloseDocument(ref application, ref document);

            // создание спецификации
            application = new Word.Application();
            templatePathObj = "E:\\Мои документы\\Программирование\\C#\\My Projects\\Диплом\\SCPLE\\Modeling\\Template.dot";

            try
            {
                document = application.Documents.Add
                    (ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
                document.SaveAs("newSpecification",".doc");
            }
            catch (Exception error)
            {
                CloseDocument(ref application, ref document);
                throw error;
            }

            Word.Table _table1 = document.Tables[1];
            Word.Table _table2 = document.Tables[2];

            WriteFile(productRepository, _table2);


            #region Помощь

            //string[] temp = new string[100];
            //int k = 0;
            ////Object o = File.Create("D:\\1.txt");
            //for (int i = 1; i < 15; ++i)
            //    for (int j = 1; j < 5; ++j)
            //        temp[k++] = _table.Cell(i, j).Range.Text;

            //MessageBox.Show(temp);
            //System.IO.File.WriteAllText(@"D:\\1.txt", temp);
            //System.IO.File.WriteAllLines(@"D:\\2.txt", temp);
            //_table.Cell(1, 1).Height = 5;

            #endregion
        }

        private void CloseDocument(ref Word._Application application, ref Word._Document document)
        {
            document.Close(ref falseObj, ref missingObj, ref missingObj);
            application.Quit(ref missingObj, ref missingObj, ref missingObj);
            document = null;
            application = null;
        }
        private string DeleteSpaces(string str)
        {
            return str.Replace(" ", "");
        }
        private void DeleteTrash(ref Word.Table _table, int _i)
        {
            for (int i = _i; i <= _i + 1; ++i)
            {
                for (int j = 1; j <= 3; ++j)
                {
                    if (_table.Cell(i, j).Range.Text.Contains("\r") || _table.Cell(i, j).Range.Text.Contains("\a"))
                    {
                        string temp = _table.Cell(i, j).Range.Text.Replace("\r", null);
                        _table.Cell(i, j).Range.Text = temp;

                        temp = _table.Cell(i, j).Range.Text.Replace("\r", null);
                        _table.Cell(i, j).Range.Text = temp;

                        temp = _table.Cell(i, j).Range.Text.Replace("\a", null);
                        _table.Cell(i, j).Range.Text = temp;
                        string t = _table.Cell(i, j).Range.Text;
                    }
                    
                }
            }
        }
        private void ReadFile(ProductRepository productRepository, Word.Table _table)
        {
            for (int i = 2; i <= _table.Rows.Count; ++i)
            {
                int j = 1;
                
                string t = _table.Cell(i, j).Range.Text;
                t = _table.Cell(i, j + 1).Range.Text;

                if ((_table.Cell(i, j).Range.Text != "") && 
                    (_table.Cell(i, j + 1).Range.Text != "") && 
                    _isSame == false)
                {
                    FillingProductElements(productRepository, _table, i, j);
                }
                else if (_table.Cell(i, j).Range.Text != "")
                {
                    AddToSameDesignation(productRepository, _table, i, j);
                }
                else if ((_table.Cell(i, j + 1).Range.Text != "") && _isSame == false)
                {
                    CreateProduct(productRepository, _table, ref i, j + 1);
                }
                else if ((_table.Cell(i, j).Range.Text != "") &&
                         (_table.Cell(i, j + 1).Range.Text != "") &&
                         _isSame == true)
                {
                    AddToSameDesignation(productRepository, _table, i, j);
                    AddToSameName(productRepository, _table, i, j + 1);
                }
                else if ((_table.Cell(i, j + 1).Range.Text != "") && _isSame == true)
                {
                    AddToSameName(productRepository, _table, i, j + 1);
                }
            }
        }
        private void CreateProduct(ProductRepository productRepository, Word.Table _table, ref int i, int j)
        {
            productRepository.Products.Add(new Product(_table.Cell(i, j + 1).Range.Text));
            //DeleteTrash( ref _table, i + 1);
            string manufactures = "";
            while
                ((_table.Cell(i + 1, j - 1).Range.Text == "")
                 &&
                 (_table.Cell(i + 1, j).Range.Text != ""))
            {
                manufactures += (_table.Cell(++i, j).Range.Text + " ");
                //DeleteTrash(ref _table, i + 1);
            }
            productRepository.Products[productRepository.Products.Count - 1].
                manufacturers = manufactures;
        }
        private void FillingProductElements(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            string partOfNewName = DeleteSpaces(_table.Cell(i, j + 1).Range.Text);

            for (int k = 0;
                k < productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count;
                ++k)
            {
                string partOfExistingName =
                    DeleteSpaces(productRepository.Products[productRepository.Products.Count - 1].elements_Name[i].Name)
                        .Substring(0, partOfNewName.Length - 1);

                if (partOfNewName.Equals(partOfExistingName, StringComparison.Ordinal))
                {
                    _sameNamePosition = k;
                    AddToSameDesignation(productRepository, _table, i, j);
                    IsSame(_table, i, j);
                    return;
                }
            }

            productRepository.Products[productRepository.Products.Count - 1].
                elements_Name.
                Add(new ElementNameObject(_table.Cell(i, j + 1).Range.Text));
            productRepository.Products[productRepository.Products.Count - 1].
                elements_Name[productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count - 1].
                elements_Designator += _table.Cell(i, j).Range.Text;

            _sameNamePosition = productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count - 1;
            IsSame(_table, i, j);
        }
        private void AddToSameDesignation(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            //productRepository.Products[productRepository.Products.Count - 1].
            //        elements_Name[
            //            productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count - 1].
            //        elements_Designator[productRepository.Products[productRepository.Products.Count - 1].
            //            elements_Name[
            //                productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count - 1].
            //            elements_Designator.Count - 1].Name += _table.Cell(i, j).Range.Text;
            productRepository.Products[productRepository.Products.Count - 1].
                    elements_Name[_sameNamePosition].
                    elements_Designator += ("|" + _table.Cell(i, j).Range.Text);
        }
        private void AddToSameName(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            //productRepository.Products[productRepository.Products.Count - 1].
            //    elements_Name[
            //        productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count - 1].Name +=
            //    _table.Cell(i, j).Range.Text;
            productRepository.Products[productRepository.Products.Count - 1].
                elements_Name[_sameNamePosition].Name += (" " + _table.Cell(i, j).Range.Text);
        }

        private void WriteFile(ProductRepository productRepository, Word.Table _table)
        {
            int positionNumber = 10;
            _table.Rows.Add();
            _table.Cell(2, 5).Range.Text = productRepository.Products[0].Name;

            int i = 4;
            for (int k = 0; k < productRepository.Products.Count; ++k)
            {
                _table.Rows.Add();
                _table.Cell(i, 5).Range.Text = productRepository.Products[k].manufacturers;
                _table.Rows.Add();
                i += 2;

                for (int j = 0; j < productRepository.Products[k].elements_Name.Count; ++j)
                {
                    _table.Rows.Add();
                    
                    _table.Cell(i, 3).Range.Text = positionNumber.ToString();
                    _table.Cell(i, 5).Range.Text = productRepository.Products[0].elements_Name[j].Name;
                    _table.Cell(i, 6).Range.Text =
                        productRepository.Products[0].elements_Name[j].DesignatorsCount.ToString();
                    _table.Cell(i, 6).Range.Text = productRepository.Products[0].elements_Name[j].elements_Designator;
                    i += 2;
                    positionNumber += 2;
                    _table.Rows.Add();
                }
            }
            
        }

        private void IsSame(Word.Table _table, int i, int j)
        {
            if ((_table.Cell(i, j + 2).Range.Text == ""))
                _isSame = true;
            else
                _isSame = false;
        }

        #endregion

        #region Variables
        private Word._Application application;
        private Word._Document document;

        Object missingObj = System.Reflection.Missing.Value;
        Object trueObj = true;
        Object falseObj = false;
        private bool _beforeCapacitors;
        /// <summary>
        /// Path of file
        /// </summary>
        private string _filePath;
        private bool _isSame;
        private int _sameNamePosition;

        #endregion
    }
}
