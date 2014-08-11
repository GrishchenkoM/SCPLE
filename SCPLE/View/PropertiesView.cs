using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SCPLE.Interface;

namespace SCPLE.View
{
    delegate void ChangeParameters(
        object newData, string name1, string name2);

    public partial class PropertiesView : Form, IPropertiesView
    {
        #region Constructor & Initialization
        public PropertiesView(SCPLE.Presenter.Presenter presenter)
        {
            InitializeComponent();
            _changedTemplatePath = false;
            _changedSettingsPath = false;
            _presenter = presenter;
            Save_btn.Enabled = false;
        }

        public void SetParameters(Parameters parameters)
        {
            Initialization(parameters);
        }

        public void Initialization(Parameters parameters)
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
            ElementsOfSMDMounting_chkBx.Checked = _parameters.ElementsOfSMDMounting;
            FileDoc_chkBx.Checked = _parameters.FileDoc;
            FileXls_chkBx.Checked = _parameters.FileXls;
            ListOfitems_chkBx.Checked = _parameters.ListOfitems;
            Pcb_chkBx.Checked = _parameters.Pcb;
            DesignPcb_maskedTB.Text = _parameters.DesignPcbFirstString;
            DesignPcb_maskedTB.SelectionStart = 0;

            AssemblyDrawing_cmbBx.Text = _parameters.AssemblyDrawingFormat;
            CertifyingSheet_cmbBx.Text = _parameters.CertifyingSheetFormat;
            ElectricalCircuit_cmbBx.Text = _parameters.ElectricalCircuitFormat;
            ListOfitems_cmbBx.Text = _parameters.ListOfitemsFormat;
            Pcb_cmbBx.Text = _parameters.PcbFormat;

            SpecificationFileTemplate_txBx = _parameters.TemplateFilePath;
            SettingsFile_txBx = _parameters.SettingsFilePath;
            resetChanges();
        }

        private void resetChanges()
        {
            _changedTemplatePath = false;
            _changedSettingsPath = false;
            _changedDesignPcb_maskedTB = false;
            _changedFileDoc_chkBx = false;
            _changedFileXls_chkBx = false;
            _changedElementsOfSMDMounting_chkBx = false;
            _changedBorrowedItems_chkBx = false;
            _changedPcb_chkBx = false;
            _changedCertifyingSheet_chkBx = false;
            _changedAssemblyDrawing_chkBx = false;
            _changedElectricalCircuit_chkBx = false;
            _changedListOfitems_chkBx = false;
            _changedPcb_cmbBx = false;
            _changedCertifyingSheet_cmbBx = false;
            _changedAssemblyDrawing_cmbBx = false;
            _changedElectricalCircuit_cmbBx = false;
            _changedListOfitems_cmbBx = false;
        }

        #endregion

        #region Buttons
        private void SpecificationTemplatePath_btn_Click(object sender, EventArgs e)
        {
            string temp = SpecificationTemplate_txBx.Text;

            _document = DocumentList.SPECIFICATION_TEMPLATE;

            Stream myStream = null;
            openFileDialog1.InitialDirectory = @"D:\";
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
                    MessageBox.Show("Cannot find file. " + ex.Message);
                    return;
                }
            }
            else
                return;
        }
        private void SettingsPath_btn_Click(object sender, EventArgs e)
        {
            string temp = Settings_txBx.Text;

            _document = DocumentList.SETTINGS;

            Stream myStream = null;
            openFileDialog1.InitialDirectory = @"D:\";
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
                    MessageBox.Show("Cannot find file. " + ex.Message);
                    return;
                }
            }
            else
                return;
        }
        private void Save_btn_Click(object sender, EventArgs e)
        {
            ChangeParameters changeParameters = new ChangeParameters(_presenter.ChangeParameters);
            
            if (_changedTemplatePath)
                changeParameters(SpecificationFileTemplate_txBx, "Template", "path");
            if (_changedSettingsPath)
                changeParameters(SettingsFile_txBx, "Settings", "path");
            if (_changedDesignPcb_maskedTB)
                changeParameters(DesignPcb_maskedTB.Text, "Main", "designationPcb");
            if (_changedFileDoc_chkBx)
                changeParameters(FileDoc_chkBx.Checked, "FileCreation", "doc");
            if (_changedFileXls_chkBx)
                changeParameters(FileXls_chkBx.Checked, "FileCreation", "xls");
            if (_changedElementsOfSMDMounting_chkBx)
                changeParameters(ElementsOfSMDMounting_chkBx.Checked, "Main", "ElementsOfSMDMounting");
            if (_changedBorrowedItems_chkBx)
                changeParameters(BorrowedItems_chkBx.Checked, "Main", "BorrowedItems");
            if (_changedPcb_chkBx)
                changeParameters(Pcb_chkBx.Checked, "Pcb", "is");
            if (_changedCertifyingSheet_chkBx)
                changeParameters(CertifyingSheet_chkBx.Checked, "CertifyingSheet", "is");
            if (_changedAssemblyDrawing_chkBx)
                changeParameters(AssemblyDrawing_chkBx.Checked, "AssemblyDrawing", "is");
            if (_changedElectricalCircuit_chkBx)
                changeParameters(ElectricalCircuit_chkBx.Checked, "ElectricalCircuit", "is");
            if (_changedListOfitems_chkBx)
                changeParameters(ListOfitems_chkBx.Checked, "ListOfitems", "is");
            if (_changedPcb_cmbBx)
                changeParameters(Pcb_cmbBx.Text, "Pcb", "format");
            if (_changedCertifyingSheet_cmbBx)
                changeParameters(CertifyingSheet_cmbBx.Text, "CertifyingSheet", "format");
            if (_changedAssemblyDrawing_cmbBx)
                changeParameters(AssemblyDrawing_cmbBx.Text, "AssemblyDrawing", "format");
            if (_changedElectricalCircuit_cmbBx)
                changeParameters(ElectricalCircuit_cmbBx.Text, "ElectricalCircuit", "format");
            if (_changedListOfitems_cmbBx)
                changeParameters(ListOfitems_cmbBx.Text, "ListOfitems", "format");

            resetChanges();
            Save_btn.Enabled = false;
        }
        private void Exit_btn_Click(object sender, EventArgs e)
        {
            CloseForm(this, EventArgs.Empty);
            IsVisible(false);
        }
        #endregion

        private void SetFilepathService(DocumentList document)
        {
            SetDocument(document, EventArgs.Empty);
            if (SetFilePath != null)
                SetFilePath(this, EventArgs.Empty);
        }

        public void ShowForm()
        {
            this.Show();
            IsVisible();
        }

        public void IsVisible(bool isVisible = true)
        {
            this.Visible = isVisible;
        }

        public string SpecificationFileTemplate_txBx
        {
            get { return SpecificationTemplate_txBx.Text; }
            set { SpecificationTemplate_txBx.Text = value; }
        }
        public string SettingsFile_txBx
        {
            get { return Settings_txBx.Text; }
            set { Settings_txBx.Text = value; }
        }

        #region Variables

        #region Changeble Variables
        private bool _changedTemplatePath;
        private bool _changedSettingsPath;
        private bool _changedDesignPcb_maskedTB;
        private bool _changedFileDoc_chkBx;
        private bool _changedFileXls_chkBx;
        private bool _changedElementsOfSMDMounting_chkBx;
        private bool _changedBorrowedItems_chkBx;
        private bool _changedPcb_chkBx;
        private bool _changedCertifyingSheet_chkBx;
        private bool _changedAssemblyDrawing_chkBx;
        private bool _changedElectricalCircuit_chkBx;
        private bool _changedListOfitems_chkBx;
        private bool _changedPcb_cmbBx;
        private bool _changedCertifyingSheet_cmbBx;
        private bool _changedAssemblyDrawing_cmbBx;
        private bool _changedElectricalCircuit_cmbBx;
        private bool _changedListOfitems_cmbBx;
        #endregion

        private IPresenter _presenter;
        private DocumentList _document;
        public string openFileString = "Введите адрес файла или нажмите кнопку 'Обзор...'";
        private Parameters _parameters;

        #endregion

        #region Events
        public event EventHandler<EventArgs> SetDocument;
        public event EventHandler<EventArgs> SetFilePath;
        public event EventHandler<EventArgs> CloseForm;
        #endregion

        #region Click Events
        private void DesignPcb_maskedTB_TextChanged(object sender, EventArgs e)
        {
            _changedDesignPcb_maskedTB = true;
        }
        private void FileDoc_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedFileDoc_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void FileXls_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedFileXls_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void ElementsOfSMDMounting_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedElementsOfSMDMounting_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void BorrowedItems_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedBorrowedItems_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void Pcb_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedPcb_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void CertifyingSheet_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedCertifyingSheet_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void AssemblyDrawing_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedAssemblyDrawing_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void ElectricalCircuit_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedElectricalCircuit_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void ListOfitems_chkBx_CheckedChanged(object sender, EventArgs e)
        {
            _changedListOfitems_chkBx = true;
            Save_btn.Enabled = true;
        }
        private void Pcb_cmbBx_Click(object sender, EventArgs e)
        {
            _changedPcb_cmbBx = true;
            Save_btn.Enabled = true;
        }
        private void CertifyingSheet_cmbBx_Click(object sender, EventArgs e)
        {
            _changedCertifyingSheet_cmbBx = true;
            Save_btn.Enabled = true;
        }
        private void AssemblyDrawing_cmbBx_Click(object sender, EventArgs e)
        {
            _changedAssemblyDrawing_cmbBx = true;
            Save_btn.Enabled = true;
        }
        private void ElectricalCircuit_cmbBx_Click(object sender, EventArgs e)
        {
            _changedElectricalCircuit_cmbBx = true;
            Save_btn.Enabled = true;
        }
        private void ListOfitems_cmbBx_Click(object sender, EventArgs e)
        {
            _changedListOfitems_cmbBx = true;
            Save_btn.Enabled = true;
        }
        #endregion

        private void PropertiesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void PropertiesView_Load(object sender, EventArgs e)
        {
            Save_btn.Enabled = false;
        }


        




    }
}
