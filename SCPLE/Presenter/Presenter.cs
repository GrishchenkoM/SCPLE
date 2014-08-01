using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SCPLE.Interface;
using SCPLE.Model;

namespace SCPLE.Presenter
{
    public class Presenter : IPresenter
    {
        /// <summary>
        /// В конструтор передается конкретный экземпляр представления
        /// и происходит подписка на все нужные события.
        /// </summary>
        public Presenter(IFilePathView view)
        {
            _model = new Model.Model();
            _filePathView = view;
            
            _filePathView.IsCorrectSpecificationFilePath += () => IsCorrect(_filePathView.SpecificationFileName_txbx, out isOk);
            _filePathView.IsCorrectListFilePath += () => IsCorrect(_filePathView.ListFileName_txbx, out isOk);
            _filePathView.SetFilePath += new EventHandler<EventArgs>(SetFilePath);
            _filePathView.SetDocument += new EventHandler<EventArgs>(SetDocument);
            _filePathView.OpenFile += new EventHandler<EventArgs>(OpenFile);
            RefreshView();
        }

        // public - for testing
        public void OpenFile(object sender, EventArgs e)
        {
            //_filePathModel.FileOpen(_filePathView.ListFileName_txbx);
            if (sender is string)
                _model.FileService((string)sender);
        }
       
        private void IsCorrect(string fileaPath, out bool isOk)
        {
            if (!_model.IsCorrect(fileaPath))
            {
                isOk = false;
                _filePathView.ShowError("Invalid Path of file");
                return;
            }
            isOk = true;
        }
        private void RefreshView()
        {
            if (_document == DocumentList.LIST)
                _model.FilePath = _filePathView.ListFileName_txbx;
            else if (_document == DocumentList.SPECIFICATION)
                _model.FilePath = _filePathView.SpecificationFileName_txbx;
            else
            {
                _filePathView.ListFileName_txbx = "";
                _filePathView.SpecificationFileName_txbx = "";
                _model.FilePath = "";
                _document = 0;
            }
        }
        public void SetFilePath(object sender, EventArgs e)
        {
            bool isOk;
            RefreshView();
            if (_document == DocumentList.LIST)
            {
                IsCorrect(_filePathView.ListFileName_txbx, out isOk);
                _filePathView.IsOk(isOk, true);
            }
            else
            {
                IsCorrect(_filePathView.SpecificationFileName_txbx, out isOk);
                _filePathView.IsOk(isOk, false);
            }
        }
        public void SetDocument(object sender, EventArgs e)
        {
            if (sender is DocumentList)
                _document = (DocumentList)sender;
            
        }
        public void Run()
        {
            _filePathView.Show();
        }

        private IFilePathModel _model;
        private IFilePathView _filePathView;
        private DocumentList _document;

        bool isOk;
    }
}
