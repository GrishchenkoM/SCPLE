using Scple;
using Scple.Interface;
using Scple.Interfaces;
using Scple.Presenters;
using NUnit.Framework;

namespace SCPLETestProject
{
    [TestFixture]
    public class PresenterUnitTest
    {
        private IViewFilePathMainForm _filePathView;
        private Presenter _presenter;
        
        [SetUp]
        public void Presenter()
        {
            _filePathView = new MainFormView();
            _filePathView.SpecificationTemplateFileNameTxbx = "D\\1.txt";
            _presenter = new Presenter(_filePathView);
        }
    }
}