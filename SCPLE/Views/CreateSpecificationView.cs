using System;
using System.Globalization;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Scple.Interface;

namespace Scple.View
{
    /// <summary>
    /// Представление, в которм указываются необходимые параметры 
    /// для создания первого листа спецификации и файла спецификации вцелом
    /// </summary>
    public partial class CreateSpecificationView : Form, IViewFileCreation
    {
        #region Delegates
        delegate void IsEnableDelegate();
        #endregion

        #region IViewFileCreation Methods
        #region Methods
        public void Initialization(Parameters parameters)
        {
            _parameters = parameters;
            Init();
        }
        public void IsEnabled()
        {
            Invoke(new IsEnableDelegate(IsEnable));
        }
        #endregion
        #region Events
        public event EventHandler<EventArgs> ProcessingView;
        public event EventHandler<EventArgs> StartCreating;
        public event EventHandler<EventArgs> CloseForm;
        #endregion
        #endregion

        #region Constructor & Initialization
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="filePath">Путь к файлу-перечню</param>
        public CreateSpecificationView(string filePath)
        {
            InitializeComponent();
            _parameters = new Parameters();
            Init();
            DesignDoc_maskedTB.Text = "";
            DesignDoc_TB.Text = "";
            DesignPcb_maskedTB.Text = "";
            DesignPcb_TB.Text = "";
            toolStripStatusLabel1.Text = "Шаг 2/2 - Дополнительная информация";
            SpecDesignation_gbx.Enabled = true;
            DesignationPcb_gBx.Enabled = true;
            CreationFile_gBx.Enabled = true;
            ElementsOfSMDMounting_chkBx.Enabled = true;
            BorrowedItems_chkBx.Enabled = true;
            gb_SourcePosition.Enabled = true;
            Start_btn.Enabled = true;

            if (ListOrSpecDocument(filePath))
                _listFilePath = filePath;
            else
            {
                string message1 = "Выбранный файл НЕ является ни перечнем элементов, ни спецификацией.";
                string message2 = "Если это не так:";
                string message3 = "1. Проверте корректность информации в файле";
                string message4 = "2. Измените расширение файла (.doc <-> .docx)";
                string message5 = "Если проблема не будет решена, обратитесь к разработчику программы";
                MessageBox.Show(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n", message1, message2, message3, message4, message5), "Ошибка!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private bool ListOrSpecDocument(string filePath)
        {
            Word.Application applicationWord = null;
            Word._Document documentWord = null;
            try
            {
                applicationWord = new Word.Application();
                Object _missingObj = System.Reflection.Missing.Value;
                Object _falseObj = false;
                Object templatePathObj = filePath;
                documentWord = applicationWord.Documents.Add
                    (ref templatePathObj, ref _missingObj, ref _missingObj, ref _missingObj);
                
                Word.Table _table = documentWord.Tables[1];
                if ((_table.Cell(1, 1).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("№") &&
                     _table.Cell(1, 3).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("поз")) &&
                    _table.Cell(1, 4).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("обозн"))
                {
                    FileXls_rb_CheckedChanged(this, EventArgs.Empty);

                    SpecDesignation_gbx.Enabled = false;
                    DesignationPcb_gBx.Enabled = false;
                    CreationFile_gBx.Enabled = false;
                    ElementsOfSMDMounting_chkBx.Enabled = false;
                    BorrowedItems_chkBx.Enabled = false;
                    gb_SourcePosition.Enabled = false;
                    
                    toolStripStatusLabel1.Text = "Шаг 2/2 - Подготовка к созданию спецификации .xlsx";
                    CloseDocument(applicationWord, ref documentWord);
                    return true;
                }
                else if ((_table.Cell(1, 1).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("поз") &&
                          _table.Cell(1, 2).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("наименование")))
                {
                    XlsSettings_gBx.Enabled = false;
                    toolStripStatusLabel1.Text = "Шаг 2/2 - Подготовка к созданию спецификации .docx";
                    CloseDocument(applicationWord, ref documentWord);
                    return true;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Ошибка! Выберите корректный файл!";
                    Start_btn.Enabled = false;
                    CloseDocument(applicationWord, ref documentWord);
                    return false;
                }
            }
            catch (Exception)
            {
                toolStripStatusLabel1.Text = "Ошибка! Выберите корректный файл!";
                Start_btn.Enabled = false;
                CloseDocument(applicationWord, ref documentWord);
                SpecDesignation_gbx.Enabled = false;
                DesignationPcb_gBx.Enabled = false;
                CreationFile_gBx.Enabled = false;
                ElementsOfSMDMounting_chkBx.Enabled = false;
                BorrowedItems_chkBx.Enabled = false;
                return false;
            }
        }
        /// <summary>
        /// Инициализация параметров представления
        /// </summary>
        private void Init()
        {
            AssemblyDrawing_chkBx.Checked = _parameters.AssemblyDrawing;
            BorrowedItems_chkBx.Checked = _parameters.BorrowedItems;
            CertifyingSheet_chkBx.Checked = _parameters.CertifyingSheet;
            ElectricalCircuit_chkBx.Checked = _parameters.ElectricalCircuit;
            ElementsOfSMDMounting_chkBx.Checked = _parameters.ElementsOfSmdMounting;
            FileDoc_rb.Checked = _parameters.FileDoc;
            FileXls_rb.Checked = _parameters.FileXls;
            ListOfitems_chkBx.Checked = _parameters.ListOfitems;
            Pcb_chkBx.Checked = _parameters.Pcb;
            DesignDoc_maskedTB.Text = "";
            DesignDoc_maskedTB.SelectionStart = 0;
            DesignDoc_TB.Text = "";
            DesignPcb_maskedTB.Text = _parameters.DesignPcbFirstString;
            DesignPcb_maskedTB.SelectionStart = 0;
            DesignPcb_TB.Text = "";
            SourcePosition_maskedTB.Text = _parameters.SourcePosition;
            Hat_chkBx.Checked = _parameters.Hat;
            FirstPage_chkBx.Checked = _parameters.FirstPage;
            RatingPlusName_chkBx.Checked = _parameters.RatingPlusName;
        }
        #endregion

        #region Close
        private void CloseDocument(Word._Application application, ref Word._Document document)
        {
            if (application != null)
            {
                try
                {
                    if (document != null)
                        document.Close(ref _falseObj, ref _missingObj, ref _missingObj);
                }
                catch (Exception) { }
                finally
                {
                    try
                    {
                        application.Quit(ref _missingObj, ref _missingObj, ref _missingObj);
                    }
                    catch (Exception) { }
                    finally
                    {
                        document = null;
                        application = null;
                    }
                }
            }
        }
        #endregion

        #region Buttons
        private void Start_btn_Click(object sender, EventArgs e)
        {
            if ((SourcePosition_maskedTB.Text == "" || SourcePosition_maskedTB.Text == " " ) && SourcePosition_maskedTB.Enabled)
            {
                MessageBox.Show("Необходимо указать начальную позицию", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            Parameters parameters = new Parameters();
            ReadParameters(parameters);

            _processingView = new ProcessingView(_listFilePath, parameters);

            ProcessingView(_processingView, EventArgs.Empty);

            _processingView.Visible = true;
            this.Enabled = false;
            _processingView.Show();

            StartCreating(_processingView, EventArgs.Empty);
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            CloseForm(this, EventArgs.Empty);
            this.Close();
        }
        #endregion

        #region Event Handlers
        private void CreateSpecificationView_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseForm(this, EventArgs.Empty);
        }
        private void DesignDoc_maskedTB_Click(object sender, EventArgs e)
        {
            DesignDoc_maskedTB.SelectionStart = 0;
        }
        private void DesignPcb_maskedTB_Click(object sender, EventArgs e)
        {
            DesignPcb_maskedTB.SelectionStart = 0;
        }
        #endregion

        #region Auxiliary
        void IsEnable()
        {
            this.Enabled = true;
        }
        void ReadParameters(Parameters parameters)
        {
            parameters = _parameters;
            parameters.AssemblyDrawing = AssemblyDrawing_chkBx.Checked;
            parameters.BorrowedItems = BorrowedItems_chkBx.Checked;
            parameters.CertifyingSheet = CertifyingSheet_chkBx.Checked;
            parameters.ElectricalCircuit = ElectricalCircuit_chkBx.Checked;
            parameters.ElementsOfSmdMounting = ElementsOfSMDMounting_chkBx.Checked;
            parameters.FileDoc = FileDoc_rb.Checked;
            parameters.FileXls = FileXls_rb.Checked;
            parameters.ListOfitems = ListOfitems_chkBx.Checked;
            parameters.Pcb = Pcb_chkBx.Checked;
            parameters.DesignDocFirstString = DesignDoc_maskedTB.Text;
            parameters.DesignDocSecondString = DesignDoc_TB.Text;
            parameters.DesignPcbFirstString = DesignPcb_maskedTB.Text;
            parameters.DesignPcbSecondString = DesignPcb_TB.Text;
            parameters.SourcePosition = SourcePosition_maskedTB.Text;
            parameters.Hat = Hat_chkBx.Checked;
            parameters.FirstPage = FirstPage_chkBx.Checked;
            parameters.RatingPlusName = RatingPlusName_chkBx.Checked;
        }
        #endregion
        
        #region Variables
        private ProcessingView _processingView;
        private Parameters _parameters;
        private readonly string _listFilePath;

        Object _missingObj = System.Reflection.Missing.Value;
        Object _falseObj = false;
        #endregion

        private void FileDoc_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (CreationFile_gBx.Enabled)
            {
                XlsSettings_gBx.Enabled = false;
                ElementsOfSMDMounting_chkBx.Enabled = true;
                BorrowedItems_chkBx.Enabled = true;
            }
        }

        private void FileXls_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (CreationFile_gBx.Enabled)
            {
                XlsSettings_gBx.Enabled = true;
                ElementsOfSMDMounting_chkBx.Enabled = false;
                BorrowedItems_chkBx.Enabled = false;
            }

        }
        
    }
}
