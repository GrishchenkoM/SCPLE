using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using SCPLE.Interface;
using SCPLE.Model;
using SCPLE.View;
using System.Xml;

namespace SCPLE.Presenter
{
    public class Presenter : IPresenter
    {
        #region Конструктор
        public Presenter(IFilePathMainFormView view)
        {
            _modelPath = new ModelFilePath();
            _modelCreation = new ModelCreationSpecification();

            

            _filePathView = view;
            
            _filePathView.IsCorrectSpecificationFilePath += () => IsCorrect(_filePathView.SpecificationTemplateFileName_txbx, out isOk);
            _filePathView.IsCorrectListFilePath += () => IsCorrect(_filePathView.ListFileName_txbx, out isOk);
            _filePathView.SetFilePath += new EventHandler<EventArgs>(SetFilePath);
            _filePathView.SetDocument += new EventHandler<EventArgs>(SetDocument);
            _filePathView.CreateScView += new EventHandler<EventArgs>(CreateScView);

            _modelCreation.ChangeProgressBar +=new EventHandler<EventArgs>(ChangeProgressBar);
            _modelCreation.ChangeReadListStatusLabel += new EventHandler<EventArgs>(ChangeReadListStatusLabel);
            _modelCreation.ChangeCreateSpecStatusLabel += new EventHandler<EventArgs>(ChangeCreateSpecStatusLabel);
            _modelCreation.ChangeStatusLabel += new EventHandler<EventArgs>(ChangeStatusLabel);
            _modelCreation.ChangeStatusButton += new EventHandler<EventArgs>(ChangeStatusButton);

            _parameters = new Parameters();
            _propertiesView = new PropertiesView(this);
            _propertiesView.SetFilePath += new EventHandler<EventArgs>(SetFilePath);
            _propertiesView.SetDocument += new EventHandler<EventArgs>(SetDocument);
            _propertiesView.CloseForm += new EventHandler<EventArgs>(CloseFormPropertiesView);
            ReadConfigurationFile();
            _propertiesView.SetParameters(_parameters);
            _propertiesView.IsVisible(false);
            _propertiesView.Show();
            _propertiesView.Hide();

            SetFilePath(this, EventArgs.Empty);
            _filePathView.SetPropertiesView(_propertiesView);
            RefreshView();
        }
        #endregion

        public void CreateScView(object sender, EventArgs e)
        {
            if (sender is IFileCreationView)
                _fileCreationView = (IFileCreationView)sender;
            _fileCreationView.Initialization(_parameters);

            _fileCreationView.ProcessingView += new EventHandler<EventArgs>(ProcessingView);
            _fileCreationView.StartCreating += new EventHandler<EventArgs>(StartCreating);
            _fileCreationView.CloseForm += new EventHandler<EventArgs>(CloseForm);
            
            //_fileCreationView.OpenFile += new EventHandler<EventArgs>(OpenFile);
        }

        public void ProcessingView(object sender, EventArgs e)
        {
            if (sender is Processing)
                _processingView = (IProcessingView) sender;
            //_processingView.Initialization(_parameters);
            _processingView.OpenFile += new EventHandler<EventArgs>(OpenFile);
            _processingView.StopCreating += new EventHandler<EventArgs>(StopCreating);
            _parameters = _processingView.Parameters;
            _modelCreation.SetParameters(_parameters);
        }

        public void ChangeProgressBar(object sender, EventArgs e)
        {
            _processingView.ChangeProgressBar((int)sender);
        }
        public void ChangeReadListStatusLabel(object sender, EventArgs e)
        {
            if (!(sender is string))
                return;

            string temp = (string) sender;
            Color color = new Color();

            if (temp.Contains("В процессе..."))
                color = Color.Red;
            else if (temp.Contains("Готово!"))
                color = Color.Green;
            
            _processingView.ChangeReadListStatusLabel((string)sender, color);
        }
        public void ChangeCreateSpecStatusLabel(object sender, EventArgs e)
        {
            if (!(sender is string))
                return;

            string temp = (string)sender;
            Color color = new Color();

            if (temp.Contains("В процессе..."))
                color = Color.Red;
            else if (temp.Contains("Готово!"))
                color = Color.Green;

            _processingView.ChangeCreateSpecStatusLabel((string)sender, color);
        }
        public void ChangeStatusLabel(object sender, EventArgs e)
        {
            if (!(sender is string))
                return;

            string temp = (string)sender;
            Color color = new Color();

            if (temp.Contains("подождите"))
                color = Color.Red;
            else if (temp.Contains("успешно") || temp.Contains("готово"))
                color = Color.Green;

            _processingView.ChangeStatusLabel((string)sender, color);
        }

        public void ChangeStatusButton(object sender, EventArgs e)
        {
            _processingView.ChangeStatusButton((string) sender);
        }
        


        public void StartCreating(object sender, EventArgs e)
        {
            process = new Thread(((Processing)_processingView).StartCreating);
            process.Start();
        }

        public void StopCreating(object sender, EventArgs e)
        {
            process.Abort();
            _modelCreation.CloseAll();

            _fileCreationView.IsEnabled(true);
        }

        public void CloseForm(object sender, EventArgs e)
        {
            _filePathView.IsEnabled(true);
            _filePathView.SetPropertiesView(_propertiesView);
        }
        public void CloseFormPropertiesView(object sender, EventArgs e)
        {
            //_propertiesView.IsVisible(false);
            _filePathView.IsEnabled(true);
            _filePathView.SetPropertiesView(_propertiesView);
        }



        #region ReadConfiguration
        private void ReadConfigurationFile()
        {
            _document = DocumentList.SPECIFICATION_TEMPLATE;
            _xmlDoc = new XmlDocument();

            bool isOk;
            IsCorrect("D:\\Settings.xml", out isOk);
            if (isOk)
                SetParameters("D:\\Settings.xml");
            else
            {
                #region Create XML
                XmlDeclaration xmlDecl = _xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                _xmlDoc.AppendChild(xmlDecl);

                #region Settings
                XmlElement settings = _xmlDoc.CreateElement("Settings");

                #region Main
                XmlElement markerMain = _xmlDoc.CreateElement("marker");
                markerMain.SetAttribute("type", "Main");

                XmlElement designationPcb = _xmlDoc.CreateElement("designationPcb");
                designationPcb.InnerText = "758716";
                markerMain.AppendChild(designationPcb);

                #region FileCreation
                XmlElement markerFileCreation = _xmlDoc.CreateElement("marker");
                markerFileCreation.SetAttribute("type", "FileCreation");
                XmlElement doc = _xmlDoc.CreateElement("doc");
                doc.InnerText = "true";
                XmlElement xls = _xmlDoc.CreateElement("xls");
                xls.InnerText = "false";
                markerFileCreation.AppendChild(doc);
                markerFileCreation.AppendChild(xls);
                markerMain.AppendChild(markerFileCreation);
                #endregion

                #region OtherDocuments
                XmlElement markerOtherDocuments = _xmlDoc.CreateElement("marker");
                markerOtherDocuments.SetAttribute("type", "OtherDocuments");

                string[] typeName = new string[5]
            {
                "Pcb", 
                "CertifyingSheet", 
                "AssemblyDrawing", 
                "ElectricalCircuit", 
                "ListOfitems"
            };

                for (int i = 0; i < typeName.Length; ++i)
                {
                    XmlElement markerPcb = _xmlDoc.CreateElement("marker");
                    markerPcb.SetAttribute("type", typeName[i]);
                    XmlElement isElement = _xmlDoc.CreateElement("is");
                    isElement.InnerText = "true";
                    XmlElement format = _xmlDoc.CreateElement("format");
                    format.InnerText = "A3";
                    markerPcb.AppendChild(isElement);
                    markerPcb.AppendChild(format);
                    markerOtherDocuments.AppendChild(markerPcb);
                }
                markerMain.AppendChild(markerOtherDocuments);
                #endregion

                #region ElementsOfSMDMounting
                XmlElement ElementsOfSMDMounting = _xmlDoc.CreateElement("ElementsOfSMDMounting");
                ElementsOfSMDMounting.InnerText = "true";
                markerMain.AppendChild(ElementsOfSMDMounting);
                #endregion

                #region BorrowedItems
                XmlElement BorrowedItems = _xmlDoc.CreateElement("BorrowedItems");
                BorrowedItems.InnerText = "false";
                markerMain.AppendChild(BorrowedItems);
                #endregion
                #endregion

                settings.AppendChild(markerMain);

                #region Template
                XmlElement markerTemplate = _xmlDoc.CreateElement("marker");
                markerTemplate.SetAttribute("type", "Template");

                XmlElement pathTempl = _xmlDoc.CreateElement("path");
                pathTempl.InnerText = "E:\\Мои документы\\Программирование\\C#\\My Projects\\Диплом\\SCPLE\\Modeling\\Template.dot";
                markerTemplate.AppendChild(pathTempl);
                #endregion

                settings.AppendChild(markerTemplate);

                #region Settings
                XmlElement markerSettings = _xmlDoc.CreateElement("marker");
                markerSettings.SetAttribute("type", "Settings");

                XmlElement pathSett = _xmlDoc.CreateElement("path");
                pathSett.InnerText = "D:\\Settings.xml";
                markerSettings.AppendChild(pathSett);
                #endregion

                settings.AppendChild(markerSettings);

                _xmlDoc.AppendChild(settings);
                #endregion

                #endregion
                _xmlDoc.Save("D:\\Settings.xml");
            }
            
            _filePathView.SpecificationTemplateFileName_txbx = _parameters.TemplateFilePath;
        }
        private void SetParameters(string path)
        {
            _parameters.DesignDocFirstString = "";
            _parameters.DesignDocSecondString = "";
            _parameters.DesignPcbSecondString = "";

            XmlTextReader reader = new XmlTextReader(path);

            bool isEnd3 = false, isEnd4 = false, isEnd5 = false;
            while (true)
            {
                reader.ReadToFollowing("marker");
                reader.MoveToFirstAttribute();
                if (reader.Value == "Main")
                {
                    bool isEnd1 = false;
                    bool isEnd2 = false;

                    reader.ReadToFollowing("designationPcb");
                    _parameters.DesignPcbFirstString = reader.ReadElementContentAsString();

                    while (true)
                    {
                        reader.ReadToFollowing("marker");
                        reader.MoveToFirstAttribute();

                        if (reader.Value == "FileCreation")
                        {
                            reader.ReadToFollowing("doc");
                            _parameters.FileDoc = Convert.ToBoolean(reader.ReadElementContentAsString());
                            reader.ReadToFollowing("xls");
                            _parameters.FileXls = Convert.ToBoolean(reader.ReadElementContentAsString());
                            isEnd1 = true;
                            continue;
                        }

                        if (reader.Value == "OtherDocuments")
                        {
                            while (true)
                            {

                                reader.ReadToFollowing("marker");
                                reader.MoveToFirstAttribute();

                                if (reader.Value == "Pcb")
                                {
                                    reader.ReadToFollowing("is");
                                    _parameters.Pcb = Convert.ToBoolean(reader.ReadElementContentAsString());
                                    reader.ReadToFollowing("format");
                                    _parameters.PcbFormat = reader.ReadElementContentAsString();
                                }

                                if (reader.Value == "AssemblyDrawing")
                                {
                                    reader.ReadToFollowing("is");
                                    _parameters.AssemblyDrawing = Convert.ToBoolean(reader.ReadElementContentAsString());
                                    reader.ReadToFollowing("format");
                                    _parameters.AssemblyDrawingFormat = reader.ReadElementContentAsString();
                                }

                                if (reader.Value == "ElectricalCircuit")
                                {
                                    reader.ReadToFollowing("is");
                                    _parameters.ElectricalCircuit = Convert.ToBoolean(reader.ReadElementContentAsString());
                                    reader.ReadToFollowing("format");
                                    _parameters.ElectricalCircuitFormat = reader.ReadElementContentAsString();
                                }

                                if (reader.Value == "CertifyingSheet")
                                {
                                    reader.ReadToFollowing("is");
                                    _parameters.CertifyingSheet = Convert.ToBoolean(reader.ReadElementContentAsString());
                                    reader.ReadToFollowing("format");
                                    _parameters.CertifyingSheetFormat = reader.ReadElementContentAsString();
                                }

                                if (reader.Value == "ListOfitems")
                                {
                                    reader.ReadToFollowing("is");
                                    _parameters.ListOfitems = Convert.ToBoolean(reader.ReadElementContentAsString());
                                    reader.ReadToFollowing("format");
                                    _parameters.ListOfitemsFormat = reader.ReadElementContentAsString();
                                    isEnd2 = true;
                                    break;
                                }
                                if (isEnd2)
                                    break;
                            }
                        }
                        if (isEnd1 && isEnd2)
                            break;
                    }
                    reader.ReadToFollowing("ElementsOfSMDMounting");
                    _parameters.ElementsOfSMDMounting = Convert.ToBoolean(reader.ReadElementContentAsString());

                    reader.ReadToFollowing("BorrowedItems");
                    _parameters.BorrowedItems = Convert.ToBoolean(reader.ReadElementContentAsString());

                    isEnd3 = true;
                }
                if (reader.Value == "Template")
                {
                    reader.ReadToFollowing("path");
                    _parameters.TemplateFilePath = reader.ReadElementContentAsString();
                    isEnd4 = true;
                }
                if (reader.Value == "Settings")
                {
                    reader.ReadToFollowing("path");
                    _parameters.SettingsFilePath = reader.ReadElementContentAsString();
                    isEnd5 = true;
                }
                if (isEnd3 && isEnd4 && isEnd5)
                    break;
            }
            reader.Close();
        }

        public void ChangeParameters(object newData, string name1, string name2)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_parameters.SettingsFilePath);

            XmlNodeList xnl = xmlDoc.DocumentElement.GetElementsByTagName("marker");
            XmlNodeList xmlNode;

            xnl = xmlDoc.DocumentElement.GetElementsByTagName("marker");
            for (int i = 0; i < xnl.Count; ++i)
            {
                if (xnl.Item(i).Attributes[0].Value == name1)
                {
                    xmlNode = xnl.Item(i).ChildNodes;

                    for (int j = 0; j < xmlNode.Count; ++j)
                        if (xmlNode.Item(j).Name == name2)
                        {
                            xmlNode[j].InnerText = newData.ToString();
                            xmlDoc.Save(_parameters.SettingsFilePath);
                            ReadConfigurationFile();
                            return;
                        }
                }
            }
        }

        #endregion

        #region FilePath Service
        private void IsCorrect(string fileaPath, out bool isOk)
        {
            if (!_modelPath.IsCorrect(fileaPath))
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
                _modelPath.FilePath = _filePathView.ListFileName_txbx;
            else if (_document == DocumentList.SPECIFICATION_TEMPLATE)
                _modelPath.FilePath = _propertiesView.SpecificationFileTemplate_txBx;
            else if (_document == DocumentList.SETTINGS)
                _modelPath.FilePath = _propertiesView.SettingsFile_txBx;
            else
            {
                _filePathView.ListFileName_txbx = "";
                _filePathView.SpecificationTemplateFileName_txbx = "";
                _modelPath.FilePath = "";
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
                _filePathView.IsOk(isOk, DocumentList.LIST);
            }
            else if (_document == DocumentList.SPECIFICATION_TEMPLATE)
            {
                IsCorrect(_propertiesView.SpecificationFileTemplate_txBx, out isOk);
                _filePathView.IsOk(isOk, DocumentList.SPECIFICATION_TEMPLATE);
            }
            else if (_document == DocumentList.SETTINGS)
            {
                IsCorrect(_propertiesView.SettingsFile_txBx, out isOk);
                _filePathView.IsOk(isOk, DocumentList.SETTINGS);
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
        #endregion

        #region Specification Creation

        // public - for testing
        public void OpenFile(object sender, EventArgs e)
        {
            //_filePathModel.FileOpen(_filePathView.ListFileName_txbx);
            if (sender is string)
                _modelCreation.FileService((string)sender);
            
            StopCreating(this, EventArgs.Empty);
        }
        #endregion

        #region Variables
        private IFilePathModel _modelPath;
        private ICreationSpecificationModel _modelCreation;

        private IFilePathMainFormView _filePathView;
        private IFileCreationView _fileCreationView;
        private IProcessingView _processingView;
        private PropertiesView _propertiesView;

        private DocumentList _document;
        private Parameters _parameters;
        private XmlDocument _xmlDoc;
        bool isOk;

        private Thread process;
        #endregion
    }
}
