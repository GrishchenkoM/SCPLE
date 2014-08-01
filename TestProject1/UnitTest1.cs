
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCPLE;
using SCPLE.Interface;
using SCPLE.Presenter;
using NUnit.Framework;
using NUnit.Mocks;
using System.Windows.Forms;

namespace SCPLETestProject
{
    [TestFixture]
    public class UnitTest1
    {
        private IFilePathView _filePathView;
        private Presenter _presenter;

        [Test]
        public void SetSpecificationFileCorrect()
        {
            _filePathView = new MainFormView();
            _filePathView.SpecificationFileName_txbx = "D\\1.txt";
            _presenter = new Presenter(_filePathView);
            _presenter.SetFilePath(_filePathView, EventArgs.Empty);
        }
        [Test]
        public void SetListFileCorrect()
        {
            _filePathView = new MainFormView();
            _filePathView.SpecificationFileName_txbx = "D\\2.txt";
            _presenter = new Presenter(_filePathView);
            _presenter.SetFilePath(_filePathView, EventArgs.Empty);
        }
        [Test]
        public void CreateDocFile()
        {
            _presenter = new Presenter(new MainFormView());
            _presenter.OpenFile("D:\\Плата задатчика тока рекуперации ПЭ3 V21.doc",EventArgs.Empty);
            //_presenter.OpenFile("D:\\ПЭ3 EX70-01правленный.doc", EventArgs.Empty);
            //_presenter.OpenFile("D:\\ПЭ 122Х6.doc", EventArgs.Empty);
        }
    }
}
