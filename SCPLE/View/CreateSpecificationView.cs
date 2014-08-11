using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SCPLE.Interface;

namespace SCPLE.View
{
    public partial class CreateSpecificationView : Form, IFileCreationView
    {
        delegate void IsEnableDelegate(bool isEnabled);

        public CreateSpecificationView(string ListFilePath, IFilePathMainFormView view)
        {
            InitializeComponent();
            _parameters = new Parameters();

            Init();
            DesignDoc_maskedTB.Text = "";
            DesignDoc_TB.Text = "";
            DesignPcb_maskedTB.Text = "";
            DesignPcb_TB.Text = "";
            toolStripStatusLabel1.Text = "Шаг 2/2";

            _parentView = view;
            _listFilePath = ListFilePath;
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
            DesignDoc_maskedTB.Text = "";
            DesignDoc_maskedTB.SelectionStart = 0;
            DesignDoc_TB.Text = "";
            DesignPcb_maskedTB.Text = _parameters.DesignPcbFirstString;
            DesignPcb_maskedTB.SelectionStart = 0;
            DesignPcb_TB.Text = "";
        }


        private void Start_btn_Click(object sender, EventArgs e)
        {
            Parameters parameters = new Parameters();
            parameters = _parameters;
            parameters.AssemblyDrawing = AssemblyDrawing_chkBx.Checked;
            parameters.BorrowedItems = BorrowedItems_chkBx.Checked;
            parameters.CertifyingSheet = CertifyingSheet_chkBx.Checked;
            parameters.ElectricalCircuit = ElectricalCircuit_chkBx.Checked;
            parameters.ElementsOfSMDMounting = ElementsOfSMDMounting_chkBx.Checked;
            parameters.FileDoc = FileDoc_chkBx.Checked;
            parameters.FileXls = FileXls_chkBx.Checked;
            parameters.ListOfitems = ListOfitems_chkBx.Checked;
            parameters.Pcb = Pcb_chkBx.Checked;
            parameters.DesignDocFirstString = DesignDoc_maskedTB.Text;
            parameters.DesignDocSecondString = DesignDoc_TB.Text;
            parameters.DesignPcbFirstString = DesignPcb_maskedTB.Text;
            parameters.DesignPcbSecondString = DesignPcb_TB.Text;

            _processingView = new Processing(_listFilePath, parameters);

            ProcessingView(_processingView, EventArgs.Empty);

            _processingView.Visible = true;
            this.Enabled = false;
            _processingView.Show();

            StartCreating(_processingView, EventArgs.Empty);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            //_parentView.VisibleForm(true);
            //this.Close();
            
            CloseForm(this, EventArgs.Empty);
            this.Close();
        }

        public void IsEnabled(bool isEnabled)
        {
            Invoke(new IsEnableDelegate(IsEnable), isEnabled);
        }

        void IsEnable(bool isEnabled)
        {
            this.Enabled = true;
        }


        private Parameters _parameters;
        private string _listFilePath;
        private string _specificationTemplateFilePath;
        private IFilePathMainFormView _parentView;
        public Processing _processingView;
        
        public event EventHandler<EventArgs> OpenFile;
        public event EventHandler<EventArgs> ProcessingView;
        public event EventHandler<EventArgs> StartCreating;
        public event EventHandler<EventArgs> CloseForm;

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

        
    }
}
