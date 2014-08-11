using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;

namespace SCPLE.Interface
{
    public interface IFilePathMainFormView : IView
    {
        string SpecificationTemplateFileName_txbx { get; set; }
        string ListFileName_txbx { get; set; }
        event Action IsCorrectSpecificationFilePath; // пользователь пытается добавить файл
        event Action IsCorrectListFilePath; // пользователь пытается добавить файл
        event EventHandler<EventArgs> SetDocument;
        event EventHandler<EventArgs> SetFilePath;
        event EventHandler<EventArgs> CreateScView;

        void IsOk(bool isOk, DocumentList documentList);
        void IsEnabled(bool isEnabled);
        void VisibleForm(bool value);
        void SetPropertiesView(IPropertiesView propertiesView);
    }
}
