using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace SCPLE.Interface
{
    public interface IFilePathView : IView
    {
        string SpecificationFileName_txbx { get; set; }
        string ListFileName_txbx { get; set; }
        event Action IsCorrectSpecificationFilePath; // пользователь пытается добавить файл
        event Action IsCorrectListFilePath; // пользователь пытается добавить файл
        event EventHandler<EventArgs> SetDocument;
        event EventHandler<EventArgs> SetFilePath;
        event EventHandler<EventArgs> OpenFile;
        void ShowError(string errorMessage);
        void IsOk(bool isOk, bool isListFile);
    }
}
