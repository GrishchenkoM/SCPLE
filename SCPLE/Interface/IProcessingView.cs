using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SCPLE.Interface
{
    public interface IProcessingView
    {
        void ChangeProgressBar(int i);
        void ChangeReadListStatusLabel(string status, Color color);
        void ChangeCreateSpecStatusLabel(string status, Color color);
        void ChangeStatusLabel(string status, Color color);
        void ChangeStatusButton(string name);
        void CloseForm();
        Parameters Parameters { get; }
        event EventHandler<EventArgs> OpenFile;
        event EventHandler<EventArgs> StartProcessing;
        event EventHandler<EventArgs> StopCreating;
    }
}
