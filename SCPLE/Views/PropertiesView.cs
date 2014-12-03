using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Scple.Interface;
using Scple.Presenters;
using SCPLE.Properties;

namespace Scple.View
{
    #region Delegates
    delegate void ChangeParameters(
        object newData, string name1, string name2);
    delegate void SaveSmdDesignatorsListToFile(Collection<string> SmdIdentificators);
    #endregion

    /// <summary>
    /// Представление, отвечающее за установку 
    /// пользователем настроек по умолчанию
    /// </summary>
    public partial class PropertiesView : Form, IViewProperties
    {
        #region IViewProperties
        #region Methods
        /// <summary>
        /// Инкапсуляция TextBox шаблона спецификации
        /// </summary>
        public string SpecificationFileTemplateTxBx
        {
            get { return SpecificationTemplate_txBx.Text; }
            set { SpecificationTemplate_txBx.Text = value; }
        }
        /// <summary>
        /// Инкапсуляция TextBox файла настроек программы
        /// </summary>
        public string SettingsFileTxBx
        {
            get { return Settings_txBx.Text; }
            set { Settings_txBx.Text = value; }
        }
        /// <summary>
        /// Отображает форму
        /// </summary>
        public void ShowForm()
        {
            this.Show();
            IsVisible(true);
        }
        /// <summary>
        /// Разрешение видимости формы
        /// </summary>
        public void IsVisible(bool isVisible)
        {
            this.Visible = isVisible;
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> SetDocument;
        public event EventHandler<EventArgs> SetFilePath;
        public event EventHandler<EventArgs> CloseForm;
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="presenter"></param>
        public PropertiesView(Presenter presenter)
        {
            InitializeComponent();
            _changedTemplatePath = false;
            _changedSettingsPath = false;
            _presenter = presenter;
            AddBtn.Enabled = false;
            DelBtn.Enabled = false;
            Save_btn.Enabled = false;
        }
        #endregion
        
        #region Initialization
        /// <summary>
        /// Установка параметров по умолчанию
        /// </summary>
        /// <param name="parameters"></param>
        public void SetParameters(Parameters parameters)
        {
            Initialization(parameters);
        }
        private void Initialization(Parameters parameters)
        {
            _parameters = parameters;
            Init();
        }
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
            DesignPcb_maskedTB.Text = _parameters.DesignPcbFirstString;
            DesignPcb_maskedTB.SelectionStart = 0;
            SourcePosition_maskedTB.Text = _parameters.SourcePosition;
            SourcePosition_maskedTB.SelectionStart = 0;

            AssemblyDrawing_cmbBx.Text = _parameters.AssemblyDrawingFormat;
            CertifyingSheet_cmbBx.Text = _parameters.CertifyingSheetFormat;
            ElectricalCircuit_cmbBx.Text = _parameters.ElectricalCircuitFormat;
            ListOfitems_cmbBx.Text = _parameters.ListOfitemsFormat;
            Pcb_cmbBx.Text = _parameters.PcbFormat;

            SpecificationFileTemplateTxBx = _parameters.TemplateFilePath;
            SettingsFileTxBx = _parameters.SettingsFilePath;

            SmdDesignatorsFile_txBx.Text = _parameters.SmdIdentificatorsFilePath;
            for (int i = 0; i < _parameters.SmdIdentificators.Count; ++i)
                IdentificatorsListBox.Items.Add(_parameters.SmdIdentificators[i]);

            Hat_chkBx.Checked = _parameters.Hat;
            FirstPage_chkBx.Checked = _parameters.FirstPage;
            RatingPlusName_chkBx.Checked = _parameters.RatingPlusName;

            AddBtn.Enabled = false;
            DelBtn.Enabled = false;

            ResetChanges();
        }
        private void ResetChanges()
        {
            _changedTemplatePath = false;
            _changedSettingsPath = false;
            _changedDesignPcbMaskedTb = false;
            _changedFileDocRb = false;
            _changedFileXlsRb = false;
            _changedElementsOfSmdMountingChkBx = false;
            _changedBorrowedItemsChkBx = false;
            _changedPcbChkBx = false;
            _changedCertifyingSheetChkBx = false;
            _changedAssemblyDrawingChkBx = false;
            _changedElectricalCircuitChkBx = false;
            _changedListOfitemsChkBx = false;
            _changedPcbCmbBx = false;
            _changedCertifyingSheetCmbBx = false;
            _changedAssemblyDrawingCmbBx = false;
            _changedElectricalCircuitCmbBx = false;
            _changedListOfItemsCmbBx = false;
            _changedListOfSmdIdentificators = false;
            _changedSourcePositionMaskedTb = false;
            _changeHat = false;
            _changeFirstPage = false;
            _changeRatingPlusName = false;
            _changeSmdDesignatorsFilePath = false;
        }
        #endregion
        
        #region Buttons
        private void SpecificationTemplatePath_btn_Click(object sender, EventArgs e)
        {
            string temp = SpecificationTemplate_txBx.Text;

            _document = DocumentList.Template;

            Stream myStream = null;
            openFileDialog1.InitialDirectory = @_parameters.TemplateFilePath;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "doc files (*.doc)|*.doc";
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            SpecificationTemplate_txBx.Text = openFileDialog1.FileName;
                            SetFilepathService(_document);
                            if (temp != SpecificationTemplate_txBx.Text)
                            {
                                _changedTemplatePath = true;
                                Save_btn.Enabled = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot find file. " + ex.Message,
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
            else
                return;
        }
        private void SettingsPath_btn_Click(object sender, EventArgs e)
        {
            string temp = Settings_txBx.Text;

            _document = DocumentList.Settings;

            Stream myStream = null;
            openFileDialog1.InitialDirectory = @_parameters.SettingsFilePath;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Xml files (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            Settings_txBx.Text = openFileDialog1.FileName;
                            SetFilepathService(_document);
                            if (temp != Settings_txBx.Text)
                            {
                                _changedSettingsPath = true;
                                Save_btn.Enabled = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot find file. " + ex.Message, "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
            else
                return;
        }
        private void AddIdentificator(object sender, EventArgs e)
        {
            if (IdentificatorTxBx.Text == null && IdentificatorTxBx.Text.Length == 0)
                    return;
            for (int i = 0; i < IdentificatorsListBox.Items.Count; ++i)
                if (IdentificatorsListBox.Items[i].Equals(IdentificatorTxBx.Text))
                {
                    ResetAddBtnService();
                    return;
                }
            IdentificatorsListBox.Items.Add(IdentificatorTxBx.Text);
            _changedListOfSmdIdentificators = true;
            ResetAddBtnService();
            AddBtn.Enabled = false;
            Save_btn.Enabled = true;
        }
        private void DeleteIdentificator(object sender, EventArgs e)
        {
            for (int i = 0; i < IdentificatorsListBox.Items.Count; ++i)
            {
                if (IdentificatorsListBox.Items[i].Equals(_tempIdentificatorForRemove))
                {
                    IdentificatorsListBox.SelectedItem = null;
                    IdentificatorsListBox.Items.Remove(_tempIdentificatorForRemove);
                    _changedListOfSmdIdentificators = true;
                    break;
                }
            }
            DelBtn.Enabled = false;
            Save_btn.Enabled = true;
        }
        private void Save_btn_Click(object sender, EventArgs e)
        {
            _changeParameters = new ChangeParameters(_presenter.ChangeParameters);
            _saveSmdDesignatorsListToFile = new SaveSmdDesignatorsListToFile(_presenter.SaveSmdDesignatorsListToFile);
            
            if (_changedTemplatePath)
                _changeParameters(SpecificationFileTemplateTxBx, "Template", "path");
            if (_changedSettingsPath)
                _changeParameters(SettingsFileTxBx, "Settings", "path");
            if (_changedDesignPcbMaskedTb)
                _changeParameters(DesignPcb_maskedTB.Text, "Main", "designationPcb");
            if (_changedFileDocRb)
            {
                _changeParameters(FileDoc_rb.Checked, "FileCreation", "doc");
                _changeParameters(FileXls_rb.Checked, "FileCreation", "xls");
            }
            if (_changedFileXlsRb)
            {
                _changeParameters(FileDoc_rb.Checked, "FileCreation", "doc");
                _changeParameters(FileXls_rb.Checked, "FileCreation", "xls");
            }
            if (_changedElementsOfSmdMountingChkBx)
                _changeParameters(ElementsOfSMDMounting_chkBx.Checked, "Main", "ElementsOfSMDMounting");
            if (_changedBorrowedItemsChkBx)
                _changeParameters(BorrowedItems_chkBx.Checked, "Main", "BorrowedItems");
            if (_changedPcbChkBx)
                _changeParameters(Pcb_chkBx.Checked, "Pcb", "is");
            if (_changedCertifyingSheetChkBx)
                _changeParameters(CertifyingSheet_chkBx.Checked, "CertifyingSheet", "is");
            if (_changedAssemblyDrawingChkBx)
                _changeParameters(AssemblyDrawing_chkBx.Checked, "AssemblyDrawing", "is");
            if (_changedElectricalCircuitChkBx)
                _changeParameters(ElectricalCircuit_chkBx.Checked, "ElectricalCircuit", "is");
            if (_changedListOfitemsChkBx)
                _changeParameters(ListOfitems_chkBx.Checked, "ListOfitems", "is");
            if (_changedPcbCmbBx)
                _changeParameters(Pcb_cmbBx.Text, "Pcb", "format");
            if (_changedCertifyingSheetCmbBx)
                _changeParameters(CertifyingSheet_cmbBx.Text, "CertifyingSheet", "format");
            if (_changedAssemblyDrawingCmbBx)
                _changeParameters(AssemblyDrawing_cmbBx.Text, "AssemblyDrawing", "format");
            if (_changedElectricalCircuitCmbBx)
                _changeParameters(ElectricalCircuit_cmbBx.Text, "ElectricalCircuit", "format");
            if (_changedListOfItemsCmbBx)
                _changeParameters(ListOfitems_cmbBx.Text, "ListOfitems", "format");

            if (_changeSmdDesignatorsFilePath)
            {
                _changeParameters(SmdDesignatorsFile_txBx.Text, "SMD", "path");
                for (int i = 0; i < _parameters.SmdIdentificators.Count; ++i)
                    IdentificatorsListBox.Items.Add(_parameters.SmdIdentificators[i]);
                IdentificatorsListBox.Refresh();
            }
            if (_changedListOfSmdIdentificators)
            {
                Collection<string> collection = new Collection<string>();
                foreach (string item in IdentificatorsListBox.Items)
                {
                    collection.Add(item);
                }
                _saveSmdDesignatorsListToFile(collection);
            }
            if (_changedSourcePositionMaskedTb)
                _changeParameters(SourcePosition_maskedTB.Text, "Main", "SourcePosition");
            if (_changeHat)
                _changeParameters(Hat_chkBx.Checked, "Excell", "Hat");
            if (_changeFirstPage)
                _changeParameters(FirstPage_chkBx.Checked, "Excell", "FirstPage");
            if (_changeRatingPlusName)
                _changeParameters(RatingPlusName_chkBx.Checked, "Excell", "RatingPlusName");

            ResetChanges();
            Save_btn.Enabled = false;
        }
        private void Exit_btn_Click(object sender, EventArgs e)
        {
            CloseForm(this, EventArgs.Empty);
            IsVisible(false);
        }
        private void DesignatorsFile_btn_Click(object sender, EventArgs e)
        {
            string temp = SmdDesignatorsFile_txBx.Text;

            _document = DocumentList.Settings;

            Stream myStream = null;
            openFileDialog1.InitialDirectory = @_parameters.SettingsFilePath;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Txt files (*.txt)|*.txt";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            SmdDesignatorsFile_txBx.Text = openFileDialog1.FileName;
                            SetFilepathService(_document);
                            if (temp != SmdDesignatorsFile_txBx.Text)
                            {
                                _changeSmdDesignatorsFilePath = true;
                                Save_btn.Enabled = true;
                                IdentificatorsListBox.Items.Clear();
                                _parameters.SmdIdentificators.Clear();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot find file. " + ex.Message, "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
            else
                return;
        }
        #endregion
        
        #region Auxiliary
        private void SetFilepathService(DocumentList document)
        {
            SetDocument(document, EventArgs.Empty);
            if (SetFilePath != null)
                SetFilePath(this, EventArgs.Empty);
        }
        private void PropertiesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        private void PropertiesView_Load(object sender, EventArgs e)
        {
            Save_btn.Enabled = false;
        }
        private void ResetAddBtnService()
        {
            IdentificatorTxBx.Text = "";
            AddBtn.Enabled = false;
        }
        private void PictureChange()
        {
            if (!Hat_chkBx.Checked && !FirstPage_chkBx.Checked && !RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._000;
            else if (!Hat_chkBx.Checked && !FirstPage_chkBx.Checked && RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._001;
            else if (!Hat_chkBx.Checked && FirstPage_chkBx.Checked && !RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._010;
            else if (!Hat_chkBx.Checked && FirstPage_chkBx.Checked && RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._011;
            else if (Hat_chkBx.Checked && !FirstPage_chkBx.Checked && !RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._100;
            else if (Hat_chkBx.Checked && !FirstPage_chkBx.Checked && RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._101;
            else if (Hat_chkBx.Checked && FirstPage_chkBx.Checked && !RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._110;
            else if (Hat_chkBx.Checked && FirstPage_chkBx.Checked && RatingPlusName_chkBx.Checked)
                pictureBox1.Image = Resources._111;
        }
        #endregion
        
        #region Click Events
        private void DesignPcb_maskedTB_TextChanged(object sender, EventArgs e)
        {
            _changedDesignPcbMaskedTb = true;
        }
        private void FileDoc_rb_CheckedChanged(object sender, EventArgs e)
        {
            _changedFileDocRb = true;
            Save_btn.Enabled = true;
        }
        private void FileXls_rb_CheckedChanged(object sender, EventArgs e)
        {
            _changedFileXlsRb = true;
            Save_btn.Enabled = true;
        }
        private void ElementsOfSMDMounting_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedElementsOfSmdMountingChkBx = true;
            Save_btn.Enabled = true;
        }
        private void BorrowedItems_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedBorrowedItemsChkBx = true;
            Save_btn.Enabled = true;
        }
        private void Pcb_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedPcbChkBx = true;
            Save_btn.Enabled = true;
        }
        private void CertifyingSheet_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedCertifyingSheetChkBx = true;
            Save_btn.Enabled = true;
        }
        private void AssemblyDrawing_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedAssemblyDrawingChkBx = true;
            Save_btn.Enabled = true;
        }
        private void ElectricalCircuit_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedElectricalCircuitChkBx = true;
            Save_btn.Enabled = true;
        }
        private void ListOfitems_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedListOfitemsChkBx = true;
            Save_btn.Enabled = true;
        }
        private void Pcb_cmbBx_Click(object sender, EventArgs e)
        {
            _changedPcbCmbBx = true;
            Save_btn.Enabled = true;
        }
        private void CertifyingSheet_cmbBx_Click(object sender, EventArgs e)
        {
            _changedCertifyingSheetCmbBx = true;
            Save_btn.Enabled = true;
        }
        private void AssemblyDrawing_cmbBx_Click(object sender, EventArgs e)
        {
            _changedAssemblyDrawingCmbBx = true;
            Save_btn.Enabled = true;
        }
        private void ElectricalCircuit_cmbBx_Click(object sender, EventArgs e)
        {
            _changedElectricalCircuitCmbBx = true;
            Save_btn.Enabled = true;
        }
        private void ListOfitems_cmbBx_Click(object sender, EventArgs e)
        {
            _changedListOfItemsCmbBx = true;
            Save_btn.Enabled = true;
        }
        private void IdentificatorTxBx_TextChanged(object sender, EventArgs e)
        {
            AddBtn.Enabled = true;
        }
        private void IdentificatorsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _tempIdentificatorForRemove = IdentificatorsListBox.SelectedItem.ToString();
                DelBtn.Enabled = true;
            }
            catch (Exception) { }
        }
        private void SourcePosition_mskTxBx_TextChanged(object sender, EventArgs e)
        {
            _changedSourcePositionMaskedTb = true;
            Save_btn.Enabled = true;
        }
        private void Hat_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changeHat = true;
            Save_btn.Enabled = true;
            PictureChange();
        }
        private void FirstPage_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changeFirstPage = true;
            Save_btn.Enabled = true;
            PictureChange();
        }
        private void RatingPlusName_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changeRatingPlusName = true;
            Save_btn.Enabled = true;
            PictureChange();
        }
        private void SourcePosition_maskedTB_Click(object sender, EventArgs e)
        {
            SourcePosition_maskedTB.SelectionStart = 0;
        }
        #endregion

        #region Variables

        #region Changeble Variables
        private bool _changedTemplatePath;
        private bool _changedSettingsPath;
        private bool _changedDesignPcbMaskedTb;
        private bool _changedFileDocRb;
        private bool _changedFileXlsRb;
        private bool _changedElementsOfSmdMountingChkBx;
        private bool _changedBorrowedItemsChkBx;
        private bool _changedPcbChkBx;
        private bool _changedCertifyingSheetChkBx;
        private bool _changedAssemblyDrawingChkBx;
        private bool _changedElectricalCircuitChkBx;
        private bool _changedListOfitemsChkBx;
        private bool _changedPcbCmbBx;
        private bool _changedCertifyingSheetCmbBx;
        private bool _changedAssemblyDrawingCmbBx;
        private bool _changedElectricalCircuitCmbBx;
        private bool _changedListOfItemsCmbBx;
        private bool _changedListOfSmdIdentificators;
        private bool _changedSourcePositionMaskedTb;
        private bool _changeHat;
        private bool _changeFirstPage;
        private bool _changeRatingPlusName;
        private bool _changeSmdDesignatorsFilePath;
        #endregion

        private readonly IPresenter _presenter;
        private DocumentList _document;
        private Parameters _parameters;
        private ChangeParameters _changeParameters;
        private SaveSmdDesignatorsListToFile _saveSmdDesignatorsListToFile;
        private string _tempIdentificatorForRemove;
        #endregion
    }
}
