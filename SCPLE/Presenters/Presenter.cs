using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Scple.Interface;
using Scple.Interfaces;
using Scple.Models;
using SCPLE.Properties;
using Scple.View;
using System.Xml;

namespace Scple.Presenters
{
    /// <summary>
    /// Класс Presenter
    /// </summary>
    public class Presenter : IPresenter
    {
#region IPresenter
        /// <summary>
        /// Запуск главного Представления
        /// </summary>
        public void Run()
        {
            _filePathView.Show();
        }
        /// <summary>
        /// Изменение параметров по умолчанию
        /// </summary>
        /// <param name="newData">Новая информация</param>
        /// <param name="name1">Тэг внешний</param>
        /// <param name="name2">Тэг внутренний</param>
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

                    ListBox.ObjectCollection tempCollection = null;
                    try
                    {
                        tempCollection = (ListBox.ObjectCollection)newData;
                    }
                    catch (Exception){}
                    
                    if (tempCollection != null)
                    {
                        XmlElement markerSmd = xmlDoc.CreateElement("marker");
                        markerSmd.SetAttribute("type", "SMD");

                        if (tempCollection.Count < xmlNode.Count)
                        {
                            XmlNode outer;
                            for (int j = 0; j < tempCollection.Count; ++j)
                                xmlNode.Item(j).InnerText = tempCollection[j].ToString();
                            for (int j = xmlNode.Count - tempCollection.Count + 1; j < xmlNode.Count; ++j)
                            {
                                outer = xmlNode.Item(j).ParentNode;
                                outer.RemoveChild(xmlNode.Item(j));
                            }
                        }
                        else if (tempCollection.Count > xmlNode.Count)
                        {
                            for (int j = 0; j < xmlNode.Count; ++j)
                                xmlNode.Item(j).InnerText = tempCollection[j].ToString();
                            for (int j = tempCollection.Count - xmlNode.Count + 1; j < tempCollection.Count; ++j)
                            {
                                XmlElement newElement = xmlDoc.CreateElement("id");
                                newElement.InnerText = tempCollection[j].ToString();
                                xnl.Item(i).AppendChild(newElement);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < tempCollection.Count; ++j)
                                xmlNode.Item(j).InnerText = tempCollection[j].ToString();
                        }
                        xmlDoc.Save(_parameters.SettingsFilePath);
                    }
                    else
                        for (int j = 0; j < xmlNode.Count; ++j)
                            if (xmlNode.Item(j).Name == name2)
                            {
                                xmlNode[j].InnerText = newData.ToString();
                                xmlDoc.Save(_parameters.SettingsFilePath);
                                break;
                            }
                    ReadConfigurationFile();
                }
            }
        }
        /// <summary>
        /// Запись измененного списка SMD идентификаторов в файл
        /// </summary>
        /// <param name="SmdIdentificators">Список SMD идентификаторов</param>
        public void SaveSmdDesignatorsListToFile(Collection<string> SmdIdentificators)
        {
            File.Delete(_parameters.SmdIdentificatorsFilePath);
            try
            {
                StreamWriter sw = new StreamWriter(_parameters.SmdIdentificatorsFilePath);
                foreach (string identificator in SmdIdentificators)
                    sw.WriteLine(identificator);
                sw.Close();
            }
            catch (Exception){}
        }
#endregion

#region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="view"></param>
        public Presenter(IViewFilePathMainForm view)
        {
            _modelPath = new ModelFilePath();
            _modelCreation = new ModelCreationSpecification();
            _parameters = new Parameters();
            _parameters.SmdIdentificators = new Collection<string>();

            _filePathView = view;
            _filePathView.SetFilePath += new EventHandler<EventArgs>(SetFilePath);
            _filePathView.SetDocument += new EventHandler<EventArgs>(SetDocument);
            _filePathView.CreateScView += new EventHandler<EventArgs>(CreateScView);

            _modelCreation.ChangeProgressBar +=new EventHandler<EventArgs>(ChangeProgressBar);
            _modelCreation.ChangeReadListStatusLabel += new EventHandler<EventArgs>(ChangeReadListStatusLabel);
            _modelCreation.ChangeCreateSpecStatusLabel += new EventHandler<EventArgs>(ChangeCreateSpecStatusLabel);
            _modelCreation.ChangeStatusLabel += new EventHandler<EventArgs>(ChangeStatusLabel);
            _modelCreation.ChangeStatusButton += new EventHandler<EventArgs>(ChangeStatusButton);
            _modelCreation.ChangeReadFileStatus += new EventHandler<EventArgs>(SetFileStatus);

            
            _propertiesView = new PropertiesView(this);
            _propertiesView.SetFilePath += new EventHandler<EventArgs>(SetFilePath);
            _propertiesView.SetDocument += new EventHandler<EventArgs>(SetDocument);
            _propertiesView.CloseForm += new EventHandler<EventArgs>(CloseFormPropertiesView);

            ReadConfigurationFile();
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

#region View Creation
        private void CreateScView(object sender, EventArgs e)
        {
            IViewFileCreation temp = sender as IViewFileCreation;
            if (temp != null)
                _fileCreationView = temp;
            _fileCreationView.Initialization(_parameters);

            _fileCreationView.ProcessingView += new EventHandler<EventArgs>(ProcessingView);
            _fileCreationView.StartCreating += new EventHandler<EventArgs>(StartCreating);
            _fileCreationView.CloseForm += new EventHandler<EventArgs>(CloseForm);
        }
        private void ProcessingView(object sender, EventArgs e)
        {
            if (sender is ProcessingView)
                _processingView = (IViewProcessing) sender;
            _processingView.OpenFile += new EventHandler<EventArgs>(OpenFile);
            _processingView.StopCreating += new EventHandler<EventArgs>(StopCreating);
            _modelCreation.SetParameters(_parameters);
        }
#endregion

#region Change
        private void ChangeProgressBar(object sender, EventArgs e)
        {
            _processingView.ChangeProgressBar((int)sender);
        }
        private void ChangeReadListStatusLabel(object sender, EventArgs e)
        {
            string temp = sender as string;
            if (temp == null)
                return;
            
            Color color = new Color();

            if (temp.Contains("В процессе..."))
                color = Color.Red;
            else if (temp.Contains("Готово!"))
                color = Color.Green;
            
            _processingView.ChangeReadListStatusLabel(temp, color);
        }
        private void ChangeCreateSpecStatusLabel(object sender, EventArgs e)
        {
            string temp = sender as string;
            if (temp == null)
                return;

            Color color = new Color();

            if (temp.Contains("В процессе..."))
                color = Color.Red;
            else if (temp.Contains("Готово!"))
                color = Color.Green;

            _processingView.ChangeCreateSpecStatusLabel(temp, color);
        }
        private void ChangeStatusLabel(object sender, EventArgs e)
        {
            string temp = sender as string;
            if (temp == null)
                return;

            Color color = new Color();

            if (temp.ToLower().Contains("подождите"))
                color = Color.Red;
            else if (temp.ToLower().Contains("успешно") || temp.ToLower().Contains("готово"))
                color = Color.Green;

            _processingView.ChangeStatusLabel(temp, color);
        }
        private void ChangeStatusButton(object sender, EventArgs e)
        {
            _processingView.ChangeStatusButton((string) sender);
        }
        private void SetFileStatus(object sender, EventArgs e)
        {
            _processingView.ChangeReadFileStatus((string)sender);
        }
#endregion
        
#region Close
        private void CloseForm(object sender, EventArgs e)
        {
            _filePathView.IsVisible(true);
            _filePathView.SetPropertiesView(_propertiesView);
        }
        private void CloseFormPropertiesView(object sender, EventArgs e)
        {
            _filePathView.IsVisible(true);
            _filePathView.SetPropertiesView(_propertiesView);
        }
#endregion

#region Configuration
        private void ReadConfigurationFile()
        {
            string executablePath, pathOfTemplate, pathOfSettings;

            _document = DocumentList.Template;
            _xmlDoc = new XmlDocument();

            if (IsResources(out executablePath, out pathOfSettings))
                SetParameters(pathOfSettings);
            else
            {
                CreateResourcesDir(executablePath, out pathOfTemplate);
                CreateSettingsFile(executablePath, pathOfTemplate, pathOfSettings);

                _xmlDoc.Save(pathOfSettings);
            }

            _filePathView.SpecificationTemplateFileNameTxbx = _parameters.TemplateFilePath;
        }
        private bool IsResources(out string executablePath, out string pathOfSettings)
        {
            bool isOk;
            executablePath = FindExePath.ExePath();
            pathOfSettings = executablePath.Substring(0, executablePath.LastIndexOf('\\') + 1) + "Resources\\" + "Settings.xml";
            IsCorrect(pathOfSettings, out isOk, false);
            return isOk;
        }// Определение существования Ресурсов
        private void CreateSettingsFile(string executablePath, string pathOfTemplate, string pathOfSettings)
        {
            XmlDeclaration xmlDecl = _xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            _xmlDoc.AppendChild(xmlDecl);

            XmlElement settings = _xmlDoc.CreateElement("Settings");

            CreateAttributeMain(ref settings);
            CreateAttributeSmd(executablePath, ref settings);
            CreateAttributeTemplate(ref settings, pathOfTemplate);
            CreateAttributeSettings(ref settings, pathOfSettings);
            CreateAttributeExcell(ref settings);

            _xmlDoc.AppendChild(settings);
        }

#region CreateElements
        private void CreateElementFileCreation(ref XmlElement markerMain)
        {
            XmlElement markerFileCreation = _xmlDoc.CreateElement("marker");
            markerFileCreation.SetAttribute("type", "FileCreation");
            XmlElement doc = _xmlDoc.CreateElement("doc");
            doc.InnerText = "true";
            XmlElement xls = _xmlDoc.CreateElement("xls");
            xls.InnerText = "false";
            markerFileCreation.AppendChild(doc);
            markerFileCreation.AppendChild(xls);
            markerMain.AppendChild(markerFileCreation);
        }
        private void CreateElementOtherDocuments(ref XmlElement markerMain)
        {
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
        }
        private void CreateElementElementsOfSmdMounting(ref XmlElement markerMain)
        {
            XmlElement ElementsOfSMDMounting = _xmlDoc.CreateElement("ElementsOfSMDMounting");
            ElementsOfSMDMounting.InnerText = "true";
            markerMain.AppendChild(ElementsOfSMDMounting);
        }
        private void CreateElementBorrowedItems(ref XmlElement markerMain)
        {
            XmlElement BorrowedItems = _xmlDoc.CreateElement("BorrowedItems");
            BorrowedItems.InnerText = "false";
            markerMain.AppendChild(BorrowedItems);
        }
        private void CreateElementSourcePosition(ref XmlElement markerMain)
        {
            XmlElement SourcePosition = _xmlDoc.CreateElement("SourcePosition");
            SourcePosition.InnerText = "false";
            markerMain.AppendChild(SourcePosition);
        }
#endregion

#region CreateAttributes
        private void CreateAttributeMain(ref XmlElement settings)
        {
            XmlElement markerMain = _xmlDoc.CreateElement("marker");
            markerMain.SetAttribute("type", "Main");

            XmlElement designationPcb = _xmlDoc.CreateElement("designationPcb");
            designationPcb.InnerText = "758716";
            markerMain.AppendChild(designationPcb);

            CreateElementFileCreation(ref markerMain);
            CreateElementOtherDocuments(ref markerMain);
            CreateElementElementsOfSmdMounting(ref markerMain);
            CreateElementBorrowedItems(ref markerMain);
            CreateElementSourcePosition(ref markerMain);

            settings.AppendChild(markerMain);
        }
        private void CreateAttributeSmd(string executablePath, ref XmlElement settings)
        {
            XmlElement markerSMD = _xmlDoc.CreateElement("marker");
            markerSMD.SetAttribute("type", "SMD");

            string pathOfSmdDesignatorsFile = executablePath.Substring(0, executablePath.LastIndexOf('\\') + 1) + "Resources\\" +
                                  "SMD_Designators.txt";

            if (!File.Exists(pathOfSmdDesignatorsFile))
            {
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(pathOfSmdDesignatorsFile);
                    sw.WriteLine("Chip");
                    sw.WriteLine("0805");
                    sw.WriteLine("1206");
                    sw.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    if (sw != null)
                        sw.Close();
                }
            }

            XmlElement filePath = _xmlDoc.CreateElement("path");
            filePath.InnerText = pathOfSmdDesignatorsFile;
            markerSMD.AppendChild(filePath);

            settings.AppendChild(markerSMD);
        }
        private void CreateAttributeTemplate(ref XmlElement settings, string pathOfTemplate)
        {
            XmlElement markerTemplate = _xmlDoc.CreateElement("marker");
            markerTemplate.SetAttribute("type", "Template");

            XmlElement pathTempl = _xmlDoc.CreateElement("path");
            pathTempl.InnerText = pathOfTemplate;
            markerTemplate.AppendChild(pathTempl);

            settings.AppendChild(markerTemplate);
        }
        private void CreateAttributeSettings(ref XmlElement settings, string pathOfSettings)
        {
            XmlElement markerSettings = _xmlDoc.CreateElement("marker");
            markerSettings.SetAttribute("type", "Settings");

            XmlElement pathSett = _xmlDoc.CreateElement("path");
            pathSett.InnerText = pathOfSettings;
            markerSettings.AppendChild(pathSett);

            settings.AppendChild(markerSettings);
        }
        private void CreateAttributeExcell(ref XmlElement settings)
        {
            XmlElement markerExcell = _xmlDoc.CreateElement("marker");
            markerExcell.SetAttribute("type", "Excell");

#region Hat
            XmlElement markerHat = _xmlDoc.CreateElement("Hat");
            markerHat.InnerText = "false";
            markerExcell.AppendChild(markerHat);
#endregion

#region FirstPage
            XmlElement markerFirstPage = _xmlDoc.CreateElement("FirstPage");
            markerFirstPage.InnerText = "false";
            markerExcell.AppendChild(markerFirstPage);
#endregion

#region RatingPlusName
            XmlElement markerRatingPlusName = _xmlDoc.CreateElement("RatingPlusName");
            markerRatingPlusName.InnerText = "true";
            markerExcell.AppendChild(markerRatingPlusName);
#endregion

            settings.AppendChild(markerExcell);
        }
#endregion

        private void CreateResourcesDir(string executablePath, out string pathOfTemplate)
        {
            Directory.CreateDirectory(executablePath.Substring(0, executablePath.LastIndexOf('\\') + 1) +
                                          "Resources");
            pathOfTemplate = executablePath.Substring(0, executablePath.LastIndexOf('\\') + 1) +
                                    "Resources\\" + "Template.doc";
            File.WriteAllBytes(pathOfTemplate, Resources.Template);
            string pathOfHelp = executablePath.Substring(0, executablePath.LastIndexOf('\\') + 1) + "Resources\\" +
                                "Spec-Creator_manual.chm";
            File.WriteAllBytes(pathOfHelp, Resources.Spec_Creator_manual);
        }
        private void SetParameters(string path)
        {
            _parameters.DesignDocFirstString = "";
            _parameters.DesignDocSecondString = "";
            _parameters.DesignPcbSecondString = "";

            XmlTextReader reader = new XmlTextReader(path);

            bool isEnd3 = false, isEnd4 = false, isEnd5 = false, isEnd6 = false, isEnd7 = false;
            while (true)
            {
                reader.ReadToFollowing("marker");
                reader.MoveToFirstAttribute();
                if (reader.Value == "Main")
                    ReadAttributeMain(ref reader, out isEnd3);
                if (reader.Value == "SMD")
                    ReadAttributeSmd(ref reader, out isEnd4, path);
                if (reader.Value == "Template")
                {
                    ReadAttribute(ref reader, ref _parameters.TemplateFilePath);
                    isEnd5 = true;
                }
                if (reader.Value == "Settings")
                {
                    ReadAttribute(ref reader, ref _parameters.SettingsFilePath);
                    isEnd6 = true;
                }
                if (reader.Value == "Excell")
                    ReadAttributeExcell(ref reader, out isEnd7);
                
                if (isEnd3 && isEnd4 && isEnd5 && isEnd6 && isEnd7)
                    break;
            }
            reader.Close();
        }

        private void ReadAttributeMain(ref XmlTextReader reader, out bool isEnd3)
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
                    _parameters.FileDoc = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);
                    reader.ReadToFollowing("xls");
                    _parameters.FileXls = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);
                    isEnd1 = true;
                    continue;
                }

                if (reader.Value == "OtherDocuments")
                    ReadAttributeOtherDocuments(ref reader, out isEnd2);
                
                if (isEnd1 && isEnd2) break;
            }

            reader.ReadToFollowing("ElementsOfSMDMounting");
            _parameters.ElementsOfSmdMounting = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);

            reader.ReadToFollowing("BorrowedItems");
            _parameters.BorrowedItems = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);

            reader.ReadToFollowing("SourcePosition");
            _parameters.SourcePosition = reader.ReadElementContentAsString();

            isEnd3 = true;
        }
        private void ReadAttributeSmd(ref XmlTextReader reader, out bool isEnd4, string path)
        {
            reader.ReadToFollowing("path");
            string filePath = reader.ReadElementContentAsString();

            if (_parameters.SmdIdentificators.Count == 0)
                if (File.Exists(filePath))
                    AddSmdIdentificators(ref _parameters, filePath);

            reader.Close();
            isEnd4 = true;
            reader = new XmlTextReader(path);
            while (reader.Value != "Template")
            {
                reader.ReadToFollowing("marker");
                reader.MoveToFirstAttribute();
            }
        }
        private void ReadAttributeExcell(ref XmlTextReader reader, out bool isEnd7)
        {
            reader.ReadToFollowing("Hat");
            _parameters.Hat = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);

            reader.ReadToFollowing("FirstPage");
            _parameters.FirstPage = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);

            reader.ReadToFollowing("RatingPlusName");
            _parameters.RatingPlusName = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);
            isEnd7 = true;
        }
        private void ReadAttributeOtherDocuments(ref XmlTextReader reader, out bool isEnd2)
        {
            isEnd2 = false;
            while (true)
            {
                reader.ReadToFollowing("marker");
                reader.MoveToFirstAttribute();

                if (reader.Value == "Pcb")
                    ReadAttribute(ref reader, ref _parameters.Pcb, ref _parameters.PcbFormat);
                if (reader.Value == "AssemblyDrawing")
                    ReadAttribute(ref reader, ref _parameters.AssemblyDrawing, ref _parameters.AssemblyDrawingFormat);
                if (reader.Value == "ElectricalCircuit")
                    ReadAttribute(ref reader, ref _parameters.ElectricalCircuit, ref _parameters.ElectricalCircuitFormat);
                if (reader.Value == "CertifyingSheet")
                    ReadAttribute(ref reader, ref _parameters.CertifyingSheet, ref _parameters.CertifyingSheetFormat);
                if (reader.Value == "ListOfitems")
                {
                    ReadAttribute(ref reader, ref _parameters.ListOfitems, ref _parameters.ListOfitemsFormat);
                    isEnd2 = true;
                    break;
                }
                if (isEnd2) break;
            }
        }

        private void AddSmdIdentificators(ref Parameters parameters, string filePath)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filePath);

                string line = sr.ReadLine();

                while (line != null)
                {
                    parameters.SmdIdentificators.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                parameters.SmdIdentificatorsFilePath = filePath;
            }
            catch (Exception)
            {
                sr.Close();
                parameters.SmdIdentificatorsFilePath = "";
            }
        }
        private void ReadAttribute(ref XmlTextReader reader, ref bool parameter1, ref string parameter2)
        {
            reader.ReadToFollowing("is");
            parameter1 = Convert.ToBoolean(reader.ReadElementContentAsString(), CultureInfo.CurrentCulture);
            reader.ReadToFollowing("format");
            parameter2 = reader.ReadElementContentAsString();
        }
        private void ReadAttribute(ref XmlTextReader reader, ref string parameter)
        {
            reader.ReadToFollowing("path");
            parameter = reader.ReadElementContentAsString();
        }
#endregion

#region FilePath Service
        private void IsCorrect(string fileaPath, out bool isOk, bool showError = true)
        {
            if (!_modelPath.IsCorrect(fileaPath))
            {
                isOk = false;
                if (showError)
                    _filePathView.ShowError("Invalid Path of file");
                return;
            }
            isOk = true;
        }
        private void RefreshView()
        {
            if (_document == DocumentList.List)
                _modelPath.FilePath = _filePathView.ListFileNameTxbx;
            else if (_document == DocumentList.Template)
                _modelPath.FilePath = _propertiesView.SpecificationFileTemplateTxBx;
            else if (_document == DocumentList.Settings)
                _modelPath.FilePath = _propertiesView.SettingsFileTxBx;
            else
            {
                _filePathView.ListFileNameTxbx = "";
                _filePathView.SpecificationTemplateFileNameTxbx = "";
                _modelPath.FilePath = "";
                _document = 0;
            }
        }
        private void SetFilePath(object sender, EventArgs e)
        {
            bool isOk;
            RefreshView();
            if (_document == DocumentList.List)
            {
                IsCorrect(_filePathView.ListFileNameTxbx, out isOk);
                _filePathView.IsOk(isOk, DocumentList.List);
            }
            else if (_document == DocumentList.Template)
            {
                IsCorrect(_propertiesView.SpecificationFileTemplateTxBx, out isOk);
                _filePathView.IsOk(isOk, DocumentList.Template);
            }
            else if (_document == DocumentList.Settings)
            {
                IsCorrect(_propertiesView.SettingsFileTxBx, out isOk);
                _filePathView.IsOk(isOk, DocumentList.Settings);
            }
        }
        private void SetDocument(object sender, EventArgs e)
        {
            if (sender is DocumentList)
                _document = (DocumentList)sender;
        }
#endregion

#region Specification Creation
        private void OpenFile(object sender, EventArgs e)
        {
            string temp = sender as string;
            if (temp != null)
                _modelCreation.FileService(temp);

            StopCreating(this, EventArgs.Empty);
        }

#region Start\Stop Creating
        private void StartCreating(object sender, EventArgs e)
        {
            _process = new Thread(((ProcessingView)_processingView).StartCreating);
            _process.Start();
        }
        private void StopCreating(object sender, EventArgs e)
        {
            _process.Abort();
            _modelCreation.CloseAll();
            _fileCreationView.IsEnabled();
        }
#endregion
#endregion

#region Variables
#region Model
        private readonly IModelFilePath _modelPath;
        private readonly IModelCreationSpecification _modelCreation;
        #endregion

#region View
        private readonly IViewFilePathMainForm _filePathView;
        private IViewFileCreation _fileCreationView;
        private IViewProcessing _processingView;
        private readonly PropertiesView _propertiesView;
#endregion

        private DocumentList _document;
        private Parameters _parameters;
        private XmlDocument _xmlDoc;
        private Thread _process;
#endregion
    }
}
