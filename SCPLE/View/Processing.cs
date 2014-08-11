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
    delegate void ChangeProgressBarDelegate(int i);
    delegate void ChangeReadListStatusLabelDelegate(string status, Color color);
    delegate void ChangeCreateSpecStatusLabelDelegate(string status, Color color);
    delegate void ChangeStatusLabelDelegate(string status, Color color);
    delegate void CloseFormDelegate();
    delegate void ChangeStatusButtonDelegate(string name);

    public partial class Processing : Form, IProcessingView
    {
        public Processing(string listFilePath, Parameters parameters)
        {
            InitializeComponent();

            _listFilePath = listFilePath;
            _parameters = parameters;
            progressBar1.Step = -1;
            isClosed = false;
        }

        public void StartCreating()
        {
            if (OpenFile != null)
                OpenFile(_listFilePath, EventArgs.Empty);
        }
        private void _CancelProcessingButton_Click(object sender, EventArgs e)
        {
            if (StopCreating != null)
                StopCreating(this, EventArgs.Empty);
            
            ClosedForm();
        }

        public void CloseForm()
        {
            //Invoke(new CloseFormDelegate(ClosedForm));
            ClosedForm();
        }
        void ClosedForm()
        {
            this.Close();
        }

        public void ChangeProgressBar(int i)
        {
            Invoke(new ChangeProgressBarDelegate(SetProgressBarValue), i);
        }
        void SetProgressBarValue(int i)
        {
            if (progressBar1.Step == -1)
            {
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                if (i > 100)
                {
                    _progressBarChangingValue = i / 100;
                    isMoreThan100 = true;
                }
                else
                {
                    _progressBarChangingValue = 100 / i;
                    isMoreThan100 = false;
                }
            }
            else if (i == -1)
            {
                progressBar1.Value = 0;
                _progressBarChangingValue = 0;
                progressBar1.Step = -1;
            }
            else
            {
                if (isMoreThan100)
                {
                    if (i % _progressBarChangingValue == 0)
                        if (progressBar1.Value < 100)
                            ++progressBar1.Value;
                }
                else
                    progressBar1.Value += _progressBarChangingValue;
            }
        }
        public void ChangeReadListStatusLabel(string status, Color color)
        {
            Invoke(new ChangeReadListStatusLabelDelegate(SetReadListStatusLabel), status, color);
        }
        void SetReadListStatusLabel(string status, Color color)
        {
            label1.Text = status;
            label1.ForeColor = color;
        }
        public void ChangeCreateSpecStatusLabel(string status, Color color)
        {
            Invoke(new ChangeCreateSpecStatusLabelDelegate(SetCreateSpecStatusLabel), status, color);
        }
        void SetCreateSpecStatusLabel(string status, Color color)
        {
            label3.Text = status;
            label3.ForeColor = color;
        }
        public void ChangeStatusLabel(string status, Color color)
        {
            Invoke(new ChangeStatusLabelDelegate(SetStatusLabel), status, color);
        }
        void SetStatusLabel(string status, Color color)
        {
            label5.Text = status;
            label5.ForeColor = color;
        }

        public void ChangeStatusButton(string name)
        {
            Invoke(new ChangeStatusButtonDelegate(ChangeButtonName), name);
        }

        void ChangeButtonName(string name)
        {
            button1.Text = "Выход";
        }

        public Parameters Parameters
        {
            get { return _parameters; }
        }

        #region Events
        public event EventHandler<EventArgs> OpenFile;
        public event EventHandler<EventArgs> StartProcessing;
        public event EventHandler<EventArgs> StopCreating;
        #endregion

        #region Variables

        private string _listFilePath;
        private Parameters _parameters;
        private int _progressBarChangingValue;
        private bool isMoreThan100;

        #endregion

        private void Processing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosed)
            {
                isClosed = true;
                _CancelProcessingButton_Click(sender, EventArgs.Empty);
            }
        }

        private bool isClosed;
    }
}
