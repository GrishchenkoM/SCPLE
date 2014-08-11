using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace SCPLE.Interface
{
    public interface ICreationSpecificationModel
    {
        void FileService(string path);
        void CloseAll();
        void SetParameters(Parameters parameters);
        event EventHandler<EventArgs> ChangeProgressBar;
        event EventHandler<EventArgs> ChangeReadListStatusLabel;
        event EventHandler<EventArgs> ChangeCreateSpecStatusLabel;
        event EventHandler<EventArgs> ChangeStatusLabel;
        event EventHandler<EventArgs> ChangeStatusButton;
    }
}
