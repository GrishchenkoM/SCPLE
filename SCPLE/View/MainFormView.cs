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

namespace SCPLE
{
    public enum DocumentList
    {
        LIST,
        SPECIFICATION
    };
    public enum TaskList
    {
        SPECIFICATION_CREATION,
        COMPLIANCE_VERIFICATION,
        VALIDATION
    };
    

    public partial class MainFormView : Form, IFilePathView
    {
        #region Реализация IView

        public void Show()
        {
            Application.Run(this);
        }

        public void Close()
        {
            Application.Exit();
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage);
            InputValidation(_document);
        }

        public void IsOk(bool isOk, bool isListFile)
        {
            if (isListFile)
                _twoFilesReady[0] = isOk;
            else
                _twoFilesReady[1] = isOk;
        }

        #endregion

        #region Конструктор
        public MainFormView()
        {
            InitializeComponent();
            List_txBx.Text = openFileString;
            Specification_txBx.Text = openFileString;
            toolStripStatusLabel1.Text = "Шаг 1/2";
            _twoFilesReady = new [] {false, false};
            createSpec_rb_CheckedChanged(this, EventArgs.Empty);
            isNextStep();
        }
        #endregion

        #region Работа с Button и TextBox

        private void _listFilePathButton_Click(object sender, EventArgs e)
        {
            _document = DocumentList.LIST;
            if (ListFileName_txbx != "" && ListFileName_txbx != openFileString && List_txBx.ReadOnly == false)
            {
                SetFilepathService(_document);
            }
            else
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    ListFileName_txbx = openFileDialog1.FileName;
                else
                    return;

                SetFilepathService(_document);
            }
            isNextStep();
        }
        private void _specificationFilePathButton_Click(object sender, EventArgs e)
        {
            _document = DocumentList.SPECIFICATION;
            if (SpecificationFileName_txbx != "" && SpecificationFileName_txbx != openFileString && Specification_txBx.ReadOnly == false)
            {
                SetFilepathService(_document);
            }
            else
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    SpecificationFileName_txbx = openFileDialog1.FileName;
                else
                    return;

                SetFilepathService(_document);
            }
            isNextStep();
        }

        private void List_txBx_Enter(object sender, EventArgs e)
        {
            if (List_txBx.ReadOnly == false)
                List_txBx.Text = "";
        }
        private void List_txBx_Leave(object sender, EventArgs e)
        {
            if (List_txBx.Text == "")
                List_txBx.Text = openFileString;
        }
        private void Specification_txBx_Enter(object sender, EventArgs e)
        {
            if (Specification_txBx.ReadOnly == false)
                Specification_txBx.Text = "";
        }
        private void Specification_txBx_Leave(object sender, EventArgs e)
        {
            if (Specification_txBx.Text == "")
                Specification_txBx.Text = openFileString;
        }

        private void SetFilepathService(DocumentList document)
        {
            SetDocument(document, EventArgs.Empty);
            if (SetFilePath != null)
                SetFilePath(this, EventArgs.Empty);
            
            isNextStep();
        }

        private void isNextStep()
        {
            if (_twoFilesReady[(int)DocumentList.LIST])
            {
                label1.Text = "OK";
                label1.ForeColor = Color.Green;
                List_txBx.ReadOnly = true;
            }
            else
            {
                label1.Text = "X";
                label1.ForeColor = Color.Red;
                List_txBx.ReadOnly = false;
                List_txBx.Text = "";
            }

            if (_twoFilesReady[(int)DocumentList.SPECIFICATION])
            {
                label3.Text = "OK";
                label3.ForeColor = Color.Green;
                Specification_txBx.ReadOnly = true;
            }
            else
            {
                label3.Text = "X";
                label3.ForeColor = Color.Red;
                Specification_txBx.ReadOnly = false;
                Specification_txBx.Text = "";
            }
            
            if (((_twoFilesReady[0] && _twoFilesReady[1] == true) && _isTwoFiles)
                ||
                (_twoFilesReady[0] && (createSpec_rb.Checked == true))
                ||
                ((_twoFilesReady[1] && (validation_rb.Checked == true))))
            {
                NextForm_btn.Enabled = true;
                label5.ForeColor = Color.Green;
                label5.Text = "Всё готово! нажмите кнопку 'Далее'";
                return;
            }

            NextForm_btn.Enabled = false;
            label5.ForeColor = Color.Red;
            label5.Text = "Предоставьте необходимые данные для работы";
            List_txBx_Leave(this, EventArgs.Empty);
            Specification_txBx_Leave(this, EventArgs.Empty);
            this.Refresh();
        }

        public string SpecificationFileName_txbx
        {
            get { return Specification_txBx.Text; }
            set { Specification_txBx.Text = value; }
        }
        public string ListFileName_txbx
        {
            get { return List_txBx.Text; }
            set { List_txBx.Text = value; }
        }

        #region Мигание TextBox

        private int takt, count, max;

        private void Specification_tb_timer1_Tick(object sender, EventArgs e)
        {

            if (Specification_txBx.BackColor == Color.Red)
                Specification_txBx.BackColor = Color.White;
            else
                Specification_txBx.BackColor = Color.Red;

            count += takt;
            if (count >= max)
            {
                Specification_txBx.BackColor = Color.White;
                timer1.Stop();
                timer1.Tick -= new EventHandler(Specification_tb_timer1_Tick);
                return;
            }
        }

        private void List_tb_timer1_Tick(object sender, EventArgs e)
        {

            if (List_txBx.BackColor == Color.Red)
                List_txBx.BackColor = Color.White;
            else
                List_txBx.BackColor = Color.Red;

            count += takt;
            if (count >= max)
            {
                List_txBx.BackColor = Color.White;
                timer1.Stop();
                timer1.Tick -= new EventHandler(List_tb_timer1_Tick);
                return;
            }
        }

        private void InputValidation(DocumentList document)
        {
            takt = 100;
            timer1.Interval = takt;
            max = takt * 5;
            count = 0;

            if (document == DocumentList.LIST)
            {
                List_txBx.BackColor = Color.Red;
                timer1.Tick += new EventHandler(List_tb_timer1_Tick);
            }
            else
            {
                Specification_txBx.BackColor = Color.Red;
                timer1.Tick += new EventHandler(Specification_tb_timer1_Tick);
            }
            timer1.Start();
        }

        #endregion

        private void _NextFormButton_Click(object sender, EventArgs e)
        {
            if (OpenFile != null)
                OpenFile(ListFileName_txbx, EventArgs.Empty);
        }

        #endregion

        #region Работа с RadioButton

        private void createSpec_rb_CheckedChanged(object sender, EventArgs e)
        {
            ListPath_btn.Enabled = true;
            List_txBx.Enabled = true;
            label6.Enabled = true;
            SpecPath_btn.Enabled = false;
            Specification_txBx.Enabled = false;
            label7.Enabled = false;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = false;
            label4.Visible = false;

            _isTwoFiles = false;
            isNextStep();
        }

        private void complVerification_rb_CheckedChanged(object sender, EventArgs e)
        {
            ListPath_btn.Enabled = true;
            List_txBx.Enabled = true;
            label6.Enabled = true;
            SpecPath_btn.Enabled = true;
            Specification_txBx.Enabled = true;
            label7.Enabled = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;

            _isTwoFiles = true;
            isNextStep();
        }

        private void validation_rb_CheckedChanged(object sender, EventArgs e)
        {
            ListPath_btn.Enabled = false;
            List_txBx.Enabled = false;
            label6.Enabled = false;
            SpecPath_btn.Enabled = true;
            Specification_txBx.Enabled = true;
            label7.Enabled = true;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = true;
            label4.Visible = true;

            _isTwoFiles = false;
            isNextStep();
        }

        #endregion

        private void ChosenTask()
        {
            if (createSpec_rb.Checked == true)
                _task = TaskList.SPECIFICATION_CREATION;
            if (complVerification_rb.Checked == true)
                _task = TaskList.COMPLIANCE_VERIFICATION;
            if (validation_rb.Checked == true)
                _task = TaskList.VALIDATION;
        }

        #region Переменные
        private IPresenter _presenter;
        private IFilePathView _view;
        private DocumentList _document;
        private TaskList _task;
        private bool _isOk = false;
        private bool _isTwoFiles;
        private bool[] _twoFilesReady;
        private Thread thread;
        public string openFileString = "Введите адрес файла или нажмите кнопку 'Обзор...'";
        #endregion

        #region События
        public event Action IsCorrectSpecificationFilePath;
        public event Action IsCorrectListFilePath;
        public event EventHandler<EventArgs> SetDocument;
        public event EventHandler<EventArgs> SetFilePath;
        public event EventHandler<EventArgs> OpenFile;
        #endregion

        private void MainFormView_Load(object sender, EventArgs e)
        {
            
        }
        
    }
}
