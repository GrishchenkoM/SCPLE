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
        SPECIFICATION,
        LIST
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

        #endregion

        public MainFormView()
        {
            InitializeComponent();
            createSpec_rb_CheckedChanged(this, EventArgs.Empty);
            List_txBx.Text = "Введите адрес файла";
            toolStripStatusLabel1.Text = "Готово!";
            
            if (!isOk)
            {
                NextForm_btn.Enabled = false;
                label5.ForeColor = Color.Red;
                label5.Text = "Предоставьте необходимые данные для работы";
            }
            else
            {
                label5.ForeColor = Color.Green;
                label5.Text = "Всё готово! нажмите кнопку 'Далее'";
            }
        }

        #region Работа с Button и TextBox

        private void _listFilePathButton_Click(object sender, EventArgs e)
        {
            _document = DocumentList.LIST;
            if (ListFileName_txbx != "" && ListFileName_txbx != "Введите адрес файла")
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
                ListFileName_txbx = "";
            }
        }
        private void _specificationFilePathButton_Click(object sender, EventArgs e)
        {
            _document = DocumentList.SPECIFICATION;
            if (SpecificationFileName_txbx != "" && SpecificationFileName_txbx != "Введите адрес файла")
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
                SpecificationFileName_txbx = "";
            }
        }

        private void _list_tb_MouseClick(object sender, MouseEventArgs e)
        {
            ListFileName_txbx = "";
        }
        private void _specification_tb_MouseClick(object sender, MouseEventArgs e)
        {
            SpecificationFileName_txbx = "";
        }

        private void SetFilepathService(DocumentList document)
        {
            SetDocument(document, EventArgs.Empty);
            if (SetFilePath != null)
                SetFilePath(this, EventArgs.Empty);
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

        #endregion

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

        private void InputValidation(DocumentList document)
        {
            if (document == DocumentList.LIST)
            {
                List_txBx.BackColor = Color.Red;
                //timer1.Tick += new EventHandler(FilePath_tb_timer1_Tick);
                timer1.Start();
            }
            else
            {
                Specification_txBx.BackColor = Color.Red;
                //timer1.Tick += new EventHandler(FilePath_tb_timer1_Tick);
                timer1.Start();
            }


        }


        private IPresenter _presenter;
        private IFilePathView _view;
        private DocumentList _document;
        private TaskList _task;
        private bool isOk = false;
        public event Action IsCorrectSpecificationFilePath;
        public event Action IsCorrectListFilePath;
        public event EventHandler<EventArgs> SetDocument;
        public event EventHandler<EventArgs> SetFilePath;

        private Thread thread;


    }
}
