using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using SCPLE.Interface;
using SCPLE.View;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace SCPLE.Model
{
    class ModelCreationSpecification : ICreationSpecificationModel
    {
        #region File Service
        public void FileService(string path)
        {
            try
            {
                Object templatePathObj = path;

                ChangeStatusLabel("Подготовка чтения файла перечня...", EventArgs.Empty);

                #region Работа с перечнем

                applicationWord = new Word.Application();

                try
                {
                    documentWord = applicationWord.Documents.Add
                        (ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
                }
                catch (Exception error)
                {
                    CloseDocument(ref applicationWord, ref documentWord);
                    throw error;
                }
                //application.Visible = true;


                ProductRepository productRepository = new ProductRepository();
                productRepository.Products.Add(new Product("Прочие изделия"));

                Word.Table _table;

                for (int i = 1; i <= documentWord.Tables.Count; ++i)
                {
                    _table = documentWord.Tables[i];
                    ReadFile(productRepository, _table);
                }

                // закрываем перечень
                CloseDocument(ref applicationWord, ref documentWord);

                #endregion

                ChangeStatusLabel("Подготовка создания файла спецификации...", EventArgs.Empty);

                #region Работа со спецификацией

                //templatePathObj = "E:\\Мои документы\\Программирование\\C#\\My Projects\\Диплом\\SCPLE\\Modeling\\Template.dot";
                //templatePathObj = "D:\\Грищенко\\MY!\\программирование\\My Projects\\Диплом\\SCPLE\\Modeling\\Template.dot";
                templatePathObj = _parameters.TemplateFilePath;

                #region Create File

                string tempName = "AAOT.";
                string tempPath = path.Substring(0, path.LastIndexOf('\\') + 1);
                if (_parameters.DesignDocFirstString != "" ||
                    _parameters.DesignDocSecondString != "")
                    tempName += _parameters.DesignDocFirstString + "." + _parameters.DesignDocSecondString;
                else
                    tempName += "newSpecificationFile";

                string fullFileName = tempPath + tempName;

                if (_parameters.FileDoc)
                {
                    #region Создание файла Doc

                    try
                    {
                        applicationWord = new Word.Application();
                        documentWord = applicationWord.Documents.Add
                            (ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);


                        documentWord.SaveAs(fullFileName + ".doc");
                    }
                    catch (Exception error)
                    {
                        CloseDocument(ref applicationWord, ref documentWord);
                        throw error;
                    }

                    #endregion

                    #region Заполнение файла Doc

                    Word.Table _tab = null;

                    try
                    {
                        for (int i = 1; i < documentWord.Tables.Count; ++i)
                        {
                            _tab = documentWord.Tables[i];
                            WriteFileToDoc(productRepository, _tab, i);
                        }
                        documentWord.Save();
                    }
                    catch (Exception)
                    {
                        CloseDocument(ref applicationWord, ref documentWord);
                        return;
                    }

                    #endregion

                    CloseDocument(ref applicationWord, ref documentWord);
                }

                if (_parameters.FileXls)
                {
                    #region Создание файла Excel

                    applicationExcel = new Excel.Application();

                    try
                    {
                        applicationExcel.SheetsInNewWorkbook = 1;
                        applicationExcel.Workbooks.Add(Type.Missing);

                        excelappworkbooks = applicationExcel.Workbooks;
                        excelappworkbook = excelappworkbooks[1];
                        excelappworkbook.SaveAs(fullFileName + ".xlsx");
                    }
                    catch (Exception error)
                    {
                        applicationExcel.Workbooks.Close();
                        CloseAll();
                        //throw;
                    }

                    #endregion

                    #region Заполнение файла Excel

                    try
                    {
                        WriteFileToXls(productRepository, excelappworkbook);
                        excelappworkbook.Save();
                    }
                    catch (Exception)
                    {
                        CloseAll();
                        //throw;
                    }


                    #endregion

                    CloseDocument(ref applicationExcel);

                    ChangeStatusLabel("Готово!", EventArgs.Empty);
                }

                #endregion

                #endregion

                ChangeStatusButton("Выход", EventArgs.Empty);

                ChangeStatusLabel("Готово!", EventArgs.Empty);
            }
            catch (Exception)
            {
                CloseAll();
                ChangeProgressBar(-1, EventArgs.Empty);
                ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                ChangeStatusLabel("Ошибка!", EventArgs.Empty);
            }

        }
        #endregion

        public void SetParameters(Parameters parameters)
        {
            _parameters = new Parameters();
            _parameters = parameters;
        }
        
        #region Read File

        private void ReadFile(ProductRepository productRepository, Word.Table _table)
        {
            if (!(_table.Cell(1, 1).Range.Text.ToLower().Contains("поз") && _table.Cell(1, 2).Range.Text.ToLower().Contains("наименование")))
            {
                return;
            }

            int i = 0;
            try
            {
                ChangeProgressBar(_table.Rows.Count, EventArgs.Empty); //передаем кол-во строк в док-те
                ChangeReadListStatusLabel("В процессе...", EventArgs.Empty);
                ChangeCreateSpecStatusLabel("Ожидание", EventArgs.Empty);
                ChangeStatusLabel("Пожалуйста, подождите", EventArgs.Empty);

                for (i = 2; i <= _table.Rows.Count; ++i)
                {
                    int j = 1;
                    int k, l = 0;

                    //string t = _table.Cell(i, j).Range.Text;
                    //t = _table.Cell(i, j + 1).Range.Text;

                    //при объединенной ячейке программа рухнет

                    //int z = _table.Cell(i, j).RowIndex;
                    //z = _table.Cell(i, j + 1).RowIndex;
                    //z = _table.Cell(i, j + 2).RowIndex;

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
                    ChangeProgressBar(i, EventArgs.Empty);
                }
                ChangeProgressBar(-1, EventArgs.Empty);
                ChangeReadListStatusLabel("Готово!", EventArgs.Empty);
                ChangeStatusLabel("Чтение файла перечня выполнено успешно!", EventArgs.Empty);
            }
            catch (Exception e)
            {
                int tempPageNumber = i/30 + 1;
                int currentLine = i - (i/30)*30;
                string message = "Ошибка в строке " + i + " (" + tempPageNumber + " лист, " + currentLine + " строка). " + 
                                 "Проверте корректность создания файла перечня.";
                ChangeProgressBar(-1, EventArgs.Empty);
                ChangeReadListStatusLabel("Ошибка!", EventArgs.Empty);
                ChangeStatusLabel("Чтение файла перечня НЕ выполнено!", EventArgs.Empty);
                MessageBox.Show(message);

                throw;
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

            ++_totalAmountElementsNames; //общее количество ElementNameObject

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

        #region Write File to Doc

        private void WriteFileToDoc(ProductRepository productRepository, Word.Table _table, int tabNumber)
        {
            ChangeProgressBar(_totalAmountElementsNames, EventArgs.Empty);
            ChangeCreateSpecStatusLabel("В процессе...", EventArgs.Empty);
            ChangeStatusLabel("Создание файла '.doc'. Пожалуйста, подождите", EventArgs.Empty);

            if (tabNumber == 1)
                WrtiteFirstPage(productRepository, _table);
            else
            {
                int positionNumber = 10;
                _table.Rows.Add();


                int lastLine, i = 4;
                for (int k = 0; k < productRepository.Products.Count; ++k)
                {
                    AddName(productRepository, _table, ref i, k);
                    AddManufacturers(productRepository, _table, ref i, k);
                    _table.Rows.Add();
                    _table.Rows.Add();
                    i += 2;

                    for (int j = 0; j < productRepository.Products[k].elements_Name.Count; ++j)
                    {
                        string t = productRepository.Products[k].elements_Name[j].Name;

                        _table.Cell(i, 3).Range.Text = positionNumber.ToString();
                        AddElementsName(productRepository, _table, i, k, j, out lastLine);
                        AddDesignatorsCount(productRepository, _table, k, j, lastLine);
                        AddDesignators(productRepository, _table, ref i, k, j, lastLine);
                        i += 3;
                        positionNumber += 2;
                        _table.Rows.Add();
                        _table.Rows.Add();
                        ChangeProgressBar(j, EventArgs.Empty);
                    }
                    ++i;
                }
                ChangeProgressBar(_totalAmountElementsNames, EventArgs.Empty);
                ChangeCreateSpecStatusLabel("Готово!", EventArgs.Empty);
                ChangeStatusLabel("Создание файла спецификации '.doc' выполнено успешно!", EventArgs.Empty);

                ++i;
                ChangeProgressBar(-1, EventArgs.Empty);
                if (i < _table.Rows.Count)
                    ChangeProgressBar(_table.Rows.Count - i, EventArgs.Empty);
                ChangeStatusLabel("Последняя доработка...", EventArgs.Empty);

                int temp = 0;
                while (_table.Rows.Count > i)
                {
                    _table.Rows[i].Delete();
                    ChangeProgressBar(++temp, EventArgs.Empty);
                }
                while ((i + 3)%30 != 0)
                {
                    _table.Rows.Add();
                    ++i;
                }
            }
        }

        private void WrtiteFirstPage(ProductRepository productRepository, Word.Table _table)
        {
            int i = 4;

            string nameOfDoc = "AAOT." + _parameters.DesignDocFirstString + "." + _parameters.DesignDocSecondString;
            string nameOfPcb = "AAOT." + _parameters.DesignPcbFirstString + "." + _parameters.DesignPcbSecondString;

            if (_parameters.AssemblyDrawing)
            {
                _table.Cell(i, 2).Range.Text = _parameters.AssemblyDrawingFormat;
                _table.Cell(i, 4).Range.Text = nameOfDoc + " СБ";
                _table.Cell(i, 5).Range.Text = "Сборочный чертеж";
                i += 2;
            }
            
            if (_parameters.ElectricalCircuit)
            {
                _table.Cell(i, 2).Range.Text = _parameters.ElectricalCircuitFormat;
                _table.Cell(i, 4).Range.Text = nameOfDoc + " ЭЗ";
                _table.Cell(i, 5).Range.Text = "Схема электрическая";
                _table.Cell(++i, 5).Range.Text = "принципиальная";
                i += 2;
            }
            
            if (_parameters.ListOfitems)
            {
                _table.Cell(i, 2).Range.Text = _parameters.ListOfitemsFormat;
                _table.Cell(i, 4).Range.Text = nameOfDoc + " ПЭЗ";
                _table.Cell(i, 5).Range.Text = "Перечень элементов";
                i += 2;
            }

            if (_parameters.Pcb)
            {
                _table.Cell(i, 2).Range.Text = "-";
                _table.Cell(i, 4).Range.Text = nameOfPcb + " М";
                _table.Cell(i, 5).Range.Text = "Плата. Данные конструкции";
                _table.Cell(i, 7).Range.Text = "CD";
                i += 2;
            }

            if (_parameters.CertifyingSheet)
            {
                _table.Cell(i, 2).Range.Text = _parameters.CertifyingSheetFormat;
                _table.Cell(i, 4).Range.Text = nameOfPcb + " М-УД";
                _table.Cell(i, 5).Range.Text = "Плата. Данные конструкции";
                _table.Cell(++i, 5).Range.Text = "Удостоверяющий лист";
                i += 2;
            }

            _table.Cell(18, 2).Range.Text = _parameters.PcbFormat;
            _table.Cell(18, 4).Range.Text = nameOfPcb;
            _table.Cell(18, 5).Range.Text = "Плата";
            _table.Cell(18, 6).Range.Text = "1";

            if (_parameters.ElementsOfSMDMounting && !_parameters.BorrowedItems)
            {
                _table.Rows[_table.Rows.Count].Cells[2].Merge(_table.Rows[_table.Rows.Count].Cells[3]);
                _table.Rows[_table.Rows.Count].Cells[2].Merge(_table.Rows[_table.Rows.Count].Cells[3]);

                _table.Cell(_table.Rows.Count, 2).Range.Text = "* элементы SMD монтажа";
            }
            else if (!_parameters.ElementsOfSMDMounting && _parameters.BorrowedItems)
            {
                _table.Rows[_table.Rows.Count].Cells[2].Merge(_table.Rows[_table.Rows.Count].Cells[3]);
                _table.Rows[_table.Rows.Count].Cells[2].Merge(_table.Rows[_table.Rows.Count].Cells[3]);

                _table.Cell(_table.Rows.Count, 2).Range.Text = "* заимствованные изделия";
            }
            else if (_parameters.ElementsOfSMDMounting && _parameters.BorrowedItems)
            {
                _table.Rows[_table.Rows.Count - 1].Cells[2].Merge(_table.Rows[_table.Rows.Count - 1].Cells[3]);
                _table.Rows[_table.Rows.Count - 1].Cells[2].Merge(_table.Rows[_table.Rows.Count - 1].Cells[3]);

                _table.Rows[_table.Rows.Count].Cells[2].Merge(_table.Rows[_table.Rows.Count].Cells[3]);
                _table.Rows[_table.Rows.Count].Cells[2].Merge(_table.Rows[_table.Rows.Count].Cells[3]);

                _table.Cell(_table.Rows.Count - 1, 2).Range.Text = "* элементы SMD монтажа";
                _table.Cell(_table.Rows.Count, 2).Range.Text = "** заимствованные изделия";
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
            _table.Rows.Add();
            if (productRepository.Products[k].manufacturers == null ||
                productRepository.Products[k].manufacturers.Count == 0)
                return;

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
                            //_table.Rows.Add();
                            _table.Cell(lastLine, 5).Range.Text = oldTemp;
                            //_table.Rows.Add();
                            oldTemp = newTemp = " ";
                            isAdded = false;
                        }
                    }
                    else if (newTemp.Length > maxLengthOfName && !isAdded)
                    {
                        //_table.Rows.Add();
                        _table.Cell(lastLine++, 5).Range.Text = newTemp;
                        _table.Rows.Add();
                        oldTemp = newTemp = " ";
                    }
                    else
                    {
                        //_table.Rows.Add();
                        _table.Cell(lastLine++, 5).Range.Text = oldTemp;
                        _table.Rows.Add();
                        oldTemp = newTemp = " ";
                        isAdded = false;
                        m--;
                    }
                }
                lastLine--;
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

            bool isAdded = false;

            for (int m = 0; m < productRepository.Products[k].elements_Name[j].elements_Designator.Count; ++m)
            {
                newTemp += productRepository.Products[k].elements_Name[j].elements_Designator[m];
                if (newTemp.Length <= 7)
                {
                    oldTemp = newTemp;
                    isAdded = true;
                    if (m + 1 != productRepository.Products[k].elements_Name[j].elements_Designator.Count)
                        continue;
                    else
                    {
                        // Добавление звёздочки если элемент - SMD
                        if (_parameters.ElementsOfSMDMounting)
                            if (productRepository.Products[k].elements_Name[j].Name.Contains("Chip") ||
                                productRepository.Products[k].elements_Name[j].Name.Contains("0805") ||
                                productRepository.Products[k].elements_Name[j].Name.Contains("1206"))
                                oldTemp += "*";

                        _table.Cell(i++, 7).Range.Text = oldTemp;
                        _table.Rows.Add();
                        oldTemp = newTemp = " ";
                        isAdded = false;
                    }
                }
                else if (newTemp.Length > 7 && !isAdded)
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
            
            ++i;
            ++i;
        }
        #endregion

        #region Write File to Xls

        private void WriteFileToXls(ProductRepository productRepository, Excel.Workbook excelappworkbook)
        {
            ChangeProgressBar(_totalAmountElementsNames, EventArgs.Empty);
            ChangeCreateSpecStatusLabel("В процессе...", EventArgs.Empty);
            ChangeStatusLabel("Создание файла '.xlsx'. Пожалуйста, подождите...", EventArgs.Empty);
            
            excelsheets = excelappworkbook.Worksheets;

            //Получаем массив ссылок на листы выбранной книги
            //excelsheets = applicationExcel.Worksheets;

            //Получаем ссылку на лист 1
            excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);

            //Выбираем ячейку для вывода A1
            //excelcells = excelworksheet.get_Range("A1", "A1");
            //При выборе одной ячейки можно не указывать вторую границу 
            excelcells = excelworksheet.get_Range("A1", Type.Missing);
            
            int positionNumber = 10;

            int i = 1;

            WrtiteFirstPage(productRepository, ref i);
            i += 2;

            for (int k = 0; k < productRepository.Products.Count; ++k)
            {
                AddNameToXls(productRepository, ref i, k);
                AddManufacturersToXls(productRepository, ref i, k);
                i += 2;

                for (int j = 0; j < productRepository.Products[k].elements_Name.Count; ++j)
                {

                    ((Excel.Range)excelworksheet.Cells[i, 2]).Value2 = positionNumber.ToString();
                    AddElementsNameToXls(productRepository, i, k, j);
                    AddDesignatorsCountToXls(productRepository, i, k, j);
                    AddDesignatorsToXls(productRepository, ref i, k, j);
                    ++i;
                    positionNumber += 2;
                    ChangeProgressBar(j, EventArgs.Empty);
                }
                ++i;
            }
            ChangeProgressBar(_totalAmountElementsNames, EventArgs.Empty);
            ChangeCreateSpecStatusLabel("Готово!", EventArgs.Empty);
            ChangeStatusLabel("Создание файла спецификации '.xlsx' выполнено успешно!", EventArgs.Empty);
            ChangeProgressBar(-1, EventArgs.Empty);
            ++i;
        }

        private void WrtiteFirstPage(ProductRepository productRepository, ref int i)
        {
            string nameOfDoc = "AAOT." + _parameters.DesignDocFirstString + "." + _parameters.DesignDocSecondString;
            string nameOfPcb = "AAOT." + _parameters.DesignPcbFirstString + "." + _parameters.DesignPcbSecondString;

            if (_parameters.AssemblyDrawing)
            {
                ((Excel.Range)excelworksheet.Cells[i, 1]).Value2 = _parameters.AssemblyDrawingFormat;
                ((Excel.Range)excelworksheet.Cells[i, 3]).Value2 = nameOfDoc + " СБ";
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 = "Сборочный чертеж";
                ++i;
            }

            if (_parameters.ElectricalCircuit)
            {
                ((Excel.Range)excelworksheet.Cells[i, 1]).Value2 = _parameters.ElectricalCircuitFormat;
                ((Excel.Range)excelworksheet.Cells[i, 3]).Value2 = nameOfDoc + " ЭЗ";
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 = "Схема электрическая ";
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 += "принципиальная";
                ++i;
            }

            if (_parameters.ListOfitems)
            {
                ((Excel.Range)excelworksheet.Cells[i, 1]).Value2 = _parameters.ListOfitemsFormat;
                ((Excel.Range)excelworksheet.Cells[i, 3]).Value2 = nameOfDoc + " ПЭЗ";
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 = "Перечень элементов";
                ++i;
            }

            if (_parameters.Pcb)
            {
                ((Excel.Range)excelworksheet.Cells[i, 1]).Value2 = "-";
                ((Excel.Range)excelworksheet.Cells[i, 3]).Value2 = nameOfPcb + " М";
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 = "Плата. Данные конструкции";
                ((Excel.Range)excelworksheet.Cells[i, 6]).Value2 = "CD";
                ++i;
            }

            if (_parameters.CertifyingSheet)
            {
                ((Excel.Range)excelworksheet.Cells[i, 1]).Value2 = _parameters.CertifyingSheetFormat;
                ((Excel.Range)excelworksheet.Cells[i, 3]).Value2 = nameOfPcb + " М-УД";
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 = "Плата. Данные конструкции ";
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 += "Удостоверяющий лист";
                ++i;
            }

            ((Excel.Range)excelworksheet.Cells[i, 1]).Value2 = _parameters.PcbFormat;
            ((Excel.Range)excelworksheet.Cells[i, 3]).Value2 = nameOfPcb;
            ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 = "Плата";
            ((Excel.Range)excelworksheet.Cells[i, 5]).Value2 = "1";
        }

        void AddNameToXls(ProductRepository productRepository, ref int i, int k)
        {
            ((Excel.Range)excelworksheet.Cells[i++, 4]).Value2 = productRepository.Products[k].Name;
        }
        void AddManufacturersToXls(ProductRepository productRepository, ref int i, int k)
        {
            if (productRepository.Products[k].manufacturers == null ||
                productRepository.Products[k].manufacturers.Count == 0)
                return;

            for (int x = 0; x < productRepository.Products[k].manufacturers.Count; ++x)
            {
                ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 += (productRepository.Products[k].manufacturers[x] + " ");
            }
        }
        void AddElementsNameToXls(ProductRepository productRepository, int i, int k, int j)
        {
            ((Excel.Range)excelworksheet.Cells[i, 4]).Value2 = productRepository.Products[k].elements_Name[j].Name.Replace("(F", "mF");
        }
        void AddDesignatorsCountToXls(ProductRepository productRepository, int i, int k, int j)
        {
            ((Excel.Range)excelworksheet.Cells[i, 5]).Value2 = productRepository.Products[k].elements_Name[j].DesignatorsCount.ToString();
        }
        void AddDesignatorsToXls(ProductRepository productRepository, ref int i, int k, int j)
        {
            for (int m = 0; m < productRepository.Products[k].elements_Name[j].elements_Designator.Count; ++m)
            {
                ((Excel.Range)excelworksheet.Cells[i, 6]).Value2 += (productRepository.Products[k].elements_Name[j].elements_Designator[m] + " ");
            }
        }
        #endregion

        #endregion

        #region Close
        private void CloseDocument(ref Word._Application application, ref Word._Document document)
        {
            if (application != null)
            {
                try
                {
                    if (document != null)
                        document.Close(ref falseObj, ref missingObj, ref missingObj);

                }
                catch (Exception)
                {}
                finally
                {
                    try
                    {
                        application.Quit(ref missingObj, ref missingObj, ref missingObj);
                    }
                    catch (Exception)
                    {}
                    finally
                    {
                        document = null;
                        application = null;
                    }
                }
            }
        }
        private void CloseDocument(ref Excel.Application application)
        {
            if (application != null)
            {
                try
                {
                    if (application.Workbooks != null)
                        application.Workbooks.Close();
                }
                catch (Exception)
                {

                }
                finally
                {
                    try
                    {
                        application.Quit();
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        application = null;
                    }
                }
            }

        }
        public void CloseAll()
        {
            CloseDocument(ref applicationWord, ref documentWord);
            CloseDocument(ref applicationExcel);
        }
        #endregion

        #region Variables
        private Word._Application applicationWord;
        private Excel.Application applicationExcel;
        private Word._Document documentWord;
        private Excel.Sheets excelsheets;
        private Excel.Worksheet excelworksheet;
        private Excel.Range excelcells;
        private Excel.Workbooks excelappworkbooks;
        private Excel.Workbook excelappworkbook;

        Object missingObj = System.Reflection.Missing.Value;
        Object trueObj = true;
        Object falseObj = false;
        private bool _beforeCapacitors;
        private string _filePath;
        private bool _isSame;
        private int _sameNamePosition;
        private int _totalAmountElementsNames;

        private Parameters _parameters;

        #endregion

        #region Events
        public event EventHandler<EventArgs> ChangeProgressBar;
        public event EventHandler<EventArgs> ChangeReadListStatusLabel;
        public event EventHandler<EventArgs> ChangeCreateSpecStatusLabel;
        public event EventHandler<EventArgs> ChangeStatusLabel;
        public event EventHandler<EventArgs> ChangeStatusButton;
        #endregion
    }
}
