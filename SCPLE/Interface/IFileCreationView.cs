using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCPLE.View;

namespace SCPLE.Interface
{
    public interface IFileCreationView
    {
        void Initialization(Parameters parameters);
        void IsEnabled(bool isEnabled);

        event EventHandler<EventArgs> ProcessingView;
        event EventHandler<EventArgs> StartCreating;
        event EventHandler<EventArgs> CloseForm;
    }
}
