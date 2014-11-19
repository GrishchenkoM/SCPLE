using System;
using NUnit.Framework;
using Scple.Models;

namespace SCPLETestProject
{
    [TestFixture]
    public class ModelPatternDeterminationUnitTest
    {
        private ModelPatternDetermination modelPatternDetermination;

        [SetUp]
        public void ModelPatternDetermination()
        {
            modelPatternDetermination = new ModelPatternDetermination();
        }

        [TestCase(InitWord.IsDocument.LIST)]
        public void IsListReturnTrue(InitWord.IsDocument isDocument)
        {
            InitWord.InitializeTable(isDocument);
            Assert.True(modelPatternDetermination.IsList(InitWord.Table, 1));
            InitWord.Close();
        }

        [TestCase(InitWord.IsDocument.SPECIFICATION)]
        public void IsSpecificationReturnTrue(InitWord.IsDocument isDocument)
        {
            InitWord.InitializeTable(isDocument);
            Assert.True(modelPatternDetermination.IsSpecification(InitWord.Table, 1));
            InitWord.Close();
        }

        [TestCase(InitWord.IsDocument.LIST)]
        public void IsSpecificationReturnFalse(InitWord.IsDocument isDocument)
        {
            InitWord.InitializeTable(isDocument);
            Assert.False(modelPatternDetermination.IsSpecification(InitWord.Table, 1));
            InitWord.Close();
        }

        [TestCase(InitWord.IsDocument.SPECIFICATION)]
        public void IsListReturnFalse(InitWord.IsDocument isDocument)
        {
            InitWord.InitializeTable(isDocument);
            Assert.False(modelPatternDetermination.IsList(InitWord.Table, 1));
            InitWord.Close();
        }
        
    }
}
