using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE.Interface
{
    public interface IPresenter
    {
        void Run();
        void ChangeParameters(object newData, string name1, string name2);
    }
}
