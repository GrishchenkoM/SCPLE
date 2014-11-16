using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Scple.Interface;
using Scple.View;

namespace Scple
{
    /// <summary>
    /// Перечень используемых документов
    /// </summary>
    public enum DocumentList
    {
        List,
        Template,
        Settings
    };
    
    /// <summary>
    /// Главное Представление, в котором указывается путь к файлу-перечню элементов,
    /// а так же имеется доступ к общим настройкам программы по умолчанию
    /// </summary>
    public partial class MainFormView : Form, IViewFilePathMainForm
    {
        #region IView
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
            MessageBox.Show(errorMessage, "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
            InputValidation(_document);
        }
        #endregion
        
        #region IViewFilePathMainForm
        #region Methods
        public string SpecificationTemplateFileNameTxbx
        {
            get { return SpecificationTemplate_txBx.Text; }
            set { SpecificationTemplate_txBx.Text = value; }
        }
        public string ListFileNameTxbx
        {
            get { return List_txBx.Text; }
            set { List_txBx.Text = value; }
        }
        public void IsOk(bool isOk, DocumentList documentList)
        {
            if (documentList == DocumentList.List)
                _threeFilesReady[0] = isOk;
            else if (documentList == DocumentList.Template)
                _threeFilesReady[1] = isOk;
            else if (documentList == DocumentList.Settings)
                _threeFilesReady[2] = isOk;

        }
        public void SetPropertiesView(IViewProperties propertiesView)
        {
            _propertiesView = propertiesView;
        }
        public void IsVisible(bool isVisible)
        {
            this.Visible = isVisible;
        }
        #endregion

        #region События
        public event EventHandler<EventArgs> SetDocument;
        public event EventHandler<EventArgs> SetFilePath;
        public event EventHandler<EventArgs> CreateScView;
        #endregion
        #endregion
        
        #region Constructor
        public MainFormView()
        {
            InitializeComponent();
            List_txBx.Text = _openFileString;
            toolStripStatusLabel1.Text = "Шаг 1/2 - Выбор файла перечня элементов";
            _threeFilesReady = new [] {false, false, false};
            _openFileString = "Введите адрес файла или нажмите кнопку 'Обзор...'";
            isNextStep();
        }
        #endregion

        #region Button
        private void _listFilePathButton_Click(object sender, EventArgs e)
        {
            _document = DocumentList.List;

            Stream myStream = null;
            openFileDialog1.InitialDirectory = @"D:\";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "doc files (*.doc)|*.doc|docx files (*.docx)|*.docx";

            if (ListFileNameTxbx != null && ListFileNameTxbx.Length != 0 && ListFileNameTxbx != _openFileString && List_txBx.ReadOnly == false)
                SetFilepathService(_document);
            else
            {
                if (ListFileNameTxbx != null && ListFileNameTxbx.Length != 0)
                    openFileDialog1.InitialDirectory = @ListFileNameTxbx;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                ListFileNameTxbx = openFileDialog1.FileName;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cannot find file. Data: " + ex.Message,
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

                SetFilepathService(_document);
            }
            isNextStep();
        }

        private void _NextFormButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.ForeColor = Color.Red;
            toolStripStatusLabel1.Text = "Проверка корректности файла...";
            label5.ForeColor = Color.Red;
            label5.Text = "Подождите...";
            Cursor.Current = Cursors.WaitCursor;
            this.Refresh();
            ListPath_btn.Enabled = false;
            NextForm_btn.Enabled = false;
            _newCreation = new CreateSpecificationView(ListFileNameTxbx);
            CreateScView(_newCreation, EventArgs.Empty);

            _newCreation.Visible = true;
            this.Hide();
            _newCreation.Show();

            ListPath_btn.Enabled = true;
            NextForm_btn.Enabled = true;
            toolStripStatusLabel1.ForeColor = SystemColors.ControlText;
            Cursor.Current = Cursors.Default;
            label5.ForeColor = Color.Green;
            label5.Text = "Всё готово! нажмите кнопку 'Далее'";
            toolStripStatusLabel1.Text = "Шаг 1/2 - Выбор файла перечня элементов";
            this.Refresh();
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _propertiesView.ShowForm();
            this.Hide();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper helper = new Helper();
        }
        #endregion

        #region TextBox
        private void List_txBx_Enter(object sender, EventArgs e)
        {
            if (List_txBx.ReadOnly == false)
                List_txBx.Text = "";
        }
        private void List_txBx_Leave(object sender, EventArgs e)
        {
            if (List_txBx.Text != null && List_txBx.Text.Length == 0)
                List_txBx.Text = _openFileString;
        }

        #region Мигание TextBox
        private int takt, count, max;
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

            if (document == DocumentList.List)
            {
                List_txBx.BackColor = Color.Red;
                timer1.Tick += new EventHandler(List_tb_timer1_Tick);
            }
            timer1.Start();
        }
        #endregion
        #endregion
        
        #region Label
        private void isNextStep()
        {
            if (_threeFilesReady[(int)DocumentList.List])
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

            if (_threeFilesReady[(int)DocumentList.Template])
            {
                label3.Text = "OK";
                label3.ForeColor = Color.Green;
                SpecificationTemplate_txBx.ReadOnly = true;
            }
            else
            {
                label3.Text = "X";
                label3.ForeColor = Color.Red;
                SpecificationTemplate_txBx.ReadOnly = false;
                SpecificationTemplate_txBx.Text = "";
            }

            if (label1.Text == "OK" && label3.Text == "OK")
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
            this.Refresh();
        }
        #endregion

        #region Auxiliary
        private void SetFilepathService(DocumentList document)
        {
            SetDocument(document, EventArgs.Empty);
            if (SetFilePath != null)
                SetFilePath(this, EventArgs.Empty);

            isNextStep();
        }
        private void MainFormView_Load(object sender, EventArgs e)
        {
            isNextStep();
        }
        #endregion

        #region Variables
        private IViewProperties _propertiesView;
        private CreateSpecificationView _newCreation;
        private DocumentList _document;
        private readonly bool[] _threeFilesReady;
        private readonly string _openFileString;
        #endregion
    }
}
