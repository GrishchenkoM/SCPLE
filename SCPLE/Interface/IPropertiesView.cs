using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE.Interface
{
    public interface IPropertiesView
    {
        //void ChangeParameters(
        //object newData, string name1, string name2,
        //string name3 = null, string name4 = null);

        string SpecificationFileTemplate_txBx {get; set; }
        string SettingsFile_txBx { get; set; }

        event EventHandler<EventArgs> SetDocument;
        event EventHandler<EventArgs> SetFilePath;
        event EventHandler<EventArgs> CloseForm;

        void ShowForm();
        void IsVisible(bool isVisible = true);
    }
}
