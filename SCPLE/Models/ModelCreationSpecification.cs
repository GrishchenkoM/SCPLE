using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using SCPLE;
using Scple.Interfaces;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Scple.Models
{
    /// <summary>
    /// Преобразование файлов
    /// </summary>
    public class ModelCreationSpecification : IModelCreationSpecification
    {
        public ModelCreationSpecification()
        {
            _designatorNameRepository = new DesignatorNameRepository();
            _singularProduct = new SingularProduct();
        }
#region IModelCreationSpecification
#region Methods
        /// <summary>
        /// Работа с файлом
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void FileService(string path)
        {
            try
            {
                _applicationWord = new Word.Application();
                ProductRepository productRepository = new ProductRepository();

                ReadFile(productRepository, path);
                ChangeStatusLabel("Подготовка создания файла спецификации...", EventArgs.Empty);
                WriteFile(productRepository, path);

                CloseDocument(ref _applicationWord, ref _documentWord);
                ChangeStatusButton("Выход", EventArgs.Empty);
                ChangeStatusLabel("Готово!", EventArgs.Empty);
            }
            catch (OutOfMemoryException ex)
            {
                Handling(ex);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                Handling(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Handling(ex);
            }
            catch (StackOverflowException ex)
            {
                Handling(ex);
            }
            catch (Exception ex)
            {
                CloseAll();
                if (!ex.Message.Contains("Cancel"))
                    ChangeProgressBar(-1, EventArgs.Empty);
                ChangeStatusLabel("Отменено", EventArgs.Empty);
            }
        }
        
        private void ReadFile(ProductRepository productRepository, string path)
        {
            Object templatePathObj = path;
            Word.Table _table = null;

#region Определение _documentWord
            try
            {
                _documentWord = _applicationWord.Documents.Add
                    (ref templatePathObj, ref _missingObj, ref _missingObj, ref _missingObj);
                ListOrSpecDocument(_documentWord);
            }
            catch (Exception)
            {
                ChangeReadFileStatus("Ошибка", EventArgs.Empty);
                CloseDocument(ref _applicationWord, ref _documentWord);
                throw;
            }
#endregion

            try
            {
                if (_isList)
                {
                    ChangeReadFileStatus("- Чтение файла перечня элементов", EventArgs.Empty);
                    ChangeStatusLabel("Подготовка чтения файла перечня...", EventArgs.Empty);

                    productRepository.Products.Add(new Product("Прочие изделия"));

                    for (int i = 1; i <= _documentWord.Tables.Count; ++i)
                    {
                        _table = _documentWord.Tables[i];
                        ReadListFile(productRepository, _table);
                    }
                }
                else if (_isSpecification)
                {
                    string fullFileName;
                    string fileName = CreateFullFileName(path, out fullFileName);
                    ChangeReadFileStatus("- Чтение файла спецификации: " + fileName, EventArgs.Empty);

                    for (int i = 1; i <= _documentWord.Tables.Count; ++i)
                    {
                        _table = _documentWord.Tables[i];
                        ReadSpecFile(productRepository, _table);
                    }
                }
                else
                {
                    throw new Exception("Unknown file!");
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void WriteFile(ProductRepository productRepository, string path)
        {
            string fullFileName;

            CreateFullFileName(path, out fullFileName);

            if (_isList && _parameters.FileDoc)
            {
                DesignationsSorting(productRepository);

                #region Создание файла Doc

                Word._Application applicationWord = null;
                Object templatePathObj = _parameters.TemplateFilePath;

                try
                {
                    applicationWord = new Word.Application();
                    _documentWord = applicationWord.Documents.Add
                        (ref templatePathObj, ref _missingObj, ref _missingObj, ref _missingObj);


                    _documentWord.SaveAs(fullFileName + ".docx");
                }
                catch (Exception)
                {
                    CloseDocument(ref applicationWord, ref _documentWord);
                    throw;
                }

                #endregion

                #region Заполнение файла Doc

                Word.Table _tab = null;

                try
                {
                    for (int i = 1; i < _documentWord.Tables.Count; ++i)
                    {
                        _tab = _documentWord.Tables[i];
                        WriteFileToDoc(productRepository, _tab, i);
                    }
                    _documentWord.Save();
                }
                catch (Exception)
                {
                    CloseDocument(ref applicationWord, ref _documentWord);
                    ChangeProgressBar(-1, EventArgs.Empty);
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    ChangeStatusLabel("Ошибка!", EventArgs.Empty);
                    return;
                }

                #endregion

                CloseDocument(ref applicationWord, ref _documentWord);
            }
            else
            {
                if (_isList)
                    DesignationsSorting(productRepository);

                #region Создание файла Excel

                _applicationExcel = new Excel.Application();

                try
                {
                    _applicationExcel.SheetsInNewWorkbook = 1;
                    _applicationExcel.Workbooks.Add(Type.Missing);

                    _excelAppWorkBooks = _applicationExcel.Workbooks;
                    _excelAppWorkBook = _excelAppWorkBooks[1];
                    _excelAppWorkBook.SaveAs(fullFileName + ".xlsx");
                }
                catch (Exception)
                {
                    _applicationExcel.Workbooks.Close();
                    CloseAll();
                }

                #endregion

                #region Заполнение файла Excel

                try
                {
                    WriteFileToXls(productRepository, _excelAppWorkBook);
                    _excelAppWorkBook.Save();
                }
                catch (Exception)
                {
                    CloseAll();
                }

                #endregion

                CloseDocument(ref _applicationExcel);
            }
        }

        private void Handling(Exception ex)
        {
            string message = string.Format("{0}\n", "Описание: ") + string.Format("{0}\n\n", ex.Message);
            message += "Для решения данной проблемы обратитесь к разработчику программы.";

            ChangeProgressBar(-1, EventArgs.Empty);
            ChangeReadListStatusLabel("Ошибка!", EventArgs.Empty);
            ChangeStatusLabel("Работа с файлом прервана!", EventArgs.Empty);
            MessageBox.Show(message, "Непредвиденная ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            try
            {
                throw new UnhandledException(ex);
            }
            catch (Exception)
            {
                CloseAll();
                ChangeStatusLabel("Ошибка!", EventArgs.Empty);
            }
        }
        private bool Warning(Exception ex, int row)
        {
            if (!ex.Message.Contains("Cancel"))
            {
                int tempPageNumber = row / 30 + 1;
                int currentLine = row - (row / 30) * 30;
                string message = string.Format("Обнаружена ошибка на {0} строке ({1} лист, {2} строка).\n",
                                                                    row, tempPageNumber, currentLine);
                message += string.Format("Описание: {0}\n\n", ex.Message);
                message += "ПОПЫТАТЬСЯ ПРОДОЛЖИТЬ РАБОТУ ПРОГРАММЫ?\n\n";
                message += "Нажмите ДА, если хотите продолжить.";
                var result = MessageBox.Show(message, "Ошибка",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.No)
                {
                    CloseAll();
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    ChangeStatusLabel("Ошибка!", EventArgs.Empty);
                    return false;
                }
            }
            return true;
        }
        private void DoWarning(Exception ex, int row, EventHandler<EventArgs> eventHandler)
        {
            if (!Warning(ex, row))
            {
                eventHandler("Ошибка!", EventArgs.Empty);
                throw new UnhandledException();
            }
        }
        private void Error(Exception ex, EventHandler<EventArgs> eventHandler)
        {
            if (!ex.Message.Contains("Cancel"))
            {
                ChangeProgressBar(-1, EventArgs.Empty);
                eventHandler("Ошибка!", EventArgs.Empty);
                ChangeStatusLabel("НЕ выполнено!", EventArgs.Empty);
            }
        }

        private void ListOrSpecDocument(Word._Document _documentWord)
        {
            ModelPatternDetermination modelPatternDetermination = new ModelPatternDetermination();

            try
            {
                Word.Table _table = _documentWord.Tables[1];

                if (modelPatternDetermination.IsSpecification(_table, 1))
                {
                    _isList = false;
                    _isSpecification = true;
                }
                else if (modelPatternDetermination.IsList(_table, 1))
                {
                    _isList = true;
                    _isSpecification = false;
                }
            }
            catch (Exception)
            {
                _isList = false;
                _isSpecification = false;
            }
        }

        private void DesignationsSorting(ProductRepository productRepository)
        {
            productRepository.SortByDesignationName(); 
        }
        private string CreateFullFileName(string path, out string fullFileName)
        {
            string tempName = "AAOT.";
            string tempPath = path.Substring(0, path.LastIndexOf('\\') + 1);

            if (_isSpecification)
            {
                int length = path.Length - tempPath.Length;
                string tempNameWithExt = path.Substring(path.LastIndexOf('\\') + 1, length);
                tempName = tempNameWithExt.Substring(0, tempNameWithExt.LastIndexOf("."));
            }
            else if (_parameters.DesignDocFirstString != "" ||
                _parameters.DesignDocSecondString != "")
                tempName += _parameters.DesignDocFirstString + "." + _parameters.DesignDocSecondString;
            else
                tempName += "newSpecificationFile";

            fullFileName = tempPath + tempName;
            return tempName;
        }
        /// <summary>
        /// Обновление параметров
        /// </summary>
        /// <param name="parameters">Параметры</param>
        public void SetParameters(Parameters parameters)
        {
            _parameters = new Parameters();
            _parameters = parameters;
        }
        /// <summary>
        /// Закрыть все документы
        /// </summary>
        public void CloseAll()
        {
            CloseDocument(ref _applicationWord, ref _documentWord);
            CloseDocument(ref _applicationExcel);
        }
#endregion

#region Events
        /// <summary>
        /// Изменение величины заполнения ProgressBar
        /// </summary>
        public event EventHandler<EventArgs> ChangeProgressBar;
        /// <summary>
        /// Изменение статуса ReadListStatusLabel
        /// </summary>
        public event EventHandler<EventArgs> ChangeReadListStatusLabel;
        /// <summary>
        /// Изменение статуса CreateSpecStatusLabel
        /// </summary>
        public event EventHandler<EventArgs> ChangeCreateSpecStatusLabel;
        /// <summary>
        /// Изменение общего статуса StatusLabel
        /// </summary>
        public event EventHandler<EventArgs> ChangeStatusLabel;
        /// <summary>
        /// Изменение статуса кнопки "Отмена"
        /// </summary>
        public event EventHandler<EventArgs> ChangeStatusButton;
        /// <summary>
        /// Изменение статуса файла
        /// </summary>
        public event EventHandler<EventArgs> ChangeReadFileStatus;
#endregion
#endregion

#region Auxiliary

#region Read File
        private void ReadListFile(ProductRepository productRepository, Word.Table _table)
        {
            int i = 0;

            try
            {
                ModelPatternDetermination _patternDetermination = new ModelPatternDetermination();
                if (!_patternDetermination.IsList(_table, 1))
                    return;

                ChangeProgressBar(_table.Rows.Count, EventArgs.Empty); //передаем кол-во строк в док-те

                ChangeReadListStatusLabel("В процессе...", EventArgs.Empty);
                ChangeCreateSpecStatusLabel("Ожидание", EventArgs.Empty);
                ChangeStatusLabel("Пожалуйста, подождите", EventArgs.Empty);

                for (i = 2; i <= _table.Rows.Count; ++i)
                {
                    int j = 1;
                    int k, l = 0;

                    if (_patternDetermination.IsList(_table, i)) continue;

                    if (!IsEmptyCell(_table, i, j))
                        if (!IsEmptyCell(_table, i, j + 1))
                        {
                            if (IsMissingDesignator(_table, i, j + 1))
                                continue;
                            while ((i + l <= _table.Rows.Count) && IsEmptyCell(_table, i + l, j + 2))
                                ++l;
                            FillingProductElements(productRepository, _table, i, j, _isList);
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
                            AddToSameDesignation(productRepository, _table, i, j);

                    else if (!IsEmptyCell(_table, i, j + 1))
                        CreateProduct(productRepository, _table, ref i, j + 1);

                    ChangeProgressBar(i, EventArgs.Empty);
                }
                ChangeProgressBar(-1, EventArgs.Empty);
                ChangeReadListStatusLabel("Готово!", EventArgs.Empty);
                ChangeStatusLabel("Чтение файла перечня выполнено успешно!", EventArgs.Empty);
            }
            catch (OutOfMemoryException ex)
            {
                Handling(ex);
                ChangeReadListStatusLabel("Ошибка!", EventArgs.Empty);
                ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (StackOverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
            catch (UnhandledException ex)
            {
                Error(ex, ChangeReadListStatusLabel);
            }
            catch (Exception ex)
            {
                Error(ex, ChangeReadListStatusLabel);
                throw;
            }
        }

        private bool IsEmptyCell(Word.Table _table, int row, int col)
        {
            try
            {
                if (_table.Cell(row, col).Range.Text.Equals("\r\a"))
                    return true;
                return false;
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
                return false;
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
                return false;
            }
            catch (Exception ex)
            {
                DoWarning(ex, row, ChangeReadListStatusLabel);
                return false;
            }
        }
        private bool IsMissingDesignator(Word.Table _table, int row, int col)
        {
            try
            {
                if (_table.Cell(row, col).Range.Text.ToLower(CultureInfo.CurrentCulture)
                    .Contains("не уст") ||
                    _table.Cell(row, col)
                        .Range.Text.ToLower(CultureInfo.CurrentCulture)
                        .Contains("отсутс"))
                    return true;
                return false;
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
                return false;
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
                return false;
            }
            catch (UnhandledException)
            {
                throw new UnhandledException();
            }
            catch (Exception ex)
            {
                DoWarning(ex, row, ChangeReadListStatusLabel);
                return false;
            }
        }
        private void ReadSpecFile(ProductRepository productRepository, Word.Table _table)
        {
            int i = 0;

            try
            {
                ModelPatternDetermination _patternDetermination = new ModelPatternDetermination();
                if (!_patternDetermination.IsSpecification(_table, 1))
                    return;

                ChangeProgressBar(_table.Rows.Count, EventArgs.Empty); //передаем кол-во строк в док-те
                ChangeReadListStatusLabel("В процессе...", EventArgs.Empty);
                ChangeCreateSpecStatusLabel("Ожидание", EventArgs.Empty);
                ChangeStatusLabel("Пожалуйста, подождите", EventArgs.Empty);

                for (i = 2; i <= _table.Rows.Count; ++i)
                {
                    int j = 2, k, l = 0;

                    if (_table.Rows[i].Cells.Count < 7)
                        continue;

                    if (!IsEmptyCell(_table, i, j + 3))
                        if ((!IsEmptyCell(_table, i, j)) || (!IsEmptyCell(_table, i, j + 1)))
                        {
                            FillingProductElements(productRepository, _table, i, j + 3, _isList);
                            AddCount(productRepository, _table, i, j + 4);

                            // read format
                            productRepository.Products[productRepository.Products.Count - 1].
                                ElementsName[
                                    productRepository.Products[productRepository.Products.Count - 1]
                                        .ElementsName.Count - 1].
                                Format = ReplaceText(_table, i, j, "\r\a", null); 
                            
                            // read position
                            productRepository.Products[productRepository.Products.Count - 1].
                                ElementsName[
                                    productRepository.Products[productRepository.Products.Count - 1]
                                        .ElementsName.Count - 1].
                                Position = ReplaceText(_table, i, j + 1, "\r\a", null);

                            // read designation
                            productRepository.Products[productRepository.Products.Count - 1].
                                ElementsName[
                                    productRepository.Products[productRepository.Products.Count - 1]
                                        .ElementsName.Count - 1].
                                Designation = ReplaceText(_table, i, j + 2, "\r\a", null);

                            while ((i + l <= _table.Rows.Count) && (IsEmptyCell(_table, i + l, j + 4)))
                            {
                                if (IsEmptyCell(_table, i + l, j) && (IsEmptyCell(_table, i + l, j + 1)) &&
                                    (IsEmptyCell(_table, i + l, j + 2)) && (IsEmptyCell(_table, i + l, j + 3)) &&
                                    (IsEmptyCell(_table, i + l, j + 4)))
                                {
                                    l--;
                                    break;
                                }
                                ++l;
                            }

                            if (l > 0)
                                for (k = 1; k <= l; ++k)
                                {
                                    AddToSameName(productRepository, _table, i + k, j + 3);
                                    AddCount(productRepository, _table, i + k, j + 4);
                                    AddToSameDesignation(productRepository, _table, i + k, j + 5);
                                }

                            i += l;
                            l = 0;
                            if (i + 1 > _table.Rows.Count)
                                continue;
                            if (_table.Rows[i + 1].Cells.Count < 7)
                                continue;

                            while ((i + l + 1 <= _table.Rows.Count) && (!IsEmptyCell(_table, i + l + 1, j + 5)) &&
                                    IsEmptyCell(_table, i + l + 1, j + 3))
                                ++l;

                            if (l > 0)
                                for (k = 1; k <= l; ++k)
                                    AddToSameDesignation(productRepository, _table, i + k, j + 5);
                        }
                        else
                            if (i < _table.Rows.Count)
                                CreateProduct(productRepository, _table, ref i, j + 3);

                    ChangeProgressBar(i, EventArgs.Empty);


                }
                ChangeProgressBar(-1, EventArgs.Empty);
                ChangeReadListStatusLabel("Готово!", EventArgs.Empty);
                ChangeStatusLabel("Чтение файла спецификации выполнено успешно!", EventArgs.Empty);
            }
            catch (OutOfMemoryException ex)
            {
                Handling(ex);
                ChangeReadListStatusLabel("Ошибка!", EventArgs.Empty);
                ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (StackOverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
            catch (UnhandledException ex)
            {
                Error(ex, ChangeReadListStatusLabel);
            }
            catch (Exception ex)
            {
                Error(ex, ChangeReadListStatusLabel);
                throw;
            }
        }

        private string ReplaceText(Word.Table _table, int row, int col, string oldText, string newText)
        {
            try
            {
                return _table.Cell(row, col).Range.Text.Replace(oldText, newText);
            }
            catch (Exception ex)
            {
                DoWarning(ex, row, ChangeReadListStatusLabel);
                return "";
            }
        }

        private string DeleteSpacesFromElementsName(ProductRepository productRepository, int k)
        {
            try
            {
                return
                    DeleteSpaces(productRepository.Products[productRepository.Products.Count - 1].ElementsName[k].Name);
            }
            catch (Exception ex)
            {
                Handling(ex);
                throw new UnhandledException(ex);
            }
        }
        private string DeleteSpaces(string str)
        {
            return str.Replace(" ", "");
        }
        private void CreateProduct(ProductRepository productRepository, Word.Table _table, ref int i, int j)
        {
            try
            {
                productRepository.Products.Add(new Product(ReplaceText(_table, i, j, "\r\a", null)));
                while
                    ((IsEmptyCell(_table, i + 1, j - 1))
                     &&
                     (!IsEmptyCell(_table, i + 1, j)))
                {
                    productRepository.Products[productRepository.Products.Count - 1].
                        Manufacturers.Add(ReplaceText(_table, ++i, j, "\r\a", null) + " ");
                }
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Handling(ex);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (Exception ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
        }
        private void FillingProductElements(ProductRepository productRepository, Word.Table _table, int i, int j, bool isList)
        {
            try
            {
                if (isList)
                {
                    string partOfNewName = DeleteSpaces(ReplaceText(_table, i, j + 1, "\r\a", null));

                    for (int k = 0;
                        k < productRepository.Products[productRepository.Products.Count - 1].ElementsName.Count;
                        ++k)
                    {
                        string partOfExistingName = "";
                        string existingNameWithoutSpaces = DeleteSpacesFromElementsName(productRepository, k);

                        if (existingNameWithoutSpaces.Length >= partOfNewName.Length)
                            partOfExistingName = DeleteSpacesFromElementsName(productRepository, k)
                                .Substring(0, partOfNewName.Length);
                        else
                            partOfExistingName = DeleteSpacesFromElementsName(productRepository, k);

                        if (partOfNewName.Equals(partOfExistingName, StringComparison.Ordinal))
                        {
                            _sameNamePosition = k;
                            AddToSameDesignation(productRepository, _table, i, j);
                            return;
                        }
                    }
                    productRepository.Products[productRepository.Products.Count - 1].
                        ElementsName.
                        Add(new ElementNameObject(ReplaceText(_table, i, j + 1, "\r\a", null)));

                    ++_totalAmountElementsNames; //общее количество ElementNameObject

                    productRepository.Products[productRepository.Products.Count - 1].
                        ElementsName[
                            productRepository.Products[productRepository.Products.Count - 1].ElementsName.Count - 1
                        ].
                        ElementsDesignator.Add(ReplaceText(_table, i, j, "\r\a", null));
                }
                else
                {
                    productRepository.Products[productRepository.Products.Count - 1].
                        ElementsName.
                        Add(new ElementNameObject(ReplaceText(_table, i, j, "r\a", null)));

                    ++_totalAmountElementsNames; //общее количество ElementNameObject

                    productRepository.Products[productRepository.Products.Count - 1].
                        ElementsName[
                            productRepository.Products[productRepository.Products.Count - 1].ElementsName.Count - 1
                        ].
                        ElementsDesignator.Add(ReplaceText(_table, i, j + 2, "\r\a", null));
                }
                _sameNamePosition =
                    productRepository.Products[productRepository.Products.Count - 1].ElementsName.Count - 1;
            }
            catch (UnhandledException){}
            catch (Exception ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
        }
        private void AddToSameDesignation(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            try
            {
                if (IsEmptyCell(_table, i, j)) return;
                productRepository.Products[productRepository.Products.Count - 1].
                    ElementsName[_sameNamePosition].ElementsDesignator.Add(
                        ReplaceText(_table, i, j, "\r\a", null).Replace(" ", null));
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Handling(ex);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (Exception ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
        }
        private void AddToSameName(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            try
            {
                string partOfNewName = DeleteSpaces(ReplaceText(_table, i, j, "\r\a", null));
                string existingNameWithoutSpaces =
                    DeleteSpaces(
                        productRepository.Products[productRepository.Products.Count - 1].ElementsName[_sameNamePosition]
                            .Name);

                if (existingNameWithoutSpaces.Contains(partOfNewName)) return;

                productRepository.Products[productRepository.Products.Count - 1].
                    ElementsName[_sameNamePosition].Name += (" " + ReplaceText(_table, i, j, "\r\a", null));
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Handling(ex);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (Exception ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }
        }
        private void AddCount(ProductRepository productRepository, Word.Table _table, int i, int j)
        {
            try
            {
                if (ReplaceText(_table, i, j, "\r\a", null).Replace(" ", null) != "")
                    productRepository.Products[productRepository.Products.Count - 1].
                        ElementsName[_sameNamePosition].DesignatorsCount +=
                        Convert.ToInt32(ReplaceText(_table, i, j, "\r\a", null), CultureInfo.CurrentCulture);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Handling(ex);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (Exception ex)
            {
                DoWarning(ex, i, ChangeReadListStatusLabel);
            }

        }
#endregion

#region Write File

#region Write File to Doc

        private void WriteFileToDoc(ProductRepository productRepository, Word.Table _table, int tabNumber)
        {
            ChangeProgressBar(_totalAmountElementsNames, EventArgs.Empty);
            ChangeCreateSpecStatusLabel("В процессе...", EventArgs.Empty);
            ChangeStatusLabel("Создание файла 'docx'. Пожалуйста, подождите", EventArgs.Empty);

            int lastLine, i = 4, m = 0;
            try
            {
                if (tabNumber == 1)
                    WrtiteFirstPage(_table);
                else
                {
                    int positionNumber = Convert.ToInt32(_parameters.SourcePosition, CultureInfo.CurrentCulture);
                    
                    _table.Rows.Add();
                    for (int k = 0; k < productRepository.Products.Count; ++k)
                    {
                        if (!_parameters.RatingPlusName)
                        {
                            AddName(productRepository, _table, ref i, k);
                            _table.Cell(i, 5).Range.Underline =
                                Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                            AddManufacturers(productRepository, _table, ref i, k);
                            _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            _table.Rows.Add();
                            _table.Rows.Add();
                            i += 2;
                        }

                        for (int j = 0; j < productRepository.Products[k].ElementsName.Count; ++j)
                        {
                            _table.Cell(i, 3).Range.Text = positionNumber.ToString(CultureInfo.CurrentCulture);

                            if (_parameters.RatingPlusName)
                            {
                                if (AddProductToElementsNameDoc(productRepository, _table, i, k, j))
                                {
                                    _table.Rows.Add();
                                    ++i;
                                }
                            }
                            AddElementsName(productRepository, _table, i, k, j, out lastLine);
                            AddDesignatorsCount(productRepository, _table, k, j, lastLine);
                            AddDesignators(productRepository, _table, ref i, k, j);
                            i += 3;
                            positionNumber += 2;
                            _table.Rows.Add();
                            _table.Rows.Add();
                            ChangeProgressBar(++m, EventArgs.Empty);
                        }
                        ++i;
                    }
                    ChangeProgressBar(_totalAmountElementsNames, EventArgs.Empty);
                    ChangeCreateSpecStatusLabel("Готово!", EventArgs.Empty);
                    ChangeStatusLabel("Создание файла спецификации '.docx' выполнено успешно!", EventArgs.Empty);

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
                    ChangeProgressBar(-1, EventArgs.Empty);
                }
            }
            catch (OutOfMemoryException ex)
            {
                Handling(ex);
                ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (StackOverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (UnhandledException)
            {
                throw new UnhandledException();
            }
            catch (Exception ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
        }

        private void WrtiteFirstPage(Word.Table _table)
        {
            int i = 2;

            string nameOfDoc = "AAOT." + _parameters.DesignDocFirstString + "." + _parameters.DesignDocSecondString;
            string nameOfPcb = "AAOT." + _parameters.DesignPcbFirstString + "." + _parameters.DesignPcbSecondString;

            try
            {
                _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                    Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                _table.Cell(i, 5).Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                _table.Cell(i, 5).Range.Text = "Документация";
                i += 2;

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

                i += 2;
                _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                    Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                _table.Cell(i, 5).Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                _table.Cell(i, 5).Range.Text = "Детали";
                i += 2;

                _table.Cell(i, 2).Range.Text = _parameters.PcbFormat;
                _table.Cell(i, 4).Range.Text = nameOfPcb;
                _table.Cell(i, 5).Range.Text = "Плата";
                _table.Cell(i, 6).Range.Text = "1";

                if (_parameters.ElementsOfSmdMounting && !_parameters.BorrowedItems)
                {
                    _table.Rows[_table.Rows.Count].Cells[4].Merge(_table.Rows[_table.Rows.Count].Cells[5]);
                    _table.Cell(_table.Rows.Count, 4).Range.Text = "* Элементы SMD монтажа";
                }
                else if (!_parameters.ElementsOfSmdMounting && _parameters.BorrowedItems)
                {
                    _table.Rows[_table.Rows.Count].Cells[4].Merge(_table.Rows[_table.Rows.Count].Cells[5]);
                    _table.Cell(_table.Rows.Count, 4).Range.Text = "* Заимствованные изделия";
                }
                else if (_parameters.ElementsOfSmdMounting && _parameters.BorrowedItems)
                {
                    _table.Rows[_table.Rows.Count - 1].Cells[4].Merge(_table.Rows[_table.Rows.Count - 1].Cells[5]);
                    _table.Rows[_table.Rows.Count].Cells[4].Merge(_table.Rows[_table.Rows.Count].Cells[5]);

                    _table.Cell(_table.Rows.Count - 1, 4).Range.Text = "* Элементы SMD монтажа";
                    _table.Cell(_table.Rows.Count, 4).Range.Text = "** Заимствованные изделия";
                }
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (StackOverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (UnhandledException)
            {
                throw new UnhandledException();
            }
            catch (Exception ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
        }
        private void AddName(ProductRepository productRepository, Word.Table _table, ref int i, int k)
        {
            try
            {
                _table.Rows.Add();
                _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                    Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                _table.Cell(i, 5).Range.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                _table.Cell(i, 5).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                _table.Cell(i++, 5).Range.Text = productRepository.Products[k].Name;
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (StackOverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (UnhandledException)
            {
                throw new UnhandledException();
            }
            catch (Exception ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
        }
        private void AddManufacturers(ProductRepository productRepository, Word.Table _table, ref int i, int k)
        {
            int maxLengthOfManufacturersName = 25;

            try
            {
                _table.Rows.Add();
                if (productRepository.Products[k].Manufacturers == null ||
                    productRepository.Products[k].Manufacturers.Count == 0)
                    return;

                List<string> partsofname = new List<string>();

                for (int x = 0; x < productRepository.Products[k].Manufacturers.Count; ++x)
                {
                    int begin = 0;
                    int end;
                    do
                    {
                        end = productRepository.Products[k].Manufacturers[x].IndexOf(" ", begin,
                            StringComparison.OrdinalIgnoreCase);
                        partsofname.Add(
                            productRepository.Products[k].Manufacturers[x].Substring(begin, end - begin)
                                .Replace("\r", "")
                                .Replace("\n", ""));
                        begin = end + 1;
                    } while (
                        productRepository.Products[k].Manufacturers[x].IndexOf(" ", begin,
                            StringComparison.OrdinalIgnoreCase) > end - begin);
                    end = productRepository.Products[k].Manufacturers[x].Length;

                    if (
                        productRepository.Products[k].Manufacturers[x].Substring(begin, end - begin).Replace(" ", null) !=
                        null
                        &&
                        productRepository.Products[k].Manufacturers[x].Substring(begin, end - begin)
                            .Replace(" ", null)
                            .Length != 0)
                        partsofname.Add(
                            productRepository.Products[k].Manufacturers[x].Substring(begin, end - begin)
                                .Replace(" ", null));
                }
                string oldTemp = "";
                string newTemp = "";
                bool isAdded = false;

                for (int m = 0; m < partsofname.Count; ++m)
                {
                    if (partsofname[m] == "") continue;
                    newTemp += (partsofname[m] + " ");
                    if (newTemp.Length <= maxLengthOfManufacturersName)
                    {
                        oldTemp = newTemp;
                        isAdded = true;
                        if (m + 1 != partsofname.Count)
                            continue;
                        else
                        {
                            _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                                Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            _table.Cell(i++, 5).Range.Text = oldTemp;
                            _table.Rows.Add();
                            oldTemp = newTemp = "";
                            isAdded = false;
                        }
                    }
                    else if (newTemp.Length > maxLengthOfManufacturersName && !isAdded)
                    {
                        _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                            Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        _table.Cell(i++, 5).Range.Text = newTemp;
                        _table.Rows.Add();
                        oldTemp = newTemp = "";
                    }
                    else
                    {
                        _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                            Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        _table.Cell(i++, 5).Range.Text = oldTemp;
                        _table.Rows.Add();
                        oldTemp = newTemp = "";
                        isAdded = false;
                        --m;
                    }
                    _table.Cell(i, 5).Range.ParagraphFormat.Alignment =
                        Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                }
            }
            catch (OutOfMemoryException ex)
            {
                Handling(ex);
                ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (StackOverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (UnhandledException)
            {
                throw new UnhandledException();
            }
            catch (Exception ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
        }

        private bool AddProductToElementsNameDoc(ProductRepository productRepository, Word.Table _table, int i, int k,
            int j)
        {
            string singularProductName = _singularProduct.ReturnSingularProductName(productRepository.Products[k].Name);// Product
            List<string> currentDesignatorsNames =
                _designatorNameRepository.ReturnDesignatorsName(
                    productRepository.Products[k].ElementsName[j].ElementsDesignator[0]); // C1 -> C -> "конденсатор, ионистор"
            if (currentDesignatorsNames == null) return true;

            foreach (string name in currentDesignatorsNames)
            {
                if (singularProductName == name)
                {
                    if (!IsContainsInElemName(productRepository.Products[k].ElementsName[j].Name.ToLower(),
                        singularProductName))
                    {
                        _table.Cell(i, 5).Range.Text = singularProductName;
                        return true;
                    }
                    return false;
                }
            }

            foreach (string name in currentDesignatorsNames)
                if (IsContainsInElemName(productRepository.Products[k].ElementsName[j].Name.ToLower(), name))
                    return false;
            
            _table.Cell(i, 5).Range.Text = currentDesignatorsNames[0];
            return true;
        }

        private bool IsContainsInElemName(string elementsName, string designatorsName)
        {
            if (elementsName.Contains(designatorsName)) 
                return true;
            return false;
        }
        private void AddElementsName(ProductRepository productRepository, Word.Table _table, int i, int k, int j, out int lastLine)
        {
            int maxLengthOfName = 25;
            lastLine = i;

            try{
                if (productRepository.Products[k].ElementsName[j].Name.Length <= maxLengthOfName)
                    _table.Cell(i, 5).Range.Text = productRepository.Products[k].ElementsName[j].Name.Replace("(F", "mF");
                else
                {
                    List<string> partsofname = new List<string>();
                    int begin = 0;
                    int end;
                    do
                    {
                        end = productRepository.Products[k].ElementsName[j].Name.IndexOf(" ", begin,
                            StringComparison.OrdinalIgnoreCase);
                        partsofname.Add(
                            productRepository.Products[k].ElementsName[j].Name.Substring(begin, end - begin)
                                .Replace("(F", "mF").Replace("\r", "").Replace("\n", ""));
                        begin = end + 1;
                    } while (
                        productRepository.Products[k].ElementsName[j].Name.IndexOf(" ", begin,
                            StringComparison.OrdinalIgnoreCase) > end - begin);
                    end = productRepository.Products[k].ElementsName[j].Name.Length;
                    if (
                        productRepository.Products[k].ElementsName[j].Name.Substring(begin, end - begin)
                            .Replace(" ", null) != null
                        &&
                        productRepository.Products[k].ElementsName[j].Name.Substring(begin, end - begin)
                            .Replace(" ", null)
                            .Length != 0)
                        partsofname.Add(
                            productRepository.Products[k].ElementsName[j].Name.Substring(begin, end - begin)
                                .Replace(" ", null));

                    string oldTemp = "";
                    string newTemp = "";
                    bool isAdded = false;

                    for (int m = 0; m < partsofname.Count; ++m)
                    {
                        if (partsofname[m] == "") continue;
                        newTemp += (partsofname[m] + " ");
                        if (newTemp.Length <= maxLengthOfName)
                        {
                            oldTemp = newTemp;
                            isAdded = true;
                            if (m + 1 != partsofname.Count)
                                continue;
                            else
                            {
                                _table.Cell(lastLine, 5).Range.Text = oldTemp;
                                oldTemp = newTemp = " ";
                                isAdded = false;
                            }
                        }
                        else if (newTemp.Length > maxLengthOfName && !isAdded)
                        {
                            _table.Cell(lastLine++, 5).Range.Text = newTemp;
                            _table.Rows.Add();
                            oldTemp = newTemp = "";
                        }
                        else
                        {
                            _table.Cell(lastLine++, 5).Range.Text = oldTemp;
                            _table.Rows.Add();
                            oldTemp = newTemp = "";
                            isAdded = false;
                            --m;
                        }
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                Handling(ex);
                ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (UnhandledException)
            {
                throw new UnhandledException();
            }
            catch (Exception ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
        }
        private void AddDesignatorsCount(ProductRepository productRepository, Word.Table _table, int k, int j, int lastLine)
        {
            _table.Cell(lastLine, 6).Range.Text =
                        productRepository.Products[k].ElementsName[j].DesignatorsCount.ToString(CultureInfo.CurrentCulture);
        }
        private void AddDesignators(ProductRepository productRepository, Word.Table _table, ref int i, int k, int j)
        {
            string oldTemp = "";
            string newTemp = "";
            bool isAdded = false;

            try
            {
                // Добавление звёздочки если элемент - SMD
                if (_parameters.ElementsOfSmdMounting)
                    for (int l = 0; l < _parameters.SmdIdentificators.Count; ++l)
                        if (
                            productRepository.Products[k].ElementsName[j].Name.Contains(
                                _parameters.SmdIdentificators[l]))
                        {
                            newTemp += "*";
                            break;
                        }

                for (int m = 0; m < productRepository.Products[k].ElementsName[j].ElementsDesignator.Count; ++m)
                {
                    newTemp += productRepository.Products[k].ElementsName[j].ElementsDesignator[m];
                    if (newTemp.Length <= 7)
                    {
                        oldTemp = newTemp;
                        isAdded = true;
                        if (m + 1 != productRepository.Products[k].ElementsName[j].ElementsDesignator.Count)
                            continue;
                        else
                        {
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
            catch (OutOfMemoryException ex)
            {
                Handling(ex);
                ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
            }
            catch (NullReferenceException ex)
            {
                Handling(ex);
            }
            catch (OverflowException ex)
            {
                Handling(ex);
            }
            catch (ArgumentNullException ex)
            {
                Handling(ex);
            }
            catch (IndexOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
            catch (UnhandledException)
            {
                throw new UnhandledException();
            }
            catch (Exception ex)
            {
                if (Warning(ex, i))
                {
                    ChangeCreateSpecStatusLabel("Ошибка!", EventArgs.Empty);
                    throw new UnhandledException();
                }
            }
        }
#endregion

#region Write File to Xls
        private void WriteFileToXls(ProductRepository productRepository, Excel.Workbook excelappworkbook)
        {
            int i = 1, k = 0; // i - rows
            int positionNumber = -1;

            ChangeProgressBar(_totalAmountElementsNames, EventArgs.Empty);
            ChangeCreateSpecStatusLabel("В процессе...", EventArgs.Empty);
            ChangeStatusLabel("Создание файла '.xlsx'. Пожалуйста, подождите...", EventArgs.Empty);
            
            _excelSheets = excelappworkbook.Worksheets;
            _excelWorkSheet = (Excel.Worksheet)_excelSheets.get_Item(1);

            _excelWorkSheet.Columns[1].ColumnWidth = 3;
            _excelWorkSheet.Columns[2].ColumnWidth = 4;
            _excelWorkSheet.Columns[3].ColumnWidth = 22;
            _excelWorkSheet.Columns[4].ColumnWidth = 60;
            _excelWorkSheet.Columns[5].ColumnWidth = 4;
            _excelWorkSheet.Columns[6].ColumnWidth = 30;

            if (_parameters.Hat)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 = "Формат";
                ((Excel.Range)_excelWorkSheet.Cells[i, 2]).Value2 = "Поз.";
                ((Excel.Range)_excelWorkSheet.Cells[i, 3]).Value2 = "Обозначение";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = "Наименование";
                ((Excel.Range)_excelWorkSheet.Cells[i, 5]).Value2 = "Кол.";
                ((Excel.Range)_excelWorkSheet.Cells[i, 6]).Value2 = "Примечание";
                ++i;
            }
            ChangeProgressBar(i, EventArgs.Empty);
            
            if (_isList)
            {
                WrtiteFirstPage(ref i);
                ++i;
                positionNumber = Convert.ToInt32(_parameters.SourcePosition, CultureInfo.CurrentCulture);
            }
            ChangeProgressBar(i, EventArgs.Empty);

            if (!_parameters.FirstPage)
            {
                int x = 0;
                while ((productRepository.Products[x].Name != "Детали") &&
                       (productRepository.Products[x].Name != "Стандартные изделия") &&
                       productRepository.Products[x].Name != "Прочие изделия")
                    ++x;
                k = x;
            }
            
            for ( ; k < productRepository.Products.Count; ++k)
            {
                if (!_parameters.RatingPlusName)
                {
                    AddNameToXls(productRepository, ref i, k);
                    AddManufacturersToXls(productRepository, ref i, k);
                    ++i;
                }
                ChangeProgressBar(i, EventArgs.Empty);
                Dictionary<int, bool> duplication = new Dictionary<int, bool>(productRepository.Products[k].ElementsName.Count);
                DeterminationOfDuplication(productRepository, duplication, k);
                for (int j = 0; j < productRepository.Products[k].ElementsName.Count; ++j)
                {
                    AddFormatToXls(productRepository, i, k, j);
                    if (positionNumber != -1)
                        ((Excel.Range)_excelWorkSheet.Cells[i, 2]).Value2 = positionNumber.ToString(CultureInfo.CurrentCulture);
                    else
                        AddPositionToXls(productRepository, i, k, j);
                    AddDesignation(productRepository, i, k, j);
                    if (_parameters.RatingPlusName)
                        if (AddProductToElementsNameXls(productRepository, i, k, j))
                            ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 += " ";
                    AddElementsNameToXls(productRepository, i, k, j, duplication);
                    AddDesignatorsCountToXls(productRepository, i, k, j);
                    AddDesignatorsToXls(productRepository, ref i, k, j);
                    ++i;
                    if (positionNumber != -1) positionNumber += 2;
                    ChangeProgressBar(i, EventArgs.Empty);
                 }
                ++i;
                ChangeProgressBar(i, EventArgs.Empty);
                duplication.Clear();
            }
            ChangeCreateSpecStatusLabel("Готово!", EventArgs.Empty);
            ChangeStatusLabel("Создание файла спецификации '.xlsx' выполнено успешно!", EventArgs.Empty);
            ChangeProgressBar(-1, EventArgs.Empty);
            ++i;
        }

        private void DeterminationOfDuplication(ProductRepository productRepository, Dictionary<int, bool> duplication,
            int k)
        {
            for (int i = 0; i < productRepository.Products[k].ElementsName.Count - 1; ++i)
                for (int j = i + 1; j < productRepository.Products[k].ElementsName.Count; ++j)
                    if (productRepository.Products[k].ElementsName[i].Name ==
                        productRepository.Products[k].ElementsName[j].Name)
                    {
                        if (!duplication.ContainsKey(i))
                            duplication.Add(i, true);
                        if (!duplication.ContainsKey(j))
                            duplication.Add(j, true);
                    }
        }

        private void AddDesignation(ProductRepository productRepository, int i, int k, int j)
        {
            ((Excel.Range) _excelWorkSheet.Cells[i, 3]).Value2 =
                productRepository.Products[k].ElementsName[j].Designation;
        }
        private void AddPositionToXls(ProductRepository productRepository, int i, int k, int j)
        {
            ((Excel.Range)_excelWorkSheet.Cells[i, 2]).Value2 =
                productRepository.Products[k].ElementsName[j].Position;
        }
        private void AddFormatToXls(ProductRepository productRepository, int i, int k, int j)
        {
            ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 =
                productRepository.Products[k].ElementsName[j].Format;
        }

        private void WrtiteFirstPage(ref int i)
        {
            string nameOfDoc = "AAOT." + _parameters.DesignDocFirstString + "." + _parameters.DesignDocSecondString;
            string nameOfPcb = "AAOT." + _parameters.DesignPcbFirstString + "." + _parameters.DesignPcbSecondString;

            if (_parameters.AssemblyDrawing)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 = _parameters.AssemblyDrawingFormat;
                ((Excel.Range)_excelWorkSheet.Cells[i, 3]).Value2 = nameOfDoc + " СБ";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = "Сборочный чертеж";
                ++i;
            }

            if (_parameters.ElectricalCircuit)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 = _parameters.ElectricalCircuitFormat;
                ((Excel.Range)_excelWorkSheet.Cells[i, 3]).Value2 = nameOfDoc + " ЭЗ";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = "Схема электрическая ";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 += "принципиальная";
                ++i;
            }

            if (_parameters.ListOfitems)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 = _parameters.ListOfitemsFormat;
                ((Excel.Range)_excelWorkSheet.Cells[i, 3]).Value2 = nameOfDoc + " ПЭЗ";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = "Перечень элементов";
                ++i;
            }

            if (_parameters.Pcb)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 = "-";
                ((Excel.Range)_excelWorkSheet.Cells[i, 3]).Value2 = nameOfPcb + " М";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = "Плата. Данные конструкции";
                ((Excel.Range)_excelWorkSheet.Cells[i, 6]).Value2 = "CD";
                ++i;
            }

            if (_parameters.CertifyingSheet)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 = _parameters.CertifyingSheetFormat;
                ((Excel.Range)_excelWorkSheet.Cells[i, 3]).Value2 = nameOfPcb + " М-УД";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = "Плата. Данные конструкции ";
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 += "Удостоверяющий лист";
                ++i;
            }

            ((Excel.Range)_excelWorkSheet.Cells[i, 1]).Value2 = _parameters.PcbFormat;
            ((Excel.Range)_excelWorkSheet.Cells[i, 3]).Value2 = nameOfPcb;
            ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = "Плата";
            ((Excel.Range)_excelWorkSheet.Cells[i, 5]).Value2 = "1";
        }
        private void AddNameToXls(ProductRepository productRepository, ref int i, int k)
        {
            ((Excel.Range)_excelWorkSheet.Cells[i++, 4]).Value2 = productRepository.Products[k].Name;
        }
        private void AddManufacturersToXls(ProductRepository productRepository, ref int i, int k)
        {
            if (productRepository.Products[k].Manufacturers == null ||
                productRepository.Products[k].Manufacturers.Count == 0)
                return;

            for (int x = 0; x < productRepository.Products[k].Manufacturers.Count; ++x)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 += (productRepository.Products[k].Manufacturers[x] + " ");
            }
        }
        private bool AddProductToElementsNameXls(ProductRepository productRepository, int i, int k, int j)
        {
            string singularProductName = _singularProduct.ReturnSingularProductName(productRepository.Products[k].Name);// Product
            List<string> currentDesignatorsNames =
                _designatorNameRepository.ReturnDesignatorsName(
                    productRepository.Products[k].ElementsName[j].ElementsDesignator[0]); // C1 -> C -> "конденсатор, ионистор"
            if (currentDesignatorsNames == null) return true;

            foreach (string name in currentDesignatorsNames)
            {
                if (singularProductName == name)
                {
                    if (!IsContainsInElemName(productRepository.Products[k].ElementsName[j].Name.ToLower(),
                        singularProductName))
                    {
                        ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = singularProductName;
                        return true;
                    }
                    return false;
                }
            }

            foreach (string name in currentDesignatorsNames)
                if (IsContainsInElemName(productRepository.Products[k].ElementsName[j].Name.ToLower(), name))
                    return false;

            ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 = currentDesignatorsNames[0];
            return true;
        }
        private void AddElementsNameToXls(ProductRepository productRepository, int i, int k, int j, Dictionary<int, bool> duplication)
        {
            ((Excel.Range)_excelWorkSheet.Cells[i, 4]).Value2 += productRepository.Products[k].ElementsName[j].Name.Replace("(F", "mF");
            if (duplication.ContainsKey(j))
            {
                _excelWorkSheet.Range["A1"].Offset[i - 1, 3].Interior.ColorIndex = 36;
            }
        }
        private void AddDesignatorsCountToXls(ProductRepository productRepository, int i, int k, int j)
        {
            ((Excel.Range)_excelWorkSheet.Cells[i, 5]).Value2 = productRepository.Products[k].ElementsName[j].DesignatorsCount.ToString(CultureInfo.CurrentCulture);
        }
        private void AddDesignatorsToXls(ProductRepository productRepository, ref int i, int k, int j)
        {
            for (int m = 0; m < productRepository.Products[k].ElementsName[j].ElementsDesignator.Count; ++m)
            {
                ((Excel.Range)_excelWorkSheet.Cells[i, 6]).Value2 += (productRepository.Products[k].ElementsName[j].ElementsDesignator[m] + " ");
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
                        document.Close(ref _falseObj, ref _missingObj, ref _missingObj);
                }
                catch (Exception){}
                finally
                {
                    try
                    {
                        application.Quit(ref _missingObj, ref _missingObj, ref _missingObj);
                    }
                    catch (Exception){}
                    finally
                    {
                        document = null;
                        application = null;
                    }
                }
            }
        }
        private static void CloseDocument(ref Excel.Application application)
        {
            if (application != null)
            {
                try
                {
                    if (application.Workbooks != null)
                        application.Workbooks.Close();
                }
                catch (Exception){}
                finally
                {
                    try
                    {
                        application.Quit();
                    }
                    catch (Exception){}
                    finally
                    {
                        application = null;
                    }
                }
            }

        }
#endregion

#endregion

#region Variables
        private Word._Application _applicationWord;
        private Word._Document _documentWord;
        private Excel.Application _applicationExcel;
        private Excel.Sheets _excelSheets;
        private Excel.Worksheet _excelWorkSheet;
        private Excel.Workbooks _excelAppWorkBooks;
        private Excel.Workbook _excelAppWorkBook;
        private Parameters _parameters;
        private Object _missingObj = System.Reflection.Missing.Value;
        private Object _falseObj = false;
        private int _sameNamePosition;
        private int _totalAmountElementsNames;
        private bool _isList;
        private bool _isSpecification;
        private DesignatorNameRepository _designatorNameRepository;
        private SingularProduct _singularProduct;
        #endregion
    }
}
