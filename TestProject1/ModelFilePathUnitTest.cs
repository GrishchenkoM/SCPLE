using Scple;
using Scple.Interface;
using NUnit.Framework;
using Scple.Interfaces;
using Scple.Models;
using Scple.Presenters;
using Scple.View;

namespace SCPLETestProject
{
    [TestFixture]
    public class ModelFilePathUnitTest
    {
        private ModelFilePath modelFilePath;
        //private PresenterCreatioForm _presenterCreation;

        //[Test]
        //public void SetSpecificationFileCorrect()
        //{
            
        //    //_presenter.SetFilePath(_filePathView, EventArgs.Empty);
        //}
        //[Test]
        //public void SetListFileCorrect()
        //{
        //    _filePathView = new MainFormView();
        //    _filePathView.SpecificationTemplateFileNameTxbx = "D\\2.txt";
        //    _presenter = new Presenter(_filePathView);
        //    //_presenter.SetFilePath(_filePathView, EventArgs.Empty);
        //}

        [SetUp]
        public void ModelFilePath()
        {
            modelFilePath = new ModelFilePath();
        }

        [TestCase(InitWord.IsDocument.LIST)]
        [TestCase(InitWord.IsDocument.SPECIFICATION)]
        [TestCase(InitWord.IsDocument.UNKNOWN)]
        public void IsCorrectFilePathReturnTrue(InitWord.IsDocument isDocument)
        {
            Assert.True(modelFilePath.IsCorrect(InitWord.FilePath(isDocument)));
        }

        [TestCase(InitWord.IsDocument.NOTCREATED)]
        public void IsCorrectFilePathReturnFalse(InitWord.IsDocument isDocument)
        {
            Assert.False(modelFilePath.IsCorrect(InitWord.FilePath(isDocument)));
        }
    }
}
