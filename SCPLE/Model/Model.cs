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
            Object templatePathObj = path;
            #region Работа с перечнем
            application = new Word.Application();
            
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
            

            ProductRepository productRepository = new ProductRepository();
            productRepository.Products.Add(new Product("Прочие изделия"));
            
            Word.Table _table;

            for (int i = 1; i <= document.Tables.Count; ++i)
            {
                _table = document.Tables[i];
                ReadFile(productRepository, _table);
            }

            // закрываем перечень
            CloseDocument(ref application, ref document);
            #endregion

            #region Работа со спецификацией
            application = new Word.Application();
            templatePathObj = "E:\\Мои документы\\Программирование\\C#\\My Projects\\Диплом\\SCPLE\\Modeling\\Template.dot";
            //templatePathObj = "D:\\Грищенко\\MY!\\программирование\\My Projects\\Диплом\\SCPLE\\Modeling\\Template.dot";

            #region Создание файла
            try
            {
                document = application.Documents.Add
                    (ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
                document.SaveAs("D:\\newSpecification1");
            }
            catch (Exception error)
            {
                CloseDocument(ref application, ref document);
                throw error;
            }
            #endregion

            #region Заполнение файла
            Word.Table _table1 = document.Tables[1];
            Word.Table _table2 = document.Tables[2];
            
            try
            {
                WriteFile(productRepository, _table2);
                document.Save();
            }
            catch (Exception)
            {
                CloseDocument(ref application, ref document);
                return;
            }
            #endregion



            //document.Save();


            CloseDocument(ref application, ref document);
            #endregion

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

        #region Read File

        private void ReadFile(ProductRepository productRepository, Word.Table _table)
        {
            if (!(_table.Cell(1, 1).Range.Text.ToLower().Contains("поз") && _table.Cell(1, 2).Range.Text.ToLower().Contains("наименование")))
            {
                return;
            }
            for (int i = 2; i <= _table.Rows.Count; ++i)
            {
                int j = 1;
                int k, l = 0;

                string t = _table.Cell(i, j).Range.Text;
                t = _table.Cell(i, j + 1).Range.Text;

                //при объединенной ячейке программа рухнет

                int z = _table.Cell(i, j).RowIndex;
                z = _table.Cell(i, j + 1).RowIndex;
                z = _table.Cell(i, j + 2).RowIndex;

                if (!(_table.Cell(i, j).Range.Text.Equals("\r\a")))
                {
                    if (!(_table.Cell(i, j + 1).Range.Text.Equals("\r\a")))
                    {
                        if ((_table.Cell(i, j + 1).Range.Text.ToLower().Contains("не уст") ||
                             _table.Cell(i, j + 1).Range.Text.ToLower().Contains("отсутс")))
                            continue;

                        while ((_table.Cell(i + l, j + 2).Range.Text.Equals("\r\a")))
                            ++l;
                        FillingProductElements(productRepository, _table, i, j);
                        if (l > 0)
                            for (k = 1; k <= l; ++k)
                            {
                                AddToSameName(productRepository, _table, i + k, j + 1);
                                AddToSameDesignation(productRepository, _table, i + k, j);
                            }
                        i += l;
                        AddCount(productRepository, _table, i, j + 2);
                    }
                    else
                    {
                        AddToSameDesignation(productRepository, _table, i, j);
                    }
                }
                else if (!(_table.Cell(i, j + 1).Range.Text.Equals("\r\a")))
                {
                    string rr = _table.Cell(i, j + 1).Range.Text;
                    CreateProduct(productRepository, _table, ref i, j + 1);
                }
            }
        }

        private string DeleteSpaces(string str)
        {
            return str.Replace(" ", "");
        }
        private void CreateProduct(ProductRepository productRepository, Word.Table _table, ref int i, int j)
        {
            productRepository.Products.Add(new Product(_table.Cell(i, j).Range.Text.Replace("\r\a", null)));
            while
                ((_table.Cell(i + 1, j - 1).Range.Text.Equals("\r\a"))
                 &&
                 (!_table.Cell(i + 1, j).Range.Text.Equals("\r\a")))
            {
                productRepository.Products[productRepository.Products.Count - 1].
                manufacturers.Add(_table.Cell(++i, j).Range.Text.Replace("\r\a", null) + " ");
            }
        }
        private void FillingProductElements(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            string partOfNewName = DeleteSpaces(_table.Cell(i, j + 1).Range.Text.Replace("\r\a", null));

            for (int k = 0;
                k < productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count;
                ++k)
            {
                string partOfExistingName = "";
                string existingNameWithoutSpaces =
                    DeleteSpaces(productRepository.Products[productRepository.Products.Count - 1].elements_Name[k].Name);

                if (existingNameWithoutSpaces.Length >= partOfNewName.Length)
                {
                    partOfExistingName =
                    DeleteSpaces(productRepository.Products[productRepository.Products.Count - 1].elements_Name[k].Name)
                        .Substring(0, partOfNewName.Length);
                }
                else
                {
                    partOfExistingName =
                    DeleteSpaces(productRepository.Products[productRepository.Products.Count - 1].elements_Name[k].Name);
                }

                if (partOfNewName.Equals(partOfExistingName, StringComparison.Ordinal))
                {
                    _sameNamePosition = k;
                    AddToSameDesignation(productRepository, _table, i, j);
                    //IsSame(productRepository, _table, i, j + 2);
                    return;
                }
            }

            productRepository.Products[productRepository.Products.Count - 1].
                elements_Name.
                Add(new ElementNameObject(_table.Cell(i, j + 1).Range.Text.Replace("\r\a", null)));
            productRepository.Products[productRepository.Products.Count - 1].
                elements_Name[productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count - 1].
                elements_Designator.Add(_table.Cell(i, j).Range.Text.Replace("\r\a", null));

            _sameNamePosition = productRepository.Products[productRepository.Products.Count - 1].elements_Name.Count - 1;
            //IsSame(productRepository, _table, i, j + 2);
        }
        private void AddToSameDesignation(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            if (_table.Cell(i, j).Range.Text.Equals("\r\a"))
                return;
            productRepository.Products[productRepository.Products.Count - 1].
                    elements_Name[_sameNamePosition].elements_Designator.Add(_table.Cell(i, j).Range.Text.Replace("\r\a", null).Replace(" ", null));
        }
        private void AddToSameName(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            productRepository.Products[productRepository.Products.Count - 1].
                elements_Name[_sameNamePosition].Name += (" " + _table.Cell(i, j).Range.Text.Replace("\r\a", null));
        }
        private void AddCount(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            productRepository.Products[productRepository.Products.Count - 1].
                    elements_Name[_sameNamePosition].DesignatorsCount +=
                    Convert.ToInt32(_table.Cell(i, j).Range.Text.Replace("\r\a", null));
        }
        #endregion

        #region Write File

        private void WriteFile(ProductRepository productRepository, Word.Table _table)
        {
            int positionNumber = 10;
            _table.Rows.Add();

            int lastLine, i = 4;
            for (int k = 0; k < productRepository.Products.Count; ++k)
            {
                AddName(productRepository, _table, ref i, k);
                AddManufacturers(productRepository, _table, ref i, k);
                _table.Rows.Add();
                i += 2;

                for (int j = 0; j < productRepository.Products[k].elements_Name.Count; ++j)
                {
                    string t = productRepository.Products[k].elements_Name[j].Name;
                    
                    _table.Rows.Add();
                    
                    _table.Cell(i, 3).Range.Text = positionNumber.ToString();
                    AddElementsName(productRepository, _table, i, k, j, out lastLine);
                    AddDesignatorsCount(productRepository, _table, k, j, lastLine);
                    AddDesignators(productRepository, _table, ref i, k, j, lastLine);
                    i += 3;
                    positionNumber += 2;
                }
                ++i;
            }
            ++i;
            
            while (_table.Rows.Count > i)
            {
                _table.Rows[i].Delete();
            }
            while ((i + 3)%30 != 0)
            {
                _table.Rows.Add();
                ++i;
            }
        }

        void AddName(ProductRepository productRepository, Word.Table _table, ref int i, int k)
        {
            _table.Rows.Add();
            _table.Cell(i++, 5).Range.Text = productRepository.Products[k].Name;
            _table.Cell(i - 1, 5).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
        }
        void AddManufacturers(ProductRepository productRepository, Word.Table _table, ref int i, int k)
        {
            if (productRepository.Products[k].manufacturers == null ||
                productRepository.Products[k].manufacturers.Count == 0)
                return;
            _table.Rows.Add();

            List<string> partsofname = new List<string>();
            string t = productRepository.Products[k].Name;
            
            for (int x = 0; x < productRepository.Products[k].manufacturers.Count; ++x)
            {
                int begin = 0;
                int end;
                do
                {
                    end = productRepository.Products[k].manufacturers[x].IndexOf(" ", begin);
                    partsofname.Add(
                        productRepository.Products[k].manufacturers[x].Substring(begin, end - begin));
                    begin = end + 1;
                } while (productRepository.Products[k].manufacturers[x].IndexOf(" ", begin) > begin);
                end = productRepository.Products[k].manufacturers[x].Length;
                string temp = productRepository.Products[k].manufacturers[x].Substring(begin, end - begin)
                    .Replace(" ", null);
                if (temp != "")
                    partsofname.Add(temp);
            }
            string oldTemp = "";
            string newTemp = "";
            bool isAdded = false;

            for (int m = 0; m < partsofname.Count; ++m)
            {
                newTemp += (partsofname[m] + " ");
                if (newTemp.Length <= 25)
                {
                    oldTemp = newTemp;
                    isAdded = true;
                    if (m + 1 != partsofname.Count)
                        continue;
                    else
                    {
                        _table.Cell(i++, 5).Range.Text = oldTemp;
                        _table.Rows.Add();
                        oldTemp = newTemp = " ";
                        isAdded = false;
                    }
                }
                else if (newTemp.Length > 25 && !isAdded)
                {
                    _table.Cell(i++, 5).Range.Text = newTemp;
                    _table.Rows.Add();
                    oldTemp = newTemp = " ";
                }
                else
                {
                    _table.Cell(i++, 5).Range.Text = oldTemp;
                    _table.Rows.Add();
                    oldTemp = newTemp = " ";
                    isAdded = false;
                    m--;
                }
            }
        }
        void AddElementsName(ProductRepository productRepository, Word.Table _table, int i, int k, int j, out int lastLine)
        {
            int maxLengthOfName = 26;
            lastLine = i;
            if (productRepository.Products[k].elements_Name[j].Name.Length <= maxLengthOfName)
                _table.Cell(i, 5).Range.Text = productRepository.Products[k].elements_Name[j].Name.Replace("(F", "mF");
            else
            {
                List<string> partsofname = new List<string>();
                int begin = 0;
                int end;
                do
                {
                    end = productRepository.Products[k].elements_Name[j].Name.IndexOf(" ", begin);
                    partsofname.Add(
                        productRepository.Products[k].elements_Name[j].Name.Substring(begin, end - begin)
                            .Replace("(F", "mF"));
                    begin = end + 1;
                } while (productRepository.Products[k].elements_Name[j].Name.IndexOf(" ", begin) > begin);
                end = productRepository.Products[k].elements_Name[j].Name.Length;
                if (
                    productRepository.Products[k].elements_Name[j].Name.Substring(begin, end - begin).Replace(" ", null) !=
                    "")
                    partsofname.Add(
                        productRepository.Products[k].elements_Name[j].Name.Substring(begin, end - begin)
                            .Replace(" ", null));

                string oldTemp = "";
                string newTemp = "";
                bool isAdded = false;

                for (int m = 0; m < partsofname.Count; ++m)
                {
                    newTemp += (partsofname[m] + " ");
                    if (newTemp.Length <= maxLengthOfName)
                    {
                        oldTemp = newTemp;
                        isAdded = true;
                        if (m + 1 != partsofname.Count)
                            continue;
                        else
                        {
                            _table.Rows.Add();
                            _table.Cell(lastLine, 5).Range.Text = oldTemp;
                            
                            oldTemp = newTemp = " ";
                            isAdded = false;
                        }
                    }
                    else if (newTemp.Length > maxLengthOfName && !isAdded)
                    {
                        //_table.Rows.Add();
                        _table.Cell(lastLine++, 5).Range.Text = newTemp;
                        
                        oldTemp = newTemp = " ";
                    }
                    else
                    {
                        //_table.Rows.Add();
                        _table.Cell(lastLine++, 5).Range.Text = oldTemp;
                        
                        oldTemp = newTemp = " ";
                        isAdded = false;
                        m--;
                    }
                }
            }
        }
        void AddDesignatorsCount(ProductRepository productRepository, Word.Table _table, int k, int j, int lastLine)
        {
            _table.Cell(lastLine, 6).Range.Text =
                        productRepository.Products[k].elements_Name[j].DesignatorsCount.ToString();
        }
        void AddDesignators(ProductRepository productRepository, Word.Table _table, ref int i, int k, int j, int lastLine)
        {
            string oldTemp = "";
            string newTemp = "";
            //_table.Rows.Add();
            if (productRepository.Products[k].elements_Name[j].elements_Designator.Count <= lastLine - i)
            {
                int temp = lastLine;
                for (int m = productRepository.Products[k].elements_Name[j].elements_Designator.Count - 1; m >= 0; m--)
                    _table.Cell(temp--, 7).Range.Text = productRepository.Products[k].elements_Name[j].elements_Designator[m].Replace(" ", null);
                i = lastLine;
                
                //++i;
                return;
            }

            
            bool isAdded = false;

            for (int m = 0; m < productRepository.Products[k].elements_Name[j].elements_Designator.Count; ++m)
            {
                newTemp += productRepository.Products[k].elements_Name[j].elements_Designator[m];
                if (newTemp.Length <= 8)
                {
                    oldTemp = newTemp;
                    isAdded = true;
                    if (m + 1 != productRepository.Products[k].elements_Name[j].elements_Designator.Count)
                        continue;
                    else
                    {
                        
                        _table.Cell(i++, 7).Range.Text = oldTemp;
                        _table.Rows.Add();
                        oldTemp = newTemp = " ";
                        isAdded = false;
                    }
                }
                else if (newTemp.Length > 8 && !isAdded)
                {
                    
                    _table.Cell(i++, 7).Range.Text = newTemp;
                    _table.Rows.Add();
                    oldTemp = newTemp = " ";
                }
                else
                {
                    
                    _table.Cell(i++, 7).Range.Text = oldTemp;
                    _table.Rows.Add();
                    oldTemp = newTemp = " ";
                    isAdded = false;
                    m--;
                }
            }
            //if (i < lastLine)
            //{
            //    i = lastLine;
            //    //_table.Rows.Add();
            //}
            ++i;
            ++i;
            _table.Rows.Add();
            _table.Rows.Add();
        }
        #endregion

        

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
