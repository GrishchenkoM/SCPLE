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
            _filePathModel = new FilePathModel();
            _filePathView = view;

            _filePathView.IsCorrectSpecificationFilePath += () => IsCorrect(_filePathView.SpecificationFileName_txbx);
            _filePathView.IsCorrectListFilePath += () => IsCorrect(_filePathView.ListFileName_txbx);
            _filePathView.SetFilePath += new EventHandler<EventArgs>(SetFilePath);
            _filePathView.SetDocument += new EventHandler<EventArgs>(SetDocument);
            RefreshView();
        }
       
        private void IsCorrect(string fileaPath)
        {
            if (!_filePathModel.IsCorrect(fileaPath))
                _filePathView.ShowError("Invalid Path of file");
        }

        public void SetFilePath(object sender, EventArgs e)
        {
            RefreshView();
            if (_document == DocumentList.LIST)
                IsCorrect(_filePathView.ListFileName_txbx);
            else
                IsCorrect(_filePathView.SpecificationFileName_txbx);
        }

        public void SetDocument(object sender, EventArgs e)
        {
            if (sender is DocumentList)
                _document = (DocumentList)sender;
            
        }
        
        private void RefreshView()
        {
            //if (_document == 0) return;
            if (_document == DocumentList.LIST)
                _filePathModel.FilePath = _filePathView.ListFileName_txbx;
            else if (_document == DocumentList.SPECIFICATION)
                _filePathModel.FilePath = _filePathView.SpecificationFileName_txbx;
            else
            {
                _filePathView.ListFileName_txbx = "";
                _filePathView.SpecificationFileName_txbx = "";
                _filePathModel.FilePath = "";
                _document = 0;
            }
        }

        public void Run()
        {
            _filePathView.Show();
        }

        private IFilePathModel _filePathModel;
        private IFilePathView _filePathView;
        private DocumentList _document;
    }
}
