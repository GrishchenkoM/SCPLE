using Scple;
using Scple.Interface;
using NUnit.Framework;
using Scple.Presenters;

namespace SCPLETestProject
{
    [TestFixture]
    public class UnitTest1
    {
        private IViewFilePathMainForm _filePathView;
        private Presenter _presenter;
        //private PresenterCreatioForm _presenterCreation;

        [Test]
        public void SetSpecificationFileCorrect()
        {
            _filePathView = new MainFormView();
            _filePathView.SpecificationTemplateFileNameTxbx = "D\\1.txt";
            _presenter = new Presenter(_filePathView);
            //_presenter.SetFilePath(_filePathView, EventArgs.Empty);
        }
        [Test]
        public void SetListFileCorrect()
        {
            _filePathView = new MainFormView();
            _filePathView.SpecificationTemplateFileNameTxbx = "D\\2.txt";
            _presenter = new Presenter(_filePathView);
            //_presenter.SetFilePath(_filePathView, EventArgs.Empty);
        }
        [Test]
        public void CreateDocFile()
        {
            _presenter = new Presenter(new MainFormView());
            
            //_presenter.OpenFile("D:\\ПЭ3 EX70-01правленный.doc", EventArgs.Empty);
            
        }
    }
}
