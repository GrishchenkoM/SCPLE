using System;
using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Forms;
using Scple.Interface;

namespace Scple.View
{
    #region Delegates
    delegate void CloseFormDelegate();
    delegate void ChangeProgressBarDelegate(int i);
    delegate void ChangeReadListStatusLabelDelegate(string status, Color color);
    delegate void ChangeCreateSpecStatusLabelDelegate(string status, Color color);
    delegate void ChangeStatusLabelDelegate(string status, Color color);
    delegate void ChangeStatusButtonDelegate(string name);
    delegate void ChangeLabelReadFileStatusDelegate(string name);
    #endregion

    /// <summary>
    /// Представление, отвечающее за отображение
    /// процесса создания спецификации
    /// </summary>
    public partial class ProcessingView : Form, IViewProcessing
    {
        #region IViewProcessing
        public Parameters Parameters
        {
            get { return _parameters; }
        }
        public void CloseForm()
        {
            ClosedForm();
        }
        public void StartCreating()
        {
            if (OpenFile != null)
                OpenFile(_listFilePath, EventArgs.Empty);
        }

        #region Changing
        public void ChangeProgressBar(int i)
        {
            try
            {
                Invoke(new ChangeProgressBarDelegate(SetProgressBarValue), i);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cancel"))
                    throw new Exception("Cancel");
            }
        }
        public void ChangeReadListStatusLabel(string status, Color color)
        {
            try
            {
                Invoke(new ChangeReadListStatusLabelDelegate(SetReadListStatusLabel), status, color);
            }
            catch (Exception) { }
        }
        public void ChangeCreateSpecStatusLabel(string status, Color color)
        {
            try
            {
                Invoke(new ChangeCreateSpecStatusLabelDelegate(SetCreateSpecStatusLabel), status, color);
            }
            catch (Exception) { }
        }
        public void ChangeStatusLabel(string status, Color color)
        {
            try 
            {
                Invoke(new ChangeStatusLabelDelegate(SetStatusLabel), status, color);
            }
            catch (Exception) { }
        }
        public void ChangeStatusButton(string name)
        {
            try
            {
                Invoke(new ChangeStatusButtonDelegate(ChangeButtonName), name);
            }
            catch (Exception){}
        }
        public void ChangeReadFileStatus(string name)
        {
            try
            {
                Invoke(new ChangeLabelReadFileStatusDelegate(SetReadFileStatus), name);
            }
            catch (Exception) { }
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> OpenFile;
        public event EventHandler<EventArgs> StopCreating;
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="listFilePath">Адрес файла-перечня элементов</param>
        /// <param name="parameters">Параметры создания спецификации</param>
        public ProcessingView(string listFilePath, Parameters parameters)
        {
            InitializeComponent();
            _listFilePath = listFilePath;
            _parameters = parameters;
            progressBar1.Step = -1;
            _isClosed = false;
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
        }
        #endregion

        #region Button
        private void _CancelProcessingButton_Click(object sender, EventArgs e)
        {
            if (StopCreating != null)
                StopCreating(this, EventArgs.Empty);
            
            ClosedForm();
        }
        #endregion

        #region Handlers
        private void Processing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isClosed)
            {
                _isClosed = true;
                _CancelProcessingButton_Click(sender, EventArgs.Empty);
            }
        }
        #endregion

        #region Controlls
        private void SetProgressBarValue(int i)
        {
            if (progressBar1.Step == -1)
            {
                progressBar1.Step = 1;
                progressBar1.Value = 0;

                _progressBarChangingValue = ((double)100) / i;
            }
            else if (i == -1)
            {
                progressBar1.Value = 0;
                _progressBarChangingValue = 0;
                progressBar1.Step = -1;
            }
            else
                progressBar1.Value = (int)(_progressBarChangingValue * i);
            
        }
        private void SetReadListStatusLabel(string status, Color color)
        {
            label1.Text = status;
            label1.ForeColor = color;
        }
        private void SetCreateSpecStatusLabel(string status, Color color)
        {
            label3.Text = status;
            label3.ForeColor = color;
        }
        private void SetStatusLabel(string status, Color color)
        {
            label5.Text = status;
            label5.ForeColor = color;
        }
        private void ChangeButtonName(string name)
        {
            button1.Text = "Выход";
            button1.BackColor = Color.Honeydew;
            button1.ForeColor = Color.IndianRed;
        }
        private void SetReadFileStatus(string name)
        {
            label2.Text = name;
            label4.Text = "- Создание файла спецификации";
        }
        #endregion

        #region Auxiliary
        void ClosedForm()
        {
            this.Close();
        }
        #endregion
        
        #region Variables
        private readonly Parameters _parameters;
        private readonly string _listFilePath;
        private double _progressBarChangingValue;
        private bool _isMoreThan100;
        private bool _isClosed;
        #endregion
    }
}